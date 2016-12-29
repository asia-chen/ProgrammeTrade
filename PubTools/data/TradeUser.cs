using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;

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
        public Account account { get; set; }
        /// 报单
        public BindingList<Order> order { get; set; }
        /// 成交
        public BindingList<Trade> trade { get; set; }
        /// 持仓
        public BindingList<UserPosition> position { get; set; }
        /// 合约
        public Dictionary<String, data.Instrument> instruments { get; set; }

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
            order = new BindingList<Order>();
            trade = new BindingList<Trade>();
            position = new BindingList<UserPosition>();
            instruments = new Dictionary<string, Instrument>();
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

            if (resStr[1].Equals("Order") && resStr[2].Equals("insert"))
            {
                onRspOrderInsert(resStr);
                return;
            }
            if (resStr[1].Equals("Order") && resStr[2].Equals("action"))
            {
                onRspOrderAction(resStr);
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
            if (resStr[1].Equals("Query") && resStr[2].Equals("marketdata"))
            {
                onRspQryMarketData(resStr);
                return;
            }

            /*if (resStr[1].Equals("Query") && resStr[2].Equals("marginrate"))
            {
                //  thisTradeUser.onReqQryMarginRate(resStr);
            }*/
            if (resStr[1].Equals("Query") && resStr[2].Equals("instrument"))
            {
                OnRspQryInstrument(resStr);
                return;
            }
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
                connectMsg = "login ok";
            }
            else
            {
                currStatus = 3;
                connectMsg = resStr[4];
            }

            FormTool.DisplayStatusMessage(connectMsg);
        }

        // -----------------------------------------------------------
        /// <summary>请求确认结算单</summary>
        public int ReqSettlementInfoConfirm()
        {
            if (currStatus != 2)
                return -1;

            String[] para = new String[8];

            para[0] = requestID.ToString();
            requestID++;

            para[1] = "sys";
            para[2] = "ReqSettlementInfoConfirm";
            para[3] = this.brokerID.Trim();
            para[4] = this.userID.Trim();
            para[6] = DateTime.Now.ToString("yyyyMMdd");
            para[7] = DateTime.Now.ToString("HH:mm:ss");

            this.ctpApi.tradeSendRequest(para);
            return 0;
        }


        // -------------------------------------------------------------------------------
        /// <summary>请求查询资金</summary>
        public int ReqQryAccount()
        {
            if (this.ctpApi == null)
                return -1;
            if (currStatus != 2)
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
            FormTool.AccountProcess(account);
            return 0;
        }

        // --------------------------------------------------------------------------------
        /// <summary>查询持仓</summary>
        public int ReqQryPosition()
        {
            if (this.ctpApi == null)
                return -1;
            if (currStatus != 2)
                return -2;

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
                this.position = new BindingList<UserPosition>();
            }

            // 记录数
            int numPosition = int.Parse(resStr[5]);
            // 每记录字段数
            int eachrecord = 43;

            lock (this.position)
            {
                this.position.Clear();
                for (int i = 0; i < numPosition; i++)
                {
                    UserPosition tmp_position = new UserPosition();
                    tmp_position.SetData(resStr, i * eachrecord + 6);

                    // 过滤0持仓记录
                    if (tmp_position.Position == 0)
                        continue;

                    // 循环查找，合并同合约、同方向持仓
                    int j = 0;
                    for (j = 0; j < position.Count; j++)
                    {
                        if (tmp_position.InstrumentID.Equals(position[j].InstrumentID) && tmp_position.PosiDirection.Equals(position[j].PosiDirection))
                            break;
                    }

                    if (j >= position.Count)
                    {
                        this.position.Add(tmp_position);
                    }
                    else
                    {
                        // if (tmp_position.PositionDate.Equals(Const.THOST_FTDC_PSD_Today))
                        {
                            tmp_position.YdPosition += position[j].YdPosition;
                            tmp_position.Position += position[j].Position;
                            tmp_position.PositionCost += position[j].PositionCost;
                            tmp_position.UseMargin += position[j].UseMargin;
                            tmp_position.PositionProfit += position[j].PositionProfit;

                            position.RemoveAt(j);
                            position.Insert(j, tmp_position);
                        }
                    }
                }
            }


            for (int i = 0; i < this.position.Count; i++)
            {
                Console.WriteLine("position: " + this.position[i].Position.ToString());
            }
            return 0;
        }

        // --------------------------------------------------------------------------------
        /// <summary>查询行情</summary>
        public int ReqQryMarketData(String instrumentID)
        {
            if (this.ctpApi == null)
                return -1;

            String[] para = new String[7];

            para[0] = requestID.ToString();
            requestID++;

            para[1] = "Query";
            para[2] = "marketdata";
            para[3] = this.brokerID.Trim();
            para[4] = this.userID.Trim();
            para[5] = "";
            para[6] = instrumentID;

            this.ctpApi.tradeSendRequest(para);
            return 0;
        }
        /// <summary>查询行情返回</summary>
        private int onRspQryMarketData(String[] resStr)
        {
            int eachRecord = 44;
            int totalRecord = (resStr.Length - 6) / eachRecord;
            for (int i = 0; i < totalRecord; i++)
            {
                Console.WriteLine(resStr[20 * i + 26]);
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
        /// <param name="orderType">委托类型，两位数字字符串，影响OrderLocalID</param>
        /// <returns>-1 委托类型长度错误，-2 委托类型内容错误 -3 发送错误 成功：请求编号（orderref）</returns>
        public int ReqOrderInsert(String instrumentID, String direction, String offsetFlag, double price, long volume,String orderType)
        {
            if (orderType.Length != 2)
                return -1;
            try
            {
                long.Parse(orderType);
            }
            catch (Exception)
            {
                return -2;
            }

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
            para[11] = requestID.ToString() + orderType;

            Console.WriteLine("Send an Order:" + para[11]);
            if(this.ctpApi.tradeSendRequest(para) == 0)
                return (int)requestID;
            else
                return -3;
        }

        /// <summary>报单返回：目前仅处理错误信息</summary>
        private int onRspOrderInsert(String[] resStr)
        {
            FormTool.DisplayErrorMessage(resStr[7]);

            String[] result = new String[2];
            result[0] = resStr[0];
            result[1] = resStr[7];
            DataProcess.OrderError(result);
            return 0;
        }

        /// <summary>报单返回</summary>
        private int onRtnOrder(String[] resStr)
        {
            lock (order)
            {
                Order thisorder = new Order();
                thisorder.SetData(resStr, 0);

                // 根据返回报单修改请求号，确保不重复（与订阅流的方式相关）
                long thisRequestID = long.Parse(thisorder.OrderRef) / 100;
                if (requestID < thisRequestID)
                    requestID = thisRequestID + 1;

                // TODO: 对于拒绝的报单，如何展示、存储，需完善
                if (thisorder.OrderSysID != null && !thisorder.OrderSysID.Trim().Equals(""))
                {
                    // 当前列表中是否已包含该报单，存在则更新，否则则添加
                    int i = 0;
                    for (i = 0; i < order.Count; i++)
                    {
                        if (order.ElementAt(i).OrderSysID.Equals(thisorder.OrderSysID))
                            break;
                    }

                    if (i < order.Count)
                    {
                        order.RemoveAt(i);
                    }
                    this.order.Add(thisorder);
                }
                else if(thisorder.OrderSysID.Trim().Equals("") && thisorder.OrderStatus.Equals("5"))
                {
                    String[] result = new String[2];
                    int len = resStr[9].Length - 2;
                    result[0] = resStr[9].Substring(0, len);
                    result[1] = resStr[56];
                    DataProcess.OrderError(result);
                }
            }

            return 0;
        }

        // --------------------------------------------------------------------------------
        /// <summary>请求撤单</summary>
        /// <param name="OrderSysID">报单号</param>
        public int ReqOrderAction(String OrderSysID, String ExchangeID)
        {
            String[] para = new String[8];

            para[0] = requestID.ToString();
            requestID++;

            para[1] = "Order";
            para[2] = "action";
            para[3] = this.brokerID.Trim();
            para[4] = this.userID.Trim();
            para[5] = "";

            para[6] = OrderSysID;
            para[7] = ExchangeID;

            this.ctpApi.tradeSendRequest(para);
            Console.WriteLine("Send an Order Action:" + OrderSysID);
            return 0;
        }

        public int onRspOrderAction(String[] resStr)
        {
            FormTool.DisplayErrorMessage(resStr[7]);
            return 0;
        }
        
        // --------------------------------------------------------------------------------
        // 成交返回
        private int onRtnTrade(String[] resStr)
        {
            Trade thistrade = new Trade();
            thistrade.SetData(resStr, 0);
            lock (this.trade)
            {
                this.trade.Add(thistrade);
            }

            DataProcess.TradeProcess(thistrade);
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
        }*/

        /// --------------------------------------------------------------------------------
        /// <summary>查询合约</summary>
        /// <param name="instrumentID">合约代码，空为查询全部</param>
        public int ReqQryInstrument(String instrumentID)
        {
            if (this.ctpApi == null)
                return -1;

            if (instrumentID == null)
                instrumentID = "";

            String[] para = new String[7];

            para[0] = requestID.ToString();
            requestID++;

            para[1] = "Query";
            para[2] = "instrument";
            para[3] = this.brokerID.Trim();
            para[4] = this.userID.Trim();
            para[5] = "";
            para[6] = instrumentID;

            return this.ctpApi.tradeSendRequest(para);
        }

        public int OnRspQryInstrument(String[] resStr)
        {
            if (this.instruments == null)
            {
                this.instruments = new Dictionary<string, Instrument>();
            }

            // 记录数
            int numInstrument = int.Parse(resStr[5]);
            // 每记录字段数
            int eachrecord = 31;

            for (int i = 0; i < numInstrument; i++)
            {
                Instrument tmp_instrument = new Instrument();
                tmp_instrument.SetData(resStr, i * eachrecord + 6);

                if (!instruments.ContainsKey(tmp_instrument.InstrumentID))
                {
                    this.instruments.Add(tmp_instrument.InstrumentID, tmp_instrument);
                }
            }
            return 0;
        }
        
    }
}
