Namespace Models.Item.Product

    Public Class ItemSaveRequest
        Public Property tin As String
        Public Property bhfId As String
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
        Public Property bcd As String
        Public Property dftPrc As Decimal
        Public Property grpPrcL1 As Decimal
        Public Property grpPrcL2 As Decimal?
        Public Property grpPrcL3 As Decimal?
        Public Property grpPrcL4 As Decimal?
        Public Property grpPrcL5 As Decimal?
        Public Property addInfo As String
        Public Property sftyQty As Decimal?
        Public Property isrcAplcbYn As String
        Public Property useYn As String
        Public Property regrNm As String
        Public Property regrId As String
        Public Property modrNm As String
        Public Property modrId As String
    End Class

    Public Class BaseResponse
        Public Property resultCd As String
        Public Property resultMsg As String
        Public Property resultDt As String
    End Class

End Namespace
