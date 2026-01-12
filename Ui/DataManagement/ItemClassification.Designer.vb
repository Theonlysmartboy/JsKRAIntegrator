<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ItemClassification
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
        Me.TxtSearchItemClass = New System.Windows.Forms.TextBox()
        Me.BtnLoadStoredItemClassifications = New System.Windows.Forms.Button()
        Me.DataGridViewItemClassification = New System.Windows.Forms.DataGridView()
        Me.BtnSyncItemClassification = New System.Windows.Forms.Button()
        CType(Me.DataGridViewItemClassification, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TxtSearchItemClass
        '
        Me.TxtSearchItemClass.Location = New System.Drawing.Point(214, 8)
        Me.TxtSearchItemClass.Name = "TxtSearchItemClass"
        Me.TxtSearchItemClass.Size = New System.Drawing.Size(269, 20)
        Me.TxtSearchItemClass.TabIndex = 9
        '
        'BtnLoadStoredItemClassifications
        '
        Me.BtnLoadStoredItemClassifications.Location = New System.Drawing.Point(551, 8)
        Me.BtnLoadStoredItemClassifications.Name = "BtnLoadStoredItemClassifications"
        Me.BtnLoadStoredItemClassifications.Size = New System.Drawing.Size(181, 30)
        Me.BtnLoadStoredItemClassifications.TabIndex = 8
        Me.BtnLoadStoredItemClassifications.Text = "Load Local Item Classifications"
        Me.BtnLoadStoredItemClassifications.UseVisualStyleBackColor = True
        '
        'DataGridViewItemClassification
        '
        Me.DataGridViewItemClassification.AllowUserToAddRows = False
        Me.DataGridViewItemClassification.AllowUserToDeleteRows = False
        Me.DataGridViewItemClassification.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewItemClassification.Location = New System.Drawing.Point(12, 44)
        Me.DataGridViewItemClassification.Name = "DataGridViewItemClassification"
        Me.DataGridViewItemClassification.ReadOnly = True
        Me.DataGridViewItemClassification.Size = New System.Drawing.Size(740, 385)
        Me.DataGridViewItemClassification.TabIndex = 7
        '
        'BtnSyncItemClassification
        '
        Me.BtnSyncItemClassification.Location = New System.Drawing.Point(12, 8)
        Me.BtnSyncItemClassification.Name = "BtnSyncItemClassification"
        Me.BtnSyncItemClassification.Size = New System.Drawing.Size(160, 30)
        Me.BtnSyncItemClassification.TabIndex = 6
        Me.BtnSyncItemClassification.Text = "Sync Item Classifications"
        Me.BtnSyncItemClassification.UseVisualStyleBackColor = True
        '
        'ItemClassification
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(764, 441)
        Me.Controls.Add(Me.TxtSearchItemClass)
        Me.Controls.Add(Me.BtnLoadStoredItemClassifications)
        Me.Controls.Add(Me.DataGridViewItemClassification)
        Me.Controls.Add(Me.BtnSyncItemClassification)
        Me.Name = "ItemClassification"
        Me.Text = "ItemClassification"
        CType(Me.DataGridViewItemClassification, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TxtSearchItemClass As TextBox
    Friend WithEvents BtnLoadStoredItemClassifications As Button
    Friend WithEvents DataGridViewItemClassification As DataGridView
    Friend WithEvents BtnSyncItemClassification As Button
End Class
