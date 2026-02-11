Namespace Models.Item.Stock
    Public Class StockMovementSaveRequest
        Public Property tin As String
        Public Property bhfId As String
        Public Property sarNo As Integer
        Public Property orgSarNo As Integer
        Public Property regTyCd As String
        Public Property custTin As String
        Public Property custNm As String
        Public Property custBhfId As String
        Public Property sarTyCd As String
        Public Property ocrnDt As String
        Public Property totItemCnt As Integer
        Public Property totTaxblAmt As Decimal
        Public Property totTaxAmt As Decimal
        Public Property totAmt As Decimal
        Public Property remark As String
        Public Property regrId As String
        Public Property regrNm As String
        Public Property modrId As String
        Public Property modrNm As String
        Public Property itemList As List(Of StockMovementItem)
    End Class
End Namespace
