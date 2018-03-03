using System;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using MN.Enterprise.Base;
using MN.Enterprise.Data;
using us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.DALCQueryHelpers
{
    public class UserIPAddressExistsHelper: DALCHelper
    {
        
        public override DbCommandWrapper InitializeCommand(Database db, DataTransferObject criteria)
        {
            UserIPAddress userIPAddressDto = criteria as UserIPAddress;

            DbCommandWrapper cw = DbCommandFactory.GetStoredProcCommandWrapper(db, "spAdminExistsUserIPAddress");

            cw.AddInParameter("IPOctet1Begin", DbType.Int32, userIPAddressDto.BeginAddress.Octet1);

            cw.AddInParameter("IPOctet2Begin", DbType.Int32, userIPAddressDto.BeginAddress.Octet2);

            cw.AddInParameter("IPOctet3Begin", DbType.Int32, userIPAddressDto.BeginAddress.Octet3);

            cw.AddInParameter("IPOctet3End", DbType.Int32, userIPAddressDto.EndAddress.Octet3);

            cw.AddInParameter("IPOctet4Begin", DbType.Int32, userIPAddressDto.BeginAddress.Octet4);

            cw.AddInParameter("IPOctet4End", DbType.Int32, userIPAddressDto.EndAddress.Octet4);

            return cw;
        }

    }
}
