Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports Microsoft.VisualBasic

Public Class TeamQuestion
    Inherits DbObject
    Implements IDbObject


    'Public Property Id As Integer
    'Public Property GameNum As Integer
    Public Property Team As Team
    Public Property QuestionText As String
    Public Property Answer As String
    Public Property Comments As String
    Public Property Sources As String
    Public Property Author As String
    Public Property IsBlackBox As Boolean
    Public Property IsReady As Boolean
    Public Property ImageData As Byte()


    Public Sub ReadFromDb(rdr As SqlDataReader) Implements IDbObject.ReadFromDb
        'Id = rdr("Id")
        'GameNum = rdr("GameNum")
        Dim teamId = rdr("TeamId")
        Team = Team.All(teamId)
        QuestionText = rdr("QuestionText")
        Answer = rdr("Answer")
        Comments = rdr("Comments")
        Sources = rdr("Sources")
        Author = rdr("Author")
        IsBlackBox = rdr("IsBlackBox")
        IsReady = rdr("IsReady")
        ImageData = ResolveDbNull(rdr("ImageData"))
    End Sub




    Public Shared Function GetQuestion(teamId As Integer) As TeamQuestion

        Dim sql = "select * from TeamQuestion where TeamId = " & teamId & " and GameNum = " & Utility.CurrentGameNum
        Dim aa = ReadCollectionFromDb(Of TeamQuestion)(sql, Nothing)

        If aa.Length = 0 Then
            Return New TeamQuestion()
        Else
            Return aa(0)
        End If
    End Function

    Public Shared Function GetAll() As TeamQuestion()
        Dim sql = "select * from TeamQuestion where GameNum = " & Utility.CurrentGameNum
        Return ReadCollectionFromDb(Of TeamQuestion)(sql, Nothing)
    End Function


    Public Sub Save()
        ExecStoredProc("SaveTeamQuestion",
            Sub(cmd)
                cmd.Parameters.AddWithValue("@gameNum", Utility.CurrentGameNum)
                cmd.Parameters.AddWithValue("@teamId", Team.Id)
                cmd.Parameters.AddWithValue("@questionText", QuestionText)
                cmd.Parameters.AddWithValue("@answer", Answer)
                cmd.Parameters.AddWithValue("@comments", Comments)
                cmd.Parameters.AddWithValue("@sources", Sources)
                cmd.Parameters.AddWithValue("@author", Author)
                cmd.Parameters.AddWithValue("@isBlackBox", IsBlackBox)
                cmd.Parameters.AddWithValue("@isReady", IsReady)
                If ImageData IsNot Nothing AndAlso ImageData.Length > 0 Then
                    Dim dt = New SqlParameter("@imageData", SqlDbType.VarBinary, ImageData.Length)
                    dt.Value = ImageData
                    cmd.Parameters.Add(dt)
                End If
            End Sub)
    End Sub


End Class
