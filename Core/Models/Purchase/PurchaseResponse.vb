Namespace Models.Purchase
    Public Class PurchaseResponse
        Inherits BaseResponse

        Public Property result As PurchaseData
    End Class

    Public Class PurchaseData
        Public Property purchaseId As String
        Public Property accepted As Boolean
    End Class
End Namespace
