using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

namespace PubTools.data
{
    /// <summary>
    ///  持仓
    /// </summary>
    public class UserPosition
    {
        ///合约代码
        String InstrumentID;
        ///经纪公司代码
        String BrokerID;
        ///投资者代码
        String InvestorID;
        ///持仓多空方向
        String PosiDirection;
        ///投机套保标志
        String HedgeFlag;
        ///持仓日期
        String PositionDate;  ///今日持仓 THOST_FTDC_PSD_Today '1'; 历史持仓  THOST_FTDC_PSD_History '2'
        ///上日持仓
        long YdPosition;
        ///今日持仓
        long Position;
        ///多头冻结
        long LongFrozen;
        ///空头冻结
        long ShortFrozen;
        ///开仓冻结金额
        double LongFrozenAmount;
        ///开仓冻结金额
        double ShortFrozenAmount;
        ///开仓量
        long OpenVolume;
        ///平仓量
        long CloseVolume;
        ///开仓金额
        double OpenAmount;
        ///平仓金额
        double CloseAmount;
        ///持仓成本
        double PositionCost;
        ///上次占用的保证金
        double PreMargin;
        ///占用的保证金
        double UseMargin;
        ///冻结的保证金
        double FrozenMargin;
        ///冻结的资金
        double FrozenCash;
        ///冻结的手续费
        double FrozenCommission;
        ///资金差额
        double CashIn;
        ///手续费
        double Commission;
        ///平仓盈亏
        double CloseProfit;
        ///持仓盈亏
        double PositionProfit;
        ///上次结算价
        double PreSettlementPrice;
        ///本次结算价
        double SettlementPrice;
        ///交易日
        String TradingDay;
        ///结算编号
        int SettlementID;
        ///开仓成本
        double OpenCost;
        ///交易所保证金
        double ExchangeMargin;
        ///组合成交形成的持仓
        long CombPosition;
        ///组合多头冻结
        long CombLongFrozen;
        ///组合空头冻结
        long CombShortFrozen;
        ///逐日盯市平仓盈亏
        double CloseProfitByDate;
        ///逐笔对冲平仓盈亏
        double CloseProfitByTrade;
        ///今日持仓
        long TodayPosition;
        ///保证金率
        double MarginRateByMoney;
        ///保证金率(按手数)
        double MarginRateByVolume;
        ///执行冻结
        long StrikeFrozen;
        ///执行冻结金额
        double StrikeFrozenAmount;
        ///放弃执行冻结
        long AbandonFrozen;

