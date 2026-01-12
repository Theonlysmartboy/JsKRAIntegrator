<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Logs
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    Friend WithEvents PanelTop As Panel
    Friend WithEvents FlowTopControls As FlowLayoutPanel
    Friend WithEvents GridLogs As DataGridView
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents dpStart As DateTimePicker
    Friend WithEvents dpEnd As DateTimePicker
    Friend WithEvents btnFilter As Button
    Friend WithEvents btnClear As Button

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Logs))
        Me.PanelTop = New System.Windows.Forms.Panel()
        Me.FlowTopControls = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnFilter = New System.Windows.Forms.Button()
        Me.dpEnd = New System.Windows.Forms.DateTimePicker()
        Me.dpStart = New System.Windows.Forms.DateTimePicker()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.GridLogs = New System.Windows.Forms.DataGridView()
        Me.PanelTop.SuspendLayout()
        Me.FlowTopControls.SuspendLayout()
        CType(Me.GridLogs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelTop
        '
        Me.PanelTop.Controls.Add(Me.FlowTopControls)
        Me.PanelTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelTop.Location = New System.Drawing.Point(0, 0)
        Me.PanelTop.Name = "PanelTop"
        Me.PanelTop.Padding = New System.Windows.Forms.Padding(10)
        Me.PanelTop.Size = New System.Drawing.Size(1000, 50)
        Me.PanelTop.TabIndex = 1
        '
        'FlowTopControls
        '
        Me.FlowTopControls.AutoSize = True
        Me.FlowTopControls.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.FlowTopControls.Controls.Add(Me.btnClear)
        Me.FlowTopControls.Controls.Add(Me.btnFilter)
        Me.FlowTopControls.Controls.Add(Me.dpEnd)
        Me.FlowTopControls.Controls.Add(Me.dpStart)
        Me.FlowTopControls.Controls.Add(Me.txtSearch)
        Me.FlowTopControls.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowTopControls.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft
        Me.FlowTopControls.Location = New System.Drawing.Point(10, 10)
        Me.FlowTopControls.Name = "FlowTopControls"
        Me.FlowTopControls.Size = New System.Drawing.Size(980, 30)
        Me.FlowTopControls.TabIndex = 0
        Me.FlowTopControls.WrapContents = False
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(887, 3)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(90, 23)
        Me.btnClear.TabIndex = 0
        Me.btnClear.Text = "Clear Logs"
        '
        'btnFilter
        '
        Me.btnFilter.Location = New System.Drawing.Point(821, 3)
        Me.btnFilter.Name = "btnFilter"
        Me.btnFilter.Size = New System.Drawing.Size(60, 23)
        Me.btnFilter.TabIndex = 1
        Me.btnFilter.Text = "Filter"
        '
        'dpEnd
        '
        Me.dpEnd.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dpEnd.Location = New System.Drawing.Point(695, 3)
        Me.dpEnd.Name = "dpEnd"
        Me.dpEnd.Size = New System.Drawing.Size(120, 20)
        Me.dpEnd.TabIndex = 2
        '
        'dpStart
        '
        Me.dpStart.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dpStart.Location = New System.Drawing.Point(569, 3)
        Me.dpStart.Name = "dpStart"
        Me.dpStart.Size = New System.Drawing.Size(120, 20)
        Me.dpStart.TabIndex = 3
        '
        'txtSearch
        '
        Me.txtSearch.ForeColor = System.Drawing.Color.Gray
        Me.txtSearch.Location = New System.Drawing.Point(363, 3)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(200, 20)
        Me.txtSearch.TabIndex = 4
        Me.txtSearch.Text = "Search logs..."
        '
        'GridLogs
        '
        Me.GridLogs.AllowUserToAddRows = False
        Me.GridLogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GridLogs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridLogs.Location = New System.Drawing.Point(0, 50)
        Me.GridLogs.Name = "GridLogs"
        Me.GridLogs.ReadOnly = True
        Me.GridLogs.Size = New System.Drawing.Size(1000, 550)
        Me.GridLogs.TabIndex = 0
        '
        'LogsForm
        '
        Me.ClientSize = New System.Drawing.Size(1000, 600)
        Me.Controls.Add(Me.GridLogs)
        Me.Controls.Add(Me.PanelTop)
        Me.Name = "Logs"
        Me.Text = "Logs"
        Me.PanelTop.ResumeLayout(False)
        Me.PanelTop.PerformLayout()
        Me.FlowTopControls.ResumeLayout(False)
        Me.FlowTopControls.PerformLayout()
        CType(Me.GridLogs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
End Class