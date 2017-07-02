
Partial Class TeamPage
    Inherits System.Web.UI.Page


    Protected Overrides Sub OnLoad(e As EventArgs)

        MyBase.OnLoad(e)
        UserManager.TestAccess(True)
        If UserManager.CurrentUser.UserType = UserTypeEnum.Admin Then Response.Redirect("/admin.aspx", True)

        If UserManager.CurrentUser.IsTeamLeader Then

            If Request("addPlayer") IsNot Nothing Then
                UserManager.ApproveUser(Request("addPlayer"))
            ElseIf Request("rejectPlayer") IsNot Nothing Then
                UserManager.RejectUser(Request("rejectPlayer"))
            End If

        End If


    End Sub


End Class
