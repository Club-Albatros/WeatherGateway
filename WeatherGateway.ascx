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
   <div><span><%= LocalizeString("Details") %></span> <a href="<%# Eval("Code", "http://meteo.jdc.ch/station/?id={0}") %>" target="_blank"><%# Eval("Code", "http://meteo.jdc.ch/station/?id={0}") %></a></div>
  </div>
 </ItemTemplate>
</asp:Repeater>
