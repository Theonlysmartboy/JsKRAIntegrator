Namespace Models.Branch.Customer
    Public Class CustomerResponse
        Public Property resultCd As String
        Public Property resultMsg As String
        Public Property resultDt As String
        Public Property data As CustomerData
    End Class

    Public Class CustomerData
        Public Property custList As List(Of CustomerInfo)
    End Class

    Public Class CustomerInfo
        Public Property Tin As String
        Public Property TaxprNm As String
        Public Property TaxprSttsCd As String
        Public Property PrvncNm As String
        Public Property DstrtNm As String
        Public Property SctrNm As String
        Public Property LocDesc As String
    End Class
End Namespace