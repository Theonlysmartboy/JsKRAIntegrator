Namespace Models.Code
    Public Class CodeDataResponse
        Inherits BaseResponse
        Public Property data As CodeDataRoot
    End Class

    Public Class CodeDataRoot
        Public Property clsList As List(Of CodeClass)
    End Class

    Public Class CodeClass
        Public Property cdCls As String
        Public Property cdClsNm As String
        Public Property cdClsDesc As String
        Public Property useYn As String
        Public Property userDfnNm1 As String
        Public Property userDfnNm2 As String
        Public Property userDfnNm3 As String
        Public Property dtlList As List(Of CodeDetail)
    End Class

    Public Class CodeDetail
        Public Property cd As String
        Public Property cdNm As String
        Public Property cdDesc As String
        Public Property useYn As String
        Public Property srtOrd As Integer
        Public Property userDfnCd1 As String
        Public Property userDfnCd2 As String
        Public Property userDfnCd3 As String
        Public Property remark As String
    End Class
End Namespace
