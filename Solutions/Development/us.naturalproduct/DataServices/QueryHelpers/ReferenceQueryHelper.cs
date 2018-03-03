using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using us.naturalproduct.Common;

namespace us.naturalproduct.QueryHelpers
{
    public class ReferenceQueryHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DataTable GetCountries()
        {
            // Create the Database object, using the default database service. The
            // default database service is determined through configuration.
            SqlDatabase db = new SqlDatabase(Config.ConnString);

            string sqlCommand = "spGetCountries";

            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            return db.ExecuteDataSet(dbCommand).Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DataTable GetArticleDocTypes()
        {
            // Create the Database object, using the default database service. The
            // default database service is determined through configuration.
            SqlDatabase db = new SqlDatabase(Config.ConnString);

            string sqlCommand = "spGetArticleDocTypes";

            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            return db.ExecuteDataSet(dbCommand).Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DataTable GetIssueDocTypes()
        {
            // Create the Database object, using the default database service. The
            // default database service is determined through configuration.
            SqlDatabase db = new SqlDatabase(Config.ConnString);

            string sqlCommand = "spGetIssueDocTypes";

            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            return db.ExecuteDataSet(dbCommand).Tables[0];
        }
    }
}