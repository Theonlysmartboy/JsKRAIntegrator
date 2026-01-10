Namespace Models.Purchase
    Public Class PurchaseInfoResponse
        Inherits BaseResponse

        Public Property data As PurchaseInfoData
    End Class

    Public Class PurchaseInfoData
        Public Property saleList As List(Of PurchaseRecord)
    End Class

    Public Class PurchaseRecord
        Public Property spplrTin As String
        Public Property spplrNm As String
        Public Property spplrBhfId As String
        Public Property spplrInvcNo As Integer
        Public Property spplrSdcId As String
        Public Property spplrMrcNo As String

        Public Property rcptTyCd As String
        Public Property pmtTyCd As String
        Public Property cfmDt As String
        Public Property salesDt As String
        Public Property stockRlsDt As String

        Public Property totItemCnt As Integer
        Public Property totAmt As Decimal
        Public Property totTaxAmt As Decimal

        Public Property itemList As List(Of PurchaseItemList)
    End Class

    Public Class PurchaseItemList
        Public Property itemSeq As Integer
        Public Property itemCd As String
        Public Property itemClsCd As String
        Public Property itemNm As String
        Public Property bcd As String
        Public Property pkgUnitCd As String
        Public Property pkg As Integer
        Public Property qtyUnitCd As String
        Public Property qty As Decimal
        Public Property prc As Decimal
        Public Property splyAmt As Decimal
        Public Property dcRt As Decimal
        Public Property dcAmt As Decimal
        Public Property taxTyCd As String
        Public Property taxblAmt As Decimal
        Public Property taxAmt As Decimal
        Public Property totAmt As Decimal
    End Class
End Namespace
