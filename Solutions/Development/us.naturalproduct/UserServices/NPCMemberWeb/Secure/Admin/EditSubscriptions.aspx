<%@ Page Language="C#" EnableViewState="true" MasterPageFile="~/SiteMasters/Secure.master"
    AutoEventWireup="true" CodeFile="EditSubscriptions.aspx.cs" Inherits="us.naturalproduct.web.EditSubscriptions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContent" runat="Server">
    <h1>
        Edit Subscriptions</h1>
    <div id="BodyContent">
        <div class="RegistrationForm">
            <fieldset class="ValSummary">
                <asp:ValidationSummary ID="vsAddSubscription" ValidationGroup="Add" HeaderText="Please correct the following errors:"
                    runat="server" />
                <asp:Label ID="LblStatus" runat="server" EnableViewState="false" />
            </fieldset>
            <br />
            <fieldset>
                <legend>Add New Subscription</legend>
                <fieldset>
                    <legend>Select Volume, Issue and Article</legend>
                    <div>
                        <label>
                            Volume:</label>
                        <asp:DropDownList ID="ddlVolumes" ValidationGroup="Add" DataTextField="VolumeName"
                            DataValueField="VolumeId" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlVolumes_SelectedIndexChanged" />
                        <asp:RequiredFieldValidator ID="rfvVolumes" runat="server" ControlToValidate="ddlVolumes"
                            ValidationGroup="Add" ErrorMessage="A volume is required.">*</asp:RequiredFieldValidator>
                    </div>
                    <div>
                        <label>
                            Issue:</label>
                        <asp:DropDownList ID="ddlIssues" ValidationGroup="Add" DataTextField="IssueName"
                            DataValueField="VolumeIssueId" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlIssues_SelectedIndexChanged" />
                        <asp:RequiredFieldValidator ID="rfvIssues" runat="server" ControlToValidate="ddlIssues"
                            ValidationGroup="Add" ErrorMessage="An issue is required.">*</asp:RequiredFieldValidator>
                    </div>
                    <div>
                        <asp:GridView ID="GvNewSubscriptions" runat="server" ValidationGroup="Add" AutoGenerateColumns="False"
                            DataKeyNames="ArticleId">
                            <Columns>
                                <asp:BoundField DataField="ArticleId" Visible="False" />
                                <asp:BoundField HeaderText="Article" DataField="Title" ReadOnly="True" />
                                <%--                                    <asp:TemplateField HeaderText="Effective Date">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbDateEffective" runat="server" CssClass="Date" Text='<%# DateTime.Now.ToString("MM/dd/yyyy") %>' />
                                            <asp:RequiredFieldValidator ID="rfvDateEffective" ControlToValidate="tbDateEffective" runat="server" ValidationGroup="Add" ErrorMessage="Effective date is required.">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="revDateEffective" ControlToValidate="tbDateEffective" runat="server" ErrorMessage="Date must be in mm/dd/yyy format." 
                                             ValidationExpression="(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d" ValidationGroup="Add">*</asp:RegularExpressionValidator>                            
                                        </ItemTemplate>
                                    </asp:TemplateField>
--%>
                                <%--                                    <asp:TemplateField HeaderText="Expiration Date">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbDateExpiration" runat="server" CssClass="Date" Text='<%# DateTime.Now.ToString("MM/dd/yyyy") %>' />
                                            <asp:RequiredFieldValidator ID="rfvDateExpiration" ControlToValidate="tbDateExpiration" ValidationGroup="Add" runat="server" ErrorMessage="Expiration date is required.">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="revDateExpiration" ControlToValidate="tbDateExpiration" runat="server" ErrorMessage="Date must be in mm/dd/yyy format." 
                                             ValidationExpression="(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d" ValidationGroup="Add">*</asp:RegularExpressionValidator>                            
                                        </ItemTemplate>
                                    </asp:TemplateField>
