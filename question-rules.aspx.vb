
Partial Class TeamQuestionPage
    Inherits System.Web.UI.Page

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
        UserManager.TestAccess(True)
    End Sub

End Class
