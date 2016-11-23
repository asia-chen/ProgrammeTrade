using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

namespace PubTools.data
{
    public class Order : IComparable, ICloneable 
    {
        ///经纪公司代码
        String BrokerID;
        ///投资者代码
        String InvestorID;
        ///合约代码
        String InstrumentID;
        ///报单引用
        public String OrderRef;
        ///用户代码
        String UserID;
        ///报单价格条件
        String OrderPriceType;
        ///买卖方向
        String Direction;
        ///组合开平标志
        String CombOffsetFlag;
        ///组合投机套保标志
        String CombHedgeFlag;
        ///价格
        double LimitPrice;
        ///数量
        long VolumeTotalOriginal;
        ///有效期类型
        String TimeCondition;
        ///GTD日期
        String GTDDate;
        ///成交量类型
        String VolumeCondition;
        ///最小成交量
        long MinVolume;
        ///触发条件
        String ContingentCondition;
        ///止损价
        double StopPrice;
        ///强平原因
        String ForceCloseReason;
        ///自动挂起标志
        String IsAutoSuspend;
        ///业务单元
        String BusinessUnit;
        ///请求编号
        String RequestID;
        ///本地报单编号
        String OrderLocalID;
        ///交易所代码
        String ExchangeID;
        ///会员代码
        String ParticipantID;
        ///客户代码
        String ClientID;
        ///合约在交易所的代码
        String ExchangeInstID;
        ///交易所交易员代码
        String TraderID;
        ///安装编号
        long InstallID;
        ///报单提交状态
        String OrderSubmitStatus;
        ///报单提示序号
        long NotifySequence;
        ///交易日
        String TradingDay;
        ///结算编号
        long SettlementID;
        ///报单编号
        public String OrderSysID;
        ///报单来源
        String OrderSource;
        ///报单状态
        String OrderStatus;
        ///报单类型
        String OrderType;
        ///今成交数量
        long VolumeTraded;
        ///剩余数量
        long VolumeTotal;
        ///报单日期
        String InsertDate;
        ///委托时间
        String InsertTime;
        ///激活时间
        String ActiveTime;
        ///挂起时间
        String SuspendTime;
        ///最后修改时间
        String UpdateTime;
        ///撤销时间
        String CancelTime;
        ///最后修改交易所交易员代码
        String ActiveTraderID;
        ///结算会员编号
        String ClearingPartID;
        ///序号
        long SequenceNo;
        ///前置编号
        long FrontID;
        ///会话编号
        long SessionID;
        ///用户端产品信息
        String UserProductInfo;
        ///状态信息
        String StatusMsg;
        ///用户强评标志
        String UserForceClose;
        ///操作用户代码
        String ActiveUserID;
        ///经纪公司报单编号
        long BrokerOrderSeq;
        ///相关报单
        String RelativeOrderSysID;
        ///郑商所成交数量
        long ZCETotalTradedVolume;
        ///互换单标志
        long IsSwapOrder;

