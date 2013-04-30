<!--#include file="header.htm"-->
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
<!--#include file="footer.htm"-->