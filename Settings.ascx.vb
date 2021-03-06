Imports System
Imports System.Web.UI.WebControls
Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports Albatros.DNN.Modules.WeatherGateway.Entities.Stations

Partial Public Class Settings
 Inherits DotNetNuke.Entities.Modules.ModuleSettingsBase

#Region " Properties "
 Private _settings As Albatros.DNN.Modules.WeatherGateway.ModuleSettings
 Public Shadows Property Settings() As Albatros.DNN.Modules.WeatherGateway.ModuleSettings
  Get

   If _settings Is Nothing Then
    _settings = Albatros.DNN.Modules.WeatherGateway.ModuleSettings.GetSettings(ModuleId)
   End If
   Return _settings

  End Get
  Set(ByVal Value As Albatros.DNN.Modules.WeatherGateway.ModuleSettings)
   _settings = Value
  End Set
 End Property
#End Region

#Region " Page Events "
 Private Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init

  cblStations.DataSource = StationsController.GetStations
  cblStations.DataBind()

 End Sub
#End Region

#Region " Base Method Implementations "
 Public Overrides Sub LoadSettings()
  Try

   For Each itm As ListItem In cblStations.Items
    itm.Selected = Settings.StationsToShow.ContainsKey(Integer.Parse(itm.Value))
   Next
   txtSMSUsername.Text = Settings.SMSGatewayUsername
   txtSMSPassword.Text = Settings.SMSGatewayPassword

  Catch exc As Exception
   ProcessModuleLoadException(Me, exc)
  End Try
 End Sub

 Public Overrides Sub UpdateSettings()
  Try

   Dim res As New List(Of String)
   For Each itm As ListItem In cblStations.Items
    If itm.Selected Then
     res.Add(itm.Value)
    End If
   Next
   Settings.StationsToShow = StationsController.GetStationDictionary(String.Join(";", res))
   If txtSMSPassword.Text.Trim <> "" Then
    Settings.SMSGatewayPassword = txtSMSPassword.Text.Trim
   End If
   Settings.SMSGatewayUsername = txtSMSUsername.Text.Trim
   Settings.SaveSettings()

  Catch exc As Exception
   ProcessModuleLoadException(Me, exc)
  End Try
 End Sub

#End Region

End Class


