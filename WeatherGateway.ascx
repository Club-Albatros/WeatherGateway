<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="WeatherGateway.ascx.vb" Inherits="Albatros.DNN.Modules.WeatherGateway.WeatherGateway" %>

<asp:Repeater runat="server" ID="rpStations">
 <ItemTemplate>
  <h2><%# Eval("Name") %><%# Eval("Altitude", "  ({0:0} m)") %></h2>
  <div class="stationStats">
   <div><span><%= LocalizeString("Time")%></span> <%# LatestMeasurement(Eval("StationId")).Datime.ToString("dd MMM HH:mm") %></div>
   <div><span><%= LocalizeString("WindDir")%></span> <%# LocalizeString(LatestMeasurement(Eval("StationId")).WindDirectionNEWS)%></div>
   <div><span><%= LocalizeString("WindAvg")%></span> <%# LatestMeasurement(Eval("StationId")).WindAverage.ToString("0.0") %> km/h</div>
   <div><span><%= LocalizeString("WindMax")%></span> <%# LatestMeasurement(Eval("StationId")).WindMaximum.ToString("0") %> km/h</div>
   <div><span><%= LocalizeString("Temperature") %></span> <%# LatestMeasurement(Eval("StationId")).Temperature.ToString("0.0") %></div>
   <div><span><%= LocalizeString("Status")%></span> <%# Eval("LatestStatus") %></div>
   <div><span><%= LocalizeString("Details") %></span> <a href="<%# Eval("Code", "http://meteo.jdc.ch/station/{0}/") %>" target="_blank"><%# Eval("Code", "http://meteo.jdc.ch/station/{0}/")%></a></div>
  </div>
  <div class="stationGraph">
  <canvas id="graph<%# Eval("StationId") %>" data-value="<%# Eval("StationId") %>" data-deco="<%# Eval("Deco") %>" width="150" height="150" title="<%= LocalizeString("ChartHelp")%>">Data
  </canvas>
  </div>
  <div class="clear">
  </div>
 </ItemTemplate>
</asp:Repeater>

<div class="wg_buttonbar" runat="server" id="divManagement" visible="false">
 <asp:Button runat="server" ID="cmdEditStations" resourcekey="EditStations" class="dnnPrimaryAction" />
</div>

<script type="text/javascript">
(function ($, Sys) {

 $(document).ready(function () {

  service = new WeatherService($, {
   serverErrorText: '<%= LocalizeJSString("ServerError") %>',
   serverErrorWithDescriptionText: '<%= LocalizeJSString("ServerErrorWithDescription") %>',
   errorBoxId: '#pUrlsServiceErrorBox<%= ModuleId %>'
  }, <%= ModuleId %>);

  $.each($('.stationGraph canvas'), function(index, element) {
   var decos = $(this).attr('data-deco').split(';');
   
   if ($(this)[0].getContext) {
    var ctx = $(this)[0].getContext("2d");
    drawChart($(this).attr('data-value'), ctx, 75, 75, 70, 10, 2, decos, service);
   }
  });

 }); // doc ready

} (jQuery, window.Sys));
</script>

