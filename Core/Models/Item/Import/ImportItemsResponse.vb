Namespace Models.Item.Import
    Public Class ImportItemsResponse
        Public Property resultCd As String
        Public Property resultMsg As String
        Public Property resultDt As String
        Public Property data As ImportItemsData
    End Class

    Public Class ImportItemsData
        Public Property itemList As List(Of ImportItem)
    End Class

    Public Class ImportItem
        Public Property taskCd As String
        Public Property dclDe As String
        Public Property itemSeq As Integer
        Public Property dclNo As String
        Public Property hsCd As String
        Public Property itemNm As String
        Public Property imptItemsttsCd As String
        Public Property orgnNatCd As String
        Public Property exptNatCd As String
        Public Property pkg As Decimal?
        Public Property pkgUnitCd As String
        Public Property qty As Decimal?
        Public Property qtyUnitCd As String
        Public Property totWt As Decimal?
        Public Property netWt As Decimal?
        Public Property spplrNm As String
        Public Property agntNm As String
        Public Property invcFcurAmt As Decimal?
        Public Property invcFcurCd As String
        Public Property invcFcurExcrt As Decimal?
    End Class
End Namespace