Imports Microsoft.VisualBasic

Public Class UserManager

    Public Shared Sub ClearUserCache()
        HttpContext.Current.Session("user") = Nothing
    End Sub

    Public Shared Property CurrentUser As User
        Get
            Dim us = HttpContext.Current.Session("user")
            If us IsNot Nothing Then Return us

            Dim tt = HttpContext.Current.Request.Cookies("ut")
            If tt Is Nothing Then Return Nothing

            Try

                Dim ticket As String = Utility.DecodeText(tt.Value)

                Dim ttt = ticket.Split("#")

                Dim uId = Integer.Parse(ttt(0))

                Dim u = GetUserById(uId)

                HttpContext.Current.Session("user") = u
                Return u

            Catch
                Return Nothing
            End Try

        End Get
        Set(u As User)
            HttpContext.Current.Session("user") = u

            If u Is Nothing Then
                HttpContext.Current.Response.Cookies.Add(New HttpCookie("ut", Nothing) With {.Expires = DateTime.Now.AddDays(-1)})
            Else
                Dim ticket As String = Utility.EncodeText(u.Id & "#" & New Random().Next(99999))
                HttpContext.Current.Response.Cookies.Add(New HttpCookie("ut", ticket) With {.Expires = DateTime.Now.AddDays(30)})
            End If


        End Set

    End Property

    Private Shared Function GetUserById(id As Integer) As User

        Dim usr As User = Nothing

        DbObject.ReadStoredProc("GetUserById",
            Sub(cmd)
                cmd.Parameters.AddWithValue("@id", id)
            End Sub,
            Sub(rdr)
                usr = New User
                usr.ReadFromDb(rdr)
            End Sub
        )

        Return usr
    End Function

    Public Shared Sub OnFacabookLogin(u As FaceBookUser)

        Dim usr As New User

        DbObject.ReadStoredProc("FacebookLogin",
            Sub(cmd)
                cmd.Parameters.AddWithValue("@facebookId", u.Id)
                cmd.Parameters.AddWithValue("@name", u.Name)
                cmd.Parameters.AddWithValue("@email", u.EMail)
            End Sub,
            Sub(rdr)
                usr.ReadFromDb(rdr)
            End Sub
        )

        CurrentUser = usr

        GotoMyPage()
    End Sub


    Public Shared Function OnDirectLogin(email As String, password As String) As Boolean

        If String.IsNullOrWhiteSpace(email) OrElse String.IsNullOrWhiteSpace(password) Then Return False

        Dim usr As User = Nothing

        DbObject.ReadStoredProc("DirectLogin",
            Sub(cmd)
                cmd.Parameters.AddWithValue("@email", email)
                cmd.Parameters.AddWithValue("@password", password)
            End Sub,
            Sub(rdr)
                usr = New User
                usr.ReadFromDb(rdr)
            End Sub
        )

        If usr Is Nothing Then Return False

        CurrentUser = usr

        GotoMyPage()

        Return True

    End Function

    Public Shared Sub ApproveUser(userId As Integer)

        If CurrentUser Is Nothing OrElse (CurrentUser.UserType <> UserTypeEnum.Admin AndAlso (Not CurrentUser.IsTeamLeader OrElse CurrentUser.Team Is Nothing)) Then Return

        DbObject.ExecStoredProc("ApproveUser",
            Sub(cmd)
                cmd.Parameters.AddWithValue("@userId", userId)
                cmd.Parameters.AddWithValue("@approvedById", CurrentUser.Id)
            End Sub
        )
    End Sub

    Public Shared Sub RejectUser(userId As Integer)

        If CurrentUser Is Nothing OrElse (CurrentUser.UserType <> UserTypeEnum.Admin AndAlso (Not CurrentUser.IsTeamLeader OrElse CurrentUser.Team Is Nothing)) Then Return

        DbObject.ExecStoredProc("RejectUser",
            Sub(cmd)
                cmd.Parameters.AddWithValue("@userId", userId)
            End Sub
        )
    End Sub



    Public Shared Sub TestAccess(Optional teamPage As Boolean = False)
        If CurrentUser Is Nothing Then HttpContext.Current.Response.Redirect("/", True)
        If teamPage Then
            If CurrentUser.UserType = UserTypeEnum.Admin Then Return ' bypass
            If CurrentUser.Team Is Nothing OrElse CurrentUser.UserType = UserTypeEnum.None Then HttpContext.Current.Response.Redirect("/personal.aspx", True)
            If Not CurrentUser.IsApproved Then HttpContext.Current.Response.Redirect("/team-not-approved.aspx", True)
        End If

    End Sub

	Public Shared Sub TestAdminAccess()
		If CurrentUser Is Nothing OrElse CurrentUser.UserType <> UserTypeEnum.Admin Then HttpContext.Current.Response.Redirect("/", True)
	End Sub


	Public Shared Sub GotoMyPage()

        If CurrentUser Is Nothing Then
            HttpContext.Current.Response.Redirect("/")
            Return
        End If

        If CurrentUser.UserType = UserTypeEnum.NotSelected Then
            My.Response.Redirect("/personal.aspx")
            Return
        End If


        Select Case CurrentUser.UserType
            Case UserTypeEnum.PlayerPhila
                HttpContext.Current.Response.Redirect("/team.aspx")
            Case UserTypeEnum.Admin
                HttpContext.Current.Response.Redirect("/admin.aspx")
            Case Else
                HttpContext.Current.Response.Redirect("/")
        End Select


    End Sub



End Class
