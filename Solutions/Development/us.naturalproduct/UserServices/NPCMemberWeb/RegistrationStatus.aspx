<%@ Page Language="C#" MasterPageFile="~/SiteMasters/Unsecure.master" AutoEventWireup="true" CodeFile="RegistrationStatus.aspx.cs" Inherits="us.naturalproduct.web.RegistrationStatus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageContent" Runat="Server">
    <h1>Registration Successful</h1>
        <div id="BodyContent">
            <p>You have successfully created an account with NPC.  Please be sure to 
            store you login information in a safe place.</p>
            <div align="left">
                <asp:HyperLink ID="lnkLogin" Text="Return To Home Page" NavigateUrl="~/Default.aspx" runat="server" /> 
            </div>               
        </div>    
</asp:Content>

