using System;
using System.Web.UI;
using us.naturalproduct.web.Masters;

namespace us.naturalproduct.web
{
    public partial class Subscription : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((Secure) Master).PageTitle = "Manage Subscription";
        }
    }
}