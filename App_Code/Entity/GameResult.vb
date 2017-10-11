Imports System.Data.SqlClient
Imports Microsoft.VisualBasic

Public Class GameResult
    Inherits DbObject
    Implements IDbObject, IComparable


    Public Property GameNum As Integer
    Public Property Team As Team
    Public Property Points As Integer
    Public Property Position As Integer
    Public Property Rank As Decimal
    Public Property MoonRatingTemp As Integer
    Public Property MoonRatingBase As Integer ' game rating before time adjustment
    Public Property AdjustedMoodRating As Integer ' adjusted by time mood rating for the current game
    Public Property MoonRating As Integer ' final Moon rating for the time of this game

    Public Sub ReadFromDb(rdr As SqlDataReader) Implements IDbObject.ReadFromDb
        GameNum = rdr("GameNum")
        Dim teamId = rdr("TeamId")
        Team = Team.All(teamId)
        Points = ResolveDbNull(rdr("Points"), 0)
        Position = rdr("Position")
        Rank = rdr("Rank")
    End Sub

    Private Shared _all As Dictionary(Of Integer, List(Of GameResult))

    'Private Shared Sub CalculateRank()
    '    For Each gn In All.Keys

    '        All(gn).Sort()
    '        Dim r = 1
    '        Dim r1 = 1
    '        Dim prevP As Integer = 0

    '        For Each g In All(gn)
    '            If g.Points <> prevP Then
    '                r1 = r
    '            End If

    '            g.Rank = r1
    '            r = r + 1
    '            prevP = g.Points
    '        Next
    '    Next

    'End Sub

    Private Shared Sub CalculateRatingForGame(gameNumber As Integer, isLatestGame As Boolean)
        Dim i = 0

        '  calculate GameResult.MoonRating (adjust by time)

        For Each gn In All.Keys

            If gn > gameNumber Then Continue For ' skip newer games

            i += 1

            Dim k As Double = 1
            If i <= 6 Then
                k = 1
            ElseIf i <= 12 Then
                k = 3 / 4
            ElseIf i <= 18 Then
                k = 2 / 4
            ElseIf i <= 24 Then
                k = 1 / 4
            Else
                k = 0
            End If


            Dim grs = All(gn) ' list of game results 

            For Each gr In grs
                gr.MoonRatingTemp = k * gr.MoonRatingBase
                If isLatestGame Then
                    gr.AdjustedMoodRating = gr.MoonRatingTemp
                End If
            Next

        Next


        For Each t In Team.All.Values
            Dim r = t.GameResults.Where(Function(x) x.GameNum <= gameNumber).OrderByDescending(Function(x) x.MoonRatingTemp).Take(3).Sum(Function(x) x.MoonRatingTemp)
            Dim gr = t.GameResults.FirstOrDefault(Function(x) x.GameNum = gameNumber)
            If gr IsNot Nothing Then
                gr.MoonRating = r
            End If
            If isLatestGame Then
                t.CurrentMoonRating = r
            End If
        Next
    End Sub

    Private Shared Sub CalculateRating()



        For Each t In Team.All.Values
            t.MoonRatingTemp = 50 ' staring Moon rating
        Next

        ' calculate GameResult.MoonRatingBase (i.e. Moon rating for every game before time adjustment)
        ' Team.MoonRating is using as temporarily storage for calculation team's rating after every game

        For gameNumber = 1 To All.Keys.Max()

            If Not All.ContainsKey(gameNumber) Then Continue For
            Dim grs = All(gameNumber) ' list of game results 

            Dim R As Integer = 0
            For Each gr In grs
                R += gr.Team.MoonRatingTemp
            Next

            Dim k As Double = 1400 * Math.Atan((R + 1000) / 20000)

            Dim N = grs.Count

            For Each gr In grs
                Dim e As Double = Math.Exp(0.4 * (1 - gr.Rank) / Math.Sqrt(N))
                Dim mr As Integer = k * e
                gr.MoonRatingBase = mr
                gr.Team.MoonRatingTemp = mr
            Next
        Next


        Dim f = True
        For Each gn In All.Keys
            CalculateRatingForGame(gn, f)
            f = False
        Next

        For Each t In Team.All.Values
            t.MaxinumMoonRating = t.GameResults.Max(Function(x) x.MoonRating)
            t.IsBestMaximumRating = False
        Next

        Team.All.OrderByDescending(Function(x) x.Value.MaxinumMoonRating).First().Value.IsBestMaximumRating = True

    End Sub



    Public Function CompareTo(obj As Object) As Integer Implements IComparable.CompareTo
        Dim gr As GameResult = obj
        If obj Is Nothing Then Return -1
        Return gr.Points - Points
    End Function

    Public Shared ReadOnly Property All As Dictionary(Of Integer, List(Of GameResult))
        Get
            If _all Is Nothing Then
                Dim aa = ReadCollectionFromDb(Of GameResult)("select * from GameResult order by GameNum desc", Nothing)
                _all = New Dictionary(Of Integer, List(Of GameResult))
                For Each a In aa
                    If Not _all.ContainsKey(a.GameNum) Then
                        _all.Add(a.GameNum, New List(Of GameResult))
                    End If
                    _all(a.GameNum).Add(a)
                    a.Team.GameResults.Add(a)
                Next

                'CalculateRank()
                CalculateRating()

            End If

            Return _all
        End Get
    End Property

End Class