        /// 解析数据
        public int SetData(String[] resStr, int pos)
        {
            if (resStr.Length < pos)
                return -1;

            ///合约代码
            InstrumentID = resStr[pos];
            ///经纪公司代码
            BrokerID = resStr[pos + 1];
            ///投资者代码
            InvestorID = resStr[pos + 2];
            ///持仓多空方向
            PosiDirection = resStr[pos + 3];
            ///投机套保标志
            HedgeFlag = resStr[pos + 4];
            ///持仓日期
            PositionDate = resStr[pos + 5];  ///今日持仓 THOST_FTDC_PSD_Today '1'; 历史持仓  THOST_FTDC_PSD_History '2'
            ///上日持仓
            YdPosition = long.Parse(resStr[pos + 6]);
            ///今日持仓
            Position = long.Parse(resStr[pos + 7]);
            ///多头冻结
            LongFrozen = long.Parse(resStr[pos + 8]);
            ///空头冻结
            ShortFrozen = long.Parse(resStr[pos + 9]);
            ///开仓冻结金额
            LongFrozenAmount = double.Parse(resStr[pos + 10]);
            ///开仓冻结金额
            ShortFrozenAmount = double.Parse(resStr[pos + 11]);
            ///开仓量
            OpenVolume = long.Parse(resStr[pos + 12]);
            ///平仓量
            CloseVolume = long.Parse(resStr[pos + 13]);
            ///开仓金额
            OpenAmount = double.Parse(resStr[pos + 14]);
            ///平仓金额
            CloseAmount = double.Parse(resStr[pos + 15]);
            ///持仓成本
            PositionCost = double.Parse(resStr[pos + 16]);
            ///上次占用的保证金
            PreMargin = double.Parse(resStr[pos + 17]);
            ///占用的保证金
            UseMargin = double.Parse(resStr[pos + 18]);
            ///冻结的保证金
            FrozenMargin = double.Parse(resStr[pos + 19]);
            ///冻结的资金
            FrozenCash = double.Parse(resStr[pos + 20]);
            ///冻结的手续费
            FrozenCommission = double.Parse(resStr[pos + 21]);
            ///资金差额
            CashIn = double.Parse(resStr[pos + 22]);
            ///手续费
            Commission = double.Parse(resStr[pos + 23]);
            ///平仓盈亏
            CloseProfit = double.Parse(resStr[pos + 24]);
            ///持仓盈亏
            PositionProfit = double.Parse(resStr[pos + 25]);
            ///上次结算价
            PreSettlementPrice = double.Parse(resStr[pos + 26]);
            ///本次结算价
            SettlementPrice = double.Parse(resStr[pos + 27]);
            ///交易日
            TradingDay = resStr[pos + 28];
            ///结算编号
            SettlementID = int.Parse(resStr[pos + 29]);
            ///开仓成本
            OpenCost = double.Parse(resStr[pos + 30]);
            ///交易所保证金
            ExchangeMargin = double.Parse(resStr[pos + 31]);
            ///组合成交形成的持仓
            CombPosition = long.Parse(resStr[pos + 32]);
            ///组合多头冻结
            CombLongFrozen = long.Parse(resStr[pos + 33]);
            ///组合空头冻结
            CombShortFrozen = long.Parse(resStr[pos + 34]);
            ///逐日盯市平仓盈亏
            CloseProfitByDate = double.Parse(resStr[pos + 35]);
            ///逐笔对冲平仓盈亏
            CloseProfitByTrade = double.Parse(resStr[pos + 36]);
            ///今日持仓
            TodayPosition = long.Parse(resStr[pos + 37]);
            ///保证金率
            MarginRateByMoney = double.Parse(resStr[pos + 38]);
            ///保证金率(按手数)
            MarginRateByVolume = double.Parse(resStr[pos + 39]);
            ///执行冻结
            StrikeFrozen = long.Parse(resStr[pos + 40]);
            ///执行冻结金额
            StrikeFrozenAmount = double.Parse(resStr[pos + 41]);
            ///放弃执行冻结
            AbandonFrozen = long.Parse(resStr[pos + 42]);

            RefreshToDataTable();
            return 0;
        }

        /// <summary>
        /// 将数据刷新到datatable，供显示使用
        /// </summary>
         public void RefreshToDataTable()
        {
            lock (GlobalVar.ds)
            {
                Boolean found = false;
                DataTable dt = GlobalVar.ds.Tables["positions"];

                foreach (DataRow dr in dt.Rows)
                {
                    String tmp_instrumentid = (String)dr["InstrumentID"];
                    String tmp_brokerid = (String)dr["BrokerID"];
                    String tmp_investorid = (String)dr["InvestorID"];
                    String tmp_posidirection = (String)dr["PosiDirection"];
                    if (tmp_instrumentid.Equals(this.InstrumentID) && tmp_brokerid.Equals(this.BrokerID)
                        && tmp_investorid.Equals(this.InvestorID) && tmp_posidirection.Equals(this.PosiDirection))
                    {
                        found = true;
                        SetToDatarow(dr);
                        dt.AcceptChanges();
                        break;
                    }
                }
                if (!found)
                {
                    DataRow dr = dt.NewRow();
                    SetToDatarow(dr);
                    dt.Rows.Add(dr);
                }
            }
        }

