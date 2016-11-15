using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

namespace PubTools.data
{
    public class Account
    {
        ///经纪公司代码
        public String BrokerID;
        ///投资者帐号
        public String AccountID;
        ///上次质押金额
        public double PreMortgage;
        ///上次信用额度
        public double PreCredit;
        ///上次存款额
        public double PreDeposit;
        ///上次结算准备金
        public double PreBalance;
        ///上次占用的保证金
        public double PreMargin;
        ///利息基数
        public double InterestBase;
        ///利息收入
        public double Interest;
        ///入金金额
        public double Deposit;
        ///出金金额
        public double Withdraw;
        ///冻结的保证金
        public double FrozenMargin;
        ///冻结的资金
        public double FrozenCash;
        ///冻结的手续费
        public double FrozenCommission;
        ///当前保证金总额
        public double CurrMargin;
        ///资金差额
        public double CashIn;
        ///手续费
        public double Commission;
        ///平仓盈亏
        public double CloseProfit;
        ///持仓盈亏
        public double PositionProfit;
        ///期货结算准备金
        public double Balance;
        ///可用资金
        public double Available;
        ///可取资金
        public double WithdrawQuota;
        ///基本准备金
        public double Reserve;
        ///交易日
        public String TradingDay;
        ///结算编号
        long SettlementID;
        ///信用额度
        public double Credit;
        ///质押金额
        public double Mortgage;
        ///交易所保证金
        public double ExchangeMargin;
        ///投资者交割保证金
        public double DeliveryMargin;
        ///交易所交割保证金
        public double ExchangeDeliveryMargin;
        ///保底期货结算准备金
        public double ReserveBalance;
        ///币种代码
        public String CurrencyID;
        ///上次货币质入金额
        public double PreFundMortgageIn;
        ///上次货币质出金额
        public double PreFundMortgageOut;
        ///货币质入金额
        public double FundMortgageIn;
        ///货币质出金额
        public double FundMortgageOut;
        ///货币质押余额
        public double FundMortgageAvailable;
        ///可质押货币金额
        public double MortgageableFund;
        ///特殊产品占用保证金
        public double SpecProductMargin;
        ///特殊产品冻结保证金
        public double SpecProductFrozenMargin;
        ///特殊产品手续费
        public double SpecProductCommission;
        ///特殊产品冻结手续费
        public double SpecProductFrozenCommission;
        ///特殊产品持仓盈亏
        public double SpecProductPositionProfit;
        ///特殊产品平仓盈亏
        public double SpecProductCloseProfit;
        ///根据持仓盈亏算法计算的特殊产品持仓盈亏
        public double SpecProductPositionProfitByAlg;
        ///特殊产品交易所保证金
        public double SpecProductExchangeMargin;

        /// <summary>
        /// 解析返回数据
        /// </summary>
        /// <param name="resStr"></param>
        /// <returns>0 成功</returns>
 
