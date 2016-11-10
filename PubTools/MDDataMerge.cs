using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Threading;
using MySql.Data.MySqlClient;

namespace PubTools
{
    public class MDDataMerge
    {
        public int progress = 0;
        public int newDataNum = 0;
        public int totalRows = 0;
        public int thisRow = 0;

        DataTable dt = null;
        DataTable dtDB = null;

        public MDDataMerge(DataTable paraDt, DataTable paraDtDb)
        {            
            dt = paraDt;
            dtDB = paraDtDb;
            progress = 0;
            newDataNum = 0;

            Thread t = new Thread(MergeMarketData);
            t.Start();
        }

        // 合并数据并回写DB，记录新补充数据记录条数
        public void MergeMarketData()
        {
            // 合并数据，并写数据库
            totalRows = dt.Rows.Count;
            PubTools.GlobalVar.mysqltool.StartTransaction();
            for (int i = 0; i < totalRows;i++ )
            {
                DataRow dr = dt.Rows[i];
                String TradingDay = (String)dr["TradingDay"];
                long volume = (long)dr["Volume"];
                DataRow drFind = dtDB.Rows.Find(new object[] {TradingDay, volume});
                if (drFind == null)
                {
                    // 新数据，需要补入
                    dtDB.ImportRow(dr);
                    PubTools.GlobalVar.mysqltool.InsertMD(dr);
                    newDataNum = newDataNum + 1;
                }
                progress = i * 100 / totalRows;
                thisRow = i;
            }
            PubTools.GlobalVar.mysqltool.Commit();
            
            this.progress = -1;
        }
    }
}



/*
            // Create the InsertCommand.         
            String insertcmd = @"INSERT INTO marketdata.md_import (
                    TradingDay, InstrumentID, LastPrice, PreSettlementPrice, PreClosePrice, 
                    OpenPrice, HighestPrice, LowestPrice, Volume, OpenInterest,
                    SettlementPrice, UpdateTime, BidPrice1, BidVolume1, AskPrice1,
                    AskVolume1, AveragePrice, ActionDay, UpAndDown)
                VALUES (
                    @TradingDay, @InstrumentID, @LastPrice, @PreSettlementPrice, @PreClosePrice, 
                    @OpenPrice, @HighestPrice, @LowestPrice, @Volume, @OpenInterest,
                    @SettlementPrice, @UpdateTime, @BidPrice1, @BidVolume1, @AskPrice1,
                    @AskVolume1, @AveragePrice, @ActionDay, @UpAndDown)";
            MySqlCommand mysqlcmd = new MySqlCommand(insertcmd, conn);
            mysqlcmd.Parameters.Add("@TradingDay", MySqlDbType.VarChar, 8, "TradingDay");
            mysqlcmd.Parameters.Add("@InstrumentID", MySqlDbType.VarChar, 10, "InstrumentID");
            mysqlcmd.Parameters.Add("@LastPrice", MySqlDbType.Int32, 11, "LastPrice");
            mysqlcmd.Parameters.Add("@PreSettlementPrice", MySqlDbType.Int32, 11, "PreSettlementPrice");
            mysqlcmd.Parameters.Add("@PreClosePrice", MySqlDbType.Int32, 11, "PreClosePrice");
            mysqlcmd.Parameters.Add("@OpenPrice", MySqlDbType.Int32, 11, "OpenPrice");
            mysqlcmd.Parameters.Add("@HighestPrice", MySqlDbType.Int32, 11, "HighestPrice");
            mysqlcmd.Parameters.Add("@LowestPrice", MySqlDbType.Int32, 11, "LowestPrice");
            mysqlcmd.Parameters.Add("@Volume", MySqlDbType.Int32, 11, "Volume");
            mysqlcmd.Parameters.Add("@OpenInterest", MySqlDbType.Int32, 11, "OpenInterest");
            mysqlcmd.Parameters.Add("@SettlementPrice", MySqlDbType.Int32, 11, "SettlementPrice");
            mysqlcmd.Parameters.Add("@UpdateTime", MySqlDbType.VarChar, 8, "UpdateTime");
            mysqlcmd.Parameters.Add("@BidPrice1", MySqlDbType.Int32, 11, "BidPrice1");
            mysqlcmd.Parameters.Add("@BidVolume1", MySqlDbType.Int32, 11, "BidVolume1");
            mysqlcmd.Parameters.Add("@AskPrice1", MySqlDbType.Int32, 11, "AskPrice1");
            mysqlcmd.Parameters.Add("@AskVolume1", MySqlDbType.Int32, 11, "AskVolume1");
            mysqlcmd.Parameters.Add("@AveragePrice", MySqlDbType.Int32, 11, "AveragePrice");
            mysqlcmd.Parameters.Add("@ActionDay", MySqlDbType.VarChar, 8, "ActionDay");
            mysqlcmd.Parameters.Add("@UpAndDown", MySqlDbType.VarChar, 8, "UpAndDown");

            da.InsertCommand = mysqlcmd;
            int rows = da.Update(GlobalVar.ds, "md_import");
            dtDB.AcceptChanges();
             * */
