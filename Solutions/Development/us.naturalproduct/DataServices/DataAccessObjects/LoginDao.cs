using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using us.naturalproduct.Common;
using us.naturalproduct.DataTransferObjects;
using us.naturalproduct.QueryHelpers;

namespace us.naturalproduct.DataAccessObjects
{
    public class LoginDao
    {
        #region Deprecated
        //public Login GetUserCredentials(string emailAddress)
        //{
        //    // Create the Database object, using the default database service. The
        //    // default database service is determined through configuration.
        //    SqlDatabase db = new SqlDatabase(Config.ConnString);

        //    Login loginDto = new Login();

        //    try
        //    {
        //        DataTable tblPwd = UserQueryHelper.GetUserPassword(db, emailAddress);

        //        if (tblPwd.Rows.Count > 0)
        //        {
        //            loginDto.EmailAddress = emailAddress;
        //            loginDto.PasswordHash = (string) tblPwd.Rows[0]["PasswordHash"];
        //            loginDto.PasswordSalt = (string) tblPwd.Rows[0]["PasswordSalt"];
        //        }

        //        return loginDto;
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        Console.WriteLine(sqlEx.ToString());

        //        throw new DataException("An exception occured getting the user's password from the database.", sqlEx);
        //    }
        //}

        //public Login GetIPCredentials(string ipAddress)
        //{
        //    // Create the Database object, using the default database service. The
        //    // default database service is determined through configuration.
        //    SqlDatabase db = new SqlDatabase(Config.ConnString);

        //    Login loginDto = new Login();

        //    try
        //    {
        //        string email = UserQueryHelper.IsIPValid(db, ipAddress);

        //        loginDto.EmailAddress = email;
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        Console.WriteLine(sqlEx.ToString());

        //        throw new DataException("An exception occured while validatin user's IP.", sqlEx);
        //    }

        //    return loginDto;
        //}
        #endregion
    }
}