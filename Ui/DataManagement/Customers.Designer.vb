<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Customers
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
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Loader = New JsToolBox.Loaders.DualRingLoader()
        Me.DgvCustomers = New System.Windows.Forms.DataGridView()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TxtCustomerTIN = New System.Windows.Forms.TextBox()
        Me.TxtSearchCustomerList = New System.Windows.Forms.TextBox()
        Me.btnQueryCustomers = New System.Windows.Forms.Button()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.RtxtRemark = New System.Windows.Forms.RichTextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TxtFax = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.BtnSaveCustomer = New System.Windows.Forms.Button()
        Me.TxtCustTin = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TxtCustName = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TxtAddress = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TxtEmail = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TxtTel = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtCustNo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DualRingLoader1 = New JsToolBox.Loaders.DualRingLoader()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.DgvCustomers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(684, 311)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Loader)
        Me.TabPage1.Controls.Add(Me.DgvCustomers)
        Me.TabPage1.Controls.Add(Me.Panel2)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(776, 435)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Customers Query"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Loader
        '
        Me.Loader.ArcLength = 220
        Me.Loader.InnerRingColor = System.Drawing.Color.Turquoise
        Me.Loader.LoaderColor = System.Drawing.Color.DodgerBlue
        Me.Loader.Location = New System.Drawing.Point(386, 184)
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
        'DgvCustomers
        '
        Me.DgvCustomers.AllowUserToAddRows = False
        Me.DgvCustomers.AllowUserToDeleteRows = False
        Me.DgvCustomers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvCustomers.Location = New System.Drawing.Point(13, 49)
        Me.DgvCustomers.Name = "DgvCustomers"
        Me.DgvCustomers.ReadOnly = True
        Me.DgvCustomers.Size = New System.Drawing.Size(750, 382)
        Me.DgvCustomers.TabIndex = 8
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Controls.Add(Me.TxtCustomerTIN)
        Me.Panel2.Controls.Add(Me.TxtSearchCustomerList)
        Me.Panel2.Controls.Add(Me.btnQueryCustomers)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(3, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(770, 429)
        Me.Panel2.TabIndex = 12
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(8, 24)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(72, 13)
        Me.Label9.TabIndex = 12
        Me.Label9.Text = "Customer TIN"
        '
        'TxtCustomerTIN
        '
        Me.TxtCustomerTIN.Location = New System.Drawing.Point(86, 18)
        Me.TxtCustomerTIN.Name = "TxtCustomerTIN"
        Me.TxtCustomerTIN.Size = New System.Drawing.Size(261, 20)
        Me.TxtCustomerTIN.TabIndex = 11
        '
        'TxtSearchCustomerList
        '
        Me.TxtSearchCustomerList.Location = New System.Drawing.Point(497, 18)
        Me.TxtSearchCustomerList.Name = "TxtSearchCustomerList"
        Me.TxtSearchCustomerList.Size = New System.Drawing.Size(261, 20)
        Me.TxtSearchCustomerList.TabIndex = 9
        '
        'btnQueryCustomers
        '
        Me.btnQueryCustomers.Location = New System.Drawing.Point(371, 12)
        Me.btnQueryCustomers.Name = "btnQueryCustomers"
        Me.btnQueryCustomers.Size = New System.Drawing.Size(120, 30)
        Me.btnQueryCustomers.TabIndex = 7
        Me.btnQueryCustomers.Text = "Query"
        Me.btnQueryCustomers.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Panel1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(676, 285)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Customer Branch"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'RtxtRemark
        '
        Me.RtxtRemark.Location = New System.Drawing.Point(401, 98)
        Me.RtxtRemark.Name = "RtxtRemark"
        Me.RtxtRemark.Size = New System.Drawing.Size(247, 96)
        Me.RtxtRemark.TabIndex = 31
        Me.RtxtRemark.Text = ""
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(348, 113)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(47, 13)
        Me.Label8.TabIndex = 30
        Me.Label8.Text = "Remark:"
        '
        'TxtFax
        '
        Me.TxtFax.Location = New System.Drawing.Point(401, 66)
        Me.TxtFax.Name = "TxtFax"
        Me.TxtFax.Size = New System.Drawing.Size(247, 20)
        Me.TxtFax.TabIndex = 29
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(338, 66)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(44, 13)
        Me.Label7.TabIndex = 28
        Me.Label7.Text = "Fax No:"
        '
        'BtnSaveCustomer
        '
        Me.BtnSaveCustomer.Location = New System.Drawing.Point(122, 235)
        Me.BtnSaveCustomer.Name = "BtnSaveCustomer"
        Me.BtnSaveCustomer.Size = New System.Drawing.Size(75, 23)
        Me.BtnSaveCustomer.TabIndex = 27
        Me.BtnSaveCustomer.Text = "Save"
        Me.BtnSaveCustomer.UseVisualStyleBackColor = True
        '
        'TxtCustTin
        '
        Me.TxtCustTin.Location = New System.Drawing.Point(85, 56)
        Me.TxtCustTin.Name = "TxtCustTin"
        Me.TxtCustTin.Size = New System.Drawing.Size(247, 20)
        Me.TxtCustTin.TabIndex = 26
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(4, 59)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(75, 13)
        Me.Label6.TabIndex = 25
        Me.Label6.Text = "Customer TIN:"
        '
        'TxtCustName
        '
        Me.TxtCustName.Location = New System.Drawing.Point(401, 23)
        Me.TxtCustName.Name = "TxtCustName"
        Me.TxtCustName.Size = New System.Drawing.Size(247, 20)
        Me.TxtCustName.TabIndex = 24
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(338, 23)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(57, 13)
        Me.Label5.TabIndex = 23
        Me.Label5.Text = "Full Name:"
        '
        'TxtAddress
        '
        Me.TxtAddress.Location = New System.Drawing.Point(85, 99)
        Me.TxtAddress.Name = "TxtAddress"
        Me.TxtAddress.Size = New System.Drawing.Size(247, 20)
        Me.TxtAddress.TabIndex = 22
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(4, 95)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(48, 13)
        Me.Label4.TabIndex = 21
        Me.Label4.Text = "Address:"
        '
        'TxtEmail
        '
        Me.TxtEmail.Location = New System.Drawing.Point(85, 141)
        Me.TxtEmail.Name = "TxtEmail"
        Me.TxtEmail.Size = New System.Drawing.Size(247, 20)
        Me.TxtEmail.TabIndex = 20
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(4, 181)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(61, 13)
        Me.Label3.TabIndex = 19
        Me.Label3.Text = "Telephone:"
        '
        'TxtTel
        '
        Me.TxtTel.Location = New System.Drawing.Point(85, 181)
        Me.TxtTel.Name = "TxtTel"
        Me.TxtTel.Size = New System.Drawing.Size(247, 20)
        Me.TxtTel.TabIndex = 18
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(4, 141)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(35, 13)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "Email:"
        '
        'TxtCustNo
        '
        Me.TxtCustNo.Location = New System.Drawing.Point(85, 16)
        Me.TxtCustNo.Name = "TxtCustNo"
        Me.TxtCustNo.Size = New System.Drawing.Size(247, 20)
        Me.TxtCustNo.TabIndex = 16
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(4, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 13)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "Customer No:"
        '
        'DualRingLoader1
        '
        Me.DualRingLoader1.ArcLength = 220
        Me.DualRingLoader1.InnerRingColor = System.Drawing.Color.Turquoise
        Me.DualRingLoader1.LoaderColor = System.Drawing.Color.DodgerBlue
        Me.DualRingLoader1.Location = New System.Drawing.Point(338, 197)
        Me.DualRingLoader1.Name = "DualRingLoader1"
        Me.DualRingLoader1.OuterRingColor = System.Drawing.Color.SkyBlue
        Me.DualRingLoader1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.DualRingLoader1.RingThickness = 4
        Me.DualRingLoader1.Size = New System.Drawing.Size(75, 75)
        Me.DualRingLoader1.Speed = 100
        Me.DualRingLoader1.TabIndex = 14
        Me.DualRingLoader1.Text = "Loading"
        Me.DualRingLoader1.Visible = False
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.TxtCustTin)
        Me.Panel1.Controls.Add(Me.BtnSaveCustomer)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.RtxtRemark)
        Me.Panel1.Controls.Add(Me.TxtAddress)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.TxtFax)
        Me.Panel1.Controls.Add(Me.TxtEmail)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.TxtTel)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.TxtCustName)
        Me.Panel1.Controls.Add(Me.TxtCustNo)
        Me.Panel1.Controls.Add(Me.DualRingLoader1)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(670, 279)
        Me.Panel1.TabIndex = 32
        '
        'Customers
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(684, 311)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Customers"
        Me.Text = "Customers"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.DgvCustomers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Loader As JsToolBox.Loaders.DualRingLoader
    Friend WithEvents DgvCustomers As DataGridView
    Friend WithEvents btnQueryCustomers As Button
    Friend WithEvents DualRingLoader1 As JsToolBox.Loaders.DualRingLoader
    Friend WithEvents TxtCustomerTIN As TextBox
    Friend WithEvents TxtCustNo As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents TxtCustTin As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents TxtCustName As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents TxtAddress As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TxtEmail As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TxtTel As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents BtnSaveCustomer As Button
    Friend WithEvents Label8 As Label
    Friend WithEvents TxtFax As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents RtxtRemark As RichTextBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents TxtSearchCustomerList As TextBox
    Friend WithEvents Label9 As Label
End Class
