<%@ Page Language="c#" MasterPageFile="~/SiteMasters/Secure.master" Inherits="us.naturalproduct.web.JournalArchive" CodeFile="JournalArchive.aspx.cs" %>
<asp:content contentplaceholderid="PageContent" runat="server">
	<h1>Journal Archive</h1>
	<div id="BodyContent">
        <asp:Repeater ID="rptrVolumes" runat="server" OnItemDataBound="rptrVolumes_ItemDataBound">
            <HeaderTemplate>
                <h2>Volumes</h2>
                <ul>
            </HeaderTemplate>
            <ItemTemplate>
                <li>
                   <asp:HyperLink ID="lnkVolume" runat="server" />               
                </li>
            </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
        </asp:Repeater>
	</div>
</asp:content>
