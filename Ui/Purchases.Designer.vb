<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Purchases
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
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPageGet = New System.Windows.Forms.TabPage()
        Me.Loader = New JsToolBox.Loaders.DualRingLoader()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.TxtSearchPurchases = New System.Windows.Forms.TextBox()
        Me.BtnPurchaseGet = New System.Windows.Forms.Button()
        Me.DtgvPurchasesGet = New System.Windows.Forms.DataGridView()
        Me.TabPageSend = New System.Windows.Forms.TabPage()
        Me.Loader2 = New JsToolBox.Loaders.DualRingLoader()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.BtnPurchaseFetch = New System.Windows.Forms.Button()
        Me.TxtPurchaseSendSearch = New System.Windows.Forms.TextBox()
        Me.BtnUploadPurchase = New System.Windows.Forms.Button()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.BtnSavePurchaseInfo = New System.Windows.Forms.Button()
        Me.TableLayoutPanel6 = New System.Windows.Forms.TableLayoutPanel()
        Me.DgvPurchaseHeader = New System.Windows.Forms.DataGridView()
        Me.DgvPurchaseItems = New System.Windows.Forms.DataGridView()
        Me.TabControl1.SuspendLayout()
        Me.TabPageGet.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.DtgvPurchasesGet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageSend.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        Me.TableLayoutPanel5.SuspendLayout()
        Me.TableLayoutPanel6.SuspendLayout()
        CType(Me.DgvPurchaseHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvPurchaseItems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPageGet)
        Me.TabControl1.Controls.Add(Me.TabPageSend)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1234, 561)
        Me.TabControl1.TabIndex = 0
        '
        'TabPageGet
        '
        Me.TabPageGet.Controls.Add(Me.Loader)
        Me.TabPageGet.Controls.Add(Me.TableLayoutPanel1)
        Me.TabPageGet.Location = New System.Drawing.Point(4, 22)
        Me.TabPageGet.Name = "TabPageGet"
        Me.TabPageGet.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageGet.Size = New System.Drawing.Size(792, 424)
        Me.TabPageGet.TabIndex = 0
        Me.TabPageGet.Text = "Get"
        Me.TabPageGet.UseVisualStyleBackColor = True
        '
        'Loader
        '
        Me.Loader.ArcLength = 220
        Me.Loader.InnerRingColor = System.Drawing.Color.DeepSkyBlue
        Me.Loader.LoaderColor = System.Drawing.Color.DodgerBlue
        Me.Loader.Location = New System.Drawing.Point(170, 0)
        Me.Loader.Name = "Loader"
        Me.Loader.OuterRingColor = System.Drawing.Color.DarkTurquoise
        Me.Loader.RingThickness = 4
        Me.Loader.Size = New System.Drawing.Size(75, 75)
        Me.Loader.Speed = 100
        Me.Loader.TabIndex = 1
        Me.Loader.Text = "Loading"
        Me.Loader.Visible = False
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.DtgvPurchasesGet, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(786, 418)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 3
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.TxtSearchPurchases, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.BtnPurchaseGet, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(780, 35)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'TxtSearchPurchases
        '
        Me.TxtSearchPurchases.Dock = System.Windows.Forms.DockStyle.Top
        Me.TxtSearchPurchases.Location = New System.Drawing.Point(198, 3)
        Me.TxtSearchPurchases.Name = "TxtSearchPurchases"
        Me.TxtSearchPurchases.Size = New System.Drawing.Size(384, 20)
        Me.TxtSearchPurchases.TabIndex = 6
        '
        'BtnPurchaseGet
        '
        Me.BtnPurchaseGet.Dock = System.Windows.Forms.DockStyle.Top
        Me.BtnPurchaseGet.Location = New System.Drawing.Point(3, 3)
        Me.BtnPurchaseGet.Name = "BtnPurchaseGet"
        Me.BtnPurchaseGet.Size = New System.Drawing.Size(189, 29)
        Me.BtnPurchaseGet.TabIndex = 5
        Me.BtnPurchaseGet.Text = "Get"
        Me.BtnPurchaseGet.UseVisualStyleBackColor = True
        '
        'DtgvPurchasesGet
        '
        Me.DtgvPurchasesGet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DtgvPurchasesGet.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DtgvPurchasesGet.Location = New System.Drawing.Point(3, 44)
        Me.DtgvPurchasesGet.Name = "DtgvPurchasesGet"
        Me.DtgvPurchasesGet.Size = New System.Drawing.Size(780, 371)
        Me.DtgvPurchasesGet.TabIndex = 1
        '
        'TabPageSend
        '
        Me.TabPageSend.Controls.Add(Me.Loader2)
        Me.TabPageSend.Controls.Add(Me.TableLayoutPanel3)
        Me.TabPageSend.Location = New System.Drawing.Point(4, 22)
        Me.TabPageSend.Name = "TabPageSend"
        Me.TabPageSend.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageSend.Size = New System.Drawing.Size(1226, 535)
        Me.TabPageSend.TabIndex = 1
        Me.TabPageSend.Text = "Send"
        Me.TabPageSend.UseVisualStyleBackColor = True
        '
        'Loader2
        '
        Me.Loader2.ArcLength = 220
        Me.Loader2.InnerRingColor = System.Drawing.Color.DeepSkyBlue
        Me.Loader2.LoaderColor = System.Drawing.Color.DodgerBlue
        Me.Loader2.Location = New System.Drawing.Point(159, 4)
        Me.Loader2.Name = "Loader2"
        Me.Loader2.OuterRingColor = System.Drawing.Color.DarkTurquoise
        Me.Loader2.RingThickness = 4
        Me.Loader2.Size = New System.Drawing.Size(75, 75)
        Me.Loader2.Speed = 100
        Me.Loader2.TabIndex = 1
        Me.Loader2.Text = "Loading"
        Me.Loader2.Visible = False
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 1
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.TableLayoutPanel4, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.TableLayoutPanel5, 0, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.TableLayoutPanel6, 0, 1)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 3
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(1220, 529)
        Me.TableLayoutPanel3.TabIndex = 0
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 3
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.BtnPurchaseFetch, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.TxtPurchaseSendSearch, 1, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.BtnUploadPurchase, 2, 0)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 1
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(1214, 46)
        Me.TableLayoutPanel4.TabIndex = 0
        '
        'BtnPurchaseFetch
        '
        Me.BtnPurchaseFetch.Dock = System.Windows.Forms.DockStyle.Top
        Me.BtnPurchaseFetch.Location = New System.Drawing.Point(3, 3)
        Me.BtnPurchaseFetch.Name = "BtnPurchaseFetch"
        Me.BtnPurchaseFetch.Size = New System.Drawing.Size(297, 40)
        Me.BtnPurchaseFetch.TabIndex = 7
        Me.BtnPurchaseFetch.Text = "FETCH"
        Me.BtnPurchaseFetch.UseVisualStyleBackColor = True
        '
        'TxtPurchaseSendSearch
        '
        Me.TxtPurchaseSendSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.TxtPurchaseSendSearch.Location = New System.Drawing.Point(306, 3)
        Me.TxtPurchaseSendSearch.Name = "TxtPurchaseSendSearch"
        Me.TxtPurchaseSendSearch.Size = New System.Drawing.Size(601, 20)
        Me.TxtPurchaseSendSearch.TabIndex = 8
        '
        'BtnUploadPurchase
        '
        Me.BtnUploadPurchase.Dock = System.Windows.Forms.DockStyle.Top
        Me.BtnUploadPurchase.Location = New System.Drawing.Point(913, 3)
        Me.BtnUploadPurchase.Name = "BtnUploadPurchase"
        Me.BtnUploadPurchase.Size = New System.Drawing.Size(298, 40)
        Me.BtnUploadPurchase.TabIndex = 9
        Me.BtnUploadPurchase.Text = "Send"
        Me.BtnUploadPurchase.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.ColumnCount = 3
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.BtnSavePurchaseInfo, 1, 0)
        Me.TableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(3, 478)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 1
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(1214, 48)
        Me.TableLayoutPanel5.TabIndex = 2
        '
        'BtnSavePurchaseInfo
        '
        Me.BtnSavePurchaseInfo.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.BtnSavePurchaseInfo.Location = New System.Drawing.Point(306, 22)
        Me.BtnSavePurchaseInfo.Name = "BtnSavePurchaseInfo"
        Me.BtnSavePurchaseInfo.Size = New System.Drawing.Size(601, 23)
        Me.BtnSavePurchaseInfo.TabIndex = 0
        Me.BtnSavePurchaseInfo.Text = "Save"
        Me.BtnSavePurchaseInfo.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel6
        '
        Me.TableLayoutPanel6.ColumnCount = 2
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.0!))
        Me.TableLayoutPanel6.Controls.Add(Me.DgvPurchaseHeader, 0, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.DgvPurchaseItems, 1, 0)
        Me.TableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel6.Location = New System.Drawing.Point(3, 55)
        Me.TableLayoutPanel6.Name = "TableLayoutPanel6"
        Me.TableLayoutPanel6.RowCount = 1
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel6.Size = New System.Drawing.Size(1214, 417)
        Me.TableLayoutPanel6.TabIndex = 3
        '
        'DgvPurchaseHeader
        '
        Me.DgvPurchaseHeader.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvPurchaseHeader.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgvPurchaseHeader.Location = New System.Drawing.Point(3, 3)
        Me.DgvPurchaseHeader.Name = "DgvPurchaseHeader"
        Me.DgvPurchaseHeader.Size = New System.Drawing.Size(479, 411)
        Me.DgvPurchaseHeader.TabIndex = 2
        '
        'DgvPurchaseItems
        '
        Me.DgvPurchaseItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvPurchaseItems.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgvPurchaseItems.Location = New System.Drawing.Point(488, 3)
        Me.DgvPurchaseItems.Name = "DgvPurchaseItems"
        Me.DgvPurchaseItems.Size = New System.Drawing.Size(723, 411)
        Me.DgvPurchaseItems.TabIndex = 3
        '
        'Purchases
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1234, 561)
        Me.Controls.Add(Me.TabControl1)
        Me.MaximizeBox = False
        Me.Name = "Purchases"
        Me.Text = "Purchases"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageGet.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        CType(Me.DtgvPurchasesGet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageSend.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel4.PerformLayout()
        Me.TableLayoutPanel5.ResumeLayout(False)
        Me.TableLayoutPanel6.ResumeLayout(False)
        CType(Me.DgvPurchaseHeader, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvPurchaseItems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPageGet As TabPage
    Friend WithEvents TabPageSend As TabPage
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents BtnPurchaseGet As Button
    Friend WithEvents TxtSearchPurchases As TextBox
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel4 As TableLayoutPanel
    Friend WithEvents BtnPurchaseFetch As Button
    Friend WithEvents TxtPurchaseSendSearch As TextBox
    Friend WithEvents BtnUploadPurchase As Button
    Friend WithEvents DtgvPurchasesGet As DataGridView
    Friend WithEvents Loader As JsToolBox.Loaders.DualRingLoader
    Friend WithEvents Loader2 As JsToolBox.Loaders.DualRingLoader
    Friend WithEvents TableLayoutPanel5 As TableLayoutPanel
    Friend WithEvents BtnSavePurchaseInfo As Button
    Friend WithEvents TableLayoutPanel6 As TableLayoutPanel
    Friend WithEvents DgvPurchaseHeader As DataGridView
    Friend WithEvents DgvPurchaseItems As DataGridView
End Class