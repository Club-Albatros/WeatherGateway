Imports System
Imports DotNetNuke

Namespace Data

 Partial Public MustInherit Class DataProvider

#Region " Shared/Static Methods "

  ' singleton reference to the instantiated object 
  Private Shared objProvider As DataProvider = Nothing

  ' constructor
  Shared Sub New()
   CreateProvider()
  End Sub

  ' dynamically create provider
  Private Shared Sub CreateProvider()
   objProvider = CType(DotNetNuke.Framework.Reflection.CreateObject("data", "Albatros.DNN.Modules.WeatherGateway.Data", ""), DataProvider)
  End Sub

  ' return the provider
  Public Shared Shadows Function Instance() As DataProvider
   Return objProvider
  End Function

#End Region

#Region " General Methods "
  Public MustOverride Function GetNull(Field As Object) As Object
#End Region

#Region " Measurement Methods "
	Public MustOverride Function GetMeasurement(measurementId As Int32) As IDataReader
	Public MustOverride Function AddMeasurement(datime As Date, humidity As Double, pressure As Double, stationId As Int32, temperature As Double, windAverage As Double, windDirection As Int32, windMaximum As Double) As Integer	
	Public MustOverride Sub UpdateMeasurement(datime As Date, humidity As Double, measurementId As Int32, pressure As Double, stationId As Int32, temperature As Double, windAverage As Double, windDirection As Int32, windMaximum As Double)	
	Public MustOverride Sub DeleteMeasurement(measurementId As Int32)
#End Region

#Region " Station Methods "
	Public MustOverride Function GetStation(stationId As Int32) As IDataReader
	Public MustOverride Function AddStation(altitude As Int32, code As String, deco As String, lastSuccessfulRetrieval As Date, latestStatus As String, latitude As Double, longitude As Double, name As String, provider As String, sMSCode As String, viewOrder As Int32) As Integer	
	Public MustOverride Sub UpdateStation(altitude As Int32, code As String, deco As String, lastSuccessfulRetrieval As Date, latestStatus As String, latitude As Double, longitude As Double, name As String, provider As String, sMSCode As String, stationId As Int32, viewOrder As Int32)	
	Public MustOverride Sub DeleteStation(stationId As Int32)
#End Region


 End Class

End Namespace

