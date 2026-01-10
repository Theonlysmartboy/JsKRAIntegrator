Imports Core.Models.Code
Imports MySql.Data.MySqlClient
Namespace Repo

    Public Class CodeRepository
        Private ReadOnly _cs As String

        Public Sub New(cs As String)
            _cs = cs
        End Sub

        Public Async Function SaveOrUpdateAsync(cls As CodeClass) As Task(Of Integer)
            Using conn As New MySqlConnection(_cs)
                Await conn.OpenAsync()

                ' Check if record exists
                Dim checkCmd As New MySqlCommand("SELECT id FROM code_classes WHERE cdCls=@c", conn)
                checkCmd.Parameters.AddWithValue("@c", cls.cdCls)

                Dim result = Await checkCmd.ExecuteScalarAsync()

                If result IsNot Nothing Then
                    ' UPDATE
                    Dim updateCmd As New MySqlCommand("
                    UPDATE code_classes
                    SET cdClsNm=@nm, cdClsDesc=@desc, useYn=@useYn,
                        userDfnNm1=@u1, userDfnNm2=@u2, userDfnNm3=@u3
                    WHERE cdCls=@c", conn)

                    updateCmd.Parameters.AddWithValue("@nm", cls.cdClsNm)
                    updateCmd.Parameters.AddWithValue("@desc", cls.cdClsDesc)
                    updateCmd.Parameters.AddWithValue("@useYn", cls.useYn)
                    updateCmd.Parameters.AddWithValue("@u1", cls.userDfnNm1)
                    updateCmd.Parameters.AddWithValue("@u2", cls.userDfnNm2)
                    updateCmd.Parameters.AddWithValue("@u3", cls.userDfnNm3)
                    updateCmd.Parameters.AddWithValue("@c", cls.cdCls)

                    Await updateCmd.ExecuteNonQueryAsync()

                    Return CInt(result)
                Else
                    ' INSERT
                    Dim insertCmd As New MySqlCommand("
                    INSERT INTO code_classes(cdCls, cdClsNm, cdClsDesc, useYn,
                        userDfnNm1, userDfnNm2, userDfnNm3)
                    VALUES(@c, @nm, @desc, @useYn, @u1, @u2, @u3);
                    SELECT LAST_INSERT_ID();", conn)

                    insertCmd.Parameters.AddWithValue("@c", cls.cdCls)
                    insertCmd.Parameters.AddWithValue("@nm", cls.cdClsNm)
                    insertCmd.Parameters.AddWithValue("@desc", cls.cdClsDesc)
                    insertCmd.Parameters.AddWithValue("@useYn", cls.useYn)
                    insertCmd.Parameters.AddWithValue("@u1", cls.userDfnNm1)
                    insertCmd.Parameters.AddWithValue("@u2", cls.userDfnNm2)
                    insertCmd.Parameters.AddWithValue("@u3", cls.userDfnNm3)

                    Return Await insertCmd.ExecuteScalarAsync()
                End If
            End Using
        End Function
    End Class
End Namespace
