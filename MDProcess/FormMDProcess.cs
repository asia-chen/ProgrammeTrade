using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Threading;

using PubTools;
using DevExpress.XtraCharts;

namespace MDProcess
{
    public partial class FormMDProcess : Form
    {
        private String dataDir = "";

        public FormMDProcess()
        {
            InitializeComponent();
        }

        // 初始化
        private void FormMDProcess_Load(object sender, EventArgs e)
        {
            progressBar.Visible = false;
            dtPicker.Visible = false;
        }

        // 选择目录并读取
        private void bReadData_Click(object sender, EventArgs e)
        {
            FormTool.DisableButtons(this);
            FolderBrowserDialog dataRoot = new FolderBrowserDialog();
            dataRoot.SelectedPath = PubTools.GlobalVar.appConfig.defaultDir;
            if (dataRoot.ShowDialog() != DialogResult.OK)
            {
                FormTool.EnableButtons(this);
                dataDir = "";
                return;                
            }

            dataDir = dataRoot.SelectedPath;
            lbCurOp.Items.Clear();
            this.progressBar.Visible = true;
            bgWorker.RunWorkerAsync();
        }

        // 修改listbox内容
        public delegate void SetList(String msg);
        public void SetListImpl(String msg)
        {
            if (lbCurOp.InvokeRequired)
            {
                this.Invoke(new SetList(SetListImpl));
            }
            else
            {
                msg = DateTime.Now.ToString("HH:mm:ss") + "  " + msg;
                lbCurOp.Items.Insert(0, msg);
            }
        }

        // 修改progressbar
        public delegate void SetProgress(int progress);
        public void SetProgressImpl(int progress)
        {
            if (progressBar.InvokeRequired)
            {
                this.Invoke(new SetProgress(SetProgressImpl));
            }
            else
            {                
                Font font = this.Font;
                PointF pt = new PointF(this.progressBar.Width / 2 - 10, this.progressBar.Height / 2 - 10);                
                this.progressBar.CreateGraphics().DrawString(progress.ToString() + "%", font, (Brush)(new SolidBrush(this.ForeColor)), pt);
                progressBar.Value = progress;
            }
        }
        
        // 修改状态栏
        public delegate void SetStatus(String msg);
        public void SetStatusImpl(String msg)
        {
            if (this.statusStrip.InvokeRequired)
            {
                this.Invoke(new SetStatus(SetStatusImpl));
            }
            else
            {
                this.statusLabelLeft.Text = msg;
            }
        }
        
        // 数据读取及合并
        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (dataDir.Equals(""))
                return;
            if (!PubTools.GlobalVar.mysqltool.Connect())
            {
                MessageBox.Show("无法连接数据库，请检查");
                return;
            }

            SetList setlist = new SetList(SetListImpl);
            SetProgress setprogress = new SetProgress(SetProgressImpl);
            SetStatus setstatus = new SetStatus(SetStatusImpl);
            int newDataNum = 0;

            // 列出子目录
            DirectoryInfo info = new DirectoryInfo(dataDir);
            DirectoryInfo[] subDirList = info.GetDirectories();
            for (int i = 0; i < subDirList.Length; i++)
            {
                newDataNum = 0;
                String path = dataDir + "\\" + subDirList[i].Name;
                int pathLen = path.Length + 1;
                String tableName = subDirList[i].Name;

                this.Invoke(setprogress, 0);
                // 新建TABLE
                this.Invoke(setlist, tableName + ": Read XML File Begin.");
                DataTable dt = PubTools.GlobalVar.ds.Tables.Add(tableName);
                DataTool.InitMDTable(dt);
                
                // 逐个文件读入本地datatable，并合并到数据库
                String[] filenames = System.IO.Directory.GetFiles(path);
                for (int j = 0; j < filenames.Length; j++)
                {
                    String filename = filenames[j].Substring(pathLen);
                    try
                    {
                        dt.ReadXml(filenames[j]);
                    }
                    catch (Exception ex)
                    {
                        this.Invoke(setlist, tableName + ": Err " + filenames[j].Substring(pathLen) + " " + ex.Message);
                        continue;
                    }

                    // 根据本地数据获取：时间段
                    String maxDay = (String)dt.Compute("max(TradingDay)", "");
                    String minDay = (String)dt.Compute("min(TradingDay)", "");

                    // 读取数据库中数据
                    DataTable dtDB = GlobalVar.ds.Tables["md_import"];
                    dtDB.Rows.Clear();
                    String cmd = "select * from md_import where InstrumentID='" + tableName + "' and TradingDay between '" + minDay + "' and '" + maxDay + "'";
                    GlobalVar.mysqltool.ReadData(cmd, "md_import");
                    this.Invoke(setstatus, tableName + ": " + filename + " mem/DB: " + dt.Rows.Count.ToString() + "/" + dtDB.Rows.Count.ToString());

                    // 合并到数据库                
                    PubTools.MDDataMerge merge = new PubTools.MDDataMerge(dt, dtDB);
                    while (merge.progress >= 0)
                    {
                        Thread.Sleep(PubTools.Const.SleepTime);
                    }
                    this.Invoke(setprogress, 100 * j / filenames.Length);
                    newDataNum += merge.newDataNum;
                    dt.Rows.Clear();
                }
                this.Invoke(setlist, tableName + ": New Data rows number: " + newDataNum.ToString());

                // 清除数据
                GlobalVar.ds.Tables.Remove(dt);
                dt.Dispose();                
            }
            GlobalVar.mysqltool.DisConnect();
        }
        
