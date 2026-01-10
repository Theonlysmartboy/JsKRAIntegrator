Namespace Models.Init

    ' Base response already has Code, Message, etc.
    Public Class InitInfoResponse
        Inherits BaseResponse

        ' Map 'data' JSON object
        Public Property data As InitInfoDataWrapper
    End Class

    ' Wrapper around the info object
    Public Class InitInfoDataWrapper
        Public Property info As InitInfo
    End Class

    ' Actual device info returned
    Public Class InitInfo
        Public Property tin As String
        Public Property taxprNm As String
        Public Property bsnsActv As String
        Public Property bhfId As String
        Public Property bhfNm As String
        Public Property bhfOpenDt As String
        Public Property prvncNm As String
        Public Property dstrtNm As String
        Public Property sctrNm As String
        Public Property locDesc As String
        Public Property hqYn As String
        Public Property mgrNm As String
        Public Property mgrTelNo As String
        Public Property mgrEmail As String
        Public Property sdcId As String
        Public Property mrcNo As String
        Public Property dvcId As String
        Public Property intrlKey As String
        Public Property signKey As String
        Public Property cmcKey As String
        Public Property lastPchsInvcNo As Integer?
        Public Property lastSaleRcptNo As Integer?
        Public Property lastInvcNo As Integer?
        Public Property lastSaleInvcNo As Integer?
        Public Property lastTrainInvcNo As Integer?
        Public Property lastProfrmInvcNo As Integer?
        Public Property lastCopyInvcNo As Integer?
        Public Property vatTyCd As String
    End Class

End Namespace
