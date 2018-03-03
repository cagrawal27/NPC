<%@ Page Language="C#" MasterPageFile="~/SiteMasters/Secure.master" AutoEventWireup="true" CodeFile="IssueArchive.aspx.cs" Inherits="us.naturalproduct.web.IssueArchive" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageContent" Runat="Server">
 	<div id="BodyHead">
        <asp:HyperLink ID="lnkVolumeArchive" runat="server" Text="Back To Journal Archive" NavigateUrl="~/Secure/JournalArchive.aspx" />
	</div>	
	<div id="BodyContent">
        <h2><asp:label ID="lblVolume" runat="server" /></h2> 
	    <asp:Repeater ID="rptrIssues" runat="server" OnItemDataBound="rptrIssues_ItemDataBound">
            <HeaderTemplate>
                <ul>
            </HeaderTemplate>
            <ItemTemplate>
                <li>
                   <asp:HyperLink ID="lnkIssuePage" runat="server" />               
                </li>
            </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
	    </asp:Repeater>
	</div>
</asp:Content>