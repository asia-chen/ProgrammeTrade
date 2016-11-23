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

        // -----------------------------------------------------------------------------------------------------
        /// <summary>
        /// 初始化交易用户表
        /// TODO：内容待整理
        /// </summary>
        /// <param name="ds">DataSet</param>
        public static void InitUsers(DataSet ds)
        {
            DataTable dt = ds.Tables.Add("users");

            dt.Columns.Add("brokerID", typeof(String));
            dt.Columns.Add("userID", typeof(String));
            dt.Columns.Add("password", typeof(String));
            dt.Columns.Add("tradeAddr", typeof(String));
            dt.Columns.Add("tradePort", typeof(String));
            dt.Columns.Add("connectMsg", typeof(String));
            dt.Columns.Add("foundShares",typeof(String));
            dt.Columns.Add("maxValue", typeof(String));
            dt.PrimaryKey = new DataColumn[] { dt.Columns["brokerID"], dt.Columns["userID"] };
        }

        /// <summary>
        /// 初始化交易账户表
        /// TODO：内容待整理
        /// </summary>
        /// <param name="ds">DataSet</param>
        public static void InitAccount(DataSet ds)
        {
            DataTable dt = ds.Tables.Add("accounts");

           ///单位净值
            dt.Columns.Add("foundValue", typeof(double));
            ///风险度
            dt.Columns.Add("riskValue", typeof(double));
            ///基金份额
            dt.Columns.Add("foundShares", typeof(double));
            ///回撤
            dt.Columns.Add("retracement",typeof(double));
            ///冻结资金（手续费、现金、保证金）
            dt.Columns.Add("forzonCash", typeof(double));
            
            ///经纪公司代码
            dt.Columns.Add("BrokerID", typeof(String));
            ///投资者帐号
            dt.Columns.Add("AccountID", typeof(String));
            ///上次结算准备金
            dt.Columns.Add("PreBalance", typeof(double));
            ///上次占用的保证金
            dt.Columns.Add("PreMargin", typeof(double));
            ///入金金额
            dt.Columns.Add("Deposit", typeof(double));
            ///出金金额
            dt.Columns.Add("Withdraw", typeof(double));
            ///冻结的保证金
            dt.Columns.Add("FrozenMargin", typeof(double));
            ///冻结的资金
            dt.Columns.Add("FrozenCash", typeof(double));
            ///冻结的手续费
            dt.Columns.Add("FrozenCommission", typeof(double));
            ///当前保证金总额
            dt.Columns.Add("CurrMargin", typeof(double));
            ///资金差额
            dt.Columns.Add("CashIn", typeof(double));
            ///手续费
            dt.Columns.Add("Commission", typeof(double));
            ///平仓盈亏
            dt.Columns.Add("CloseProfit", typeof(double));
            ///持仓盈亏
            dt.Columns.Add("PositionProfit", typeof(double));
            ///期货结算准备金
            dt.Columns.Add("Balance", typeof(double));
            ///可用资金
            dt.Columns.Add("Available", typeof(double));
            ///可取资金
            dt.Columns.Add("WithdrawQuota", typeof(double));
            ///基本准备金
            dt.Columns.Add("Reserve", typeof(double));
            ///交易日
            dt.Columns.Add("TradingDay", typeof(String));
            ///质押金额
            dt.Columns.Add("Mortgage", typeof(double));
            ///交易所保证金
            dt.Columns.Add("ExchangeMargin", typeof(double));
            ///投资者交割保证金
            dt.Columns.Add("DeliveryMargin", typeof(double));
            ///交易所交割保证金
            dt.Columns.Add("ExchangeDeliveryMargin", typeof(double));
            ///保底期货结算准备金
            dt.Columns.Add("ReserveBalance", typeof(double));
            ///币种代码
            dt.Columns.Add("CurrencyID", typeof(String));

            dt.PrimaryKey = new DataColumn[] { dt.Columns["brokerID"], dt.Columns["AccountID"] };//
        }

        /// <summary>
        /// 初始化交易持仓表
        /// TODO：内容待整理
        /// </summary>
        /// <param name="ds">DataSet</param>
        public void InitPosition(DataSet ds)
        {
            DataTable dt = ds.Tables.Add("positions");

            ///开仓均价 
            dt.Columns.Add("openCost",typeof(double));
            ///交易所保证金率
            dt.Columns.Add("marginRate",typeof(double));
            ///风险度
            dt.Columns.Add("singleRiskValue",typeof(double));
            ///浮亏比例
            dt.Columns.Add("profitRate",typeof(double));
            ///报单冻结
            dt.Columns.Add("frozenValue", typeof(long));
            
            
            ///合约代码
            dt.Columns.Add("InstrumentID", typeof(String));
            ///经纪公司代码  
            dt.Columns.Add("BrokerID", typeof(String));
            ///投资者代码  
            dt.Columns.Add("InvestorID", typeof(String));
            ///持仓多空方向
            dt.Columns.Add("PosiDirection", typeof(String));
            ///投机套保标志
            dt.Columns.Add("HedgeFlag", typeof(String));
            ///持仓日期
            dt.Columns.Add("PositionDate", typeof(String));  ///今日持仓 THOST_FTDC_PSD_Today '1'; 历史持仓  THOST_FTDC_PSD_History '2'
            ///上日持仓
            dt.Columns.Add("YdPosition", typeof(long));
            ///今日持仓
            dt.Columns.Add("Position", typeof(long));
            ///多头冻结
            dt.Columns.Add("LongFrozen", typeof(long));
            ///空头冻结
            dt.Columns.Add("ShortFrozen", typeof(long));
            ///开仓冻结金额
            dt.Columns.Add("LongFrozenAmount", typeof(double));
            ///开仓冻结金额
            dt.Columns.Add("ShortFrozenAmount", typeof(double));
            ///开仓量
            dt.Columns.Add("OpenVolume", typeof(long));
            ///平仓量
            dt.Columns.Add("CloseVolume", typeof(long));
            ///开仓金额
            dt.Columns.Add("OpenAmount", typeof(double));
            ///平仓金额
            dt.Columns.Add("CloseAmount", typeof(double));
            ///持仓成本
            dt.Columns.Add("PositionCost", typeof(double));
            ///上次占用的保证金
            dt.Columns.Add("PreMargin", typeof(double));
            ///占用的保证金
            dt.Columns.Add("UseMargin", typeof(double));
            ///冻结的保证金
            dt.Columns.Add("FrozenMargin", typeof(double));
            ///冻结的资金
            dt.Columns.Add("FrozenCash", typeof(double));
            ///冻结的手续费
            dt.Columns.Add("FrozenCommission", typeof(double));
            ///资金差额
            dt.Columns.Add("CashIn", typeof(double));
            ///手续费
            dt.Columns.Add("Commission", typeof(double));
            ///平仓盈亏
            dt.Columns.Add("CloseProfit", typeof(double));
            ///持仓盈亏
            dt.Columns.Add("PositionProfit", typeof(double));
            ///上次结算价
            dt.Columns.Add("PreSettlementPrice", typeof(double));
            ///本次结算价
            dt.Columns.Add("SettlementPrice", typeof(double));
            ///交易日1
            dt.Columns.Add("TradingDay", typeof(String));
            ///结算编号
            dt.Columns.Add("SettlementID", typeof(long));
            ///开仓成本
            dt.Columns.Add("OpenCost", typeof(double));
            ///交易所保证金
            dt.Columns.Add("ExchangeMargin", typeof(double));
            ///组合成交形成的持仓
            dt.Columns.Add("CombPosition", typeof(long));
            ///组合多头冻结
            dt.Columns.Add("CombLongFrozen", typeof(long));
            ///组合空头冻结
            dt.Columns.Add("CombShortFrozen", typeof(long));
            ///逐日盯市平仓盈亏
            dt.Columns.Add("CloseProfitByDate", typeof(double));
            ///逐笔对冲平仓盈亏
            dt.Columns.Add("CloseProfitByTrade", typeof(double));
            ///今日持仓
            dt.Columns.Add("TodayPosition", typeof(long));
            ///保证金率
            dt.Columns.Add("MarginRateByMoney", typeof(double));
            ///保证金率(按手数)
            dt.Columns.Add("MarginRateByVolume", typeof(double));
            ///执行冻结
            dt.Columns.Add("StrikeFrozen", typeof(long));
            ///执行冻结金额
            dt.Columns.Add("StrikeFrozenAmount", typeof(double));
            ///放弃执行冻结
            dt.Columns.Add("AbandonFrozen", typeof(long));

            dt.PrimaryKey = new DataColumn[] { dt.Columns["InstrumentID"], dt.Columns["brokerID"], dt.Columns["InvestorID"], dt.Columns["PosiDirection"] };
        }

        /// <summary>
        /// 初始化交易持仓表
        /// TODO：内容待整理
        /// </summary>
        /// <param name="ds">DataSet</param>
        public void InitOrder(DataSet ds)
        {
            DataTable dt = ds.Tables.Add("order");

            ///报单状态
            dt.Columns.Add("orderStatus", typeof(String));
            
            ///经纪公司代码
            dt.Columns.Add("BrokerID", typeof(String));
            ///投资者代码
            dt.Columns.Add("InvestorID", typeof(String));
            ///合约代码
            dt.Columns.Add("InstrumentID", typeof(String));
            ///报单价格条件
            dt.Columns.Add("OrderPriceType", typeof(String));
            ///买卖方向
            dt.Columns.Add("Direction", typeof(String));
            ///组合开平标志
            dt.Columns.Add("CombOffsetFlag", typeof(String));
            ///价格
            dt.Columns.Add("LimitPrice", typeof(double));
            ///数量
            dt.Columns.Add("VolumeTotalOriginal", typeof(long));
            ///有效期类型
            dt.Columns.Add("TimeCondition", typeof(String));
            ///触发条件
            dt.Columns.Add("ContingentCondition",typeof(String));
            ///止损价
            dt.Columns.Add("StopPrice", typeof(long));
            ///强平原因
            dt.Columns.Add("ForceCloseReason", typeof(String));
            ///自动挂起标志
            dt.Columns.Add("IsAutoSuspend", typeof(String));
            ///请求编号
            dt.Columns.Add("RequestID", typeof(String));
            ///本地报单编号
            dt.Columns.Add("OrderLocalID", typeof(String));
            ///交易所代码
       //     dt.Columns.Add("ExchangeID", typeof(String));
            ///会员代码
      //      dt.Columns.Add("ParticipantID", typeof(String));
            ///客户代码
       //     dt.Columns.Add("ClientID", typeof(String));
            ///合约在交易所的代码
       //     dt.Columns.Add("ExchangeInstID", typeof(String));
            ///交易所交易员代码
            dt.Columns.Add("TraderID", typeof(String));
            ///安装编号
       //     dt.Columns.Add("InstallID", typeof(long));
            ///报单提交状态
            dt.Columns.Add("OrderSubmitStatus", typeof(String));
            ///报单提示序号
            dt.Columns.Add("NotifySequence", typeof(long));
            ///交易日
      //      dt.Columns.Add("TradingDay", typeof(String));
            ///结算编号
      //      dt.Columns.Add("SettlementID", typeof(long));
            ///报单编号
            dt.Columns.Add("OrderSysID", typeof(String));
            ///报单来源
            dt.Columns.Add("OrderSource", typeof(String));
            ///报单状态
            dt.Columns.Add("OrderStatus", typeof(String));
            ///报单类型
            dt.Columns.Add("OrderType", typeof(String));
            ///今成交数量
            dt.Columns.Add("VolumeTraded", typeof(long));
            ///剩余数量
            dt.Columns.Add("VolumeTotal", typeof(long));
            ///报单日期
       //     dt.Columns.Add("InsertDate", typeof(String));
            ///委托时间
            dt.Columns.Add("InsertTime", typeof(String));
            ///激活时间
            dt.Columns.Add("ActiveTime", typeof(String));
            ///挂起时间
            dt.Columns.Add("SuspendTime", typeof(String));
            ///最后修改时间
            dt.Columns.Add("UpdateTime", typeof(String));
            ///撤销时间
            dt.Columns.Add("CancelTime", typeof(String));
            ///最后修改交易所交易员代码
         //   dt.Columns.Add("ActiveTraderID", typeof(String));
            ///结算会员编号
         //   dt.Columns.Add("ClearingPartID", typeof(String));
            ///序号
            dt.Columns.Add("SequenceNo", typeof(long));
            ///前置编号
            dt.Columns.Add("FrontID", typeof(long));
            ///会话编号
            dt.Columns.Add("SessionID", typeof(long));
            ///用户端产品信息
            dt.Columns.Add("UserProductInfo", typeof(String));
            ///状态信息
            dt.Columns.Add("StatusMsg", typeof(String));
            ///用户强评标志
            dt.Columns.Add("UserForceClose", typeof(String));
            ///操作用户代码
            dt.Columns.Add("ActiveUserID", typeof(String));
            ///经纪公司报单编号
            dt.Columns.Add("BrokerOrderSeq", typeof(long));
            ///相关报单
            dt.Columns.Add("RelativeOrderSysID", typeof(String));
            ///郑商所成交数量
            dt.Columns.Add("ZCETotalTradedVolume", typeof(long));
            ///互换单标志
            dt.Columns.Add("IsSwapOrder", typeof(long));

            dt.PrimaryKey = new DataColumn[] { dt.Columns["OrderSysID"] };
        }

        public void InitTrade(DataSet ds)
        {
            DataTable dt = ds.Tables.Add("trade");

            ///经纪公司代码
            dt.Columns.Add("BrokerID", typeof(String));
            ///投资者代码
            dt.Columns.Add("InvestorID", typeof(String));
            ///合约代码
            dt.Columns.Add("InstrumentID", typeof(String));
            ///报单引用
            dt.Columns.Add("OrderRef", typeof(String));
            ///用户代码
            dt.Columns.Add("UserID", typeof(String));
            ///交易所代码
            dt.Columns.Add("ExchangeID", typeof(String));
            ///成交编号
            dt.Columns.Add("TradeID", typeof(String));
            ///买卖方向
            dt.Columns.Add("Direction", typeof(String));
            ///报单编号
            dt.Columns.Add("OrderSysID", typeof(String));
            ///会员代码
            dt.Columns.Add("ParticipantID", typeof(String));
            ///客户代码
            dt.Columns.Add("ClientID", typeof(String));
            ///交易角色
            dt.Columns.Add("TradingRole", typeof(String));
            ///合约在交易所的代码
            dt.Columns.Add("ExchangeInstID", typeof(String));
            ///开平标志
            dt.Columns.Add("OffsetFlag", typeof(String));
            ///投机套保标志
            dt.Columns.Add("HedgeFlag", typeof(String));
            ///价格
            dt.Columns.Add("Price", typeof(double));
            ///数量
            dt.Columns.Add("Volume", typeof(long));
            ///成交时期
            dt.Columns.Add("TradeDate", typeof(String));
            ///成交时间
            dt.Columns.Add("TradeTime", typeof(String));
            ///成交类型
            dt.Columns.Add("TradeType", typeof(String));
            ///成交价来源
            dt.Columns.Add("PriceSource", typeof(String));
            ///交易所交易员代码
            dt.Columns.Add("TraderID", typeof(String));
            ///本地报单编号
            dt.Columns.Add("OrderLocalID", typeof(String));
            ///结算会员编号
            dt.Columns.Add("ClearingPartID", typeof(String));
            ///业务单元
            dt.Columns.Add("BusinessUnit", typeof(String));
            ///序号
            dt.Columns.Add("SequenceNo", typeof(long));
            ///交易日
            dt.Columns.Add("TradingDay", typeof(String));
            ///结算编号
            dt.Columns.Add("SettlementID", typeof(long));
            ///经纪公司报单编号
            dt.Columns.Add("BrokerOrderSeq", typeof(long));
            ///成交来源
            dt.Columns.Add("TradeSource", typeof(String));


            // dt.PrimaryKey = new DataColumn[] { dt.Columns["InstrumentID"], dt.Columns["brokerID"], dt.Columns["InvestorID"], dt.Columns["PosiDirection"] };
        }

    /*    // 保证金率
        public void InitMarginRate(DataSet ds)
        {
            DataTable dt = ds.Tables.Add("marginrate");

            ///合约代码
            dt.Columns.Add("InstrumentID", typeof(String));
            ///投资者范围
            dt.Columns.Add("InvestorRange", typeof(String));
            ///经纪公司代码
            dt.Columns.Add("BrokerID", typeof(String));
            ///投资者代码
            dt.Columns.Add("InvestorID", typeof(String));
            ///投机套保标志
            dt.Columns.Add("HedgeFlag", typeof(String));
            ///多头保证金率
            dt.Columns.Add("LongMarginRatioByMoney", typeof(double));
            ///多头保证金费
            dt.Columns.Add("LongMarginRatioByVolume", typeof(double));
            ///空头保证金率
            dt.Columns.Add("ShortMarginRatioByMoney", typeof(double));
            ///空头保证金费
            dt.Columns.Add("ShortMarginRatioByVolume", typeof(double));
            ///是否相对交易所收取
            dt.Columns.Add("IsRelative", typeof(String));

            dt.PrimaryKey = new DataColumn[] { dt.Columns["InstrumentID"] };
        }
*/
        // 合约
        public void InitInstrument(DataSet ds)
        {
            DataTable dt = ds.Tables.Add("instrument");
            ///合约代码
            dt.Columns.Add("InstrumentID", typeof(String));
            ///交易所代码
           // dt.Columns.Add("ExchangeID", typeof(String));
            ///合约名称
          //  dt.Columns.Add("InstrumentName", typeof(String));
            ///合约在交易所的代码
          //  dt.Columns.Add("ExchangeInstID", typeof(String));
            ///产品代码
         //   dt.Columns.Add("ProductID", typeof(String));
            ///产品类型
          //  dt.Columns.Add("ProductClass", typeof(String));
            ///交割年份
          //  dt.Columns.Add("DeliveryYear", typeof(long));
            ///交割月
          //  dt.Columns.Add("DeliveryMonth", typeof(long));
            ///市价单最大下单量
          //  dt.Columns.Add("MaxMarketOrderVolume", typeof(double));
            ///市价单最小下单量
         //   dt.Columns.Add("MinMarketOrderVolume", typeof(double));
            ///限价单最大下单量
          //  dt.Columns.Add("MaxLimitOrderVolume", typeof(double));
            ///限价单最小下单量
           // dt.Columns.Add("MinLimitOrderVolume", typeof(double));
            ///合约数量乘数
              dt.Columns.Add("VolumeMultiple", typeof(double));
            ///最小变动价位
          //  dt.Columns.Add("PriceTick", typeof(double));
            ///创建日
          //  dt.Columns.Add("CreateDate", typeof(String));
            ///上市日
          //  dt.Columns.Add("OpenDate", typeof(String));
            ///到期日
         //   dt.Columns.Add("ExpireDate", typeof(String));
            ///开始交割日
         //   dt.Columns.Add("StartDelivDate", typeof(String));
            ///结束交割日
         //   dt.Columns.Add("EndDelivDate", typeof(String));
            ///合约生命周期状态
         //   dt.Columns.Add("InstLifePhase", typeof(String));
            ///当前是否交易
         //   dt.Columns.Add("IsTrading", typeof(long));
            ///持仓类型
            //dt.Columns.Add("PositionType", typeof(String));
            ///持仓日期类型
            dt.Columns.Add("PositionDateType", typeof(String));
            ///多头保证金率
            dt.Columns.Add("LongMarginRatio", typeof(double));
            ///空头保证金率
            dt.Columns.Add("ShortMarginRatio", typeof(double));
            ///是否使用大额单边保证金算法
        //    dt.Columns.Add("MaxMarginSideAlgorithm", typeof(String));
            ///基础商品代码
       //     dt.Columns.Add("UnderlyingInstrID", typeof(String));
            ///执行价
          //  dt.Columns.Add("StrikePrice", typeof(double));
            ///期权类型
         //   dt.Columns.Add("OptionsType", typeof(String));
            ///合约基础商品乘数
        //    dt.Columns.Add("UnderlyingMultiple", typeof(double));
            ///组合类型
            dt.Columns.Add("CombinationType", typeof(String));

            dt.PrimaryKey = new DataColumn[] { dt.Columns["InstrumentID"] };
        }    
    }
}
