﻿function WeatherService($, settings, mid) {
 var moduleId = mid;
 var baseServicepath = $.dnnSF(moduleId).getServiceRoot('Albatros/WeatherGateway');

 this.getStationData = function (stationId, hours, success) {
  $.ajax({
   type: "GET",
   url: baseServicepath + stationId + '/Data?hours=' + hours,
   beforeSend: $.dnnSF(moduleId).setModuleHeaders,
   data: {}
  }).done(function (data) {
   if (success != undefined) {
    success(data);
   }
  }).fail(function (xhr, status) {
   displayMessage(settings.errorBoxId, settings.serverErrorWithDescription + eval("(" + xhr.responseText + ")").ExceptionMessage, "dnnFormWarning");
  });
 };

}