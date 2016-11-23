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


char *splitstr = "><";
		
// 交易接口及回调接口
string tradeUsers[MAXUSERS];
CThostFtdcTraderApi *tradeApis[MAXUSERS];
CTradeSpi *tradeSpis[MAXUSERS];
static CallbackDelegate tradeCallbacks[MAXUSERS];
int indicator = -1;

//行情接口
CThostFtdcMdApi *pMdApi = NULL;
CMdSpi* pMdSpi = NULL;
static CallbackDelegate MdCallback;
/*
CThostFtdcTraderApi *pTradeApi;
CTradeSpi *pTradeSpi;
static CallbackDelegate tradeCallback = NULL;*/

// 交易日
TThostFtdcDateType TradingDay;

// 初始化交易接口
int TradeInitAPI(char *server_addr, CallbackDelegate callback, char* tradeUser)
{
	if (indicator >= MAXUSERS)
		return -1;

	// 第一次进入，初始化
	if (indicator == -1)
	{
		for (int i=0;i<MAXUSERS;i++)
		{
			tradeApis[i] = NULL;
			tradeSpis[i] = NULL;
			tradeCallbacks[i] = NULL;
			tradeUsers[i] = "";
		}
		indicator = 0;
	}

	// 回调函数
	tradeCallbacks[indicator] = callback;
	
	// brokerid + userid
	tradeUsers[indicator] = (string)tradeUser;

	// 标准流程
	tradeApis[indicator] = CThostFtdcTraderApi::CreateFtdcTraderApi("");
	tradeSpis[indicator] = new CTradeSpi(tradeApis[indicator], indicator);

	// 公共流，全部传输
	// TODO 暂未知哪些数据
	tradeApis[indicator]->SubscribePublicTopic(THOST_TERT_RESTART);
	// 私有流：从登录后传输
	// tradeApis[indicator]->SubscribePrivateTopic(THOST_TERT_QUICK);

	tradeApis[indicator]->RegisterSpi(tradeSpis[indicator]);
	tradeApis[indicator]->RegisterFront(server_addr);
	tradeApis[indicator]->Init();
	strcpy_s(TradingDay, sizeof(TThostFtdcDateType), tradeApis[indicator]->GetTradingDay());

	indicator++;
	return 0;
}

// 初始化行情接口
int MdInitAPI(char *server_addr, CallbackDelegate callback)
{
	// 防止重复初始化
	if (pMdApi != NULL)
		return -1;

	// 回调函数
	MdCallback = callback;

	// 标准流程
	pMdApi = CThostFtdcMdApi::CreateFtdcMdApi("", false, false);
	pMdSpi = new CMdSpi(pMdApi);

	pMdApi->RegisterSpi(pMdSpi);
	pMdApi->RegisterFront(server_addr);
	pMdApi->Init();
	// pMdApi->Join();

	strcpy_s(TradingDay, sizeof(TThostFtdcDateType), pMdApi->GetTradingDay());
	if (TradingDay == "")
		return -1;
	else
		return 0;
}


// 回调函数，所有TradeSpi.cpp中的函数，返回值均通过此处返回C#
int TradeResponse(char *result, int nRequestID, int bIsLast, int m_indicator)
{
	if (tradeCallbacks[m_indicator] == NULL)
		return -1;

	(*(tradeCallbacks[m_indicator]))(result, nRequestID, bIsLast);

	return 0;
}

// 回调函数，所有TradeSpi.cpp中的行情函数，返回值均通过此处返回C#

int MdResponse(char *result)
{
	if (MdCallback == NULL)
		return -1;

	(*(MdCallback))(result, -7,-7);

	return 0;
}

