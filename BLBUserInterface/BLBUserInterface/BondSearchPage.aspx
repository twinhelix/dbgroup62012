<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BondSearchPage.aspx.cs" Inherits="BLBUserInterface.BondSearchPage" %>

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
    
        <asp:Label ID="LabelBondSearch" runat="server" Text="Search for a Bond" 
            Font-Bold="True" Font-Size="Larger"></asp:Label>
    
    </div>
    <table class="style1">
        <tr>
            <td>
    
        <asp:DropDownList ID="DropDownSearch" runat="server"            
            AutoPostBack="True">
        </asp:DropDownList>
    
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="TextSearchString" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorInput" runat="server" 
                    ErrorMessage="Please enter a value!" ControlToValidate="TextSearchString"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Button ID="ButtonSearch" runat="server" Text="Search" 
                    onclick="ButtonSearch_Click" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <asp:GridView ID="GVSearchResult" runat="server">
    <Columns>
    <asp:TemplateField HeaderText = "Select Bond">
               <ItemTemplate>
                  <input name ="RadioButton" type = "radio" value='<%# Eval("CUSIP") %>' />
               </ItemTemplate>
              </asp:TemplateField>
              </Columns>
    </asp:GridView>
    <asp:Label ID="LabelResults" runat="server" Text="No results found!"></asp:Label>
    <br />
    <br />
        <asp:Label ID="BuyLabel" runat="server" Text="Select bond to buy"></asp:Label>
    <br />
    <asp:Label ID="LabelQuantity" runat="server" Text="Quantity to Buy: "></asp:Label>
    <asp:TextBox ID="TBQuantity" runat="server"></asp:TextBox>
    <asp:Button ID="BTNBuy" runat="server" Text="Buy Selected Bond" 
        onclick="BTNBuy_Click" />
    <p>
        &nbsp;</p>
    </form>
</body>
</html>
