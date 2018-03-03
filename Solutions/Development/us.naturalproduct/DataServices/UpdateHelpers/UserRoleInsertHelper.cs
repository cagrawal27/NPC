using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using us.naturalproduct.Common;

namespace us.naturalproduct.UpdateHelpers
{
    public class UserRoleInsertHelper : BaseDataAccessHelper
    {
        public UserRoleInsertHelper()
        {
        }

        private DbCommand dbCommand;

        public void InitCommand(Database db, Int32 roleId, Int32 userId)
        {
            string sqlCommand = "dbo.spInsertUserRole";

            dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "RoleId", DbType.Int32, roleId);
            db.AddInParameter(dbCommand, "UserId", DbType.Int32, userId);
        }

        public Int32 Execute(Database db, DbTransaction txn)
        {
            return db.ExecuteNonQuery(dbCommand, txn);
        }
    }
}