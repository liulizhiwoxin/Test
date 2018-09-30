using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vincent;

namespace BuysingooShop.Weixin.WeixinPay
{
    public partial class JSAPI_PAY : System.Web.UI.Page
    {
        protected string wx_packageValue = "";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            Vincent._Log.SaveMessage("进来页面");

            if (!IsPostBack)
            {
                try
                {
                    Vincent._Weixin.WeixinPay weixinpay = new Vincent._Weixin.WeixinPay("sAppID", "sAppSecret", "sPartner", "sPartnerKey", "sReturnUrl");

                    string strBillNo = Vincent._Weixin.WeixinPay.getTimestamp();
                    var userid = Vincent._Request.GetString("userid");
                    var user_name = Vincent._Request.GetString("user_name");


                    var orid = userid + "_" + strBillNo;
                    var money = Vincent._Convert.ToInt64(Vincent._WebConfig.GetAppSettingsString("pro_price"), 1);


                    //decimal money = (decimal)_Request.GetFloat("money");
                    string strWeixin_OpenID = "";
                    string strCode = Request["code"] == null ? "" : Request["code"];

                    Vincent._Log.SaveMessage("第一步，获取code:" + strCode);

                    if (string.IsNullOrEmpty(strCode))
                    {
                        //参数需要自己处理
                        string _OAuth_Url = Vincent._Weixin.WeixinPay.OAuth2_GetUrl_Pay(Request.Url.ToString());

                        Response.Redirect(_OAuth_Url);
                        return;
                    }
                    else
                    {
                        Vincent._Weixin.ReturnValue retValue = Vincent._Weixin.WeixinPay.OAuth2_Access_Token(strCode);

                        if (retValue.HasError)
                        {
                            Vincent._Log.SaveMessage("获取code失败：" + retValue.Message);

                            //Response.Write("获取code失败：" + retValue.Message);
                            //return;
                        }
                        strWeixin_OpenID = retValue.GetStringValue("Weixin_OpenID");
                        string strWeixin_Token = retValue.GetStringValue("Weixin_Token");

                        if (string.IsNullOrEmpty(strWeixin_OpenID))
                        {
                            Vincent._Log.SaveMessage("openid出错：" + strWeixin_OpenID);

                            //Response.Write("openid出错");
                            //return;
                        }
                    }

                    string _Pay_Package = "";

                    string IsTestPay = Vincent._WebConfig.GetAppSettingsString("IsTestPay");

                    
                    if(IsTestPay == "1")
                        _Pay_Package = weixinpay.Get_RequestHtml(strWeixin_OpenID, orid, 0.1M, "门川家居", "hb_store");
                    else
                        _Pay_Package = weixinpay.Get_RequestHtml(strWeixin_OpenID, orid, money, "门川家居", "hb_store");
                    //string _Pay_Package = Interface_WxPay.Get_RequestHtml(strWeixin_OpenID, id, money, "宇航龙商城", "hb_store");


                    Vincent._Log.SaveMessage("第三步 结束：" + _Pay_Package);


                    //微信jspai支付
                    if (_Pay_Package.Length > 0)
                    {
                        wx_packageValue = _Pay_Package;
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

    }
}