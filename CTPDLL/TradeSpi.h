#pragma once

#pragma comment(lib, "thostmduserapi.lib")
#pragma comment(lib, "thosttraderapi.lib")

#include "include\ThostFtdcMdApi.h"
#include "include\ThostFtdcTraderApi.h"
#include "include\ThostFtdcUserApiDataType.h"
#include "include\ThostFtdcUserApiStruct.h"

class CMdSpi : public CThostFtdcMdSpi
{
	public:
	CMdSpi(CThostFtdcMdApi *pMdApi);
	~CMdSpi() ;

	public:
	virtual void OnFrontConnected();
	virtual void OnFrontDisconnected(int r);

//	virtual int ReqUserLogin(CThostFtdcReqUserLoginField *pReqUserLoginField, int nRequestID);
	virtual void OnRspUserLogin(CThostFtdcRspUserLoginField *pRspUserLogin, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast);

//	virtual int ReqUserLogout(CThostFtdcUserLogoutField *pUserLogout, int nRequestID);
	virtual void OnRspUserLogout(CThostFtdcUserLogoutField *pUserLogout, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast);
	

	///深度行情通知
	virtual void OnRtnDepthMarketData(CThostFtdcDepthMarketDataField *pDepthMarketData);
	virtual void OnRspUnSubMarketData(CThostFtdcSpecificInstrumentField *pSpecificInstrument, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast);
//	virtual int UnSubscribeMarketData(char *ppInstrumentID[], int nCount);
	private:
	CThostFtdcMdApi *pMdApi;
};