        //数据读取、合并完毕
        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            FormTool.EnableButtons(this);
            this.lbCurOp.Items.Insert(0, "操作完成");
            this.progressBar.Visible = false;
        }
        
        // 从tick中汇总合同及日期，填入下拉框
        private void bGetInstrument_Click(object sender, EventArgs e)
        {
            FormTool.DisableButtons(this);
            lbCurOp.Items.Clear();
            cbInstrument.Items.Clear();
            dtPicker.Visible = false;

            try
            {
                GlobalVar.mysqltool.Connect();
                GlobalVar.mysqltool.GetSimpleInstruments();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            DataTable dt = GlobalVar.ds.Tables["simpleInstruments"];
            foreach (DataRow dr in dt.Rows)
            {
                cbInstrument.Items.Add((String)dr["InstrumentID"]);
            }

            if (dt.Rows.Count > 0)
            {
                cbInstrument.SelectedIndex = 0;
                dtPicker.Visible = true;
            }
            FormTool.EnableButtons(this);
        }
        
        // 根据合同确定日期范围，默认最后一天
        private void cbInstrument_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbInstrument.Items.Count <= 0)
                return;

            DataTable dt = GlobalVar.ds.Tables["simpleInstruments"];
            String thisInstrument = (String)cbInstrument.SelectedItem;
            DataRow dr = dt.Rows.Find(thisInstrument);
            String maxDay = (String)dr["maxTradingDay"];
            String minDay = (String)dr["minTradingDay"];

            dtPicker.MaxDate = DateTime.ParseExact(maxDay, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
            dtPicker.MinDate = DateTime.ParseExact(minDay, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);

            if (dtPicker.Value < dtPicker.MinDate)
                dtPicker.Value = dtPicker.MinDate;
            else if (dtPicker.Value > dtPicker.MaxDate)
                dtPicker.Value = dtPicker.MaxDate;
            dtPicker_ValueChanged(sender, e);
        }

        // 日期变化，读取数据、绘分时图
        private void dtPicker_ValueChanged(object sender, EventArgs e)
        {
            dtTick.Rows.Clear();
            DataProcess dp = null;
            try
            {
                dp = new DataProcess((String)cbInstrument.SelectedItem);
                dp.SetDisplayTick(dtTick, dtPicker.Value.ToString("yyyyMMdd"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (dtTick.Rows.Count > 0)
            {
                PubTools.FormTool.SetCustomLabel(chartControlTickVol, dp.drSegment);

                // 需考虑均价的最大最小值
                double maxprice = (double)dtTick.Compute("max(LastPrice)", "");
                double minprice = (double)dtTick.Compute("min(LastPrice)", "");
                WholeRange wr = (WholeRange)((XYDiagram)chartControlTick.Diagram).AxisY.WholeRange;
                VisualRange vr = (VisualRange)((XYDiagram)chartControlTick.Diagram).AxisY.VisualRange;
                wr.SetMinMaxValues(minprice, maxprice);
                vr.SetMinMaxValues(minprice, maxprice);
                
                // 设置持仓量坐标轴
                long maxOpenInterest = (long)dtTick.Compute("max(OpenInterest)", "");
                long minOpenInterest = (long)dtTick.Compute("min(OpenInterest)", "");
                wr = (WholeRange)((XYDiagram)chartControlTickVol.Diagram).SecondaryAxesY[0].WholeRange;
                vr = (VisualRange)((XYDiagram)chartControlTickVol.Diagram).SecondaryAxesY[0].VisualRange;
                wr.SetMinMaxValues(minOpenInterest, maxOpenInterest);
                vr.SetMinMaxValues(minOpenInterest, maxOpenInterest);
            }
            chartControlTick.Invalidate();
            chartControlTickVol.Invalidate();
        }
    }
}
