Imports System
Imports System.Runtime.Serialization

Namespace Entities.Measurements
  Partial Public Class MeasurementInfo

#Region " Private Members "
#End Region
	
#Region " Constructors "
  Public Sub New()
  End Sub

  Public Sub New(measurementId As Int32, datime As Date, humidity As Double, pressure As Double, stationId As Int32, temperature As Double, windAverage As Double, windDirection As Int32, windMaximum As Double)
   Me.Datime = datime
   Me.Humidity = humidity
   Me.MeasurementId = measurementId
   Me.Pressure = pressure
   Me.StationId = stationId
   Me.Temperature = temperature
   Me.WindAverage = windAverage
   Me.WindDirection = windDirection
   Me.WindMaximum = windMaximum
  End Sub
#End Region
	
#Region " Public Properties "
  <DataMember()>
  Public Property Datime As Date = DateTime.Now
  <DataMember()>
  Public Property Humidity As Double = 0
  <DataMember()>
  Public Property MeasurementId As Int32 = -1
  <DataMember()>
  Public Property Pressure As Double = 0
  <DataMember()>
  Public Property StationId As Int32 = -1
  <DataMember()>
  Public Property Temperature As Double = 0
  <DataMember()>
  Public Property WindAverage As Double = 0
  <DataMember()>
  Public Property WindDirection As Int32 = 0
  <DataMember()>
  Public Property WindMaximum As Double = 0
#End Region

 End Class
End Namespace


