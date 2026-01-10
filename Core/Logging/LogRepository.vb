Imports Core.Enums
Imports MySql.Data.MySqlClient

Namespace Logging
    Public Class LogRepository

        Private ReadOnly _connectionString As String

        Public Sub New(connectionString As String)
            _connectionString = connectionString
        End Sub

        Public Async Function AddLogAsync(level As LogLevel,
                                          message As String,
                                          details As String) As Task

            Using conn As New MySqlConnection(_connectionString)
                Await conn.OpenAsync()

                Dim sql = "INSERT INTO logs (level, message, details, created_at) 
                           VALUES (@level, @message, @details, NOW())"

                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@level", level.ToString())
                    cmd.Parameters.AddWithValue("@message", message)
                    cmd.Parameters.AddWithValue("@details", details)

                    Await cmd.ExecuteNonQueryAsync()
                End Using
            End Using

        End Function

        Public Async Function GetLogsAsync(Optional limit As Integer = 200) As Task(Of DataTable)
            Dim dt As New DataTable()
            dt.Columns.Add("Id", GetType(Integer))
            dt.Columns.Add("Level", GetType(String))
            dt.Columns.Add("Message", GetType(String))
            dt.Columns.Add("Details", GetType(String))
            dt.Columns.Add("Timestamp", GetType(DateTime))
            For Each col As DataColumn In dt.Columns
                col.AllowDBNull = True
            Next
            Using conn As New MySqlConnection(_connectionString)
                Await conn.OpenAsync()
                Dim sql = "SELECT id As Id, level As Level, message As Message, details As Details," &
                            "created_at As Timestamp FROM logs ORDER BY Id ASC LIMIT @limit"
                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@limit", limit)
                    Using reader = Await cmd.ExecuteReaderAsync()
                        dt.BeginLoadData()
                        dt.Load(reader)
                        dt.EndLoadData()
                    End Using
                End Using
            End Using
            Return dt
        End Function

        Public Async Function ClearLogsAsync() As Task
            Using conn As New MySqlConnection(_connectionString)
                Await conn.OpenAsync()

                Dim query As String = "DELETE FROM logs"
                Using cmd As New MySqlCommand(query, conn)
                    Await cmd.ExecuteNonQueryAsync()
                End Using
            End Using
        End Function


    End Class
End Namespace
