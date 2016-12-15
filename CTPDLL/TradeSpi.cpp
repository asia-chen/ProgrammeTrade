#include "stdafx.h"
#include <iostream>
#include <string>
using namespace std;

#include "CTPDLL.h"
#include "TradeSpi.h"


// 交易号
extern char *splitstr;


// 保留在DLL中的相关信息，后续其他函数可能用到
// 交易日
extern TThostFtdcDateType TradingDay;

// 目前仅实现了OnFrontConnected和OnRspUserLogin，若需要实现其他的接口，需要先在TradeSpi.h中增加相应的函数，
// 然后在此处以自己的代码将数据进行转换，最后通过TradeResponse返回给C#，供C#解析

// 构造及析构函数，可添加自己的处理
CTradeSpi::CTradeSpi(CThostFtdcTraderApi *pTradeApi, int indicator)
{
	m_pTradeApi = pTradeApi;
	m_indicator = indicator;
}

//行情
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
// 当客户端与交易后台建立起通信连接时（还未登录前），该方法被调用。
void CTradeSpi::OnFrontConnected()
{
	TradeResponse("-1><sys><OnFrontConnected><0><FrontConnected><0", -1, 1, m_indicator);
};
// 当客户端与交易后台断开连接时，该方法被调用。
void CTradeSpi::OnFrontDisconnected( int nReason)
{
	TradeResponse("-1><sys><OnFrontDisconnected><0><FrontDisconnected><0", -1, 1, m_indicator);
};

// 登录结果：若出错返回错误内容，否则返回登录时间
// 正常做法应该是返回： nRequestID><sys><login><message.....       此为简化版
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
			result = result + splitstr + "登录成功";
			result = result + splitstr + "0";
		}
		TradeResponse((char *)result.c_str(), nRequestID, nIsLast, m_indicator);
	}
	return ;	
}

