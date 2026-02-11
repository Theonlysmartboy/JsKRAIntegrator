Imports Core.Models.Branch.Insurance
Imports MySql.Data.MySqlClient

Namespace Repo.BranchRepo
    Public Interface IBranchInsuranceRepository
        Function GetAll() As List(Of BranchInsurance)
        Function GetUnsynced() As List(Of BranchInsurance)
        Sub Save(insurances As List(Of BranchInsurance))
        Sub MarkAsSynced(ids As List(Of Integer))
    End Interface

    Public Class BranchInsuranceRepository
        Implements IBranchInsuranceRepository
        Private ReadOnly _connectionString As String

        Public Sub New(connectionString As String)
            _connectionString = connectionString
        End Sub

        Public Function GetAll() As List(Of BranchInsurance) Implements IBranchInsuranceRepository.GetAll
            Dim list As New List(Of BranchInsurance)
            Using conn As New MySqlConnection(_connectionString)
                conn.Open()
                Dim cmd As New MySqlCommand("SELECT * FROM BranchInsurance", conn)
                Dim reader = cmd.ExecuteReader()
                While reader.Read()
                    list.Add(New BranchInsurance With {
                    .Id = CInt(reader("Id")),
                    .InsuranceCode = reader("InsuranceCode").ToString(),
                    .InsuranceName = reader("InsuranceName").ToString(),
                    .InsuranceRate = CDec(reader("InsuranceRate")),
                    .UseYn = reader("UseYn").ToString(),
                    .IsSynced = CBool(reader("IsSynced"))
                })
                End While
            End Using
            Return list
        End Function

        Public Function GetUnsynced() As List(Of BranchInsurance) Implements IBranchInsuranceRepository.GetUnsynced
            Return GetAll().Where(Function(x) x.IsSynced = False).ToList()
        End Function

        Public Sub Save(insurances As List(Of BranchInsurance)) Implements IBranchInsuranceRepository.Save
            Using conn As New MySqlConnection(_connectionString)
                conn.Open()
                For Each Item As BranchInsurance In insurances
                    Dim cmd As New MySqlCommand("INSERT INTO BranchInsurance (InsuranceCode, InsuranceName, InsuranceRate, UseYn, " &
                                                "IsSynced, CreatedBy, CreatedDate) VALUES (@Code,@Name,@Rate,@UseYn,0,@User,NOW())", conn)
                    cmd.Parameters.AddWithValue("@Code", Item.InsuranceCode)
                    cmd.Parameters.AddWithValue("@Name", Item.InsuranceName)
                    cmd.Parameters.AddWithValue("@Rate", Item.InsuranceRate)
                    cmd.Parameters.AddWithValue("@UseYn", Item.UseYn)
                    cmd.Parameters.AddWithValue("@User", Item.CreatedBy)
                    cmd.ExecuteNonQuery()
                Next
            End Using
        End Sub

        Public Sub MarkAsSynced(ids As List(Of Integer)) Implements IBranchInsuranceRepository.MarkAsSynced
            Using conn As New MySqlConnection(_connectionString)
                conn.Open()
                For Each id In ids
                    Dim cmd As New MySqlCommand("UPDATE BranchInsurance SET IsSynced = 1 WHERE Id = @Id", conn)
                    cmd.Parameters.AddWithValue("@Id", id)
                    cmd.ExecuteNonQuery()
                Next
            End Using
        End Sub
    End Class
End Namespace