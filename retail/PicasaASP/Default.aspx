<%@ Page Language="C#" MasterPageFile="~/design/MasterPage.master" ValidateRequest="false" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="_Default" Title="Untitled Page" %>
<%@ Register TagPrefix="art" TagName="DefaultMenu" Src="DefaultMenu.ascx" %> 
<%@ Register TagPrefix="art" TagName="DefaultHeader" Src="DefaultHeader.ascx" %> 

<asp:Content ID="PageTitle" ContentPlaceHolderID="TitleContentPlaceHolder" Runat="Server">
     Welcome to asap-PICS
</asp:Content>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContentPlaceHolder" Runat="Server">
    <art:DefaultHeader ID="DefaultHeader" runat="server" />
</asp:Content>
<asp:Content ID="MenuContent" ContentPlaceHolderID="MenuContentPlaceHolder" Runat="Server">
    <art:DefaultMenu ID="DefaultMenuContent" runat="server" />
</asp:Content>

<asp:Content ID="SheetContent" ContentPlaceHolderID="SheetContentPlaceHolder" Runat="Server">
    <div style="text-align: center;">
        <br />
        <p>
    	    <span class="art-button-wrapper">
    		    <span class="art-button-l"> </span>
    		    <span class="art-button-r"> </span>
    		    <a class="art-button" href="Welcome.aspx">Login</a>
    	    </span>
        </p>
        <br />

        <p>
    	    <span class="art-button-wrapper">
    		    <span class="art-button-l"> </span>
    		    <span class="art-button-r"> </span>
    		    <a class="art-button" href="Subscription.aspx">Suscribe</a>
    	    </span>
        </p>
        <br />
        <br />
    </div>
   
 
</asp:Content>
