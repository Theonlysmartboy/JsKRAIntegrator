Imports Core.Config
Imports Core.Logging
Imports Core.Models.Code
Imports Core.Models.Item.Classification
Imports Core.Repo
Imports Core.Repo.Item.Classification
Imports Core.Services

Public Class DataManagement
    Private _settingsManager As SettingsManager
    Private _integrator As VSCUIntegrator
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

    Private Sub DataManagementForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Add placeholder text
        SetPlaceholder(TxtSearchCodes, "Start typing to search...")
        SetPlaceholder(TxtSearchItemClass, "Start typing to search...")
        SetPlaceholder(TxtSearchNotices, "Start typing to search...")
        ' Attach placeholder handlers
        AddHandler TxtSearchCodes.Enter, AddressOf TextBox_Enter
        AddHandler TxtSearchCodes.Leave, AddressOf TextBox_Leave

        AddHandler TxtSearchItemClass.Enter, AddressOf TextBox_Enter
        AddHandler TxtSearchItemClass.Leave, AddressOf TextBox_Leave

        AddHandler TxtSearchNotices.Enter, AddressOf TextBox_Enter
        AddHandler TxtSearchNotices.Leave, AddressOf TextBox_Leave
    End Sub

    Private Sub TxtSearchCodes_TextChanged(sender As Object, e As EventArgs) Handles TxtSearchCodes.TextChanged
        If TxtSearchCodes.ForeColor = Color.Gray Then Exit Sub

        FilterGrid(DataGridViewCodes, TxtSearchCodes.Text)
    End Sub

    Private Sub TxtSearchItemClass_TextChanged(sender As Object, e As EventArgs) Handles TxtSearchItemClass.TextChanged
        If TxtSearchItemClass.ForeColor = Color.Gray Then Exit Sub

        FilterGrid(DataGridViewItemClassification, TxtSearchItemClass.Text)
    End Sub

    Private Sub TxtSearchNotices_TextChanged(sender As Object, e As EventArgs) Handles TxtSearchNotices.TextChanged
        If TxtSearchNotices.ForeColor = Color.Gray Then Exit Sub

        FilterGrid(DataGridViewNotices, TxtSearchNotices.Text)
    End Sub

    Private Async Sub ButtonSyncCodes_Click(sender As Object, e As EventArgs) Handles ButtonSyncCodes.Click
        Try
            ButtonSyncCodes.Enabled = False
            ButtonSyncCodes.Text = "Syncing..."

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
            ButtonSyncCodes.Enabled = True
            ButtonSyncCodes.Text = "Sync Codes"
        End Try
    End Sub

    Private Async Sub BtnLoadLocalCodes_Click(sender As Object, e As EventArgs) Handles BtnLoadLocalCodes.Click
        Await LoadCodesIntoGridAsync()
    End Sub

    Private Async Function LoadCodesIntoGridAsync() As Task
        Dim repo As New CodeLookupRepository(_conn)

        Dim dt = Await repo.LoadAllFlattenedAsync()
        OriginalTables(DataGridViewCodes) = dt
        DataGridViewCodes.DataSource = dt
    End Function
    Private Async Sub ButtonSyncItemClassification_Click(sender As Object, e As EventArgs) Handles ButtonSyncItemClassification.Click
        Try
            ButtonSyncItemClassification.Enabled = False
            ButtonSyncItemClassification.Text = "Syncing..."

            ' 1) Load required settings
            Dim tin = Await _settingsManager.GetSettingAsync("pin")
            Dim bhfId = Await _settingsManager.GetSettingAsync("branch_id")
            Dim lastReqDt = Await _settingsManager.GetSettingAsync("last_itemcls_sync")

            If String.IsNullOrWhiteSpace(lastReqDt) Then
                lastReqDt = "20180520000000"
            End If

            ' 2) Build request payload
            Dim req As New ItemClassificationRequest With {
            .tin = tin,
            .bhfId = bhfId,
            .lastReqDt = lastReqDt
        }

            ' 3) Call VSCU endpoint
            Dim resp = Await _integrator.SendItemClassificationInfoAsync(req)

            ' 1) Authority check
            If resp Is Nothing OrElse resp.resultCd <> "000" Then
                Dim msg As String = If(resp IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(resp.resultMsg), resp.resultMsg, "Integrator error")

                MessageBox.Show(msg, "Error")
                Return
            End If

            ' 2) Data availability check
            If resp.data Is Nothing OrElse resp.data.itemClsList Is Nothing OrElse resp.data.itemClsList.Count = 0 Then
                MessageBox.Show("No new Item classifications found.", "Information")
                Return
            End If

            ' 4) Flatten response → convert itemClsList → ItemClassificationEntry list
            Dim flatList As New List(Of ItemClassificationEntry)
            For Each cls In resp.data.itemClsList
                flatList.Add(New ItemClassificationEntry With {
        .Code = cls.itemClsCd,
        .Name = cls.itemClsNm,
        .Level = cls.itemClsLvl,
        .TaxTypeCode = If(cls.taxTyCd, ""),
        .MajorTag = If(cls.mjrTgYn, ""),
        .UseYn = If(cls.useYn, "N")
    })
            Next


            ' 5) Save to MySQL
            Dim repo As New ItemClassificationRepository(_conn)

            ' Optional: clear old classifications before insert
            Await repo.ClearAsync()

            For Each r In flatList
                Try
                    Await repo.SaveAsync(r)
                Catch ex As Exception
                    CustomAlert.ShowAlert(DataManagement.ActiveForm, $"Failed saving classification: {r.Code} - {r.Name}. Error: {ex.Message}",
                                          "Error", CustomAlert.AlertType.Error)
                End Try
            Next

            ' 6) Save new sync timestamp
            Await _settingsManager.SetSettingAsync("last_itemcls_sync", DateTime.Now.ToString("yyyyMMddHHmmss"))

            ' 7) Refresh Grid or UI
            Await LoadItemClassificationIntoGridAsync()

            CustomAlert.ShowAlert(DataManagement.ActiveForm, "Item classification data synced successfully!",
                                  "Success", CustomAlert.AlertType.Success)

        Catch ex As Exception
            CustomAlert.ShowAlert(DataManagement.ActiveForm, "Error during item classification sync: " & ex.Message,
                                  "Error", CustomAlert.AlertType.Error)
        Finally
            ButtonSyncItemClassification.Enabled = True
            ButtonSyncItemClassification.Text = "Sync Item Classification"
        End Try
    End Sub

    Private Async Sub BtnLoadStoredItemClassifications_Click(sender As Object, e As EventArgs) Handles BtnLoadStoredItemClassifications.Click
        Await LoadItemClassificationIntoGridAsync()
    End Sub

    Private Async Function LoadItemClassificationIntoGridAsync() As Task
        Dim repo As New ItemClassificationRepository(_conn)
        Dim dt = Await repo.LoadAllAsync()
        OriginalTables(DataGridViewItemClassification) = dt
        DataGridViewItemClassification.DataSource = dt
    End Function
    Private Sub ButtonFetchNotices_Click(sender As Object, e As EventArgs) Handles ButtonFetchNotices.Click

    End Sub
    Private Sub FilterGrid(grid As DataGridView, search As String)
        If Not OriginalTables.ContainsKey(grid) Then Exit Sub
        Dim dt As DataTable = OriginalTables(grid)
        Dim dv As DataView = dt.DefaultView
        If String.IsNullOrWhiteSpace(search) Then
            dv.RowFilter = ""
        Else
            Dim safeSearch = search.Replace("'", "''")
            Dim filter As String = String.Join(" OR ", dt.Columns.Cast(Of DataColumn).Select(
                        Function(c) $"Convert([{c.ColumnName}], 'System.String') LIKE '%{safeSearch}%'"))
            dv.RowFilter = filter
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

End Class