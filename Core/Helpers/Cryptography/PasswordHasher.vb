Imports System.Security.Cryptography

Namespace Helpers.Cryptography

    Public Class PasswordHasher

        Private Const SaltSize As Integer = 32
        Private Const KeySize As Integer = 64
        Private Const Iterations As Integer = 100000

        Public Shared Function HashPassword(password As String) As String
            Dim salt(SaltSize - 1) As Byte
            Using rng As New RNGCryptoServiceProvider()
                rng.GetBytes(salt)
            End Using

            Dim pbkdf2 As New Rfc2898DeriveBytes(password, salt, Iterations)
            Dim key As Byte() = pbkdf2.GetBytes(KeySize)

            Return Iterations & "." & Convert.ToBase64String(salt) & "." & Convert.ToBase64String(key)
        End Function

        Public Shared Function VerifyPassword(password As String, storedHash As String) As Boolean
            Dim parts = storedHash.Split("."c)
            If parts.Length <> 3 Then Return False

            Dim iterations = Integer.Parse(parts(0))
            Dim salt = Convert.FromBase64String(parts(1))
            Dim storedKey = Convert.FromBase64String(parts(2))

            Dim pbkdf2 As New Rfc2898DeriveBytes(password, salt, iterations)
            Dim computedKey = pbkdf2.GetBytes(storedKey.Length)

            Return SlowEquals(storedKey, computedKey)
        End Function

        ' Timing-safe comparison (Framework-compatible)
        Private Shared Function SlowEquals(a As Byte(), b As Byte()) As Boolean
            Dim diff As Integer = a.Length Xor b.Length
            For i As Integer = 0 To Math.Min(a.Length, b.Length) - 1
                diff = diff Or (a(i) Xor b(i))
            Next
            Return diff = 0
        End Function

    End Class

End Namespace