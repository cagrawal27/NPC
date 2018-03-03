<%@ Page Language="C#" MasterPageFile="~/SiteMasters/Secure.master" AutoEventWireup="true" CodeFile="Issue.aspx.cs" Inherits="us.naturalproduct.web.Issue" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageContent" Runat="Server">
	<div id="BodyContent">
       <h2 id="HdrVolume" runat="server">Volume <%# Eval("VolumeName") %> (<%# Eval("VolumeYear") %>)</h2>
	   <h3 id="HdrIssue" runat="server">Issue <%# Eval("IssueName") %> </h3>
        <div class="IssueDetails">
            <asp:Repeater ID="rptrIssueDocs" runat="server">
                <ItemTemplate>
                    <h4>
                        <asp:HyperLink ID="lnkIssueDoc" NavigateUrl='<%# Eval("DocId", "javascript:PopUp(\"ViewDoc.aspx?docId={0}\");") %>' runat="server" Text="PDF" ImageUrl="~/images/pdf_icon.gif" />
                        <asp:HyperLink ID="lnkIssueDocText" NavigateUrl='<%# Eval("DocId", "javascript:PopUp(\"ViewDoc.aspx?docId={0}\");") %>' runat="server" Text='<%# String.Format("{0} ({1}KB)",Eval("IssueDocTypeDescription").ToString(), Eval("FileSizeKB").ToString()) %>' />
                    </h4>
                </ItemTemplate>
            </asp:Repeater>
        </div>                  
        <div class="ArticleList">
            <asp:Repeater ID="rptrArticles" runat="server" OnItemDataBound="rptrArticles_ItemDataBound">
                <ItemTemplate>
                    <div>
                        <asp:Label ID="lblArticleId" runat="server" Visible="false" Text='<%# Eval("ArticleId") %>' />
                        <span class="title"><%# Eval("Title") %></span><br />                        
                        <span class="auth"><%# Eval("Authors") %></span><br />
                        <span>Keywords:  <%# Eval("Keywords") %></span><br />
                        <span>Web Published Date:  <%# Eval("CreationDateTime", "{0:dd MMM yyyy}")%></span><br />
                        <asp:Repeater ID="rptrDocs" runat="server">
                            <ItemTemplate>
                                <asp:HyperLink ID="lnkDoc" NavigateUrl='<%# Eval("DocId", "javascript:PopUp(\"ViewDoc.aspx?docId={0}\");") %>' runat="server" Text="(PDF)" ImageUrl="~/images/pdf_icon.gif" />
                                <asp:HyperLink ID="lnkDocText" NavigateUrl='<%# Eval("DocId", "javascript:PopUp(\"ViewDoc.aspx?docId={0}\");") %>' runat="server" Text='<%# String.Format("{0} ({1}KB)",Eval("ArtDocTypeDescription").ToString(), Eval("FileSizeKB").ToString()) %>' />&nbsp;&nbsp;
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </ItemTemplate>           
            </asp:Repeater>                                
        </div>
	</div>   
</asp:Content>