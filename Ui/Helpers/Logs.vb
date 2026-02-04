
Imports Core.Logging
Imports Ui.Helpers

Public Class Logs
    Private ReadOnly _logRepo As LogRepository
    Private LogsTable As DataTable
    Private IsPlaceholder As Boolean = True

    Public Sub New(logRepo As LogRepository)
        InitializeComponent()
        _logRepo = logRepo
    End Sub
    Private Async Sub LogsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LogsTable = Await _logRepo.GetLogsAsync()
        GridLogs.DataSource = LogsTable.DefaultView

        SetupGridColumnWidths()

        GridLogs.DefaultCellStyle.WrapMode = DataGridViewTriState.True
        GridLogs.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        GridLogs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        GridLogs.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True
        GridLogs.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 11, FontStyle.Bold)
        GridLogs.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        GridLogs.ColumnHeadersHeight = 40    ' Increase header height
        GridLogs.EnableHeadersVisualStyles = False   ' Important!

        AddHandler GridLogs.CellFormatting, AddressOf GridLogs_CellFormatting
        AddHandler txtSearch.GotFocus, Sub()
                                           If IsPlaceholder Then
                                               txtSearch.Text = ""
                                               txtSearch.ForeColor = Color.Black
                                               IsPlaceholder = False
                                           End If
                                       End Sub

        AddHandler txtSearch.LostFocus, Sub()
                                            If txtSearch.Text.Trim() = "" Then
                                                IsPlaceholder = True
                                                txtSearch.ForeColor = Color.Gray
                                                txtSearch.Text = "Search logs..."
                                                ' Reset filter when placeholder returns
                                                ApplyFilter()
                                            End If
                                        End Sub
        AddHandler GridLogs.CellDoubleClick, AddressOf GridLogs_CellDoubleClick

    End Sub
    Private Sub SetupGridColumnWidths()
        If GridLogs.Columns.Count = 0 Then Exit Sub

        ' Adjust based on your column names
        With GridLogs.Columns
            .Item("Id").FillWeight = 20        ' Smallest
            .Item("Level").FillWeight = 40     ' Small
            .Item("Timestamp").FillWeight = 70 ' Medium
            .Item("Message").FillWeight = 150  ' Big
            .Item("Details").FillWeight = 330  ' Largest
        End With
    End Sub
    Private Sub btnFilter_Click(sender As Object, e As EventArgs) Handles btnFilter.Click
        ApplyFilter()
    End Sub
    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        ' Ignore placeholder mode
        If txtSearch.ForeColor = Color.Gray Then Exit Sub
        ' just for safety
        If IsPlaceholder Then Exit Sub

        ApplyFilter()
    End Sub
    Private Async Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click

        Dim result = CustomAlert.ShowAlert(Me, "Are you sure you want to clear ALL logs?" & vbCrLf & "This action cannot be undone.",
            "Confirm Deletion", CustomAlert.AlertType.Confirm, CustomAlert.ButtonType.YesNo)

        If result = DialogResult.Yes Then
            Try
                Await _logRepo.ClearLogsAsync()

                CustomAlert.ShowAlert(Me, "Logs cleared successfully!", "Success",
                    CustomAlert.AlertType.Success, CustomAlert.ButtonType.OK)

                ' Reload data and reset filters
                LogsTable = Await _logRepo.GetLogsAsync()
                GridLogs.DataSource = LogsTable.DefaultView
                txtSearch.Text = ""
                dpStart.Value = DateTime.Now
                dpEnd.Value = DateTime.Now

            Catch ex As Exception
                CustomAlert.ShowAlert(Me, "Failed to clear logs: " & ex.Message, "Error",
                    CustomAlert.AlertType.Error, CustomAlert.ButtonType.OK)
            Finally

            End Try
        Else
            ' Optional: notify cancel
            CustomAlert.ShowAlert(Me, "Action cancelled.", "Cancelled", CustomAlert.AlertType.Info, CustomAlert.ButtonType.OK
            )
        End If

    End Sub
    Private Sub ApplyFilter()
        If LogsTable Is Nothing Then Exit Sub

        Dim dv As DataView = LogsTable.DefaultView

        Dim search As String = txtSearch.Text.Trim()
        Dim startD As Date = dpStart.Value.Date
        Dim endD As Date = dpEnd.Value.Date.AddDays(1).AddSeconds(-1)

        Dim filter As String = $"Timestamp >= #{startD}# AND Timestamp <= #{endD}#"

        If search <> "" AndAlso txtSearch.ForeColor <> Color.Gray Then
            Dim safeSearch = search.Replace("'", "''")

            ' Build OR group
            Dim searchFilter As String =
        $"(Message LIKE '%{safeSearch}%' OR Level LIKE '%{safeSearch}%' OR Details LIKE '%{safeSearch}%')"

            filter &= " AND " & searchFilter
        End If

        dv.RowFilter = filter
    End Sub
    Private Sub GridLogs_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs)
        If e.RowIndex < 0 Then Exit Sub

        Dim grid = CType(sender, DataGridView)

        ' Identify the Level column safely
        If grid.Columns(e.ColumnIndex).DataPropertyName <> "Level" Then Exit Sub

        Dim levelValue As String = ""

        If e.Value IsNot Nothing AndAlso e.Value IsNot DBNull.Value Then
            levelValue = e.Value.ToString().ToUpper()
        End If

        Dim row = grid.Rows(e.RowIndex)

        Select Case levelValue
            Case "INFO"
                row.DefaultCellStyle.BackColor = Color.LightBlue

            Case "ERROR"
                row.DefaultCellStyle.BackColor = Color.LightCoral

            Case "WARNING", "WARN"
                row.DefaultCellStyle.BackColor = Color.Khaki

            Case Else
                row.DefaultCellStyle.BackColor = Color.White
        End Select
    End Sub

    Private Sub GridLogs_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex < 0 Then Exit Sub ' Ignore headers

        Dim row = GridLogs.Rows(e.RowIndex)
        Dim copiedText As New Text.StringBuilder()

        For Each cell As DataGridViewCell In row.Cells
            Dim valueText As String = ""

            If cell.Value IsNot Nothing AndAlso cell.Value IsNot DBNull.Value Then
                valueText = cell.Value.ToString()
            End If

            copiedText.AppendLine($"{GridLogs.Columns(cell.ColumnIndex).HeaderText}: {valueText}")

        Next

        Clipboard.SetText(copiedText.ToString())
        CustomAlert.ShowAlert(Me, "Row copied to clipboard!", "Copied", CustomAlert.AlertType.Info, CustomAlert.ButtonType.OK
        )
    End Sub
End Class