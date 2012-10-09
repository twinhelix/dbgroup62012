<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    <table class="style1">
        <tr>
            <td>
                <asp:Label ID="LabelUsername" runat="server" Text="Username:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LabelPassword" runat="server" Text="Password:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtPassword" runat="server" input type="password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtTotal" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="BtnLogin" runat="server" Height="26px" Text="Login" 
                    Width="128px" onclick="ButtonAdd_Click" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
