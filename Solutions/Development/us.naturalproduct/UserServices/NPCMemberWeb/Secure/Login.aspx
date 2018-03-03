<%@ Page Language="c#" MasterPageFile="~/SiteMasters/Secure.master" Inherits="us.naturalproduct.web.Login" CodeFile="Login.aspx.cs" %>

<asp:Content ContentPlaceHolderID="PageContent" runat="server">
    <h1>NPC Member Login</h1>
    <div id="BodyContent">
        <div class="GreyPanel">
        <div class="LoginForm">
            <fieldset>
                <legend>Enter Your NPC Credentials</legend>
                <div>
                    <label>Email:</label>
                    <asp:TextBox ID="tbEmail" runat="server" />
                    <asp:requiredfieldvalidator ID="RfvEmail"  
                        ControlToValidate="tbEmail" runat="server"
                        errormessage="Email is required.">*</asp:requiredfieldvalidator>                                    
                    <asp:regularexpressionvalidator ID="RegexValEmail" runat="server" 
                        ControlToValidate="tbEmail" 
                        ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                        errormessage="An email address in the format user@domain.com is required." 
                        Display="Static">*</asp:regularexpressionvalidator>  
                </div>
                <div>
                    <label>Password:</label>
                    <asp:TextBox ID="tbPassword" runat="server" TextMode="password" Text="superMan.04" />
                    <asp:requiredfieldvalidator ID="RfvPassword"  
                        ControlToValidate="tbPassword" runat="server"
                        errormessage="Password is required.">*</asp:requiredfieldvalidator>                                    
                </div>
            </fieldset>
            <fieldset class="ValSummary">
                <asp:ValidationSummary ID="VsLogin" HeaderText="Please correct the following errors:" runat="server" />                     
                <asp:Label ID="lblErrors" runat="server" />                          
            </fieldset><br />
            <asp:Button ID="btnLogin" Text="Login" runat="server" />
            <asp:Button ID="btnReset" Text="Reset" runat="server" /><br /><br />
            <asp:HyperLink ID="lnkForgetPassword" NavigateUrl="~/ForgotPassword.aspx" runat="server" Text="Forget your password?" />
        </div>
    </div>
    </div>
</asp:Content>
