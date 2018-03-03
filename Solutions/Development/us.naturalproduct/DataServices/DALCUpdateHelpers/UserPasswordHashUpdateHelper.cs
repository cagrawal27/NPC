using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using MN.Enterprise.Base;
using MN.Enterprise.Data;
using us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.DALCUpdateHelpers
{
    public class UserPasswordHashUpdateHelper : DALCHelper
    {
        public override DbCommandWrapper InitializeCommand(Database db, DataTransferObject criteria)
        {
            User userDto = criteria as User;

            DbCommandWrapper cw = DbCommandFactory.GetStoredProcCommandWrapper(db, "spUpdateUserPasswordHash");

            cw.AddInParameter("Email", DbType.String, userDto.EmailAddress);

            cw.AddInParameter("PasswordHash", DbType.String, userDto.PasswordHash);

            cw.AddInParameter("AccountStatus", DbType.Int32, userDto.AccountStatus);

            return cw;
        }
    }
}