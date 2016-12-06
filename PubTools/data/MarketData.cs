using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PubTools.data
{
    public class MarketData
    {
        CtpMdApi mdApi = null;
        
        /// <summary>当前状态：0 未连接；1 已连接未登录；2 已登录；3 登录错误</summary>
        public int currStatus = 0;
        /// <summary>登录反馈信息</summary>
        public String connectMsg;

        /// 内部使用
        private long requestID = 0;
        
        /// <summary>经纪人代码</summary>
        private String brokerID;
        /// <summary>用户ID</summary>
        private String userID;
        /// <summary>密码</summary>
        private String password;
        /// <summary>服务器网络地址</summary>
        private String mdAddr;

        // 
        Dictionary<String, int> instrumentIndex = null;
        List<List<MD>> marketData = null;

        public MarketData(String _brokerID, String _userID, String _password, String _mdAddr)
        {
            marketData = new List<List<MD>>();
            instrumentIndex = new Dictionary<string, int>();

            brokerID = _brokerID;
            userID = _userID;
            password = _password;
            mdAddr = _mdAddr;

            mdApi = new CtpMdApi(brokerID, userID, mdAddr, mdCallBackFunction);
        }

        private void mdCallBackFunction(String[] resStr, int nRequestID)
        {
            // 连接
            if (resStr[1].Equals("Md") && resStr[2].Equals("OnFrontConnected"))
            {
                onConnected();
                return;
            }
            // 登录
            if (resStr[1].Equals("Md") && resStr[2].Equals("login"))
            {
                onLogin(resStr);
                return;
            }

            // 订阅行情返回
            if (resStr[1].Equals("Md") && resStr[2].Equals("subscribe"))
            {
                onSubscribeMarketData(resStr);
                return;
            }

            // 退订行情返回
            if (resStr[1].Equals("Md") && resStr[2].Equals("unsubscribe"))
            {
                onUnSubscribeMarketData(resStr);
                return;
            }

            // 行情数据
            if (resStr[1].Equals("Md") && resStr[2].Equals("marketdata"))
            {
                onRtnDepthMarketData(resStr);
                return;
            }
        }

        // -----------------------------------------------------------
        /// <summary>系统已连接</summary> 
        private void onConnected()
        {
            currStatus = 1;
            FormTool.DisplayStatusMessage("MD connected.");
        }
        
        /// <summary>请求登录</summary>
        /// <returns>0 成功发出请求；-1 未连接；-2 已登录；-3 未初始化CTP；-4 参数错误</returns>
        public int ReqLogin()
        {
            if (currStatus < 1)
                return -1;
            if (currStatus == 2)
                return -2;
            if (this.mdApi == null)
                return -3;
            if (this.brokerID.Equals("") || this.userID.Equals("") || this.password.Equals(""))
                return -4;

            String[] para = new String[6];

            para[0] = requestID.ToString();
            requestID++;

            para[1] = "Md";
            para[2] = "login";
            para[3] = this.brokerID.Trim();
            para[4] = this.userID.Trim();
            para[5] = this.password.Trim();

            this.mdApi.mdSendRequest(para);
            return 0;
        }
        /// <summary>登录请求返回</summary>
        private void onLogin(String[] resStr)
        {
            if (resStr[3].Equals("0"))
            {
                currStatus = 2;
                connectMsg = "MD login ok";
            }
            else
            {
                currStatus = 3;
                connectMsg = "MD: " + resStr[4];
            }

            FormTool.DisplayStatusMessage(connectMsg);
        }


        /// <summary>订阅行情</summary>
        /// <returns>0 成功发出请求；-1 未连接</returns>
        public int SubscribeMarketData(String instrumentID)
        {
            if (currStatus != 2)
                return -1;

            String[] para = new String[7];

            para[0] = requestID.ToString();
            requestID++;

            para[1] = "Md";
            para[2] = "subscribe";
            para[3] = this.brokerID.Trim();
            para[4] = this.userID.Trim();
            para[5] = "";
            para[6] = instrumentID;

            this.mdApi.mdSendRequest(para);
            return 0;
        }
        private void onSubscribeMarketData(String[] resStr)
        {
            if (resStr[3].Equals("0"))
            {
                FormTool.DisplayStatusMessage("订阅行情成功");

                // 本地索引数据添加
                List<MD> md = new List<MD>();
                marketData.Add(md);
                instrumentIndex.Add(resStr[6], marketData.Count);
            }
            else
            {
                FormTool.DisplayErrorMessage("订阅行情失败" + resStr[4]);
            }
        }

        /// <summary>退订行情</summary>
        /// <returns>0 成功发出请求；-1 未连接</returns>
        public int UnSubscribeMarketData(String instrumentID)
        {
            if (currStatus != 2)
                return -1;

            String[] para = new String[7];

            para[0] = requestID.ToString();
            requestID++;

            para[1] = "Md";
            para[2] = "unsubscribe";
            para[3] = this.brokerID.Trim();
            para[4] = this.userID.Trim();
            para[5] = "";
            para[6] = instrumentID;

            this.mdApi.mdSendRequest(para);
            return 0;
        }
        private void onUnSubscribeMarketData(String[] resStr)
        {
            if (resStr[3].Equals("0"))
            {
                FormTool.DisplayStatusMessage("退订行情成功");

                // 本地索引数据修改：仅删除索引，不删除实际数据
                instrumentIndex.Remove(resStr[6]);
            }
            else
            {
                FormTool.DisplayErrorMessage("退订行情失败：" + resStr[4]);
            }
        }

        /// <summary>行情数据返回：更新本地数据</summary>
        /// ??根据合约ID 触发对应回调？
        private void onRtnDepthMarketData(String[] resStr)
        {
            MD md = new MD();
            md.SetData(resStr);

            int index = instrumentIndex[md.InstrumentID];
            marketData[index - 1].Add(md);

            int thisCount = marketData[index - 1].Count;
            long thisVolume = 0;

            if ( thisCount>= 2)
            {
                thisVolume = marketData[index - 1][thisCount - 1].Volume - marketData[index - 1][thisCount - 2].Volume;
            }
            FormTool.DisplayMarketData(md.GetData(thisVolume));
        }
        // -----------------------------------------------------------
        /// <summary>获取合约最新行情</summary>
        /// <param name="instrumentID">合约代码</param>
        /// <returns>最新行情，无数据时返回null</returns>
        public MD getMarketData(String instrumentID)
        {
            List<MD> tempList = marketData[instrumentIndex[instrumentID]];
            MD result = null;
            if (tempList.Count > 0)
            {
                result = tempList[tempList.Count - 1];
            }
            tempList = null;
            return result;
        }
    }
}
