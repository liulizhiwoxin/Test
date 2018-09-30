using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vincent;

namespace BuysingooShop.Admin.dialog
{
    public partial class dialog_print : Web.UI.ManagePage
    {
        private string order_no = string.Empty;
        protected Model.orders model = new Model.orders();
        protected Model.manager adminModel = new Model.manager();

        protected void Page_Load(object sender, EventArgs e)
        {
            order_no = Vincent._DTcms.DTRequest.GetQueryString("order_no");
            if (order_no == "")
            {
                JscriptMsg("传输参数不正确！", "back", "Error");
                return;
            }
            if (!new BLL.orders().Exists(order_no))
            {
                JscriptMsg("订单不存在或已被删除！", "back", "Error");
                return;
            }
            if (!Page.IsPostBack)
            {
                ShowInfo(order_no);
            }
        }

        #region 赋值操作=================================
        private void ShowInfo(string _order_no)
        {
            BLL.orders bll = new BLL.orders();
            model = bll.GetModel(_order_no);
            adminModel = GetAdminInfo();
            //this.rptList.DataSource = model.order_goods;
            //this.rptList.DataBind();
        }

        protected string GetGroupName(int groupId)
        {
            string str = "推广员";
            if (groupId == 1)
            {
                str = "金卡会员";
            }
            else if (groupId == 2)
            {
                str = "钻石会员";
            }
            else if (groupId == 3)
            {
                str = "服务商";
            }
            else if (groupId == 4)
            {
                str = "市级代理";
            }
            else if (groupId == 5)
            {
                str = "公司";
            }

            return str;
        }

        /// <summary>
        /// 根据用户ID，获取用户组名
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        protected string GetGroupNameByUserId(int userId)
        {
            var managerMode = new BuysingooShop.BLL.users().GetModel(userId);

            var groupId = managerMode.group_id;

            string str = "推广员";
            if (groupId == 1)
            {
                str = "金卡会员";
            }
            else if (groupId == 2)
            {
                str = "钻石会员";
            }
            else if (groupId == 3)
            {
                str = "服务商";
            }
            else if (groupId == 4)
            {
                str = "市级代理";
            }
            else if (groupId == 5)
            {
                str = "公司";
            }

            return str;
        }

        #endregion
    }
}