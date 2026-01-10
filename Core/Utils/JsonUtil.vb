Imports Newtonsoft.Json

Namespace Utils
    Public Class JsonUtil
        Public Shared Function ToJson(obj As Object) As String
            Return JsonConvert.SerializeObject(obj)
        End Function

        Public Shared Function FromJson(Of T)(json As String) As T
            Return JsonConvert.DeserializeObject(Of T)(json)
        End Function
    End Class
End Namespace
