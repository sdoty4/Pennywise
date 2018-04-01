Imports System.Drawing.Imaging

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim Pennys = Image.FromFile("c:\temp\Pennies\pennies.jpg")
        Dim Penny = New Image(100) {}
        Dim size As Integer = 280

        For x = 0 To 9
            For y = 0 To 9

                Dim index = x * 10 + y
                Penny(index) = New Bitmap(size, size)
                Dim g = Graphics.FromImage(Penny(index))
                g.DrawImage(Pennys, New Rectangle(0, 0, size, size), New Rectangle(x * size, y * size, size, size), GraphicsUnit.Pixel)
                g.Dispose()

            Next
        Next

        For i = 0 To 99
            Dim bitmap = New Bitmap(Penny(i))
            bitmap.Save("c:\temp\Pennies\jpg\" & i + 1 & ".jpg", ImageFormat.Jpeg)
        Next

        MsgBox("done")



    End Sub


End Class
