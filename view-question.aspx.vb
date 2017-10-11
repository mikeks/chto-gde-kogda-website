
Imports System.IO

Partial Class ViewQuestionPage
	Inherits System.Web.UI.Page

	Protected GameNum As Integer
	Protected QuestionNum As Integer

	Protected Qu As GameQuestion

	Protected Overrides Sub OnLoad(e As EventArgs)
		MyBase.OnLoad(e)
		UserManager.TestAccess()

        Try
			GameNum = Request("GameNum")
            QuestionNum = Request("QuestionNum")

            If GameNum > Utility.QuestionsAvaliableForGame Then Throw New Exception("Not avaliable")

            'DirectCast(Master, MasterPage).CanonicalUrl = "http://chtogdekogda.org/view-question.aspx?gameNum=" & GameNum & "&questionNum=" & QuestionNum


            Qu = GameQuestion.GetQuestion(GameNum, QuestionNum)
            If Qu.QuestionText Is Nothing Then Throw New Exception()
        Catch ex As Exception
			Response.Redirect("/", True)
		End Try


	End Sub



End Class
