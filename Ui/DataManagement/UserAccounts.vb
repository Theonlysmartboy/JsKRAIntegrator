Imports Core.Config
Imports Core.Helpers.Cryptography
Imports Core.Logging
Imports Core.Models.Branch.User
Imports Core.Services
Imports Ui.Helpers
Imports Ui.Repo.BranchRepo

Public Class UserAccounts
    Private _settingsManager As SettingsManager
    Private _integrator As VSCUIntegrator
    Private ReadOnly _branchUserRepo As IBranchUserRepository
    Private ReadOnly _branchRepo As IBranchRepository
    Private _logger As Logger
    Private _conn As String

    Public Sub New(connectionString As String)
        InitializeComponent()
        _conn = connectionString
        ' Initialize settings manager
        _settingsManager = New SettingsManager(connectionString)
        ' Initialize logger
        Dim repo As New LogRepository(connectionString)
        _logger = New Logger(repo)
        _branchUserRepo = New BranchUserRepository(connectionString)
        _branchRepo = New BranchRepository(connectionString)
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

    Private Async Sub UserAccounts_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await LoadBranchesAsync()
    End Sub

    Private Sub CmbBranches_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbBranches.SelectedIndexChanged
        If CmbBranches.SelectedIndex <= 0 Then
            ' Default selection
            txtTin.Text = ""
            txtBhfId.Text = ""
            Return
        End If
        ' Get selected DataRow
        Dim drv As DataRowView = TryCast(CmbBranches.SelectedItem, DataRowView)
        If drv IsNot Nothing Then
            txtTin.Text = drv("tin").ToString()
            txtBhfId.Text = drv("bhf_id").ToString()
        End If
    End Sub

    Private Async Sub btnSaveUser_Click(sender As Object, e As EventArgs) Handles BtnSaveUser.Click
        Try
            Loader.Visible = True
            Loader.Text = "Saving user..."
            BtnSaveUser.Enabled = False
            If String.IsNullOrWhiteSpace(txtTin.Text) OrElse String.IsNullOrWhiteSpace(txtBhfId.Text) OrElse
               String.IsNullOrWhiteSpace(txtUserId.Text) OrElse String.IsNullOrWhiteSpace(txtUserName.Text) OrElse
               String.IsNullOrWhiteSpace(txtPassword.Text) Then
                CustomAlert.ShowAlert(Me, "Please select Branch and fill in all required fields (User ID, User Name, Password).", "Validation Error", CustomAlert.AlertType.Warning, CustomAlert.ButtonType.OK)
                Exit Sub
            End If
            Dim validationMsg = (New PasswordValidator()).ValidatePassword(txtPassword.Text.Trim())
            If validationMsg IsNot Nothing Then
                CustomAlert.ShowAlert(Me, validationMsg, "Validation Error", CustomAlert.AlertType.Warning, CustomAlert.ButtonType.OK)
                Exit Sub
            End If
            Dim existingUser = Await _branchUserRepo.GetByUserIdAsync(txtUserId.Text.Trim())
            If existingUser IsNot Nothing Then
                CustomAlert.ShowAlert(Me, "A user with this User ID already exists locally. Please choose a different User ID.", "Validation Error", CustomAlert.AlertType.Warning, CustomAlert.ButtonType.OK)
                Exit Sub
            End If
            Dim passwordHash = PasswordHasher.HashPassword(txtPassword.Text.Trim())
            Dim req As New BranchUserSaveRequest With {
                .tin = txtTin.Text.Trim(),
                .bhfId = txtBhfId.Text.Trim(),
                .userId = txtUserId.Text.Trim(),
                .userNm = txtUserName.Text.Trim(),
                .pwd = passwordHash,
                .adrs = If(String.IsNullOrWhiteSpace(TxtAddress.Text), Nothing, TxtAddress.Text.Trim),
                .cntc = If(String.IsNullOrWhiteSpace(TxtContact.Text), Nothing, TxtContact.Text.Trim),
                .authCd = Nothing,
                .remark = If(String.IsNullOrWhiteSpace(RtxtRemark.Text), Nothing, RtxtRemark.Text.Trim),
                .useYn = "Y",
                .regrNm = "Admin",
                .regrId = "Admin",
                .modrNm = "Admin",
                .modrId = "Admin"
            }
            Dim resp = Await _integrator.SaveBranchUserAsync(req)
            If resp Is Nothing OrElse resp.resultCd <> "000" Then
                CustomAlert.ShowAlert(Me, resp?.resultMsg, "Error", CustomAlert.AlertType.Error, CustomAlert.ButtonType.OK)
                Return
            End If
            ' Convert request → DB model
            Dim localUser As New BranchUser With {
                .Tin = req.tin,
                .BhfId = req.bhfId,
                .UserId = req.userId,
                .UserNm = req.userNm,
                .Pwd = req.pwd,
                .Adrs = req.adrs,
                .Cntc = req.cntc,
                .AuthCd = req.authCd,
                .Remark = req.remark,
                .UseYn = req.useYn,
                .RegrNm = req.regrNm,
                .RegrId = req.regrId,
                .ModrNm = req.modrNm,
                .ModrId = req.modrId,
                .ResultDt = ParseKraDate(resp.resultDt)
            }
            Await _branchUserRepo.SaveAsync(localUser)
            CustomAlert.ShowAlert(Me, "Branch user saved locally and in VSCU.", "Success", CustomAlert.AlertType.Success, CustomAlert.ButtonType.OK)
        Catch ex As Exception
            CustomAlert.ShowAlert(Me, "An error occured while saving user" & ex.Message, "Error", CustomAlert.AlertType.Error, CustomAlert.ButtonType.OK)
        Finally
            clearForm()
            Loader.Visible = False
            Loader.Text = "Loading..."
            BtnSaveUser.Enabled = True
        End Try
    End Sub

    'Helper functions
    Private Async Function LoadBranchesAsync() As Task
        Dim dt As DataTable = Await _branchRepo.GetAllAsync()
        ' Add default row
        Dim defaultRow = dt.NewRow()
        defaultRow("bhf_id") = ""
        defaultRow("bhf_nm") = "-- Select Branch --"
        defaultRow("tin") = ""
        dt.Rows.InsertAt(defaultRow, 0)
        CmbBranches.DataSource = dt
        CmbBranches.DisplayMember = "bhf_nm"
        CmbBranches.ValueMember = "bhf_id"
    End Function

    Private Function ParseKraDate(value As String) As DateTime
        Return DateTime.ParseExact(value, "yyyyMMddHHmmss", Globalization.CultureInfo.InvariantCulture)
    End Function

    Private Sub clearForm()
        CmbBranches.SelectedIndex = 0
        txtTin.Text = ""
        txtBhfId.Text = ""
        txtUserId.Text = ""
        txtUserName.Text = ""
        txtPassword.Text = ""
        TxtAddress.Text = ""
        TxtContact.Text = ""
        RtxtRemark.Text = ""
    End Sub
End Class