using System;
using System.Web.UI.WebControls;
using us.naturalproduct.BusinessServices.BusinessFacades;
using us.naturalproduct.Common;
using us.naturalproduct.DataTransferObjects;
using us.naturalproduct.web.Masters;
using dto = us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.web
{
    public partial class Registration : BasePage
    {
        private RegistrationFacade RegistrationBL;

        #region Event Handlers

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            RegistrationBL = new RegistrationFacade();
            ((Unsecure) Master).PageTitle = "New Member Registration";

            if (!Page.IsPostBack)
            {
                LoadLists();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRegister_OnClick(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                VsRegistration.Visible = true;
            else
            {
                //Get values from the form
                GetPageValues();

                ActionStatus status = RegistrationBL.RegisterUser();

                if (status.IsSuccessful)
                    Response.Redirect(Pages.Registration_Status, false);
                else
                    lblErrors.Text = GetFormattedMessages(status);
            }
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

        #endregion

        /// <summary>
        /// Loads any lists on the page.
        /// </summary>
        private void LoadLists()
        {
            ListItem liDefault = new ListItem("-- Select --", "");

            //Load Country drop down list
            ddlCountry.DataSource = GetCountries(false);
            ddlCountry.DataTextField = Constants.CountryName;
            ddlCountry.DataValueField = Constants.CountryId;
            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, liDefault);

            //Load State drop down list
            ddlStateProvince.DataSource = GetStates(false);
            ddlStateProvince.DataTextField = Constants.StateName;
            ddlStateProvince.DataValueField = Constants.StateCode;
            ddlStateProvince.DataBind();
        }

        private void GetPageValues()
        {
            DataTransferObjects.Registration regDto = new DataTransferObjects.Registration();

            regDto.EmailAddress = tbEmail.Text.Trim();

            regDto.Password = tbPassword.Text.Trim();

            regDto.SecretQuestion1 = Convert.ToInt32(ddlPassQuestion1.SelectedValue);

            regDto.SecretAnswer1 = tbPassAnswer1.Text.Trim();

            regDto.SecretQuestion2 = Convert.ToInt32(ddlPassQuestion2.SelectedValue);

            regDto.SecretAnswer2 = tbPassAnswer2.Text.Trim();

            regDto.AccountType = Convert.ToInt32(ddlAccountType.SelectedValue);

            regDto.FirstName = tbFirstName.Text.Trim();

            regDto.LastName = tbLastName.Text.Trim();

            regDto.MiddleInitial = tbMiddleInitial.Text.Trim();

            regDto.AddressLine1 = tbAddressLine1.Text.Trim();

            regDto.AddressLine2 = tbAddressLine2.Text.Trim();

            regDto.City = tbCity.Text.Trim();

            regDto.State = ddlStateProvince.SelectedValue.Equals("NA")
                               ? tbStateProvince.Text.Trim()
                               : ddlStateProvince.SelectedValue;

            regDto.Zip = tbZipPostalCode.Text.Trim();

            regDto.CountryId = Convert.ToInt32(ddlCountry.SelectedValue);

            regDto.Phone = tbPhone.Text.Trim();

            regDto.Fax = tbFax.Text.Trim();

            RegistrationBL.RegistrationDto = regDto;
        }
    }
}