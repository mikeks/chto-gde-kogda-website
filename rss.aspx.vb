
Partial Class rss
	Inherits System.Web.UI.Page

	Protected FeedId As Integer
	Protected FeedText As String

	Protected Overrides Sub OnLoad(e As EventArgs)

		DbObject.ReadStoredProc("GetRss",
			Sub(cmd)
				cmd.Parameters.AddWithValue("@gameNum", Utility.QuestionsAvaliableForGame)
			End Sub,
			Sub(rdr)
				FeedId = rdr("Id")
				FeedText = rdr("Text")
			End Sub
			)

	End Sub

End Class
