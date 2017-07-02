
Partial Class contacts
	Inherits System.Web.UI.Page

    Protected ErrorMsg As String = ""
    Protected IsSent As Boolean = False

    Protected Name As String
    Protected Email As String
    Protected Topic As String
    Protected Msg As String

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)

        Name = Request("msgName")
        Email = Request("msgEmail")
        Topic = Request("msgTopic")
        Msg = Request("msgText")

        If Request("submitMessage") IsNot Nothing Then

            If String.IsNullOrWhiteSpace(Name) Or String.IsNullOrWhiteSpace(Email) Or String.IsNullOrWhiteSpace(Topic) Or String.IsNullOrWhiteSpace(Msg) Then
                ErrorMsg = "Ошибочка. Все поля обязательны для заполнения!"
            ElseIf Not EMailSender.IsValidEMail(Email) Then
                ErrorMsg = "Ошибочка. Email указан не верно."
            Else
                IsSent = EMailSender.SendEmail(Email, Name, Topic, Msg)
                If Not IsSent Then
                    ErrorMsg = "Ничего не вышло. Беда. Все пропало :("
                Else
                    Name = ""
                    Email = ""
                    Topic = ""
                    Msg = ""
                End If
            End If

        End If

    End Sub


End Class
