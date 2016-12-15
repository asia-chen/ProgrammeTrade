#include "stdafx.h"
#include <iostream>
#include <string>
using namespace std;

#include "CTPDLL.h"
#include "TradeSpi.h"


// ���׺�
extern char *splitstr;


// ������DLL�е������Ϣ�������������������õ�
// ������
extern TThostFtdcDateType TradingDay;

// Ŀǰ��ʵ����OnFrontConnected��OnRspUserLogin������Ҫʵ�������Ľӿڣ���Ҫ����TradeSpi.h��������Ӧ�ĺ�����
// Ȼ���ڴ˴����Լ��Ĵ��뽫���ݽ���ת�������ͨ��TradeResponse���ظ�C#����C#����

// ���켰����������������Լ��Ĵ���
CTradeSpi::CTradeSpi(CThostFtdcTraderApi *pTradeApi, int indicator)
{
	m_pTradeApi = pTradeApi;
	m_indicator = indicator;
}

//����
CMdSpi::CMdSpi(CThostFtdcMdApi *pMApi)
{
	m_pMdApi = pMApi;
}
CMdSpi::~CMdSpi() 
{
}

bool IsErrorRspInfo(CThostFtdcRspInfoField *pRspInfo)
{
	if(pRspInfo && pRspInfo->ErrorID != 0)
	{
		return true;
	}
	return false;
}

// --------------------------------------------------------------------------------------
// ���ͻ����뽻�׺�̨������ͨ������ʱ����δ��¼ǰ�����÷��������á�
void CTradeSpi::OnFrontConnected()
{
	TradeResponse("-1><sys><OnFrontConnected><0><FrontConnected><0", -1, 1, m_indicator);
};
// ���ͻ����뽻�׺�̨�Ͽ�����ʱ���÷��������á�
void CTradeSpi::OnFrontDisconnected( int nReason)
{
	TradeResponse("-1><sys><OnFrontDisconnected><0><FrontDisconnected><0", -1, 1, m_indicator);
};

// ��¼������������ش������ݣ����򷵻ص�¼ʱ��
// ��������Ӧ���Ƿ��أ� nRequestID><sys><login><message.....       ��Ϊ�򻯰�
void CTradeSpi::OnRspUserLogin(CThostFtdcRspUserLoginField *pRspUserLogin, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	string result = "";
	if(IsErrorRspInfo(pRspInfo))
	{
		result = result + ltos(pRspInfo->ErrorID) + splitstr + (string)pRspInfo->ErrorMsg;
		TradeResponse((char *)result.c_str(), nRequestID, -1, m_indicator);
		return ;
	}

	result = result + ltos(nRequestID);
	result = result + splitstr + "sys";
	result = result + splitstr + "login";

	int nIsLast = 0;
	if (bIsLast)
		nIsLast = 1;

	if (pRspInfo != NULL)
	{
		// fail 
		if (pRspInfo->ErrorID != 0)
		{
			result = result + splitstr + ltos(pRspInfo->ErrorID);
			result = result + splitstr + (string)pRspInfo->ErrorMsg;
			result = result + splitstr + "0";
		}
		else
		{
			result = result + splitstr + "0";
			result = result + splitstr + "��¼�ɹ�";
			result = result + splitstr + "0";
		}
		TradeResponse((char *)result.c_str(), nRequestID, nIsLast, m_indicator);
	}
	return ;	
}

/// ����ȷ�Ͻ��㵥
int CTradeSpi::ReqSettlementInfoConfirm(vector<string> v, int nRequestID)
{
	CThostFtdcSettlementInfoConfirmField settlementInfoConfirm;
	memset(&settlementInfoConfirm, 0, sizeof(CThostFtdcSettlementInfoConfirmField));

	///���͹�˾����
	strcpy_s(settlementInfoConfirm.BrokerID, sizeof(TThostFtdcBrokerIDType), (char *)v[3].c_str());
	///Ͷ���ߴ���
	strcpy_s(settlementInfoConfirm.InvestorID, sizeof(TThostFtdcInvestorIDType), (char *)v[4].c_str());
	/// ȷ������
	strcpy_s(settlementInfoConfirm.ConfirmDate, sizeof(TThostFtdcDateType), (char *)v[6].c_str());
	/// ȷ��ʱ��
	strcpy_s(settlementInfoConfirm.ConfirmTime, sizeof(TThostFtdcTimeType), (char *)v[7].c_str());

	int result = m_pTradeApi->ReqSettlementInfoConfirm(&settlementInfoConfirm, nRequestID);
	return result;
}
void CTradeSpi::OnRspSettlementInfoConfirm(CThostFtdcSettlementInfoConfirmField *pSettlementInfoConfirm, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	string result = "";
	return ;
}

int CTradeSpi::ReqQryTradingAccount(char *brokerID, char *userID, int nRequestID)
{
	CThostFtdcQryTradingAccountField qryTradingAccount;
	memset(&qryTradingAccount, 0, sizeof(CThostFtdcQryTradingAccountField));

	///���͹�˾����
	strcpy_s(qryTradingAccount.BrokerID, sizeof(TThostFtdcBrokerIDType),brokerID);	;
	///Ͷ���ߴ���
	strcpy_s(qryTradingAccount.InvestorID, sizeof(TThostFtdcInvestorIDType), userID); // ?? ��ȷ�����ֵ
	///���ִ���
	strcpy_s(qryTradingAccount.CurrencyID, sizeof(TThostFtdcCurrencyIDType), "CNY"); //  ?? ��ȷ�����ֵ

	return m_pTradeApi->ReqQryTradingAccount(&qryTradingAccount, nRequestID);
}

