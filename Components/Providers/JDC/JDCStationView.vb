Imports Newtonsoft.Json

Namespace Providers.JDC
 Public Class JDCStationView
  Inherits JDCRequest

  Public Property Url As String = "http://meteo.jdc.ch/API/?Action=StationView"

  Public Sub New()
  End Sub
  Public Sub New(stationSerial As String)
   If Not String.IsNullOrEmpty(stationSerial) Then
    Url &= "&serial=" & stationSerial
   End If
  End Sub

  <JsonProperty("Stations")>
  Public Property Stations As New List(Of JDCStation)

 End Class
End Namespace
