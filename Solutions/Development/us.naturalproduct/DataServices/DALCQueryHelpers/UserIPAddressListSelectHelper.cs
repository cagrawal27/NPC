using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using MN.Enterprise.Base;
using MN.Enterprise.Data;
using us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.DALCQueryHelpers
{
    public class UserIPAddressListSelectHelper: DALCHelper<UserIPAddress>
    {
        public override DbCommandWrapper InitializeCommand(Database db, DataTransferObject criteria)
        {
            UserIPAddress userIPAddressDto = criteria as UserIPAddress;

            //Create a database command object within which T-SQL commands can 
            //be executed.
            DbCommandWrapper cw = DbCommandFactory.GetStoredProcCommandWrapper(db, "spAdminGetUserIPAddresses");

            //TODO:  Add the appropriate parameter to the commandwrapper object here
            cw.AddInParameter("UserId", DbType.Int32, userIPAddressDto.UserId);
            
            return cw;
        }

        public override List<UserIPAddress> ConvertResultsList(DbCommandWrapper cw, IDataReader reader)
        {      
            //Check if reader is null.  No results returned
            if (reader == null)
                return null;

            List<UserIPAddress> userIPAddresses = new List<UserIPAddress>();

            while (reader.Read())
            {
                //Init new subscription object for each subscription record
                UserIPAddress userIPAddress = new UserIPAddress();

                //Populate object from record
                userIPAddress.UserIPId = Convert.ToInt32(reader["UserIPId"]);
                userIPAddress.UserId = Convert.ToInt32(reader["UserId"]);

                userIPAddress.BeginAddress = new IPAddress();
                userIPAddress.EndAddress = new IPAddress();

                userIPAddress.BeginAddress.Octet1 = Convert.ToInt32(reader["IPOctet1Begin"]);
                userIPAddress.BeginAddress.Octet2 = Convert.ToInt32(reader["IPOctet2Begin"]);
                userIPAddress.BeginAddress.Octet3 = Convert.ToInt32(reader["IPOctet3Begin"]);
                userIPAddress.BeginAddress.Octet4 = Convert.ToInt32(reader["IPOctet4Begin"]);

                userIPAddress.EndAddress.Octet1 = userIPAddress.BeginAddress.Octet1;
                userIPAddress.EndAddress.Octet2 = userIPAddress.BeginAddress.Octet2;

                object value = reader["IPOctet3End"];
                userIPAddress.EndAddress.Octet3 = (value == DBNull.Value) ? userIPAddress.BeginAddress.Octet3 : Convert.ToInt32(value);
                userIPAddress.EndAddress.Octet4 = Convert.ToInt32(reader["IPOctet4End"]);

                //Add to list array
                userIPAddresses.Add(userIPAddress);
            }

            if (userIPAddresses.Count == 0)
                return null;

            return userIPAddresses;
            
        }
    }
}
