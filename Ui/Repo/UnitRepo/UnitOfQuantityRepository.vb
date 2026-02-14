Imports Core.Models.Units
Imports MySql.Data.MySqlClient

Namespace Repo.UnitRepo
    Public Class UnitOfQuantityRepository
        Private ReadOnly _conn As String

        Public Sub New(conn As String)
            _conn = conn
        End Sub

        Public Async Function GetAll() As Task(Of List(Of UnitOfQuantity))
            Dim list As New List(Of UnitOfQuantity)
            Dim sql As String = "SELECT Code, CodeName FROM unit_of_quantity WHERE Code IS NOT NULL ORDER BY SortCode"
            Using conn As New MySqlConnection(_conn)
                Await conn.OpenAsync()
                Dim cmd As New MySqlCommand(sql, conn)
                Using reader = cmd.ExecuteReader()
                    While reader.Read()
                        list.Add(New UnitOfQuantity With {
                            .Code = reader("Code").ToString(),
                            .CodeName = reader("CodeName").ToString()
                        })
                    End While
                End Using
            End Using
                Return list
        End Function
    End Class
End Namespace
