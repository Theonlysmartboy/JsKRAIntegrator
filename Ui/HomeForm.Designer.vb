<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class HomeForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.InitializationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InitializeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DataMgtToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SyncToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ItemClassificationsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.TaxTypesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SalesReceiptTypeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProductTypesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TransactionTypeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PurchaseReceiptTypeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StockInOutTypeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RegistrationTypeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.CountriesCodesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PaymentTypesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PurchaseStatusToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TaxpayerStatusToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImportItemStatusToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BranchStatusToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.UnitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PackagingUnitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TransactionProgressToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InventoryAdjustmentReasonToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CreditNoteReasonToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.CurrenciesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BanksToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TaxOfficesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LocalesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CategoryLevelsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProductManagementToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SalesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PurchasesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSettings = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripLogs = New System.Windows.Forms.ToolStripMenuItem()
        Me.NoticesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LblStatus = New System.Windows.Forms.Label()
        Me.Loader = New JsToolBox.Loaders.DualRingLoader()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.InitializationToolStripMenuItem, Me.DataMgtToolStripMenuItem, Me.ProductManagementToolStripMenuItem, Me.SalesToolStripMenuItem, Me.PurchasesToolStripMenuItem, Me.ToolStripSettings, Me.ToolStripLogs, Me.NoticesToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(800, 24)
        Me.MenuStrip1.TabIndex = 6
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'InitializationToolStripMenuItem
        '
        Me.InitializationToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.InitializeToolStripMenuItem})
        Me.InitializationToolStripMenuItem.Name = "InitializationToolStripMenuItem"
        Me.InitializationToolStripMenuItem.Size = New System.Drawing.Size(83, 20)
        Me.InitializationToolStripMenuItem.Text = "Initialization"
        '
        'InitializeToolStripMenuItem
        '
        Me.InitializeToolStripMenuItem.Name = "InitializeToolStripMenuItem"
        Me.InitializeToolStripMenuItem.Size = New System.Drawing.Size(117, 22)
        Me.InitializeToolStripMenuItem.Text = "Initialize"
        '
        'DataMgtToolStripMenuItem
        '
        Me.DataMgtToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SyncToolStripMenuItem, Me.ToolStripSeparator1, Me.ItemClassificationsToolStripMenuItem, Me.ToolStripSeparator2, Me.TaxTypesToolStripMenuItem, Me.SalesReceiptTypeToolStripMenuItem, Me.ProductTypesToolStripMenuItem, Me.TransactionTypeToolStripMenuItem, Me.PurchaseReceiptTypeToolStripMenuItem, Me.StockInOutTypeToolStripMenuItem, Me.RegistrationTypeToolStripMenuItem, Me.ToolStripSeparator4, Me.CountriesCodesToolStripMenuItem, Me.PaymentTypesToolStripMenuItem, Me.PurchaseStatusToolStripMenuItem, Me.TaxpayerStatusToolStripMenuItem, Me.ImportItemStatusToolStripMenuItem, Me.BranchStatusToolStripMenuItem, Me.ToolStripSeparator3, Me.UnitToolStripMenuItem, Me.PackagingUnitToolStripMenuItem, Me.TransactionProgressToolStripMenuItem, Me.InventoryAdjustmentReasonToolStripMenuItem, Me.CreditNoteReasonToolStripMenuItem, Me.ToolStripSeparator5, Me.CurrenciesToolStripMenuItem, Me.BanksToolStripMenuItem, Me.TaxOfficesToolStripMenuItem, Me.LocalesToolStripMenuItem, Me.CategoryLevelsToolStripMenuItem})
        Me.DataMgtToolStripMenuItem.Name = "DataMgtToolStripMenuItem"
        Me.DataMgtToolStripMenuItem.Size = New System.Drawing.Size(117, 20)
        Me.DataMgtToolStripMenuItem.Text = "Data Management"
        '
        'SyncToolStripMenuItem
        '
        Me.SyncToolStripMenuItem.Name = "SyncToolStripMenuItem"
        Me.SyncToolStripMenuItem.Size = New System.Drawing.Size(230, 22)
        Me.SyncToolStripMenuItem.Text = "Sync Codes"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(227, 6)
        '
        'ItemClassificationsToolStripMenuItem
        '
        Me.ItemClassificationsToolStripMenuItem.Name = "ItemClassificationsToolStripMenuItem"
        Me.ItemClassificationsToolStripMenuItem.Size = New System.Drawing.Size(230, 22)
        Me.ItemClassificationsToolStripMenuItem.Text = "Item Classifications"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(227, 6)
        '
        'TaxTypesToolStripMenuItem
        '
        Me.TaxTypesToolStripMenuItem.Name = "TaxTypesToolStripMenuItem"
        Me.TaxTypesToolStripMenuItem.Size = New System.Drawing.Size(230, 22)
        Me.TaxTypesToolStripMenuItem.Text = "Tax Types"
        '
        'SalesReceiptTypeToolStripMenuItem
        '
        Me.SalesReceiptTypeToolStripMenuItem.Name = "SalesReceiptTypeToolStripMenuItem"
        Me.SalesReceiptTypeToolStripMenuItem.Size = New System.Drawing.Size(230, 22)
        Me.SalesReceiptTypeToolStripMenuItem.Text = "Sales Receipt Type"
        '
        'ProductTypesToolStripMenuItem
        '
        Me.ProductTypesToolStripMenuItem.Name = "ProductTypesToolStripMenuItem"
        Me.ProductTypesToolStripMenuItem.Size = New System.Drawing.Size(230, 22)
        Me.ProductTypesToolStripMenuItem.Text = "Product Types"
        '
        'TransactionTypeToolStripMenuItem
        '
        Me.TransactionTypeToolStripMenuItem.Name = "TransactionTypeToolStripMenuItem"
        Me.TransactionTypeToolStripMenuItem.Size = New System.Drawing.Size(230, 22)
        Me.TransactionTypeToolStripMenuItem.Text = "Transaction Types"
        '
        'PurchaseReceiptTypeToolStripMenuItem
        '
        Me.PurchaseReceiptTypeToolStripMenuItem.Name = "PurchaseReceiptTypeToolStripMenuItem"
        Me.PurchaseReceiptTypeToolStripMenuItem.Size = New System.Drawing.Size(230, 22)
        Me.PurchaseReceiptTypeToolStripMenuItem.Text = "Purchase Receipt Type"
        '
        'StockInOutTypeToolStripMenuItem
        '
        Me.StockInOutTypeToolStripMenuItem.Name = "StockInOutTypeToolStripMenuItem"
        Me.StockInOutTypeToolStripMenuItem.Size = New System.Drawing.Size(230, 22)
        Me.StockInOutTypeToolStripMenuItem.Text = "Stock In out type"
        '
        'RegistrationTypeToolStripMenuItem
        '
        Me.RegistrationTypeToolStripMenuItem.Name = "RegistrationTypeToolStripMenuItem"
        Me.RegistrationTypeToolStripMenuItem.Size = New System.Drawing.Size(230, 22)
        Me.RegistrationTypeToolStripMenuItem.Text = "Registration Type"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(227, 6)
        '
        'CountriesCodesToolStripMenuItem
        '
        Me.CountriesCodesToolStripMenuItem.Name = "CountriesCodesToolStripMenuItem"
        Me.CountriesCodesToolStripMenuItem.Size = New System.Drawing.Size(230, 22)
        Me.CountriesCodesToolStripMenuItem.Text = "Countries Codes"
        '
        'PaymentTypesToolStripMenuItem
        '
        Me.PaymentTypesToolStripMenuItem.Name = "PaymentTypesToolStripMenuItem"
        Me.PaymentTypesToolStripMenuItem.Size = New System.Drawing.Size(230, 22)
        Me.PaymentTypesToolStripMenuItem.Text = "Payment Methods"
        '
        'PurchaseStatusToolStripMenuItem
        '
        Me.PurchaseStatusToolStripMenuItem.Name = "PurchaseStatusToolStripMenuItem"
        Me.PurchaseStatusToolStripMenuItem.Size = New System.Drawing.Size(230, 22)
        Me.PurchaseStatusToolStripMenuItem.Text = "Purchase Status"
        '
        'TaxpayerStatusToolStripMenuItem
        '
        Me.TaxpayerStatusToolStripMenuItem.Name = "TaxpayerStatusToolStripMenuItem"
        Me.TaxpayerStatusToolStripMenuItem.Size = New System.Drawing.Size(230, 22)
        Me.TaxpayerStatusToolStripMenuItem.Text = "Taxpayer Status"
        '
        'ImportItemStatusToolStripMenuItem
        '
        Me.ImportItemStatusToolStripMenuItem.Name = "ImportItemStatusToolStripMenuItem"
        Me.ImportItemStatusToolStripMenuItem.Size = New System.Drawing.Size(230, 22)
        Me.ImportItemStatusToolStripMenuItem.Text = "Import Item Status"
        '
        'BranchStatusToolStripMenuItem
        '
        Me.BranchStatusToolStripMenuItem.Name = "BranchStatusToolStripMenuItem"
        Me.BranchStatusToolStripMenuItem.Size = New System.Drawing.Size(230, 22)
        Me.BranchStatusToolStripMenuItem.Text = "Branch Status"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(227, 6)
        '
        'UnitToolStripMenuItem
        '
        Me.UnitToolStripMenuItem.Name = "UnitToolStripMenuItem"
        Me.UnitToolStripMenuItem.Size = New System.Drawing.Size(230, 22)
        Me.UnitToolStripMenuItem.Text = "Unit of Quantity"
        '
        'PackagingUnitToolStripMenuItem
        '
        Me.PackagingUnitToolStripMenuItem.Name = "PackagingUnitToolStripMenuItem"
        Me.PackagingUnitToolStripMenuItem.Size = New System.Drawing.Size(230, 22)
        Me.PackagingUnitToolStripMenuItem.Text = "Packaging Unit"
        '
        'TransactionProgressToolStripMenuItem
        '
        Me.TransactionProgressToolStripMenuItem.Name = "TransactionProgressToolStripMenuItem"
        Me.TransactionProgressToolStripMenuItem.Size = New System.Drawing.Size(230, 22)
        Me.TransactionProgressToolStripMenuItem.Text = "Transaction Progress"
        '
        'InventoryAdjustmentReasonToolStripMenuItem
        '
        Me.InventoryAdjustmentReasonToolStripMenuItem.Name = "InventoryAdjustmentReasonToolStripMenuItem"
        Me.InventoryAdjustmentReasonToolStripMenuItem.Size = New System.Drawing.Size(230, 22)
        Me.InventoryAdjustmentReasonToolStripMenuItem.Text = "Inventory Adjustment Reason"
        '
        'CreditNoteReasonToolStripMenuItem
        '
        Me.CreditNoteReasonToolStripMenuItem.Name = "CreditNoteReasonToolStripMenuItem"
        Me.CreditNoteReasonToolStripMenuItem.Size = New System.Drawing.Size(230, 22)
        Me.CreditNoteReasonToolStripMenuItem.Text = "Credit Note Reason"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(227, 6)
        '
        'CurrenciesToolStripMenuItem
        '
        Me.CurrenciesToolStripMenuItem.Name = "CurrenciesToolStripMenuItem"
        Me.CurrenciesToolStripMenuItem.Size = New System.Drawing.Size(230, 22)
        Me.CurrenciesToolStripMenuItem.Text = "Currencies"
        '
        'BanksToolStripMenuItem
        '
        Me.BanksToolStripMenuItem.Name = "BanksToolStripMenuItem"
        Me.BanksToolStripMenuItem.Size = New System.Drawing.Size(230, 22)
        Me.BanksToolStripMenuItem.Text = "Banks"
        '
        'TaxOfficesToolStripMenuItem
        '
        Me.TaxOfficesToolStripMenuItem.Name = "TaxOfficesToolStripMenuItem"
        Me.TaxOfficesToolStripMenuItem.Size = New System.Drawing.Size(230, 22)
        Me.TaxOfficesToolStripMenuItem.Text = "Tax offices"
        '
        'LocalesToolStripMenuItem
        '
        Me.LocalesToolStripMenuItem.Name = "LocalesToolStripMenuItem"
        Me.LocalesToolStripMenuItem.Size = New System.Drawing.Size(230, 22)
        Me.LocalesToolStripMenuItem.Text = "Locales"
        '
        'CategoryLevelsToolStripMenuItem
        '
        Me.CategoryLevelsToolStripMenuItem.Name = "CategoryLevelsToolStripMenuItem"
        Me.CategoryLevelsToolStripMenuItem.Size = New System.Drawing.Size(230, 22)
        Me.CategoryLevelsToolStripMenuItem.Text = "Category Levels"
        '
        'ProductManagementToolStripMenuItem
        '
        Me.ProductManagementToolStripMenuItem.Name = "ProductManagementToolStripMenuItem"
        Me.ProductManagementToolStripMenuItem.Size = New System.Drawing.Size(135, 20)
        Me.ProductManagementToolStripMenuItem.Text = "Product Management"
        '
        'SalesToolStripMenuItem
        '
        Me.SalesToolStripMenuItem.Name = "SalesToolStripMenuItem"
        Me.SalesToolStripMenuItem.Size = New System.Drawing.Size(45, 20)
        Me.SalesToolStripMenuItem.Text = "Sales"
        '
        'PurchasesToolStripMenuItem
        '
        Me.PurchasesToolStripMenuItem.Name = "PurchasesToolStripMenuItem"
        Me.PurchasesToolStripMenuItem.Size = New System.Drawing.Size(72, 20)
        Me.PurchasesToolStripMenuItem.Text = "Purchases"
        '
        'ToolStripSettings
        '
        Me.ToolStripSettings.Name = "ToolStripSettings"
        Me.ToolStripSettings.Size = New System.Drawing.Size(61, 20)
        Me.ToolStripSettings.Text = "Settings"
        '
        'ToolStripLogs
        '
        Me.ToolStripLogs.Name = "ToolStripLogs"
        Me.ToolStripLogs.Size = New System.Drawing.Size(44, 20)
        Me.ToolStripLogs.Text = "Logs"
        '
        'NoticesToolStripMenuItem
        '
        Me.NoticesToolStripMenuItem.Name = "NoticesToolStripMenuItem"
        Me.NoticesToolStripMenuItem.Size = New System.Drawing.Size(59, 20)
        Me.NoticesToolStripMenuItem.Text = "Notices"
        '
        'LblStatus
        '
        Me.LblStatus.AutoSize = True
        Me.LblStatus.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.LblStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblStatus.Location = New System.Drawing.Point(0, 587)
        Me.LblStatus.Name = "LblStatus"
        Me.LblStatus.Size = New System.Drawing.Size(0, 20)
        Me.LblStatus.TabIndex = 7
        '
        'Loader
        '
        Me.Loader.ArcLength = 220
        Me.Loader.InnerRingColor = System.Drawing.Color.Turquoise
        Me.Loader.LoaderColor = System.Drawing.Color.DodgerBlue
        Me.Loader.Location = New System.Drawing.Point(357, 230)
        Me.Loader.Name = "Loader"
        Me.Loader.OuterRingColor = System.Drawing.Color.SkyBlue
        Me.Loader.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Loader.RingThickness = 4
        Me.Loader.Size = New System.Drawing.Size(75, 75)
        Me.Loader.Speed = 100
        Me.Loader.TabIndex = 8
        Me.Loader.Text = "Loading"
        Me.Loader.Visible = False
        '
        'HomeForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 607)
        Me.Controls.Add(Me.Loader)
        Me.Controls.Add(Me.LblStatus)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "HomeForm"
        Me.Text = "Dashboard"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ToolStripLogs As ToolStripMenuItem
    Friend WithEvents ToolStripSettings As ToolStripMenuItem
    Friend WithEvents InitializationToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DataMgtToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SalesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents InitializeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LblStatus As Label
    Friend WithEvents ProductManagementToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PurchasesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SyncToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ItemClassificationsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NoticesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TaxTypesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CountriesCodesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PaymentTypesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BranchStatusToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents UnitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PackagingUnitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TransactionProgressToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TransactionTypeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TaxpayerStatusToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ProductTypesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents StockInOutTypeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ImportItemStatusToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RegistrationTypeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CreditNoteReasonToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CurrenciesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PurchaseStatusToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents InventoryAdjustmentReasonToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BanksToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SalesReceiptTypeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PurchaseReceiptTypeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TaxOfficesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LocalesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CategoryLevelsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As ToolStripSeparator
    Friend WithEvents Loader As JsToolBox.Loaders.DualRingLoader
End Class