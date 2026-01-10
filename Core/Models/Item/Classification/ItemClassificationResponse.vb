Namespace Models.Item.Classification

    Public Class ItemClassificationResponse
        Inherits BaseResponse
        Public Property data As ItemClassificationData
    End Class

    Public Class ItemClassificationData
        Public Property itemClsList As List(Of ItemCls)
    End Class

    Public Class ItemCls
        Public Property itemClsCd As String
        Public Property itemClsNm As String
        Public Property itemClsLvl As Integer
        Public Property taxTyCd As String
        Public Property mjrTgYn As String
        Public Property useYn As String
    End Class
End Namespace
