using BuysingooShop.Api.Common;
using BuysingooShop.Api.Common.CommonHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BuysingooShop.Api.Controllers;

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
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        //swagger 文档中不显示这个接口
        [ApiExplorerSettings(IgnoreApi = true)]
        public string GetUserProof(string code)
        {
            try
            {
                UserLogin UserProof = new UserHelper().GetOpenId(code);
                //为了数据安全，不对外提供这个密钥
                UserProof.session_key = null;
                return UserProof.openid;
            }
            catch (Exception e)
            {
                return (e.Message);
            }
        }

    }
}
