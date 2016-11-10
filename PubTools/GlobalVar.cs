using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using System.Windows.Forms;

namespace PubTools
{
    public class GlobalVar
    {
        // 配置文件内容
        public static PubTools.AppConfig appConfig = null;
        public static DataSet ds = null;
        public static MySqlTool mysqltool = null;

        // 初始化
        public static void Init()
        {
            appConfig = new AppConfig();
            mysqltool = new MySqlTool();
            mysqltool.connStr = appConfig.mysqlConStr;
            if (!mysqltool.Connect())
            {
                MessageBox.Show("无法连接数据库，请检查配置文件");
                System.Environment.Exit(1);
            }
            
            // 初始化本地表，并读入基础数据
            ds = new DataSet();

            DataTable dt = ds.Tables.Add("md_import");
            DataTool.InitMDTable(dt);
            dt.PrimaryKey = new DataColumn[] { dt.Columns["TradingDay"], dt.Columns["Volume"] };

            dt = ds.Tables.Add("simpleInstruments");
            DataTool.InitSimpleInstruments(dt);
            
            dt = ds.Tables.Add("product");
            DataTool.InitProduct(dt);
            String cmd = "select * from product";
            mysqltool.ReadData(cmd, "product");
            
            dt = ds.Tables.Add("tradesegment");
            DataTool.InitTradeSeqgment(dt);
            cmd = "select * from tradesegment";
            mysqltool.ReadData(cmd, "tradesegment");

            mysqltool.DisConnect();
        }


    }
}
