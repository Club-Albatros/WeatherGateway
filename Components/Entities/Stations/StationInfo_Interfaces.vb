Imports System
Imports System.Data
Imports System.IO
Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Json
Imports System.Text
Imports System.Xml
Imports System.Xml.Schema
Imports System.Xml.Serialization

Imports DotNetNuke.Common.Utilities
Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Services.Tokens

Namespace Entities.Stations
  <Serializable(), XmlRoot("Station"), DataContract()>
  Partial Public Class StationInfo
   Implements IHydratable
   Implements IPropertyAccess
   Implements IXmlSerializable

#Region " IHydratable Implementation "
 ''' -----------------------------------------------------------------------------
 ''' <summary>
 ''' Fill hydrates the object from a Datareader
 ''' </summary>
 ''' <remarks>The Fill method is used by the CBO method to hydrtae the object
 ''' rather than using the more expensive Refection  methods.</remarks>
 ''' <history>
 ''' 	[pdonker]	07/10/2014  Created
 ''' </history>
 ''' -----------------------------------------------------------------------------
 Public Sub Fill(dr As IDataReader) Implements IHydratable.Fill

  Altitude = Convert.ToInt32(Null.SetNull(dr.Item("Altitude"), Altitude))
  Code = Convert.ToString(Null.SetNull(dr.Item("Code"), Code))
  Deco = Convert.ToString(Null.SetNull(dr.Item("Deco"), Deco))
  LastSuccessfulRetrieval = CDate(Null.SetNull(dr.Item("LastSuccessfulRetrieval"), LastSuccessfulRetrieval))
  LatestStatus = Convert.ToString(Null.SetNull(dr.Item("LatestStatus"), LatestStatus))
  Latitude = Convert.ToDouble(Null.SetNull(dr.Item("Latitude"), Latitude))
  Longitude = Convert.ToDouble(Null.SetNull(dr.Item("Longitude"), Longitude))
  Name = Convert.ToString(Null.SetNull(dr.Item("Name"), Name))
  Provider = Convert.ToString(Null.SetNull(dr.Item("Provider"), Provider))
  SMSCode = Convert.ToString(Null.SetNull(dr.Item("SMSCode"), SMSCode))
  StationId = Convert.ToInt32(Null.SetNull(dr.Item("StationId"), StationId))
  ViewOrder = Convert.ToInt32(Null.SetNull(dr.Item("ViewOrder"), ViewOrder))

 End Sub
 ''' -----------------------------------------------------------------------------
 ''' <summary>
 ''' Gets and sets the Key ID
 ''' </summary>
 ''' <remarks>The KeyID property is part of the IHydratble interface.  It is used
 ''' as the key property when creating a Dictionary</remarks>
 ''' <history>
 ''' 	[pdonker]	07/10/2014  Created
 ''' </history>
 ''' -----------------------------------------------------------------------------
 Public Property KeyID() As Integer Implements IHydratable.KeyID
  Get
   Return StationId
  End Get
  Set(value As Integer)
   StationId = value
  End Set
 End Property
#End Region

#Region " IPropertyAccess Implementation "
 Public Function GetProperty(strPropertyName As String, strFormat As String, formatProvider As System.Globalization.CultureInfo, AccessingUser As DotNetNuke.Entities.Users.UserInfo, AccessLevel As DotNetNuke.Services.Tokens.Scope, ByRef PropertyNotFound As Boolean) As String Implements DotNetNuke.Services.Tokens.IPropertyAccess.GetProperty
  Dim OutputFormat As String = String.Empty
  Dim portalSettings As DotNetNuke.Entities.Portals.PortalSettings = DotNetNuke.Entities.Portals.PortalController.GetCurrentPortalSettings()
  If strFormat = String.Empty Then
   OutputFormat = "D"
  Else
   OutputFormat = strFormat
  End If
  Select Case strPropertyName.ToLower
   Case "altitude"
    Return (Me.Altitude.ToString(OutputFormat, formatProvider))
   Case "code"
    Return PropertyAccess.FormatString(Me.Code, strFormat)
   Case "deco"
    Return PropertyAccess.FormatString(Me.Deco, strFormat)
   Case "lastsuccessfulretrieval"
    Return (Me.LastSuccessfulRetrieval.ToString(OutputFormat, formatProvider))
   Case "lateststatus"
    Return PropertyAccess.FormatString(Me.LatestStatus, strFormat)
   Case "latitude"
    Return (Me.Latitude.ToString(OutputFormat, formatProvider))
   Case "longitude"
    Return (Me.Longitude.ToString(OutputFormat, formatProvider))
   Case "name"
    Return PropertyAccess.FormatString(Me.Name, strFormat)
   Case "provider"
    Return PropertyAccess.FormatString(Me.Provider, strFormat)
   Case "smscode"
    Return PropertyAccess.FormatString(Me.SMSCode, strFormat)
   Case "stationid"
    Return (Me.StationId.ToString(OutputFormat, formatProvider))
   Case "vieworder"
    Return (Me.ViewOrder.ToString(OutputFormat, formatProvider))
   Case Else
    PropertyNotFound = True
  End Select

  Return Null.NullString
 End Function

 Public ReadOnly Property Cacheability() As DotNetNuke.Services.Tokens.CacheLevel Implements DotNetNuke.Services.Tokens.IPropertyAccess.Cacheability
  Get
   Return CacheLevel.fullyCacheable
  End Get
 End Property
#End Region

