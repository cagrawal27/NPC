using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using MN.Enterprise.Base;
using MN.Enterprise.Data;
using us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.DALCQueryHelpers
{
    public class RoleListByUserIdSelectHelper: DALCHelper<Role>
    {
        public override DbCommandWrapper InitializeCommand(Database db, DataTransferObject criteria)
        {
            User userDto = criteria as User;

            DbCommandWrapper cw = DbCommandFactory.GetStoredProcCommandWrapper(db, "spGetUserRolesByUserId");

            cw.AddInParameter("UserId", DbType.Int32, userDto.UserId);

            return cw;
        }

        public override List<Role> ConvertResultsList(DbCommandWrapper cw, IDataReader reader)
        {
            if (null == reader)
                return null;

            List<Role> roleList = new List<Role>();

            while (reader.Read())
            {
                Role role = new Role();

                role.RoleId = Convert.ToInt32(reader["RoleId"]);

                roleList.Add(role);
            }

            if (0 == roleList.Count)
                return null;

            return roleList;
        }
    }
}
