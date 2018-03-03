using System;
using System.IO;
using System.Web.UI.WebControls;
using us.naturalproduct.BusinessServices.BusinessFacades;
using us.naturalproduct.DataTransferObjects;
using us.naturalproduct.web.Masters;

namespace us.naturalproduct.web
{
    public partial class ManageIssues : BaseAdminPage
    {
        private IssueFacade IssueBF;
        private static ListItem liDefault = new ListItem("-- Select --", "");
        private static string issueSessKey = "ManageIssueDto";

        protected void Page_Load(object sender, EventArgs e)
        {
            //Set title
            ((Secure) Master).PageTitle = "Manage Issues";

            IssueBF = new IssueFacade();

            if (!Page.IsPostBack)
            {
                LoadLists();
            }
        }

        private void LoadLists()
        {
            ddlVolumes.DataSource = VolumeFacade.AdminGetVolumes();

            ddlVolumes.DataBind();

            ddlVolumes.Items.Insert(0, liDefault);
        }

        protected void ddlIssues_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int32 VolumeIssueId = Convert.ToInt32(ddlIssues.SelectedValue);

            IssueBF.IssueDto = IssueFacade.AdminGetIssue(VolumeIssueId);

            SetPageValues();

            SetSessionValues();
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

        private void SetPageValues()
        {
            tbIssueName.Text = IssueBF.IssueDto.IssueName;

            ddlStatus.SelectedValue = IssueBF.IssueDto.IsActive ? "1" : "0";

            gvDocuments.DataSource = IssueBF.IssueDto.Documents;

            gvDocuments.DataBind();
        }

        private void GetPageValues()
        {
            IssueBF.IssueDto.IssueName = tbIssueName.Text.Trim();

            IssueBF.IssueDto.IsActive = Convert.ToBoolean(Convert.ToInt32(ddlStatus.SelectedValue));

            Int32 VolumeIssueId = Convert.ToInt32(ddlIssues.SelectedValue);

            IssueBF.IssueDto.VolumeIssueId = VolumeIssueId;
        }

        #region SESSION

        private void GetSessionValues()
        {
            //Get item from session
            Issue issueDto = Session[issueSessKey] as Issue;

            //If empty then init
            if (issueDto == null)
                IssueBF.IssueDto = new Issue();
            else
                IssueBF.IssueDto = issueDto;
        }

        private void SetSessionValues()
        {
            //If the value in the page is null then init
            if (IssueBF.IssueDto == null)
                Session[issueSessKey] = new Issue();
            else
                Session[issueSessKey] = IssueBF.IssueDto;
        }

        #endregion

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            //Get session values
            GetSessionValues();

            //Update page Value object
            GetPageValues();

            //Update database
            ActionStatus status = IssueBF.UpdateIssue();

            if (status.IsSuccessful)
            {
                DeleteTempDocs();

                IssueBF.IssueDto = null;

                SetSessionValues();

                lblStatus.Text = GetFormattedMessages(status);
            }

            //Update session
            SetSessionValues();
        }

        #region DOCUMENTS

        protected void btnAddDoc_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                //Get session values
                GetSessionValues();

                try
                {
                    if (tbUploadDocument.HasFile && tbUploadDocument.PostedFile != null)
                    {
                        string fileName = SaveDocToTemp(tbUploadDocument);

                        IssueDocument issueDoc = GetDocValues();

                        issueDoc.FullFileName = fileName;

                        IssueBF.IssueDto.Documents.Add(issueDoc);

                        SetSessionValues();

                        LoadDocGrid();

                        ResetDocumentFormValues();
                    }
                    else
                        DisplayPopup("The document cannot be empty.  Please choose a document with content.");
                }
                catch (IOException ioEx)
                {
                    throw;
                }
            }
        }

        private void LoadDocGrid()
        {
            gvDocuments.DataSource = IssueBF.IssueDto.Documents;

            gvDocuments.DataBind();
        }

        private void ResetDocumentFormValues()
        {
            ddlIssueDocTypes.SelectedIndex = 0;

            tbComments.Text = string.Empty;

            ddlDocStatus.SelectedIndex = 0;
        }

        protected void gvDocuments_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //Populate dto from session
            GetSessionValues();

            if (IssueBF.IssueDto.Documents[e.RowIndex].IsOld)
            {
                //Mark for deletion from database
                IssueBF.IssueDto.Documents[e.RowIndex].IsDeleted = true;
            }
            else
            {
                //Get the document
                IssueDocument issueDoc = IssueBF.IssueDto.Documents[e.RowIndex];

                try
                {
                    File.Delete(issueDoc.FullFileName);
                }
                catch (IOException ioEx)
                {
                    DisplayPopup("An error occured deleting the document.");
                }

                //Remove from documents list
                IssueBF.IssueDto.Documents.RemoveAt(e.RowIndex);
            }

            //Update session
            SetSessionValues();

            //Reload the grid
            LoadDocGrid();
        }

        private IssueDocument GetDocValues()
        {
            IssueDocument issueDoc = new IssueDocument();

            issueDoc.IssueDocTypeId = Convert.ToInt32(ddlIssueDocTypes.SelectedValue);

            issueDoc.Comments = tbComments.Text.Trim();

            issueDoc.CreationUserId = UserInfo.UserDto.UserId;

            issueDoc.IsActive = Convert.ToBoolean(Convert.ToInt32(ddlDocStatus.SelectedValue));

            return issueDoc;
        }

        protected void gvDocuments_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.DataRow))
            {
                if (IssueBF.IssueDto.Documents[e.Row.RowIndex].IsDeleted)
                    e.Row.Visible = false;

                Label lblDocType = (Label) e.Row.FindControl("lblDocType");

                IssueDocument issueDoc = (IssueDocument) e.Row.DataItem;

                if (!(lblDocType == null || issueDoc == null))
                {
                    ListItem liItem = ddlIssueDocTypes.Items.FindByValue(issueDoc.IssueDocTypeId.ToString());

                    if (liItem != null)
                        lblDocType.Text = liItem.Text;
                }
            }
        }

        #endregion
    }
}