#Region " IXmlSerializable Implementation "
  ''' -----------------------------------------------------------------------------
  ''' <summary>
  ''' GetSchema returns the XmlSchema for this class
  ''' </summary>
  ''' <remarks>GetSchema is implemented as a stub method as it is not required</remarks>
  ''' <history>
  ''' 	[pdonker]	07/10/2014  Created
  ''' </history>
  ''' -----------------------------------------------------------------------------
  Public Function GetSchema() As XmlSchema Implements IXmlSerializable.GetSchema
   Return Nothing
  End Function

  Private Function readElement(reader As XmlReader, ElementName As String) As String
   If (Not reader.NodeType = XmlNodeType.Element) OrElse reader.Name <> ElementName Then
    reader.ReadToFollowing(ElementName)
   End If
   If reader.NodeType = XmlNodeType.Element Then
    Return reader.ReadElementContentAsString
   Else
    Return ""
   End If
  End Function

  ''' -----------------------------------------------------------------------------
  ''' <summary>
  ''' ReadXml fills the object (de-serializes it) from the XmlReader passed
  ''' </summary>
  ''' <remarks></remarks>
  ''' <param name="reader">The XmlReader that contains the xml for the object</param>
  ''' <history>
  ''' 	[pdonker]	07/10/2014  Created
  ''' </history>
  ''' -----------------------------------------------------------------------------
  Public Sub ReadXml(reader As XmlReader) Implements IXmlSerializable.ReadXml
   Try

    If Not Int32.TryParse(readElement(reader, "Altitude"), Altitude) Then
     Altitude = Null.NullInteger
    End If
    Code = readElement(reader, "Code")
    Deco = readElement(reader, "Deco")
   Date.TryParse(readElement(reader, "LastSuccessfulRetrieval"), LastSuccessfulRetrieval)
    LatestStatus = readElement(reader, "LatestStatus")
   Double.TryParse(readElement(reader, "Latitude"), Latitude)
   Double.TryParse(readElement(reader, "Longitude"), Longitude)
    Name = readElement(reader, "Name")
    Provider = readElement(reader, "Provider")
    SMSCode = readElement(reader, "SMSCode")
    If Not Int32.TryParse(readElement(reader, "ViewOrder"), ViewOrder) Then
     ViewOrder = Null.NullInteger
    End If
   Catch ex As Exception
    ' log exception as DNN import routine does not do that
    DotNetNuke.Services.Exceptions.LogException(ex)
    ' re-raise exception to make sure import routine displays a visible error to the user
    Throw New Exception("An error occured during import of an Station", ex)
   End Try

  End Sub

  ''' -----------------------------------------------------------------------------
  ''' <summary>
  ''' WriteXml converts the object to Xml (serializes it) and writes it using the XmlWriter passed
  ''' </summary>
  ''' <remarks></remarks>
  ''' <param name="writer">The XmlWriter that contains the xml for the object</param>
  ''' <history>
  ''' 	[pdonker]	07/10/2014  Created
  ''' </history>
  ''' -----------------------------------------------------------------------------
  Public Sub WriteXml(writer As XmlWriter) Implements IXmlSerializable.WriteXml
   writer.WriteStartElement("Station")
   writer.WriteElementString("StationId",  StationId.ToString())
   writer.WriteElementString("Altitude",  Altitude.ToString())
   writer.WriteElementString("Code",  Code)
   writer.WriteElementString("Deco",  Deco.ToString())
   writer.WriteElementString("LastSuccessfulRetrieval",  LastSuccessfulRetrieval.ToString())
   writer.WriteElementString("LatestStatus",  LatestStatus)
   writer.WriteElementString("Latitude",  Latitude.ToString())
   writer.WriteElementString("Longitude",  Longitude.ToString())
   writer.WriteElementString("Name",  Name)
   writer.WriteElementString("Provider",  Provider.ToString())
   writer.WriteElementString("SMSCode",  SMSCode.ToString())
   writer.WriteElementString("ViewOrder",  ViewOrder.ToString())
   writer.WriteEndElement()
  End Sub
#End Region

#Region " ToXml Methods "
  Public Function ToXml() As String
   Return ToXml("Station")
  End Function

  Public Function ToXml(elementName As String) As String
   Dim xml As New StringBuilder
   xml.Append("<")
   xml.Append(elementName)
   AddAttribute(xml, "StationId", StationId.ToString())
   AddAttribute(xml, "Altitude", Altitude.ToString())
   AddAttribute(xml, "Code", Code)
   AddAttribute(xml, "Deco", Deco.ToString())
   AddAttribute(xml, "LastSuccessfulRetrieval", LastSuccessfulRetrieval.ToUniversalTime.ToString("u"))
   AddAttribute(xml, "LatestStatus", LatestStatus)
   AddAttribute(xml, "Latitude", Latitude.ToString())
   AddAttribute(xml, "Longitude", Longitude.ToString())
   AddAttribute(xml, "Name", Name)
   AddAttribute(xml, "Provider", Provider.ToString())
   AddAttribute(xml, "SMSCode", SMSCode.ToString())
   AddAttribute(xml, "ViewOrder", ViewOrder.ToString())
   xml.Append(" />")
   Return xml.ToString
  End Function

  Private Sub AddAttribute(ByRef xml As StringBuilder, attributeName As String, attributeValue As String)
   xml.Append(" " & attributeName)
   xml.Append("=""" & attributeValue & """")
  End Sub
#End Region

#Region " JSON Serialization "
  Public Sub WriteJSON(ByRef s As Stream)
   Dim ser As New DataContractJsonSerializer(GetType(StationInfo))
   ser.WriteObject(s, Me)
  End Sub
#End Region

 End Class
End Namespace


