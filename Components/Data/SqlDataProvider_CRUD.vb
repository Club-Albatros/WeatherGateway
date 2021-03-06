Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports DotNetNuke.Framework.Providers

Namespace Data

 Partial Public Class SqlDataProvider
  Inherits DataProvider

#Region " Private Members "

  Private Const ProviderType As String = "data"
  Private Const ModuleQualifier As String = "WeatherGateway_"

  Private _providerConfiguration As DotNetNuke.Framework.Providers.ProviderConfiguration = DotNetNuke.Framework.Providers.ProviderConfiguration.GetProviderConfiguration(ProviderType)
  Private _connectionString As String
  Private _providerPath As String
  Private _objectQualifier As String
  Private _databaseOwner As String

#End Region

#Region " Constructors "

  Public Sub New()

   ' Read the configuration specific information for this provider
   Dim objProvider As DotNetNuke.Framework.Providers.Provider = CType(_providerConfiguration.Providers(_providerConfiguration.DefaultProvider), Provider)

   'Get Connection string from web.config
   _connectionString = DotNetNuke.Common.Utilities.Config.GetConnectionString()

   If _connectionString = "" Then
    ' Use connection string specified in provider
    _connectionString = objProvider.Attributes("connectionString")
   End If

   _providerPath = objProvider.Attributes("providerPath")

   _objectQualifier = objProvider.Attributes("objectQualifier")
   If _objectQualifier <> "" And _objectQualifier.EndsWith("_") = False Then
    _objectQualifier += "_"
   End If

   _databaseOwner = objProvider.Attributes("databaseOwner")
   If _databaseOwner <> "" And _databaseOwner.EndsWith(".") = False Then
    _databaseOwner += "."
   End If

  End Sub

#End Region

#Region " Properties "

  Public ReadOnly Property ConnectionString() As String
   Get
    Return _connectionString
   End Get
  End Property

  Public ReadOnly Property ProviderPath() As String
   Get
    Return _providerPath
   End Get
  End Property

  Public ReadOnly Property ObjectQualifier() As String
   Get
    Return _objectQualifier
   End Get
  End Property

  Public ReadOnly Property DatabaseOwner() As String
   Get
    Return _databaseOwner
   End Get
  End Property

#End Region

#Region " General Methods "
  Public Overrides Function GetNull(Field As Object) As Object
   Return DotNetNuke.Common.Utilities.Null.GetNull(Field, DBNull.Value)
  End Function
#End Region


#Region " Measurement Methods "

	Public Overrides Function GetMeasurement(measurementId As Int32) As IDataReader
		Return CType(SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner & ObjectQualifier & ModuleQualifier & "GetMeasurement", measurementId), IDataReader)
	End Function
	
	Public Overrides Function AddMeasurement(datime As Date, humidity As Double, pressure As Double, stationId As Int32, temperature As Double, windAverage As Double, windDirection As Int32, windMaximum As Double) As Integer
		Return CType(SqlHelper.ExecuteScalar(ConnectionString, DatabaseOwner & ObjectQualifier & ModuleQualifier & "AddMeasurement", datime, GetNull(humidity), GetNull(pressure), stationId, GetNull(temperature), GetNull(windAverage), GetNull(windDirection), GetNull(windMaximum)), Integer)
	End Function
	
	Public Overrides Sub UpdateMeasurement(datime As Date, humidity As Double, measurementId As Int32, pressure As Double, stationId As Int32, temperature As Double, windAverage As Double, windDirection As Int32, windMaximum As Double)
		SqlHelper.ExecuteNonQuery(ConnectionString, DatabaseOwner & ObjectQualifier & ModuleQualifier & "UpdateMeasurement", datime, GetNull(humidity), measurementId, GetNull(pressure), stationId, GetNull(temperature), GetNull(windAverage), GetNull(windDirection), GetNull(windMaximum))
	End Sub

	Public Overrides Sub DeleteMeasurement(measurementId As Int32)
		SqlHelper.ExecuteNonQuery(ConnectionString, DatabaseOwner & ObjectQualifier & ModuleQualifier & "DeleteMeasurement", measurementId)
	End Sub
	
#End Region

#Region " Station Methods "

	Public Overrides Function GetStation(stationId As Int32) As IDataReader
		Return CType(SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner & ObjectQualifier & ModuleQualifier & "GetStation", stationId), IDataReader)
	End Function
	
	Public Overrides Function AddStation(altitude As Int32, code As String, deco As String, lastSuccessfulRetrieval As Date, latestStatus As String, latitude As Double, longitude As Double, name As String, provider As String, sMSCode As String, viewOrder As Int32) As Integer
		Return CType(SqlHelper.ExecuteScalar(ConnectionString, DatabaseOwner & ObjectQualifier & ModuleQualifier & "AddStation", GetNull(altitude), GetNull(code), GetNull(deco), GetNull(lastSuccessfulRetrieval), GetNull(latestStatus), GetNull(latitude), GetNull(longitude), GetNull(name), GetNull(provider), GetNull(sMSCode), viewOrder), Integer)
	End Function
	
	Public Overrides Sub UpdateStation(altitude As Int32, code As String, deco As String, lastSuccessfulRetrieval As Date, latestStatus As String, latitude As Double, longitude As Double, name As String, provider As String, sMSCode As String, stationId As Int32, viewOrder As Int32)
		SqlHelper.ExecuteNonQuery(ConnectionString, DatabaseOwner & ObjectQualifier & ModuleQualifier & "UpdateStation", GetNull(altitude), GetNull(code), GetNull(deco), GetNull(lastSuccessfulRetrieval), GetNull(latestStatus), GetNull(latitude), GetNull(longitude), GetNull(name), GetNull(provider), GetNull(sMSCode), stationId, viewOrder)
	End Sub

	Public Overrides Sub DeleteStation(stationId As Int32)
		SqlHelper.ExecuteNonQuery(ConnectionString, DatabaseOwner & ObjectQualifier & ModuleQualifier & "DeleteStation", stationId)
	End Sub
	
#End Region


 End Class

End Namespace