void CTradeSpi::OnRspQryTradingAccount(CThostFtdcTradingAccountField *pTradingAccount, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	string result = "";
	if(IsErrorRspInfo(pRspInfo))
	{
		result = result + ltos(pRspInfo->ErrorID) + splitstr + (string)pRspInfo->ErrorMsg;
		TradeResponse((char *)result.c_str(), nRequestID, -1, m_indicator);
		return ;
	}
	if(!pTradingAccount)
	{
		return;
	}

	///���͹�˾����
	result = result + (string)pTradingAccount->BrokerID;
	///Ͷ�����ʺ�
	result = result + splitstr + (string)pTradingAccount->AccountID;
	///�ϴ���Ѻ���
	result = result + splitstr + dtos(pTradingAccount->PreMortgage);
	///�ϴ����ö��
	result = result + splitstr + dtos(pTradingAccount->PreCredit);
	///�ϴδ���
	result = result + splitstr + dtos(pTradingAccount->PreDeposit);
	///�ϴν���׼����
	result = result + splitstr + dtos(pTradingAccount->PreBalance);
	///�ϴ�ռ�õı�֤��
	result = result + splitstr + dtos(pTradingAccount->PreMargin);
	///��Ϣ����
	result = result + splitstr + dtos(pTradingAccount->InterestBase);
	///��Ϣ����
	result = result + splitstr + dtos(pTradingAccount->Interest);
	///�����
	result = result + splitstr + dtos(pTradingAccount->Deposit);
	///������
	result = result + splitstr + dtos(pTradingAccount->Withdraw);
	///����ı�֤��
	result = result + splitstr + dtos(pTradingAccount->FrozenMargin);
	///������ʽ�
	result = result + splitstr + dtos(pTradingAccount->FrozenCash);
	///�����������
	result = result + splitstr + dtos(pTradingAccount->FrozenCommission);
	///��ǰ��֤���ܶ�
	result = result + splitstr + dtos(pTradingAccount->CurrMargin);
	///�ʽ���
	result = result + splitstr + dtos(pTradingAccount->CashIn);
	///������
	result = result + splitstr + dtos(pTradingAccount->Commission);
	///ƽ��ӯ��
	result = result + splitstr + dtos(pTradingAccount->CloseProfit);
	///�ֲ�ӯ��
	result = result + splitstr + dtos(pTradingAccount->PositionProfit);
	///�ڻ�����׼����
	result = result + splitstr + dtos(pTradingAccount->Balance);
	///�����ʽ�
	result = result + splitstr + dtos(pTradingAccount->Available);
	///��ȡ�ʽ�
	result = result + splitstr + dtos(pTradingAccount->WithdrawQuota);
	///����׼����
	result = result + splitstr + dtos(pTradingAccount->Reserve);
	///������
	result = result + splitstr + (string)pTradingAccount->TradingDay;
	///������
	result = result + splitstr + ltos(pTradingAccount->SettlementID);
	///���ö��
	result = result + splitstr + dtos(pTradingAccount->Credit);
	///��Ѻ���
	result = result + splitstr + dtos(pTradingAccount->Mortgage);
	///��������֤��
	result = result + splitstr + dtos(pTradingAccount->ExchangeMargin);
	///Ͷ���߽��֤��
	result = result + splitstr + dtos(pTradingAccount->DeliveryMargin);
	///���������֤��
	result = result + splitstr + dtos(pTradingAccount->ExchangeDeliveryMargin);
	///�����ڻ�����׼����
	result = result + splitstr + dtos(pTradingAccount->ReserveBalance);
	///���ִ���
	result = result + splitstr + (string)pTradingAccount->CurrencyID;
	///�ϴλ���������
	result = result + splitstr + dtos(pTradingAccount->PreFundMortgageIn);
	///�ϴλ����ʳ����
	result = result + splitstr + dtos(pTradingAccount->PreFundMortgageOut);
	///����������
	result = result + splitstr + dtos(pTradingAccount->FundMortgageIn);
	///�����ʳ����
	result = result + splitstr + dtos(pTradingAccount->FundMortgageOut);
	///������Ѻ���
	result = result + splitstr + dtos(pTradingAccount->FundMortgageAvailable);
	///����Ѻ���ҽ��
	result = result + splitstr + dtos(pTradingAccount->MortgageableFund);
	///�����Ʒռ�ñ�֤��
	result = result + splitstr + dtos(pTradingAccount->SpecProductMargin);
	///�����Ʒ���ᱣ֤��
	result = result + splitstr + dtos(pTradingAccount->SpecProductFrozenMargin);
	///�����Ʒ������
	result = result + splitstr + dtos(pTradingAccount->SpecProductCommission);
	///�����Ʒ����������
	result = result + splitstr + dtos(pTradingAccount->SpecProductFrozenCommission);
	///�����Ʒ�ֲ�ӯ��
	result = result + splitstr + dtos(pTradingAccount->SpecProductPositionProfit);
	///�����Ʒƽ��ӯ��
	result = result + splitstr + dtos(pTradingAccount->SpecProductCloseProfit);
	///���ݳֲ�ӯ���㷨����������Ʒ�ֲ�ӯ��
	result = result + splitstr + dtos(pTradingAccount->SpecProductPositionProfitByAlg);
	///�����Ʒ��������֤��
	result = result + splitstr + dtos(pTradingAccount->SpecProductExchangeMargin);

	if (bIsLast)
	{
		TradeResponse((char *)result.c_str(), nRequestID, 1, m_indicator);
	}
	else
	{
		TradeResponse((char *)result.c_str(), nRequestID, 0, m_indicator);
	}
}


