Imports Newtonsoft.Json

Namespace Providers.JDC
 Public Class JDCStationData

  <JsonProperty("duration")>
  Public Property Duration As Integer

  <JsonProperty("name")>
  Public Property Name As String

  <JsonProperty("serial")>
  Public Property Serial As Integer

  <JsonProperty("shortname")>
  Public Property ShortName As String

  <JsonProperty("start-time")>
  Public Property StartTime As DateTime

  <JsonProperty("status")>
  Public Property Status As String

  <JsonProperty("measurements")>
  Public Property Measurements As List(Of JDCDataMeasurement)

 End Class
End Namespace
