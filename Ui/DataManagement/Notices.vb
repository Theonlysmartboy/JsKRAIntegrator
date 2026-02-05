Imports Core.Config
Imports Core.Logging
Imports Core.Models.Notice
Imports Core.Services
Imports Ui.Helpers
Imports Ui.Repo

Public Class Notices
    Private _settingsManager As SettingsManager
    Private _integrator As VSCUIntegrator
    Private ReadOnly _repo As INoticeRepository
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
        _repo = New NoticeRepository(connectionString)
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

    Private Sub Notices_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' Add placeholder text
        SetPlaceholder(TxtSearchNotices, "Start typing to search...")
        ' Attach placeholder handlers
        AddHandler TxtSearchNotices.Enter, AddressOf TextBox_Enter
        AddHandler TxtSearchNotices.Leave, AddressOf TextBox_Leave
        AddHandler TxtSearchNotices.TextChanged, AddressOf TxtSearchNotices_TextChanged
    End Sub

    Private Async Sub ButtonFetchNotices_Click(sender As Object, e As EventArgs) Handles ButtonFetchNotices.Click
        Loader.Visible = True
        Dim request As New NoticeRequest With {
            .tin = Await _settingsManager.GetSettingAsync("pin"),
            .bhfId = Await _settingsManager.GetSettingAsync("branch_id"),
            .lastReqDt = Await _settingsManager.GetSettingAsync("last_notice_sync")
        }
        Dim response = Await _integrator.GetNoticesAsync(request)
        If response.resultCd <> "000" Then
            MessageBox.Show(response.resultMsg)
            Exit Sub
        End If
        ' Map API model → Entity
        Dim entities = response.data.noticeList.Select(Function(n) New VscuNotice With {
                                                    .NoticeNo = n.noticeNo,
                                                    .Title = n.title,
                                                    .Content = n.cont,
                                                    .DetailUrl = n.dtlUrl,
                                                    .RegisteredBy = n.regrNm,
                                                    .RegDt = ParseKraDate(n.regDt),
                                                    .ResultDt = ParseKraDate(response.resultDt)
                                                }).ToList()
        Await _repo.SaveAsync(entities)
        Dim dt = Await _repo.GetAllAsync()
        OriginalTables(DgvNotices) = dt.copy()
        DgvNotices.DataSource = dt
        DgvNotices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Await _settingsManager.SetSettingAsync("last_notice_sync", DateTime.Now.ToString("yyyyMMddHHmmss"))
        Loader.Visible = False
        CustomAlert.ShowAlert(Me, "Notices synced successfully.", "Success", CustomAlert.AlertType.Success, CustomAlert.ButtonType.OK)

    End Sub

    Private Sub TxtSearchNotices_TextChanged(sender As Object, e As EventArgs)
        If TxtSearchNotices.ForeColor = Color.Gray Then Exit Sub
        FilterGrid(DgvNotices, TxtSearchNotices.Text)
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