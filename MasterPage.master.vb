
Partial Class MasterPage
	Inherits System.Web.UI.MasterPage

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)

    End Sub

    Dim _isAnySel As Boolean = False

    Public Property CanonicalUrl As String

    Protected ReadOnly Property IsAnySel As Boolean
        Get
            Return _isAnySel
        End Get
    End Property

    Public Property AllowComments As Boolean

    Protected Function _isSel(url As String) As String

        If Request.Url.LocalPath.ToLowerInvariant().Contains(url.ToLowerInvariant()) Then
            _isAnySel = True
            Return " selected"
        End If

        Return ""

	End Function

End Class

