
Partial Class personal
    Inherits System.Web.UI.Page

    Protected ErrMsg As String

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)

        UserManager.TestAccess()

        If Request("save") IsNot Nothing Then

            If Request("userType") Is Nothing Then
                ErrMsg = "Пожалуйста, выберите один из пунктов."
                Return
            End If

            Try
                UserManager.CurrentUser.UserType = [Enum].Parse(GetType(UserTypeEnum), Request("userType"))

                Dim tid As Integer = Request("team")

                If tid = 0 AndAlso UserManager.CurrentUser.UserType = UserTypeEnum.PlayerPhila Then
                    ErrMsg = "Выберите вашу команду."
                    Return
                End If

                If tid <> 0 Then
                    UserManager.CurrentUser.Team = Team.All(tid)
                Else
                    UserManager.CurrentUser.Team = Nothing
                End If

                UserManager.CurrentUser.IsTeamLeader = Request("cap") IsNot Nothing

                UserManager.CurrentUser.Save()

                UserManager.GotoMyPage()

            Catch ee As Exception
                ErrMsg = "Ошибка, кэп. Что-то пошло не так. " & ee.Message
            End Try



        End If



    End Sub

End Class
