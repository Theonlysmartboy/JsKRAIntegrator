Imports Core.Models.Branch
Imports MySql.Data.MySqlClient

Namespace Repo

    Public Interface IBranchRepository
        Function SaveAsync(branches As List(Of BranchInfo), resultDt As DateTime) As Task
        Function GetAllAsync() As Task(Of DataTable)
        Function GetLastBranchSyncAsync() As Task(Of String)
    End Interface

    Public Class BranchRepository
        Implements IBranchRepository
        Private ReadOnly _connectionString As String

        Public Sub New(connectionString As String)
            _connectionString = connectionString
        End Sub

        Public Async Function SaveAsync(branches As List(Of BranchInfo), resultDt As DateTime) As Task Implements IBranchRepository.SaveAsync
            If branches Is Nothing OrElse branches.Count = 0 Then Exit Function
            Using conn As New MySqlConnection(_connectionString)
                Await conn.OpenAsync()
                Using tran = conn.BeginTransaction()
                    For Each b In branches
                        Dim sql = "INSERT INTO vscu_branches (tin, bhf_id, bhf_nm, bhf_stts_cd, prvnc_nm, dstrt_nm, sctr_nm, loc_desc, " &
                                "mgr_nm, mgr_tel_no, mgr_email, hq_yn, result_dt) VALUES (@tin, @bhf_id, @bhf_nm, @bhf_stts_cd, @prvnc_nm, " &
                                "@dstrt_nm, @sctr_nm, @loc_desc, @mgr_nm, @mgr_tel_no, @mgr_email, @hq_yn, @result_dt) ON DUPLICATE KEY UPDATE " &
                                "bhf_nm = VALUES(bhf_nm), bhf_stts_cd = VALUES(bhf_stts_cd), prvnc_nm = VALUES(prvnc_nm), dstrt_nm = " &
                                "VALUES(dstrt_nm), sctr_nm = VALUES(sctr_nm), loc_desc = VALUES(loc_desc), mgr_nm = VALUES(mgr_nm), " &
                                "mgr_tel_no = VALUES(mgr_tel_no), mgr_email = VALUES(mgr_email), hq_yn = VALUES(hq_yn), result_dt = " &
                                "VALUES(result_dt);"
                        Using cmd As New MySqlCommand(sql, conn, tran)
                            cmd.Parameters.AddWithValue("@tin", b.tin)
                            cmd.Parameters.AddWithValue("@bhf_id", b.bhfId)
                            cmd.Parameters.AddWithValue("@bhf_nm", b.bhfNm)
                            cmd.Parameters.AddWithValue("@bhf_stts_cd", b.bhfSttsCd)
                            cmd.Parameters.AddWithValue("@prvnc_nm", b.prvncNm)
                            cmd.Parameters.AddWithValue("@dstrt_nm", b.dstrtNm)
                            cmd.Parameters.AddWithValue("@sctr_nm", b.sctrNm)
                            cmd.Parameters.AddWithValue("@loc_desc", b.locDesc)
                            cmd.Parameters.AddWithValue("@mgr_nm", b.mgrNm)
                            cmd.Parameters.AddWithValue("@mgr_tel_no", b.mgrTelNo)
                            cmd.Parameters.AddWithValue("@mgr_email", b.mgrEmail)
                            cmd.Parameters.AddWithValue("@hq_yn", b.hqYn)
                            cmd.Parameters.AddWithValue("@result_dt", resultDt)
                            Await cmd.ExecuteNonQueryAsync()
                        End Using
                    Next
                    tran.Commit()
                End Using
            End Using
        End Function

        Public Async Function GetAllAsync() As Task(Of DataTable) Implements IBranchRepository.GetAllAsync
            Using conn As New MySqlConnection(_connectionString)
                Await conn.OpenAsync()
                Dim sql = "SELECT tin, bhf_id, bhf_nm, prvnc_nm, dstrt_nm, mgr_nm, mgr_tel_no, hq_yn FROM vscu_branches ORDER BY bhf_id ASC"
                Dim da As New MySqlDataAdapter(sql, conn)
                Dim dt As New DataTable()
                da.Fill(dt)
                Return dt
            End Using
        End Function

        Public Async Function GetLastBranchSyncAsync() As Task(Of String) Implements IBranchRepository.GetLastBranchSyncAsync
            Using conn As New MySqlConnection(_connectionString)
                Await conn.OpenAsync()
                Dim cmd As New MySqlCommand("SELECT DATE_FORMAT(IFNULL(MAX(result_dt),'2000-01-01'), '%Y%m%d%H%i%s') " &
                                            "FROM vscu_branches", conn)
                Dim result = Await cmd.ExecuteScalarAsync()
                Return result.ToString()
            End Using
        End Function
    End Class
End Namespace
