using BuysingooShop.Web.AppCode;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Vincent;

namespace BuysingooShop.Web.weixin
{
    public partial class weixin : System.Web.UI.Page
    {
        public string Token = Vincent._WebConfig.GetAppSettingsString("sToken");//你的token

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Vincent._WebConfig.GetAppSettingsString("IsMenu") == "1")
            //MyMenu();     //自定义菜单

            //wxmessage wx = GetWxMessage();
            //Vincent._Log.SaveMessage("客户微信：" + wx.FromUserName + " 公众号微信：" + wx.ToUserName); //ToUserName 公众号微信ID FromUserName 客户微信号ID

            ////HttpContext.Current.Session["FromUserName"] = wx.FromUserName;

            //string res = "";

            ////用户未关注
            //if (!string.IsNullOrEmpty(wx.EventName) && wx.EventName.Trim() == "subscribe")
            //{
            //    string content = "";
            //    if (!wx.EventKey.Contains("qrscene_"))
            //    {
            //        content = "/:rose欢迎关注钓鱼岛科技/:rose\n";
            //        res = sendTextMessage(wx, content);
            //    }
            //    else
            //    {
            //        content = "注册有礼";// +wx.EventKey.Replace("qrscene_", "");
            //        content += "\n<a href=\"" + Vincent._WebConfig.GetAppSettingsString("web_url") + "bind.htm?fromurl=0&wxno=" + wx.FromUserName + "&userid=" + wx.EventKey.Replace("qrscene_", "") + "\">立即注册成为会员</a>";
            //        res = sendTextMessage(wx, content);
            //    }
            //}
            ////用户已关注
            //else if (!string.IsNullOrEmpty(wx.EventName) && wx.EventName.ToLower() == "scan")
            //{
            //    //判断cookie中是否已经存在该用户信息

            //    string str = "注册有礼";// +wx.EventKey;
            //    str += "\n<a href=\"" + Vincent._WebConfig.GetAppSettingsString("web_url") + "bind.htm?fromurl=0&wxno=" + wx.FromUserName + "&userid=" + wx.EventKey + "\">立即注册成为会员</a>";
            //    res = sendTextMessage(wx, str);
            //}


            //Vincent._Log.SaveMessage(res);
            //Response.Write(res);

