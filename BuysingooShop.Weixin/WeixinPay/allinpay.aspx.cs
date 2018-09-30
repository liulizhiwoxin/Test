using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using uniondemo.com.allinpay.syb;

namespace BuysingooShop.Weixin.WeixinPay
{
    public partial class allinpay : System.Web.UI.Page
    {
        //https://vsp.allinpay.com 通联收银宝

        protected string wx_packageValue = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            var userid = Vincent._Request.GetString("userid");
            var user_name = Vincent._Request.GetString("user_name");

            string openid = "";
            SybWxPayService sybService = new SybWxPayService();

            Vincent._Weixin.WeixinPay weixinpay = new Vincent._Weixin.WeixinPay("sAppID", "sAppSecret", "sPartner", "sPartnerKey", "sReturnUrl");
            openid = Vincent._Weixin.WeixinUtility.GetOpendId();

            if (openid != "" && Vincent._Convert.ToInt(userid) > 0)
            {
                BLL.users bll = new BLL.users();
                Model.users model = bll.GetModel(Vincent._Convert.ToInt(userid));

                model.safe_question = openid;
                if (bll.Update(model))
                {
                    Vincent._Log.SaveMessage("alinpay.aspx----->openid写入数据库成功userid=" + userid + "  ------> openid=" + openid);
                }
            }

            Vincent._Log.SaveMessage("test.aspx获取openid=" + openid);

            long pro_price = Vincent._Convert.ToInt64(Vincent._WebConfig.GetAppSettingsString("pro_price"), 1);

            try
            {
                Random ran = new Random();
                var num = ran.Next(1000);

                Dictionary<String, String> rsp = sybService.pay(pro_price, user_name + "_" + num, "W02", "VIP会员一年有效期", "门川家居", openid, "", "http://shop.mc-house.com/WeixinPay/return_url.aspx", "");
                printRsp(rsp);
            }
            catch (Exception ex)
            {
                Vincent._Log.SaveMessage("收银宝支付异常:" + ex.Message);
            }

        }

        private void printRsp(Dictionary<String, String> rspDic)
        {
            string rsp = "请求返回数据:\n";
            foreach (var item in rspDic)
            {
                if(item.Key == "payinfo")
                {
                    wx_packageValue = item.Value;
                }

                rsp += item.Key + "-----" + item.Value + ";\n";
            }

            Vincent._Log.SaveMessage("" + rsp);            
        }

    }
}