using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BuysingooShop.Admin.dialog
{
    public partial class dialog_complian : Web.UI.ManagePage
    {
        protected Model.complian model = new Model.complian();
        protected void Page_Load(object sender, EventArgs e)
        {
            model = new BuysingooShop.BLL.complian().GetModel(Vincent._DTcms.DTRequest.GetQueryInt("id"));
        }
    }
}