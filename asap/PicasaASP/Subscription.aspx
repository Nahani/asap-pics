<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Subscription.aspx.cs" Inherits="PicasaASP.Subscription" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="connexion_form">
        <p id="title">Subscription</p>
        <p>
            Last name&nbsp;
            <asp:TextBox ID="LN" runat="server" />
        </p>
        <p>
            First name&nbsp;
            <asp:TextBox ID="FN" runat="server" />
        </p>
        <p>
            Login&nbsp;
            <asp:TextBox ID="LO" runat="server" />
        </p>
        <p>
            Password&nbsp;
            <asp:TextBox TextMode="Password" ID="PW"  runat="server" />
        </p>
        <p>
            Mail&nbsp;
            <asp:TextBox ID="MA" runat="server" />
        </p>
        <p>
            <asp:Button ID="Validate" runat="server" OnClick="Subscription_Click" Text="Validate" />
        </p>      
    </div>
    </form>
</body>
</html>
