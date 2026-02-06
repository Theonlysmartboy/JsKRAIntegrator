<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Branches
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
        Me.Loader = New JsToolBox.Loaders.DualRingLoader()
        Me.TxtSearchBranches = New System.Windows.Forms.TextBox()
        Me.DgvBranches = New System.Windows.Forms.DataGridView()
        Me.ButtonFetchBranches = New System.Windows.Forms.Button()
        CType(Me.DgvBranches, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Loader
        '
        Me.Loader.ArcLength = 220
        Me.Loader.InnerRingColor = System.Drawing.Color.Turquoise
        Me.Loader.LoaderColor = System.Drawing.Color.DodgerBlue
        Me.Loader.Location = New System.Drawing.Point(398, 192)
        Me.Loader.Name = "Loader"
        Me.Loader.OuterRingColor = System.Drawing.Color.SkyBlue
        Me.Loader.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Loader.RingThickness = 4
        Me.Loader.Size = New System.Drawing.Size(75, 75)
        Me.Loader.Speed = 100
        Me.Loader.TabIndex = 10
        Me.Loader.Text = "Loading"
        Me.Loader.Visible = False
        '
        'TxtSearchBranches
        '
        Me.TxtSearchBranches.Location = New System.Drawing.Point(192, 20)
        Me.TxtSearchBranches.Name = "TxtSearchBranches"
        Me.TxtSearchBranches.Size = New System.Drawing.Size(261, 20)
        Me.TxtSearchBranches.TabIndex = 9
        '
        'DgvBranches
        '
        Me.DgvBranches.AllowUserToAddRows = False
        Me.DgvBranches.AllowUserToDeleteRows = False
        Me.DgvBranches.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvBranches.Location = New System.Drawing.Point(25, 47)
        Me.DgvBranches.Name = "DgvBranches"
        Me.DgvBranches.ReadOnly = True
        Me.DgvBranches.Size = New System.Drawing.Size(750, 392)
        Me.DgvBranches.TabIndex = 8
        '
        'ButtonFetchBranches
        '
        Me.ButtonFetchBranches.Location = New System.Drawing.Point(25, 11)
        Me.ButtonFetchBranches.Name = "ButtonFetchBranches"
        Me.ButtonFetchBranches.Size = New System.Drawing.Size(120, 30)
        Me.ButtonFetchBranches.TabIndex = 7
        Me.ButtonFetchBranches.Text = "Fetch Branches"
        Me.ButtonFetchBranches.UseVisualStyleBackColor = True
        '
        'Branches
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.Loader)
        Me.Controls.Add(Me.TxtSearchBranches)
        Me.Controls.Add(Me.DgvBranches)
        Me.Controls.Add(Me.ButtonFetchBranches)
        Me.Name = "Branches"
        Me.Text = "Branches"
        CType(Me.DgvBranches, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Loader As JsToolBox.Loaders.DualRingLoader
    Friend WithEvents TxtSearchBranches As TextBox
    Friend WithEvents DgvBranches As DataGridView
    Friend WithEvents ButtonFetchBranches As Button
End Class
