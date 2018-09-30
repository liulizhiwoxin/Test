using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BuysingooShop.Web
{
    public partial class index : System.Web.UI.Page
    {
        public string imgUrlServer = "";    //图片服务器地址
        protected void Page_Load(object sender, EventArgs e)
        {
            imgUrlServer = System.Configuration.ConfigurationManager.AppSettings["imgUrlServer"].ToString();
        }
    }
}