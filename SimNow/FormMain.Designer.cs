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
            this.rtsOrder = new DevExpress.Data.RealTimeSource();
            this.rtsTrade = new DevExpress.Data.RealTimeSource();
            this.gcOrder = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.rbCloseToday = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcOrder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // tbUsername
            // 
            this.tbUsername.Location = new System.Drawing.Point(106, 87);
            this.tbUsername.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(196, 34);
            this.tbUsername.TabIndex = 0;
            this.tbUsername.Text = "054108";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 87);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 27);
            this.label1.TabIndex = 1;
            this.label1.Text = "用户名";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 135);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 27);
            this.label2.TabIndex = 3;
            this.label2.Text = "密  码";
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(106, 135);
            this.tbPassword.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(196, 34);
            this.tbPassword.TabIndex = 2;
            this.tbPassword.Text = "961123";
            // 
            // bLogin
            // 
            this.bLogin.Location = new System.Drawing.Point(322, 87);
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
            this.groupBox1.Location = new System.Drawing.Point(24, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(471, 171);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // lTradeAddr
            // 
            this.lTradeAddr.AutoSize = true;
            this.lTradeAddr.Location = new System.Drawing.Point(127, 22);
            this.lTradeAddr.Name = "lTradeAddr";
            this.lTradeAddr.Size = new System.Drawing.Size(289, 27);
            this.lTradeAddr.TabIndex = 6;
            this.lTradeAddr.Text = "tcp://180.168.146.187:10000";
            // 
            // lBrokerID
            // 
            this.lBrokerID.AutoSize = true;
            this.lBrokerID.Location = new System.Drawing.Point(30, 22);
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
            this.groupBox2.Location = new System.Drawing.Point(24, 178);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(471, 329);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rbCloseToday);
            this.panel2.Controls.Add(this.rbOpen);
            this.panel2.Controls.Add(this.rbClose);
            this.panel2.Location = new System.Drawing.Point(102, 149);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(355, 39);
            this.panel2.TabIndex = 9;
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
            this.panel1.Location = new System.Drawing.Point(102, 104);
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
            this.label5.Location = new System.Drawing.Point(25, 247);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 27);
            this.label5.TabIndex = 6;
            this.label5.Text = "数量";
            // 
            // tbVolume
            // 
            this.tbVolume.Location = new System.Drawing.Point(106, 247);
            this.tbVolume.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.tbVolume.Name = "tbVolume";
            this.tbVolume.Size = new System.Drawing.Size(196, 34);
            this.tbVolume.TabIndex = 5;
            this.tbVolume.Text = "1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 50);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 27);
            this.label3.TabIndex = 1;
            this.label3.Text = "合约";
            // 
            // bOrder
            // 
            this.bOrder.Location = new System.Drawing.Point(322, 50);
            this.bOrder.Name = "bOrder";
            this.bOrder.Size = new System.Drawing.Size(135, 34);
            this.bOrder.TabIndex = 4;
            this.bOrder.Text = "报单";
            this.bOrder.UseVisualStyleBackColor = true;
            this.bOrder.Click += new System.EventHandler(this.bOrder_Click);
            // 
            // tbInstrumentID
            // 
            this.tbInstrumentID.Location = new System.Drawing.Point(106, 50);
            this.tbInstrumentID.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.tbInstrumentID.Name = "tbInstrumentID";
            this.tbInstrumentID.Size = new System.Drawing.Size(196, 34);
            this.tbInstrumentID.TabIndex = 0;
            this.tbInstrumentID.Text = "hc1701";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 195);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 27);
            this.label4.TabIndex = 3;
            this.label4.Text = "价格";
            // 
            // tbPrice
            // 
            this.tbPrice.Location = new System.Drawing.Point(106, 195);
            this.tbPrice.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.tbPrice.Name = "tbPrice";
            this.tbPrice.Size = new System.Drawing.Size(196, 34);
            this.tbPrice.TabIndex = 2;
            this.tbPrice.Text = "3200";
            // 
            // rtsOrder
            // 
            this.rtsOrder.DisplayableProperties = null;
            this.rtsOrder.UseWeakEventHandler = true;
            // 
            // rtsTrade
            // 
            this.rtsTrade.DisplayableProperties = null;
            this.rtsTrade.UseWeakEventHandler = true;
            // 
            // gcOrder
            // 
            this.gcOrder.Location = new System.Drawing.Point(516, 34);
            this.gcOrder.MainView = this.gridView1;
            this.gcOrder.Name = "gcOrder";
            this.gcOrder.Size = new System.Drawing.Size(480, 200);
            this.gcOrder.TabIndex = 7;
            this.gcOrder.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gcOrder;
            this.gridView1.Name = "gridView1";
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
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 534);
            this.Controls.Add(this.gcOrder);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.Name = "FormMain";
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
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

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
        private DevExpress.Data.RealTimeSource rtsOrder;
        private DevExpress.Data.RealTimeSource rtsTrade;
        private DevExpress.XtraGrid.GridControl gcOrder;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.RadioButton rbCloseToday;
    }
}

