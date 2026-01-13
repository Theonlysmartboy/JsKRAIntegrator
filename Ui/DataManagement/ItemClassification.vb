Imports Core.Config
Imports Core.Logging
Imports Core.Models.Item.Classification
Imports Core.Services
Imports Ui.Repo.Item.Classification

Public Class ItemClassification
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
    Private Async Sub ItemClassification_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetPlaceholder(TxtSearchItemClass, "Start typing to search...")
        AddHandler TxtSearchItemClass.Enter, AddressOf TextBox_Enter
        AddHandler TxtSearchItemClass.Leave, AddressOf TextBox_Leave
        Await LoadItemClassificationIntoGridAsync()
    End Sub
    Private Sub TxtSearchItemClass_TextChanged(sender As Object, e As EventArgs)
        If TxtSearchItemClass.ForeColor = Color.Gray Then Exit Sub

        FilterGrid(DataGridViewItemClassification, TxtSearchItemClass.Text)
    End Sub
    Private Async Sub BtnSyncItemClassification_Click(sender As Object, e As EventArgs) Handles BtnSyncItemClassification.Click
        Try
            BtnSyncItemClassification.Enabled = False
            BtnSyncItemClassification.Text = "Syncing..."

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

                CustomAlert.ShowAlert(Me, "Failed to sync item classifications: " & msg, "Error", CustomAlert.AlertType.Error)
                Return
            End If

            ' 2) Data availability check
            If resp.data Is Nothing OrElse resp.data.itemClsList Is Nothing OrElse resp.data.itemClsList.Count = 0 Then
                CustomAlert.ShowAlert(Me, "No new item classifications found.", "Information", CustomAlert.AlertType.Info)
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
                    CustomAlert.ShowAlert(Me, $"Failed saving classification: {r.Code} - {r.Name}. Error: {ex.Message}", "Error", CustomAlert.AlertType.Error)
                End Try
            Next

            ' 6) Save new sync timestamp
            Await _settingsManager.SetSettingAsync("last_itemcls_sync", DateTime.Now.ToString("yyyyMMddHHmmss"))

            ' 7) Refresh Grid or UI
            Await LoadItemClassificationIntoGridAsync()

            CustomAlert.ShowAlert(Me, "Item classification data synced successfully!", "Success", CustomAlert.AlertType.Success)

        Catch ex As Exception
            CustomAlert.ShowAlert(Me, "Error during item classification sync: " & ex.Message, "Error", CustomAlert.AlertType.Error)
        Finally
            BtnSyncItemClassification.Enabled = True
            BtnSyncItemClassification.Text = "Sync Item Classification"
        End Try
    End Sub

    'Helpers
    Private Async Function LoadItemClassificationIntoGridAsync() As Task
        Dim repo As New ItemClassificationRepository(_conn)
        Dim dt = Await repo.LoadAllAsync()
        OriginalTables(DataGridViewItemClassification) = dt
        DataGridViewItemClassification.DataSource = dt
    End Function
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