using System;
using System.Configuration;
using System.Web.UI;

namespace us.naturalproduct.web.Masters
{
    public partial class Unsecure : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public string PageTitle
        {
            get { return this.Page.Title; }

            set
            {
                this.Page.Title = string.Format("{0} - {1}",
                                                ConfigurationSettings.AppSettings["SiteTitle"],
                                                value);
            }
        }
    }
}