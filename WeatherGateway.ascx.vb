Imports System.Linq

Imports Albatros.DNN.Modules.WeatherGateway.Entities.Measurements
Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Services.Localization
Imports DotNetNuke.Entities.Modules.Actions

Public Class WeatherGateway
 Inherits ModuleBase
 Implements IActionable

 Public Property LatestMeasurements As Dictionary(Of Integer, MeasurementInfo)

 Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

  LatestMeasurements = MeasurementsController.GetLatestMeasurements

  rpStations.DataSource = Settings.StationsToShow.Values.OrderBy(Function(s) s.ViewOrder)
  rpStations.DataBind()
  divManagement.Visible = DotNetNuke.Security.Permissions.ModulePermissionController.HasModulePermission(Me.ModuleConfiguration.ModulePermissions, "EDIT")

  AddWeatherGatewayService()

 End Sub

 Public Function LatestMeasurement(stationId As Integer) As MeasurementInfo

  If LatestMeasurements.ContainsKey(stationId) Then
   Return LatestMeasurements(stationId)
  Else
   Return New MeasurementInfo ' make sure UI doesn't throw errors
  End If

 End Function

 Private Sub cmdEditStations_Click(sender As Object, e As System.EventArgs) Handles cmdEditStations.Click
  Me.Response.Redirect(EditUrl("Stations"), False)
 End Sub

#Region " IActionable "
 Public ReadOnly Property ModuleActions As Actions.ModuleActionCollection Implements IActionable.ModuleActions
  Get
   Dim MyActions As New Actions.ModuleActionCollection
   If Security.CanEdit Then
    MyActions.Add(GetNextActionID, Localization.GetString("EditStations", LocalResourceFile), ModuleActionType.EditContent, "", "", EditUrl("Stations"), False, DotNetNuke.Security.SecurityAccessLevel.Edit, True, False)
   End If
   Return MyActions
  End Get
 End Property
#End Region

End Class