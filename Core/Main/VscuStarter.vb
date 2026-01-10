Imports System.IO
Imports Core.Config
Imports Core.Enums
Imports Core.Logging
Namespace Main

    Public Class VscuStarter
        Private _connString As String
        Private _jarPath As String
        Private _settingsManager As SettingsManager
        Private _logger As Logger
        Private Shared _vscuProcess As Process

        Public Sub New(conn As String)
            _connString = conn
            _settingsManager = New SettingsManager(_connString)
            Dim repo As New LogRepository(_connString)
            _logger = New Logger(repo)
        End Sub

        Public Async Function StartKraVscuJar() As Task(Of Boolean)
            Dim errorMessage As String = Nothing
            Try
                _jarPath = Await _settingsManager.GetSettingAsync("vscu_jar_path")

                If String.IsNullOrWhiteSpace(_jarPath) OrElse Not IO.File.Exists(_jarPath) Then
                    errorMessage = $"VSCU jar file invalid at: {_jarPath}"
                    GoTo Finish
                End If

                Dim folder = Path.GetDirectoryName(_jarPath)
                Dim psi As New ProcessStartInfo() With {
                    .FileName = "java",
                    .Arguments = "-jar """ & Path.GetFileName(_jarPath) & """",
                    .WorkingDirectory = folder,
                    .UseShellExecute = True,
                    .CreateNoWindow = False,
                    .WindowStyle = ProcessWindowStyle.Normal
                }

                Dim p = Process.Start(psi)
                If p IsNot Nothing Then
                    _vscuProcess = p
                End If

Finish:
            Catch ex As Exception
                errorMessage = "Failed to start KRA VSCU service: " & ex.Message
            End Try

            If errorMessage IsNot Nothing Then
                Await _logger.LogAsync(LogLevel.Error, "StartKraVscuJar", errorMessage)
                Return False
            Else
                Await _logger.LogAsync(LogLevel.Info, "StartKraVscuJar", "KRA VSCU service started successfully.")
                Return True
            End If
        End Function

        Public Async Function IsJarRunning() As Task(Of Boolean)
            Dim errorMessage As String = Nothing
            Dim warningMessage As String = Nothing
            Dim result As Boolean = False
            Try
                Dim ipProps = Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties()
                Dim listeners = ipProps.GetActiveTcpListeners()
                result = listeners.Any(Function(ep) ep.Port = 8088)
                If result Then
                    warningMessage = "KRA VSCU service already is running."
                End If
            Catch ex As Exception
                errorMessage = "Failed to check port: " & ex.Message
            End Try
            ' Logging
            If errorMessage IsNot Nothing Then
                Await _logger.LogAsync(LogLevel.Warning, "IsJarRunning", errorMessage)
            ElseIf warningMessage IsNot Nothing Then
                Await _logger.LogAsync(LogLevel.Warning, "IsJarRunning", warningMessage)
            End If
            Return result
        End Function

        Public Async Function StopVscu() As Task(Of String)
            Dim errorMessage As String = Nothing
            Dim result As Boolean = False
            Try
                If _vscuProcess IsNot Nothing AndAlso Not _vscuProcess.HasExited Then
                    _vscuProcess.Kill()
                    result = True
                End If
            Catch ex As Exception
                errorMessage = "Failed to stop KRA VSCU service: " & ex.Message

            End Try
            If errorMessage IsNot Nothing Then
                Await _logger.LogAsync(LogLevel.Error, "StopVscu", errorMessage)
            Else
                Await _logger.LogAsync(LogLevel.Info, "StopVscu", "KRA VSCU service stopped successfully.")
            End If
            Return _vscuProcess.Id.ToString()
        End Function

        Public Sub StopVscuByPort(port As Integer)
            Dim pid As Integer = -1

            Try
                ' Run netstat command
                Dim psi As New ProcessStartInfo("cmd.exe",
            $"/c netstat -aof | findstr :{port}") With {
            .RedirectStandardOutput = True,
            .UseShellExecute = False,
            .CreateNoWindow = True
        }

                Using p = Process.Start(psi)
                    Dim output = p.StandardOutput.ReadToEnd()

                    ' Parse PID from output
                    Dim lines = output.Split({Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)

                    For Each line In lines
                        Dim parts = line.Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries)

                        ' PID is always last column
                        Dim last = parts(parts.Length - 1)

                        If Integer.TryParse(last, pid) Then
                            Exit For
                        End If
                    Next
                End Using

                If pid > 0 Then
                    Dim proc = Process.GetProcessById(pid)
                    proc.Kill()
                End If

            Catch ex As Exception
                ' Log error if needed
            End Try
        End Sub


    End Class
End Namespace
