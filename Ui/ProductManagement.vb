Imports Core.Config
Imports Core.Logging
Imports Core.Models.Item.Info
Imports Core.Models.Item.Product
Imports Core.Repo
Imports Core.Services
Imports Ui.Helpers

Public Class ProductManagement
    Private _settingsManager As SettingsManager
    Private _integrator As VSCUIntegrator
    Private _logger As Logger
    Private _connString As String

    Private OriginalTables As New Dictionary(Of DataGridView, DataTable)
    Private headerCheckBox As New CheckBox()

    Public Sub New(connectionString As String)
        InitializeComponent()
        _connString = connectionString

        _settingsManager = New SettingsManager(connectionString)

        Dim repo As New LogRepository(_connString)
        _logger = New Logger(repo)

        Task.WhenAll(
            _settingsManager.GetSettingAsync("base_url"),
            _settingsManager.GetSettingAsync("pin"),
            _settingsManager.GetSettingAsync("branch_id"),
            _settingsManager.GetSettingAsync("device_serial"),
            _settingsManager.GetSettingAsync("timeout")
        ).ContinueWith(Sub(t)
                           _integrator = New VSCUIntegrator(New IntegratorSettings With {
                               .BaseUrl = t.Result(0),
                               .Pin = t.Result(1),
                               .BranchId = t.Result(2),
                               .DeviceSerial = t.Result(3),
                               .Timeout = If(Integer.TryParse(t.Result(4), Nothing), CInt(t.Result(4)), 30)
                           }, _logger)

                       End Sub)
    End Sub

    Private Sub ProductManagementForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Add placeholder text
        SetPlaceholder(TxtSearchItemSave, "Start typing to search...")
        SetPlaceholder(TxtItemRequestSearch, "Start typing to search...")
        SetPlaceholder(TxtImportItemUpdateSearch, "Start typing to search...")
        SetPlaceholder(TxtImportItemSearch, "Start typing to search...")

        ' Attach placeholder handlers
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

        SetupItemSaveGrid()
        SetupItemRequestGrid()
    End Sub

    '=======================================================
    ' TEXTBOX TEXT CHANGE HANDLERS
    '=======================================================
    Private Sub TxtSearchItemSave_TextChanged(sender As Object, e As EventArgs) Handles TxtSearchItemSave.TextChanged
        ' Prevent filtering when placeholder is active
        If TxtSearchItemSave.ForeColor = Color.Gray Then Exit Sub

        FilterGrid(DtgvItemSave, TxtSearchItemSave.Text.Trim())
    End Sub

    Private Sub TxtItemRequestSearch_TextChanged(sender As Object, e As EventArgs) Handles TxtItemRequestSearch.TextChanged
        ' Prevent filtering when placeholder is active
        If TxtItemRequestSearch.ForeColor = Color.Gray Then Exit Sub

        FilterGrid(DtgvItemRequest, TxtItemRequestSearch.Text.Trim())
    End Sub

    Private Sub TxtImportItemUpdateSearch_TextChanged(sender As Object, e As EventArgs) Handles TxtImportItemUpdateSearch.TextChanged
        ' Prevent filtering when placeholder is active
        If TxtImportItemUpdateSearch.ForeColor = Color.Gray Then Exit Sub

        FilterGrid(DtgvImportItemUpload, TxtImportItemUpdateSearch.Text.Trim())
    End Sub

    Private Sub TxtImportItemSearch_TextChanged(sender As Object, e As EventArgs) Handles TxtImportItemSearch.TextChanged
        ' Prevent filtering when placeholder is active
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
        Dim repo As New ProductRepository(_connString)
        Dim products = repo.GetAllProducts()

        Dim dt As DataTable =
            (From p In products Select
                itemCd = p.ItemCd,
                itemNm = p.ItemNm,
                itemClsCd = p.ItemClsCd,
                pkgUnitCd = p.PackageUnit,
                qtyUnitCd = p.QuantityUnit,
                taxTyCd = p.TaxTyCd,
                dftPrc = p.DftPrc
            ).ToDataTable()

        OriginalTables(DtgvItemSave) = dt
        DtgvItemSave.DataSource = dt

        AddHeaderCheckBox()
    End Sub

    Private Async Sub BtnSendItem_Click(sender As Object, e As EventArgs) Handles BtnSendItem.Click
        Try
            BtnSendItem.Enabled = False
            BtnSendItem.Text = "Processing..."
            ' Load integrator settings
            Dim tin = Await _settingsManager.GetSettingAsync("pin")
            Dim bhfId = Await _settingsManager.GetSettingAsync("branch_id")

            Dim repo As New ProductRepository(_connString)

            ' Collect all checked item codes first
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

            ' If none selected stop here
            If selectedItems.Count = 0 Then
                CustomAlert.ShowAlert(Me, "No items selected.", "Warning", CustomAlert.AlertType.Warning)
                Exit Sub
            End If

            ' Main communication loop
            Dim index As Integer = 0
            Dim total As Integer = selectedItems.Count

            For Each itemCd In selectedItems
                index += 1

                '------------------------------------------------------------------------
                ' 1. Read record from database
                '------------------------------------------------------------------------
                Dim product = repo.GetProductByProductCode(itemCd)

                If product Is Nothing Then
                    CustomAlert.ShowAlert(Me, $"Item '{itemCd}' not found in database.", "Error", CustomAlert.AlertType.Error)
                    Exit Sub   ' stop entire upload process if any item is missing
                End If

                '------------------------------------------------------------------------
                ' 2. Build KRA request strictly from DB values
                '------------------------------------------------------------------------
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

                '------------------------------------------------------------------------
                ' 3. Send to KRA
                '------------------------------------------------------------------------
                Dim res = Await _integrator.SaveItemAsync(req)

                '------------------------------------------------------------------------
                ' 4. Evaluate response
                '------------------------------------------------------------------------
                If res Is Nothing Then
                    CustomAlert.ShowAlert(Me, $"NO RESPONSE FROM SERVER for item {itemCd}", "Error", CustomAlert.AlertType.Error)
                    Exit Sub
                End If

                If res.resultCd <> "000" Then
                    ' STOP immediately on first error
                    CustomAlert.ShowAlert(Me, $"FAILED at item {itemCd}" & vbCrLf & $"Code: {res.resultCd}" & vbCrLf & $"Message: {res.resultMsg}", "Upload Failed", CustomAlert.AlertType.Error)
                    Exit Sub
                End If

                '------------------------------------------------------------------------
                ' 5. If success and NOT last item, continue
                '------------------------------------------------------------------------
                If index < total Then
                    Continue For
                End If

                '------------------------------------------------------------------------
                ' 6. If last item and all succeeded
                '------------------------------------------------------------------------
                CustomAlert.ShowAlert(Me, $"All {total} items uploaded successfully.", "Upload Complete", CustomAlert.AlertType.Success
            )
            Next

        Catch ex As Exception
            CustomAlert.ShowAlert(Me, "Sync Error: " & ex.Message, "Error", CustomAlert.AlertType.Error)
        Finally
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
            BtnItemRequest.Enabled = False
            BtnItemRequest.Text = "Processing..."
            Dim req As New ItemInfoRequest With {
            .tin = Await _settingsManager.GetSettingAsync("pin"),
            .bhfId = Await _settingsManager.GetSettingAsync("branch_id"),
            .lastReqDt = Await _settingsManager.GetSettingAsync("last_item_request_dt")
        }

            Dim res = Await _integrator.GetItemAsync(req)
            If res Is Nothing OrElse res.resultCd <> "000" Then
                CustomAlert.ShowAlert(Me, "No new item data returned from KRA.", "Info", CustomAlert.AlertType.Info)
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
            CustomAlert.ShowAlert(Me, "Unexpected error: " & ex.Message, "Error", CustomAlert.AlertType.Error)
        Finally
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

End Class