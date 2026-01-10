<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Settings
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer
    Private TabControlSettings As TabControl
    Private TabPageDB As TabPage
    Private TabPageSystem As TabPage

    ' Database settings controls
    Private WithEvents dgvSettings As DataGridView
    Private WithEvents btnSaveDB As Button

    ' System settings controls
    Private txtDbServer As TextBox
    Private txtDbUser As TextBox
    Private txtDbName As TextBox
    Private txtDbPassword As TextBox
    Private txtDbPrefix As TextBox
    Private WithEvents btnSaveSystem As Button

    Private Sub InitializeComponent()
        Me.TabControlSettings = New System.Windows.Forms.TabControl()
        Me.TabPageDB = New System.Windows.Forms.TabPage()
        Me.dgvSettings = New System.Windows.Forms.DataGridView()
        Me.Key = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Value = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnSaveDB = New System.Windows.Forms.Button()
        Me.TabPageSystem = New System.Windows.Forms.TabPage()
        Me.txtDbServer = New System.Windows.Forms.TextBox()
        Me.txtDbUser = New System.Windows.Forms.TextBox()
        Me.txtDbName = New System.Windows.Forms.TextBox()
        Me.txtDbPassword = New System.Windows.Forms.TextBox()
        Me.txtDbPrefix = New System.Windows.Forms.TextBox()
        Me.btnSaveSystem = New System.Windows.Forms.Button()
        Me.lblServer = New System.Windows.Forms.Label()
        Me.lblUser = New System.Windows.Forms.Label()
        Me.lblName = New System.Windows.Forms.Label()
        Me.lblPassword = New System.Windows.Forms.Label()
        Me.lblPrefix = New System.Windows.Forms.Label()
        Me.TabControlSettings.SuspendLayout()
        Me.TabPageDB.SuspendLayout()
        CType(Me.dgvSettings, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageSystem.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControlSettings
        '
        Me.TabControlSettings.Controls.Add(Me.TabPageDB)
        Me.TabControlSettings.Controls.Add(Me.TabPageSystem)
        Me.TabControlSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControlSettings.Location = New System.Drawing.Point(0, 0)
        Me.TabControlSettings.Name = "TabControlSettings"
        Me.TabControlSettings.SelectedIndex = 0
        Me.TabControlSettings.Size = New System.Drawing.Size(734, 411)
        Me.TabControlSettings.TabIndex = 0
        '
        'TabPageDB
        '
        Me.TabPageDB.Controls.Add(Me.dgvSettings)
        Me.TabPageDB.Controls.Add(Me.btnSaveDB)
        Me.TabPageDB.Location = New System.Drawing.Point(4, 22)
        Me.TabPageDB.Name = "TabPageDB"
        Me.TabPageDB.Padding = New System.Windows.Forms.Padding(10)
        Me.TabPageDB.Size = New System.Drawing.Size(726, 385)
        Me.TabPageDB.TabIndex = 0
        Me.TabPageDB.Text = "Application Settings"
        '
        'dgvSettings
        '
        Me.dgvSettings.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvSettings.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Key, Me.Value})
        Me.dgvSettings.Dock = System.Windows.Forms.DockStyle.Top
        Me.dgvSettings.Location = New System.Drawing.Point(10, 10)
        Me.dgvSettings.Name = "dgvSettings"
        Me.dgvSettings.Size = New System.Drawing.Size(706, 336)
        Me.dgvSettings.TabIndex = 0
        '
        'Key
        '
        Me.Key.HeaderText = "Key"
        Me.Key.Name = "Key"
        '
        'Value
        '
        Me.Value.HeaderText = "Value"
        Me.Value.Name = "Value"
        '
        'btnSaveDB
        '
        Me.btnSaveDB.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.btnSaveDB.FlatAppearance.BorderColor = System.Drawing.Color.Navy
        Me.btnSaveDB.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Navy
        Me.btnSaveDB.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Navy
        Me.btnSaveDB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSaveDB.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnSaveDB.Location = New System.Drawing.Point(10, 352)
        Me.btnSaveDB.Name = "btnSaveDB"
        Me.btnSaveDB.Size = New System.Drawing.Size(706, 23)
        Me.btnSaveDB.TabIndex = 1
        Me.btnSaveDB.Text = "Save"
        Me.btnSaveDB.UseVisualStyleBackColor = True
        '
        'TabPageSystem
        '
        Me.TabPageSystem.Controls.Add(Me.txtDbServer)
        Me.TabPageSystem.Controls.Add(Me.txtDbUser)
        Me.TabPageSystem.Controls.Add(Me.txtDbName)
        Me.TabPageSystem.Controls.Add(Me.txtDbPassword)
        Me.TabPageSystem.Controls.Add(Me.txtDbPrefix)
        Me.TabPageSystem.Controls.Add(Me.btnSaveSystem)
        Me.TabPageSystem.Controls.Add(Me.lblServer)
        Me.TabPageSystem.Controls.Add(Me.lblUser)
        Me.TabPageSystem.Controls.Add(Me.lblName)
        Me.TabPageSystem.Controls.Add(Me.lblPassword)
        Me.TabPageSystem.Controls.Add(Me.lblPrefix)
        Me.TabPageSystem.Location = New System.Drawing.Point(4, 22)
        Me.TabPageSystem.Name = "TabPageSystem"
        Me.TabPageSystem.Padding = New System.Windows.Forms.Padding(10)
        Me.TabPageSystem.Size = New System.Drawing.Size(726, 385)
        Me.TabPageSystem.TabIndex = 1
        Me.TabPageSystem.Text = "System Settings"
        '
        'txtDbServer
        '
        Me.txtDbServer.Location = New System.Drawing.Point(169, 10)
        Me.txtDbServer.Name = "txtDbServer"
        Me.txtDbServer.Size = New System.Drawing.Size(200, 20)
        Me.txtDbServer.TabIndex = 0
        '
        'txtDbUser
        '
        Me.txtDbUser.Location = New System.Drawing.Point(169, 73)
        Me.txtDbUser.Name = "txtDbUser"
        Me.txtDbUser.Size = New System.Drawing.Size(200, 20)
        Me.txtDbUser.TabIndex = 1
        '
        'txtDbName
        '
        Me.txtDbName.Location = New System.Drawing.Point(169, 43)
        Me.txtDbName.Name = "txtDbName"
        Me.txtDbName.Size = New System.Drawing.Size(200, 20)
        Me.txtDbName.TabIndex = 2
        '
        'txtDbPassword
        '
        Me.txtDbPassword.Location = New System.Drawing.Point(169, 104)
        Me.txtDbPassword.Name = "txtDbPassword"
        Me.txtDbPassword.Size = New System.Drawing.Size(200, 20)
        Me.txtDbPassword.TabIndex = 3
        '
        'txtDbPrefix
        '
        Me.txtDbPrefix.Location = New System.Drawing.Point(169, 133)
        Me.txtDbPrefix.Name = "txtDbPrefix"
        Me.txtDbPrefix.Size = New System.Drawing.Size(200, 20)
        Me.txtDbPrefix.TabIndex = 4
        '
        'btnSaveSystem
        '
        Me.btnSaveSystem.Location = New System.Drawing.Point(169, 175)
        Me.btnSaveSystem.Name = "btnSaveSystem"
        Me.btnSaveSystem.Size = New System.Drawing.Size(75, 23)
        Me.btnSaveSystem.TabIndex = 5
        Me.btnSaveSystem.Text = "Save"
        '
        'lblServer
        '
        Me.lblServer.Location = New System.Drawing.Point(10, 10)
        Me.lblServer.Name = "lblServer"
        Me.lblServer.Size = New System.Drawing.Size(137, 23)
        Me.lblServer.TabIndex = 6
        Me.lblServer.Text = "Database Server :"
        '
        'lblUser
        '
        Me.lblUser.Location = New System.Drawing.Point(10, 40)
        Me.lblUser.Name = "lblUser"
        Me.lblUser.Size = New System.Drawing.Size(137, 23)
        Me.lblUser.TabIndex = 7
        Me.lblUser.Text = "Database Name :"
        '
        'lblName
        '
        Me.lblName.Location = New System.Drawing.Point(10, 70)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(137, 23)
        Me.lblName.TabIndex = 8
        Me.lblName.Text = "Database User Name :"
        '
        'lblPassword
        '
        Me.lblPassword.Location = New System.Drawing.Point(10, 100)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(137, 23)
        Me.lblPassword.TabIndex = 9
        Me.lblPassword.Text = "Database User Password :"
        '
        'lblPrefix
        '
        Me.lblPrefix.Location = New System.Drawing.Point(10, 130)
        Me.lblPrefix.Name = "lblPrefix"
        Me.lblPrefix.Size = New System.Drawing.Size(137, 23)
        Me.lblPrefix.TabIndex = 10
        Me.lblPrefix.Text = "Database Prefix :"
        '
        'SettingsForm
        '
        Me.ClientSize = New System.Drawing.Size(734, 411)
        Me.Controls.Add(Me.TabControlSettings)
        Me.MaximizeBox = False
        Me.Name = "SettingsForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Settings"
        Me.TabControlSettings.ResumeLayout(False)
        Me.TabPageDB.ResumeLayout(False)
        CType(Me.dgvSettings, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageSystem.ResumeLayout(False)
        Me.TabPageSystem.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblServer As Label
    Friend WithEvents lblUser As Label
    Friend WithEvents lblName As Label
    Friend WithEvents lblPassword As Label
    Friend WithEvents lblPrefix As Label
    Friend WithEvents DtgvKey As DataGridViewTextBoxColumn
    Friend WithEvents DtgvValue As DataGridViewTextBoxColumn
    Friend WithEvents Key As DataGridViewTextBoxColumn
    Friend WithEvents Value As DataGridViewTextBoxColumn
End Class