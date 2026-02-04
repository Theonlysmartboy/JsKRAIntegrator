Imports Core.Config
Imports Core.Logging
Imports Core.Models.Notice
Imports Core.Services
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

    Private Async Sub ButtonFetchNotices_Click(sender As Object, e As EventArgs) Handles ButtonFetchNotices.Click


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

        DataGridViewNotices.DataSource = Await _repo.GetAllAsync()

        MessageBox.Show("Notices synced successfully.")
    End Sub

    Private Function ParseKraDate(value As String) As DateTime
        Return DateTime.ParseExact(
        value,
        "yyyyMMddHHmmss",
        Globalization.CultureInfo.InvariantCulture
    )
    End Function

End Class