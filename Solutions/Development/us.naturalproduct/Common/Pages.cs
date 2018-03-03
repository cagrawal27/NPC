using System;
using System.Collections.Generic;
using System.Text;

namespace us.naturalproduct.Common
{
    public static class Pages
    {
        //Secure Pages
        public const string Home = "~/Secure/Home.aspx";
        public const string Login = "~/Secure/Login.aspx";
        public const string JournalArchive = "~/Secure/JournalArchive.aspx";
        public const string IssueArchive = "~/Secure/IssueArchive.aspx";
        public const string Issue = "~/Secure/Issue.aspx";
        public const string Subscription = "~/Secure/Subscription.aspx";
        public const string CustomerService = "~/Secure/CustomerService.aspx";
        public const string AccountProfile = "~/Secure/AccountProfile.aspx";
        public const string ChangeSecurity = "~/Secure/ChangeSecurity.aspx";
        
        //Admin pages
        public const string ManageUsers = "~/Secure/Admin/ManageUsers.aspx";
        
        //Unsecure Pages
        public const string Registration = "~/Registration.aspx";
        public const string Registration_Status = "~/RegistrationStatus.aspx";
        public const string Logout = "Logout.aspx";
        public const string InstitutionalLogin = "~/InstitutionalLogin.aspx";
        public const string ForgotPassword = "~/ForgotPassword.aspx";

    }
}
