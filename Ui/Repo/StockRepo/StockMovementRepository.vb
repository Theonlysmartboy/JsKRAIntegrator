Imports Core.Models.Item.Stock
Imports MySql.Data.MySqlClient

Namespace Repo.StockRepo
    Public Interface IStockMovementRepository
        Sub Insert(move As StockMovementRecord)
        Function GetAll() As DataTable
        Function GetAllHeaders() As DataTable
        Function GetItemsByStockMoveId(stockMoveId As Integer) As DataTable
        Sub MarkAsUploaded(itemId As Integer)
    End Interface

    Public Class StockMovementRepository
        Implements IStockMovementRepository
        Private ReadOnly _connString As String

        Public Sub New(connString As String)
            _connString = connString
        End Sub

        Public Sub Insert(move As StockMovementRecord) Implements IStockMovementRepository.Insert
            Using conn As New MySqlConnection(_connString)
                conn.Open()
                Dim tran = conn.BeginTransaction()
                Try
                    Dim sql As String = "INSERT INTO etims_stock_moves (sar_no, org_sar_no, reg_ty_cd, cust_tin, cust_nm, cust_bhf_id, sar_ty_cd, " &
                        "ocrn_dt, tot_item_cnt, tot_taxbl_amt, tot_tax_amt, tot_amt, remark) VALUES (@sarNo, @orgSarNo, @regTyCd, @custTin, @custNm, " &
                        "@custBhfId, @sarTyCd, @ocrnDt, @totItemCnt, @totTaxblAmt, @totTaxAmt, @totAmt, @remark); SELECT LAST_INSERT_ID();"
                    Dim cmd As New MySqlCommand(sql, conn, tran)
                    cmd.Parameters.AddWithValue("@sarNo", move.sarNo)
                    cmd.Parameters.AddWithValue("@orgSarNo", move.orgSarNo)
                    cmd.Parameters.AddWithValue("@regTyCd", move.regTyCd)
                    cmd.Parameters.AddWithValue("@custTin", move.custTin)
                    cmd.Parameters.AddWithValue("@custNm", move.custNm)
                    cmd.Parameters.AddWithValue("@custBhfId", move.custBhfId)
                    cmd.Parameters.AddWithValue("@sarTyCd", move.sarTyCd)
                    cmd.Parameters.AddWithValue("@ocrnDt", move.ocrnDt)
                    cmd.Parameters.AddWithValue("@totItemCnt", move.totItemCnt)
                    cmd.Parameters.AddWithValue("@totTaxblAmt", move.totTaxblAmt)
                    cmd.Parameters.AddWithValue("@totTaxAmt", move.totTaxAmt)
                    cmd.Parameters.AddWithValue("@totAmt", move.totAmt)
                    cmd.Parameters.AddWithValue("@remark", move.remark)
                    Dim moveId = Convert.ToInt32(cmd.ExecuteScalar())
                    For Each it In move.itemList
                        Dim query As String = "INSERT INTO etims_stock_move_items (stock_move_id, item_seq, item_cd, item_cls_cd, item_nm, bcd, " &
                            "pkg_unit_cd, pkg, qty_unit_cd, qty, item_expr_dt, prc, sply_amt, tot_dc_amt, taxbl_amt, tax_ty_cd, tax_amt, " &
                            "tot_amt) VALUES (@moveId, @itemSeq, @itemCd, @itemClsCd, @itemNm, @bcd, @pkgUnitCd, @pkg, @qtyUnitCd, @qty, " &
                            "@itemExprDt, @prc, @splyAmt, @totDcAmt, @taxblAmt, @taxTyCd, @taxAmt, @totAmt)"
                        Dim itemCmd As New MySqlCommand(query, conn, tran)
                        itemCmd.Parameters.AddWithValue("@moveId", moveId)
                        itemCmd.Parameters.AddWithValue("@itemSeq", it.itemSeq)
                        itemCmd.Parameters.AddWithValue("@itemCd", it.itemCd)
                        itemCmd.Parameters.AddWithValue("@itemClsCd", it.itemClsCd)
                        itemCmd.Parameters.AddWithValue("@itemNm", it.itemNm)
                        itemCmd.Parameters.AddWithValue("@bcd", it.bcd)
                        itemCmd.Parameters.AddWithValue("@pkgUnitCd", it.pkgUnitCd)
                        itemCmd.Parameters.AddWithValue("@pkg", it.pkg)
                        itemCmd.Parameters.AddWithValue("@qtyUnitCd", it.qtyUnitCd)
                        itemCmd.Parameters.AddWithValue("@qty", it.qty)
                        itemCmd.Parameters.AddWithValue("@itemExprDt", it.itemExprDt)
                        itemCmd.Parameters.AddWithValue("@prc", it.prc)
                        itemCmd.Parameters.AddWithValue("@splyAmt", it.splyAmt)
                        itemCmd.Parameters.AddWithValue("@totDcAmt", it.totDcAmt)
                        itemCmd.Parameters.AddWithValue("@taxblAmt", it.taxblAmt)
                        itemCmd.Parameters.AddWithValue("@taxTyCd", it.taxTyCd)
                        itemCmd.Parameters.AddWithValue("@taxAmt", it.taxAmt)
                        itemCmd.Parameters.AddWithValue("@totAmt", it.totAmt)
                        itemCmd.ExecuteNonQuery()
                    Next
                    tran.Commit()
                Catch
                    tran.Rollback()
                    Throw
                End Try
            End Using
        End Sub

        Public Sub MarkAsUploaded(itemId As Integer) Implements IStockMovementRepository.MarkAsUploaded
            Using conn As New MySqlConnection(_connString)
                conn.Open()
                Dim query = "UPDATE etims_stock_move_items SET to_upload = 0 WHERE id = @id"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@id", itemId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub


        Public Function GetAllHeaders() As DataTable Implements IStockMovementRepository.GetAllHeaders
            Dim dt As New DataTable()
            Using conn As New MySqlConnection(_connString)
                conn.Open()
                Dim query As String = "SELECT id, cust_tin, cust_bhf_id, sar_no, ocrn_dt, tot_item_cnt, tot_taxbl_amt, tot_tax_amt, tot_amt, " &
                    "created_at FROM etims_stock_moves ORDER BY id DESC;"
                Using da As New MySqlDataAdapter(query, conn)
                    da.Fill(dt)
                End Using
            End Using
            Return dt
        End Function

        Public Function GetItemsByStockMoveId(stockMoveId As Integer) As DataTable Implements IStockMovementRepository.GetItemsByStockMoveId
            Dim dt As New DataTable()
            Using conn As New MySqlConnection(_connString)
                conn.Open()
                Dim query As String = "SELECT item_seq, item_cd, item_cls_cd, item_nm, qty, prc, sply_amt, taxbl_amt, tax_ty_cd, tax_amt, tot_amt, " &
                    "to_upload FROM etims_stock_move_items WHERE stock_move_id = @stockMoveId ORDER BY item_seq;"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@stockMoveId", stockMoveId)
                    Using da As New MySqlDataAdapter(cmd)
                        da.Fill(dt)
                    End Using
                End Using
            End Using
            Return dt
        End Function

        Public Function GetAll() As DataTable Implements IStockMovementRepository.GetAll
            Dim dt As New DataTable()
            Using conn As New MySqlConnection(_connString)
                conn.Open()
                Dim query As String = "SELECT id, tin, bhf_id, last_req_dt, result_cd, result_msg, result_dt, created_at FROM etims_stock_moves " &
                                        "ORDER BY id DESC;"
                Using cmd As New MySqlCommand(query, conn)
                    Using da As New MySqlDataAdapter(cmd)
                        da.Fill(dt)
                    End Using
                End Using
            End Using
            Return dt
        End Function
    End Class
End Namespace
