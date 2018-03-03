using System;
using System.Collections.Generic;
using MN.Enterprise.Data;
using us.naturalproduct.DALCDeleteHelpers;
using us.naturalproduct.DALCHelpers;
using us.naturalproduct.DALCQueryHelpers;
using us.naturalproduct.DALCUpdateHelpers;
using us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.DataAccessLogicComponents
{
    public class RoleDalc: CommonDALC
    {        
        /// <summary>
        /// Default constructor.
        /// </summary>
        public RoleDalc() : base(){}

        /// <summary>
        /// Constructor with a transaction.
        /// </summary>
        /// <param name="transaction"></param>
        public RoleDalc(DALCTransaction transaction)
            : base(transaction){}

        public List<Role> GetRoleList(User inUserDto)
        {
            return ExecuteQueryList<Role>(new RoleListByUserIdSelectHelper(), inUserDto);
        }
    }
}
