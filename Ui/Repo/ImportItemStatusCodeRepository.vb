Imports Core.Core.Models.Item.Import
Imports MySql.Data.MySqlClient

Namespace Repo
    Public Class ImportItemStatusCodeRepository
        Private ReadOnly _connString As String

        Public Sub New(connString As String)
            _connString = connString
        End Sub

        Public Function GetAll() As List(Of ImportItemStatusCode)
            Dim list As New List(Of ImportItemStatusCode)
            Using conn As New MySqlConnection(_connString)
                conn.Open()
                Dim cmd As New MySqlCommand("SELECT Code, CodeName FROM import_item_status", conn)
                Using rdr = cmd.ExecuteReader()
                    While rdr.Read()
                        list.Add(New ImportItemStatusCode With {
                            .StatusCode = rdr("Code").ToString(),
                            .StatusName = rdr("CodeName").ToString()
                        })
                    End While
                End Using
            End Using
            Return list
        End Function
    End Class
End Namespace
