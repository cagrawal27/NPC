<%@ Page Language="C#" EnableViewState="true" MasterPageFile="~/SiteMasters/Secure.master" AutoEventWireup="true" CodeFile="ManageArticles.aspx.cs" Inherits="us.naturalproduct.web.ManageArticles" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageContent" Runat="Server">
    <h1>Manage Articles</h1>
    <div id="BodyContent">
        <div class="RegistrationForm">
            <fieldset class="ValSummary">
                &nbsp;<asp:Label ID="lblStatus" runat="server" />                                       
            </fieldset><br />  
            <fieldset>
                <legend>Select Volume, Issue and Article</legend>              
                <div>
                    <label>Volume:</label>
                    <asp:DropDownList ID="ddlVolumes" DataTextField="VolumeName" DataValueField="VolumeId" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlVolumes_SelectedIndexChanged" />
                    <asp:requiredfieldvalidator ID="rfvVolumes" runat="server"
                        ControlToValidate="ddlVolumes"
                        errormessage="A volume is required.">*</asp:requiredfieldvalidator>                         
                </div>
                <div>
                    <label>Issue:</label>
                    <asp:DropDownList ID="ddlIssues" DataTextField="IssueName" DataValueField="VolumeIssueId" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlIssues_SelectedIndexChanged" />
                    <asp:requiredfieldvalidator ID="rfvIssues" runat="server" 
                        ControlToValidate="ddlIssues" 
                        errormessage="An issue is required.">*</asp:requiredfieldvalidator>
                </div>
                <div>
                    <label>Article:</label>
                    <asp:DropDownList ID="ddlArticles" DataTextField="ArticleName" DataValueField="ArticleId" runat="server" OnSelectedIndexChanged="ddlArticles_SelectedIndexChanged" AutoPostBack="True" />
                    <asp:requiredfieldvalidator ID="rfvArticles" runat="server" 
                        ControlToValidate="ddlArticles" 
                        errormessage="An article is required.">*</asp:requiredfieldvalidator>
                </div>
            </fieldset>
            <fieldset>
                <legend>Edit Article Details</legend>              
                <div>
                    <label>Title:</label>
                    <asp:TextBox ID="tbTitle" runat="server" MaxLength="800" TextMode="multiLine" />
                    <asp:requiredfieldvalidator ID="rfvTitle" runat="server"
                        ControlToValidate="tbTitle"
                        errormessage="Title is required." ValidationGroup="Article">*</asp:requiredfieldvalidator>
                </div>
                <div>
                    <label>Authors:</label>
                    <asp:TextBox ID="tbAuthors" runat="server" MaxLength="800" TextMode="multiLine" />
                    <asp:requiredfieldvalidator ID="rfvAuthors" runat="server"
                        ControlToValidate="tbAuthors"
                        errormessage="Authors is required." ValidationGroup="Article">*</asp:requiredfieldvalidator>
                </div>
                <div>
                    <label>Keywords:</label>
                    <asp:TextBox ID="tbKeywords" runat="server" MaxLength="800" TextMode="multiLine" />
                </div>
                <div>
                    <label>Page Number:</label>
                    <asp:TextBox ID="tbPageNumber" runat="server" />
                    <asp:requiredfieldvalidator ID="rfvPageNumber" runat="server"
                        ControlToValidate="tbPageNumber" 
                        errormessage="Page number is required." ValidationGroup="Article">*</asp:requiredfieldvalidator>
                    <asp:RegularExpressionValidator ID="revPageNumber" ValidationGroup="Article"
                        runat="server" ErrorMessage="Page number must be a number."
                        ControlToValidate="tbPageNumber" ValidationExpression="^(\d){1,4}$">*</asp:RegularExpressionValidator>
                </div>
                <div>
                    <label>Active:</label>
                    <asp:DropDownList ID="ddlStatus" runat="server">
                        <asp:ListItem Text="Yes" Value="1" />
                        <asp:ListItem Text="No" Value="0" />                        
                    </asp:DropDownList>
                </div>                
            </fieldset>      
            <fieldset>
                <legend>Edit Documents</legend>
                <div>               
                    <asp:GridView ID="gvDocuments" DataKeyNames="DocId" runat="server" AutoGenerateColumns="False" AllowSorting="false" OnRowDeleting="gvDocuments_RowDeleting" OnRowDataBound="gvDocuments_RowDataBound">
                        <HeaderStyle CssClass="HdrRow" />
                        <AlternatingRowStyle CssClass="AltRow" />
                        <Columns>
                            <asp:BoundField Visible="false" DataField="DocId" />
                            <asp:BoundField HeaderText="File Name" DataField="FileName" />
                            <asp:TemplateField HeaderText="Document Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblDocType" runat="server" Text='<%# Eval("ArtDocTypeDescription") %>' />                                   
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Active">
                                <ItemTemplate>
                                    <asp:Label ID="lblActive" runat="server" Text='<%# ((bool) Eval("IsActive"))?"Yes":"No" %>' />                                
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField DeleteImageUrl="~/images/remove.gif" HeaderText="Remove" ButtonType="image" ShowDeleteButton="True" />
                        </Columns>
                    </asp:GridView>
                </div>
                <div>
                    <label>Choose File:</label>
                    <asp:FileUpload ID="tbUploadDocument" runat="server" />
                    <asp:RequiredFieldValidator ID="rfvUploadDoc" runat="server" 
                        ControlToValidate="tbUploadDocument" ValidationGroup="Documents" 
                        ErrorMessage="A document must be selected">*</asp:RequiredFieldValidator>
                </div>
                <div>
                    <label>Document Type:</label>
                    <asp:DropDownList ID="ddlArtDocTypes" runat="server" DataSourceID="sdsArtDocTypes" DataTextField="ArtDocTypeDescription" DataValueField="ArtDocTypeId" />
                    <asp:SqlDataSource ID="sdsArtDocTypes" runat="server" ConnectionString="<%$ ConnectionStrings:NPC %>"
                        SelectCommand="spGetArticleDocTypes" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                    <asp:RequiredFieldValidator ID="rfvArtDocType" runat="server" 
                        ControlToValidate="ddlArtDocTypes" ValidationGroup="Documents" 
                        ErrorMessage="A document type must be selected">*</asp:RequiredFieldValidator>
                </div>
                <div>
                    <label>Comments:</label>
                    <asp:TextBox ID="tbComments" runat="server" MaxLength="450" TextMode="multiLine" />
                </div>
                <div>
                    <label>Active:</label>
                    <asp:DropDownList ID="ddlDocStatus" runat="server">
                        <asp:ListItem Selected="true" Text="Yes" Value="1" />
                        <asp:ListItem Text="No" Value="0" />                        
                    </asp:DropDownList>
                </div>                
                <div>
                    <label></label>
                    <asp:Button ID="btnAddDoc" runat="server" Text="Add Document" ValidationGroup="Documents" OnClick="btnAddDoc_Click" />
                </div>
            </fieldset>            
            <div>
                <label></label>
                <asp:Button ID="btnUpdate" runat="server" Text="Update" ValidationGroup="Article" CausesValidation="true" OnClick="btnUpdate_Click" />
            </div>      
        </div>
    </div>
</asp:Content>

