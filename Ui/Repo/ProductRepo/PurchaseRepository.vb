Imports Core.Models.Purchase
Imports MySql.Data.MySqlClient

Namespace Repo.ProductRepo
    Public Interface IPurchaseRepository
    Function GetAll() As DataTable
    Function GetById(id As Integer) As PurchaseTransaction
        Function Insert(purchase As PurchaseTransaction) As Integer
        Function GetItemsByPurchaseId(id As Integer) As DataTable
        Function GetByUniqueKey(spplrTin As String, spplrInvcNo As Integer, spplrBhfId As String) As PurchaseTransaction
        Sub MarkAsUploaded(id As Integer)
        Sub Update(purchase As PurchaseTransaction)
    End Interface

    Public Class PurchaseRepository
        Implements IPurchaseRepository
        Private _connString As String

        Public Sub New(connectionString As String)
            _connString = connectionString
        End Sub

        Public Function Insert(purchase As PurchaseTransaction) As Integer Implements IPurchaseRepository.Insert
            Using conn As New MySqlConnection(_connString)
                conn.Open()
                Dim tran = conn.BeginTransaction()
                Try
                    Dim cmd As New MySqlCommand("INSERT INTO purchase_transactions (spplr_tin, invc_no, org_invc_no, spplr_bhf_id, spplr_nm, " &
                        "spplr_invc_no, spplr_sdc_id, reg_ty_cd, pchs_ty_cd, rcpt_ty_cd, pmt_ty_cd, pchs_stts_cd, cfm_dt, pchs_dt, wrhs_dt, " &
                        "cncl_req_dt, cncl_dt, rfd_dt, tot_item_cnt, tot_taxbl_amt, tot_tax_amt, tot_amt, remark, is_uploaded) VALUES (@supplrTin, " &
                        "@invcNo, @orgInvcNo, @spplrBhfId, @spplrNm, @spplrInvcNo, @spplrSdcId, @regTyCd, @pchsTyCd, @rcptTyCd, @pmtTyCd, @pchsSttsCd, " &
                        "@cfmDt, @pchsDt, @wrhsDt, @cnclReqDt, @cnclDt, @rfdDt, @totItemCnt, @totTaxblAmt, @totTaxAmt, @totAmt, @remark, @isUploaded); " &
                        "SELECT LAST_INSERT_ID();", conn, tran)
                    cmd.Parameters.AddWithValue("supplrTin", purchase.SpplrTin)
                    cmd.Parameters.AddWithValue("@invcNo", purchase.InvcNo)
                    cmd.Parameters.AddWithValue("@orgInvcNo", purchase.OrgInvcNo)
                    cmd.Parameters.AddWithValue("@spplrBhfId", purchase.SpplrBhfId)
                    cmd.Parameters.AddWithValue("@spplrNm", purchase.SpplrNm)
                    cmd.Parameters.AddWithValue("@spplrInvcNo", purchase.SpplrInvcNo)
                    cmd.Parameters.AddWithValue("@spplrSdcId", purchase.SpplrSdcId)
                    cmd.Parameters.AddWithValue("@regTyCd", purchase.RegTyCd)
                    cmd.Parameters.AddWithValue("@pchsTyCd", purchase.PchsTyCd)
                    cmd.Parameters.AddWithValue("@rcptTyCd", purchase.RcptTyCd)
                    cmd.Parameters.AddWithValue("@pmtTyCd", purchase.PmtTyCd)
                    cmd.Parameters.AddWithValue("@pchsSttsCd", purchase.PchsSttsCd)
                    cmd.Parameters.AddWithValue("@cfmDt", purchase.CfmDt)
                    cmd.Parameters.AddWithValue("@pchsDt", purchase.PchsDt)
                    cmd.Parameters.AddWithValue("@wrhsDt", purchase.WrhsDt)
                    cmd.Parameters.AddWithValue("@cnclReqDt", purchase.cnclReqDt)
                    cmd.Parameters.AddWithValue("@cnclDt", purchase.CnclDt)
                    cmd.Parameters.AddWithValue("@rfdDt", purchase.RfdDt)
                    cmd.Parameters.AddWithValue("@totItemCnt", purchase.TotItemCnt)
                    cmd.Parameters.AddWithValue("@totTaxblAmt", purchase.TotTaxblAmt)
                    cmd.Parameters.AddWithValue("@totTaxAmt", purchase.TotTaxAmt)
                    cmd.Parameters.AddWithValue("@totAmt", purchase.TotAmt)
                    cmd.Parameters.AddWithValue("@remark", purchase.Remark)
                    cmd.Parameters.AddWithValue("@isUploaded", purchase.IsUploaded)
                    Dim purchaseId = Convert.ToInt32(cmd.ExecuteScalar())
                    For Each Item In purchase.Items
                        Dim itemCmd As New MySqlCommand("INSERT INTO purchase_transaction_items (purchase_id, item_seq, item_cd, item_cls_cd, " &
                            "item_nm, pkg_unit_cd, pkg, qty_unit_cd, qty, prc, sply_amt, dc_rt, dc_amt, taxbl_amt, tax_ty_cd, tax_amt, tot_amt, " &
                            "item_expr_dt) VALUES (@purchaseId, @itemSeq, @itemCd, @itemClsCd, @itemNm, @pkgUnitCd, @pkg, @qtyUnitCd, @qty, " &
                            "@prc, @splyAmt, @dcRt, @dcAmt, @taxblAmt, @taxTyCd, @taxAmt, @totAmt, @itemExprDt)", conn, tran)
                        itemCmd.Parameters.AddWithValue("@purchaseId", purchaseId)
                        itemCmd.Parameters.AddWithValue("@itemSeq", Item.ItemSeq)
                        itemCmd.Parameters.AddWithValue("@itemCd", Item.ItemCd)
                        itemCmd.Parameters.AddWithValue("@itemClsCd", Item.ItemClsCd)
                        itemCmd.Parameters.AddWithValue("@itemNm", Item.ItemNm)
                        itemCmd.Parameters.AddWithValue("@pkgUnitCd", Item.pkgUnitCd)
                        itemCmd.Parameters.AddWithValue("@pkg", Item.pkg)
                        itemCmd.Parameters.AddWithValue("@qtyUnitCd", Item.qtyUnitCd)
                        itemCmd.Parameters.AddWithValue("@qty", Item.Qty)
                        itemCmd.Parameters.AddWithValue("@prc", Item.Prc)
                        itemCmd.Parameters.AddWithValue("@splyAmt", Item.splyAmt)
                        itemCmd.Parameters.AddWithValue("@dcRt", Item.dcRt)
                        itemCmd.Parameters.AddWithValue("@dcAmt", Item.dcAmt)
                        itemCmd.Parameters.AddWithValue("@taxblAmt", Item.taxblAmt)
                        itemCmd.Parameters.AddWithValue("@taxTyCd", Item.TaxTyCd)
                        itemCmd.Parameters.AddWithValue("@taxAmt", Item.TaxAmt)
                        itemCmd.Parameters.AddWithValue("@totAmt", Item.TotAmt)
                        itemCmd.Parameters.AddWithValue("@itemExprDt", Item.itemExprDt)
                        itemCmd.ExecuteNonQuery()
                    Next
                    tran.Commit()
                    Return purchaseId
                Catch
                    tran.Rollback()
                    Throw
                End Try
            End Using
        End Function

        Public Sub MarkAsUploaded(id As Integer) Implements IPurchaseRepository.MarkAsUploaded
            Using conn As New MySqlConnection(_connString)
                conn.Open()
                Dim cmd As New MySqlCommand("UPDATE purchase_transactions SET is_uploaded=1 WHERE id=@id", conn)
                cmd.Parameters.AddWithValue("@id", id)
                cmd.ExecuteNonQuery()
            End Using
        End Sub

        Public Function GetAll() As DataTable Implements IPurchaseRepository.GetAll
            Dim dt As New DataTable()
            Using conn As New MySqlConnection(_connString)
                conn.Open()
                Dim sql As String = "SELECT id, spplr_tin, invc_no, org_invc_no, spplr_bhf_id, spplr_nm, spplr_invc_no, spplr_sdc_id, reg_ty_cd, " &
                    "pchs_ty_cd, rcpt_ty_cd, pmt_ty_cd, pchs_stts_cd, cfm_dt, pchs_dt, wrhs_dt, cncl_req_dt, cncl_dt, rfd_dt, tot_item_cnt, " &
                    "tot_taxbl_amt, tot_tax_amt, tot_amt, remark, is_uploaded FROM purchase_transactions ORDER BY id ASC"
                Dim cmd As New MySqlCommand(sql, conn)
                Using da As New MySqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
            Return dt
        End Function

        Public Function GetById(id As Integer) As PurchaseTransaction Implements IPurchaseRepository.GetById
            Dim purchase As New PurchaseTransaction()
            purchase.Items = New List(Of PurchaseTransactionItem)
            Using conn As New MySqlConnection(_connString)
                conn.Open()
                Dim cmd As New MySqlCommand("SELECT * FROM purchase_transactions WHERE id=@id", conn)
                cmd.Parameters.AddWithValue("@id", id)
                Using rdr = cmd.ExecuteReader()
                    If rdr.Read() Then
                        purchase.Id = rdr("id")
                        purchase.SpplrTin = rdr("spplr_tin").ToString()
                        purchase.InvcNo = rdr("invc_no")
                        purchase.OrgInvcNo = rdr("org_invc_no").ToString()
                        purchase.SpplrBhfId = rdr("spplr_bhf_id").ToString()
                        purchase.SpplrNm = rdr("spplr_nm").ToString()
                        purchase.SpplrInvcNo = rdr("spplr_invc_no")
                        purchase.SpplrSdcId = rdr("spplr_sdc_id").ToString()
                        purchase.RegTyCd = rdr("reg_ty_cd").ToString()
                        purchase.PchsTyCd = rdr("pchs_ty_cd").ToString()
                        purchase.RcptTyCd = rdr("rcpt_ty_cd").ToString()
                        purchase.PmtTyCd = rdr("pmt_ty_cd").ToString()
                        purchase.PchsSttsCd = rdr("pchs_stts_cd").ToString()
                        purchase.CfmDt = rdr("cfm_dt").ToString()
                        purchase.PchsDt = rdr("pchs_dt").ToString()
                        purchase.WrhsDt = rdr("wrhs_dt").ToString()
                        purchase.cnclReqDt = rdr("cncl_req_dt").ToString()
                        purchase.CnclDt = rdr("cncl_dt").ToString()
                        purchase.RfdDt = rdr("rfd_dt").ToString()
                        purchase.TotItemCnt = rdr("tot_item_cnt")
                        purchase.TotTaxblAmt = rdr("tot_taxbl_amt")
                        purchase.TotTaxAmt = rdr("tot_tax_amt")
                        purchase.TotAmt = rdr("tot_amt")
                        purchase.Remark = rdr("remark").ToString()
                    End If
                End Using
                Dim itemCmd As New MySqlCommand("SELECT * FROM purchase_transaction_items WHERE purchase_id=@id", conn)
                itemCmd.Parameters.AddWithValue("@id", id)
                Using rdr = itemCmd.ExecuteReader()
                    While rdr.Read()
                        purchase.Items.Add(New PurchaseTransactionItem With {
                            .ItemSeq = rdr("item_seq"),
                            .ItemCd = rdr("item_cd").ToString(),
                            .ItemClsCd = rdr("item_cls_cd").ToString(),
                            .ItemNm = rdr("item_nm").ToString(),
                            .pkgUnitCd = rdr("pkg_unit_cd").ToString(),
                            .pkg = rdr("pkg"),
                            .qtyUnitCd = rdr("qty_unit_cd").ToString(),
                            .Qty = rdr("qty"),
                            .Prc = rdr("prc"),
                            .splyAmt = rdr("sply_amt"),
                            .dcRt = rdr("dc_rt"),
                            .dcAmt = rdr("dc_amt"),
                            .taxblAmt = rdr("taxbl_amt"),
                            .TaxTyCd = rdr("tax_ty_cd").ToString(),
                            .TaxAmt = rdr("tax_amt"),
                            .TotAmt = rdr("tot_amt"),
                            .itemExprDt = rdr("item_expr_dt").ToString()
                        })
                    End While
                End Using
            End Using
            Return purchase
        End Function

        Public Function GetItemsByPurchaseId(id As Integer) As DataTable Implements IPurchaseRepository.GetItemsByPurchaseId
            Dim dt As New DataTable()
            Using conn As New MySqlConnection(_connString)
                conn.Open()
                Dim cmd As New MySqlCommand("SELECT item_seq, item_cd, item_cls_cd, item_nm, pkg_unit_cd, pkg, qty_unit_cd, qty, prc, sply_amt, " &
                                            "dc_rt, dc_amt, taxbl_amt, tax_ty_cd, tax_amt, tot_amt, item_expr_dt FROM purchase_transaction_items " &
                                            "WHERE purchase_id = @id ORDER BY item_seq", conn)
                cmd.Parameters.AddWithValue("@id", id)
                Using da As New MySqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
            Return dt
        End Function

        Public Function GetByUniqueKey(spplrTin As String, spplrInvcNo As Integer, spplrBhfId As String) As PurchaseTransaction Implements IPurchaseRepository.GetByUniqueKey
            Dim purchase As PurchaseTransaction = Nothing
            Using conn As New MySqlConnection(_connString)
                conn.Open()
                ' Header
                Using cmd As New MySqlCommand("SELECT * FROM purchase_transactions WHERE spplr_tin=@Tin AND spplr_invc_no=@InvcNo AND " &
                                            "spplr_bhf_id=@BhfId", conn)
                    cmd.Parameters.AddWithValue("@Tin", spplrTin)
                    cmd.Parameters.AddWithValue("@InvcNo", spplrInvcNo)
                    cmd.Parameters.AddWithValue("@BhfId", spplrBhfId)
                    Using reader = cmd.ExecuteReader()
                        If reader.Read() Then
                            purchase = New PurchaseTransaction With {
                                .Id = reader("id"),
                                .SpplrTin = reader("spplr_tin").ToString(),
                                .InvcNo = Convert.ToInt32(reader("spplr_invc_no")),
                                .OrgInvcNo = Convert.ToInt32(reader("org_invc_no")),
                                .SpplrBhfId = reader("spplr_bhf_id").ToString(),
                                .SpplrNm = reader("spplr_nm").ToString(),
                                .SpplrSdcId = reader("spplr_sdc_id").ToString(),
                                .RcptTyCd = reader("rcpt_ty_cd").ToString(),
                                .PmtTyCd = reader("pmt_ty_cd").ToString(),
                                .CfmDt = If(IsDBNull(reader("cfm_dt")), Nothing, Convert.ToDateTime(reader("cfm_dt"))),
                                .PchsDt = If(IsDBNull(reader("pchs_dt")), Nothing, Convert.ToDateTime(reader("pchs_dt"))),
                                .WrhsDt = If(IsDBNull(reader("wrhs_dt")), Nothing, Convert.ToDateTime(reader("wrhs_dt"))),
                                .TotItemCnt = Convert.ToInt32(reader("tot_item_cnt")),
                                .TotTaxblAmt = Convert.ToDecimal(reader("tot_taxbl_amt")),
                                .TotTaxAmt = Convert.ToDecimal(reader("tot_tax_amt")),
                                .TotAmt = Convert.ToDecimal(reader("tot_amt")),
                                .Remark = reader("remark").ToString(),
                                .IsUploaded = Convert.ToBoolean(reader("is_uploaded"))
                            }
                        End If
                    End Using
                End Using
                ' Items
                If purchase IsNot Nothing Then
                    Using cmd As New MySqlCommand("SELECT * FROM purchase_transaction_items WHERE purchase_id=@Id ORDER BY item_seq", conn)
                        cmd.Parameters.AddWithValue("@Id", purchase.Id)
                        Using reader = cmd.ExecuteReader()
                            While reader.Read()
                                Dim item As New PurchaseTransactionItem With {
                                    .Id = reader("id"),
                                    .PurchaseId = Convert.ToInt32(reader("purchase_id")),
                                    .itemSeq = Convert.ToInt32(reader("item_seq")),
                                    .itemCd = reader("item_cd").ToString(),
                                    .itemClsCd = reader("item_cls_cd").ToString(),
                                    .itemNm = reader("item_nm").ToString(),
                                    .pkgUnitCd = reader("pkg_unit_cd").ToString(),
                                    .pkg = Convert.ToDecimal(reader("pkg")),
                                    .qtyUnitCd = reader("qty_unit_cd").ToString(),
                                    .qty = Convert.ToDecimal(reader("qty")),
                                    .prc = Convert.ToDecimal(reader("prc")),
                                    .splyAmt = Convert.ToDecimal(reader("sply_amt")),
                                    .dcRt = Convert.ToDecimal(reader("dc_rt")),
                                    .dcAmt = Convert.ToDecimal(reader("dc_amt")),
                                    .taxblAmt = Convert.ToDecimal(reader("taxbl_amt")),
                                    .taxTyCd = reader("tax_ty_cd").ToString(),
                                    .taxAmt = Convert.ToDecimal(reader("tax_amt")),
                                    .totAmt = Convert.ToDecimal(reader("tot_amt")),
                                    .itemExprDt = reader("item_expr_dt").ToString()
                                }
                                purchase.Items.Add(item)
                            End While
                        End Using
                    End Using
                End If
            End Using
            Return purchase
        End Function

        ' Update purchase
        Public Sub Update(purchase As PurchaseTransaction) Implements IPurchaseRepository.Update
            Using conn As New MySqlConnection(_connString)
                conn.Open()
                Using trans = conn.BeginTransaction()
                    ' Update header
                    Using cmd As New MySqlCommand("UPDATE purchase_transactions SET tot_item_cnt=@TotItemCnt, tot_taxbl_amt=@TotTaxblAmt, " &
                                    "tot_tax_amt=@TotTaxAmt, tot_amt=@TotAmt, remark=@Remark, is_uploaded=@IsUploaded WHERE id=@Id", conn, trans)
                        cmd.Parameters.AddWithValue("@TotItemCnt", purchase.TotItemCnt)
                        cmd.Parameters.AddWithValue("@TotTaxblAmt", purchase.TotTaxblAmt)
                        cmd.Parameters.AddWithValue("@TotTaxAmt", purchase.TotTaxAmt)
                        cmd.Parameters.AddWithValue("@TotAmt", purchase.TotAmt)
                        cmd.Parameters.AddWithValue("@Remark", purchase.Remark)
                        cmd.Parameters.AddWithValue("@IsUploaded", purchase.IsUploaded)
                        cmd.Parameters.AddWithValue("@Id", purchase.Id)
                        cmd.ExecuteNonQuery()
                    End Using
                    ' Delete old items
                    Using cmd As New MySqlCommand("DELETE FROM purchase_transaction_items WHERE purchase_id=@Id", conn, trans)
                        cmd.Parameters.AddWithValue("@Id", purchase.Id)
                        cmd.ExecuteNonQuery()
                    End Using
                    ' Insert items
                    For Each it In purchase.Items
                        Using cmd As New MySqlCommand("INSERT INTO purchase_transaction_items (purchase_id, item_seq, item_cd, item_cls_cd, " &
                            "item_nm, pkg_unit_cd, pkg, qty_unit_cd, qty, prc, sply_amt, dc_rt, dc_amt, taxbl_amt, tax_ty_cd, tax_amt, tot_amt, " &
                            "item_expr_dt) VALUES (@PurchaseId, @ItemSeq, @ItemCd, @ItemClsCd, @ItemNm, @PkgUnitCd, @Pkg, @QtyUnitCd, @Qty, @Prc, " &
                            "@SplyAmt, @DcRt, @DcAmt, @TaxblAmt, @TaxTyCd, @TaxAmt, @TotAmt, @ItemExprDt)", conn, trans)
                            cmd.Parameters.AddWithValue("@PurchaseId", purchase.Id)
                            cmd.Parameters.AddWithValue("@ItemSeq", it.itemSeq)
                            cmd.Parameters.AddWithValue("@ItemCd", it.itemCd)
                            cmd.Parameters.AddWithValue("@ItemClsCd", it.itemClsCd)
                            cmd.Parameters.AddWithValue("@ItemNm", it.itemNm)
                            cmd.Parameters.AddWithValue("@PkgUnitCd", it.pkgUnitCd)
                            cmd.Parameters.AddWithValue("@Pkg", it.pkg)
                            cmd.Parameters.AddWithValue("@QtyUnitCd", it.qtyUnitCd)
                            cmd.Parameters.AddWithValue("@Qty", it.qty)
                            cmd.Parameters.AddWithValue("@Prc", it.prc)
                            cmd.Parameters.AddWithValue("@SplyAmt", it.splyAmt)
                            cmd.Parameters.AddWithValue("@DcRt", it.dcRt)
                            cmd.Parameters.AddWithValue("@DcAmt", it.dcAmt)
                            cmd.Parameters.AddWithValue("@TaxblAmt", it.taxblAmt)
                            cmd.Parameters.AddWithValue("@TaxTyCd", it.taxTyCd)
                            cmd.Parameters.AddWithValue("@TaxAmt", it.taxAmt)
                            cmd.Parameters.AddWithValue("@TotAmt", it.totAmt)
                            cmd.Parameters.AddWithValue("@ItemExprDt", it.itemExprDt)
                            cmd.ExecuteNonQuery()
                        End Using
                    Next
                    trans.Commit()
                End Using
            End Using
        End Sub
    End Class
End Namespace
