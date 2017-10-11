
Partial Class AdminPage
    Inherits System.Web.UI.Page


    Protected Overrides Sub OnLoad(e As EventArgs)

        MyBase.OnLoad(e)
		UserManager.TestAdminAccess()


		If Request("addPlayer") IsNot Nothing Then
            UserManager.ApproveUser(Request("addPlayer"))
        ElseIf Request("rejectPlayer") IsNot Nothing Then
            UserManager.RejectUser(Request("rejectPlayer"))
        End If





    End Sub


End Class
