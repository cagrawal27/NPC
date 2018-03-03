using System;
using System.IO;
using System.Web.UI.WebControls;
using us.naturalproduct.BusinessServices.BusinessFacades;
using us.naturalproduct.web.Masters;
using dto = us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.web
{
    public partial class AddIssue : BaseAdminPage
    {
        private IssueFacade IssueBF;
        private static string issueSessKey = "AddIssueDto";

        protected void Page_Load(object sender, EventArgs e)
        {
            //Set title
            ((Secure) Master).PageTitle = "Add Issue";

            //Init business layer
            IssueBF = new IssueFacade();

            if (!Page.IsPostBack)
            {
                Session[issueSessKey] = null;
            }
        }

        #region SESSION

        private void GetSessionValues()
        {
            //Get item from session
            dto.Issue issueDto = Session[issueSessKey] as dto.Issue;

            //If empty then init
            if (issueDto == null)
                IssueBF.IssueDto = new dto.Issue();
            else
                IssueBF.IssueDto = issueDto;
        }

        private void SetSessionValues()
        {
            //If the value in the page is null then init
            if (IssueBF.IssueDto == null)
                Session[issueSessKey] = new dto.Issue();
            else
                Session[issueSessKey] = IssueBF.IssueDto;
        }

        #endregion

        private void ResetDocumentFormValues()
        {
            ddlIssueDocTypes.SelectedIndex = 0;

            tbComments.Text = string.Empty;

            ddlDocStatus.SelectedIndex = 0;
        }

        private void ResetIssueFormValues()
        {
            ddlVolumes.SelectedIndex = 0;

            tbIssueName.Text = string.Empty;

            ddlStatus.SelectedIndex = 0;

            ResetDocumentFormValues();

            gvDocuments.DataSource = null;

            gvDocuments.DataBind();
        }

        #region DOCUMENTS RELATED

        private void LoadDocGrid()
        {
            gvDocuments.DataSource = IssueBF.IssueDto.Documents;

            gvDocuments.DataBind();
        }

        protected void gvDocuments_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //If the item bound is a row item
            if (e.Row.RowType.Equals(DataControlRowType.DataRow))
            {
                Label lblDocType = (Label) e.Row.FindControl("lblDocType");

                dto.IssueDocument issueDoc = (dto.IssueDocument) e.Row.DataItem;

                if (!(lblDocType == null || issueDoc == null))
                {
                    ListItem liItem = ddlIssueDocTypes.Items.FindByValue(issueDoc.IssueDocTypeId.ToString());

                    if (liItem != null)
                        lblDocType.Text = liItem.Text;
                }
            }
        }

        protected void gvDocuments_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GetSessionValues();

            //Get the document
            dto.IssueDocument issueDoc = IssueBF.IssueDto.Documents[e.RowIndex];

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

            //Update session
            SetSessionValues();

            //Reload the grid
            LoadDocGrid();
        }

        protected void btnAddDoc_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                GetSessionValues();

                try
                {
                    if (tbUploadDocument.HasFile && tbUploadDocument.PostedFile != null)
                    {
                        string fileName = SaveDocToTemp(tbUploadDocument);

                        dto.IssueDocument issueDoc = GetDocValues();

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

        private dto.IssueDocument GetDocValues()
        {
            dto.IssueDocument issueDoc = new dto.IssueDocument();

            issueDoc.IssueDocTypeId = Convert.ToInt32(ddlIssueDocTypes.SelectedValue);

            issueDoc.Comments = tbComments.Text.Trim();

            issueDoc.CreationUserId = UserInfo.UserDto.UserId;

            issueDoc.IsActive = Convert.ToBoolean(Convert.ToInt32(ddlDocStatus.SelectedValue));

            return issueDoc;
        }

        #endregion

        #region EVENT HANDLERS

        protected void btnAddIssue_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                //Init page objects from session
                GetSessionValues();

                IssueBF.IssueDto.ParentVolume.VolumeId = Convert.ToInt32(ddlVolumes.SelectedValue);

                IssueBF.IssueDto.IssueName = tbIssueName.Text.Trim();

                IssueBF.IssueDto.IsActive = Convert.ToBoolean(Convert.ToInt32(ddlStatus.SelectedValue));

                IssueBF.IssueDto.CreationUserId = UserInfo.UserDto.UserId;

                //Persist page objects to session
                SetSessionValues();

                dto.ActionStatus status = IssueBF.AddIssue();

                if (status.IsSuccessful)
                {
                    DeleteTempDocs();

                    ResetIssueFormValues();

                    IssueBF.IssueDto = null;

                    SetSessionValues();
                }
                else
                    lblErrors.Text = GetFormattedMessages(status);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ResetIssueFormValues();

            ResetDocumentFormValues();

            Session[issueSessKey] = null;
        }

        #endregion
    }
}