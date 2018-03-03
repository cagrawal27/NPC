using System;
using us.naturalproduct.web.Masters;

namespace us.naturalproduct.web
{
    public partial class ManageVolumes : BaseAdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Set title
            ((Secure) Master).PageTitle = "Manage Volumes";
        }
    }
}