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
                Dim cmd As New MySqlCommand("TRUNCATE TABLE itemclslist", conn)
                Await cmd.ExecuteNonQueryAsync()
            End Using
        End Function

        Public Async Function SaveAsync(entry As ItemClassificationEntry) As Task
            Using conn As New MySqlConnection(_connString)
                Await conn.OpenAsync()
                Dim cmd As New MySqlCommand("INSERT INTO itemclslist (itemClsCd, itemClsNm, itemClsLvl, taxTyCd, mjrTgYn, useYn)
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
                    "SELECT itemClsCd, itemClsNm, itemClsLvl, taxTyCd, mjrTgYn, useYn FROM itemclslist ORDER BY itemClsCd, taxTyCd"
                Using da As New MySqlDataAdapter(query, conn)
                    Dim dt As New DataTable()
                    da.Fill(dt)
                    Return dt
                End Using
            End Using
        End Function

        Public Function GetAll() As List(Of ItemClassificationEntity)
            Dim list As New List(Of ItemClassificationEntity)
            Using conn As New MySqlConnection(_connString)
                conn.Open()
                Dim cmd As New MySqlCommand("SELECT itemClsCd, itemClsNm FROM itemclslist", conn)
                Using rdr = cmd.ExecuteReader()
                    While rdr.Read()
                        list.Add(New ItemClassificationEntity With {
                            .ItemClsCd = rdr("itemClsCd").ToString(),
                            .ItemClsNm = rdr("itemClsNm").ToString()
                        })
                    End While
                End Using
            End Using
            Return list
        End Function
    End Class
End Namespace
