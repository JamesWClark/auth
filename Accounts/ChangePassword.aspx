<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="RHSauth.Accounts.ChangePassword" %>

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
    
        <asp:ChangePassword ID="ChangePassword1" runat="server" DisplayUserName="True" 
            SuccessPageUrl="~/Default.aspx">
        </asp:ChangePassword>
    
    </div>
    </form>
</body>
</html>
