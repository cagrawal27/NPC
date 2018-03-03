using System;
using System.Data;
using System.Data.SqlClient;
using us.naturalproduct.DataTransferObjects;
using us.naturalproduct.QueryHelpers;

namespace us.naturalproduct.DataAccessObjects
{
    public class UserDao
    {
        #region deprecated

        //public static User GetUserInfo(string email)
        //{
        //    User userDto = new User();

        //    try
        //    {
        //        DataSet dSetUserInfo = UserQueryHelper.GetUserInfo(email);

        //        //3 resultsets should be returned: User info, roles and subscriptions
        //        if (dSetUserInfo.Tables.Count.Equals(2))
        //        {
        //            //Get information returned from the users table
        //            if (dSetUserInfo.Tables[0].Rows.Count.Equals(1))
        //            {
        //                DataRow dr = dSetUserInfo.Tables[0].Rows[0];

        //                userDto.EmailAddress = email;

        //                userDto.UserId = (Int32) dr["UserId"];

        //                userDto.FirstName = (string) dr["FirstName"];

        //                userDto.LastName = (string) dr["LastName"];

        //                userDto.MiddleInitial = (string) dr["MiddleInitial"];

        //                userDto.AccountStatus = (Int32) dr["AccountStatus"];

        //                userDto.AccountType = (Int32) dr["AccountTypeId"];
        //            }

        //            //Get roles
        //            if (dSetUserInfo.Tables[1].Rows.Count > 0)
        //            {
        //                userDto.Roles = new string[dSetUserInfo.Tables[1].Rows.Count];

        //                for (int j = 0; j < dSetUserInfo.Tables[1].Rows.Count; j++)
        //                {
        //                    DataRow dr = dSetUserInfo.Tables[1].Rows[j];

        //                    userDto.Roles[j] = ((Int32) dr["RoleId"]).ToString();
        //                }
        //            }

        //            //Determine if the user has subscriptions
        //            //if (dSetUserInfo.Tables[2].Rows.Count > 0)
        //            //{
        //            //    DataRow dr = dSetUserInfo.Tables[2].Rows[0];
        //            //    userDto.HasValidSubscription = Convert.ToBoolean((Int32) dr["IsUserSubscriber"]);
        //            //}
        //        }
        //        else
        //            throw new DataException("An exception occured getting user information.  The number of tables returned does not match the number of tables expected.");

        //        return userDto;
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        Console.WriteLine(sqlEx.ToString());

        //        throw new DataException("An exception occured getting user information.", sqlEx);
        //    }
        //}

        //public static User GetUserInfo(int UserId)
        //{
        //    User userDto = new User();

        //    try
        //    {
        //        DataSet dSetUserInfo = UserQueryHelper.GetUserInfo(UserId);

        //        //1 resultset should be returned: User info
        //        if (dSetUserInfo.Tables.Count.Equals(1))
        //        {
        //            //Get information returned from the users table
        //            if (dSetUserInfo.Tables[0].Rows.Count.Equals(1))
        //            {
        //                DataRow dr = dSetUserInfo.Tables[0].Rows[0];

        //                userDto.UserId = (Int32) dr["UserId"];

        //                userDto.EmailAddress = (string) dr["Email"];

        //                userDto.FirstName = (string) dr["FirstName"];

        //                userDto.LastName = (string) dr["LastName"];

        //                userDto.MiddleInitial = (string) dr["MiddleInitial"];

        //                userDto.AccountStatus = (Int32) dr["AccountStatus"];

        //                userDto.AccountType = (Int32) dr["AccountTypeId"];

        //                userDto.IsActive = (bool) dr["Active"];
        //            }
        //        }

        //        return userDto;
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        Console.WriteLine(sqlEx.ToString());

        //        throw new DataException("An exception occured getting user information.", sqlEx);
        //    }
        //}

        #endregion
    }
}