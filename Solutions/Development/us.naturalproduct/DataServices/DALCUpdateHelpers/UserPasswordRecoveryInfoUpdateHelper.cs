using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using MN.Enterprise.Base;
using MN.Enterprise.Data;
using us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.DALCUpdateHelpers
{
    public class UserPasswordRecoveryInfoUpdateHelper : DALCHelper
    {
        public override DbCommandWrapper InitializeCommand(Database db, DataTransferObject criteria)
        {
            User userDto = criteria as User;

            DbCommandWrapper cw = DbCommandFactory.GetStoredProcCommandWrapper(db, "spUpdateUserPasswordRecoveryInfo");

            cw.AddInParameter("Email", DbType.String, userDto.EmailAddress);

            cw.AddInParameter("SecretQuestion1Id", DbType.Int32, userDto.SecretQuestion1Id);

            cw.AddInParameter("SecretQuestion2Id", DbType.Int32, userDto.SecretQuestion2Id);

            cw.AddInParameter("SecretAnswer1Hash", DbType.String, userDto.SecretAnswer1Hash);

            cw.AddInParameter("SecretAnswer2Hash", DbType.String, userDto.SecretAnswer2Hash);

            return cw;
        }
    }
}