--%>
                                <asp:TemplateField HeaderText="Active">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbxActive" runat="server" Checked="true" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="cbxSubscribeAll" Text="Subscribe" runat="server" AutoPostBack="true"
                                            OnCheckedChanged="cbxSubscribeAll_OnCheckedChanged" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbxSubscribe" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div>
                        <label>
                            Date Effective:</label>
                        <asp:TextBox ID="tbDateEffective" runat="server" CssClass="Date" />
                        <asp:RequiredFieldValidator ID="rfvDateEffective" ControlToValidate="tbDateEffective"
                            runat="server" ValidationGroup="Add" ErrorMessage="Effective date is required.">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revDateEffective" ControlToValidate="tbDateEffective"
                            runat="server" ErrorMessage="Date must be in mm/dd/yyy format." ValidationExpression="(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d"
                            ValidationGroup="Add">*</asp:RegularExpressionValidator>
                    </div>
                    <div>
                        <label>
                            Date Expiration:</label>
                        <asp:TextBox ID="tbDateExpiration" runat="server" CssClass="Date" />
                        <asp:RequiredFieldValidator ID="rfvDateExpiration" ControlToValidate="tbDateExpiration"
                            ValidationGroup="Add" runat="server" ErrorMessage="Expiration date is required.">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revDateExpiration" ControlToValidate="tbDateExpiration"
                            runat="server" ErrorMessage="Date must be in mm/dd/yyy format." ValidationExpression="(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d"
                            ValidationGroup="Add">*</asp:RegularExpressionValidator>
                    </div>
                        <div>
                            <asp:Button ID="BtnAddSubscriptions" ValidationGroup="Add" Text="Add Selected Subscriptions"
                                runat="server" OnClick="BtnAddSubscriptions_Click" />
                        </div>
                </fieldset>
            </fieldset>
            <fieldset class="ValSummary">
                <asp:ValidationSummary ID="vsEditSubscription" ValidationGroup="Edit" HeaderText="Please correct the following errors:"
                    runat="server" />
            </fieldset>
            <br />
            <fieldset>
                <legend>Edit Existing Subscriptions</legend>
                <div>
                    <asp:GridView ID="GvExistingSubscriptions" runat="server" AutoGenerateColumns="False"
                        DataKeyNames="SubscriptionId,VolumeIssueId,ArticleId" AllowSorting="True" AllowPaging="True"
                        OnRowDeleting="GvExistingSubscriptions_RowDeleting" OnRowEditing="GvExistingSubscriptions_RowEditing"
                        OnPageIndexChanging="GvExistingSubscriptions_PageIndexChanging" OnRowCancelingEdit="GvExistingSubscriptions_RowCancelingEdit"
                        OnRowUpdating="GvExistingSubscriptions_RowUpdating" OnRowUpdated="GvExistingSubscriptions_RowUpdated">
                        <HeaderStyle CssClass="HdrRow" />
                        <AlternatingRowStyle CssClass="AltRow" />
                        <Columns>
                            <asp:CommandField ShowEditButton="True" />
                            <asp:BoundField DataField="SubscriptionId" Visible="False" />
                            <asp:BoundField DataField="VolumeIssueId" Visible="False" ReadOnly="True" />
                            <asp:BoundField DataField="ArticleId" Visible="False" ReadOnly="True" />
                            <asp:BoundField DataField="VolumeName" HeaderText="Volume" SortExpression="Volume"
                                ReadOnly="True" />
                            <asp:BoundField DataField="IssueName" HeaderText="Issue" SortExpression="Issue" ReadOnly="True" />
                            <asp:BoundField DataField="ArticleName" HeaderText="Article" SortExpression="Article"
                                ReadOnly="True" />
                            <asp:BoundField DataField="EffectiveDate" HeaderText="Effective Date" SortExpression="EffectiveDate"
                                DataFormatString="{0:d}" HtmlEncode="False" ApplyFormatInEditMode="True">
                                <ControlStyle CssClass="Date" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ExpirationDate" HeaderText="Expiration Date" SortExpression="ExpirationDate"
                                DataFormatString="{0:d}" HtmlEncode="False" ApplyFormatInEditMode="True">
                                <ControlStyle CssClass="Date" />
                            </asp:BoundField>
                            <asp:CheckBoxField DataField="IsActive" HeaderText="Active" SortExpression="Active" />
                            <asp:TemplateField HeaderText="Remove">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImgBtnDelete" runat="server" ImageUrl="~/images/remove.gif"
                                        CommandName="Delete" OnClientClick="if (confirm('Are you sure you want to remove this article subscription?')) return true; else return false;" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--                            <asp:CommandField DeleteImageUrl="~/images/remove.gif" ButtonType="Image" ShowDeleteButton="True" />                            
--%>
                        </Columns>
                        <SelectedRowStyle BackColor="LightSkyBlue" />
                        <EditRowStyle BackColor="LightSkyBlue" />
                    </asp:GridView>
                </div>
                <div>
                    <asp:Button ID="BtnRemoveAllSubscriptions" ValidationGroup="Remove" Text="Remove All Subscriptions"
                        runat="server" OnClientClick="if (confirm('Are you sure you want to remove all article subscriptions?')) return true; else return false;"
                        OnClick="BtnRemoveAllSubscriptions_Click" />
                </div>
            </fieldset>
        </div>
    </div>
</asp:Content>
