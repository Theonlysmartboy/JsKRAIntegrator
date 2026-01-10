<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Launcher
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer
    Private lblLoading As Label
    Private picLoader As PictureBox
    Private lblBuildInfo As Label
    Private lblDeveloper As Label


    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.lblBuildInfo = New System.Windows.Forms.Label()
        Me.lblDeveloper = New System.Windows.Forms.Label()
        Me.lblLoading = New System.Windows.Forms.Label()
        Me.picLoader = New System.Windows.Forms.PictureBox()
        Me.LblTitle = New System.Windows.Forms.Label()
        CType(Me.picLoader, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblBuildInfo
        '
        Me.lblBuildInfo.Location = New System.Drawing.Point(0, 0)
        Me.lblBuildInfo.Name = "lblBuildInfo"
        Me.lblBuildInfo.Size = New System.Drawing.Size(100, 23)
        Me.lblBuildInfo.TabIndex = 0
        '
        'lblDeveloper
        '
        Me.lblDeveloper.Location = New System.Drawing.Point(0, 0)
        Me.lblDeveloper.Name = "lblDeveloper"
        Me.lblDeveloper.Size = New System.Drawing.Size(100, 23)
        Me.lblDeveloper.TabIndex = 0
        '
        'lblLoading
        '
        Me.lblLoading.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.lblLoading.ForeColor = System.Drawing.Color.Gray
        Me.lblLoading.Location = New System.Drawing.Point(0, 140)
        Me.lblLoading.Name = "lblLoading"
        Me.lblLoading.Size = New System.Drawing.Size(350, 40)
        Me.lblLoading.TabIndex = 1
        Me.lblLoading.Text = "Loading"
        Me.lblLoading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'picLoader
        '
        Me.picLoader.Image = Global.Ui.My.Resources.Resources.loader
        Me.picLoader.Location = New System.Drawing.Point(141, 46)
        Me.picLoader.Name = "picLoader"
        Me.picLoader.Size = New System.Drawing.Size(64, 64)
        Me.picLoader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picLoader.TabIndex = 0
        Me.picLoader.TabStop = False
        '
        'LblTitle
        '
        Me.LblTitle.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.LblTitle.ForeColor = System.Drawing.Color.Gray
        Me.LblTitle.Location = New System.Drawing.Point(0, 3)
        Me.LblTitle.Name = "LblTitle"
        Me.LblTitle.Size = New System.Drawing.Size(350, 40)
        Me.LblTitle.TabIndex = 2
        Me.LblTitle.Text = "Loading"
        Me.LblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Launcher
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(350, 180)
        Me.Controls.Add(Me.LblTitle)
        Me.Controls.Add(Me.picLoader)
        Me.Controls.Add(Me.lblLoading)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Launcher"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.TopMost = True
        CType(Me.picLoader, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Private WithEvents LblTitle As Label
End Class