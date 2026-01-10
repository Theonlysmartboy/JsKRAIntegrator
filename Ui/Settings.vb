
Imports Core.Config

Public Class Settings
    Private _settingsManager As SettingsManager

    Public Sub New(Optional connectionString As String = "")
        InitializeComponent()

        If Not String.IsNullOrWhiteSpace(connectionString) Then
            _settingsManager = New SettingsManager(connectionString)
        Else
            _settingsManager = Nothing ' No DB until settings exist
        End If
    End Sub

    ' --- Form Load ---
    Private Async Sub SettingsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Load DB settings into the DataGridView
        Await LoadDBSettings()

        ' Load system settings from application settings
        txtDbServer.Text = If(My.Settings("db_server") IsNot Nothing, My.Settings("db_server"), "")
        txtDbUser.Text = If(My.Settings("db_user") IsNot Nothing, My.Settings("db_user"), "")
        txtDbName.Text = If(My.Settings("db_name") IsNot Nothing, My.Settings("db_name"), "")
        txtDbPassword.Text = If(My.Settings("db_password") IsNot Nothing, My.Settings("db_password"), "")
        txtDbPrefix.Text = If(My.Settings("db_prefix") IsNot Nothing, My.Settings("db_prefix"), "")
    End Sub

    ' --- Load DB Settings into DataGridView ---
    Private Async Function LoadDBSettings() As Task
        dgvSettings.Rows.Clear()

        ' Skip DB load if no connection is configured
        If _settingsManager Is Nothing Then
            ' Optionally, disable the DB tab until a connection is set
            TabPageDB.Enabled = False
            Return
        End If

        Try
            Dim allSettings = Await _settingsManager.GetAllSettings()

            ' Predefined default keys
            Dim defaultKeys = {"base_url", "pin", "branch_id", "device_serial", "timeout"}

            For Each keyName In defaultKeys
                Dim value As String = If(allSettings.ContainsKey(keyName), allSettings(keyName), "")
                dgvSettings.Rows.Add(keyName, value)
            Next

            ' Add other settings from DB
            For Each kvp In allSettings
                If Not defaultKeys.Contains(kvp.Key) Then
                    dgvSettings.Rows.Add(kvp.Key, kvp.Value)
                End If
            Next

        Catch ex As Exception
            ' Show error if DB cannot be reached
            CustomAlert.ShowAlert(Settings.ActiveForm, "Cannot load DB settings: " & ex.Message, "Error", CustomAlert.AlertType.Error)
            TabPageDB.Enabled = False
        End Try
        If dgvSettings.Columns("Delete") Is Nothing Then
            Dim btnCol As New DataGridViewButtonColumn()
            btnCol.Name = "Delete"
            btnCol.HeaderText = "Action"
            btnCol.Text = "Delete"
            btnCol.UseColumnTextForButtonValue = True
            btnCol.Width = 20
            dgvSettings.Columns.Add(btnCol)
        End If

        SetupGridColumnWidths()
        dgvSettings.DefaultCellStyle.WrapMode = DataGridViewTriState.True
        dgvSettings.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        dgvSettings.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvSettings.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True
        dgvSettings.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        dgvSettings.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgvSettings.ColumnHeadersHeight = 30
        dgvSettings.EnableHeadersVisualStyles = False
        AddHandler dgvSettings.CellDoubleClick, AddressOf dgvSettings_CellDoubleClick
    End Function

    ' --- Save DB Settings from DataGridView ---
    Private Async Sub btnSaveDB_Click(sender As Object, e As EventArgs) Handles btnSaveDB.Click
        Try
            For Each row As DataGridViewRow In dgvSettings.Rows
                If Not row.IsNewRow Then
                    Dim key = row.Cells("Key").Value?.ToString().Trim()
                    Dim value = row.Cells("Value").Value?.ToString().Trim()
                    If Not String.IsNullOrEmpty(key) Then
                        Await _settingsManager.SetSettingAsync(key, value)
                    End If
                End If
            Next
            CustomAlert.ShowAlert(Settings.ActiveForm, "Database settings saved successfully", "Success", CustomAlert.AlertType.Success)
        Catch ex As Exception
            CustomAlert.ShowAlert(Settings.ActiveForm, "Error saving database settings: " & ex.Message, "Error", CustomAlert.AlertType.Error)
        End Try
    End Sub

    '--- Handle Delete Button Click in DataGridView ---
    Private Async Sub dgvSettings_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvSettings.CellContentClick
        If e.RowIndex < 0 Then Exit Sub

        If dgvSettings.Columns(e.ColumnIndex).Name = "Delete" Then
            Dim key As String = dgvSettings.Rows(e.RowIndex).Cells("Key").Value?.ToString()

            If String.IsNullOrEmpty(key) Then Exit Sub

            Dim confirm = CustomAlert.ShowAlert(Settings.ActiveForm,
                $"Are you sure you want to delete '{key}'?" & vbCrLf &
                "This action cannot be undone.",
                "Confirm Delete",
                CustomAlert.AlertType.Confirm)

            If confirm = DialogResult.OK Then
                Try
                    Await _settingsManager.DeleteSettingAsync(key)
                    CustomAlert.ShowAlert(Settings.ActiveForm, $"'{key}' deleted successfully.", "Success", CustomAlert.AlertType.Success)

                    ' Reload grid
                    Await LoadDBSettings()

                Catch ex As Exception
                    CustomAlert.ShowAlert(Settings.ActiveForm, "Error deleting setting: " & ex.Message,
                                      "Error", CustomAlert.AlertType.Error)
                End Try
            End If
        End If
    End Sub

    ' --- Save System Settings ---
    Private Sub btnSaveSystem_Click(sender As Object, e As EventArgs) Handles btnSaveSystem.Click
        ' Save system settings to application settings
        My.Settings("db_server") = txtDbServer.Text
        My.Settings("db_user") = txtDbUser.Text
        My.Settings("db_name") = txtDbName.Text
        My.Settings("db_password") = txtDbPassword.Text
        My.Settings("db_prefix") = txtDbPrefix.Text

        ' Persist changes to config
        My.Settings.Save()

        MessageBox.Show("System settings saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ' --- For exposing system settings to core application ---
    Public Function GetSystemSetting(key As String) As String
        Return CStr(My.Settings(key))
    End Function

    Public Sub OpenSystemTab()
        Me.TabControlSettings.SelectedTab = Me.TabPageSystem
    End Sub

    Private Sub SetupGridColumnWidths()
        If dgvSettings.Columns.Count = 0 Then Exit Sub

        ' Adjust based on your column names
        With dgvSettings.Columns
            .Item("Key").FillWeight = 70     ' medium
            .Item("Value").FillWeight = 300 ' Big
            .Item("Delete").FillWeight = 50  ' Small
        End With
    End Sub
    Private Sub dgvSettings_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex < 0 Then Exit Sub ' Ignore headers

        Dim row = dgvSettings.Rows(e.RowIndex)
        Dim copiedText As New Text.StringBuilder()

        For Each cell As DataGridViewCell In row.Cells
            copiedText.AppendLine($"{dgvSettings.Columns(cell.ColumnIndex).HeaderText}: {cell.Value}")
        Next

        Clipboard.SetText(copiedText.ToString())
        CustomAlert.ShowAlert(Logs.ActiveForm,
            "Row copied to clipboard!",
            "Copied",
            CustomAlert.AlertType.Info
        )
    End Sub

End Class