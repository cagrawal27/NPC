<%@ Page Language="C#" EnableViewState="true" MasterPageFile="~/SiteMasters/Secure.master" AutoEventWireup="true" CodeFile="AddArticle.aspx.cs" Inherits="us.naturalproduct.web.AddArticle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageContent" Runat="Server">
    <h1>Add Article</h1>
    <div id="BodyContent">
        <div class="RegistrationForm">
            <fieldset>
                <legend>Choose Volume and Issue</legend>
                <div class="help">
                    <h4>Help</h4>
                    Choose the volume and issue that this article will belong to.
                </div>                
                <asp:SqlDataSource
                    ID="sdsVolumes" runat="server" ConnectionString="<%$ ConnectionStrings:NPC %>"
                    SelectCommand="spGetAllVolumes" SelectCommandType="StoredProcedure">
                </asp:SqlDataSource>                
                <div>
                    <label>Volume:</label>
                    <asp:DropDownList ID="ddlVolumes" runat="server" AutoPostBack="True" DataSourceID="sdsVolumes" DataTextField="VolumeName" DataValueField="VolumeId" />
                    <asp:requiredfieldvalidator ID="RfvVolumes" runat="server"
                        ControlToValidate="ddlVolumes"
                        errormessage="A volume is required.">*</asp:requiredfieldvalidator>                         
                </div>
                <asp:SqlDataSource
                    ID="sdsIssues" runat="server" ConnectionString="<%$ ConnectionStrings:NPC %>"
                    SelectCommand="spGetAllIssues" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlVolumes" Name="VolumeId" PropertyName="SelectedValue"
                            Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>                
                <div>
                    <label>Issue:</label>
                    <asp:DropDownList ID="ddlIssues" runat="server" DataSourceID="sdsIssues" DataTextField="IssueName" DataValueField="VolumeIssueId" />
                    <asp:requiredfieldvalidator ID="RfvIssues" runat="server" 
                        ControlToValidate="ddlIssues" 
                        errormessage="An issue is required.">*</asp:requiredfieldvalidator>
                </div>
            </fieldset>
            <fieldset>
                <legend>Enter Article Information</legend>
                <div>
                    <label>Title:</label>
                    <asp:TextBox ID="tbTitle" runat="server" MaxLength="800" TextMode="multiLine" />
                    <asp:requiredfieldvalidator ID="rfvTitle" runat="server"
                        ControlToValidate="tbTitle"
                        errormessage="Title is required.">*</asp:requiredfieldvalidator>
                </div>
                <div>
                    <label>Authors:</label>
                    <asp:TextBox ID="tbAuthors" runat="server" MaxLength="800" TextMode="multiLine" />
                    <asp:requiredfieldvalidator ID="RfvAuthors" runat="server"
                        ControlToValidate="tbAuthors"
                        errormessage="Authors are required.">*</asp:requiredfieldvalidator>                         
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
                        <asp:ListItem Selected="true" Text="Yes" Value="1" />
                        <asp:ListItem Text="No" Value="0" />                        
                    </asp:DropDownList>
                </div>
            </fieldset>   
            <fieldset class="ValSummary">
                <asp:ValidationSummary ID="VsAddArticle" HeaderText="Please correct the following errors:" runat="server" />            
            </fieldset><br />               
            <fieldset>
                <legend>Add Documents</legend>
                <div>
                    <label>Choose File:</label>
                    <asp:FileUpload ID="tbUploadDocument" runat="server" />
                    <asp:RequiredFieldValidator ID="rfvUploadDoc" runat="server" 
                        ControlToValidate="tbUploadDocument" ValidationGroup="Documents" 
                        ErrorMessage="A document must be selected">*</asp:RequiredFieldValidator>
                </div>
                <div>
                    <label>Document Type:</label>
                    <asp:DropDownList ID="ddlArticleDocTypes" runat="server" />
                    <asp:RequiredFieldValidator ID="rfvArtDocType" runat="server" 
                        ControlToValidate="ddlArticleDocTypes" ValidationGroup="Documents" 
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
                <div>               
                    <asp:GridView ID="gvDocuments" DataKeyNames="DocId" runat="server" AutoGenerateColumns="False" AllowSorting="false" OnRowDataBound="gvDocuments_RowDataBound" OnRowDeleting="gvDocuments_RowDeleting">
                        <HeaderStyle CssClass="HdrRow" />
                        <AlternatingRowStyle CssClass="AltRow" />
                        <Columns>
                            <asp:BoundField Visible="false" DataField="DocId" />
                            <asp:BoundField HeaderText="File Name" DataField="FileName" />
                            <asp:TemplateField HeaderText="Document Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblDocType" runat="server" />                                   
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
            </fieldset>    
            <fieldset class="ValSummary">
                <asp:ValidationSummary ID="vsDocument" ValidationGroup="Documents" HeaderText="Please correct the following errors:" runat="server" />                   
                <asp:Label ID="lblErrors" runat="server" />                       
            </fieldset><br />          
            <asp:Button ID="btnAddArticle" runat="server" Text="Add Article" OnClick="btnAddArticle_OnClick" />
            <asp:Button ID="btnReset" Text="Reset" runat="server" OnClick="btnReset_Click" />           
        </div>
    </div>
</asp:Content>