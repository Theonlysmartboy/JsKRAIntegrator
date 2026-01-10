Imports System.Runtime.CompilerServices
Namespace Helpers

    Module DataTableExtensions

        <Extension()>
        Public Function ToDataTable(Of T)(source As IEnumerable(Of T)) As DataTable
            Dim dt As New DataTable()
            Dim props = GetType(T).GetProperties()

            For Each p In props
                dt.Columns.Add(p.Name, If(Nullable.GetUnderlyingType(p.PropertyType), p.PropertyType))
            Next

            For Each item In source
                Dim row = dt.NewRow()
                For Each p In props
                    Dim value = p.GetValue(item, Nothing)
                    row(p.Name) = If(value, DBNull.Value)
                Next
                dt.Rows.Add(row)
            Next

            Return dt
        End Function

    End Module
End Namespace