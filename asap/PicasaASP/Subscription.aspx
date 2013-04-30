<%@ Page Language="C#" MasterPageFile="~/design/MasterPage.master" ValidateRequest="false"
    AutoEventWireup="true" CodeFile="Subscription.aspx.cs" Inherits="PicasaASP.Subscription" %>

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
    <div id="connexion_form" style="text-align: center;">
        <br />
        <h1 style="text-align: center;" id="title">
            Subscription</h1>
        <br />
        <asp:Label Style="color: Red;" ID="reponse" runat="server"> </asp:Label>
        <br />
        <br />
        <table style="margin: 0 auto;">
            <tr>
                <td>
                    <asp:Label> Last name&nbsp;</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="LN" size="30" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label> First name&nbsp;</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="FN" runat="server" size="30" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label> Login&nbsp;</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="LO" runat="server" size="30" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label>Password&nbsp;</asp:Label>
                </td>
                <td>
                    <asp:TextBox TextMode="Password" ID="PW" runat="server" size="30" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label>Mail&nbsp;</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="MA" runat="server" size="30" />
                </td>
            </tr>
        </table>
        <p>
            <br />
            <span class="art-button-wrapper"><span class="art-button-l"></span><span class="art-button-r">
            </span>
                <asp:LinkButton class="art-button" runat="server" ID="Button1" OnCommand="Subscription_Click">Validate </asp:LinkButton>
            </span>
        </p>
        <br />
        <br />
    </div>
</asp:Content>
