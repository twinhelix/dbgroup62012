<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BLBUserInterface.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="LabelBonds" runat="server" Text="DB Bonds Market"></asp:Label>
    
        <asp:GridView ID="GVBonds" runat="server">
        <Columns>
    <asp:TemplateField HeaderText = "Select Bond">
               <ItemTemplate>
                  <input name ="RadioButton" type = "radio" value='<%# Eval("CUSIP") %>' />
               </ItemTemplate>
              </asp:TemplateField>
              </Columns>
        </asp:GridView>
    
        <br />
        <asp:Label ID="BuyLabel" runat="server" Text="Select bond to buy"></asp:Label>
    <br />
    <asp:Label ID="LabelQuantity" runat="server" Text="Quantity to Buy: "></asp:Label>
    <asp:TextBox ID="TBQuantity" runat="server"></asp:TextBox>
    <asp:Button ID="BTNBuy" runat="server" Text="Buy Selected Bond" 
        onclick="BTNBuy_Click" />
    
        </div>
    </form>
</body>
</html>
