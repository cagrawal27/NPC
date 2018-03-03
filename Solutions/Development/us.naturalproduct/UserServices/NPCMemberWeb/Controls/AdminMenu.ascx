<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminMenu.ascx.cs" Inherits="us.naturalproduct.web.AdminMenu" %>
<div id="AdminMenu">
    <a href="#">Administration</a>
    &nbsp;&nbsp;<asp:HyperLink ID="lnkArticles" runat="server" ToolTip="Articles" Text="Articles" NavigateUrl="~/Secure/Admin/ManageArticles.aspx" /><br />        
    &nbsp;&nbsp;&nbsp;&nbsp;<asp:HyperLink ID="lnkAddArticle" runat="server" ToolTip="Add Article" Text="Add Article" NavigateUrl="~/Secure/Admin/AddArticle.aspx" /><br />        
    &nbsp;&nbsp;<asp:HyperLink ID="lnkIssues" runat="server" ToolTip="Issues" Text="Issues" NavigateUrl="~/Secure/Admin/ManageIssues.aspx" /><br />    
    &nbsp;&nbsp;&nbsp;&nbsp;<asp:HyperLink ID="lnkAddIssue" runat="server" ToolTip="Add Issue" Text="Add Issue" NavigateUrl="~/Secure/Admin/AddIssue.aspx" /><br />        
    &nbsp;&nbsp;<asp:HyperLink ID="lnkSubscriptions" runat="server" ToolTip="Subscriptions" Text="Subscriptions" NavigateUrl="~/Secure/Admin/ManageSubscriptions.aspx" /><br />    
    &nbsp;&nbsp;<asp:HyperLink ID="lnkVolumes" runat="server" ToolTip="Volumes" Text="Volumes" NavigateUrl="~/Secure/Admin/ManageVolumes.aspx" /><br />    
    &nbsp;&nbsp;&nbsp;&nbsp;<asp:HyperLink ID="lnkAddVolume" runat="server" ToolTip="Add Volume" Text="Add Volume" NavigateUrl="~/Secure/Admin/AddVolume.aspx" /><br />        
    &nbsp;&nbsp;<asp:HyperLink ID="lnkUsers" runat="server" ToolTip="Users" Text="Users" NavigateUrl="~/Secure/Admin/ManageUsers.aspx" /><br />    
</div>