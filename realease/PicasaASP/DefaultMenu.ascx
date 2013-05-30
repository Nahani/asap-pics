<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DefaultMenu.ascx.cs" Inherits="Menu" %>
<ul class="art-menu">
    <li><a href="Welcome.aspx" class=" active"><span class="l"></span><span class="r"></span>
        <span class="t">Login</span></a></li>
    <li><a href="Subscription.aspx"><span class="l"></span><span class="r"></span><span
        class="t">Subscription</span></a> </li>
    <li><a href="Overview.aspx"><span class="l"></span><span class="r"></span><span class="t">
        Album overview</span></a></li>
    <% if (Session["id"] != null)
       { %>
    <li><a runat="server" onclick="alert('Disconnection successful !');" onServerClick="disconnect">
        <span class="l"></span><span class="r"></span><span class="t">
        Disconnect</span></a></li>
    <%      
        } %>
</ul>
