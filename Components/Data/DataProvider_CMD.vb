Imports System
Imports DotNetNuke

Namespace Data

 Partial Public MustInherit Class DataProvider

  Public MustOverride Function GetLatestMeasurements() As IDataReader
  Public MustOverride Function GetStation(stationId As Int32, provider As String, stationCode As String, sMSCode As String) As IDataReader
  Public MustOverride Function GetStations() As IDataReader

 End Class

End Namespace

