<%@ Page Language="C#" MasterPageFile="~/SiteMasters/Unsecure.master" AutoEventWireup="true" CodeFile="Error.aspx.cs" Inherits="us.naturalproduct.web.Error" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageContent" Runat="Server">
    <h1>Unexpected Error</h1>
        <div id="BodyContent">
            <p>
                The system has encountered an unexpected error.  The error has been logged and the appropriate 
                personnel have been notified regarding this issue.  Meanwhile, please click the link below 
                to return to the home page. 
            </p>
            <p>
                If this becomes a recurring problem, please contact Customer Service:
                <a href="mailto:customerservice@naturalproduct.us">customerservice@naturalproduct.us</a>           
            </p>
            <p>
                Thank You
            </p>
            <div align="left">
                <asp:HyperLink ID="lnkHome" Text="Go to Home Page" NavigateUrl="~/Secure/Home.aspx" runat="server" /> 
            </div>               
        </div>
</asp:Content>

