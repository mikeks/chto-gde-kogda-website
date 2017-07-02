<%@ Page Language="VB" AutoEventWireup="false" CodeFile="contacts.aspx.vb" Inherits="contacts" MasterPageFile="~/MasterPage.master" %>


<asp:Content runat="server" ContentPlaceHolderID="ContentArea">

    <section id="facebook">
        <h1>Facebook</h1>
        Присоединяйтесь к <a target="_blank" href="https://www.facebook.com/groups/ChtoGdeKogdaPhila/">нашей группе на Facebook</a> - у нас более 1,000 участников, активные обсуждения, мы публикуем информацию о будущих и прошедших играх. 
		Много интересной и полезной информации.
    </section>
    <section id="message">
        <h1>Напишите нам</h1>

        <form method="post">

            <div class="form-msg">
                <div class="error"><%= ErrorMsg %></div>
                <%If IsSent %><div class="success">Ваше сообщение успешно отправлено.</div>
                <%end If %>
                <div>
                    <span>Ваше имя</span>
                    <input name="msgName" type="text" value="<%= Name  %>" placeholder="Ваше имя" />
                </div>
                <div>
                    <span>Ваш email</span>
                    <input name="msgEmail" type="text" value="<%= Email  %>" placeholder="Ваш email" />
                </div>
                <div>
                    <span>Тема сообщения
                    </span>
                    <input name="msgTopic" type="text" value="<%= Topic %>" placeholder="Тема" />
                </div>
                <div>
                    <span>Сообщение
                    </span>
                    <textarea name="msgText"><%= Msg %></textarea>
                </div>
            </div>


            <input type="submit" class="button" name="submitMessage" value="Отправить" />

        </form>


    </section>


</asp:Content>
