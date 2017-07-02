Imports Microsoft.VisualBasic
Imports System.Net.Mail
Imports System.Net
Imports System.Diagnostics
Imports System.Data.SqlClient
Imports System.Data


Public Class EMailSender


    Public Shared Function SendEmail(fromEmail As String, fromName As String, topic As String, body As String) As Boolean
        Try
            'Dim smtp As New SmtpClient("smtp.gmail.com", 465)
            Dim smtp As New SmtpClient("mail.chtogdekogda.org", 25)
            Dim cred As New NetworkCredential("admin@chtogdekogda.org", "ChtoGdeKogda2017")
            smtp.Credentials = cred
            'smtp.EnableSsl = True
            smtp.Timeout = 10000 ' 10 sec

            Dim Message As MailMessage = New MailMessage()
            'Message.From = New MailAddress(fromEmail, fromName)
            Message.From = New MailAddress("admin@chtogdekogda.org")
            Message.To.Add("chto.gde.kogda.phila@gmail.com")
            Message.Subject = topic
            Message.Body = "From: " & fromName & " (" & fromEmail & ") " & vbNewLine & vbNewLine & body
            Message.BodyEncoding = Encoding.UTF8
            smtp.Send(Message)
            Return True
        Catch e As Exception
            Dim m = e.Message
        End Try
        Return False
    End Function

    Private Shared ReadOnly EMailValidationRegEx As New Regex("^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*\.[a-z][a-z\.]*[a-z]$", RegexOptions.Compiled Or RegexOptions.IgnoreCase)

    Public Shared Function IsValidEMail(EMail As String) As Boolean
        If EMail.EndsWith(".") Then Return False
        If EMail.Contains("..") Then Return False
        If EMail.StartsWith("@") Then Return False
        If EMail.Replace("@", "").Length + 1 <> EMail.Length Then Return False ' should be exactly one @ 
        Return EMailValidationRegEx.IsMatch(EMail)
    End Function


End Class
