<%@ Page Language="C#" MasterPageFile="~/SiteMasters/Unsecure.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="us.naturalproduct.web.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContent" runat="Server">
    <h1>Welcome to NPC Member Site</h1>
    <table id="TblContent">
        <tr>
            <td valign="top">
                <div>
                    <h2>Existing Members</h2>
                    <div class="GreyPanel">
                        <div class="DivForm">
                            <fieldset>
                                <legend>Personal Accounts</legend>
                                <asp:Button ID="btnRedirectToLogin" runat="server" Text="Click Here" OnClick="btnRedirectToLogin_Click" />
                            </fieldset>
                        </div>
                    </div>
                    <div class="GreyPanel">
                        <div class="DivForm">
                            <fieldset>
                                <legend>Institutional Accounts</legend>
                                <asp:Button ID="btnRedirectToInstLogin" runat="server" Text="Click Here" OnClick="btnRedirectToInstLogin_Click" />
                            </fieldset>
                        </div>
                    </div>
                </div>
            </td>
            <td>              
            </td>
            <td valign="top">
                <div>
                    <h2>New Members</h2>
                    <div class="GreyPanel">
                        <div class="DivForm">
                            <fieldset>
                                <legend>Click Below to Register For Free</legend>
                                <asp:Button ID="btnRedirectToRegistration" runat="server" Text="Begin Here" OnClick="btnRedirectToRegistration_Click" />
                            </fieldset>
                        </div>
                    </div>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
