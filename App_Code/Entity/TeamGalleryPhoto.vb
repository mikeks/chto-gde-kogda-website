Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports System.IO
Imports Microsoft.VisualBasic

Public Class TeamGalleryPhoto
    Inherits DbObject
    Implements IDbObject


    Public Property Id As Integer
    Public Property Team As Team
    Public Property Description As String


    Public Sub ReadFromDb(rdr As SqlDataReader) Implements IDbObject.ReadFromDb
        Id = rdr("Id")
        Dim teamId = rdr("TeamId")
        Team = Team.All(teamId)
        Description = rdr("Description")
    End Sub


    Public Shared Function GetForTeam(teamId As Integer) As TeamGalleryPhoto()
        Dim sql = "select * from TeamPhotoGallery where TeamId = " & teamId & " order by Id"
        Return ReadCollectionFromDb(Of TeamGalleryPhoto)(sql, Nothing)
    End Function

    Public Shared Function GetById(Id As Integer) As TeamGalleryPhoto
        Dim sql = "select * from TeamPhotoGallery where Id = " & Id
        Return ReadCollectionFromDb(Of TeamGalleryPhoto)(sql, Nothing).FirstOrDefault
    End Function


    Public Sub SaveDescription()
        ExecSQL("update TeamPhotoGallery set Description = @descr where id = @id",
            Sub(cmd)
                cmd.Parameters.AddWithValue("@id", Id)
                cmd.Parameters.AddWithValue("@descr", Description)
            End Sub)
    End Sub

    Public Sub Delete()
        ExecSQL("delete TeamPhotoGallery where id = " & Id)

        Try
            File.Delete(HttpContext.Current.Server.MapPath(BigImageUrl))
        Catch
        End Try


        Try
            File.Delete(HttpContext.Current.Server.MapPath(SmallImageUrl))
        Catch
        End Try


    End Sub


    Public Shared Function CreateNew(team As Team) As TeamGalleryPhoto
        Dim res As TeamGalleryPhoto = Nothing

        ReadSql("insert TeamPhotoGallery (TeamId) values (" & team.Id & "); select SCOPE_IDENTITY()", Nothing,
            Sub(rdr)
                Dim id As Integer = rdr(0)
                res = New TeamGalleryPhoto() With {.Id = id, .Team = team}
            End Sub)

        Return res
    End Function

    Public ReadOnly Property BigImageUrl As String
        Get
            Return "/Img/teams-gallery/" & Team.UrlName & "/" & Id & "B.jpg"
        End Get
    End Property

    Public ReadOnly Property SmallImageUrl As String
        Get
            Return "/Img/teams-gallery/" & Team.UrlName & "/" & Id & ".jpg"
        End Get
    End Property









End Class
