using System;
using System.Collections.Generic;
using System.Text;
using MN.Enterprise.Base;

namespace us.naturalproduct.DataTransferObjects
{
    public class BaseObject: DataTransferObject
    {
        public BaseObject()
        {
            //Active and new on initialization
            this.isActive = true;
            this.isNew = true;
        }
        
        private bool isActive;
        private bool isNew;
        private Int32 creationUserId;
        private string creationUserName;
        private DateTime creationDateTime;
        private Int32 updateUserId;
        private string updateUserName;
        private DateTime updateDateTime;

        public bool IsOld
        {
            get { return !this.isNew; }
        }

        public bool IsNew
        {
            get { return this.isNew; }
            set { this.isNew = value; }
        }

        public bool IsActive
        {
            get { return this.isActive; }
            set { this.isActive = value; }
        }

        public bool IsInactive
        {
            get { return !this.isActive; }
        }

        public Int32 CreationUserId {
            get { return this.creationUserId; }
            set { this.creationUserId = value; }
        }

        public string CreationUserName
        {
            get { return this.creationUserName; }
            set { this.creationUserName = value; }
        }

        public DateTime CreationDateTime
        {
            get { return this.creationDateTime; }
            set { this.creationDateTime = value; }
        }

        public Int32 UpdateUserId
        {
            get { return this.updateUserId; }
            set { this.updateUserId = value; }
        }

        public string UpdateUserName
        {
            get { return this.updateUserName; }
            set { this.updateUserName = value; }
        }

        public DateTime UpdateDateTime
        {
            get { return this.updateDateTime; }
            set { this.updateDateTime = value; }
        }
    }
}
