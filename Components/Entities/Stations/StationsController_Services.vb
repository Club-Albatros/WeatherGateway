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
   mapRouteManager.MapHttpRoute("Albatros/WeatherGateway", "Reorder", "reorder", New With {.Controller = "Stations", .Action = "Reorder"}, New String() {"Albatros.DNN.Modules.WeatherGateway.Entities.Stations"})
  End Sub
#End Region

#Region " Service Methods "
  Public Class orderDTO
   Public Property order As String
  End Class

  <HttpPost()>
  <DnnModuleAuthorize(AccessLevel:=DotNetNuke.Security.SecurityAccessLevel.Edit)>
  <ValidateAntiForgeryToken()>
  Public Function Reorder(postData As orderDTO) As HttpResponseMessage
   Dim newOrder As String = postData.order.Replace("station[]=", "")
   Data.DataProvider.Instance().ReorderStations(ActiveModule.PortalID, newOrder)
   Return Request.CreateResponse(HttpStatusCode.OK, "")
  End Function
#End Region

 End Class
End Namespace
