
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

    Protected ReadOnly Property Gallery As TeamGalleryPhoto()
        Get
            Return TeamGalleryPhoto.GetForTeam(Tm.Id)
        End Get
    End Property

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
        UserManager.TestAccess(True)

        If UserManager.CurrentUser.UserType = UserTypeEnum.Admin Then
            Try
                If Request("teamid") IsNot Nothing Then
                    Session("adminTeamId") = Request("teamid")
                End If
                _teamId = Session("adminTeamId")
                If _teamId = 0 Then Throw New Exception("no teamid")
            Catch
                Response.Redirect("/", True)
            End Try
        End If


        If Request("delPhoto") IsNot Nothing Then
            Try
                Dim ph = TeamGalleryPhoto.GetById(Request("delPhoto"))
                ph.Delete()
            Catch
            End Try
            Response.Redirect("/team-edit.aspx")
        End If

        If Request("save") IsNot Nothing Then

            Tm.AboutText = Request("aboutText")


            If ImageProcessor.ProcessImageUpload(Photo, ErrorMsg) Then
                Tm.SetTeamImage(Photo.PostedFile.InputStream)
            End If

            ImageProcessor.SaveGalleryImage(GalleryPhoto, ErrorMsg, Tm)

            Tm.Save()

            For Each gp In Gallery
                Dim descr = Request("photoDescription_" & gp.Id)
                If descr IsNot Nothing AndAlso descr.Trim() <> gp.Description Then
                    gp.Description = descr
                    gp.SaveDescription()
                End If
            Next

            IsSaved = True

        End If


    End Sub

End Class
