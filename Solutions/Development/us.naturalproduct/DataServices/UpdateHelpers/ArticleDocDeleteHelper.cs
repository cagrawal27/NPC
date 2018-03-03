using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using us.naturalproduct.Common;
using us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.UpdateHelpers
{
    public class ArticleDocDeleteHelper : BaseDataAccessHelper
    {
        public ArticleDocDeleteHelper()
        {
        }


        private DbCommand dbCommand;
        private string sqlCommand = "spDeleteArticleDoc";

        public void InitCommand(Database db, Int32 ArticleId, ArticleDocument artDoc)
        {
            dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "ArticleId", DbType.Int32, ArticleId);
            db.AddInParameter(dbCommand, "ArtDocTypeId", DbType.Int32, artDoc.ArtDocTypeId);
            db.AddInParameter(dbCommand, "DocId", DbType.Int32, artDoc.DocId);
        }

        public void Execute(Database db, DbTransaction txn)
        {
            db.ExecuteNonQuery(dbCommand, txn);
        }
    }
}