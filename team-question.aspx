<%@ Page Language="VB" AutoEventWireup="false" CodeFile="team-question.aspx.vb" Inherits="TeamQuestionPage" MasterPageFile="~/MasterPage.master" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentArea">

    <h1>Вопрос от команды <%= UserManager.CurrentUser.Team.Name %></h1>

    <form method="post" enctype="multipart/form-data">


    <div class="title-msg">
        Писать вопросы здорово и интересно. Особенно когда вы это делаете вместе с любимой командой! 
    </div>
<%--        <div>
        Вы можете создать черновик вопроса на этой странице. Остальные члены команды смогут его смотреть и редактировать.
        После того, как работа над вопросом закончера вопрос закончена, постаьте галочку внизу страницы. 
        </div>--%>
    <div>
        При составлении вопросов полезно соблюдать <a href="question-rules.aspx">простые правила</a>, тогда вопрос получится классный и всем понравится.
    </div>

    <div class="error" style="font-size: 30px;margin-top: 20px;">
        <%= ErrorMsg %>
    </div>    
    <%If IsSaved %>
        <div class="success" style="font-size: 30px;margin-top: 20px;">Вопрос сохранен.</div>
    <%End If%>
    <div>
        <h2>Текст вопроса</h2>
        <textarea style="width:700px;height:150px;" maxlength="700" name="question"><%= Qu.QuestionText %></textarea>
        <div class="instructions">
            Проверьте, что:
            <ol>
                <li>Вопрос не на голые знания, а на логику.</li>
                <li>Вся информация изложенная в вопросе, нужна для ответа на него. Ничего лишнего нет. Максимум 700 знаков.</li>
                <li>В вопросе присутствуют подсказки, помогающие дать единственно верный ответ на вопрос.</li>
                <li>Вопрос понятен для всех. Например, недопустим вопрос для ответа на который нужно знать местную географию нордиста.</li>
                <li>На вопрос нет другого ответа, который бы тоже подходил. Если есть сомнения, лучше добавить еще фактов (отсечки).</li>
                <li>Изложенные факты - истинны. Тем не менее допускается ссылаться на свой личный опыт.</li>
                <li>Вопрос придуман вами. Запрещено брать готовые вопросы из интернета.</li>
                <li>Вы обсудили вопрос с командой и опробывали его на паре друзей (маме, бабушке, теще). Они дадут вам feedback и помогут сделать вопрос лучше.</li>
            </ol>
        </div>
    </div>
    <div>
        <h2>Дополнительно</h2>
        <div>
            <%Dim src = Utility.GetHtmlEmbeddedImg(Qu.ImageData)
                If src IsNot Nothing Then%>
                    <img class="razd" src="<%=src %>" alt="Раздача" />
              <%End If %>
            Раздача: <input type="file" runat="server" id="razd" name="razd" />
            <div class="instructions">
                Вы можете загрузить изображение в формате jpg или png.
            </div>
        </div>
        <div style="margin-top:10px">
            <input type="checkbox" name="blackBox" id="chBB" <%If Qu.IsBlackBox Then %>checked="checked"<%End If%> /><label for="chBB">Черный ящик</label>
        </div>
    </div>
    <div>
        <h2>Ответ</h2>
        <textarea style="width:700px;height:50px;" maxlength="200" name="answer"><%= Qu.Answer %></textarea>
        <div class="instructions">
            Укажите правильный ответ и желаемую точность (“абсолютно точно”, “по смыслу”, “принимать если в ответе есть слово Ч”). Если требуется абсолютно точный ответ, это должно быть упомянуто в самом вопросе (“ответьте абсолютно точно”).
        </div>
    </div>
    <div>
        <h2>Комментарии к ответу</h2>
        <textarea style="width:700px;height:50px;" maxlength="1300" name="comments"><%= Qu.Comments %></textarea>
        <div class="instructions">
            Объясните ответ на вопрос, чтобы всем стало все понятно. 
        </div>
    </div>
    <div>
        <h2>Источники</h2>
        <textarea style="width:700px;height:50px;" maxlength="1300" name="sources"><%= Qu.Sources %></textarea>
        <div class="instructions">
            Обязательно укажите ссылки на источники, где можно проверить <strong>ВСЕ</strong> изложенные в вопросе факты. Это потребуется если другие команды попытаются оспорить
            вопрос.
        </div>
    </div>
    <div>
        <h2>Автор вопроса</h2>
        Имена авторов: <input type="text" style="width:400px" maxlength="100" name="author" value="<%= Qu.Author %>" />
    </div>
<%--    <div>
        <input type="checkbox" name="questionReady" id="qr" /><label for="qr">Вопрос готов</label>
    </div>--%>
    <div style="margin-top:25px">
        <input type="submit" class="button" name="save" value="Сохранить" />
    </div>

    </form>


</asp:Content>
