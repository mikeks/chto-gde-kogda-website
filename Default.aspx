<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" MasterPageFile="~/MasterPage.master" %>
<asp:Content runat="server" ContentPlaceHolderID="ContentArea">

	<section class="info-message">
		<h1>Клуб "Что? Где? Когда?" - Филадельфия приветствует Вас!</h1>
        <p>
		    Вы зашли на сайт одного из самых активных клубов ЧГК на территории США. Мы играем одну игру раз в месяц, на каждой игре собирается от 70 до 100 знатоков. Подробнее о наших играх можно почитать <a href="/about-game.aspx">тут</a>. 
            Обязательно приходите, если вы живёте в Филадельфии или окрестностях или если можете к нам доехать. К нам нередко приезжают гости из Нью-Йорка и других городов.
        </p>
	</section>

	<section class="info-message">
		<h1>Читайте про наши игры!</h1>

        <a class="game-article" target="_blank" href="https://www.docdroid.net/F9yX4b4/riz47.pdf#page=18">
            <div>XXII игра</div>
            <div>18 ноября 2017</div>
            <img src="/Img/media/game22.jpg" />
        </a>

        <a class="game-article" target="_blank" href="https://www.docdroid.net/UQtvtXF/riz43.pdf#page=18">
            <div>XXI игра</div>
            <div>21 октября 2017</div>
            <img src="/Img/media/game21.jpg" />
        </a>

        <a class="game-article" target="_blank" href="https://www.docdroid.net/UE5Nzbv/32compressed.pdf#page=18">
            <div>XX игра</div>
            <div>5 августа 2017</div>
            <img src="/Img/media/game20.jpg" />
        </a>

        <a class="game-article" target="_blank" href="https://www.docdroid.net/I5BCmQB/riz27.pdf#page=18">
            <div>XIX игра</div>
            <div>1 июля 2017</div>
            <img src="/Img/media/game19.jpg" />
        </a>

        <a class="game-article" target="_blank" href="https://www.docdroid.net/aDApt37/20compressed.pdf.html#page=18">
            <div>XVIII игра</div>
            <div>13 мая 2017</div>
            <img src="/Img/media/game18.jpg" />
        </a>

	</section>

    <script>
        function changeGameNum(el) {
            $('.question-list').hide();
            $('#questionList' + el.options[el.selectedIndex].value).show();
        }
    </script>

	<section class="info-message">
		<h1>Вопросы <%=Utility.QuestionsAvaliableForGame %> игры теперь доступны для просмотра и комментариев</h1>
		<%If UserManager.CurrentUser Is Nothing %>
		<div>
			Чтобы почитать вопросы, <a href="/ExtLogin/FacebookLogin.aspx">войдите</a>. Потом возвращайтесь обратно на главную (эту страницу).
		</div>
		<%Else %>
            <div class="game-select-cont">
                <select class="game-select" onchange="changeGameNum(this)">
                    <%For gameNum = Utility.QuestionsAvaliableForGame To 19 Step -1 %>
                        <option value="<%= gameNum %>"><%= gameNum %> игра</option>
                    <% Next %>
                </select>
            </div>
            <%For gameNum = 19 To Utility.QuestionsAvaliableForGame %>
            <div id="questionList<%= gameNum %>" class="question-list"<%= If(gameNum <> Utility.QuestionsAvaliableForGame, " style='display:none'", "") %>>
			<% For Each	q In GameQuestion.GetAllForGame(gameNum) %>
				<a href="view-question.aspx?gameNum=<%=gameNum %>&questionNum=<%=q.QuestionNum %>" class="question-link"><%= q.QuestionNum %></a>
				<%If q.QuestionNum Mod 10 = 0 %><br /><%end If %>
			<%Next %>
            </div>
            <%Next gameNum %>
		<%end If %>

	</section>


	<%if Utility.QuestionAcceptDaysLeft >= 0 Then%>
	<section class="info-message">
		<div class="days-left">
			До окончания приема вопросов осталось
			<div class="days-left-cnt"><%= Utility.QuestionAcceptDaysLeft %> дн.</div>
            <div>Уже прислали нам свои вопросы</div>
                <div class="days-left-cnt"><%= totalQuestions %></div>
		</div>
		<h1>Внимание команд!</h1>

		Прием вопросов от команд производится исколючительно на <strong>этом сайте</strong>. 
        Для этого вам нужно <a href="/ExtLogin/FacebookLogin.aspx">залогиниться в один клик</a>.
        После того, как вы сохраните ваш вопрос на этом сайте, мы формируем пакет вопросов, и отправляем его редакторам. 
        Поэтому просьба не опаздывать. Вопросы после объявленой даты приниматься не будут.
        Напоминаю, что по нашим правилам, команда, приславшая свой вопрос, получает за него <strong>бонусное очко</strong>, если хотя бы кто-нибудь на этот вопрос ответит.
        Мы всегда говорим спасибо всем, кто присылает нам вопросы!

        <p>Cледующие команды уже прислали нам свои вопросы: <strong><%= teamList %></strong></p>
	</section>
	<%End If%>

	<section class="info-message">
		<h1>Вниманию всем!</h1>
		Вы можете помочь нашему клубу! Нам нужны люди, которые могут подойти на следующие роли. 
		<ul>
			<li>Читать вопросы на игре. Нужен человек который может <strong>аккуратно и грамотно прочитать вопросы по-русски</strong>. Мы, все втроем, будем читать по-очереди. Так что вам нужно будет прочитать (примерно) 10 вопросов.
			</li>
<%--			<li>Помочь в качестве технического редактора. Требование - <strong>уметь гуглить</strong>. Ваша задача будет заключаться в том, чтобы оперативно проверять версии команд, по большей части - гуглить. Если вы умеете быстро гуглить - вы нам подходите. Работы может быть много или не быть совсем, в зависимости от того, как пойдет игра.</li>--%>
			<li>Вы или ваши знакомые где угодно в мире могут помочь с редакцией вопросов (за неделю до игры). Требования: <strong>надо чтобы они знали о ЧГК и желательно имели опыт игры</strong>.</li>
<%--			<li>Svetlana Shubinsky, к большому сожалению, не сможет нам помогать с приемом ответов и занесением их в компьютер 1 июля. Поэтому нам необходим человек, который сможет взять эту функцию на себя. Там не сложно, мы научим. Требования: <strong>а) владение манипулятором типа "мышь", б) не поддаваться на требования команд зачесть им ответ без отмашки от нас.</strong></li>--%>
		</ul>
		<a href="/contacts.aspx?msgTopic=%D0%BF%D0%BE%D0%BC%D0%BE%D1%89%D1%8C+%D0%BA%D0%BB%D1%83%D0%B1%D1%83#message" class="button">Подать заявку</a>
	</section>

</asp:Content>
