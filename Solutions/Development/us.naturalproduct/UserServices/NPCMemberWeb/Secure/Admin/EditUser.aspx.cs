using System;
using System.Web.UI.WebControls;
using MN.Enterprise.Business;
using us.naturalproduct.BusinessFacades;
using us.naturalproduct.BusinessServices.BusinessFacades;
using us.naturalproduct.Common;
using us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.web
{
    public partial class EditUser : BaseAdminPage
    {
        private string _UserIdParam;

        protected void Page_Load(object sender, EventArgs e)
        {
            _UserIdParam = ValidateQryParamInt32("UserId");

            if (_UserIdParam == null)
                Response.Redirect(Pages.Home);
            else
            {
                if (!IsPostBack)
                {
                    LoadLists();

                    UserFacade userFacade = new UserFacade();

                    User inUserDto = new User();

                    inUserDto.UserId = Convert.ToInt32(_UserIdParam);

                    User outUserDto = userFacade.GetUser(inUserDto);

                    if (outUserDto == null)
                        Response.Redirect(Pages.Home, true);
                    else
                    {
                        SetUserValues(outUserDto);
                        SetUserIPValues();
                    }

                    btnResetPassword.Attributes.Add("OnClick",
                                                    "if (confirm(\"Reseting the password requires that the user's email address be a valid email address.  Are you sure you want to reset this user's password?\")) return true; else return false;");
                }
            }
        }

        private void SetUserValues(User userDto)
        {
            tbEmail.Text = userDto.EmailAddress;

            tbFirstName.Text = userDto.FirstName;

            tbLastName.Text = userDto.LastName;

            tbMiddleInitial.Text = userDto.MiddleInitial;

            ddlAccountType.SelectedValue = userDto.AccountType.ToString();

            ddlAccountStatus.SelectedValue = userDto.AccountStatus.ToString();

            cbxActive.Checked = userDto.IsActive;

            LblCreationDateTime.Text = userDto.CreationDateTime.ToString();

            LblUpdateDateTime.Text = userDto.UpdateDateTime.ToString();
        }

        private void SetUserIPValues()
        {
            string selVal = ddlAccountType.SelectedValue;

            int accountType = Convert.ToInt32(selVal);

            if (accountType.Equals(Constants.Account_Type_Institutional))
            {
                DivIP.Visible = true;

                UserIPAddress userIPAddressDto = new UserIPAddress();

                userIPAddressDto.UserId = Convert.ToInt32(_UserIdParam);

                UserIPAddressFacade facade = new UserIPAddressFacade();

                GvIPAddresses.DataSource = facade.GetList(userIPAddressDto);

                GvIPAddresses.DataBind();            
            }
            else
                DivIP.Visible = false;
        }

        private User GetFormValues()
        {
            User userDto = new User();

            userDto.UserId = Convert.ToInt32(_UserIdParam);

            userDto.EmailAddress = tbEmail.Text.Trim();

            userDto.FirstName = tbFirstName.Text.Trim();

            userDto.LastName = tbLastName.Text.Trim();

            userDto.MiddleInitial = tbMiddleInitial.Text.Trim();

            userDto.AccountType = Convert.ToInt32(ddlAccountType.SelectedValue);

            userDto.AccountStatus = Convert.ToInt32(ddlAccountStatus.SelectedValue);

            userDto.IsActive = cbxActive.Checked;

            return userDto;
        }

        private void LoadLists()
        {
            SqlDataSource sdsAcctTypes = new SqlDataSource(Config.ConnString, "spGetAccountTypes");

            sdsAcctTypes.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;

            ddlAccountType.DataSource = sdsAcctTypes;

            ddlAccountType.DataValueField = "AccountTypeId";

            ddlAccountType.DataTextField = "Description";

            ddlAccountType.DataBind();

            SqlDataSource sdsAcctStatuses = new SqlDataSource(Config.ConnString, "spGetAccountStatuses");

            sdsAcctStatuses.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;

            ddlAccountStatus.DataSource = sdsAcctStatuses;

            ddlAccountStatus.DataTextField = "AccountStatusDescription";

            ddlAccountStatus.DataValueField = "AccountStatusID";

            ddlAccountStatus.DataBind();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            User userDto = GetFormValues();

            UserFacade userFacade = new UserFacade(BusinessFacadeBehavior.TRANSACTIONAL);

            userFacade.UpdateUser(userDto);

            Response.Redirect(Pages.ManageUsers);
        }

        protected void BtnAddIP_Click(object sender, EventArgs e)
        {
            UserIPAddress userIPAddressDto = new UserIPAddress();

            userIPAddressDto.UserId = Convert.ToInt32(_UserIdParam);

            userIPAddressDto.BeginAddress = new IPAddress();

            userIPAddressDto.BeginAddress.Octet1 = Convert.ToInt32(TbIPOctet1.Text);

            userIPAddressDto.BeginAddress.Octet2 = Convert.ToInt32(TbIPOctet2.Text);

            userIPAddressDto.BeginAddress.Octet3 = Convert.ToInt32(TbIPOctet3.Text);

            userIPAddressDto.BeginAddress.Octet4 = Convert.ToInt32(TbIPOctet4.Text);

            userIPAddressDto.EndAddress = new IPAddress();

            userIPAddressDto.EndAddress.Octet3 = Convert.ToInt32(TbIPOctet3End.Text);

            userIPAddressDto.EndAddress.Octet4 = Convert.ToInt32(TbIPOctet4End.Text);

            UserIPAddressFacade facade = new UserIPAddressFacade();

            ActionStatus status = facade.Exists(userIPAddressDto);

            if (status.IsSuccessful)
            {
                status = facade.Add(userIPAddressDto);

                if (status.IsSuccessful)
                    SetUserIPValues();
                else
                    LblStatus.Text = GetFormattedMessages(status);
            }
            else
                LblStatus.Text = GetFormattedMessages(status);
                
        }

        protected void GvIPAddresses_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataKey key = GvIPAddresses.DataKeys[e.RowIndex];

            bool isDeleted = false;

            if (key != null)
            {
                int UserIPId;

                int.TryParse(key.Value.ToString(), out UserIPId);

                if (UserIPId > 0)
                {
                    UserIPAddressFacade facade = new UserIPAddressFacade();

                    UserIPAddress userIPAddressDto = new UserIPAddress();

                    userIPAddressDto.UserIPId = UserIPId;

                    ActionStatus status = facade.Delete(userIPAddressDto);

                    LblStatus.Text = GetFormattedMessages(status);

                    isDeleted = status.IsSuccessful;
                }
            }

            if (isDeleted)
                SetUserIPValues();
            else
                e.Cancel = true;
        }

        protected void btnResetPassword_Click(object sender, EventArgs e)
        {
            User inUserDto = new User();

            inUserDto.EmailAddress = tbEmail.Text;

            UserFacade facade = new UserFacade(BusinessFacadeBehavior.TRANSACTIONAL);

            ActionStatus status = facade.ResetPassword(inUserDto);

            LblStatus.Text = GetFormattedMessages(status);
        }
    }
}