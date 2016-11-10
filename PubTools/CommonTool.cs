using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PubTools
{
    public class CommonTool
    {
        // 根据合约代码返回品种代码
        //    去除合约中数字
        public static String GetProduct(String InstrumentID)
        {
            String tmp = InstrumentID;
            for (char i = '0'; i <= '9'; i++)
            {
                tmp = tmp.Replace(i, ' ');
            }
            return tmp.Trim();
        }
    }
}
