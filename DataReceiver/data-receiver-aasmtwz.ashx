<%@ WebHandler Language="VB" Class="data_receiver_aasmtwz" %>

Imports System
Imports System.Web

Public Class data_receiver_aasmtwz : Implements IHttpHandler



    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        Try
            Dim data As TransferData = Newtonsoft.Json.JsonConvert.DeserializeObject(Of TransferData)(context.Request("dt"))
            DataReceiverManager.ProcessData(data)
        Catch e As Exception

            context.Response.Write(e.Message)
            context.Response.End()

        End Try


        context.Response.Write("OK")

    End Sub

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return True
        End Get
    End Property

End Class