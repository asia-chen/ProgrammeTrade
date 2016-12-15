using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PubTools.data
{
    public class DataProcess
    {
        /// <summary>数据后续处理</summary>
        /// <param name="msg">待显示内容</param> 
        public delegate void DataProcessDelegate(Object o);
        public static DataProcessDelegate orderError = null;
        public static DataProcessDelegate tradeProcess = null;
        public static DataProcessDelegate mdProcess = null;

        public static void OrderError(Object o)
        {
            if (orderError != null)
            {
                orderError(o);
            }
        }

        public static void TradeProcess(Object o)
        {
            if (tradeProcess != null)
            {
                tradeProcess(o);
            }
        }

        public static void MdProcess(Object o)
        {
            if (mdProcess != null)
            {
                mdProcess(o);
            }
        }
    }
}
