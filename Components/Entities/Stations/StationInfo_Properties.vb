Imports System
Imports System.Runtime.Serialization

Namespace Entities.Stations
  Partial Public Class StationInfo

#Region " Private Members "
#End Region
	
#Region " Constructors "
  Public Sub New()
  End Sub

  Public Sub New(stationId As Int32, altitude As Int32, code As String, deco As String, lastSuccessfulRetrieval As Date, latestStatus As String, latitude As Double, longitude As Double, name As String, provider As String, sMSCode As String, viewOrder As Int32)
   Me.Altitude = altitude
   Me.Code = code
   Me.Deco = deco
   Me.LastSuccessfulRetrieval = lastSuccessfulRetrieval
   Me.LatestStatus = latestStatus
   Me.Latitude = latitude
   Me.Longitude = longitude
   Me.Name = name
   Me.Provider = provider
   Me.SMSCode = sMSCode
   Me.StationId = stationId
   Me.ViewOrder = viewOrder
  End Sub
#End Region
	
#Region " Public Properties "
  <DataMember()>
  Public Property Altitude As Int32 = -1
  <DataMember()>
  Public Property Code As String = ""
  <DataMember()>
  Public Property Deco As String = ""
  <DataMember()>
  Public Property LastSuccessfulRetrieval As Date = Date.MinValue
  <DataMember()>
  Public Property LatestStatus As String = ""
  <DataMember()>
  Public Property Latitude As Double = 0
  <DataMember()>
  Public Property Longitude As Double = 0
  <DataMember()>
  Public Property Name As String = ""
  <DataMember()>
  Public Property Provider As String = ""
  <DataMember()>
  Public Property SMSCode As String = ""
  <DataMember()>
  Public Property StationId As Int32 = -1
  <DataMember()>
  Public Property ViewOrder As Int32 = 0
#End Region

 End Class
End Namespace


