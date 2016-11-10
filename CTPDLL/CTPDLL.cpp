// CTPDLL.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "CTPDLL.h"
#include "TradeSpi.h"
#include "tools.h"

#include <iostream> 
#include <string> 
#include <vector>
using namespace std;

// 交易接口及回调接口
CThostFtdcMdApi *pMdApi = NULL;
CMdSpi* pMdSpi = NULL;

// 交易号
int nRequestID;

// 保留在DLL中的相关信息，后续其他函数可能用到
// 交易日
TThostFtdcDateType TradingDay;
// 经纪公司代码
TThostFtdcBrokerIDType	BrokerID;
// 用户代码
TThostFtdcUserIDType	UserID;
// 密码
TThostFtdcPasswordType	Password;

// 初始化交易接口
int TradeInitAPI(char *server_addr, CallbackDelegate callback)
{
	// 防止重复初始化
	if (pMdApi != NULL)
		return -1;

	// 回调函数
	tradeCallback = callback;

	// 标准流程
	pMdApi = CThostFtdcMdApi::CreateFtdcMdApi("",false,false);
	pMdSpi  = new CMdSpi(pMdApi);

	pMdApi->RegisterSpi(pMdSpi);
	pMdApi->RegisterFront(server_addr);
	pMdApi->Init();
	// pTradeApi->Join();

	strcpy_s(TradingDay, sizeof(TThostFtdcDateType), pMdApi ->GetTradingDay());

	return 0;
}

// 回调函数，所有TradeSpi.cpp中的函数，返回值均通过此处返回C#
int TradeResponse(char *result, int nRequestID, int bIsLast)
{
	if (tradeCallback == NULL)
		return -1;

	(*tradeCallback)(result, nRequestID, bIsLast);
	return 0;
}
int MdResponse(char *result)
{
	if (tradeCallback == NULL)
		return -1;

	(*tradeCallback)(result, -1, 1);
	return 0;
}
// 接受C#发送的请求（字符串形式），解析后分发到不同函数，此函数代码行数与实现的接口内容多少密切相关
// 请求内容详见readme.txt
int TradeSendRequest(char *req)
{
	int result = 0;
	vector<string> v;
	split(req, &v);

	nRequestID = atoi(v[0].c_str());
	string modulename = v[1];
	string requesttype = v[2];

	CMdSpi *spi_ptr = dynamic_cast<CMdSpi*>(pMdSpi);

	if (modulename=="sys")
	{
		 if (requesttype=="login")
		 {
			 // 本地记录相关参数，供后期使用
			 strcpy_s(BrokerID, sizeof(TThostFtdcBrokerIDType), v[3].c_str());
			 strcpy_s(UserID, sizeof(TThostFtdcUserIDType), v[4].c_str());
			 strcpy_s(Password, sizeof(TThostFtdcPasswordType), v[5].c_str());

			 // 填写登录结构内容
			 CThostFtdcReqUserLoginField pReqUserLoginField;
			 strcpy_s(pReqUserLoginField.TradingDay, sizeof(TThostFtdcDateType), TradingDay);
			 strcpy_s(pReqUserLoginField.BrokerID, sizeof(TThostFtdcBrokerIDType), v[3].c_str());
			 strcpy_s(pReqUserLoginField.UserID, sizeof(TThostFtdcUserIDType), v[4].c_str());
			 strcpy_s(pReqUserLoginField.Password, sizeof(TThostFtdcPasswordType), v[5].c_str());
			 strcpy_s(pReqUserLoginField.ProtocolInfo, sizeof(TThostFtdcProductInfoType),"Q7");
			 strcpy_s(pReqUserLoginField.InterfaceProductInfo, sizeof(TThostFtdcProductInfoType),"THOST User");

			 result = pMdApi->ReqUserLogin(&pReqUserLoginField, nRequestID);
		 }
		 else if (requesttype=="logout")
		 {
			 
			 // 填写登出结构内容
			 CThostFtdcUserLogoutField pReqUserLogoutField;
			 strcpy_s(pReqUserLogoutField.BrokerID, sizeof(TThostFtdcBrokerIDType), v[3].c_str());
			 strcpy_s(pReqUserLogoutField.UserID, sizeof(TThostFtdcUserIDType), v[4].c_str());
			 result = pMdApi->ReqUserLogout(&pReqUserLogoutField, nRequestID);
		 }
	}
	else if (modulename=="Query")
	{
		 if (requesttype=="Md")
		 {
			 char *tmp[2];
			 TThostFtdcInstrumentIDType tmpStr;

			 strcpy_s(tmpStr, sizeof(TThostFtdcInstrumentIDType), v[3].c_str());
			 tmp[0] = (char *)&tmpStr;
			 tmp[1] = NULL;
			 result = pMdApi->SubscribeMarketData((char **)tmp, 1);
		 }
		 else if (requesttype=="unMd")
		 {
			 char *tmp[2];
			 TThostFtdcInstrumentIDType tmpStr;

			 strcpy_s(tmpStr, sizeof(TThostFtdcInstrumentIDType), v[3].c_str());
			 tmp[0] = (char *)&tmpStr;
			 tmp[1] = NULL;
			 result = pMdApi->UnSubscribeMarketData((char **)tmp, 1);
		 }
		 else if (requesttype=="position")
		 {
			 // result = pTradeApi->ReqQryInvestorPosition
		 }
		 else if (requesttype=="account")
		 {
			 // result = pTradeApi->ReqQryTradingAccount
		 }
		 
	}
	return result;
}