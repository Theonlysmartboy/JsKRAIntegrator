Imports Core.Config
Imports Core.Enums
Imports Core.Logging
Imports Core.Models.Item.Composition
Imports Core.Models.Item.Import
Imports Core.Models.Item.Info
Imports Core.Models.Item.Product
Imports Core.Services
Imports Ui.Helpers
Imports Ui.Repo.BranchRepo
Imports Ui.Repo.ItemRepo
Imports Ui.Repo.ProductRepo

Public Class ProductManagement
    Private _settingsManager As SettingsManager
    Private _integrator As VSCUIntegrator
    Private _logger As Logger
    Private _connString As String
    Private _branchImportRepo As IBranchImportItemRepository
    Private ReadOnly _branchRepo As IBranchRepository
    Private OriginalTables As New Dictionary(Of DataGridView, DataTable)
    Private headerCheckBox As New CheckBox()

    Public Sub New(connectionString As String)
        InitializeComponent()
        _connString = connectionString
        _settingsManager = New SettingsManager(connectionString)
        Dim repo As New LogRepository(_connString)
        _logger = New Logger(repo)
        _branchImportRepo = New BranchImportItemRepository(_connString)
        _branchRepo = New BranchRepository(connectionString)
        Task.WhenAll(
            _settingsManager.GetSettingAsync("base_url"),
            _settingsManager.GetSettingAsync("pin"),
            _settingsManager.GetSettingAsync("branch_id"),
            _settingsManager.GetSettingAsync("device_serial"),
            _settingsManager.GetSettingAsync("timeout")).ContinueWith(Sub(t)
                                                                          _integrator = New VSCUIntegrator(New IntegratorSettings With {
                                                                               .BaseUrl = t.Result(0),
                                                                               .Pin = t.Result(1),
                                                                               .BranchId = t.Result(2),
                                                                               .DeviceSerial = t.Result(3),
                                                                               .Timeout = If(Integer.TryParse(t.Result(4), Nothing), CInt(t.Result(4)), 30)
                                                                           }, _logger)
                                                                      End Sub)
    End Sub

    Private Async Sub ProductManagementForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetPlaceholder(TxtSearchItemSave, "Start typing to search...")
        SetPlaceholder(TxtItemRequestSearch, "Start typing to search...")
        SetPlaceholder(TxtImportItemUpdateSearch, "Start typing to search...")
        SetPlaceholder(TxtImportItemSearch, "Start typing to search...")
        AddHandler TxtSearchItemSave.Enter, AddressOf TextBox_Enter
        AddHandler TxtSearchItemSave.Leave, AddressOf TextBox_Leave
        AddHandler TxtItemRequestSearch.Enter, AddressOf TextBox_Enter
        AddHandler TxtItemRequestSearch.Leave, AddressOf TextBox_Leave
        AddHandler TxtImportItemUpdateSearch.Enter, AddressOf TextBox_Enter
        AddHandler TxtImportItemUpdateSearch.Leave, AddressOf TextBox_Leave
        AddHandler TxtImportItemSearch.Enter, AddressOf TextBox_Enter
        AddHandler TxtImportItemSearch.Leave, AddressOf TextBox_Leave
        DtgvItemSave.AutoGenerateColumns = False
        DtgvItemRequest.AutoGenerateColumns = False
        DtgvImportItemRequest.AutoGenerateColumns = False
        DtgvImportItemUpload.AutoGenerateColumns = False
        SetupItemSaveGrid()
        SetupItemRequestGrid()
        SetupImportItemRequestGrid()
        SetupImportItemUploadGrid()
        Await LoadBranchesAsync()
    End Sub

    '=======================================================
    ' TEXTBOX TEXT CHANGE HANDLERS
    '=======================================================
    Private Sub TxtSearchItemSave_TextChanged(sender As Object, e As EventArgs) Handles TxtSearchItemSave.TextChanged
        If TxtSearchItemSave.ForeColor = Color.Gray Then Exit Sub
        FilterGrid(DtgvItemSave, TxtSearchItemSave.Text.Trim())
    End Sub

    Private Sub TxtItemRequestSearch_TextChanged(sender As Object, e As EventArgs) Handles TxtItemRequestSearch.TextChanged
        If TxtItemRequestSearch.ForeColor = Color.Gray Then Exit Sub
        FilterGrid(DtgvItemRequest, TxtItemRequestSearch.Text.Trim())
    End Sub

    Private Sub TxtImportItemUpdateSearch_TextChanged(sender As Object, e As EventArgs) Handles TxtImportItemUpdateSearch.TextChanged
        If TxtImportItemUpdateSearch.ForeColor = Color.Gray Then Exit Sub
        FilterGrid(DtgvImportItemUpload, TxtImportItemUpdateSearch.Text.Trim())
    End Sub

    Private Sub TxtImportItemSearch_TextChanged(sender As Object, e As EventArgs) Handles TxtImportItemSearch.TextChanged
        If TxtImportItemSearch.ForeColor = Color.Gray Then Exit Sub
        FilterGrid(DtgvImportItemRequest, TxtImportItemSearch.Text.Trim())
    End Sub

    ' =========================
    ' ITEM SAVE GRID
    ' =========================
    Private Sub SetupItemSaveGrid()
        With DtgvItemSave
            .Columns.Clear()
            .AllowUserToAddRows = False
            .ReadOnly = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            .Columns.Add(New DataGridViewCheckBoxColumn With {
                .Name = "chkSelect",
                .Width = 30
            })
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "itemCd", .HeaderText = "Item Code", .DataPropertyName = "itemCd"})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "itemNm", .HeaderText = "Item Name", .DataPropertyName = "itemNm"})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "itemClsCd", .HeaderText = "Item Class", .DataPropertyName = "itemClsCd"})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "pkgUnitCd", .HeaderText = "Package Unit", .DataPropertyName = "pkgUnitCd"})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "qtyUnitCd", .HeaderText = "Qty Unit", .DataPropertyName = "qtyUnitCd"})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "taxTyCd", .HeaderText = "Tax Type", .DataPropertyName = "taxTyCd"})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "dftPrc", .HeaderText = "Price", .DataPropertyName = "dftPrc"})
        End With
    End Sub

    Private Sub BtnFetch_Click(sender As Object, e As EventArgs) Handles BtnFetch.Click
        Loader.Visible = True
        Loader.Text = "Loading..."
        Dim repo As New ProductRepository(_connString)
        Dim products = repo.GetAllProducts()
        Dim dt As DataTable = (From p In products Select
                itemCd = p.ItemCd,
                itemNm = p.ItemNm,
                itemClsCd = p.ItemClsCd,
                pkgUnitCd = p.PackageUnit,
                qtyUnitCd = p.QuantityUnit,
                taxTyCd = p.TaxTyCd,
                dftPrc = p.DftPrc).ToDataTable()
        OriginalTables(DtgvItemSave) = dt
        DtgvItemSave.DataSource = dt
        AddHeaderCheckBox()
        Loader.Visible = False
    End Sub

    Private Async Sub BtnSendItem_Click(sender As Object, e As EventArgs) Handles BtnSendItem.Click
        Try
            Loader.Visible = True
            Loader.Text = "Processing..."
            BtnSendItem.Enabled = False
            BtnSendItem.Text = "Processing..."
            Dim tin = Await _settingsManager.GetSettingAsync("pin")
            Dim bhfId = Await _settingsManager.GetSettingAsync("branch_id")
            Dim repo As New ProductRepository(_connString)
            Dim selectedItems As New List(Of String)
            For Each row As DataGridViewRow In DtgvItemSave.Rows
                Dim isChecked As Boolean = False
                If row.Cells("chkSelect").Value IsNot Nothing Then
                    isChecked = CBool(row.Cells("chkSelect").Value)
                End If
                If isChecked Then
                    Dim code As String = CStr(row.Cells("itemCd").Value)
                    If Not String.IsNullOrWhiteSpace(code) Then
                        selectedItems.Add(code)
                    End If
                End If
            Next
            If selectedItems.Count = 0 Then
                CustomAlert.ShowAlert(Me, "No items selected.", "Warning", CustomAlert.AlertType.Warning, CustomAlert.ButtonType.OK)
                Exit Sub
            End If
            Dim index As Integer = 0
            Dim total As Integer = selectedItems.Count
            For Each itemCd In selectedItems
                index += 1
                Dim product = repo.GetProductByProductCode(itemCd)
                If product Is Nothing Then
                    CustomAlert.ShowAlert(Me, $"Item '{itemCd}' not found in database.", "Error", CustomAlert.AlertType.Error,
                                            CustomAlert.ButtonType.OK)
                    Exit Sub
                End If
                Dim req As New ItemSaveRequest With {
                    .tin = tin,
                    .bhfId = bhfId,
                    .itemCd = product.ItemCd,
                    .itemNm = product.ItemNm,
                    .itemClsCd = product.ItemClsCd,
                    .itemTyCd = If(String.IsNullOrEmpty(product.ItemTyCd), "2", product.ItemTyCd),
                    .itemStdNm = If(String.IsNullOrEmpty(product.ItemStdNm), Nothing, product.ItemStdNm),
                    .orgnNatCd = If(String.IsNullOrEmpty(product.OrgNatCd), "KE", product.OrgNatCd),
                    .pkgUnitCd = product.PackageUnit,
                    .qtyUnitCd = product.QuantityUnit,
                    .taxTyCd = product.TaxTyCd,
                    .dftPrc = product.DftPrc,
                    .grpPrcL1 = If(product.GroupPrice1 > 0D, product.GroupPrice1, product.DftPrc),
                    .grpPrcL2 = If(product.GroupPrice2 > 0D, product.GroupPrice2, product.DftPrc),
                    .grpPrcL3 = If(product.GroupPrice3 > 0D, product.GroupPrice3, product.DftPrc),
                    .isrcAplcbYn = If(String.IsNullOrEmpty(product.IsrcAplcbYn), "N", product.IsrcAplcbYn),
                    .useYn = If(String.IsNullOrEmpty(product.UseYn), "Y", product.UseYn),
                    .regrNm = If(String.IsNullOrEmpty(product.CreatedBy), "Admin", product.CreatedBy),
                    .regrId = If(String.IsNullOrEmpty(product.CreatedBy), "Admin", product.CreatedBy),
                    .modrNm = If(String.IsNullOrEmpty(product.ModifiedBy), "Admin", product.ModifiedBy),
                    .modrId = If(String.IsNullOrEmpty(product.ModifiedBy), "Admin", product.ModifiedBy)
                }
                Dim res = Await _integrator.SaveItemAsync(req)
                If res Is Nothing Then
                    CustomAlert.ShowAlert(Me, $"NO RESPONSE FROM SERVER for item {itemCd}", "Error", CustomAlert.AlertType.Error,
                                            CustomAlert.ButtonType.OK)
                    Exit Sub
                End If
                If res.resultCd <> "000" Then
                    CustomAlert.ShowAlert(Me, $"FAILED at item {itemCd}" & vbCrLf & $"Code: {res.resultCd}" & vbCrLf & $"Message: {res.resultMsg}",
                                            "Upload Failed", CustomAlert.AlertType.Error, CustomAlert.ButtonType.OK)
                    Exit Sub
                End If
                If index < total Then
                    Continue For
                End If
                CustomAlert.ShowAlert(Me, $"All {total} items uploaded successfully.", "Upload Complete", CustomAlert.AlertType.Success,
                                        CustomAlert.ButtonType.OK)
            Next
        Catch ex As Exception
            CustomAlert.ShowAlert(Me, "Sync Error: " & ex.Message, "Error", CustomAlert.AlertType.Error, CustomAlert.ButtonType.OK)
        Finally
            Loader.Visible = False
            BtnSendItem.Enabled = True
            BtnSendItem.Text = "Upload"
        End Try
    End Sub

    ' =========================
    ' ITEM REQUEST GRID
    ' =========================
    Private Sub SetupItemRequestGrid()
        With DtgvItemRequest
            .Columns.Clear()
            .AllowUserToAddRows = False
            .ReadOnly = True
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

            .Columns.Add(New DataGridViewCheckBoxColumn With {
                .Name = "chkSelectReq",
                .Width = 30
            })
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "itemCd", .HeaderText = "Item Code", .DataPropertyName = "itemCd"})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "itemNm", .HeaderText = "Item Name", .DataPropertyName = "itemNm"})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "itemClsCd", .HeaderText = "Item Class", .DataPropertyName = "itemClsCd"})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "taxTyCd", .HeaderText = "Tax Type", .DataPropertyName = "taxTyCd"})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "pkgUnitCd", .HeaderText = "Pkg Unit", .DataPropertyName = "pkgUnitCd"})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "qtyUnitCd", .HeaderText = "Qty Unit", .DataPropertyName = "qtyUnitCd"})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "dftPrc", .HeaderText = "Price", .DataPropertyName = "dftPrc"})
        End With
    End Sub

    Private Async Sub BtnItemRequest_Click(sender As Object, e As EventArgs) Handles BtnItemRequest.Click
        Try
            Loader.Visible = True
            Loader.Text = "Processing..."
            BtnItemRequest.Enabled = False
            BtnItemRequest.Text = "Processing..."
            Dim req As New ItemInfoRequest With {
                .tin = Await _settingsManager.GetSettingAsync("pin"),
                .bhfId = Await _settingsManager.GetSettingAsync("branch_id"),
                .lastReqDt = Await _settingsManager.GetSettingAsync("last_item_request_dt")
            }
            Dim res = Await _integrator.GetItemAsync(req)
            If res Is Nothing OrElse res.resultCd <> "000" Then
                CustomAlert.ShowAlert(Me, "No new item data returned from KRA.", "Info", CustomAlert.AlertType.Info, CustomAlert.ButtonType.OK)
                Exit Sub
            Else
                Dim items = res.data.itemList
                Dim dt As DataTable =
                    (From it In items Select
                    itemCd = it.itemCd,
                    itemNm = it.itemNm,
                    itemClsCd = it.itemClsCd,
                    taxTyCd = it.taxTyCd,
                    pkgUnitCd = it.pkgUnitCd,
                    qtyUnitCd = it.qtyUnitCd,
                    dftPrc = it.dftPrc).ToDataTable()
                OriginalTables(DtgvItemRequest) = dt
                DtgvItemRequest.DataSource = dt
                Await _settingsManager.SetSettingAsync("last_item_request_dt", DateTime.Now.ToString("yyyyMMddHHmmss"))
            End If
        Catch ex As Exception
            CustomAlert.ShowAlert(Me, "Unexpected error: " & ex.Message, "Error", CustomAlert.AlertType.Error, CustomAlert.ButtonType.OK)
        Finally
            Loader.Visible = False
            BtnItemRequest.Enabled = True
            BtnItemRequest.Text = "FETCH"
        End Try
    End Sub

    ' =========================
    ' FILTERING (ALL GRIDS)
    ' =========================
    Private Sub FilterGrid(grid As DataGridView, search As String)
        If Not OriginalTables.ContainsKey(grid) Then Exit Sub
        Dim dv As DataView = OriginalTables(grid).DefaultView
        If String.IsNullOrWhiteSpace(search) Then
            dv.RowFilter = ""
        Else
            Dim safe = search.Replace("'", "''")
            dv.RowFilter = String.Join(" OR ",
                From c As DataColumn In dv.Table.Columns
                Select $"CONVERT([{c.ColumnName}], 'System.String') LIKE '%{safe}%'")
        End If
        grid.DataSource = dv
    End Sub

    ' =========================
    ' HEADER CHECKBOX
    ' =========================
    Private Sub AddHeaderCheckBox()
        Dim rect = DtgvItemSave.GetCellDisplayRectangle(0, -1, True)
        headerCheckBox.Size = New Size(18, 18)
        headerCheckBox.Location = New Point(rect.X + 4, rect.Y + 4)
        AddHandler headerCheckBox.CheckedChanged, AddressOf HeaderCheckBox_CheckedChanged
        DtgvItemSave.Controls.Add(headerCheckBox)
        'DtgvImportItemUpload.Controls.Add(headerCheckBox)
    End Sub

    Private Sub DtgvItemSave_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs)
        ' Only handle checkbox column
        If e.RowIndex < 0 Or e.ColumnIndex <> DtgvItemSave.Columns("chkSelect").Index Then Exit Sub
        ' Check if all row checkboxes are checked
        Dim allChecked As Boolean = True
        Dim anyChecked As Boolean = False
        For Each row As DataGridViewRow In DtgvItemSave.Rows
            Dim isChecked As Boolean = If(row.Cells("chkSelect").Value, False)
            allChecked = allChecked And isChecked
            anyChecked = anyChecked Or isChecked
        Next
        ' Temporarily remove header handler to prevent recursion
        RemoveHandler headerCheckBox.CheckedChanged, AddressOf HeaderCheckBox_CheckedChanged
        ' Set header checkbox state
        If allChecked Then
            headerCheckBox.CheckState = CheckState.Checked
        ElseIf anyChecked Then
            headerCheckBox.CheckState = CheckState.Indeterminate
        Else
            headerCheckBox.CheckState = CheckState.Unchecked
        End If
        ' Reattach handler
        AddHandler headerCheckBox.CheckedChanged, AddressOf HeaderCheckBox_CheckedChanged
    End Sub

    Private Sub HeaderCheckBox_CheckedChanged(sender As Object, e As EventArgs)
        ' Remove handler to avoid recursion
        RemoveHandler DtgvItemSave.CellValueChanged, AddressOf DtgvItemSave_CellValueChanged

        ' Temporarily move current cell to a non-checkbox cell to exit edit mode
        Dim currentRow = DtgvItemSave.CurrentCell?.RowIndex
        Dim currentCol = DtgvItemSave.CurrentCell?.ColumnIndex
        If currentRow.HasValue AndAlso currentCol.HasValue Then
            DtgvItemSave.CurrentCell = Nothing
        End If

        Dim checkValue As Boolean = headerCheckBox.Checked

        ' Update all rows
        For Each row As DataGridViewRow In DtgvItemSave.Rows
            If Not row.IsNewRow Then
                row.Cells("chkSelect").Value = checkValue
            End If
        Next

        ' Force the grid to repaint
        DtgvItemSave.Refresh()

        ' Restore previous current cell if needed
        If currentRow.HasValue AndAlso currentCol.HasValue Then
            DtgvItemSave.CurrentCell = DtgvItemSave.Rows(currentRow.Value).Cells(currentCol.Value)
        End If

        ' Reattach handler
        AddHandler DtgvItemSave.CellValueChanged, AddressOf DtgvItemSave_CellValueChanged
    End Sub

    ' =========================
    ' PLACEHOLDER HANDLERS
    ' =========================
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

    Private Async Sub BtnImportItemRequest_Click(sender As Object, e As EventArgs) Handles BtnImportItemRequest.Click
        Try
            Loader.Visible = True
            Loader.Text = "Processing..."
            BtnImportItemRequest.Enabled = False
            BtnImportItemRequest.Text = "Processing..."
            Dim request As New ImportItemsRequest With {
                .tin = Await _settingsManager.GetSettingAsync("pin"),
                .bhfId = Await _settingsManager.GetSettingAsync("branch_id"),
                .lastReqDt = Await _settingsManager.GetSettingAsync("last_import_item_request_dt")
            }
            Dim response = Await _integrator.GetImportItemsAsync(request)
            If response IsNot Nothing AndAlso response.resultCd = "000" Then
                If response.data IsNot Nothing AndAlso response.data.itemList IsNot Nothing Then
                    Dim items = response.data.itemList
                    _branchImportRepo.Save(items)
                    Dim dt As DataTable = (From it In items Select
                        taskCd = it.taskCd,
                        itemSeq = it.itemSeq,
                        dclNo = it.dclNo,
                        hsCd = it.hsCd,
                        itemNm = it.itemNm,
                        orgnNatCd = it.orgnNatCd,
                        pkg = it.pkg,
                        qty = it.qty,
                        qtyUnitCd = it.qtyUnitCd,
                        netWt = it.netWt,
                        spplrNm = it.spplrNm,
                        invcFcurAmt = it.invcFcurAmt,
                        invcFcurCd = it.invcFcurCd).ToDataTable()
                    OriginalTables(DtgvImportItemRequest) = dt.Copy()
                    DtgvImportItemRequest.DataSource = dt
                    Await _settingsManager.SetSettingAsync("last_import_item_request_dt", DateTime.Now.ToString("yyyyMMddHHmmss"))
                    CustomAlert.ShowAlert(Me, $"{response.data.itemList.Count} items imported successfully.", "Success",
                                    CustomAlert.AlertType.Success,
                                    CustomAlert.ButtonType.OK)
                End If
            Else
                CustomAlert.ShowAlert(Me, "Failed to fetch import items." & vbCrLf & $"Message: {response?.resultMsg}", "Error",
                                        CustomAlert.AlertType.Error,
                                        CustomAlert.ButtonType.OK)
            End If
        Catch ex As Exception
            CustomAlert.ShowAlert(Me, "Error: " & ex.Message, "Error", CustomAlert.AlertType.Error, CustomAlert.ButtonType.OK)
        Finally
            Loader.Visible = False
            BtnImportItemRequest.Enabled = True
            BtnImportItemRequest.Text = "REQUEST"
        End Try
    End Sub

    Private Sub SetupImportItemRequestGrid()
        With DtgvImportItemRequest
            .Columns.Clear()
            .AllowUserToAddRows = False
            .ReadOnly = True
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "taskCd", .HeaderText = "Task Code", .DataPropertyName = "taskCd"})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "itemSeq", .HeaderText = "Item Seq", .DataPropertyName = "itemSeq"})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "dclNo", .HeaderText = "Declaration No", .DataPropertyName = "dclNo"})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "hsCd", .HeaderText = "HS Code", .DataPropertyName = "hsCd"})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "itemNm", .HeaderText = "Item Name", .DataPropertyName = "itemNm"})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "orgnNatCd", .HeaderText = "Origin", .DataPropertyName = "orgnNatCd"})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "pkg", .HeaderText = "Package", .DataPropertyName = "pkg"})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "qty", .HeaderText = "Quantity", .DataPropertyName = "qty"})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "qtyUnitCd", .HeaderText = "Qty Unit", .DataPropertyName = "qtyUnitCd"})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "netWt", .HeaderText = "Net Weight", .DataPropertyName = "netWt"})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "spplrNm", .HeaderText = "Supplier", .DataPropertyName = "spplrNm"})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "invcFcurAmt", .HeaderText = "Invoice Amt", .DataPropertyName = "invcFcurAmt"})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "invcFcurCd", .HeaderText = "Currency", .DataPropertyName = "invcFcurCd"})
        End With
    End Sub

    Private Sub BtnImportItemFetch_Click(sender As Object, e As EventArgs) Handles BtnImportItemFetch.Click
        LoadImportStatusGrid()
    End Sub

    Private Sub BtnSaveImportItemStatus_Click(sender As Object, e As EventArgs) Handles BtnSaveImportItemStatus.Click
        Dim repo As New BranchImportItemStatusRepository(_connString)
        Dim saveCount As Integer = 0
        For Each row As DataGridViewRow In DtgvImportItemUpload.Rows
            If row.IsNewRow Then Continue For
            ' Skip empty rows
            If row.Cells("taskCd").Value Is Nothing OrElse String.IsNullOrWhiteSpace(row.Cells("taskCd").Value.ToString()) Then
                Continue For
            End If
            Dim rawDate = row.Cells("dclDe").Value
            Dim formattedDate As String = Nothing
            If rawDate IsNot Nothing AndAlso Not IsDBNull(rawDate) Then
                Dim parsedDate As DateTime
                If DateTime.TryParse(rawDate.ToString(), parsedDate) Then
                    formattedDate = parsedDate.ToString("yyyy-MM-dd")
                End If
            End If
            Dim entity As New ImportItemStatus With {
                .Id = If(row.Cells("id").Value IsNot Nothing AndAlso Not IsDBNull(row.Cells("id").Value), CInt(row.Cells("id").Value), 0),
                .TaskCd = row.Cells("taskCd").Value?.ToString(),
                .DclDe = formattedDate,
                .ItemSeq = If(row.Cells("itemSeq").Value IsNot Nothing, Convert.ToInt32(row.Cells("itemSeq").Value), 0),
                .HsCd = row.Cells("hsCd").Value?.ToString(),
                .ItemClsCd = row.Cells("itemClsCd").Value?.ToString(),
                .ItemCd = row.Cells("itemCd").Value?.ToString(),
                .ImptItemSttsCd = row.Cells("imptItemSttsCd").Value?.ToString(),
                .Remark = row.Cells("remark").Value?.ToString(),
                .ModrNm = "Admin",
                .ModrId = "Admin"
            }
            repo.Save(entity)
            saveCount += 1
        Next
        If saveCount = 0 Then
            CustomAlert.ShowAlert(Me, "No valid records to save.", "Info", CustomAlert.AlertType.Info, CustomAlert.ButtonType.OK)
            Return
        End If
        CustomAlert.ShowAlert(Me,
        $"{saveCount} record(s) saved successfully.", "Success", CustomAlert.AlertType.Success, CustomAlert.ButtonType.OK)
        LoadImportStatusGrid()
    End Sub

    Private Async Sub BtnImportItemUpload_Click(sender As Object, e As EventArgs) Handles BtnImportItemUpload.Click
        Try
            Loader.Visible = True
            Loader.Text = "Updating..."
            BtnImportItemUpload.Enabled = False
            Dim tin = Await _settingsManager.GetSettingAsync("pin")
            Dim bhfId = Await _settingsManager.GetSettingAsync("branch_id")
            ' Get selected row from grid
            If DtgvImportItemUpload.CurrentRow Is Nothing Then
                CustomAlert.ShowAlert(Me, "Select an item first.", "Warning", CustomAlert.AlertType.Warning, CustomAlert.ButtonType.OK)
                Exit Sub
            End If
            Dim repo As New BranchImportItemStatusRepository(_connString)
            For Each row As DataGridViewRow In DtgvImportItemUpload.Rows
                If row.IsNewRow Then Continue For
                Dim isChecked As Boolean =
            If(row.Cells("chkSelect").Value, False)
                If Not isChecked Then Continue For
                If CBool(row.Cells("is_uploaded").Value) Then Continue For
                Dim req As New ImportItemStatusUpdateRequest With {
                    .tin = tin,
                    .bhfId = bhfId,
                    .taskCd = row.Cells("taskCd").Value.ToString(),
                    .itemSeq = Convert.ToInt32(row.Cells("itemSeq").Value),
                    .hsCd = row.Cells("hsCd").Value.ToString(),
                    .itemClsCd = row.Cells("itemClsCd").Value.ToString(),
                    .itemCd = row.Cells("itemCd").Value.ToString(),
                    .remark = row.Cells("remark").Value?.ToString(),
                    .modrNm = "Admin",
                    .modrId = "Admin"
                }
                Dim parsedDate As DateTime
                If DateTime.TryParse(row.Cells("dclDe").Value?.ToString(), parsedDate) Then
                    req.dclDe = parsedDate.ToString("yyyyMMdd")
                Else
                    CustomAlert.ShowAlert(Me, $"Invalid Declaration Date for Task {req.taskCd}. Please correct the date format.", "Validation Error",
                                            CustomAlert.AlertType.Warning, CustomAlert.ButtonType.OK)
                End If
                Dim statusValue = row.Cells("imptItemSttsCd").Value
                If statusValue Is Nothing OrElse String.IsNullOrWhiteSpace(statusValue.ToString()) Then
                    CustomAlert.ShowAlert(Me, $"Status Code is required for Task {row.Cells("taskCd").Value}", "Validation Error",
                                            CustomAlert.AlertType.Warning, CustomAlert.ButtonType.OK)
                End If
                req.imptItemSttsCd = statusValue.ToString()
                Dim response = Await _integrator.UpdateImportItemStatusAsync(req)
                If response IsNot Nothing AndAlso response.resultCd = "000" Then
                    repo.MarkAsUploaded(CInt(row.Cells("id").Value))
                Else
                    CustomAlert.ShowAlert(Me, $"Upload failed for Task {req.taskCd}" & vbCrLf & $"Code: {response?.resultCd}" & vbCrLf &
                                            $"Message: {response?.resultMsg}",
                                            "Error",
                                            CustomAlert.AlertType.Error,
                                            CustomAlert.ButtonType.OK)
                    Exit Sub
                End If
            Next
            CustomAlert.ShowAlert(Me, "Selected records uploaded successfully.", "Success", CustomAlert.AlertType.Success,
                            CustomAlert.ButtonType.OK)
            LoadImportStatusGrid()
        Catch ex As Exception
            CustomAlert.ShowAlert(Me, "Error: " & ex.Message, "Error", CustomAlert.AlertType.Error, CustomAlert.ButtonType.OK)
        Finally
            Loader.Visible = False
            BtnImportItemUpload.Enabled = True
        End Try
    End Sub

    Private Sub SetupImportItemUploadGrid()
        With DtgvImportItemUpload
            .Columns.Clear()
            .ReadOnly = False
            .AllowUserToAddRows = True
            .AutoGenerateColumns = False
            .AllowUserToDeleteRows = True
            .EditMode = DataGridViewEditMode.EditOnEnter
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            .Columns.Add(New DataGridViewCheckBoxColumn With {
                .Name = "chkSelect",
                .Width = 30
            })
            .Columns.Add(New DataGridViewTextBoxColumn With {
                .Name = "id",
                .DataPropertyName = "id",
                .Visible = False
            })
            .Columns.Add(New DataGridViewTextBoxColumn With {
                .Name = "taskCd",
                .HeaderText = "Task Code",
                .DataPropertyName = "taskCd"
            })
            .Columns.Add(New DataGridViewTextBoxColumn With {
                .Name = "dclDe",
                .HeaderText = "Declaration Date",
                .DataPropertyName = "dclDe"
                            })
            .Columns.Add(New DataGridViewTextBoxColumn With {
                .Name = "itemSeq",
                .HeaderText = "Item Seq",
                .DataPropertyName = "itemSeq"
            })
            .Columns.Add(New DataGridViewTextBoxColumn With {
                .Name = "hsCd",
                .HeaderText = "HS Code",
                .DataPropertyName = "hsCd"
            })
            Dim itemClsColumn As New DataGridViewComboBoxColumn With {
                .Name = "itemClsCd",
                .HeaderText = "Item Class",
                .DataPropertyName = "itemClsCd",
                .DisplayMember = "itemClsNm",
                .ValueMember = "itemClsCd"
            }
            Dim clsRepo As New ItemClassificationRepository(_connString)
            itemClsColumn.DataSource = clsRepo.GetAll()
            .Columns.Add(itemClsColumn)
            .Columns.Add(New DataGridViewTextBoxColumn With {
                .Name = "itemCd",
                .HeaderText = "Item Code",
                .DataPropertyName = "itemCd"
            })
            Dim statusColumn As New DataGridViewComboBoxColumn With {
                .Name = "imptItemSttsCd",
                .HeaderText = "Status",
                .DataPropertyName = "imptItemSttsCd",
                .DisplayMember = "statusName",
                .ValueMember = "statusCode"
            }
            Dim statusRepo As New ImportItemStatusCodeRepository(_connString)
            statusColumn.DataSource = statusRepo.GetAll()
            .Columns.Add(statusColumn)
            .Columns.Add(New DataGridViewTextBoxColumn With {
                .Name = "remark",
                .HeaderText = "Remark",
                .DataPropertyName = "remark"
            })
            .Columns.Add(New DataGridViewCheckBoxColumn With {
                .Name = "is_uploaded",
                .HeaderText = "Uploaded",
                .DataPropertyName = "is_uploaded",
                .ReadOnly = True
            })
        End With
    End Sub

    Private Sub LoadImportStatusGrid()
        Dim repo As New BranchImportItemStatusRepository(_connString)
        Dim data = repo.GetAll()
        Dim dt As DataTable = (From it In data Select
            id = it.Id,
            taskCd = it.TaskCd,
            dclDe = it.DclDe,
            itemSeq = it.ItemSeq,
            hsCd = it.HsCd,
            itemClsCd = it.ItemClsCd,
            itemCd = it.ItemCd,
            imptItemSttsCd = it.ImptItemSttsCd,
            remark = it.Remark,
            is_uploaded = it.IsUploaded).ToDataTable()
        OriginalTables(DtgvImportItemUpload) = dt
        DtgvImportItemUpload.DataSource = dt
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

    Private Async Sub BtnSaveItemComposition_Click(sender As Object, e As EventArgs) Handles BtnSaveItemComposition.Click
        Try
            BtnSaveItemComposition.Enabled = False
            Loader.Visible = True
            Loader.Text = "Saving ..."
            Await Task.Yield()
            Dim tin = txtTin.Text.ToString()
            Dim bhfId = txtBhfId.Text.ToString()
            Dim itemCd = TxtItemCode.Text.ToString()
            Dim cpstItemCd = TxtCompositionCode.Text.ToString()
            Dim cpstQty = Convert.ToDecimal(TxtQuantity.Text)
            Dim regrId = "Admin"
            Dim regrNm = "Admin"
            Dim repo As New ItemCompositionRepository(_connString)
            Dim existing = Await Task.Run(Function()
                                              Return repo.GetByUniqueKey(tin, bhfId, itemCd, cpstItemCd)
                                          End Function)
            Dim entity As New ItemComposition With {
                .Tin = tin,
                .BhfId = bhfId,
                .ItemCd = itemCd,
                .CpstItemCd = cpstItemCd,
                .CpstQty = cpstQty,
                .RegrId = regrId,
                .RegrNm = regrNm
            }
            If existing IsNot Nothing Then
                Await Task.Run(Sub() repo.Update(entity))
            Else
                Await Task.Run(Sub() repo.Insert(entity))
            End If
            Loader.Text = "Uploading ..."
            Await Task.Yield()
            Dim req = BuildCompositionRequest(entity)
            Dim resp = Await _integrator.SaveItemCompositionAsync(req)

            If resp IsNot Nothing AndAlso resp.resultCd = "000" Then
                repo.MarkAsUploaded(
                    entity.Tin,
                    entity.BhfId,
                    entity.ItemCd,
                    entity.CpstItemCd)
                CustomAlert.ShowAlert(Me, "Item Composition saved and uploaded successfully", "Success",
                                        CustomAlert.AlertType.Success, CustomAlert.ButtonType.OK)
                clearInputs()
            Else
                Await _logger.LogAsync(LogLevel.Error, $"Composition upload failed: {resp?.resultMsg}")
                CustomAlert.ShowAlert(Me, $"Composition upload failed: {resp?.resultMsg}", "Error",
                                CustomAlert.AlertType.Error, CustomAlert.ButtonType.OK)
            End If
        Catch ex As Exception
            CustomAlert.ShowAlert(Me, $"An unexpected error occurred: {ex.Message}", "Error", CustomAlert.AlertType.Error, CustomAlert.ButtonType.OK)
        Finally
            Loader.Visible = False
            BtnSaveItemComposition.Enabled = True
        End Try
    End Sub

    Private Function BuildCompositionRequest(entity As ItemComposition) As ItemCompositionSaveRequest
        Return New ItemCompositionSaveRequest With {
            .tin = entity.Tin,
            .bhfId = entity.BhfId,
            .itemCd = entity.ItemCd,
            .cpstItemCd = entity.CpstItemCd,
            .cpstQty = entity.CpstQty,
            .regrId = entity.RegrId,
            .regrNm = entity.RegrNm
        }
    End Function

    Public Sub clearInputs()
        CmbBranches.SelectedIndex = 0
        txtTin.Text = ""
        txtBhfId.Text = ""
        TxtItemCode.Text = ""
        TxtCompositionCode.Text = ""
        TxtQuantity.Text = ""
    End Sub
End Class