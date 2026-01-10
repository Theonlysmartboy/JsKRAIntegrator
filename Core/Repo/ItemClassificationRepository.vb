Imports Core.Models.Item.Classification
Imports MySql.Data.MySqlClient
Namespace Repo.Item.Classification
    Public Class ItemClassificationRepository
        Private _connString As String

        Public Sub New(connString As String)
            _connString = connString
        End Sub

        Public Async Function ClearAsync() As Task
            Using conn As New MySqlConnection(_connString)
                Await conn.OpenAsync()
                Dim cmd As New MySqlCommand("TRUNCATE TABLE kra_item_classifications", conn)
                Await cmd.ExecuteNonQueryAsync()
            End Using
        End Function

        Public Async Function SaveAsync(entry As ItemClassificationEntry) As Task
            Using conn As New MySqlConnection(_connString)
                Await conn.OpenAsync()
                Dim cmd As New MySqlCommand(
                    "INSERT INTO kra_item_classifications 
                (item_cls_code, name, level, tax_type_code, major_tag, use_yn)
                VALUES (@code, @name, @lvl, @tax, @tag, @use)", conn)

                cmd.Parameters.AddWithValue("@code", entry.Code)
                cmd.Parameters.AddWithValue("@name", entry.Name)
                cmd.Parameters.AddWithValue("@lvl", entry.Level)
                cmd.Parameters.AddWithValue("@tax", entry.TaxTypeCode)
                cmd.Parameters.AddWithValue("@tag", entry.MajorTag)
                cmd.Parameters.AddWithValue("@use", entry.UseYn)

                Await cmd.ExecuteNonQueryAsync()
            End Using
        End Function

        Public Async Function LoadAllAsync() As Task(Of DataTable)
            Using conn As New MySqlConnection(_connString)
                Await conn.OpenAsync()

                Dim query As String =
                    "SELECT item_cls_code, name, level, tax_type_code, major_tag, use_yn 
                 FROM kra_item_classifications 
                 ORDER BY item_cls_code, tax_type_code"

                Using da As New MySqlDataAdapter(query, conn)
                    Dim dt As New DataTable()
                    da.Fill(dt)
                    Return dt
                End Using
            End Using
        End Function
    End Class
End Namespace
