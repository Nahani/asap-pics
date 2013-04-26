<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Welcome.aspx.cs" Inherits="PicasaASP.Welcome" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Welcome to ASAP PICS</title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="connexion_form">
        <p id="title">Authentification</p>
        <p>
            Login&nbsp;
            <asp:TextBox ID="UserBox" runat="server" />
        </p>
        <p>
            Password&nbsp;
            <asp:TextBox TextMode="Password" ID="PassBox"  runat="server" />
        </p>
        <p>
            <asp:Button ID="Login" runat="server" OnClick="Login_Click" Text="Login" />
        </p>
        <p>
        <asp:Button ID="Button1" runat="server" OnClick="Subscription_Click" Text="Subscribe" />
<%
          if (Session["user"] != null)
          {
              Response.Write("Hello  "+ Session["user"]);
          }
          else 
          {
              Response.Write("Unknown user !");
          }
     %>
</p>
    </div>
    </form>
    <p>
    
    </p>
</body>
</html>
