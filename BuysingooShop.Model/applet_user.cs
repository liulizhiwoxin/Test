using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuysingooShop.Model
{
    /// <summary>
    /// 微信小程序-用户表
    /// </summary>
    [Serializable]
    public partial class applet_user
    {
        public applet_user(){}
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string user_name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string user_sex { get; set; }

        /// <summary>
        /// 国家
        /// </summary>
        public string user_country { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        public string user_province { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string user_city { get; set; }

        /// <summary>
        /// 头像路径
        /// </summary>
        public string user_avatarUrl { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime add_time { get; set; }

        /// <summary>
        /// 用户唯一标示 openid
        /// </summary>
        public string user_openid { get; set; }

        /// <summary>
        /// 用户火种数
        /// </summary>
        public double user_kindling_num { get; set; }
    }
}
