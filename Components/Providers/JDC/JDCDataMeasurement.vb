Imports Newtonsoft.Json

Namespace Providers.JDC
 Public Class JDCDataMeasurement
  Inherits JDCBaseMeasurement

  <JsonProperty("unix-time")>
  Public Property UnixTime As Long

 End Class
End Namespace
