<%@ Page Language="VB" AutoEventWireup="false" CodeFile="signin.aspx.vb" Inherits="signin" MasterPageFile="~/MasterPage.master" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentArea">

           <h1>Вход</h1>

            <p>Если у вас есть аккаунт в <strong>Facebook</strong>, вы можете зайти на сайт</p>

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


</asp:Content>
