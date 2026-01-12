<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class DataManagement
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

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.TxtSearchCodes = New System.Windows.Forms.TextBox()
        Me.BtnLoadLocalCodes = New System.Windows.Forms.Button()
        Me.DataGridViewCodes = New System.Windows.Forms.DataGridView()
        Me.ButtonSyncCodes = New System.Windows.Forms.Button()
        CType(Me.DataGridViewCodes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TxtSearchCodes
        '
        Me.TxtSearchCodes.Location = New System.Drawing.Point(203, 8)
        Me.TxtSearchCodes.Name = "TxtSearchCodes"
        Me.TxtSearchCodes.Size = New System.Drawing.Size(259, 20)
        Me.TxtSearchCodes.TabIndex = 8
        '
        'BtnLoadLocalCodes
        '
        Me.BtnLoadLocalCodes.Location = New System.Drawing.Point(607, 8)
        Me.BtnLoadLocalCodes.Name = "BtnLoadLocalCodes"
        Me.BtnLoadLocalCodes.Size = New System.Drawing.Size(125, 30)
        Me.BtnLoadLocalCodes.TabIndex = 7
        Me.BtnLoadLocalCodes.Text = "Load Local Codes"
        Me.BtnLoadLocalCodes.UseVisualStyleBackColor = True
        '
        'DataGridViewCodes
        '
        Me.DataGridViewCodes.AllowUserToAddRows = False
        Me.DataGridViewCodes.AllowUserToDeleteRows = False
        Me.DataGridViewCodes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewCodes.Location = New System.Drawing.Point(12, 44)
        Me.DataGridViewCodes.Name = "DataGridViewCodes"
        Me.DataGridViewCodes.ReadOnly = True
        Me.DataGridViewCodes.Size = New System.Drawing.Size(741, 394)
        Me.DataGridViewCodes.TabIndex = 6
        '
        'ButtonSyncCodes
        '
        Me.ButtonSyncCodes.Location = New System.Drawing.Point(12, 8)
        Me.ButtonSyncCodes.Name = "ButtonSyncCodes"
        Me.ButtonSyncCodes.Size = New System.Drawing.Size(94, 30)
        Me.ButtonSyncCodes.TabIndex = 5
        Me.ButtonSyncCodes.Text = "Sync Codes"
        Me.ButtonSyncCodes.UseVisualStyleBackColor = True
        '
        'DataManagement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(765, 450)
        Me.Controls.Add(Me.TxtSearchCodes)
        Me.Controls.Add(Me.BtnLoadLocalCodes)
        Me.Controls.Add(Me.DataGridViewCodes)
        Me.Controls.Add(Me.ButtonSyncCodes)
        Me.MaximizeBox = False
        Me.Name = "DataManagement"
        Me.Text = "Data Management"
        CType(Me.DataGridViewCodes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TxtSearchCodes As TextBox
    Friend WithEvents BtnLoadLocalCodes As Button
    Friend WithEvents DataGridViewCodes As DataGridView
    Friend WithEvents ButtonSyncCodes As Button
End Class