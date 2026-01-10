Imports Core.Enums

Namespace Logging
    Public Class Logger
        Private ReadOnly _repo As LogRepository

        Public Sub New(repo As LogRepository)
            _repo = repo
        End Sub

        Public Async Function LogAsync(level As LogLevel,
                                       message As String,
                                       Optional payload As String = "") As Task

            Dim entry As New LogEntry With {
                .Timestamp = DateTime.Now,
                .Level = level,
                .Message = message,
                .Payload = payload
            }

            Await _repo.AddLogAsync(entry.Level, entry.Message, entry.Payload)

        End Function

    End Class
End Namespace
