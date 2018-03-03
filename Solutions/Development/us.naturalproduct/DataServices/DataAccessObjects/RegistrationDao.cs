using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using us.naturalproduct.Common;
using us.naturalproduct.DataAccessLogicComponents;
using us.naturalproduct.DataTransferObjects;
using us.naturalproduct.QueryHelpers;
using us.naturalproduct.UpdateHelpers;

namespace us.naturalproduct.DataAccessObjects
{
    public class RegistrationDao
    {
        public ActionStatus RegisterUser(Registration regDto)
        {
            // Create the Database object, using the default database service. The
            // default database service is determined through configuration.
            SqlDatabase db = new SqlDatabase(Config.ConnString);

            ActionStatus status = new ActionStatus();

            Int32 userId;

            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();

                DbTransaction txn = null;

                try
                {
                    UserDalc dalc = new UserDalc();

                    User inUserDto = new User();

                    inUserDto.EmailAddress = regDto.EmailAddress;

                    if (dalc.Exists(inUserDto))
                    {
                        status.Messages.Add(new ActionMessage(true, 1, "The email address you used already exists."));
                    }
                    else
                    {
                        txn = connection.BeginTransaction();

                        UserInsertHelper usrHlpr = new UserInsertHelper();

                        usrHlpr.InitCommand(db, regDto);

                        userId = usrHlpr.Execute(db, txn);

                        AddressInsertHelper addrHlpr = new AddressInsertHelper();

                        addrHlpr.InitCommand(db, regDto, userId);

                        addrHlpr.Execute(db, txn);

                        UserRoleInsertHelper roleHlpr = new UserRoleInsertHelper();

                        roleHlpr.InitCommand(db, regDto.DefaultRoleId, userId);

                        roleHlpr.Execute(db, txn);

                        status.IsSuccessful = true;

                        // Commit the transaction.
                        txn.Commit();
                    }
                }
                catch (SqlException sqlEx)
                {
                    // Roll back the transaction. 
                    txn.Rollback();

                    Console.WriteLine(sqlEx.ToString());

                    throw new DataException("An exception occured adding a user to the database.", sqlEx);
                }
                finally
                {
                    connection.Close();
                }
            }


            return status;
        }
    }
}