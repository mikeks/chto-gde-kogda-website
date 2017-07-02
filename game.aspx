<%@ Page Language="VB" AutoEventWireup="false" CodeFile="game.aspx.vb" Inherits="game" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Идет игра ЧТО? ГДЕ? КОГДА?</title>
    <style>
        table {
            border-collapse: collapse;
        }

        td, th {
            border: solid 1px black;
            padding: 10px;
        }

        .rightAnswer {
            background-color: green;
        }
        
        .wrongAnswer {
            background-color: red;
        }


    </style>
</head>

<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <th>Команда</th>
                    <th>Стол №</th>
                    <th>1</th>
                    <th>2</th>
                    <th>3</th>
                    <th>4</th>
                    <th>5</th>
                    <th>6</th>
                    <th>7</th>
                    <th>8</th>
                    <th>9</th>
                    <th>10</th>
                </tr>
                <tr>
                    <td>215</td>
                    <td>1</td>
                    <td class="rightAnswer">&nbsp;</td>
                    <td class="wrongAnswer">&nbsp;</td>
                    <td class="wrongAnswer">&nbsp;</td>
                    <td class="noAnswer">&nbsp;</td>
                    <td class="noAnswer">&nbsp;</td>
                    <td class="noAnswer">&nbsp;</td>
                    <td class="noAnswer">&nbsp;</td>
                    <td class="noAnswer">&nbsp;</td>
                    <td class="noAnswer">&nbsp;</td>
                    <td class="noAnswer">&nbsp;</td>
                </tr>
                <tr>
                    <td>Кадры</td>
                    <td>2</td>
                    <td class="wrongAnswer">&nbsp;</td>
                    <td class="rightAnswer">&nbsp;</td>
                    <td class="rightAnswer">&nbsp;</td>
                    <td class="noAnswer">&nbsp;</td>
                    <td class="noAnswer">&nbsp;</td>
                    <td class="noAnswer">&nbsp;</td>
                    <td class="noAnswer">&nbsp;</td>
                    <td class="noAnswer">&nbsp;</td>
                    <td class="noAnswer">&nbsp;</td>
                    <td class="noAnswer">&nbsp;</td>
                </tr>
                <tr>
                    <td>Киев+1</td>
                    <td>3</td>
                    <td class="rightAnswer">&nbsp;</td>
                    <td class="rightAnswer">&nbsp;</td>
                    <td class="rightAnswer">&nbsp;</td>
                    <td class="noAnswer">&nbsp;</td>
                    <td class="noAnswer">&nbsp;</td>
                    <td class="noAnswer">&nbsp;</td>
                    <td class="noAnswer">&nbsp;</td>
                    <td class="noAnswer">&nbsp;</td>
                    <td class="noAnswer">&nbsp;</td>
                    <td class="noAnswer">&nbsp;</td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
