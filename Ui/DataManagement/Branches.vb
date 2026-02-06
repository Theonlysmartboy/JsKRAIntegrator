Imports Core.Config
Imports Core.Logging
Imports Core.Models.Branch
Imports Core.Services
Imports Ui.Helpers
Imports Ui.Repo

Public Class Branches
    Private _settingsManager As SettingsManager
    Private _integrator As VSCUIntegrator
    Private ReadOnly _branchRepo As IBranchRepository
    Private _logger As Logger
    Private _conn As String
    Private OriginalTables As New Dictionary(Of DataGridView, DataTable)

    Public Sub New(connectionString As String)
        InitializeComponent()
        _conn = connectionString
        ' Initialize settings manager
        _settingsManager = New SettingsManager(connectionString)
        ' Initialize logger
        Dim repo As New LogRepository(connectionString)
        _logger = New Logger(repo)
        _branchRepo = New BranchRepository(connectionString)
        ' Build IntegratorSettings from DB
        Dim baseUrlTask = _settingsManager.GetSettingAsync("base_url")
        Dim pinTask = _settingsManager.GetSettingAsync("pin")
        Dim branchIdTask = _settingsManager.GetSettingAsync("branch_id")
        Dim deviceSerialTask = _settingsManager.GetSettingAsync("device_serial")
        Dim timeoutTask = _settingsManager.GetSettingAsync("timeout")
        Task.WhenAll(baseUrlTask, pinTask, branchIdTask, deviceSerialTask, timeoutTask).ContinueWith(Sub(t)
                                                                                                         Dim settings As New IntegratorSettings With {
                                                                                                             .BaseUrl = baseUrlTask.Result,
                                                                                                             .Pin = pinTask.Result,
                                                                                                             .BranchId = branchIdTask.Result,
                                                                                                             .DeviceSerial = deviceSerialTask.Result,
                                                                                                             .Timeout = If(Integer.TryParse(timeoutTask.Result, Nothing), CInt(timeoutTask.Result), 30)
                                                                                                         }
                                                                                                         _integrator = New VSCUIntegrator(settings, _logger)
                                                                                                     End Sub)
    End Sub

    Private Sub Branches_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' Add placeholder text
        SetPlaceholder(TxtSearchBranches, "Start typing to search...")
        ' Attach placeholder handlers
        AddHandler TxtSearchBranches.Enter, AddressOf TextBox_Enter
        AddHandler TxtSearchBranches.Leave, AddressOf TextBox_Leave
        AddHandler TxtSearchBranches.TextChanged, AddressOf TxtSearchBranches_TextChanged
    End Sub
    Private Async Sub ButtonFetchBranches_Click(sender As Object, e As EventArgs) Handles ButtonFetchBranches.Click
        Try
            Loader.Visible = True
            ButtonFetchBranches.Enabled = False
            Loader.Text = "Fetching branches..."
            Dim request As New BranchListRequest With {
                .tin = Await _settingsManager.GetSettingAsync("pin"),
                .bhfId = Await _settingsManager.GetSettingAsync("branch_id"),
                .lastReqDt = Await _settingsManager.GetSettingAsync("last_branch_sync")
            }
            Dim response = Await _integrator.GetBranchListAsync(request)
            If response Is Nothing OrElse response.resultCd <> "000" Then
                Dim errMsg As String = "Failed to fetch Branches."
                If response IsNot Nothing AndAlso Not String.IsNullOrEmpty(response.resultMsg) Then
                    errMsg = response.resultMsg
                End If
                CustomAlert.ShowAlert(Me, errMsg, "Error", CustomAlert.AlertType.Error, CustomAlert.ButtonType.OK)
                Exit Sub
            End If
            If response.data Is Nothing OrElse response.data.bhfList Is Nothing OrElse response.data.bhfList.Count = 0 Then
                CustomAlert.ShowAlert(Me, "No new Branches found since last sync.", "Information", CustomAlert.AlertType.Info, CustomAlert.ButtonType.OK)
                Exit Sub
            End If
            Dim resultDate = ParseKraDate(response.resultDt)
            Await _branchRepo.SaveAsync(response.data.bhfList, resultDate)
            Dim dt As DataTable = Await _branchRepo.GetAllAsync()
            OriginalTables(DgvBranches) = dt.Copy()
            DgvBranches.DataSource = dt
            CustomAlert.ShowAlert(Me, "Branches fetched and saved successfully.", "Success", CustomAlert.AlertType.Success, CustomAlert.ButtonType.OK)
        Catch ex As Exception
            CustomAlert.ShowAlert(Me, "An error occurred: " & ex.Message, "Error", CustomAlert.AlertType.Error, CustomAlert.ButtonType.OK)
        Finally
            Loader.Visible = False
            ButtonFetchBranches.Enabled = True
        End Try
    End Sub

    Private Sub TxtSearchBranches_TextChanged(sender As Object, e As EventArgs)
        If TxtSearchBranches.ForeColor = Color.Gray Then Exit Sub
        FilterGrid(DgvBranches, TxtSearchBranches.Text)
    End Sub

    Private Sub FilterGrid(grid As DataGridView, search As String)
        If Not OriginalTables.ContainsKey(grid) Then Exit Sub
        Dim dt As DataTable = OriginalTables(grid)
        Dim dv As New DataView(dt)
        If String.IsNullOrWhiteSpace(search) Then
            dv.RowFilter = ""
        Else
            Dim safeSearch = search.Replace("'", "''")  ' prevent errors
            ' Build dynamic OR-based filter across all columns
            Dim filterExpression =
            String.Join(" OR ", dt.Columns.Cast(Of DataColumn)().Select(Function(c) $"Convert([{c.ColumnName}], 'System.String') LIKE '%{safeSearch}%'"))
            dv.RowFilter = filterExpression
        End If
        grid.DataSource = dv
    End Sub

    Private Sub SetPlaceholder(txt As TextBox, placeholder As String)
        If txt Is Nothing Then Exit Sub
        If String.IsNullOrEmpty(txt.Text) Then
            txt.ForeColor = Color.Gray
            txt.Text = placeholder
            txt.Tag = placeholder
        End If
    End Sub

    Private Sub TextBox_Enter(sender As Object, e As EventArgs)
        Dim txt = DirectCast(sender, TextBox)
        If txt.Text = CStr(txt.Tag) Then
            txt.Text = ""
            txt.ForeColor = Color.Black
        End If
    End Sub

    Private Sub TextBox_Leave(sender As Object, e As EventArgs)
        Dim txt = DirectCast(sender, TextBox)
        If txt.Text = "" Then
            txt.ForeColor = Color.Gray
            txt.Text = CStr(txt.Tag)
        End If
    End Sub

    Private Function ParseKraDate(value As String) As DateTime
        Return DateTime.ParseExact(value, "yyyyMMddHHmmss", Globalization.CultureInfo.InvariantCulture)
    End Function
End Class