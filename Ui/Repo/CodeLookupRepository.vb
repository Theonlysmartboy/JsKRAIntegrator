Imports MySql.Data.MySqlClient
Namespace Repo

    Public Class CodeLookupRepository
        Private ReadOnly _connectionString As String

        Public Sub New(connectionString As String)
            _connectionString = connectionString
        End Sub
        Public Async Function LoadAllFlattenedAsync() As Task(Of DataTable)
            Using conn As New MySqlConnection(_connectionString)
                Await conn.OpenAsync()
                Dim query As String = "SELECT cls.cdCls AS codeType, dt.cd AS code, dt.cdNm AS name, dt.cdDesc AS description " +
                    "FROM code_details dt INNER JOIN code_classes cls ON cls.id = dt.class_id ORDER BY cls.cdCls, dt.cd"
                Using da As New MySqlDataAdapter(query, conn)
                    Dim dt As New DataTable()
                    da.Fill(dt)
                    Return dt
                End Using
            End Using
        End Function
    End Class
End Namespace
