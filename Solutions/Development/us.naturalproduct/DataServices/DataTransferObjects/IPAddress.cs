using System;
using System.Collections.Generic;
using System.Text;

namespace us.naturalproduct.DataTransferObjects
{
    public class IPAddress
    {
        private Int32 octet1;
        private Int32 octet2;
        private Int32 octet3;
        private Int32 octet4;

        public int Octet1
        {
            get { return octet1; }
            set { octet1 = value; }
        }

        public int Octet2
        {
            get { return octet2; }
            set { octet2 = value; }
        }

        public int Octet3
        {
            get { return octet3; }
            set { octet3 = value; }
        }

        public int Octet4
        {
            get { return octet4; }
            set { octet4 = value; }
        }
    }
}
