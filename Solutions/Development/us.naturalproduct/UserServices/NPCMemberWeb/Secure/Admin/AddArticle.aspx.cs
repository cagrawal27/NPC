using System;
using System.Diagnostics;
using System.IO;
using System.Web.UI.WebControls;
using us.naturalproduct.BusinessServices.BusinessFacades;
using us.naturalproduct.web.Masters;
using dto = us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.web
{
    public partial class AddArticle : BaseAdminPage
    {
        private static ListItem liDefault = new ListItem("-- Select --", "");
        private ArticleFacade ArticleBF;
        private static string articleSessKey = "articleDto";

        protected void Page_Load(object sender, EventArgs e)
        {
            Debug.WriteLine("In AddArticle.Page_Load");

            //Set title
            ((Secure) Master).PageTitle = "Add Article";

            //Init business layer
            ArticleBF = new ArticleFacade();

            if (!Page.IsPostBack)
            {
                LoadLists();

                Session[articleSessKey] = null;
            }
        }

        #region SESSION

        private void GetSessionValues()
        {
            //Get item from session
            dto.Article articleDto = Session[articleSessKey] as dto.Article;

            //If empty then init
            if (articleDto == null)
                ArticleBF.ArticleDto = new dto.Article();
            else
                ArticleBF.ArticleDto = articleDto;
        }

        private void SetSessionValues()
        {
            //If the value in the page is null then init
            if (ArticleBF.ArticleDto == null)
                Session[articleSessKey] = new dto.Article();
            else
                Session[articleSessKey] = ArticleBF.ArticleDto;
        }

        #endregion

        private void LoadLists()
        {
            ddlArticleDocTypes.DataSource = ReferenceDataFacade.GetArticleDocTypes();
            ddlArticleDocTypes.DataTextField = "ArtDocTypeDescription";
            ddlArticleDocTypes.DataValueField = "ArtDocTypeId";
            ddlArticleDocTypes.DataBind();
            ddlArticleDocTypes.Items.Insert(0, liDefault);
        }

        private void ResetArticleFormValues()
        {
            ddlVolumes.SelectedIndex = 0;

            ddlIssues.SelectedIndex = 0;

            tbTitle.Text = string.Empty;

            tbAuthors.Text = string.Empty;

            tbKeywords.Text = string.Empty;

            ResetDocumentFormValues();

            gvDocuments.DataSource = null;

            gvDocuments.DataBind();
        }

        private void ResetDocumentFormValues()
        {
            ddlArticleDocTypes.SelectedIndex = 0;

            tbComments.Text = string.Empty;

            ddlDocStatus.SelectedIndex = 0;
        }

        #region DOCUMENTS RELATED

        protected void btnAddDoc_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                //Init page objects from session
                GetSessionValues();

                try
                {
                    if (tbUploadDocument.HasFile && tbUploadDocument.PostedFile != null)
                    {
                        string fileName = SaveDocToTemp(tbUploadDocument);

                        dto.ArticleDocument artDoc = GetDocValues();

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

        private dto.ArticleDocument GetDocValues()
        {
            dto.ArticleDocument artDoc = new dto.ArticleDocument();

            artDoc.ArtDocTypeId = Convert.ToInt32(ddlArticleDocTypes.SelectedValue);

            artDoc.Comments = tbComments.Text.Trim();

            artDoc.CreationUserId = UserInfo.UserDto.UserId;

            artDoc.IsActive = Convert.ToBoolean(Convert.ToInt32(ddlDocStatus.SelectedValue));

            return artDoc;
        }

        private void LoadDocGrid()
        {
            //dgDocuments.DataSource = ArticleBF.ArticleDto.Documents;

            //dgDocuments.DataBind();

            gvDocuments.DataSource = ArticleBF.ArticleDto.Documents;

            gvDocuments.DataBind();
        }

        protected void gvDocuments_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //If the item bound is a row item
            if (e.Row.RowType.Equals(DataControlRowType.DataRow))
            {
                Label lblDocType = (Label) e.Row.FindControl("lblDocType");

                dto.ArticleDocument artDoc = (dto.ArticleDocument) e.Row.DataItem;

                if (!(lblDocType == null || artDoc == null))
                {
                    ListItem liItem = ddlArticleDocTypes.Items.FindByValue(artDoc.ArtDocTypeId.ToString());

                    if (liItem != null)
                        lblDocType.Text = liItem.Text;
                }
            }
        }

        protected void gvDocuments_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GetSessionValues();

            //Get the document
            dto.ArticleDocument artDoc = ArticleBF.ArticleDto.Documents[e.RowIndex];

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

            //Update session
            SetSessionValues();

            //Reload the grid
            LoadDocGrid();
        }

        #endregion

        #region EVENT HANDLERS

        protected void btnAddArticle_OnClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                //Init page objects from session
                GetSessionValues();

                ArticleBF.ArticleDto.IssueDto.VolumeIssueId = Convert.ToInt32(ddlIssues.SelectedValue);

                ArticleBF.ArticleDto.Title = tbTitle.Text.Trim();

                ArticleBF.ArticleDto.Authors = tbAuthors.Text.Trim();

                ArticleBF.ArticleDto.Keywords = tbKeywords.Text.Trim();

                ArticleBF.ArticleDto.IsActive = Convert.ToBoolean(Convert.ToInt32(ddlStatus.SelectedValue));

                ArticleBF.ArticleDto.CreationUserId = UserInfo.UserDto.UserId;

                //Persist page objects to session
                SetSessionValues();

                dto.ActionStatus status = ArticleBF.AddArticle();

                if (status.IsSuccessful)
                {
                    DeleteTempDocs();

                    ResetArticleFormValues();
                }
                else
                    lblErrors.Text = GetFormattedMessages(status);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ResetArticleFormValues();

            ResetDocumentFormValues();

            Session[articleSessKey] = null;
        }

        #endregion
    }
}