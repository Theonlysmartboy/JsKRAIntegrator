Namespace Helpers
    Public Class DatabaseHelper

        ' Returns the MySQL connection string OR redirects to settings if invalid
        Public Shared Function GetConnectionString() As String
            Dim server = My.Settings.db_server
            Dim user = My.Settings.db_user
            Dim pass = My.Settings.db_password
            Dim db = My.Settings.db_name

            ' Validate
            If String.IsNullOrWhiteSpace(server) OrElse
               String.IsNullOrWhiteSpace(user) OrElse
               String.IsNullOrWhiteSpace(db) Then
                CustomAlert.ShowAlert(Launcher.ActiveForm,
                    "Your database settings are incomplete. Please configure the database connection.",
                    "Missing Configuration", CustomAlert.AlertType.Warning)

                ' Open Settings form
                Dim frm As New Settings()
                frm.Show()
                frm.OpenSystemTab()

                Return Nothing
            End If

            ' Build connection string
            Return $"server={server};user id={user};password={pass};database={db};"
        End Function

    End Class
End Namespace
