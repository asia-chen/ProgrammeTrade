using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;

namespace PubTools
{
    public class MySqlTool
    {
        public String connStr = "";
        MySqlConnection conn = null;
        MySqlTransaction transaction = null;

        // 连接
        public Boolean Connect()
        {
            // 连接串空
            if (connStr.Equals(""))
                return false;

            // 已连接
            if (conn != null)
                return true;

            // 连接数据库
            conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        // 断开连接
        public void DisConnect()
        {
            if (conn == null)
                return;

            if (transaction != null)
                transaction.Commit();

            conn.Close();
            conn = null;
        }
       
        // 
        public void StartTransaction()
        {
            if (conn == null)
                return;
            if (transaction != null)
                return;
            transaction = conn.BeginTransaction();
        }

        public void Commit()
        {
            if (transaction == null)
                return;

            transaction.Commit();
            transaction = null;
        }

        public void Rollback()
        {
            if (transaction == null)
                return;

            transaction.Rollback();
            transaction = null;
        }

        // 从DB读数据到本地datatable中
        public void ReadData(String cmd, String tablename)
        {
            MySqlDataAdapter da = new MySqlDataAdapter(cmd, conn);
            da.Fill(GlobalVar.ds, tablename);
        }

        // 插入行情数据
        public Boolean InsertMD(DataRow dr)
        {
            String sqlstr = @"INSERT INTO md_import (
                TradingDay, InstrumentID, LastPrice, Volume, OpenInterest, 
                UpdateTime, BidPrice1, BidVolume1, AskPrice1, 
                AskVolume1, ActionDay) VALUES (";

            sqlstr += "'" + (String)dr["TradingDay"] + "',";
            sqlstr += "'" + (String)dr["InstrumentID"] + "',";
            sqlstr += (double)dr["LastPrice"] + ",";
            sqlstr += (long)dr["Volume"] + ",";
            sqlstr += (long)dr["OpenInterest"] + ",";
            sqlstr += "'" + (String)dr["UpdateTime"] + "',";
            sqlstr += (double)dr["BidPrice1"] + ",";
            sqlstr += (long)dr["BidVolume1"] + ",";
            sqlstr += (double)dr["AskPrice1"] + ",";
            sqlstr += (long)dr["AskVolume1"] + ",";
            sqlstr += "'" + (String)dr["ActionDay"] + "')";
            MySqlCommand cmd = new MySqlCommand(sqlstr, conn);
            if (cmd.ExecuteNonQuery() <= 0)
            {
                return false;
            }
            return true;
        }
       
        // 获取DB中各合同有行情数据的日期
        public void GetSimpleInstruments()
        {
            String cmd = "select a.InstrumentID,max(TradingDay) as maxTradingDay, min(TradingDay) as minTradingDay from md_import a group by InstrumentID order by InstrumentID";
            GlobalVar.mysqltool.ReadData(cmd, "simpleInstruments");
        }

        // 执行SQL：增删改
        public int Execute(String sqlStr)
        {
            MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
            return cmd.ExecuteNonQuery();
        }

        // 插入交易日
        public Boolean InsertTradingDays(String dt)
        {
            String sqlstr = @"INSERT INTO tradingdays (TradingDay) Values ('" + dt + "')";
            MySqlCommand cmd = new MySqlCommand(sqlstr, conn);
            if (cmd.ExecuteNonQuery() <= 0)
            {
                return false;
            }
            return true;
        }

        // 获取上一交易日，查不到、节假日后第一天，返回空
        public String GetLastDay(String thisDt)
        {
            String result = "";
            String sqlStr = "select * from tradingdays where TradingDay=(select max(TradingDay) from tradingdays where TradingDay<'" + thisDt + "')";
            MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                if (reader.GetInt32(1) != 1)
                {
                    result = reader.GetString(0);
                    reader.Close();
                }
            }
            return result;
        }
    }
}
