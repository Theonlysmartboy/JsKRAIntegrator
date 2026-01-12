Imports Core.Config
Imports Core.Logging
Imports Core.Services
Imports Ui.Repo


Public Class DataManagement
    Private ReadOnly _cdCls As Integer
    Private ReadOnly _repo As CodeMappedRepository

    Private _settingsManager As SettingsManager
    Private _integrator As VSCUIntegrator
    Private _logger As Logger
    Private _conn As String
    Private OriginalTables As New Dictionary(Of DataGridView, DataTable)

    Public Sub New(connectionString As String, cdCls As Integer)
        InitializeComponent()
        _conn = connectionString
        _cdCls = cdCls
        _repo = New CodeMappedRepository(_conn)
        ' Initialize logger
        Dim repo As New LogRepository(_conn)
        _logger = New Logger(repo)

    End Sub

    Private Async Sub DataManagementForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Add placeholder text
        SetPlaceholder(TxtSearchCodes, "Start typing to search...")
        ' Attach placeholder handlers
        AddHandler TxtSearchCodes.Enter, AddressOf TextBox_Enter
        AddHandler TxtSearchCodes.Leave, AddressOf TextBox_Leave
        AddHandler TxtSearchCodes.TextChanged, AddressOf TxtSearchCodes_TextChanged
        ' Update form title dynamically using cdCls or the mapped name
        Me.Text = Await GetTitleAsync()
        ' Load and display mapped table
        Dim dt = Await _repo.LoadMappedTableAsync(_cdCls.ToString("00"))
        OriginalTables(DgvCodes) = dt.Copy()
        DgvCodes.DataSource = dt
        DgvCodes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub

    Private Sub TxtSearchCodes_TextChanged(sender As Object, e As EventArgs)
        If TxtSearchCodes.ForeColor = Color.Gray Then Exit Sub
        FilterGrid(DgvCodes, TxtSearchCodes.Text)
    End Sub

    Private Sub FilterGrid(grid As DataGridView, search As String)
        If Not OriginalTables.ContainsKey(grid) Then Exit Sub
        Dim dt As DataTable = OriginalTables(grid)
        Dim dv As New DataView(dt)
        If String.IsNullOrWhiteSpace(search) Then
            dv.RowFilter = ""
        Else
            Dim safeSearch = search.Replace("'", "''")  ' prevent errors
            ' Build dynamic OR-based filter across all columns
            Dim filterExpression =
            String.Join(" OR ", dt.Columns.Cast(Of DataColumn)().Select(Function(c) $"Convert([{c.ColumnName}], 'System.String') LIKE '%{safeSearch}%'"))
            dv.RowFilter = filterExpression
        End If
        grid.DataSource = dv
    End Sub

    Private Sub SetPlaceholder(txt As TextBox, placeholder As String)
        If txt Is Nothing Then Exit Sub
        If String.IsNullOrEmpty(txt.Text) Then
            txt.ForeColor = Color.Gray
            txt.Text = placeholder
            txt.Tag = placeholder
        End If
    End Sub

    Private Sub TextBox_Enter(sender As Object, e As EventArgs)
        Dim txt = DirectCast(sender, TextBox)
        If txt.Text = CStr(txt.Tag) Then
            txt.Text = ""
            txt.ForeColor = Color.Black
        End If
    End Sub

    Private Sub TextBox_Leave(sender As Object, e As EventArgs)
        Dim txt = DirectCast(sender, TextBox)
        If txt.Text = "" Then
            txt.ForeColor = Color.Gray
            txt.Text = CStr(txt.Tag)
        End If
    End Sub

    Private Async Function GetTitleAsync() As Task(Of String)
        Dim map = Await _repo.GetMapAsync(_cdCls.ToString("00"))

        If map Is Nothing Then
            Return "Data Management"
        End If

        ' Convert table name into friendly title
        Return map.table_name.Replace("_", " ").ToUpper()
    End Function
End Class