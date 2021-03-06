Imports System
Imports System.Data
Imports System.Xml
Imports System.Xml.Schema
Imports System.Xml.Serialization

Imports DotNetNuke
Imports DotNetNuke.Common
Imports DotNetNuke.Common.Utilities
Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Entities.Portals
Imports DotNetNuke.Services.Tokens

Imports Albatros.DNN.Modules.WeatherGateway.Data

Namespace Entities.Measurements

 Partial Public Class MeasurementsController

 Public Shared Function GetMeasurement(measurementId As Int32) As MeasurementInfo
	
  Return CType(CBO.FillObject(DataProvider.Instance().GetMeasurement(measurementId), GetType(MeasurementInfo)), MeasurementInfo)

 End Function

 Public Shared Function AddMeasurement(ByRef objMeasurement As MeasurementInfo) As Integer

  objMeasurement.MeasurementId = CType(DataProvider.Instance().AddMeasurement(objMeasurement.Datime, objMeasurement.Humidity, objMeasurement.Pressure, objMeasurement.StationId, objMeasurement.Temperature, objMeasurement.WindAverage, objMeasurement.WindDirection, objMeasurement.WindMaximum), Integer)
  Return objMeasurement.MeasurementId

 End Function

 Public Shared Sub UpdateMeasurement(objMeasurement As MeasurementInfo)
	
  DataProvider.Instance().UpdateMeasurement(objMeasurement.Datime, objMeasurement.Humidity, objMeasurement.MeasurementId, objMeasurement.Pressure, objMeasurement.StationId, objMeasurement.Temperature, objMeasurement.WindAverage, objMeasurement.WindDirection, objMeasurement.WindMaximum)
	
 End Sub

 Public Shared Sub DeleteMeasurement(measurementId As Int32)
	
  DataProvider.Instance().DeleteMeasurement(measurementId)

 End Sub

 End Class
End Namespace

