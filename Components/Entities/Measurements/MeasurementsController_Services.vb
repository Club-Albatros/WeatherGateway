Imports System.Globalization
Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Json
Imports System.Web.Http
Imports System.Web.Script.Serialization

Imports DotNetNuke.Web.Api
Imports DotNetNuke.Security.Permissions
Imports DotNetNuke.Common.Utilities.DictionaryExtensions
Imports Albatros.DNN.Modules.WeatherGateway.Entities.Stations

Namespace Entities.Measurements

 Partial Public Class MeasurementsController
  Inherits DnnApiController
  Implements IServiceRouteMapper

#Region " IServiceRouteMapper "
  Public Sub RegisterRoutes(mapRouteManager As DotNetNuke.Web.Api.IMapRoute) Implements DotNetNuke.Web.Api.IServiceRouteMapper.RegisterRoutes
   mapRouteManager.MapHttpRoute("Albatros/WeatherGateway", "Report", "{provider}/{stationCode}/Report", New With {.Controller = "Measurements", .Action = "Report"}, Nothing, New String() {"Albatros.DNN.Modules.WeatherGateway.Entities.Measurements"})
   mapRouteManager.MapHttpRoute("Albatros/WeatherGateway", "SMSReport", "SMS", New With {.Controller = "Measurements", .Action = "SMSReport"}, Nothing, New String() {"Albatros.DNN.Modules.WeatherGateway.Entities.Measurements"})
   mapRouteManager.MapHttpRoute("Albatros/WeatherGateway", "StationData", "{stationId}/Data", New With {.Controller = "Measurements", .Action = "GetStationData"}, New With {.stationId = "\d+"}, New String() {"Albatros.DNN.Modules.WeatherGateway.Entities.Measurements"})
  End Sub
#End Region

#Region " Service Methods "
  <HttpGet()>
  <AllowAnonymous()>
  Public Function Report(provider As String, stationCode As String) As HttpResponseMessage
   Dim locale As String = "en-US"
   If HttpContext.Current.Request.Params("locale") IsNot Nothing Then
    locale = HttpContext.Current.Request.Params("locale")
   End If
   Dim message As String = ""
   Dim station As StationInfo = StationsController.GetStation(-1, provider, stationCode, "")
   If station IsNot Nothing Then
    Dim measurements As Dictionary(Of Integer, MeasurementInfo) = GetLatestMeasurements()
    If measurements.ContainsKey(station.StationId) Then
     Dim m As MeasurementInfo = measurements(station.StationId)
     Dim msgFormat As String = DotNetNuke.Services.Localization.Localization.GetString("SMSMessage", Globals.SharedResourcesFile, locale)
     msgFormat = msgFormat.Replace("[WINDDIRECTIONNEWS]", DotNetNuke.Services.Localization.Localization.GetString(m.WindDirectionNEWS, Globals.SharedResourcesFile, locale))
     Dim tr As New WeatherGatewayTokenReplace(station, m)
     message = tr.ReplaceTokens(msgFormat)
    End If
   End If
   Return Request.CreateResponse(HttpStatusCode.OK, message)
  End Function

  Public Class SMSDTO
   Public Property NUMBER As String
   Public Property MESSAGE As String
  End Class

  <HttpPost()>
  <AllowAnonymous()>
  Public Function SMSReport(data As SMSDTO) As HttpResponseMessage

   Using log As New Common.Logger

    Try

     log.WriteLine(HttpContext.Current.Request.Url.PathAndQuery)
     log.WriteLineFormat("Request by {0} for {1}", data.NUMBER, data.MESSAGE)
     If ActiveModule IsNot Nothing Then
      log.WriteLineFormat("ModuleId {0} TabId {1}", ActiveModule.ModuleID, ActiveModule.TabID)
     End If

     Dim locale As String = "en-US"
     If HttpContext.Current.Request.Params("locale") IsNot Nothing Then
      locale = HttpContext.Current.Request.Params("locale")
     End If
     Dim message As String = data.MESSAGE.Trim
     Try
      message = message.Split(" "c)(1) ' take second word
     Catch ex As Exception
     End Try
     log.WriteLineFormat("Request {0}", message)
     Dim station As StationInfo = StationsController.GetStation(-1, "", "", message)
     message = "" ' reset message as it now becomes outgoing message
     If station IsNot Nothing Then
      log.WriteLineFormat("Found station {0}", station.Name)
      Dim measurements As Dictionary(Of Integer, MeasurementInfo) = GetLatestMeasurements()
      If measurements.ContainsKey(station.StationId) Then
       Dim m As MeasurementInfo = measurements(station.StationId)
       log.WriteLineFormat("Found measurement at {0:u}", m.Datime)
       Dim msgFormat As String = DotNetNuke.Services.Localization.Localization.GetString("SMSMessage", Globals.SharedResourcesFile, locale)
       msgFormat = msgFormat.Replace("[WINDDIRECTIONNEWS]", DotNetNuke.Services.Localization.Localization.GetString(m.WindDirectionNEWS, Globals.SharedResourcesFile, locale))
       Dim tr As New WeatherGatewayTokenReplace(station, m)
       message = tr.ReplaceTokens(msgFormat)
      End If
     End If
     log.WriteLineFormat("Outgoing message: {0}", message)

     If message <> "" Then
      Dim settings As ModuleSettings = ModuleSettings.GetSettings(ActiveModule.ModuleID)
      Dim req As New Common.WebRequest
      With req
       .ThrowExceptions = True
       .Url = "http://www.truesenses.com/cgi-bin/smsgateway.cgi"
       .PostMode = Common.WebRequest.PostModeType.URLEncoded
       .AddPostdata("CMD", "SENDMESSAGE")
       .AddPostdata("ACCOUNT", settings.SMSGatewayUsername)
       .AddPostdata("PASSWORD", settings.SMSGatewayPassword)
       .AddPostdata("NUMBER", data.NUMBER)
       .AddPostdata("MESSAGE", message)
       '.AddPostdata("TEST", "ON")
      End With
      Dim res As String = req.GetResponse
      log.WriteLine(res.Trim)
     End If

     log.WriteLine("End request")
     log.WritePaddingLine()

    Catch ex As Exception

     log.WriteError("SMSReport", ex)
     log.WritePaddingLine()
     Return Request.CreateResponse(HttpStatusCode.BadRequest, "")

    End Try

   End Using

   Return Request.CreateResponse(HttpStatusCode.OK, "")

  End Function

  <HttpGet()>
  <DnnModuleAuthorize(AccessLevel:=DotNetNuke.Security.SecurityAccessLevel.View)>
  Public Function GetStationData(stationId As Integer) As HttpResponseMessage
   Dim hours As Integer = 1
   If HttpContext.Current.Request.Params("hours") IsNot Nothing Then
    hours = CInt(HttpContext.Current.Request.Params("hours"))
   End If
   Dim res As List(Of MeasurementPoint) = DotNetNuke.Common.Utilities.CBO.FillCollection(Of MeasurementPoint)(Data.DataProvider.Instance().GetStationData(stationId, hours))
   Return Request.CreateResponse(HttpStatusCode.OK, res)
  End Function
#End Region

 End Class
End Namespace
