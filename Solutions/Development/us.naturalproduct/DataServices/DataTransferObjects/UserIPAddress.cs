using System;
using System.Collections.Generic;
using System.Text;

namespace us.naturalproduct.DataTransferObjects
{
    public class UserIPAddress: BaseObject
    {
        public UserIPAddress()
        {
            beginAddress = new IPAddress();

            endAddress = new IPAddress();
        }

        private Int32 userIPId;

        private Int32 userId;

        private IPAddress beginAddress;

        private IPAddress endAddress;

        public int UserIPId
        {
            get { return userIPId; }
            set { userIPId = value; }
        }

        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        public IPAddress BeginAddress
        {
            get { return beginAddress; }
            set { beginAddress = value; }
        }

        public IPAddress EndAddress
        {
            get { return endAddress; }
            set { endAddress = value; }
        }

        public string BeginIP
        {
            get
            {
                return
                    String.Format("{0}.{1}.{2}.{3}", 
                                    beginAddress.Octet1.ToString(), 
                                    beginAddress.Octet2.ToString(),
                                    beginAddress.Octet3.ToString(), 
                                    beginAddress.Octet4.ToString());
            }
        }

        public string EndIP
        {
            get
            {
                return
                    String.Format("{0}.{1}.{2}.{3}", 
                                    endAddress.Octet1.ToString(), 
                                    endAddress.Octet2.ToString(),
                                    endAddress.Octet3.ToString(), 
                                    endAddress.Octet4.ToString());
            }
        }

    }
}