// 接受C#发送的请求（字符串形式），解析后分发到不同函数，此函数代码行数与实现的接口内容多少密切相关
// 请求内容详见readme.txt
int TradeSendRequest(char *req)
{
	// 交易号
	int nRequestID=0;

	// 解析请求字符串
	int result = 0;
	vector<string> v;
	split(req, &v);

	// 获取请求号、请求类型
	nRequestID = atoi(v[0].c_str());
	string modulename = v[1];
	string requesttype = v[2];
	
	// 获取请求所属用户
	string brokerID = v[3];
	string userID = v[4];
	string user = brokerID + "-" + userID;
	if (modulename == "Md")//行情相关
	{

		CMdSpi *spi_ptr = dynamic_cast<CMdSpi*>(pMdSpi);
		if (requesttype == "login")
		{
			//登录
			CThostFtdcReqUserLoginField pReqUserLoginField;
			memset(&pReqUserLoginField, 0, sizeof(CThostFtdcReqUserLoginField));
			strcpy_s(pReqUserLoginField.TradingDay, sizeof(TThostFtdcDateType), TradingDay);
			strcpy_s(pReqUserLoginField.BrokerID, sizeof(TThostFtdcBrokerIDType), v[3].c_str());
			strcpy_s(pReqUserLoginField.UserID, sizeof(TThostFtdcUserIDType), v[4].c_str());
			strcpy_s(pReqUserLoginField.Password, sizeof(TThostFtdcPasswordType), v[5].c_str());

			strcpy_s(pReqUserLoginField.UserProductInfo, sizeof(TThostFtdcProductInfoType), "Q7");
			strcpy_s(pReqUserLoginField.InterfaceProductInfo, sizeof(TThostFtdcProductInfoType), "THOST User");

			result =  pMdApi->ReqUserLogin(&pReqUserLoginField, 1);


		}
		else if (requesttype == "disconnected")
		{
			spi_ptr->OnFrontDisconnected(1);
		}
		else if (requesttype == "subscribe")
		{
			char *tmp[2];
			TThostFtdcInstrumentIDType tmpStr;

			strcpy_s(tmpStr, sizeof(TThostFtdcInstrumentIDType), v[5].c_str());
			tmp[0] = (char *)&tmpStr;
			tmp[1] = NULL;
			result = pMdApi->SubscribeMarketData((char **)tmp, 1);
		}
		else if (requesttype == "unsubscribe")
		{
			char *tmp[2];
			TThostFtdcInstrumentIDType tmpStr;

			strcpy_s(tmpStr, sizeof(TThostFtdcInstrumentIDType), v[5].c_str());
			tmp[0] = (char *)&tmpStr;
			tmp[1] = NULL;
			result = pMdApi->UnSubscribeMarketData((char **)tmp, 1);
		}
		return result;
	}
	else
	{


		// 查找对应接口
		int i = 0;
		for (i = 0; i < indicator; i++)
		{
			if (tradeUsers[i] == user)
				break;
		}

		// 未找到
		if (i >= indicator)
			return -2;

		CThostFtdcTraderApi *pTradeApi = tradeApis[i];
		CTradeSpi *spi_ptr = dynamic_cast<CTradeSpi*>(tradeSpis[i]);

		if (modulename == "sys")
		{
			if (requesttype == "login")
			{
				// 填写登录结构内容
				CThostFtdcReqUserLoginField pReqUserLoginField;
				memset(&pReqUserLoginField, 0, sizeof(CThostFtdcReqUserLoginField));
				strcpy_s(pReqUserLoginField.TradingDay, sizeof(TThostFtdcDateType), TradingDay);
				strcpy_s(pReqUserLoginField.BrokerID, sizeof(TThostFtdcBrokerIDType), v[3].c_str());
				strcpy_s(pReqUserLoginField.UserID, sizeof(TThostFtdcUserIDType), v[4].c_str());
				strcpy_s(pReqUserLoginField.Password, sizeof(TThostFtdcPasswordType), v[5].c_str());

				strcpy_s(pReqUserLoginField.UserProductInfo, sizeof(TThostFtdcProductInfoType), "Q7");
				strcpy_s(pReqUserLoginField.InterfaceProductInfo, sizeof(TThostFtdcProductInfoType), "THOST User");

				result = pTradeApi->ReqUserLogin(&pReqUserLoginField, nRequestID);
			}
			else if (requesttype == "logout")
			{
			}
		}
		else if (modulename == "Query")
		{
			if (requesttype == "order")
			{
				result = spi_ptr->ReqQryOrder((char *)v[3].c_str(), (char *)v[4].c_str(), nRequestID);
			}
			else if (requesttype == "trade")
			{
				result = spi_ptr->ReqQryTrade((char *)v[3].c_str(), (char *)v[4].c_str(), nRequestID);
			}
			else if (requesttype == "position")
			{
				result = spi_ptr->ReqQryInvestorPosition((char *)v[3].c_str(), (char *)v[4].c_str(), nRequestID);
			}
			else if (requesttype == "account")
			{
				result = spi_ptr->ReqQryTradingAccount((char *)v[3].c_str(), (char *)v[4].c_str(), nRequestID);
			}
			else if (requesttype == "marginrate")
			{
				// result = spi_ptr->ReqQryInstrumentMarginRate((char *)v[3].c_str(), (char *)v[4].c_str(), nRequestID);
			}
			else if (requesttype == "instrument")
			{
				result = spi_ptr->ReqQryInstrument((char *)v[3].c_str(), (char *)v[4].c_str(), nRequestID);
			}
		}
		else if (modulename == "Order")
		{
			if (requesttype == "insert") // 报单
			{
				result = spi_ptr->ReqOrderInsert(v, nRequestID);
			}
			else if (requesttype == "action") // 撤单
			{
				result = spi_ptr->ReqQryTrade((char *)v[3].c_str(), (char *)v[4].c_str(), nRequestID);
			}
		}
		return result;
	}
}