using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentHub.WeChatPayCore.Util
{
    internal static class WeChatBankTypes
    {
        // Fields
        private static Dictionary<string, string> _BankTypeDictionary;

        // Methods
        static WeChatBankTypes()
        {
            Dictionary<string, string> dictionary1 = new Dictionary<string, string>();
            dictionary1.Add("ICBC_DEBIT", "工商银行（借记卡）");
            dictionary1.Add("ICBC_CREDIT", "工商银行（信用卡）");
            dictionary1.Add("ABC_DEBIT", "农业银行（借记卡）");
            dictionary1.Add("ABC_CREDIT", "农业银行 （信用卡）");
            dictionary1.Add("PSBC_DEBIT", "邮政储蓄（借记卡）");
            dictionary1.Add("PSBC_CREDIT", "邮政储蓄 （信用卡）");
            dictionary1.Add("CCB_DEBIT", "建设银行（借记卡）");
            dictionary1.Add("CCB_CREDIT", "建设银行 （信用卡）");
            dictionary1.Add("CMB_DEBIT", "招商银行（借记卡）");
            dictionary1.Add("CMB_CREDIT", "招商银行（信用卡）");
            dictionary1.Add("COMM_DEBIT", "交通银行（借记卡）");
            dictionary1.Add("BOC_CREDIT", "中国银行（信用卡）");
            dictionary1.Add("BOC_DEBIT", "中国银行（借记卡）");
            dictionary1.Add("SPDB_DEBIT", "浦发银行（借记卡）");
            dictionary1.Add("SPDB_CREDIT", "浦发银行 （信用卡）");
            dictionary1.Add("GDB_DEBIT", "广发银行（借记卡）");
            dictionary1.Add("GDB_CREDIT", "广发银行（信用卡）");
            dictionary1.Add("CMBC_DEBIT", "民生银行（借记卡）");
            dictionary1.Add("CMBC_CREDIT", "民生银行（信用卡）");
            dictionary1.Add("PAB_DEBIT", "平安银行（借记卡）");
            dictionary1.Add("PAB_CREDIT", "平安银行（信用卡）");
            dictionary1.Add("CEB_DEBIT", "光大银行（借记卡）");
            dictionary1.Add("CEB_CREDIT", "光大银行（信用卡）");
            dictionary1.Add("CIB_DEBIT", "兴业银行 （借记卡）");
            dictionary1.Add("CIB_CREDIT", "兴业银行（信用卡）");
            dictionary1.Add("CITIC_DEBIT", "中信银行（借记卡）");
            dictionary1.Add("CITIC_CREDIT", "中信银行（信用卡）");
            dictionary1.Add("SDB_CREDIT", "深发银行（信用卡）");
            dictionary1.Add("BOSH_DEBIT", "上海银行（借记卡）");
            dictionary1.Add("BOSH_CREDIT\t", "上海银行 （信用卡）");
            dictionary1.Add("CRB_DEBIT", "华润银行（借记卡）");
            dictionary1.Add("HZB_DEBIT", "杭州银行（借记卡）");
            dictionary1.Add("HZB_CREDIT", "杭州银行（信用卡）");
            dictionary1.Add("BSB_DEBIT", "包商银行（借记卡）");
            dictionary1.Add("BSB_CREDIT", "包商银行 （信用卡）");
            dictionary1.Add("CQB_DEBIT", "\t重庆银行（借记卡）");
            dictionary1.Add("SDEB_DEBIT", "顺德农商行 （借记卡）");
            dictionary1.Add("SZRCB_DEBIT", "深圳农商银行（借记卡）");
            dictionary1.Add("HRBB_DEBIT", "哈尔滨银行（借记卡）");
            dictionary1.Add("BOCD_DEBIT", "成都银行（借记卡）");
            dictionary1.Add("GDNYB_DEBIT", "南粤银行 （借记卡）");
            dictionary1.Add("GDNYB_CREDIT", "南粤银行 （信用卡）");
            dictionary1.Add("GZCB_CREDIT\t", "广州银行（信用卡）");
            dictionary1.Add("JSB_DEBIT", "江苏银行（借记卡）");
            dictionary1.Add("JSB_CREDIT", "江苏银行（信用卡）");
            dictionary1.Add("NBCB_DEBIT", "宁波银行（借记卡）");
            dictionary1.Add("NBCB_CREDIT\t", "宁波银行（信用卡）");
            dictionary1.Add("NJCB_DEBIT", "南京银行（借记卡）");
            dictionary1.Add("QDCCB_DEBIT", "青岛银行（借记卡）");
            dictionary1.Add("ZJTLCB_DEBIT\t", "浙江泰隆银行（借记卡）");
            dictionary1.Add("XAB_DEBIT", "西安银行（借记卡）");
            dictionary1.Add("CSRCB_DEBIT", "常熟农商银行 （借记卡）");
            dictionary1.Add("QLB_DEBIT", "齐鲁银行（借记卡）");
            dictionary1.Add("LJB_DEBIT", "龙江银行（借记卡）");
            dictionary1.Add("HXB_DEBIT", "华夏银行（借记卡）");
            dictionary1.Add("CS_DEBIT", "测试银行借记卡快捷支付 （借记卡）");
            dictionary1.Add("AE_CREDIT", "AE （信用卡）");
            dictionary1.Add("JCB_CREDIT", "JCB （信用卡）");
            dictionary1.Add("MASTERCARD_CREDIT", "MASTERCARD （信用卡）");
            dictionary1.Add("VISA_CREDIT", "VISA （信用卡）");
            _BankTypeDictionary = dictionary1;
        }

        internal static string GetBank(string bankType) =>
            (!string.IsNullOrWhiteSpace(bankType) ? (!_BankTypeDictionary.ContainsKey(bankType) ? string.Empty : _BankTypeDictionary[bankType]) : string.Empty);
    }

}
