using System;
using System.Configuration;
using System.Web.UI;
using us.naturalproduct.Common;

namespace us.naturalproduct.web.Masters
{
    public partial class Secure : MasterPage
    {
        public string PageTitle
        {
            get { return Page.Title; }

            set
            {
                Page.Title = string.Format("{0} - {1}",
                                           ConfigurationManager.AppSettings["SiteTitle"],
                                           value);
            }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.User.Identity.IsAuthenticated)
            {
                ucMenu.Visible = true;

                //Init user data
                InitializeUserInfo();

                VerifyAccountStatus();
            }
            else
            {
                ucMenu.Visible = false;

                UserInfo.Abandon();
            }
        }

        private void InitializeUserInfo()
        {
            if (!UserInfo.IsInitialized())
                UserInfo.Init(Page.User);
        }


        private void VerifyAccountStatus()
        {
            if (UserInfo.IsAccountStale())
            {
                string pageChangeSecurity = Pages.ChangeSecurity;

                //remove the ~
                pageChangeSecurity = pageChangeSecurity.Replace("~", "");

                //If navigating to any page other than the change security page
                if (Page.Request.Path.IndexOf(pageChangeSecurity) == -1)
                {
                    Response.Redirect(string.Format("{0}?forceChange=true", Pages.ChangeSecurity));
                } 
            }
        }

    }
}