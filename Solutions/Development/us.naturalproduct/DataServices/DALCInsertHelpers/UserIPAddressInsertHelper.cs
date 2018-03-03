using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using MN.Enterprise.Base;
using MN.Enterprise.Data;
using us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.DALCInsertHelpers
{
    public class UserIPAddressInsertHelper:DALCHelper
    {
        public override DbCommandWrapper InitializeCommand(Database db, DataTransferObject criteria)
        {
            UserIPAddress userIPAddressDto = criteria as UserIPAddress;

            //Create a database command object within which T-SQL commands can 
            //be executed.
            DbCommandWrapper cw = DbCommandFactory.GetStoredProcCommandWrapper(db, "spInsertUserIPAddress");

            //Parameters are added here, update each line to reflect the parameter from
            //the subscription object
            cw.AddInParameter("UserId", DbType.Int32, userIPAddressDto.UserId);

            cw.AddInParameter("IPOctet1Begin", DbType.Int32, userIPAddressDto.BeginAddress.Octet1);

            cw.AddInParameter("IPOctet2Begin", DbType.Int32, userIPAddressDto.BeginAddress.Octet2);

            cw.AddInParameter("IPOctet3Begin", DbType.Int32, userIPAddressDto.BeginAddress.Octet3);

            cw.AddInParameter("IPOctet3End", DbType.Int32, userIPAddressDto.EndAddress.Octet3);

            cw.AddInParameter("IPOctet4Begin", DbType.Int32, userIPAddressDto.BeginAddress.Octet4);

            cw.AddInParameter("IPOctet4End", DbType.Int32, userIPAddressDto.EndAddress.Octet4);

            //Return the commandwrapper object to DALCHelper where the stored proc
            //will be executed
            return cw;

        }
    }
}
