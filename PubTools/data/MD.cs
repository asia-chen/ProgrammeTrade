using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PubTools.data
{
    public class MD
    {
        //交易日
        String TradingDay;
        ///合约代码
        public String InstrumentID { get; set; }
        ///交易所代码
        String ExchangeID;
        ///合约在交易所的代码
        String ExchangeInstID;
        ///最新价
        double LastPrice;
        ///上次结算价
        double PreSettlementPrice;
        ///昨收盘
        double PreClosePrice;
        ///昨持仓量
        double PreOpenInterest;
        ///今开盘
        double OpenPrice;
        ///最高价
        double HighestPrice;
        ///最低价
        double LowestPrice;
        ///数量
        long Volume;
        ///成交金额
        double Turnover;
        ///持仓量
        double OpenInterest;
        ///今收盘
        double ClosePrice;
        ///本次结算价
        double SettlementPrice;
        ///涨停板价
        double UpperLimitPrice;
        ///跌停板价
        double LowerLimitPrice;
        ///昨虚实度
        double PreDelta;
        ///今虚实度
        double CurrDelta;
        ///最后修改时间
        String UpdateTime;
        ///最后修改毫秒
        long UpdateMillisec;
        ///申买价一
        double BidPrice1;
        ///申买量一
        long BidVolume1;
        ///申卖价一
        double AskPrice1;
        ///申卖量一
        long AskVolume1;
        ///申买价二
        double BidPrice2;
        ///申买量二
        long BidVolume2;
        ///申卖价二
        double AskPrice2;
        ///申卖量二
        long AskVolume2;
        ///申买价三
        double BidPrice3;
        ///申买量三
        long BidVolume3;
        ///申卖价三
        double AskPrice3;
        ///申卖量三
        long AskVolume3;
        ///申买价四
        double BidPrice4;
        ///申买量四
        long BidVolume4;
        ///申卖价四
        double AskPrice4;
        ///申卖量四
        long AskVolume4;
        ///申买价五
        double BidPrice5;
        ///申买量五
        long BidVolume5;
        ///申卖价五
        double AskPrice5;
        ///申卖量五
        long AskVolume5;
        ///当日均价
        double AveragePrice;
        ///业务日期
        String ActionDay;

        public void SetData(String[] resStr)
        {
            int pos = 6;
            
            //交易日
            TradingDay = resStr[pos + 0];
            ///合约代码
            InstrumentID = resStr[pos + 1]; 
            ///交易所代码
            ExchangeID = resStr[pos + 2];
            ///合约在交易所的代码
            ExchangeInstID = resStr[pos + 3];
            ///最新价
            LastPrice = double.Parse(resStr[pos + 4]);
            ///上次结算价
            PreSettlementPrice = double.Parse(resStr[pos + 5]);
            ///昨收盘
            PreClosePrice = double.Parse(resStr[pos + 6]);
            ///昨持仓量
            PreOpenInterest = double.Parse(resStr[pos + 7]);
            ///今开盘
            OpenPrice = double.Parse(resStr[pos + 8]);
            ///最高价
            HighestPrice = double.Parse(resStr[pos + 9]);
            ///最低价
            LowestPrice = double.Parse(resStr[pos + 10]);
            ///数量
            Volume = long.Parse(resStr[pos + 11]);
            ///成交金额
            Turnover = double.Parse(resStr[pos + 12]);
            ///持仓量
            OpenInterest = double.Parse(resStr[pos + 13]);
            ///今收盘
            ClosePrice = double.Parse(resStr[pos + 14]);
            ///本次结算价
            SettlementPrice = double.Parse(resStr[pos + 15]);
            ///涨停板价
            UpperLimitPrice = double.Parse(resStr[pos + 16]);
            ///跌停板价
            LowerLimitPrice = double.Parse(resStr[pos + 17]);
            ///昨虚实度
            PreDelta = double.Parse(resStr[pos + 18]);
            ///今虚实度
            CurrDelta = double.Parse(resStr[pos + 19]);
            ///最后修改时间
            UpdateTime = resStr[pos + 20];
            ///最后修改毫秒
            UpdateMillisec = long.Parse(resStr[pos + 21]);
            ///申买价一
            BidPrice1 = double.Parse(resStr[pos + 22]);
            ///申买量一
            BidVolume1 = long.Parse(resStr[pos + 23]);
            ///申卖价一
            AskPrice1 = double.Parse(resStr[pos + 24]);
            ///申卖量一
            AskVolume1 = long.Parse(resStr[pos + 25]);
            ///申买价二
            BidPrice2 = double.Parse(resStr[pos + 26]);
            ///申买量二
            BidVolume2 = long.Parse(resStr[pos + 27]);
            ///申卖价二
            AskPrice2 = double.Parse(resStr[pos + 28]);
            ///申卖量二
            AskVolume2 = long.Parse(resStr[pos + 29]);
            ///申买价三
            BidPrice3 = double.Parse(resStr[pos + 30]);
            ///申买量三
            BidVolume3 = long.Parse(resStr[pos + 31]);
            ///申卖价三
            AskPrice3 = double.Parse(resStr[pos + 32]);
            ///申卖量三
            AskVolume3 = long.Parse(resStr[pos + 33]);
            ///申买价四
            BidPrice4 = double.Parse(resStr[pos + 34]);
            ///申买量四
            BidVolume4 = long.Parse(resStr[pos + 35]);
            ///申卖价四
            AskPrice4 = double.Parse(resStr[pos + 36]);
            ///申卖量四
            AskVolume4 = long.Parse(resStr[pos + 37]);
            ///申买价五
            BidPrice5 = double.Parse(resStr[pos + 38]);
            ///申买量五
            BidVolume5 = long.Parse(resStr[pos + 39]);
            ///申卖价五
            AskPrice5 = double.Parse(resStr[pos + 40]);
            ///申卖量五
            AskVolume5 = long.Parse(resStr[pos + 41]);
            ///当日均价
            AveragePrice = double.Parse(resStr[pos + 42]);
            ///业务日期
            ActionDay = resStr[pos + 43];
        }
    }
}
