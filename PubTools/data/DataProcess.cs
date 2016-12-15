using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PubTools.data
{
    public class DataProcess
    {
        /// <summary>数据后续处理</summary>
        public delegate void DataProcessDelegate(Object o);
        public static DataProcessDelegate orderError = null;
        public static DataProcessDelegate tradeProcess = null;
        public static DataProcessDelegate mdProcess = null;

        /// <summary>委托错误</summary>
        /// <param name="o">字符串数组，第一个存储requestid，第二个存储错误信息</param>
        public static void OrderError(Object o)
        {
            String[] result = (String[])o;
            Console.WriteLine(result[0] + " " + result[1]);


            if (orderError != null)
            {
                orderError(o);
            }
        }

        /// <summary>成交</summary>
        public static void TradeProcess(Object o)
        {
            Trade thistrade = (Trade)o;
            Console.WriteLine(thistrade.OrderSysID);

            if (tradeProcess != null)
            {
                tradeProcess(o);
            }
        }

        /// <summary>行情</summary>
        public static void MdProcess(Object o)
        {
            if (mdProcess != null)
            {
                mdProcess(o);
            }
        }
    }
}
