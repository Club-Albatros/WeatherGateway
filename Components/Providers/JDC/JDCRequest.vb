Imports Newtonsoft.Json

Namespace Providers.JDC
 Public MustInherit Class JDCRequest

  <JsonProperty("Action")>
  Public Property Action As String

  <JsonProperty("APIVersion")>
  Public Property APIVersion As String

  <JsonProperty("APIRequestsRemaining")>
  Public Property APIRequestsRemaining As Integer

  <JsonProperty("ERROR")>
  Public Property ErrorMessage As String

  <JsonProperty("IP")>
  Public Property IP As String

  <JsonProperty("ServerName")>
  Public Property ServerName As String

  <JsonProperty("ServerAddress")>
  Public Property ServerAddress As String

 End Class
End Namespace
