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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DataManagement))
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPageCodeList = New System.Windows.Forms.TabPage()
        Me.TxtSearchCodes = New System.Windows.Forms.TextBox()
        Me.BtnLoadLocalCodes = New System.Windows.Forms.Button()
        Me.DataGridViewCodes = New System.Windows.Forms.DataGridView()
        Me.ButtonSyncCodes = New System.Windows.Forms.Button()
        Me.TabPageItemClassification = New System.Windows.Forms.TabPage()
        Me.TxtSearchItemClass = New System.Windows.Forms.TextBox()
        Me.BtnLoadStoredItemClassifications = New System.Windows.Forms.Button()
        Me.DataGridViewItemClassification = New System.Windows.Forms.DataGridView()
        Me.ButtonSyncItemClassification = New System.Windows.Forms.Button()
        Me.TabPageNotices = New System.Windows.Forms.TabPage()
        Me.TxtSearchNotices = New System.Windows.Forms.TextBox()
        Me.DataGridViewNotices = New System.Windows.Forms.DataGridView()
        Me.ButtonFetchNotices = New System.Windows.Forms.Button()
        Me.TabControl1.SuspendLayout()
        Me.TabPageCodeList.SuspendLayout()
        CType(Me.DataGridViewCodes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageItemClassification.SuspendLayout()
        CType(Me.DataGridViewItemClassification, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageNotices.SuspendLayout()
        CType(Me.DataGridViewNotices, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.TabControl1.Controls.Add(Me.TabPageCodeList)
        Me.TabControl1.Controls.Add(Me.TabPageItemClassification)
        Me.TabControl1.Controls.Add(Me.TabPageNotices)
        Me.TabControl1.Location = New System.Drawing.Point(13, 13)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(740, 425)
        Me.TabControl1.TabIndex = 0
        '
        'TabPageCodeList
        '
        Me.TabPageCodeList.Controls.Add(Me.TxtSearchCodes)
        Me.TabPageCodeList.Controls.Add(Me.BtnLoadLocalCodes)
        Me.TabPageCodeList.Controls.Add(Me.DataGridViewCodes)
        Me.TabPageCodeList.Controls.Add(Me.ButtonSyncCodes)
        Me.TabPageCodeList.Location = New System.Drawing.Point(4, 25)
        Me.TabPageCodeList.Name = "TabPageCodeList"
        Me.TabPageCodeList.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageCodeList.Size = New System.Drawing.Size(732, 396)
        Me.TabPageCodeList.TabIndex = 0
        Me.TabPageCodeList.Text = "Code List"
        Me.TabPageCodeList.UseVisualStyleBackColor = True
        '
        'TxtSearchCodes
        '
        Me.TxtSearchCodes.Location = New System.Drawing.Point(197, 6)
        Me.TxtSearchCodes.Name = "TxtSearchCodes"
        Me.TxtSearchCodes.Size = New System.Drawing.Size(259, 20)
        Me.TxtSearchCodes.TabIndex = 4
        '
        'BtnLoadLocalCodes
        '
        Me.BtnLoadLocalCodes.Location = New System.Drawing.Point(601, 6)
        Me.BtnLoadLocalCodes.Name = "BtnLoadLocalCodes"
        Me.BtnLoadLocalCodes.Size = New System.Drawing.Size(125, 30)
        Me.BtnLoadLocalCodes.TabIndex = 3
        Me.BtnLoadLocalCodes.Text = "Load Local Codes"
        Me.BtnLoadLocalCodes.UseVisualStyleBackColor = True
        '
        'DataGridViewCodes
        '
        Me.DataGridViewCodes.AllowUserToAddRows = False
        Me.DataGridViewCodes.AllowUserToDeleteRows = False
        Me.DataGridViewCodes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewCodes.Location = New System.Drawing.Point(6, 42)
        Me.DataGridViewCodes.Name = "DataGridViewCodes"
        Me.DataGridViewCodes.ReadOnly = True
        Me.DataGridViewCodes.Size = New System.Drawing.Size(720, 351)
        Me.DataGridViewCodes.TabIndex = 2
        '
        'ButtonSyncCodes
        '
        Me.ButtonSyncCodes.Location = New System.Drawing.Point(6, 6)
        Me.ButtonSyncCodes.Name = "ButtonSyncCodes"
        Me.ButtonSyncCodes.Size = New System.Drawing.Size(94, 30)
        Me.ButtonSyncCodes.TabIndex = 0
        Me.ButtonSyncCodes.Text = "Sync Codes"
        Me.ButtonSyncCodes.UseVisualStyleBackColor = True
        '
        'TabPageItemClassification
        '
        Me.TabPageItemClassification.Controls.Add(Me.TxtSearchItemClass)
        Me.TabPageItemClassification.Controls.Add(Me.BtnLoadStoredItemClassifications)
        Me.TabPageItemClassification.Controls.Add(Me.DataGridViewItemClassification)
        Me.TabPageItemClassification.Controls.Add(Me.ButtonSyncItemClassification)
        Me.TabPageItemClassification.Location = New System.Drawing.Point(4, 25)
        Me.TabPageItemClassification.Name = "TabPageItemClassification"
        Me.TabPageItemClassification.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageItemClassification.Size = New System.Drawing.Size(732, 396)
        Me.TabPageItemClassification.TabIndex = 1
        Me.TabPageItemClassification.Text = "Item Classification"
        Me.TabPageItemClassification.UseVisualStyleBackColor = True
        '
        'TxtSearchItemClass
        '
        Me.TxtSearchItemClass.Location = New System.Drawing.Point(208, 6)
        Me.TxtSearchItemClass.Name = "TxtSearchItemClass"
        Me.TxtSearchItemClass.Size = New System.Drawing.Size(269, 20)
        Me.TxtSearchItemClass.TabIndex = 5
        '
        'BtnLoadStoredItemClassifications
        '
        Me.BtnLoadStoredItemClassifications.Location = New System.Drawing.Point(545, 6)
        Me.BtnLoadStoredItemClassifications.Name = "BtnLoadStoredItemClassifications"
        Me.BtnLoadStoredItemClassifications.Size = New System.Drawing.Size(181, 30)
        Me.BtnLoadStoredItemClassifications.TabIndex = 3
        Me.BtnLoadStoredItemClassifications.Text = "Load Local Item Classifications"
        Me.BtnLoadStoredItemClassifications.UseVisualStyleBackColor = True
        '
        'DataGridViewItemClassification
        '
        Me.DataGridViewItemClassification.AllowUserToAddRows = False
        Me.DataGridViewItemClassification.AllowUserToDeleteRows = False
        Me.DataGridViewItemClassification.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewItemClassification.Location = New System.Drawing.Point(6, 42)
        Me.DataGridViewItemClassification.Name = "DataGridViewItemClassification"
        Me.DataGridViewItemClassification.ReadOnly = True
        Me.DataGridViewItemClassification.Size = New System.Drawing.Size(720, 351)
        Me.DataGridViewItemClassification.TabIndex = 2
        '
        'ButtonSyncItemClassification
        '
        Me.ButtonSyncItemClassification.Location = New System.Drawing.Point(6, 6)
        Me.ButtonSyncItemClassification.Name = "ButtonSyncItemClassification"
        Me.ButtonSyncItemClassification.Size = New System.Drawing.Size(160, 30)
        Me.ButtonSyncItemClassification.TabIndex = 0
        Me.ButtonSyncItemClassification.Text = "Sync Item Classifications"
        Me.ButtonSyncItemClassification.UseVisualStyleBackColor = True
        '
        'TabPageNotices
        '
        Me.TabPageNotices.Controls.Add(Me.TxtSearchNotices)
        Me.TabPageNotices.Controls.Add(Me.DataGridViewNotices)
        Me.TabPageNotices.Controls.Add(Me.ButtonFetchNotices)
        Me.TabPageNotices.Location = New System.Drawing.Point(4, 25)
        Me.TabPageNotices.Name = "TabPageNotices"
        Me.TabPageNotices.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageNotices.Size = New System.Drawing.Size(732, 396)
        Me.TabPageNotices.TabIndex = 2
        Me.TabPageNotices.Text = "Notices"
        Me.TabPageNotices.UseVisualStyleBackColor = True
        '
        'TxtSearchNotices
        '
        Me.TxtSearchNotices.Location = New System.Drawing.Point(173, 15)
        Me.TxtSearchNotices.Name = "TxtSearchNotices"
        Me.TxtSearchNotices.Size = New System.Drawing.Size(261, 20)
        Me.TxtSearchNotices.TabIndex = 2
        '
        'DataGridViewNotices
        '
        Me.DataGridViewNotices.AllowUserToAddRows = False
        Me.DataGridViewNotices.AllowUserToDeleteRows = False
        Me.DataGridViewNotices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewNotices.Location = New System.Drawing.Point(6, 42)
        Me.DataGridViewNotices.Name = "DataGridViewNotices"
        Me.DataGridViewNotices.ReadOnly = True
        Me.DataGridViewNotices.Size = New System.Drawing.Size(720, 351)
        Me.DataGridViewNotices.TabIndex = 1
        '
        'ButtonFetchNotices
        '
        Me.ButtonFetchNotices.Location = New System.Drawing.Point(6, 6)
        Me.ButtonFetchNotices.Name = "ButtonFetchNotices"
        Me.ButtonFetchNotices.Size = New System.Drawing.Size(120, 30)
        Me.ButtonFetchNotices.TabIndex = 0
        Me.ButtonFetchNotices.Text = "Fetch Notices"
        Me.ButtonFetchNotices.UseVisualStyleBackColor = True
        '
        'DataManagement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(765, 450)
        Me.Controls.Add(Me.TabControl1)
        Me.MaximizeBox = False
        Me.Name = "DataManagement"
        Me.Text = "Data Management"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageCodeList.ResumeLayout(False)
        Me.TabPageCodeList.PerformLayout()
        CType(Me.DataGridViewCodes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageItemClassification.ResumeLayout(False)
        Me.TabPageItemClassification.PerformLayout()
        CType(Me.DataGridViewItemClassification, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageNotices.ResumeLayout(False)
        Me.TabPageNotices.PerformLayout()
        CType(Me.DataGridViewNotices, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPageCodeList As TabPage
    Friend WithEvents TabPageItemClassification As TabPage
    Friend WithEvents TabPageNotices As TabPage
    Friend WithEvents DataGridViewCodes As DataGridView
    Friend WithEvents DataGridViewItemClassification As DataGridView
    Friend WithEvents DataGridViewNotices As DataGridView
    Friend WithEvents ButtonSyncCodes As Button
    Friend WithEvents ButtonSyncItemClassification As Button
    Friend WithEvents ButtonFetchNotices As Button
    Friend WithEvents BtnLoadStoredItemClassifications As Button
    Friend WithEvents BtnLoadLocalCodes As Button
    Friend WithEvents TxtSearchCodes As TextBox
    Friend WithEvents TxtSearchItemClass As TextBox
    Friend WithEvents TxtSearchNotices As TextBox
End Class