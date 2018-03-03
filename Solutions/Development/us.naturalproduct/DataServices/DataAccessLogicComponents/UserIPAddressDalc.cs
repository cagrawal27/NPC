using System;
using System.Collections.Generic;
using System.Data;
using MN.Enterprise.Data;
using us.naturalproduct.DALCDeleteHelpers;
using us.naturalproduct.DALCHelpers;
using us.naturalproduct.DALCInsertHelpers;
using us.naturalproduct.DALCQueryHelpers;
using us.naturalproduct.DALCUpdateHelpers;
using us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.DataAccessLogicComponents
{
    public class UserIPAddressDalc: CommonDALC
    {
        public UserIPAddressDalc()
        {
        }

        public UserIPAddressDalc(DALCTransaction transaction):base(transaction)
        {
        }

        public List<UserIPAddress> GetList(UserIPAddress inUserIPAddressDto)
        {
            return ExecuteQueryList<UserIPAddress>(new UserIPAddressListSelectHelper(), inUserIPAddressDto);
        }

        public int Add(UserIPAddress inUserIPAddressDto)
        {
            return ExecuteNonQuery(new UserIPAddressInsertHelper(), inUserIPAddressDto);
        }

        public bool Exists(UserIPAddress inUserIPAddressDto)
        {
            IDataReader rdr = ExecuteQueryReader(new UserIPAddressExistsHelper(), inUserIPAddressDto);

            bool found = false;

            if (rdr != null)
            {
                if (rdr.Read())
                {
                    Int32 exists = (Int32) rdr["Exists"];

                    found = (exists > 0);
                }
            }

            return found;
        }
    
        public int Delete(UserIPAddress inUserIPAddressDto)
        {
            return ExecuteNonQuery(new UserIPAddressDeleteHelper(), inUserIPAddressDto);
        }

        public UserIPAddress GetUserByIP(UserIPAddress inUserIPAddressDto)
        {
            return ExecuteQueryDto(new UserIPAddressByIPSelectHelper(), inUserIPAddressDto) as UserIPAddress;
        }
    }
}
