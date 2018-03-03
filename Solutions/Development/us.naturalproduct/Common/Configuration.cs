using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace us.naturalproduct.Common
{
    public static class Config
    {
        public static string ConnString
        {
            get { return ConfigurationManager.ConnectionStrings["NPC"].ConnectionString; }
        }

    }
}