            if (!IsPostBack)
            {
                ActiveWeiXin(); //激活微信公众号
            }
        }


        #region 自定义菜单

        public void MyMenu()
        {
            string weixin1 = "";
            weixin1 = @" {
             ""button"":[     
                   {
                       ""name"":""业务中心"",
                       ""sub_button"":[
                        {
                            ""type"":""view"",
                            ""name"":""在线计价"",
                            ""url"":""http://wx.4008607888.cn/onlineorder.htm""
                        },
                        {
                            ""type"":""view"",
                            ""name"":""订单查询"",
                            ""url"":""http://wx.4008607888.cn/Ajax/HandlerWeixin.ashx?param=OrderList""
                        }
                        
                        ]
                    },
                    {
                       ""name"":""用户中心"",
                       ""sub_button"":[  
                        {
                            ""type"":""view"",
                            ""name"":""帐号绑定"",
                            ""url"":""http://wx.4008607888.cn/Ajax/HandlerWeixin.ashx?param=Bind""
                        },
                        {
                            ""type"":""view"",
                            ""name"":""我的积分"",
                            ""url"":""http://wx.4008607888.cn/mypoint.htm""
                        },
                        {
                            ""type"":""view"",
                            ""name"":""生产进度"",
                            ""url"":""http://wx.4008607888.cn/myproduction.htm""
                        },
                        {
                            ""type"":""view"",
                            ""name"":""发货查询"",
                            ""url"":""http://wx.4008607888.cn/myselect.htm""
                        },
                        {
                            ""type"":""view"",
                            ""name"":""支付测试"",                            
                            ""url"":""https://open.weixin.qq.com/connect/oauth2/authorize?appid=wx68fa302be9a19116&scope=snsapi_base&state=STATE&redirect_uri=http%3A%2F%2Fwx.4008607888.cn%2FWeixinPay%2FJSAPI_PAY_Test.aspx&response_type=code&connect_redirect=1#wechat_redirect""
                        }
                        ]
                    },
                    {    
                       ""name"":""展示中心"",
                       ""sub_button"":[
                        {
                           ""type"":""view"",
                           ""name"":""样板展示"",
                            ""url"":""http://wx.4008607888.cn/modelshow.htm""
                        },
                        {
                           ""type"":""view"",
                           ""name"":""工厂展示"",
                           ""url"":""http://wx.4008607888.cn/equipmentshow.htm""
                        },
                        {
                           ""type"":""view"",
                           ""name"":""工艺展示"",
                            ""url"":""http://wx.4008607888.cn/processshow.htm""
                        },
                        {
                           ""type"":""view"",
                           ""name"":""关于我们"",
                            ""url"":""http://wx.4008607888.cn/company.htm""
                        },
                        {
                           ""type"":""view"",
                           ""name"":""联系我们"",
                            ""url"":""http://wx.4008607888.cn/contactus.htm""
                        }
                        ]
                   }
                ]
            }";

            var accessToken = CWeixinService.IsExistAccess_Token();

            string i = GetPage("https://api.weixin.qq.com/cgi-bin/menu/create?access_token=" + accessToken, weixin1);

            _Log.SaveMessage("自定义菜单:" + "https://api.weixin.qq.com/cgi-bin/menu/create?access_token=" + accessToken + " 返回:" + i);
            Response.Write(i);
        }

        //{
        //                       ""type"":""view"",
        //                       ""name"":""随叫随到"",
        //                        ""url"":""https://open.weixin.qq.com/connect/oauth2/authorize?appid=wx20897a3c7e992bf4&redirect_uri=http://wx.tuibeigan.cn/Ajax/Index.ashx?param=viewnowcar&response_type=code&scope=snsapi_base&state=1#wechat_redirect""
        //                    }, 


        //可以通用!!
        public string GetPage(string posturl, string postData)
        {
            Stream outstream = null;
            Stream instream = null;
            StreamReader sr = null;
            HttpWebResponse response = null;
            HttpWebRequest request = null;
            Encoding encoding = Encoding.UTF8;
            byte[] data = encoding.GetBytes(postData);
            // 准备请求...
            try
            {
                // 设置参数
                request = WebRequest.Create(posturl) as HttpWebRequest;
                CookieContainer cookieContainer = new CookieContainer();
                request.CookieContainer = cookieContainer;
                request.AllowAutoRedirect = true;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                outstream = request.GetRequestStream();
                outstream.Write(data, 0, data.Length);
                outstream.Close();
                //发送请求并获取相应回应数据
                response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                instream = response.GetResponseStream();
                sr = new StreamReader(instream, encoding);
                //返回结果网页（html）代码
                string content = sr.ReadToEnd();
                string err = string.Empty;
                return content;
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                Response.Write(err);
                return string.Empty;
            }
        }


        #endregion

        #region 激活微信公众号

        /// <summary>
        /// 确认激活
        /// </summary>
        private void ActiveWeiXin()
        {
            string postStr = "";

            if (Request.HttpMethod.ToLower() == "post")
            {
                System.IO.Stream s = System.Web.HttpContext.Current.Request.InputStream;

                byte[] b = new byte[s.Length];
                s.Read(b, 0, (int)s.Length);
                postStr = System.Text.Encoding.UTF8.GetString(b);

                if (!string.IsNullOrEmpty(postStr))
                {
                    //ResponseMsg(postStr);
                    Response.Write(ResponseMsg(postStr));
                    Response.End();
                }

                //WriteLog("postStr:" + postStr);

            }

            else
            {
                Valid();
            }
        }

        /// <summary>
        /// 验证微信签名
        /// </summary>
        /// * 将token、timestamp、nonce三个参数进行字典序排序
        /// * 将三个参数字符串拼接成一个字符串进行sha1加密
        /// * 开发者获得加密后的字符串可与signature对比，标识该请求来源于微信。
        /// <returns></returns>
        private bool CheckSignature()
        {
            string signature = Request.QueryString["signature"].ToString();
            string timestamp = Request.QueryString["timestamp"].ToString();
            string nonce = Request.QueryString["nonce"].ToString();

            string[] ArrTmp = { Token, timestamp, nonce };
            Array.Sort(ArrTmp);     //字典排序
            string tmpStr = string.Join("", ArrTmp);
            tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
            tmpStr = tmpStr.ToLower();

            if (tmpStr == signature)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Valid()
        {
            string echoStr = Request.QueryString["echoStr"].ToString();
            if (CheckSignature())
            {
                if (!string.IsNullOrEmpty(echoStr))
                {
                    Response.Write(echoStr);
                    Response.End();
                }
            }
        }

        /// <summary>
        /// 返回信息结果(微信信息返回)
        /// </summary>
        /// <param name="weixinXML"></param>
        private string ResponseMsg(string weixinXML)
        {
            return "";  //这里写你的返回信息代码
        }

        /// <summary>
        /// unix时间转换为datetime
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        private DateTime UnixTimeToTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);

            return dtStart.Add(toNow);
        }

        /// <summary>
        /// datetime转换为unixtime
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }

        #endregion

        #region 接收扫描二维码过来的消息

        //自定义一个微信消息实体类  
        class wxmessage
        {
            public string FromUserName { get; set; }
            public string ToUserName { get; set; }
            public string MsgType { get; set; }
            public string EventName { get; set; }
            public string Content { get; set; }
            public string Recognition { get; set; }
            public string MediaId { get; set; }
            public string EventKey { get; set; }
            public string Location_X { get; set; }
            public string Location_Y { get; set; }
            public string Scale { get; set; }
            public string Label { get; set; }
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public string Precision { get; set; }

        }

        /// <summary>
        /// 接收微信消息
        /// </summary>
        /// <returns></returns>
        private wxmessage GetWxMessage()
        {
            wxmessage wx = new wxmessage();
            StreamReader str = new StreamReader(Request.InputStream, System.Text.Encoding.UTF8);
            XmlDocument xml = new XmlDocument();
            xml.Load(str);
            wx.ToUserName = xml.SelectSingleNode("xml").SelectSingleNode("ToUserName").InnerText;
            wx.FromUserName = xml.SelectSingleNode("xml").SelectSingleNode("FromUserName").InnerText;
            wx.MsgType = xml.SelectSingleNode("xml").SelectSingleNode("MsgType").InnerText;
            _Log.SaveMessage("MsgType:" + wx.MsgType);
            if (wx.MsgType.Trim() == "event")
            {
                wx.EventName = xml.SelectSingleNode("xml").SelectSingleNode("Event").InnerText;
                _Log.SaveMessage(wx.EventName);
                if (wx.EventName.ToUpper() == "LOCATION")
                {
                    wx.Latitude = xml.SelectSingleNode("xml").SelectSingleNode("Latitude").InnerText;
                    wx.Longitude = xml.SelectSingleNode("xml").SelectSingleNode("Longitude").InnerText;
                    wx.Precision = xml.SelectSingleNode("xml").SelectSingleNode("Precision").InnerText;
                }
                else
                {
                    wx.EventKey = xml.SelectSingleNode("xml").SelectSingleNode("EventKey").InnerText;
                }
            }
            if (wx.MsgType.Trim() == "text")
            {
                wx.Content = xml.SelectSingleNode("xml").SelectSingleNode("Content").InnerText;
            }
            if (wx.MsgType.Trim() == "location")
            {
                wx.Location_X = xml.SelectSingleNode("xml").SelectSingleNode("Location_X").InnerText;
                wx.Location_Y = xml.SelectSingleNode("xml").SelectSingleNode("Location_Y").InnerText;
                wx.Scale = xml.SelectSingleNode("xml").SelectSingleNode("Scale").InnerText;
                wx.Label = xml.SelectSingleNode("xml").SelectSingleNode("Label").InnerText;

            }

            if (wx.MsgType.Trim() == "voice")
            {
                wx.Recognition = xml.SelectSingleNode("xml").SelectSingleNode("Recognition").InnerText;
            }

            return wx;
        }

        /// <summary>  
        /// 发送文字消息  
        /// </summary>  
        /// <param name="wx">获取的收发者信息</param>  
        /// <param name="content">内容</param>  
        /// <returns></returns>  
        private string sendTextMessage(wxmessage wx, string content)
        {
            string res = string.Format(@"<xml>
                                   <ToUserName><![CDATA[{0}]]></ToUserName>
                                   <FromUserName><![CDATA[{1}]]></FromUserName>
                                    <CreateTime>{2}</CreateTime>
                                    <MsgType><![CDATA[text]]></MsgType>
                                    <Content><![CDATA[{3}]]></Content>
                                   </xml> ",
                wx.ToUserName, wx.FromUserName, DateTime.Now, content);
            return res;
        }


        #endregion
    }
}