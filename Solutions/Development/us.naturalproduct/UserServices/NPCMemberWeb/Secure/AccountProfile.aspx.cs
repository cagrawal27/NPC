using System;
using us.naturalproduct.web.Masters;

namespace us.naturalproduct.web
{
    /// <summary>
    /// Summary description for AccountProfile.
    /// </summary>
    public partial class AccountProfile : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((Secure) Master).PageTitle = "Account Profile";
        }

        #region Web Form Designer generated code

        protected override void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        }

        #endregion
    }
}