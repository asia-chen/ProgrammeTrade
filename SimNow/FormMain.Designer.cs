namespace SimNow
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.bLogin = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lTradeAddr = new System.Windows.Forms.Label();
            this.lBrokerID = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rbCloseToday = new System.Windows.Forms.RadioButton();
            this.rbOpen = new System.Windows.Forms.RadioButton();
            this.rbClose = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbBuy = new System.Windows.Forms.RadioButton();
            this.rbSell = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.tbVolume = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.bOrder = new System.Windows.Forms.Button();
            this.tbInstrumentID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbPrice = new System.Windows.Forms.TextBox();
            this.gcOrder = new DevExpress.XtraGrid.GridControl();
            this.gvOrder = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcTrade = new DevExpress.XtraGrid.GridControl();
            this.gvTrade = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabelLeft = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelCenter = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelRight = new System.Windows.Forms.ToolStripStatusLabel();
            this.rstOrder = new DevExpress.Data.RealTimeSource();
            this.rtsTrade = new DevExpress.Data.RealTimeSource();
            this.bMDConnect = new System.Windows.Forms.Button();
            this.bMDLogin = new System.Windows.Forms.Button();
            this.bMDSubscribe = new System.Windows.Forms.Button();
            this.gcMarketData = new DevExpress.XtraGrid.GridControl();
            this.gvMarketData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.bRefreshPosition = new System.Windows.Forms.Button();
            this.bFundRefresh = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.tbPositionProfit = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbExchangeMargin = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbAvailable = new System.Windows.Forms.TextBox();
            this.gcPosition = new DevExpress.XtraGrid.GridControl();
            this.gvPosition = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn25 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn27 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn23 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn24 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn26 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn28 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtsPosition = new DevExpress.Data.RealTimeSource();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcOrder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvOrder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTrade)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTrade)).BeginInit();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcMarketData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMarketData)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcPosition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPosition)).BeginInit();
            this.SuspendLayout();
            // 
            // tbUsername
            // 
            this.tbUsername.Location = new System.Drawing.Point(106, 65);
            this.tbUsername.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(196, 34);
            this.tbUsername.TabIndex = 0;
            this.tbUsername.Text = "054108";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 65);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 27);
            this.label1.TabIndex = 1;
            this.label1.Text = "用户名";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 107);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 27);
            this.label2.TabIndex = 3;
            this.label2.Text = "密  码";
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(106, 107);
            this.tbPassword.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(196, 34);
            this.tbPassword.TabIndex = 2;
            this.tbPassword.Text = "961123";
            // 
            // bLogin
            // 
            this.bLogin.Location = new System.Drawing.Point(322, 65);
            this.bLogin.Name = "bLogin";
            this.bLogin.Size = new System.Drawing.Size(135, 34);
            this.bLogin.TabIndex = 4;
            this.bLogin.Text = "登录";
            this.bLogin.UseVisualStyleBackColor = true;
            this.bLogin.Click += new System.EventHandler(this.bLogin_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lTradeAddr);
            this.groupBox1.Controls.Add(this.lBrokerID);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.bLogin);
            this.groupBox1.Controls.Add(this.tbUsername);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbPassword);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox1.Location = new System.Drawing.Point(24, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(471, 152);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "登录";
            // 
            // lTradeAddr
            // 
            this.lTradeAddr.AutoSize = true;
            this.lTradeAddr.Location = new System.Drawing.Point(127, 31);
            this.lTradeAddr.Name = "lTradeAddr";
            this.lTradeAddr.Size = new System.Drawing.Size(289, 27);
            this.lTradeAddr.TabIndex = 6;
            this.lTradeAddr.Text = "tcp://180.168.146.187:10000";
            // 
            // lBrokerID
            // 
            this.lBrokerID.AutoSize = true;
            this.lBrokerID.Location = new System.Drawing.Point(30, 31);
            this.lBrokerID.Name = "lBrokerID";
            this.lBrokerID.Size = new System.Drawing.Size(60, 27);
            this.lBrokerID.TabIndex = 5;
            this.lBrokerID.Text = "9999";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.Controls.Add(this.panel1);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.tbVolume);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.bOrder);
            this.groupBox2.Controls.Add(this.tbInstrumentID);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.tbPrice);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox2.Location = new System.Drawing.Point(24, 157);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(471, 238);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "委托";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rbCloseToday);
            this.panel2.Controls.Add(this.rbOpen);
            this.panel2.Controls.Add(this.rbClose);
            this.panel2.Location = new System.Drawing.Point(102, 111);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(355, 39);
            this.panel2.TabIndex = 9;
            // 
            // rbCloseToday
            // 
            this.rbCloseToday.AutoSize = true;
            this.rbCloseToday.Location = new System.Drawing.Point(127, 3);
            this.rbCloseToday.Name = "rbCloseToday";
            this.rbCloseToday.Size = new System.Drawing.Size(70, 31);
            this.rbCloseToday.TabIndex = 9;
            this.rbCloseToday.TabStop = true;
            this.rbCloseToday.Text = "平今";
            this.rbCloseToday.UseVisualStyleBackColor = true;
            // 
            // rbOpen
            // 
            this.rbOpen.AutoSize = true;
            this.rbOpen.Location = new System.Drawing.Point(15, 3);
            this.rbOpen.Name = "rbOpen";
            this.rbOpen.Size = new System.Drawing.Size(70, 31);
            this.rbOpen.TabIndex = 7;
            this.rbOpen.TabStop = true;
            this.rbOpen.Text = "开仓";
            this.rbOpen.UseVisualStyleBackColor = true;
            // 
            // rbClose
            // 
            this.rbClose.AutoSize = true;
            this.rbClose.Location = new System.Drawing.Point(239, 3);
            this.rbClose.Name = "rbClose";
            this.rbClose.Size = new System.Drawing.Size(70, 31);
            this.rbClose.TabIndex = 8;
            this.rbClose.TabStop = true;
            this.rbClose.Text = "平仓";
            this.rbClose.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbBuy);
            this.panel1.Controls.Add(this.rbSell);
            this.panel1.Location = new System.Drawing.Point(102, 67);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 39);
            this.panel1.TabIndex = 8;
            // 
            // rbBuy
            // 
            this.rbBuy.AutoSize = true;
            this.rbBuy.Location = new System.Drawing.Point(15, 3);
            this.rbBuy.Name = "rbBuy";
            this.rbBuy.Size = new System.Drawing.Size(70, 31);
            this.rbBuy.TabIndex = 7;
            this.rbBuy.TabStop = true;
            this.rbBuy.Text = "买入";
            this.rbBuy.UseVisualStyleBackColor = true;
            // 
            // rbSell
            // 
            this.rbSell.AutoSize = true;
            this.rbSell.Location = new System.Drawing.Point(127, 3);
            this.rbSell.Name = "rbSell";
            this.rbSell.Size = new System.Drawing.Size(70, 31);
            this.rbSell.TabIndex = 8;
            this.rbSell.TabStop = true;
            this.rbSell.Text = "卖出";
            this.rbSell.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 195);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 27);
            this.label5.TabIndex = 6;
            this.label5.Text = "数量";
            // 
            // tbVolume
            // 
            this.tbVolume.Location = new System.Drawing.Point(103, 195);
            this.tbVolume.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.tbVolume.Name = "tbVolume";
            this.tbVolume.Size = new System.Drawing.Size(196, 34);
            this.tbVolume.TabIndex = 5;
            this.tbVolume.Text = "1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 27);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 27);
            this.label3.TabIndex = 1;
            this.label3.Text = "合约";
            // 
            // bOrder
            // 
            this.bOrder.Location = new System.Drawing.Point(322, 27);
            this.bOrder.Name = "bOrder";
            this.bOrder.Size = new System.Drawing.Size(135, 34);
            this.bOrder.TabIndex = 4;
            this.bOrder.Text = "报单";
            this.bOrder.UseVisualStyleBackColor = true;
            this.bOrder.Click += new System.EventHandler(this.bOrder_Click);
            // 
            // tbInstrumentID
            // 
            this.tbInstrumentID.Location = new System.Drawing.Point(106, 27);
            this.tbInstrumentID.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.tbInstrumentID.Name = "tbInstrumentID";
            this.tbInstrumentID.Size = new System.Drawing.Size(196, 34);
            this.tbInstrumentID.TabIndex = 0;
            this.tbInstrumentID.Text = "hc1701";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 154);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 27);
            this.label4.TabIndex = 3;
            this.label4.Text = "价格";
            // 
            // tbPrice
            // 
            this.tbPrice.Location = new System.Drawing.Point(103, 154);
            this.tbPrice.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.tbPrice.Name = "tbPrice";
            this.tbPrice.Size = new System.Drawing.Size(196, 34);
            this.tbPrice.TabIndex = 2;
            this.tbPrice.Text = "3200";
            // 
            // gcOrder
            // 
            this.gcOrder.Location = new System.Drawing.Point(516, 26);
            this.gcOrder.MainView = this.gvOrder;
            this.gcOrder.Name = "gcOrder";
            this.gcOrder.Size = new System.Drawing.Size(642, 235);
            this.gcOrder.TabIndex = 7;
            this.gcOrder.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvOrder});
            // 
            // gvOrder
            // 
            this.gvOrder.ActiveFilterEnabled = false;
            this.gvOrder.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn8,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn16,
            this.gridColumn18,
            this.gridColumn19});
            this.gvOrder.GridControl = this.gcOrder;
            this.gvOrder.Name = "gvOrder";
            this.gvOrder.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvOrder.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvOrder.OptionsBehavior.AllowFixedGroups = DevExpress.Utils.DefaultBoolean.False;
            this.gvOrder.OptionsBehavior.Editable = false;
            this.gvOrder.OptionsBehavior.ReadOnly = true;
            this.gvOrder.OptionsFilter.AllowColumnMRUFilterList = false;
            this.gvOrder.OptionsFilter.AllowFilterEditor = false;
            this.gvOrder.OptionsFilter.AllowFilterIncrementalSearch = false;
            this.gvOrder.OptionsFilter.AllowMRUFilterList = false;
            this.gvOrder.OptionsFilter.AllowMultiSelectInCheckedFilterPopup = false;
            this.gvOrder.OptionsView.AllowHtmlDrawGroups = false;
            this.gvOrder.OptionsView.EnableAppearanceEvenRow = true;
            this.gvOrder.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gvOrder.OptionsView.ShowGroupPanel = false;
            this.gvOrder.OptionsView.ShowIndicator = false;
            this.gvOrder.DoubleClick += new System.EventHandler(this.gvOrder_DoubleClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "报单号";
            this.gridColumn1.FieldName = "OrderSysID";
            this.gridColumn1.MinWidth = 30;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 248;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "合约";
            this.gridColumn2.FieldName = "InstrumentID";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            this.gridColumn2.Width = 159;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "报单时间";
            this.gridColumn8.FieldName = "InsertTime";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 1;
            this.gridColumn8.Width = 212;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "买卖";
            this.gridColumn3.FieldName = "Direction";
            this.gridColumn3.MinWidth = 10;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 3;
            this.gridColumn3.Width = 84;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "开平";
            this.gridColumn4.FieldName = "CombOffsetFlag";
            this.gridColumn4.MinWidth = 10;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 4;
            this.gridColumn4.Width = 79;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "价格";
            this.gridColumn5.FieldName = "LimitPrice";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 5;
            this.gridColumn5.Width = 211;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "总数量";
            this.gridColumn6.FieldName = "VolumeTotalOriginal";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 7;
            this.gridColumn6.Width = 184;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "状态";
            this.gridColumn7.FieldName = "OrderStatus";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 6;
            this.gridColumn7.Width = 147;
            // 
            // gridColumn16
            // 
            this.gridColumn16.Caption = "已成交";
            this.gridColumn16.FieldName = "VolumeTraded";
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.Visible = true;
            this.gridColumn16.VisibleIndex = 8;
            this.gridColumn16.Width = 211;
            // 
            // gridColumn18
            // 
            this.gridColumn18.Caption = "剩余量";
            this.gridColumn18.FieldName = "VolumeTotal";
            this.gridColumn18.Name = "gridColumn18";
            this.gridColumn18.Visible = true;
            this.gridColumn18.VisibleIndex = 9;
            this.gridColumn18.Width = 113;
            // 
            // gridColumn19
            // 
            this.gridColumn19.Caption = "gridColumn19";
            this.gridColumn19.FieldName = "ExchangeID";
            this.gridColumn19.Name = "gridColumn19";
            // 
            // gcTrade
            // 
            this.gcTrade.Location = new System.Drawing.Point(516, 270);
            this.gcTrade.MainView = this.gvTrade;
            this.gcTrade.Name = "gcTrade";
            this.gcTrade.Size = new System.Drawing.Size(642, 235);
            this.gcTrade.TabIndex = 8;
            this.gcTrade.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvTrade});
            // 
            // gvTrade
            // 
            this.gvTrade.ActiveFilterEnabled = false;
            this.gvTrade.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn17,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12,
            this.gridColumn13,
            this.gridColumn14,
            this.gridColumn15});
            this.gvTrade.GridControl = this.gcTrade;
            this.gvTrade.Name = "gvTrade";
            this.gvTrade.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvTrade.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvTrade.OptionsBehavior.AllowFixedGroups = DevExpress.Utils.DefaultBoolean.False;
            this.gvTrade.OptionsBehavior.Editable = false;
            this.gvTrade.OptionsBehavior.ReadOnly = true;
            this.gvTrade.OptionsFilter.AllowColumnMRUFilterList = false;
            this.gvTrade.OptionsFilter.AllowFilterEditor = false;
            this.gvTrade.OptionsFilter.AllowFilterIncrementalSearch = false;
            this.gvTrade.OptionsFilter.AllowMRUFilterList = false;
            this.gvTrade.OptionsFilter.AllowMultiSelectInCheckedFilterPopup = false;
            this.gvTrade.OptionsView.AllowHtmlDrawGroups = false;
            this.gvTrade.OptionsView.EnableAppearanceEvenRow = true;
            this.gvTrade.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gvTrade.OptionsView.ShowGroupPanel = false;
            this.gvTrade.OptionsView.ShowIndicator = false;
            // 
            // gridColumn17
            // 
            this.gridColumn17.Caption = "成交编号";
            this.gridColumn17.FieldName = "TradeID";
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.Visible = true;
            this.gridColumn17.VisibleIndex = 0;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "报单号";
            this.gridColumn9.FieldName = "OrderSysID";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 1;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "合约";
            this.gridColumn10.FieldName = "InstrumentID";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 3;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "成交时间";
            this.gridColumn11.FieldName = "TradeDate";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 2;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "买卖";
            this.gridColumn12.FieldName = "Direction";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 4;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "开平";
            this.gridColumn13.FieldName = "OffsetFlag";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 5;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "价格";
            this.gridColumn14.FieldName = "Price";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 6;
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "数量";
            this.gridColumn15.FieldName = "Volume";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 7;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabelLeft,
            this.statusLabelCenter,
            this.statusLabelRight});
            this.statusStrip.Location = new System.Drawing.Point(0, 734);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1244, 22);
            this.statusStrip.TabIndex = 9;
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
            // 
            // rstOrder
            // 
            this.rstOrder.DisplayableProperties = null;
            this.rstOrder.UseWeakEventHandler = true;
            // 
            // rtsTrade
            // 
            this.rtsTrade.DisplayableProperties = null;
            this.rtsTrade.UseWeakEventHandler = true;
            // 
            // bMDConnect
            // 
            this.bMDConnect.Location = new System.Drawing.Point(24, 597);
            this.bMDConnect.Name = "bMDConnect";
            this.bMDConnect.Size = new System.Drawing.Size(135, 34);
            this.bMDConnect.TabIndex = 10;
            this.bMDConnect.Text = "行情连接";
            this.bMDConnect.UseVisualStyleBackColor = true;
            this.bMDConnect.Click += new System.EventHandler(this.bMDConnect_Click);
            // 
            // bMDLogin
            // 
            this.bMDLogin.Location = new System.Drawing.Point(165, 597);
            this.bMDLogin.Name = "bMDLogin";
            this.bMDLogin.Size = new System.Drawing.Size(135, 34);
            this.bMDLogin.TabIndex = 11;
            this.bMDLogin.Text = "行情登录";
            this.bMDLogin.UseVisualStyleBackColor = true;
            this.bMDLogin.Click += new System.EventHandler(this.bMDLogin_Click);
            // 
            // bMDSubscribe
            // 
            this.bMDSubscribe.Location = new System.Drawing.Point(306, 597);
            this.bMDSubscribe.Name = "bMDSubscribe";
            this.bMDSubscribe.Size = new System.Drawing.Size(135, 34);
            this.bMDSubscribe.TabIndex = 12;
            this.bMDSubscribe.Text = "行情订阅";
            this.bMDSubscribe.UseVisualStyleBackColor = true;
            this.bMDSubscribe.Click += new System.EventHandler(this.bMDSubscribe_Click);
            // 
            // gcMarketData
            // 
            this.gcMarketData.Location = new System.Drawing.Point(24, 643);
            this.gcMarketData.MainView = this.gvMarketData;
            this.gcMarketData.Name = "gcMarketData";
            this.gcMarketData.Size = new System.Drawing.Size(416, 82);
            this.gcMarketData.TabIndex = 13;
            this.gcMarketData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMarketData});
            // 
            // gvMarketData
            // 
            this.gvMarketData.ActiveFilterEnabled = false;
            this.gvMarketData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn20,
            this.gridColumn21,
            this.gridColumn22});
            this.gvMarketData.GridControl = this.gcMarketData;
            this.gvMarketData.Name = "gvMarketData";
            this.gvMarketData.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvMarketData.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvMarketData.OptionsBehavior.AllowFixedGroups = DevExpress.Utils.DefaultBoolean.False;
            this.gvMarketData.OptionsBehavior.Editable = false;
            this.gvMarketData.OptionsBehavior.ReadOnly = true;
            this.gvMarketData.OptionsFilter.AllowColumnMRUFilterList = false;
            this.gvMarketData.OptionsFilter.AllowFilterEditor = false;
            this.gvMarketData.OptionsFilter.AllowFilterIncrementalSearch = false;
            this.gvMarketData.OptionsFilter.AllowMRUFilterList = false;
            this.gvMarketData.OptionsFilter.AllowMultiSelectInCheckedFilterPopup = false;
            this.gvMarketData.OptionsView.AllowHtmlDrawGroups = false;
            this.gvMarketData.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMarketData.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gvMarketData.OptionsView.ShowGroupPanel = false;
            this.gvMarketData.OptionsView.ShowIndicator = false;
            // 
            // gridColumn20
            // 
            this.gridColumn20.Caption = " ";
            this.gridColumn20.FieldName = "Name";
            this.gridColumn20.Name = "gridColumn20";
            this.gridColumn20.Visible = true;
            this.gridColumn20.VisibleIndex = 0;
            // 
            // gridColumn21
            // 
            this.gridColumn21.Caption = "价格";
            this.gridColumn21.FieldName = "Price";
            this.gridColumn21.Name = "gridColumn21";
            this.gridColumn21.Visible = true;
            this.gridColumn21.VisibleIndex = 1;
            // 
            // gridColumn22
            // 
            this.gridColumn22.Caption = "数量";
            this.gridColumn22.FieldName = "Volume";
            this.gridColumn22.Name = "gridColumn22";
            this.gridColumn22.Visible = true;
            this.gridColumn22.VisibleIndex = 2;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.bRefreshPosition);
            this.groupBox3.Controls.Add(this.bFundRefresh);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.tbPositionProfit);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.tbExchangeMargin);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.tbAvailable);
            this.groupBox3.Location = new System.Drawing.Point(24, 401);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(471, 171);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "资金";
            // 
            // bRefreshPosition
            // 
            this.bRefreshPosition.Location = new System.Drawing.Point(322, 74);
            this.bRefreshPosition.Name = "bRefreshPosition";
            this.bRefreshPosition.Size = new System.Drawing.Size(135, 34);
            this.bRefreshPosition.TabIndex = 16;
            this.bRefreshPosition.Text = "刷新持仓";
            this.bRefreshPosition.UseVisualStyleBackColor = true;
            this.bRefreshPosition.Click += new System.EventHandler(this.bRefreshPosition_Click);
            // 
            // bFundRefresh
            // 
            this.bFundRefresh.Location = new System.Drawing.Point(322, 33);
            this.bFundRefresh.Name = "bFundRefresh";
            this.bFundRefresh.Size = new System.Drawing.Size(135, 34);
            this.bFundRefresh.TabIndex = 7;
            this.bFundRefresh.Text = "刷新资金";
            this.bFundRefresh.UseVisualStyleBackColor = true;
            this.bFundRefresh.Click += new System.EventHandler(this.bFundRefresh_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(21, 118);
            this.label8.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 27);
            this.label8.TabIndex = 15;
            this.label8.Text = "浮盈";
            // 
            // tbPositionProfit
            // 
            this.tbPositionProfit.Location = new System.Drawing.Point(102, 118);
            this.tbPositionProfit.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.tbPositionProfit.Name = "tbPositionProfit";
            this.tbPositionProfit.Size = new System.Drawing.Size(196, 34);
            this.tbPositionProfit.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 74);
            this.label7.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 27);
            this.label7.TabIndex = 13;
            this.label7.Text = "占用";
            // 
            // tbExchangeMargin
            // 
            this.tbExchangeMargin.Location = new System.Drawing.Point(102, 74);
            this.tbExchangeMargin.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.tbExchangeMargin.Name = "tbExchangeMargin";
            this.tbExchangeMargin.Size = new System.Drawing.Size(196, 34);
            this.tbExchangeMargin.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 31);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 27);
            this.label6.TabIndex = 11;
            this.label6.Text = "可用";
            // 
            // tbAvailable
            // 
            this.tbAvailable.Location = new System.Drawing.Point(102, 31);
            this.tbAvailable.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.tbAvailable.Name = "tbAvailable";
            this.tbAvailable.Size = new System.Drawing.Size(196, 34);
            this.tbAvailable.TabIndex = 10;
            // 
            // gcPosition
            // 
            this.gcPosition.Location = new System.Drawing.Point(516, 519);
            this.gcPosition.MainView = this.gvPosition;
            this.gcPosition.Name = "gcPosition";
            this.gcPosition.Size = new System.Drawing.Size(642, 205);
            this.gcPosition.TabIndex = 15;
            this.gcPosition.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvPosition});
            // 
            // gvPosition
            // 
            this.gvPosition.ActiveFilterEnabled = false;
            this.gvPosition.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn25,
            this.gridColumn27,
            this.gridColumn23,
            this.gridColumn24,
            this.gridColumn26,
            this.gridColumn28});
            this.gvPosition.GridControl = this.gcPosition;
            this.gvPosition.Name = "gvPosition";
            this.gvPosition.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvPosition.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvPosition.OptionsBehavior.AllowFixedGroups = DevExpress.Utils.DefaultBoolean.False;
            this.gvPosition.OptionsBehavior.Editable = false;
            this.gvPosition.OptionsBehavior.ReadOnly = true;
            this.gvPosition.OptionsFilter.AllowColumnMRUFilterList = false;
            this.gvPosition.OptionsFilter.AllowFilterEditor = false;
            this.gvPosition.OptionsFilter.AllowFilterIncrementalSearch = false;
            this.gvPosition.OptionsFilter.AllowMRUFilterList = false;
            this.gvPosition.OptionsFilter.AllowMultiSelectInCheckedFilterPopup = false;
            this.gvPosition.OptionsView.AllowHtmlDrawGroups = false;
            this.gvPosition.OptionsView.EnableAppearanceEvenRow = true;
            this.gvPosition.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gvPosition.OptionsView.ShowGroupPanel = false;
            this.gvPosition.OptionsView.ShowIndicator = false;
            // 
            // gridColumn25
            // 
            this.gridColumn25.Caption = "合约";
            this.gridColumn25.FieldName = "InstrumentID";
            this.gridColumn25.Name = "gridColumn25";
            this.gridColumn25.Visible = true;
            this.gridColumn25.VisibleIndex = 0;
            // 
            // gridColumn27
            // 
            this.gridColumn27.Caption = "买卖";
            this.gridColumn27.FieldName = "PosiDirection";
            this.gridColumn27.Name = "gridColumn27";
            this.gridColumn27.Visible = true;
            this.gridColumn27.VisibleIndex = 1;
            // 
            // gridColumn23
            // 
            this.gridColumn23.Caption = "数量";
            this.gridColumn23.FieldName = "Position";
            this.gridColumn23.Name = "gridColumn23";
            this.gridColumn23.Visible = true;
            this.gridColumn23.VisibleIndex = 2;
            // 
            // gridColumn24
            // 
            this.gridColumn24.Caption = "成本";
            this.gridColumn24.FieldName = "PositionCost";
            this.gridColumn24.Name = "gridColumn24";
            this.gridColumn24.Visible = true;
            this.gridColumn24.VisibleIndex = 3;
            // 
            // gridColumn26
            // 
            this.gridColumn26.Caption = "保证金";
            this.gridColumn26.FieldName = "UseMargin";
            this.gridColumn26.Name = "gridColumn26";
            this.gridColumn26.Visible = true;
            this.gridColumn26.VisibleIndex = 4;
            // 
            // gridColumn28
            // 
            this.gridColumn28.Caption = "持仓盈亏";
            this.gridColumn28.Name = "gridColumn28";
            this.gridColumn28.Visible = true;
            this.gridColumn28.VisibleIndex = 5;
            // 
            // rtsPosition
            // 
            this.rtsPosition.DisplayableProperties = null;
            this.rtsPosition.UseWeakEventHandler = true;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1244, 756);
            this.Controls.Add(this.gcPosition);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.gcMarketData);
            this.Controls.Add(this.bMDSubscribe);
            this.Controls.Add(this.bMDLogin);
            this.Controls.Add(this.bMDConnect);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.gcTrade);
            this.Controls.Add(this.gcOrder);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "模拟交易";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcOrder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvOrder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTrade)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTrade)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcMarketData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMarketData)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcPosition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPosition)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Button bLogin;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbVolume;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button bOrder;
        private System.Windows.Forms.TextBox tbInstrumentID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbPrice;
        private System.Windows.Forms.RadioButton rbSell;
        private System.Windows.Forms.RadioButton rbBuy;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rbOpen;
        private System.Windows.Forms.RadioButton rbClose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lBrokerID;
        private System.Windows.Forms.Label lTradeAddr;
        private DevExpress.XtraGrid.GridControl gcOrder;
        private DevExpress.XtraGrid.Views.Grid.GridView gvOrder;
        private System.Windows.Forms.RadioButton rbCloseToday;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.GridControl gcTrade;
        private DevExpress.XtraGrid.Views.Grid.GridView gvTrade;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelLeft;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelCenter;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelRight;
        private DevExpress.Data.RealTimeSource rstOrder;
        private DevExpress.Data.RealTimeSource rtsTrade;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
        private System.Windows.Forms.Button bMDConnect;
        private System.Windows.Forms.Button bMDLogin;
        private System.Windows.Forms.Button bMDSubscribe;
        private DevExpress.XtraGrid.GridControl gcMarketData;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMarketData;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn20;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn21;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn22;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbAvailable;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbPositionProfit;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbExchangeMargin;
        private System.Windows.Forms.Button bFundRefresh;
        private DevExpress.XtraGrid.GridControl gcPosition;
        private DevExpress.XtraGrid.Views.Grid.GridView gvPosition;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn25;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn27;
        private DevExpress.Data.RealTimeSource rtsPosition;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn23;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn24;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn26;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn28;
        private System.Windows.Forms.Button bRefreshPosition;
    }
}

