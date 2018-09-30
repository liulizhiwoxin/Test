using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vincent;

namespace BuysingooShop.Admin
{
    public partial class index : Web.UI.ManagePage
    {
        protected Model.manager admin_info;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                admin_info = GetAdminInfo();
            }
        }

        //安全退出
        protected void lbtnExit_Click(object sender, EventArgs e)
        {
            Session[Vincent._DTcms.DTKeys.SESSION_ADMIN_INFO] = null;
            Vincent._DTcms.Utils.WriteCookie("AdminName", "SimpleLife", -14400);
            Vincent._DTcms.Utils.WriteCookie("AdminPwd", "SimpleLife", -14400);
            Response.Redirect("/login.aspx");
        }

    }
}