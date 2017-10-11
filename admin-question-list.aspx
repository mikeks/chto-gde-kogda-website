<%@ Page Language="VB" AutoEventWireup="false" CodeFile="admin-question-list.aspx.vb" Inherits="TeamQuestionPage" MasterPageFile="~/MasterPage.master" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentArea">

    <h1>Вопросы</h1>

	<ul>
		<%For Each q In Questions%>
		<li>
			<a href="/admin-question.aspx?gameNum=<%= q.GameNum %>&questionNum=<%= q.QuestionNum %>">
				Вопрос №<%= q.QuestionNum %>
			</a>
		</li>
		<%Next%>
	</ul>


	<a class="button" href="/admin-question.aspx?gameNum=<%= GameNum %>&questionNum=<%= NextQuestionNum %>">Добавить вопрос номер <%= NextQuestionNum %></a>



</asp:Content>
