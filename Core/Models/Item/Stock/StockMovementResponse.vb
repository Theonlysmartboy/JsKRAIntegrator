Namespace Models.Item.Stock
    Public Class StockMovementResponse
        Public Property resultCd As String
        Public Property resultMsg As String
        Public Property resultDt As String
        Public Property data As StockMovementData
    End Class

    Public Class StockMovementData
        Public Property stockList As List(Of StockMovementRecord)
    End Class

    Public Class StockMovementRecord
        Public Property sarNo As Integer
        Public Property orgSarNo As String
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
        Public Property itemList As List(Of StockMovementItem)
    End Class

    Public Class StockMovementItem
        Public Property Id As Integer
        Public Property itemSeq As Integer
        Public Property itemCd As String
        Public Property itemClsCd As String
        Public Property itemNm As String
        Public Property bcd As String
        Public Property pkgUnitCd As String
        Public Property pkg As Decimal
        Public Property qtyUnitCd As String
        Public Property qty As Decimal
        Public Property itemExprDt As String
        Public Property prc As Decimal
        Public Property splyAmt As Decimal
        Public Property totDcAmt As Decimal
        Public Property taxblAmt As Decimal
        Public Property taxTyCd As String
        Public Property taxAmt As Decimal
        Public Property totAmt As Decimal
        Public Property toUpload As Boolean
    End Class
End Namespace