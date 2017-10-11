<%@ Page Language="VB" AutoEventWireup="false" CodeFile="view-question.aspx.vb" Inherits="ViewQuestionPage" MasterPageFile="~/MasterPage.master" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentArea">

    <h1>Игра <%= GameNum %>. Вопрос <%= QuestionNum %></h1>

    <%If Qu.IsBestOfTheGame Then %>
        <img style="vertical-align:top" width="35" src="/Img/logo.png" />
        <span class="best-question-label">
            По результатам голосования команд, этот вопрос признан <strong>лучшим вопросом игры</strong>.
        </span>
    <%End If %>


    <div>

        <table>
            <tr>
                <td>
                    <img src="Img/child.png" width="50" title="Детский вопрос (простой)" />
                </td>
                <td>
                    <% Dim editorV = (Qu.EditorMark - 10) / 20 * 370
                        Dim teamV = 370
                        If Qu.TeamsAnsweredCount > 0 Then
                            teamV = (1 - Qu.TeamsAnsweredCount / Qu.TeamsPlayedCount) * 370
                        End If

                    %>
                    <div style="position: relative; left: <%=editorV%>px; color: #bbb; font-size: 32px; top: 8px;width:50px" title="Оценка редакторов">&#x25BC;</div>
                    <div style="width: 400px; height: 1px; background-color: black;"></div>
                    <div style="position: relative; left: <%=teamV%>px; color: blue; font-size: 32px; top: -8px;width:50px" title="Взятие вопроса командами">&#x25B2;</div>

                </td>
                <td>
                    <img style="z-index:100" src="Img/grob.png" width="40" title="Гроб (сложный или неберущийся вопрос)" />
                </td>
            </tr>

        </table>

        <div style="margin-bottom: 50px; font-size: small">
            <%If Qu.EditorMark > 0 %>
            <div>
                <span style="color: #ccc">&#x25BC;</span>
                Редакторы посчитали что это вопрос <%= Qu.EditorMarkDescr %> (<%= Qu.EditorMark / 10 %>).
            </div>
            <%End If %>
            <div>
                <span style="color: blue">&#x25B2;</span>
                <%if Qu.TeamsAnsweredCount = 0 %>
                На игре на этот вопрос никто не смог ответить...
                <%Else %>
                На игре ответило команд: <%= Qu.TeamsAnsweredCount %> из <%= Qu.TeamsPlayedCount %>.
                <%End If %>
            </div>
            <%if Qu.TeamsAnsweredCount > 0 %>
            <div>
                Верно ответили команды: 
                <% Dim f = False
                    For Each tms In Qu.TeamsAnsweredTeams
                        If f Then Response.Write(", ")
                        Response.Write("<a href=""http://chtogdekogda.org/team/" & tms.UrlName & """>" & tms.Name & "</a>")
                        f = True
                    Next %>
            </div>
            <%End If %>
        </div>
    </div>


    <%If Qu.Team Isnot Nothing %>
    <div class="question-from"><strong>Вопрос от команды <a href="<%= Qu.Team.FullUrl %>"><%= Qu.Team.Name %></a></strong></div>
    <%End If%>


    <div style="font-size: 24px">
        <div>
            <%Dim src = Utility.GetHtmlEmbeddedImg(Qu.ImageData)
                If src IsNot Nothing Then%>
            <div>РАЗДАЧА</div>
            <img class="razd" src="<%=src %>" alt="Раздача" />
            <%End If %>
            <%If Qu.IsBlackBox Then %><div>ЧЕРНЫЙ ЯЩИК</div>
            <%End If%>
            <%= Qu.QuestionText %>
        </div>

        <br />
        <div class="transparentButton" onclick="$('#ansDiv').show();$(this).hide();">
            Показать ответ
        </div>
        <div class="answer-block" style="display:none" id="ansDiv">

            <div>
                <strong>Ответ:</strong> <%= Qu.Answer %>
            </div>
            <%If not String.IsNullOrEmpty(Qu.AcceptedAnswers) Then %>
            <div>
                <strong>Зачет:</strong> <%= Qu.AcceptedAnswers %>
            </div>
            <%end If %>

            <%If not String.IsNullOrEmpty(Qu.Comments) Then %>
            <div>
                <strong>Комментарии:</strong> <%= Qu.Comments%>
            </div>
            <%end If %>

            <%If not String.IsNullOrEmpty(Qu.Sources) Then %>
            <div>
                <strong>Источники:</strong> <%= Qu.Sources%>
            </div>
            <%end If %>

            <%If not String.IsNullOrEmpty(Qu.Author) AndAlso (Qu.GameNum > 19 Or (Qu.IsClubQuestion OrElse Qu.Team IsNot Nothing)) Then %>
            <div>
                <strong>Автор:</strong> <%= Qu.Author%>
            </div>
            <%end If %>

            <%If not String.IsNullOrEmpty(Qu.EditorComments) Then %>
            <div>
                <strong>Заметки по результатам тестирования:</strong> <%= Qu.EditorComments%>
            </div>
            <%end If %>

        </div>
    </div>


    <h3><strong>Вам понравился этот вопрос? Напишите, что вы думаете:</strong></h3>

    <div class="fb-comments" data-href="<%= "http://chtogdekogda.org/view-question.aspx?gameNum=" & GameNum & "&questionNum=" & QuestionNum %>" data-numposts="5"></div>

    <br />
    <br />
    <a href="view-question.aspx?GameNum=<%= GameNum%>&QuestionNum=<%=QuestionNum - 1 %>" class="button">Предыдущий вопрос</a>
    <a href="view-question.aspx?GameNum=<%= GameNum%>&QuestionNum=<%=QuestionNum + 1 %>" class="button">Следующий вопрос</a>

</asp:Content>
