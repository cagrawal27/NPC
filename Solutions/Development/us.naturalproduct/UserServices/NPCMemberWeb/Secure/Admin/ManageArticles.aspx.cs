using System;
using System.IO;
using System.Web.UI.WebControls;
using us.naturalproduct.BusinessServices.BusinessFacades;
using us.naturalproduct.DataTransferObjects;
using us.naturalproduct.web.Masters;

namespace us.naturalproduct.web
{
    public partial class ManageArticles : BaseAdminPage
    {
        private ArticleFacade ArticleBF;
        private static ListItem liDefault = new ListItem("-- Select --", "");
        private static string artSessKey = "ManageArticleDto";

        protected void Page_Load(object sender, EventArgs e)
        {
            //Set title
            ((Secure) Master).PageTitle = "Manage Articles";

            ArticleBF = new ArticleFacade();

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

        protected void ddlVolumes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlVolumes.SelectedIndex > 0)
            {
                Int32 VolumeId = Convert.ToInt32(ddlVolumes.SelectedValue);

                ddlIssues.DataSource = IssueFacade.AdminGetIssues(VolumeId);

                ddlIssues.DataBind();

                ddlIssues.Items.Insert(0, liDefault);
                ddlArticles.Items.Clear();
            }
        }

        protected void ddlIssues_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlIssues.SelectedIndex > 0)
            {
                Int32 VolumeIssueId = Convert.ToInt32(ddlIssues.SelectedValue);

                ddlArticles.DataSource = ArticleFacade.AdminGetArticles(VolumeIssueId);

                ddlArticles.DataBind();

                ddlArticles.Items.Insert(0, liDefault);
            }
        }

        protected void ddlArticles_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int32 ArticleId = Convert.ToInt32(ddlArticles.SelectedValue);

            ArticleBF.ArticleDto = ArticleFacade.AdminGetArticle(ArticleId);

            SetPageValues();

            SetSessionValues();
        }

        private void SetPageValues()
        {
            tbTitle.Text = ArticleBF.ArticleDto.Title;

            tbAuthors.Text = ArticleBF.ArticleDto.Authors;

            tbKeywords.Text = ArticleBF.ArticleDto.Keywords;

            tbPageNumber.Text = ArticleBF.ArticleDto.PageNumber.ToString();

            ddlStatus.SelectedValue = ArticleBF.ArticleDto.IsActive ? "1" : "0";

            gvDocuments.DataSource = ArticleBF.ArticleDto.Documents;

            gvDocuments.DataBind();
        }

        private void GetPageValues()
        {
            ArticleBF.ArticleDto.Title = tbTitle.Text.Trim();

            ArticleBF.ArticleDto.Authors = tbAuthors.Text.Trim();

            ArticleBF.ArticleDto.Keywords = tbKeywords.Text.Trim();

            ArticleBF.ArticleDto.PageNumber = Convert.ToInt32(tbPageNumber.Text.Trim());

            ArticleBF.ArticleDto.IsActive = Convert.ToBoolean(Convert.ToInt32(ddlStatus.SelectedValue));
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            //Get session values
            GetSessionValues();

            //Update page Value object
            GetPageValues();

            //Update database
            ActionStatus status = ArticleBF.UpdateArticle();

            if (status.IsSuccessful)
            {
                DeleteTempDocs();

                ArticleBF.ArticleDto = null;

                SetSessionValues();

                lblStatus.Text = GetFormattedMessages(status);
            }

            //Update session
            SetSessionValues();
        }

        #region SESSION

        private void GetSessionValues()
        {
            //Get item from session
            Article artDto = Session[artSessKey] as Article;

            //If empty then init
            if (artDto == null)
                ArticleBF.ArticleDto = new Article();
            else
                ArticleBF.ArticleDto = artDto;
        }

        private void SetSessionValues()
        {
            //If the value in the page is null then init
            if (ArticleBF.ArticleDto == null)
                Session[artSessKey] = new Article();
            else
                Session[artSessKey] = ArticleBF.ArticleDto;
        }

        #endregion

        #region DOCUMENTS

        private void LoadDocGrid()
        {
            gvDocuments.DataSource = ArticleBF.ArticleDto.Documents;

            gvDocuments.DataBind();
        }

        private void ResetDocumentFormValues()
        {
            ddlArtDocTypes.SelectedIndex = 0;

            tbComments.Text = string.Empty;

            ddlDocStatus.SelectedIndex = 0;
        }

        private ArticleDocument GetDocValues()
        {
            ArticleDocument artDoc = new ArticleDocument();

            artDoc.ArtDocTypeId = Convert.ToInt32(ddlArtDocTypes.SelectedValue);

            artDoc.Comments = tbComments.Text.Trim();

            artDoc.CreationUserId = UserInfo.UserDto.UserId;

            artDoc.IsActive = Convert.ToBoolean(Convert.ToInt32(ddlDocStatus.SelectedValue));

            return artDoc;
        }

        protected void gvDocuments_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.DataRow))
            {
                if (ArticleBF.ArticleDto.Documents[e.Row.RowIndex].IsDeleted)
                    e.Row.Visible = false;

                Label lblDocType = (Label) e.Row.FindControl("lblDocType");

                ArticleDocument artDoc = (ArticleDocument) e.Row.DataItem;

                if (!(lblDocType == null || artDoc == null))
                {
                    ListItem liItem = ddlArtDocTypes.Items.FindByValue(artDoc.ArtDocTypeId.ToString());

                    if (liItem != null)
                        lblDocType.Text = liItem.Text;
                }
            }
        }

        protected void gvDocuments_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //Populate dto from session
            GetSessionValues();

            if (ArticleBF.ArticleDto.Documents[e.RowIndex].IsOld)
            {
                //Mark for deletion from database
                ArticleBF.ArticleDto.Documents[e.RowIndex].IsDeleted = true;
            }
            else
            {
                //Get the document
                ArticleDocument artDoc = ArticleBF.ArticleDto.Documents[e.RowIndex];

                try
                {
                    File.Delete(artDoc.FullFileName);
                }
                catch (IOException ioEx)
                {
                    DisplayPopup("An error occured deleting the document.");
                }

                //Remove from documents list
                ArticleBF.ArticleDto.Documents.RemoveAt(e.RowIndex);
            }

            //Update session
            SetSessionValues();

            //Reload the grid
            LoadDocGrid();
        }

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

                        ArticleDocument artDoc = GetDocValues();

                        artDoc.FullFileName = fileName;

                        ArticleBF.ArticleDto.Documents.Add(artDoc);

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

        #endregion
    }
}