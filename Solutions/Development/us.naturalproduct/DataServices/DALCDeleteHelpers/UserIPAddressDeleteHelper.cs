using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using MN.Enterprise.Base;
using MN.Enterprise.Data;
using us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.DALCDeleteHelpers
{
    public class UserIPAddressDeleteHelper: DALCHelper
    {
        public override DbCommandWrapper InitializeCommand(Database db, DataTransferObject criteria)
        {
            UserIPAddress userDto = criteria as UserIPAddress;

            DbCommandWrapper cw = DbCommandFactory.GetStoredProcCommandWrapper(db, "spDeleteUserIPAddress");

            cw.AddInParameter("UserIPId", DbType.Int32, userDto.UserIPId);

            return cw;
        }

    }
}
