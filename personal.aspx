<%@ Page Language="VB" AutoEventWireup="false" CodeFile="personal.aspx.vb" Inherits="personal" MasterPageFile="~/MasterPage.master" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentArea">

    <h1>Пару слов о себе</h1>

    <form action="personal.aspx" method="post">

        <div class="error"><%= ErrMsg %></div>

        <ul class="reg-list">

            <li>
                <input onclick="$('#playerOpt').show();" type="radio" name="userType" value="PlayerPhila" id="PlayerPhila" /><label for="PlayerPhila">Я играю в клубе ЧГК Филадельфия</label>
                <div id="playerOpt" style="display: none">
                    <span class="fieldTitle">Команда
                    </span>
                    <select class="dd-team" name="team">
                        <option value="0">Выберите</option>
                        <%Dim tid = If(UserManager.CurrentUser.Team IsNot Nothing, UserManager.CurrentUser.Team.Id, 0) %>
                        <%For Each t In Team.All.Values.OrderBy(Function(x) x.Name).ToList%>
                        <option <% If tid = t.Id Then %>selected="selected"<%end If%> value="<%= t.Id %>"><%= t.Name %></option>
                        <%Next%>
                    </select>
                    <div class="cb-cap">
                        <input type="checkbox" name="cap" id="cap" />
                        <label for="cap">Я капитан команды</label>
                    </div>
                </div>

            </li>
            <li>
                <input onclick="$('#playerOpt').hide();" type="radio" name="userType" value="Editor" id="Editor" /><label for="Editor">Я хочу быть редактором (тестировать вопросы)</label>
            </li>
            <li>
                <input onclick="$('#playerOpt').hide();" type="radio" name="userType" value="PlayerOther" id="PlayerOther" /><label for="PlayerOther">Я играю в другом клубе ЧГК</label>
            </li>
            <li>
                <input onclick="$('#playerOpt').hide();" type="radio" name="userType" value="ClubRepresentative" id="ClubRepresentative" /><label for="ClubRepresentative">Я представитель другого клуба ЧГК</label>
            </li>
            <li>
                <input onclick="$('#playerOpt').hide();" type="radio" name="userType" value="None" id="None" /><label for="None">Я просто проходил мимо</label>
            </li>
        </ul>
        <input type="submit" class="button" name="save" value="Сохранить" />

    </form>


</asp:Content>
