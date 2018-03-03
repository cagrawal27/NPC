<%@ Page Language="C#" MasterPageFile="~/SiteMasters/Secure.master" AutoEventWireup="true" CodeFile="ManageVolumes.aspx.cs" Inherits="us.naturalproduct.web.ManageVolumes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageContent" Runat="Server">
    <h1>Manage Volumes</h1>
    <div id="BodyContent">
        <div class="RegistrationForm">
            <fieldset>
                <legend>Select a Volume</legend>
                <div>
                    <asp:GridView ID="gvVolumes" runat="server" AutoGenerateColumns="False" DataKeyNames="VolumeId" DataSourceID="sdsVolumes">
                        <HeaderStyle CssClass="HdrRow" />
                        <AlternatingRowStyle CssClass="AltRow" />  
                        <Columns>
                            <asp:CommandField ShowEditButton="True" />
                            <asp:BoundField DataField="VolumeId" HeaderText="VolumeId" InsertVisible="False"
                                ReadOnly="True" SortExpression="VolumeId" Visible="False" />
                            <asp:BoundField DataField="VolumeName" HeaderText="VolumeName" SortExpression="VolumeName" NullDisplayText="Volume Name is required." />
                            <asp:BoundField DataField="VolumeYear" HeaderText="VolumeYear" SortExpression="VolumeYear" NullDisplayText="Volume Year is required." />
                            <asp:CheckBoxField DataField="Active" HeaderText="Active" SortExpression="Active" />
                            <asp:BoundField DataField="Issues" HeaderText="Issues" ReadOnly="True" SortExpression="Issues" />
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="sdsVolumes" runat="server" ConnectionString="<%$ ConnectionStrings:NPC %>"
                        SelectCommand="spGetAllVolumesDetailed" SelectCommandType="StoredProcedure" UpdateCommand="spUpdateVolume"
                        UpdateCommandType="StoredProcedure">
                        <UpdateParameters>
                            <asp:Parameter Name="VolumeId" Type="Int32" />
                            <asp:Parameter Name="VolumeName" Type="String" />
                            <asp:Parameter Name="VolumeYear" Type="String" />
                            <asp:Parameter Name="Active" Type="Boolean" />
                            <asp:SessionParameter SessionField="UserId" Name="UpdateUserId" DefaultValue="1" Type="Int32" />                           
                        </UpdateParameters>
                    </asp:SqlDataSource>
                </div>
            </fieldset>      
        </div>
    </div>
</asp:Content>