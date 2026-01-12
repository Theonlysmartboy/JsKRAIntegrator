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
        Me.ItemClassificationsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProductManagementToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SalesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PurchasesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSettings = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripLogs = New System.Windows.Forms.ToolStripMenuItem()
        Me.NoticesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LblStatus = New System.Windows.Forms.Label()
        Me.TaxTypesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
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
        Me.DataMgtToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SyncToolStripMenuItem, Me.ItemClassificationsToolStripMenuItem, Me.TaxTypesToolStripMenuItem})
        Me.DataMgtToolStripMenuItem.Name = "DataMgtToolStripMenuItem"
        Me.DataMgtToolStripMenuItem.Size = New System.Drawing.Size(117, 20)
        Me.DataMgtToolStripMenuItem.Text = "Data Management"
        '
        'SyncToolStripMenuItem
        '
        Me.SyncToolStripMenuItem.Name = "SyncToolStripMenuItem"
        Me.SyncToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.SyncToolStripMenuItem.Text = "Sync Codes"
        '
        'ItemClassificationsToolStripMenuItem
        '
        Me.ItemClassificationsToolStripMenuItem.Name = "ItemClassificationsToolStripMenuItem"
        Me.ItemClassificationsToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.ItemClassificationsToolStripMenuItem.Text = "Item Classifications"
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
        Me.LblStatus.Location = New System.Drawing.Point(0, 430)
        Me.LblStatus.Name = "LblStatus"
        Me.LblStatus.Size = New System.Drawing.Size(0, 20)
        Me.LblStatus.TabIndex = 7
        '
        'TaxTypesToolStripMenuItem
        '
        Me.TaxTypesToolStripMenuItem.Name = "TaxTypesToolStripMenuItem"
        Me.TaxTypesToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.TaxTypesToolStripMenuItem.Text = "Tax Types"
        '
        'HomeForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
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
End Class