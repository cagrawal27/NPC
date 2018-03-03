<%@ Page Language="C#" EnableViewState="true" MasterPageFile="~/SiteMasters/Secure.master" AutoEventWireup="true" CodeFile="ManageUsers.aspx.cs" Inherits="us.naturalproduct.web.ManageUsers" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageContent" Runat="Server">
    <h1>Manage Users</h1>
    <div id="BodyContent">
        <div class="RegistrationForm">
            <fieldset>
                <legend>Search Criteria</legend>
                <div>
                    <label>First Name:</label>
                    <asp:TextBox ID="tbFirstname" runat="server" d/>
                </div>            
                <div>
                    <label>Last Name:</label>
                    <asp:TextBox ID="tbLastname" runat="server" />
                </div>            
                <div>
                    <label>Email:</label>
                    <asp:TextBox ID="tbEmail" runat="server" />
                </div>
                <div>
                    <label>Account Type:</label>
                    <asp:DropDownList ID="ddlAccountType" runat="server" />
                </div>
                <div>
                    <label>Account Status:</label>
                    <asp:DropDownList ID="ddlAccountStatus" runat="server" />
                </div>                
                    <asp:Button ID="btnFilter" runat="server" Text="Search" CommandName="Select" OnClick="btnFilter_Click" />
            </fieldset>
            <fieldset>
                <legend>Select an User to Edit</legend>
                <div>
                    <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="False" DataKeyNames="UserId" AllowSorting="True" AllowPaging="True" >
                        <HeaderStyle CssClass="HdrRow" />
                        <AlternatingRowStyle CssClass="AltRow" />  
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HyperLink ID="BtnEdit" runat="server"  Text="Edit" NavigateUrl='<%# Eval("UserId", "~\\Secure\\Admin\\EditUser.aspx?UserId={0}") %>'  />
                                </ItemTemplate>
                            </asp:TemplateField>                             
                            <asp:BoundField DataField="UserId" HeaderText="UserId" InsertVisible="False" ReadOnly="True"
                                SortExpression="UserId" Visible="False" />
                            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" ReadOnly="True" />
                            <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName" />
                            <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName" />
                            <asp:BoundField DataField="MiddleInitial" HeaderText="Middle Initial" SortExpression="MiddleInitial" />
                            <asp:BoundField DataField="AcctTypeDescription" HeaderText="Account Type" ReadOnly="True"
                                SortExpression="AcctTypeDescription" />
                            <asp:BoundField DataField="AcctStatusDescription" HeaderText="Account Status" ReadOnly="True" SortExpression="AcctStatusDescription" />
                            <asp:CheckBoxField DataField="Active" HeaderText="Active" SortExpression="Active" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LnkDelete" Text="Delete" runat="server" CommandName="Delete" CommandArgument='<%# Eval("UserId") %>' 
                                    OnClientClick="if (confirm('Are you sure you want to permanently delete a user from the system?')) return true; else return false;" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>               
                </div>
            </fieldset> 
        </div>
    </div>
</asp:Content>

