Imports Newtonsoft.Json

Namespace Providers.JDC
 Public MustInherit Class JDCBaseMeasurement

  <JsonProperty("humidity")>
  Public Property Humidity As Single

  <JsonProperty("pressure")>
  Public Property Pressure As Single

  <JsonProperty("temperature")>
  Public Property Temperature As Single

  <JsonProperty("wind-average")>
  Public Property WindAverage As Single

  <JsonProperty("wind-direction")>
  Public Property WindDirection As Integer

  <JsonProperty("wind-maximum")>
  Public Property WindMaximum As Single

 End Class
End Namespace
