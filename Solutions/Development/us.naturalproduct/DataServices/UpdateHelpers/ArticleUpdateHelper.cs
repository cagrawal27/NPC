using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using us.naturalproduct.Common;
using us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.UpdateHelpers
{
    public class ArticleUpdateHelper : BaseDataAccessHelper
    {
        public ArticleUpdateHelper()
        {
        }

        private DbCommand dbCommand;

        public void InitCommand(Database db, Article article)
        {
            string sqlCommand = "dbo.spUpdateArticle";

            dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "ArticleId", DbType.Int32, article.ArticleId);

            db.AddInParameter(dbCommand, "Title", DbType.String, article.Title);

            db.AddInParameter(dbCommand, "Authors", DbType.String, article.Authors);

            db.AddInParameter(dbCommand, "Keywords", DbType.String, article.Keywords);

            db.AddInParameter(dbCommand, "PageNumber", DbType.Int32, article.PageNumber);

            db.AddInParameter(dbCommand, "Active", DbType.Int32, ConvBoolToInt32(article.IsActive));

            db.AddInParameter(dbCommand, "UpdateUserId", DbType.Int32, article.UpdateUserId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="txn"></param>
        /// <returns>A Int32 variable containing the VolumeIssueId</returns>
        public void Execute(Database db, DbTransaction txn)
        {
            db.ExecuteNonQuery(dbCommand, txn);
        }
    }
}