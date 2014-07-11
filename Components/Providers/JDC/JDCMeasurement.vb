Imports Newtonsoft.Json

Namespace Providers.JDC
 Public Class JDCMeasurement
  Inherits JDCBaseMeasurement

  <JsonProperty("date")>
  Public Property Datime As DateTime

 End Class
End Namespace
