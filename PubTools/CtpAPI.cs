using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;

namespace PubTools
{
    /// <summary>仅负责接口：发送交易请求、接收回调数据，将多条记录组合返回</summary>
    public class CtpAPI
    {
        const String splitstr = "><";

        /// <summary>回调：业务处理由相应对象完成</summary>
        GlobalVar.TradeCallBack tradeCallBack = null;

        // 请求列表
        private Dictionary<string, string[]> reqList;
        // 
        private Dictionary<string, int> countList;
        // 返回结果列表
        private Dictionary<string, string[]> resList;

        // ------------------------------------------------------------------------
        /// <summary>供DLL回调：返回数据</summary>
        [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public delegate void CallbackDelegate(string instr, int nRequestID, int bIsLast);
        public CallbackDelegate tradeCallback;

        /// <summary>调用DLL：初始化接口</summary>
        [DllImport("CTPDLL", EntryPoint = "TradeInitAPI", CallingConvention = CallingConvention.Cdecl)]
        private extern static int TradeInitAPI(string serveraddr, CallbackDelegate func, string user);

        /// <summary>调用DLL：发送请求</summary>
        [DllImport("CTPDLL", EntryPoint = "TradeSendRequest", CallingConvention = CallingConvention.Cdecl)]
        private extern static int TradeSendRequest(string instr);

        // ------------------------------------------------------------------------
        /// <summary>初始化，尝试连接服务器</summary>
        /// <param name="brokerID">经纪人代码</param>
        /// <param name="userID">用户ID</param>
        /// <param name="tradeAddr">交易接入地址：tcp://xxx.xxx.xxx.xxx:xxxx</param>
        /// <param name="thisone">业务回调函数</param>
        public CtpAPI(String brokerID, String userID, String tradeAddr, GlobalVar.TradeCallBack thisone)
        {
            tradeCallBack = thisone;
            reqList = new Dictionary<string, string[]>();
            countList = new Dictionary<string, int>();
            resList = new Dictionary<string, string[]>();
            tradeCallback = new CallbackDelegate(this.tradeCallbackFunction);
            TradeInitAPI(tradeAddr, tradeCallback, brokerID.Trim() + "-" + userID.Trim());
        }

        /// <summary>发送交易请求</summary>
        /// <param name="paras">paras</param>
        /// <returns>0 发送成功；其他：发送失败</returns>
        public int tradeSendRequest(string[] paras)
        {
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

        /// <summary>交易回调函数</summary>
        /// <param name="instr">结果，或出错信息</param>
        /// <param name="nRequestID">请求号（负数为服务器推送）</param>
        /// <param name="bIsLast">0 有后续数据；1 数据结束；-1 出错（instr为出错信息）</param>
        private void tradeCallbackFunction(string instr, int nRequestID, int bIsLast)
        {
            // 解析返回数据
            string sRequestID = nRequestID.ToString(); 
            string[] resStr = Regex.Split(instr, splitstr, RegexOptions.IgnoreCase);

            //通过返回的请求序列号，去查询当初请求的内容
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
            }

            tradeCallBack(resStr, nRequestID);


            
        }

        /// <summary>将string[]格式的参数数组 合并成用分割符分割的string</summary>
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
