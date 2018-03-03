using System;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using MN.Enterprise.Base;
using MN.Enterprise.Data;
using us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.DALCQueryHelpers
{
    public class UserExistsHelper: DALCHelper
    {
        public override DbCommandWrapper InitializeCommand(Database db, DataTransferObject criteria)
        {
            User userDto = criteria as User;

            DbCommandWrapper cw = DbCommandFactory.GetStoredProcCommandWrapper(db, "spExistsUser");

            cw.AddInParameter("Email", DbType.String, userDto.EmailAddress);

            return cw;
        }
        
    }
}
