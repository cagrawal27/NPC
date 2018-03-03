<%@ Page Language="C#" MasterPageFile="~/SiteMasters/Secure.master" AutoEventWireup="true" CodeFile="AddIssue.aspx.cs" Inherits="us.naturalproduct.web.AddIssue" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageContent" Runat="Server">
    <h1>Add Issue</h1>
    <div id="BodyContent">
        <div class="RegistrationForm">
            <fieldset>
                <legend>Choose Volume</legend>
                <div class="help">
                    <h4>Help</h4>
                    Choose the volume that this issue will belong to.
                </div>                
                <asp:SqlDataSource
                    ID="sdsVolumes" runat="server" ConnectionString="<%$ ConnectionStrings:NPC %>"
                    SelectCommand="spGetAllVolumes" SelectCommandType="StoredProcedure">
                </asp:SqlDataSource>                
                <div>
                    <label>Volume:</label>
                    <asp:DropDownList ID="ddlVolumes" runat="server" DataSourceID="sdsVolumes" DataTextField="VolumeName" DataValueField="VolumeId" />
                    <asp:requiredfieldvalidator ID="RfvVolumes" runat="server"
                        ControlToValidate="ddlVolumes"
                        errormessage="A volume is required.">*</asp:requiredfieldvalidator>                         
                </div>
            </fieldset>
            <fieldset>
                <legend>Enter Issue Information</legend>
                <div>
                    <label>Issue Name:</label>
                    <asp:TextBox ID="tbIssueName" runat="server" />
                    <asp:requiredfieldvalidator ID="rfvIssueName" runat="server"
                        ControlToValidate="tbIssueName"
                        errormessage="Issue name is required.">*</asp:requiredfieldvalidator>
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
                <asp:ValidationSummary ID="vsAddIssue" HeaderText="Please correct the following errors:" runat="server" />            
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
                <div>               
                    <asp:GridView ID="gvDocuments" DataKeyNames="DocId" runat="server" AutoGenerateColumns="False" AllowSorting="false" OnRowDataBound="gvDocuments_RowDataBound" OnRowDeleting="gvDocuments_RowDeleting">
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
            </fieldset>    
            <fieldset class="ValSummary">
                <asp:ValidationSummary ID="vsDocument" ValidationGroup="Documents" HeaderText="Please correct the following errors:" runat="server" />                   
                <asp:Label ID="lblErrors" runat="server" />                       
            </fieldset><br />          
            <asp:Button ID="btnAddIssue" runat="server" Text="Add Issue" OnClick="btnAddIssue_Click" />
            <asp:Button ID="btnReset" Text="Reset" runat="server" OnClick="btnReset_Click" />           
        </div>
    </div>
</asp:Content>

