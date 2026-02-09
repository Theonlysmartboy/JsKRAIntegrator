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
                    CustomAlert.ShowAlert(Me, resp.resultMsg, "Error", CustomAlert.AlertType.Error, CustomAlert.ButtonType.OK)
                    LblStatus.Text = $"Code: {resp.resultCd}, Message: {resp.resultMsg}"
                    LblStatus.ForeColor = Color.Red
                ElseIf resp.data IsNot Nothing AndAlso resp.data.info IsNot Nothing Then
                    Dim info = resp.data.info
                    Await SaveInitInfoAsync(info)
                    CustomAlert.ShowAlert(Me, $"Code: {resp.resultCd}, Message: {resp.resultMsg} TIN: {info.tin}, Device: {info.dvcId}, Branch: {info.bhfNm}",
                                          "Success", CustomAlert.AlertType.Success, CustomAlert.ButtonType.OK)
                    LblStatus.Text = $"Code: {resp.resultCd}, Message: {resp.resultMsg} TIN: {info.tin}, Device: {info.dvcId}, Branch: {info.bhfNm}"
                    LblStatus.ForeColor = Color.Green
                Else
                    CustomAlert.ShowAlert(Me, $"Code: {resp.resultCd}, Message: {resp.resultMsg}", "Success", CustomAlert.AlertType.Info, CustomAlert.ButtonType.OK)
                    LblStatus.Text = $"Code: {resp.resultCd}, Message: {resp.resultMsg}"
                    LblStatus.ForeColor = Color.Green
                End If
            Else
                CustomAlert.ShowAlert(Me, "No response from VSCU", "Error", CustomAlert.AlertType.Error, CustomAlert.ButtonType.OK)
                LblStatus.Text = "No response from VSCU"
                LblStatus.ForeColor = Color.Red
            End If

        Catch ex As Exception
            CustomAlert.ShowAlert(Me, "Initialization failed: " & ex.Message, "Error", CustomAlert.AlertType.Error, CustomAlert.ButtonType.OK)
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
            Loader.Visible = True
            Loader.Text = "Syncing..."
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
            CustomAlert.ShowAlert(Me, "Codes synced successfully!", "Success", CustomAlert.AlertType.Success, CustomAlert.ButtonType.OK)
        Catch ex As Exception
            CustomAlert.ShowAlert(Me, "Sync Error: " & ex.Message, "Error", CustomAlert.AlertType.Error, CustomAlert.ButtonType.OK)
        Finally
            Loader.Visible = False
            Loader.Text = "Loading"
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

    Private Sub CountriesCodesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CountriesCodesToolStripMenuItem.Click
        Dim countriesForm As New DataManagement(_conn, 5)
        countriesForm.ShowDialog()
    End Sub

    Private Sub PaymentTypesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PaymentTypesToolStripMenuItem.Click
        Dim paymentMethodForm As New DataManagement(_conn, 7)
        paymentMethodForm.ShowDialog()
    End Sub

    Private Sub BranchStatusToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BranchStatusToolStripMenuItem.Click
        Dim branchForm As New DataManagement(_conn, 9)
        branchForm.ShowDialog()
    End Sub

    Private Sub UnitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UnitToolStripMenuItem.Click
        Dim uoqForm As New DataManagement(_conn, 10)
        uoqForm.ShowDialog()
    End Sub

    Private Sub TransactionProgressToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TransactionProgressToolStripMenuItem.Click
        Dim tpForm As New DataManagement(_conn, 11)
        tpForm.ShowDialog()
    End Sub

    Private Sub StockInOutTypeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StockInOutTypeToolStripMenuItem.Click
        Dim sInOutForm As New DataManagement(_conn, 12)
        sInOutForm.ShowDialog()
    End Sub

    Private Sub TransactionTypeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TransactionTypeToolStripMenuItem.Click
        Dim tTypeForm As New DataManagement(_conn, 14)
        tTypeForm.ShowDialog()
    End Sub

    Private Sub TaxpayerStatusToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TaxpayerStatusToolStripMenuItem.Click
        Dim tPayerSForm As New DataManagement(_conn, 15)
        tPayerSForm.ShowDialog()
    End Sub

    Private Sub PackagingUnitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PackagingUnitToolStripMenuItem.Click
        Dim puForm As New DataManagement(_conn, 17)
        puForm.ShowDialog()
    End Sub

    Private Sub ProductTypesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProductTypesToolStripMenuItem.Click
        Dim productTypeForm As New DataManagement(_conn, 24)
        productTypeForm.ShowDialog()
    End Sub

    Private Sub ImportItemStatusToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportItemStatusToolStripMenuItem.Click
        Dim importItemSForm As New DataManagement(_conn, 26)
        importItemSForm.ShowDialog()
    End Sub

    Private Sub RegistrationTypeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RegistrationTypeToolStripMenuItem.Click
        Dim rTypeForm As New DataManagement(_conn, 31)
        rTypeForm.ShowDialog()
    End Sub

    Private Sub CreditNoteReasonToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreditNoteReasonToolStripMenuItem.Click
        Dim cNoteReasonForm As New DataManagement(_conn, 32)
        cNoteReasonForm.ShowDialog()
    End Sub

    Private Sub CurrenciesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CurrenciesToolStripMenuItem.Click
        Dim currencyForm As New DataManagement(_conn, 33)
        currencyForm.ShowDialog()
    End Sub

    Private Sub PurchaseStatusToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PurchaseStatusToolStripMenuItem.Click
        Dim purchaseSForm As New DataManagement(_conn, 34)
        purchaseSForm.ShowDialog()
    End Sub

    Private Sub InventoryAdjustmentReasonToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InventoryAdjustmentReasonToolStripMenuItem.Click
        Dim invenAdjReasonForm As New DataManagement(_conn, 35)
        invenAdjReasonForm.ShowDialog()
    End Sub

    Private Sub BanksToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BanksToolStripMenuItem.Click
        Dim bankForm As New DataManagement(_conn, 36)
        bankForm.ShowDialog()
    End Sub

    Private Sub SalesReceiptTypeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalesReceiptTypeToolStripMenuItem.Click
        Dim salesTypeForm As New DataManagement(_conn, 37)
        salesTypeForm.ShowDialog()
    End Sub

    Private Sub PurchaseReceiptTypeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PurchaseReceiptTypeToolStripMenuItem.Click
        Dim purchaseReceiptTypeForm As New DataManagement(_conn, 38)
        purchaseReceiptTypeForm.ShowDialog()
    End Sub

    Private Sub TaxOfficesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TaxOfficesToolStripMenuItem.Click
        Dim taxOfficesForm As New DataManagement(_conn, 45)
        taxOfficesForm.ShowDialog()
    End Sub

    Private Sub LocalesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LocalesToolStripMenuItem.Click
        Dim localesForm As New DataManagement(_conn, 48)
        localesForm.ShowDialog()
    End Sub

    Private Sub CategoryLevelsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CategoryLevelsToolStripMenuItem.Click
        Dim catLevelsForm As New DataManagement(_conn, 49)
        catLevelsForm.ShowDialog()
    End Sub

    Private Sub BranchListToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BranchListToolStripMenuItem.Click
        Dim branchListForm As New Branches(_conn)
        branchListForm.ShowDialog()
    End Sub

    Private Sub CustomerBranchToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CustomerBranchToolStripMenuItem.Click
        Dim customerBranchForm As New Customers(_conn)
        customerBranchForm.ShowDialog()
    End Sub

    Private Sub UserAccountsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UserAccountsToolStripMenuItem.Click
        Dim userAccountsForm As New UserAccounts(_conn)
        userAccountsForm.ShowDialog()
    End Sub

    Private Sub InsurancesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InsurancesToolStripMenuItem.Click
        Dim insuranceForm As New InsuranceForm(_conn)
        insuranceForm.ShowDialog()
    End Sub
End Class