Imports System.Drawing.Imaging
Imports System.IO

Public Class Form1

    '-- convert to grayscale --'
    Dim grayscale As New Imaging.ColorMatrix(New Single()() {
                    New Single() {0.299, 0.299, 0.299, 0, 0},
                    New Single() {0.587, 0.587, 0.587, 0, 0},
                    New Single() {0.114, 0.114, 0.114, 0, 0},
                    New Single() {0, 0, 0, 1, 0},
                    New Single() {0, 0, 0, 0, 1}
                })
    Dim imgattr As New Imaging.ImageAttributes()


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        SplitSheet(Me.txtSetNumber.Text)
        MsgBox("done")
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Rotated(Me.txtSetNumber.Text)

        'For i = 1 To 100

        '    Dim img = New Bitmap("c:\temp\Pennies\set1-gs\" & i & ".tif")

        '    Me.TextBox1.Text = i & ".tif" & vbCr & TextBox1.Text

        '    Me.PictureBox1.Image = img
        '    Me.PictureBox1.Refresh()

        '    Dim newImg = RotateUp3(img)

        '    newImg.Save("c:\temp\Pennies\set1-rotated\" & i & ".tif")

        '    Me.PictureBox2.Image = newImg
        '    Me.PictureBox2.Refresh()


        '    img.Dispose()
        '    newImg.Dispose()

        'Next


    End Sub



    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ConvertGrayscale(Me.txtSetNumber.Text)
        MsgBox("Done")
    End Sub

    Private Function RotateUp(img As Bitmap) As Image

        '-- rotate image and check each box for minimum pixel color distribution --'

        Dim x1 As Integer = 190
        Dim y1 As Integer = 85
        Dim x2 As Integer = 225
        Dim y2 As Integer = 160

        Dim bestAngle As Integer = 0

        Dim total As Integer = 0
        Dim count As Integer = 0
        Dim lowestDiff As Double = 99999999999


        For rotate = 0 To 360 Step 2

            Dim newImg = RotateImage(img, rotate)

            Dim totalDiff As Double = 0

            For x = x1 To x2
                For y = y1 To y2

                    '-- get pixel brightness --'
                    Dim r = CInt(newImg.GetPixel(x, y).R)

                    '-- keep total for average pixel brightness --'
                    count += 1
                    total += r
                    Dim avg = total / count
                    Dim diff = Math.Abs(r - avg)
                    totalDiff += diff

                Next
            Next

            If totalDiff < lowestDiff Then
                lowestDiff = totalDiff
                bestAngle = rotate
            End If

            newImg.Dispose()

        Next

        img = RotateImage(img, bestAngle)

        Return img

    End Function

    Private Function RotateUp2(img As Bitmap) As Image

        '-- check for the word LIBERTY where the boxed range is the brightest --'

        Dim x1 As Integer = 10
        Dim y1 As Integer = 130
        Dim x2 As Integer = 80
        Dim y2 As Integer = 150

        Dim bestAngle As Integer = 0

        Dim highestTotal As Integer = 0
        Dim total As Integer = 0


        For rotate = 0 To 360 Step 2

            Dim newImg = RotateImage(img, rotate)

            total = 0

            For x = x1 To x2
                For y = y1 To y2

                    '-- get pixel brightness --'
                    Dim r = CInt(newImg.GetPixel(x, y).R)

                    '-- keep total for pixel brightness --'
                    total += r

                Next
            Next

            If total > highestTotal Then
                highestTotal = total
                bestAngle = rotate
                Me.PictureBox3.Image = newImg
                Me.PictureBox3.Refresh()
                Me.TextBox1.Text = "Higest: " & highestTotal & vbCrLf & TextBox1.Text
                Me.TextBox1.Refresh()
            End If

            Me.PictureBox2.Image = newImg
            Me.PictureBox2.Refresh()
            Threading.Thread.Sleep(100)

            newImg.Dispose()

        Next

        img = RotateImage(img, bestAngle)

        Return img

    End Function

    Private Function RotateUp3(img As Bitmap) As Image

        '-- check for the word LIBERTY where the boxed range is the brightest --'

        '-- 89 out of 100

        Dim x1 As Integer = 0
        Dim y1 As Integer = 0
        Dim x2 As Integer = 0
        Dim y2 As Integer = 0

        Dim bestAngle As Integer = 0

        Dim highestTotal As Integer = 0
        Dim total As Integer = 0


        For rotate = 0 To 360 Step 2

            Dim newImg = RotateImage(img, rotate)

            total = 0

            '-- LIBERTY --'
            x1 = 15
            y1 = 145
            x2 = 65
            y2 = 145

            For x = x1 To x2
                For y = y1 To y2

                    '-- get pixel brightness difference --'
                    Dim r1 = CInt(newImg.GetPixel(x, y).R)
                    Dim r2 = CInt(newImg.GetPixel(x + 1, y).R)
                    total += Math.Abs(r1 - r2)

                Next
            Next


            '-- YEAR --'
            x1 = 175
            y1 = 180
            x2 = 225
            y2 = 180

            For x = x1 To x2
                For y = y1 To y2

                    '-- get pixel brightness differenc3 --'
                    Dim r1 = CInt(newImg.GetPixel(x, y).R)
                    Dim r2 = CInt(newImg.GetPixel(x + 1, y).R)
                    total += Math.Abs(r1 - r2)

                Next
            Next





            If total > highestTotal Then
                highestTotal = total
                bestAngle = rotate
                Me.PictureBox3.Image = newImg
                Me.PictureBox3.Refresh()
                Me.TextBox1.Text = "Highest: " & highestTotal & vbCrLf & TextBox1.Text
                Me.TextBox1.Refresh()
            End If

            Me.PictureBox2.Image = newImg
            Me.PictureBox2.Refresh()
            'Threading.Thread.Sleep(10)

            newImg.Dispose()

        Next

        img = RotateImage(img, bestAngle)

        Return img

    End Function

    Private Function RotateUp4(img As Bitmap) As Image

        '-- check for the word LIBERTY where the boxed range is the brightest --'
        '-- set1 76 of 100 --

        Dim x1 As Integer = 15
        Dim y1 As Integer = 145
        Dim x2 As Integer = 65
        Dim y2 As Integer = 145

        Dim bestAngle As Integer = 0

        Dim highestTotal As Integer = 0
        Dim total As Integer = 0


        For rotate = 0 To 360 Step 2

            Dim newImg = RotateImage(img, rotate)

            total = 0

            '-- LIBERTY --'
            For x = x1 To x2
                For y = y1 To y2

                    '-- get pixel brightness difference --'
                    Dim r1 = CInt(newImg.GetPixel(x, y).R)
                    Dim r2 = CInt(newImg.GetPixel(x + 1, y).R)

                    total += Math.Abs(r1 - r2)

                Next
            Next


            '-- YEAR --'
            x1 = 175
            y1 = 180
            x2 = 225
            y2 = 180
            For x = x1 To x2
                For y = y1 To y2
                    '-- get pixel brightness differenc3 --'
                    Dim r1 = CInt(newImg.GetPixel(x, y).R)
                    Dim r2 = CInt(newImg.GetPixel(x + 1, y).R)
                    total += Math.Abs(r1 - r2)
                Next
            Next



            '-- VERTICAL LINES --'
            x1 = 65
            y1 = 80
            x2 = 65
            y2 = 125
            For x = x1 To x2
                For y = y1 To y2
                    '-- get pixel brightness difference --'
                    Dim r1 = CInt(newImg.GetPixel(x, y).R)
                    Dim r2 = CInt(newImg.GetPixel(x, y + 1).R)

                    Dim r3 = CInt(newImg.GetPixel(x + 145, y).R)
                    Dim r4 = CInt(newImg.GetPixel(x + 145, y + 1).R)

                    '-- subtract from total because we want low change rate on these lines
                    total -= (Math.Abs(r1 - r2) + Math.Abs(r3 - r4))
                Next
            Next


            If total > highestTotal Then
                highestTotal = total
                bestAngle = rotate
                Me.TextBox1.Text = "Higest: " & highestTotal & vbCrLf & TextBox1.Text
                Me.TextBox1.Refresh()
            End If

            Me.PictureBox2.Image = newImg
            Me.PictureBox2.Refresh()

            newImg.Dispose()

        Next

        img = RotateImage(img, bestAngle)
        Me.PictureBox3.Image = img
        Me.PictureBox3.Refresh()

        Return img

    End Function


    Private Function RotateUp5(img As Bitmap, ByRef rotateAngle As Integer) As Image

        '-- check for the word LIBERTY where the boxed range is the brightest --'
        '-- set1 76 of 100 --

        Dim x1 As Integer = 0
        Dim y1 As Integer = 0
        Dim x2 As Integer = 0
        Dim y2 As Integer = 0

        Dim bestAngle As Integer = 0

        Dim highestTotal As Integer = 0
        Dim total As Integer = 0


        For rotate = 0 To 360 Step 2

            Dim newImg = RotateImage(img, rotate)

            total = 0

            '-- LIBERTY --'
            x1 = 15
            y1 = 145
            x2 = 65
            y2 = 145

            For x = x1 To x2
                For y = y1 To y2
                    '-- get pixel brightness difference --'
                    Dim r1 = CInt(newImg.GetPixel(x, y - 1).R)
                    Dim r2 = CInt(newImg.GetPixel(x + 1, y - 1).R)

                    Dim r3 = CInt(newImg.GetPixel(x, y).R)
                    Dim r4 = CInt(newImg.GetPixel(x + 1, y).R)

                    Dim r5 = CInt(newImg.GetPixel(x, y + 1).R)
                    Dim r6 = CInt(newImg.GetPixel(x + 1, y + 1).R)
                    total += Math.Abs(r1 - r2) + Math.Abs(r3 - r4) + Math.Abs(r5 - r6)
                Next
            Next


            '-- YEAR --'
            x1 = 175
            y1 = 180
            x2 = 225
            y2 = 180
            For x = x1 To x2
                For y = y1 To y2
                    '-- get pixel brightness differenc3 --'
                    Dim r1 = CInt(newImg.GetPixel(x, y - 1).R)
                    Dim r2 = CInt(newImg.GetPixel(x + 1, y - 1).R)
                    Dim r3 = CInt(newImg.GetPixel(x, y).R)
                    Dim r4 = CInt(newImg.GetPixel(x + 1, y).R)
                    Dim r5 = CInt(newImg.GetPixel(x, y + 1).R)
                    Dim r6 = CInt(newImg.GetPixel(x + 1, y + 1).R)
                    total += Math.Abs(r1 - r2) + Math.Abs(r3 - r4) + Math.Abs(r5 - r6)
                Next
            Next



            '-- VERTICAL LINES --'
            x1 = 65
            y1 = 80
            x2 = 65
            y2 = 125
            For x = x1 To x2
                For y = y1 To y2
                    '-- get pixel brightness difference --'
                    Dim r1 = CInt(newImg.GetPixel(x - 1, y).R)
                    Dim r2 = CInt(newImg.GetPixel(x - 1, y + 1).R)
                    Dim r3 = CInt(newImg.GetPixel(x, y).R)
                    Dim r4 = CInt(newImg.GetPixel(x, y + 1).R)
                    Dim r5 = CInt(newImg.GetPixel(x + 1, y).R)
                    Dim r6 = CInt(newImg.GetPixel(x + 1, y + 1).R)
                    total -= (Math.Abs(r1 - r2) + Math.Abs(r3 - r4) + Math.Abs(r5 - r6))
                Next
            Next
            x1 = 210
            y1 = 80
            x2 = 210
            y2 = 125
            For x = x1 To x2
                For y = y1 To y2
                    '-- get pixel brightness difference --'
                    Dim r1 = CInt(newImg.GetPixel(x - 1, y).R)
                    Dim r2 = CInt(newImg.GetPixel(x - 1, y + 1).R)
                    Dim r3 = CInt(newImg.GetPixel(x, y).R)
                    Dim r4 = CInt(newImg.GetPixel(x, y + 1).R)
                    Dim r5 = CInt(newImg.GetPixel(x + 1, y).R)
                    Dim r6 = CInt(newImg.GetPixel(x + 1, y + 1).R)
                    total -= (Math.Abs(r1 - r2) + Math.Abs(r3 - r4) + Math.Abs(r5 - r6))
                Next
            Next


            If total > highestTotal Then
                highestTotal = total
                bestAngle = rotate
                rotateAngle = bestAngle
                Me.TextBox1.Text = "Highest: " & highestTotal & vbCrLf & TextBox1.Text
                Me.TextBox1.Refresh()
            End If

            Me.PictureBox2.Image = newImg
            Me.PictureBox2.Refresh()

            newImg.Dispose()

        Next

        img = RotateImage(img, bestAngle)
        Me.PictureBox3.Image = img
        Me.PictureBox3.Refresh()

        Return img

    End Function

    Private Function RotateUp6(img As Bitmap, ByRef rotateAngle As Integer) As Image

        Dim x1 As Integer = 0
        Dim y1 As Integer = 0
        Dim x2 As Integer = 0
        Dim y2 As Integer = 0

        Dim bestAngle As Integer = 0

        Dim highestTotal As Integer = 0
        Dim total As Integer = 0


        For rotate = 0 To 360 Step 2

            Dim newImg = RotateImage(img, rotate)

            total = 0

            '-- LIBERTY --'
            x1 = 20
            y1 = 150
            x2 = 90
            y2 = 150

            For x = x1 To x2
                For y = y1 To y2
                    '-- get pixel brightness difference --'
                    Dim r1 = CInt(newImg.GetPixel(x, y - 1).R)
                    Dim r2 = CInt(newImg.GetPixel(x + 1, y - 1).R)

                    Dim r3 = CInt(newImg.GetPixel(x, y).R)
                    Dim r4 = CInt(newImg.GetPixel(x + 1, y).R)

                    Dim r5 = CInt(newImg.GetPixel(x, y + 1).R)
                    Dim r6 = CInt(newImg.GetPixel(x + 1, y + 1).R)
                    total += Math.Abs(r1 - r2) + Math.Abs(r3 - r4) + Math.Abs(r5 - r6)
                Next
            Next


            '-- YEAR --'
            x1 = 180
            y1 = 180
            x2 = 230
            y2 = 180
            For x = x1 To x2
                For y = y1 To y2
                    '-- get pixel brightness differenc3 --'
                    Dim r1 = CInt(newImg.GetPixel(x, y - 1).R)
                    Dim r2 = CInt(newImg.GetPixel(x + 1, y - 1).R)
                    Dim r3 = CInt(newImg.GetPixel(x, y).R)
                    Dim r4 = CInt(newImg.GetPixel(x + 1, y).R)
                    Dim r5 = CInt(newImg.GetPixel(x, y + 1).R)
                    Dim r6 = CInt(newImg.GetPixel(x + 1, y + 1).R)
                    total += Math.Abs(r1 - r2) + Math.Abs(r3 - r4) + Math.Abs(r5 - r6)
                Next
            Next



            '-- VERTICAL LINES --'
            '-- subtracting from total because these should be smooth
            x1 = 50
            y1 = 90
            x2 = 50
            y2 = 120
            For x = x1 To x2
                For y = y1 To y2
                    '-- get pixel brightness difference --'
                    Dim r1 = CInt(newImg.GetPixel(x - 1, y).R)
                    Dim r2 = CInt(newImg.GetPixel(x - 1, y + 1).R)
                    Dim r3 = CInt(newImg.GetPixel(x, y).R)
                    Dim r4 = CInt(newImg.GetPixel(x, y + 1).R)
                    Dim r5 = CInt(newImg.GetPixel(x + 1, y).R)
                    Dim r6 = CInt(newImg.GetPixel(x + 1, y + 1).R)
                    total -= (Math.Abs(r1 - r2) + Math.Abs(r3 - r4) + Math.Abs(r5 - r6))
                Next
            Next
            x1 = 210
            y1 = 90
            x2 = 210
            y2 = 120
            For x = x1 To x2
                For y = y1 To y2
                    '-- get pixel brightness difference --'
                    Dim r1 = CInt(newImg.GetPixel(x - 1, y).R)
                    Dim r2 = CInt(newImg.GetPixel(x - 1, y + 1).R)
                    Dim r3 = CInt(newImg.GetPixel(x, y).R)
                    Dim r4 = CInt(newImg.GetPixel(x, y + 1).R)
                    Dim r5 = CInt(newImg.GetPixel(x + 1, y).R)
                    Dim r6 = CInt(newImg.GetPixel(x + 1, y + 1).R)
                    total -= (Math.Abs(r1 - r2) + Math.Abs(r3 - r4) + Math.Abs(r5 - r6))
                Next
            Next


            If total > highestTotal Then
                highestTotal = total
                bestAngle = rotate
                rotateAngle = bestAngle
                Me.TextBox1.Text = "Highest: " & highestTotal & vbCrLf & TextBox1.Text
                Me.TextBox1.Refresh()
            End If

            Me.PictureBox2.Image = newImg
            Me.PictureBox2.Refresh()

            newImg.Dispose()

        Next

        img = RotateImage(img, bestAngle)
        Me.PictureBox3.Image = img
        Me.PictureBox3.Refresh()

        Return img

    End Function



    Private Function ConvertBW(img As Image) As Image

        Using ms As New MemoryStream
            Dim parms As EncoderParameters = New EncoderParameters(1)
            Dim codec As ImageCodecInfo = ImageCodecInfo.GetImageDecoders().FirstOrDefault(Function(decoder) decoder.FormatID = Imaging.ImageFormat.Tiff.Guid)
            parms.Param(0) = New EncoderParameter(Encoder.Compression, CLng(EncoderValue.CompressionCCITT4))
            img.Save(ms, codec, parms)
            Return Bitmap.FromStream(ms)
        End Using

    End Function


    Private Function RotateImage(img As Image, angle As Single) As Bitmap
        Dim retBMP As New Bitmap(img.Width, img.Height)
        retBMP.SetResolution(img.HorizontalResolution, img.VerticalResolution)
        Using g = Graphics.FromImage(retBMP)
            g.TranslateTransform(img.Width \ 2, img.Height \ 2)
            g.RotateTransform(angle)
            g.TranslateTransform(-img.Width \ 2, -img.Height \ 2)
            g.DrawImage(img, New PointF(0, 0))
            Return retBMP
        End Using
    End Function


    Private Function NormalizeImage(img As Bitmap) As Bitmap

        Dim w = img.Width
        Dim h = img.Height
        Dim sd As BitmapData = img.LockBits(New Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb)

        Dim bytes As Integer = sd.Stride * sd.Height
        Dim buffer As Byte() = New Byte(bytes - 1) {}
        Dim result As Byte() = New Byte(bytes - 1) {}

        Runtime.InteropServices.Marshal.Copy(sd.Scan0, buffer, 0, bytes)
        img.UnlockBits(sd)

        Dim current As Integer = 0
        Dim max As Byte = 0
        Dim min As Byte = 0

        For i = 0 To buffer.Length - 1
            max = Math.Max(max, buffer(i))
            min = Math.Min(min, buffer(i))
        Next

        For y = 0 To h - 1
            For x = 0 To w - 1
                current = y * sd.Stride + x * 4
                For i = 0 To 2
                    result(current + i) = CByte((buffer(current + i) - min) * 100 / (max - min))
                Next
                result(current + 3) = 255
            Next
        Next

        Dim resimg = New Bitmap(w, h)
        Dim rd As BitmapData = resimg.LockBits(New Rectangle(0, 0, w, h), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb)
        Runtime.InteropServices.Marshal.Copy(result, 0, rd.Scan0, bytes)
        resimg.UnlockBits(rd)
        Return resimg

    End Function


    Private Function ConvertGS(img As Image) As Image

        imgattr.SetColorMatrix(grayscale)
        Using g As Graphics = Graphics.FromImage(img)
            g.DrawImage(img, New Rectangle(0, 0, img.Width, img.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgattr)
        End Using
        Return img

    End Function

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        If IsNumeric(Me.txtSetNumber.Text) Then

            SplitSheet(Me.txtSetNumber.Text)
            ConvertGrayscale(Me.txtSetNumber.Text)
            Rotated(Me.txtSetNumber.Text)

            MsgBox("Done with sheet: " & Me.txtSetNumber.Text)

        End If

    End Sub


    Private Sub SplitSheet(SheetNumber)

        Dim Pennys = Image.FromFile("c:\temp\pennies\sheets\" & SheetNumber & ".tif")
        Dim Penny = New Image(100) {}
        Dim size As Integer = 268

        For x = 0 To 9
            For y = 0 To 9
                Dim index = x * 10 + y
                Penny(index) = New Bitmap(size, size)
                Dim g = Graphics.FromImage(Penny(index))
                g.DrawImage(Pennys, New Rectangle(0, 0, size, size), New Rectangle(x * size, y * size, size, size), GraphicsUnit.Pixel)
                g.Dispose()
            Next
        Next

        Dim a = ((SheetNumber - 1) * 100) + 1
        Dim b = a + 99
        Dim c = 0


        For i = a To b
            Dim bitmap = New Bitmap(Penny(c))
            bitmap.Save("c:\temp\Pennies\color\" & i & ".tif", ImageFormat.Tiff)
            c += 1
        Next

    End Sub

    Private Sub ConvertGrayscale(SheetNumber)
        Dim a = ((SheetNumber - 1) * 100) + 1
        Dim b = a + 99

        For i = a To b
            Dim img = Bitmap.FromFile("c:\temp\pennies\color\" & i & ".tif")
            img = ConvertGS(img)
            img.Save("c:\temp\pennies\gs\" & i & ".tif", ImageFormat.Tiff)
            img.Dispose()
        Next

    End Sub

    Private Sub Rotated(SheetNumber)
        Dim a = ((SheetNumber - 1) * 100) + 1
        Dim b = a + 99

        For i = a To b
            Dim img = Bitmap.FromFile("c:\temp\pennies\gs\" & i & ".tif")

            Dim rotatedAngle As Integer = 0
            img = RotateUp6(img, rotatedAngle)
            img.Save("c:\temp\pennies\gs-rotated\" & i & ".tif", ImageFormat.Tiff)
            img.Dispose()

            img = Bitmap.FromFile("c:\temp\pennies\color\" & i & ".tif")
            img = RotateImage(img, rotatedAngle)
            img.Save("c:\temp\pennies\color-rotated\" & i & ".tif", ImageFormat.Tiff)
            img.Dispose()

        Next


    End Sub

End Class
