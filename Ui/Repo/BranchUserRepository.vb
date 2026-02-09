Imports Core.Models.Branch.User
Imports MySql.Data.MySqlClient

Namespace Repo

    Public Interface IBranchUserRepository
        Function GetByUserIdAsync(userId As String) As Task(Of BranchUser)
        Function SaveAsync(user As BranchUser) As Task
    End Interface

    Public Class BranchUserRepository
        Implements IBranchUserRepository
        Private ReadOnly _connString As String

        Public Sub New(connectionString As String)
            _connString = connectionString
        End Sub

        Public Async Function GetByUserIdAsync(userId As String) As Task(Of BranchUser) Implements IBranchUserRepository.GetByUserIdAsync
            Using conn As New MySqlConnection(_connString)
                Await conn.OpenAsync()
                Dim query As String = "SELECT * FROM branch_users WHERE user_id = @user_id LIMIT 1"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@user_id", userId)
                    Using reader = Await cmd.ExecuteReaderAsync()
                        If Await reader.ReadAsync() Then
                            Return New BranchUser With {
                        .Tin = reader("tin").ToString(),
                        .BhfId = reader("bhf_id").ToString(),
                        .UserId = reader("user_id").ToString(),
                        .UserNm = reader("user_nm").ToString(),
                        .Pwd = reader("pwd").ToString(),
                        .Adrs = If(IsDBNull(reader("adrs")), Nothing, reader("adrs").ToString()),
                        .Cntc = If(IsDBNull(reader("cntc")), Nothing, reader("cntc").ToString()),
                        .AuthCd = If(IsDBNull(reader("auth_cd")), Nothing, reader("auth_cd").ToString()),
                        .Remark = If(IsDBNull(reader("remark")), Nothing, reader("remark").ToString()),
                        .UseYn = reader("use_yn").ToString(),
                        .RegrNm = reader("regr_nm").ToString(),
                        .RegrId = reader("regr_id").ToString(),
                        .ModrNm = reader("modr_nm").ToString(),
                        .ModrId = reader("modr_id").ToString(),
                        .ResultDt = Convert.ToDateTime(reader("result_dt"))
                    }
                        End If
                    End Using
                End Using
            End Using
            Return Nothing
        End Function

        Public Async Function SaveAsync(user As BranchUser) As Task Implements IBranchUserRepository.SaveAsync
            Using conn As New MySqlConnection(_connString)
                Await conn.OpenAsync()
                Dim query As String = "INSERT INTO branch_users (tin, bhf_id, user_id, user_nm, pwd, adrs, cntc, auth_cd, remark, " &
                    "use_yn, regr_nm, regr_id, modr_nm, modr_id, result_dt) VALUES (@tin, @bhf_id, @user_id, @user_nm, @pwd, @adrs, " &
                    "@cntc, @auth_cd, @remark, @use_yn, @regr_nm, @regr_id, @modr_nm, @modr_id, @result_dt) ON DUPLICATE KEY UPDATE " &
                    "user_nm=@user_nm, adrs=@adrs, cntc=@cntc, auth_cd=@auth_cd, remark=@remark, use_yn=@use_yn, modr_nm=@modr_nm, " &
                    "modr_id=@modr_id, result_dt=@result_dt;"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@tin", user.Tin)
                    cmd.Parameters.AddWithValue("@bhf_id", user.BhfId)
                    cmd.Parameters.AddWithValue("@user_id", user.UserId)
                    cmd.Parameters.AddWithValue("@user_nm", user.UserNm)
                    cmd.Parameters.AddWithValue("@pwd", user.Pwd)
                    cmd.Parameters.AddWithValue("@adrs", user.Adrs)
                    cmd.Parameters.AddWithValue("@cntc", user.Cntc)
                    cmd.Parameters.AddWithValue("@auth_cd", user.AuthCd)
                    cmd.Parameters.AddWithValue("@remark", user.Remark)
                    cmd.Parameters.AddWithValue("@use_yn", user.UseYn)
                    cmd.Parameters.AddWithValue("@regr_nm", user.RegrNm)
                    cmd.Parameters.AddWithValue("@regr_id", user.RegrId)
                    cmd.Parameters.AddWithValue("@modr_nm", user.ModrNm)
                    cmd.Parameters.AddWithValue("@modr_id", user.ModrId)
                    cmd.Parameters.AddWithValue("@result_dt", user.ResultDt)
                    Await cmd.ExecuteNonQueryAsync()
                End Using
            End Using
        End Function
    End Class
End Namespace