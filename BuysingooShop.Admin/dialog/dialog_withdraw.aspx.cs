using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vincent;
namespace BuysingooShop.Admin.dialog
{
    public partial class dialog_withdraw : Web.UI.ManagePage
    {
        private string withdraw_id = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            withdraw_id = Vincent._DTcms.DTRequest.GetQueryString("withdraw_id");

            if (withdraw_id == "")
            {
                JscriptMsg("传输参数不正确！", "back", "Error");
                return;
            }
            if (!new BLL.withdraw().Exists(int.Parse(withdraw_id.ToString())))
            {
                JscriptMsg("订单不存在或已被删除！", "back", "Error");
                return;
            }
            if (!Page.IsPostBack)
            {
                ShowInfo(withdraw_id);
            }
        }

        #region 赋值操作=================================
        private void ShowInfo(string withdraw_id)
        {
            hiddOrderNo.Value = withdraw_id;

        }
        #endregion
    }
}