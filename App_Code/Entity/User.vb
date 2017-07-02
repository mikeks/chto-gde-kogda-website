Imports System.Data.SqlClient
Imports Microsoft.VisualBasic

Public Enum UserTypeEnum
    NotSelected = 0
    None = 1
    PlayerPhila = 2
    PlayerOther = 3
    Editor = 4
    ClubRepresentative = 5
    Admin = 6
End Enum


Public Class User
    Inherits DbObject
    Implements IDbObject


    Public Property Id As Integer
    Public Property Name As String
    Public Property FacebookId As String
    Public Property Email As String
    Public Property Team As Team
    Public Property IsTeamLeader As Boolean
    Public Property ApprovedBy As Integer

    Public ReadOnly Property IsApproved As Boolean
        Get
            Return ApprovedBy <> 0
        End Get
    End Property

    Public Property UserType As UserTypeEnum



    Public ReadOnly Property Title As String
        Get
            Select Case UserType
                Case UserTypeEnum.PlayerPhila
                    If Team IsNot Nothing Then
                        Return If(IsTeamLeader, "Капитан команды ", "Игрок команды ") & Team.Name
                    Else
                        Return ""
                    End If
                Case UserTypeEnum.Editor
                    Return "Редактор"
                Case UserTypeEnum.PlayerOther
                    Return "Игрок"
                Case UserTypeEnum.ClubRepresentative
                    Return "Представитель клуба"
                Case UserTypeEnum.NotSelected
                    Return ""
                Case UserTypeEnum.Admin
                    Return "Великий Админ"
            End Select

            Return "Гость"
        End Get
    End Property


    Public Sub ReadFromDb(rdr As SqlDataReader) Implements IDbObject.ReadFromDb
        Id = rdr("Id")
        Name = rdr("Name")
        FacebookId = ResolveDbNull(rdr("FacebookId"))
        Email = ResolveDbNull(rdr("Email"))
        Dim teamId = ResolveDbNull(rdr("TeamId"), 0)
        If teamId > 0 Then
            Team = Team.All(teamId)
        End If
        IsTeamLeader = rdr("IsTeamLeader")
        UserType = rdr("UserType")
        ApprovedBy = ResolveDbNull(rdr("ApprovedBy"), 0)
    End Sub

    Public Sub Save()

        ExecStoredProc("SaveUser",
            Sub(cmd)
                cmd.Parameters.AddWithValue("@id", Id)
                cmd.Parameters.AddWithValue("@name", Name)
                If Team IsNot Nothing Then cmd.Parameters.AddWithValue("@teamId", Team.Id)
                cmd.Parameters.AddWithValue("@isTeamLeader", IsTeamLeader)
                cmd.Parameters.AddWithValue("@userType", UserType)
            End Sub
        )

    End Sub

End Class
