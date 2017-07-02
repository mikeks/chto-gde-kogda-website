<%@ Page Language="VB" AutoEventWireup="false" CodeFile="team-edit.aspx.vb" Inherits="TeamPageEdit" ValidateRequest="false" MasterPageFile="~/MasterPage.master" %>

<asp:Content runat="server" ContentPlaceHolderID="Head">
    <script src="https://cloud.tinymce.com/stable/tinymce.min.js?apiKey=yj2e4c55v2frzthtbbj929sx6q8djaoq4fyfgddcqbzj3mq5"></script>
    <script>tinymce.init({ selector: 'textarea' });</script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentArea">
    <h1>Команда <%= Tm.Name %></h1>

    <div class="error" style="font-size: 30px; margin-top: 20px;">
        <%= ErrorMsg %>
    </div>
    <%If IsSaved %>
    <div class="success" style="font-size: 30px; margin-top: 20px;">Изменения сохранены.</div>
    <%End If%>

    <form method="post" enctype="multipart/form-data">

                    
            <div>Публичный адрес вашей странички: <a href="<%= Tm.FullUrl %>" class="team-url"><%= Tm.FullFriendlyUrl %></a></div>
            <div class="note">Просто скопируйте этот адрес и используйте его где угодно (на Facebook, Instagram, ВК, в ЖЖ или в вашем блоге).</div>


        <section>
            <h2>Ваша фотка</h2>
            
            <%Dim src = Utility.GetHtmlEmbeddedImg(Tm.TeamImage)
            If src IsNot Nothing Then%>
            <img class="team-image-edit" src="<%=src %>" alt="Команда <%= Tm.Name %> собственной персоной" />
            <%End If %>
            
            <div>
                <input type="file" runat="server" id="Photo" name="razd" />
                <div class="instructions">
                    Вы можете загрузить изображение в формате jpg или png.
                </div>
            </div>

        </section>

        <section>
            <h2>Расскажите о вашей команде</h2>
            <div class="instructions">
                Например, что можно включить:
            <ol>
                <li>О создании. Например. Команда <%= Tm.Name %> создана в 1917 году в Санкт-Петербурге и пережила три революции.</li>
                <li>О наградах. Например. Команда <%= Tm.Name %> была 17 раз номинирована на премию Друзь Года.</li>
                <li>Немного истории. Сначала в команде было 6 человек. Да и сейчас 6.</li>
                <li>Расскажите о ваших победах.</li>
                <li>Что запомнилось, интересные игры.</li>
                <li>Можно рассказать об игроках. О каждом или хотя бы о капитане.</li>
                <li>Как вы тренируетесь.</li>
                <li>Ваш рецепт - Как красиво победить (проиграть).</li>
                <li>Все что угодно, касающеся вашей команды.</li>
            </ol>
            </div>
            <textarea style="width: 700px; height: 150px;" maxlength="700" name="aboutText"><%= Tm.AboutText %></textarea>
        </section>

        <div style="margin-top: 25px">
            <input type="submit" class="button" name="save" value="Сохранить" /> 
        </div>

    </form>

</asp:Content>