// ��ѯ�ֲ�
int CTradeSpi::ReqQryInvestorPosition(char *brokerID, char *userID, int nRequestID)
{
	CThostFtdcQryInvestorPositionField qryInvestorPosition;
	memset(&qryInvestorPosition, 0, sizeof(CThostFtdcQryInvestorPositionField));

	///���͹�˾����
	strcpy_s(qryInvestorPosition.BrokerID, sizeof(TThostFtdcBrokerIDType),brokerID);	;
	///Ͷ���ߴ���
	strcpy_s(qryInvestorPosition.InvestorID, sizeof(TThostFtdcInvestorIDType), userID); // ?? ��ȷ�����ֵ
	///���ִ���
	strcpy_s(qryInvestorPosition.InstrumentID, sizeof(TThostFtdcInstrumentIDType), ""); //  ��ֵ�����ѯȫ����ͬ��

	return m_pTradeApi->ReqQryInvestorPosition(&qryInvestorPosition, nRequestID);
}

void CTradeSpi::OnRspQryInvestorPosition(CThostFtdcInvestorPositionField *pInvestorPosition, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	string result = "";
	if(IsErrorRspInfo(pRspInfo))
	{
		result = result + ltos(pRspInfo->ErrorID) + splitstr + (string)pRspInfo->ErrorMsg;
		TradeResponse((char *)result.c_str(), nRequestID, -1, m_indicator);
		return ;
	}
	if(!pInvestorPosition)
	{
		// �޳ֲ����
		TradeResponse("", nRequestID, 1, m_indicator);
		return;
	}

	///��Լ����
	result = result + (string)pInvestorPosition->InstrumentID;
	///���͹�˾����
	result = result + splitstr + (string)pInvestorPosition->BrokerID;
	///Ͷ���ߴ���
	result = result + splitstr + (string)pInvestorPosition->InvestorID;
	///�ֲֶ�շ���
	result = result + splitstr + ctos(pInvestorPosition->PosiDirection);
	///Ͷ���ױ���־
	result = result + splitstr + ctos(pInvestorPosition->HedgeFlag);
	///�ֲ�����
	result = result + splitstr + ctos(pInvestorPosition->PositionDate);
	///���ճֲ�
	result = result + splitstr + ltos(pInvestorPosition->YdPosition);
	///���ճֲ�
	result = result + splitstr + ltos(pInvestorPosition->Position);
	///��ͷ����
	result = result + splitstr + ltos(pInvestorPosition->LongFrozen);
	///��ͷ����
	result = result + splitstr + ltos(pInvestorPosition->ShortFrozen);
	///���ֶ�����
	result = result + splitstr + dtos(pInvestorPosition->LongFrozenAmount);
	///���ֶ�����
	result = result + splitstr + dtos(pInvestorPosition->ShortFrozenAmount);
	///������
	result = result + splitstr + ltos(pInvestorPosition->OpenVolume);
	///ƽ����
	result = result + splitstr + ltos(pInvestorPosition->CloseVolume);
	///���ֽ��
	result = result + splitstr + dtos(pInvestorPosition->OpenAmount);
	///ƽ�ֽ��
	result = result + splitstr + dtos(pInvestorPosition->CloseAmount);
	///�ֲֳɱ�
	result = result + splitstr + dtos(pInvestorPosition->PositionCost);
	///�ϴ�ռ�õı�֤��
	result = result + splitstr + dtos(pInvestorPosition->PreMargin);
	///ռ�õı�֤��
	result = result + splitstr + dtos(pInvestorPosition->UseMargin);
	///����ı�֤��
	result = result + splitstr + dtos(pInvestorPosition->FrozenMargin);
	///������ʽ�
	result = result + splitstr + dtos(pInvestorPosition->FrozenCash);
	///�����������
	result = result + splitstr + dtos(pInvestorPosition->FrozenCommission);
	///�ʽ���
	result = result + splitstr + dtos(pInvestorPosition->CashIn);
	///������
	result = result + splitstr + dtos(pInvestorPosition->Commission);
	///ƽ��ӯ��
	result = result + splitstr + dtos(pInvestorPosition->CloseProfit);
	///�ֲ�ӯ��
	result = result + splitstr + dtos(pInvestorPosition->PositionProfit);
	///�ϴν����
	result = result + splitstr + dtos(pInvestorPosition->PreSettlementPrice);
	///���ν����
	result = result + splitstr + dtos(pInvestorPosition->SettlementPrice);
	///������
	result = result + splitstr + (string)pInvestorPosition->TradingDay;
	///������
	result = result + splitstr + ltos(pInvestorPosition->SettlementID);
	///���ֳɱ�
	result = result + splitstr + dtos(pInvestorPosition->OpenCost);
	///��������֤��
	result = result + splitstr + dtos(pInvestorPosition->ExchangeMargin);
	///��ϳɽ��γɵĳֲ�
	result = result + splitstr + ltos(pInvestorPosition->CombPosition);
	///��϶�ͷ����
	result = result + splitstr + ltos(pInvestorPosition->CombLongFrozen);
	///��Ͽ�ͷ����
	result = result + splitstr + ltos(pInvestorPosition->CombShortFrozen);
	///���ն���ƽ��ӯ��
	result = result + splitstr + dtos(pInvestorPosition->CloseProfitByDate);
	///��ʶԳ�ƽ��ӯ��
	result = result + splitstr + dtos(pInvestorPosition->CloseProfitByTrade);
	///���ճֲ�
	result = result + splitstr + ltos(pInvestorPosition->TodayPosition);
	///��֤����
	result = result + splitstr + dtos(pInvestorPosition->MarginRateByMoney);
	///��֤����(������)
	result = result + splitstr + dtos(pInvestorPosition->MarginRateByVolume);
	///ִ�ж���
	result = result + splitstr + ltos(pInvestorPosition->StrikeFrozen);
	///ִ�ж�����
	result = result + splitstr + dtos(pInvestorPosition->StrikeFrozenAmount);
	///����ִ�ж���
	result = result + splitstr + ltos(pInvestorPosition->AbandonFrozen);

	if (bIsLast)
	{
		TradeResponse((char *)result.c_str(), nRequestID, 1, m_indicator);
	}
	else
	{
		TradeResponse((char *)result.c_str(), nRequestID, 0, m_indicator);
	}
}


