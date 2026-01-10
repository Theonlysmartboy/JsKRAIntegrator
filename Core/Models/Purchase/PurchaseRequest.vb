Namespace Models.Purchase
    Public Class PurchaseRequest
        Public Property purchaseId As String
        Public Property requestDate As String
        Public Property supplierName As String
        Public Property supplierPin As String
        Public Property items As List(Of PurchaseItem)
        Public Property total As Decimal
        Public Property pin As String
    End Class

    Public Class PurchaseItem
        Public Property itemCode As String
        Public Property quantity As Decimal
        Public Property cost As Decimal
    End Class
End Namespace
