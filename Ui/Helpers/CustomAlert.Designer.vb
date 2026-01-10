<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CustomAlert
    Inherits System.Windows.Forms.Form

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
        Me.components = New System.ComponentModel.Container()
        Me.PanelTop = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelTitle = New System.Windows.Forms.Label()
        Me.PictureIcon = New System.Windows.Forms.PictureBox()
        Me.LabelMessage = New System.Windows.Forms.Label()
        Me.ButtonFlow = New System.Windows.Forms.FlowLayoutPanel()
        Me.BtnOk = New System.Windows.Forms.Button()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.FadeIn = New System.Windows.Forms.Timer(Me.components)
        Me.FadeOut = New System.Windows.Forms.Timer(Me.components)
        Me.PanelTop.SuspendLayout()
        CType(Me.PictureIcon, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ButtonFlow.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelTop
        '
        Me.PanelTop.ColumnCount = 1
        Me.PanelTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.PanelTop.Controls.Add(Me.LabelTitle, 0, 0)
        Me.PanelTop.Controls.Add(Me.PictureIcon, 0, 1)
        Me.PanelTop.Controls.Add(Me.LabelMessage, 0, 2)
        Me.PanelTop.Controls.Add(Me.ButtonFlow, 0, 3)
        Me.PanelTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelTop.Location = New System.Drawing.Point(0, 0)
        Me.PanelTop.Name = "PanelTop"
        Me.PanelTop.RowCount = 4
        Me.PanelTop.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.PanelTop.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.PanelTop.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.PanelTop.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.PanelTop.Size = New System.Drawing.Size(300, 250)
        Me.PanelTop.TabIndex = 0
        '
        'LabelTitle
        '
        Me.LabelTitle.AutoSize = True
        Me.LabelTitle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelTitle.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold)
        Me.LabelTitle.Location = New System.Drawing.Point(3, 0)
        Me.LabelTitle.Name = "LabelTitle"
        Me.LabelTitle.Size = New System.Drawing.Size(294, 25)
        Me.LabelTitle.TabIndex = 0
        Me.LabelTitle.Text = "Title"
        Me.LabelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PictureIcon
        '
        Me.PictureIcon.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.PictureIcon.Location = New System.Drawing.Point(126, 28)
        Me.PictureIcon.Name = "PictureIcon"
        Me.PictureIcon.Size = New System.Drawing.Size(48, 48)
        Me.PictureIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureIcon.TabIndex = 1
        Me.PictureIcon.TabStop = False
        '
        'LabelMessage
        '
        Me.LabelMessage.AutoSize = True
        Me.LabelMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelMessage.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.LabelMessage.Location = New System.Drawing.Point(3, 79)
        Me.LabelMessage.MaximumSize = New System.Drawing.Size(280, 0)
        Me.LabelMessage.Name = "LabelMessage"
        Me.LabelMessage.Size = New System.Drawing.Size(280, 121)
        Me.LabelMessage.TabIndex = 2
        Me.LabelMessage.Text = "Message goes here"
        Me.LabelMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ButtonFlow
        '
        Me.ButtonFlow.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ButtonFlow.AutoSize = True
        Me.ButtonFlow.Controls.Add(Me.BtnOk)
        Me.ButtonFlow.Controls.Add(Me.BtnCancel)
        Me.ButtonFlow.Location = New System.Drawing.Point(44, 203)
        Me.ButtonFlow.Name = "ButtonFlow"
        Me.ButtonFlow.Size = New System.Drawing.Size(212, 44)
        Me.ButtonFlow.TabIndex = 3
        Me.ButtonFlow.WrapContents = False
        '
        'BtnOk
        '
        Me.BtnOk.BackColor = System.Drawing.Color.RoyalBlue
        Me.BtnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnOk.ForeColor = System.Drawing.Color.White
        Me.BtnOk.Location = New System.Drawing.Point(3, 3)
        Me.BtnOk.Name = "BtnOk"
        Me.BtnOk.Size = New System.Drawing.Size(100, 38)
        Me.BtnOk.TabIndex = 0
        Me.BtnOk.Text = "OK"
        Me.BtnOk.UseVisualStyleBackColor = False
        '
        'BtnCancel
        '
        Me.BtnCancel.BackColor = System.Drawing.Color.Red
        Me.BtnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCancel.ForeColor = System.Drawing.Color.White
        Me.BtnCancel.Location = New System.Drawing.Point(109, 3)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(100, 38)
        Me.BtnCancel.TabIndex = 1
        Me.BtnCancel.Text = "Cancel"
        Me.BtnCancel.UseVisualStyleBackColor = False
        Me.BtnCancel.Visible = False
        '
        'FadeIn
        '
        Me.FadeIn.Interval = 20
        '
        'FadeOut
        '
        Me.FadeOut.Interval = 20
        '
        'CustomAlert
        '
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(300, 250)
        Me.Controls.Add(Me.PanelTop)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "CustomAlert"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.PanelTop.ResumeLayout(False)
        Me.PanelTop.PerformLayout()
        CType(Me.PictureIcon, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ButtonFlow.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PanelTop As TableLayoutPanel
    Friend WithEvents LabelTitle As Label
    Friend WithEvents PictureIcon As PictureBox
    Friend WithEvents LabelMessage As Label
    Friend WithEvents ButtonFlow As FlowLayoutPanel
    Friend WithEvents BtnOk As Button
    Friend WithEvents BtnCancel As Button
    Friend WithEvents FadeIn As Timer
    Friend WithEvents FadeOut As Timer
End Class