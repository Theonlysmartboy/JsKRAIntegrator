Imports Core.Config
Imports Core.Logging
Imports Core.Models.Branch.Customer
Imports Core.Services
Imports Ui.Helpers
Imports Ui.Repo

Public Class Customers
    Private _settingsManager As SettingsManager
    Private _integrator As VSCUIntegrator
    Private ReadOnly _customerRepo As ICustomerRepository
    Private _logger As Logger
    Private _conn As String
    Private OriginalTables As New Dictionary(Of DataGridView, DataTable)

    Public Sub New(connectionString As String)
        InitializeComponent()
        _conn = connectionString
        _settingsManager = New SettingsManager(connectionString)
        Dim repo As New LogRepository(connectionString)
        _logger = New Logger(repo)
        _customerRepo = New CustomerRepository(connectionString)
        ' Build IntegratorSettings from DB
        Dim baseUrlTask = _settingsManager.GetSettingAsync("base_url")
        Dim pinTask = _settingsManager.GetSettingAsync("pin")
        Dim branchIdTask = _settingsManager.GetSettingAsync("branch_id")
        Dim deviceSerialTask = _settingsManager.GetSettingAsync("device_serial")
        Dim timeoutTask = _settingsManager.GetSettingAsync("timeout")
        Task.WhenAll(baseUrlTask, pinTask, branchIdTask, deviceSerialTask,
                     timeoutTask).ContinueWith(Sub(t)
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

    Private Sub Customers_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetPlaceholder(TxtSearchCustomerList, "Start typing to search...")
        AddHandler TxtSearchCustomerList.Enter, AddressOf TextBox_Enter
        AddHandler TxtSearchCustomerList.Leave, AddressOf TextBox_Leave
        AddHandler TxtSearchCustomerList.TextChanged, AddressOf TxtSearchCustomerList_TextChanged
    End Sub

    Private Async Sub ButtonFetchCustomers_Click(sender As Object, e As EventArgs) Handles btnQueryCustomers.Click
        Try
            Loader.Visible = True
            Loader.Text = "Fetching customers..."
            btnQueryCustomers.Enabled = False
            ' Basic validation
            If String.IsNullOrWhiteSpace(TxtCustomerTIN.Text) Then
                CustomAlert.ShowAlert(Me, "Please enter Customer TIN to fetch records.", "Validation", CustomAlert.AlertType.Warning, CustomAlert.ButtonType.OK)
                Exit Sub
            End If
            Dim req As New CustomerRequest With {
                .tin = Await _settingsManager.GetSettingAsync("pin"),
                .bhfId = Await _settingsManager.GetSettingAsync("branch_id"),
                .custmTin = TxtCustomerTIN.Text.Trim()
            }
            Dim response = Await _integrator.GetCustomerListAsync(req)
            If response Is Nothing OrElse response.resultCd <> "000" Then
                Dim msg = If(response IsNot Nothing AndAlso Not String.IsNullOrEmpty(response.resultMsg), response.resultMsg,
                    "Failed to fetch customer records.")
                CustomAlert.ShowAlert(Me, msg, "Error", CustomAlert.AlertType.Error, CustomAlert.ButtonType.OK)
                Exit Sub
            End If
            If response.data Is Nothing OrElse response.data.custList Is Nothing OrElse response.data.custList.Count = 0 Then
                CustomAlert.ShowAlert(Me, "Customer Records Not found.", "Information", CustomAlert.AlertType.Info, CustomAlert.ButtonType.OK)
                Exit Sub
            End If
            Dim resultDate = ParseKraDate(response.resultDt)
            Await _customerRepo.SaveAsync(response.data.custList)
            Dim dt As DataTable = Await _customerRepo.GetAllAsync()
            OriginalTables(DgvCustomers) = dt.Copy()
            DgvCustomers.DataSource = dt
            CustomAlert.ShowAlert(Me, "Customers fetched and saved successfully.", "Success", CustomAlert.AlertType.Success, CustomAlert.ButtonType.OK)
        Catch ex As Exception
            CustomAlert.ShowAlert(Me, "An error occurred: " & ex.Message, "Error", CustomAlert.AlertType.Error, CustomAlert.ButtonType.OK)
        Finally
            Loader.Visible = False
            btnQueryCustomers.Enabled = True
        End Try
    End Sub

    Private Async Sub BtnSaveCustomer_Click(sender As Object, e As EventArgs) Handles BtnSaveCustomer.Click
        Try
            DualRingLoader1.Visible = True
            BtnSaveCustomer.Enabled = False
            Loader.Text = "Sending to VSCU..."
            ' Basic validation
            If String.IsNullOrWhiteSpace(TxtCustNo.Text) OrElse String.IsNullOrWhiteSpace(TxtCustTin.Text) OrElse String.IsNullOrWhiteSpace(TxtCustName.Text) Then
                CustomAlert.ShowAlert(Me, "Customer Number, TIN and Name are required.", "Validation", CustomAlert.AlertType.Warning, CustomAlert.ButtonType.OK)
                Exit Sub
            End If
            Dim request As New SaveBranchCustomerRequest With {
                .tin = Await _settingsManager.GetSettingAsync("pin"),
                .bhfId = Await _settingsManager.GetSettingAsync("branch_id"),
                .custNo = TxtCustNo.Text.Trim(),
                .custTin = TxtCustTin.Text.Trim(),
                .custNm = TxtCustName.Text.Trim(),
                .adrs = If(String.IsNullOrWhiteSpace(TxtAddress.Text), Nothing, TxtAddress.Text.Trim()),
                .telNo = If(String.IsNullOrWhiteSpace(TxtTel.Text), Nothing, TxtTel.Text.Trim()),
                .email = If(String.IsNullOrWhiteSpace(TxtEmail.Text), Nothing, TxtEmail.Text.Trim()),
                .faxNo = If(String.IsNullOrWhiteSpace(TxtFax.Text), Nothing, TxtFax.Text.Trim()),
                .useYn = "Y",
                .remark = If(String.IsNullOrWhiteSpace(RtxtRemark.Text), Nothing, RtxtRemark.Text.Trim()),
                .regrNm = "Admin",
                .regrId = "Admin",
                .modrNm = "Admin",
                .modrId = "Admin"
            }
            Dim response = Await _integrator.SaveBranchCustomerAsync(request)
            If response Is Nothing OrElse response.resultCd <> "000" Then
                Dim errMsg As String = "Failed to save customer."
                If response IsNot Nothing AndAlso Not String.IsNullOrEmpty(response.resultMsg) Then
                    errMsg = response.resultMsg
                End If
                CustomAlert.ShowAlert(Me, errMsg, "Error", CustomAlert.AlertType.Error, CustomAlert.ButtonType.OK)
                Exit Sub
            End If
            CustomAlert.ShowAlert(Me, "Customer successfully saved to VSCU.", "Success", CustomAlert.AlertType.Success, CustomAlert.ButtonType.OK)
            ClearForm()
        Catch ex As Exception
            CustomAlert.ShowAlert(Me, "Unexpected error: " & ex.Message, "System Error", CustomAlert.AlertType.Error, CustomAlert.ButtonType.OK)
        Finally
            DualRingLoader1.Visible = False
            BtnSaveCustomer.Enabled = True
        End Try
    End Sub

    'Helper Methods
    Private Sub TxtSearchCustomerList_TextChanged(sender As Object, e As EventArgs)
        If TxtSearchCustomerList.ForeColor = Color.Gray Then Exit Sub
        FilterGrid(DgvCustomers, TxtSearchCustomerList.Text)
    End Sub

    Private Sub FilterGrid(grid As DataGridView, search As String)
        If Not OriginalTables.ContainsKey(grid) Then Exit Sub
        Dim dt As DataTable = OriginalTables(grid)
        Dim dv As New DataView(dt)
        If String.IsNullOrWhiteSpace(search) Then
            dv.RowFilter = ""
        Else
            Dim safeSearch = search.Replace("'", "''")
            dv.RowFilter = String.Join(" OR ", dt.Columns.Cast(Of DataColumn)().Select(Function(c) $"Convert([{c.ColumnName}], 'System.String') LIKE '%{safeSearch}%'"))
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

    Private Sub ClearForm()
        TxtCustNo.Clear()
        TxtCustTin.Clear()
        TxtCustName.Clear()
        TxtAddress.Clear()
        TxtTel.Clear()
        TxtEmail.Clear()
    End Sub
End Class
