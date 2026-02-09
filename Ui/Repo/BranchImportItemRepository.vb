Imports Core.Models.Item.Import
Imports MySql.Data.MySqlClient

Namespace Repo

    Public Interface IBranchImportItemRepository
        Sub Save(items As List(Of ImportItem))
        Function GetAll() As List(Of ImportItem)
    End Interface

    Public Class BranchImportItemRepository
        Implements IBranchImportItemRepository
        Private ReadOnly _connectionString As String

        Public Sub New(connectionString As String)
            _connectionString = connectionString
        End Sub

        Public Sub Save(items As List(Of ImportItem)) Implements IBranchImportItemRepository.Save
            Using conn As New MySqlConnection(_connectionString)
                conn.Open()
                For Each Item As ImportItem In items
                    Dim sql As String = "INSERT INTO BranchImportItem (TaskCd, DclDe, ItemSeq, DclNo, HsCd, ItemNm, ImptItemSttsCd, OrgnNatCd, " &
                                        "ExptNatCd, Pkg, PkgUnitCd, Qty, QtyUnitCd, TotWt, NetWt, SpplrNm, AgntNm, InvcFcurAmt, InvcFcurCd, " &
                                        "InvcFcurExcrt) VALUES (@TaskCd, @DclDe, @ItemSeq, @DclNo, @HsCd, @ItemNm, @Status, @Orgn, @Expt, @Pkg, " &
                                        "@PkgUnit, @Qty, @QtyUnit, @TotWt, @NetWt, @Spplr, @Agnt, @Amt, @Cur, @Excrt)"
                    Dim cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@TaskCd", Item.taskCd)
                    cmd.Parameters.AddWithValue("@DclDe", Item.dclDe)
                    cmd.Parameters.AddWithValue("@ItemSeq", Item.itemSeq)
                    cmd.Parameters.AddWithValue("@DclNo", Item.dclNo)
                    cmd.Parameters.AddWithValue("@HsCd", Item.hsCd)
                    cmd.Parameters.AddWithValue("@ItemNm", Item.itemNm)
                    cmd.Parameters.AddWithValue("@Status", Item.imptItemsttsCd)
                    cmd.Parameters.AddWithValue("@Orgn", Item.orgnNatCd)
                    cmd.Parameters.AddWithValue("@Expt", Item.exptNatCd)
                    cmd.Parameters.AddWithValue("@Pkg", Item.pkg)
                    cmd.Parameters.AddWithValue("@PkgUnit", Item.pkgUnitCd)
                    cmd.Parameters.AddWithValue("@Qty", Item.qty)
                    cmd.Parameters.AddWithValue("@QtyUnit", Item.qtyUnitCd)
                    cmd.Parameters.AddWithValue("@TotWt", Item.totWt)
                    cmd.Parameters.AddWithValue("@NetWt", Item.netWt)
                    cmd.Parameters.AddWithValue("@Spplr", Item.spplrNm)
                    cmd.Parameters.AddWithValue("@Agnt", Item.agntNm)
                    cmd.Parameters.AddWithValue("@Amt", Item.invcFcurAmt)
                    cmd.Parameters.AddWithValue("@Cur", Item.invcFcurCd)
                    cmd.Parameters.AddWithValue("@Excrt", Item.invcFcurExcrt)
                    cmd.ExecuteNonQuery()
                Next
            End Using
        End Sub

        Public Function GetAll() As List(Of ImportItem) Implements IBranchImportItemRepository.GetAll
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace