<%@ Control Language="c#" Inherits="us.naturalproduct.web.Menu" CodeFile="Menu.ascx.cs" %>
<div id="Menu">
	<asp:HyperLink ID="lnkHome" runat="server" ToolTip="Home" Text="Home" NavigateUrl="~/Secure/Home.aspx" /><br />
	<asp:HyperLink ID="lnkJrnlArchive" runat="server" ToolTip="View Journal Archive" Text="Journal Archive" NavigateUrl="~/Secure/JournalArchive.aspx" /><br />
	<asp:HyperLink ID="LnkChngSecurity" runat="server" ToolTip="Change Security Information" Text="Change Security" NavigateUrl="~/Secure/ChangeSecurity.aspx" /><br />
</div>