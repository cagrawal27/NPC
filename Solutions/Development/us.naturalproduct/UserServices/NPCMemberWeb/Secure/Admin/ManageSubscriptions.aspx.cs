using System;
using System.Web.UI.WebControls;
using us.naturalproduct.web.Masters;

namespace us.naturalproduct.web
{
    public partial class ManageSubscriptions : BaseAdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Set title
            ((Secure) Master).PageTitle = "Manage Subscriptions";
        }

        protected void gvUsers_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            if (!e.NewSelectedIndex.Equals(-1))
            {
                if (gvUsers.SelectedIndex.Equals(e.NewSelectedIndex))
                {
                    gvUsers.SelectedIndex = -1;

                    e.Cancel = true;
                }
            }
        }

        protected void fvNewSubscription_ItemInserting(object sender, FormViewInsertEventArgs e)
        {
            if (!Page.IsValid)
            {
                e.Cancel = true;
            }
        }
    }
}