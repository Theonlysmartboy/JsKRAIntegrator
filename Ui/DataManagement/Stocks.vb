Imports System.ComponentModel
Imports Core.Config
Imports Core.Logging
Imports Core.Models.Item.Stock
Imports Core.Services
Imports Ui.Helpers
Imports Ui.Repo
Imports Ui.Repo.ItemRepo
Imports Ui.Repo.StockRepo

Public Class Stocks
    Private _integrator As VSCUIntegrator
    Private _conn As String
    Private _settingsManager As SettingsManager
    Private _logger As Logger
    Private _stockMoveRepo As IStockMovementRepository

    Public Sub New(connectionString As String)
        InitializeComponent()
        _conn = connectionString
        _settingsManager = New SettingsManager(connectionString)
        Dim repo As New LogRepository(connectionString)
        _logger = New Logger(repo)
        _stockMoveRepo = New StockMovementRepository(connectionString)
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

    Private Sub InsuranceForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DgvStockMoveHeader.AutoGenerateColumns = False
        DgvStockMoveItems.AutoGenerateColumns = False
        SetupHeaderGridColumns()
        SetupItemsGridColumns()
        LoadStockMoveGrid()
    End Sub

    Private Sub DgvStockMoveHeader_SelectionChanged(sender As Object, e As EventArgs) Handles DgvStockMoveHeader.SelectionChanged
        If DgvStockMoveHeader.CurrentRow Is Nothing Then Exit Sub
        If DgvStockMoveHeader.CurrentRow.Cells("id").Value Is Nothing Then Exit Sub
        Dim stockMoveId As Integer = Convert.ToInt32(DgvStockMoveHeader.CurrentRow.Cells("id").Value)
        DgvStockMoveItems.DataSource = New BindingList(Of StockMovementItem)(_stockMoveRepo.GetItemsByStockMoveId(stockMoveId))
        DgvStockMoveItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub

    Private Async Sub BtnFetchStockMove_Click(sender As Object, e As EventArgs) Handles BtnFetchStockMove.Click
        Try
            BtnFetchStockMove.Enabled = False
            Loader.Visible = True
            Loader.Text = "Fetching ..."
            Dim tin = Await _settingsManager.GetSettingAsync("pin")
            Dim bhfId = Await _settingsManager.GetSettingAsync("branch_id")
            Dim lastReqDt = Await _settingsManager.GetSettingAsync("last_stock_move_dt")
            If String.IsNullOrWhiteSpace(lastReqDt) Then
                lastReqDt = "20180101000000"
            End If
            Dim req As New StockMovementRequest With {
                .tin = tin,
                .bhfId = bhfId,
                .lastReqDt = lastReqDt
            }
            Dim res = Await _integrator.GetStockMoveAsync(req)
            If res Is Nothing OrElse res.data Is Nothing Then
                CustomAlert.ShowAlert(Me, "Failed to fetch stock movement data.", "Error", CustomAlert.AlertType.Error, CustomAlert.ButtonType.OK)
                Exit Sub
            End If
            For Each MoveItem In res.data.stockList
                _stockMoveRepo.Insert(MoveItem)
            Next
            Await _settingsManager.SetSettingAsync("last_stock_move_dt", DateTime.Now.ToString("yyyyMMddHHmmss"))
            LoadStockMoveGrid()
            CustomAlert.ShowAlert(Me, "Stock movement data fetched and saved successfully.", "Success", CustomAlert.AlertType.Success, CustomAlert.ButtonType.OK)
        Catch ex As Exception
            CustomAlert.ShowAlert(Me, "An error occurred: " & ex.Message, "Error", CustomAlert.AlertType.Error, CustomAlert.ButtonType.OK)
        Finally
            BtnFetchStockMove.Enabled = True
            Loader.Visible = False
            Loader.Text = "Loading ..."
        End Try
    End Sub

    Private Async Sub BtnSaveStockMove_Click(sender As Object, e As EventArgs) Handles BtnSaveStockMove.Click
        Try
            BtnSaveStockMove.Enabled = False
            Loader.Visible = True
            Loader.Text = "Saving ..."
            ' Gather header info
            Dim header As New StockMovementRecord With {
                .custTin = Nothing,
                .custBhfId = Nothing,
                .sarNo = 1,
                .ocrnDt = DateTime.Now.ToString("yyyyMMdd"),
                .totItemCnt = DgvStockMoveItems.Rows.Count,
                .totTaxblAmt = 0,
                .totTaxAmt = 0,
                .totAmt = 0,
                .remark = Nothing,
                .itemList = New List(Of StockMovementItem)
            }
            ' Gather items from DgvStockMoveItems
            For Each row As DataGridViewRow In DgvStockMoveItems.Rows
                If row.IsNewRow Then Continue For
                Dim item As New StockMovementItem With {
                    .itemSeq = Convert.ToInt32(row.Cells("item_seq").Value),
                    .itemCd = row.Cells("item_cd").Value.ToString(),
                    .itemClsCd = row.Cells("item_cls_cd").Value.ToString(),
                    .itemNm = row.Cells("item_nm").Value.ToString(),
                    .bcd = If(row.Cells("bcd").Value, Nothing),
                    .pkgUnitCd = row.Cells("pkg_unit_cd").Value.ToString(),
                    .pkg = Convert.ToDecimal(row.Cells("pkg").Value),
                    .qtyUnitCd = row.Cells("qty_unit_cd").Value.ToString(),
                    .qty = Convert.ToDecimal(row.Cells("qty").Value),
                    .itemExprDt = If(row.Cells("item_expr_dt").Value, Nothing),
                    .prc = Convert.ToDecimal(row.Cells("prc").Value),
                    .splyAmt = Convert.ToDecimal(row.Cells("sply_amt").Value),
                    .totDcAmt = Convert.ToDecimal(row.Cells("tot_dc_amt").Value),
                    .taxblAmt = Convert.ToDecimal(row.Cells("taxbl_mt").Value),
                    .taxTyCd = row.Cells("taxTyCd").Value.ToString(),
                    .taxAmt = Convert.ToDecimal(row.Cells("tax_amt").Value),
                    .totAmt = Convert.ToDecimal(row.Cells("tot_amt").Value)
                }
                header.itemList.Add(item)
                ' Add to header totals
                header.totTaxblAmt += item.taxblAmt
                header.totTaxAmt += item.taxAmt
                header.totAmt += item.totAmt
            Next
            ' Save to local DB
            _stockMoveRepo.Insert(header)
            ' Upload immediately
            Loader.Text = "Uploading ..."
            Dim request As New StockMovementSaveRequest With {
                .tin = Await _settingsManager.GetSettingAsync("pin"),
                .bhfId = Await _settingsManager.GetSettingAsync("branch_id"),
                .sarNo = header.sarNo,
                .orgSarNo = header.sarNo,
                .regTyCd = "M",
                .custTin = header.custTin,
                .custNm = Nothing,
                .custBhfId = header.custBhfId,
                .sarTyCd = "11",
                .ocrnDt = header.ocrnDt,
                .totItemCnt = header.totItemCnt,
                .totTaxblAmt = header.totTaxblAmt,
                .totTaxAmt = header.totTaxAmt,
                .totAmt = header.totAmt,
                .remark = header.remark,
                .regrId = "Admin",
                .regrNm = "Admin",
                .modrId = "Admin",
                .modrNm = "Admin",
                .itemList = header.itemList
            }
            Dim res = Await _integrator.SaveStockMoveAsync(request)
            If res.resultCd = "000" Then
                CustomAlert.ShowAlert(Me, "Stock movement saved and uploaded successfully!", "Success", CustomAlert.AlertType.Success, CustomAlert.ButtonType.OK)
            Else
                CustomAlert.ShowAlert(Me, "Saved locally but upload failed: " & res.resultMsg, "Warning", CustomAlert.AlertType.Warning, CustomAlert.ButtonType.OK)
            End If
            LoadStockMoveGrid()
        Catch ex As Exception
            CustomAlert.ShowAlert(Me, "Error: " & ex.Message, "Error", CustomAlert.AlertType.Error, CustomAlert.ButtonType.OK)
        Finally
            BtnSaveStockMove.Enabled = True
            Loader.Visible = False
            Loader.Text = "Loading ..."
        End Try
    End Sub

    Private Sub SetupHeaderGridColumns()
        DgvStockMoveHeader.Columns.Clear()
        DgvStockMoveHeader.Columns.Add("id", "ID")
        DgvStockMoveHeader.Columns("id").Visible = False
        DgvStockMoveHeader.Columns.Add("cust_tin", "Customer TIN")
        DgvStockMoveHeader.Columns.Add("cust_bhf_id", "Branch ID")
        DgvStockMoveHeader.Columns.Add("sar_no", "SAR No")
        DgvStockMoveHeader.Columns.Add("sar_no", "SAR No")
        DgvStockMoveHeader.Columns.Add("ocrn_dt", "Occurrence Date")
        DgvStockMoveHeader.Columns.Add("tot_item_cnt", "Total Items")
        DgvStockMoveHeader.Columns.Add("tot_taxbl_amt", "Taxable Amount")
        DgvStockMoveHeader.Columns.Add("tot_tax_amt", "Tax Amount")
        DgvStockMoveHeader.Columns.Add("tot_amt", "Total Amount")
        DgvStockMoveHeader.AllowUserToAddRows = True
        DgvStockMoveHeader.ReadOnly = False
    End Sub

    Private Sub SetupItemsGridColumns()
        DgvStockMoveItems.Columns.Clear()
        ' Basic item info
        DgvStockMoveItems.Columns.Add("item_seq", "Seq")
        DgvStockMoveItems.Columns.Add("item_cd", "Item Code")
        ' Item classification combo
        Dim itemClsColumn As New DataGridViewComboBoxColumn With {
            .Name = "item_cls_cd",
            .HeaderText = "Item Class",
            .DataPropertyName = "item_cls_cd",
            .DisplayMember = "itemClsNm",
            .ValueMember = "itemClsCd"
        }
        Dim clsRepo As New ItemClassificationRepository(_conn)
        itemClsColumn.DataSource = clsRepo.GetAll()
        DgvStockMoveItems.Columns.Add(itemClsColumn)
        ' Other item details
        DgvStockMoveItems.Columns.Add("item_nm", "Item Name")
        DgvStockMoveItems.Columns.Add("bcd", "Barcode")
        DgvStockMoveItems.Columns.Add("pkg_unit_cd", "Package Unit")
        DgvStockMoveItems.Columns.Add("pkg", "Package Qty")
        DgvStockMoveItems.Columns.Add("qty_unit_cd", "Quantity Unit")
        DgvStockMoveItems.Columns.Add("qty", "Quantity")
        DgvStockMoveItems.Columns.Add("item_expr_dt", "Expiry Date")
        DgvStockMoveItems.Columns.Add("prc", "Price")
        DgvStockMoveItems.Columns.Add("sply_amt", "Supply Amount")
        DgvStockMoveItems.Columns.Add("tot_dc_amt", "Total Discount Amount")
        DgvStockMoveItems.Columns.Add("taxbl_amt", "Taxable Amount")
        ' Tax type combo
        Dim taxCol As New DataGridViewComboBoxColumn With {
            .Name = "tax_ty_cd",
            .HeaderText = "Tax Type",
            .DataPropertyName = "tax_ty_cd",
            .DisplayMember = "CodeName",
            .ValueMember = "Code"
        }
        Dim taxTypeRepo As New TaxTypeRepository(_conn)
        taxCol.DataSource = taxTypeRepo.GetAll()
        DgvStockMoveItems.Columns.Add(taxCol)
        DgvStockMoveItems.Columns.Add("tax_amt", "Tax Amount")
        DgvStockMoveItems.Columns.Add("tot_amt", "Total Amount")
        ' Checkbox for upload status
        Dim colUpload As New DataGridViewCheckBoxColumn With {
            .Name = "to_upload",
            .HeaderText = "To Upload",
            .TrueValue = True,
            .FalseValue = False
        }
        DgvStockMoveItems.Columns.Add(colUpload)
        ' Grid settings
        DgvStockMoveItems.AllowUserToAddRows = True
        DgvStockMoveItems.ReadOnly = False
        DgvStockMoveItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub

    Private Sub LoadStockMoveGrid()
        DgvStockMoveHeader.DataSource = _stockMoveRepo.GetAllHeaders()
        DgvStockMoveHeader.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub
End Class