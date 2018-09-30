using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using BuysingooShop.BLL;
namespace BuysingooShop.Admin.finance
{
    public partial class total_shopinfo : Web.UI.ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {



        }
         
        #region 返回总交易量=============================
        protected string GetUserAmount()
        {
            string amount = string.Empty;
            BLL.orders bll = new BLL.orders();
            DataTable dt = bll.GetSellTotal().Tables[0];
            if (dt.Rows.Count > 0)
            {
                amount = dt.Rows[0]["order_amount"].ToString();
            }
            else
            {
                amount = "0";
            }

            return amount;
        }
        #endregion
    }
}
