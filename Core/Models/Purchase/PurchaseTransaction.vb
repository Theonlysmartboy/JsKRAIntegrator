Namespace Models.Purchase
    Public Class PurchaseTransaction
        Public Property Id As Integer
        Public Property InvcNo As Integer
        Public Property RegTyCd As String
        Public Property PchsTyCd As String
        Public Property RcptTyCd As String
        Public Property PmtTyCd As String
        Public Property PchsSttsCd As String
        Public Property CfmDt As String
        Public Property PchsDt As String
        Public Property TotItemCnt As Integer
        Public Property TotTaxblAmt As Decimal
        Public Property TotTaxAmt As Decimal
        Public Property TotAmt As Decimal
        Public Property Remark As String
        Public Property IsUploaded As Boolean
        Public Property Items As List(Of PurchaseTransactionItem)
    End Class
End Namespace