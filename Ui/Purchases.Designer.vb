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
        Me.TabPageSend = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel6 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DgvPurchaseHeader = New System.Windows.Forms.DataGridView()
        Me.DgvPurchaseItems = New System.Windows.Forms.DataGridView()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.BtnReset = New System.Windows.Forms.Button()
        Me.BtnSavePurchaseInfo = New System.Windows.Forms.Button()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.BtnPurchaseFetchRemote = New System.Windows.Forms.Button()
        Me.BtnUploadPurchase = New System.Windows.Forms.Button()
        Me.TxtPurchaseSendSearch = New System.Windows.Forms.TextBox()
        Me.BtnPurchaseFetchLocal = New System.Windows.Forms.Button()
        Me.Loader = New JsToolBox.Loaders.DualRingLoader()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPageSend.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.TableLayoutPanel6.SuspendLayout()
        CType(Me.DgvPurchaseHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvPurchaseItems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel5.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabPageSend
        '
        Me.TabPageSend.Controls.Add(Me.Loader)
        Me.TabPageSend.Controls.Add(Me.TableLayoutPanel3)
        Me.TabPageSend.Location = New System.Drawing.Point(4, 22)
        Me.TabPageSend.Name = "TabPageSend"
        Me.TabPageSend.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageSend.Size = New System.Drawing.Size(1226, 535)
        Me.TabPageSend.TabIndex = 1
        Me.TabPageSend.Text = "Send"
        Me.TabPageSend.UseVisualStyleBackColor = True
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
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(1220, 529)
        Me.TableLayoutPanel3.TabIndex = 0
        '
        'TableLayoutPanel6
        '
        Me.TableLayoutPanel6.ColumnCount = 1
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel6.Controls.Add(Me.DgvPurchaseItems, 0, 3)
        Me.TableLayoutPanel6.Controls.Add(Me.DgvPurchaseHeader, 0, 1)
        Me.TableLayoutPanel6.Controls.Add(Me.Label1, 0, 2)
        Me.TableLayoutPanel6.Controls.Add(Me.Label2, 0, 0)
        Me.TableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel6.Location = New System.Drawing.Point(3, 34)
        Me.TableLayoutPanel6.Name = "TableLayoutPanel6"
        Me.TableLayoutPanel6.RowCount = 4
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.0!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 37.0!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.0!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 57.0!))
        Me.TableLayoutPanel6.Size = New System.Drawing.Size(1214, 459)
        Me.TableLayoutPanel6.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(3, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1208, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Purchase Header"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 182)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1208, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Purchase Items"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'DgvPurchaseHeader
        '
        Me.DgvPurchaseHeader.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvPurchaseHeader.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgvPurchaseHeader.Location = New System.Drawing.Point(3, 16)
        Me.DgvPurchaseHeader.Name = "DgvPurchaseHeader"
        Me.DgvPurchaseHeader.Size = New System.Drawing.Size(1208, 163)
        Me.DgvPurchaseHeader.TabIndex = 2
        '
        'DgvPurchaseItems
        '
        Me.DgvPurchaseItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvPurchaseItems.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgvPurchaseItems.Location = New System.Drawing.Point(3, 198)
        Me.DgvPurchaseItems.Name = "DgvPurchaseItems"
        Me.DgvPurchaseItems.Size = New System.Drawing.Size(1208, 258)
        Me.DgvPurchaseItems.TabIndex = 5
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.ColumnCount = 3
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.BtnSavePurchaseInfo, 1, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.BtnReset, 0, 0)
        Me.TableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(3, 499)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 1
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(1214, 27)
        Me.TableLayoutPanel5.TabIndex = 2
        '
        'BtnReset
        '
        Me.BtnReset.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.BtnReset.Location = New System.Drawing.Point(3, 3)
        Me.BtnReset.Name = "BtnReset"
        Me.BtnReset.Size = New System.Drawing.Size(297, 21)
        Me.BtnReset.TabIndex = 1
        Me.BtnReset.Text = "Reset"
        Me.BtnReset.UseVisualStyleBackColor = True
        '
        'BtnSavePurchaseInfo
        '
        Me.BtnSavePurchaseInfo.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.BtnSavePurchaseInfo.Location = New System.Drawing.Point(306, 3)
        Me.BtnSavePurchaseInfo.Name = "BtnSavePurchaseInfo"
        Me.BtnSavePurchaseInfo.Size = New System.Drawing.Size(601, 21)
        Me.BtnSavePurchaseInfo.TabIndex = 0
        Me.BtnSavePurchaseInfo.Text = "Save"
        Me.BtnSavePurchaseInfo.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 4
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.BtnPurchaseFetchLocal, 1, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.TxtPurchaseSendSearch, 2, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.BtnUploadPurchase, 3, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.BtnPurchaseFetchRemote, 0, 0)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 1
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(1214, 25)
        Me.TableLayoutPanel4.TabIndex = 0
        '
        'BtnPurchaseFetchRemote
        '
        Me.BtnPurchaseFetchRemote.Dock = System.Windows.Forms.DockStyle.Top
        Me.BtnPurchaseFetchRemote.Location = New System.Drawing.Point(3, 3)
        Me.BtnPurchaseFetchRemote.Name = "BtnPurchaseFetchRemote"
        Me.BtnPurchaseFetchRemote.Size = New System.Drawing.Size(236, 19)
        Me.BtnPurchaseFetchRemote.TabIndex = 10
        Me.BtnPurchaseFetchRemote.Text = "FETCH VSCU DATA"
        Me.BtnPurchaseFetchRemote.UseVisualStyleBackColor = True
        '
        'BtnUploadPurchase
        '
        Me.BtnUploadPurchase.Dock = System.Windows.Forms.DockStyle.Top
        Me.BtnUploadPurchase.Location = New System.Drawing.Point(972, 3)
        Me.BtnUploadPurchase.Name = "BtnUploadPurchase"
        Me.BtnUploadPurchase.Size = New System.Drawing.Size(239, 19)
        Me.BtnUploadPurchase.TabIndex = 9
        Me.BtnUploadPurchase.Text = "Send"
        Me.BtnUploadPurchase.UseVisualStyleBackColor = True
        '
        'TxtPurchaseSendSearch
        '
        Me.TxtPurchaseSendSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.TxtPurchaseSendSearch.Location = New System.Drawing.Point(487, 3)
        Me.TxtPurchaseSendSearch.Name = "TxtPurchaseSendSearch"
        Me.TxtPurchaseSendSearch.Size = New System.Drawing.Size(479, 20)
        Me.TxtPurchaseSendSearch.TabIndex = 8
        '
        'BtnPurchaseFetchLocal
        '
        Me.BtnPurchaseFetchLocal.Dock = System.Windows.Forms.DockStyle.Top
        Me.BtnPurchaseFetchLocal.Location = New System.Drawing.Point(245, 3)
        Me.BtnPurchaseFetchLocal.Name = "BtnPurchaseFetchLocal"
        Me.BtnPurchaseFetchLocal.Size = New System.Drawing.Size(236, 19)
        Me.BtnPurchaseFetchLocal.TabIndex = 7
        Me.BtnPurchaseFetchLocal.Text = "FETCH LOCAL DATA"
        Me.BtnPurchaseFetchLocal.UseVisualStyleBackColor = True
        '
        'Loader
        '
        Me.Loader.ArcLength = 220
        Me.Loader.InnerRingColor = System.Drawing.Color.DeepSkyBlue
        Me.Loader.LoaderColor = System.Drawing.Color.DodgerBlue
        Me.Loader.Location = New System.Drawing.Point(159, 4)
        Me.Loader.Name = "Loader"
        Me.Loader.OuterRingColor = System.Drawing.Color.DarkTurquoise
        Me.Loader.RingThickness = 4
        Me.Loader.Size = New System.Drawing.Size(75, 75)
        Me.Loader.Speed = 100
        Me.Loader.TabIndex = 1
        Me.Loader.Text = "Loading"
        Me.Loader.Visible = False
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPageSend)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1234, 561)
        Me.TabControl1.TabIndex = 0
        '
        'Purchases
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1234, 561)
        Me.Controls.Add(Me.TabControl1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Purchases"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Purchases"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.TabPageSend.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel6.ResumeLayout(False)
        Me.TableLayoutPanel6.PerformLayout()
        CType(Me.DgvPurchaseHeader, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvPurchaseItems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel5.ResumeLayout(False)
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel4.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabPageSend As TabPage
    Friend WithEvents Loader As JsToolBox.Loaders.DualRingLoader
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel4 As TableLayoutPanel
    Friend WithEvents BtnPurchaseFetchLocal As Button
    Friend WithEvents TxtPurchaseSendSearch As TextBox
    Friend WithEvents BtnUploadPurchase As Button
    Friend WithEvents BtnPurchaseFetchRemote As Button
    Friend WithEvents TableLayoutPanel5 As TableLayoutPanel
    Friend WithEvents BtnSavePurchaseInfo As Button
    Friend WithEvents BtnReset As Button
    Friend WithEvents TableLayoutPanel6 As TableLayoutPanel
    Friend WithEvents DgvPurchaseItems As DataGridView
    Friend WithEvents DgvPurchaseHeader As DataGridView
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents TabControl1 As TabControl
End Class