/// 请求确认结算单
int CTradeSpi::ReqSettlementInfoConfirm(vector<string> v, int nRequestID)
{
	CThostFtdcSettlementInfoConfirmField settlementInfoConfirm;
	memset(&settlementInfoConfirm, 0, sizeof(CThostFtdcSettlementInfoConfirmField));

	///经纪公司代码
	strcpy_s(settlementInfoConfirm.BrokerID, sizeof(TThostFtdcBrokerIDType), (char *)v[3].c_str());
	///投资者代码
	strcpy_s(settlementInfoConfirm.InvestorID, sizeof(TThostFtdcInvestorIDType), (char *)v[4].c_str());
	/// 确认日期
	strcpy_s(settlementInfoConfirm.ConfirmDate, sizeof(TThostFtdcDateType), (char *)v[6].c_str());
	/// 确认时间
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

	///经纪公司代码
	strcpy_s(qryTradingAccount.BrokerID, sizeof(TThostFtdcBrokerIDType),brokerID);	;
	///投资者代码
	strcpy_s(qryTradingAccount.InvestorID, sizeof(TThostFtdcInvestorIDType), userID); // ?? 不确定这个值
	///币种代码
	strcpy_s(qryTradingAccount.CurrencyID, sizeof(TThostFtdcCurrencyIDType), "CNY"); //  ?? 不确定这个值

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

	///经纪公司代码
	result = result + (string)pTradingAccount->BrokerID;
	///投资者帐号
	result = result + splitstr + (string)pTradingAccount->AccountID;
	///上次质押金额
	result = result + splitstr + dtos(pTradingAccount->PreMortgage);
	///上次信用额度
	result = result + splitstr + dtos(pTradingAccount->PreCredit);
	///上次存款额
	result = result + splitstr + dtos(pTradingAccount->PreDeposit);
	///上次结算准备金
	result = result + splitstr + dtos(pTradingAccount->PreBalance);
	///上次占用的保证金
	result = result + splitstr + dtos(pTradingAccount->PreMargin);
	///利息基数
	result = result + splitstr + dtos(pTradingAccount->InterestBase);
	///利息收入
	result = result + splitstr + dtos(pTradingAccount->Interest);
	///入金金额
	result = result + splitstr + dtos(pTradingAccount->Deposit);
	///出金金额
	result = result + splitstr + dtos(pTradingAccount->Withdraw);
	///冻结的保证金
	result = result + splitstr + dtos(pTradingAccount->FrozenMargin);
	///冻结的资金
	result = result + splitstr + dtos(pTradingAccount->FrozenCash);
	///冻结的手续费
	result = result + splitstr + dtos(pTradingAccount->FrozenCommission);
	///当前保证金总额
	result = result + splitstr + dtos(pTradingAccount->CurrMargin);
	///资金差额
	result = result + splitstr + dtos(pTradingAccount->CashIn);
	///手续费
	result = result + splitstr + dtos(pTradingAccount->Commission);
	///平仓盈亏
	result = result + splitstr + dtos(pTradingAccount->CloseProfit);
	///持仓盈亏
	result = result + splitstr + dtos(pTradingAccount->PositionProfit);
	///期货结算准备金
	result = result + splitstr + dtos(pTradingAccount->Balance);
	///可用资金
	result = result + splitstr + dtos(pTradingAccount->Available);
	///可取资金
	result = result + splitstr + dtos(pTradingAccount->WithdrawQuota);
	///基本准备金
	result = result + splitstr + dtos(pTradingAccount->Reserve);
	///交易日
	result = result + splitstr + (string)pTradingAccount->TradingDay;
	///结算编号
	result = result + splitstr + ltos(pTradingAccount->SettlementID);
	///信用额度
	result = result + splitstr + dtos(pTradingAccount->Credit);
	///质押金额
	result = result + splitstr + dtos(pTradingAccount->Mortgage);
	///交易所保证金
	result = result + splitstr + dtos(pTradingAccount->ExchangeMargin);
	///投资者交割保证金
	result = result + splitstr + dtos(pTradingAccount->DeliveryMargin);
	///交易所交割保证金
	result = result + splitstr + dtos(pTradingAccount->ExchangeDeliveryMargin);
	///保底期货结算准备金
	result = result + splitstr + dtos(pTradingAccount->ReserveBalance);
	///币种代码
	result = result + splitstr + (string)pTradingAccount->CurrencyID;
	///上次货币质入金额
	result = result + splitstr + dtos(pTradingAccount->PreFundMortgageIn);
	///上次货币质出金额
	result = result + splitstr + dtos(pTradingAccount->PreFundMortgageOut);
	///货币质入金额
	result = result + splitstr + dtos(pTradingAccount->FundMortgageIn);
	///货币质出金额
	result = result + splitstr + dtos(pTradingAccount->FundMortgageOut);
	///货币质押余额
	result = result + splitstr + dtos(pTradingAccount->FundMortgageAvailable);
	///可质押货币金额
	result = result + splitstr + dtos(pTradingAccount->MortgageableFund);
	///特殊产品占用保证金
	result = result + splitstr + dtos(pTradingAccount->SpecProductMargin);
	///特殊产品冻结保证金
	result = result + splitstr + dtos(pTradingAccount->SpecProductFrozenMargin);
	///特殊产品手续费
	result = result + splitstr + dtos(pTradingAccount->SpecProductCommission);
	///特殊产品冻结手续费
	result = result + splitstr + dtos(pTradingAccount->SpecProductFrozenCommission);
	///特殊产品持仓盈亏
	result = result + splitstr + dtos(pTradingAccount->SpecProductPositionProfit);
	///特殊产品平仓盈亏
	result = result + splitstr + dtos(pTradingAccount->SpecProductCloseProfit);
	///根据持仓盈亏算法计算的特殊产品持仓盈亏
	result = result + splitstr + dtos(pTradingAccount->SpecProductPositionProfitByAlg);
	///特殊产品交易所保证金
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


// 查询持仓
int CTradeSpi::ReqQryInvestorPosition(char *brokerID, char *userID, int nRequestID)
{
	CThostFtdcQryInvestorPositionField qryInvestorPosition;
	memset(&qryInvestorPosition, 0, sizeof(CThostFtdcQryInvestorPositionField));

	///经纪公司代码
	strcpy_s(qryInvestorPosition.BrokerID, sizeof(TThostFtdcBrokerIDType),brokerID);	;
	///投资者代码
	strcpy_s(qryInvestorPosition.InvestorID, sizeof(TThostFtdcInvestorIDType), userID); // ?? 不确定这个值
	///币种代码
	strcpy_s(qryInvestorPosition.InstrumentID, sizeof(TThostFtdcInstrumentIDType), ""); //  空值代表查询全部合同？

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
		// 无持仓情况
		TradeResponse("", nRequestID, 1, m_indicator);
		return;
	}

	///合约代码
	result = result + (string)pInvestorPosition->InstrumentID;
	///经纪公司代码
	result = result + splitstr + (string)pInvestorPosition->BrokerID;
	///投资者代码
	result = result + splitstr + (string)pInvestorPosition->InvestorID;
	///持仓多空方向
	result = result + splitstr + ctos(pInvestorPosition->PosiDirection);
	///投机套保标志
	result = result + splitstr + ctos(pInvestorPosition->HedgeFlag);
	///持仓日期
	result = result + splitstr + ctos(pInvestorPosition->PositionDate);
	///上日持仓
	result = result + splitstr + ltos(pInvestorPosition->YdPosition);
	///今日持仓
	result = result + splitstr + ltos(pInvestorPosition->Position);
	///多头冻结
	result = result + splitstr + ltos(pInvestorPosition->LongFrozen);
	///空头冻结
	result = result + splitstr + ltos(pInvestorPosition->ShortFrozen);
	///开仓冻结金额
	result = result + splitstr + dtos(pInvestorPosition->LongFrozenAmount);
	///开仓冻结金额
	result = result + splitstr + dtos(pInvestorPosition->ShortFrozenAmount);
	///开仓量
	result = result + splitstr + ltos(pInvestorPosition->OpenVolume);
	///平仓量
	result = result + splitstr + ltos(pInvestorPosition->CloseVolume);
	///开仓金额
	result = result + splitstr + dtos(pInvestorPosition->OpenAmount);
	///平仓金额
	result = result + splitstr + dtos(pInvestorPosition->CloseAmount);
	///持仓成本
	result = result + splitstr + dtos(pInvestorPosition->PositionCost);
	///上次占用的保证金
	result = result + splitstr + dtos(pInvestorPosition->PreMargin);
	///占用的保证金
	result = result + splitstr + dtos(pInvestorPosition->UseMargin);
	///冻结的保证金
	result = result + splitstr + dtos(pInvestorPosition->FrozenMargin);
	///冻结的资金
	result = result + splitstr + dtos(pInvestorPosition->FrozenCash);
	///冻结的手续费
	result = result + splitstr + dtos(pInvestorPosition->FrozenCommission);
	///资金差额
	result = result + splitstr + dtos(pInvestorPosition->CashIn);
	///手续费
	result = result + splitstr + dtos(pInvestorPosition->Commission);
	///平仓盈亏
	result = result + splitstr + dtos(pInvestorPosition->CloseProfit);
	///持仓盈亏
	result = result + splitstr + dtos(pInvestorPosition->PositionProfit);
	///上次结算价
	result = result + splitstr + dtos(pInvestorPosition->PreSettlementPrice);
	///本次结算价
	result = result + splitstr + dtos(pInvestorPosition->SettlementPrice);
	///交易日
	result = result + splitstr + (string)pInvestorPosition->TradingDay;
	///结算编号
	result = result + splitstr + ltos(pInvestorPosition->SettlementID);
	///开仓成本
	result = result + splitstr + dtos(pInvestorPosition->OpenCost);
	///交易所保证金
	result = result + splitstr + dtos(pInvestorPosition->ExchangeMargin);
	///组合成交形成的持仓
	result = result + splitstr + ltos(pInvestorPosition->CombPosition);
	///组合多头冻结
	result = result + splitstr + ltos(pInvestorPosition->CombLongFrozen);
	///组合空头冻结
	result = result + splitstr + ltos(pInvestorPosition->CombShortFrozen);
	///逐日盯市平仓盈亏
	result = result + splitstr + dtos(pInvestorPosition->CloseProfitByDate);
	///逐笔对冲平仓盈亏
	result = result + splitstr + dtos(pInvestorPosition->CloseProfitByTrade);
	///今日持仓
	result = result + splitstr + ltos(pInvestorPosition->TodayPosition);
	///保证金率
	result = result + splitstr + dtos(pInvestorPosition->MarginRateByMoney);
	///保证金率(按手数)
	result = result + splitstr + dtos(pInvestorPosition->MarginRateByVolume);
	///执行冻结
	result = result + splitstr + ltos(pInvestorPosition->StrikeFrozen);
	///执行冻结金额
	result = result + splitstr + dtos(pInvestorPosition->StrikeFrozenAmount);
	///放弃执行冻结
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


// 查询报单
int CTradeSpi::ReqQryOrder(char *brokerID, char *userID, int nRequestID)
{
	CThostFtdcQryOrderField qryOrder;
	memset(&qryOrder, 0, sizeof(CThostFtdcQryOrderField));

	///经纪公司代码
	strcpy_s(qryOrder.BrokerID, sizeof(TThostFtdcBrokerIDType), brokerID);
	///投资者代码
	strcpy_s(qryOrder.InvestorID, sizeof(TThostFtdcInvestorIDType), userID); 
	/*///合约代码
	strcpy_s(qryOrder.InstrumentID, sizeof(TThostFtdcInstrumentIDType), "");
	///交易所代码
	strcpy_s(qryOrder.ExchangeID, sizeof(TThostFtdcExchangeIDType), "");
	///报单代码
	strcpy_s(qryOrder.OrderSysID, sizeof(TThostFtdcOrderSysIDType), "");*/

	return m_pTradeApi->ReqQryOrder(&qryOrder, nRequestID);
}
void CTradeSpi::OnRspQryOrder(CThostFtdcOrderField *pOrder, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{

}

// 查询成交
int CTradeSpi::ReqQryTrade(char *brokerID, char *userID, int nRequestID)
{
	CThostFtdcQryTradeField qryTrade;
	memset(&qryTrade, 0, sizeof(CThostFtdcQryTradeField));

	///经纪公司代码
	strcpy_s(qryTrade.BrokerID, sizeof(TThostFtdcBrokerIDType), brokerID);
	///投资者代码
	strcpy_s(qryTrade.InvestorID, sizeof(TThostFtdcInvestorIDType), userID); 
	///合约代码
	strcpy_s(qryTrade.InstrumentID, sizeof(TThostFtdcInstrumentIDType), "");
	///交易所代码
	strcpy_s(qryTrade.ExchangeID, sizeof(TThostFtdcExchangeIDType), "");
	///报单代码
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
	///经纪公司代码
	result = result + splitstr + (string)pTrade->BrokerID;
	///投资者代码
	result = result + splitstr + (string)pTrade->InvestorID;
	///合约代码
	result = result + splitstr + (string)pTrade->InstrumentID;
	///报单引用
	result = result + splitstr + (string)pTrade->OrderRef;
	///用户代码
	result = result + splitstr + (string)pTrade->UserID;
	///交易所代码
	result = result + splitstr + (string)pTrade->ExchangeID;
	///成交编号
	result = result + splitstr + (string)pTrade->TradeID;
	///买卖方向
	result = result + splitstr + ctos(pTrade->Direction);
	///报单编号
	result = result + splitstr + (string)pTrade->OrderSysID;
	///会员代码
	result = result + splitstr + (string)pTrade->ParticipantID;
	///客户代码
	result = result + splitstr + (string)pTrade->ClientID;
	///交易角色
	result = result + splitstr + ctos(pTrade->TradingRole);
	///合约在交易所的代码
	result = result + splitstr + (string)pTrade->ExchangeInstID;
	///开平标志
	result = result + splitstr + ctos(pTrade->OffsetFlag);
	///投机套保标志
	result = result + splitstr + ctos(pTrade->HedgeFlag);
	///价格
	result = result + splitstr + dtos(pTrade->Price);
	///数量
	result = result + splitstr + ltos(pTrade->Volume);
	///成交时期
	result = result + splitstr + (string)pTrade->TradeDate;
	///成交时间
	result = result + splitstr + (string)pTrade->TradeTime;
	///成交类型
	result = result + splitstr + ctos(pTrade->TradeType);
	///成交价来源
	result = result + splitstr + ctos(pTrade->PriceSource);
	///交易所交易员代码
	result = result + splitstr + (string)pTrade->TraderID;
	///本地报单编号
	result = result + splitstr + (string)pTrade->OrderLocalID;
	///结算会员编号
	result = result + splitstr + (string)pTrade->ClearingPartID;
	///业务单元
	result = result + splitstr + (string)pTrade->BusinessUnit;
	///序号
	result = result + splitstr + ltos(pTrade->SequenceNo);
	///交易日
	result = result + splitstr + (string)pTrade->TradingDay;
	///结算编号
	result = result + splitstr + ltos(pTrade->SettlementID);
	///经纪公司报单编号
	result = result + splitstr + ltos(pTrade->BrokerOrderSeq);
	///成交来源
	result = result + splitstr + ctos(pTrade->TradeSource);

	TradeResponse((char *)result.c_str(), -1, 1, m_indicator);
}

// 查询合同
int CTradeSpi::ReqQryInstrument(char *brokerID, char *userID, int nRequestID)
{
	CThostFtdcQryInstrumentField qryInstrument;
	memset(&qryInstrument, 0, sizeof(CThostFtdcQryInstrumentField));

	//  空值代表查询全部合同，可模糊查询
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

	///合约代码
	result = result + (string)pInstrument->InstrumentID;
	///交易所代码
	result = result + splitstr + (string)pInstrument->ExchangeID;
	///合约名称
	result = result + splitstr + (string)pInstrument->InstrumentName;
	///合约在交易所的代码
	result = result + splitstr + (string)pInstrument->ExchangeInstID;
	///产品代码
	result = result + splitstr + (string)pInstrument->ProductID;
	///产品类型
	result = result + splitstr + ctos(pInstrument->ProductClass);
	///交割年份
	result = result + splitstr + ltos(pInstrument->DeliveryYear);
	///交割月
	result = result + splitstr + ltos(pInstrument->DeliveryMonth);
	///市价单最大下单量
	result = result + splitstr + dtos(pInstrument->MaxMarketOrderVolume);
	///市价单最小下单量
	result = result + splitstr + dtos(pInstrument->MinMarketOrderVolume);
	///限价单最大下单量
	result = result + splitstr + dtos(pInstrument->MaxLimitOrderVolume);
	///限价单最小下单量
	result = result + splitstr + dtos(pInstrument->MinLimitOrderVolume);
	///合约数量乘数
	result = result + splitstr + dtos(pInstrument->VolumeMultiple);
	///最小变动价位
	result = result + splitstr + dtos(pInstrument->PriceTick);
	///创建日
	result = result + splitstr + (string)pInstrument->CreateDate;
	///上市日
	result = result + splitstr + (string)pInstrument->OpenDate;
	///到期日
	result = result + splitstr + (string)pInstrument->ExpireDate;
	///开始交割日
	result = result + splitstr + (string)pInstrument->StartDelivDate;
	///结束交割日
	result = result + splitstr + (string)pInstrument->EndDelivDate;
	///合约生命周期状态
	result = result + splitstr + ctos(pInstrument->InstLifePhase);
	///当前是否交易
	result = result + splitstr + ltos(pInstrument->IsTrading);
	///持仓类型
	result = result + splitstr + ctos(pInstrument->PositionType);
	///持仓日期类型
	result = result + splitstr + ctos(pInstrument->PositionDateType);
	///多头保证金率
	result = result + splitstr + dtos(pInstrument->LongMarginRatio);
	///空头保证金率
	result = result + splitstr + dtos(pInstrument->ShortMarginRatio);
	///是否使用大额单边保证金算法
	result = result + splitstr + ctos(pInstrument->MaxMarginSideAlgorithm);
	///基础商品代码
	result = result + splitstr + (string)pInstrument->UnderlyingInstrID;
	///执行价(数字过大，溢出！！)
	result = result + splitstr + dtos(pInstrument->StrikePrice);
	///期权类型
	result = result + splitstr + ctos(pInstrument->OptionsType);
	///合约基础商品乘数
	result = result + splitstr + dtos(pInstrument->UnderlyingMultiple);
	///组合类型
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

// 请求报单
int CTradeSpi::ReqOrderInsert(vector<string> v, int nRequestID)
{
	CThostFtdcInputOrderField inputOrder;
	memset(&inputOrder, 0, sizeof(CThostFtdcInputOrderField));

	///经纪公司代码
	strcpy_s(inputOrder.BrokerID, sizeof(TThostFtdcBrokerIDType), (char *)v[3].c_str());
	///投资者代码
	strcpy_s(inputOrder.InvestorID, sizeof(TThostFtdcInvestorIDType), (char *)v[4].c_str());
	///合约代码
	strcpy_s(inputOrder.InstrumentID, sizeof(TThostFtdcInstrumentIDType), (char *)v[6].c_str());
	///报单引用：记录localID
	strcpy_s(inputOrder.OrderRef, sizeof(TThostFtdcOrderRefType), (char *)v[11].c_str());
	///用户代码 ??
	strcpy_s(inputOrder.UserID, sizeof(TThostFtdcUserIDType), (char *)v[4].c_str());
	///报单价格条件
	inputOrder.OrderPriceType = THOST_FTDC_OPT_LimitPrice;
	///买卖方向： 0 买 1 卖 THOST_FTDC_D_Buy
	inputOrder.Direction = ((char *)v[7].c_str())[0];
	///组合开平标志 ??      开仓 '0', 平仓 '1', 强平 '2', 平今 '3', 平昨 '4', 强减 '5', 本地强平 '6'
	inputOrder.CombOffsetFlag[0] = ((char *)v[8].c_str())[0];

	///组合投机套保标志 ?? 套保 '1'             , 套利 '2',投机 '3' 
	/// 特别说明：
	///      上海无套利单，正常应该用 投机
	///      自行区分平今平昨（若服务器端持仓不足，似乎不反馈错误或通过其他函数返回）
	///      套保单需单独申请，模拟环节可能未限制，实际环境可能被禁止
	///      注：使用Q7下的单，均为套保。郑州不区分平今、平昨，上海需区分，但不知从哪返回错误。
	/// 开仓时若使用投机选项，平仓时会如何，待验证
	inputOrder.CombHedgeFlag[0] = THOST_FTDC_ECIDT_Hedge;
	///价格
	inputOrder.LimitPrice = atof((char *)v[9].c_str());
	///数量
	inputOrder.VolumeTotalOriginal = atoi((char *)v[10].c_str());
	///有效期类型
	inputOrder.TimeCondition = THOST_FTDC_TC_GFD;
	///GTD日期
	strcpy_s(inputOrder.GTDDate, sizeof(TThostFtdcDateType), "");
	///成交量类型
	inputOrder.VolumeCondition = THOST_FTDC_VC_AV;
	///最小成交量 ??
	inputOrder.MinVolume  = 0;
	///触发条件
	inputOrder.ContingentCondition = THOST_FTDC_CC_Immediately;
	///止损价 ??
	inputOrder.StopPrice  = 0;
	///强平原因
	inputOrder.ForceCloseReason  = THOST_FTDC_FCC_NotForceClose;
	///自动挂起标志 ?? 
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
	///经纪公司代码
	result = result + splitstr + (string)pOrder->BrokerID;
	///投资者代码
	result = result + splitstr + (string)pOrder->InvestorID;
	///合约代码
	result = result + splitstr + (string)pOrder->InstrumentID;
	///报单引用
	result = result + splitstr + (string)pOrder->OrderRef;
	///用户代码
	result = result + splitstr + (string)pOrder->UserID;
	///报单价格条件
	result = result + splitstr + ctos(pOrder->OrderPriceType);
	///买卖方向
	result = result + splitstr + ctos(pOrder->Direction);
	///组合开平标志
	result = result + splitstr + (string)pOrder->CombOffsetFlag;
	///组合投机套保标志
	result = result + splitstr + (string)pOrder->CombHedgeFlag;
	///价格
	result = result + splitstr + dtos(pOrder->LimitPrice);
	///数量
	result = result + splitstr + ltos(pOrder->VolumeTotalOriginal);
	///有效期类型
	result = result + splitstr + ctos(pOrder->TimeCondition);
	///GTD日期
	result = result + splitstr + (string)pOrder->GTDDate;
	///成交量类型
	result = result + splitstr + ctos(pOrder->VolumeCondition);
	///最小成交量
	result = result + splitstr + ltos(pOrder->MinVolume);
	///触发条件
	result = result + splitstr + ctos(pOrder->ContingentCondition);
	///止损价
	result = result + splitstr + dtos(pOrder->StopPrice);
	///强平原因
	result = result + splitstr + ctos(pOrder->ForceCloseReason);
	///自动挂起标志
	result = result + splitstr + ltos(pOrder->IsAutoSuspend);
	///业务单元
	result = result + splitstr + (string)pOrder->BusinessUnit;
	///请求编号
	result = result + splitstr + ltos(pOrder->RequestID);
	///本地报单编号
	result = result + splitstr + (string)pOrder->OrderLocalID;
	///交易所代码
	result = result + splitstr + (string)pOrder->ExchangeID;
	///会员代码
	result = result + splitstr + (string)pOrder->ParticipantID;
	///客户代码
	result = result + splitstr + (string)pOrder->ClientID;
	///合约在交易所的代码
	result = result + splitstr + (string)pOrder->ExchangeInstID;
	///交易所交易员代码
	result = result + splitstr + (string)pOrder->TraderID;
	///安装编号
	result = result + splitstr + ltos(pOrder->InstallID);
	///报单提交状态
	result = result + splitstr + ctos(pOrder->OrderSubmitStatus);
	///报单提示序号
	result = result + splitstr + ltos(pOrder->NotifySequence);
	///交易日
	result = result + splitstr + (string)pOrder->TradingDay;
	///结算编号
	result = result + splitstr + ltos(pOrder->SettlementID);
	///报单编号
	result = result + splitstr + (string)pOrder->OrderSysID;
	///报单来源
	result = result + splitstr + ctos(pOrder->OrderSource);
	///报单状态
	result = result + splitstr + ctos(pOrder->OrderStatus);
	///报单类型
	result = result + splitstr + ctos(pOrder->OrderType);
	///今成交数量
	result = result + splitstr + ltos(pOrder->VolumeTraded);
	///剩余数量
	result = result + splitstr + ltos(pOrder->VolumeTotal);
	///报单日期
	result = result + splitstr + (string)pOrder->InsertDate;
	///委托时间
	result = result + splitstr + (string)pOrder->InsertTime;
	///激活时间
	result = result + splitstr + (string)pOrder->ActiveTime;
	///挂起时间
	result = result + splitstr + (string)pOrder->SuspendTime;
	///最后修改时间
	result = result + splitstr + (string)pOrder->UpdateTime;
	///撤销时间
	result = result + splitstr + (string)pOrder->CancelTime;
	///最后修改交易所交易员代码
	result = result + splitstr + (string)pOrder->ActiveTraderID;
	///结算会员编号
	result = result + splitstr + (string)pOrder->ClearingPartID;
	///序号
	result = result + splitstr + ltos(pOrder->SequenceNo);
	///前置编号
	result = result + splitstr + ltos(pOrder->FrontID);
	///会话编号
	result = result + splitstr + ltos(pOrder->SessionID);
	///用户端产品信息
	result = result + splitstr + (string)pOrder->UserProductInfo;
	///状态信息
	result = result + splitstr + (string)pOrder->StatusMsg;
	///用户强评标志
	result = result + splitstr + ltos(pOrder->UserForceClose);
	///操作用户代码
	result = result + splitstr + (string)pOrder->ActiveUserID;
	///经纪公司报单编号
	result = result + splitstr + ltos(pOrder->BrokerOrderSeq);
	///相关报单
	result = result + splitstr + (string)pOrder->RelativeOrderSysID;
	///郑商所成交数量
	result = result + splitstr + ltos(pOrder->ZCETotalTradedVolume);
	///互换单标志
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

// 请求撤单
int CTradeSpi::ReqOrderAction(vector<string> v, int nRequestID)
{
	CThostFtdcInputOrderActionField inputOrderAction;
	memset(&inputOrderAction, 0, sizeof(CThostFtdcInputOrderActionField));

	///经纪公司代码
	strcpy_s(inputOrderAction.BrokerID, sizeof(TThostFtdcBrokerIDType), (char *)v[3].c_str());
	///投资者代码
	strcpy_s(inputOrderAction.InvestorID, sizeof(TThostFtdcInvestorIDType), (char *)v[4].c_str());
	///报单号
	strcpy_s(inputOrderAction.OrderSysID, sizeof(TThostFtdcOrderSysIDType), (char *)v[6].c_str());
	///操作标志
	inputOrderAction.ActionFlag = THOST_FTDC_AF_Delete;
	///用户代码
	strcpy_s(inputOrderAction.UserID, sizeof(TThostFtdcUserIDType), (char *)v[4].c_str());
	///交易所代码
	strcpy_s(inputOrderAction.ExchangeID, sizeof(TThostFtdcExchangeIDType), (char *)v[7].c_str());

	return m_pTradeApi->ReqOrderAction(&inputOrderAction, nRequestID);
}

// 请求撤单响应
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

// 请求行情
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

	//交易日
	result = result + (string)pDepthMarketData->TradingDay;
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
//  行情
// --------------------------------------------------------------------------------------
// 行情连接
void CMdSpi::OnFrontConnected()
{
	MdResponse("-1><Md><OnFrontConnected><0><FrontConnected><0");
};

// 登录
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
			result = result + splitstr + "登录成功";
		}
		result = result + splitstr + "1";
		result = result + splitstr + (string)pRspUserLogin->TradingDay;
		MdResponse((char *)result.c_str());
	}
	return;
}

// 登出
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
			result = result + splitstr + "登出成功";
		}
	}
	MdResponse((char *)result.c_str());
	return;
}

