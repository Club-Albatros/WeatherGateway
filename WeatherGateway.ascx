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
  <canvas id="graph<%# Eval("StationId") %>" data-value="<%# Eval("StationId") %>" width="150" height="150" title="<%= LocalizeString("ChartHelp")%>">Data
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
   if ($(this)[0].getContext) {
    var ctx = $(this)[0].getContext("2d");
    ctx.fillStyle = '#E6EDF7';
    ctx.beginPath();
    ctx.arc(75, 75, 70, 0, 2*Math.PI, false);
    ctx.fill();
    ctx.strokeStyle = '#82B4FA';
    ctx.lineWidth = 5;
    ctx.beginPath();
    ctx.arc(75, 75, 70, 0, 2*Math.PI, false);
    ctx.stroke();
    ctx.strokeStyle = '#0053C7';
    ctx.lineWidth = 0.5;
    ctx.beginPath();
    ctx.arc(75, 75, 20, 0, 2*Math.PI, false);
    ctx.stroke();
    ctx.beginPath();
    ctx.arc(75, 75, 40, 0, 2*Math.PI, false);
    ctx.stroke();
    ctx.beginPath();
    ctx.arc(75, 75, 60, 0, 2*Math.PI, false);
    ctx.stroke();
    ctx.fillStyle = '#0053C7';
    ctx.fillRect(75,75,1,1);
    ctx.strokeStyle = '#FF5252';
    ctx.lineWidth = 2;
    service.getStationData($(this).attr('data-value'), 2, function(data) {
     $.each(data, function(i, elem) {
      var h1 = elem.WA * 2;
      if (h1 > 70) {h1=70};
      var y1 = Math.cos(Math.PI * elem.WD/180) * h1;
      var x1 = Math.sin(Math.PI * elem.WD/180) * h1;
      var h2 = elem.WM * 2;
      if (h2 > 70) {h2=70};
      var y2 = Math.cos(Math.PI * elem.WD/180) * h2;
      var x2 = Math.sin(Math.PI * elem.WD/180) * h2;
      ctx.beginPath();
      ctx.moveTo(75 - y1, 75 + x1);
      ctx.lineTo(75 - y2, 75 + x2);
      // ctx.arc(75 - y1, 75 + x1, 2, 0, 2*Math.PI, false);
      ctx.stroke();
     });
    });
   }
  });

 }); // doc ready

} (jQuery, window.Sys));
</script>

