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

// ���׽ӿڼ��ص��ӿ�
CThostFtdcMdApi *pMdApi = NULL;
CMdSpi* pMdSpi = NULL;

// ���׺�
int nRequestID;

// ������DLL�е������Ϣ�������������������õ�
// ������
TThostFtdcDateType TradingDay;
// ���͹�˾����
TThostFtdcBrokerIDType	BrokerID;
// �û�����
TThostFtdcUserIDType	UserID;
// ����
TThostFtdcPasswordType	Password;

// ��ʼ�����׽ӿ�
int TradeInitAPI(char *server_addr, CallbackDelegate callback)
{
	// ��ֹ�ظ���ʼ��
	if (pMdApi != NULL)
		return -1;

	// �ص�����
	tradeCallback = callback;

	// ��׼����
	pMdApi = CThostFtdcMdApi::CreateFtdcMdApi("",false,false);
	pMdSpi  = new CMdSpi(pMdApi);

	pMdApi->RegisterSpi(pMdSpi);
	pMdApi->RegisterFront(server_addr);
	pMdApi->Init();
	// pTradeApi->Join();

	strcpy_s(TradingDay, sizeof(TThostFtdcDateType), pMdApi ->GetTradingDay());

	return 0;
}

// �ص�����������TradeSpi.cpp�еĺ���������ֵ��ͨ���˴�����C#
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
// ����C#���͵������ַ�����ʽ����������ַ�����ͬ�������˺�������������ʵ�ֵĽӿ����ݶ����������
// �����������readme.txt
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
			 // ���ؼ�¼��ز�����������ʹ��
			 strcpy_s(BrokerID, sizeof(TThostFtdcBrokerIDType), v[3].c_str());
			 strcpy_s(UserID, sizeof(TThostFtdcUserIDType), v[4].c_str());
			 strcpy_s(Password, sizeof(TThostFtdcPasswordType), v[5].c_str());

			 // ��д��¼�ṹ����
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
			 
			 // ��д�ǳ��ṹ����
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