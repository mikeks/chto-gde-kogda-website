<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Net" %>

<script runat="server">

    Private Shared ReadOnly Property CallbackURL As String
        Get
            Return My.Request.Url.GetLeftPart(UriPartial.Path)
        End Get
    End Property

    Private Function getAccessToken(ByVal code As String) As String
        ' do server-to-server auth call.
        Try
            Dim ph =
                String.Format("https://graph.facebook.com/oauth/access_token?client_id={0}&redirect_uri={1}&client_secret={2}&code={3}",
                              Utility.FacebookAppID,
                              HttpUtility.UrlEncode(CallbackURL),
                              FacebookLogin.AppSecret,
                              code
                            )

            Dim req As WebRequest = HttpWebRequest.Create(ph)
            req.Method = "GET"
            Dim response As String
            Using resp As WebResponse = req.GetResponse()
                Dim sr = New StreamReader(resp.GetResponseStream())
                response = sr.ReadToEnd()
                sr.Close()
            End Using

            Dim js = SimpleJSON.JSONDecoder.Decode(response)

            'Dim token = Utility.ExtractPar(response, "access_token")
            Return js("access_token").StringValue

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Sub ShowErr(Msg As String)
        Response.Write("<s" & "cript>alert(""" & Msg & """);location.href='/Registration.aspx'</s" & "cript>")
        Response.End()
    End Sub

    Protected Sub Load_Page(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

        Dim code As String = My.Request.QueryString("code")
        Dim accessToken As String = My.Request.QueryString("accessToken")
        If code IsNot Nothing OrElse accessToken IsNot Nothing Then
            If code IsNot Nothing Then
                'step 2. get access token
                accessToken = getAccessToken(code)
                If accessToken Is Nothing Then
                    ShowErr("Error #1")
                End If
            End If

            'step 3. login
            Dim fb As New FacebookLogin(accessToken)
            Dim u = fb.GetFaceBookUser()
            If u Is Nothing Then
                ShowErr("error 2")
            End If

            UserManager.OnFacabookLogin(u)

            'Response.Write("<div>SUCCESS</div>")
            'Response.Write(u.Name)

        Else
            'step 1. redirect to facebook for auth.

            Dim redirectURL = "https://www.facebook.com/v2.3/dialog/oauth?client_id=" & Utility.FacebookAppID & "&redirect_uri=" & HttpUtility.UrlEncode(CallbackURL) & "&scope=email,public_profile,user_friends"
            My.Response.Redirect(redirectURL, True)
        End If


    End Sub
</script>