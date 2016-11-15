using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PubTools.data
{
    /// <summary>
    /// 交易用户// CtpAPI ctpapi = new CtpAPI("9999", "054102", "tcp://xxx.xxx.xxx.xxx:xxxx", 
    /// </summary>
    public class TradeUser
    {

        public Boolean isConnected = false;
        public Boolean isLogin = false;

        /// <summary>当前状态：0 未连接；1 已连接未登录；2 已登录；3 登录错误</summary>
        public int currStatus = 0;
        /// <summary>登录反馈信息</summary>
        public String connectMsg;

        /// <summary>经纪人代码</summary>
        private String brokerID;
        /// <summary>用户ID</summary>
        private String userID;
        /// <summary>密码</summary>
        private String password;
        /// <summary>服务器网络地址</summary>
        private String tradeAddr;

        /// 内部使用
        private long requestID = 0;
        private CtpAPI ctpApi;

        /// 业务数据
        /// 资金
        private Account account = null;
        /// 报单
        private List<Order> order = null;
        /// 成交
        private List<Trade> trade = null;
        /// 持仓
        private List<UserPosition> position = null;

        /// <summary>
        /// 构造函数，初始化变量及CTP连接
        /// </summary>
        public TradeUser(String _brokerID, String _userID, String _password, String _tradeAddr)
        {
            if (_brokerID.Equals("") || _userID.Equals("") || _password.Equals("") || _tradeAddr.Equals(""))
                throw new Exception("参数错误");

            brokerID = _brokerID;
            userID = _userID;
            password = _password;
            tradeAddr = _tradeAddr;

            currStatus = 0;
            connectMsg = "";
            this.ctpApi = new CtpAPI(brokerID, userID, tradeAddr, this.tradeCallBack);
            
            account = new Account();
            order = new List<Order>();
            trade = new List<Trade>();
            position = new List<UserPosition>();
        }

        /// <summary>
        /// 接口返回数据分发
        /// </summary>
        /// <param name="resStr">接口返回数据</param>
        /// <param name="nRequestID">请求号</param>
        private void tradeCallBack(String[] resStr, int nRequestID)
        {            
            if (resStr[1].Equals("sys") && resStr[2].Equals("OnFrontConnected"))
            {
                onConnected();
                return;
            }
            if (resStr[1].Equals("sys") && resStr[2].Equals("login"))
            {
                onLogin();
                return;
            }
            if (resStr[1].Equals("sys") && resStr[2].Equals("OnRtnOrder"))
            {
                onRtnOrder(resStr);
                return;
            }
            if (resStr[1].Equals("sys") && resStr[2].Equals("OnRtnTrade"))
            {
                onRtnTrade(resStr);
                return;
            }
            if (resStr[1].Equals("Query") && resStr[2].Equals("account"))
            {
                onReqQryAccount(resStr);
                return;
            }
            if (resStr[1].Equals("Query") && resStr[2].Equals("position"))
            {
                onReqQryPosition(resStr);
                return;
            }
            /*if (resStr[1].Equals("Query") && resStr[2].Equals("marginrate"))
            {
                //  thisTradeUser.onReqQryMarginRate(resStr);
            }
            if (resStr[1].Equals("Query") && resStr[2].Equals("instrument"))
            {
                onReqQryInstrument(resStr);
                return;
            }*/
        }

        // 登录流程
        public void LoginProgress()
        {
            /*connectMsg = "等待连接 . . .";

            int count = 0;
            while (!isConnected && count < Const.TimeoutCount)
            {
                Thread.Sleep(Const.TimeoutEachLen);
                count++;
            }
            if (!isConnected)
            {
                connectMsg = "等待连接超时";
                return;
            }

            connectMsg = "请求登录 . . .";
            count = 0;
            this.ReqLogin();

            while (!isLogin && count < Const.TimeoutCount)
            {
                Thread.Sleep(Const.TimeoutEachLen);
                count++;
            }
            if (!isLogin)
            {
                connectMsg = "登录超时";
                return;
            }
            connectMsg = "已登录";

            // 主动查询资金、持仓
            // this.ReqQryAccount();
            // this.ReqQryPosition();
            return;*/
        }

        /// <summary>
        /// 系统已连接
        /// </summary> 
        public void onConnected()
        {
            currStatus = 1;
        }

        // -----------------------------------------------------------
        // 主动请求数据

        /// <summary>
        /// 请求登录
        /// </summary>
        /// <returns>0 成功发出请求；-1 未连接；-2 已登录；-3 未初始化CTP；-4 参数错误</returns>
         public int ReqLogin()
        {
            if (currStatus < 1)
                return -1;
            if (currStatus == 2)
                return -2;
            if (this.ctpApi == null)
                return -3;
            if (this.brokerID.Equals("") || this.userID.Equals("") || this.password.Equals(""))
                return -4;

            String[] para = new String[6];

            para[0] = requestID.ToString();
            requestID++;

            para[1] = "sys";
            para[2] = "login";
            para[3] = this.brokerID.Trim();
            para[4] = this.userID.Trim();
            para[5] = this.password.Trim();

            this.ctpApi.tradeSendRequest(para);
            return 0;
        }
        /// <summary>
        /// 登录请求返回
        /// </summary>
        public void onLogin()
        {
            currStatus = 2;
            currStatus = 3;
        }

        // -------------------------------------------------------------------------------
        // 查询资金
        public int ReqQryAccount()
        {
            if (this.ctpApi == null)
                return -1;
            if (!isLogin)
                return -2;

            String[] para = new String[6];

            para[0] = requestID.ToString();
            requestID++;

            para[1] = "Query";
            para[2] = "account";
            para[3] = this.brokerID.Trim();
            para[4] = this.userID.Trim();
            para[5] = "";

            this.ctpApi.tradeSendRequest(para);
            return 0;
        }

        public int onReqQryAccount(String[] resStr)
        {
            if (this.account == null)
            {
                account = new Account();
            }

            account.SetData(resStr);
            return 0;
        }

        // --------------------------------------------------------------------------------
        // 查询持仓
        public int ReqQryPosition()
        {
            if (this.ctpApi == null)
                return -1;

            String[] para = new String[6];

            para[0] = requestID.ToString();
            requestID++;

            para[1] = "Query";
            para[2] = "position";
            para[3] = this.brokerID.Trim();
            para[4] = this.userID.Trim();
            para[5] = "";

            this.ctpApi.tradeSendRequest(para);
            return 0;
        }

        public int onReqQryPosition(String[] resStr)
        {
            if (this.position == null)
            {
                this.position = new List<UserPosition>();
            }

            this.position.Clear();
            // 记录数
            int numPosition = int.Parse(resStr[5]);
            // 每记录字段数
            int eachrecord = 43;

            for (int i = 0; i < numPosition; i++)
            {
                UserPosition tmp_position = new UserPosition();
                tmp_position.SetData(resStr, i * eachrecord + 6);
                this.position.Add(tmp_position);
            }
            return 0;
        }

        // --------------------------------------------------------------------------------
        // 报单返回
        public int onRtnOrder(String[] resStr)
        {
            Order thisorder = new Order();
            thisorder.SetData(resStr, 0);
            this.order.Add(thisorder);
            return 0;
        }

        // --------------------------------------------------------------------------------
        // 成交返回
        public int onRtnTrade(String[] resStr)
        {
            Trade thistrade = new Trade();
            thistrade.SetData(resStr, 0);
            this.trade.Add(thistrade);
            return 0;
        }

        // --------------------------------------------------------------------------------
        // 查询保证金率
        public int ReqQryMarginRate()
        {
            if (this.ctpApi == null)
                return -1;

            String[] para = new String[6];

            para[0] = requestID.ToString();
            requestID++;

            para[1] = "Query";
            para[2] = "marginrate";
            para[3] = this.brokerID.Trim();
            para[4] = this.userID.Trim();
            para[5] = "";

            this.ctpApi.tradeSendRequest(para);
            return 0;
        }

        /*public int onReqQryMarginRate(String[] resStr)
        {
            if (this.marginrate == null)
            {
                this.marginrate = new List<MarginRate>();
            }

            this.marginrate.Clear();
            // 记录数
            int numMarginRate = int.Parse(resStr[5]);
            // 每记录字段数
            int eachrecord = 10;

            for (int i = 0; i < numMarginRate; i++)
            {
                MarginRate tmp_marginrate = new MarginRate();
                tmp_marginrate.SetData(resStr, i * eachrecord + 6);
                this.marginrate.Add(tmp_marginrate);
            }
            return 0;
        }

        // --------------------------------------------------------------------------------
        // 查询合约
        public int ReqQryInstrument()
        {
            if (this.ctpApi == null)
                return -1;

            String[] para = new String[6];

            para[0] = requestID.ToString();
            requestID++;

            para[1] = "Query";
            para[2] = "instrument";
            para[3] = this.brokerID.Trim();
            para[4] = this.userID.Trim();
            para[5] = "";

            this.ctpApi.tradeSendRequest(para);
            return 0;
        }

        public int onReqQryInstrument(String[] resStr)
        {
            if (this.instrument == null)
            {
                this.instrument = new List<Instrument>();
            }

            this.instrument.Clear();
            // 记录数
            int numInstrument = int.Parse(resStr[5]);
            // 每记录字段数
            int eachrecord = 31;

            for (int i = 0; i < numInstrument; i++)
            {
                Instrument tmp_instrument = new Instrument();
                tmp_instrument.SetData(resStr, i * eachrecord + 6);
                this.instrument.Add(tmp_instrument);
            }
            return 0;
        }
        */
    }
}
