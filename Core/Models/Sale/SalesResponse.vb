Namespace Models.Sale
    Public Class SalesResponse
        Inherits BaseResponse

        Public Property data As SalesResponseData
    End Class

    Public Class SalesResponseData
        Public Property rcptNo As Nullable(Of Integer)
        Public Property intrlData As String
        Public Property rcptSign As String
        Public Property totRcptNo As Nullable(Of Integer)
        Public Property vsdcRcptPbctDate As String
        Public Property sdcId As String
        Public Property mrcNo As String
    End Class
End Namespace
