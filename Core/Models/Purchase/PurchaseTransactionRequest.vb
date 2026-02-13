Namespace Models.Purchase
    Public Class PurchaseTransactionRequest
        Public Property tin As String
        Public Property spplrTin As String
        Public Property bhfId As String
        Public Property invcNo As Integer
        Public Property orgInvcNo As Integer
        Public Property spplrBhfId As String
        Public Property spplrNm As String
        Public Property spplrInvcNo As Integer
        Public Property spplrSdcId As String
        Public Property spplrAddr As String
        Public Property regTyCd As String
        Public Property pchsTyCd As String
        Public Property rcptTyCd As String
        Public Property pmtTyCd As String
        Public Property pchsSttsCd As String
        Public Property cfmDt As String
        Public Property pchsDt As String
        Public Property wrhsDt As String
        Public Property cnclReqDt As String
        Public Property cnclDt As String
        Public Property rfdDt As String
        Public Property totItemCnt As Integer
        Public Property taxblAmtA As Decimal
        Public Property taxblAmtB As Decimal
        Public Property taxblAmtC As Decimal
        Public Property taxblAmtD As Decimal
        Public Property taxblAmtE As Decimal
        Public Property taxAmtA As Decimal
        Public Property taxAmtB As Decimal
        Public Property taxAmtC As Decimal
        Public Property taxAmtD As Decimal
        Public Property taxAmtE As Decimal
        Public Property taxRtA As Decimal
        Public Property taxRtB As Decimal
        Public Property taxRtC As Decimal
        Public Property taxRtD As Decimal
        Public Property taxRtE As Decimal
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