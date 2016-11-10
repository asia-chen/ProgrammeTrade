using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using PubTools;

namespace MDProcess
{
    class DataProcess
    {
        public DataRow[] drSegment = null;

        private String productID = "";
        private String productSrc = "";
        private String instrumentID = "";
        private String periodID = "";

        public DataProcess(String paraInstrumentID)
        {
            if (!GlobalVar.mysqltool.Connect())
            {
                throw new Exception("Can't Connect DB");
            }
            instrumentID = paraInstrumentID;

            // 获取品种信息及对应交易时段
            productID = CommonTool.GetProduct(instrumentID);
            productSrc = (String)GlobalVar.ds.Tables["product"].Rows.Find(productID)["BelongTo"];
            periodID = (String)GlobalVar.ds.Tables["product"].Rows.Find(productID)["PeriodID"];
            drSegment = GlobalVar.ds.Tables["tradesegment"].Select("PeriodID='" + periodID + "'", "SeqNo asc");
            
            if (drSegment.Length <= 0)
                throw new Exception("Can't Find Instrument");            
        }
        
        // 读取DB中tick数据，并整理到显示用datatable中
        public void SetDisplayTick(DataTable dtTick, String dt)
        {
            // 清理本地数据，读入DB
            DataTable dtSrc = GlobalVar.ds.Tables["md_import"];
            dtSrc.Rows.Clear();

            // 获取数据：
            // 上海、金融：按tradingday获取
            // 大连、郑州：按tradingday及上一交易日获取
            String cmd = "select * from md_import where InstrumentID='" + instrumentID + "'";
            if (productSrc.Equals(Const.ExgSH) || productSrc.Equals(Const.ExgJR))
            {
                cmd = cmd + " and TradingDay='" + dt + "' ";
            }
            else
            {
                // 获取上一交易日
                String lastDay = "";
                if (productSrc.Equals(Const.ExgDL) || productSrc.Equals(Const.ExgZZ))
                {
                    lastDay = GlobalVar.mysqltool.GetLastDay(dt);
                }

                if (lastDay.Equals(""))
                {
                    cmd = cmd + " and TradingDay='" + dt + "' and UpdateTime<='15:00:00' ";
                }
                else
                {
                    cmd = cmd + " and (TradingDay='" + dt + "' and UpdateTime<='15:00:00' or TradingDay='" + lastDay + "' and UpdateTime>'21:00:00') ";
                }
            }
            cmd = cmd + " order by Volume";
            GlobalVar.mysqltool.ReadData(cmd, "md_import");

            if (dtSrc.Rows.Count <= 0)
                return;


            // 每交易日开盘时间、收盘时间
            String tmp = dt + (String)drSegment[0]["BeginTime"];
            DateTime dtBegin = DateTime.ParseExact(tmp, "yyyyMMddHHmm", System.Globalization.CultureInfo.CurrentCulture);
            dtBegin = dtBegin.AddDays((long)drSegment[0]["BeginDay"]);

            // 第一个点
            DataRow drTarget = dtTick.NewRow();
            DataRow drSrc = dtSrc.Rows[0];

            drTarget["UpdateTime"] = dtBegin.ToString("HH:mm");
            drTarget["LastPrice"] = (double)drSrc["PreClosePrice"];
            drTarget["AveragePrice"] = (double)drSrc["AveragePrice"] / 10.0;
            drTarget["Volume"] = 0;
            drTarget["OpenInterest"] = (long)drSrc["OpenInterest"];
            drTarget["PreClosePrice"] = (double)drSrc["PreClosePrice"];
            dtTick.Rows.Add(drTarget);

            DataRow drLast = dtTick.NewRow();
            CopyMDDataRow(drTarget, drLast);


            // 开始循环处理
            DateTime dtCur = dtBegin;
            int pData = 0;
            int totalData = dtSrc.Rows.Count;

            // 过滤交易时间外数据
            while (pData < totalData)
            {
                drSrc = dtSrc.Rows[pData];
                DateTime dtData = DateTime.ParseExact((String)drSrc["ActionDay"] + ((String)drSrc["UpdateTime"]).Substring(0,5), "yyyyMMddHH:mm", System.Globalization.CultureInfo.CurrentCulture);
                if (dtData <= dtCur)
                {
                    pData = pData + 1;
                }
                else
                {
                    break;
                }
            }
            
            for (int i = 0; i < drSegment.Length; i++)
            {
                DateTime dtSegBegin = DateTime.ParseExact(dt + (String)drSegment[i]["BeginTime"], "yyyyMMddHHmm", System.Globalization.CultureInfo.CurrentCulture);
                dtSegBegin = dtSegBegin.AddDays((long)drSegment[i]["BeginDay"]);
                if (dtCur < dtSegBegin)
                    dtCur = dtSegBegin.AddMinutes(1);

                DateTime dtEnd = DateTime.ParseExact(dt + (String)drSegment[i]["EndTime"], "yyyyMMddHHmm", System.Globalization.CultureInfo.CurrentCulture);
                dtEnd = dtEnd.AddDays((long)drSegment[i]["EndDay"]);
                while (dtCur <= dtEnd)
                {
                    while (pData < totalData)
                    {
                        drSrc = dtSrc.Rows[pData];
                        DateTime dtData = DateTime.ParseExact((String)drSrc["ActionDay"] + ((String)drSrc["UpdateTime"]).Substring(0,5), "yyyyMMddHH:mm", System.Globalization.CultureInfo.CurrentCulture);
                        if (dtData <= dtCur)
                        {
                            pData = pData + 1;
                            CopyMDDataRow(drSrc,drLast);
                        }
                        else
                        {
                            drLast["UpdateTime"] = dtCur.ToString("HH:mm");
                            drTarget = dtTick.NewRow();
                            CopyMDDataRow(drLast, drTarget);
                            dtTick.Rows.Add(drTarget);
                            break;
                        }
                    }
                    dtCur = dtCur.AddMinutes(1);
                }
            }
            dtTick.AcceptChanges();
            
            dtTick.WriteXml("tick.xml");
            // 整理成交量
            long vol = 0;
            for (int i = 0; i < dtTick.Rows.Count; i++)
            {
                long curvol = (long)dtTick.Rows[i]["Volume"];
                dtTick.Rows[i]["Volume"] = curvol - vol;
                vol = curvol;
            }
            dtTick.AcceptChanges();
        }

        private void CopyMDDataRow(DataRow drSrc, DataRow drTarget)
        {
            drTarget["UpdateTime"] = ((String)drSrc["UpdateTime"]).Substring(0, 5);
            drTarget["LastPrice"] = (double)drSrc["LastPrice"];
            drTarget["AveragePrice"] = (double)drSrc["AveragePrice"] / 10.0;
            drTarget["Volume"] = (long)drSrc["Volume"];
            drTarget["OpenInterest"] = (long)drSrc["OpenInterest"];
            double tmp = (double)drSrc["PreClosePrice"];
            drTarget["PreClosePrice"] = (double)drSrc["PreClosePrice"];
        }
    }
}
