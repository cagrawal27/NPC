<%@ Page Language="C#" EnableViewState="true" MasterPageFile="~/SiteMasters/Secure.master" CodeFile="EditUser.aspx.cs" Inherits="us.naturalproduct.web.EditUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageContent" runat="Server">
    <h1>
        Edit User</h1>
    <div id="BodyContent">
        <div class="RegistrationForm">
            <fieldset class="ValSummary">
                <asp:Label ID="LblStatus" runat="server" EnableViewState="false" />
            </fieldset>            
            <fieldset>
                <legend>Edit User Details</legend>
                <div>
                    <div>
                        <label>
                            Email:</label>
                        <asp:TextBox ID="tbEmail" runat="server" Enabled="False" />
                    </div>
                    <div>
                        <label>
                            First Name:</label>
                        <asp:TextBox ID="tbFirstName" runat="server" />
                    </div>
                    <div>
                        <label>
                            Last Name:</label>
                        <asp:TextBox ID="tbLastName" runat="server" />
                    </div>
                    <div>
                        <label>
                            Middle Initial:</label>
                        <asp:TextBox ID="tbMiddleInitial" runat="server" />
                    </div>
                    <div>
                        <label>
                            Account Type:</label>
                        <asp:DropDownList ID="ddlAccountType" runat="server" />
                    </div>
                    <div>
                        <label>
                            Account Status:</label>
                        <asp:DropDownList ID="ddlAccountStatus" runat="server" />
                    </div>
                    <div>
                        <label>
                            Active:</label>
                        <asp:CheckBox ID="cbxActive" runat="server" />
                    </div>
                    <div>
                        <label>
                            Created Date/Time:</label>
                        <asp:Label ID="LblCreationDateTime" runat="server" CssClass="ColTwo" />
                    </div>
                    <div>
                        <label>
                            Update Date/Time:</label>
                        <asp:Label ID="LblUpdateDateTime" runat="server" CssClass="ColTwo" />
                    </div>
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CommandName="Update" OnClick="btnUpdate_Click" />
                    <asp:Button ID="btnResetPassword" runat="server" Text="Reset Password" OnClick="btnResetPassword_Click" />
                </div>
            </fieldset>
        </div>
        <div class="RegistrationForm" id="DivIP" runat="server">
            <fieldset class="ValSummary">
                <asp:ValidationSummary ID="VsIPAddress" HeaderText="Please correct the following errors:"
                    runat="server" ValidationGroup="IPAddress" />
            </fieldset>
            <fieldset>
                <legend>Edit IP Addresses</legend>
                <div>
                    <div>
                        <label>
                            &nbsp;IP Address Range:</label>
                        <asp:TextBox ID="TbIPOctet1" runat="server" Width="20px" MaxLength="3" ValidationGroup="IPAddress"></asp:TextBox>.
                        <asp:RequiredFieldValidator ID="RfvIPOctet1" runat="server" ControlToValidate="TbIPOctet1"
                            ErrorMessage="All IP Address fields are required." ValidationGroup="IPAddress"
                            Display="dynamic">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RevIPOctet1" runat="server" ControlToValidate="TbIPOctet1"
                            ErrorMessage="IP Address must be a valid integer." ValidationExpression="^\d{1,3}$"
                            Display="dynamic" ValidationGroup="IPAddress">*</asp:RegularExpressionValidator>
                        <asp:TextBox ID="TbIPOctet2" runat="server" Width="20px" MaxLength="3" ValidationGroup="IPAddress" />.
                        <asp:RequiredFieldValidator ID="RfvIPOctet2" Display="dynamic" runat="server" ControlToValidate="TbIPOctet2"
                            ErrorMessage="All IP Address fields are required." ValidationGroup="IPAddress">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Display="dynamic"
                            runat="server" ControlToValidate="TbIPOctet2" ErrorMessage="IP Address must be a valid integer."
                            ValidationExpression="^\d{1,3}$" ValidationGroup="IPAddress">*</asp:RegularExpressionValidator>
                        <asp:TextBox ID="TbIPOctet3" runat="server" Width="20px" MaxLength="3" ValidationGroup="IPAddress" /> - 
                        <asp:RequiredFieldValidator ID="RfvIPOctet3" runat="server" Display="dynamic" ControlToValidate="TbIPOctet3"
                            ErrorMessage="All IP Address fields are required." ValidationGroup="IPAddress">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Display="dynamic"
                            runat="server" ControlToValidate="TbIPOctet3" ErrorMessage="IP Address must be a valid integer."
                            ValidationExpression="^\d{1,3}$" ValidationGroup="IPAddress">*</asp:RegularExpressionValidator>
                        <asp:TextBox ID="TbIPOctet3End" runat="server" Width="20px" MaxLength="3" ValidationGroup="IPAddress" />.
                        <asp:RequiredFieldValidator ID="RfvIPOctet3End" runat="server" Display="dynamic" ControlToValidate="TbIPOctet3End"
                            ErrorMessage="All IP Address fields are required." ValidationGroup="IPAddress">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RevIPOctet3End" Display="dynamic"
                            runat="server" ControlToValidate="TbIPOctet3End" ErrorMessage="IP Address must be a valid integer."
                            ValidationExpression="^\d{1,3}$" ValidationGroup="IPAddress">*</asp:RegularExpressionValidator>
                        <asp:CompareValidator ID="CvIPOctet3End" runat="server" ValidationGroup="IPAddress" ErrorMessage="Last octet must be greater than beginning address" 
                            ControlToValidate="TbIPOctet3" ControlToCompare="TbIPOctet3End" Type="integer" Operator="lessthanequal">*</asp:CompareValidator>
                        <asp:TextBox ID="TbIPOctet4" runat="server" Width="20px" MaxLength="3" ValidationGroup="IPAddress" /> -
                        <asp:RequiredFieldValidator ID="RfvIPOctet4" runat="server" Display="dynamic" ControlToValidate="TbIPOctet4"
                            ErrorMessage="All IP Address fields are required." ValidationGroup="IPAddress">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" Display="dynamic"
                            runat="server" ControlToValidate="TbIPOctet4" ErrorMessage="IP Address must be a valid integer."
                            ValidationExpression="^\d{1,3}$" ValidationGroup="IPAddress">*</asp:RegularExpressionValidator>
                        <asp:TextBox ID="TbIPOctet4End" runat="server" Width="20px" MaxLength="3" ValidationGroup="IPAddress" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="dynamic" ControlToValidate="TbIPOctet4End" ErrorMessage="All IP Address fields are required." ValidationGroup="IPAddress">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" Display="dynamic"
                            runat="server" ControlToValidate="TbIPOctet4End" ErrorMessage="IP Address must be a valid integer."
                            ValidationExpression="^\d{1,3}$" ValidationGroup="IPAddress">*</asp:RegularExpressionValidator>
                        <asp:CompareValidator ID="CompValLastOctet" runat="server" ValidationGroup="IPAddress" ErrorMessage="Last octet must be greater than beginning address" 
                            ControlToValidate="TbIPOctet4" ControlToCompare="TbIPOctet4End" Type="integer" Operator="lessthanequal">*</asp:CompareValidator>
                    </div>
                    <div>
                        <label>
                        </label>
                        <asp:Button ID="BtnAddIP" runat="server" Text="Add IP" ValidationGroup="IPAddress"
                            OnClick="BtnAddIP_Click" />
                    </div>
                </div>
                <div>
                    <asp:GridView ID="GvIPAddresses" DataKeyNames="UserIPId" runat="server" AutoGenerateColumns="False"
                        OnRowDeleting="GvIPAddresses_RowDeleting">
                        <HeaderStyle CssClass="HdrRow" />
                        <AlternatingRowStyle CssClass="AltRow" />
                        <Columns>
                            <asp:BoundField DataField="UserIPId" Visible="False" />
                            <asp:BoundField DataField="BeginIP" HeaderText="Begin IP Address" />
                            <asp:BoundField DataField="EndIP" HeaderText="End IP Address" />
                            <asp:CommandField DeleteImageUrl="~/images/remove.gif" HeaderText="Remove" ButtonType="Image"
                                ShowDeleteButton="True" />
                        </Columns>
                    </asp:GridView>
                </div>
            </fieldset>
        </div>
    </div>
</asp:Content>
