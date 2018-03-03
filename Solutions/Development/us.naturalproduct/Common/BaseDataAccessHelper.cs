using System;
using System.Collections.Generic;
using System.Text;

namespace us.naturalproduct.Common
{
    public class BaseDataAccessHelper
    {
        public static Int32 ConvBoolToInt32(bool arg)
        {
            if (arg)
                return 1;
            return 0;
        }


    }
}
