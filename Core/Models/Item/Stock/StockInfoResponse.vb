Namespace Models.Item.Stock
    Public Class StockInfoResponse
        Inherits BaseResponse

        Public Property result As StockInfoData
    End Class

    Public Class StockInfoData
        Public Property itemCode As String
        Public Property currentQuantity As Decimal
        Public Property updated As Boolean
    End Class
End Namespace
