using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using PubTools;
using PubTools.data;
using System.Threading;

namespace SimNow
{
    public partial class FormMain : Form
    {
        // BrokerID统一为：9999
        // 标准CTP：
        //    第一组：Trade Front：180.168.146.187:10000，Market Front：180.168.146.187:10010；【电信】
        //    第二组：Trade Front：180.168.146.187:10001，Market Front：180.168.146.187:10011；【电信】
        //    第三组：Trade Front：218.202.237.33 :10002，Market Front：218.202.237.33 :10012；【移动】
        //CTPMini1：
        //    第一组：Trade Front：180.168.146.187:10003，Market Front：180.168.146.187:10013；【电信】

        TradeUser myuser = null;

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            this.bLogin.Enabled = true;
            this.bOrder.Enabled = false;

            GlobalVar.currForm = this;
            FormTool.setStatusMessage = this.setStatusImpl;
            FormTool.setErrMessage = this.setStatusImpl;
        }

        private void bLogin_Click(object sender, EventArgs e)
        {
            myuser = new TradeUser("9999", "054108", "961123", "tcp://180.168.146.187:10000");
            if (PubTools.CommonTool.TimeoutWait(ref myuser.currStatus, 1) != 0)
            {
                Console.WriteLine("Err: Connect Time Out!!");
                return;
            }
            myuser.ReqLogin();

            if (PubTools.CommonTool.TimeoutWait(ref myuser.currStatus, 2) != 0)
            {
                Console.WriteLine("Err: Login Failed! " + myuser.connectMsg);
                return;
            }

            myuser.ReqSettlementInfoConfirm();


            this.bLogin.Enabled = false;
            this.bOrder.Enabled = true;

            this.rbBuy.Checked = true;
            this.rbOpen.Checked = true;

            rstOrder.DataSource = myuser.order;
            gcOrder.DataSource = rstOrder;

            rtsTrade.DataSource = myuser.trade;
            gcTrade.DataSource = rtsTrade;

        }

        private void bOrder_Click(object sender, EventArgs e)
        {
            if (myuser == null)
                return;

            if (this.tbInstrumentID.Text.Equals("") || this.tbPrice.Text.Equals("") || this.tbVolume.Text.Equals(""))
            {
                return;
            }
            
            double price = 0.0;
            long volume = 0;
            try
            {
                price = double.Parse(this.tbPrice.Text);
                volume = long.Parse(this.tbVolume.Text);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            String BuyOrSell = "";
            String OpenOrClose = "";
            if (this.rbBuy.Checked)
                BuyOrSell = Const.TradeBuy;
            else
                BuyOrSell = Const.TradeSell;
            if (this.rbOpen.Checked)
                OpenOrClose = Const.TradeOpen;
            else if (this.rbCloseToday.Checked)
            {
                OpenOrClose = Const.TradeCloseToday;
            }
            else
            {
                OpenOrClose = Const.TradeClose;
            }

            myuser.ReqOrderInsert(this.tbInstrumentID.Text, BuyOrSell, OpenOrClose, price, volume);
        }

        /// <summary>/// 显示状态栏信息/// </summary>
        private void setStatusImpl(String msg)
        {
            if (this.statusStrip.InvokeRequired)
            {
                this.Invoke(new FormTool.SetFormMessage(setStatusImpl));
            }
            else
            {
                this.statusLabelLeft.Text = msg;
            }
        }

        private void gvOrder_DoubleClick(object sender, EventArgs e)
        {
            if (gvOrder.FocusedRowHandle < 0)
                return;

            String OrderSysID = (string)gvOrder.GetFocusedRowCellValue("OrderSysID");
            String ExchangeID = (string)gvOrder.GetFocusedRowCellValue("ExchangeID");

            String msg = "是否确定撤销报单" + OrderSysID + "？";
            DialogResult result = MessageBox.Show(msg, "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result.Equals(DialogResult.No))
            {
                return;
            }

            // 执行撤单
            myuser.ReqOrderAction(OrderSysID, ExchangeID);
        }
    }
}
