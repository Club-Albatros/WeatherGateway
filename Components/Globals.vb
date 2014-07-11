Public Class Globals

 Public Const SharedResourcesFile As String = "~/DesktopModules/Albatros/WeatherGateway/App_LocalResources/SharedResources.resx"

 Public Shared Function UnixTimeStampToDateTime(timeStamp As Long) As DateTime
  Dim res As New DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)
  res = res.AddSeconds(timeStamp).ToLocalTime()
  Return res
 End Function

End Class
