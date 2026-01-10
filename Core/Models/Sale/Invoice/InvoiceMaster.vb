Namespace Models.Sale.Invoice
    Public Class InvoiceMaster
        Public Property InvoiceNo As String
        Public Property InvoiceDate As DateTime?
        Public Property DeliveryNoteNo As String
        Public Property LPONo As String

        Public Property CustomerCode As String
        Public Property CustomerPinNo As String
        Public Property CustomerName As String
        Public Property CustomerVATNo As String
        Public Property CustomerAddress1 As String
        Public Property CustomerCity As String
        Public Property CustomerCountry As String
        Public Property CustomerPhone As String
        Public Property PartyName As String

        Public Property SalesmanCode As String
        Public Property SalesmanName As String
        Public Property Pricelist As String

        Public Property Remark As String

        Public Property SubTotal As Decimal
        Public Property VATAmount As Decimal
        Public Property Discount As Decimal
        Public Property TotalAmount As Decimal
        Public Property TotatWeight As Decimal

        Public Property PreparedBy As String
        Public Property AuthorizedBy As String

        Public Property QR_Code As String

        ' Control Unit (CU) fields
        Public Property CU_Inv_No As String
        Public Property CU_Serial_No As String
        Public Property CU_Datetime As DateTime?
        Public Property SrNo As String
    End Class

    Public Class InvoiceDetail
        Public Property InvoiceNo As String
        Public Property SrNo As Integer
        Public Property ProductCode As String
        Public Property ItemCode As String
        Public Property Description As String
        Public Property Pack As String
        Public Property QUCODE As String
        Public Property SupplierPacking As Decimal
        Public Property ProductUnit As String
        Public Property isAlternetUnit As Integer
        Public Property AlternetUnit As String
        Public Property Qty As Decimal
        Public Property QUAMT As Decimal
        Public Property Price As Decimal
        Public Property Amount As Decimal
        Public Property Disc As Decimal
        Public Property DiscAmount As Decimal
        Public Property VATCode As String
        Public Property VATAmount As Decimal
        Public Property NetAmount As Decimal

        ' used to map to itemClsCd in VSCU itemList
        Public Property ItemClsCode As String
    End Class

    Public Class InvoiceDetailETIMS
        Public Property itemSeq() As Integer
            Get
                Return m_itemSeq
            End Get
            Set
                m_itemSeq = Value
            End Set
        End Property
        Private m_itemSeq As Integer

        Public Property itemCd() As String
            Get
                Return m_itemCd
            End Get
            Set
                m_itemCd = Value
            End Set
        End Property
        Private m_itemCd As String

        Public Property itemClsCd() As String
            Get
                Return m_itemClsCd
            End Get
            Set
                m_itemClsCd = Value
            End Set
        End Property
        Private m_itemClsCd As String

        Public Property itemNm() As String
            Get
                Return m_itemNm
            End Get
            Set
                m_itemNm = Value
            End Set
        End Property
        Private m_itemNm As String

        Public Property bcd() As Object
            Get
                Return m_bcd
            End Get
            Set
                m_bcd = Value
            End Set
        End Property
        Private m_bcd As Object

        Public Property pkgUnitCd() As String
            Get
                Return m_pkgUnitCd
            End Get
            Set
                m_pkgUnitCd = Value
            End Set
        End Property
        Private m_pkgUnitCd As String

        Public Property pkg() As Object
            Get
                Return m_pkg
            End Get
            Set
                m_pkg = Value
            End Set
        End Property
        Private m_pkg As Object

        Public Property qtyUnitCd() As String
            Get
                Return m_qtyUnitCd
            End Get
            Set
                m_qtyUnitCd = Value
            End Set
        End Property
        Private m_qtyUnitCd As String

        Public Property qty() As Object
            Get
                Return m_qty
            End Get
            Set
                m_qty = Value
            End Set
        End Property
        Private m_qty As Object

        Public Property prc() As Object
            Get
                Return m_prc
            End Get
            Set
                m_prc = Value
            End Set
        End Property
        Private m_prc As Object

        Public Property splyAmt() As Object
            Get
                Return m_splyAmt
            End Get
            Set
                m_splyAmt = Value
            End Set
        End Property
        Private m_splyAmt As Object

        Public Property dcRt() As Object
            Get
                Return m_dcRt
            End Get
            Set
                m_dcRt = Value
            End Set
        End Property
        Private m_dcRt As Object

        Public Property dcAmt() As Object
            Get
                Return m_dcAmt
            End Get
            Set
                m_dcAmt = Value
            End Set
        End Property
        Private m_dcAmt As Object

        Public Property isrccCd() As Object
            Get
                Return m_isrccCd
            End Get
            Set
                m_isrccCd = Value
            End Set
        End Property
        Private m_isrccCd As Object

        Public Property isrccNm() As Object
            Get
                Return m_isrccNm
            End Get
            Set
                m_isrccNm = Value
            End Set
        End Property
        Private m_isrccNm As Object

        Public Property isrcRt() As Object
            Get
                Return m_isrcRt
            End Get
            Set
                m_isrcRt = Value
            End Set
        End Property
        Private m_isrcRt As Object

        Public Property isrcAmt() As Object
            Get
                Return m_isrcAmt
            End Get
            Set
                m_isrcAmt = Value
            End Set
        End Property
        Private m_isrcAmt As Object

        Public Property taxTyCd() As String
            Get
                Return m_taxTyCd
            End Get
            Set
                m_taxTyCd = Value
            End Set
        End Property
        Private m_taxTyCd As String

        Public Property taxblAmt() As Object
            Get
                Return m_taxblAmt
            End Get
            Set
                m_taxblAmt = Value
            End Set
        End Property
        Private m_taxblAmt As Object

        Public Property taxAmt() As Object
            Get
                Return m_taxAmt
            End Get
            Set
                m_taxAmt = Value
            End Set
        End Property
        Private m_taxAmt As Object

        Public Property totAmt() As Object
            Get
                Return m_totAmt
            End Get
            Set
                m_totAmt = Value
            End Set
        End Property
        Private m_totAmt As Object

    End Class

End Namespace