// ��ѯ����
int CTradeSpi::ReqQryOrder(char *brokerID, char *userID, int nRequestID)
{
	CThostFtdcQryOrderField qryOrder;
	memset(&qryOrder, 0, sizeof(CThostFtdcQryOrderField));

	///���͹�˾����
	strcpy_s(qryOrder.BrokerID, sizeof(TThostFtdcBrokerIDType), brokerID);
	///Ͷ���ߴ���
	strcpy_s(qryOrder.InvestorID, sizeof(TThostFtdcInvestorIDType), userID); 
	/*///��Լ����
	strcpy_s(qryOrder.InstrumentID, sizeof(TThostFtdcInstrumentIDType), "");
	///����������
	strcpy_s(qryOrder.ExchangeID, sizeof(TThostFtdcExchangeIDType), "");
	///��������
	strcpy_s(qryOrder.OrderSysID, sizeof(TThostFtdcOrderSysIDType), "");*/

	return m_pTradeApi->ReqQryOrder(&qryOrder, nRequestID);
}
void CTradeSpi::OnRspQryOrder(CThostFtdcOrderField *pOrder, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{

}

// ��ѯ�ɽ�
int CTradeSpi::ReqQryTrade(char *brokerID, char *userID, int nRequestID)
{
	CThostFtdcQryTradeField qryTrade;
	memset(&qryTrade, 0, sizeof(CThostFtdcQryTradeField));

	///���͹�˾����
	strcpy_s(qryTrade.BrokerID, sizeof(TThostFtdcBrokerIDType), brokerID);
	///Ͷ���ߴ���
	strcpy_s(qryTrade.InvestorID, sizeof(TThostFtdcInvestorIDType), userID); 
	///��Լ����
	strcpy_s(qryTrade.InstrumentID, sizeof(TThostFtdcInstrumentIDType), "");
	///����������
	strcpy_s(qryTrade.ExchangeID, sizeof(TThostFtdcExchangeIDType), "");
	///��������
	strcpy_s(qryTrade.TradeID, sizeof(TThostFtdcTradeIDType), "");

	return m_pTradeApi->ReqQryTrade(&qryTrade, nRequestID);
}

void CTradeSpi::OnRtnTrade(CThostFtdcTradeField *pTrade)
{
	string result = "";
	if(!pTrade)
	{
		return;
	}

	result = result + "-1" + splitstr + "sys" + splitstr + "OnRtnTrade" + splitstr + "0" + splitstr + "" + splitstr + "1";
	///���͹�˾����
	result = result + splitstr + (string)pTrade->BrokerID;
	///Ͷ���ߴ���
	result = result + splitstr + (string)pTrade->InvestorID;
	///��Լ����
	result = result + splitstr + (string)pTrade->InstrumentID;
	///��������
	result = result + splitstr + (string)pTrade->OrderRef;
	///�û�����
	result = result + splitstr + (string)pTrade->UserID;
	///����������
	result = result + splitstr + (string)pTrade->ExchangeID;
	///�ɽ����
	result = result + splitstr + (string)pTrade->TradeID;
	///��������
	result = result + splitstr + ctos(pTrade->Direction);
	///�������
	result = result + splitstr + (string)pTrade->OrderSysID;
	///��Ա����
	result = result + splitstr + (string)pTrade->ParticipantID;
	///�ͻ�����
	result = result + splitstr + (string)pTrade->ClientID;
	///���׽�ɫ
	result = result + splitstr + ctos(pTrade->TradingRole);
	///��Լ�ڽ������Ĵ���
	result = result + splitstr + (string)pTrade->ExchangeInstID;
	///��ƽ��־
	result = result + splitstr + ctos(pTrade->OffsetFlag);
	///Ͷ���ױ���־
	result = result + splitstr + ctos(pTrade->HedgeFlag);
	///�۸�
	result = result + splitstr + dtos(pTrade->Price);
	///����
	result = result + splitstr + ltos(pTrade->Volume);
	///�ɽ�ʱ��
	result = result + splitstr + (string)pTrade->TradeDate;
	///�ɽ�ʱ��
	result = result + splitstr + (string)pTrade->TradeTime;
	///�ɽ�����
	result = result + splitstr + ctos(pTrade->TradeType);
	///�ɽ�����Դ
	result = result + splitstr + ctos(pTrade->PriceSource);
	///����������Ա����
	result = result + splitstr + (string)pTrade->TraderID;
	///���ر������
	result = result + splitstr + (string)pTrade->OrderLocalID;
	///�����Ա���
	result = result + splitstr + (string)pTrade->ClearingPartID;
	///ҵ��Ԫ
	result = result + splitstr + (string)pTrade->BusinessUnit;
	///���
	result = result + splitstr + ltos(pTrade->SequenceNo);
	///������
	result = result + splitstr + (string)pTrade->TradingDay;
	///������
	result = result + splitstr + ltos(pTrade->SettlementID);
	///���͹�˾�������
	result = result + splitstr + ltos(pTrade->BrokerOrderSeq);
	///�ɽ���Դ
	result = result + splitstr + ctos(pTrade->TradeSource);

	TradeResponse((char *)result.c_str(), -1, 1, m_indicator);
}

// ��ѯ��ͬ
int CTradeSpi::ReqQryInstrument(char *brokerID, char *userID, int nRequestID)
{
	CThostFtdcQryInstrumentField qryInstrument;
	memset(&qryInstrument, 0, sizeof(CThostFtdcQryInstrumentField));

	//  ��ֵ�����ѯȫ����ͬ����ģ����ѯ
	strcpy_s(qryInstrument.InstrumentID, sizeof(TThostFtdcInstrumentIDType), ""); 

	return m_pTradeApi->ReqQryInstrument(&qryInstrument, nRequestID);	
}

void CTradeSpi::OnRspQryInstrument(CThostFtdcInstrumentField *pInstrument, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	string result = "";
	if(IsErrorRspInfo(pRspInfo))
	{
		result = result + ltos(pRspInfo->ErrorID) + splitstr + (string)pRspInfo->ErrorMsg;
		TradeResponse((char *)result.c_str(), nRequestID, -1, m_indicator);
		return ;
	}
	if(!pInstrument)
	{
		return;
	}

	///��Լ����
	result = result + (string)pInstrument->InstrumentID;
	///����������
	result = result + splitstr + (string)pInstrument->ExchangeID;
	///��Լ����
	result = result + splitstr + (string)pInstrument->InstrumentName;
	///��Լ�ڽ������Ĵ���
	result = result + splitstr + (string)pInstrument->ExchangeInstID;
	///��Ʒ����
	result = result + splitstr + (string)pInstrument->ProductID;
	///��Ʒ����
	result = result + splitstr + ctos(pInstrument->ProductClass);
	///�������
	result = result + splitstr + ltos(pInstrument->DeliveryYear);
	///������
	result = result + splitstr + ltos(pInstrument->DeliveryMonth);
	///�м۵�����µ���
	result = result + splitstr + dtos(pInstrument->MaxMarketOrderVolume);
	///�м۵���С�µ���
	result = result + splitstr + dtos(pInstrument->MinMarketOrderVolume);
	///�޼۵�����µ���
	result = result + splitstr + dtos(pInstrument->MaxLimitOrderVolume);
	///�޼۵���С�µ���
	result = result + splitstr + dtos(pInstrument->MinLimitOrderVolume);
	///��Լ��������
	result = result + splitstr + dtos(pInstrument->VolumeMultiple);
	///��С�䶯��λ
	result = result + splitstr + dtos(pInstrument->PriceTick);
	///������
	result = result + splitstr + (string)pInstrument->CreateDate;
	///������
	result = result + splitstr + (string)pInstrument->OpenDate;
	///������
	result = result + splitstr + (string)pInstrument->ExpireDate;
	///��ʼ������
	result = result + splitstr + (string)pInstrument->StartDelivDate;
	///����������
	result = result + splitstr + (string)pInstrument->EndDelivDate;
	///��Լ��������״̬
	result = result + splitstr + ctos(pInstrument->InstLifePhase);
	///��ǰ�Ƿ���
	result = result + splitstr + ltos(pInstrument->IsTrading);
	///�ֲ�����
	result = result + splitstr + ctos(pInstrument->PositionType);
	///�ֲ���������
	result = result + splitstr + ctos(pInstrument->PositionDateType);
	///��ͷ��֤����
	result = result + splitstr + dtos(pInstrument->LongMarginRatio);
	///��ͷ��֤����
	result = result + splitstr + dtos(pInstrument->ShortMarginRatio);
	///�Ƿ�ʹ�ô��߱�֤���㷨
	result = result + splitstr + ctos(pInstrument->MaxMarginSideAlgorithm);
	///������Ʒ����
	result = result + splitstr + (string)pInstrument->UnderlyingInstrID;
	///ִ�м�(���ֹ����������)
	result = result + splitstr + dtos(pInstrument->StrikePrice);
	///��Ȩ����
	result = result + splitstr + ctos(pInstrument->OptionsType);
	///��Լ������Ʒ����
	result = result + splitstr + dtos(pInstrument->UnderlyingMultiple);
	///�������
	result = result + splitstr + ctos(pInstrument->CombinationType);

	if (bIsLast)
	{
		TradeResponse((char *)result.c_str(), nRequestID, 1, m_indicator);
	}
	else
	{
		TradeResponse((char *)result.c_str(), nRequestID, 0, m_indicator);
	}
}

// ���󱨵�
int CTradeSpi::ReqOrderInsert(vector<string> v, int nRequestID)
{
	CThostFtdcInputOrderField inputOrder;
	memset(&inputOrder, 0, sizeof(CThostFtdcInputOrderField));

	///���͹�˾����
	strcpy_s(inputOrder.BrokerID, sizeof(TThostFtdcBrokerIDType), (char *)v[3].c_str());
	///Ͷ���ߴ���
	strcpy_s(inputOrder.InvestorID, sizeof(TThostFtdcInvestorIDType), (char *)v[4].c_str());
	///��Լ����
	strcpy_s(inputOrder.InstrumentID, sizeof(TThostFtdcInstrumentIDType), (char *)v[6].c_str());
	///�������ã���¼localID
	strcpy_s(inputOrder.OrderRef, sizeof(TThostFtdcOrderRefType), (char *)v[11].c_str());
	///�û����� ??
	strcpy_s(inputOrder.UserID, sizeof(TThostFtdcUserIDType), (char *)v[4].c_str());
	///�����۸�����
	inputOrder.OrderPriceType = THOST_FTDC_OPT_LimitPrice;
	///�������� 0 �� 1 �� THOST_FTDC_D_Buy
	inputOrder.Direction = ((char *)v[7].c_str())[0];
	///��Ͽ�ƽ��־ ??      ���� '0', ƽ�� '1', ǿƽ '2', ƽ�� '3', ƽ�� '4', ǿ�� '5', ����ǿƽ '6'
	inputOrder.CombOffsetFlag[0] = ((char *)v[8].c_str())[0];

	///���Ͷ���ױ���־ ?? �ױ� '1'             , ���� '2',Ͷ�� '3' 
	/// �ر�˵����
	///      �Ϻ���������������Ӧ���� Ͷ��
	///      ��������ƽ��ƽ�����������˳ֲֲ��㣬�ƺ������������ͨ�������������أ�
	///      �ױ����赥�����룬ģ�⻷�ڿ���δ���ƣ�ʵ�ʻ������ܱ���ֹ
	///      ע��ʹ��Q7�µĵ�����Ϊ�ױ���֣�ݲ�����ƽ��ƽ���Ϻ������֣�����֪���ķ��ش���
	/// ����ʱ��ʹ��Ͷ��ѡ�ƽ��ʱ����Σ�����֤
	inputOrder.CombHedgeFlag[0] = THOST_FTDC_ECIDT_Hedge;
	///�۸�
	inputOrder.LimitPrice = atof((char *)v[9].c_str());
	///����
	inputOrder.VolumeTotalOriginal = atoi((char *)v[10].c_str());
	///��Ч������
	inputOrder.TimeCondition = THOST_FTDC_TC_GFD;
	///GTD����
	strcpy_s(inputOrder.GTDDate, sizeof(TThostFtdcDateType), "");
	///�ɽ�������
	inputOrder.VolumeCondition = THOST_FTDC_VC_AV;
	///��С�ɽ��� ??
	inputOrder.MinVolume  = 0;
	///��������
	inputOrder.ContingentCondition = THOST_FTDC_CC_Immediately;
	///ֹ��� ??
	inputOrder.StopPrice  = 0;
	///ǿƽԭ��
	inputOrder.ForceCloseReason  = THOST_FTDC_FCC_NotForceClose;
	///�Զ������־ ?? 
	inputOrder.IsAutoSuspend  = 0;
	inputOrder.RequestID = nRequestID;

	return m_pTradeApi->ReqOrderInsert(&inputOrder, nRequestID);
}

void CTradeSpi::OnRtnOrder(CThostFtdcOrderField *pOrder)
{
	string result = "";
	if(!pOrder)
	{
		return;
	}

	result = result + "-1" + splitstr + "sys" + splitstr + "OnRtnOrder" + splitstr + "0" + splitstr + "" + splitstr + "1";
	///���͹�˾����
	result = result + splitstr + (string)pOrder->BrokerID;
	///Ͷ���ߴ���
	result = result + splitstr + (string)pOrder->InvestorID;
	///��Լ����
	result = result + splitstr + (string)pOrder->InstrumentID;
	///��������
	result = result + splitstr + (string)pOrder->OrderRef;
	///�û�����
	result = result + splitstr + (string)pOrder->UserID;
	///�����۸�����
	result = result + splitstr + ctos(pOrder->OrderPriceType);
	///��������
	result = result + splitstr + ctos(pOrder->Direction);
	///��Ͽ�ƽ��־
	result = result + splitstr + (string)pOrder->CombOffsetFlag;
	///���Ͷ���ױ���־
	result = result + splitstr + (string)pOrder->CombHedgeFlag;
	///�۸�
	result = result + splitstr + dtos(pOrder->LimitPrice);
	///����
	result = result + splitstr + ltos(pOrder->VolumeTotalOriginal);
	///��Ч������
	result = result + splitstr + ctos(pOrder->TimeCondition);
	///GTD����
	result = result + splitstr + (string)pOrder->GTDDate;
	///�ɽ�������
	result = result + splitstr + ctos(pOrder->VolumeCondition);
	///��С�ɽ���
	result = result + splitstr + ltos(pOrder->MinVolume);
	///��������
	result = result + splitstr + ctos(pOrder->ContingentCondition);
	///ֹ���
	result = result + splitstr + dtos(pOrder->StopPrice);
	///ǿƽԭ��
	result = result + splitstr + ctos(pOrder->ForceCloseReason);
	///�Զ������־
	result = result + splitstr + ltos(pOrder->IsAutoSuspend);
	///ҵ��Ԫ
	result = result + splitstr + (string)pOrder->BusinessUnit;
	///������
	result = result + splitstr + ltos(pOrder->RequestID);
	///���ر������
	result = result + splitstr + (string)pOrder->OrderLocalID;
	///����������
	result = result + splitstr + (string)pOrder->ExchangeID;
	///��Ա����
	result = result + splitstr + (string)pOrder->ParticipantID;
	///�ͻ�����
	result = result + splitstr + (string)pOrder->ClientID;
	///��Լ�ڽ������Ĵ���
	result = result + splitstr + (string)pOrder->ExchangeInstID;
	///����������Ա����
	result = result + splitstr + (string)pOrder->TraderID;
	///��װ���
	result = result + splitstr + ltos(pOrder->InstallID);
	///�����ύ״̬
	result = result + splitstr + ctos(pOrder->OrderSubmitStatus);
	///������ʾ���
	result = result + splitstr + ltos(pOrder->NotifySequence);
	///������
	result = result + splitstr + (string)pOrder->TradingDay;
	///������
	result = result + splitstr + ltos(pOrder->SettlementID);
	///�������
	result = result + splitstr + (string)pOrder->OrderSysID;
	///������Դ
	result = result + splitstr + ctos(pOrder->OrderSource);
	///����״̬
	result = result + splitstr + ctos(pOrder->OrderStatus);
	///��������
	result = result + splitstr + ctos(pOrder->OrderType);
	///��ɽ�����
	result = result + splitstr + ltos(pOrder->VolumeTraded);
	///ʣ������
	result = result + splitstr + ltos(pOrder->VolumeTotal);
	///��������
	result = result + splitstr + (string)pOrder->InsertDate;
	///ί��ʱ��
	result = result + splitstr + (string)pOrder->InsertTime;
	///����ʱ��
	result = result + splitstr + (string)pOrder->ActiveTime;
	///����ʱ��
	result = result + splitstr + (string)pOrder->SuspendTime;
	///����޸�ʱ��
	result = result + splitstr + (string)pOrder->UpdateTime;
	///����ʱ��
	result = result + splitstr + (string)pOrder->CancelTime;
	///����޸Ľ���������Ա����
	result = result + splitstr + (string)pOrder->ActiveTraderID;
	///�����Ա���
	result = result + splitstr + (string)pOrder->ClearingPartID;
	///���
	result = result + splitstr + ltos(pOrder->SequenceNo);
	///ǰ�ñ��
	result = result + splitstr + ltos(pOrder->FrontID);
	///�Ự���
	result = result + splitstr + ltos(pOrder->SessionID);
	///�û��˲�Ʒ��Ϣ
	result = result + splitstr + (string)pOrder->UserProductInfo;
	///״̬��Ϣ
	result = result + splitstr + (string)pOrder->StatusMsg;
	///�û�ǿ����־
	result = result + splitstr + ltos(pOrder->UserForceClose);
	///�����û�����
	result = result + splitstr + (string)pOrder->ActiveUserID;
	///���͹�˾�������
	result = result + splitstr + ltos(pOrder->BrokerOrderSeq);
	///��ر���
	result = result + splitstr + (string)pOrder->RelativeOrderSysID;
	///֣�����ɽ�����
	result = result + splitstr + ltos(pOrder->ZCETotalTradedVolume);
	///��������־
	result = result + splitstr + ltos(pOrder->IsSwapOrder);


	TradeResponse((char *)result.c_str(), -1, 1, m_indicator);
}

void CTradeSpi::OnRspOrderInsert(CThostFtdcInputOrderField *pInputOrder, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	string result = "";

	if(IsErrorRspInfo(pRspInfo))
	{
		result = result + ltos(pRspInfo->ErrorID) + splitstr + (string)pRspInfo->ErrorMsg;
		TradeResponse((char *)result.c_str(), nRequestID, -1, m_indicator);
		return ;
	}
}

// ���󳷵�
int CTradeSpi::ReqOrderAction(vector<string> v, int nRequestID)
{
	CThostFtdcInputOrderActionField inputOrderAction;
	memset(&inputOrderAction, 0, sizeof(CThostFtdcInputOrderActionField));

	///���͹�˾����
	strcpy_s(inputOrderAction.BrokerID, sizeof(TThostFtdcBrokerIDType), (char *)v[3].c_str());
	///Ͷ���ߴ���
	strcpy_s(inputOrderAction.InvestorID, sizeof(TThostFtdcInvestorIDType), (char *)v[4].c_str());
	///������
	strcpy_s(inputOrderAction.OrderSysID, sizeof(TThostFtdcOrderSysIDType), (char *)v[6].c_str());
	///������־
	inputOrderAction.ActionFlag = THOST_FTDC_AF_Delete;
	///�û�����
	strcpy_s(inputOrderAction.UserID, sizeof(TThostFtdcUserIDType), (char *)v[4].c_str());
	///����������
	strcpy_s(inputOrderAction.ExchangeID, sizeof(TThostFtdcExchangeIDType), (char *)v[7].c_str());

	return m_pTradeApi->ReqOrderAction(&inputOrderAction, nRequestID);
}

// ���󳷵���Ӧ
void CTradeSpi::OnRspOrderAction(CThostFtdcInputOrderActionField *pInputOrderAction, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	string result = "";

	if(IsErrorRspInfo(pRspInfo))
	{
		result = result + ltos(pRspInfo->ErrorID) + splitstr + (string)pRspInfo->ErrorMsg;
		TradeResponse((char *)result.c_str(), nRequestID, -1, m_indicator);
		return ;
	}
}

// ��������
int CTradeSpi::ReqQryDepthMarketData(vector<string> v, int nRequestID)
{
	CThostFtdcQryDepthMarketDataField depthMarketData;
	memset(&depthMarketData, 0, sizeof(CThostFtdcQryDepthMarketDataField));

	strcpy_s(depthMarketData.InstrumentID, sizeof(TThostFtdcInstrumentIDType), (char *)v[6].c_str());
	return m_pTradeApi->ReqQryDepthMarketData(&depthMarketData, nRequestID);
}

void CTradeSpi::OnRspQryDepthMarketData(CThostFtdcDepthMarketDataField *pDepthMarketData, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	string result = "";

	if(IsErrorRspInfo(pRspInfo))
	{
		result = result + ltos(pRspInfo->ErrorID) + splitstr + (string)pRspInfo->ErrorMsg;
		TradeResponse((char *)result.c_str(), nRequestID, -1, m_indicator);
		return ;
	}
	if(!pDepthMarketData)
	{
		return;
	}

	//������
	result = result + (string)pDepthMarketData->TradingDay;
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

	if (bIsLast)
	{
		TradeResponse((char *)result.c_str(), nRequestID, 1, m_indicator);
	}
	else
	{
		TradeResponse((char *)result.c_str(), nRequestID, 0, m_indicator);
	}

	return ;
}

// --------------------------------------------------------------------------------------
//  ����
// --------------------------------------------------------------------------------------
// ��������
void CMdSpi::OnFrontConnected()
{
	MdResponse("-1><Md><OnFrontConnected><0><FrontConnected><0");
};

// ��¼
void CMdSpi::OnRspUserLogin(CThostFtdcRspUserLoginField *pRspUserLogin, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	string result = "";

	result = result + ltos(nRequestID);
	result = result + splitstr + "Md";
	result = result + splitstr + "login";

	// fail 
	if (pRspInfo != NULL)
	{
		if (pRspInfo->ErrorID != 0)
		{
			result = result + splitstr + ltos(pRspInfo->ErrorID);
			result = result + splitstr + (string)pRspInfo->ErrorMsg;
		}
		else
		{
			result = result + splitstr + "0";
			result = result + splitstr + "��¼�ɹ�";
		}
		result = result + splitstr + "1";
		result = result + splitstr + (string)pRspUserLogin->TradingDay;
		MdResponse((char *)result.c_str());
	}
	return;
}

// �ǳ�
void CMdSpi::OnRspUserLogout(CThostFtdcUserLogoutField *pUserLogout, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	string result = "";

	result = result + ltos(nRequestID);
	result = result + splitstr + "Md";
	result = result + splitstr + "logout";

	if (pRspInfo != NULL)
	{
		if (pRspInfo->ErrorID != 0)
		{
			result = result + splitstr + ltos(pRspInfo->ErrorID);
			result = result + splitstr + (string)pRspInfo->ErrorMsg;
		}
		else
		{
			result = result + splitstr + "0";
			result = result + splitstr + "�ǳ��ɹ�";
		}
	}
	MdResponse((char *)result.c_str());
	return;
}

// ��������
int CMdSpi::SubscribeMarketData(char* instrumentID)
{
	char *tmp[2];
	TThostFtdcInstrumentIDType tmpStr;

	strcpy_s(tmpStr, sizeof(TThostFtdcInstrumentIDType), instrumentID);
	tmp[0] = (char *)&tmpStr;
	tmp[1] = NULL;
	return m_pMdApi->SubscribeMarketData((char **)tmp, 1);
}

/// �������鷵�أ�Ŀǰ��֧�ֵ���ͬ���ģ�������bIisLast
void CMdSpi::OnRspSubMarketData(CThostFtdcSpecificInstrumentField *pSpecificInstrument, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	string result = "";

	result = result + ltos(nRequestID);
	result = result + splitstr + "Md";
	result = result + splitstr + "subscribe";

	if (pRspInfo != NULL)
	{
		if (pRspInfo->ErrorID != 0)
		{
			result = result + splitstr + ltos(pRspInfo->ErrorID);
			result = result + splitstr + (string)pRspInfo->ErrorMsg;
		}
		else
		{
			result = result + splitstr + "0";
			result = result + splitstr + "���ĳɹ�";
		}
	}
	result = result + splitstr + "1";
	result = result + splitstr + (string)pSpecificInstrument->InstrumentID;
	MdResponse((char *)result.c_str());
	return;
}

// �˶�����
int CMdSpi::UnSubscribeMarketData(char* instrumentID)
{
	char *tmp[2];
	TThostFtdcInstrumentIDType tmpStr;

	strcpy_s(tmpStr, sizeof(TThostFtdcInstrumentIDType), instrumentID);
	tmp[0] = (char *)&tmpStr;
	tmp[1] = NULL;
	return m_pMdApi->UnSubscribeMarketData((char **)tmp, 1);
}
void CMdSpi::OnRspUnSubMarketData(CThostFtdcSpecificInstrumentField *pSpecificInstrument, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	string result = "";

	result = result + ltos(nRequestID);
	result = result + splitstr + "Md";
	result = result + splitstr + "unsubscribe";

	if (pRspInfo != NULL)
	{
		if (pRspInfo->ErrorID != 0)
		{
			result = result + splitstr + ltos(pRspInfo->ErrorID);
			result = result + splitstr + (string)pRspInfo->ErrorMsg;
		}
		else
		{
			result = result + splitstr + "0";
			result = result + splitstr + "�˶��ɹ�";
		}
	}
	result = result + splitstr + "1";
	result = result + splitstr + (string)pSpecificInstrument->InstrumentID;
	MdResponse((char *)result.c_str());
	return;
}

//���鷵��
void CMdSpi::OnRtnDepthMarketData(CThostFtdcDepthMarketDataField *pDepthMarketData)
{
	string result = "-1";
	result = result + splitstr + "Md";
	result = result + splitstr + "marketdata";
	result = result + splitstr + "0" + splitstr + "0" + splitstr + "1";
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
