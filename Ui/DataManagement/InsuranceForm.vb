Imports Core.Config
Imports Core.Logging
Imports Core.Models.Branch.Insurance
Imports Core.Services
Imports Ui.Helpers
Imports Ui.Repo

Public Class InsuranceForm
    Private _integrator As VSCUIntegrator
    Private _conn As String
    Private _settingsManager As SettingsManager
    Private _logger As Logger
    Private _branchInsuranceRepo As IBranchInsuranceRepository

    Public Sub New(connectionString As String)
        InitializeComponent()
        _conn = connectionString
        _settingsManager = New SettingsManager(connectionString)
        Dim repo As New LogRepository(connectionString)
        _logger = New Logger(repo)
        _branchInsuranceRepo = New BranchInsuranceRepository(connectionString)
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

    Private Sub InsuranceForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ConfigureGrid()
        LoadGrid()
    End Sub

    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs) Handles BtnRefresh.Click
        LoadGrid()
    End Sub

    Private Async Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        Try
            SetLoading(True, "Saving insurance...")
            Dim newItems As New List(Of BranchInsurance)
            For Each row As DataGridViewRow In DgvInsuranceList.Rows
                If row.IsNewRow Then Continue For
                If row.Cells("IsSynced").Value Is Nothing Then
                    Dim insurance As New BranchInsurance With {
                        .InsuranceCode = row.Cells("InsuranceCode").Value?.ToString(),
                        .InsuranceName = row.Cells("InsuranceName").Value?.ToString(),
                        .InsuranceRate = Convert.ToDecimal(row.Cells("InsuranceRate").Value),
                        .UseYn = row.Cells("UseYn").Value?.ToString(),
                        .CreatedBy = "Admin"
                    }
                    newItems.Add(insurance)
                End If
            Next
            If newItems.Any() Then
                _branchInsuranceRepo.Save(newItems)
            End If
            Await SendUnsyncedToVSCU()
            LoadGrid()
            CustomAlert.ShowAlert(Me, "Insurance saved and synced successfully.", "Success", CustomAlert.AlertType.Success,
                                CustomAlert.ButtonType.OK)
        Catch ex As Exception
            CustomAlert.ShowAlert(Me, "Error while saving insurance: " & ex.Message, "Error", CustomAlert.AlertType.Error,
                                CustomAlert.ButtonType.OK)
        Finally
            SetLoading(False)
        End Try
    End Sub

    Private Async Sub BtnSend2VSCU_Click(sender As Object, e As EventArgs) Handles BtnSend2VSCU.Click
        Try
            SetLoading(True, "Sending to VSCU...")
            Dim selectedIds As New List(Of Integer)
            For Each row As DataGridViewRow In DgvInsuranceList.Rows
                If row.Cells("Select").Value IsNot Nothing AndAlso Convert.ToBoolean(row.Cells("Select").Value) Then
                    selectedIds.Add(CInt(row.Cells("Id").Value))
                End If
            Next
            If Not selectedIds.Any() Then
                CustomAlert.ShowAlert(Me, "Please select at least one insurance record.", "Information", CustomAlert.AlertType.Info,
                                    CustomAlert.ButtonType.OK)
                Exit Sub
            End If
            Await SendToVSCU(selectedIds)
            CustomAlert.ShowAlert(Me, "Selected insurance sent successfully.", "Success", CustomAlert.AlertType.Success,
                                CustomAlert.ButtonType.OK)
            LoadGrid()
        Catch ex As Exception
            CustomAlert.ShowAlert(Me, "Error sending insurance: " & ex.Message, "Error", CustomAlert.AlertType.Error,
                                CustomAlert.ButtonType.OK)
        Finally
            SetLoading(False)
        End Try
    End Sub

    'Helper methods
    Private Sub ConfigureGrid()
        DgvInsuranceList.AutoGenerateColumns = False
        DgvInsuranceList.AllowUserToAddRows = True
        DgvInsuranceList.Columns.Clear()
        ' Hidden ID column
        Dim colId As New DataGridViewTextBoxColumn()
        colId.Name = "Id"
        colId.HeaderText = "Id"
        colId.Visible = False
        DgvInsuranceList.Columns.Add(colId)
        ' Checkbox column
        Dim chk As New DataGridViewCheckBoxColumn()
        chk.Name = "Select"
        chk.HeaderText = "Select"
        DgvInsuranceList.Columns.Add(chk)
        DgvInsuranceList.Columns.Add("InsuranceCode", "Code")
        DgvInsuranceList.Columns.Add("InsuranceName", "Name")
        DgvInsuranceList.Columns.Add("InsuranceRate", "Rate (%)")
        DgvInsuranceList.Columns.Add("UseYn", "Active")
        DgvInsuranceList.Columns.Add("IsSynced", "Synced")
        DgvInsuranceList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub

    Private Sub LoadGrid()
        Dim data = _branchInsuranceRepo.GetAll()
        DgvInsuranceList.Rows.Clear()
        For Each Item As BranchInsurance In data
            DgvInsuranceList.Rows.Add(Item.Id,
                                    False,
                                    Item.InsuranceCode,
                                    Item.InsuranceName,
                                    Item.InsuranceRate,
                                    Item.UseYn,
                                    Item.IsSynced)
        Next
    End Sub

    Private Async Function SendUnsyncedToVSCU() As Task(Of Integer)
        Dim unsynced = _branchInsuranceRepo.GetUnsynced()
        Dim syncedIds As New List(Of Integer)
        For Each item As BranchInsurance In unsynced
            Dim request As New BranchInsuranceRequest With {
                .tin = Await _settingsManager.GetSettingAsync("pin"),
                .bhfId = Await _settingsManager.GetSettingAsync("branch_id"),
                .isrccCd = item.InsuranceCode,
                .isrccNm = item.InsuranceName,
                .isrcRt = item.InsuranceRate,
                .useYn = item.UseYn,
                .regrNm = "Admin",
                .regrId = "Admin",
                .modrNm = "Admin",
                .modrId = "Admin"
            }
            Dim response = Await _integrator.SaveBranchInsuranceAsync(request)
            If response IsNot Nothing AndAlso response.resultCd = "000" Then
                syncedIds.Add(item.Id)
            End If
        Next
        If syncedIds.Any() Then
            _branchInsuranceRepo.MarkAsSynced(syncedIds)
        End If
        Return syncedIds.Count
    End Function

    Private Async Function SendToVSCU(selectedIds As List(Of Integer)) As Task
        ' Filter insurances by selected IDs
        Dim insurances = _branchInsuranceRepo.GetAll().Where(Function(x) selectedIds.Contains(x.Id)).ToList()
        Dim syncedIds As New List(Of Integer)
        For Each insurance As Core.Models.Branch.Insurance.BranchInsurance In insurances
            Dim request As New BranchInsuranceRequest With {
                .tin = Await _settingsManager.GetSettingAsync("pin"),
                .bhfId = Await _settingsManager.GetSettingAsync("branch_id"),
                .isrccCd = insurance.InsuranceCode,
                .isrccNm = insurance.InsuranceName,
                .isrcRt = insurance.InsuranceRate,
                .useYn = insurance.UseYn,
                .regrNm = "Admin",
                .regrId = "Admin",
                .modrNm = "Admin",
                .modrId = "Admin"
            }
            Dim response = Await _integrator.SaveBranchInsuranceAsync(request)
            If response IsNot Nothing AndAlso response.resultCd = "000" Then
                syncedIds.Add(insurance.Id)
            End If
        Next
        If syncedIds.Any() Then
            _branchInsuranceRepo.MarkAsSynced(syncedIds)
        End If
    End Function

    Private Sub SetLoading(isLoading As Boolean, Optional message As String = "")
        Loader.Visible = isLoading
        Loader.Text = message
        BtnSave.Enabled = Not isLoading
        BtnSend2VSCU.Enabled = Not isLoading
        BtnRefresh.Enabled = Not isLoading
        DgvInsuranceList.Enabled = Not isLoading
    End Sub
End Class