using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using MN.Enterprise.Base;
using MN.Enterprise.Data;
using us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.DALCHelpers
{
    public class UserUpdateHelper : DALCHelper
    {
        public override DbCommandWrapper InitializeCommand(Database db, DataTransferObject criteria)
        {
            User userDto = criteria as User;

            DbCommandWrapper cw = DbCommandFactory.GetStoredProcCommandWrapper(db, "spUpdateUser");

            cw.AddInParameter("UserId", DbType.Int32, userDto.UserId);

            cw.AddInParameter("FirstName", DbType.String, userDto.FirstName);

            cw.AddInParameter("LastName", DbType.String, userDto.LastName);

            cw.AddInParameter("MiddleInitial", DbType.String, userDto.MiddleInitial);

            cw.AddInParameter("AccountTypeId", DbType.Int32, userDto.AccountType);

            cw.AddInParameter("AccountStatus", DbType.Int32, userDto.AccountStatus);

            cw.AddInParameter("Active", DbType.Boolean, userDto.IsActive);

            cw.AddInParameter("UpdateUserId", DbType.Int32, userDto.UpdateUserId);

            return cw;
        }
    }
}