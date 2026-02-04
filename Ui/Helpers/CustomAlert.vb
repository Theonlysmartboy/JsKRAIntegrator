Imports System.Runtime.InteropServices
Namespace Helpers
    Public Class CustomAlert

        Public Enum AlertType
            Success
            [Error]
            Warning
            Info
            Confirm
        End Enum
        Public Enum ButtonType
            OK
            OKCancel
            YesNo
        End Enum

        Private AlertResult As DialogResult = DialogResult.None
        Private IconRotation As Single = -90 ' start rotated off-screen
        Private IconRotationStep As Single = 5 ' rotation per tick
        Private IconAnimationDone As Boolean = False
        Private IconImage As Image

        ' Rounded corners
        <DllImport("Gdi32.dll", EntryPoint:="CreateRoundRectRgn")>
        Private Shared Function CreateRoundRectRgn(left As Integer, top As Integer, right As Integer, bottom As Integer,
                                                width As Integer, height As Integer) As IntPtr
        End Function

        Public Shared Function ShowAlert(owner As Form, msg As String, title As String, type As AlertType, button As ButtonType) As DialogResult
            Dim frm As New CustomAlert()
            frm.LabelTitle.Text = title
            frm.LabelMessage.Text = msg
            frm.IconRotation = -90
            frm.IconEntryTimer.Start()
            frm.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, frm.Width, frm.Height, 20, 20))
            frm.TopMost = True

            Select Case type
                Case AlertType.Success
                    frm.LabelTitle.ForeColor = Color.White
                    frm.PanelHeader.BackColor = Color.SeaGreen
                    frm.IconImage = My.Resources.success
                Case AlertType.Error
                    frm.LabelTitle.ForeColor = Color.White
                    frm.PanelHeader.BackColor = Color.Firebrick
                    frm.IconImage = My.Resources._error
                Case AlertType.Warning
                    frm.LabelTitle.ForeColor = Color.White
                    frm.PanelHeader.BackColor = Color.Goldenrod
                    frm.IconImage = My.Resources.warning
                Case AlertType.Info
                    frm.LabelTitle.ForeColor = Color.White
                    frm.PanelHeader.BackColor = Color.SteelBlue
                    frm.IconImage = My.Resources.information
                Case AlertType.Confirm
                    frm.LabelTitle.ForeColor = Color.White
                    frm.PanelHeader.BackColor = Color.DarkSlateGray
                    frm.IconImage = My.Resources.question
            End Select
            frm.PictureIcon.Image = frm.IconImage
            Select Case button
                Case ButtonType.OK
                    frm.BtnCancel.Visible = False
                    frm.BtnOk.Text = "OK"
                    frm.BtnOk.DialogResult = DialogResult.OK
                Case ButtonType.OKCancel
                    frm.BtnCancel.Visible = True
                    frm.BtnOk.Text = "OK"
                    frm.BtnOk.DialogResult = DialogResult.OK
                    frm.BtnCancel.DialogResult = DialogResult.Cancel
                Case ButtonType.YesNo
                    frm.BtnOk.Text = "Yes"
                    frm.BtnOk.BackColor = Color.SeaGreen
                    frm.BtnOk.DialogResult = DialogResult.Yes
                    frm.BtnCancel.Visible = True
                    frm.BtnCancel.Text = "No"
                    frm.BtnCancel.BackColor = Color.Firebrick
                    frm.BtnCancel.DialogResult = DialogResult.No
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
            AlertResult = BtnOk.DialogResult
            FadeOut.Start()
        End Sub

        Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
            AlertResult = BtnCancel.DialogResult
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

        Private Function RotateImage(img As Image, angle As Single) As Image
            Dim bmp As New Bitmap(img.Width, img.Height)
            Using g As Graphics = Graphics.FromImage(bmp)
                g.TranslateTransform(img.Width / 2, img.Height / 2)
                g.RotateTransform(angle)
                g.TranslateTransform(-img.Width / 2, -img.Height / 2)
                g.DrawImage(img, 0, 0)
            End Using
            Return bmp
        End Function
        Private Sub IconEntryTimer_Tick(sender As Object, e As EventArgs) Handles IconEntryTimer.Tick
            If IconRotation < 0 Then
                IconRotation += IconRotationStep
                PictureIcon.Image = RotateImage(IconImage, IconRotation) ' pick correct icon per alert type
            Else
                ' Stop animation
                IconEntryTimer.Stop()
                PictureIcon.Image = IconImage ' final image without rotation
                IconAnimationDone = True
            End If
        End Sub
    End Class
End Namespace