Imports System
Imports System.Runtime.Serialization

Namespace Entities.Measurements
  Partial Public Class MeasurementInfo

#Region " Public Properties "
  <DataMember()>
  Public ReadOnly Property WindDirectionNEWS() As String
   Get
    Select Case WindDirection
     Case Is < 23
      Return "N"
     Case Is < 68
      Return "NE"
     Case Is < 113
      Return "E"
     Case Is < 158
      Return "SE"
     Case Is < 203
      Return "S"
     Case Is < 248
      Return "SW"
     Case Is < 293
      Return "W"
     Case Is < 338
      Return "NW"
     Case Else
      Return "N"
    End Select
   End Get
  End Property
#End Region

 End Class
End Namespace


