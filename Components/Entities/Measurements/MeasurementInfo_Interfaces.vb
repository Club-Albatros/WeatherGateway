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

Namespace Entities.Measurements
 <Serializable(), XmlRoot("Measurement"), DataContract()>
 Partial Public Class MeasurementInfo
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

   Datime = CDate(Null.SetNull(dr.Item("Datime"), Datime))
   Humidity = Convert.ToDouble(Null.SetNull(dr.Item("Humidity"), Humidity))
   MeasurementId = Convert.ToInt32(Null.SetNull(dr.Item("MeasurementId"), MeasurementId))
   Pressure = Convert.ToDouble(Null.SetNull(dr.Item("Pressure"), Pressure))
   StationId = Convert.ToInt32(Null.SetNull(dr.Item("StationId"), StationId))
   Temperature = Convert.ToDouble(Null.SetNull(dr.Item("Temperature"), Temperature))
   WindAverage = Convert.ToDouble(Null.SetNull(dr.Item("WindAverage"), WindAverage))
   WindDirection = Convert.ToInt32(Null.SetNull(dr.Item("WindDirection"), WindDirection))
   WindMaximum = Convert.ToDouble(Null.SetNull(dr.Item("WindMaximum"), WindMaximum))

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
    Return MeasurementId
   End Get
   Set(value As Integer)
    MeasurementId = value
   End Set
  End Property
#End Region

#Region " IPropertyAccess Implementation "
  Public Function GetProperty(strPropertyName As String, strFormat As String, formatProvider As System.Globalization.CultureInfo, AccessingUser As DotNetNuke.Entities.Users.UserInfo, AccessLevel As DotNetNuke.Services.Tokens.Scope, ByRef PropertyNotFound As Boolean) As String Implements DotNetNuke.Services.Tokens.IPropertyAccess.GetProperty
   Dim OutputFormat As String = String.Empty
   Dim portalSettings As DotNetNuke.Entities.Portals.PortalSettings = DotNetNuke.Entities.Portals.PortalController.GetCurrentPortalSettings()
   If strFormat = String.Empty Then
    OutputFormat = ""
   Else
    OutputFormat = strFormat
   End If
   Select Case strPropertyName.ToLower
    Case "datime"
     Return (Me.Datime.ToString(OutputFormat, formatProvider))
    Case "humidity"
     Return (Me.Humidity.ToString(OutputFormat, formatProvider))
    Case "measurementid"
     Return (Me.MeasurementId.ToString(OutputFormat, formatProvider))
    Case "pressure"
     Return (Me.Pressure.ToString(OutputFormat, formatProvider))
    Case "stationid"
     Return (Me.StationId.ToString(OutputFormat, formatProvider))
    Case "temperature"
     Return (Me.Temperature.ToString(OutputFormat, formatProvider))
    Case "windaverage"
     Return (Me.WindAverage.ToString(OutputFormat, formatProvider))
    Case "winddirection"
     Return (Me.WindDirection.ToString(OutputFormat, formatProvider))
    Case "windmaximum"
     Return (Me.WindMaximum.ToString(OutputFormat, formatProvider))
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

    Date.TryParse(readElement(reader, "Datime"), Datime)
    Double.TryParse(readElement(reader, "Humidity"), Humidity)
    Double.TryParse(readElement(reader, "Pressure"), Pressure)
    If Not Int32.TryParse(readElement(reader, "StationId"), StationId) Then
     StationId = Null.NullInteger
    End If
    Double.TryParse(readElement(reader, "Temperature"), Temperature)
    Double.TryParse(readElement(reader, "WindAverage"), WindAverage)
    If Not Int32.TryParse(readElement(reader, "WindDirection"), WindDirection) Then
     WindDirection = Null.NullInteger
    End If
    Double.TryParse(readElement(reader, "WindMaximum"), WindMaximum)
   Catch ex As Exception
    ' log exception as DNN import routine does not do that
    DotNetNuke.Services.Exceptions.LogException(ex)
    ' re-raise exception to make sure import routine displays a visible error to the user
    Throw New Exception("An error occured during import of an Measurement", ex)
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
   writer.WriteStartElement("Measurement")
   writer.WriteElementString("MeasurementId", MeasurementId.ToString())
   writer.WriteElementString("Datime", Datime.ToString())
   writer.WriteElementString("Humidity", Humidity.ToString())
   writer.WriteElementString("Pressure", Pressure.ToString())
   writer.WriteElementString("StationId", StationId.ToString())
   writer.WriteElementString("Temperature", Temperature.ToString())
   writer.WriteElementString("WindAverage", WindAverage.ToString())
   writer.WriteElementString("WindDirection", WindDirection.ToString())
   writer.WriteElementString("WindMaximum", WindMaximum.ToString())
   writer.WriteEndElement()
  End Sub
#End Region

#Region " ToXml Methods "
  Public Function ToXml() As String
   Return ToXml("Measurement")
  End Function

  Public Function ToXml(elementName As String) As String
   Dim xml As New StringBuilder
   xml.Append("<")
   xml.Append(elementName)
   AddAttribute(xml, "MeasurementId", MeasurementId.ToString())
   AddAttribute(xml, "Datime", Datime.ToUniversalTime.ToString("u"))
   AddAttribute(xml, "Humidity", Humidity.ToString())
   AddAttribute(xml, "Pressure", Pressure.ToString())
   AddAttribute(xml, "StationId", StationId.ToString())
   AddAttribute(xml, "Temperature", Temperature.ToString())
   AddAttribute(xml, "WindAverage", WindAverage.ToString())
   AddAttribute(xml, "WindDirection", WindDirection.ToString())
   AddAttribute(xml, "WindMaximum", WindMaximum.ToString())
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
   Dim ser As New DataContractJsonSerializer(GetType(MeasurementInfo))
   ser.WriteObject(s, Me)
  End Sub
#End Region

 End Class
End Namespace


