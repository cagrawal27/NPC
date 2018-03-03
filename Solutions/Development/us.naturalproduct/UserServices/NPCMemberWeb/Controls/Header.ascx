<%@ Control Language="c#" Inherits="us.naturalproduct.web.Header" CodeFile="Header.ascx.cs" %>
<div>
<div id="PageSuperHeader"></div>
<div id="PageHeader">Natural Product Communications</div>
<div id="PageSubHeader">
    <asp:LoginView id="LoginView1" runat="server">
        <LoggedInTemplate>
            Welcome, <b>
                <asp:LoginName ID="LoginName1" runat="server" /></b>&nbsp;|
            <asp:loginstatus id="LoginStatus1" LogoutText="Log Out" 
                runat="server" logoutpageurl="~/Logout.aspx" logoutaction="Redirect" />                
        </LoggedInTemplate>
    </asp:LoginView>   
</div>
</div>