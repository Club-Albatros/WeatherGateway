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

Namespace Entities.Stations

 Partial Public Class StationsController

  Public Shared Function GetStation(stationId As Int32, provider As String, stationCode As String, smsCode As String) As StationInfo

   Return CType(CBO.FillObject(DataProvider.Instance().GetStation(stationId, provider, stationCode, smsCode), GetType(StationInfo)), StationInfo)

  End Function

 Public Shared Function AddStation(ByRef objStation As StationInfo) As Integer

  objStation.StationId = CType(DataProvider.Instance().AddStation(objStation.Altitude, objStation.Code, objStation.Deco, objStation.LastSuccessfulRetrieval, objStation.LatestStatus, objStation.Latitude, objStation.Longitude, objStation.Name, objStation.Provider, objStation.SMSCode, objStation.ViewOrder), Integer)
  Return objStation.StationId

 End Function

  Public Shared Sub UpdateStation(objStation As StationInfo)

  DataProvider.Instance().UpdateStation(objStation.Altitude, objStation.Code, objStation.Deco, objStation.LastSuccessfulRetrieval, objStation.LatestStatus, objStation.Latitude, objStation.Longitude, objStation.Name, objStation.Provider, objStation.SMSCode, objStation.StationId, objStation.ViewOrder)

  End Sub

  Public Shared Sub DeleteStation(stationId As Int32)

   DataProvider.Instance().DeleteStation(stationId)

  End Sub

 End Class
End Namespace

