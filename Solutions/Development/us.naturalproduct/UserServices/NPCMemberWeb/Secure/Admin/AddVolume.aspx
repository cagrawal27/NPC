<%@ Page Language="C#" MasterPageFile="~/SiteMasters/Secure.master" AutoEventWireup="true" CodeFile="AddVolume.aspx.cs" Inherits="us.naturalproduct.web.AddVolume" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageContent" Runat="Server">
    <h1>Add Volume</h1>
    <div id="BodyContent">
        <div class="RegistrationForm">
            <fieldset class="ValSummary">
                <asp:Label ID="LblStatus" runat="server" EnableViewState="false" />
            </fieldset>           
            <fieldset>
                <legend>Enter Volume Information</legend>
                <div>
                    <label>Volume Name:</label>
                    <asp:TextBox ID="tbVolumeName" runat="server" />
                    <asp:requiredfieldvalidator ID="rfvVolumeName" runat="server"
                        ControlToValidate="tbVolumeName"
                        errormessage="Volume name is required.">*</asp:requiredfieldvalidator>
                </div>
                <div>
                    <label>Volume Year:</label>
                    <asp:TextBox ID="tbVolumeYear" runat="server" />
                    <asp:requiredfieldvalidator ID="rfvVolumeYear" runat="server"
                        ControlToValidate="tbVolumeYear"
                        errormessage="Volume year is required.">*</asp:requiredfieldvalidator>
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
            <asp:Button ID="btnAddVolume" runat="server" Text="Add Volume" OnClick="btnAddVolume_Click" />
            <asp:Button ID="btnReset" Text="Reset" runat="server" OnClick="btnReset_Click" />           
        </div>
    </div>
</asp:Content>