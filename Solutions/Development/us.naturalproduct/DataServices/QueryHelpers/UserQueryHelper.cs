using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using us.naturalproduct.Common;

namespace us.naturalproduct.QueryHelpers
{
    public class UserQueryHelper
    {

        #region Deprecated
		//public static bool DoesUserExist(Database db, string emailAddress)
        //{
        //    string sqlCommand = "dbo.spDoesUserExist";

        //    DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

        //    db.AddParameter(dbCommand, "ReturnValue", DbType.Int16, ParameterDirection.ReturnValue, null,
        //                    DataRowVersion.Default, null);

        //    db.AddInParameter(dbCommand, "Email", DbType.String, emailAddress);

        //    db.ExecuteNonQuery(dbCommand);

        //    Int32 returnCode = (Int32) dbCommand.Parameters[0].Value;

        //    //-1 -> User Exists
        //    // 0 -> User Does not exist
        //    return returnCode.Equals(-1);
        //}

        //public static DataTable GetUserPassword(Database db, string emailAddress)
        //{
        //    string sqlCmd = "dbo.spGetUserPasswordAndSalt";

        //    DbCommand dbCmd = db.GetStoredProcCommand(sqlCmd);

        //    db.AddInParameter(dbCmd, "Email", DbType.String, emailAddress);

        //    return db.ExecuteDataSet(dbCmd).Tables[0];
        //}

        //public static DataSet GetUserInfo(string emailAddress)
        //{
        //    // Create the Database object, using the default database service. The
        //    // default database service is determined through configuration.
        //    SqlDatabase db = new SqlDatabase(Config.ConnString);

        //    string sqlCmd = "dbo.spGetUserInfo";

        //    DbCommand dbCmd = db.GetStoredProcCommand(sqlCmd);

        //    db.AddInParameter(dbCmd, "Email", DbType.String, emailAddress);

        //    return db.ExecuteDataSet(dbCmd);
        //}

        //public static DataSet GetUserInfo(int UserId)
        //{
        //    // Create the Database object, using the default database service. The
        //    // default database service is determined through configuration.
        //    SqlDatabase db = new SqlDatabase(Config.ConnString);

        //    string sqlCmd = "dbo.spGetUserInfoById";

        //    DbCommand dbCmd = db.GetStoredProcCommand(sqlCmd);

        //    db.AddInParameter(dbCmd, "UserId", DbType.Int32, UserId);

        //    return db.ExecuteDataSet(dbCmd);
        //}

        //public static string IsIPValid(Database db, string ipAddress)
        //{
        //    string sqlCmd = "dbo.spDoesUserIPAddressExist";

        //    DbCommand dbCmd = db.GetStoredProcCommand(sqlCmd);

        //    db.AddInParameter(dbCmd, "IPAddress", DbType.String, ipAddress);

        //    db.AddOutParameter(dbCmd, "Email", DbType.String, 50);

        //    db.ExecuteNonQuery(dbCmd);

        //    string email = (string) db.GetParameterValue(dbCmd, "Email");

        //    return email;
        //}
 
	#endregion    
    }
}