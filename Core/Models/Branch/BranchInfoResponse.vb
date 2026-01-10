Namespace Models.Branch
    Public Class BranchInfoResponse
        Inherits BaseResponse

        Public Property result As BranchInfoData
    End Class

    Public Class BranchInfoData
        Public Property branchId As String
        Public Property branchName As String
        Public Property address As String
        Public Property mobile As String
        Public Property registered As Boolean
    End Class
End Namespace
