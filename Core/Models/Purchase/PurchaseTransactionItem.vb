Namespace Models.Purchase
    Public Class PurchaseTransactionItem
        Public Property Id As Integer
        Public Property PurchaseId As Integer
        Public Property ItemSeq As Integer
        Public Property ItemCd As String
        Public Property ItemClsCd As String
        Public Property ItemNm As String
        Public Property Qty As Decimal
        Public Property Prc As Decimal
        Public Property TaxTyCd As String
        Public Property TaxAmt As Decimal
        Public Property TotAmt As Decimal
        Public Property pkgUnitCd As String
        Public Property pkg As Integer
        Public Property qtyUnitCd As String
        Public Property splyAmt As Decimal
        Public Property itemExprDt As Object
        Public Property taxblAmt As Decimal
        Public Property dcAmt As Integer
        Public Property dcRt As Integer
    End Class
End Namespace