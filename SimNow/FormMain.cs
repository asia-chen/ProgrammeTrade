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

namespace SimNow
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            TradeUser myuser = new TradeUser("9999", "054102", "password", "tcp://111.111.111.111:1111");
            while (myuser.currStatus == 1)
            {
                // wait for connect
            }
            myuser.ReqLogin();

            // if login fail

            // query 
        }
    }
}