        public int SetData(String[] resStr)
        {
            if (resStr.Length < 6)
                return -1;
            int pos = 6;

            ///经纪公司代码
            BrokerID = resStr[pos];
            ///投资者帐号
            AccountID = resStr[pos + 1];
            ///上次质押金额
            PreMortgage = double.Parse(resStr[pos + 2]);
            ///上次信用额度
            PreCredit = double.Parse(resStr[pos + 3]);
            ///上次存款额
            PreDeposit = double.Parse(resStr[pos + 4]);
            ///上次结算准备金
            PreBalance = double.Parse(resStr[pos + 5]);
            ///上次占用的保证金
            PreMargin = double.Parse(resStr[pos + 6]);
            ///利息基数
            InterestBase = double.Parse(resStr[pos + 7]);
            ///利息收入
            Interest = double.Parse(resStr[pos + 8]);
            ///入金金额
            Deposit = double.Parse(resStr[pos + 9]);
            ///出金金额
            Withdraw = double.Parse(resStr[pos + 10]);
            ///冻结的保证金
            FrozenMargin = double.Parse(resStr[pos + 11]);
            ///冻结的资金
            FrozenCash = double.Parse(resStr[pos + 12]);
            ///冻结的手续费
            FrozenCommission = double.Parse(resStr[pos + 13]);
            ///当前保证金总额
            CurrMargin = double.Parse(resStr[pos + 14]);
            ///资金差额
            CashIn = double.Parse(resStr[pos + 15]);
            ///手续费
            Commission = double.Parse(resStr[pos + 16]);
            ///平仓盈亏
            CloseProfit = double.Parse(resStr[pos + 17]);
            ///持仓盈亏
            PositionProfit = double.Parse(resStr[pos + 18]);
            ///期货结算准备金
            Balance = double.Parse(resStr[pos + 19]);
            ///可用资金
            Available = double.Parse(resStr[pos + 20]);
            ///可取资金
            WithdrawQuota = double.Parse(resStr[pos + 21]);
            ///基本准备金
            Reserve = double.Parse(resStr[pos + 22]);
            ///交易日
            TradingDay = resStr[pos + 23];
            ///结算编号
            SettlementID = long.Parse(resStr[pos + 24]);
            ///信用额度
            Credit = double.Parse(resStr[pos + 25]);
            ///质押金额
            Mortgage = double.Parse(resStr[pos + 26]);
            ///交易所保证金
            ExchangeMargin = double.Parse(resStr[pos + 27]);
            ///投资者交割保证金
            DeliveryMargin = double.Parse(resStr[pos + 28]);
            ///交易所交割保证金
            ExchangeDeliveryMargin = double.Parse(resStr[pos + 29]);
            ///保底期货结算准备金
            ReserveBalance = double.Parse(resStr[pos + 30]);
            ///币种代码
            CurrencyID = resStr[pos + 31];
            ///上次货币质入金额
            PreFundMortgageIn = double.Parse(resStr[pos + 32]);
            ///上次货币质出金额
            PreFundMortgageOut = double.Parse(resStr[pos + 33]);
            ///货币质入金额
            FundMortgageIn = double.Parse(resStr[pos + 34]);
            ///货币质出金额
            FundMortgageOut = double.Parse(resStr[pos + 35]);
            ///货币质押余额
            FundMortgageAvailable = double.Parse(resStr[pos + 36]);
            ///可质押货币金额
            MortgageableFund = double.Parse(resStr[pos + 37]);
            ///特殊产品占用保证金
            SpecProductMargin = double.Parse(resStr[pos + 38]);
            ///特殊产品冻结保证金
            SpecProductFrozenMargin = double.Parse(resStr[pos + 39]);
            ///特殊产品手续费
            SpecProductCommission = double.Parse(resStr[pos + 40]);
            ///特殊产品冻结手续费
            SpecProductFrozenCommission = double.Parse(resStr[pos + 41]);
            ///特殊产品持仓盈亏
            SpecProductPositionProfit = double.Parse(resStr[pos + 42]);
            ///特殊产品平仓盈亏
            SpecProductCloseProfit = double.Parse(resStr[pos + 43]);
            ///根据持仓盈亏算法计算的特殊产品持仓盈亏
            SpecProductPositionProfitByAlg = double.Parse(resStr[pos + 44]);
            ///特殊产品交易所保证金
            SpecProductExchangeMargin = double.Parse(resStr[pos + 45]);

            return 0;
        }

        // 将数据刷新到datatable，供显示使用
        public void RefreshToDataTable(DataTable dt)
        {
            Boolean found = false;
            foreach (DataRow dr in dt.Rows)
            {
                String tmp_brokerid = (String)dr["BrokerID"];
                String tmp_userid = (String)dr["AccountID"];
                if (tmp_brokerid.Equals(this.BrokerID) && tmp_userid.Equals(this.AccountID))
                {
                    found = true;
                    SetToDatarow(dr);
                    dt.AcceptChanges();
                    break;
                }
            }
            if (!found)
            {
                DataRow dr = dt.NewRow();
                SetToDatarow(dr);
                dt.Rows.Add(dr);
            }
        }

