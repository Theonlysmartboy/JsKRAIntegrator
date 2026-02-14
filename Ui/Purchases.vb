Imports Core.Config
Imports Core.Logging
Imports Core.Models.Purchase
Imports Core.Services
Imports Ui.Helpers
Imports Ui.Repo
Imports Ui.Repo.ItemRepo
Imports Ui.Repo.ProductRepo
Imports Ui.Repo.UnitRepo

Public Class Purchases
    Private _settingsManager As SettingsManager
    Private _integrator As VSCUIntegrator
    Private _purchaseRepo As PurchaseRepository
    Private _logger As Logger
    Private OriginalTables As New Dictionary(Of DataGridView, DataTable)
    Private _tin As String
    Private _branchId As String
    Private _connString As String

    Public Sub New(connectionString As String)
        InitializeComponent()
        ' Initialize settings manager
        _connString = connectionString
        _settingsManager = New SettingsManager(_connString)
        ' Initialize logger
        Dim repo As New LogRepository(DatabaseHelper.GetConnectionString())
        _logger = New Logger(repo)
        ' Build IntegratorSettings from DB
        Dim baseUrlTask = _settingsManager.GetSettingAsync("base_url")
        Dim pinTask = _settingsManager.GetSettingAsync("pin")
        Dim branchIdTask = _settingsManager.GetSettingAsync("branch_id")
        Dim deviceSerialTask = _settingsManager.GetSettingAsync("device_serial")
        Dim timeoutTask = _settingsManager.GetSettingAsync("timeout")
        _purchaseRepo = New PurchaseRepository(_connString)
        _tin = pinTask.Result
        _branchId = branchIdTask.Result
        Task.WhenAll(baseUrlTask, pinTask, branchIdTask, deviceSerialTask, timeoutTask).ContinueWith(Sub(t)
                                                                                                         Dim settings As New IntegratorSettings With {
                                                                                                            .BaseUrl = baseUrlTask.Result,
                                                                                                            .Pin = pinTask.Result,
                                                                                                            .BranchId = branchIdTask.Result,
                                                                                                            .DeviceSerial = deviceSerialTask.Result,
                                                                                                            .Timeout = If(Integer.TryParse(timeoutTask.Result, Nothing),
                                                                                                            CInt(timeoutTask.Result), 30)
                                                                                                            }
                                                                                                         _integrator = New VSCUIntegrator(settings, _logger)
                                                                                                     End Sub)
    End Sub

    Private Sub PurchaseForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Add placeholder text
        SetPlaceholder(TxtPurchaseSendSearch, "Start typing to search...")
        ' Attach placeholder handlers
        AddHandler TxtPurchaseSendSearch.Enter, AddressOf TextBox_Enter
        AddHandler TxtPurchaseSendSearch.Leave, AddressOf TextBox_Leave
        AddHandler DgvPurchaseHeader.SelectionChanged, AddressOf DgvPurchaseHeader_SelectionChanged
        ' Setup grid
        SetupPurchaseHeaderGrid()
        SetupPurchaseItemsGrid()
    End Sub

    Private Sub TxtPurchaseSendSearch_TextChanged(sender As Object, e As EventArgs) Handles TxtPurchaseSendSearch.TextChanged
        ' Prevent filtering when placeholder is active
        If TxtPurchaseSendSearch.ForeColor = Color.Gray Then Exit Sub
        FilterGrid(DgvPurchaseHeader, TxtPurchaseSendSearch.Text.Trim())
    End Sub

    Private Async Sub BtnPurchaseFetchRemote_Click(sender As Object, e As EventArgs) Handles BtnPurchaseFetchRemote.Click
        Try
            ToggleLoader(True, "Fetching data...")
            ' Get integrator settings
            Dim tin = Await _settingsManager.GetSettingAsync("pin")
            Dim bhfId = Await _settingsManager.GetSettingAsync("branch_id")
            Dim lastReqDt = Await _settingsManager.GetSettingAsync("last_purchase_get_dt")
            If String.IsNullOrWhiteSpace(lastReqDt) Then
                lastReqDt = "20180101000000"
            End If
            ' Build request
            Dim req As New PurchaseInfoRequest With {
                .tin = tin,
                .bhfId = bhfId,
                .lastReqDt = lastReqDt
            }
            ' Call integrator
            Dim res = Await _integrator.GetPurchaseAsync(req)

            If res Is Nothing OrElse res.data Is Nothing OrElse res.data.saleList Is Nothing Then
                CustomAlert.ShowAlert(Me, "No purchase data returned.", "Info", CustomAlert.AlertType.Info, CustomAlert.ButtonType.OK)
                Exit Sub
            End If
            Dim purchases = res.data.saleList
            ' Load rows
            For Each p In purchases
                DgvPurchaseHeader.Rows.Add(
                    p.spplrTin,
                    p.spplrNm,
                    p.spplrBhfId,
                    p.spplrInvcNo,
                    p.salesDt,
                    p.totItemCnt,
                    p.totAmt,
                    p.totTaxAmt
                )
            Next
            ' Store a DataTable for searching
            OriginalTables(DgvPurchaseHeader) = (From p In purchases Select p.spplrTin,
                        p.spplrNm,
                        p.spplrBhfId,
                        p.spplrInvcNo,
                        p.salesDt,
                        p.totItemCnt,
                        p.totAmt,
                        p.totTaxAmt
                ).ToDataTable()
            ' Update last request timestamp
            Await _settingsManager.SetSettingAsync("last_purchase_get_dt", DateTime.Now.ToString("yyyyMMddHHmmss"))
            CustomAlert.ShowAlert(Me, $"Loaded {purchases.Count} purchase records.", "Success", CustomAlert.AlertType.Success,
                                    CustomAlert.ButtonType.OK)
        Catch ex As Exception
            CustomAlert.ShowAlert(Me, "Error: " & ex.Message, "Error", CustomAlert.AlertType.Error, CustomAlert.ButtonType.OK)
        Finally
            ToggleLoader(False, "")
        End Try
    End Sub

    Private Sub BtnPurchaseFetch_Click(sender As Object, e As EventArgs) Handles BtnPurchaseFetchLocal.Click
        ' Load header
        Dim dtHeader = _purchaseRepo.GetAll()
        DgvPurchaseHeader.DataSource = dtHeader
        ' Optionally select the first purchase to load items
        If dtHeader.Rows.Count > 0 Then
            Dim firstPurchaseId = Convert.ToInt32(dtHeader.Rows(0)("id"))
            LoadPurchaseItems(firstPurchaseId)
        End If
    End Sub

    Private Sub BtnSavePurchaseInfo_Click(sender As Object, e As EventArgs) Handles BtnSavePurchaseInfo.Click
        Try
            DgvPurchaseHeader.EndEdit()
            DgvPurchaseItems.EndEdit()
            If DgvPurchaseHeader.Rows.Count = 0 OrElse DgvPurchaseHeader.Rows(0).IsNewRow Then
                Throw New Exception("Enter purchase header information.")
            End If
            Dim headerRow = DgvPurchaseHeader.Rows(0)
            Dim invcNo As Integer
            Integer.TryParse(headerRow.Cells("invcNo").Value?.ToString(), invcNo)
            Dim purchase As New PurchaseTransaction With {
                .InvcNo = invcNo,
                .OrgInvcNo = If(IsNumeric(headerRow.Cells("orgInvcNo").Value), Convert.ToInt32(headerRow.Cells("orgInvcNo").Value), 0),
                .RegTyCd = SafeStr(headerRow.Cells("regTyCd")),
                .PchsTyCd = SafeStr(headerRow.Cells("pchsTyCd")),
                .RcptTyCd = SafeStr(headerRow.Cells("rcptTyCd")),
                .PmtTyCd = SafeStr(headerRow.Cells("pmtTyCd")),
                .PchsSttsCd = SafeStr(headerRow.Cells("pchsSttsCd")),
                .CfmDt = SafeStr(headerRow.Cells("cfmDt")),
                .PchsDt = SafeStr(headerRow.Cells("pchsDt")),
                .WrhsDt = SafeStr(headerRow.Cells("wrhsDt")),
                .cnclReqDt = SafeStr(headerRow.Cells("cnclReqDt")),
                .CnclDt = SafeStr(headerRow.Cells("cnclDt")),
                .RfdDt = SafeStr(headerRow.Cells("rfdDt")),
                .Remark = SafeStr(headerRow.Cells("remark")),
                .Items = New List(Of PurchaseTransactionItem)
            }
            Dim itemCount As Integer = 0
            Dim totalTax As Decimal = 0
            Dim totalAmt As Decimal = 0
            For Each row As DataGridViewRow In DgvPurchaseItems.Rows
                If row.IsNewRow Then Continue For
                Dim item As New PurchaseTransactionItem
                item.ItemSeq = If(IsNumeric(row.Cells("itemSeq").Value), Convert.ToInt32(row.Cells("itemSeq").Value), 0)
                item.ItemCd = row.Cells("itemCd").Value?.ToString()
                item.ItemClsCd = row.Cells("itemClsCd").Value?.ToString()
                item.ItemNm = row.Cells("itemNm").Value?.ToString()
                item.pkgUnitCd = row.Cells("pkgUnitCd").Value?.ToString()
                Dim pkgVal As Decimal
                Decimal.TryParse(row.Cells("pkg").Value?.ToString(), pkgVal)
                item.pkg = pkgVal
                item.qtyUnitCd = row.Cells("qtyUnitCd").Value?.ToString()
                Dim qtyVal As Decimal
                Decimal.TryParse(row.Cells("qty").Value?.ToString(), qtyVal)
                item.Qty = qtyVal
                Dim prcVal As Decimal
                Decimal.TryParse(row.Cells("prc").Value?.ToString(), prcVal)
                item.Prc = prcVal
                item.splyAmt = qtyVal * prcVal
                Dim dcRtVal As Decimal
                Decimal.TryParse(row.Cells("dcRt").Value?.ToString(), dcRtVal)
                item.dcRt = dcRtVal
                Dim dcAmtVal As Decimal
                Decimal.TryParse(row.Cells("dcAmt").Value?.ToString(), dcAmtVal)
                item.dcAmt = dcAmtVal
                item.taxblAmt = item.splyAmt - item.dcAmt
                item.TaxTyCd = row.Cells("taxTyCd").Value?.ToString()
                Dim taxVal As Decimal
                Decimal.TryParse(row.Cells("taxAmt").Value?.ToString(), taxVal)
                item.TaxAmt = taxVal
                Dim totVal As Decimal
                Decimal.TryParse(row.Cells("totAmt").Value?.ToString(), totVal)
                item.itemExprDt = SafeStr(row.Cells("itemExprDt"))
                itemCount += 1
                item.TotAmt = totVal
                totalAmt += item.TotAmt
                totalTax += item.TaxAmt
                purchase.Items.Add(item)
            Next
            purchase.TotItemCnt = itemCount
            purchase.TotAmt = totalAmt
            purchase.TotTaxAmt = totalTax
            purchase.TotTaxblAmt = totalAmt - totalTax
            _purchaseRepo.Insert(purchase)
            CustomAlert.ShowAlert(Me, "Purchase saved successfully.", "Success", CustomAlert.AlertType.Success, CustomAlert.ButtonType.OK)
            ResetGridDataSource(DgvPurchaseHeader)
            ResetGridDataSource(DgvPurchaseItems)
        Catch ex As Exception
            CustomAlert.ShowAlert(Me, ex.Message, "Error", CustomAlert.AlertType.Error, CustomAlert.ButtonType.OK)
        End Try
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        ' Header grid
        ResetGridDataSource(DgvPurchaseHeader)
        ' Items grid
        ResetGridDataSource(DgvPurchaseItems)
        TxtPurchaseSendSearch.Clear()
    End Sub

    Private Sub DgvPurchaseHeader_SelectionChanged(sender As Object, e As EventArgs) Handles DgvPurchaseHeader.SelectionChanged
        If DgvPurchaseHeader.CurrentRow Is Nothing Then Exit Sub
        ' ignore the new row
        If DgvPurchaseHeader.CurrentRow.IsNewRow Then Exit Sub
        ' ignore rows without id
        Dim cellValue = DgvPurchaseHeader.CurrentRow.Cells("id").Value
        If cellValue Is Nothing OrElse IsDBNull(cellValue) Then Exit Sub
        Dim purchaseId As Integer
        If Not Integer.TryParse(cellValue.ToString(), purchaseId) Then Exit Sub
        LoadPurchaseItems(purchaseId)
    End Sub

    Private Async Sub BtnUploadPurchase_Click(sender As Object, e As EventArgs) Handles BtnUploadPurchase.Click
        DgvPurchaseHeader.EndEdit()
        ToggleLoader(True, "Uploading...")
        Dim successCount As Integer = 0
        Dim failCount As Integer = 0
        For Each row As DataGridViewRow In DgvPurchaseHeader.Rows
            If row.IsNewRow Then Continue For
            Dim isChecked = If(row.Cells("chkSelect").Value, False)
            If Not isChecked Then Continue For
            If CBool(row.Cells("is_uploaded").Value) Then Continue For
            Dim id = CInt(row.Cells("id").Value)
            Dim ok = Await UploadPurchase(id)
            If ok Then
                successCount += 1
            Else
                failCount += 1
            End If
        Next
        ToggleLoader(False, "")
        BtnPurchaseFetch_Click(Nothing, Nothing)
        'single summary alert
        CustomAlert.ShowAlert(Me, $"Upload complete. Success: {successCount}, Failed: {failCount}", "Upload Result",
        CustomAlert.AlertType.Info,
        CustomAlert.ButtonType.OK)
    End Sub

    'Helpers
    Private Sub SetupPurchaseHeaderGrid()
        With DgvPurchaseHeader
            .Columns.Clear()
            .AllowUserToAddRows = True
            .AutoGenerateColumns = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            Dim colId As New DataGridViewTextBoxColumn With {
                .Name = "id",
                .HeaderText = "ID",
                .DataPropertyName = "id",
                .Visible = False
            }
            .Columns.Add(colId)
            ' Checkbox for selection
            .Columns.Add(New DataGridViewCheckBoxColumn() With {.Name = "chkSelect", .HeaderText = "Select"})
            ' Invoice No
            .Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "invcNo", .HeaderText = "Invoice No", .DataPropertyName = "invc_no"})
            'Orginal Invoice No
            .Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "orgInvcNo", .HeaderText = "Original Invoice No", .DataPropertyName = "org_invc_no"})
            ' Registration Type
            .Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "regTyCd", .HeaderText = "Registration Type", .DataPropertyName = "reg_ty_cd"})
            ' Purchase Type
            .Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "pchsTyCd", .HeaderText = "Purchase Type", .DataPropertyName = "pchs_ty_cd"})
            ' Receipt Type
            .Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "rcptTyCd", .HeaderText = "Receipt Type", .DataPropertyName = "rcpt_ty_cd"})
            ' Payment Type
            .Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "pmtTyCd", .HeaderText = "Payment Type", .DataPropertyName = "pmt_ty_cd"})
            ' Purchase Status
            .Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "pchsSttsCd", .HeaderText = "Purchase Status", .DataPropertyName = "pchs_stts_cd"})
            ' Confirmation Date
            .Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "cfmDt", .HeaderText = "Confirmation Date", .DataPropertyName = "cfm_dt", .ValueType = GetType(String)})
            ' Purchase Date
            .Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "pchsDt", .HeaderText = "Purchase Date", .DataPropertyName = "pchs_dt", .ValueType = GetType(String)})
            'Warehouse Date
            .Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "wrhsDt", .HeaderText = "Warehouse Date", .DataPropertyName = "wrhs_dt", .ValueType = GetType(String)})
            ' Cancellation Reason
            .Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "cnclReqDt", .HeaderText = "Cancel Requested", .DataPropertyName = "cncl_req_dt"})
            'Cancellation Date
            .Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "cnclDt", .HeaderText = "Cancellation Date", .DataPropertyName = "cncl_dt", .ValueType = GetType(String)})
            'Refund Date
            .Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "rfdDt", .HeaderText = "Refund Date", .DataPropertyName = "rfd_dt", .ValueType = GetType(String)})
            ' Total Item Count
            .Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "totItemCnt", .HeaderText = "Total Items", .DataPropertyName = "tot_item_cnt", .ReadOnly = True})
            ' Total Taxable Amount
            .Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "totTaxblAmt", .HeaderText = "Total Taxable Amt", .DataPropertyName = "tot_taxbl_amt", .ReadOnly = True})
            ' Total Tax Amount
            .Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "totTaxAmt", .HeaderText = "Total Tax Amt", .DataPropertyName = "tot_tax_amt", .ReadOnly = True})
            ' Remark
            .Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "remark", .HeaderText = "Remark", .DataPropertyName = "remark"})
            ' Uploaded status (read-only)
            .Columns.Add(New DataGridViewCheckBoxColumn() With {.Name = "is_uploaded", .HeaderText = "Uploaded", .DataPropertyName = "is_uploaded", .ReadOnly = True})
        End With
    End Sub

    Private Async Sub SetupPurchaseItemsGrid()
        With DgvPurchaseItems
            .Columns.Clear()
            .AllowUserToAddRows = True
            .AutoGenerateColumns = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            'Item sequence
            .Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "itemSeq", .HeaderText = "Seq", .DataPropertyName = "item_seq"})
            'Item code
            .Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "itemCd", .HeaderText = "Item Code", .DataPropertyName = "item_cd"})
            ' Item Class ComboBox
            Dim itemClsColumn As New DataGridViewComboBoxColumn With {
                .Name = "itemClsCd",
                .HeaderText = "Item Class",
                .DataPropertyName = "item_cls_cd",
                .DisplayMember = "itemClsNm",
                .ValueMember = "itemClsCd"
            }
            Dim clsRepo As New ItemClassificationRepository(_connString)
            itemClsColumn.DataSource = clsRepo.GetAll()
            .Columns.Add(itemClsColumn)
            ' Item Name
            .Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "itemNm", .HeaderText = "Item Name", .DataPropertyName = "item_nm"})
            ' Package Unit ComboBox
            Dim pkgUnitCol As New DataGridViewComboBoxColumn With {
                .Name = "pkgUnitCd",
                .HeaderText = "Package Unit",
                .DataPropertyName = "pkg_unit_cd",
                .DisplayMember = "CodeName",
                .ValueMember = "Code"
            }
            Dim pkgUnitRepo As New PackagingUnitRepository(_connString)
            pkgUnitCol.DataSource = Await pkgUnitRepo.GetAll()
            .Columns.Add(pkgUnitCol)
            ' Package Quantity
            .Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "pkg", .HeaderText = "Package Qty", .DataPropertyName = "pkg"})
            ' Quantity Unit ComboBox
            Dim qtyUnitCol As New DataGridViewComboBoxColumn With {
                .Name = "qtyUnitCd",
                .HeaderText = "Quantity Unit",
                .DataPropertyName = "qty_unit_cd",
                .DisplayMember = "CodeName",
                .ValueMember = "Code"
            }
            Dim qtyUnitRepo As New UnitOfQuantityRepository(_connString)
            qtyUnitCol.DataSource = Await qtyUnitRepo.GetAll()
            .Columns.Add(qtyUnitCol)
            ' Quantity
            .Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "qty", .HeaderText = "Qty", .DataPropertyName = "qty"})
            'Price
            .Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "prc", .HeaderText = "Price", .DataPropertyName = "prc"})
            ' Supply Amount
            .Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "splyAmt", .HeaderText = "Supply Amt", .DataPropertyName = "sply_amt"})
            ' Discount Rate
            .Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "dcRt", .HeaderText = "Discount Rate", .DataPropertyName = "dc_rt"})
            ' Discount Amount
            .Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "dcAmt", .HeaderText = "Discount Amt", .DataPropertyName = "dc_amt"})
            ' Taxable Amount
            .Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "taxblAmt", .HeaderText = "Taxable Amt", .DataPropertyName = "taxbl_amt"})
            ' Tax Type ComboBox
            Dim taxCol As New DataGridViewComboBoxColumn With {
                .Name = "taxTyCd",
                .HeaderText = "Tax Type",
                .DataPropertyName = "tax_ty_cd",
                .DisplayMember = "CodeName",
                .ValueMember = "Code"
            }
            Dim taxTypeRepo As New TaxTypeRepository(_connString)
            taxCol.DataSource = taxTypeRepo.GetAll()
            .Columns.Add(taxCol)
            ' Tax Amount
            .Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "taxAmt", .HeaderText = "Tax Amt", .DataPropertyName = "tax_amt"})
            ' Total Amount
            .Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "totAmt", .HeaderText = "Total", .DataPropertyName = "tot_amt"})
            ' Item Expiration Date
            .Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "itemExprDt", .HeaderText = "Expiration Date", .DataPropertyName = "item_expr_dt"})
        End With
    End Sub

    Private Sub LoadPurchaseItems(purchaseId As Integer)
        Dim dtItems = _purchaseRepo.GetItemsByPurchaseId(purchaseId)
        DgvPurchaseItems.DataSource = dtItems
    End Sub

    Private Async Function UploadPurchase(id As Integer) As Task(Of Boolean)
        Dim purchase = _purchaseRepo.GetById(id)
        Dim request = BuildRequest(purchase)
        Dim response = Await _integrator.SavePurchaseAsync(request)
        If response IsNot Nothing AndAlso response.resultCd = "000" Then
            _purchaseRepo.MarkAsUploaded(id)
            Return True
        End If
        Return False
    End Function

    Private Function BuildRequest(purchase As PurchaseTransaction) As PurchaseTransactionRequest
        Dim taxblA As Decimal = 0
        Dim taxblB As Decimal = 0
        Dim taxblC As Decimal = 0
        Dim taxblD As Decimal = 0
        Dim taxblE As Decimal = 0
        Dim taxAmtA As Decimal = 0
        Dim taxAmtB As Decimal = 0
        Dim taxAmtC As Decimal = 0
        Dim taxAmtD As Decimal = 0
        Dim taxAmtE As Decimal = 0
        ComputeTaxBuckets(purchase.Items, taxblA, taxblB, taxblC, taxblD, taxblE, taxAmtA, taxAmtB, taxAmtC, taxAmtD, taxAmtE)
        Dim req As New PurchaseTransactionRequest With {
            .tin = _tin,
            .bhfId = _branchId,
            .spplrTin = purchase.SpplrTin,
            .invcNo = purchase.InvcNo,
            .orgInvcNo = purchase.OrgInvcNo,
            .spplrBhfId = purchase.SpplrBhfId,
            .spplrNm = purchase.SpplrNm,
            .spplrInvcNo = purchase.SpplrInvcNo,
            .spplrSdcId = purchase.SpplrSdcId,
            .regTyCd = purchase.RegTyCd,
            .pchsTyCd = purchase.PchsTyCd,
            .rcptTyCd = purchase.RcptTyCd,
            .pmtTyCd = purchase.PmtTyCd,
            .pchsSttsCd = purchase.PchsSttsCd,
            .cfmDt = FormatEtimsDate(purchase.CfmDt),
            .pchsDt = FormatDate8(purchase.PchsDt),
            .wrhsDt = FormatEtimsDate(purchase.WrhsDt),
            .cnclReqDt = FormatEtimsDate(purchase.cnclReqDt),
            .cnclDt = FormatEtimsDate(purchase.CnclDt),
            .rfdDt = FormatEtimsDate(purchase.RfdDt),
            .totItemCnt = purchase.Items.Count,
            .taxblAmtA = taxblA,
            .taxblAmtB = taxblB,
            .taxblAmtC = taxblC,
            .taxblAmtD = taxblD,
            .taxblAmtE = taxblE,
            .taxAmtA = taxAmtA,
            .taxAmtB = taxAmtB,
            .taxAmtC = taxAmtC,
            .taxAmtD = taxAmtD,
            .taxAmtE = taxAmtE,
            .taxRtA = 0,
            .taxRtB = 16,
            .taxRtC = 0,
            .taxRtD = 0,
            .taxRtE = 8,
            .totTaxblAmt = purchase.TotTaxblAmt,
            .totTaxAmt = purchase.TotTaxAmt,
            .totAmt = purchase.TotAmt,
            .remark = purchase.Remark,
            .regrNm = "Admin",
            .regrId = "Admin",
            .modrNm = "Admin",
            .modrId = "Admin",
            .itemList = New List(Of PurchaseTransactionItem)
        }
        For Each it In purchase.Items
            req.itemList.Add(New PurchaseTransactionItem With {
                .ItemSeq = it.ItemSeq,
                .ItemCd = it.ItemCd,
                .ItemClsCd = it.ItemClsCd,
                .ItemNm = it.ItemNm,
                .pkgUnitCd = it.pkgUnitCd,
                .pkg = it.pkg,
                .qtyUnitCd = it.qtyUnitCd,
                .Qty = it.Qty,
                .Prc = it.Prc,
                .splyAmt = it.splyAmt,
                .dcRt = it.dcRt,
                .dcAmt = it.dcAmt,
                .taxblAmt = it.taxblAmt,
                .TaxTyCd = it.TaxTyCd,
                .TaxAmt = it.TaxAmt,
                .TotAmt = it.TotAmt,
                .itemExprDt = FormatDate8(it.itemExprDt)
            })
        Next
        Return req
    End Function

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

    Private Sub FilterGrid(grid As DataGridView, search As String)
        If Not OriginalTables.ContainsKey(grid) Then Exit Sub
        Dim dt As DataTable = OriginalTables(grid)
        Dim dv As DataView = dt.DefaultView
        If String.IsNullOrWhiteSpace(search) Then
            dv.RowFilter = ""
        Else
            Dim safeSearch As String = search.Replace("'", "''")
            Dim filters As New List(Of String)
            For Each col As DataColumn In dt.Columns
                filters.Add($"CONVERT([{col.ColumnName}], 'System.String') LIKE '%{safeSearch}%'")
            Next
            dv.RowFilter = String.Join(" OR ", filters)
        End If
        ' Clear rows but keep columns
        grid.Rows.Clear()
        ' Repopulate dynamically
        For Each rowView As DataRowView In dv
            Dim values As New List(Of Object)
            For Each col As DataGridViewColumn In grid.Columns
                ' Skip checkbox or unbound columns
                If dt.Columns.Contains(col.Name) Then
                    values.Add(rowView(col.Name))
                Else
                    ' Set default value for unbound column (e.g., checkbox)
                    values.Add(If(TypeOf col Is DataGridViewCheckBoxColumn, False, Nothing))
                End If
            Next
            grid.Rows.Add(values.ToArray())
        Next
    End Sub

    'Toggler
    Private Sub ToggleLoader(isLoading As Boolean, message As String)
        Loader.Visible = isLoading
        Loader.Text = message
        BtnPurchaseFetchLocal.Enabled = Not isLoading
        BtnPurchaseFetchRemote.Enabled = Not isLoading
        BtnUploadPurchase.Enabled = Not isLoading
        BtnReset.Enabled = Not isLoading
    End Sub

    'Reset data source of a grid
    Private Sub ResetGridDataSource(grid As DataGridView)
        If grid.DataSource IsNot Nothing Then
            grid.DataSource = Nothing
        Else
            grid.Rows.Clear()
        End If
    End Sub

    'Safely get string value from a cell, handling nulls and DBNull
    Private Function SafeStr(cell As DataGridViewCell) As String
        If cell Is Nothing OrElse cell.Value Is Nothing OrElse IsDBNull(cell.Value) Then
            Return ""
        End If
        Return cell.Value.ToString()
    End Function

    Private Function FormatDate8(value As String) As String
        If String.IsNullOrWhiteSpace(value) Then Return ""
        Dim dt As DateTime
        If DateTime.TryParse(value, dt) Then
            Return dt.ToString("yyyyMMdd")
        End If
        Return value
    End Function

    Public Function FormatEtimsDate(dt As DateTime) As String
        Return dt.ToString("yyyyMMddHHmmss")
    End Function


    Private Sub ComputeTaxBuckets(
    items As List(Of PurchaseTransactionItem),
    ByRef taxblA As Decimal,
    ByRef taxblB As Decimal,
    ByRef taxblC As Decimal,
    ByRef taxblD As Decimal,
    ByRef taxblE As Decimal,
    ByRef taxAmtA As Decimal,
    ByRef taxAmtB As Decimal,
    ByRef taxAmtC As Decimal,
    ByRef taxAmtD As Decimal,
    ByRef taxAmtE As Decimal)

        For Each it In items
            Select Case it.TaxTyCd?.Trim().ToUpper()
                Case "A"
                    taxblA += it.taxblAmt
                    taxAmtA += it.TaxAmt

                Case "B"
                    taxblB += it.taxblAmt
                    taxAmtB += it.TaxAmt

                Case "C"
                    taxblC += it.taxblAmt
                    taxAmtC += it.TaxAmt

                Case "D"
                    taxblD += it.taxblAmt
                    taxAmtD += it.TaxAmt

                Case "E"
                    taxblE += it.taxblAmt
                    taxAmtE += it.TaxAmt
            End Select
        Next
    End Sub


End Class