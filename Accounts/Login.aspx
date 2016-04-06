<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="RHSauth.Accounts.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        Domain<br />
        <asp:DropDownList ID="ddlDomain" runat="server" AutoPostBack="True" 
            onselectedindexchanged="ddlDomain_SelectedIndexChanged">
            <asp:ListItem Selected="True">STUDENT</asp:ListItem>
            <asp:ListItem>ROCKHURST</asp:ListItem>
        </asp:DropDownList>
        <br />
        <br />
        <asp:Login ID="Login1" runat="server" DestinationPageUrl="~/Default.aspx">
        </asp:Login>
    
    </div>
    </form>
</body>
</html>