        /// <summary>
        /// 将内存变量设置到datarow中
        /// </summary>
        /// <param name="dr"></param>
        private void SetToDatarow(DataRow dr)
        {
            ///合约代码
            dr["InstrumentID"] = this.InstrumentID;
            ///经纪公司代码
            dr["BrokerID"] = this.BrokerID;
            ///投资者代码
            dr["InvestorID"] = this.InvestorID;
            ///持仓多空方向
            dr["PosiDirection"] = this.PosiDirection;
            ///投机套保标志
            dr["HedgeFlag"] = this.HedgeFlag;
            ///持仓日期
            dr["PositionDate"] = this.PositionDate;  ///今日持仓 THOST_FTDC_PSD_Today '1'"] = this.; 历史持仓  THOST_FTDC_PSD_History '2'
            ///上日持仓
            dr["YdPosition"] = this.YdPosition;
            ///今日持仓
            dr["Position"] = this.Position;
            ///多头冻结
            dr["LongFrozen"] = this.LongFrozen;
            ///空头冻结
            dr["ShortFrozen"] = this.ShortFrozen;
            ///开仓冻结金额
            dr["LongFrozenAmount"] = this.LongFrozenAmount;
            ///开仓冻结金额
            dr["ShortFrozenAmount"] = this.ShortFrozenAmount;
            ///开仓量
            dr["OpenVolume"] = this.OpenVolume;
            ///平仓量
            dr["CloseVolume"] = this.CloseVolume;
            ///开仓金额
            dr["OpenAmount"] = this.OpenAmount;
            ///平仓金额
            dr["CloseAmount"] = this.CloseAmount;
            ///持仓成本
            dr["PositionCost"] = this.PositionCost;
            ///上次占用的保证金
            dr["PreMargin"] = this.PreMargin;
            ///占用的保证金
            dr["UseMargin"] = this.UseMargin;
            ///冻结的保证金
            dr["FrozenMargin"] = this.FrozenMargin;
            ///冻结的资金
            dr["FrozenCash"] = this.FrozenCash;
            ///冻结的手续费
            dr["FrozenCommission"] = this.FrozenCommission;
            ///资金差额
            dr["CashIn"] = this.CashIn;
            ///手续费
            dr["Commission"] = this.Commission;
            ///平仓盈亏
            dr["CloseProfit"] = this.CloseProfit;
            ///持仓盈亏
            dr["PositionProfit"] = this.PositionProfit;
            ///上次结算价
            dr["PreSettlementPrice"] = this.PreSettlementPrice;
            ///本次结算价
            dr["SettlementPrice"] = this.SettlementPrice;
            ///交易日
            dr["TradingDay"] = this.TradingDay;
            ///结算编号
            dr["SettlementID"] = this.SettlementID;
            ///开仓成本
            dr["OpenCost"] = this.OpenCost;
            ///交易所保证金
            dr["ExchangeMargin"] = this.ExchangeMargin;
            ///组合成交形成的持仓
            dr["CombPosition"] = this.CombPosition;
            ///组合多头冻结
            dr["CombLongFrozen"] = this.CombLongFrozen;
            ///组合空头冻结
            dr["CombShortFrozen"] = this.CombShortFrozen;
            ///逐日盯市平仓盈亏
            dr["CloseProfitByDate"] = this.CloseProfitByDate;
            ///逐笔对冲平仓盈亏
            dr["CloseProfitByTrade"] = this.CloseProfitByTrade;
            ///今日持仓
            dr["TodayPosition"] = this.TodayPosition;
            ///保证金率
            dr["MarginRateByMoney"] = this.MarginRateByMoney;
            ///保证金率(按手数)
            dr["MarginRateByVolume"] = this.MarginRateByVolume;
            ///执行冻结
            dr["StrikeFrozen"] = this.StrikeFrozen;
            ///执行冻结金额
            dr["StrikeFrozenAmount"] = this.StrikeFrozenAmount;
            ///放弃执行冻结
            dr["AbandonFrozen"] = this.AbandonFrozen;
        }    
    }
}
