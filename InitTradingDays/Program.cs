using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PubTools;

namespace InitTradingDays
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime dt = DateTime.ParseExact("20160801", "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
            GlobalVar.Init4Data();
            GlobalVar.mysqltool.Connect();

            for (int i = 0; i < 3650; i++)
            {
                DateTime thisDt = dt.AddDays(i);
                if (thisDt.DayOfWeek != DayOfWeek.Sunday && thisDt.DayOfWeek !=DayOfWeek.Saturday)
                {
                    String dtStr = thisDt.ToString("yyyyMMdd");
                    GlobalVar.mysqltool.InsertTradingDays(dtStr);
                    Console.Write(dtStr + "  ");
                }
            }
            GlobalVar.mysqltool.Commit();
        }
    }
}
