Imports MySql.Data.MySqlClient
Imports Core.Models.Branch.Customer

Namespace Repo
    Public Interface ICustomerRepository
        Function SaveAsync(customers As List(Of CustomerInfo)) As Task
        Function GetAllAsync() As Task(Of DataTable)
    End Interface

    Public Class CustomerRepository
        Implements ICustomerRepository

        Private ReadOnly _connString As String

        Public Sub New(connectionString As String)
            _connString = connectionString
        End Sub

        Public Async Function SaveAsync(customers As List(Of CustomerInfo)) As Task Implements ICustomerRepository.SaveAsync
            If customers Is Nothing OrElse customers.Count = 0 Then Exit Function
            Using conn As New MySqlConnection(_connString)
                Await conn.OpenAsync()
                For Each c In customers
                    Dim sql = "INSERT INTO vscu_customers (tin, taxprNm, taxprSttsCd, prvncNm, dstrtNm, sctrNm, locDesc) " &
                              "VALUES (@Tin, @TaxprNm, @TaxprSttsCd, @PrvncNm, @DstrtNm, @SctrNm, @LocDesc) " &
                              "ON DUPLICATE KEY UPDATE taxprNm=VALUES(taxprNm), taxprSttsCd=VALUES(taxprSttsCd), " &
                              "prvncNm=VALUES(prvncNm), dstrtNm=VALUES(dstrtNm), sctrNm=VALUES(sctrNm), locDesc=VALUES(locDesc);"
                    Using cmd As New MySqlCommand(sql, conn)
                        cmd.Parameters.AddWithValue("@Tin", c.Tin)
                        cmd.Parameters.AddWithValue("@TaxprNm", c.TaxprNm)
                        cmd.Parameters.AddWithValue("@TaxprSttsCd", c.TaxprSttsCd)
                        cmd.Parameters.AddWithValue("@PrvncNm", c.PrvncNm)
                        cmd.Parameters.AddWithValue("@DstrtNm", c.DstrtNm)
                        cmd.Parameters.AddWithValue("@SctrNm", c.SctrNm)
                        cmd.Parameters.AddWithValue("@LocDesc", c.LocDesc)
                        Await cmd.ExecuteNonQueryAsync()
                    End Using
                Next
            End Using
        End Function

        Public Async Function GetAllAsync() As Task(Of DataTable) Implements ICustomerRepository.GetAllAsync
            Using conn As New MySqlConnection(_connString)
                Await conn.OpenAsync()
                Dim da As New MySqlDataAdapter("SELECT tin, taxprNm, taxprSttsCd, prvncNm, dstrtNm, sctrNm, " &
                                                "locDesc FROM vscu_customers ORDER BY taxprNm ASC", conn)
                Dim dt As New DataTable()
                da.Fill(dt)
                Return dt
            End Using
        End Function
    End Class
End Namespace
