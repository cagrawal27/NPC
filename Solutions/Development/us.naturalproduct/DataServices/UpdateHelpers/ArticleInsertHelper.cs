using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using us.naturalproduct.Common;
using us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.UpdateHelpers
{
    public class ArticleInsertHelper : BaseDataAccessHelper
    {
        public ArticleInsertHelper()
        {
        }

        private DbCommand dbCommand;

        public void InitCommand(Database db, Article article)
        {
            string sqlCommand = "dbo.spInsertArticle";

            dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddOutParameter(dbCommand, "ArticleId", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "VolumeIssueId", DbType.Int32, article.IssueDto.VolumeIssueId);
            db.AddInParameter(dbCommand, "Title", DbType.String, article.Title);
            db.AddInParameter(dbCommand, "Authors", DbType.String, article.Authors);
            db.AddInParameter(dbCommand, "Keywords", DbType.String, article.Keywords);
            db.AddInParameter(dbCommand, "Active", DbType.Int32, ConvBoolToInt32(article.IsActive));
            db.AddInParameter(dbCommand, "CreationUserId", DbType.Int32, article.CreationUserId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="txn"></param>
        /// <returns>A Int32 variable containing the IssueArticleId</returns>
        public Int32 Execute(Database db, DbTransaction txn)
        {
            db.ExecuteNonQuery(dbCommand, txn);

            return (Int32) dbCommand.Parameters[0].Value;
        }
    }
}