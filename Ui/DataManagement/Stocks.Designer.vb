<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Stocks
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Loader = New JsToolBox.Loaders.DualRingLoader()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.BtnFetchStockMove = New System.Windows.Forms.Button()
        Me.TxtSearchStockMovement = New System.Windows.Forms.TextBox()
        Me.BtnUploadStockMoves = New System.Windows.Forms.Button()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.DgvStockMoveItems = New System.Windows.Forms.DataGridView()
        Me.DgvStockMoveHeader = New System.Windows.Forms.DataGridView()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.BtnSaveStockMove = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        CType(Me.DgvStockMoveItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvStockMoveHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel4.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Loader)
        Me.TabPage1.Controls.Add(Me.TableLayoutPanel1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1176, 535)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Stock Movement Query"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Loader
        '
        Me.Loader.ArcLength = 200
        Me.Loader.InnerRingColor = System.Drawing.Color.DeepSkyBlue
        Me.Loader.LoaderColor = System.Drawing.Color.DodgerBlue
        Me.Loader.Location = New System.Drawing.Point(8, 8)
        Me.Loader.Name = "Loader"
        Me.Loader.OuterRingColor = System.Drawing.Color.Turquoise
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
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel3, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel4, 0, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 84.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1170, 529)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 3
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.Label2, 1, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.BtnFetchStockMove, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.TxtSearchStockMovement, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.BtnUploadStockMoves, 2, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(1164, 46)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(294, 34)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(576, 12)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Stock Movement Header"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'BtnFetchStockMove
        '
        Me.BtnFetchStockMove.Dock = System.Windows.Forms.DockStyle.Top
        Me.BtnFetchStockMove.Location = New System.Drawing.Point(3, 3)
        Me.BtnFetchStockMove.Name = "BtnFetchStockMove"
        Me.BtnFetchStockMove.Size = New System.Drawing.Size(285, 20)
        Me.BtnFetchStockMove.TabIndex = 0
        Me.BtnFetchStockMove.Text = "Fetch"
        Me.BtnFetchStockMove.UseVisualStyleBackColor = True
        '
        'TxtSearchStockMovement
        '
        Me.TxtSearchStockMovement.Dock = System.Windows.Forms.DockStyle.Top
        Me.TxtSearchStockMovement.Location = New System.Drawing.Point(294, 3)
        Me.TxtSearchStockMovement.Name = "TxtSearchStockMovement"
        Me.TxtSearchStockMovement.Size = New System.Drawing.Size(576, 20)
        Me.TxtSearchStockMovement.TabIndex = 1
        '
        'BtnUploadStockMoves
        '
        Me.BtnUploadStockMoves.Dock = System.Windows.Forms.DockStyle.Top
        Me.BtnUploadStockMoves.Location = New System.Drawing.Point(876, 3)
        Me.BtnUploadStockMoves.Name = "BtnUploadStockMoves"
        Me.BtnUploadStockMoves.Size = New System.Drawing.Size(285, 20)
        Me.BtnUploadStockMoves.TabIndex = 4
        Me.BtnUploadStockMoves.Text = "Upload"
        Me.BtnUploadStockMoves.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 1
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.Label3, 0, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.DgvStockMoveItems, 0, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.DgvStockMoveHeader, 0, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(3, 55)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 3
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 57.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(1164, 438)
        Me.TableLayoutPanel3.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(3, 175)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1158, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Stock Movement Items"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'DgvStockMoveItems
        '
        Me.DgvStockMoveItems.AllowUserToAddRows = False
        Me.DgvStockMoveItems.AllowUserToDeleteRows = False
        Me.DgvStockMoveItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvStockMoveItems.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgvStockMoveItems.Location = New System.Drawing.Point(3, 191)
        Me.DgvStockMoveItems.Name = "DgvStockMoveItems"
        Me.DgvStockMoveItems.ReadOnly = True
        Me.DgvStockMoveItems.Size = New System.Drawing.Size(1158, 244)
        Me.DgvStockMoveItems.TabIndex = 6
        '
        'DgvStockMoveHeader
        '
        Me.DgvStockMoveHeader.AllowUserToAddRows = False
        Me.DgvStockMoveHeader.AllowUserToDeleteRows = False
        Me.DgvStockMoveHeader.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvStockMoveHeader.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgvStockMoveHeader.Location = New System.Drawing.Point(3, 3)
        Me.DgvStockMoveHeader.Name = "DgvStockMoveHeader"
        Me.DgvStockMoveHeader.ReadOnly = True
        Me.DgvStockMoveHeader.Size = New System.Drawing.Size(1158, 169)
        Me.DgvStockMoveHeader.TabIndex = 2
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 3
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.BtnSaveStockMove, 1, 0)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(3, 499)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 1
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(1164, 27)
        Me.TableLayoutPanel4.TabIndex = 2
        '
        'BtnSaveStockMove
        '
        Me.BtnSaveStockMove.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.BtnSaveStockMove.Location = New System.Drawing.Point(294, 3)
        Me.BtnSaveStockMove.Name = "BtnSaveStockMove"
        Me.BtnSaveStockMove.Size = New System.Drawing.Size(576, 21)
        Me.BtnSaveStockMove.TabIndex = 0
        Me.BtnSaveStockMove.Text = "Save"
        Me.BtnSaveStockMove.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1184, 561)
        Me.TabControl1.TabIndex = 0
        '
        'Stocks
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1184, 561)
        Me.Controls.Add(Me.TabControl1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Stocks"
        Me.Text = "Stock"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.TabPage1.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        CType(Me.DgvStockMoveItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvStockMoveHeader, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents Loader As JsToolBox.Loaders.DualRingLoader
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents BtnFetchStockMove As Button
    Friend WithEvents TxtSearchStockMovement As TextBox
    Friend WithEvents BtnUploadStockMoves As Button
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents DgvStockMoveHeader As DataGridView
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TableLayoutPanel4 As TableLayoutPanel
    Friend WithEvents BtnSaveStockMove As Button
    Friend WithEvents DgvStockMoveItems As DataGridView
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
End Class
