Imports Core.Models.Code
Imports MySql.Data.MySqlClient

Namespace Repo
    Public Class CodeMappedRepository
        Private ReadOnly _cs As String

        Public Sub New(cs As String)
            _cs = cs
        End Sub

        Public Async Function GetMapAsync(cdCls As String) As Task(Of CodeClassMap)
            Using conn As New MySqlConnection(_cs)
                Await conn.OpenAsync()
                Dim sql = "SELECT * FROM code_class_map WHERE cdCls=@c"
                Dim cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@c", cdCls)
                Using r = Await cmd.ExecuteReaderAsync()
                    If Await r.ReadAsync() Then
                        Return New CodeClassMap With {
                        .cdCls = cdCls,
                        .table_name = r("table_name"),
                        .code_column = r("code_column"),
                        .name_column = r("name_column"),
                        .desc_column = r("desc_column"),
                        .sort_column = r("sort_column"),
                        .remark_column = If(IsDBNull(r("remark_column")), Nothing, r("remark_column"))
                    }
                    End If
                End Using
            End Using
            Return Nothing
        End Function

        Public Async Function SaveAsync(cls As CodeClass, dt As CodeDetail) As Task
            Dim map = Await GetMapAsync(cls.cdCls)
            If map Is Nothing Then
                Throw New Exception("No mapping found for cdCls = " & cls.cdCls)
            End If
            Dim sql = $"INSERT INTO {map.table_name}
                ({map.code_column}, {map.sort_column}, {map.name_column}, {map.desc_column}, {map.remark_column})
            VALUES (@code, @sort, @nm, @desc, @rmk)
            ON DUPLICATE KEY UPDATE
                {map.name_column}=@nm,
                {map.desc_column}=@desc,
                {map.sort_column}=@sort,
                {map.remark_column}=@rmk;"
            Using conn As New MySqlConnection(_cs)
                Await conn.OpenAsync()
                Dim cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@code", dt.cd)
                cmd.Parameters.AddWithValue("@sort", dt.srtOrd)
                cmd.Parameters.AddWithValue("@nm", dt.cdNm)
                cmd.Parameters.AddWithValue("@desc", dt.cdDesc)
                cmd.Parameters.AddWithValue("@rmk", dt.remark)
                Await cmd.ExecuteNonQueryAsync()
            End Using
        End Function

        Public Async Function LoadMappedTableAsync(cdCls As String) As Task(Of DataTable)
            Dim map = Await GetMapAsync(cdCls)
            If map Is Nothing Then Throw New Exception("Mapping not found for cdCls = " & cdCls)
            Dim dt As New DataTable()
            Dim sql = $"SELECT 
                    {map.code_column} AS Code,
                    {map.name_column} AS Name,
                    {map.desc_column} AS Description,
                    {map.sort_column} AS SortOrder,
                    {If(String.IsNullOrEmpty(map.remark_column), "NULL", map.remark_column)} AS Remark
               FROM {map.table_name}
               ORDER BY {map.sort_column}"
            Using conn As New MySqlConnection(_cs)
                Await conn.OpenAsync()
                Using da As New MySqlDataAdapter(sql, conn)
                    da.Fill(dt)
                End Using
            End Using
            Return dt
        End Function
    End Class
End Namespace