
Imports System.IO

Partial Class TeamQuestionPage
    Inherits System.Web.UI.Page

    Protected ErrorMsg As String
    Protected IsSaved As Boolean

    Protected Qu As TeamQuestion

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
        UserManager.TestAccess(True)

        If Request("save") IsNot Nothing Then



            Dim t As New TeamQuestion() With {
                .Team = UserManager.CurrentUser.Team,
                .QuestionText = Request("question"),
                .Answer = Request("answer"),
                .Comments = Request("comments"),
                .Sources = Request("sources"),
                .Author = Request("author"),
                .IsReady = Request("questionReady") IsNot Nothing,
                .IsBlackBox = Request("blackBox") IsNot Nothing
            }

            If razd.PostedFile IsNot Nothing AndAlso razd.PostedFile.ContentLength > 0 Then
                Dim ext = Path.GetExtension(razd.PostedFile.FileName).ToLowerInvariant()
                If ext <> ".jpg" And ext <> ".png" Then
                    ErrorMsg = "Раздача должна быть в формете PNG или JPG."
                ElseIf razd.PostedFile.ContentLength > 10000000 Then
                    ErrorMsg = "Что за...?? Почему файл раздачи такой большой?"
                Else
                    Dim len = razd.PostedFile.ContentLength
                    ReDim t.ImageData(len)
                    razd.PostedFile.InputStream.Read(t.ImageData, 0, len)
                End If
            End If



            t.Save()
            IsSaved = True

        End If

        Qu = TeamQuestion.GetQuestion(UserManager.CurrentUser.Team.Id)

    End Sub



End Class
