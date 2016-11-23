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

        // 报单本地编号
        private long localOrderID = 0;
        
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
                onLogin(resStr);
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

        // -----------------------------------------------------------
        /// <summary>系统已连接</summary> 
        private void onConnected()
        {
            currStatus = 1;
        }

        // -----------------------------------------------------------
        /// <summary>请求登录</summary>
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
        private void onLogin(String[] resStr)
        {
            if (resStr[3].Equals("0"))
            {
                currStatus = 2;
                return;
            }

            currStatus = 3;
            connectMsg = resStr[4];
        }

        // -------------------------------------------------------------------------------
        /// <summary>请求查询资金</summary>
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
        /// <summary>查询资金返回</summary>
        private int onReqQryAccount(String[] resStr)
        {
            if (this.account == null)
            {
                account = new Account();
            }

            account.SetData(resStr);
            return 0;
        }

        // --------------------------------------------------------------------------------
        /// <summary>查询持仓</summary>
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
        /// <summary>查询持仓返回</summary>
        private int onReqQryPosition(String[] resStr)
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
        /// <summary>请求报单</summary>
        /// <param name="instrumentID">合约</param>
        /// <param name="direction">买卖方向</param>
        /// <param name="offsetFlag">开平</param>
        /// <param name="price">价格</param>
        /// <param name="volume">数量</param>
        public int ReqOrderInsert(String instrumentID, String direction, String offsetFlag, double price, long volume)
        {
            String[] para = new String[12];

            para[0] = requestID.ToString();
            requestID++;

            para[1] = "Order";
            para[2] = "insert";
            para[3] = this.brokerID.Trim();
            para[4] = this.userID.Trim();
            para[5] = "";

            para[6] = instrumentID;
            para[7] = direction;
            para[8] = offsetFlag;
            para[9] = price.ToString();
            para[10] = volume.ToString();
            para[11] = localOrderID.ToString();
            localOrderID++;

            this.ctpApi.tradeSendRequest(para);
            Console.WriteLine("Send an Order:" + localOrderID.ToString());
            return 0;
        }

        /// <summary>报单返回</summary>
        private int onRtnOrder(String[] resStr)
        {
            Order thisorder = new Order();
            thisorder.SetData(resStr, 0);

            // TODO: 对于拒绝的报单，如何展示、存储，需完善
            if (thisorder.OrderRef != null && !thisorder.OrderRef.Equals(""))
            {
                // 登录后重传各个报单，确保本地委托编号唯一（若订阅私有流不是重传模式，此处需调整）
                long tmpOrderID = long.Parse(thisorder.OrderRef);
                if (tmpOrderID >= localOrderID)
                    localOrderID = tmpOrderID + 1;

                // 当前列表中是否已包含该报单，存在则更新，否则则添加
                Order existOrder = this.order.Find(
                delegate(Order order)
                {
                    return order.OrderSysID.Equals(thisorder.OrderSysID);
                });

                if (existOrder == null)
                {
                    this.order.Add(thisorder);
                }
                else
                {
                    // TODO 是否可行，待验证？？
                    existOrder = (Order)thisorder.Clone();
                }
            }

            return 0;
        }

        // --------------------------------------------------------------------------------
        // 成交返回
        private int onRtnTrade(String[] resStr)
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
