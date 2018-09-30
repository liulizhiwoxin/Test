using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BuysingooShop.Weixin
{
    public partial class index : System.Web.UI.Page
    {        
        protected void Page_Load(object sender, EventArgs e)
        {

            Vincent._Weixin.WeixinPay weixinpay = new Vincent._Weixin.WeixinPay("sAppID", "sAppSecret", "sPartner", "sPartnerKey", "sReturnUrl");

            string strWeixin_OpenID = "";
            string strCode = Request["code"] == null ? "" : Request["code"];

            Vincent._Log.SaveMessage("分享,第一步，获取code:" + strCode);

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
                    Vincent._Log.SaveMessage("分享,获取code失败：" + retValue.Message);

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

                if (strWeixin_OpenID != "")
                {
                    var user_id = Vincent._Request.GetInt("userid", 0);
                    Vincent._Log.SaveMessage("分享页面user_id：" + user_id);

                    BLL.users bll = new BLL.users();
                    Model.users model = bll.GetModel(user_id);

                    model.safe_question = strWeixin_OpenID;
                    if (bll.Update(model))
                    {
                        Vincent._Log.SaveMessage("index.aspx---->openid写入数据库成功userid=" + user_id + "  ------> openid=" + strWeixin_OpenID);
                    }

                    Vincent._Weixin.WeixinUtility weixin = new Vincent._Weixin.WeixinUtility("sAppID", "sAppSecret");
                    var userinfo = weixin.GetWeixinUserInfo(strWeixin_OpenID);

                    // 更新客户图象信息                

                    if (user_id > 0 && userinfo.headimgurl != "" && userinfo.headimgurl != null)
                    {
                        divLogo.InnerHtml = "<img src=\"" + userinfo.headimgurl + "\" />";
                        spanRealName.InnerText = userinfo.nickname;

                        var outmsgId = new BLL.users().UpdateHeadImageUrl(user_id, userinfo.headimgurl);
                        Vincent._Log.SaveMessage("更新会员(会员id=" + user_id + ")头像," + outmsgId);
                    }

                    Vincent._Log.SaveMessage("图象地址：" + userinfo.headimgurl);
                }
            }

            



            //var userid = Vincent._Request.GetString("userid");
            //string openid = "";
            
            //Vincent._Weixin.WeixinPay weixinpay = new Vincent._Weixin.WeixinPay("sAppID", "sAppSecret", "sPartner", "sPartnerKey", "sReturnUrl");
            //openid = Vincent._Weixin.WeixinUtility.GetOpendId();

            //if (openid != "" && Vincent._Convert.ToInt(userid) > 0)
            //{
            //    BLL.users bll = new BLL.users();
            //    Model.users model = bll.GetModel(Vincent._Convert.ToInt(userid));

            //    model.safe_question = openid;
            //    if (bll.Update(model))
            //    {
            //        Vincent._Log.SaveMessage("index.aspx---->openid写入数据库成功userid=" + userid + "  ------> openid=" + openid);
            //    }


            //    Vincent._Weixin.WeixinUtility weixin = new Vincent._Weixin.WeixinUtility("sAppID", "sAppSecret");
            //    var userinfo = weixin.GetWeixinUserInfo(openid);

            //    // 更新客户图象信息
            //    var user_id = Vincent._Request.GetInt("userid", 0);
            //    Vincent._Log.SaveMessage("分享页面user_id：" + user_id);

            //    if (user_id > 0 && userinfo.headimgurl != "" && userinfo.headimgurl != null)
            //    {
            //        divLogo.InnerHtml = "<img src=\"" + userinfo.headimgurl + "\" />";
            //        spanRealName.InnerText = userinfo.nickname;

            //        var outmsgId = new BLL.users().UpdateHeadImageUrl(user_id, userinfo.headimgurl);
            //        Vincent._Log.SaveMessage("更新会员(会员id=" + user_id + ")头像," + outmsgId);
            //    }
            //    //SummitLocalStorage.LocalStorage.get({ key: "summit" });

            //    Vincent._Log.SaveMessage("图象地址：" + userinfo.headimgurl);
            //    //hOpenID.Value = strWeixin_OpenID;

            //}


        }

        
    }
}