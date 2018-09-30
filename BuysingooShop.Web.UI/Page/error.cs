using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Text;
using Vincent;

namespace BuysingooShop.Web.UI.Page
{
    public partial class error : System.Web.UI.Page
    {
        protected internal Model.siteconfig config = new BLL.siteconfig().loadConfig();
        protected string msg = string.Empty;

        /// <summary>
        /// 重写虚方法,此方法将在Init事件前执行
        /// </summary>
        public error()
        {
            msg = Vincent._DTcms.Utils.ToHtml(Vincent._DTcms.DTRequest.GetQueryString("msg"));
        }

    }
}
