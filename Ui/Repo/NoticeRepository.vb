Imports Core.Models.Notice
Imports MySql.Data.MySqlClient

Namespace Repo
    Public Interface INoticeRepository
        Function SaveAsync(notices As List(Of VscuNotice)) As Task
        Function GetAllAsync() As Task(Of DataTable)

    End Interface

    Public Class NoticeRepository
        Implements INoticeRepository

        Private ReadOnly _connectionString As String

        Public Sub New(connectionString As String)
            _connectionString = connectionString
        End Sub

        Public Async Function GetAllAsync() As Task(Of DataTable) Implements INoticeRepository.GetAllAsync
            Using conn As New MySqlConnection(_connectionString)
                Await conn.OpenAsync()
                Dim da As New MySqlDataAdapter("SELECT notice_no, title, content, detail_url, registered_by, reg_dt, result_dt FROM vscu_notices ORDER BY reg_dt DESC", conn)
                Dim dt As New DataTable()
                da.Fill(dt)
                Return dt
            End Using
        End Function

        Public Async Function SaveAsync(notices As List(Of VscuNotice)) As Task Implements INoticeRepository.SaveAsync
            If notices Is Nothing OrElse notices.Count = 0 Then Exit Function
            Using conn As New MySqlConnection(_connectionString)
                Await conn.OpenAsync()
                Using tran = conn.BeginTransaction()
                    For Each n In notices
                        Dim sql = "INSERT INTO vscu_notices (notice_no, title, content, detail_url, registered_by, reg_dt, result_dt) " &
                                "VALUES (@NoticeNo, @Title, @Content, @DetailUrl, @RegisteredBy, @RegDt, @ResultDt) ON DUPLICATE KEY UPDATE " &
                                "title = VALUES(title), content = VALUES(content), detail_url = VALUES(detail_url), registered_by = " &
                                "VALUES(registered_by), reg_dt = VALUES(reg_dt), result_dt = VALUES(result_dt);"
                        Dim cmd As New MySqlCommand(sql, conn, tran)
                        cmd.Parameters.AddWithValue("@NoticeNo", n.NoticeNo)
                        cmd.Parameters.AddWithValue("@Title", n.Title)
                        cmd.Parameters.AddWithValue("@Content", n.Content)
                        cmd.Parameters.AddWithValue("@DetailUrl", n.DetailUrl)
                        cmd.Parameters.AddWithValue("@RegisteredBy", n.RegisteredBy)
                        cmd.Parameters.AddWithValue("@RegDt", n.RegDt)
                        cmd.Parameters.AddWithValue("@ResultDt", n.ResultDt)

                        Await cmd.ExecuteNonQueryAsync()
                    Next
                    tran.Commit()
                End Using
            End Using
        End Function
    End Class
End Namespace