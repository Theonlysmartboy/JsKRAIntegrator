Imports Core.Models.Code
Imports MySql.Data.MySqlClient
Namespace Repo

    Public Class CodeDetailRepository
        Private ReadOnly _cs As String

        Public Sub New(cs As String)
            _cs = cs
        End Sub

        Public Async Function SaveOrUpdateAsync(classId As Integer, dtl As CodeDetail) As Task
            Using conn As New MySqlConnection(_cs)
                Await conn.OpenAsync()

                ' Check if exists
                Dim checkCmd As New MySqlCommand("
                SELECT id FROM code_details 
                WHERE class_id=@cid AND cd=@cd", conn)

                checkCmd.Parameters.AddWithValue("@cid", classId)
                checkCmd.Parameters.AddWithValue("@cd", dtl.cd)

                Dim exists = Await checkCmd.ExecuteScalarAsync()

                If exists IsNot Nothing Then
                    ' UPDATE
                    Dim updateCmd As New MySqlCommand("
                    UPDATE code_details
                    SET cdNm=@nm, cdDesc=@desc, useYn=@useYn, srtOrd=@srt,
                        userDfnCd1=@u1, userDfnCd2=@u2, userDfnCd3=@u3
                    WHERE id=@id", conn)

                    updateCmd.Parameters.AddWithValue("@id", exists)
                    updateCmd.Parameters.AddWithValue("@nm", dtl.cdNm)
                    updateCmd.Parameters.AddWithValue("@desc", dtl.cdDesc)
                    updateCmd.Parameters.AddWithValue("@useYn", dtl.useYn)
                    updateCmd.Parameters.AddWithValue("@srt", dtl.srtOrd)
                    updateCmd.Parameters.AddWithValue("@u1", dtl.userDfnCd1)
                    updateCmd.Parameters.AddWithValue("@u2", dtl.userDfnCd2)
                    updateCmd.Parameters.AddWithValue("@u3", dtl.userDfnCd3)

                    Await updateCmd.ExecuteNonQueryAsync()
                Else
                    ' INSERT
                    Dim insertCmd As New MySqlCommand("
                    INSERT INTO code_details(class_id, cd, cdNm, cdDesc,
                        useYn, srtOrd, userDfnCd1, userDfnCd2, userDfnCd3)
                    VALUES(@cid, @cd, @nm, @desc, @useYn, @srt, @u1, @u2, @u3)", conn)

                    insertCmd.Parameters.AddWithValue("@cid", classId)
                    insertCmd.Parameters.AddWithValue("@cd", dtl.cd)
                    insertCmd.Parameters.AddWithValue("@nm", dtl.cdNm)
                    insertCmd.Parameters.AddWithValue("@desc", dtl.cdDesc)
                    insertCmd.Parameters.AddWithValue("@useYn", dtl.useYn)
                    insertCmd.Parameters.AddWithValue("@srt", dtl.srtOrd)
                    insertCmd.Parameters.AddWithValue("@u1", dtl.userDfnCd1)
                    insertCmd.Parameters.AddWithValue("@u2", dtl.userDfnCd2)
                    insertCmd.Parameters.AddWithValue("@u3", dtl.userDfnCd3)

                    Await insertCmd.ExecuteNonQueryAsync()
                End If
            End Using
        End Function
    End Class
End Namespace