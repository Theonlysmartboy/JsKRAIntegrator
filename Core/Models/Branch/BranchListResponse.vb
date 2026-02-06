Namespace Models.Branch

    Public Class BranchListResponse
        Public Property resultCd As String
        Public Property resultMsg As String
        Public Property resultDt As String
        Public Property data As BranchListData
    End Class

    Public Class BranchListData
        Public Property bhfList As List(Of BranchInfo)
    End Class

    Public Class BranchInfo
        Public Property tin As String
        Public Property bhfId As String
        Public Property bhfNm As String
        Public Property bhfSttsCd As String
        Public Property prvncNm As String
        Public Property dstrtNm As String
        Public Property sctrNm As String
        Public Property locDesc As String
        Public Property mgrNm As String
        Public Property mgrTelNo As String
        Public Property mgrEmail As String
        Public Property hqYn As String
    End Class

End Namespace
