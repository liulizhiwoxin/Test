using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuysingooShop.Model
{
    /// <summary>
    /// 会员 火种详情表
    /// </summary>
    [Serializable]
    public partial class applet_kindling
    {
        public applet_kindling() { }

        /// <summary>
        /// 会员，被助力的会员 openid
        /// </summary>
        public string user_openid { get; set; }

        /// <summary>
        /// 助力者 的 openid
        /// </summary>
        public string help_openid { get; set; }

    }
}
