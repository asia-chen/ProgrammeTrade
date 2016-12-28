using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PubTools.data
{
    public class Instrument
    {
        ///合约代码
        public String InstrumentID { get; set; }
        ///交易所代码
        public String ExchangeID { get; set; }
        ///合约名称
        public String InstrumentName { get; set; }
        ///合约在交易所的代码
        public String ExchangeInstID { get; set; }
        ///产品代码
        String ProductID;
        ///产品类型
        String ProductClass;
        ///交割年份
        long DeliveryYear;
        ///交割月
        long DeliveryMonth;
        ///市价单最大下单量
        double MaxMarketOrderVolume;
        ///市价单最小下单量
        double MinMarketOrderVolume;
        ///限价单最大下单量
        public double MaxLimitOrderVolume { get; set; }
        ///限价单最小下单量
        public double MinLimitOrderVolume { get; set; }
        ///合约数量乘数
        public double VolumeMultiple { get; set; }
        ///最小变动价位
        public double PriceTick { get; set; }
        ///创建日
        String CreateDate;
        ///上市日
        String OpenDate;
        ///到期日
        String ExpireDate;
        ///开始交割日
        String StartDelivDate;
        ///结束交割日
        String EndDelivDate;
        ///合约生命周期状态
        String InstLifePhase;
        ///当前是否交易
        long IsTrading;
        ///持仓类型
        String PositionType;
        ///持仓日期类型
        String PositionDateType;
        ///多头保证金率
        public double LongMarginRatio { get; set; }
        ///空头保证金率
        public double ShortMarginRatio { get; set; }
        ///是否使用大额单边保证金算法
        String MaxMarginSideAlgorithm;
        ///基础商品代码
        String UnderlyingInstrID;
        ///执行价
        double StrikePrice;
        ///期权类型
        String OptionsType;
        ///合约基础商品乘数
        public double UnderlyingMultiple { get; set; }
        ///组合类型
        String CombinationType;

        // 解析返回数据
        public int SetData(String[] resStr, int pos)
        {
            if (resStr.Length < pos)
                return -1;

            ///合约代码
            InstrumentID = resStr[pos + 0];
            ///交易所代码
            ExchangeID = resStr[pos + 1];
            ///合约名称
            InstrumentName = resStr[pos + 2];
            ///合约在交易所的代码
            ExchangeInstID = resStr[pos + 3];
            ///产品代码
            ProductID = resStr[pos + 4];
            ///产品类型
            ProductClass = resStr[pos + 5];
            ///交割年份
            DeliveryYear = long.Parse(resStr[pos + 6]);
            ///交割月
            DeliveryMonth = long.Parse(resStr[pos + 7]);
            ///市价单最大下单量
            MaxMarketOrderVolume = double.Parse(resStr[pos + 8]);
            ///市价单最小下单量
            MinMarketOrderVolume = double.Parse(resStr[pos + 9]);
            ///限价单最大下单量
            MaxLimitOrderVolume = double.Parse(resStr[pos + 10]);
            ///限价单最小下单量
            MinLimitOrderVolume = double.Parse(resStr[pos + 11]);
            ///合约数量乘数
            VolumeMultiple = double.Parse(resStr[pos + 12]);
            ///最小变动价位
            PriceTick = double.Parse(resStr[pos + 13]);
            ///创建日
            CreateDate = resStr[pos + 14];
            ///上市日
            OpenDate = resStr[pos + 15];
            ///到期日
            ExpireDate = resStr[pos + 16];
            ///开始交割日
            StartDelivDate = resStr[pos + 17];
            ///结束交割日
            EndDelivDate = resStr[pos + 18];
            ///合约生命周期状态
            InstLifePhase = resStr[pos + 19];
            ///当前是否交易
            IsTrading = long.Parse(resStr[pos + 20]);
            ///持仓类型
            PositionType = resStr[pos + 21];
            ///持仓日期类型
            PositionDateType = resStr[pos + 22];
            ///多头保证金率
            LongMarginRatio = double.Parse(resStr[pos + 23]);
            ///空头保证金率
            ShortMarginRatio = double.Parse(resStr[pos + 24]);
            ///是否使用大额单边保证金算法
            MaxMarginSideAlgorithm = resStr[pos + 25];
            ///基础商品代码
            UnderlyingInstrID = resStr[pos + 26];
            ///执行价
            StrikePrice = double.Parse(resStr[pos + 27]);
            ///期权类型
            OptionsType = resStr[pos + 28];
            ///合约基础商品乘数
            UnderlyingMultiple = double.Parse(resStr[pos + 29]);
            ///组合类型
            CombinationType = resStr[pos + 30];

            return 0;
        }
    }
}
