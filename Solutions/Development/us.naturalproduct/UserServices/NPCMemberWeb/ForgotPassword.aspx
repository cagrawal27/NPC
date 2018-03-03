<%@ Page Language="C#" EnableViewState="true" MasterPageFile="~/SiteMasters/Unsecure.master" AutoEventWireup="true" CodeFile="ForgotPassword.aspx.cs" Inherits="us.naturalproduct.web.ForgotPassword" Title="Forget Your Password?" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageContent" Runat="Server">
    <h1>Forget Your Password?</h1>
    <div id="BodyContent">
        <div class="GreyPanelWide">
        <div class="RegistrationForm">
            <fieldset class="ValSummary">
                <asp:Literal ID="LitStatus" runat="server" EnableViewState="false" />                          
            </fieldset><br />        
            <fieldset>
                <legend>Step 1 - Enter Your Email Address</legend>
                <div class="help">
                    <h4>Help</h4>
                    This is the email address you normally use to login.
                </div>                
                <div>
                    <label>Email:</label>
                    <asp:TextBox ID="tbEmail" runat="server" ValidationGroup="Email" />
                    <asp:requiredfieldvalidator ID="RfvEmail" ValidationGroup="Email"
                        ControlToValidate="tbEmail" runat="server"
                        errormessage="Email is required.">*</asp:requiredfieldvalidator>                                    
                    <asp:regularexpressionvalidator ID="RegexValEmail" runat="server" 
                        ControlToValidate="tbEmail" ValidationGroup="Email"
                        ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                        errormessage="An email address in the format user@domain.com is required." 
                        Display="Static">*</asp:regularexpressionvalidator>  
                </div>
            </fieldset>       
            <fieldset id="FldRecoveryInfo" runat="server" visible="false">
                <legend>Step 2 - Enter Your Password Recovery Information</legend>
                <div class="help">
                    <h4>Help</h4>
                    The password question list to the left shows the two questions you selected at the time
                    of registration.  Choose one of the questions and type the password answer exactly
                    as you typed when registering.  Please note that the password answer is case sensitive.<br /><br />
                    <b>What if I can't remember my password recovery information?</b>
                       Please contact Customer Service at
                       <a href="mailto:customerservice@naturalproduct.us">customerservice@naturalproduct.us</a> and request a password reset.  Your new password will be emailed to the email address specified at the
                        time of registration.
                </div>                 
                <div>
                    <label>Password Question:</label>
                    <asp:DropDownList ID="ddlPasswordQuestions" runat="server" ValidationGroup="RecoveryInfo">
                    </asp:DropDownList>
                    <asp:requiredfieldvalidator ID="RfvPasswordQuestion" ValidationGroup="RecoveryInfo"
                        ControlToValidate="ddlPasswordQuestions" runat="server"
                        errormessage="Password question is required.">*</asp:requiredfieldvalidator>                                    
                </div>
                <div>
                    <label>Password Answer:</label>
                    <asp:TextBox ID="tbPasswordAnswer" runat="server" ValidationGroup="RecoveryInfo" />
                    <asp:requiredfieldvalidator ID="RfvPasswordAnswer" ValidationGroup="RecoveryInfo"
                        ControlToValidate="tbPasswordAnswer" runat="server"
                        errormessage="Password answer is required.">*</asp:requiredfieldvalidator>                                     
                </div>                
            </fieldset>         
            <fieldset id="FldPassword" runat="server" visible="false">
                <legend>Step 3 - Change Your Password</legend>
                <div class="help">
                    <h4>Help</h4>
                    Please enter a new password that is between 8 and 15 characters. Your password must contain at 
                    minimum one of each character: 
                    <ul>
                        <li>An uppercase character</li><li>A lowercase character</li><li>A digit</li><li>Special character {- + ? * $ ^ . | ! @ # % & _ = ,}</li></ul>
                </div>                
                <div>
                    <label>New Password:</label>
                    <asp:TextBox ID="tbPassword" runat="server" MaxLength="15" ValidationGroup="Password" TextMode="Password" />
                    <asp:requiredfieldvalidator ID="RfvPassword" runat="server" 
                        ControlToValidate="tbPassword" ValidationGroup="Password"
                        errormessage="A password is required.">*</asp:requiredfieldvalidator>
                    <asp:CustomValidator id="CvPassword" runat="server"
                            ControlToValidate="tbPassword" ValidationGroup="Password"
                            OnServerValidate="CvPassword_OnValidate"
                            ErrorMessage="Please choose a valid password."
                            Display="Static">*</asp:CustomValidator>      
                </div>
                <div>
                    <label>Re-enter Password:</label>
                    <asp:TextBox ID="tbPasswordRepeat" runat="server" MaxLength="15" TextMode="Password" ValidationGroup="Password"/>
                    <asp:requiredfieldvalidator ID="RfvPasswordRepeat" runat="server" 
                        ControlToValidate="tbPasswordRepeat" ValidationGroup="Password"
                        errormessage="Please re-enter the password.">*</asp:requiredfieldvalidator>
                    <asp:CompareValidator id="CompVPassword" runat="server" ValidationGroup="Password"
                        ControlToValidate="tbPasswordRepeat" ControlToCompare="tbPassword" 
                        Operation="Equal" Type="String" 
                        ErrorMessage="Password does not match.">*</asp:CompareValidator>
                </div>              
            </fieldset>
            <fieldset class="ValSummary">
                <asp:ValidationSummary ID="VsEmail" ValidationGroup="Email" HeaderText="Please correct the following errors:" runat="server" />                                    
                <asp:ValidationSummary ID="VsRecoveryInfo" ValidationGroup="RecoveryInfo" HeaderText="Please correct the following errors:" runat="server" />                                    
                <asp:ValidationSummary ID="VsPassword" ValidationGroup="Password" HeaderText="Please correct the following errors:" runat="server" />                     
                <asp:Label ID="lblStatus" runat="server" EnableViewState="false" />                          
            </fieldset><br />              
            <asp:Button ID="btnSubmitEmail" Text="Submit Email" runat="server" ValidationGroup="Email" OnClick="btnSubmitEmail_Click" />
            <asp:Button ID="btnSubmitAnswer" Text="Submit Answer" runat="server" ValidationGroup="RecoveryInfo" Visible="false" OnClick="btnSubmitAnswer_Click" />            
            <asp:Button ID="btnChangePassword" Text="Change Password" runat="server" ValidationGroup="Password" Visible="false" OnClick="btnChangePassword_Click" />            
            <asp:Button ID="btnReset" Text="Reset" runat="server" OnClick="btnReset_Click" />            
            <asp:Button ID="btnGoToLogin" runat="server" Text="Go To Login" OnClick="btnGoToLogin_Click" />
        </div>
    </div>
    </div>
</asp:Content>

