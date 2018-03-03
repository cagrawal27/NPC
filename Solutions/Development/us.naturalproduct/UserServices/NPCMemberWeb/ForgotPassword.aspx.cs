using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using MN.Enterprise.Business;
using us.naturalproduct.BusinessServices.BusinessFacades;
using us.naturalproduct.Common;
using us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.web
{
    public partial class ForgotPassword : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            tbEmail.Focus();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CvPassword_OnValidate(object sender, ServerValidateEventArgs e)
        {
            if (WebUtils.IsStrongPassword(e.Value))
                e.IsValid = true;
            else
                e.IsValid = false;
        }

        private User GetPageValues()
        {
            User userDto = new User();

            userDto.EmailAddress = tbEmail.Text;

            return userDto;
        }

        protected void btnSubmitEmail_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                User inUserDto = GetPageValues();

                PasswordRecoveryQuestionFacade facade = new PasswordRecoveryQuestionFacade();

                List<PasswordRecoveryQuestion> qstnList = facade.GetPasswordQuestions(inUserDto);

                if (qstnList == null)
                {
                    //if email does not exist display error
                    LitStatus.Text = GetFormattedMessages("Email address does not exist.");
                }
                else
                {
                    //Hide/Show appropriate UI elements
                    FldRecoveryInfo.Visible = true;

                    tbEmail.Enabled = false;

                    btnSubmitEmail.Visible = false;

                    btnSubmitAnswer.Visible = true;

                    //Populate password question list
                    ddlPasswordQuestions.DataSource = qstnList;

                    ddlPasswordQuestions.DataTextField = "Description";

                    ddlPasswordQuestions.DataValueField = "QuestionId";

                    ddlPasswordQuestions.DataBind();

                    ddlPasswordQuestions.Items.Insert(0, liDefault);

                    ddlPasswordQuestions.Focus();
                }
            }
        }

        protected void btnSubmitAnswer_Click(object sender, EventArgs e)
        {
            //TODO:  Implement Answer Verification
            if (IsValid)
            {
                User inUserDto = GetPageValues();

                inUserDto.SecretQuestionId = Convert.ToInt32(ddlPasswordQuestions.SelectedValue);

                inUserDto.SecretAnswer = tbPasswordAnswer.Text;

                UserFacade facade = new UserFacade();

                if (facade.VerifyPasswordAnswer(inUserDto))
                {
                    FldPassword.Visible = true;

                    ddlPasswordQuestions.Enabled = false;

                    tbPasswordAnswer.Enabled = false;

                    btnSubmitAnswer.Visible = false;

                    btnChangePassword.Visible = true;

                    tbPassword.Focus();
                }
                else
                {
                    LitStatus.Text = GetFormattedMessages("Password answer is invalid.");
                }
            }
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                User inUserDto = GetPageValues();

                inUserDto.Password = tbPassword.Text;

                UserFacade facade = new UserFacade(BusinessFacadeBehavior.TRANSACTIONAL);

                ActionStatus status = facade.UpdatePassword(inUserDto);

                if (status.IsSuccessful)
                {
                    tbPassword.Enabled = false;

                    tbPasswordRepeat.Enabled = false;

                    btnChangePassword.Visible = false;
                }

                LitStatus.Text = GetFormattedMessages(status);
            }
        }

        protected void btnGoToLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect(Pages.Login, false);
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect(Pages.ForgotPassword, false);
        }
    }
}