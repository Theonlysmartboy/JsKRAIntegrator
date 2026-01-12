Imports Core.Config
Imports Core.Logging
Imports Core.Main
Imports Core.Models.Code
Imports Core.Models.Init
Imports Core.Services
Imports Ui.Helpers
Imports Ui.Repo

Public Class HomeForm

    Private _integrator As VSCUIntegrator
    Private ReadOnly _logRepo As LogRepository
    Private _settingsManager As SettingsManager
    Private _logger As Logger
    ' <-- Class-level connection string
    Private _conn As String

    Public Sub New(integrator As VSCUIntegrator, logRepo As LogRepository)
        InitializeComponent()
        _integrator = integrator
        _logRepo = logRepo

        ' Initialize connection string once
        _conn = DatabaseHelper.GetConnectionString()
        If String.IsNullOrEmpty(_conn) Then
            ' Optionally show settings form immediately if connection missing
            Dim settings As New Settings()
            settings.ShowDialog()
            _conn = DatabaseHelper.GetConnectionString()
        End If
        ' Initialize settings manager
        _settingsManager = New SettingsManager(_conn)

        ' Initialize logger
        _logger = New Logger(logRepo)

        ' Build IntegratorSettings from DB
        Dim baseUrlTask = _settingsManager.GetSettingAsync("base_url")
        Dim pinTask = _settingsManager.GetSettingAsync("pin")
        Dim branchIdTask = _settingsManager.GetSettingAsync("branch_id")
        Dim deviceSerialTask = _settingsManager.GetSettingAsync("device_serial")
        Dim timeoutTask = _settingsManager.GetSettingAsync("timeout")
    End Sub

    Public Async Function InitializeAsync() As Task
        Dim baseUrl = Await _settingsManager.GetSettingAsync("base_url")
        Dim pin = Await _settingsManager.GetSettingAsync("pin")
        Dim branch = Await _settingsManager.GetSettingAsync("branch_id")
        Dim deviceSerial = Await _settingsManager.GetSettingAsync("device_serial")
        Dim timeout = Await _settingsManager.GetSettingAsync("timeout")

        Dim settings As New IntegratorSettings With {
        .BaseUrl = baseUrl,
        .Pin = pin,
        .BranchId = branch,
        .DeviceSerial = deviceSerial,
        .Timeout = Integer.Parse(timeout)
    }

        _integrator = New VSCUIntegrator(settings, _logger)
    End Function

    Private Sub ToolStripSettings_Click(sender As Object, e As EventArgs) Handles ToolStripSettings.Click
        If String.IsNullOrEmpty(_conn) Then Return
        Dim settings As New Settings(_conn)
        settings.ShowDialog()
    End Sub

    Private Sub ToolStripLogs_Click(sender As Object, e As EventArgs) Handles ToolStripLogs.Click
        Dim logs As New Logs(_logRepo)
        logs.ShowDialog()
    End Sub

    Private Async Sub InitializeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InitializeToolStripMenuItem.Click
        InitializationToolStripMenuItem.Enabled = False
        InitializeToolStripMenuItem.Enabled = False
        LblStatus.Text = "Initializing device..."
        Try
            ' Build the request from DB
            Dim tin = Await _settingsManager.GetSettingAsync("pin")
            Dim bhfId = Await _settingsManager.GetSettingAsync("branch_id")
            Dim dvcSrlNo = Await _settingsManager.GetSettingAsync("device_serial")

            Dim req As New InitInfoRequest With {
                .tin = tin,
                .bhfId = bhfId,
                .dvcSrlNo = dvcSrlNo
            }

            ' Call VSCU endpoint
            Dim resp = Await _integrator.InitializeAsync(req)

            If resp IsNot Nothing Then
                If resp.resultCd = "ERROR" Then
                    CustomAlert.ShowAlert(HomeForm.ActiveForm,
                                          resp.resultMsg,
                                          "Error", CustomAlert.AlertType.Error)
                    LblStatus.Text = $"Code: {resp.resultCd}, Message: {resp.resultMsg}"
                    LblStatus.ForeColor = Color.Red
                ElseIf resp.data IsNot Nothing AndAlso resp.data.info IsNot Nothing Then
                    Dim info = resp.data.info
                    Await SaveInitInfoAsync(info)
                    CustomAlert.ShowAlert(HomeForm.ActiveForm,
                                          $"Code: {resp.resultCd}, Message: {resp.resultMsg} TIN: {info.tin}, Device: {info.dvcId}, Branch: {info.bhfNm}",
                                          "Success", CustomAlert.AlertType.Success)
                    LblStatus.Text = $"Code: {resp.resultCd}, Message: {resp.resultMsg} TIN: {info.tin}, Device: {info.dvcId}, Branch: {info.bhfNm}"
                    LblStatus.ForeColor = Color.Green
                Else
                    CustomAlert.ShowAlert(HomeForm.ActiveForm,
                                          $"Code: {resp.resultCd}, Message: {resp.resultMsg}",
                                          "Success", CustomAlert.AlertType.Info)
                    LblStatus.Text = $"Code: {resp.resultCd}, Message: {resp.resultMsg}"
                    LblStatus.ForeColor = Color.Green
                End If
            Else
                CustomAlert.ShowAlert(HomeForm.ActiveForm,
                                      "No response from VSCU",
                                      "Error", CustomAlert.AlertType.Error)
                LblStatus.Text = "No response from VSCU"
                LblStatus.ForeColor = Color.Red
            End If

        Catch ex As Exception
            CustomAlert.ShowAlert(HomeForm.ActiveForm,
                                  "Initialization failed: " & ex.Message,
                                  "Error", CustomAlert.AlertType.Error)
            LblStatus.Text = $"Initialization failed: {ex.Message}"
            LblStatus.ForeColor = Color.Red
        Finally
            InitializationToolStripMenuItem.Enabled = True
            InitializeToolStripMenuItem.Enabled = True
        End Try
    End Sub

    Public Async Function SaveInitInfoAsync(info As InitInfo) As Task
        Await _settingsManager.SetSettingAsync("taxpr_name", info.taxprNm)
        Await _settingsManager.SetSettingAsync("business_activity", info.bsnsActv)
        Await _settingsManager.SetSettingAsync("branch_id", info.bhfId)
        Await _settingsManager.SetSettingAsync("branch_name", info.bhfNm)
        Await _settingsManager.SetSettingAsync("branch_open_date", info.bhfOpenDt)
        Await _settingsManager.SetSettingAsync("province_name", info.prvncNm)
        Await _settingsManager.SetSettingAsync("district_name", info.dstrtNm)
        Await _settingsManager.SetSettingAsync("sector_name", info.sctrNm)
        Await _settingsManager.SetSettingAsync("location_description", If(info.locDesc, ""))

        Await _settingsManager.SetSettingAsync("hq", info.hqYn)
        Await _settingsManager.SetSettingAsync("manager_name", info.mgrNm)
        Await _settingsManager.SetSettingAsync("manager_phone", info.mgrTelNo)
        Await _settingsManager.SetSettingAsync("manager_email", info.mgrEmail)

        Await _settingsManager.SetSettingAsync("device_id", info.dvcId)
        Await _settingsManager.SetSettingAsync("vat_type", info.vatTyCd)
    End Function

    Private Sub ProductManagementToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProductManagementToolStripMenuItem.Click
        Dim productsForm As New ProductManagement(_conn)
        productsForm.ShowDialog()
    End Sub

    Private Sub SalesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalesToolStripMenuItem.Click
        Dim salesForm As New Sales(_conn)
        salesForm.ShowDialog()
    End Sub

    Private Sub PurchasesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PurchasesToolStripMenuItem.Click
        Dim purchasesForm As New Purchases(_conn)
        purchasesForm.ShowDialog()
    End Sub

    Private Sub Main_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Dim starter As New VscuStarter(_conn)
        starter.StopVscuByPort(8088)
    End Sub

    Private Async Sub SyncToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SyncToolStripMenuItem.Click
        Try
            SyncToolStripMenuItem.Enabled = False
            SyncToolStripMenuItem.Text = "Syncing..."
            ' 1) Load required settings
            Dim tin = Await _settingsManager.GetSettingAsync("pin")
            Dim bhfId = Await _settingsManager.GetSettingAsync("branch_id")
            Dim lastReqDt = Await _settingsManager.GetSettingAsync("last_code_sync")
            If String.IsNullOrWhiteSpace(lastReqDt) Then
                lastReqDt = "20000000000000"
            End If
            ' 2) Build request payload
            Dim req As New CodeDataRequest With {
            .tin = tin,
            .bhfId = bhfId,
            .lastReqDt = lastReqDt
        }
            ' 3) Call VSCU endpoint
            Dim resp = Await _integrator.GetCodeDataAsync(req)
            ' 3.1) Authority check
            If resp Is Nothing OrElse resp.resultCd <> "000" Then
                Dim msg As String = If(resp IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(resp.resultMsg), resp.resultMsg, "Integrator error")
                MessageBox.Show(msg, "Error")
                Return
            End If
            ' 3.2) Data availability check
            If resp.data Is Nothing OrElse resp.data.clsList Is Nothing OrElse resp.data.clsList.Count = 0 Then
                MessageBox.Show("No new codes returned.", "Information")
                Return
            End If
            Dim mappedRepo As New CodeMappedRepository(_conn)
            For Each cls In resp.data.clsList
                For Each dt In cls.dtlList
                    Await mappedRepo.SaveAsync(cls, dt)
                Next
            Next
            Await _settingsManager.SetSettingAsync("last_code_sync", DateTime.Now.ToString("yyyyMMddHHmmss"))
            CustomAlert.ShowAlert(Me, "Codes synced successfully!", "Success", CustomAlert.AlertType.Success)
        Catch ex As Exception
            CustomAlert.ShowAlert(Me, "Sync Error: " & ex.Message, "Error", CustomAlert.AlertType.Error)
        Finally
            SyncToolStripMenuItem.Enabled = True
            SyncToolStripMenuItem.Text = "Sync Codes"
        End Try
    End Sub

    Private Sub ItemClassificationsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ItemClassificationsToolStripMenuItem.Click
        Dim itemClassForm As New ItemClassification(_conn)
        itemClassForm.ShowDialog()
    End Sub

    Private Sub NoticesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NoticesToolStripMenuItem.Click
        Dim noticeForm As New Notices(_conn)
        noticeForm.ShowDialog()
    End Sub

    Private Sub TaxTypesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TaxTypesToolStripMenuItem.Click
        Dim taxTypeForm As New DataManagement(_conn, 4)
        taxTypeForm.ShowDialog()
    End Sub
End Class