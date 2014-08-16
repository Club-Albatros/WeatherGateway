Imports System.Linq
Imports Albatros.DNN.Modules.WeatherGateway.Entities.Stations

Public Class EditStations
 Inherits ModuleBase

 Private Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init

  AddWeatherGatewayService()
  rpStations.DataSource = Settings.StationsToShow.Values.OrderBy(Function(s) s.ViewOrder)
  rpStations.DataBind()

 End Sub

 Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

  DotNetNuke.Framework.jQuery.RequestUIRegistration()

 End Sub

 Private Sub cmdUpdate_Click(sender As Object, e As System.EventArgs) Handles cmdUpdate.Click

  For Each itm As RepeaterItem In rpStations.Items
   Dim txtSMS As TextBox = CType(itm.FindControl("txtSMS"), TextBox)
   Dim txtDeco As TextBox = CType(itm.FindControl("txtDeco"), TextBox)
   Dim hidStationId As HiddenField = CType(itm.FindControl("hidStationId"), HiddenField)
   Dim s As StationInfo = StationsController.GetStation(Integer.Parse(hidStationId.Value), "", "", "")
   If s IsNot Nothing Then
    If s.SMSCode <> txtSMS.Text Or s.Deco <> txtDeco.Text Then
     s.SMSCode = txtSMS.Text.Trim
     s.Deco = txtDeco.Text.Trim
     StationsController.UpdateStation(s)
    End If
   End If
  Next
  Settings.ClearCache()

  Me.Response.Redirect(DotNetNuke.Common.NavigateURL(), False)

 End Sub

 Private Sub cmdCancel_Click(sender As Object, e As System.EventArgs) Handles cmdCancel.Click
  Me.Response.Redirect(DotNetNuke.Common.NavigateURL(), False)
 End Sub

 Private Sub rpStations_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rpStations.ItemDataBound
  If (e.Item.ItemType = ListItemType.Item) Or (e.Item.ItemType = ListItemType.AlternatingItem) Then
   Dim s As StationInfo = CType(e.Item.DataItem, StationInfo)
   Dim txtSMS As TextBox = CType(e.Item.FindControl("txtSMS"), TextBox)
   Dim txtDeco As TextBox = CType(e.Item.FindControl("txtDeco"), TextBox)
   Dim hidStationId As HiddenField = CType(e.Item.FindControl("hidStationId"), HiddenField)
   txtSMS.Text = s.SMSCode
   txtDeco.Text = s.Deco
   hidStationId.Value = s.StationId.ToString
  End If
 End Sub
End Class