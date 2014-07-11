Imports System.Net
Imports Newtonsoft.Json

Namespace Providers.JDC
 Public Class JDCWeatherProvider

  Public Shared Function GetLatestMeasurements() As JDCStationView
   Dim res As New JDCStationView
   Using w As New WebClient
    Dim returnData As String = ""
    Try
     returnData = w.DownloadString(res.Url)
    Catch ex As Exception
     res.ErrorMessage = ex.Message
    End Try
    If Not String.IsNullOrEmpty(returnData) Then
     res = JsonConvert.DeserializeObject(Of JDCStationView)(returnData)
    End If
   End Using
   Return res
  End Function

  Public Shared Function GetStationMeasurements(stationCode As String, retrievalPeriod As Integer) As JDCDataView
   Dim res As New JDCDataView(stationCode, retrievalPeriod)
   Using w As New WebClient
    Dim returnData As String = ""
    Try
     returnData = w.DownloadString(res.Url)
    Catch ex As Exception
     res.ErrorMessage = ex.Message
    End Try
    If Not String.IsNullOrEmpty(returnData) Then
     res = JsonConvert.DeserializeObject(Of JDCDataView)(returnData)
    End If
   End Using
   Return res
  End Function

 End Class
End Namespace
