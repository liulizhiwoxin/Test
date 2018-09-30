using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vincent;
using System.Data;

namespace BuysingooShop.Admin.outlet
{
    public partial class outlet_detail : System.Web.UI.Page
    {

        private int id = 0;
        private int refund_id = 0;
        protected Model.orders model = new Model.orders();
        protected Model.user_address addressModel = new Model.user_address();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.id = Vincent._DTcms.DTRequest.GetQueryInt("id", 0);
            this.refund_id = Vincent._DTcms.DTRequest.GetQueryInt("refund_id", 0);

            //如果传过来的是退货id
            if (this.id == 0)
            {
                this.id = this.refund_id;
                if (this.id == 0)
                {
                    //JscriptMsg("传输参数不正确！", "back", "Error");
                    return;
                }

                Model.refund refund = new BLL.refund().GetModel(this.id);

                if (!new BLL.orders().Exists(refund.order_no))
                {
                    //JscriptMsg("记录不存在或已被删除！", "back", "Error");
                    return;
                }
                if (!Page.IsPostBack)
                {
                     //ChkAdminLevel("order_list", Vincent._DTcms.DTEnums.ActionEnum.View.ToString()); //检查权限
                    ShowInfos(refund.order_no);
                    orderStatus();
                }

                addressModel = new BuysingooShop.BLL.user_address().GetModels(refund.user_id);
            }
            else
            {
                if (this.id == 0)
                {
                    //JscriptMsg("传输参数不正确！", "back", "Error");
                    return;
                }

                //Model.refund refund = new BLL.refund().GetModel(this.id);
                model = new BLL.orders().GetModel(this.id);

                if (model == null)
                {
                    //JscriptMsg("记录不存在或已被删除！", "back", "Error");
                    return;
                }
                if (!Page.IsPostBack)
                {
                    //ChkAdminLevel("order_list", Vincent._DTcms.DTEnums.ActionEnum.View.ToString()); //检查权限
                    ShowInfos(model.order_no);
                    orderStatus();
                }

                addressModel = new BuysingooShop.BLL.user_address().GetModels(model.user_id);
            }



        }

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            BLL.orders bll = new BLL.orders();
            model = bll.GetModel(_id);

            DataTable dt = bll.GetOrderList(0, " t1.order_no='" + _id + "'", " t1.id desc").Tables[0];

            var refund_status = 0;
            if (dt.Rows[0]["refund_status"].ToString() != "" && dt.Rows[0]["refund_status"].ToString() != null)
            {
                refund_status = int.Parse(dt.Rows[0]["refund_status"].ToString());
            }


            //绑定商品列表
            this.rptList.DataSource = model.order_goods;
            this.rptList.DataBind();
            //获得会员信息
            if (model.user_id > 0)
            {
                Model.users user_info = new BLL.users().GetModel(model.user_id);
                if (user_info != null)
                {
                    Model.user_groups group_info = new BLL.user_groups().GetModel(user_info.group_id);
                    if (group_info != null)
                    {
                        dlUserInfo.Visible = true;
                        lbUserName.Text = user_info.user_name;
                        lbUserGroup.Text = group_info.title;
                        lbUserDiscount.Text = group_info.discount.ToString() + " %";
                        lbUserAmount.Text = user_info.amount.ToString();
                        lbUserPoint.Text = user_info.point.ToString();
                    }
                }
            }

