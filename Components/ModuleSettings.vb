Imports System.Linq
Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Common.Utilities.DictionaryExtensions
Imports Albatros.DNN.Modules.WeatherGateway.Entities.Stations

Public Class ModuleSettings

#Region " Properties "
 Private Property ModuleId As Integer = -1
 Private Property Settings As Hashtable

 Private _stationsToShow As String = ""
 Private _stationsToShowBuff As Dictionary(Of Integer, StationInfo)
 Public Property StationsToShow() As Dictionary(Of Integer, StationInfo)
  Get
   If _stationsToShowBuff Is Nothing Then
    _stationsToShowBuff = StationsController.GetStationDictionary(_stationsToShow)
   End If
   Return _stationsToShowBuff
  End Get
  Set(ByVal value As Dictionary(Of Integer, StationInfo))
   _stationsToShowBuff = value
   _stationsToShow = String.Join(";", _stationsToShowBuff.Select(Function(x) x.Value.StationID.ToString).ToArray)
  End Set
 End Property

 Public Property SMSGatewayUsername As String = ""
 Public Property SMSGatewayPassword As String = ""
#End Region

#Region " Constructors "
 Public Sub New(moduleId As Integer)

  _ModuleId = moduleId
  _Settings = (New DotNetNuke.Entities.Modules.ModuleController).GetModuleSettings(moduleId)
  _stationsToShow = _Settings.GetValue(Of String)("StationsToShow", _stationsToShow)
  SMSGatewayUsername = _Settings.GetValue(Of String)("SMSGatewayUsername", SMSGatewayUsername)
  SMSGatewayPassword = _Settings.GetValue(Of String)("SMSGatewayPassword", SMSGatewayPassword)

 End Sub
#End Region

#Region " Public Members "
 Public Sub SaveSettings()

  Dim objModules As New ModuleController
  objModules.UpdateModuleSetting(ModuleId, "StationsToShow", _stationsToShow)
  objModules.UpdateModuleSetting(ModuleId, "SMSGatewayUsername", SMSGatewayUsername)
  objModules.UpdateModuleSetting(ModuleId, "SMSGatewayPassword", SMSGatewayPassword)
  DotNetNuke.Common.Utilities.DataCache.SetCache(CacheKey(ModuleId), Me)

 End Sub

 Public Shared Function GetSettings(moduleId As Integer) As ModuleSettings

  Dim res As ModuleSettings = Nothing
  Try
   res = CType(DotNetNuke.Common.Utilities.DataCache.GetCache(CacheKey(moduleId)), ModuleSettings)
  Catch ex As Exception
  End Try
  If res Is Nothing Then
   res = New ModuleSettings(moduleId)
   DotNetNuke.Common.Utilities.DataCache.SetCache(CacheKey(moduleId), res)
  End If
  Return res

 End Function

 Public Sub ClearCache()
  DotNetNuke.Common.Utilities.DataCache.ClearCache(CacheKey(ModuleId))
 End Sub

 Public Shared Function CacheKey(moduleId As Integer) As String
  Return String.Format("SettingsModule{0}", moduleId)
 End Function
#End Region

End Class
