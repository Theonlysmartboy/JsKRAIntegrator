Namespace Models.Purchase
    Public Class PurchaseTransactionItem
        Public Property Id As Object
        Public Property PurchaseId As Integer
        Public Property itemSeq As Integer
        Public Property itemCd As String
        Public Property itemClsCd As String
        Public Property itemNm As String
        Public Property qty As Decimal
        Public Property prc As Decimal
        Public Property taxTyCd As String
        Public Property taxAmt As Decimal
        Public Property totAmt As Decimal
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