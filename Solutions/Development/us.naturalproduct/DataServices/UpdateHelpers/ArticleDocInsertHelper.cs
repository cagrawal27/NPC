using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using us.naturalproduct.Common;
using us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.UpdateHelpers
{
    public class ArticleDocInsertHelper : BaseDataAccessHelper
    {
        public ArticleDocInsertHelper()
        {
        }

        private DbCommand dbCommand;
        private string sqlCommand = "dbo.spInsertArticleDoc";

        public void InitCommand(Database db, Int32 articleId, ArticleDocument artDoc)
        {
            dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "ArticleId", DbType.Int32, articleId);
            db.AddInParameter(dbCommand, "ArtDocTypeId", DbType.Int32, artDoc.ArtDocTypeId);
            db.AddInParameter(dbCommand, "Data", DbType.Binary, artDoc.Data);
            db.AddInParameter(dbCommand, "FileName", DbType.String, artDoc.FileName);
            db.AddInParameter(dbCommand, "FileSizeKB", DbType.Int32, artDoc.CalculateFileSizeInKB());
            db.AddInParameter(dbCommand, "Comments", DbType.String, artDoc.Comments);
            db.AddInParameter(dbCommand, "Active", DbType.Int32, ConvBoolToInt32(artDoc.IsActive));
            db.AddInParameter(dbCommand, "CreationUserId", DbType.Int32, artDoc.CreationUserId);
        }

        public void Execute(Database db, DbTransaction txn)
        {
            db.ExecuteNonQuery(dbCommand, txn);
        }
    }
}