<%@ Page Language="VB" AutoEventWireup="false" CodeFile="checkQuestion.aspx.vb" Inherits="checkQuestion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
                <h1>Проверка вопроса №1</h1>

        Вопрос
        <asp:TextBox ID="TextBox4" runat="server" MaxLength="250" ReadOnly="true" TextMode="MultiLine"></asp:TextBox>

        Ответ
        <asp:TextBox ID="TextBox2" runat="server" MaxLength="50" ></asp:TextBox>

        Комментарии к ответу
        <asp:TextBox ID="TextBox3" runat="server" MaxLength="500" TextMode="MultiLine"></asp:TextBox>

        <asp:RadioButtonList ID="RadioButtonList1" runat="server">
            <asp:ListItem Text="Принять вопрос без изменений" />
            <asp:ListItem Text="Принять вопрос c правками" />
            <asp:ListItem Text="Отклонить вопрос" />
        </asp:RadioButtonList>

        Поясните ваше решение. Если есть правки, опишите их:
        <asp:TextBox ID="TextBox1" runat="server" MaxLength="500" TextMode="MultiLine"></asp:TextBox>


    </div>
    </form>
</body>
</html>
