<%@ Page Language="C#" MasterPageFile="~/SiteMasters/Secure.master" AutoEventWireup="true" CodeFile="ChangeSecurity.aspx.cs" Inherits="us.naturalproduct.web.ChangeSecurity" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageContent" Runat="Server">
    <h1>Change Security Information</h1>
    <div id="BodyContent">
        <div class="GreyPanelWide">
            <div class="RegistrationForm">  
                <fieldset class="ValSummary">
                    <asp:Literal ID="LitStatus" runat="server" EnableViewState="false" />                          
                </fieldset><br />
                <fieldset>
                    <legend>Current Password Recovery Information</legend>
                    <div>
                        <label>Secret Question 1:</label>
                        <asp:Label ID="LblSecretQstn1" CssClass="ColTwo" runat="server" Text="Secret Question 1" />
                    </div>
                    <div>
                        <label>Secret Answer 1:</label>
                        <span class="ColTwo">****</span>
                    </div>
                    <div>
                        <label>Secret Question 2:</label>
                        <asp:Label ID="LblSecretQstn2" CssClass="ColTwo" runat="server" Text="Secret Question 1" />
                    </div>
                    <div>
                        <label>Secret Answer 2:</label>
                        <span class="ColTwo">****</span>
                    </div>
                </fieldset>           
                <fieldset>
                    <legend>Change Password Recovery Information</legend>
                    <div class="help">
                        <h4>Help</h4>
                        This information will be used to ask you questions in case you forget your password.
                        Please be sure to choose simple one or two word answers that are easy to remember.
                        If you forget your password, you must answer these questions so that you can reset
                        your password.  Your password answer is case sensitive.
                    </div>
                    <div>
                        <label>Secret Question 1:</label>
                        <asp:DropDownList ID="ddlPassQuestion1" runat="server" ValidationGroup="RecoveryInfo">
                            <asp:ListItem Selected="true" Text="-- Select --" Value="0" />
                            <asp:ListItem Text="Mother's Maiden Name" Value="1" />
                            <asp:ListItem Text="City of Birth" Value="2" />
                            <asp:ListItem Text="Name of Favorite Teacher/Professor" Value="3" />
                            <asp:ListItem Text="Name of First School" Value="4" />
                        </asp:DropDownList>
                        <asp:requiredfieldvalidator ID="RfvPassQues1" runat="server" 
                            ControlToValidate="ddlPassQuestion1" ValidationGroup="RecoveryInfo"
                            errormessage="Please choose Security Question 1." 
                            InitialValue="0">*</asp:requiredfieldvalidator>
                    </div>
                    <div>
                        <label>Answer 1:</label>
                        <asp:TextBox ID="tbPassAnswer1" MaxLength="50" runat="server" ValidationGroup="RecoveryInfo" />
                        <asp:requiredfieldvalidator ID="RfvPassAns1" ControlToValidate="tbPassAnswer1" runat="server" ValidationGroup="RecoveryInfo"
                            errormessage="Please enter a password answer.">*</asp:requiredfieldvalidator>
                    </div>
                    <div>
                        <label>Secret Question 2:</label>
                        <asp:DropDownList ID="ddlPassQuestion2" runat="server" ValidationGroup="RecoveryInfo">
                            <asp:ListItem Selected="true" Text="-- Select --" Value="0" />
                            <asp:ListItem Text="Mother's Maiden Name" Value="1" />
                            <asp:ListItem Text="City of Birth" Value="2" />
                            <asp:ListItem Text="Name of Favorite Teacher/Professor" Value="3" />
                            <asp:ListItem Text="Name of First School" Value="4" />
                        </asp:DropDownList>
                        <asp:requiredfieldvalidator ID="RfvPassQues2" runat="server" 
                            ControlToValidate="ddlPassQuestion2" ValidationGroup="RecoveryInfo"
                            errormessage="Please choose Security Question 2." 
                            InitialValue="0">*</asp:requiredfieldvalidator>
                        <asp:CompareValidator id="CvPasswordQuestion" 
                            ControlToValidate="ddlPassQuestion2" ControlToCompare="ddlPassQuestion1" 
                            Operator="NotEqual" Type="Integer" runat="server" ValidationGroup="RecoveryInfo"
                            ErrorMessage="Please choose a different password question.">*</asp:CompareValidator>
                    </div>
                    <div>
                        <label>Answer 2:</label>
                        <asp:TextBox ID="tbPassAnswer2" MaxLength="50" runat="server" ValidationGroup="RecoveryInfo" />
                        <asp:requiredfieldvalidator ID="RfvPassAns2" ControlToValidate="tbPassAnswer2" runat="server" ValidationGroup="RecoveryInfo"
                            errormessage="Please enter a password answer.">*</asp:requiredfieldvalidator>
                    </div>
                </fieldset>
                <fieldset class="ValSummary">
                    <asp:ValidationSummary ID="VsRecoveryInfo" ValidationGroup="RecoveryInfo" HeaderText="Please correct the following errors:" runat="server" />                     
                </fieldset><br />              
                <asp:Button ID="BtnChangeRecoveryInfo" Text="Change Recovery Info" runat="server" ValidationGroup="RecoveryInfo" OnClick="BtnChangeRecoveryInfo_Click" />                        
                <fieldset>
                    <legend>Change Your Password</legend>
                    <div class="help">
                        <h4>Help</h4>
                        Please enter a new password that is between 8 and 15 characters. Your password must contain at 
                        minimum one of each character: 
                        <ul>
                            <li>An uppercase character</li><li>A lowercase character</li><li>A digit</li><li>Special character {- + ? * $ ^ . | ! @ # % & _ = ,}</li></ul>
                    </div>
                    <div>
                        <label>Current Password:</label>
                        <span class="ColTwo">****</span>
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
                    <asp:ValidationSummary ID="VsPassword" ValidationGroup="Password" HeaderText="Please correct the following errors:" runat="server" />                     
                </fieldset><br />              
                <asp:Button ID="btnChangePassword" Text="Change Password" runat="server" ValidationGroup="Password" OnClick="btnChangePassword_Click" />            
            </div>
        </div>
    </div>
</asp:Content>

