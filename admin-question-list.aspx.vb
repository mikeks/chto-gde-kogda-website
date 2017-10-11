
Imports System.IO

Partial Class TeamQuestionPage
    Inherits System.Web.UI.Page

	Protected GameNum As Integer
	Protected Questions As GameQuestion()
	Protected NextQuestionNum As Integer = 1

	Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
		UserManager.TestAdminAccess()

		GameNum = Request("gameNum")
		Questions = GameQuestion.GetAllForGame(GameNum)

		Dim lastQ = Questions.LastOrDefault()

		If lastQ IsNot Nothing Then
			NextQuestionNum = lastQ.QuestionNum + 1
		End If


	End Sub



End Class
