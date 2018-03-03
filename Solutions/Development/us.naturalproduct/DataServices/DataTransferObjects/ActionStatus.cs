using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace us.naturalproduct.DataTransferObjects
{
    public class ActionStatus: BaseObject
    {
        public ActionStatus()
        {
            this.messages = new List<ActionMessage>();
            
            //Unsuccessful by default
            this.isSuccessful = false;
        }

        private bool isSuccessful;

        private List<ActionMessage> messages;

        public bool IsSuccessful
        {
            get { return this.isSuccessful; }
            set { this.isSuccessful = value; }
        }

        public List<ActionMessage> Messages
        {
            get { return this.messages; }
            set { this.messages = value; }
        }

    }
}
