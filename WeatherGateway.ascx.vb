Imports Albatros.DNN.Modules.WeatherGateway.Entities.Measurements

Public Class WeatherGateway
 Inherits ModuleBase

 Public Property LatestMeasurements As Dictionary(Of Integer, MeasurementInfo)

 Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

  LatestMeasurements = MeasurementsController.GetLatestMeasurements

  rpStations.DataSource = Settings.StationsToShow.Values
  rpStations.DataBind()

  AddWeatherGatewayService()

 End Sub

 Public Function LatestMeasurement(stationId As Integer) As MeasurementInfo

  If LatestMeasurements.ContainsKey(stationId) Then
   Return LatestMeasurements(stationId)
  Else
   Return New MeasurementInfo ' make sure UI doesn't throw errors
  End If

 End Function

End Class