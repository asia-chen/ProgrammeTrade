using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;

namespace CTPSample
{
	class CtpApi
	{
        const String splitstr = "><";

        private Dictionary<string, string[]> reqList;
        private Dictionary<string, int> countList;
        private Dictionary<string, string[]> resList;

        private data.TradeUser thisTradeUser;

        // ------------------------------------------------------------------------
        // 供DLL回调
        [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public delegate void CallbackDelegate(string instr, int nRequestID, int bIsLast);
        public CallbackDelegate tradeCallback;

        // 调用DLL使用
        [DllImport("CTPDLL", EntryPoint = "TradeInitAPI", CallingConvention = CallingConvention.Cdecl)]
        private extern static int TradeInitAPI(string serveraddr, CallbackDelegate func, string user);

        [DllImport("CTPDLL", EntryPoint = "TradeSendRequest", CallingConvention = CallingConvention.Cdecl)]
        private extern static int TradeSendRequest(string instr);


        //lcf
        // 行情调用DLL使用
        [DllImport("CTPDLL", EntryPoint = "MdInitAPI", CallingConvention = CallingConvention.Cdecl)]
        private extern static int MdInitAPI(string serveraddr, CallbackDelegate func);



        // ------------------------------------------------------------------------


        // 初始化
        public CtpApi(string tradeAddr,data.TradeUser thisone)//每个user 都有各自的 一组list？
        {
            thisTradeUser = thisone;
            reqList = new Dictionary<string, string[]>();
            countList = new Dictionary<string, int>();
            resList = new Dictionary<string, string[]>();
            tradeCallback = new CallbackDelegate(this.tradeCallbackFunction);
            TradeInitAPI(tradeAddr, tradeCallback, thisone.brokerID.Trim() + "-" + thisone.userID.Trim());
  
        }
        public CtpApi(string tradeAddr)//建立行情空连接        
        {
            tradeCallback = new CallbackDelegate(this.tradeCallbackFunction);    
            MdInitAPI(tradeAddr, tradeCallback);
         }

        // 发送交易请求
        public int tradeSendRequest(string[] paras)
        {
            /*while (reqList.Count > 0)
            {
                Console.WriteLine("request count =" + reqList.Count.ToString() + " " + reqList.ElementAt(0).Value[1] + reqList.ElementAt(0).Value[2]);
                Thread.Sleep(200);
            }*/
            if (paras[0] == "-1")
            {
                return TradeSendRequest(packString(paras));
            }
            else
            {
                reqList.Add(paras[0], paras);
                return TradeSendRequest(packString(paras));
            }
        }

      
        // 交易回调函数
        // instr: 结果
        // nRequestID：请求号（负数为服务器推送）
        // bIsLast： 0 有后续数据；1 数据结束；-1 出错（instr为出错信息）
        private void tradeCallbackFunction(string instr, int nRequestID, int bIsLast)
        {            
            // 解析返回数据
//            Console.WriteLine(instr);
            string sRequestID = nRequestID.ToString();//通过返回的请求序列号，去查询当初请求的内容
            string[] resStr = Regex.Split(instr, splitstr, RegexOptions.IgnoreCase);
      
            if (nRequestID >= 0)
            {
                if (bIsLast < 0)                    // 出错处理
                {
                    reqList[sRequestID][3] = resStr[0];//错误号
                    reqList[sRequestID][4] = resStr[1];//错误信息
                }
                else                                // 请求成功
                {
                    reqList[sRequestID][3] = "0";
                    reqList[sRequestID][4] = "";
                }

                // 处理：一次请求，多次回应的情况
                string[] tempStr;
                string[] newStr;
                // 更新查询结果到resList（根据相同的查询号，每条返回记录加上相同查询“头”后合并成一条，并更新）
                // countList更新该条查询的返回记录数
                if (resList.ContainsKey(sRequestID))
                {
                    int tempInt = countList[sRequestID];
                    tempStr = resList[sRequestID];
                    newStr = new string[tempStr.Length + resStr.Length];

                    Array.Copy(tempStr, 0, newStr, 0, tempStr.Length);
                    Array.Copy(resStr, 0, newStr, tempStr.Length, resStr.Length);

                    resList.Remove(sRequestID);
                    countList.Remove(sRequestID);

                    resList.Add(sRequestID, newStr);
                    countList.Add(sRequestID, tempInt + 1);
                }
                else
                {
                    resList.Add(sRequestID, resStr);
                    if (instr.Equals(""))
                    {
                        countList.Add(sRequestID, 0);
                    }
                    else
                    {
                        countList.Add(sRequestID, 1);
                    }
                }

                if (bIsLast == 0)
                {
                    return;
                }

                // add message head
                int copysize = 6;

                tempStr = resList[sRequestID];
                int count = countList[sRequestID];

                newStr = new string[tempStr.Length + copysize];
                Array.Copy(reqList[sRequestID], 0, newStr, 0, copysize);
                Array.Copy(resList[sRequestID], 0, newStr, copysize, resList[sRequestID].Length);

                reqList.Remove(sRequestID);
                countList.Remove(sRequestID);
                resList.Remove(sRequestID);

                resStr = newStr;
                resStr[5] = count.ToString();//记录的条数
            }
            else//// 处理nRequestID < 0的情况 ，系统推送 或 行情（-1）从此处入口
            {
                if (resStr[1].Equals("Md") && resStr[2].Equals("OnFrontConnected"))
                {
                    GlobalVar.isMdReady = 2;//建立行情空连接
                    return;
                }
                if (resStr[1].Equals("Md") && resStr[2].Equals("login"))
                {
                    if (resStr[3].Equals("0"))
                    {
                        GlobalVar.isMdReady = 4;//行情登录成功
                        return;
                    }
                    else
                    {
                        GlobalVar.isMdReady = 5;//登录失败
                        return;
                    }
                }
                if (resStr[1].Equals("Md") && resStr[2].Equals("Rtn"))
                {
                    GlobalVar.CMdApi.GetMdData(resStr,3);
                   
                    return;
                }
            }

// 处理nRequestID >= 0,且通过加工后的情况

            if (resStr[1].Equals("sys") && resStr[2].Equals("OnFrontConnected"))
            {
                thisTradeUser.onConnected();
                return;
            }
            if (resStr[1].Equals("sys") && resStr[2].Equals("login"))
            {
                thisTradeUser.onLogin();
                return;
            }
            if (resStr[1].Equals("sys") && resStr[2].Equals("OnRtnOrder"))
            {
                thisTradeUser.onRtnOrder(resStr);
                return;
            }
            if (resStr[1].Equals("sys") && resStr[2].Equals("OnRtnTrade"))
            {
                thisTradeUser.onRtnTrade(resStr);
                return;
            }
            if (resStr[1].Equals("Query") && resStr[2].Equals("account"))
            {
                thisTradeUser.onReqQryAccount(resStr);
                return;
            }
            if (resStr[1].Equals("Query") && resStr[2].Equals("position"))
            {
                 thisTradeUser.onReqQryPosition(resStr);
                 return;
            }
            if (resStr[1].Equals("Query") && resStr[2].Equals("marginrate"))
            {
              //  thisTradeUser.onReqQryMarginRate(resStr);
            }
            if (resStr[1].Equals("Query") && resStr[2].Equals("instrument"))
            {
                thisTradeUser.onReqQryInstrument(resStr);
                return;
            }
        }
        
        // 将string[]格式的参数数组 合并成用 >< 作为分割符的string
       private string packString(string[] paras)
        {
            string result = "";
            int num_paras = paras.Length;

            for (int i = 0; i < num_paras; i++)
            {
                if (paras[i] == null || paras[i].Equals(""))
                    result = result + " ";
                else
                    result = result + paras[i];
                if (i < num_paras - 1)
                {
                    result = result + splitstr;
                }
            }
            return result;
        }
    }
}
