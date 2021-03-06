Imports System
Imports DotNetNuke

Namespace Data

 Partial Public MustInherit Class DataProvider

  Public MustOverride Function GetLatestMeasurements() As IDataReader
  Public MustOverride Function GetStation(stationId As Int32, provider As String, stationCode As String, sMSCode As String) As IDataReader
  Public MustOverride Function GetStationData(stationID As Int32, hours As Int32) As IDataReader
  Public MustOverride Function GetStations() As IDataReader
  Public MustOverride Function GetStationsToFollow() As IDataReader
  Public MustOverride Sub ReorderStations(portalId As Int32, stationOrder As String)

 End Class

End Namespace

