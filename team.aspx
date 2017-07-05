<%@ Page Language="VB" AutoEventWireup="false" CodeFile="team.aspx.vb" Inherits="TeamPage" MasterPageFile="~/MasterPage.master" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentArea">
    <h1>Команда <%= UserManager.CurrentUser.Team.Name %></h1>

    <section>
        <h1>Ваша страничка</h1>

        На публичной страничке команды вы можете рассказать о вашей команде, о том как вы появились, о ваших успехах и о каждом из вас. Можно добавить ваше фото.
        Кроме того, на этой страничке отображаются разультаты последних игр.
        <div style="margin:20px 0 40px 0">
            <a class="button" href="<%= UserManager.CurrentUser.Team.FullUrl %>">Страничка команды <%= UserManager.CurrentUser.Team.Name %></a>
        </div>

    </section>

    <section>
        <h1>Вопрос от команды на ближайшую игру</h1>

	<%if Utility.QuestionAcceptDaysLeft >= 0 Then%>
        До окончания приема вопросов на ближайшую игру осталось игру <span class="days-left-span"><%= Utility.QuestionAcceptDaysLeft.ToString("00") %></span> дней. Если на ваш вопрос 
        во время игры другими командами будет дан хотя бы один правильный ответ, то команда получает бонусное <strong>очко</strong>.
	<%End If%>
        
        <div style="margin:20px 0 10px 0">
            <a class="button" href="/team-question.aspx">Вопрос <%= UserManager.CurrentUser.Team.Name %></a>
        </div>

        <div class="note">О правилах и советах по написанию вопросов можно почитать <a href="http://chtogdekogda.org/question-rules.aspx">тут</a>.</div>
    </section>

    <section>
        <h1>Ваша команда</h1>

        В вашей команде на сегодняшний момент числятся. Эти люди имеют доступ к вопросу от команды и к редактированию странички:
        <ul>
        <%
            Dim players As User() = UserManager.CurrentUser.Team.GetPlayers()

        %>
        <%For Each p In players.Where(Function(x) x.IsApproved)%>
            <li>
                <%= p.Name %><%If p.IsTeamLeader %> <strong>(капитан)</strong><%End If %><%If p.Id = UserManager.CurrentUser.Id %> (это Вы)<%End If%>
            </li>
        <%Next%>
        </ul>

        <%If UserManager.CurrentUser.IsTeamLeader Then %>

            <h2>Заявки на участие в команде</h2>
            <% If players.Count(Function(x) Not x.IsApproved) > 0 %>
                <p>Пожалуйста, подтвердите, что данные игроки действительно играют в вашей команде. Им будет доступен вопрос команды и доступ к редактированию странички команды.</p>
                <ul>
                <%For Each p In players.Where(Function(x) Not x.IsApproved)%>
                    <li>
                        <span class="candidate-name">
                            <%= p.Name %>
                        </span>
                        <a class="button" onclick="return confirm('Принять <%= p.Name %> в команду?');" href="?addPlayer=<%= p.Id %>">Принять в команду</a>
                        <a class="button redButton" style="margin-left:10px" onclick="return confirm('Что, правда отказать?');" href="?rejectPlayer=<%= p.Id %>">Отказать</a>
                    </li>
                <%Next%>
                </ul>
            <%else %>
               <div>Сейчас нет заявок от других игроков на вступление в эту команду.</div>
            <%End If %>
        <%End If%>



    </section>


</asp:Content>
