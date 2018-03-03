using System;
using System.Web.UI.WebControls;
using MN.Enterprise.Business;
using us.naturalproduct.BusinessFacades;
using us.naturalproduct.BusinessServices.BusinessFacades;
using us.naturalproduct.Common;
using us.naturalproduct.DataTransferObjects;
using us.naturalproduct.web.Masters;

namespace us.naturalproduct.web
{
    public partial class ManageUsers : BaseAdminPage
    {
        private SqlDataSource sdsUsers;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Set title
            ((Secure)Master).PageTitle = "Manage Users";

            if (!IsPostBack)
            {
                LoadLists();
            }
        }

        private void LoadLists()
        {
            LoadAccountTypes();

            LoadAccountStatuses();

            LoadUsers();
        }

        private void LoadAccountStatuses()
        {
            SqlDataSource sdsAcctStatuses = new SqlDataSource(Config.ConnString, "spGetAccountStatuses");

            sdsAcctStatuses.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;

            ddlAccountStatus.DataSource = sdsAcctStatuses;

            ddlAccountStatus.DataTextField = "AccountStatusDescription";

            ddlAccountStatus.DataValueField = "AccountStatusID";

            ddlAccountStatus.DataBind();
        }

        private void LoadAccountTypes()
        {
            SqlDataSource sdsAcctTypes = new SqlDataSource(Config.ConnString, "spGetAccountTypes");

            sdsAcctTypes.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;

            ddlAccountType.DataSource = sdsAcctTypes;

            ddlAccountType.DataValueField = "AccountTypeId";

            ddlAccountType.DataTextField = "Description";

            ddlAccountType.DataBind();
        }

        private void LoadUsers()
        {
            sdsUsers = new SqlDataSource(Config.ConnString, "spAdminGetUsersByParams");

            sdsUsers.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;

            sdsUsers.SelectParameters.Add(new ControlParameter("FirstName", "tbFirstname", "Text"));

            sdsUsers.SelectParameters.Add(new ControlParameter("LastName", "tbLastname", "Text"));

            sdsUsers.SelectParameters.Add(new ControlParameter("Email", "tbEmail", "Text"));

            sdsUsers.SelectParameters.Add(new ControlParameter("AccountTypeId", "ddlAccountType", "SelectedValue"));

            sdsUsers.SelectParameters.Add(new ControlParameter("AccountStatusId", "ddlAccountStatus", "SelectedValue"));

            sdsUsers.DeleteCommand = "spDeleteUser";

            sdsUsers.DeleteCommandType = SqlDataSourceCommandType.StoredProcedure;

            gvUsers.DataSource = sdsUsers;

            gvUsers.DataBind();

        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            LoadUsers();
        }
    }
}