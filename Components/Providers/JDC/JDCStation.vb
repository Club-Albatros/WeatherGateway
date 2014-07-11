Imports Newtonsoft.Json

Namespace Providers.JDC
 Public Class JDCStation

  <JsonProperty("altitude")>
  Public Property Altitude As Integer

  <JsonProperty("expiration-date")>
  Public Property ExpirationDate As DateTime

  <JsonProperty("last-measurements")>
  Public Property Measurements As New List(Of JDCMeasurement)

  <JsonProperty("latitude")>
  Public Property Latitude As Single

  <JsonProperty("longitude")>
  Public Property Longitude As Single

  <JsonProperty("name")>
  Public Property Name As String

  <JsonProperty("sequence")>
  Public Property Sequence As Integer

  <JsonProperty("serial")>
  Public Property Serial As Integer

  <JsonProperty("shortname")>
  Public Property ShortName As String

  <JsonProperty("short-name")>
  Public Property ShortName2 As String

  <JsonProperty("status")>
  Public Property Status As String

  <JsonProperty("timezone")>
  Public Property TimeZone As String

 End Class
End Namespace
