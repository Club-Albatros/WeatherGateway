Imports System
Imports System.Data
Imports System.Linq
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

Namespace Entities.Stations

 Partial Public Class StationsController

  Public Shared Function GetStations() As List(Of StationInfo)
   Return DotNetNuke.Common.Utilities.CBO.FillCollection(Of StationInfo)(DataProvider.Instance().GetStations())
  End Function

  Public Shared Function GetStationsToFollow() As Dictionary(Of Integer, StationInfo)
   Return DotNetNuke.Common.Utilities.CBO.FillDictionary(Of Integer, StationInfo)("StationId", DataProvider.Instance().GetStationsToFollow())
  End Function

  Public Shared Function GetStationDictionary(stations As String) As Dictionary(Of Integer, StationInfo)
   Dim stationList As List(Of StationInfo) = GetStations()
   Dim res As New Dictionary(Of Integer, StationInfo)
   If Not String.IsNullOrEmpty(stations) Then
    For Each stationId As String In stations.Split(";"c)
     Dim sId As Integer = Integer.Parse(stationId)
     Dim station As StationInfo = stationList.First(Function(s) s.StationId = sId)
     res.Add(station.StationId, station)
    Next
   End If
   Return res
  End Function

 End Class
End Namespace

