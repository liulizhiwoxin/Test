using BuysingooShop.Api.Common;
using BuysingooShop.Api.Common.CommonHelpers;
using LitJson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace BuysingooShop.Api.Controllers
{
    /// <summary>
    /// 主控制器
    /// </summary>
    public class MarkController : ApiController
    {

        [HttpGet]
        public List<string> Test(string name) {
            return new List<string>() {
                "yangkun",
                "zhoalong",
                 name
            };
        }
        /// <summary>
        /// 获取用户凭证
        /// </summary>
        /// <param name="code">微信用户code</param>
        /// <param name="nickname">微信昵称</param>
        /// <param name="sex">性别</param>
        /// <param name="country">国家</param>
        /// <param name="province">省份</param>
        /// <param name="city">城市</param>
        /// <param name="avatarUrl">用户头像路径</param>
        /// <returns>返回用户的openid</returns>
        [HttpGet]
        //swagger 文档中不显示这个接口
        [ApiExplorerSettings(IgnoreApi = true)]
        public string GetUserProof(string code, string nickname, string sex, string country, string province, string city, string avatarUrl)
        {
            
            try
            {
                UserLogin UserProof = new UserHelper().GetOpenId(code);
                //为了数据安全，不对外提供这个密钥
                UserProof.session_key = null;

                if(UserProof.openid=="" || UserProof.openid==null)
                {
                    return "openid为空！";
                }

                //添加用户信息到数据库
                sex = sex == "1" ? "男" : "女";
                Model.applet_user applet_User = new Model.applet_user();
                applet_User.user_name = nickname;
                applet_User.user_openid = UserProof.openid;
                applet_User.user_avatarUrl = avatarUrl;
                applet_User.user_city = city;
                applet_User.user_country = country;
                applet_User.user_province = province;
                applet_User.user_sex = sex;
                applet_User.user_kindling_num = 1;

                string message = "";
                BLL.applet_user user = new BLL.applet_user();
                //查询当前openid  是否存在
                if (!user.Exists_openid(UserProof.openid))
                {
                    //添加
                    int row = user.Add(applet_User);
                    message = "点燃成功";
                }
                else
                {
                    //查询用户火苗数是否为1
                    if (user.GetList_user_kindling_num("user_openid='"+ UserProof.openid + "'", "id asc") == 1)
                    {
                        //已经自己点燃过火种
                        message = "已点燃";
                    }
                    else
                    {
                        //为点燃火种，点燃火种
                        if (user.Update("user_kindling_num=1", "user_openid=" + UserProof.openid + ""))
                        {
                            message = "点燃成功";
                        }
                        else
                        {
                            message = "点燃失败";
                        }
                    }
                }

                object dataobj = new
                {
                    UserProof.openid,
                    message
                };
                string json = JsonConvert.SerializeObject(dataobj); 
                return json;
            }
            catch (Exception e)
            {
                return (e.Message);
            }
        }
        /// <summary>
        /// 助力接口
        /// </summary>
        /// <param name="user_openid">被助力者的openid</param>
        /// <param name="code">助力者的code</param>
        /// <param name="nickname">助力者的微信昵称</param>
        /// <param name="sex">助力者的性别</param>
        /// <param name="country">助力者的国家</param>
        /// <param name="province">助力者的省份</param>
        /// <param name="city">助力者的城市</param>
        /// <param name="avatarUrl">助力者的用户头像路径</param>
        /// <returns>true/false</returns>
        [HttpGet]
        public string help(string user_openid,string code, string nickname, string sex, string country, string province, string city, string avatarUrl)
        {
            string Message = "";
            try
            {
                UserLogin UserProof = new UserHelper().GetOpenId(code);
                //为了数据安全，不对外提供这个密钥
                UserProof.session_key = null;

                //添加用户信息到数据库

                sex = sex == "1" ? "男" : "女";
                Model.applet_user applet_User = new Model.applet_user();
                applet_User.user_name = nickname;
                applet_User.user_openid = UserProof.openid;
                applet_User.user_avatarUrl = avatarUrl;
                applet_User.user_city = city;
                applet_User.user_country = country;
                applet_User.user_province = province;
                applet_User.user_sex = sex;
                applet_User.user_kindling_num = 0;

                BLL.applet_user user = new BLL.applet_user();

                //查询当前openid  是否存在
                if (!user.Exists_openid(UserProof.openid))
                {
                    //添加
                    int row = user.Add(applet_User);
                }
                
                //为好友助力



                return UserProof.openid;
            }
            catch (Exception e)
            {
                return (e.Message);
            }


            return Message;
        }
            



    }
}
