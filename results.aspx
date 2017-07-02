<%@ Page Language="VB" AutoEventWireup="false" CodeFile="results.aspx.vb" Inherits="results" MasterPageFile="~/MasterPage.master" %>


<asp:Content runat="server" ContentPlaceHolderID="ContentArea">

    <h1>Лидирующая команда</h1>

    <p>Тут будет размещена информация о победителе прошлой игре - <strong>OneГоги</strong>. Но для этого эта команда должна заполнить <a href="http://chtogdekogda.org/team/OneGogi">информацию о себе</a>.</p>

    <h1>Результаты последних 5 игр</h1>
    <div>
        Нажмите на любую команду, чтобы попасть на <strong>страницу команды</strong>. 
    </div>

    <table class="results-table">
        <tr>
            <td rowspan="2">№</td>
            <td rowspan="2">Команда</td>
            <td colspan="5">Вопросов взято</td>
            <td rowspan="2">Рейтинг*</td>
        </tr>
        <tr>
            <%For i = 0 To 4
                    Dim gameNum = GameResult.All.Keys(i)
                    %>
            <td><%= gameNum %> игра</td>
            <%Next %>
        </tr>

            <%  Dim n = 1, n1 = 1, prevR = 0
                For Each t In Team.All.Values.OrderByDescending(Function(x) x.AvgPoints)
                    If prevR <> t.AvgPoints Then n1 = n
                     %>
        <tr>
            <td><%= n1 %></td>
            <td><a href="<%= t.FullUrl %>"><%= t.Name %></a></td>
            <%
                For index = 0 To 4
                    Dim ii = index
                    Dim g = t.GameResults.FirstOrDefault(Function(x) x.GameNum = GameResult.All.Keys(ii))

                    Dim med = ""
                    If g IsNot Nothing AndAlso g.Rank <= 3 Then med = "<span class='medal-" & g.Rank & "'>" & g.Rank & "</span>"

            %>
            <td><%= If(g Is Nothing, "-", g.Points & " " & med) %></td>
            <%Next%>
            <td><%= t.AvgPoints %></td>
        </tr>
                <% 
                        prevR = t.AvgPoints
                        n += 1
                    Next%>

    </table>

    <div class="note">
        * рейтинг считается как простое среднее значение очков за последние 5 игр. Пропуски игр не влияют на рейтинг.
    </div>



</asp:Content>
