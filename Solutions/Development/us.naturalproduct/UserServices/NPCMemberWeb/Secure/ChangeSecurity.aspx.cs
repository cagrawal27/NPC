using System;
using System.Web.UI.WebControls;
using MN.Enterprise.Business;
using us.naturalproduct.BusinessServices.BusinessFacades;
using us.naturalproduct.DataTransferObjects;
using us.naturalproduct.web.Masters;

namespace us.naturalproduct.web
{
    public partial class ChangeSecurity : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((Secure) Master).PageTitle = "Change Security Information";

            string forceChange = Request.QueryString["forceChange"];

            if (forceChange != null && forceChange == "true")
            {
                ActionStatus status = new ActionStatus();

                status.Messages.Add(
                    new ActionMessage(
                        "Your password was recently reset.  You must change your password before proceeding."));

                LitStatus.Text = GetFormattedMessages(status);
            }

            LoadPageValues();
        }

        private User GetCurrentUserObj()
        {
            User outUserDto = new User();

            outUserDto.EmailAddress = User.Identity.Name;

            return outUserDto;
        }

        private void LoadPageValues()
        {
            User inUserDto = GetCurrentUserObj();

            UserFacade facade = new UserFacade();

            User outUserDto = facade.GetUser(inUserDto);

            LblSecretQstn1.Text = ddlPassQuestion1.Items.FindByValue(outUserDto.SecretQuestion1Id.ToString()).Text;

            LblSecretQstn2.Text = ddlPassQuestion1.Items.FindByValue(outUserDto.SecretQuestion2Id.ToString()).Text;
        }

        protected void CvPassword_OnValidate(object source, ServerValidateEventArgs args)
        {
            if (WebUtils.IsStrongPassword(args.Value))
                args.IsValid = true;
            else
                args.IsValid = false;
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                User inUserDto = GetCurrentUserObj();

                inUserDto.Password = tbPassword.Text;

                UserFacade facade = new UserFacade(BusinessFacadeBehavior.TRANSACTIONAL);

                ActionStatus status = facade.UpdatePassword(inUserDto);

                LitStatus.Text = GetFormattedMessages(status);

                if (status.IsSuccessful)
                    UserInfo.Abandon();
            }
        }

        protected void BtnChangeRecoveryInfo_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                User inUserDto = GetCurrentUserObj();

                inUserDto.SecretQuestion1Id = Convert.ToInt32(ddlPassQuestion1.SelectedValue);

                inUserDto.SecretAnswer1 = tbPassAnswer1.Text.Trim();

                inUserDto.SecretQuestion2Id = Convert.ToInt32(ddlPassQuestion2.SelectedValue);

                inUserDto.SecretAnswer2 = tbPassAnswer2.Text.Trim();

                UserFacade facade = new UserFacade(BusinessFacadeBehavior.TRANSACTIONAL);

                ActionStatus status = facade.UpdatePasswordRecoveryInfo(inUserDto);

                if (status.IsSuccessful)
                {
                    tbPassAnswer1.Text = "";

                    tbPassAnswer2.Text = "";
                }

                LitStatus.Text = GetFormattedMessages(status);
            }
        }
    }
}