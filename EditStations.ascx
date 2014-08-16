<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="EditStations.ascx.vb" Inherits="Albatros.DNN.Modules.WeatherGateway.EditStations" %>

<ul class="wg_stations">
<asp:Repeater runat="server" ID="rpStations">
 <ItemTemplate>
  <li id="station_<%# Eval("StationId")%>" class="ui-state-default wg_station">
   <div class="wg_editrowdiv"><%# Eval("Name")%></div>
   <div class="wg_editrowdiv"><asp:TextBox runat="server" ID="txtSMS" Width="100" /></div>
   <div class="wg_editrowdiv"><asp:TextBox runat="server" ID="txtDeco" Width="200" /></div><br />
   <div class="wg_editrowdiv wg_smalltext">&nbsp;</div>
   <div class="wg_editrowdiv wg_smalltext"><%= LocalizeString("SMSCode") %></div>
   <div class="wg_editrowdiv wg_smalltext"><%= LocalizeString("Deco") %></div>
   <asp:HiddenField runat="server" ID="hidStationId" />
  </li>
 </ItemTemplate>
</asp:Repeater>
</ul>

<div class="wg_buttonbar">
 <asp:Button runat="server" ID="cmdUpdate" resourcekey="cmdUpdate" class="dnnPrimaryAction" />
 <asp:Button runat="server" ID="cmdCancel" resourcekey="cmdCancel" class="dnnSecondaryAction" />
</div>

<script type="text/javascript">
 var weatherService

 (function ($, Sys) {
 $(document).ready(function () {

  weatherService = new WeatherService($, {
   serverErrorText: '<%= LocalizeJSString("ServerError") %>',
   serverErrorWithDescriptionText: '<%= LocalizeJSString("ServerErrorWithDescription") %>',
   errorBoxId: '#serviceErrorBox<%= ModuleId %>'
  },
  <%= ModuleId %>);

  $('.wg_stations').sortable({
   update: function (event, ui) {
    weatherService.reorder($('.wg_stations').sortable('serialize'), null)
   }
  });

 });
} (jQuery, window.Sys));


</script>