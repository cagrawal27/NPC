using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using us.naturalproduct.Common;

namespace us.naturalproduct.QueryHelpers
{
    public class IssueQueryHelper
    {
        public static DataSet GetActiveIssues(Int32 VolumeId)
        {
            // Create the Database object, using the default database service. The
            // default database service is determined through configuration.
            SqlDatabase db = new SqlDatabase(Config.ConnString);

            string sqlCmd = "dbo.spGetActiveIssues";

            DbCommand dbCmd = db.GetStoredProcCommand(sqlCmd);

            db.AddInParameter(dbCmd, "VolumeId", DbType.Int32, VolumeId);

            return db.ExecuteDataSet(dbCmd);
        }

        public static DataSet AdminGetIssue(Int32 VolumeIssueId)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCmd = "spAdminGetIssue";

            DbCommand dbCmd = db.GetStoredProcCommand(sqlCmd);

            db.AddInParameter(dbCmd, "VolumeIssueId", DbType.Int32, VolumeIssueId);

            return db.ExecuteDataSet(dbCmd);
        }

        public static DataTable AdminGetIssues(Int32 VolumeId)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCmd = "spAdminGetIssues";

            DbCommand dbCmd = db.GetStoredProcCommand(sqlCmd);

            db.AddInParameter(dbCmd, "VolumeId", DbType.Int32, VolumeId);

            return db.ExecuteDataSet(dbCmd).Tables[0];
        }
    }
}