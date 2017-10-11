<%@ Page Language="VB" AutoEventWireup="false" CodeFile="admin-question.aspx.vb" Inherits="TeamQuestionPage" MasterPageFile="~/MasterPage.master" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentArea">

	<a href="/admin-question-list.aspx?gameNum=<%= GameNum %>">&lt;&lt;&lt; Назад к списку вопросов</a> <br />
	<%If IsSaved %>
	<a href="http://chtogdekogda.org/admin-question.aspx?gameNum=<%= GameNum %>&questionNum=<%= QuestionNum + 1 %>">Добавить следующий вопрос</a> 
	<%End If %>

	<h1>Игра <%= GameNum %>. Вопрос <%= QuestionNum %></h1>


	<form method="post" enctype="multipart/form-data">

		<input type="hidden" name="GameNum" value="<%= GameNum %>" />
		<input type="hidden" name="QuestionNum" value="<%= QuestionNum %>" />

		<div class="error" style="font-size: 30px; margin-top: 20px;">
			<%= ErrorMsg %>
		</div>
		<%If IsSaved %>
		<div class="success" style="font-size: 30px; margin-top: 20px;">Вопрос сохранен.</div>
		<%End If%>
		<div>
			<h2>Команда</h2>
			<select name="team">
				<option value="0">(нет)</option>
			<%For Each t In Team.All %>
				<option <%If Qu.Team IsNot Nothing AndAlso t.Value.Id = Qu.Team.Id %>selected="selected"<%End if%> value="<%=t.Value.Id %>"><%=t.Value.Name %></option>
			<%Next %>
			</select>
		</div>

		<div>
			<h2>Текст вопроса</h2>
			<textarea style="width: 700px; height: 150px;" maxlength="700" name="question"><%= Qu.QuestionText %></textarea>
		</div>
		<div>
			<h2>Дополнительно</h2>
			<div>
				<%Dim src = Utility.GetHtmlEmbeddedImg(Qu.ImageData)
					If src IsNot Nothing Then%>
				<img class="razd" src="<%=src %>" alt="Раздача" />
				<%End If %>
            Раздача:
				<input type="file" runat="server" id="razd" name="razd" />
				<div class="instructions">
					Вы можете загрузить изображение в формате jpg или png.
				</div>
			</div>
			<div style="margin-top: 10px">
				<input type="checkbox" name="blackBox" id="chBB" <%If Qu.IsBlackBox Then %>checked="checked" <%End If%> /><label for="chBB">Черный ящик</label>
			</div>
			<div style="margin-top: 10px">
				<input type="checkbox" name="IsBestOfTheGame" id="chBest" <%If Qu.IsBestOfTheGame Then %>checked="checked" <%End If%> /><label for="chBest">Лучший вопрос игры</label>
			</div>
		</div>
		<div>
			<h2>Ответ</h2>
			<textarea style="width: 700px; height: 50px;" maxlength="200" name="answer"><%= Qu.Answer %></textarea>
		</div>
		<div>
			<h2>Зачет</h2>
			<textarea style="width: 700px; height: 50px;" maxlength="200" name="acceptedAnswers"><%= Qu.AcceptedAnswers %></textarea>
		</div>
		<div>
			<h2>Комментарии к ответу</h2>
			<textarea style="width: 700px; height: 50px;" maxlength="1300" name="comments"><%= Qu.Comments %></textarea>
		</div>
		<div>
			<h2>Источники</h2>
			<textarea style="width: 700px; height: 50px;" maxlength="1300" name="sources"><%= Qu.Sources %></textarea>
		</div>
		<div>
			<h2>Автор вопроса</h2>
			Имена авторов:
			<input type="text" style="width: 400px" maxlength="100" name="author" value="<%= Qu.Author %>" />
		</div>
		<div>
			<h2>Комментарий редакторов</h2>
			<textarea style="width: 700px; height: 50px;" maxlength="1300" name="EditorComments"><%= Qu.EditorComments %></textarea>
		</div>
		<div>
			<h2>Оценка редакторов</h2>
			<input type="text" style="width: 100px" maxlength="100" name="EditorMark" value="<%= Qu.EditorMark %>" />
				<div class="instructions">
					10 - легко, 20 - средне, 30 - сложно.
				</div>
		</div>
		<div style="margin-top: 25px">
			<input type="submit" class="button" name="save" value="Сохранить" />
		</div>

	</form>


</asp:Content>
