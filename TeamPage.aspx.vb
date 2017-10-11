
Imports System.Web.UI.DataVisualization.Charting

Partial Class PublicTeamPage
    Inherits System.Web.UI.Page

    Protected Tm As Team
    Protected CompareToTeams As IEnumerable(Of Team)
    Protected CompareToTeam As Team
    Protected Gallery As TeamGalleryPhoto()
    Protected CanonicalUrl As String

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)


        Dim t = Request("team")

        If t Is Nothing Then Response.Redirect("/", True)

        Tm = Team.All.Values.FirstOrDefault(Function(x) x.UrlName.ToLowerInvariant() = t.ToLowerInvariant())

        If Tm Is Nothing Then Response.Redirect("/", True)

        If Tm.PlayedLast3Games Then
            CompareToTeams = Team.All.Values.Where(Function(x) x.PlayedLast3Games AndAlso x.Id <> Tm.Id)
        End If

        If Request("compareWith") IsNot Nothing AndAlso Request("compareWith") <> 0 Then
            Try
                CompareToTeam = Team.All(Request("compareWith"))
            Catch
            End Try
        End If



        Gallery = TeamGalleryPhoto.GetForTeam(Tm.Id)

        For Each gr In Tm.GameResults
            Chart1.Series(0).Points.AddXY(gr.GameNum, gr.MoonRating)
        Next

        If CompareToTeam IsNot Nothing Then
            For Each gr In CompareToTeam.GameResults
                Chart1.Series(1).Points.AddXY(gr.GameNum, gr.MoonRating)
            Next

            Chart1.Legends.Add(New Legend() With {.Docking = Docking.Bottom, .Alignment = Drawing.StringAlignment.Center})
            Chart1.Series(0).Name = Tm.Name
            Chart1.Series(1).Name = CompareToTeam.Name

        End If



        'Chart1.Series(1).Legend = "xx"

        If Tm.GameResults.Count < 3 Then
            Chart1.Visible = False
        End If

        CanonicalUrl = "http://chtogdekogda.org/team/" & Tm.UrlName

    End Sub



End Class
