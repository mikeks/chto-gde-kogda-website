<%@ Page Language="VB" AutoEventWireup="false" CodeFile="registration.aspx.vb" Inherits="Registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="global.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Регистрация</h1>

            <img src="facebookLogin.png" />

            <ul class="regList">
                <li>
                    <span class="fieldTitle">Имя:
                    </span>
                    <input type="text" placeholder="Имя" />
                </li>
                <li>
                    <span class="fieldTitle">Фамилия: 
                    </span>
                    <input type="text" placeholder="Фамилия" />
                </li>
                <li>
                    <span class="fieldTitle">Email: 
                    </span>
                    <input type="text" placeholder="Email" />
                </li>
                <li>
                    <span class="fieldTitle">Придумайте логин: 
                    </span>
                    <input type="text" placeholder="Логин" />
                </li>
                <li>
                    <span class="fieldTitle">Придумайте пароль: 
                    </span>
                    <input type="text" placeholder="Пароль" />
                </li>

            </ul>


            <input type="submit" value="Зарегистрироваться" />

        </div>
    </form>
</body>
</html>
