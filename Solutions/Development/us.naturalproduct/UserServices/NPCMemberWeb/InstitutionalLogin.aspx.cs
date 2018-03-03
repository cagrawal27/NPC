using System;
using System.Web.Security;
using us.naturalproduct.BusinessServices.BusinessFacades;
using us.naturalproduct.Common;
using dto = us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.web
{
    public partial class InstitutionalLogin : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.User.Identity.IsAuthenticated)
                AuthenticateIP();
        }

        private void AuthenticateIP()
        {
            dto.Login inLoginDto = GetPageValues();

            LoginFacade facade = new LoginFacade();

            dto.Login outLoginDto = facade.AuthenticateIP(inLoginDto);

            if (outLoginDto != null)
            {
                // For this sample, always set the persist flag to false.
                if (Request.QueryString["returnurl"] != null)
                {
                    FormsAuthentication.RedirectFromLoginPage(outLoginDto.EmailAddress, false);
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(outLoginDto.EmailAddress, false);

                    Response.Redirect(Pages.Home, false);
                }
            }
            else
                LblIPAddress.Text = inLoginDto.IPAddress;
        }

        private dto.Login GetPageValues()
        {
            dto.Login loginDto = new dto.Login();

            loginDto.IPAddress = Request.ServerVariables["REMOTE_ADDR"];

            return loginDto;
        }
    }
}