            //根据订单状态，显示各类操作按钮
            switch (int.Parse(dt.Rows[0]["status"].ToString()))
            {
                case 1: //如果是线下支付，支付状态为0，如果是线上支付，支付成功后会自动改变订单状态为已确认
                    if (int.Parse(dt.Rows[0]["payment_status"].ToString()) == 1)
                    {
                        //作废订单、修改商品总金额、修改配送费用、修改支付手续费、修改积分总计按钮、确认付款、取消订单
                        //btnInvalid.Visible = btnEditExpressFee.Visible = btnEditPaymentFee.Visible = btnPayment.Visible = btnEditRealAmount.Visible = btnCancel.Visible = true;
                    }
                    //作废订单、修改订单备注、取消订单、修改收货按钮显示显示
                    //btnInvalid.Visible = btnEditRemark.Visible = btnCancel.Visible = btnEditAcceptInfo.Visible = true;
                    break;
                case 2: //如果是DIY待确认状态
                    if (int.Parse(dt.Rows[0]["express_status"].ToString()) == 1 && refund_status == 0)
                    {
                        //作废订单、确认发货、修改订单备注按钮显示
                        //btnInvalid.Visible = btnExpress.Visible = btnEditRemark.Visible = true;
                    }
                    else if (int.Parse(dt.Rows[0]["express_status"].ToString()) == 1 && refund_status == 1)
                    {
                        //作废订单可见
                        //btnInvalid.Visible = true;
                    }
                    else if (int.Parse(dt.Rows[0]["express_status"].ToString()) == 1 && refund_status == 2)
                    {
                        //作废订单可见
                        //btnInvalid.Visible = true;
                    }
                    else if (int.Parse(dt.Rows[0]["express_status"].ToString()) == 1 && refund_status == 3)
                    {
                        //作废订单可见
                        //btnInvalid.Visible = true;
                    }
                    else if (int.Parse(dt.Rows[0]["express_status"].ToString()) == 1 && refund_status == 4)
                    {
                        //作废订单可见
                        //btnInvalid.Visible = true;
                    }
                    else if (int.Parse(dt.Rows[0]["express_status"].ToString()) == 1 && refund_status == 5)
                    {
                        //作废订单、确认发货、修改订单备注按钮显示
                        //btnInvalid.Visible = btnExpress.Visible = btnEditRemark.Visible = true;
                    }
                    else if (model.express_status == 2)
                    {
                        //作废订单、完成订单可见
                        //btnInvalid.Visible = btnComplete.Visible = true;
                    }
                    break;
            }

        }
        #endregion

        /// <summary>
        /// 赋值操作
        /// Danny
        /// </summary>
        /// <param name="_id"></param>
        private void ShowInfos(string order_no)
        {
            BLL.orders bll = new BLL.orders();
            model = bll.GetModel(order_no);

            DataTable dt = bll.GetOrderList(0, " t1.order_no='" + order_no + "'", " t1.id desc").Tables[0];

            var refund_status = 0;
            if (dt.Rows[0]["refund_status"].ToString() != "" && dt.Rows[0]["refund_status"].ToString() != null)
            {
                refund_status = int.Parse(dt.Rows[0]["refund_status"].ToString());
            }


            //绑定商品列表
            this.rptList.DataSource = model.order_goods;
            this.rptList.DataBind();
            //获得会员信息
            if (model.user_id > 0)
            {
                Model.users user_info = new BLL.users().GetModel(model.user_id);
                if (user_info != null)
                {
                    Model.user_groups group_info = new BLL.user_groups().GetModel(user_info.group_id);
                    if (group_info != null)
                    {
                        dlUserInfo.Visible = true;
                        lbUserName.Text = user_info.user_name;
                        lbUserGroup.Text = group_info.title;
                        lbUserDiscount.Text = group_info.discount.ToString() + " %";
                        lbUserAmount.Text = user_info.amount.ToString();
                        lbUserPoint.Text = user_info.point.ToString();
                        lbnick_Name.Text = user_info.nick_name;
                    }
                }
            }

            //根据订单状态，显示各类操作按钮
            switch (int.Parse(dt.Rows[0]["status"].ToString()))
            {
                case 1: //如果是线下支付，支付状态为0，如果是线上支付，支付成功后会自动改变订单状态为已确认
                    if (int.Parse(dt.Rows[0]["payment_status"].ToString()) == 1)
                    {
                        //作废订单、修改商品总金额、修改配送费用、修改支付手续费、修改积分总计按钮、确认付款、取消订单
                     //   btnInvalid.Visible = btnEditExpressFee.Visible = btnEditPaymentFee.Visible = btnPayment.Visible = btnEditRealAmount.Visible = btnCancel.Visible = true;
                    }
                    //作废订单、修改订单备注、取消订单、修改收货按钮显示显示
                 //   btnInvalid.Visible = btnEditRemark.Visible = btnCancel.Visible = btnEditAcceptInfo.Visible = true;
                    break;
                case 2: //如果是DIY待确认状态
                    if (int.Parse(dt.Rows[0]["express_status"].ToString()) == 1 && refund_status == 0)
                    {
                        //作废订单、确认发货、修改订单备注按钮显示
                        //btnInvalid.Visible = btnExpress.Visible = btnEditRemark.Visible = true;
                    }
                    else if (int.Parse(dt.Rows[0]["express_status"].ToString()) == 1 && refund_status == 1)
                    {
                        //作废订单可见
                       // btnInvalid.Visible = true;
                    }
                    else if (int.Parse(dt.Rows[0]["express_status"].ToString()) == 1 && refund_status == 2)
                    {
                        //作废订单可见
                      //  btnInvalid.Visible = true;
                    }
                    else if (int.Parse(dt.Rows[0]["express_status"].ToString()) == 1 && refund_status == 3)
                    {
                        //作废订单可见
                     //   btnInvalid.Visible = true;
                    }
                    else if (int.Parse(dt.Rows[0]["express_status"].ToString()) == 1 && refund_status == 4)
                    {
                        //作废订单可见
                       // btnInvalid.Visible = true;
                    }
                    else if (int.Parse(dt.Rows[0]["express_status"].ToString()) == 1 && refund_status == 5)
                    {
                        //作废订单、确认发货、修改订单备注按钮显示
                       // btnInvalid.Visible = btnExpress.Visible = btnEditRemark.Visible = true;
                    }
                    else if (model.express_status == 2)
                    {
                        //作废订单、完成订单可见
                        //btnInvalid.Visible = btnComplete.Visible = true;
                    }
                    break;
            }



            //根据订单状态，显示各类操作按钮
            //switch (model.status)
            //{
            //    case 1: //如果是线下支付，支付状态为0，如果是线上支付，支付成功后会自动改变订单状态为已确认
            //        if (model.payment_status == 1)
            //        {
            //            //作废订单、修改商品总金额、修改配送费用、修改支付手续费、修改积分总计按钮、确认付款、取消订单
            //            btnInvalid.Visible = btnEditExpressFee.Visible = btnEditPaymentFee.Visible = btnPayment.Visible = btnEditRealAmount.Visible = btnCancel.Visible = true;
            //        }
            //        //作废订单、修改订单备注、取消订单、修改收货按钮显示显示
            //        btnInvalid.Visible=btnEditRemark.Visible = btnCancel.Visible = btnEditAcceptInfo.Visible = true;
            //        break;
            //    case 2: //如果是DIY待确认状态
            //        if (model.express_status == 1)
            //        {
            //            //作废订单、确认发货、修改订单备注按钮显示
            //            btnInvalid.Visible=btnExpress.Visible = btnEditRemark.Visible = true;
            //        }
            //        else if (model.express_status == 2)
            //        {
            //            //作废订单、完成订单可见
            //            btnInvalid.Visible=btnComplete.Visible = true;
            //        }
            //        break;
            //}

        }

        #region 订单状态图
        /// <summary>
        /// 订单状态图
        /// </summary>
        public void orderStatus()
        {
            lt_status.Text = "<div class='order-flow' style='width:850px'>";
            if (model.status < 99)
            {
                lt_status.Text += "<div class='order-flow-left'>";
                lt_status.Text += "<a class='order-flow-input'>生成</a>";
                lt_status.Text += "<span><p class='name'>订单已生成</p><p>" + model.add_time + "</p></span></div>";

                if (model.payment_status == 1)
                {
                    lt_status.Text += "<div class='order-flow-wait'>";
                    lt_status.Text += "<a class='order-flow-input'>付款</a>";
                    lt_status.Text += "<span><p class='name'>等待付款</p></span></div>";
                }
                else if (model.payment_status == 2)
                {
                    lt_status.Text += "<div class='order-flow-arrive'>";
                    lt_status.Text += "<a class='order-flow-input'>付款</a>";
                    lt_status.Text += "<span><p class='name'>已付款</p><p>" + model.payment_time + "</p></span></div>";
                }

                if (model.express_status == 1)
                {
                    lt_status.Text += "<div class='order-flow-wait'>";
                    lt_status.Text += "<a class='order-flow-input'>发货</a>";
                    lt_status.Text += "<span><p class='name'>等待发货</p></span></div>";
                }
                else if (model.express_status == 2)
                {
                    lt_status.Text += "<div class='order-flow-arrive'>";
                    lt_status.Text += "<a class='order-flow-input'>发货</a>";
                    lt_status.Text += "<span><p class='name'>已发货</p><p>" + model.express_time + "</p></span></div>";
                }

                if (model.status == 90)
                {
                    lt_status.Text += "<div class='order-flow-right-arrive'>";
                    lt_status.Text += "<a class='order-flow-input'>完成</a>";
                    lt_status.Text += "<span><p class='name'>订单完成</p><p>" + model.complete_time + "</p></span></div>";
                }
                else
                {
                    lt_status.Text += "<div class='order-flow-right-wait'>";
                    lt_status.Text += "<a class='order-flow-input'>完成</a>";
                    lt_status.Text += "<span><p class='name'>等待完成</p></span></div>";
                }
            }
            else if (model.status == 99)
            {
                lt_status.Text += "<div style='text-align:center;line-height:30px; font-size:20px; color:Red;'>该订单已取消</div>";

            }
            else if (model.status == 100)
            {
                lt_status.Text += "<div style='text-align:center;line-height:30px; font-size:20px; color:Red;'>该订单已作废</div>";

            }
            lt_status.Text += "</div>";

        }
        #endregion
    }
}