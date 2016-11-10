using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace PubTools
{
    public class AppConfig
    {
        public String mysqlConStr = null;
        public String defaultDir = null;

        public AppConfig()
        {
            for (int i = 0; i < ConfigurationManager.ConnectionStrings.Count; i++)
            {
                String name = ConfigurationManager.ConnectionStrings[i].Name;
                String addr = ConfigurationManager.ConnectionStrings[i].ConnectionString;

                if (name.Equals("mysql"))
                    mysqlConStr = addr;
            }

            defaultDir = ConfigurationManager.AppSettings["defaultDir"];
        }
    }
}
