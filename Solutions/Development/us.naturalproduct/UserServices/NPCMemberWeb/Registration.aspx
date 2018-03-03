<%@ Page Language="C#" EnableViewState="true" MasterPageFile="~/SiteMasters/Unsecure.master" AutoEventWireup="true" CodeFile="Registration.aspx.cs" Inherits="us.naturalproduct.web.Registration" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContent" runat="Server">
    <h1>New Member Registration</h1>
    <div class="GreyPanelWide">
        <div class="RegistrationForm">
            <fieldset>
                <legend>Create Your NPC Login</legend>
                <div class="help">
                    <h4>Help</h4>
                    Please enter a valid email address in the format user@domain.com.  You will use this email
                    address to log in and also to receive membership related email from NPC.<br /><br />
                    Please enter a password that is between 8 and 15 characters. Your password must contain at 
                    minimum one of each character: 
                    <ul>
                        <li>An uppercase character</li>
                        <li>A lowercase character</li>
                        <li>A digit</li>
                        <li>Special character {- + ? * $ ^ . | ! @ # % & _ = ,}</li>
                    </ul>
                </div>
                <div>
                    <label>Email Address:</label>
                    <asp:TextBox ID="tbEmail" runat="server" MaxLength="50" />
                    <asp:requiredfieldvalidator ID="RfvEmail" runat="server"
                        ControlToValidate="tbEmail" Display="Static"
                        errormessage="An Email Address is required.">*</asp:requiredfieldvalidator>                         
                    <asp:regularexpressionvalidator ID="RegexValEmail" runat="server" 
                        ControlToValidate="tbEmail" 
                        ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                        errormessage="An email address in the format user@domain.com is required." 
                        Display="Static">*</asp:regularexpressionvalidator>  
                </div>
                <div>
                    <label>Password:</label>
                    <asp:TextBox ID="tbPassword" runat="server" MaxLength="15" TextMode="Password" />
                    <asp:requiredfieldvalidator ID="RfvPassword" runat="server" 
                        ControlToValidate="tbPassword" 
                        errormessage="A password is required.">*</asp:requiredfieldvalidator>
                    <asp:CustomValidator id="CvPassword" runat="server"
                            ControlToValidate="tbPassword"
                            OnServerValidate="CvPassword_OnValidate"
                            ErrorMessage="Please choose a valid password."
                            Display="Static">*</asp:CustomValidator>      
                </div>
                <div>
                    <label>Re-enter Password:</label>
                    <asp:TextBox ID="tbPasswordRepeat" runat="server" MaxLength="15" TextMode="Password" />
                    <asp:requiredfieldvalidator ID="RfvPasswordRepeat" runat="server" 
                        ControlToValidate="tbPasswordRepeat" 
                        errormessage="Please re-enter the password.">*</asp:requiredfieldvalidator>
                    <asp:CompareValidator id="CompVPassword" runat="server" 
                        ControlToValidate="tbPasswordRepeat" ControlToCompare="tbPassword" 
                        Operation="Equal" Type="String" 
                        ErrorMessage="Password does not match.">*</asp:CompareValidator>
                </div>
            </fieldset>
            <fieldset>
                <legend>Enter Password Recovery Information</legend>
                <div class="help">
                    <h4>Help</h4>
                    This information will be used to ask you questions in case you forget your password.
                    Please be sure to choose simple one or two word answers that are easy to remember.
                    If you forget your password, you must answer these questions so that you can reset
                    your password.
                </div>
                <div>
                    <label>Secret Question 1:</label>
                    <asp:DropDownList ID="ddlPassQuestion1" runat="server">
                        <asp:ListItem Selected="true" Text="-- Select --" Value="0" />
                        <asp:ListItem Text="Mother's Maiden Name" Value="1" />
                        <asp:ListItem Text="City of Birth" Value="2" />
                        <asp:ListItem Text="Name of Favorite Teacher/Professor" Value="3" />
                        <asp:ListItem Text="Name of First School" Value="4" />
                    </asp:DropDownList>
                    <asp:requiredfieldvalidator ID="RfvPassQues1" runat="server" 
                        ControlToValidate="ddlPassQuestion1" 
                        errormessage="Please choose Security Question 1." 
                        InitialValue="0">*</asp:requiredfieldvalidator>
                </div>
                <div>
                    <label>Answer 1:</label>
                    <asp:TextBox ID="tbPassAnswer1" MaxLength="50" runat="server" />
                    <asp:requiredfieldvalidator ID="RfvPassAns1" ControlToValidate="tbPassAnswer1" runat="server" 
                        errormessage="Please enter a password answer.">*</asp:requiredfieldvalidator>
                </div>
                <div>
                    <label>Secret Question 2:</label>
                    <asp:DropDownList ID="ddlPassQuestion2" runat="server">
                        <asp:ListItem Selected="true" Text="-- Select --" Value="0" />
                        <asp:ListItem Text="Mother's Maiden Name" Value="1" />
                        <asp:ListItem Text="City of Birth" Value="2" />
                        <asp:ListItem Text="Name of Favorite Teacher/Professor" Value="3" />
                        <asp:ListItem Text="Name of First School" Value="4" />
                    </asp:DropDownList>
                    <asp:requiredfieldvalidator ID="RfvPassQues2" runat="server" 
                        ControlToValidate="ddlPassQuestion2"  
                        errormessage="Please choose Security Question 2." 
                        InitialValue="0">*</asp:requiredfieldvalidator>
                    <asp:CompareValidator id="CvPasswordQuestion" 
                        ControlToValidate="ddlPassQuestion2" ControlToCompare="ddlPassQuestion1" 
                        Operator="NotEqual" Type="Integer" runat="server"
                        ErrorMessage="Please choose a different password question.">*</asp:CompareValidator>
                </div>
                <div>
                    <label>Answer 2:</label>
                    <asp:TextBox ID="tbPassAnswer2" MaxLength="50" runat="server" />
                    <asp:requiredfieldvalidator ID="RfvPassAns2" ControlToValidate="tbPassAnswer2" runat="server" 
                        errormessage="Please enter a password answer.">*</asp:requiredfieldvalidator>
                </div>
            </fieldset>
            <fieldset>
                <legend>Enter Contact Information</legend>
                <div class="help">
                    <h4>Help</h4>
                    Please enter your contact information.  This information will not be shared with anyone.                   
                </div>
                <div>
                    <label>Account Type:</label>
                    <asp:DropDownList ID="ddlAccountType" runat="server">
                        <asp:ListItem Selected="true" Text="-- Select --" Value="" />
                        <asp:ListItem Text="Personal" Value="1" />
                        <asp:ListItem Text="Institutional" Value="2" />
                    </asp:DropDownList>
                    <asp:requiredfieldvalidator ID="RfvAccountType" runat="server" 
                        ControlToValidate="ddlAccountType" 
                        errormessage="Please choose an account type.">*</asp:requiredfieldvalidator>
                </div>
                <div>
                    <label>First Name:</label>
                    <asp:TextBox ID="tbFirstName" runat="server" />
                    <asp:requiredfieldvalidator ID="RfvFirstName" runat="server" 
                        ControlToValidate="tbFirstName" 
                        errormessage="First Name is required.">*</asp:requiredfieldvalidator>
                </div>
                <div>
                    <label for="tbLastName">
                        Last Name/Surname:</label>
                    <asp:TextBox ID="tbLastName" runat="server" />
                    <asp:requiredfieldvalidator ID="RfvLastName" runat="server" 
                        ControlToValidate="tbLastName" 
                        errormessage="Last Name is required.">*</asp:requiredfieldvalidator>
                </div>
                <div>
                    <label for="tbMiddleInitial">
                        Middle Initial:</label>
                    <asp:TextBox ID="tbMiddleInitial" runat="server" />
                </div>
                <div>
                    <label for="tbAddressLine1">
                        Address Line 1:</label>
                    <asp:TextBox ID="tbAddressLine1" runat="server" />
                    <asp:requiredfieldvalidator ID="RfvAddress1" runat="server" 
                        ControlToValidate="tbAddressLine1" 
                        errormessage="Address Line 1 is required.">*</asp:requiredfieldvalidator>                   
                </div>
                <div>
                    <label for="tbAddressLine2">
                        Address Line 2:</label>
                    <asp:TextBox ID="tbAddressLine2" runat="server" />
                </div>
                <div>
                    <label for="tbCity">
                        City:</label>
                    <asp:TextBox ID="tbCity" runat="server" />
                    <asp:requiredfieldvalidator ID="RfvCity" runat="server" 
                        ControlToValidate="tbCity" 
                        errormessage="City is required.">*</asp:requiredfieldvalidator>                   
                </div>
                <div>
                    <label for="ddlStateProvince">
                        State/Province:</label>
                    <asp:DropDownList ID="ddlStateProvince" runat="server" />
                    <asp:TextBox ID="tbStateProvince" runat="server" />               
                </div>
                <div>
                    <label for="tbZipPostalCode">
                        Zip/Postal Code:</label>
                    <asp:TextBox ID="tbZipPostalCode" runat="server" />
                    <asp:requiredfieldvalidator ID="RfvZipPostalCode" runat="server" 
                        ControlToValidate="tbZipPostalCode" 
                        errormessage="Zip/Postal Code is required.">*</asp:requiredfieldvalidator>                   
                </div>
                <div>
                    <label for="ddlCountry">
                        Country:</label>
                    <asp:DropDownList ID="ddlCountry" runat="server" />
                    <asp:requiredfieldvalidator ID="RfvCountry" runat="server" 
                        ControlToValidate="tbZipPostalCode" 
                        errormessage="Country is required.">*</asp:requiredfieldvalidator>                 
                </div>
                <div>
                    <label for="tbPhone">
                        Phone:</label>
                    <asp:TextBox ID="tbPhone" runat="server" />
                    <asp:regularexpressionvalidator ID="RevPhone" runat="server" 
                        ControlToValidate="tbPhone" 
                        ValidationExpression="^(\d|-){10,20}$" 
                        errormessage="Phone number must be less than 20 characters, contain digits and the - character."                         Display="Static">*</asp:regularexpressionvalidator>                      
                </div>
                <div>
                    <label for="tbFax">
                        Fax:</label>
                    <asp:TextBox ID="tbFax" runat="server" />
                    <asp:regularexpressionvalidator ID="RevFax" runat="server" 
                        ControlToValidate="tbFax" 
                        ValidationExpression="^(\d|-){10,20}$" 
                        errormessage="Phone number must be less than 20 characters and only contain digits and the - character." 
                        Display="Static">*</asp:regularexpressionvalidator>                      
                </div>
            </fieldset>
<%--TODO: CAPTCHA CODE 
            <fieldset>
                <legend>Security Measure</legend>
                <div class="help">
                    <h4>
                        Help</h4>
                    Enter the characters exactly as they are shown in the image to the left. This will
                    further increase the security of your account and the NPC network.
                </div>
                <div>
                    <label for="tbSecurityImage">
                        <img src="images/secret.jpeg" /></label>
                    <asp:TextBox ID="tbSecurityImage" runat="server" />
                </div>
            </fieldset>
--%>                   
            <fieldset class="ValSummary">
                <asp:ValidationSummary ID="VsRegistration" HeaderText="Please correct the following errors:" runat="server" />            
                <asp:Label ID="lblErrors" runat="server" />            
            </fieldset><br />
            <asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_OnClick" />
            <asp:Button ID="btnReset" Text="Reset" runat="server" />
        </div>
    </div>
</asp:Content>
