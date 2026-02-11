Imports Core.Models.Purchase
Imports MySql.Data.MySqlClient

Namespace Repo.ProductRepo
    Public Interface IPurchaseRepository
    Function GetAll() As DataTable
    Function GetById(id As Integer) As PurchaseTransaction
    Function Insert(purchase As PurchaseTransaction) As Integer
    Sub MarkAsUploaded(id As Integer)
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
                    Dim cmd As New MySqlCommand("INSERT INTO purchase_transactions (invc_no, reg_ty_cd, pchs_ty_cd, rcpt_ty_cd, pmt_ty_cd, " &
                                                "pchs_stts_cd, cfm_dt, pchs_dt, tot_item_cnt, tot_taxbl_amt, tot_tax_amt, tot_amt, remark) " &
                                                "VALUES (@invcNo, @regTyCd, @pchsTyCd, @rcptTyCd, @pmtTyCd, @pchsSttsCd, @cfmDt, @pchsDt, " &
                                                "@totItemCnt, @totTaxblAmt, @totTaxAmt, @totAmt, @remark); SELECT LAST_INSERT_ID();", conn, tran)
                    cmd.Parameters.AddWithValue("@invcNo", purchase.InvcNo)
                    cmd.Parameters.AddWithValue("@regTyCd", purchase.RegTyCd)
                    cmd.Parameters.AddWithValue("@pchsTyCd", purchase.PchsTyCd)
                    cmd.Parameters.AddWithValue("@rcptTyCd", purchase.RcptTyCd)
                    cmd.Parameters.AddWithValue("@pmtTyCd", purchase.PmtTyCd)
                    cmd.Parameters.AddWithValue("@pchsSttsCd", purchase.PchsSttsCd)
                    cmd.Parameters.AddWithValue("@cfmDt", purchase.CfmDt)
                    cmd.Parameters.AddWithValue("@pchsDt", purchase.PchsDt)
                    cmd.Parameters.AddWithValue("@totItemCnt", purchase.TotItemCnt)
                    cmd.Parameters.AddWithValue("@totTaxblAmt", purchase.TotTaxblAmt)
                    cmd.Parameters.AddWithValue("@totTaxAmt", purchase.TotTaxAmt)
                    cmd.Parameters.AddWithValue("@totAmt", purchase.TotAmt)
                    cmd.Parameters.AddWithValue("@remark", purchase.Remark)
                    Dim purchaseId = Convert.ToInt32(cmd.ExecuteScalar())
                    For Each Item In purchase.Items
                        Dim itemCmd As New MySqlCommand("INSERT INTO purchase_transaction_items (purchase_id,item_seq,item_cd,item_cls_cd,item_nm, " &
                                                        "qty,prc,tax_ty_cd,tax_amt,tot_amt) VALUES (@purchaseId,@itemSeq,@itemCd,@itemClsCd,@itemNm, " &
                                                        "@qty,@prc,@taxTyCd,@taxAmt,@totAmt)", conn, tran)
                        itemCmd.Parameters.AddWithValue("@purchaseId", purchaseId)
                        itemCmd.Parameters.AddWithValue("@itemSeq", Item.ItemSeq)
                        itemCmd.Parameters.AddWithValue("@itemCd", Item.ItemCd)
                        itemCmd.Parameters.AddWithValue("@itemClsCd", Item.ItemClsCd)
                        itemCmd.Parameters.AddWithValue("@itemNm", Item.ItemNm)
                        itemCmd.Parameters.AddWithValue("@qty", Item.Qty)
                        itemCmd.Parameters.AddWithValue("@prc", Item.Prc)
                        itemCmd.Parameters.AddWithValue("@taxTyCd", Item.TaxTyCd)
                        itemCmd.Parameters.AddWithValue("@taxAmt", Item.TaxAmt)
                        itemCmd.Parameters.AddWithValue("@totAmt", Item.TotAmt)
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
                Dim cmd As New MySqlCommand("UPDATE purchase_transactions SET is_uploaded=1, upload_date=NOW() WHERE id=@id", conn)
                cmd.Parameters.AddWithValue("@id", id)
                cmd.ExecuteNonQuery()
            End Using
        End Sub

        Public Function GetAll() As DataTable Implements IPurchaseRepository.GetAll
            Dim dt As New DataTable()
            Using conn As New MySqlConnection(_connString)
                conn.Open()
                Dim cmd As New MySqlCommand("SELECT id, invc_no, pchs_dt, tot_amt, is_uploaded FROM purchase_transactions ORDER BY id DESC", conn)
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
                        purchase.InvcNo = rdr("invc_no")
                        purchase.RegTyCd = rdr("reg_ty_cd").ToString()
                        purchase.PchsTyCd = rdr("pchs_ty_cd").ToString()
                        purchase.RcptTyCd = rdr("rcpt_ty_cd").ToString()
                        purchase.PmtTyCd = rdr("pmt_ty_cd").ToString()
                        purchase.PchsSttsCd = rdr("pchs_stts_cd").ToString()
                        purchase.CfmDt = rdr("cfm_dt").ToString()
                        purchase.PchsDt = rdr("pchs_dt").ToString()
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
                            .Qty = rdr("qty"),
                            .Prc = rdr("prc"),
                            .TaxTyCd = rdr("tax_ty_cd").ToString(),
                            .TaxAmt = rdr("tax_amt"),
                            .TotAmt = rdr("tot_amt")
                        })
                    End While
                End Using
            End Using
            Return purchase
        End Function
    End Class
End Namespace
