Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data

Namespace Data

 Partial Public Class SqlDataProvider

#Region " Measurement Methods "
	Public Overrides Function GetMeasurementsByStation(stationId As Int32 , StartRowIndex As Integer, MaximumRows As Integer, OrderBy As String) As IDataReader
		Return CType(SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner & ObjectQualifier & ModuleQualifier & "GetMeasurementsByStation", stationId, StartRowIndex, MaximumRows, OrderBy.ToUpper), IDataReader)
	End Function

#End Region

#Region " Station Methods "
#End Region

 End Class

End Namespace
