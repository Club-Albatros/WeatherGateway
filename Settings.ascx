<%@ Control Language="vb" AutoEventWireup="false" Codebehind="Settings.ascx.vb" Inherits="Albatros.DNN.Modules.WeatherGateway.Settings" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>

<fieldset>
 <div class="dnnFormItem">
  <dnn:label id="plStations" runat="server" controlname="cblStations" suffix=":" />
  <asp:CheckBoxList runat="server" ID="cblStations" DataTextField="Name" DataValueField="StationID" />
 </div>
 <div class="dnnFormItem">
  <dnn:label id="plSMSUsername" runat="server" controlname="txtSMSUsername" suffix=":" />
  <asp:TextBox runat="server" ID="txtSMSUsername" Width="200" />
 </div>
 <div class="dnnFormItem">
  <dnn:label id="plSMSPassword" runat="server" controlname="txtSMSPassword" suffix=":" />
  <asp:TextBox runat="server" ID="txtSMSPassword" Width="200" TextMode="Password" />
 </div>
</fieldset>

