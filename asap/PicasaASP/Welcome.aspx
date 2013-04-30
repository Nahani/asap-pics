<%@ Page Language="C#" MasterPageFile="~/design/MasterPage.master" ValidateRequest="false"
    AutoEventWireup="true" CodeFile="Welcome.aspx.cs" Inherits="PicasaASP.Welcome" %>

<%@ Import Namespace="Artisteer" %>
<%@ Register TagPrefix="artisteer" Namespace="Artisteer" %>
<%@ Register TagPrefix="art" TagName="DefaultMenu" Src="DefaultMenu.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultHeader" Src="DefaultHeader.ascx" %>
<asp:Content ID="PageTitle" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
    Welcome to asap-PICS
</asp:Content>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContentPlaceHolder" runat="Server">
    <art:DefaultHeader ID="DefaultHeader" runat="server" />
</asp:Content>
<asp:Content ID="MenuContent" ContentPlaceHolderID="MenuContentPlaceHolder" runat="Server">
    <art:DefaultMenu ID="DefaultMenuContent" runat="server" />
</asp:Content>
<asp:Content ID="SheetContent" ContentPlaceHolderID="SheetContentPlaceHolder" runat="Server">
    <div style="text-align: center;">
        <div id="connexion_form">
            <br />
            <h1 style="text-align: center;" id="title">
                Authentification</h1>
            <br />
            <asp:Label Style="color: Red;" ID="reponse" runat="server"> </asp:Label>
            <br />
            <br />
            <table style="margin: 0 auto;">
                <tr>
                    <td>
                        Login&nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="UserBox" runat="server" size="30" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Password&nbsp;
                    </td>
                    <td>
                        <asp:TextBox TextMode="Password" ID="PassBox" size="30" runat="server" />
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <table style="margin: 0 auto;">
                <tr>
                    <span class="art-button-wrapper"><span class="art-button-l"></span><span class="art-button-r">
                    </span>
                        <asp:LinkButton class="art-button" runat="server" ID="Login" OnCommand="Login_Click">Login </asp:LinkButton>
                    </span>OR <span class="art-button-wrapper"><span class="art-button-l"></span><span
                        class="art-button-r"></span>
                        <asp:LinkButton class="art-button" runat="server" ID="Button1" OnCommand="Subscription_Click">Subscribe </asp:LinkButton>
                    </span>
                </tr>
            </table>
        </div>
        <br />
        <br />
        <br />
</asp:Content>
