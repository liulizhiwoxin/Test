using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using uniondemo.com.allinpay.syb;

namespace BuysingooShop.Weixin
{
    public partial class test : System.Web.UI.Page
    {
        string openid = "";
        SybWxPayService sybService = new SybWxPayService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Vincent._Weixin.WeixinPay weixinpay = new Vincent._Weixin.WeixinPay("sAppID", "sAppSecret", "sPartner", "sPartnerKey", "sReturnUrl");
                openid = Vincent._Weixin.WeixinUtility.GetOpendId();

                Vincent._Log.SaveMessage("test.aspx获取openid=" + openid);
               
            }
        }

        protected void btn_pay_Click(object sender, EventArgs e)
        {
            try
            {

                Dictionary<String, String> rsp = sybService.pay(1, DateTime.Now.ToFileTime().ToString(), "W02", "VIP会员一年有效期", "门川家居", openid, "", "http://shop.mc-house.com/WeixinPay/return_url.aspx", "");
                printRsp(rsp);
            }
            catch (Exception ex)
            {
                this.tblank.Value = ex.Message;
            }

        }

        private void printRsp(Dictionary<String, String> rspDic)
        {
            string rsp = "请求返回数据:\n";
            foreach (var item in rspDic)
            {
                rsp += item.Key + "-----" + item.Value + ";\n";
            }
            this.tblank.Value = rsp;
        }
    }
}