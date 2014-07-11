Imports Newtonsoft.Json

Namespace Providers.JDC
 Public Class JDCDataView
  Inherits JDCRequest

  Public Property Url As String = "http://meteo.jdc.ch/API/?Action=DataView&serial={0}&duration={1}"

  Public Sub New(stationCode As String, retrievalPeriod As Integer)
   Url = String.Format(Url, stationCode, retrievalPeriod)
  End Sub

  <JsonProperty("data")>
  Public Property Data As JDCStationData

 End Class
End Namespace
