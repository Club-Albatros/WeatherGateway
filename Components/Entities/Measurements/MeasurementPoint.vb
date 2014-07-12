Imports System.Runtime.Serialization

Imports DotNetNuke.Common.Utilities
Imports DotNetNuke.Entities.Modules

Namespace Entities.Measurements
 Public Class MeasurementPoint
  Implements IHydratable

  <DataMember()>
  Public Property DT As DateTime
  <DataMember()>
  Public Property WD As Integer
  <DataMember()>
  Public Property WA As Integer
  <DataMember()>
  Public Property WM As Integer

  Public Sub Fill(dr As System.Data.IDataReader) Implements DotNetNuke.Entities.Modules.IHydratable.Fill
   DT = CDate(Null.SetNull(dr.Item("Datime"), DT))
   WD = Convert.ToInt32(Null.SetNull(dr.Item("WD"), WD))
   WA = Convert.ToInt32(Null.SetNull(dr.Item("WA"), WA))
   WM = Convert.ToInt32(Null.SetNull(dr.Item("WM"), WM))
  End Sub

  Public Property KeyID As Integer Implements DotNetNuke.Entities.Modules.IHydratable.KeyID
   Get
    Return Null.NullInteger
   End Get
   Set(value As Integer)
   End Set
  End Property
 End Class
End Namespace
