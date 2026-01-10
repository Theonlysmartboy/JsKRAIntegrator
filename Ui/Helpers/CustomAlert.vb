Imports System.Runtime.InteropServices

Public Class CustomAlert

    Public Enum AlertType
        Success
        [Error]
        Warning
        Info
        Confirm
    End Enum

    Private AlertResult As DialogResult = DialogResult.None

    ' Rounded corners
    <DllImport("Gdi32.dll", EntryPoint:="CreateRoundRectRgn")>
    Private Shared Function CreateRoundRectRgn(left As Integer, top As Integer, right As Integer, bottom As Integer,
                                               width As Integer, height As Integer) As IntPtr
    End Function

    Public Shared Function ShowAlert(owner As Form, msg As String, title As String, type As AlertType) As DialogResult
        Dim frm As New CustomAlert()
        frm.LabelTitle.Text = title
        frm.LabelMessage.Text = msg

        frm.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, frm.Width, frm.Height, 20, 20))
        frm.TopMost = True

        Select Case type
            Case AlertType.Success
                frm.PanelTop.BackColor = Color.SeaGreen
                frm.PictureIcon.Image = My.Resources.success
            Case AlertType.Error
                frm.PanelTop.BackColor = Color.Firebrick
                frm.PictureIcon.Image = My.Resources._error
            Case AlertType.Warning
                frm.PanelTop.BackColor = Color.Goldenrod
                frm.PictureIcon.Image = My.Resources.warning
            Case AlertType.Info
                frm.PanelTop.BackColor = Color.SteelBlue
                frm.PictureIcon.Image = My.Resources.information
            Case AlertType.Confirm
                frm.PanelTop.BackColor = Color.DarkSlateBlue
                frm.PictureIcon.Image = My.Resources.question
                frm.BtnCancel.Visible = True
        End Select

        frm.Opacity = 0

        ' Show dialog first, then start fade in
        frm.Show(owner)
        frm.FadeIn.Start()

        ' Wait until form is closed
        Do While frm.Visible
            Application.DoEvents()
        Loop

        Return frm.AlertResult
    End Function


    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOk.Click
        AlertResult = DialogResult.OK
        FadeOut.Start()
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        AlertResult = DialogResult.Cancel
        FadeOut.Start()
    End Sub

    ' Fade Animation
    Private Sub FadeIn_Tick(sender As Object, e As EventArgs) Handles FadeIn.Tick
        If Me.Opacity < 1 Then
            Me.Opacity += 0.07
        Else
            FadeIn.Stop()
        End If
    End Sub

    Private Sub FadeOut_Tick(sender As Object, e As EventArgs) Handles FadeOut.Tick
        If Me.Opacity > 0 Then
            Me.Opacity -= 0.07
        Else
            FadeOut.Stop()
            Me.Close()
        End If
    End Sub

End Class