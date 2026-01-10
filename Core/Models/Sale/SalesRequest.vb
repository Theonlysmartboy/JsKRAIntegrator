Namespace Models.Sale
    Public Class SalesRequest
        Public Property tin As String
        Public Property bhfId As String
        Public Property trdInvcNo As String
        Public Property invcNo As Long
        Public Property orgInvcNo As Long
        Public Property custTin As String
        Public Property custNm As String
        Public Property salesTyCd As String
        Public Property rcptTyCd As String
        Public Property pmtTyCd As String
        Public Property salesSttsCd As String
        Public Property cfmDt As String
        Public Property salesDt As String
        'Public Property stockRlsDt As String
        Public Property totItemCnt As Integer
        Public Property taxblAmtA As Decimal
        Public Property taxblAmtB As Decimal
        Public Property taxblAmtC As Decimal
        Public Property taxblAmtD As Decimal
        Public Property taxblAmtE As Decimal
        Public Property taxRtA As Decimal
        Public Property taxRtB As Decimal
        Public Property taxRtC As Decimal
        Public Property taxRtD As Decimal
        Public Property taxRtE As Decimal
        Public Property taxAmtA As Decimal
        Public Property taxAmtB As Decimal
        Public Property taxAmtC As Decimal
        Public Property taxAmtD As Decimal
        Public Property taxAmtE As Decimal
        Public Property totTaxblAmt As Decimal
        Public Property totTaxAmt As Decimal
        Public Property totAmt As Decimal
        Public Property prchrAcptcYn As String
        Public Property remark As String
        Public Property regrId As String
        Public Property regrNm As String
        Public Property modrId As String
        Public Property modrNm As String
        Public Property receipt As Object
        Public Property itemList As List(Of Object)
    End Class
End Namespace
