Imports System.IO
Imports Microsoft.VisualBasic

Public Class ImageProcessor

    Public Shared Function ProcessImageUpload(photo As HtmlInputFile, ByRef errorMsg As String) As Boolean

        If photo.PostedFile IsNot Nothing AndAlso photo.PostedFile.ContentLength > 0 Then
            Dim ext = Path.GetExtension(photo.PostedFile.FileName).ToLowerInvariant()
            If ext <> ".jpg" And ext <> ".png" Then
                errorMsg = "Фотка должна быть в формете PNG или JPG."
                Return False
            ElseIf photo.PostedFile.ContentLength > 10000000 Then
                errorMsg = "Слишком большой файл. Загрузите файл поменьше."
                Return False
            End If
            Return True
        Else
            Return False
        End If
    End Function



    Public Shared Sub SaveGalleryImage(photo As HtmlInputFile, ByRef errorMsg As String, team As Team)

        If Not ProcessImageUpload(photo, errorMsg) Then Return

        Dim ph = HttpContext.Current.Server.MapPath("~/Img/teams-gallery/" & team.UrlName)
        Directory.CreateDirectory(ph)

        Dim galleryPhoto = TeamGalleryPhoto.CreateNew(team)


        Dim fnum = galleryPhoto.Id
        Dim smallPh = HttpContext.Current.Server.MapPath(galleryPhoto.SmallImageUrl)
        Dim bigPh = HttpContext.Current.Server.MapPath(galleryPhoto.BigImageUrl)

        Try
            If File.Exists(smallPh) Then File.Delete(smallPh)
        Catch
        End Try

        Try
            If File.Exists(bigPh) Then File.Delete(bigPh)
        Catch
        End Try

        Using img = Drawing.Image.FromStream(photo.PostedFile.InputStream)
            Dim img300 = Utility.ResizeImage(img, 300)
            img300.Save(smallPh)
            img.Save(bigPh)
        End Using

    End Sub



End Class
