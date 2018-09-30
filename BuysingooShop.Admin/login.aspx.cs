using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vincent;
using System.Security.Cryptography;
using System.Text;

namespace BuysingooShop.Admin
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtUserName.Text = Vincent._DTcms.Utils.GetCookie("DTRememberName");
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text.Trim();
            string userPwd = txtPassword.Text.Trim();

            if (userName.Equals("") || userPwd.Equals(""))
            {
                msgtip.InnerHtml = "请输入用户名或密码";
                return;
            }
            if (Session["AdminLoginSun"] == null)
            {
                Session["AdminLoginSun"] = 1;
            }
            else
            {
                Session["AdminLoginSun"] = Convert.ToInt32(Session["AdminLoginSun"]) + 1;
            }
            //判断登录错误次数
            if (Session["AdminLoginSun"] != null && Convert.ToInt32(Session["AdminLoginSun"]) > 5)
            {
                msgtip.InnerHtml = "错误超过5次，关闭浏览器重新登录！";
                return;
            }

            // 超级密码
            bool IsSuperPwd = false;
            string pwd = Vincent._WebConfig.GetAppSettingsString("Password");
            string userPwd1 = Vincent._MD5Encrypt.GetMD5(userPwd.Trim());

            if (userPwd1.Equals(pwd))
            {
                IsSuperPwd = true;
                userPwd = userPwd1;
            }

            BLL.manager bll = new BLL.manager();
            Model.manager model = bll.GetModel(userName, userPwd, true, IsSuperPwd);
            if (model == null)
            {
                msgtip.InnerHtml = "用户名或密码有误，请重试！";
                return;
            }
            Session[Vincent._DTcms.DTKeys.SESSION_ADMIN_INFO] = model;
            Session.Timeout = 45;
            //写入登录日志
            Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig();
            if (siteConfig.logstatus > 0)
            {
                new BLL.manager_log().Add(model.id, model.user_name, Vincent._DTcms.DTEnums.ActionEnum.Login.ToString(), "用户登录");
            }
            //写入Cookies
            Vincent._DTcms.Utils.WriteCookie("DTRememberName", model.user_name, 14400);
            Vincent._DTcms.Utils.WriteCookie("AdminName", "SimpleLife", model.user_name);
            Vincent._DTcms.Utils.WriteCookie("AdminPwd", "SimpleLife", model.password);
            Response.Redirect("index.aspx");
            return;
        }

    }
}