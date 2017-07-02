<%@ Page Language="VB" AutoEventWireup="false" CodeFile="userInfo.aspx.vb" Inherits="UserInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="global.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Информация о Вас</h1>
            <ul class="regList">

                <li>
                    <span class="fieldTitle">Команда клуба ЧГК Филадельфии, в которой вы играете:
                    </span>
                    <asp:DropDownList ID="TeamsDropDown" runat="server"></asp:DropDownList>
                </li>
                <li>
                    <asp:CheckBox Text="Я капитан команды" runat="server" />
                </li>
                <li>
                    <asp:CheckBox Text="Я хочу помогать с редакцией вопросов" runat="server" />
                </li>
            </ul>

            <input type="submit" value="Сохранить" />

        </div>
    </form>
</body>
</html>
