
Partial Class LoginPage
    Inherits System.Web.UI.Page

    Protected ErrorMsg As String

    Protected Overrides Sub OnLoad(e As EventArgs)

        MyBase.OnLoad(e)

        If Request("save") IsNot Nothing Then


            If Not UserManager.OnDirectLogin(Request("email"), Request("password")) Then
                ErrorMsg = "Неверный email или пароль."
            End If

        End If
    End Sub

End Class
