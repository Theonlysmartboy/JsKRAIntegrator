Namespace Models.Purchase
    Public Class PurchaseTransactionRequest
        Public Property invcNo As Integer
        Public Property orgInvcNo As Integer
        Public Property regTyCd As String
        Public Property pchsTyCd As String
        Public Property rcptTyCd As String
        Public Property pmtTyCd As String
        Public Property pchsSttsCd As String
        Public Property cfmDt As String
        Public Property pchsDt As String
        Public Property wrhsDt As String
        Public Property totItemCnt As Integer
        Public Property totTaxblAmt As Decimal
        Public Property totTaxAmt As Decimal
        Public Property totAmt As Decimal
        Public Property remark As String
        Public Property regrNm As String
        Public Property regrId As String
        Public Property modrNm As String
        Public Property modrId As String
        Public Property itemList As List(Of PurchaseTransactionItem)
    End Class
End Namespace