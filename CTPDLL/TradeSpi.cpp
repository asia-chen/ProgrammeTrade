#include "stdafx.h"
#include <iostream>
#include <string>
using namespace std;

#include "CTPDLL.h"
#include "TradeSpi.h"

// Ŀǰ��ʵ����OnFrontConnected��OnRspUserLogin������Ҫʵ�������Ľӿڣ���Ҫ����TradeSpi.h��������Ӧ�ĺ�����
// Ȼ���ڴ˴����Լ��Ĵ��뽫���ݽ���ת�������ͨ��TradeResponse���ظ�C#����C#����

// ���켰����������������Լ��Ĵ���
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
// ���ͻ����뽻�׺�̨������ͨ������ʱ����δ��¼ǰ�����÷��������á�
void CMdSpi::OnFrontConnected()
{
	TradeResponse("-1><Md><OnFrontConnected><0><FrontConnected><0", -1, 1);
};

//�Ͽ�����
void CMdSpi::OnFrontDisconnected(int r)
{
	TradeResponse("-1><Md><OnFrontDisconnected><0><FrontDisconnected><0", -1, 1);
};
// ��¼������������ش������ݣ����򷵻ص�¼ʱ��
// ��������Ӧ���Ƿ��أ� nRequestID><sys><login><message.....       ��Ϊ�򻯰�
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
		//������
		result = result + splitstr + (string)pDepthMarketData->TradingDay;
		///��Լ����
		result = result + splitstr + (string)pDepthMarketData->InstrumentID;
		///����������
		result = result + splitstr + (string)pDepthMarketData->ExchangeID;
		///��Լ�ڽ������Ĵ���
		result = result + splitstr + (string)pDepthMarketData->ExchangeInstID;
		///���¼�
		result = result + splitstr + dtos(pDepthMarketData->LastPrice);
		///�ϴν����
		result = result + splitstr + dtos(pDepthMarketData->PreSettlementPrice);
		///������
		result = result + splitstr + dtos(pDepthMarketData->PreClosePrice);
		///��ֲ���
		result = result + splitstr + dtos(pDepthMarketData->PreOpenInterest);
		///����
		result = result + splitstr + dtos(pDepthMarketData->OpenPrice);
		///��߼�
		result = result + splitstr + dtos(pDepthMarketData->HighestPrice);
		///��ͼ�
		result = result + splitstr + dtos(pDepthMarketData->LowestPrice);
		///����
		result = result + splitstr + ltos(pDepthMarketData->Volume);
		///�ɽ����
		result = result + splitstr + dtos(pDepthMarketData->Turnover);
		///�ֲ���
		result = result + splitstr + dtos(pDepthMarketData->OpenInterest);
		///������
		result = result + splitstr + dtos(pDepthMarketData->ClosePrice);
		///���ν����
		result = result + splitstr + dtos(pDepthMarketData->SettlementPrice);
		///��ͣ���
		result = result + splitstr + dtos(pDepthMarketData->UpperLimitPrice);
		///��ͣ���
		result = result + splitstr + dtos(pDepthMarketData->LowerLimitPrice);
		///����ʵ��
		result = result + splitstr + dtos(pDepthMarketData->PreDelta);
		///����ʵ��
		result = result + splitstr + dtos(pDepthMarketData->CurrDelta);
		///����޸�ʱ��
		result = result + splitstr + (string)pDepthMarketData->UpdateTime;
		///����޸ĺ���
		result = result + splitstr + ltos(pDepthMarketData->UpdateMillisec);
		///�����һ
		result = result + splitstr + dtos(pDepthMarketData->BidPrice1);
		///������һ
		result = result + splitstr + ltos(pDepthMarketData->BidVolume1);
		///������һ
		result = result + splitstr + dtos(pDepthMarketData->AskPrice1);
		///������һ
		result = result + splitstr + ltos(pDepthMarketData->AskVolume1);
		///����۶�
		result = result + splitstr + dtos(pDepthMarketData->BidPrice2);
		///��������
		result = result + splitstr + ltos(pDepthMarketData->BidVolume2);
		///�����۶�
		result = result + splitstr + dtos(pDepthMarketData->AskPrice2);
		///��������
		result = result + splitstr + ltos(pDepthMarketData->AskVolume2);
		///�������
		result = result + splitstr + dtos(pDepthMarketData->BidPrice3);
		///��������
		result = result + splitstr + ltos(pDepthMarketData->BidVolume3);
		///��������
		result = result + splitstr + dtos(pDepthMarketData->AskPrice3);
		///��������
		result = result + splitstr + ltos(pDepthMarketData->AskVolume3);
		///�������
		result = result + splitstr + dtos(pDepthMarketData->BidPrice4);
		///��������
		result = result + splitstr + ltos(pDepthMarketData->BidVolume4);
		///��������
		result = result + splitstr + dtos(pDepthMarketData->AskPrice4);
		///��������
		result = result + splitstr + ltos(pDepthMarketData->AskVolume4);
		///�������
		result = result + splitstr + dtos(pDepthMarketData->BidPrice5);
		///��������
		result = result + splitstr + ltos(pDepthMarketData->BidVolume5);
		///��������
		result = result + splitstr + dtos(pDepthMarketData->AskPrice5);
		///��������
		result = result + splitstr + ltos(pDepthMarketData->AskVolume5);
		///���վ���
		result = result + splitstr + dtos(pDepthMarketData->AveragePrice);
		///ҵ������
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

