Imports DotNetNuke.Framework
Imports DotNetNuke.Web.Client.ClientResourceManagement

Public Class ModuleBase
 Inherits DotNetNuke.Entities.Modules.PortalModuleBase

#Region " Properties "
 Private _security As ContextSecurity
 Public Property Security() As ContextSecurity
  Get
   If _security Is Nothing Then
    Dim cacheKey As String = String.Format("Security-{0}-{1}", ModuleId, UserId)
    _security = CType(DotNetNuke.Common.Utilities.DataCache.GetCache(cacheKey), ContextSecurity)
    If _security Is Nothing Then
     _security = New ContextSecurity(Me.ModuleConfiguration, UserInfo)
     DotNetNuke.Common.Utilities.DataCache.SetCache(cacheKey, _security)
    End If
   End If
   Return _security
  End Get
  Set(ByVal value As ContextSecurity)
   _security = value
  End Set
 End Property

 Private _settings As ModuleSettings
 Public Shadows Property Settings() As ModuleSettings
  Get

   If _settings Is Nothing Then
    _settings = ModuleSettings.GetSettings(ModuleId)
   End If
   Return _settings

  End Get
  Set(ByVal Value As ModuleSettings)
   _settings = Value
  End Set
 End Property
#End Region

#Region " Event Handlers "
 Private Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

  If Context.Items("WeatherGatewayModuleBaseInitialized") Is Nothing Then

   jQuery.RequestRegistration()
   Dim script As New StringBuilder
   script.AppendLine("<script type=""text/javascript"">")
   script.AppendLine("//<![CDATA[")
   script.AppendLine(String.Format("var appPath='{0}'", DotNetNuke.Common.ApplicationPath))
   script.AppendLine("//]]>")
   script.AppendLine("</script>")
   DotNetNuke.UI.Utilities.ClientAPI.RegisterClientScriptBlock(Page, "WeatherGatewayAppPath", script.ToString)
   AddWeatherGatewayService()

   Context.Items("WeatherGatewayModuleBaseInitialized") = True
  End If

 End Sub
#End Region

#Region " Public Methods "
 Public Sub AddWeatherGatewayService()

  If Context.Items("WeatherGatewayServiceAdded") Is Nothing Then
   DotNetNuke.Framework.jQuery.RequestDnnPluginsRegistration()
   DotNetNuke.Framework.ServicesFramework.Instance.RequestAjaxScriptSupport()
   DotNetNuke.Framework.ServicesFramework.Instance.RequestAjaxAntiForgerySupport()
   AddJavascriptFile("albatros.weathergateway.js", 70)
   Context.Items("WeatherGatewayServiceAdded") = True
  End If

 End Sub

 Public Sub AddJavascriptFile(jsFilename As String, priority As Integer)
  ClientResourceManager.RegisterScript(Page, ResolveUrl("~/DesktopModules/Albatros/WeatherGateway/js/" & jsFilename), priority)
 End Sub

 Public Sub AddCssFile(cssFilename As String)
  ClientResourceManager.RegisterStyleSheet(Page, ResolveUrl("~/DesktopModules/Albatros/WeatherGateway/css/" & cssFilename), DotNetNuke.Web.Client.FileOrder.Css.ModuleCss)
 End Sub

 Public Function LocalizeJSString(resourceKey As String) As String
  Return DotNetNuke.UI.Utilities.ClientAPI.GetSafeJSString(LocalizeString(resourceKey))
 End Function

 Public Function LocalizeJSString(resourceKey As String, resourceFile As String) As String
  Return DotNetNuke.UI.Utilities.ClientAPI.GetSafeJSString(DotNetNuke.Services.Localization.Localization.GetString(resourceKey, resourceFile))
 End Function
#End Region

End Class
