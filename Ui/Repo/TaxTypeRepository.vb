Imports Core.Models.Tax
Imports MySql.Data.MySqlClient

Namespace Repo
    Public Interface ITaxTypeRepository
        Function GetAll() As List(Of TaxType)
    End Interface
    Public Class TaxTypeRepository
        Implements ITaxTypeRepository

        Private ReadOnly _connStr As String

        Public Sub New(connStr As String)
            _connStr = connStr
        End Sub

        Public Function GetAll() As List(Of TaxType) Implements ITaxTypeRepository.GetAll
            Dim list As New List(Of TaxType)
            Using conn As New MySqlConnection(_connStr)
                conn.Open()
                Dim sql = "SELECT Code, CodeName, CodeDescription FROM tax_type ORDER BY SortCode"
                Dim cmd As New MySqlCommand(sql, conn)
                Using rdr = cmd.ExecuteReader()
                    While rdr.Read()
                        list.Add(New TaxType With {
                            .Code = rdr("Code").ToString(),
                            .CodeName = rdr("CodeName").ToString(),
                            .CodeDescription = rdr("CodeDescription").ToString()
                        })
                    End While
                End Using
            End Using
            Return list
        End Function
    End Class
End Namespace