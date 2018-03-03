<%@ Page Language="C#" MasterPageFile="~/SiteMasters/Unsecure.master" CodeFile="InstitutionalLogin.aspx.cs" Inherits="us.naturalproduct.web.InstitutionalLogin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageContent" Runat="Server">
    <h1>Invalid Institutional Account</h1>
        <div id="BodyContent">
            <p>Your IP Address is <asp:Label ID="LblIPAddress" runat="server" />.  You do not have a valid Institutional Account. 
                Please contact Customer Service via the email address below to get your Institutional account activated.
                <a href="mailto:customerservice@naturalproduct.us">customerservice@naturalproduct.us</a>
                <br />
                
            </p>
            <br />
            <div align="left">
                <asp:HyperLink ID="lnkLogin" Text="Go to Secure Login Page" NavigateUrl="~/Secure/Login.aspx" runat="server" /> 
            </div>               
        </div>   
</asp:Content>

