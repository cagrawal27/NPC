using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using us.naturalproduct.Common;
using us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.UpdateHelpers
{
    public class UserUpdateHelper : BaseDataAccessHelper
    {
        public UserUpdateHelper()
        {
        }

        private DbCommand dbCommand;


        public void InitCommand(Database db, User userDto)
        {
            string sqlCommand = "dbo.spUpdateUser";

            dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "UserId", DbType.Int32, userDto.UserId);

            db.AddInParameter(dbCommand, "FirstName", DbType.String, userDto.FirstName);

            db.AddInParameter(dbCommand, "LastName", DbType.String, userDto.LastName);

            db.AddInParameter(dbCommand, "MiddleInitial", DbType.String, userDto.MiddleInitial);

            db.AddInParameter(dbCommand, "AccountTypeId", DbType.Int32, userDto.AccountType);

            db.AddInParameter(dbCommand, "AccountStatus", DbType.Int32, userDto.AccountStatus);

            db.AddInParameter(dbCommand, "Active", DbType.Int32, ConvBoolToInt32(userDto.IsActive));

            db.AddInParameter(dbCommand, "UpdateUserId", DbType.Int32, userDto.UpdateUserId);
        }

        public Int32 Execute(Database db, DbTransaction txn)
        {
            return db.ExecuteNonQuery(dbCommand, txn);
        }
    }
}