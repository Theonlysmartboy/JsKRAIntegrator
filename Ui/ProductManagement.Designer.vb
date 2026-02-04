<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ProductManagement
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
        Me.TabItemSave = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.DtgvItemSave = New System.Windows.Forms.DataGridView()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.BtnSendItem = New System.Windows.Forms.Button()
        Me.BtnFetch = New System.Windows.Forms.Button()
        Me.TxtSearchItemSave = New System.Windows.Forms.TextBox()
        Me.TabItemRequest = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel6 = New System.Windows.Forms.TableLayoutPanel()
        Me.TxtItemRequestSearch = New System.Windows.Forms.TextBox()
        Me.BtnItemRequest = New System.Windows.Forms.Button()
        Me.DtgvItemRequest = New System.Windows.Forms.DataGridView()
        Me.TabImportItemUpdate = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.BtnImportItemFetch = New System.Windows.Forms.Button()
        Me.BtnImportItemUpload = New System.Windows.Forms.Button()
        Me.TxtImportItemUpdateSearch = New System.Windows.Forms.TextBox()
        Me.DtgvImportItemUpload = New System.Windows.Forms.DataGridView()
        Me.TabImportItemRequest = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel7 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel8 = New System.Windows.Forms.TableLayoutPanel()
        Me.BtnImportItemRequest = New System.Windows.Forms.Button()
        Me.TxtImportItemSearch = New System.Windows.Forms.TextBox()
        Me.DtgvImportItemRequest = New System.Windows.Forms.DataGridView()
        Me.Loader = New JsToolBox.Loaders.DualRingLoader()
        Me.TabControl1.SuspendLayout()
        Me.TabItemSave.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.DtgvItemSave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TabItemRequest.SuspendLayout()
        Me.TableLayoutPanel5.SuspendLayout()
        Me.TableLayoutPanel6.SuspendLayout()
        CType(Me.DtgvItemRequest, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabImportItemUpdate.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        CType(Me.DtgvImportItemUpload, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabImportItemRequest.SuspendLayout()
        Me.TableLayoutPanel7.SuspendLayout()
        Me.TableLayoutPanel8.SuspendLayout()
        CType(Me.DtgvImportItemRequest, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.TabControl1.Controls.Add(Me.TabItemSave)
        Me.TabControl1.Controls.Add(Me.TabItemRequest)
        Me.TabControl1.Controls.Add(Me.TabImportItemUpdate)
        Me.TabControl1.Controls.Add(Me.TabImportItemRequest)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1193, 562)
        Me.TabControl1.TabIndex = 0
        '
        'TabItemSave
        '
        Me.TabItemSave.Controls.Add(Me.TableLayoutPanel1)
        Me.TabItemSave.Location = New System.Drawing.Point(4, 25)
        Me.TabItemSave.Name = "TabItemSave"
        Me.TabItemSave.Padding = New System.Windows.Forms.Padding(3)
        Me.TabItemSave.Size = New System.Drawing.Size(1185, 533)
        Me.TabItemSave.TabIndex = 0
        Me.TabItemSave.Text = "Item Save"
        Me.TabItemSave.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 98.98219!))
        Me.TableLayoutPanel1.Controls.Add(Me.DtgvItemSave, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.28708!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 89.71292!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1179, 527)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'DtgvItemSave
        '
        Me.DtgvItemSave.AllowUserToAddRows = False
        Me.DtgvItemSave.AllowUserToDeleteRows = False
        Me.DtgvItemSave.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DtgvItemSave.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DtgvItemSave.Location = New System.Drawing.Point(3, 57)
        Me.DtgvItemSave.Name = "DtgvItemSave"
        Me.DtgvItemSave.ReadOnly = True
        Me.DtgvItemSave.Size = New System.Drawing.Size(1173, 467)
        Me.DtgvItemSave.TabIndex = 0
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 3
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 359.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.BtnSendItem, 2, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.BtnFetch, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.TxtSearchItemSave, 1, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(1173, 48)
        Me.TableLayoutPanel2.TabIndex = 1
        '
        'BtnSendItem
        '
        Me.BtnSendItem.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtnSendItem.Location = New System.Drawing.Point(1095, 3)
        Me.BtnSendItem.Name = "BtnSendItem"
        Me.BtnSendItem.Size = New System.Drawing.Size(75, 42)
        Me.BtnSendItem.TabIndex = 3
        Me.BtnSendItem.Text = "UPLOAD"
        Me.BtnSendItem.UseVisualStyleBackColor = True
        '
        'BtnFetch
        '
        Me.BtnFetch.Dock = System.Windows.Forms.DockStyle.Left
        Me.BtnFetch.Location = New System.Drawing.Point(3, 3)
        Me.BtnFetch.Name = "BtnFetch"
        Me.BtnFetch.Size = New System.Drawing.Size(75, 42)
        Me.BtnFetch.TabIndex = 1
        Me.BtnFetch.Text = "FETCH"
        Me.BtnFetch.UseVisualStyleBackColor = True
        '
        'TxtSearchItemSave
        '
        Me.TxtSearchItemSave.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TxtSearchItemSave.Location = New System.Drawing.Point(410, 25)
        Me.TxtSearchItemSave.Name = "TxtSearchItemSave"
        Me.TxtSearchItemSave.Size = New System.Drawing.Size(401, 20)
        Me.TxtSearchItemSave.TabIndex = 4
        '
        'TabItemRequest
        '
        Me.TabItemRequest.Controls.Add(Me.TableLayoutPanel5)
        Me.TabItemRequest.Location = New System.Drawing.Point(4, 25)
        Me.TabItemRequest.Name = "TabItemRequest"
        Me.TabItemRequest.Padding = New System.Windows.Forms.Padding(3)
        Me.TabItemRequest.Size = New System.Drawing.Size(1185, 533)
        Me.TabItemRequest.TabIndex = 1
        Me.TabItemRequest.Text = "Item Request"
        Me.TabItemRequest.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.ColumnCount = 1
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.TableLayoutPanel6, 0, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.DtgvItemRequest, 0, 1)
        Me.TableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 2
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(1179, 527)
        Me.TableLayoutPanel5.TabIndex = 0
        '
        'TableLayoutPanel6
        '
        Me.TableLayoutPanel6.ColumnCount = 3
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel6.Controls.Add(Me.TxtItemRequestSearch, 1, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.BtnItemRequest, 0, 0)
        Me.TableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel6.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel6.Name = "TableLayoutPanel6"
        Me.TableLayoutPanel6.RowCount = 1
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel6.Size = New System.Drawing.Size(1173, 46)
        Me.TableLayoutPanel6.TabIndex = 0
        '
        'TxtItemRequestSearch
        '
        Me.TxtItemRequestSearch.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TxtItemRequestSearch.Location = New System.Drawing.Point(393, 23)
        Me.TxtItemRequestSearch.Name = "TxtItemRequestSearch"
        Me.TxtItemRequestSearch.Size = New System.Drawing.Size(384, 20)
        Me.TxtItemRequestSearch.TabIndex = 1
        '
        'BtnItemRequest
        '
        Me.BtnItemRequest.Location = New System.Drawing.Point(3, 3)
        Me.BtnItemRequest.Name = "BtnItemRequest"
        Me.BtnItemRequest.Size = New System.Drawing.Size(75, 40)
        Me.BtnItemRequest.TabIndex = 2
        Me.BtnItemRequest.Text = "REQUEST"
        Me.BtnItemRequest.UseVisualStyleBackColor = True
        '
        'DtgvItemRequest
        '
        Me.DtgvItemRequest.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DtgvItemRequest.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DtgvItemRequest.Location = New System.Drawing.Point(3, 55)
        Me.DtgvItemRequest.Name = "DtgvItemRequest"
        Me.DtgvItemRequest.Size = New System.Drawing.Size(1173, 469)
        Me.DtgvItemRequest.TabIndex = 1
        '
        'TabImportItemUpdate
        '
        Me.TabImportItemUpdate.Controls.Add(Me.TableLayoutPanel3)
        Me.TabImportItemUpdate.Location = New System.Drawing.Point(4, 25)
        Me.TabImportItemUpdate.Name = "TabImportItemUpdate"
        Me.TabImportItemUpdate.Size = New System.Drawing.Size(1185, 533)
        Me.TabImportItemUpdate.TabIndex = 3
        Me.TabImportItemUpdate.Text = "Import Item Update"
        Me.TabImportItemUpdate.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 1
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.TableLayoutPanel4, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.DtgvImportItemUpload, 0, 1)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 2
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.141791!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.85821!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(1185, 533)
        Me.TableLayoutPanel3.TabIndex = 0
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 3
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel4.Controls.Add(Me.BtnImportItemFetch, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.BtnImportItemUpload, 2, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.TxtImportItemUpdateSearch, 1, 0)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 1
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(1179, 42)
        Me.TableLayoutPanel4.TabIndex = 0
        '
        'BtnImportItemFetch
        '
        Me.BtnImportItemFetch.Dock = System.Windows.Forms.DockStyle.Left
        Me.BtnImportItemFetch.Location = New System.Drawing.Point(3, 3)
        Me.BtnImportItemFetch.Name = "BtnImportItemFetch"
        Me.BtnImportItemFetch.Size = New System.Drawing.Size(75, 36)
        Me.BtnImportItemFetch.TabIndex = 2
        Me.BtnImportItemFetch.Text = "FETCH"
        Me.BtnImportItemFetch.UseVisualStyleBackColor = True
        '
        'BtnImportItemUpload
        '
        Me.BtnImportItemUpload.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtnImportItemUpload.Location = New System.Drawing.Point(1101, 3)
        Me.BtnImportItemUpload.Name = "BtnImportItemUpload"
        Me.BtnImportItemUpload.Size = New System.Drawing.Size(75, 36)
        Me.BtnImportItemUpload.TabIndex = 3
        Me.BtnImportItemUpload.Text = "UPLOAD"
        Me.BtnImportItemUpload.UseVisualStyleBackColor = True
        '
        'TxtImportItemUpdateSearch
        '
        Me.TxtImportItemUpdateSearch.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TxtImportItemUpdateSearch.Location = New System.Drawing.Point(395, 19)
        Me.TxtImportItemUpdateSearch.Name = "TxtImportItemUpdateSearch"
        Me.TxtImportItemUpdateSearch.Size = New System.Drawing.Size(386, 20)
        Me.TxtImportItemUpdateSearch.TabIndex = 4
        '
        'DtgvImportItemUpload
        '
        Me.DtgvImportItemUpload.AllowUserToAddRows = False
        Me.DtgvImportItemUpload.AllowUserToDeleteRows = False
        Me.DtgvImportItemUpload.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DtgvImportItemUpload.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DtgvImportItemUpload.Location = New System.Drawing.Point(3, 51)
        Me.DtgvImportItemUpload.Name = "DtgvImportItemUpload"
        Me.DtgvImportItemUpload.ReadOnly = True
        Me.DtgvImportItemUpload.Size = New System.Drawing.Size(1179, 479)
        Me.DtgvImportItemUpload.TabIndex = 1
        '
        'TabImportItemRequest
        '
        Me.TabImportItemRequest.Controls.Add(Me.TableLayoutPanel7)
        Me.TabImportItemRequest.Location = New System.Drawing.Point(4, 25)
        Me.TabImportItemRequest.Name = "TabImportItemRequest"
        Me.TabImportItemRequest.Size = New System.Drawing.Size(1185, 533)
        Me.TabImportItemRequest.TabIndex = 2
        Me.TabImportItemRequest.Text = "Import Item Request"
        Me.TabImportItemRequest.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel7
        '
        Me.TableLayoutPanel7.ColumnCount = 1
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel7.Controls.Add(Me.TableLayoutPanel8, 0, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.DtgvImportItemRequest, 0, 1)
        Me.TableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel7.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel7.Name = "TableLayoutPanel7"
        Me.TableLayoutPanel7.RowCount = 2
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.0!))
        Me.TableLayoutPanel7.Size = New System.Drawing.Size(1185, 533)
        Me.TableLayoutPanel7.TabIndex = 0
        '
        'TableLayoutPanel8
        '
        Me.TableLayoutPanel8.ColumnCount = 3
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel8.Controls.Add(Me.BtnImportItemRequest, 0, 0)
        Me.TableLayoutPanel8.Controls.Add(Me.TxtImportItemSearch, 1, 0)
        Me.TableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel8.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel8.Name = "TableLayoutPanel8"
        Me.TableLayoutPanel8.RowCount = 1
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel8.Size = New System.Drawing.Size(1179, 47)
        Me.TableLayoutPanel8.TabIndex = 0
        '
        'BtnImportItemRequest
        '
        Me.BtnImportItemRequest.Location = New System.Drawing.Point(3, 3)
        Me.BtnImportItemRequest.Name = "BtnImportItemRequest"
        Me.BtnImportItemRequest.Size = New System.Drawing.Size(75, 41)
        Me.BtnImportItemRequest.TabIndex = 0
        Me.BtnImportItemRequest.Text = "REQUEST"
        Me.BtnImportItemRequest.UseVisualStyleBackColor = True
        '
        'TxtImportItemSearch
        '
        Me.TxtImportItemSearch.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TxtImportItemSearch.Location = New System.Drawing.Point(395, 24)
        Me.TxtImportItemSearch.Name = "TxtImportItemSearch"
        Me.TxtImportItemSearch.Size = New System.Drawing.Size(386, 20)
        Me.TxtImportItemSearch.TabIndex = 1
        '
        'DtgvImportItemRequest
        '
        Me.DtgvImportItemRequest.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DtgvImportItemRequest.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DtgvImportItemRequest.Location = New System.Drawing.Point(3, 56)
        Me.DtgvImportItemRequest.Name = "DtgvImportItemRequest"
        Me.DtgvImportItemRequest.Size = New System.Drawing.Size(1179, 474)
        Me.DtgvImportItemRequest.TabIndex = 1
        '
        'Loader
        '
        Me.Loader.ArcLength = 220
        Me.Loader.InnerRingColor = System.Drawing.Color.DeepSkyBlue
        Me.Loader.LoaderColor = System.Drawing.Color.DodgerBlue
        Me.Loader.Location = New System.Drawing.Point(518, 0)
        Me.Loader.Name = "Loader"
        Me.Loader.OuterRingColor = System.Drawing.Color.DarkTurquoise
        Me.Loader.RingThickness = 4
        Me.Loader.Size = New System.Drawing.Size(75, 75)
        Me.Loader.Speed = 100
        Me.Loader.TabIndex = 1
        Me.Loader.Text = "Loading"
        Me.Loader.Visible = False
        '
        'ProductManagement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1193, 562)
        Me.Controls.Add(Me.Loader)
        Me.Controls.Add(Me.TabControl1)
        Me.MaximizeBox = False
        Me.Name = "ProductManagement"
        Me.Text = "Product Management"
        Me.TabControl1.ResumeLayout(False)
        Me.TabItemSave.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.DtgvItemSave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.TabItemRequest.ResumeLayout(False)
        Me.TableLayoutPanel5.ResumeLayout(False)
        Me.TableLayoutPanel6.ResumeLayout(False)
        Me.TableLayoutPanel6.PerformLayout()
        CType(Me.DtgvItemRequest, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabImportItemUpdate.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel4.PerformLayout()
        CType(Me.DtgvImportItemUpload, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabImportItemRequest.ResumeLayout(False)
        Me.TableLayoutPanel7.ResumeLayout(False)
        Me.TableLayoutPanel8.ResumeLayout(False)
        Me.TableLayoutPanel8.PerformLayout()
        CType(Me.DtgvImportItemRequest, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabItemSave As TabPage
    Friend WithEvents TabItemRequest As TabPage
    Friend WithEvents TabImportItemRequest As TabPage
    Friend WithEvents TabImportItemUpdate As TabPage
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents DtgvItemSave As DataGridView
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents BtnFetch As Button
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel4 As TableLayoutPanel
    Friend WithEvents BtnImportItemFetch As Button
    Friend WithEvents DtgvImportItemUpload As DataGridView
    Friend WithEvents BtnSendItem As Button
    Friend WithEvents TxtSearchItemSave As TextBox
    Friend WithEvents TableLayoutPanel5 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel6 As TableLayoutPanel
    Friend WithEvents DtgvItemRequest As DataGridView
    Friend WithEvents TxtItemRequestSearch As TextBox
    Friend WithEvents BtnItemRequest As Button
    Friend WithEvents TableLayoutPanel7 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel8 As TableLayoutPanel
    Friend WithEvents DtgvImportItemRequest As DataGridView
    Friend WithEvents BtnImportItemRequest As Button
    Friend WithEvents TxtImportItemSearch As TextBox
    Friend WithEvents BtnImportItemUpload As Button
    Friend WithEvents TxtImportItemUpdateSearch As TextBox
    Friend WithEvents Loader As JsToolBox.Loaders.DualRingLoader
End Class