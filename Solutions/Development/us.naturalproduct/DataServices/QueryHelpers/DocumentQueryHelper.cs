using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using us.naturalproduct.Common;

namespace us.naturalproduct.QueryHelpers
{
    public class DocumentQueryHelper
    {
        public void Test()
        {
            //
        }

        public static IDataReader GetDocument(Int32 docId)
        {
            // Create the Database object, using the default database service. The
            // default database service is determined through configuration.
            SqlDatabase db = new SqlDatabase(Config.ConnString);

            string sqlCmd = "dbo.spGetDocument";

            DbCommand dbCmd = db.GetStoredProcCommand(sqlCmd);

            db.AddInParameter(dbCmd, "DocId", DbType.Int32, docId);

            return db.ExecuteReader(dbCmd);
        }
    }
}