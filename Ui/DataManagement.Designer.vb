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
        Me.DgvCodes = New System.Windows.Forms.DataGridView()
        CType(Me.DgvCodes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TxtSearchCodes
        '
        Me.TxtSearchCodes.Location = New System.Drawing.Point(203, 8)
        Me.TxtSearchCodes.Name = "TxtSearchCodes"
        Me.TxtSearchCodes.Size = New System.Drawing.Size(259, 20)
        Me.TxtSearchCodes.TabIndex = 8
        '
        'DgvCodes
        '
        Me.DgvCodes.AllowUserToAddRows = False
        Me.DgvCodes.AllowUserToDeleteRows = False
        Me.DgvCodes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvCodes.Location = New System.Drawing.Point(12, 44)
        Me.DgvCodes.Name = "DgvCodes"
        Me.DgvCodes.ReadOnly = True
        Me.DgvCodes.Size = New System.Drawing.Size(741, 394)
        Me.DgvCodes.TabIndex = 6
        '
        'DataManagement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(765, 450)
        Me.Controls.Add(Me.TxtSearchCodes)
        Me.Controls.Add(Me.DgvCodes)
        Me.MaximizeBox = False
        Me.Name = "DataManagement"
        Me.Text = "Data Management"
        CType(Me.DgvCodes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TxtSearchCodes As TextBox
    Friend WithEvents DgvCodes As DataGridView
End Class