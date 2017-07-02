<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage.master" %>
<asp:Content runat="server" ContentPlaceHolderID="ContentArea">
    <% UserManager.ClearUserCache() %>
    <%UserManager.TestAccess()%>
    <%If UserManager.CurrentUser.Team Is Nothing Then Response.Redirect("/") %>
    <%If UserManager.CurrentUser.IsApproved Then Response.Redirect("/team.aspx") %>
    <h1>Команда <%= UserManager.CurrentUser.Team.Name %></h1>

    <div class="not-approved-status">Ваш статус не подтвержден.</div>
    <div>
        <%If UserManager.CurrentUser.IsTeamLeader %>
        Обратитесь к администрации ЧГК, чтобы они подтвердили, что вы действительно капитан команды.
        <%Else %>
        Обратитесь к капитану вашей команды, чтобы он зашел на свою страничку и подтвердил, что вы игрок команды.
        <%End If%>
    </div>
    <div>
        Пока Ваш статус не подтвержден, вы не имеете доступа к закрытым разделам сайта.
    </div>
    <div class="note">Пока ваш статус не подтвержден, Вы еще можете <a href="personal.aspx">поменять ваши данные</a>.</div>
</asp:Content>
