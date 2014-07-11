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

Namespace Entities.Stations

 Partial Public Class StationsController
  Inherits DnnApiController
  Implements IServiceRouteMapper

#Region " IServiceRouteMapper "
  Public Sub RegisterRoutes(mapRouteManager As DotNetNuke.Web.Api.IMapRoute) Implements DotNetNuke.Web.Api.IServiceRouteMapper.RegisterRoutes
   'mapRouteManager.MapHttpRoute("Albatros/WeatherGateway", "Default", "{controller}/{action}/{id}", New With {.Controller = "Stations", .Action = "MyMethod"}, New With {.id = "\d*"}, New String() {"Albatros.DNN.Modules.WeatherGateway.Data.Entities.Stations"})
  End Sub
#End Region

#Region " Service Methods "
  <HttpGet()>
  <DnnModuleAuthorize(AccessLevel:=DotNetNuke.Security.SecurityAccessLevel.View)>
  Public Function MyMethod(id As Integer) As HttpResponseMessage
   Dim res As Boolean = True
   Return Request.CreateResponse(HttpStatusCode.OK, res)
  End Function
#End Region

 End Class
End Namespace
