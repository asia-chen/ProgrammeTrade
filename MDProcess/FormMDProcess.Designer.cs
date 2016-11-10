namespace MDProcess
{
    partial class FormMDProcess
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView1 = new DevExpress.XtraCharts.LineSeriesView();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView2 = new DevExpress.XtraCharts.LineSeriesView();
            DevExpress.XtraCharts.XYDiagram xyDiagram2 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.SecondaryAxisY secondaryAxisY1 = new DevExpress.XtraCharts.SecondaryAxisY();
            DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.Series series3 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView3 = new DevExpress.XtraCharts.LineSeriesView();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView4 = new DevExpress.XtraCharts.LineSeriesView();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMDProcess));
            this.dtTick = new System.Data.DataTable();
            this.UpdateTime = new System.Data.DataColumn();
            this.LastPrice = new System.Data.DataColumn();
            this.AveragePrice = new System.Data.DataColumn();
            this.Volume = new System.Data.DataColumn();
            this.OpenInterest = new System.Data.DataColumn();
            this.PreClosePrice = new System.Data.DataColumn();
            this.bReadData = new System.Windows.Forms.Button();
            this.bGetInstrument = new System.Windows.Forms.Button();
            this.lbCurOp = new System.Windows.Forms.ListBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabelLeft = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelCenter = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelRight = new System.Windows.Forms.ToolStripStatusLabel();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.dtPicker = new System.Windows.Forms.DateTimePicker();
            this.cbInstrument = new System.Windows.Forms.ComboBox();
            this.cbIndex = new System.Windows.Forms.ComboBox();
            this.bGenIndex = new System.Windows.Forms.Button();
            this.chartControlTick = new DevExpress.XtraCharts.ChartControl();
            this.dsDisplay = new System.Data.DataSet();
            this.chartControlTickVol = new DevExpress.XtraCharts.ChartControl();
            ((System.ComponentModel.ISupportInitialize)(this.dtTick)).BeginInit();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartControlTick)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsDisplay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControlTickVol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(secondaryAxisY1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView4)).BeginInit();
            this.SuspendLayout();
            // 
            // dtTick
            // 
            this.dtTick.Columns.AddRange(new System.Data.DataColumn[] {
            this.UpdateTime,
            this.LastPrice,
            this.AveragePrice,
            this.Volume,
            this.OpenInterest,
            this.PreClosePrice});
            this.dtTick.TableName = "dtTick";
            // 
            // UpdateTime
            // 
            this.UpdateTime.Caption = "时间";
            this.UpdateTime.ColumnName = "UpdateTime";
            // 
            // LastPrice
            // 
            this.LastPrice.Caption = "价格";
            this.LastPrice.ColumnName = "LastPrice";
            this.LastPrice.DataType = typeof(double);
            // 
            // AveragePrice
            // 
            this.AveragePrice.Caption = "均价";
            this.AveragePrice.ColumnName = "AveragePrice";
            this.AveragePrice.DataType = typeof(double);
            // 
            // Volume
            // 
            this.Volume.Caption = "成交量";
            this.Volume.ColumnName = "Volume";
            this.Volume.DataType = typeof(long);
            // 
            // OpenInterest
            // 
            this.OpenInterest.Caption = "持仓量";
            this.OpenInterest.ColumnName = "OpenInterest";
            this.OpenInterest.DataType = typeof(long);
            // 
            // PreClosePrice
            // 
            this.PreClosePrice.ColumnName = "PreClosePrice";
            this.PreClosePrice.DataType = typeof(double);
            // 
            // bReadData
            // 
            this.bReadData.Location = new System.Drawing.Point(14, 14);
            this.bReadData.Margin = new System.Windows.Forms.Padding(5);
            this.bReadData.Name = "bReadData";
            this.bReadData.Size = new System.Drawing.Size(187, 40);
            this.bReadData.TabIndex = 0;
            this.bReadData.Text = "选择目录导入、合并";
            this.bReadData.UseVisualStyleBackColor = true;
            this.bReadData.Click += new System.EventHandler(this.bReadData_Click);
            // 
            // bGetInstrument
            // 
            this.bGetInstrument.Location = new System.Drawing.Point(256, 14);
            this.bGetInstrument.Margin = new System.Windows.Forms.Padding(5);
            this.bGetInstrument.Name = "bGetInstrument";
            this.bGetInstrument.Size = new System.Drawing.Size(207, 40);
            this.bGetInstrument.TabIndex = 1;
            this.bGetInstrument.Text = "汇总数据";
            this.bGetInstrument.UseVisualStyleBackColor = true;
            this.bGetInstrument.Click += new System.EventHandler(this.bGetInstrument_Click);
            // 
            // lbCurOp
            // 
            this.lbCurOp.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbCurOp.FormattingEnabled = true;
            this.lbCurOp.HorizontalScrollbar = true;
            this.lbCurOp.ItemHeight = 17;
            this.lbCurOp.Location = new System.Drawing.Point(12, 88);
            this.lbCurOp.Name = "lbCurOp";
            this.lbCurOp.Size = new System.Drawing.Size(451, 599);
            this.lbCurOp.TabIndex = 2;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabelLeft,
            this.statusLabelCenter,
            this.statusLabelRight});
            this.statusStrip.Location = new System.Drawing.Point(0, 701);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1455, 22);
            this.statusStrip.TabIndex = 3;
            this.statusStrip.Text = "statusStrip1";
            // 
            // statusLabelLeft
            // 
            this.statusLabelLeft.Name = "statusLabelLeft";
            this.statusLabelLeft.Size = new System.Drawing.Size(0, 17);
            // 
            // statusLabelCenter
            // 
            this.statusLabelCenter.Name = "statusLabelCenter";
            this.statusLabelCenter.Size = new System.Drawing.Size(0, 17);
            // 
            // statusLabelRight
            // 
            this.statusLabelRight.Name = "statusLabelRight";
            this.statusLabelRight.Size = new System.Drawing.Size(0, 17);
            this.statusLabelRight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bgWorker
            // 
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
            this.bgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 59);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(451, 23);
            this.progressBar.TabIndex = 4;
            // 
            // dtPicker
            // 
            this.dtPicker.Checked = false;
            this.dtPicker.Location = new System.Drawing.Point(618, 14);
            this.dtPicker.Name = "dtPicker";
            this.dtPicker.Size = new System.Drawing.Size(165, 29);
            this.dtPicker.TabIndex = 5;
            this.dtPicker.ValueChanged += new System.EventHandler(this.dtPicker_ValueChanged);
            // 
            // cbInstrument
            // 
            this.cbInstrument.FormattingEnabled = true;
            this.cbInstrument.Location = new System.Drawing.Point(478, 14);
            this.cbInstrument.Name = "cbInstrument";
            this.cbInstrument.Size = new System.Drawing.Size(121, 29);
            this.cbInstrument.TabIndex = 6;
            this.cbInstrument.SelectedIndexChanged += new System.EventHandler(this.cbInstrument_SelectedIndexChanged);
            // 
            // cbIndex
            // 
            this.cbIndex.FormattingEnabled = true;
            this.cbIndex.Location = new System.Drawing.Point(789, 14);
            this.cbIndex.Name = "cbIndex";
            this.cbIndex.Size = new System.Drawing.Size(121, 29);
            this.cbIndex.TabIndex = 7;
            // 
            // bGenIndex
            // 
            this.bGenIndex.Location = new System.Drawing.Point(928, 10);
            this.bGenIndex.Margin = new System.Windows.Forms.Padding(5);
            this.bGenIndex.Name = "bGenIndex";
            this.bGenIndex.Size = new System.Drawing.Size(78, 40);
            this.bGenIndex.TabIndex = 8;
            this.bGenIndex.Text = "生成";
            this.bGenIndex.UseVisualStyleBackColor = true;
            // 
            // chartControlTick
            // 
            xyDiagram1.AxisX.GridLines.Visible = true;
            xyDiagram1.AxisX.Label.Visible = false;
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            this.chartControlTick.Diagram = xyDiagram1;
            this.chartControlTick.Location = new System.Drawing.Point(480, 59);
            this.chartControlTick.Name = "chartControlTick";
            series1.ArgumentDataMember = "UpdateTime";
            series1.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            series1.DataSource = this.dtTick;
            series1.Name = "价位";
            series1.ShowInLegend = false;
            series1.ValueDataMembersSerializable = "LastPrice";
            series1.View = lineSeriesView1;
            this.chartControlTick.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            this.chartControlTick.SeriesTemplate.View = lineSeriesView2;
            this.chartControlTick.Size = new System.Drawing.Size(926, 354);
            this.chartControlTick.TabIndex = 9;
            // 
            // dsDisplay
            // 
            this.dsDisplay.DataSetName = "dsDisplay";
            this.dsDisplay.Tables.AddRange(new System.Data.DataTable[] {
            this.dtTick});
            // 
            // chartControlTickVol
            // 
            xyDiagram2.AxisX.Label.TextColor = System.Drawing.SystemColors.WindowText;
            xyDiagram2.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram2.AxisY.VisibleInPanesSerializable = "-1";
            secondaryAxisY1.AxisID = 0;
            secondaryAxisY1.Name = "Secondary AxisY 1";
            secondaryAxisY1.VisibleInPanesSerializable = "-1";
            xyDiagram2.SecondaryAxesY.AddRange(new DevExpress.XtraCharts.SecondaryAxisY[] {
            secondaryAxisY1});
            this.chartControlTickVol.Diagram = xyDiagram2;
            this.chartControlTickVol.Location = new System.Drawing.Point(480, 412);
            this.chartControlTickVol.Name = "chartControlTickVol";
            series2.ArgumentDataMember = "UpdateTime";
            series2.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            series2.DataSource = this.dtTick;
            series2.Name = "成交量";
            series2.ShowInLegend = false;
            series2.ValueDataMembersSerializable = "Volume";
            series3.ArgumentDataMember = "UpdateTime";
            series3.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            series3.DataSource = this.dtTick;
            series3.Name = "持仓量";
            series3.ShowInLegend = false;
            series3.ValueDataMembersSerializable = "OpenInterest";
            lineSeriesView3.AxisYName = "Secondary AxisY 1";
            series3.View = lineSeriesView3;
            this.chartControlTickVol.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series2,
        series3};
            this.chartControlTickVol.SeriesTemplate.View = lineSeriesView4;
            this.chartControlTickVol.Size = new System.Drawing.Size(926, 267);
            this.chartControlTickVol.TabIndex = 10;
            // 
            // FormMDProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1455, 723);
            this.Controls.Add(this.chartControlTickVol);
            this.Controls.Add(this.chartControlTick);
            this.Controls.Add(this.bGenIndex);
            this.Controls.Add(this.cbIndex);
            this.Controls.Add(this.cbInstrument);
            this.Controls.Add(this.dtPicker);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.lbCurOp);
            this.Controls.Add(this.bGetInstrument);
            this.Controls.Add(this.bReadData);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FormMDProcess";
            this.Text = "数据导入";
            this.Load += new System.EventHandler(this.FormMDProcess_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtTick)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControlTick)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsDisplay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(secondaryAxisY1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControlTickVol)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bReadData;
        private System.Windows.Forms.Button bGetInstrument;
        private System.Windows.Forms.ListBox lbCurOp;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelLeft;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelCenter;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelRight;
        private System.Windows.Forms.DateTimePicker dtPicker;
        private System.Windows.Forms.ComboBox cbInstrument;
        private System.Windows.Forms.ComboBox cbIndex;
        private System.Windows.Forms.Button bGenIndex;
        private DevExpress.XtraCharts.ChartControl chartControlTick;
        private System.Data.DataTable dtTick;
        private System.Data.DataColumn UpdateTime;
        private System.Data.DataColumn LastPrice;
        private System.Data.DataColumn AveragePrice;
        private System.Data.DataColumn Volume;
        private System.Data.DataColumn OpenInterest;
        private System.Data.DataSet dsDisplay;
        private DevExpress.XtraCharts.ChartControl chartControlTickVol;
        private System.Data.DataColumn PreClosePrice;
    }
}

