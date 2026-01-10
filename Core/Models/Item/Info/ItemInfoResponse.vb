Namespace Models.Item.Info

    Public Class ItemInfoResponse
        Inherits BaseResponse
        Public Property data As ItemInfoData
    End Class

    Public Class ItemInfoData
        Public Property itemList As List(Of ItemInfo)
    End Class

    Public Class ItemInfo
        Public Property tin As String
        Public Property itemCd As String
        Public Property itemClsCd As String
        Public Property itemTyCd As String
        Public Property itemNm As String
        Public Property itemStdNm As String
        Public Property orgnNatCd As String
        Public Property pkgUnitCd As String
        Public Property qtyUnitCd As String
        Public Property taxTyCd As String
        Public Property btchNo As String
        Public Property regBhfId As String
        Public Property bcd As String
        Public Property dftPrc As Decimal
        Public Property grpPrcL1 As Decimal
        Public Property grpPrcL2 As Decimal
        Public Property grpPrcL3 As Decimal
        Public Property grpPrcL4 As Decimal
        Public Property grpPrcL5 As Decimal
        Public Property addInfo As String
        Public Property sftyQty As Decimal
        Public Property isrcAplcbYn As String
        Public Property rraModYn As String
        Public Property useYn As String
    End Class
End Namespace
