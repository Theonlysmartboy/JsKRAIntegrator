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
        Me.DataGridViewNotices = New System.Windows.Forms.DataGridView()
        Me.ButtonFetchNotices = New System.Windows.Forms.Button()
        CType(Me.DataGridViewNotices, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TxtSearchNotices
        '
        Me.TxtSearchNotices.Location = New System.Drawing.Point(169, 10)
        Me.TxtSearchNotices.Name = "TxtSearchNotices"
        Me.TxtSearchNotices.Size = New System.Drawing.Size(261, 20)
        Me.TxtSearchNotices.TabIndex = 5
        '
        'DataGridViewNotices
        '
        Me.DataGridViewNotices.AllowUserToAddRows = False
        Me.DataGridViewNotices.AllowUserToDeleteRows = False
        Me.DataGridViewNotices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewNotices.Location = New System.Drawing.Point(2, 37)
        Me.DataGridViewNotices.Name = "DataGridViewNotices"
        Me.DataGridViewNotices.ReadOnly = True
        Me.DataGridViewNotices.Size = New System.Drawing.Size(750, 392)
        Me.DataGridViewNotices.TabIndex = 4
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
        'Notices
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(764, 441)
        Me.Controls.Add(Me.TxtSearchNotices)
        Me.Controls.Add(Me.DataGridViewNotices)
        Me.Controls.Add(Me.ButtonFetchNotices)
        Me.Name = "Notices"
        Me.Text = "Notices"
        CType(Me.DataGridViewNotices, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TxtSearchNotices As TextBox
    Friend WithEvents DataGridViewNotices As DataGridView
    Friend WithEvents ButtonFetchNotices As Button
End Class
