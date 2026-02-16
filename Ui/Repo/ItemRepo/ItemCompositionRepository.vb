Imports Core.Models.Item.Composition
Imports MySql.Data.MySqlClient

Namespace Repo.ItemRepo
    Public Interface IItemCompositionRepository
        Function GetByUniqueKey(tin As String, bhfId As String, itemCd As String, cpstItemCd As String) As ItemComposition
        Sub Insert(item As ItemComposition)
        Sub Update(item As ItemComposition)
        Function MarkAsUploaded(tin As String, bhfId As String, itemCd As String, cpstItemCd As String) As Boolean
    End Interface

    Public Class ItemCompositionRepository
        Implements IItemCompositionRepository
        Private ReadOnly _conn As String

        Public Sub New(conn As String)
            _conn = conn
        End Sub

        Public Function GetByUniqueKey(tin As String, bhfId As String, itemCd As String, cpstItemCd As String) As ItemComposition Implements IItemCompositionRepository.GetByUniqueKey
            Dim sql = "SELECT * FROM item_compositions WHERE tin=@tin AND bhf_id=@bhfId AND item_cd=@itemCd AND cpst_item_cd=@cpstItemCd"
            Using conn As New MySqlConnection(_conn)
                conn.Open()
                Dim cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@tin", tin)
                cmd.Parameters.AddWithValue("@bhfId", bhfId)
                cmd.Parameters.AddWithValue("@itemCd", itemCd)
                cmd.Parameters.AddWithValue("@cpstItemCd", cpstItemCd)
                Using rdr = cmd.ExecuteReader()
                    If rdr.Read() Then
                        Return New ItemComposition With {
                            .Id = rdr("id"),
                            .Tin = rdr("tin").ToString(),
                            .BhfId = rdr("bhf_id").ToString(),
                            .ItemCd = rdr("item_cd").ToString(),
                            .CpstItemCd = rdr("cpst_item_cd").ToString(),
                            .CpstQty = Convert.ToDecimal(rdr("cpst_qty")),
                            .RegrId = rdr("regr_id").ToString(),
                            .RegrNm = rdr("regr_nm").ToString(),
                            .CreatedAt = Convert.ToDateTime(rdr("created_at"))
                        }
                    End If
                End Using
            End Using
            Return Nothing
        End Function

        Public Sub Insert(item As ItemComposition) Implements IItemCompositionRepository.Insert
            Dim sql = "INSERT INTO item_compositions (tin, bhf_id, item_cd, cpst_item_cd, cpst_qty, regr_id, regr_nm) " &
                "VALUES (@tin,@bhfId,@itemCd,@cpstItemCd,@cpstQty,@regrId,@regrNm)"
            Using conn As New MySqlConnection(_conn)
                conn.Open()
                Dim cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@tin", item.Tin)
                cmd.Parameters.AddWithValue("@bhfId", item.BhfId)
                cmd.Parameters.AddWithValue("@itemCd", item.ItemCd)
                cmd.Parameters.AddWithValue("@cpstItemCd", item.CpstItemCd)
                cmd.Parameters.AddWithValue("@cpstQty", item.CpstQty)
                cmd.Parameters.AddWithValue("@regrId", item.RegrId)
                cmd.Parameters.AddWithValue("@regrNm", item.RegrNm)
                cmd.ExecuteNonQuery()
            End Using
        End Sub

        Public Sub Update(item As ItemComposition) Implements IItemCompositionRepository.Update
            Dim sql = "UPDATE item_compositions SET cpst_qty=@cpstQty, regr_id=@regrId, regr_nm=@regrNm WHERE tin=@tin AND bhf_id=@bhfId AND " &
                "item_cd=@itemCd AND cpst_item_cd=@cpstItemCd"
            Using conn As New MySqlConnection(_conn)
                conn.Open()
                Dim cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@cpstQty", item.CpstQty)
                cmd.Parameters.AddWithValue("@regrId", item.RegrId)
                cmd.Parameters.AddWithValue("@regrNm", item.RegrNm)
                cmd.Parameters.AddWithValue("@tin", item.Tin)
                cmd.Parameters.AddWithValue("@bhfId", item.BhfId)
                cmd.Parameters.AddWithValue("@itemCd", item.ItemCd)
                cmd.Parameters.AddWithValue("@cpstItemCd", item.CpstItemCd)
                cmd.ExecuteNonQuery()
            End Using
        End Sub

        Public Function MarkAsUploaded(tin As String, bhfId As String, itemCd As String, cpstItemCd As String) As Boolean _
    Implements IItemCompositionRepository.MarkAsUploaded
            Dim sql = "UPDATE item_compositions SET UploadYn = 'Y' WHERE tin=@tin AND bhf_id=@bhfId AND item_cd=@itemCd AND cpst_item_cd=@cpstItemCd"
            Using conn As New MySqlConnection(_conn)
                conn.Open()
                Dim cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@tin", tin)
                cmd.Parameters.AddWithValue("@bhfId", bhfId)
                cmd.Parameters.AddWithValue("@itemCd", itemCd)
                cmd.Parameters.AddWithValue("@cpstItemCd", cpstItemCd)
                Return cmd.ExecuteNonQuery() > 0
            End Using
        End Function
    End Class
End Namespace
