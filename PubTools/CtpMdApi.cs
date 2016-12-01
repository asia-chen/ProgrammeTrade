using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;

namespace PubTools
{
    public class CtpMdApi
    {
        /// <summary>回调：业务处理由相应对象完成</summary>
        GlobalVar.CTPCallBack mdCallBack = null;

        // ------------------------------------------------------------------------
        /// <summary>供DLL回调：返回数据</summary>
        [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public delegate void CallbackDelegate(string instr, int nRequestID, int bIsLast);
        public CallbackDelegate mdCallback;

        /// <summary>调用DLL：初始化接口</summary>
        [DllImport("CTPDLL", EntryPoint = "MdInitAPI", CallingConvention = CallingConvention.Cdecl)]
        private extern static int MdInitAPI(string serveraddr, CallbackDelegate func, string user);

        /// <summary>调用DLL：发送请求</summary>
        [DllImport("CTPDLL", EntryPoint = "MdSendRequest", CallingConvention = CallingConvention.Cdecl)]
        private extern static int MdSendRequest(string instr);

        // ------------------------------------------------------------------------
        /// <summary>初始化，尝试连接服务器</summary>
        /// <param name="brokerID">经纪人代码</param>
        /// <param name="userID">用户ID</param>
        /// <param name="tradeAddr">交易接入地址：tcp://xxx.xxx.xxx.xxx:xxxx</param>
        /// <param name="thisone">业务回调函数</param>
        public CtpMdApi(String brokerID, String userID, String tradeAddr, GlobalVar.CTPCallBack thisone)
        {
            mdCallBack = thisone;
            mdCallback = new CallbackDelegate(this.mdCallbackFunction);
            MdInitAPI(tradeAddr, mdCallback, brokerID.Trim() + "-" + userID.Trim());
        }

        /// <summary>发送交易请求</summary>
        /// <param name="paras">paras</param>
        /// <returns>0 发送成功；其他：发送失败</returns>
        public int mdSendRequest(string[] paras)
        {
            if (paras[0] == "-1")
            {
                return MdSendRequest(CommonTool.packString(paras));
            }
            else
            {
                return MdSendRequest(CommonTool.packString(paras));
            }
        }

        /// <summary>交易回调函数</summary>
        /// <param name="instr">结果，或出错信息</param>
        /// <param name="nRequestID">请求号（负数为服务器推送）</param>
        /// <param name="bIsLast">0 有后续数据；1 数据结束；-1 出错（instr为出错信息）</param>
        private void mdCallbackFunction(string instr, int nRequestID, int bIsLast)
        {
            // 解析返回数据
            string sRequestID = nRequestID.ToString(); 
            string[] resStr = Regex.Split(instr, Const.splitstr, RegexOptions.IgnoreCase);

            mdCallBack(resStr, nRequestID);
        }        
    }
}
