Imports Core.Config
Imports Core.Logging
Imports Core.Main
Imports Core.Services
Imports Ui.Helpers

Public Class Launcher

    Private loadingDots As Integer = 0

    Private WithEvents FadeInTimer As New Timer With {.Interval = 30}
    Private WithEvents StayTimer As New Timer With {.Interval = 5000} '5 seconds
    Private WithEvents FadeOutTimer As New Timer With {.Interval = 30}
    Private WithEvents LoadingTimer As New Timer With {.Interval = 500}

    Private Async Sub Launcher_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Me.Opacity = 0
        ' Center loader image dynamically
        picLoader.Left = (Me.ClientSize.Width - picLoader.Width) \ 2
        picLoader.Top = 30

        lblLoading.Left = (Me.ClientSize.Width - lblLoading.Width) \ 2
        lblLoading.Top = picLoader.Bottom + 10
        SetupFooterLabels()

        LoadingTimer.Start()
        FadeInTimer.Start()  'start fade-in

        Await LoadDependenciesAsync()
    End Sub

    '===========================
    '       LOAD APP
    '===========================
    Private Async Function LoadDependenciesAsync() As Task
        ' Build connection string
        Dim conn As String = DatabaseHelper.GetConnectionString()

        If conn Is Nothing Then
            Return  ' Avoid crashing if settings missing
        End If

        Dim logRepo As New LogRepository(conn)
        Dim logger As New Logger(logRepo)
        Dim settings As New SettingsManager(conn)

        ' Load settings
        Dim baseUrl = Await settings.GetBaseUrl()
        Dim pin = Await settings.GetPin()
        Dim branch = Await settings.GetBranchId()
        Dim deviceSerial = Await settings.GetDeviceSerial()
        Dim timeout = Await settings.GetTimeout()

        Dim intSettings As New IntegratorSettings With {
            .BaseUrl = baseUrl,
            .Pin = pin,
            .BranchId = branch,
            .DeviceSerial = deviceSerial,
            .Timeout = timeout
        }
        'start Vscu
        Dim starter As New VscuStarter(conn)
        If (Await starter.IsJarRunning()) Then
            ' Already running
        Else
            Await starter.StartKraVscuJar()
        End If

        Dim integrator As New VSCUIntegrator(intSettings, logger)

        ' Store form for later
        _nextForm = New Main(integrator, logRepo)
    End Function

    Private _nextForm As Form

    '===========================
    '       ANIMATION LOGIC
    '===========================

    ' Fade-in
    Private Sub fadeInTimer_Tick(sender As Object, e As EventArgs) Handles FadeInTimer.Tick
        If Me.Opacity < 1 Then
            Me.Opacity += 0.05
        Else
            FadeInTimer.Stop()
            StayTimer.Start()     ' Show splash for 20 sec
        End If
    End Sub

    ' After 20 seconds → fade out
    Private Sub stayTimer_Tick(sender As Object, e As EventArgs) Handles StayTimer.Tick
        StayTimer.Stop()
        FadeOutTimer.Start()
    End Sub

    ' Fade-out
    Private Sub fadeOutTimer_Tick(sender As Object, e As EventArgs) Handles FadeOutTimer.Tick
        If Me.Opacity > 0 Then
            Me.Opacity -= 0.05
        Else
            FadeOutTimer.Stop()
            LoadingTimer.Stop()

            If _nextForm IsNot Nothing Then _nextForm.Show()

            Me.Close()
        End If
    End Sub

    ' Loading text animation
    Private Sub LoadingTimer_Tick(sender As Object, e As EventArgs) Handles LoadingTimer.Tick
        loadingDots = (loadingDots + 1) Mod 4
        lblLoading.Text = "Loading" & New String("."c, loadingDots)
    End Sub

    Private Sub SetupFooterLabels()
        ' === FOOTER PANEL ===
        Dim footerPanel As New Panel() With {
            .Dock = DockStyle.Bottom,
            .Height = 30,
            .BackColor = Color.Transparent
        }

        ' === LEFT LABEL (Version, Build, Model) ===
        Dim lblLeft As New Label() With {
            .AutoSize = True,
            .ForeColor = Color.Gray,
            .Font = New Font("Segoe UI", 9, FontStyle.Regular),
            .Location = New Point(10, 7)
        }

        Dim version = My.Application.Info.Version.ToString()
        lblLeft.Text = $"V{version} "
        Dim model = My.Application.Info.AssemblyName  ' or your custom model string
        LblTitle.Text = model

        ' === RIGHT LABEL (Developer / Company) ===
        Dim lblRight As New Label() With {
            .AutoSize = True,
            .ForeColor = Color.Gray,
            .Font = New Font("Segoe UI", 9, FontStyle.Regular)
        }

        Dim companyName = My.Application.Info.CompanyName
        Dim developerName = "By: " & If(String.IsNullOrWhiteSpace(companyName), "Unknown Developer", companyName)

        lblRight.Text = developerName

        ' Position right-aligned when panel is resized
        AddHandler footerPanel.Resize,
            Sub()
                lblRight.Left = footerPanel.Width - lblRight.Width - 10
                lblRight.Top = 7
            End Sub

        ' Add controls
        footerPanel.Controls.Add(lblLeft)
        footerPanel.Controls.Add(lblRight)
        Me.Controls.Add(footerPanel)
    End Sub

End Class