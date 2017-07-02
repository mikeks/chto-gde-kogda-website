Imports System.Data.SqlClient
Imports Microsoft.VisualBasic

Public Class GameResult
    Inherits DbObject
    Implements IDbObject, IComparable


    Public Property GameNum As Integer
    Public Property Team As Team
    Public Property Points As Integer
    Public Property Rank As Integer

    Public Sub ReadFromDb(rdr As SqlDataReader) Implements IDbObject.ReadFromDb
        GameNum = rdr("GameNum")
        Dim teamId = rdr("TeamId")
        Team = Team.All(teamId)
        Points = rdr("Points")
    End Sub

    Private Shared _all As Dictionary(Of Integer, List(Of GameResult))

    Private Shared Sub CalculateRank()
        For Each gn In All.Keys

            All(gn).Sort()
            Dim r = 1
            Dim r1 = 1
            Dim prevP As Integer = 0

            For Each g In All(gn)
                If g.Points <> prevP Then
                    r1 = r
                End If

                g.Rank = r1
                r = r + 1
                prevP = g.Points
            Next
        Next

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

                CalculateRank()

            End If

            Return _all
        End Get
    End Property

End Class
