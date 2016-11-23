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

            this.bLogin.Enabled = false;
            this.bOrder.Enabled = true;

            this.rbBuy.Checked = true;
            this.rbOpen.Checked = true;

            gcOrder.DataSource = myuser.order;
            gcTrade.DataSource = myuser.trade;

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
                BuyOrSell = "0";
            else
                BuyOrSell = "1";
            if (this.rbOpen.Checked)
                OpenOrClose = "0";
            else
            {
                OpenOrClose = "1";
            }

            myuser.ReqOrderInsert(this.tbInstrumentID.Text, BuyOrSell, OpenOrClose, price, volume);
        }
    }
}
