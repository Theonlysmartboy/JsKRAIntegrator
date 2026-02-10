Imports Core.Models.Item.Import
Imports MySql.Data.MySqlClient

Namespace Repo
    Public Interface IBranchImportItemStatusRepository
        Function GetAll() As List(Of ImportItemStatus)
        Function GetPending() As List(Of ImportItemStatus)
        Sub Save(entity As ImportItemStatus)
        Sub MarkAsUploaded(id As Integer)
    End Interface

    Public Class BranchImportItemStatusRepository
        Implements IBranchImportItemStatusRepository
        Private ReadOnly _connString As String

        Public Sub New(connectionString As String)
            _connString = connectionString
        End Sub

        Public Sub Save(entity As ImportItemStatus) Implements IBranchImportItemStatusRepository.Save
            Using conn As New MySqlConnection(_connString)
                conn.Open()
                If entity.Id = 0 Then
                    Dim cmd As New MySqlCommand("INSERT INTO branch_import_item_status_updates (task_cd, dcl_de, item_seq, hs_cd, item_cls_cd, " &
                                                "item_cd, impt_item_stts_cd, remark, modr_nm, modr_id) VALUES (@task_cd, @dcl_de, @item_seq, " &
                                                "@hs_cd, @item_cls_cd, @item_cd, @status_cd, @remark, @modr_nm, @modr_id)", conn)
                    AddParameters(cmd, entity)
                    cmd.ExecuteNonQuery()
                Else
                    Dim cmd As New MySqlCommand("UPDATE branch_import_item_status_updates SET task_cd = @task_cd, dcl_de = @dcl_de, " &
                                                "item_seq = @item_seq, hs_cd = @hs_cd, item_cls_cd = @item_cls_cd, item_cd = @item_cd, " &
                                                "impt_item_stts_cd = @status_cd, remark = @remark, modr_nm = @modr_nm, modr_id = @modr_id " &
                                                "WHERE id = @id", conn)
                    AddParameters(cmd, entity)
                    cmd.Parameters.AddWithValue("@id", entity.Id)
                    cmd.ExecuteNonQuery()
                End If
            End Using
        End Sub

        Public Sub MarkAsUploaded(id As Integer) Implements IBranchImportItemStatusRepository.MarkAsUploaded
            Using conn As New MySqlConnection(_connString)
                conn.Open()
                Dim cmd As New MySqlCommand("UPDATE branch_import_item_status_updates SET is_uploaded = 1, uploaded_at = NOW() WHERE id = @id", conn)
                cmd.Parameters.AddWithValue("@id", id)
                cmd.ExecuteNonQuery()
            End Using
        End Sub

        Public Function GetAll() As List(Of ImportItemStatus) _
        Implements IBranchImportItemStatusRepository.GetAll
            Dim list As New List(Of ImportItemStatus)
            Using conn As New MySqlConnection(_connString)
                conn.Open()
                Dim cmd As New MySqlCommand("SELECT * FROM branch_import_item_status_updates ORDER BY created_at DESC", conn)
                Using reader = cmd.ExecuteReader()
                    While reader.Read()
                        list.Add(MapReader(reader))
                    End While
                End Using
            End Using
            Return list
        End Function

        Public Function GetPending() As List(Of ImportItemStatus) Implements IBranchImportItemStatusRepository.GetPending
            Dim list As New List(Of ImportItemStatus)
            Using conn As New MySqlConnection(_connString)
                conn.Open()
                Dim cmd As New MySqlCommand("SELECT * FROM branch_import_item_status_updates WHERE is_uploaded = 0 ORDER BY created_at ASC", conn)
                Using reader = cmd.ExecuteReader()
                    While reader.Read()
                        list.Add(MapReader(reader))
                    End While
                End Using
            End Using
            Return list
        End Function

        Private Sub AddParameters(cmd As MySqlCommand, entity As ImportItemStatus)
            cmd.Parameters.AddWithValue("@task_cd", entity.TaskCd)
            cmd.Parameters.AddWithValue("@dcl_de", entity.DclDe)
            cmd.Parameters.AddWithValue("@item_seq", entity.ItemSeq)
            cmd.Parameters.AddWithValue("@hs_cd", entity.HsCd)
            cmd.Parameters.AddWithValue("@item_cls_cd", entity.ItemClsCd)
            cmd.Parameters.AddWithValue("@item_cd", entity.ItemCd)
            cmd.Parameters.AddWithValue("@status_cd", entity.ImptItemSttsCd)
            cmd.Parameters.AddWithValue("@remark", entity.Remark)
            cmd.Parameters.AddWithValue("@modr_nm", entity.ModrNm)
            cmd.Parameters.AddWithValue("@modr_id", entity.ModrId)
        End Sub

        Private Function MapReader(reader As MySqlDataReader) As ImportItemStatus
            Return New ImportItemStatus With {
                .Id = Convert.ToInt32(reader("id")),
                .TaskCd = reader("task_cd").ToString(),
                .DclDe = reader("dcl_de").ToString(),
                .ItemSeq = Convert.ToInt32(reader("item_seq")),
                .HsCd = reader("hs_cd").ToString(),
                .ItemClsCd = reader("item_cls_cd").ToString(),
                .ItemCd = reader("item_cd").ToString(),
                .ImptItemSttsCd = reader("impt_item_stts_cd").ToString(),
                .Remark = reader("remark").ToString(),
                .ModrNm = reader("modr_nm").ToString(),
                .ModrId = reader("modr_id").ToString(),
                .IsUploaded = Convert.ToBoolean(reader("is_uploaded"))
            }
        End Function
    End Class
End Namespace
