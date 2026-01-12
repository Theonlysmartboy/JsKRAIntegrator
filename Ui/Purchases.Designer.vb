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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Purchases))
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPageGet = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.TxtSearchPurchases = New System.Windows.Forms.TextBox()
        Me.BtnPurchaseGet = New System.Windows.Forms.Button()
        Me.DtgvPurchasesGet = New System.Windows.Forms.DataGridView()
        Me.TabPageSend = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.BtnPurchaseFetch = New System.Windows.Forms.Button()
        Me.TxtPurchaseSendSearch = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.DtgvPurchaseSend = New System.Windows.Forms.DataGridView()
        Me.TabControl1.SuspendLayout()
        Me.TabPageGet.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.DtgvPurchasesGet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageSend.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        CType(Me.DtgvPurchaseSend, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.TabControl1.Size = New System.Drawing.Size(800, 450)
        Me.TabControl1.TabIndex = 0
        '
        'TabPageGet
        '
        Me.TabPageGet.Controls.Add(Me.TableLayoutPanel1)
        Me.TabPageGet.Location = New System.Drawing.Point(4, 22)
        Me.TabPageGet.Name = "TabPageGet"
        Me.TabPageGet.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageGet.Size = New System.Drawing.Size(792, 424)
        Me.TabPageGet.TabIndex = 0
        Me.TabPageGet.Text = "Get"
        Me.TabPageGet.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.DtgvPurchasesGet, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.67943!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 87.32057!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(786, 418)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 3
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel2.Controls.Add(Me.TxtSearchPurchases, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.BtnPurchaseGet, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(780, 47)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'TxtSearchPurchases
        '
        Me.TxtSearchPurchases.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TxtSearchPurchases.Location = New System.Drawing.Point(263, 24)
        Me.TxtSearchPurchases.Name = "TxtSearchPurchases"
        Me.TxtSearchPurchases.Size = New System.Drawing.Size(254, 20)
        Me.TxtSearchPurchases.TabIndex = 6
        '
        'BtnPurchaseGet
        '
        Me.BtnPurchaseGet.Location = New System.Drawing.Point(3, 3)
        Me.BtnPurchaseGet.Name = "BtnPurchaseGet"
        Me.BtnPurchaseGet.Size = New System.Drawing.Size(75, 41)
        Me.BtnPurchaseGet.TabIndex = 5
        Me.BtnPurchaseGet.Text = "Get"
        Me.BtnPurchaseGet.UseVisualStyleBackColor = True
        '
        'DtgvPurchasesGet
        '
        Me.DtgvPurchasesGet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DtgvPurchasesGet.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DtgvPurchasesGet.Location = New System.Drawing.Point(3, 56)
        Me.DtgvPurchasesGet.Name = "DtgvPurchasesGet"
        Me.DtgvPurchasesGet.Size = New System.Drawing.Size(780, 359)
        Me.DtgvPurchasesGet.TabIndex = 1
        '
        'TabPageSend
        '
        Me.TabPageSend.Controls.Add(Me.TableLayoutPanel3)
        Me.TabPageSend.Location = New System.Drawing.Point(4, 22)
        Me.TabPageSend.Name = "TabPageSend"
        Me.TabPageSend.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageSend.Size = New System.Drawing.Size(792, 424)
        Me.TabPageSend.TabIndex = 1
        Me.TabPageSend.Text = "Send"
        Me.TabPageSend.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 1
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.TableLayoutPanel4, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.DtgvPurchaseSend, 0, 1)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 2
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.09824!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 86.90176!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(786, 418)
        Me.TableLayoutPanel3.TabIndex = 0
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 3
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33444!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33445!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33112!))
        Me.TableLayoutPanel4.Controls.Add(Me.BtnPurchaseFetch, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.TxtPurchaseSendSearch, 1, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.Button1, 2, 0)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 1
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(780, 48)
        Me.TableLayoutPanel4.TabIndex = 0
        '
        'BtnPurchaseFetch
        '
        Me.BtnPurchaseFetch.Dock = System.Windows.Forms.DockStyle.Left
        Me.BtnPurchaseFetch.Location = New System.Drawing.Point(3, 3)
        Me.BtnPurchaseFetch.Name = "BtnPurchaseFetch"
        Me.BtnPurchaseFetch.Size = New System.Drawing.Size(75, 42)
        Me.BtnPurchaseFetch.TabIndex = 7
        Me.BtnPurchaseFetch.Text = "FETCH"
        Me.BtnPurchaseFetch.UseVisualStyleBackColor = True
        '
        'TxtPurchaseSendSearch
        '
        Me.TxtPurchaseSendSearch.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TxtPurchaseSendSearch.Location = New System.Drawing.Point(263, 25)
        Me.TxtPurchaseSendSearch.Name = "TxtPurchaseSendSearch"
        Me.TxtPurchaseSendSearch.Size = New System.Drawing.Size(254, 20)
        Me.TxtPurchaseSendSearch.TabIndex = 8
        '
        'Button1
        '
        Me.Button1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Button1.Location = New System.Drawing.Point(702, 3)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 42)
        Me.Button1.TabIndex = 9
        Me.Button1.Text = "Send"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'DtgvPurchaseSend
        '
        Me.DtgvPurchaseSend.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DtgvPurchaseSend.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DtgvPurchaseSend.Location = New System.Drawing.Point(3, 57)
        Me.DtgvPurchaseSend.Name = "DtgvPurchaseSend"
        Me.DtgvPurchaseSend.Size = New System.Drawing.Size(780, 358)
        Me.DtgvPurchaseSend.TabIndex = 1
        '
        'Purchases
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
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
        CType(Me.DtgvPurchaseSend, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents DtgvPurchaseSend As DataGridView
    Friend WithEvents TxtPurchaseSendSearch As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents DtgvPurchasesGet As DataGridView
End Class