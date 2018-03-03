<%@ Page Language="C#" EnableViewState="true" MasterPageFile="~/SiteMasters/Secure.master" AutoEventWireup="true" CodeFile="ManageSubscriptions.aspx.cs" Inherits="us.naturalproduct.web.ManageSubscriptions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageContent" Runat="Server">
    <h1>Manage Subscriptions</h1>
    <div id="BodyContent">
        <div class="RegistrationForm">
            <fieldset>
                <legend>Select a User</legend>
                <div>
                    <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="False" DataKeyNames="UserId" DataSourceID="sdsUsers" AllowSorting="True" AllowPaging="True">
                        <HeaderStyle CssClass="HdrRow" />
                        <AlternatingRowStyle CssClass="AltRow" />  
                        <Columns>
                            <asp:TemplateField HeaderText="Subscriptions">
                                <ItemTemplate>
                                    <asp:HyperLink ID="BtnEditSubscriptions" runat="server"  Text="Edit" NavigateUrl='<%# Eval("UserId", "~\\Secure\\Admin\\EditSubscriptions.aspx?UserId={0}") %>'  />
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
                            
                        </Columns>
                        <SelectedRowStyle BackColor="LightSkyBlue" />
                        <EditRowStyle BackColor="LightSkyBlue" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="sdsUsers" runat="server" ConnectionString="<%$ ConnectionStrings:NPC %>"
                        SelectCommand="spAdminGetAllUsers" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                </div>
            </fieldset>          
        </div>
    </div>
</asp:Content>