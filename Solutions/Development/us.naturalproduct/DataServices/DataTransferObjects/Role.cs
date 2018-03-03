using System;
using System.Collections.Generic;
using System.Text;

namespace us.naturalproduct.DataTransferObjects
{
    public class Role: BaseObject
    {
        public Role() : base() { }

        public Role(Int32 argRoleId): base()
        {
            this.roleId = argRoleId;
        }

        public Role(Int32 argRoleId, string argRoleDescription): base()
        {
            this.roleId = argRoleId;
            this.roleDescription = argRoleDescription;
        }


        private Int32 roleId;
        private string roleDescription;

        public Int32 RoleId
        {
            get { return this.roleId; }
            set { this.roleId = value; }
        }

        public string RoleDescription
        {
            get { return this.roleDescription; }
            set { this.roleDescription = value; }
        }
    }
}
