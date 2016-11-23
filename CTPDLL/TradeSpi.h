#pragma once

#pragma comment(lib, "thostmduserapi.lib")
#pragma comment(lib, "thosttraderapi.lib")

#include "include\ThostFtdcMdApi.h"
#include "include\ThostFtdcTraderApi.h"
#include "include\ThostFtdcUserApiDataType.h"
#include "include\ThostFtdcUserApiStruct.h"

class CTradeSpi : public CThostFtdcTraderSpi
{
	public:
	CTradeSpi(CThostFtdcTraderApi *pTraderApi, int indicator);
	~CTradeSpi() ;

	public:
	virtual void OnFrontConnected();
	virtual void OnFrontDisconnected(int nReason);
	virtual void OnRspUserLogin(CThostFtdcRspUserLoginField *pRspUserLogin, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast);
	
	// ��ѯ�ʽ�
	virtual int ReqQryTradingAccount(char *brokerID, char *userID, int nRequestID);
	virtual void OnRspQryTradingAccount(CThostFtdcTradingAccountField *pTradingAccount, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast);

	// ��ѯ�ֲ�
	virtual int ReqQryInvestorPosition(char *brokerID, char *userID, int nRequestID);
	virtual void OnRspQryInvestorPosition(CThostFtdcInvestorPositionField *pInvestorPosition, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast);

	///����֪ͨ
	virtual int ReqQryOrder(char *brokerID, char *userID, int nRequestID);
	virtual void OnRspQryOrder(CThostFtdcOrderField *pOrder, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast);

	///�ɽ�֪ͨ
	virtual int ReqQryTrade(char *brokerID, char *userID, int nRequestID);
	virtual void OnRtnTrade(CThostFtdcTradeField *pTrade);

	///��ѯ��Լ
	virtual int ReqQryInstrument(char *brokerID, char *userID, int nRequestID);
	virtual void OnRspQryInstrument(CThostFtdcInstrumentField *pInstrument, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast);

	// ��������
	virtual int ReqOrderInsert(vector<string> v, int nRequestID);
	virtual void OnRspOrderInsert(CThostFtdcInputOrderField *pInputOrder, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast);
	virtual void OnRtnOrder(CThostFtdcOrderField *pOrder);


	private:
	CThostFtdcTraderApi *m_pTradeApi;
	int m_indicator;
};

class CMdSpi : public CThostFtdcMdSpi
{
public:
	CMdSpi(CThostFtdcMdApi *pMdApi);
	~CMdSpi();

public:
	virtual void OnFrontConnected();
	virtual void OnRspUserLogin(CThostFtdcRspUserLoginField *pRspUserLogin, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast);
	///�������֪ͨ
	virtual void OnRtnDepthMarketData(CThostFtdcDepthMarketDataField *pDepthMarketData);

private:
	CThostFtdcMdApi *m_pMdApi;
};