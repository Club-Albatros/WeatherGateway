Imports System.IO
Imports System.Text

Namespace Common
 Public Class Logger
  Implements IDisposable

#Region " Properties "
  Private _logStream As StreamWriter
  Public ReadOnly Property LogStream As StreamWriter
   Get
    If _logStream Is Nothing Then
     Dim logPath As String = DotNetNuke.Common.HostMapPath & "\WeatherGateway\Log\"
     Try
      If Not Directory.Exists(logPath) Then Directory.CreateDirectory(logPath)
      _logStream = New StreamWriter(String.Format("{0}{1:yyyyMM}.log", logPath, Now), True, Encoding.UTF8)
     Catch ex As Exception
     End Try
    End If
    Return _logStream
   End Get
  End Property
#End Region

#Region " Public Methods "
  Public Sub WriteLine(message As String)
   If LogStream Is Nothing Then Exit Sub
   LogStream.WriteLine([String].Format("{0:dd HH:mm:ss} {1}", DateTime.Now, message))
  End Sub

  Public Sub WriteLineFormat(format As String, ParamArray args() As Object)
   WriteLine(String.Format(format, args))
  End Sub

  Public Sub WriteError(component As String, err As Exception)
   WriteLineFormat("An error occurred in {0}", component)
   WriteLineFormat("Error message: {0}", err.Message)
   WriteLine(err.StackTrace)
  End Sub

  Public Sub WritePaddingLine()
   If LogStream Is Nothing Then Exit Sub
   LogStream.WriteLine()
  End Sub
#End Region

#Region " IDisposable Support "
  Private disposedValue As Boolean ' To detect redundant calls

  ' IDisposable
  Protected Overridable Sub Dispose(disposing As Boolean)
   If Not Me.disposedValue Then
    If disposing Then
     ' TODO: dispose managed state (managed objects).
     If _logStream IsNot Nothing Then
      _logStream.Flush()
      _logStream.Close()
      _logStream.Dispose()
     End If
    End If

    ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
    ' TODO: set large fields to null.
   End If
   Me.disposedValue = True
  End Sub

  ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
  'Protected Overrides Sub Finalize()
  '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
  '    Dispose(False)
  '    MyBase.Finalize()
  'End Sub

  ' This code added by Visual Basic to correctly implement the disposable pattern.
  Public Sub Dispose() Implements IDisposable.Dispose
   ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
   Dispose(True)
   GC.SuppressFinalize(Me)
  End Sub
#End Region

 End Class
End Namespace
