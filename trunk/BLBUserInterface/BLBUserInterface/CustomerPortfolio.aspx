<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerPortfolio.aspx.cs"
    Inherits="BLBUserInterface.CustomerPortfolio" %>

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
        <asp:Label ID="LabelTitle" runat="server" Text=""></asp:Label>
    </div>
    <table class="style1">
        <tr>
            <td>
                <asp:TextBox ID="TextBoxCustomerID" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="BtnGetPortfolio" runat="server" Text="Get Portfolio" OnClick="BtnGetPortfolio_Click" />
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <asp:GridView ID="GVPortfolio" runat="server">
        <Columns>
            <asp:TemplateField HeaderText="Select Bond">
                <ItemTemplate>
                    <input name="RadioButton" type="radio" value='<%# Eval("CUSIP") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <br />
    <asp:Label ID="BuyLabel" runat="server" Text="Select bond to sell"></asp:Label>
    <br />
    <asp:Label ID="LabelQuantity" runat="server" Text="Quantity to Sell: "></asp:Label>
    <asp:TextBox ID="TBQuantity" runat="server"></asp:TextBox>
    <asp:Button ID="BTNSell" runat="server" Text="Sell Selected Bond" OnClick="BTNSell_Click" />
    <br />
    </form>
</body>
</html>
