
Imports Core.Enums

Namespace Logging
    Public Class LogEntry
        Public Property Id As Integer
        Public Property Timestamp As DateTime
        Public Property Level As LogLevel
        Public Property Message As String
        Public Property Payload As String
    End Class
End Namespace
