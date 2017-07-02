Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports Microsoft.VisualBasic

Public Class Utility


    Public Const CurrentGameNum As Integer = 19



    Public Shared ReadOnly Property QuestionAcceptDaysLeft As Integer
        Get
            Return (New Date(2017, 6, 20) - Date.Now).Days
        End Get
    End Property

    Public Shared ReadOnly Property FacebookAppID As String
        Get
            Return ConfigurationManager.AppSettings("facebook.appId")
        End Get
    End Property

    Public Shared Function GetHtmlEmbeddedImg(data As Byte()) As String
        If data Is Nothing Then Return Nothing

        Dim s = Convert.ToBase64String(data)
        Return "data:image/jpeg;base64," + s

    End Function

    Public Shared Function ResizeImage(image As Image, width As Integer) As Bitmap
        Dim height As Integer = image.Height * width / image.Width
        Return ResizeImage(image, width, height)
    End Function

    Public Shared Function ResizeImage(image As Image, width As Integer, height As Integer) As Bitmap
        Dim destRect As New Rectangle(0, 0, width, height)
        Dim destImage As New Bitmap(width, height)

        destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution)

        Using gr = Graphics.FromImage(destImage)
            gr.CompositingMode = CompositingMode.SourceCopy
            gr.CompositingQuality = CompositingQuality.HighQuality
            gr.InterpolationMode = InterpolationMode.HighQualityBicubic
            gr.SmoothingMode = SmoothingMode.HighQuality
            gr.PixelOffsetMode = PixelOffsetMode.HighQuality

            Using wrapMode As New ImageAttributes()
                wrapMode.SetWrapMode(Drawing2D.WrapMode.TileFlipXY)
                gr.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode)
            End Using

        End Using

        Return destImage

    End Function


    Public Shared Function ExtractPar(ByVal s As String, ByVal paramName As String) As String
        If String.IsNullOrEmpty(s) Then Return Nothing
        Dim Idx As Integer = s.IndexOf(paramName & "=")
        If Idx = -1 Then Return Nothing
        Dim Idx2 As Integer = s.IndexOf("&", Idx)
        If Idx2 = -1 Then Idx2 = s.Length
        Return s.Substring(Idx + paramName.Length + 1, Idx2 - Idx - paramName.Length - 1)
    End Function

    Public Shared Function EncodeByte(ByVal src As Byte()) As Byte()
        Dim tdes As New System.Security.Cryptography.TripleDESCryptoServiceProvider()
        Dim mem As New System.IO.MemoryStream()
        tdes.Key = Convert.FromBase64String(ConfigurationManager.AppSettings("ciph.key"))
        tdes.IV = Convert.FromBase64String(ConfigurationManager.AppSettings("ciph.iv"))
        Dim sst As New System.Security.Cryptography.CryptoStream(mem, tdes.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write)
        sst.Write(src, 0, src.Length)
        sst.Close()
        Return mem.ToArray()
    End Function


    Public Shared Function DecodeByte(ByVal src As Byte()) As Byte()
        Dim tdes As New System.Security.Cryptography.TripleDESCryptoServiceProvider()
        Dim mem As New System.IO.MemoryStream()
        tdes.Key = Convert.FromBase64String(ConfigurationManager.AppSettings("ciph.key"))
        tdes.IV = Convert.FromBase64String(ConfigurationManager.AppSettings("ciph.iv"))
        Dim sst As New System.Security.Cryptography.CryptoStream(mem, tdes.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Write)
        sst.Write(src, 0, src.Length)
        sst.Close()
        Return mem.ToArray()
    End Function

    Public Shared Function Base64Url(ByVal bytes As Byte()) As String
        Return Convert.ToBase64String(bytes).TrimEnd("="c).Replace("+", "-").Replace("/", "_")
    End Function

    Public Shared Function Base64Url(ByVal data As String) As Byte()
        Dim pad As Integer = data.Length Mod 4
        If pad > 0 Then data = data.PadRight(data.Length + 4 - pad, "="c)
        Return Convert.FromBase64String(data.Replace("-", "+").Replace("_", "/"))
    End Function



    Public Shared Function EncodeText(ByVal txt As String) As String
        If (txt.Length = 0) Then Return ""
        Return Base64Url(EncodeByte(Encoding.ASCII.GetBytes(txt)))
    End Function

    Public Shared Function DecodeText(ByVal txt As String) As String
        If (txt Is Nothing OrElse txt.Length = 0) Then Return String.Empty
        Return Encoding.ASCII.GetString(DecodeByte(Base64Url(txt)))
    End Function





End Class
