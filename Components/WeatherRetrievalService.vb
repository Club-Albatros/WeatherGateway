Imports DotNetNuke.Services.Exceptions

Imports Albatros.DNN.Modules.WeatherGateway.Providers.JDC
Imports Albatros.DNN.Modules.WeatherGateway.Entities.Stations
Imports Albatros.DNN.Modules.WeatherGateway.Entities.Measurements

Public Class WeatherRetrievalService
 Inherits DotNetNuke.Services.Scheduling.SchedulerClient

 Public Sub New(objScheduleHistoryItem As DotNetNuke.Services.Scheduling.ScheduleHistoryItem)
  MyBase.new()
  Me.ScheduleHistoryItem = objScheduleHistoryItem
 End Sub

 Public Overrides Sub DoWork()

  Dim log As New StringBuilder

  Try

   ' Calculate schedule timelapse in seconds
   Dim timelapse As Integer = 600 ' ten minutes default
   Select Case ScheduleHistoryItem.TimeLapseMeasurement
    Case "s"
     timelapse = ScheduleHistoryItem.TimeLapse
    Case "m"
     timelapse = ScheduleHistoryItem.TimeLapse * 60
    Case "h"
     timelapse = ScheduleHistoryItem.TimeLapse * 3600
    Case "d"
     timelapse = ScheduleHistoryItem.TimeLapse * 86400
   End Select

   Dim stationsToFollow As Dictionary(Of Integer, StationInfo) = StationsController.GetStationsToFollow
   Dim stationsOnline As New List(Of Integer)

   ' JDC server
   Dim data As JDCStationView = JDCWeatherProvider.GetLatestMeasurements
   If data.ErrorMessage = "OK" Then
    For Each ws As JDCStation In data.Stations

     ' Get the station
     Dim station As StationInfo = StationsController.GetStation(-1, "JDC", ws.Serial.ToString, "")
     If station Is Nothing Then
      station = New StationInfo With {.Altitude = ws.Altitude, .Provider = "JDC", .Code = ws.Serial.ToString, .Latitude = ws.Latitude, .Longitude = ws.Longitude, .Name = ws.Name}
      StationsController.AddStation(station)
     Else
      With station ' just in case there were any changes
       .Altitude = ws.Altitude
       .Latitude = ws.Latitude
       .Longitude = ws.Longitude
       .Name = ws.Name
      End With
     End If

     stationsOnline.Add(station.StationId)

     If stationsToFollow.ContainsKey(station.StationId) Then

      ' Find out how long the station hasn't been downloaded
      Dim secsSinceLastRetrieval As Integer = 86400
      If Not station.LastSuccessfulRetrieval = DateTime.MinValue Then
       secsSinceLastRetrieval = CInt(Now.Subtract(station.LastSuccessfulRetrieval).TotalSeconds)
      End If

      ' What is the oldest measurement we have retrieved already?
      Dim oldestRecord As DateTime = Now
      For Each m As JDCMeasurement In ws.Measurements
       If m.Datime < oldestRecord Then
        oldestRecord = m.Datime
       End If
      Next
      Dim oldestRecordSeconds As Integer = CInt(Now.Subtract(oldestRecord).TotalSeconds)

      ' Now decide if we need to load a full history or not
      If secsSinceLastRetrieval > 1.5 * timelapse And oldestRecordSeconds < secsSinceLastRetrieval Then ' we failed to retrieve in between values so we will do a full retrieval
       Try
        Dim stationData As JDCDataView = JDCWeatherProvider.GetStationMeasurements(station.Code, secsSinceLastRetrieval)
        If stationData.ErrorMessage = "OK" Then
         For Each m As JDCDataMeasurement In stationData.Data.Measurements
          Dim measurement As New MeasurementInfo With {.Datime = Globals.UnixTimeStampToDateTime(m.UnixTime), .Humidity = m.Humidity, .Pressure = m.Pressure, .StationId = station.StationId, .Temperature = m.Temperature, .WindAverage = m.WindAverage, .WindDirection = m.WindDirection, .WindMaximum = m.WindMaximum}
          MeasurementsController.AddMeasurement(measurement)
         Next
        Else
         log.AppendFormat("Error retrieving JDC StationData for station {0} for period {1}: {2}" & vbCrLf, station.Code, secsSinceLastRetrieval, data.ErrorMessage)
        End If
       Catch ex As Exception
        log.AppendFormat("Error retrieving JDC StationData for station {0} for period {1}: {2}" & vbCrLf, station.Code, secsSinceLastRetrieval, ex.Message)
        log.AppendLine(ex.StackTrace)
       End Try
      End If

      For Each m As JDCMeasurement In ws.Measurements
       Dim measurement As New MeasurementInfo With {.Datime = m.Datime, .Humidity = m.Humidity, .Pressure = m.Pressure, .StationId = station.StationId, .Temperature = m.Temperature, .WindAverage = m.WindAverage, .WindDirection = m.WindDirection, .WindMaximum = m.WindMaximum}
       MeasurementsController.AddMeasurement(measurement)
      Next

     End If ' follow station

     ' Update station last retrieval time
     station.LatestStatus = ws.Status
     station.LastSuccessfulRetrieval = Now
     StationsController.UpdateStation(station)
     log.AppendFormat("Finished retrieving JDC Station {0} ({1})" & vbCrLf, station.Name, station.Code)

    Next

    For Each station As StationInfo In stationsToFollow.Values
     If stationsOnline.Contains(station.StationId) Then
      If station.LatestStatus <> "online" Then
       station.LatestStatus = "online"
       StationsController.UpdateStation(station)
      End If
     Else
      If station.LatestStatus = "online" Then
       station.LatestStatus = "offline"
       StationsController.UpdateStation(station)
      End If
     End If
    Next

   Else
    log.AppendFormat("Error retrieving JDC StationView: {0}" & vbCrLf, data.ErrorMessage)
   End If

   Me.ScheduleHistoryItem.Succeeded = True
   Me.ScheduleHistoryItem.AddLogNote(log.ToString.Replace(vbCrLf, "<br />"))

  Catch ex As Exception

   Me.ScheduleHistoryItem.Succeeded = False
   Me.ScheduleHistoryItem.AddLogNote("Service failed: " & ex.Message & "(" & ex.StackTrace & ")<br />" & log.ToString.Replace(vbCrLf, "<br />"))
   Me.Errored(ex)
   LogException(ex)

  End Try

 End Sub

End Class
