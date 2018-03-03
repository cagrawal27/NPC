using System;
using System.Web.Security;
using us.naturalproduct.BusinessServices.BusinessFacades;
using us.naturalproduct.Common;
using us.naturalproduct.DataTransferObjects;
using us.naturalproduct.web.Masters;
using dto = us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.web
{
    /// <summary>
    /// Summary description for WebForm1.
    /// </summary>
    public partial class Login : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            ((Secure) Master).PageTitle = "Login";

            tbEmail.Focus();
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
            this.Load += new EventHandler(this.Page_Load);
            this.btnLogin.Click += new EventHandler(btnLogin_OnClick);
        }

        #endregion    

        private void btnLogin_OnClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                dto.Login inLoginDto = GetPageValues();

                LoginFacade facade = new LoginFacade();

                ActionStatus status = facade.AuthenticateUser(inLoginDto);

                // Here's the call to validate the user's credentials and redirect to the appropriate
                // page if valid.
                if (status.IsSuccessful)
                {
                    // For this sample, always set the persist flag to false.
                    if (Request.QueryString["returnurl"] != null)
                    {
                        FormsAuthentication.RedirectFromLoginPage(inLoginDto.EmailAddress, false);
                    }
                    else
                    {
                        FormsAuthentication.SetAuthCookie(inLoginDto.EmailAddress, false);

                        Response.Redirect(Pages.Home, false);
                    }
                }
                else
                    lblErrors.Text = GetFormattedMessages(status);
            }
            else
                VsLogin.Visible = true;
        }

        private dto.Login GetPageValues()
        {
            dto.Login loginDto = new DataTransferObjects.Login();

            loginDto.EmailAddress = tbEmail.Text.Trim();

            loginDto.Password = tbPassword.Text.Trim();

            return loginDto;
        }
    }
}