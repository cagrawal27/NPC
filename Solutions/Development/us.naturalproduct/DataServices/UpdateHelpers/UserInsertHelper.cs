using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using us.naturalproduct.Common;
using us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.UpdateHelpers
{
    public class UserInsertHelper : BaseDataAccessHelper
    {
        public UserInsertHelper()
        {
        }

        private DbCommand dbCommand;

        public void InitCommand(Database db, Registration regDto)
        {
            string sqlCommand = "dbo.spInsertUser";

            dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddParameter(dbCommand, "ReturnValue", DbType.Int16, ParameterDirection.ReturnValue, null,
                            DataRowVersion.Default, null);

            db.AddInParameter(dbCommand, "Email", DbType.String, regDto.EmailAddress);
            db.AddInParameter(dbCommand, "PasswordHash", DbType.String, regDto.PasswordHash);
            db.AddInParameter(dbCommand, "PasswordSalt", DbType.String, regDto.PasswordSalt);
            db.AddInParameter(dbCommand, "FirstName", DbType.String, regDto.FirstName);
            db.AddInParameter(dbCommand, "LastName", DbType.String, regDto.LastName);
            db.AddInParameter(dbCommand, "MiddleInitial", DbType.String, regDto.MiddleInitial);
            db.AddInParameter(dbCommand, "SecretQuestion1Id", DbType.Int32, regDto.SecretQuestion1);
            db.AddInParameter(dbCommand, "SecretQuestion2Id", DbType.Int32, regDto.SecretQuestion2);
            db.AddInParameter(dbCommand, "SecretAnswer1Hash", DbType.String, regDto.SecretAnswer1Hash);
            db.AddInParameter(dbCommand, "SecretAnswer2Hash", DbType.String, regDto.SecretAnswer2Hash);
            db.AddInParameter(dbCommand, "AccountTypeId", DbType.Int32, regDto.AccountType);
            db.AddInParameter(dbCommand, "AccountStatus", DbType.Int32, regDto.AccountStatus);
            db.AddInParameter(dbCommand, "Active", DbType.Int32, ConvBoolToInt32(regDto.IsActive));
            db.AddInParameter(dbCommand, "CreationUserId", DbType.Int32, regDto.CreationUserId);
        }

        public Int32 Execute(Database db, DbTransaction txn)
        {
            db.ExecuteNonQuery(dbCommand, txn);

            return (Int32) dbCommand.Parameters[0].Value;
        }
    }
}