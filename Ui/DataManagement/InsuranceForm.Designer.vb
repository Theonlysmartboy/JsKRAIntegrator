<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class InsuranceForm
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.BtnRefresh = New System.Windows.Forms.Button()
        Me.BtnSend2VSCU = New System.Windows.Forms.Button()
        Me.TxtSearchInsuranceList = New System.Windows.Forms.TextBox()
        Me.DgvInsuranceList = New System.Windows.Forms.DataGridView()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.BtnSave = New System.Windows.Forms.Button()
        Me.Loader = New JsToolBox.Loaders.DualRingLoader()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.DgvInsuranceList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.DgvInsuranceList, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel3, 0, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 4
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 84.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(800, 450)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 3
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.BtnRefresh, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.BtnSend2VSCU, 2, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.TxtSearchInsuranceList, 1, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(794, 28)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'BtnRefresh
        '
        Me.BtnRefresh.Dock = System.Windows.Forms.DockStyle.Top
        Me.BtnRefresh.Location = New System.Drawing.Point(3, 3)
        Me.BtnRefresh.Name = "BtnRefresh"
        Me.BtnRefresh.Size = New System.Drawing.Size(192, 22)
        Me.BtnRefresh.TabIndex = 0
        Me.BtnRefresh.Text = "Refresh"
        Me.BtnRefresh.UseVisualStyleBackColor = True
        '
        'BtnSend2VSCU
        '
        Me.BtnSend2VSCU.Dock = System.Windows.Forms.DockStyle.Top
        Me.BtnSend2VSCU.Location = New System.Drawing.Point(598, 3)
        Me.BtnSend2VSCU.Name = "BtnSend2VSCU"
        Me.BtnSend2VSCU.Size = New System.Drawing.Size(193, 22)
        Me.BtnSend2VSCU.TabIndex = 1
        Me.BtnSend2VSCU.Text = "Upload"
        Me.BtnSend2VSCU.UseVisualStyleBackColor = True
        '
        'TxtSearchInsuranceList
        '
        Me.TxtSearchInsuranceList.Dock = System.Windows.Forms.DockStyle.Top
        Me.TxtSearchInsuranceList.Location = New System.Drawing.Point(201, 3)
        Me.TxtSearchInsuranceList.Name = "TxtSearchInsuranceList"
        Me.TxtSearchInsuranceList.Size = New System.Drawing.Size(391, 20)
        Me.TxtSearchInsuranceList.TabIndex = 2
        '
        'DgvInsuranceList
        '
        Me.DgvInsuranceList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvInsuranceList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgvInsuranceList.Location = New System.Drawing.Point(3, 37)
        Me.DgvInsuranceList.Name = "DgvInsuranceList"
        Me.DgvInsuranceList.Size = New System.Drawing.Size(794, 355)
        Me.DgvInsuranceList.TabIndex = 1
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 3
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel3.Controls.Add(Me.BtnSave, 1, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(3, 398)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(794, 28)
        Me.TableLayoutPanel3.TabIndex = 2
        '
        'BtnSave
        '
        Me.BtnSave.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.BtnSave.Location = New System.Drawing.Point(267, 3)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(258, 22)
        Me.BtnSave.TabIndex = 0
        Me.BtnSave.Text = "Save"
        Me.BtnSave.UseVisualStyleBackColor = True
        '
        'Loader
        '
        Me.Loader.ArcLength = 220
        Me.Loader.InnerRingColor = System.Drawing.Color.Turquoise
        Me.Loader.LoaderColor = System.Drawing.Color.DodgerBlue
        Me.Loader.Location = New System.Drawing.Point(258, 0)
        Me.Loader.Name = "Loader"
        Me.Loader.OuterRingColor = System.Drawing.Color.DodgerBlue
        Me.Loader.RingThickness = 4
        Me.Loader.Size = New System.Drawing.Size(75, 75)
        Me.Loader.Speed = 100
        Me.Loader.TabIndex = 1
        Me.Loader.Text = "Loading"
        Me.Loader.Visible = False
        '
        'InsuranceForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.Loader)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "InsuranceForm"
        Me.Text = "InsuranceForm"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        CType(Me.DgvInsuranceList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents DgvInsuranceList As DataGridView
    Friend WithEvents BtnRefresh As Button
    Friend WithEvents BtnSend2VSCU As Button
    Friend WithEvents TxtSearchInsuranceList As TextBox
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents BtnSave As Button
    Friend WithEvents Loader As JsToolBox.Loaders.DualRingLoader
End Class
