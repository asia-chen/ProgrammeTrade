using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

namespace PubTools.data
{
    public class Trade
    {
        ///经纪公司代码
        String BrokerID;
        ///投资者代码
        String InvestorID;
        ///合约代码
        String InstrumentID;
        ///报单引用
        String OrderRef;
        ///用户代码
        String UserID;
        ///交易所代码
        String ExchangeID;
        ///成交编号
        String TradeID;
        ///买卖方向
        String Direction;
        ///报单编号
        String OrderSysID;
        ///会员代码
        String ParticipantID;
        ///客户代码
        String ClientID;
        ///交易角色
        String TradingRole;
        ///合约在交易所的代码
        String ExchangeInstID;
        ///开平标志
        String OffsetFlag;
        ///投机套保标志
        String HedgeFlag;
        ///价格
        double Price;
        ///数量
        long Volume;
        ///成交时期
        String TradeDate;
        ///成交时间
        String TradeTime;
        ///成交类型
        String TradeType;
        ///成交价来源
        String PriceSource;
        ///交易所交易员代码
        String TraderID;
        ///本地报单编号
        String OrderLocalID;
        ///结算会员编号
        String ClearingPartID;
        ///业务单元
        String BusinessUnit;
        ///序号
        long SequenceNo;
        ///交易日
        String TradingDay;
        ///结算编号
        long SettlementID;
        ///经纪公司报单编号
        long BrokerOrderSeq;
        ///成交来源
        String TradeSource;

        public void SetData(String[] resStr, int offset)
        {
            int pos = 6 + offset;
            ///经纪公司代码
            BrokerID = resStr[pos + 0];
            ///投资者代码
            InvestorID = resStr[pos + 1];
            ///合约代码
            InstrumentID = resStr[pos + 2];
            ///报单引用
            OrderRef = resStr[pos + 3];
            ///用户代码
            UserID = resStr[pos + 4];
            ///交易所代码
            ExchangeID = resStr[pos + 5];
            ///成交编号
            TradeID = resStr[pos + 6];
            ///买卖方向
            Direction = resStr[pos + 7];
            ///报单编号
            OrderSysID = resStr[pos + 8];
            ///会员代码
            ParticipantID = resStr[pos + 9];
            ///客户代码
            ClientID = resStr[pos + 10];
            ///交易角色
            TradingRole = resStr[pos + 11];
            ///合约在交易所的代码
            ExchangeInstID = resStr[pos + 12];
            ///开平标志
            OffsetFlag = resStr[pos + 13];
            ///投机套保标志
            HedgeFlag = resStr[pos + 14];
            ///价格
            Price = double.Parse(resStr[pos + 15]);
            ///数量
            Volume = long.Parse(resStr[pos + 16]);
            ///成交时期
            TradeDate = resStr[pos + 17];
            ///成交时间
            TradeTime = resStr[pos + 18];
            ///成交类型
            TradeType = resStr[pos + 19];
            ///成交价来源
            PriceSource = resStr[pos + 20];
            ///交易所交易员代码
            TraderID = resStr[pos + 21];
            ///本地报单编号
            OrderLocalID = resStr[pos + 22];
            ///结算会员编号
            ClearingPartID = resStr[pos + 23];
            ///业务单元
            BusinessUnit = resStr[pos + 24];
            ///序号
            SequenceNo = long.Parse(resStr[pos + 25]);
            ///交易日
            TradingDay = resStr[pos + 26];
            ///结算编号
            SettlementID = long.Parse(resStr[pos + 27]);
            ///经纪公司报单编号
            BrokerOrderSeq = long.Parse(resStr[pos + 28]);
            ///成交来源
            TradeSource = resStr[pos + 29];
        }

        private void AddToDatatable(DataTable dt)
        {
                DataRow dr = dt.NewRow();

                ///经纪公司代码
                dr["BrokerID"] = this.BrokerID;
                ///投资者代码
                dr["InvestorID"] = this.InvestorID;
                ///合约代码
                dr["InstrumentID"] = this.InstrumentID;
                ///报单引用
                dr["OrderRef"] = this.OrderRef;
                ///用户代码
                dr["UserID"] = this.UserID;
                ///交易所代码
                dr["ExchangeID"] = this.ExchangeID;
                ///成交编号
                dr["TradeID"] = this.TradeID;
                ///买卖方向
                dr["Direction"] = this.Direction;
                ///报单编号
                dr["OrderSysID"] = this.OrderSysID;
                ///会员代码
                dr["ParticipantID"] = this.ParticipantID;
                ///客户代码
                dr["ClientID"] = this.ClientID;
                ///交易角色
                dr["TradingRole"] = this.TradingRole;
                ///合约在交易所的代码
                dr["ExchangeInstID"] = this.ExchangeInstID;
                ///开平标志
                dr["OffsetFlag"] = this.OffsetFlag;
                ///投机套保标志
                dr["HedgeFlag"] = this.HedgeFlag;
                ///价格
                dr["Price"] = this.Price;
                ///数量
                dr["Volume"] = this.Volume;
                ///成交时期
                dr["TradeDate"] = this.TradeDate;
                ///成交时间
                dr["TradeTime"] = this.TradeTime;
                ///成交类型
                dr["TradeType"] = this.TradeType;
                ///成交价来源
                dr["PriceSource"] = this.PriceSource;
                ///交易所交易员代码
                dr["TraderID"] = this.TraderID;
                ///本地报单编号
                dr["OrderLocalID"] = this.OrderLocalID;
                ///结算会员编号
                dr["ClearingPartID"] = this.ClearingPartID;
                ///业务单元
                dr["BusinessUnit"] = this.BusinessUnit;
                ///序号
                dr["SequenceNo"] = this.SequenceNo;
                ///交易日
                dr["TradingDay"] = this.TradingDay;
                ///结算编号
                dr["SettlementID"] = this.SettlementID;
                ///经纪公司报单编号
                dr["BrokerOrderSeq"] = this.BrokerOrderSeq;
                ///成交来源
                dr["TradeSource"] = this.TradeSource;

                dt.Rows.Add(dr);
        }
    }
}
