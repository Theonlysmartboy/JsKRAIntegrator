Imports System.Net.Http
Imports System.Text
Namespace Main

    Public Class ApiClient
        Private ReadOnly _baseUrl As String
        Private ReadOnly _timeoutMs As Integer
        Private ReadOnly _http As HttpClient

        Public Sub New(baseUrl As String, timeoutMs As Integer)
            _baseUrl = If(String.IsNullOrWhiteSpace(baseUrl), "http://localhost:8088", baseUrl).TrimEnd("/"c)
            _timeoutMs = If(timeoutMs <= 0, 30000, timeoutMs)

            _http = New HttpClient()
            _http.Timeout = TimeSpan.FromMilliseconds(_timeoutMs)
        End Sub

        Private Function BuildUrl(endpoint As String) As String
            If String.IsNullOrWhiteSpace(endpoint) Then
                Return _baseUrl
            End If

            If endpoint.StartsWith("/") Then
                Return _baseUrl & endpoint
            Else
                Return _baseUrl & "/" & endpoint
            End If
        End Function

        ''' <summary>
        ''' Returns the full URL for logging / diagnostics.
        ''' </summary>
        Public Function FullUrl(endpoint As String) As String
            Return BuildUrl(endpoint)
        End Function

        ''' <summary>
        ''' POST JSON payload to endpoint and return raw response body.
        ''' </summary>
        Public Async Function SendAsync(endpoint As String, json As String) As Task(Of String)
            Dim url = BuildUrl(endpoint)
            Dim content = New StringContent(json, Encoding.UTF8, "application/json")
            Dim resp = Await _http.PostAsync(url, content)
            resp.EnsureSuccessStatusCode()
            Return Await resp.Content.ReadAsStringAsync()
        End Function

        ''' <summary>
        ''' GET endpoint and return raw response body.
        ''' </summary>
        Public Async Function GetAsync(endpoint As String) As Task(Of String)
            Dim url = BuildUrl(endpoint)
            Dim resp = Await _http.GetAsync(url)
            resp.EnsureSuccessStatusCode()
            Return Await resp.Content.ReadAsStringAsync()
        End Function
    End Class
End Namespace
