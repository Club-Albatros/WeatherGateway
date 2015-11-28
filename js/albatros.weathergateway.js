function WeatherService($, settings, mid) {
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

 this.reorder = function (order, success) {
  $.ajax({
   type: "POST",
   url: baseServicepath + "Reorder",
   beforeSend: $.dnnSF(moduleId).setModuleHeaders,
   data: { order: order }
  }).done(function (data) {
   if (success != undefined) {
    success();
   }
  }).fail(function (xhr, status) {
   displayMessage(settings.errorBoxId, settings.serverErrorWithDescription + eval("(" + xhr.responseText + ")").ExceptionMessage, "dnnFormWarning");
  });
 };

}

function drawChart(stationId, ctx, ctrX, ctrY, outerRad, speedStep, speedFactor, decos, service) {

 ctx.fillStyle = '#E6EDF7';
 ctx.beginPath();
 ctx.arc(ctrX, ctrY, outerRad, 0, 2 * Math.PI, false);
 ctx.fill();
 ctx.strokeStyle = '#82B4FA';
 ctx.lineWidth = 5;
 ctx.beginPath();
 ctx.arc(ctrX, ctrY, outerRad, 0, 2 * Math.PI, false);
 ctx.stroke();
 ctx.fillStyle = '#0053C7';
 ctx.strokeStyle = '#0053C7';
 ctx.lineWidth = 0.5;
 var st = speedStep * speedFactor;
 while (st < outerRad) {
  ctx.beginPath();
  ctx.arc(ctrX, ctrY, st, 0, 2 * Math.PI, false);
  ctx.stroke();
  ctx.textAlign = 'right';
  ctx.font = ((speedStep * speedFactor) / 2).toFixed(0) + 'px Helvetica';
  ctx.fillText((st / speedFactor).toFixed(0), ctrX + st - 1, ctrY + (speedStep * speedFactor) / 8);
  st += speedStep * speedFactor;
 }
 ctx.fillStyle = '#0053C7';
 ctx.fillRect(ctrX, ctrY, 1, 1);

 for (var i = 0; i < decos.length; i++) {
  if (decos[i] != '') {
   var angles = decos[i].split('-');
   ctx.beginPath();
   ctx.moveTo(ctrX, ctrY);
   ctx.arc(ctrX, ctrY, outerRad, Math.PI * angles[0] / 180 - Math.PI / 2, Math.PI * angles[1] / 180 - Math.PI / 2, false)
   ctx.closePath();
   ctx.fillStyle = 'rgba(128, 128, 128, 0.5)';
   ctx.fill();
   ctx.restore();
  }
 }

 service.getStationData(stationId, 2, function (data) {
  $.each(data, function (i, elem) {
   var h1 = elem.WA * speedFactor;
   if (h1 > outerRad) { h1 = outerRad };
   var x1 = Math.cos(Math.PI * (90 - elem.WD) / 180) * h1;
   var y1 = Math.sin(Math.PI * (90 - elem.WD) / 180) * h1;
   var h2 = elem.WM * speedFactor;
   if (h2 > outerRad) { h2 = outerRad };
   var x2 = Math.cos(Math.PI * (90 - elem.WD) / 180) * h2;
   var y2 = Math.sin(Math.PI * (90 - elem.WD) / 180) * h2;
   ctx.strokeStyle = '#FF5252';
   ctx.globalAlpha = Math.pow(i / data.length, 2);
   ctx.lineWidth = 2;
   ctx.beginPath();
   ctx.moveTo(ctrX + x1, ctrY - y1);
   ctx.lineTo(ctrX + x2, ctrY - y2);
   ctx.stroke();
   ctx.closePath();
   if (i == data.length - 1) {
    ctx.strokeStyle = '#0053C7';
    ctx.globalAlpha = 1;
    ctx.lineWidth = 3;
    ctx.beginPath();
    ctx.moveTo(ctrX, ctrY);
    ctx.lineTo(ctrX + x1, ctrY - y1);
    ctx.stroke();
    ctx.closePath();
   }
  });
 });

}
