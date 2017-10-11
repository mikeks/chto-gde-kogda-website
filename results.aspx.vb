
Partial Class results
    Inherits System.Web.UI.Page

    Protected IsShowPoints As Boolean

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)

        IsShowPoints = (Request("mode") = "points")


    End Sub

End Class
