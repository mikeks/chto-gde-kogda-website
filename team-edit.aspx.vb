
Imports System.IO

Partial Class TeamPageEdit
    Inherits System.Web.UI.Page


    Protected ErrorMsg As String
    Protected IsSaved As Boolean

    Dim _teamId As Integer

    Protected ReadOnly Property Tm As Team
        Get
            If UserManager.CurrentUser.UserType = UserTypeEnum.Admin Then
                Return Team.All(_teamId)
            End If
            Return UserManager.CurrentUser.Team
        End Get
    End Property

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
        UserManager.TestAccess(True)

        If UserManager.CurrentUser.UserType = UserTypeEnum.Admin Then
            Try
                _teamId = Request("teamid")
                If _teamId = 0 Then Throw New Exception("no teamid")
            Catch
                Response.Redirect("/", True)
            End Try
        End If

        If Request("save") IsNot Nothing Then

            Tm.AboutText = Request("aboutText")

            If Photo.PostedFile IsNot Nothing AndAlso Photo.PostedFile.ContentLength > 0 Then
                Dim ext = Path.GetExtension(Photo.PostedFile.FileName).ToLowerInvariant()
                If ext <> ".jpg" And ext <> ".png" Then
                    ErrorMsg = "Раздача должна быть в формете PNG или JPG."
                ElseIf Photo.PostedFile.ContentLength > 10000000 Then
                    ErrorMsg = "Слишком большой файл. Загрузите файл поменьше."
                Else
                    'Dim len = Photo.PostedFile.ContentLength
                    'ReDim Tm.TeamImage(len)
                    Tm.SetTeamImage(Photo.PostedFile.InputStream)
                    'Photo.PostedFile.InputStream.Read(Tm.TeamImage, 0, len)
                End If
            End If

            Tm.Save()
            IsSaved = True

        End If


    End Sub

End Class
