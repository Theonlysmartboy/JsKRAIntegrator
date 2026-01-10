Namespace Models.Receipt
    Public Class Receipt
        Public Property custTin() As String
            Get
                Return m_custTin
            End Get
            Set
                m_custTin = Value
            End Set
        End Property
        Private m_custTin As String
        Public Property custMblNo() As String
            Get
                Return m_custMblNo
            End Get
            Set
                m_custMblNo = Value
            End Set
        End Property
        Private m_custMblNo As String
        Public Property rptNo() As String
            Get
                Return m_rptNo
            End Get
            Set
                m_rptNo = Value
            End Set
        End Property
        Private m_rptNo As String
        Public Property trdeNm() As String
            Get
                Return m_trdeNm
            End Get
            Set
                m_trdeNm = Value
            End Set
        End Property
        Private m_trdeNm As String
        Public Property adrs() As String
            Get
                Return m_adrs
            End Get
            Set
                m_adrs = Value
            End Set
        End Property
        Private m_adrs As String
        Public Property topMsg() As String
            Get
                Return m_topMsg
            End Get
            Set
                m_topMsg = Value
            End Set
        End Property
        Private m_topMsg As String
        Public Property btmMsg() As String
            Get
                Return m_btmMsg
            End Get
            Set
                m_btmMsg = Value
            End Set
        End Property
        Private m_btmMsg As String
        Public Property prchrAcptcYn() As String
            Get
                Return m_prchrAcptcYn
            End Get
            Set
                m_prchrAcptcYn = Value
            End Set
        End Property
        Private m_prchrAcptcYn As String
    End Class
End Namespace