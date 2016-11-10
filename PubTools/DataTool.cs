using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace PubTools
{
    public class DataTool
    {
        // 合同、交易日表
        public static void InitSimpleInstruments(DataTable dt)
        {
            dt.Columns.Add("InstrumentID", typeof(String));
            dt.Columns.Add("MaxTradingDay", typeof(String));
            dt.Columns.Add("MinTradingDay", typeof(String));
            dt.PrimaryKey = new DataColumn[] { dt.Columns["InstrumentID"] };
        }

        // 行情数据表结构
        public static void InitMDTable(DataTable dt)
        {
            dt.Columns.Add("TradingDay", typeof(String));
            dt.Columns.Add("InstrumentID", typeof(String));
            dt.Columns.Add("LastPrice", typeof(double));
            dt.Columns.Add("PreSettlementPrice", typeof(double));
            dt.Columns.Add("PreClosePrice", typeof(double));
            dt.Columns.Add("OpenPrice", typeof(double));
            dt.Columns.Add("HighestPrice", typeof(double));
            dt.Columns.Add("LowestPrice", typeof(double));
            dt.Columns.Add("Volume", typeof(long));
            dt.Columns.Add("OpenInterest", typeof(long));
            dt.Columns.Add("SettlementPrice", typeof(double));
            dt.Columns.Add("UpdateTime", typeof(String));
            dt.Columns.Add("BidPrice1", typeof(double));
            dt.Columns.Add("BidVolume1", typeof(long));
            dt.Columns.Add("AskPrice1", typeof(double));
            dt.Columns.Add("AskVolume1", typeof(long));
            dt.Columns.Add("AveragePrice", typeof(double));
            dt.Columns.Add("ActionDay", typeof(String));
            dt.Columns.Add("UpAndDown", typeof(double));

            dt.Columns.Add("MDTime", typeof(String));
        }

        // K线数据表结构
        public static void InitKLine(DataTable dt)
        {
            dt.Columns.Add("TradingDay", typeof(String));
            dt.Columns.Add("InstrumentID", typeof(String));
            dt.Columns.Add("ActionDay", typeof(String));
            dt.Columns.Add("UpdateTime", typeof(String));

            dt.Columns.Add("SeqNo", typeof(long)); // 当日序号，1分钟为单位

            dt.Columns.Add("OpenPrice", typeof(double)); // 开
            dt.Columns.Add("HighestPrice", typeof(double)); // 高
            dt.Columns.Add("LowestPrice", typeof(double)); // 低
            dt.Columns.Add("ClosePrice", typeof(double)); //收
            dt.Columns.Add("Volume", typeof(long));  // 量
            dt.Columns.Add("OpenInterest", typeof(long)); // 仓
            dt.Columns.Add("AveragePrice", typeof(double)); // 均？
        }

        // 品种表
        public static void InitProduct(DataTable dt)
        {
            dt.Columns.Add("ProductID", typeof(String));
            dt.Columns.Add("BelongTo", typeof(String));
            dt.Columns.Add("ProductName", typeof(String));
            dt.Columns.Add("PeriodID", typeof(String));
            dt.PrimaryKey = new DataColumn[] { dt.Columns["ProductID"] };
        }

        // 交易时段表
        public static void InitTradeSeqgment(DataTable dt)
        {
            dt.Columns.Add("PeriodID", typeof(String));
            dt.Columns.Add("SeqNo", typeof(long));
            dt.Columns.Add("BeginDay", typeof(long));
            dt.Columns.Add("BeginTime", typeof(String));
            dt.Columns.Add("EndDay", typeof(long));
            dt.Columns.Add("EndTime", typeof(String));
            dt.PrimaryKey = new DataColumn[] { dt.Columns["PeriodID"], dt.Columns["SeqNo"] };
        }
    }
}
