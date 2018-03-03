using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace us.naturalproduct.QueryHelpers
{
    public class ArticleQueryHelper
    {
        public static DataSet AdminGetArticle(Int32 ArticleId)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCmd = "spAdminGetArticle";

            DbCommand dbCmd = db.GetStoredProcCommand(sqlCmd);

            db.AddInParameter(dbCmd, "ArticleId", DbType.Int32, ArticleId);

            return db.ExecuteDataSet(dbCmd);
        }

        public static DataTable AdminGetArticles(Int32 VolumeIssueId)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCmd = "spAdminGetArticles";

            DbCommand dbCmd = db.GetStoredProcCommand(sqlCmd);

            db.AddInParameter(dbCmd, "VolumeIssueId", DbType.Int32, VolumeIssueId);

            return db.ExecuteDataSet(dbCmd).Tables[0];
        }
    }
}