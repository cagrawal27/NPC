<%@ Page Language="C#" EnableViewState="true" MasterPageFile="~/SiteMasters/Secure.master" AutoEventWireup="true" CodeFile="ManageIssues.aspx.cs" Inherits="us.naturalproduct.web.ManageIssues" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageContent" Runat="Server">
    <h1>Manage Issues</h1>
    <div id="BodyContent">
        <div class="RegistrationForm">
            <fieldset class="ValSummary">
                &nbsp;<asp:Label ID="lblStatus" runat="server" />                                       
            </fieldset><br />          
            <fieldset>
                <legend>Select Volume & Issue</legend>
                <div>
                    <label>Volume:</label>
                    <asp:DropDownList ID="ddlVolumes" runat="server" AutoPostBack="True" DataTextField="VolumeName" DataValueField="VolumeId" OnSelectedIndexChanged="ddlVolumes_SelectedIndexChanged" />
                </div>
                <div>
                    <label>Issue:</label>
                    <asp:DropDownList ID="ddlIssues" runat="server" DataTextField="IssueName" DataValueField="VolumeIssueId" OnSelectedIndexChanged="ddlIssues_SelectedIndexChanged" AutoPostBack="True" />
                </div>                
            </fieldset>
            <fieldset>
                <legend>Edit Issue Details</legend>              
                <div>
                    <label>Issue Name:</label>
                    <asp:TextBox ID="tbIssueName" runat="server" />
                    <asp:requiredfieldvalidator ID="rfvIssueName" runat="server"
                        ControlToValidate="tbIssueName"
                        errormessage="Issue name is required." ValidationGroup="Issue">*</asp:requiredfieldvalidator>
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
                                    <asp:Label ID="lblDocType" runat="server" Text='<%# Eval("IssueDocTypeDescription") %>' />                                   
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
                    <asp:DropDownList ID="ddlIssueDocTypes" runat="server" DataSourceID="sdsIssueDocTypes" DataTextField="IssueDocTypeDescription" DataValueField="IssueDocTypeId" /><asp:SqlDataSource
                        ID="sdsIssueDocTypes" runat="server" ConnectionString="<%$ ConnectionStrings:NPC %>"
                        SelectCommand="spGetIssueDocTypes" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                    <asp:RequiredFieldValidator ID="rfvIssueDocType" runat="server" 
                        ControlToValidate="ddlIssueDocTypes" ValidationGroup="Documents" 
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
                <asp:Button ID="btnUpdate" runat="server" Text="Update" ValidationGroup="Issue" CausesValidation="true" OnClick="btnUpdate_Click" />
            </div>              
        </div>
    </div>
</asp:Content>

