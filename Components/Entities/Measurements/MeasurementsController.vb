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

  Public Shared Function GetLatestMeasurements() As Dictionary(Of Integer, MeasurementInfo)

   Return DotNetNuke.Common.Utilities.CBO.FillDictionary(Of Integer, MeasurementInfo)("StationId", DataProvider.Instance().GetLatestMeasurements())

  End Function

 End Class
End Namespace

