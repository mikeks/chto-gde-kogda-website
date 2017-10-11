<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TeamPage.aspx.vb" Inherits="PublicTeamPage" MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>


<asp:Content runat="server" ContentPlaceHolderID="Head">
    <meta property="og:title" content="Команда <%= Tm.Name %> в клубе ЧГК Филадельфия" />
    <meta property="og:url" content="<%= Tm.FullUrl %>" />
    <%If Gallery.Length > 0 %>
    <meta property="og:image" content="http://chtogdekogda.org<%= Gallery.First().BigImageUrl %>" />
    <%End If %>
    <script>
        function compareChartWith(el) {
            location.href = "<%=CanonicalUrl %>?compareWith=" + el.options[el.selectedIndex].value;
        }
    </script>
</asp:Content>

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

        <h2>Результаты игр</h2>

        <div class="rating-chart-cont">
            <form runat="server">
                <asp:Chart ID="Chart1" runat="server" Width="500" Height="300" CssClass="rating-chart">
                    <Titles>
                        <asp:Title Name="xx"  Text="Динамика рейтинга команды" Font="Microsoft Sans Serif, 14pt" />
                    </Titles>
                    <Series>
                        <asp:Series BorderColor="Blue" BorderWidth="3" ChartType="Line" Color="Red" Name="Series1"></asp:Series>
                        <asp:Series BorderColor="Blue" BorderWidth="3" ChartType="Line" Color="Blue" Name="Series2"></asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
            </form>
            <%If CompareToTeams IsNot Nothing AndAlso CompareToTeams.Count > 0 %>
                Сравнить с
                <select onchange="compareChartWith(this)">
                    <option value="0"></option>
                    <%For Each t In CompareToTeams %>
                    <option <%= If(CompareToTeam IsNot Nothing AndAlso CompareToTeam.Id = t.Id, " selected=selected", "") %> value="<%= t.Id %>"><%= t.Name %></option>
                    <%Next %>
                </select>
            <%End If %>
        </div>

        <div>
            Лучший результат: <%= Tm.GameResults.Max(Function(x) x.Points) %> правильных ответов
            <br />
            Текущий рейтинг: <%= Tm.CurrentMoonRating %>
            <br />
            Максимальный рейтинг: <%= Tm.MaxinumMoonRating %>
        </div>



        <table class="results-table">
            <tr>
                <td>
                    Игра №
                </td>
                <td>
                    Занятое место
                </td>
                <td>
                    Взято вопросов
                </td>
<%--                <td>
                    Рейтинг
                </td>--%>
            </tr>

            <% For Each gr In Tm.GameResults%>
            <tr>
                <td>
                    <%= gr.GameNum%>
                </td>
                <td>
                    <%= gr.Position %>
                </td>
                <td>
                    <%= If(gr.Points > 0, gr.Points, "?") %>
                </td>
<%--                <td>
                    <%=gr.MoonRating %>
                </td>--%>
            </tr>
            <%Next%>
        </table>

    </section>
    <section>
        <h2>О команде</h2>
        <%= If(String.IsNullOrWhiteSpace(Tm.AboutText), "Больше про эту команду никто пока ничего не знает. Если вы играете в этой команде, вы можете легко это исправить! <a href='/ExtLogin/FacebookLogin.aspx'>Войдите в один клик через Facebook</a>, чтобы получить доступ.", Tm.AboutText.Replace("""Img/", """/Img/")) %>
    </section>

    <section>
        <h2>Фотогалерея</h2>

        <%
            For Each gp In Gallery
        %>

        <div class="gallery-box">
            <a target="_blank" href="/Img/teams-gallery/<%= Tm.UrlName %>/<%=gp.Id %>B.jpg">
                <img src="/Img/teams-gallery/<%= Tm.UrlName %>/<%=gp.Id %>.jpg" />
            </a>
            <div class="gallery-description">
                <%= gp.Description %>
            </div>
        </div>

        <%
            Next
        %>
        <%if Gallery.Length = 0 %>
        Команда пока не добавила свои фотки в галлерею. Если вы играете в этой команде, помогите ей, добавьте фоток!
    <%End If %>
    </section>
</asp:Content>