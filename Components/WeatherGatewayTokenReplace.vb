Imports DotNetNuke.Common.Utilities
Imports DotNetNuke.Services.Tokens
Imports Albatros.DNN.Modules.WeatherGateway.Entities.Stations
Imports Albatros.DNN.Modules.WeatherGateway.Entities.Measurements

Public Class WeatherGatewayTokenReplace
 Inherits BaseCustomTokenReplace

 Public Sub New(station As StationInfo, measurement As MeasurementInfo)

  Me.PropertySource.Add("station", station)
  Me.PropertySource.Add("measurement", measurement)

 End Sub

 Public Shadows Function ReplaceTokens(ByVal strSourceText As String) As String
  Return MyBase.ReplaceTokens(strSourceText)
 End Function

End Class
