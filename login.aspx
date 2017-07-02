<%@ Page Language="VB" AutoEventWireup="false" CodeFile="login.aspx.vb" Inherits="LoginPage" MasterPageFile="~/MasterPage.master" %>


<asp:Content runat="server" ContentPlaceHolderID="ContentArea">

    <h1>Вход</h1>

    <div class="error" style="font-size: 24px;margin: 20px 0;">
        <%= ErrorMsg %>
    </div>    

    <form method="post">
        <input type="text" placeholder="Email" style="width:400px;font-size:20px" maxlength="100" name="email" value="<%= Request("email") %>" /><br />
        <input type="password" placeholder="Пароль" style="width:400px;font-size:20px;margin-top:20px" maxlength="20" name="password" />
        <div style="margin-top:25px">
            <input type="submit" class="button" name="save" value="Вход" />
        </div>
    </form>



</asp:Content>
