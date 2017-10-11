Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic

Public Class DbObject


    Protected Shared Function GetConnection() As SqlConnection
        Dim conn = New SqlConnection(ConfigurationManager.ConnectionStrings(0).ConnectionString)
        conn.Open()
        Return conn
    End Function


    Protected Shared Sub ReadSql(sql As String, addParAction As Action(Of SqlCommand), f As Action(Of SqlDataReader))
        Using conn = GetConnection()
            Dim cmd As New SqlCommand(sql, conn)
            If addParAction IsNot Nothing Then addParAction(cmd)
            Using rdr = cmd.ExecuteReader()
                While rdr.Read()
                    f(rdr)
                End While
            End Using
        End Using
    End Sub




    Private Shared Sub _exec(storedProcName As String, addParAction As Action(Of SqlCommand), cmdType As CommandType)
        Using conn = GetConnection()
            Dim cmd As New SqlCommand(storedProcName, conn) With {.CommandType = cmdType}
            If addParAction IsNot Nothing Then addParAction(cmd)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Public Shared Sub ReadStoredProc(storedProcName As String, addParAction As Action(Of SqlCommand), readRow As Action(Of SqlDataReader))
        Using conn = GetConnection()
            Dim cmd As New SqlCommand(storedProcName, conn) With {.CommandType = CommandType.StoredProcedure}
            If addParAction IsNot Nothing Then addParAction(cmd)
            Using rdr = cmd.ExecuteReader()
                While rdr.Read
                    readRow(rdr)
                End While
            End Using
        End Using
    End Sub


    Public Shared Sub ExecStoredProc(storedProcName As String, addParAction As Action(Of SqlCommand))
        _exec(storedProcName, addParAction, CommandType.StoredProcedure)
    End Sub

    Public Shared Sub ExecSQL(storedProcName As String, Optional addParAction As Action(Of SqlCommand) = Nothing)
        _exec(storedProcName, addParAction, CommandType.Text)
    End Sub



    Protected Shared Function ReadCollectionFromDb(Of T As {IDbObject, New})(sql As String, addParAction As Action(Of SqlCommand)) As T()
        Dim ss As New List(Of T)

        ReadSql(sql, addParAction,
            Sub(rdr)
                Dim itm As New T
                itm.ReadFromDb(rdr)
                ss.Add(itm)
            End Sub)

        Return ss.ToArray()

    End Function


    Protected Shared Function ReadCollectionFromDb(Of T As IDbObject)(sql As String, addParAction As Action(Of SqlCommand), factory As Func(Of T)) As T()
        Dim ss As New List(Of T)

        ReadSql(sql, addParAction,
            Sub(rdr)
                Dim itm = factory()
                itm.ReadFromDb(rdr)
                ss.Add(itm)
            End Sub)

        Return ss.ToArray()

    End Function

    Protected Function ResolveDbNull(o As Object, Optional def As Object = Nothing) As Object
        Return If(TypeOf o Is DBNull, Nothing, o)
    End Function


End Class
