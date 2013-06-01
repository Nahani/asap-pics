﻿<%@ Page Language="C#" MasterPageFile="~/design/MasterPage.master" ValidateRequest="false"
    AutoEventWireup="true" CodeFile="Overview.aspx.cs" Inherits="PicasaASP.Overview" %>

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
    <br />
    <br />
    <div style="text-align: center;">
        <h1 style="text-align: center;" id="title">
            Brief overview of your albums</h1>
        <br />
        <h1 ID="reponseAlbums" style="text-align: center; color: green;" runat="server" />
        <br />
        <asp:Panel ID="albums" runat="server" width="100%" />
        <hr />
        <h1 style="text-align: center;" id="H1">
            Available read-only online albums</h1>
        <br />
        <h1 ID="reponseOtherAlbums" style="text-align: center; color: green;" runat="server" />
        <br />
        <asp:Panel ID="albumsVisu" runat="server" width="100%" />
        <hr />
        <h1 ID="reponse" style="text-align: center; color: green;" runat="server" >
            Please choose an album to print its pictures</h1>
        <br />
        <br />
        <asp:Panel ID="images" runat="server" width="100%" />
        <br />
        <br />
        <br />
        <br />
    </div>
</asp:Content>