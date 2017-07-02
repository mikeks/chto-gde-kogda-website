
Partial Class UserInfo
    Inherits System.Web.UI.Page

    Dim Teams As String() = {
    "<Нет>",
    "ВА-БАНК",
    "ЖАР-ПТИЦА",
    "Киев+1",
    "ИНТЕР",
    "Два Штата",
    "АНГАРА",
    "ГУБ-ЧК",
    "Deja Vu",
    "РУМБА",
    "OneГоги",
    "Разные",
    "West Chester",
    "ВОСТОК",
    "<другая команда клуба ЧГК>"
}

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
        TeamsDropDown.DataSource = Teams
        TeamsDropDown.DataBind()

    End Sub

End Class
