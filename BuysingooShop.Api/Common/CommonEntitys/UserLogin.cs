using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuysingooShop.Api.Common
{
    public class UserLogin
    {
        /// <summary>
        /// 用户唯一标识
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 会话密钥
        /// </summary>
        public string session_key { get; set; }

        /// <summary>
        /// 用户在开放平台的唯一标识符
        /// </summary>
        public string unionid { get; set; }
    }
}