        // 排序使用
        public int CompareTo(Object o)
        {
            return this.OrderSysID.CompareTo(((Order)o).OrderSysID);
        }
        // 复制使用
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public void SetData(String[] resStr,int offset)
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
            ///报单价格条件
            OrderPriceType = resStr[pos + 5];
            ///买卖方向
            Direction = resStr[pos + 6];
            ///组合开平标志
            CombOffsetFlag = resStr[pos + 7];
            ///组合投机套保标志
            CombHedgeFlag = resStr[pos + 8];
            ///价格
            LimitPrice = double.Parse(resStr[pos + 9]);
            ///数量
            VolumeTotalOriginal = long.Parse(resStr[pos + 10]);
            ///有效期类型
            TimeCondition = resStr[pos + 11];
            ///GTD日期
            GTDDate = resStr[pos + 12];
            ///成交量类型
            VolumeCondition = resStr[pos + 13];
            ///最小成交量
            MinVolume = long.Parse(resStr[pos + 14]);
            ///触发条件
            ContingentCondition = resStr[pos + 15];
            ///止损价
            StopPrice = double.Parse(resStr[pos + 16]);
            ///强平原因
            ForceCloseReason = resStr[pos + 17];
            ///自动挂起标志
            IsAutoSuspend = resStr[pos + 18];
            ///业务单元
            BusinessUnit = resStr[pos + 19];
            ///请求编号
            RequestID = resStr[pos + 20];
            ///本地报单编号
            OrderLocalID = resStr[pos + 21];
            ///交易所代码
            ExchangeID = resStr[pos + 22];
            ///会员代码
            ParticipantID = resStr[pos + 23];
            ///客户代码
            ClientID = resStr[pos + 24];
            ///合约在交易所的代码
            ExchangeInstID = resStr[pos + 25];
            ///交易所交易员代码
            TraderID = resStr[pos + 26];
            ///安装编号
            InstallID = long.Parse(resStr[pos + 27]);
            ///报单提交状态
            OrderSubmitStatus = resStr[pos + 28];
            ///报单提示序号
            NotifySequence = long.Parse(resStr[pos + 29]);
            ///交易日
            TradingDay = resStr[pos + 30];
            ///结算编号
            SettlementID = long.Parse(resStr[pos + 31]);
            ///报单编号
            OrderSysID = resStr[pos + 32];
            ///报单来源
            OrderSource = resStr[pos + 33];
            ///报单状态
            OrderStatus = resStr[pos + 34];
            ///报单类型
            OrderType = resStr[pos + 35];
            ///今成交数量
            VolumeTraded = long.Parse(resStr[pos + 36]);
            ///剩余数量
            VolumeTotal = long.Parse(resStr[pos + 37]);
            ///报单日期
            InsertDate = resStr[pos + 38];
            ///委托时间
            InsertTime = resStr[pos + 39];
            ///激活时间
            ActiveTime = resStr[pos + 40];
            ///挂起时间
            SuspendTime = resStr[pos + 41];
            ///最后修改时间
            UpdateTime = resStr[pos + 42];
            ///撤销时间
            CancelTime = resStr[pos + 43];
            ///最后修改交易所交易员代码
            ActiveTraderID = resStr[pos + 44];
            ///结算会员编号
            ClearingPartID = resStr[pos + 45];
            ///序号
            SequenceNo = long.Parse(resStr[pos + 46]);
            ///前置编号
            FrontID = long.Parse(resStr[pos + 47]);
            ///会话编号
            SessionID = long.Parse(resStr[pos + 48]);
            ///用户端产品信息
            UserProductInfo = resStr[pos + 49];
            ///状态信息
            StatusMsg = resStr[pos + 50];
            ///用户强评标志
            UserForceClose = resStr[pos + 51];
            ///操作用户代码
            ActiveUserID = resStr[pos + 52];
            ///经纪公司报单编号
            BrokerOrderSeq = long.Parse(resStr[pos + 53]);
            ///相关报单
            RelativeOrderSysID = resStr[pos + 54];
            ///郑商所成交数量
            ZCETotalTradedVolume = long.Parse(resStr[pos + 55]);
            ///互换单标志
            IsSwapOrder = long.Parse(resStr[pos + 56]);

            Console.WriteLine("InsertTime:" + InsertTime + " LimitPrice:" + LimitPrice.ToString() 
                + " OrderStatus:" + OrderStatus
                + " CombOffsetFlag:" + CombOffsetFlag
                + " CombHedgeFlag:" + CombHedgeFlag
                + " StatusMsg:" + StatusMsg 
                + " OrderLocalID:" + OrderLocalID                 
                + " OrderSysID is: " + OrderSysID
                );

            if (!this.OrderSysID.Equals(""))
            {
                // AddToDatatable();
            }
        }

