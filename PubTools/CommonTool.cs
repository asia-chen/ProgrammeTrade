using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;

namespace PubTools
{
    public class CommonTool
    {
        /// 根据合约代码返回品种代码，去除合约中数字
        public static String GetProduct(String InstrumentID)
        {
            String tmp = InstrumentID;
            for (char i = '0'; i <= '9'; i++)
            {
                tmp = tmp.Replace(i, ' ');
            }
            return tmp.Trim();
        }

        /// 超时判断
        /// 返回：0 成功；-1 超时
        public static int TimeoutWait(ref int variable, int value)
        {
            int count = 0;
            while (variable != value && count < Const.TimeOutCount)
            {
                Thread.Sleep(Const.SleepTime);
                count++;
            }
            if (count >= Const.TimeOutCount)
                return -1;
            return 0;
        }

    }
}
