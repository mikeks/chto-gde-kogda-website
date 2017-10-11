
Imports System.IO

Partial Class TeamQuestionPage
    Inherits System.Web.UI.Page

    Protected ErrorMsg As String
    Protected IsSaved As Boolean

	Protected GameNum As Integer
	Protected QuestionNum As Integer


	Protected Qu As GameQuestion

	Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
		UserManager.TestAdminAccess()

		GameNum = Request("GameNum")
		QuestionNum = Request("QuestionNum")

		If Request("save") IsNot Nothing Then

			Dim t As New GameQuestion() With {
				.GameNum = GameNum,
				.QuestionNum = QuestionNum,
				.QuestionText = Request("question"),
				.Answer = Request("answer"),
				.AcceptedAnswers = Request("acceptedAnswers"),
				.Comments = Request("comments"),
				.Sources = Request("sources"),
				.Author = Request("author"),
				.EditorMark = Request("EditorMark"),
				.EditorComments = Request("EditorComments"),
				.IsBlackBox = Request("blackBox") IsNot Nothing,
				.IsBestOfTheGame = Request("IsBestOfTheGame") IsNot Nothing
			}

			Dim teamId As Integer = Request("team")
			If teamId > 0 Then
				t.Team = Team.All(teamId)
			End If


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

			Qu = GameQuestion.GetQuestion(GameNum, QuestionNum)


	End Sub



End Class
