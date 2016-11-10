#include "stdafx.h"
#include <iostream>
#include <string>
using namespace std;

#include "CTPDLL.h"
#include "TradeSpi.h"

// 目前仅实现了OnFrontConnected和OnRspUserLogin，若需要实现其他的接口，需要先在TradeSpi.h中增加相应的函数，
// 然后在此处以自己的代码将数据进行转换，最后通过TradeResponse返回给C#，供C#解析

// 构造及析构函数，可添加自己的处理
CMdSpi::CMdSpi(CThostFtdcMdApi *pMdApi) 
{
}

CMdSpi::~CMdSpi() 
{
}

bool IsErrorRspInfo(CThostFtdcRspInfoField *pRspInfo)
{
	if (pRspInfo && pRspInfo->ErrorID != 0)
	{
		return true;
	}
	return false;
}

// --------------------------------------------------------------------------------------
// 当客户端与交易后台建立起通信连接时（还未登录前），该方法被调用。
void CMdSpi::OnFrontConnected()
{
	TradeResponse("-1><Md><OnFrontConnected><0><FrontConnected><0", -1, 1);
};

//断开连接
void CMdSpi::OnFrontDisconnected(int r)
{
	TradeResponse("-1><Md><OnFrontDisconnected><0><FrontDisconnected><0", -1, 1);
};
// 登录结果：若出错返回错误内容，否则返回登录时间
// 正常做法应该是返回： nRequestID><sys><login><message.....       此为简化版
void CMdSpi::OnRspUserLogin(CThostFtdcRspUserLoginField *pRspUserLogin, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (pRspInfo != NULL)
	{
		// fail 
		if (pRspInfo->ErrorID != 0)
		{
			TradeResponse("-1><Md><login><Wrong!", nRequestID, bIsLast);
		}
		else
		{
			MdResponse("-1><Md><login><0");
		}
	}
	return ;	
}
void CMdSpi::OnRspUserLogout(CThostFtdcUserLogoutField *pUserLogout, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (pRspInfo != NULL)
	{
		// fail 
		if (pRspInfo->ErrorID != 0)
		{
			TradeResponse("-1><Md><logout><Wrong!", nRequestID, bIsLast);
		}
		else
		{
			MdResponse("-1><Md><logout><0");
		}
	}
	return;
}
void CMdSpi::OnRtnDepthMarketData(CThostFtdcDepthMarketDataField *pDepthMarketData)
{
//	CThostFtdcDepthMarketDataField *pDMData;
	if (pDepthMarketData != NULL)
	{

		string result = "-1";
		string splitstr = "><";
		result = result + splitstr + "Md";
		result = result + splitstr + "Rtn";
		//交易日
		result = result + splitstr + (string)pDepthMarketData->TradingDay;
		///合约代码
		result = result + splitstr + (string)pDepthMarketData->InstrumentID;
		///交易所代码
		result = result + splitstr + (string)pDepthMarketData->ExchangeID;
		///合约在交易所的代码
		result = result + splitstr + (string)pDepthMarketData->ExchangeInstID;
		///最新价
		result = result + splitstr + dtos(pDepthMarketData->LastPrice);
		///上次结算价
		result = result + splitstr + dtos(pDepthMarketData->PreSettlementPrice);
		///昨收盘
		result = result + splitstr + dtos(pDepthMarketData->PreClosePrice);
		///昨持仓量
		result = result + splitstr + dtos(pDepthMarketData->PreOpenInterest);
		///今开盘
		result = result + splitstr + dtos(pDepthMarketData->OpenPrice);
		///最高价
		result = result + splitstr + dtos(pDepthMarketData->HighestPrice);
		///最低价
		result = result + splitstr + dtos(pDepthMarketData->LowestPrice);
		///数量
		result = result + splitstr + ltos(pDepthMarketData->Volume);
		///成交金额
		result = result + splitstr + dtos(pDepthMarketData->Turnover);
		///持仓量
		result = result + splitstr + dtos(pDepthMarketData->OpenInterest);
		///今收盘
		result = result + splitstr + dtos(pDepthMarketData->ClosePrice);
		///本次结算价
		result = result + splitstr + dtos(pDepthMarketData->SettlementPrice);
		///涨停板价
		result = result + splitstr + dtos(pDepthMarketData->UpperLimitPrice);
		///跌停板价
		result = result + splitstr + dtos(pDepthMarketData->LowerLimitPrice);
		///昨虚实度
		result = result + splitstr + dtos(pDepthMarketData->PreDelta);
		///今虚实度
		result = result + splitstr + dtos(pDepthMarketData->CurrDelta);
		///最后修改时间
		result = result + splitstr + (string)pDepthMarketData->UpdateTime;
		///最后修改毫秒
		result = result + splitstr + ltos(pDepthMarketData->UpdateMillisec);
		///申买价一
		result = result + splitstr + dtos(pDepthMarketData->BidPrice1);
		///申买量一
		result = result + splitstr + ltos(pDepthMarketData->BidVolume1);
		///申卖价一
		result = result + splitstr + dtos(pDepthMarketData->AskPrice1);
		///申卖量一
		result = result + splitstr + ltos(pDepthMarketData->AskVolume1);
		///申买价二
		result = result + splitstr + dtos(pDepthMarketData->BidPrice2);
		///申买量二
		result = result + splitstr + ltos(pDepthMarketData->BidVolume2);
		///申卖价二
		result = result + splitstr + dtos(pDepthMarketData->AskPrice2);
		///申卖量二
		result = result + splitstr + ltos(pDepthMarketData->AskVolume2);
		///申买价三
		result = result + splitstr + dtos(pDepthMarketData->BidPrice3);
		///申买量三
		result = result + splitstr + ltos(pDepthMarketData->BidVolume3);
		///申卖价三
		result = result + splitstr + dtos(pDepthMarketData->AskPrice3);
		///申卖量三
		result = result + splitstr + ltos(pDepthMarketData->AskVolume3);
		///申买价四
		result = result + splitstr + dtos(pDepthMarketData->BidPrice4);
		///申买量四
		result = result + splitstr + ltos(pDepthMarketData->BidVolume4);
		///申卖价四
		result = result + splitstr + dtos(pDepthMarketData->AskPrice4);
		///申卖量四
		result = result + splitstr + ltos(pDepthMarketData->AskVolume4);
		///申买价五
		result = result + splitstr + dtos(pDepthMarketData->BidPrice5);
		///申买量五
		result = result + splitstr + ltos(pDepthMarketData->BidVolume5);
		///申卖价五
		result = result + splitstr + dtos(pDepthMarketData->AskPrice5);
		///申卖量五
		result = result + splitstr + ltos(pDepthMarketData->AskVolume5);
		///当日均价
		result = result + splitstr + dtos(pDepthMarketData->AveragePrice);
		///业务日期
		result = result + splitstr + (string)pDepthMarketData->ActionDay;

		MdResponse((char *)result.c_str());
	}
	return;
}
void CMdSpi::OnRspUnSubMarketData(CThostFtdcSpecificInstrumentField *pSpecificInstrument, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (pRspInfo != NULL)
	{
		// fail 
		if (pRspInfo->ErrorID != 0)
		{
			TradeResponse("-1><Md><unMd><Wrong!", nRequestID, bIsLast);
		}
		else
		{
			MdResponse("-1><Md><unMd><0");
		}
	}
	return;
}

