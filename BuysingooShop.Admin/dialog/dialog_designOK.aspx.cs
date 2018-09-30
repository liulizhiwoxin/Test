using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vincent;

namespace BuysingooShop.Admin.dialog
{
    public partial class dialog_designOK : Web.UI.ManagePage
    {
        private string order_no = string.Empty;
        public Model.orders model = new Model.orders();
        protected BLL.orders bll = new BLL.orders();

        protected void Page_Load(object sender, EventArgs e)
        {
            order_no = Vincent._DTcms.DTRequest.GetQueryString("orderNo");
            hiddNO.Value = order_no;
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
                model = bll.GetModel(order_no);
            }
        }

        //确认设计
        protected void btnOK_Click(object sender, EventArgs e)
        {
            string orderNO = hiddNO.Value;
            if (orderNO == "")
            {
                JscriptMsg("传输参数不正确！", "back", "Error");
                return;
            }
            if (!new BLL.orders().Exists(orderNO))
            {
                JscriptMsg("订单不存在或已被删除！", "back", "Error");
                return;
            }
            model = bll.GetModel(orderNO);
            model.status = 3;
            model.confirm_time = DateTime.Now;
            if (bll.Update(model))
            {
                JscriptMsg("DIY设计确认成功！", "back", "success");
            }
            else
            {
                JscriptMsg("DIY设计确认失败！", "back", "Error");
            }
            //if (bll.Update(model))
            //{
            //    Response.Write("{\"status\":\"1\",\"msg\":\"DIY设计确认成功！\"}");
            //}
            //else
            //{
            //    Response.Write("{\"status\":\"0\",\"msg\":\"DIY设计确认失败！\"}");
            //}

        }
         
    }
}