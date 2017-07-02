
Partial Class PublicTeamPage
    Inherits System.Web.UI.Page

    Protected Tm As Team

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)

        Dim t = Request("team")

        If t Is Nothing Then Response.Redirect("/", True)

        Tm = Team.All.Values.FirstOrDefault(Function(x) x.UrlName.ToLowerInvariant() = t.ToLowerInvariant())

        If tm Is Nothing Then Response.Redirect("/", True)



    End Sub



End Class
