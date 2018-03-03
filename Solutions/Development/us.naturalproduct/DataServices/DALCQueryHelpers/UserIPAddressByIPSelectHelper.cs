using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using MN.Enterprise.Base;
using MN.Enterprise.Data;
using us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.DALCQueryHelpers
{
    public class UserIPAddressByIPSelectHelper: DALCHelper
    {
        public UserIPAddressByIPSelectHelper()
        {
        }

        public override DbCommandWrapper InitializeCommand(Database db, DataTransferObject criteria)
        {
            UserIPAddress userIPAddressDto = criteria as UserIPAddress;

            DbCommandWrapper cw = DbCommandFactory.GetStoredProcCommandWrapper(db, "spGetUserIPAddressByIP");

            cw.AddInParameter("@IPOctet1", DbType.Int32, userIPAddressDto.BeginAddress.Octet1);

            cw.AddInParameter("@IPOctet2", DbType.Int32, userIPAddressDto.BeginAddress.Octet2);

            cw.AddInParameter("@IPOctet3", DbType.Int32, userIPAddressDto.BeginAddress.Octet3);

            cw.AddInParameter("@IPOctet4", DbType.Int32, userIPAddressDto.BeginAddress.Octet4);

            return cw;            
        }

        public override DataTransferObject ConvertResultsDto(DbCommandWrapper cw, IDataReader reader)
        {
            UserIPAddress userIPAddressDto = null;
            
            if (reader.Read())
            {
                userIPAddressDto = new UserIPAddress();

                userIPAddressDto.UserId = Convert.ToInt32(reader["UserId"]);
            }

            return userIPAddressDto;
        }
    }
}
