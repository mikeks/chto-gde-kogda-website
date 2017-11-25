<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Net" %>
<%@ Import Namespace="Facebook" %>
<%@ Import Namespace="SimpleJSON" %>

<script runat="server">

	Private Shared ReadOnly Property AppId As String
		Get
			Return ConfigurationManager.AppSettings("facebook.appId")
		End Get
	End Property

	Private Shared ReadOnly Property AppSecret As String
		Get
			Return ConfigurationManager.AppSettings("facebook.secret")
		End Get
	End Property

	Private Shared ReadOnly Property PageName As String
		Get
			Return ConfigurationManager.AppSettings("facebook.page")
		End Get
	End Property

	Private Shared ReadOnly Property PageID As String
		Get
			Return ConfigurationManager.AppSettings("facebook.pageID")
		End Get
	End Property


	Private Function ConvertToUTF(ByVal s As String) As String   'Конверт символов UTF, записанных в виде \uXXXX в нормальный текст.
		Return Regex.Replace(s, "\\u([\da-f]{4})", Function(m As Match) ChrW(CInt("&H" & m.Groups(1).Value)))
	End Function

	Private Function getAccessToken(ByVal code As String) As String
		' do server-to-server auth call.
		Try
			Dim ph =
				String.Format("https://graph.facebook.com/oauth/access_token?client_id={0}&redirect_uri={1}&client_secret={2}&code={3}",
							  AppId,
							  My.Request.Url.GetLeftPart(UriPartial.Path),
							  AppSecret,
							  code
							)

			Dim req As System.Net.WebRequest = System.Net.HttpWebRequest.Create(ph)
			req.Method = "GET"
			Dim response As String
			Using resp As WebResponse = req.GetResponse()
				Dim sr = New StreamReader(resp.GetResponseStream())
				response = sr.ReadToEnd()
				sr.Close()
			End Using

			Dim respJs = ConvertToUTF(response)
			Dim r As JObject = JSONDecoder.Decode(respJs)

			Return r("access_token").StringValue()

		Catch ex As Exception
			Return Nothing
		End Try
	End Function



	Protected Sub Load_Page(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

		Try



			Dim code As String = My.Request.QueryString("code")
			If code IsNot Nothing Then
				'step 2. get user access token
				Dim accessToken = getAccessToken(code)
				If accessToken Is Nothing Then
					Response.Write("Facebook login failed!")
					Return
				End If

				' step 3. get page access token

				Dim fb = New FacebookClient(accessToken)
				Dim r As IDictionary(Of String, Object) = fb.Get("me/accounts")

				Dim pageAccessToken As String = Nothing
				For Each d In r("data")
					If d("name") = PageName Then
						pageAccessToken = d("access_token")
						Exit For
					End If
				Next

				If pageAccessToken Is Nothing Then
					Response.Write("Page not found.")
					Return
				End If

				' step 4. publish feed



				fb = New FacebookClient(pageAccessToken)
				Dim o As Dictionary(Of String, Object) = New Dictionary(Of String, Object)
				o.Add("message", "test")
				'o.Add("link", "http://chtogdekogda.org/view-question.aspx?gameNum=22&questionNum=1")

				Try
					Dim result = fb.Post("/" + PageID + "/feed", o)

					If result("id") IsNot Nothing Then
						Response.Write("DONE")
					Else
						Response.Write(result)
					End If

				Catch ex As Exception

					Response.Write("POST FAILED: " & ex.Message)

				End Try

			Else
				'step 1. redirect to facebook for auth.
				'Response.Cookies.Add(New HttpCookie("ClientNfo", SiteManager.ClientNfo))
				If My.Request.QueryString("newFacebookFeed") <> "start" Then Return

				If My.Request.QueryString("type") IsNot Nothing AndAlso My.Request.QueryString("type").ToLower() = "idiom" Then
					HttpContext.Current.Cache.Add("fbFeedType", "idiom", Nothing, Date.Now.AddMinutes(1), Cache.NoSlidingExpiration, CacheItemPriority.Default, Nothing)
				Else
					HttpContext.Current.Cache.Add("fbFeedType", "wod", Nothing, Date.Now.AddMinutes(1), Cache.NoSlidingExpiration, CacheItemPriority.Default, Nothing)
				End If

				Dim callBackURL = My.Request.Url.GetLeftPart(UriPartial.Path)
				Dim redirectURL = "https://www.facebook.com/dialog/oauth?client_id=" & AppId & "&redirect_uri=" & callBackURL & "&scope=publish_actions,manage_pages,publish_pages"
				My.Response.Redirect(redirectURL, True)
			End If

		Catch ex As Exception
			My.Response.Write("FAILED")
		End Try

	End Sub


</script>