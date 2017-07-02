<%@ Page Language="VB" AutoEventWireup="false" CodeFile="admin.aspx.vb" Inherits="AdminPage" MasterPageFile="~/MasterPage.master" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentArea">
    <h1>Приветствую тебя о великий админ <%= UserManager.CurrentUser.Name %>!</h1>


    <h2>Игроки</h2>

    <div>
        Тут можно посмотреть списки всех игроков и подтверждать или отклонять заявки игроков в команды (если они есть).
         Капитаны команд тоже могут подтверждать игроков своих команд. А вот капитанов могут подтверждать только админы.
    </div>

    <ul class="team-list">
    <%
        For Each t In Team.All
    %>
        <li>Команда <%= t.Value.Name %>
                <ol>
                    <%For Each p In t.Value.GetPlayers()%>
                    <li>
                        <span class="candidate-name">
                            <%= p.Name %><%If p.IsTeamLeader %> <strong>(капитан)</strong><%End If %>
                        </span>
                        <%If not p.IsApproved %>
                        <a class="button" onclick="return confirm('Принять <%= p.Name %> в команду?');" href="?addPlayer=<%= p.Id %>">Принять в команду</a>
                        <a class="button redButton" style="margin-left: 10px" onclick="return confirm('Что, правда отказать?');" href="?rejectPlayer=<%= p.Id %>">Отказать</a>
                        <%End If %>
                    </li>
                    <%Next%>
                </ol>
        </li>
    <%Next%>
    </ul>

    <h2>Вопросы от команд</h2>

    <%
        Dim qq = TeamQuestion.GetAll()
        For Each q In qq
        %>

            <h3><%=q.Team.Name %></h3>

         <%
             Dim src = Utility.GetHtmlEmbeddedImg(q.ImageData)
             If src IsNot Nothing Then%>
                    <img class="razd" src="<%=src %>" alt="Раздача" />
              <%End If %>

        <%If q.IsBlackBox %>
    <div>
        ЧЕРНЫЙ ЯЩИК
    </div>
    <%End If%>
            <div>
            <%=q.QuestionText %>    
            </div>

            <div>
                Ответ: <%=q.Answer %>
            </div>
            <div>
                Комментарии: <%=q.Comments %>
            </div>
            <div>
                Источники: <%=q.Sources %>
            </div>
            <div>
                Автор: <%=q.Author %>
            </div>

       <% Next  %>

</asp:Content>
