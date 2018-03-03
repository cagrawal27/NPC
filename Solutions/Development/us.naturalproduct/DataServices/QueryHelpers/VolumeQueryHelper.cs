using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using us.naturalproduct.Common;

namespace us.naturalproduct.QueryHelpers
{
    public class VolumeQueryHelper
    {
        public static DataTable GetActiveVolumes()
        {
            // Create the Database object, using the default database service. The
            // default database service is determined through configuration.
            SqlDatabase db = new SqlDatabase(Config.ConnString);

            string sqlCmd = "dbo.spGetActiveVolumes";

            DbCommand dbCmd = db.GetStoredProcCommand(sqlCmd);

            return db.ExecuteDataSet(dbCmd).Tables[0];
        }

        public static DataTable AdminGetVolumes()
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCmd = "dbo.spAdminGetVolumes";

            DbCommand dbCmd = db.GetStoredProcCommand(sqlCmd);

            return db.ExecuteDataSet(dbCmd).Tables[0];
        }
    }
}