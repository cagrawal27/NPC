using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using MN.Enterprise.Business;
using us.naturalproduct.BusinessServices.BusinessFacades;
using us.naturalproduct.Common;
using us.naturalproduct.DataTransferObjects;
using dto = us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.web
{
    public partial class EditSubscriptions : BaseAdminPage
    {
        private string _UserIdParam;

        protected new void Page_Load(object sender, EventArgs e)
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
                        LoadExistingSubscriptions();
                    }
                }
            }
        }

        private void LoadLists()
        {
            ddlVolumes.DataSource = VolumeFacade.AdminGetVolumes();

            ddlVolumes.DataBind();

            ddlVolumes.Items.Insert(0, liDefault);
        }

        private void LoadExistingSubscriptions()
        {
            SubscriptionFacade facade = new SubscriptionFacade();

            Subscription subscriptionDto = new Subscription();

            subscriptionDto.UserId = Convert.ToInt32(_UserIdParam);

            GvExistingSubscriptions.DataSource = facade.GetUserSubscriptions(subscriptionDto);

            GvExistingSubscriptions.DataBind();
        }

        private void LoadAvailableSubscriptions()
        {
            if (ddlVolumes.SelectedIndex > 0 && ddlIssues.SelectedIndex > 0)
            {
                Subscription subscriptionDto = new Subscription();

                subscriptionDto.VolumeIssueId = Convert.ToInt32(ddlIssues.SelectedValue);

                subscriptionDto.UserId = Convert.ToInt32(_UserIdParam);

                SubscriptionFacade facade = new SubscriptionFacade();

                GvNewSubscriptions.DataSource = facade.GetUserAvailableSubscriptions(subscriptionDto);

                GvNewSubscriptions.DataBind();
            }
        }

        protected void ddlVolumes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlVolumes.SelectedIndex > 0)
            {
                Int32 VolumeId = Convert.ToInt32(ddlVolumes.SelectedValue);

                ddlIssues.DataSource = IssueFacade.AdminGetIssues(VolumeId);

                ddlIssues.DataBind();

                ddlIssues.Items.Insert(0, liDefault);
            }
        }

        protected void ddlIssues_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlIssues.SelectedIndex > 0)
            {
                LoadAvailableSubscriptions();
                tbDateEffective.Text = DateTime.Now.ToString("MM/dd/yyyy");
                tbDateExpiration.Text = DateTime.Now.AddYears(1).ToString("MM/dd/yyyy");
            }
        }

        protected void cbxSubscribeAll_OnCheckedChanged(object sender, EventArgs e)
        {
            //Iterate over each row in the gridview control
            foreach (GridViewRow currentRow in GvNewSubscriptions.Rows)
            {
                //Get a handle to the checkbox control in the current row
                CheckBox cbxSubscribe = (CheckBox)currentRow.FindControl("cbxSubscribe");

                cbxSubscribe.Checked = !cbxSubscribe.Checked;
            }
        }

        #region GridView Eents
        protected void GvExistingSubscriptions_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvExistingSubscriptions.PageIndex = e.NewPageIndex;

            LoadExistingSubscriptions();
        }

        protected void GvExistingSubscriptions_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Subscription subscriptionDto = new Subscription();

            subscriptionDto.SubscriptionId = Convert.ToInt32(GvExistingSubscriptions.DataKeys[e.RowIndex].Value);

            SubscriptionFacade subscriptionFacade = new SubscriptionFacade(BusinessFacadeBehavior.TRANSACTIONAL);

            ActionStatus status = subscriptionFacade.DeleteSubscription(subscriptionDto);

            if (status.IsSuccessful)
            {
                LoadExistingSubscriptions();

                LoadAvailableSubscriptions();
            }

            LblStatus.Text = GetFormattedMessages(status);
        }

        protected void GvExistingSubscriptions_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GvExistingSubscriptions.EditIndex = e.NewEditIndex;

            LoadExistingSubscriptions();
        }

        protected void GvExistingSubscriptions_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GvExistingSubscriptions.EditIndex = -1;

            LoadExistingSubscriptions();
        }

        protected void GvExistingSubscriptions_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            TextBox tbEffectiveDate = GvExistingSubscriptions.Rows[e.RowIndex].Cells[7].Controls[0] as TextBox;

            TextBox tbExpirationDate = GvExistingSubscriptions.Rows[e.RowIndex].Cells[8].Controls[0] as TextBox;

            CheckBox cbxActive = GvExistingSubscriptions.Rows[e.RowIndex].Cells[9].Controls[0] as CheckBox;

            if (null != tbEffectiveDate && null != tbExpirationDate && null != cbxActive)
            {
                //Init subscription object
                DataTransferObjects.Subscription subscriptionDto = new DataTransferObjects.Subscription();

                //Init facade
                SubscriptionFacade facade = new SubscriptionFacade(BusinessFacadeBehavior.TRANSACTIONAL);

                //populate subscription object
                subscriptionDto.SubscriptionId =
                    Convert.ToInt32(GvExistingSubscriptions.DataKeys[e.RowIndex]["SubscriptionId"]);

                subscriptionDto.UserId = Convert.ToInt32(_UserIdParam);

                subscriptionDto.VolumeIssueId =
                    Convert.ToInt32(GvExistingSubscriptions.DataKeys[e.RowIndex]["VolumeIssueId"]);

                subscriptionDto.ArticleId = Convert.ToInt32(GvExistingSubscriptions.DataKeys[e.RowIndex]["ArticleId"]);

                subscriptionDto.EffectiveDate = DateTime.Parse(tbEffectiveDate.Text);

                subscriptionDto.ExpirationDate = DateTime.Parse(tbExpirationDate.Text);

                subscriptionDto.IsActive = cbxActive.Checked;

                subscriptionDto.UpdateUserId = UserInfo.UserDto.UserId;

                ActionStatus status = facade.UpdateSubscription(subscriptionDto);

                if (status.IsSuccessful)
                {
                    GvExistingSubscriptions.EditIndex = -1;

                    LoadExistingSubscriptions();
                }

                LblStatus.Text = GetFormattedMessages(status);
            }
        }

        protected void GvExistingSubscriptions_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            DisplayPopup("In RowUpdated");

            GvExistingSubscriptions.EditIndex = -1;
        }

        #endregion

        #region Button Events
        protected void BtnRemoveAllSubscriptions_Click(object sender, EventArgs e)
        {
            ActionStatus status = null;

            if (GvExistingSubscriptions.Rows.Count > 0)
            {
                List<dto.Subscription> subscriptionsToDelete = new List<dto.Subscription>();

                //Iterate over each row in the gridview control
                foreach (GridViewRow currentRow in GvExistingSubscriptions.Rows)
                {
                    //Create a new subscription object
                    dto.Subscription subscriptionDto = new dto.Subscription();

                    //Populate the subscriptionId
                    subscriptionDto.SubscriptionId =
                        Convert.ToInt32(GvExistingSubscriptions.DataKeys[currentRow.RowIndex]["SubscriptionId"]);

                    subscriptionsToDelete.Add(subscriptionDto);
                }

                SubscriptionFacade facade = new SubscriptionFacade(BusinessFacadeBehavior.TRANSACTIONAL);

                status = facade.DeleteSubscriptions(subscriptionsToDelete);

                LoadExistingSubscriptions();
            }
            else
            {
                status = new ActionStatus();

                status.Messages.Add(new ActionMessage("No subscriptions available to delete."));
            }

            LblStatus.Text = GetFormattedMessages(status);
        }

        protected void BtnAddSubscriptions_Click(object sender, EventArgs e)
        {
            List<Subscription> selectedSubscriptions = new List<Subscription>();

            //Iterate over each row in the gridview control
            foreach (GridViewRow currentRow in GvNewSubscriptions.Rows)
            {
                //Get a handle to the checkbox control in the current row
                CheckBox cbxSubscribe = (CheckBox)currentRow.FindControl("cbxSubscribe");

                //If it is checked
                if (cbxSubscribe.Checked)
                {
                    //Create a new subscription object
                    Subscription subscriptionDto = new Subscription();

                    //Populate the articleId
                    subscriptionDto.ArticleId = (Int32)GvNewSubscriptions.DataKeys[currentRow.RowIndex].Value;

                    subscriptionDto.VolumeIssueId = Convert.ToInt32(ddlIssues.SelectedValue);

                    subscriptionDto.UserId = Convert.ToInt32(_UserIdParam);

                    //TextBox tbDateEffective = (TextBox)currentRow.FindControl("tbDateEffective");
                    //TextBox tbDateExpiration = (TextBox)currentRow.FindControl("tbDateExpiration");
                    CheckBox cbxActive = (CheckBox)currentRow.FindControl("cbxActive");

                    subscriptionDto.EffectiveDate = DateTime.Parse(tbDateEffective.Text);

                    subscriptionDto.ExpirationDate = DateTime.Parse(tbDateExpiration.Text);

                    subscriptionDto.IsActive = (Boolean)cbxActive.Checked;

                    subscriptionDto.CreationUserId = UserInfo.UserDto.UserId;

                    //add it to list
                    selectedSubscriptions.Add(subscriptionDto);
                }
            }

            SubscriptionFacade facade = new SubscriptionFacade(BusinessFacadeBehavior.TRANSACTIONAL);

            ActionStatus status = facade.AddSubscriptions(selectedSubscriptions);

            //If subscription added successfully then reload available and existing
            //subscriptions
            if (status.IsSuccessful)
            {
                LoadAvailableSubscriptions();

                LoadExistingSubscriptions();
            }

            LblStatus.Text = GetFormattedMessages(status);
        }

        #endregion
    }
}