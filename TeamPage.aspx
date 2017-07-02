<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TeamPage.aspx.vb" Inherits="PublicTeamPage" MasterPageFile="~/MasterPage.master" %>


<asp:Content runat="server" ContentPlaceHolderID="ContentArea">


    <h1>Команда <%= Tm.Name %></h1>


    <%If UserManager.CurrentUser IsNot Nothing AndAlso UserManager.CurrentUser.Team IsNot Nothing AndAlso UserManager.CurrentUser.Team.Id = Tm.Id AndAlso UserManager.CurrentUser.IsApproved Then %>
    <div class="your-team">
        Это страничка вашей команды. Вы можете ее редактировать. Расскажите всем о вашем коллективе!
        <div class="button-holder" style="text-align: center">
            <a class="button" href="/team-edit.aspx">Редактировать</a>
        </div>

        <hr style="margin: 30px 0 15px 0" />
        <div>
            Адрес вашей странички: 
            <div class="team-url"><%= Tm.FullFriendlyUrl %></div>
            <div class="note">Просто скопируйте этот адрес и используйте его где угодно (на Facebook, Instagram, ВК, в ЖЖ или в вашем блоге).</div>
        </div>
    </div>
    <%End If%>
    <%If UserManager.CurrentUser IsNot Nothing AndAlso UserManager.CurrentUser.UserType = UserTypeEnum.Admin Then%>
    <div class="your-team">
        Вы админ, вам все можно.
        <div class="button-holder" style="text-align: center">
            <a class="button" href="/team-edit.aspx?teamid=<%= Tm.Id %>">Редактировать</a>
        </div>
    </div>
    <%End If %>
    <%Dim src = Utility.GetHtmlEmbeddedImg(Tm.TeamImage)
        If src IsNot Nothing Then%>
    <img class="team-image" src="<%=src %>" alt="Команда <%= Tm.Name %> собственной персоной" />
    <%End If %>

    <section>

        <%if Tm.IsInLeague %>

        <div>Команда входит в <strong>Лигу Чемпионов</strong> Филадельфийского клуба ЧГК.</div>

        <%End If %>

        <h2>Результаты последних игр</h2>
        <ul>
            <% For Each gr In Tm.GameResults%>
            <li>
                Игра <%= gr.GameNum%>. <%= gr.Rank %> место. Взято <%= gr.Points %> вопросов.
            </li>
            <%Next%>
        </ul>
    </section>


    <%= If(String.IsNullOrWhiteSpace(Tm.AboutText), "Больше про эту команду никто пока ничего не знает. Если вы играете в этой команде, вы можете легко это исправить! <a href='/ExtLogin/FacebookLogin.aspx'>Войдите в один клик через Facebook</a>, чтобы получить доступ.", Tm.AboutText) %>
</asp:Content>
