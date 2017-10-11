
Partial Class _Default
	Inherits System.Web.UI.Page

    Protected totalQuestions As String
    Protected teamList As String
    'Protected readyQuestions As Integer

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)

        DirectCast(Master, MasterPage).CanonicalUrl = "http://chtogdekogda.org"

        If Request("logout") IsNot Nothing Then
            UserManager.CurrentUser = Nothing
            Response.Redirect("/", True)
        End If

        Dim qq = TeamQuestion.GetAll()
        Dim cnt = qq.Count()
        If cnt = 1 Then
            totalQuestions = "1 команда"
        ElseIf cnt >= 2 AndAlso cnt <= 4 Then
            totalQuestions = cnt & " команды"
        Else
            totalQuestions = cnt & " команд"
        End If

        teamList = ""
        For Each q In qq
            If teamList.Length > 0 Then teamList &= ", "
            teamList &= q.Team.Name
        Next

        '   readyQuestions = qq.Count(Function(x) x.IsReady)

    End Sub


End Class
