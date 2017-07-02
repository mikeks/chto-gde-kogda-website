Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO
Imports Microsoft.VisualBasic

Public Class Team
    Inherits DbObject
    Implements IDbObject


    Public Property Id As Integer
    Public Property Name As String
    Public Property UrlName As String
    Public Property AboutText As String
    Public Property TeamImage As Byte()
    Public Property IsInLeague As Boolean

    Public Sub SetTeamImage(str As Stream)

        Using img = Image.FromStream(str)
            Dim img500 = Utility.ResizeImage(img, 500)


            Using ws As New MemoryStream
                img500.Save(ws, ImageFormat.Jpeg)
                TeamImage = ws.ToArray()
            End Using

        End Using

    End Sub


    Public ReadOnly Property FullUrl As String
        Get
            Return "http://chtogdekogda.org/team/" & UrlName
        End Get
    End Property

    Public ReadOnly Property FullFriendlyUrl As String
        Get
            Return "chtogdekogda.org/team/" & UrlName
        End Get
    End Property

    Public Property GameResults As New List(Of GameResult)

    Public ReadOnly Property AvgPoints As Integer
        Get
            Return GameResults.Sum(Function(x) x.Points) / GameResults.Count
        End Get
    End Property

    Public Sub ReadFromDb(rdr As SqlDataReader) Implements IDbObject.ReadFromDb
        Id = rdr("Id")
        Name = rdr("Name")
        UrlName = rdr("UrlName")
        AboutText = ResolveDbNull(rdr("AboutText"))
        TeamImage = ResolveDbNull(rdr("TeamImage"))
        IsInLeague = rdr("IsInLeague")
    End Sub

    Private Shared _all As Dictionary(Of Integer, Team)

    Public Shared ReadOnly Property All As Dictionary(Of Integer, Team)
        Get
            If _all Is Nothing Then
                Dim aa = ReadCollectionFromDb(Of Team)("select * from Team", Nothing)
                _all = New Dictionary(Of Integer, Team)
                For Each a In aa
                    _all.Add(a.Id, a)
                Next

            End If
            Return _all
        End Get
    End Property

    Public Sub Save()
        ExecStoredProc("SaveTeam",
            Sub(cmd)
                cmd.Parameters.AddWithValue("@id", Id)
                cmd.Parameters.AddWithValue("@aboutText", AboutText)
                If TeamImage IsNot Nothing AndAlso TeamImage.Length > 0 Then
                    Dim dt = New SqlParameter("@teamImage", SqlDbType.VarBinary, TeamImage.Length)
                    dt.Value = TeamImage
                    cmd.Parameters.Add(dt)
                End If
            End Sub)
    End Sub

    Public Function GetPlayers() As User()

        Return ReadCollectionFromDb(Of User)("select * from [User] where TeamId = " & Id, Nothing)

    End Function

End Class
