using System;
using System.Web.UI;
using us.naturalproduct.Common;

namespace us.naturalproduct.web
{
    public partial class Default : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRedirectToLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect(Pages.Login, false);
        }

        protected void btnRedirectToRegistration_Click(object sender, EventArgs e)
        {
            Response.Redirect(Pages.Registration, false);
        }

        protected void btnRedirectToInstLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect(Pages.InstitutionalLogin, false);
        }
    }
}