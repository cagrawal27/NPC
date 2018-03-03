using System;
using us.naturalproduct.web.Masters;

namespace us.naturalproduct.web
{
    public partial class Logout : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((Unsecure) Master).PageTitle = "Logged out of the system";
        }
    }
}