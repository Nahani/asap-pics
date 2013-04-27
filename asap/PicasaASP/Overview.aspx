<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Overview.aspx.cs" Inherits="PicasaASP.Overview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="main" runat="server">
    <p id="title">Brief overview of albums</p>
    <p class="P">Your albums</p>
    <asp:Panel ID="albums" runat="server" />
    <hr />
    <p class="P">Available read-only online albums </p>
    <asp:Panel ID="albumsVisu" runat="server" />
    <hr />
    <p class="P">Images</p>
    <asp:Panel ID="images" runat="server"/>
    </form>
</body>
</html>
