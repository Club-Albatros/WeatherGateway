Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data

Namespace Data

 Partial Public Class SqlDataProvider

  Public Overrides Function GetLatestMeasurements() As IDataReader
   Return CType(SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner & ObjectQualifier & ModuleQualifier & "GetLatestMeasurements"), IDataReader)
  End Function

  Public Overrides Function GetStation(stationId As Int32, provider As String, stationCode As String, sMSCode As String) As IDataReader
   Return CType(SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner & ObjectQualifier & ModuleQualifier & "GetStation", stationId, provider, stationCode, sMSCode), IDataReader)
  End Function

  Public Overrides Function GetStations() As IDataReader
   Return CType(SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner & ObjectQualifier & ModuleQualifier & "GetStations"), IDataReader)
  End Function

 End Class

End Namespace
