using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BuysingooShop.Weixin
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            string openid = "";

            string IsTestPay = Vincent._WebConfig.GetAppSettingsString("IsTestPay");


            if (IsTestPay == "1")
            {

            }
            else
            {
                Vincent._Weixin.WeixinPay weixinpay = new Vincent._Weixin.WeixinPay("sAppID", "sAppSecret", "sPartner", "sPartnerKey", "sReturnUrl");
                openid = Vincent._Weixin.WeixinUtility.GetOpendId();

                Vincent._Log.SaveMessage("login.aspx获取openid=" + openid);

                if (openid != "")
                {
                    BLL.users bll = new BLL.users();
                    Model.users model = bll.GetModelByOpenId(openid);

                    if (model != null && model.id > 0)
                    {
                        hMobile.Value = model.user_name;
                        hVip.Value = model.IsBuwei.ToString();
                    }

                    //model.avatar = openid;
                    //if (bll.Update(model))
                    //{
                    //    Vincent._Log.SaveMessage("openid写入数据库成功userid=" + userid + "  ------> openid=" + openid);
                    //}
                }
            }

        }
    }
}