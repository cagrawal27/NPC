using System;
using System.Data;
using us.naturalproduct.DataAccessObjects;
using us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.BusinessServices.BusinessFacades
{
    public class ArticleFacade
    {
        private Article articleDto;

        public Article ArticleDto
        {
            get { return this.articleDto; }
            set { this.articleDto = value; }
        }

        public ActionStatus AddArticle()
        {
            ActionStatus status = ArticleDao.AddArticle(articleDto);

            return status;
        }

        public static Article AdminGetArticle(Int32 ArticleId)
        {
            return ArticleDao.AdminGetArticle(ArticleId);
        }

        public static DataTable AdminGetArticles(Int32 VolumeIssueId)
        {
            return ArticleDao.AdminGetArticles(VolumeIssueId);
        }

        public ActionStatus UpdateArticle()
        {
            ActionStatus status = ArticleDao.UpdateArticle(articleDto);

            if (status.IsSuccessful)
                status.Messages.Add(new ActionMessage(false, 1, "Successfully updated article."));
            else
                status.Messages.Add(new ActionMessage(true, 1, "Failed to update article."));

            return status;
        }
    }
}