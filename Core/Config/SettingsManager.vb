Imports MySql.Data.MySqlClient

Namespace Config
    Public Class SettingsManager

        Private ReadOnly _connectionString As String

        Public Sub New(connectionString As String)
            _connectionString = connectionString
        End Sub
        Public Async Function SetSettingAsync(key As String, value As String) As Task

            Using conn As New MySqlConnection(_connectionString)
                Await conn.OpenAsync()

                Dim sql = "
                INSERT INTO settings (setting_key, setting_value)
                VALUES (@key, @value)
                ON DUPLICATE KEY UPDATE setting_value = @value
                "

                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@key", key)
                    cmd.Parameters.AddWithValue("@value", value)
                    Await cmd.ExecuteNonQueryAsync()
                End Using
            End Using

        End Function
        Public Async Function GetSettingAsync(key As String) As Task(Of String)

            Using conn As New MySqlConnection(_connectionString)
                Await conn.OpenAsync()

                Dim sql = "SELECT setting_value FROM settings WHERE setting_key = @key LIMIT 1"

                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@key", key)

                    Dim result = Await cmd.ExecuteScalarAsync()
                    Return If(result Is Nothing, Nothing, result.ToString())
                End Using
            End Using

        End Function

        Public Async Function GetAllSettings() As Task(Of Dictionary(Of String, String))
            Dim settings As New Dictionary(Of String, String)()

            Using conn As New MySqlConnection(_connectionString)
                Await conn.OpenAsync()
                Dim sql = "SELECT setting_key, setting_value FROM settings"
                Using cmd As New MySqlCommand(sql, conn)
                    Using reader = Await cmd.ExecuteReaderAsync()
                        While Await reader.ReadAsync()
                            Dim key = reader("setting_key").ToString()
                            Dim value = If(reader("setting_value") IsNot DBNull.Value, reader("setting_value").ToString(), "")
                            settings(key) = value
                        End While
                    End Using
                End Using
            End Using

            Return settings
        End Function

        Public Async Function DeleteSettingAsync(key As String) As Task
            Using conn As New MySqlConnection(_connectionString)
                Await conn.OpenAsync()
                Dim sql = "DELETE FROM settings WHERE setting_key = @key LIMIT 1"
                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@key", key)
                    Await cmd.ExecuteNonQueryAsync()
                End Using
            End Using
        End Function


        ' Shortcut helpers
        Public Async Function GetBaseUrl() As Task(Of String)
            Return Await GetSettingAsync("base_url")
        End Function

        Public Async Function GetPin() As Task(Of String)
            Return Await GetSettingAsync("pin")
        End Function

        Public Async Function GetBranchId() As Task(Of String)
            Return Await GetSettingAsync("branch_id")
        End Function

        Public Async Function GetDeviceSerial() As Task(Of String)
            Return Await GetSettingAsync("device_serial")
        End Function
        Public Async Function GetTimeout() As Task(Of Integer)
            Dim timeoutStr = Await GetSettingAsync("timeout")
            Dim timeout As Integer
            If Integer.TryParse(timeoutStr, timeout) Then
                Return timeout
            Else
                Return 30 ' default timeout
            End If
        End Function

    End Class
End Namespace
