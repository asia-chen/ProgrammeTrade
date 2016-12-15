using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PubTools
{
    public class Const
    {
        public const int SleepTime = 100;
        public const int TimeOutCount = 60;

        public const String splitstr = "><";

        public const String ExgSH = "shfe";
        public const String ExgDL = "dce";
        public const String ExgZZ = "czce";
        public const String ExgJR = "cffex";

        public const String TradeBuy = "0";
        public const String TradeSell = "1";

        public const String TradeOpen = "0";
        public const String TradeCloseToday = "3";
        public const String TradeClose = "4";

        /// <summary>今仓</summary>
        public const String THOST_FTDC_PSD_Today = "1";
        /// <summary>昨仓</summary>
        public const String THOST_FTDC_PSD_History = "2";
    }
}
