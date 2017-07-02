Imports System.Activities.Statements
Imports Microsoft.VisualBasic
Imports SimpleJSON
Imports System.Net
Imports System.IO

Public Class FaceBookUser
    'Public GlobalId As String
    Public Id As String
    Public Name As String
    'Public Locale As String
    Public EMail As String
    'Public Birthday As Date? = Nothing
    'Public Location As String
    'Public TimeZone As Integer = -5
    'Public SchoolId As String = Nothing
    'Public UniversityName As String = Nothing
    'Public JobName As String
    Public AvatarURL As String
End Class

Public Class AppRequest
	Public AppId As String
	Public FromId As String
	Public ToId As String
	Public Id As String
	Public CreatedTime As DateTime
	Public Data As String
	Public Message As String
End Class


Public Class FacebookLogin

	Public Shared ReadOnly Property AppSecret As String
		Get
			Return ConfigurationManager.AppSettings("facebook.secret")
		End Get
	End Property

	Private accessToken As String
    Private errorMessage As String = Nothing


    Public ReadOnly Property GetErrorMessage As String
		Get
			Return errorMessage
		End Get
	End Property

    Public Sub New(accessToken As String)
        Me.accessToken = accessToken
    End Sub

    Private Function ConvertToUTF(ByVal s As String) As String	 'Конверт символов UTF, записанных в виде \uXXXX в нормальный текст.
		Return Regex.Replace(s, "\\u([\da-f]{4})", Function(m As Match) ChrW(CInt("&H" & m.Groups(1).Value)))
	End Function

	Private Function GetOpenGraphObject(objId As String, fields As String, Optional limit As Integer? = Nothing, Optional customFields As String = Nothing) As JObject
		Try
			If String.IsNullOrWhiteSpace(accessToken) Then Return Nothing

			Dim req As System.Net.WebRequest = System.Net.HttpWebRequest.Create("https://graph.facebook.com/" & objId & "?" & If(limit.HasValue, "limit=" & limit.Value.ToString() & "&", String.Empty) & If(String.IsNullOrEmpty(fields), String.Empty, "fields=" & fields & "&") & If(Not String.IsNullOrEmpty(customFields), customFields, "access_token=" & accessToken))
			Using resp As WebResponse = req.GetResponse()
				Dim response As String = (New StreamReader(resp.GetResponseStream())).ReadToEnd()
				response = ConvertToUTF(response)
				Dim r As JObject = JSONDecoder.Decode(response)
				Return r
			End Using
		Catch ex As Exception
			Return Nothing
		End Try
	End Function

    Public Function GetAppRequests() As List(Of AppRequest)
		Dim r As JObject = GetOpenGraphObject("me/apprequests", Nothing)
		If r Is Nothing Then Return Nothing

		Try
			Dim hasKey = Function(o As JObject, key As String) o.ObjectValue.ContainsKey(key)
			Dim hasKey2 = Function(o As JObject, key1 As String, key2 As String) o.ObjectValue.ContainsKey(key1) AndAlso o(key1).ObjectValue.ContainsKey(key2)
			If Not hasKey(r, "data") Or r("data").Kind <> JObjectKind.Array Then Return Nothing

			Dim result = New List(Of AppRequest)
			For Each i In r("data").ArrayValue
				Dim request = New AppRequest
				If hasKey2(i, "application", "id") Then request.AppId = i("application")("id").StringValue
				If hasKey2(i, "from", "id") Then request.FromId = i("from")("id").StringValue
				If hasKey2(i, "to", "id") Then request.ToId = i("to")("id").StringValue
				If hasKey(i, "created_time") Then request.CreatedTime = DateTime.Parse(i("created_time").StringValue)
				If hasKey(i, "data") Then request.Data = i("data").StringValue
				If hasKey(i, "id") Then request.Id = i("id").StringValue
				If hasKey(i, "message") Then request.Message = i("message").StringValue
				result.Add(request)
			Next
			Return result
		Catch ex As Exception
			Return Nothing
		End Try
	End Function


	Public Function GetFaceBookUser() As FaceBookUser

        Dim r As JObject = GetOpenGraphObject("me", "id,name,email")
        If r Is Nothing Then Return Nothing

		Try
			Dim getObj = Function(obj As JObject, key As String) If(obj IsNot Nothing AndAlso obj.ObjectValue.ContainsKey(key), obj(key), Nothing)

			Dim hasKey = Function(key As String) r.ObjectValue.ContainsKey(key)
			Dim hasKey2 = Function(key1 As String, key2 As String) r.ObjectValue.ContainsKey(key1) AndAlso r(key1).ObjectValue.ContainsKey(key2)
			Dim hasKey3 = Function(key1 As String, key2 As String, key3 As String) r.ObjectValue.ContainsKey(key1) AndAlso r(key1).ObjectValue.ContainsKey(key2) AndAlso r(key1)(key2).ObjectValue.ContainsKey(key3)

			Dim fbu As New FaceBookUser

            If hasKey("id") Then fbu.Id = r("id").StringValue
            'If hasKey("token_for_business") Then
            '	fbu.GlobalId = r("token_for_business").StringValue
            'Else
            '	fbu.GlobalId = fbu.LocalId
            'End If

            If hasKey("name") Then fbu.Name = r("name").StringValue
            'If hasKey("locale") Then fbu.Locale = r("locale").StringValue
            If hasKey("email") Then fbu.EMail = r("email").StringValue
            'If hasKey2("location", "name") Then fbu.Location = r("location")("name").StringValue
            'If hasKey("timezone") Then fbu.TimeZone = r("timezone").IntValue

            'If hasKey("birthday") Then
            '	Dim b As String = r("birthday")
            '	If b.Length = 10 Then
            '		Try
            '			fbu.Birthday = New Date(Integer.Parse(b.Substring(6)), Integer.Parse(b.Substring(0, 2)), Integer.Parse(b.Substring(3, 2)))
            '		Catch
            '			fbu.Birthday = Nothing
            '		End Try
            '	End If
            'End If

            If hasKey3("picture", "data", "url") Then fbu.AvatarURL = r("picture")("data")("url")

            'If hasKey("education") Then
            '	Try
            '		Dim e As JObject = r("education")
            '		Dim i As Integer
            '		For i = 0 To e.Count - 1
            '			Dim t As String = e(i)("type").StringValue()
            '			If fbu.SchoolId Is Nothing AndAlso t = "High School" Then fbu.SchoolId = e(i)("school")("id").StringValue
            '			If fbu.UniversityName Is Nothing AndAlso t = "College" Then fbu.UniversityName = e(i)("school")("name").StringValue
            '		Next
            '	Catch
            '	End Try
            'End If

            'If hasKey("work") Then
            '	Try
            '		Dim e As JObject = r("work")
            '		If e.Count > 0 Then fbu.JobName = e(0)("employer")("name").StringValue()
            '	Catch
            '	End Try
            'End If

            Return fbu

		Catch e As Exception
			Return Nothing
		End Try
	End Function




End Class