        private void SetToDatarow(DataRow dr)
        {
            ///经纪公司代码
            dr["BrokerID"] = this.BrokerID;
            ///投资者帐号
            dr["AccountID"] = this.AccountID;
            ///上次质押金额
            dr["PreMortgage"] = this.PreMortgage;
            ///上次信用额度
            dr["PreCredit"] = this.PreCredit;
            ///上次存款额
            dr["PreDeposit"] = this.PreDeposit;
            ///上次结算准备金
            dr["PreBalance"] = this.PreBalance;
            ///上次占用的保证金
            dr["PreMargin"] = this.PreMargin;
            ///利息基数
            dr["InterestBase"] = this.InterestBase;
            ///利息收入
            dr["Interest"] = this.Interest;
            ///入金金额
            dr["Deposit"] = this.Deposit;
            ///出金金额
            dr["Withdraw"] = this.Withdraw;
            ///冻结的保证金
            dr["FrozenMargin"] = this.FrozenMargin;
            ///冻结的资金
            dr["FrozenCash"] = this.FrozenCash;
            ///冻结的手续费
            dr["FrozenCommission"] = this.FrozenCommission;
            ///当前保证金总额
            dr["CurrMargin"] = this.CurrMargin;
            ///资金差额
            dr["CashIn"] = this.CashIn;
            ///手续费
            dr["Commission"] = this.Commission;
            ///平仓盈亏
            dr["CloseProfit"] = this.CloseProfit;
            ///持仓盈亏
            dr["PositionProfit"] = this.PositionProfit;
            ///期货结算准备金
            dr["Balance"] = this.Balance;
            ///可用资金
            dr["Available"] = this.Available;
            ///可取资金
            dr["WithdrawQuota"] = this.WithdrawQuota;
            ///基本准备金
            dr["Reserve"] = this.Reserve;
            ///交易日
            dr["TradingDay"] = this.TradingDay;
            ///结算编号
            dr["SettlementID"] = this.SettlementID;
            ///信用额度
            dr["Credit"] = this.Credit;
            ///质押金额
            dr["Mortgage"] = this.Mortgage;
            ///交易所保证金
            dr["ExchangeMargin"] = this.ExchangeMargin;
            ///投资者交割保证金
            dr["DeliveryMargin"] = this.DeliveryMargin;
            ///交易所交割保证金
            dr["ExchangeDeliveryMargin"] = this.ExchangeDeliveryMargin;
            ///保底期货结算准备金
            dr["ReserveBalance"] = this.ReserveBalance;
            ///币种代码
            dr["CurrencyID"] = this.CurrencyID;
            ///上次货币质入金额
            dr["PreFundMortgageIn"] = this.PreFundMortgageIn;
            ///上次货币质出金额
            dr["PreFundMortgageOut"] = this.PreFundMortgageOut;
            ///货币质入金额
            dr["FundMortgageIn"] = this.FundMortgageIn;
            ///货币质出金额
            dr["FundMortgageOut"] = this.FundMortgageOut;
            ///货币质押余额
            dr["FundMortgageAvailable"] = this.FundMortgageAvailable;
            ///可质押货币金额
            dr["MortgageableFund"] = this.MortgageableFund;
            ///特殊产品占用保证金
            dr["SpecProductMargin"] = this.SpecProductMargin;
            ///特殊产品冻结保证金
            dr["SpecProductFrozenMargin"] = this.SpecProductFrozenMargin;
            ///特殊产品手续费
            dr["SpecProductCommission"] = this.SpecProductCommission;
            ///特殊产品冻结手续费
            dr["SpecProductFrozenCommission"] = this.SpecProductFrozenCommission;
            ///特殊产品持仓盈亏
            dr["SpecProductPositionProfit"] = this.SpecProductPositionProfit;
            ///特殊产品平仓盈亏
            dr["SpecProductCloseProfit"] = this.SpecProductCloseProfit;
            ///根据持仓盈亏算法计算的特殊产品持仓盈亏
            dr["SpecProductPositionProfitByAlg"] = this.SpecProductPositionProfitByAlg;
            ///特殊产品交易所保证金
            dr["SpecProductExchangeMargin"] = this.SpecProductExchangeMargin;
        }
    }
}