        private void AddToDatatable()
        {
            lock (GlobalVar.ds)
            {
                Boolean found = false;
                DataTable dt = GlobalVar.ds.Tables["order"];

                foreach (DataRow dr in dt.Rows)
                {
                    String tmp_OrderSysID = (String)dr["OrderSysID"];
                    if (tmp_OrderSysID.Equals(this.OrderSysID.Trim()))
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

        private void SetToDatarow(DataRow dr)
        {
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
            ///报单价格条件
            dr["OrderPriceType"] = this.OrderPriceType;
            ///买卖方向
            if (this.Direction.Equals("0"))
            {
                dr["Direction"] = "买";
            }
            else
            {
                dr["Direction"] = "卖";
            }
            ///组合开平标志
            dr["CombOffsetFlag"] = this.CombOffsetFlag;
            ///组合投机套保标志
            dr["CombHedgeFlag"] = this.CombHedgeFlag;
            ///价格
            dr["LimitPrice"] = this.LimitPrice;
            ///数量
            dr["VolumeTotalOriginal"] = this.VolumeTotalOriginal;
            ///有效期类型
            dr["TimeCondition"] = this.TimeCondition;
            ///GTD日期
            dr["GTDDate"] = this.GTDDate;
            ///成交量类型
            dr["VolumeCondition"] = this.VolumeCondition;
            ///最小成交量
            dr["MinVolume"] = this.MinVolume;
            ///触发条件
            dr["ContingentCondition"] = this.ContingentCondition;
            ///止损价
            dr["StopPrice"] = this.StopPrice;
            ///强平原因
            dr["ForceCloseReason"] = this.ForceCloseReason;
            ///自动挂起标志
            dr["IsAutoSuspend"] = this.IsAutoSuspend;
            ///业务单元
            dr["BusinessUnit"] = this.BusinessUnit;
            ///请求编号
            dr["RequestID"] = this.RequestID;
            ///本地报单编号
            dr["OrderLocalID"] = this.OrderLocalID;
            ///交易所代码
            dr["ExchangeID"] = this.ExchangeID;
            ///会员代码
            dr["ParticipantID"] = this.ParticipantID;
            ///客户代码
            dr["ClientID"] = this.ClientID;
            ///合约在交易所的代码
            dr["ExchangeInstID"] = this.ExchangeInstID;
            ///交易所交易员代码
            dr["TraderID"] = this.TraderID;
            ///安装编号
            dr["InstallID"] = this.InstallID;
            ///报单提交状态
            dr["OrderSubmitStatus"] = this.OrderSubmitStatus;
            ///报单提示序号
            dr["NotifySequence"] = this.NotifySequence;
            ///交易日
            dr["TradingDay"] = this.TradingDay;
            ///结算编号
            dr["SettlementID"] = this.SettlementID;
            ///报单编号
            dr["OrderSysID"] = this.OrderSysID.Trim();
            ///报单来源
            dr["OrderSource"] = this.OrderSource;
            ///报单状态
            dr["OrderStatus"] = this.OrderStatus;
            ///报单类型
            dr["OrderType"] = this.OrderType;
            ///今成交数量
            dr["VolumeTraded"] = this.VolumeTraded;
            ///剩余数量
            dr["VolumeTotal"] = this.VolumeTotal;
            ///报单日期
            dr["InsertDate"] = this.InsertDate;
            ///委托时间
            dr["InsertTime"] = this.InsertTime;
            ///激活时间
            dr["ActiveTime"] = this.ActiveTime;
            ///挂起时间
            dr["SuspendTime"] = this.SuspendTime;
            ///最后修改时间
            dr["UpdateTime"] = this.UpdateTime;
            ///撤销时间
            dr["CancelTime"] = this.CancelTime;
            ///最后修改交易所交易员代码
            dr["ActiveTraderID"] = this.ActiveTraderID;
            ///结算会员编号
            dr["ClearingPartID"] = this.ClearingPartID;
            ///序号
            dr["SequenceNo"] = this.SequenceNo;
            ///前置编号
            dr["FrontID"] = this.FrontID;
            ///会话编号
            dr["SessionID"] = this.SessionID;
            ///用户端产品信息
            dr["UserProductInfo"] = this.UserProductInfo;
            ///状态信息
            dr["StatusMsg"] = this.StatusMsg;
            ///用户强评标志
            dr["UserForceClose"] = this.UserForceClose;
            ///操作用户代码
            dr["ActiveUserID"] = this.ActiveUserID;
            ///经纪公司报单编号
            dr["BrokerOrderSeq"] = this.BrokerOrderSeq;
            ///相关报单
            dr["RelativeOrderSysID"] = this.RelativeOrderSysID;
            ///郑商所成交数量
            dr["ZCETotalTradedVolume"] = this.ZCETotalTradedVolume;
            ///互换单标志
            dr["IsSwapOrder"] = this.IsSwapOrder;
        }
    }
}
