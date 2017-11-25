Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports Microsoft.VisualBasic

Public Class GameQuestion
	Inherits DbObject
	Implements IDbObject


	'Public Property Id As Integer
	Public Property GameNum As Integer
	Public Property QuestionNum As Integer

	Public ReadOnly Property RoundNum As Integer
		Get
			Return QuestionNum / 10 + 1
		End Get
	End Property

	Public ReadOnly Property QuestionInRoundNum As Integer
		Get
			Return QuestionNum - (RoundNum - 1) * 10
		End Get
	End Property


	Public Property Team As Team
	Public Property QuestionText As String
	Public Property Answer As String
	Public Property AcceptedAnswers As String
	Public Property Comments As String
	Public Property Sources As String
	Public Property Author As String
	Public Property IsBlackBox As Boolean
    Public Property EditorMark As Integer

    Public ReadOnly Property EditorMarkDescr As String
        Get
            If EditorMark <= 15 Then
                Return "легкий"
            ElseIf EditorMark > 25 Then
                Return "сложный"
            Else
                Return "средней сложности"
            End If
        End Get
    End Property

    Public Property EditorComments As String
	Public Property ImageData As Byte()
    Public Property IsBestOfTheGame As Boolean
    Public Property IsClubQuestion As Boolean
    Public Property TeamsAnswered As String

    Public ReadOnly Property TeamsAnsweredCount As Integer
        Get
            If String.IsNullOrEmpty(TeamsAnswered) Then Return 0
            Return TeamsAnswered.Split(",").Length
        End Get
    End Property

    Public ReadOnly Property TeamsAnsweredTeams As IEnumerable(Of Team)
        Get
            Dim tms As New List(Of Team)
            If String.IsNullOrEmpty(TeamsAnswered) Then Return tms
            For Each tm In TeamsAnswered.Split(",")
                Dim teamId As Integer = tm
                tms.Add(Team.All(teamId))
            Next
            Return tms
        End Get
    End Property

    Public Property TeamsPlayedCount As Integer



    Public Sub ReadFromDb(rdr As SqlDataReader) Implements IDbObject.ReadFromDb
		'Id = rdr("Id")
		GameNum = rdr("GameNum")
		QuestionNum = rdr("QuestionNum")
		Dim teamId As Integer = ResolveDbNull(rdr("TeamId"), 0)
		If teamId <> 0 Then
			Team = Team.All(teamId)
		End If
		QuestionText = rdr("QuestionText")
		Answer = rdr("Answer")
		AcceptedAnswers = rdr("AcceptedAnswers")
		Comments = rdr("Comments")
		Sources = rdr("Sources")
		Author = rdr("Author")
		IsBlackBox = rdr("IsBlackBox")
		IsBestOfTheGame = rdr("IsBestOfTheGame")
        IsClubQuestion = rdr("IsClubQuestion")
        EditorMark = rdr("EditorMark")
        EditorComments = rdr("EditorComments")
        ImageData = ResolveDbNull(rdr("ImageData"))

        TeamsAnswered = ResolveDbNull(rdr("TeamsAnswered"), 0)
        TeamsPlayedCount = ResolveDbNull(rdr("TeamsPlayedCount"), 0)

    End Sub


	Public Shared Function GetQuestion(gameNum As Integer, questionNum As Integer) As GameQuestion

		Dim sql = "select * from GameQuestion where GameNum = " & gameNum & " and QuestionNum = " & questionNum
		Dim aa = ReadCollectionFromDb(Of GameQuestion)(sql, Nothing)

		If aa.Length = 0 Then Return New GameQuestion() With {.GameNum = gameNum, .QuestionNum = questionNum}
		Return aa(0)
	End Function

	Public Shared Function GetAllForGame(gameNum As Integer) As GameQuestion()
		Dim sql = "select * from GameQuestion where GameNum = " & gameNum & " order by QuestionNum"
		Return ReadCollectionFromDb(Of GameQuestion)(sql, Nothing)
	End Function


	Public Sub Save()
		ExecStoredProc("SaveGameQuestion",
			Sub(cmd)
				cmd.Parameters.AddWithValue("@gameNum", GameNum)
				cmd.Parameters.AddWithValue("@questionNum", QuestionNum)
				If Team IsNot Nothing Then cmd.Parameters.AddWithValue("@teamId", Team.Id)
				cmd.Parameters.AddWithValue("@questionText", QuestionText)
				cmd.Parameters.AddWithValue("@answer", Answer)
				cmd.Parameters.AddWithValue("@acceptedAnswers", AcceptedAnswers)
				cmd.Parameters.AddWithValue("@comments", Comments)
				cmd.Parameters.AddWithValue("@sources", Sources)
				cmd.Parameters.AddWithValue("@author", Author)
				cmd.Parameters.AddWithValue("@isBlackBox", IsBlackBox)
				cmd.Parameters.AddWithValue("@editorMark", EditorMark)
				cmd.Parameters.AddWithValue("@editorComments", EditorComments)
				cmd.Parameters.AddWithValue("@isBestOfTheGame", IsBestOfTheGame)
				If ImageData IsNot Nothing AndAlso ImageData.Length > 0 Then
					Dim dt = New SqlParameter("@imageData", SqlDbType.VarBinary, ImageData.Length)
					dt.Value = ImageData
					cmd.Parameters.Add(dt)
				End If
			End Sub)
	End Sub



End Class
