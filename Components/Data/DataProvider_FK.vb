Imports System
Imports DotNetNuke

Namespace Data

 Partial Public MustInherit Class DataProvider

#Region " Measurement Methods "
	Public MustOverride Function GetMeasurementsByStation(stationId As Int32 , pageIndex As Int32, pageSize As Int32, orderBy As String) As IDataReader
#End Region

#Region " Station Methods "
#End Region

 End Class

End Namespace