// 订阅行情
int CMdSpi::SubscribeMarketData(char* instrumentID)
{
	char *tmp[2];
	TThostFtdcInstrumentIDType tmpStr;

	strcpy_s(tmpStr, sizeof(TThostFtdcInstrumentIDType), instrumentID);
	tmp[0] = (char *)&tmpStr;
	tmp[1] = NULL;
	return m_pMdApi->SubscribeMarketData((char **)tmp, 1);
}

/// 订阅行情返回：目前仅支持单合同订阅，不考虑bIisLast
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
			result = result + splitstr + "订阅成功";
		}
	}
	result = result + splitstr + "1";
	result = result + splitstr + (string)pSpecificInstrument->InstrumentID;
	MdResponse((char *)result.c_str());
	return;
}

// 退订行情
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
			result = result + splitstr + "退订成功";
		}
	}
	result = result + splitstr + "1";
	result = result + splitstr + (string)pSpecificInstrument->InstrumentID;
	MdResponse((char *)result.c_str());
	return;
}

//行情返回
void CMdSpi::OnRtnDepthMarketData(CThostFtdcDepthMarketDataField *pDepthMarketData)
{
	string result = "-1";
	result = result + splitstr + "Md";
	result = result + splitstr + "marketdata";
	result = result + splitstr + "0" + splitstr + "0" + splitstr + "1";
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
