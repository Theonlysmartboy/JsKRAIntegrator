<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Notices
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
        Me.TxtSearchNotices = New System.Windows.Forms.TextBox()
        Me.DgvNotices = New System.Windows.Forms.DataGridView()
        Me.ButtonFetchNotices = New System.Windows.Forms.Button()
        Me.Loader = New JsToolBox.Loaders.DualRingLoader()
        CType(Me.DgvNotices, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TxtSearchNotices
        '
        Me.TxtSearchNotices.Location = New System.Drawing.Point(169, 10)
        Me.TxtSearchNotices.Name = "TxtSearchNotices"
        Me.TxtSearchNotices.Size = New System.Drawing.Size(261, 20)
        Me.TxtSearchNotices.TabIndex = 5
        '
        'DgvNotices
        '
        Me.DgvNotices.AllowUserToAddRows = False
        Me.DgvNotices.AllowUserToDeleteRows = False
        Me.DgvNotices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvNotices.Location = New System.Drawing.Point(2, 37)
        Me.DgvNotices.Name = "DgvNotices"
        Me.DgvNotices.ReadOnly = True
        Me.DgvNotices.Size = New System.Drawing.Size(750, 392)
        Me.DgvNotices.TabIndex = 4
        '
        'ButtonFetchNotices
        '
        Me.ButtonFetchNotices.Location = New System.Drawing.Point(2, 1)
        Me.ButtonFetchNotices.Name = "ButtonFetchNotices"
        Me.ButtonFetchNotices.Size = New System.Drawing.Size(120, 30)
        Me.ButtonFetchNotices.TabIndex = 3
        Me.ButtonFetchNotices.Text = "Fetch Notices"
        Me.ButtonFetchNotices.UseVisualStyleBackColor = True
        '
        'Loader
        '
        Me.Loader.ArcLength = 220
        Me.Loader.InnerRingColor = System.Drawing.Color.Turquoise
        Me.Loader.LoaderColor = System.Drawing.Color.DodgerBlue
        Me.Loader.Location = New System.Drawing.Point(375, 182)
        Me.Loader.Name = "Loader"
        Me.Loader.OuterRingColor = System.Drawing.Color.SkyBlue
        Me.Loader.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Loader.RingThickness = 4
        Me.Loader.Size = New System.Drawing.Size(75, 75)
        Me.Loader.Speed = 100
        Me.Loader.TabIndex = 6
        Me.Loader.Text = "Loading"
        Me.Loader.Visible = False
        '
        'Notices
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(764, 441)
        Me.Controls.Add(Me.Loader)
        Me.Controls.Add(Me.TxtSearchNotices)
        Me.Controls.Add(Me.DgvNotices)
        Me.Controls.Add(Me.ButtonFetchNotices)
        Me.Name = "Notices"
        Me.Text = "Notices"
        CType(Me.DgvNotices, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TxtSearchNotices As TextBox
    Friend WithEvents DgvNotices As DataGridView
    Friend WithEvents ButtonFetchNotices As Button
    Friend WithEvents Loader As JsToolBox.Loaders.DualRingLoader
End Class
