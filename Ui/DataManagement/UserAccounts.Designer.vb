<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UserAccounts
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
        Me.RtxtRemark = New System.Windows.Forms.RichTextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.BtnSaveUser = New System.Windows.Forms.Button()
        Me.txtUserName = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtUserId = New System.Windows.Forms.TextBox()
        Me.TxtAddress = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Loader = New JsToolBox.Loaders.DualRingLoader()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtBhfId = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtTin = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CmbBranches = New System.Windows.Forms.ComboBox()
        Me.TxtContact = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'RtxtRemark
        '
        Me.RtxtRemark.Location = New System.Drawing.Point(64, 118)
        Me.RtxtRemark.Name = "RtxtRemark"
        Me.RtxtRemark.Size = New System.Drawing.Size(247, 96)
        Me.RtxtRemark.TabIndex = 49
        Me.RtxtRemark.Text = ""
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(11, 152)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(47, 13)
        Me.Label8.TabIndex = 48
        Me.Label8.Text = "Remark:"
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(383, 160)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(247, 20)
        Me.txtPassword.TabIndex = 47
        Me.txtPassword.UseSystemPasswordChar = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(317, 163)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(56, 13)
        Me.Label7.TabIndex = 46
        Me.Label7.Text = "Password:"
        '
        'BtnSaveUser
        '
        Me.BtnSaveUser.Location = New System.Drawing.Point(13, 223)
        Me.BtnSaveUser.Name = "BtnSaveUser"
        Me.BtnSaveUser.Size = New System.Drawing.Size(80, 40)
        Me.BtnSaveUser.TabIndex = 45
        Me.BtnSaveUser.Text = "Save"
        Me.BtnSaveUser.UseVisualStyleBackColor = True
        '
        'txtUserName
        '
        Me.txtUserName.Location = New System.Drawing.Point(383, 121)
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.Size = New System.Drawing.Size(247, 20)
        Me.txtUserName.TabIndex = 42
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(317, 124)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(63, 13)
        Me.Label5.TabIndex = 41
        Me.Label5.Text = "User Name:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(10, 47)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(48, 13)
        Me.Label4.TabIndex = 39
        Me.Label4.Text = "Address:"
        '
        'txtUserId
        '
        Me.txtUserId.Location = New System.Drawing.Point(383, 83)
        Me.txtUserId.Name = "txtUserId"
        Me.txtUserId.Size = New System.Drawing.Size(247, 20)
        Me.txtUserId.TabIndex = 38
        '
        'TxtAddress
        '
        Me.TxtAddress.Location = New System.Drawing.Point(64, 47)
        Me.TxtAddress.Name = "TxtAddress"
        Me.TxtAddress.Size = New System.Drawing.Size(247, 20)
        Me.TxtAddress.TabIndex = 36
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(317, 83)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(44, 13)
        Me.Label2.TabIndex = 35
        Me.Label2.Text = "User Id:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 13)
        Me.Label1.TabIndex = 33
        Me.Label1.Text = "Branch"
        '
        'Loader
        '
        Me.Loader.ArcLength = 220
        Me.Loader.InnerRingColor = System.Drawing.Color.Turquoise
        Me.Loader.LoaderColor = System.Drawing.Color.DodgerBlue
        Me.Loader.Location = New System.Drawing.Point(320, 182)
        Me.Loader.Name = "Loader"
        Me.Loader.OuterRingColor = System.Drawing.Color.SkyBlue
        Me.Loader.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Loader.RingThickness = 4
        Me.Loader.Size = New System.Drawing.Size(75, 75)
        Me.Loader.Speed = 100
        Me.Loader.TabIndex = 32
        Me.Loader.Text = "Loading"
        Me.Loader.Visible = False
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.TxtContact)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.txtBhfId)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.txtTin)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.txtPassword)
        Me.Panel1.Controls.Add(Me.CmbBranches)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.RtxtRemark)
        Me.Panel1.Controls.Add(Me.txtUserName)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.BtnSaveUser)
        Me.Panel1.Controls.Add(Me.Loader)
        Me.Panel1.Controls.Add(Me.TxtAddress)
        Me.Panel1.Controls.Add(Me.txtUserId)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(644, 291)
        Me.Panel1.TabIndex = 50
        '
        'txtBhfId
        '
        Me.txtBhfId.Enabled = False
        Me.txtBhfId.Location = New System.Drawing.Point(383, 47)
        Me.txtBhfId.Name = "txtBhfId"
        Me.txtBhfId.Size = New System.Drawing.Size(247, 20)
        Me.txtBhfId.TabIndex = 54
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(317, 54)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(56, 13)
        Me.Label6.TabIndex = 53
        Me.Label6.Text = "Branch Id:"
        '
        'txtTin
        '
        Me.txtTin.Enabled = False
        Me.txtTin.Location = New System.Drawing.Point(383, 10)
        Me.txtTin.Name = "txtTin"
        Me.txtTin.Size = New System.Drawing.Size(247, 20)
        Me.txtTin.TabIndex = 52
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(317, 13)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 13)
        Me.Label3.TabIndex = 51
        Me.Label3.Text = "Branch TIN:"
        '
        'CmbBranches
        '
        Me.CmbBranches.FormattingEnabled = True
        Me.CmbBranches.Location = New System.Drawing.Point(64, 10)
        Me.CmbBranches.Name = "CmbBranches"
        Me.CmbBranches.Size = New System.Drawing.Size(247, 21)
        Me.CmbBranches.TabIndex = 50
        '
        'TxtContact
        '
        Me.TxtContact.Location = New System.Drawing.Point(64, 83)
        Me.TxtContact.Name = "TxtContact"
        Me.TxtContact.Size = New System.Drawing.Size(247, 20)
        Me.TxtContact.TabIndex = 56
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(10, 86)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(47, 13)
        Me.Label9.TabIndex = 55
        Me.Label9.Text = "Contact:"
        '
        'UserAccounts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(644, 291)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "UserAccounts"
        Me.Text = "User Accounts"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents RtxtRemark As RichTextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents BtnSaveUser As Button
    Friend WithEvents txtUserName As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents txtUserId As TextBox
    Friend WithEvents TxtAddress As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Loader As JsToolBox.Loaders.DualRingLoader
    Friend WithEvents Panel1 As Panel
    Friend WithEvents CmbBranches As ComboBox
    Friend WithEvents txtBhfId As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents txtTin As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TxtContact As TextBox
    Friend WithEvents Label9 As Label
End Class
