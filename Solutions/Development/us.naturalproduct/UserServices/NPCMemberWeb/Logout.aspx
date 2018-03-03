<%@ Page Language="C#" MasterPageFile="~/SiteMasters/Unsecure.master" AutoEventWireup="true" CodeFile="Logout.aspx.cs" Inherits="us.naturalproduct.web.Logout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageContent" Runat="Server">

    <h1>Logout Successful</h1>
        <div id="BodyContent">
        <p>You have successfully logged out of the system.</p>
        <div align="left">
            <asp:HyperLink ID="lnkDefault" Text="Go to Home Page" NavigateUrl="~/Default.aspx" runat="server" /> 
        </div><br />
        <div align="left">
            <asp:HyperLink ID="lnkLogin" Text="Go to Login Page" NavigateUrl="~/Secure/Login.aspx" runat="server" /> 
        </div>       

    </div>      
    
</asp:Content>

