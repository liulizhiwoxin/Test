using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using BuysingooShop.Api.Common;
using BuysingooShop.Api.Common.CommonTools;
using System.Configuration;

namespace BuysingooShop.Api.Common.CommonHelpers
{
    public class UserHelper
    {
        /// <summary>
        /// 登录凭证校验
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public UserLogin GetOpenId(string code)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(code))
                    throw new Exception("缺少参数code");
                //获取web.config配置文件内容
                string appid = ConfigurationManager.AppSettings["appid"].ToString();
                string secret = ConfigurationManager.AppSettings["secret"].ToString();
                string grant_type= ConfigurationManager.AppSettings["grant_type"].ToString();

                string ApiUrl = $"https://api.weixin.qq.com/sns/jscode2session?appid="+appid+"&secret="+secret+"&js_code="+code+"&grant_type="+ grant_type + "";
                //请求失败重试，三次后放弃
                string GetResult = "";
                for (int i = 0; i < 3; i++)
                {
                    try
                    {
                        GetResult = HttpHelper.Request(ApiUrl);
                        if (!string.IsNullOrEmpty(GetResult))
                            break;
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
                UserLogin Result = JsonHelper.ParseFormJson<UserLogin>(GetResult);
                if (Result != null)
                {
                    return Result;
                }
                throw new Exception("获取用户信息失败");
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        
    }
}