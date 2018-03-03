using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using us.naturalproduct.Common;
using us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.UpdateHelpers
{
    public class ExceptionInsertHelper : BaseDataAccessHelper
    {
        public static void Execute(ExceptionItem exDto)
        {
            SqlDatabase db = new SqlDatabase(Config.ConnString);

            DbCommand dbCommand;

            string sqlCommand = "dbo.spInsertException";

            dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "Source", DbType.String, exDto.ExceptionDetails.Source);
            db.AddInParameter(dbCommand, "LogDateTime", DbType.DateTime, DateTime.Now.ToString());
            db.AddInParameter(dbCommand, "Message", DbType.String, exDto.ExceptionDetails.Message);
            db.AddInParameter(dbCommand, "Form", DbType.String, exDto.FormVars);
            db.AddInParameter(dbCommand, "QueryString", DbType.String, exDto.QueryString);
            db.AddInParameter(dbCommand, "TargetSite", DbType.String, exDto.ExceptionDetails.TargetSite.ToString());
            db.AddInParameter(dbCommand, "StackTrace", DbType.String, exDto.ExceptionDetails.StackTrace.ToString());
            db.AddInParameter(dbCommand, "Referrer", DbType.String, exDto.Referrer);

            db.ExecuteNonQuery(dbCommand);
        }
    }
}