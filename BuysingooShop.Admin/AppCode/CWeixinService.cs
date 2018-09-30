using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Service/WeixinServiceHandler.ashx类文件
/// </summary>
/// 
namespace BuysingooShop.Web.AppCode
{
    public class CWeixinService
    {
        public CWeixinService()
        {
        }

        #region 生成二维码图片

        /// <summary>
        /// 生成二维码图片
        /// </summary>
        public static void Qrcode()
        {
            string json = "";

            var userid = Vincent._Request.GetInt("userid", 0);  //用户ID
            if (userid > 0)
            {
                var ticket_url = Vincent._WebConfig.GetAppSettingsString("ticket_url");
                var access_token = IsExistAccess_Token();
                var postDataStr = "{\"action_name\": \"QR_LIMIT_SCENE\", \"action_info\": {\"scene\": {\"scene_id\": " + userid + "}}}";

                var ticket_json = Vincent._WebHttp.HttpPost(ticket_url + "?access_token=" + access_token, postDataStr);
                //System.Data.DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(ticket_json);

                //URL: https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token=TOKEN
                //POST数据格式：json
                //POST数据例子：{"action_name": "QR_LIMIT_SCENE", "action_info": {"scene": {"scene_id": 123}}}

                //正确的Json返回结果: 
                //{"ticket":"gQH47joAAAAAAAAAASxodHRwOi8vd2VpeGluLnFxLmNvbS9xL2taZ2Z3TVRtNzJXV1Brb3ZhYmJJAAIEZ23sUwMEmm3sUw==","expire_seconds":60,"url":"http:\/\/weixin.qq.com\/q\/kZgfwMTm72WWPkovabbI"}
                //错误的Json返回示例: 
                //{"errcode":40013,"errmsg":"invalid appid"}

                string ticket = ticket_json.Substring(11, ticket_json.IndexOf(",") - 12);

                //下载二维码
                //https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket=TICKET
                var showqrcode_url = Vincent._WebConfig.GetAppSettingsString("showqrcode_url");

                if (ticket.Contains("errcode"))
                {
                    json = "{\"Response\":\" error\",\"msg\":2}";
                }
                else
                {
                    if (ticket == "")
                        json = "{\"Response\":\"" + showqrcode_url + "?ticket=" + ticket + "\",\"msg\":0}";
                    else
                        json = "{\"Response\":\"" + showqrcode_url + "?ticket=" + ticket + "\",\"msg\":1}";
                }
            }

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write(json);
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 根据当前日期 判断Access_Token 是否超期  如果超期返回新的Access_Token   否则返回之前的Access_Token
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static string IsExistAccess_Token()
        {
            string Token = string.Empty;
            DateTime YouXRQ;

            var temp = Vincent._Utility.GetMapPath("site.config");

            System.Xml.XmlDocument doc = Vincent._Xml.LoadXmlDoc(Vincent._Utility.GetMapPath("site.config"));
            Token = doc.SelectSingleNode(@"/siteconfig/accesstoken").InnerText;
            var access_token_time = doc.SelectSingleNode(@"/siteconfig/accesstoken_time").InnerText;
            YouXRQ = Vincent._Convert.ToDateTime(access_token_time, DateTime.Now);

            if (DateTime.Now > YouXRQ)
            {
                DateTime _youxrq = DateTime.Now;
                _youxrq = _youxrq.AddSeconds(3600);

                //修改
                Token = GetAccessToken();
                string filePath = Vincent._Utility.GetMapPath("site.config");
                Vincent._Xml.UpdateNodeInnerText(filePath, @"/siteconfig/accesstoken", Token);
                Vincent._Xml.UpdateNodeInnerText(filePath, @"/siteconfig/accesstoken_time", _youxrq.ToString());
            }
            return Token;
        }

        /// <summary>
        /// 获取ticket
        /// </summary>
        /// <returns></returns>
        private static string GetAccessToken()
        {
            var newAccessToken = "";

            //<add key="sToken" value="dydpcb" />
            //<add key="sAppID" value="wx1b56795b0b198498" />
            //<add key="sAppSecret" value="8bba2ffc1ed7293acff5eefb9eb8a4df" />
            //<add key="sPartnerKey" value="0E49C67DFB7586506C27D3F89FAA9BAA" />
            //<add key="sPartner" value="1371619902" />
            //<add key="sEncodingAESKey" value="xPeNagi5vPpVuBh8hX3UwWcz4Dy5tYltQDZALYWPRy8" />

            //更新动做
            var AppID = Vincent._WebConfig.GetAppSettingsString("sAppID");
            var AppSecret = Vincent._WebConfig.GetAppSettingsString("sAppSecret");
            var AppSecret_Url = Vincent._WebConfig.GetAppSettingsString("sAppSecret_Url");
            var postDataStr = "grant_type=client_credential&appid=" + AppID + "&secret=" + AppSecret;

            //{"access_token":"WiRJVgYRpELlVPecvmQZd7_0o_U6PGmVBgzXaAgOuH86cxOqYEnOR--FInos54I4axKyUuW3Sy1W4ln4hhYoMYRbkic3_41vmdZ1uQVhcpg","expires_in":7200}
            var json = Vincent._WebHttp.HttpGet(AppSecret_Url, postDataStr);
            if (json.Contains("errcode"))
            {
                Vincent._Log.SaveMessage(AppSecret_Url + "\r\n" + json);
            }
            else
            {
                newAccessToken = json.Substring(17, json.IndexOf(",") - 18);
            }

            return newAccessToken;
        }

        #endregion

    }

}