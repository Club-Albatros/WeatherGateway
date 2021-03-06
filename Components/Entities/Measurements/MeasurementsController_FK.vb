Imports System
Imports System.Data
Imports System.Xml
Imports System.Xml.Schema
Imports System.Xml.Serialization

Imports DotNetNuke
Imports DotNetNuke.Common
Imports DotNetNuke.Common.Utilities
Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Entities.Portals
Imports DotNetNuke.Services.Tokens

Imports Albatros.DNN.Modules.WeatherGateway.Data

Namespace Entities.Measurements

 Partial Public Class MeasurementsController

  Public Shared Function GetMeasurementsByStation(stationId As Int32 ) As List(Of MeasurementInfo)

   Return DotNetNuke.Common.Utilities.CBO.FillCollection(Of MeasurementInfo)(DataProvider.Instance().GetMeasurementsByStation(stationId, 0, 1, ""))

  End Function

  Public Shared Function GetMeasurementsByStation(stationId As Int32 , pageIndex As Int32, pageSize As Int32, orderBy As String, ByRef totalRecords As Integer) As List(Of MeasurementInfo)

   If pageIndex < 0 Then
    pageIndex = 0
    pageSize = Integer.MaxValue
   End If

   Dim res As New List(Of MeasurementInfo)
   Using ir As IDataReader = DataProvider.Instance().GetMeasurementsByStation(stationId, pageIndex, pageSize, orderBy)
    res = DotNetNuke.Common.Utilities.CBO.FillCollection(Of MeasurementInfo)(ir, totalRecords)
   End Using
   Return Res

  End Function


 End Class
End Namespace

