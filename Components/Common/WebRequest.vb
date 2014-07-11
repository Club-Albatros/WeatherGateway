Imports System.Net
Imports System.IO
Imports System.Text
Imports System.Collections.Specialized
Imports System.Web

Namespace Common
 Public Class WebRequest

#Region " Private Members "
  Private _PostData As NameValueCollection
  Private _PostStream As MemoryStream
  Private _PostWriter As BinaryWriter
  Private Const MultiPartBoundary As String = "-----------------------------7cf2a327f01ae"
#End Region

#Region " Properties "
  Public Property Url As String
  Public Property IsInError As Boolean = False
  Public Property WebRequest As HttpWebRequest
  Public Property WebResponse As HttpWebResponse
  Public Property ThrowExceptions As Boolean = False
  Public Property ErrorMsg As String = ""
  Public Property Timeout As Integer = 30
  Public Property PostMode As PostModeType = PostModeType.URLEncoded
  Public ReadOnly Property PostData() As NameValueCollection
   Get
    Return _PostData
   End Get
  End Property
#End Region

#Region " Constructors "
  Public Sub New()
   _PostData = New NameValueCollection
  End Sub
#End Region

#Region " Public Methods "
  Public Sub AddPostdata(PostCollection As NameValueCollection)
   For Each key As String In PostCollection.Keys
    _PostData.Add(key, PostCollection(key))
   Next
  End Sub

  Public Sub AddPostdata(VarName As String, VarValue As String)
   _PostData.Add(VarName, VarValue)
  End Sub

  Public Function GetResponse() As String

   Dim Resp As StreamReader = Me.GetResponseStream()
   If Resp Is Nothing Then
    Return ""
   Else
    Dim lcResult As String = Resp.ReadToEnd()
    Resp.Close()
    Return lcResult
   End If

  End Function

  Public Function GetResponseStream(Optional Request As HttpWebRequest = Nothing) As StreamReader

   Try

    If Request Is Nothing Then
     Request = CType(Net.WebRequest.Create(Me.Url), HttpWebRequest)
    End If
    With Request
     .UserAgent = "Albatros"
     .Timeout = Me.Timeout * 1000
    End With

    ' Handle Post Data
    If Me.PostData.Count > 0 Then

     Request.Method = "POST"
     Select Case Me.PostMode
      Case PostModeType.URLEncoded
       Request.ContentType = "application/x-www-form-urlencoded"
      Case PostModeType.MultipartForm
       Request.ContentType = "multipart/form-data; boundary=" + MultiPartBoundary
      Case Else
       Request.ContentType = "text/xml"
     End Select

     ' Add post data
     Using postStream As New IO.MemoryStream

      Using postWriter As New IO.BinaryWriter(postStream)
       Dim iTotal As Integer = _PostData.Keys.Count
       Dim i As Integer = 0
       For Each key As String In _PostData.Keys
        i += 1
        Dim str As String
        Select Case _PostMode
         Case PostModeType.URLEncoded
          str = key & "=" & System.Web.HttpUtility.UrlEncode(Encoding.GetEncoding(1252).GetBytes(_PostData(key)))
          If i < iTotal Then
           str &= "&"
          End If
         Case PostModeType.MultipartForm
          str = "--" & MultiPartBoundary & "\r\n" & "Content-Disposition: form-data; name=\""" & key & "\""\r\n\r\n"
         Case Else
          str = _PostData(key)
        End Select
        postWriter.Write(Encoding.GetEncoding(1252).GetBytes(str))
       Next

       ' Inject into request stream
       Using ReqStream As Stream = Request.GetRequestStream
        postStream.WriteTo(ReqStream)
       End Using

      End Using

     End Using

    End If ' post data

    Dim Response As HttpWebResponse = CType(Request.GetResponse, HttpWebResponse)
    Me.WebResponse = Response

    Dim enc As Encoding
    Try
     If Response.ContentEncoding.Length > 0 Then
      enc = Encoding.GetEncoding(Response.ContentEncoding)
     Else
      enc = Encoding.GetEncoding(1252)
     End If
    Catch ex As Exception
     enc = Encoding.GetEncoding(1252)
    End Try

    Return New StreamReader(Response.GetResponseStream, enc)

   Catch ex As Exception

    If Me.ThrowExceptions Then
     Throw (ex)
    Else
     Me.ErrorMsg = ex.Message
     Me.IsInError = True
     Return Nothing
    End If

   End Try
  End Function
#End Region

#Region " Event Handling "
  Public Event OnReceiveData As OnReceiveDataHandler

  Public Delegate Sub OnReceiveDataHandler(sender As Object, e As OnReceiveDataEventArgs)

  Public Class OnReceiveDataEventArgs
   Public CurrentByteCount As Long = 0
   Public TotalBytes As Long = 0
   Public NumberOfReads As Integer = 0
   Public CurrentChunk As Char()
   Public Done As Boolean = False
   Public Cancel As Boolean = False
  End Class
#End Region

#Region " Public Enums "
  Public Enum PostModeType
   URLEncoded
   MultipartForm
   Raw
  End Enum
#End Region

 End Class
End Namespace