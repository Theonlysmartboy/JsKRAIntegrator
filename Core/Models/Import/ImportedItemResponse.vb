Namespace Models.Import
    Public Class ImportedItemResponse
        Inherits BaseResponse

        Public Property result As ImportedItemData
    End Class

    Public Class ImportedItemData
        Public Property itemCode As String
        Public Property importId As String
        Public Property accepted As Boolean
    End Class
End Namespace
