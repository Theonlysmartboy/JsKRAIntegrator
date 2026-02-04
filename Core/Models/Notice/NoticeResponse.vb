Namespace Models.Notice

    Public Class NoticeResponse
        Public Property resultCd As String
        Public Property resultMsg As String
        Public Property resultDt As String
        Public Property data As NoticeData
    End Class

    Public Class NoticeData
        Public Property noticeList As List(Of NoticeItem)
    End Class

    Public Class NoticeItem
        Public Property noticeNo As Integer
        Public Property title As String
        Public Property cont As String
        Public Property dtlUrl As String
        Public Property regrNm As String
        Public Property regDt As String
    End Class

End Namespace
