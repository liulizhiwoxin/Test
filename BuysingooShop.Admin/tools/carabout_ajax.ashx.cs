using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using BuysingooShop.BLL;
using Vincent;
using BuysingooShop.Model;
using BuysingooShop.Web.UI;
using order_goods = BuysingooShop.Model.order_goods;


namespace BuysingooShop.Web.tools
{
    /// <summary>
    /// carabout_ajax 的摘要说明
    /// </summary>
    public class carabout_ajax : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            //取得处事类型
            string action = Vincent._DTcms.DTRequest.GetQueryString("action");

            switch (action)
            {
                case "checkstrCode":
                    GetCheckCode(context);//检查折扣码是否可用
                    break;
                case "checkcoupon":
                    GetCheckCoupon(context);//检查优惠券是否可用
                    break;
                case "submit_order":
                    GetSubmitOrder(context);//提交订单
                    break;
                case "submit_withdraw":
                    submit_withdraw(context);//提现
                    break;
                case "submit_order_account":
                    GetSubmitOrderAccount(context);//账户余额支付
                    break;
                case "submit_recharge":
                    GetSubmitRecharge(context);//提交充值
                    break;
                case "cancel_order":
                    CancelOrder(context);//取消订单
                    break;
                case "orderComment":
                    orderComment(context);//获取要评价的商品列表
                    break;
                case "validate_str_code":
                    validate_str_code(context);//验证优惠券
                    break;
                case "goodsComment":
                    goodsComment(context);//提交评论
                    break;
                case "deleteComment":
                    deleteComment(context);//删除评论
                    break;
                case "delete_good":
                    GetDelete_good(context);//删除购物车的商品
                    break;
                case "submit_consult":
                    SubmitGoodsConsult(context);//提交商品咨询
                    break;
                case "getPageConsult":
                    GetPageConsult(context);//获取咨询分页数据
                    break;
                case "getPageComment":
                    GetPageComment(context);//获取评论分页数据
                    break;
                case "getPageMonthSell":
                    GetPageMonthSell(context);//获取月销量数据
                    break;
                case "submit_pay":
                    SubmitPay(context);//提交支付
                    break;
                case "resetPwd":
                    ResetPwd(context);//修改密码
                    break;
                case "editUserInfo"://修改用户信息
                    EditUserInfo(context);
                    break;
            }
        }

        #region 检查折扣码是否可用===============================
        /// <summary>
        /// 检查折扣码是否可用
        /// </summary>
        /// <param name="context"></param>
        public void GetCheckCode(HttpContext context)
        {
            string strcode = Vincent._DTcms.DTRequest.GetString("strCode");
            string brandid = Vincent._DTcms.DTRequest.GetString("brandId");
            string strWhere = string.Format("str_code='{0}' AND brand_id={1}", strcode, brandid);
            BuysingooShop.Model.manager modelcode = new BuysingooShop.BLL.manager().GetModelCode(strWhere);
            if (strcode == "")
            {
                context.Response.Write("{\"status\":0,\"msg\":\"请输入折扣码信息！\"}");
                return;
            }
            //取得用户登录信息
            BuysingooShop.Model.users usersinfo = context.Session[Vincent._DTcms.DTKeys.SESSION_USER_INFO] as BuysingooShop.Model.users;
            if (usersinfo == null)
            {
                context.Response.Write("{\"status\":0,\"msg\":\"你还未登陆，请先登录！\"}");
                return;
            }
            if (modelcode == null)
            {
                context.Response.Write("{\"status\":0,\"msg\":\"*该折扣码不存在！\"}");
                return;
            }
            else
            {
                context.Response.Write("{\"status\":1,\"msg\":\"该折扣码可用！\",\"value\":" + modelcode.str_code_rage + "}");
                return;
            }
        } 
        #endregion

        #region 检查优惠券是否可用===============================
        /// <summary>
        /// 检查优惠券是否可用
        /// </summary>
        /// <param name="context"></param>
        protected void GetCheckCoupon(HttpContext context)
        {
            string couponCode = Vincent._DTcms.DTRequest.GetString("coupon");
            BuysingooShop.Model.users userinfo = context.Session[Vincent._DTcms.DTKeys.SESSION_USER_INFO] as BuysingooShop.Model.users;
            if (userinfo == null)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"你还没有登录，请登录！\",\"value\":\"0\"}");
                return;
            }
            if (string.IsNullOrEmpty(couponCode))
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"请输入优惠券！\",\"value\":\"0\"}");
                return;
            }
            BuysingooShop.Model.user_coupon model = new BuysingooShop.Model.user_coupon();
            BuysingooShop.BLL.user_coupon bll = new BuysingooShop.BLL.user_coupon();
            string strwhere=string.Format("status={0} AND CONVERT(varchar(100), GETDATE(), 21) Between start_time And end_time AND userid={1}",1,userinfo.id);
            model = bll.GetModel(strwhere);
            if (model!=null)
            {
                context.Response.Write("{\"status\":\"1\",\"msg\":\"该优惠券可用！\",\"value\":"+model.amount+"}");
                return;
            }
            context.Response.Write("{\"status\":\"0\",\"msg\":\"你的优惠券不可用！\",\"value\":\"0\"}");
        } 
        #endregion

        #region 提交订单=========================================
        /// <summary>
        /// 提交订单
        /// </summary>
        /// <param name="context"></param>
        public void GetSubmitOrder(HttpContext context)
        {
            string goods = Vincent._DTcms.DTRequest.GetFormString("goods");
            string addressId = Vincent._DTcms.DTRequest.GetFormString("addressId");
            string expressId = Vincent._DTcms.DTRequest.GetFormString("expressId");
            int bill_type = Vincent._DTcms.DTRequest.GetInt("bill_type",0);
            string invoice_rise = Vincent._DTcms.DTRequest.GetString("invoice_rise");
            string totalprice = Vincent._DTcms.DTRequest.GetFormString("totalprice");
            string str_code = Vincent._DTcms.DTRequest.GetString("str_code");
            decimal or_amount = 0M;//付款金额

            Model.orders model = new Model.orders();
            BuysingooShop.BLL.orders bll = new BuysingooShop.BLL.orders();
            BuysingooShop.Model.users userinfo = context.Session[Vincent._DTcms.DTKeys.SESSION_USER_INFO] as BuysingooShop.Model.users;
            if (userinfo == null)
            {
                context.Response.Write("{\"status\":0,\"msg\":\"你还未登陆，请先登录！\"}");
                return;
            }

            if (addressId == "")
            {
                context.Response.Write("{\"status\":0,\"msg\":\"地址获取失败！\"}");
                return;
            }

            //验证优惠券
            var j = 0;

            BuysingooShop.BLL.user_coupon couponbll = new BLL.user_coupon();
            BuysingooShop.Model.user_coupon coupon = null;
            if (str_code != "")
            {

                coupon = couponbll.GetModel(" str_code='" + str_code + "'");
                if (coupon == null)
                {
                    j = 1;
                }
                else
                {
                    if (DateTime.Compare(coupon.end_time, DateTime.Now) <= 0)
                    {
                        j = 2;
                        
                    }
                    if (coupon.status == 2)
                    {
                        j = 3;
                    }
                }
            }
            if (j == 1)
            {
                context.Response.Write("{\"status\":0,\"msg\":\"优惠券编码输入有误！\"}");
                return;
            }
            if (j == 2)
            {
                context.Response.Write("{\"status\":0,\"msg\":\"优惠券已经过期！\"}");
                return;
            }
            if (j == 3)
            {
                context.Response.Write("{\"status\":0,\"msg\":\"优惠券已使用！\"}");
                return;
            }

            //订单信息
            Model.user_address modelAddress = new BLL.user_address().GetModel(int.Parse(addressId));
            if (modelAddress == null)
            {
                context.Response.Write("{\"status\":0,\"msg\":\"地址获取失败！\"}");
                return;
            }
            Model.express modelExpress = new BLL.express().GetModel(int.Parse(expressId));

            model.order_no = CreateOrderNo();
            model.accept_name = modelAddress.acceptName;
            model.area = modelAddress.id.ToString();
            model.mobile = modelAddress.mobile;
            model.address = modelAddress.address;
            model.post_code = modelAddress.postcode.ToString();
            model.add_time=DateTime.Now;
            model.user_id = userinfo.id;
            model.user_name = userinfo.user_name;
            model.express_id = int.Parse(expressId);
            model.express_fee = modelExpress.express_fee;
            model.express_status = 1;
            model.status = 1;
            //decimal real_amount = Decimal.Parse(totalprice) - modelExpress.express_fee;
            
            model.down_order = "PC端";

            


            ////商品信息value="<%#Eval("id") %>|<%#Eval("type") %>|<%#Eval("price") %>|<%#Eval("quantity") %>|<%#Eval("weight") %>|<%#Eval("img_url") %>"

            List<Model.order_goods> list = new List<order_goods>();

            string[] strArr = Vincent._DTcms.Utils.DelLastChar(goods, "&").Split('&');
            foreach (var item in strArr)
            {

                string[] strArr2 = item.Split('|');
                Model.order_goods modelGoods = new Model.order_goods();
                modelGoods.goods_id = int.Parse(strArr2[0].ToString());
                modelGoods.type = strArr2[1].ToString();
                modelGoods.goods_price = decimal.Parse(strArr2[2].ToString());
                modelGoods.quantity = int.Parse(strArr2[3].ToString());

                if (strArr2[1].ToString() != "智能家居") 
                {
                    modelGoods.weight = decimal.Parse(strArr2[4].ToString());
                }
                else
                {
                    //modelGoods.goods_color = strArr2[4].ToString();
                }

                modelGoods.goods_pic = strArr2[5].ToString();
                modelGoods.goods_title = strArr2[6].ToString();

                decimal goodsam = 0M;
                if (strArr2[1].ToString() != "智能家居")
                {
                    goodsam = decimal.Parse(strArr2[2].ToString()) * decimal.Parse(strArr2[4].ToString()) * int.Parse(strArr2[3].ToString());//商品金额
                }
                else
                {
                    goodsam = decimal.Parse(strArr2[2].ToString()) * int.Parse(strArr2[3].ToString());//商品金额
                }

                
                or_amount += goodsam;

                list.Add(modelGoods);
            }
            model.order_goods = list;
            model.real_amount = or_amount;

            or_amount += modelExpress.express_fee;//加上配送费用

            model.order_amount = or_amount;

            var k = 0;
            var p = 0;
            if (coupon != null)
            {
                decimal payamount = or_amount - coupon.amount;
                if (payamount > 0)
                {
                    model.payable_amount = payamount;//实付款
                    model.str_code = str_code;

                    or_amount = payamount;
                }
                else
                {
                    model.payable_amount = 0M;//实付款
                    model.str_code = str_code;
                    model.status = 2;
                    model.payment_status = 2;
                    p = bll.Add(model);
                    k=1;

                    or_amount = 0M;
                }
            }
            else
            {
                model.payable_amount = Decimal.Parse(totalprice);
            }

            

            if (bill_type != 0)
            {
                model.is_bill = 1;
                model.bill_type = bill_type;
                model.invoice_rise = invoice_rise;
            }
            

            
            int order = bll.Add(model);
            

            //优惠券使用记录
            BuysingooShop.BLL.user_coupon_log cbll = new BLL.user_coupon_log();
            BuysingooShop.Model.user_coupon_log cmodel = new Model.user_coupon_log();


            if (coupon != null)
            {
                
                cmodel.user_id = userinfo.id;
                cmodel.user_name = userinfo.user_name;
                cmodel.coupon_id = coupon.id;
                cmodel.str_code = coupon.str_code;
                cmodel.order_id = order;
                cmodel.order_no = model.order_no;
                cmodel.add_time = coupon.add_time;
                cmodel.use_time = DateTime.Now;
                cmodel.status = 1;

            }

            if (k == 1 && p > 0)
            {
                cmodel.status = 2;
                cbll.Add(cmodel);

                coupon.status = 2;
                couponbll.Update(coupon);


                context.Response.Write("{\"status\":3,\"msg\":\"订单提交成功,该订单不要付款！\"}");
                return;
            }

            if (order > 0)
            {
                context.Session["price_"+order] = or_amount;//应付金额保存到session

                if (coupon != null)
                {
                    cbll.Add(cmodel);
                }

                string no = CreateOrderNo();
                string idd = order.ToString();

                string orderId = idd + "_" + no;
                context.Response.Write("{\"status\":1,\"msg\":\"订单提交成功，请付款！\",\"orderId\":\"" + orderId + "\"}");
                foreach (string item in strArr)
                {
                    string[] strArr2 = item.Split('|');
                    ShopCart.ClearCart(strArr2[0]);
                }
            }
            else
            {
                if (coupon != null)
                {
                    cbll.Add(cmodel);
                }

                context.Response.Write("{\"status\":0,\"msg\":\"订单提交失败，请重新提交订单！\"}");
            }

        }

        /// <summary>
        /// 账户余额支付
        /// </summary>
        /// <param name="context"></param>
        public void GetSubmitOrderAccount(HttpContext context)
        {
            string goods = Vincent._DTcms.DTRequest.GetFormString("goods");
            string addressId = Vincent._DTcms.DTRequest.GetFormString("addressId");
            string expressId = Vincent._DTcms.DTRequest.GetFormString("expressId");
            int bill_type = Vincent._DTcms.DTRequest.GetInt("bill_type",0);
            string invoice_rise = Vincent._DTcms.DTRequest.GetString("invoice_rise");
            string totalprice = Vincent._DTcms.DTRequest.GetFormString("totalprice");
            string str_code = Vincent._DTcms.DTRequest.GetString("str_code");
            decimal or_amount = 0M;//付款金额

            Model.orders model = new Model.orders();
            BuysingooShop.BLL.orders bll = new BuysingooShop.BLL.orders();
            BuysingooShop.Model.users userinfo = context.Session[Vincent._DTcms.DTKeys.SESSION_USER_INFO] as BuysingooShop.Model.users;
            if (userinfo == null)
            {
                context.Response.Write("{\"status\":0,\"msg\":\"你还未登陆，请先登录！\"}");
                return;
            }

            //验证优惠券
            var j = 0;

            BuysingooShop.BLL.user_coupon couponbll = new BLL.user_coupon();
            BuysingooShop.Model.user_coupon coupon = null;
            if (str_code != "")
            {

                coupon = couponbll.GetModel(" str_code='" + str_code + "'");
                if (coupon == null)
                {
                    j = 1;
                }
                else
                {
                    if (DateTime.Compare(coupon.end_time, DateTime.Now) <= 0)
                    {
                        j = 2;
                        
                    }
                    if (coupon.status == 2)
                    {
                        j = 3;
                    }
                }
            }
            if (j == 1)
            {
                context.Response.Write("{\"status\":0,\"msg\":\"优惠券编码输入有误！\"}");
                return;
            }
            if (j == 2)
            {
                context.Response.Write("{\"status\":0,\"msg\":\"优惠券已经过期！\"}");
                return;
            }
            if (j == 3)
            {
                context.Response.Write("{\"status\":0,\"msg\":\"优惠券已使用！\"}");
                return;
            }
            

            

            //订单信息
            Model.user_address modelAddress = new BLL.user_address().GetModel(int.Parse(addressId));
            Model.express modelExpress = new BLL.express().GetModel(int.Parse(expressId));

            model.order_no = CreateOrderNo();
            model.accept_name = modelAddress.acceptName;
            model.area = modelAddress.id.ToString();
            model.mobile = modelAddress.mobile;
            model.address = modelAddress.address;
            model.post_code = modelAddress.postcode.ToString();
            model.add_time = DateTime.Now;
            model.user_id = userinfo.id;
            model.user_name = userinfo.user_name;
            model.express_id = int.Parse(expressId);
            model.express_fee = modelExpress.express_fee;
            model.express_status = 1;
            model.status = 1;
            //decimal real_amount = Decimal.Parse(totalprice) - modelExpress.express_fee;
            
            model.down_order = "PC端";


            ////商品信息value="<%#Eval("id") %>|<%#Eval("type") %>|<%#Eval("price") %>|<%#Eval("quantity") %>|<%#Eval("weight") %>|<%#Eval("img_url") %>"

            List<Model.order_goods> list = new List<order_goods>();

            string[] strArr = Vincent._DTcms.Utils.DelLastChar(goods, "&").Split('&');
            foreach (var item in strArr)
            {

                string[] strArr2 = item.Split('|');
                Model.order_goods modelGoods = new Model.order_goods();
                modelGoods.goods_id = int.Parse(strArr2[0].ToString());
                modelGoods.type = strArr2[1].ToString();
                modelGoods.goods_price = decimal.Parse(strArr2[2].ToString());
                modelGoods.quantity = int.Parse(strArr2[3].ToString());

                if (strArr2[1].ToString() != "智能家居")
                {
                    modelGoods.weight = decimal.Parse(strArr2[4].ToString());
                }
                else
                {
                    //modelGoods.goods_color = strArr2[4].ToString();
                }

                modelGoods.goods_pic = strArr2[5].ToString();
                modelGoods.goods_title = strArr2[6].ToString();

                decimal goodsam = 0M;
                if (strArr2[1].ToString() != "智能家居")
                {
                    goodsam = decimal.Parse(strArr2[2].ToString()) * decimal.Parse(strArr2[4].ToString()) * int.Parse(strArr2[3].ToString());//商品金额
                }
                else
                {
                    goodsam = decimal.Parse(strArr2[2].ToString()) * int.Parse(strArr2[3].ToString());//商品金额
                }
                or_amount += goodsam;

                list.Add(modelGoods);
            }

            model.order_goods = list;
            model.real_amount = or_amount;

            or_amount += modelExpress.express_fee;//加上配送费用

            model.order_amount = or_amount;

            if (coupon != null)
            {
                decimal payamount = or_amount - coupon.amount;
                if (payamount > 0)
                {
                    model.payable_amount = payamount;//实付款
                    model.str_code = str_code;
                }
                else
                {
                    model.payable_amount = 0M;//实付款
                    model.str_code = str_code;
                }
            }
            else
            {
                model.payable_amount = or_amount; 
            }


            if (bill_type != 0)
            {
                model.is_bill = 1;
                model.bill_type = bill_type;
                model.invoice_rise = invoice_rise;
            }




            
            int orderId = bll.Add(model);

            //用户表
            BuysingooShop.BLL.users bl = new BLL.users();
            Model.users user1 = bl.GetModel(userinfo.id);

            if (coupon != null)
            {
                decimal payamount = or_amount - coupon.amount;
                if (payamount > 0)
                {
                    user1.amount = user1.amount - payamount;
                }
                else
                {
                    
                }
            }
            else
            {
                //decimal totalamount = decimal.Parse(totalprice);
                user1.amount = user1.amount - or_amount;
            }

            


            //优惠券使用记录
            BuysingooShop.BLL.user_coupon_log cbll = new BLL.user_coupon_log();
            BuysingooShop.Model.user_coupon_log cmodel = new Model.user_coupon_log();
            if (coupon != null)
            {
                
                cmodel.user_id = userinfo.id;
                cmodel.user_name = userinfo.user_name;
                cmodel.coupon_id = coupon.id;
                cmodel.str_code = coupon.str_code;
                cmodel.order_id = orderId;
                cmodel.order_no = model.order_no;
                cmodel.add_time = coupon.add_time;
                cmodel.use_time = DateTime.Now;
                cmodel.status = 1;
            }

            if (orderId > 0 && bl.Update(user1))
            {
                if (BuysingooShop.BLL.OrdersBLL.ConfirmPay(orderId, 2,2))  //1确认快递代收  2客户已经付款 
                {
                    if (coupon != null)
                    {
                        cmodel.status = 2;
                        cbll.Add(cmodel);
                        coupon.status = 2;
                        couponbll.Update(coupon);
                    }

                    context.Response.Write("{\"status\":1,\"msg\":\"订单支付成功！\"}");

                    foreach (string item in strArr)
                    {
                        string[] strArr2 = item.Split('|');
                        ShopCart.ClearCart(strArr2[0]);
                    }
                    
                }
            }
            else
            {
                if (coupon != null)
                {
                    cbll.Add(cmodel);
                }

                context.Response.Write("{\"status\":0,\"msg\":\"订单提交失败，请重新提交订单！\"}");
            }

        }

        /// <summary>
        /// 提交充值
        /// </summary>
        /// <param name="context"></param>
        public void GetSubmitRecharge(HttpContext context)
        {
            var amount = _Request.GetFloat("amount");

            Model.user_point_log model = new Model.user_point_log();
            BuysingooShop.BLL.user_point_log bll = new BuysingooShop.BLL.user_point_log();
            BuysingooShop.Model.users userinfo = context.Session[Vincent._DTcms.DTKeys.SESSION_USER_INFO] as BuysingooShop.Model.users;
            if (userinfo == null)
            {
                context.Response.Write("{\"status\":0,\"msg\":\"你还未登陆，请先登录！\"}");
                return;
            }

            //订单信息
            string orderno = "zf" + CreateOrderNo();
            model.user_id = userinfo.id;
            model.user_name = userinfo.user_name;
            model.value = 0;
            model.remark = "充值";
            model.add_time = DateTime.Now;
            model.order_no = orderno;
            model.amount = decimal.Parse(amount.ToString());
            model.pointtype = 1;
            model.status = 1;

            int orderId = bll.Addrecharge(model,false);

            string rechargeno = bll.GetModel(orderId).order_no;
            string outmsg = "{\"status\":1,\"msg\":\"充值成功！\",\"orderId\":\"rechargeno\"}";
            string outmsgs = outmsg.Replace("rechargeno", rechargeno);

            if (orderId > 0)
            {
                context.Response.Write(outmsgs);
            }
            else
            {
                context.Response.Write("{\"status\":0,\"msg\":\"订单提交失败，请重新提交订单！\"}");
            }

        }

        /// <summary>
        /// 提现
        /// </summary>
        /// <param name="context"></param>
        public void submit_withdraw(HttpContext context)
        {
            var user_id = _Request.GetInt("user_id");
            var amount = _Request.GetFloat("amount");
            var banktype = _Request.GetStringForm("banktype");
            var remark = _Request.GetStringForm("remark");
            var card_no = _Request.GetStringForm("card_no");

            Model.withdraw model = new Model.withdraw();
            BuysingooShop.BLL.withdraw bll = new BuysingooShop.BLL.withdraw();
            BuysingooShop.Model.users userinfo = context.Session[Vincent._DTcms.DTKeys.SESSION_USER_INFO] as BuysingooShop.Model.users;
            if (userinfo == null)
            {
                context.Response.Write("{\"status\":0,\"msg\":\"你还未登陆，请先登录！\"}");
                return;
            }

            //获取用户信息
            Model.users users = new BLL.users().GetModel(userinfo.id);


            //提现信息
            model.user_id = userinfo.id;
            model.card_no = card_no;
            model.amount = decimal.Parse(amount.ToString());
            model.banktype = int.Parse(banktype);
            model.remark=remark;
            model.status = 1;
            model.addtime = DateTime.Now;

            //更新冻结金额
            users.frozen_amount += decimal.Parse(amount.ToString());
            bool user_frozen_amount = new BLL.users().Update(users);

            int orderId = bll.Add(model);
            string outmsg = "{\"status\":1,\"msg\":\"申请已提交，可能需要2-5个工作日！\"}";

            if (orderId > 0 && user_frozen_amount)
            {
                context.Response.Write(outmsg);
            }
            else
            {
                context.Response.Write("{\"status\":0,\"msg\":\"提交失败！\"}");
            }
        }


        /// <summary>
        /// 生成订单编号
        /// </summary>
        /// <returns></returns>
        public string CreateOrderNo()
        {
            string nowtime = DateTime.Now.ToString("yyyyMMddHHmmss");
            //生成随机数
            string randonNum = new Random().Next(0, 999999).ToString();
            //补齐位数
            randonNum = string.Format("{0:d6}", randonNum);
            return nowtime + randonNum;
        } 
        #endregion

        #region 取消订单=========================================
        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="context"></param>
        public void CancelOrder(HttpContext context)
        {
            string orderId = Vincent._DTcms.DTRequest.GetFormString("orderId");
            BuysingooShop.Model.orders model = new BuysingooShop.BLL.orders().GetModel(Int32.Parse(orderId));
            BuysingooShop.BLL.orders bll = new BuysingooShop.BLL.orders();
            if (model==null)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"取消订单出错！\"}");
                return;
            }
            model.status = 99;
            if (bll.Update(model))
            {
                context.Response.Write("{\"status\":\"1\",\"msg\":\"订单取消成功！\"}");
                return;
            }
            context.Response.Write("{\"status\":\"0\",\"msg\":\"订单取消失败！\"}");
        } 
        #endregion

        #region 删除，修改购物车的商品===========================
        /// <summary>
        /// 删除，修改购物车的商品
        /// </summary>
        /// <param name="context"></param>
        public void GetDelete_good(HttpContext context)
        {
            string[] dataidarray = Vincent._DTcms.DTRequest.GetQueryString("dataid").Split(',');
            //num=0 代表删除商品
            string datanum = Vincent._DTcms.DTRequest.GetQueryString("datanum");
            bool bl = true;
            foreach (string dataid in dataidarray)
            {
                bl = ShopCart.UpdateCart(dataid, datanum);
            }
            if (bl)
            {
                context.Response.Write("1");
            }
            else
            {
                context.Response.Write("0");
            }
        } 
        #endregion

        #region 获取咨询分页数据=================================
        /// <summary>
        /// 获取咨询分页数据
        /// </summary>
        /// <param name="context"></param>
        public void GetPageConsult(HttpContext context)
        {
            int pageSize = Vincent._DTcms.DTRequest.GetInt("pageSize", 8);
            int pageIndex = Vincent._DTcms.DTRequest.GetInt("pageIndex", 1);
            int totalCount;
            string goodid = Vincent._DTcms.DTRequest.GetQueryString("goodid");

            string strwhere = " is_reply=1 and data_type=0 and article_id=" + goodid;
            DataTable table =
                new BuysingooShop.BLL.article_comment().GetList(pageSize, pageIndex, strwhere, "id desc", out totalCount).Tables[0];
            StringBuilder sb = new StringBuilder();
            if (table.Rows.Count > 0)
            {
                sb.Append("<ul class='u-consulting-qa'>");
                foreach (DataRow items in table.Rows)
                {
                    sb.Append("<li style='border: 1px solid #C3C3C3;'>");
                    sb.Append("<div class='u-consulting-ask'><p class='u-consulting-a'>");
                    sb.Append("<span class='u-consulting-n'>" + items["user_name"].ToString() + "</span>");
                    sb.Append("<span class='u-consulting-time'>" + items["add_time"] + "</p>");
                    sb.Append("<p class='u-consulting-q'>" + items["content"] + "</div>");
                    sb.Append("<div class='u-consulting-ans'><p class='u-consulting-c'>");
                    sb.Append("<span class='u-consulting-s'>酒定制客服回复：</span>");
                    sb.Append("<span class='u-consulting-time'>" + items["reply_time"] + "</span></p>");
                    sb.Append("<p class='u-consulting-r'>" + items["reply_content"] + "</div>");
                    sb.Append("</li>");
                }
                sb.Append("</ul>");
            }
            //获取分页码
            string strPage = Vincent._DTcms.Utils.OutPageList(pageSize, pageIndex, totalCount, 8);
            context.Response.Write(sb.ToString() + strPage);
            context.Response.End();
        } 
        #endregion

        #region 获取评论分页数据=================================
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        private void GetPageComment(HttpContext context)
        {
            string goodid = Vincent._DTcms.DTRequest.GetQueryString("goodid");
            string commenttype = Vincent._DTcms.DTRequest.GetQueryString("commenttype");
            int pageSize = Vincent._DTcms.DTRequest.GetQueryInt("pagesize", 3);
            int pageIndex = Vincent._DTcms.DTRequest.GetQueryInt("pageindex", 1);
            int totalCount;
            string strwhere = string.Format("article_id={0} and is_reply={1} and data_type={2}", goodid, 1, 1);
            if (commenttype != "0")
            {
                strwhere += " and comment_type=" + commenttype;
            }
            DataTable dt = new BuysingooShop.BLL.article_comment().GetList(pageSize, pageIndex, strwhere, "id desc", out totalCount).Tables[0];
            BuysingooShop.Model.users userModel = new BuysingooShop.Model.users();
            BuysingooShop.BLL.users userBll = new BuysingooShop.BLL.users();
            StringBuilder strTxt = new StringBuilder();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    userModel = userBll.GetModel(int.Parse(item["user_id"].ToString()));
                    strTxt.Append("<div class='u-evaluate-box'>");
                    strTxt.Append("<div class='u-img-box'><div class='u-img'>");
                    strTxt.Append("<img src='" + (userModel == null ? "images/photo.jpg" : userModel.avatar) + "' height='54' width='54'>");
                    strTxt.Append("</div><div class='u-name'>" + (userModel==null ? "匿名用户" : userModel.user_name) + "</div></div>");
                    strTxt.Append("<div class='u-reply-wrap'><div class='u-evaluate-info'>");
                    strTxt.Append("<div class='u-evaluate-exp'>心得：</div><div class='u-evaluate-text'>");
                    strTxt.Append("<p class='u-evaluate-t'>" + item["content"].ToString() + "</p>");
                    strTxt.Append("<p class='u-evaluate-buydate'>购买日期：" + item["add_time"].ToString() + "</p>");
                    strTxt.Append("</div><div class='clear'></div></div>");
                    strTxt.Append("<div class='u-evaluate-praise'><div class='u-evaluate-praise'>");
                    //strTxt.Append("<div id='_plidyz536021' style='display: none'>");
                    //strTxt.Append("<i></i><span>已赞(<b id='_nubf536021' pldztj='536021'>0</b>)</span><em>+1</em></div>");
                    //strTxt.Append("<div id='_pliddz536021' onclick='addpldz(536021,this,2505)'>");
                    //strTxt.Append("<i></i><span>赞(<b pldztj='536021'>0</b>)</span><em>+1</em></div>");
                    strTxt.Append("</div></div>");
                    strTxt.Append("</div></div></div>");

                    //<div class="u-evaluate-page-wrap" id="_detailEvaluatePage"><div class="u-evaluate-page">
                    //<a class="prve" href="javascript:void(0);" title="上一页">上一页</a><a class="current">1</a>
                    //<a href="javascript:void(0);" onclick="sub(2,-1);">2</a>
                    //<a class="next" href="javascript:void(0);" title="下一页" onclick="sub(2,-1);">下一页</a></div></div>
                }
                //strTxt.Append("<div class='u-evaluate-page-wrap' id='_detailEvaluatePage'><div class='u-evaluate-page'>");
                //strTxt.Append("</div></div></div>");
                //strTxt.Append("</div></div></div>");
                //strTxt.Append("</div></div></div>");
                string strpage = Vincent._DTcms.Utils.OutPageList(pageSize, pageIndex, totalCount, 8);
                strTxt.Append(strpage);
            }
            else
            {
                strTxt.Append("<span style='text-algin:conter;'>暂无评论数据</span>");
            }
            context.Response.Write(strTxt.ToString());
            context.Response.End();
        }
        #endregion

        #region 提交商品咨询=====================================
        /// <summary>
        /// 提交商品咨询
        /// </summary>
        /// <param name="context"></param>
        public void SubmitGoodsConsult(HttpContext context)
        {
            string data = Vincent._DTcms.DTRequest.GetQueryString("data").ToString();
            int articleid = int.Parse(Vincent._DTcms.DTRequest.GetQueryString("articleid"));
            BuysingooShop.Model.users userinfo = context.Session[Vincent._DTcms.DTKeys.SESSION_USER_INFO] as BuysingooShop.Model.users;
            if (string.IsNullOrEmpty(data))
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"数据不能为空！\"}");
                return;
            }
            BuysingooShop.Model.article_comment model = new BuysingooShop.Model.article_comment();
            model.content = data;
            model.data_type = 0;
            model.article_id = articleid;
            model.user_ip = Vincent._DTcms.DTRequest.GetIP();
            model.channel_id = 2;
            model.user_id = userinfo == null ? 0 : userinfo.id;
            model.user_name = userinfo == null ? "匿名用户" : userinfo.user_name;

            BuysingooShop.BLL.article_comment bll = new BuysingooShop.BLL.article_comment();
            if (bll.Add(model) > 0)
            {
                context.Response.Write("{\"status\": 1, \"msg\": \"数据提交成功，等待审核！\"}");
                return;
            }
            context.Response.Write("{\"status\": 0, \"msg\": \"数据提交失败！\"}");
            context.Response.End();

        }
        #endregion

        #region 获取月销量数据===================================
        /// <summary>
        ///获取月销量数据
        /// </summary>
        /// <param name="context"></param>
        public void GetPageMonthSell(HttpContext context)
        {
            int pageIndex = Vincent._DTcms.DTRequest.GetInt("pageIndex", 1);
            int pageSize = Vincent._DTcms.DTRequest.GetInt("pageSize", 5);
            int totalCount;
            string strWhere = "CONVERT(CHAR(7),add_time,120)=CONVERT(CHAR(7),GETDATE(),120)";
            DataTable dtRecord = new BuysingooShop.BLL.orders().GetMultiList(pageSize, pageIndex, strWhere, "add_time desc", out totalCount).Tables[0];

            StringBuilder strTxtBuilder=new StringBuilder();
            strTxtBuilder.Append("<table  class='table-deal-record'><tr>");
            strTxtBuilder.Append("<th class=th-buyer'>买家</th>");
            strTxtBuilder.Append("<th class=th-goods'>款式/型号</th>");
            strTxtBuilder.Append("<th class=th-quantity'>数量</th>");
            strTxtBuilder.Append("<th class=th-price'>拍下价格</th>");
            strTxtBuilder.Append("<th class='th-dealtime'>成交时间</th></tr>");
            if (dtRecord.Rows.Count > 0)
            {
                foreach (DataRow itemRow in dtRecord.Rows)
                {
                    strTxtBuilder.Append("<tr><td class='cell-align-l buyer'>");
                    strTxtBuilder.Append("<div class='line'>" + itemRow["user_name"].ToString() == "" ? "匿名用户" : itemRow["user_name"].ToString() + "</div>");
                    strTxtBuilder.Append("<div class='line'>");
                    strTxtBuilder.Append("<img src='../images/b_red_5.gif' class='rank' border='0 align='absmiddle'>");
                    strTxtBuilder.Append("</div></td>");
                    strTxtBuilder.Append("<td class='cell-align-l style'>" + itemRow["goods_title"].ToString() + "</td>");
                    strTxtBuilder.Append("<td class='quantity'>" + itemRow["quantity"].ToString() + "</td>");
                    strTxtBuilder.Append("<td class='rice'><em>" + itemRow["payable_amount"].ToString() + "</em></td>");
                    strTxtBuilder.Append("<td class='dealtime'><span class='date'>" + itemRow["add_time"].ToString().Substring(0, 10) + "</span>");
                    strTxtBuilder.Append("<span class='time'>" + itemRow["add_time"].ToString().Substring(11) + "</span></td>");
                    strTxtBuilder.Append("</tr>");
                }
            }
            strTxtBuilder.Append("</table>");
            string strpage = Vincent._DTcms.Utils.OutPageList(pageSize, pageIndex, totalCount, 8);
            context.Response.Write(strTxtBuilder.ToString() + strpage);
            context.Response.End();
        } 
        #endregion

        #region 提交支付=========================================
        /// <summary>
        /// 提交支付
        /// </summary>
        /// <param name="context"></param>
        public void SubmitPay(HttpContext context)
        {
            //付款方式
            string payType = Vincent._DTcms.DTRequest.GetString("payType");
            string str = context.Session["orderNo"].ToString();
            string[]  strArray = str.Substring(1, str.Length - 2).Replace("'", "").Split(',');
            
            //付款的操作
            //
            //
            //

            BuysingooShop.BLL.orders bll = new BuysingooShop.BLL.orders();
            try
            {
                for (int i = 0; i < strArray.Length; i++)
                {
                    //付款成功，更新订单支付状态、订单状态
                    string strwhere = string.Format("payment_status={0},payment_time='{1}',payment_id={2},status={3}", 2, DateTime.Now.ToString(), payType,2);
                    bll.UpdateField(int.Parse(strArray[i].ToString()), strwhere);
                }
                context.Response.Write("{\"status\":1,\"msg\":\"支付成功！\"}");
            }
            catch (Exception)
            {
                context.Response.Write("{\"status\":0,\"msg\":\"支付失败！\"}");
            }
            context.Response.End();

        }
        public void GetSeesion(HttpContext context)
        {
            string session = context.Request["orderNo"].ToString();
            string str = context.Session["session"].ToString();
            context.Response.Write(str);
            context.Response.End();
        }
        #endregion

        #region 获取要评价的商品列表=============================
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void orderComment(HttpContext context)
        {
            string orderNo = Vincent._DTcms.DTRequest.GetString("order_no");

            BuysingooShop.Model.orders model = new BuysingooShop.BLL.orders().GetModel(orderNo);
            BuysingooShop.BLL.order_goods bll = new BuysingooShop.BLL.order_goods();

            if (model==null)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"你评论的订单不存在！\"}");
                return;
            }
            DataTable dt = bll.GetList(0, string.Format("order_id={0}", model.id), "id desc").Tables[0];
            StringBuilder strTxt = new StringBuilder();
            if (dt.Rows.Count>0)
            {
                foreach (DataRow itemRow in dt.Rows)
                {
                    strTxt.Append("<table><tr class='one'>");
                    strTxt.Append("<th width='90'>订单号</th><th width='105'>订单商品</th><th width='120'>订单金额</th>");
                    strTxt.Append("<th width='140'>下单时间</th><th width='115'>订单状态</th><th width='70'> 数量</th></tr>");
                    strTxt.Append("<tr style='border-bottom: 1px solid #abc;'>");
                    strTxt.Append("<td>" + model.order_no + "</td><td>" + itemRow["goods_title"] + "</td>");
                    strTxt.Append("<td>￥" + decimal.Parse(itemRow["real_price"].ToString()) * decimal.Parse(itemRow["quantity"].ToString()) + "</td>");
                    strTxt.Append("<td>" + model.add_time + "</td><td>已完成</td><td>2</td></tr>");
                    strTxt.Append("<tr><td colspan='6'><div id='commentdiv'>");
                    strTxt.Append("<textarea style='margin-left: 20px;' class='txtAreaComment' cols='80' rows='4'></textarea>");
                    strTxt.Append("<input style='float: right; margin-right: 100px;margin-top: 10px;' class='submitBut' data='" + itemRow["goods_id"] + "' type='button' value='提交评价' />");
                    strTxt.Append("</div></td></tr>");
                    strTxt.Append("</table>");
                }
                context.Response.Write("{\"status\":\"1\",\"msg\":\"" + strTxt + "\"}");
            }
            else
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"" + strTxt + "\"}");
            }
            context.Response.End();
        } 
        #endregion


        /// <summary>
        /// 验证优惠券
        /// </summary>
        /// <param name="context"></param>
        public void validate_str_code(HttpContext context)
        {
            var str_code = _Request.GetString("str_code");

            Model.user_coupon model = new Model.user_coupon();
            model = new BLL.user_coupon().GetModel(" str_code='"+str_code+"'");

            if (model == null)
            {
                context.Response.Write("{\"status\":0,\"msg\":\"优惠券编码输入有误！\"}");
                return;
            }
            //string amount = model.amount.ToString();

            string outmsg = "{\"status\":1,\"msg\":\"成功！\",\"str_amount\":" + model.amount + "}";


            context.Response.Write(outmsg);

        }


        #region 提交评论的处理方法OK=============================
        private void goodsComment(HttpContext context)
        {
            StringBuilder strTxt = new StringBuilder();
            BuysingooShop.BLL.article_comment bll = new BuysingooShop.BLL.article_comment();
            BuysingooShop.Model.article_comment model = new BuysingooShop.Model.article_comment();

            int article_id = int.Parse(Vincent._DTcms.DTRequest.GetString("good_id"));
            string _content = Vincent._DTcms.DTRequest.GetString("txtContent");
            if (article_id == 0)
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"对不起，参数传输有误！\"}");
                return;
            }
            if (string.IsNullOrEmpty(_content))
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"对不起，请输入评论的内容！\"}");
                return;
            }
            //检查该文章是否存在
            BuysingooShop.Model.article artModel = new BuysingooShop.BLL.article().GetModel(article_id);
            if (artModel == null)
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"对不起，主题不存在或已删除！\"}");
                return;
            }
            //检查用户是否登录
            int user_id = 0;
            string user_name = "匿名用户";
            BuysingooShop.Model.users userModel = new BuysingooShop.Web.UI.BasePage().GetUserInfo();
            if (userModel != null)
            {
                user_id = userModel.id;
                user_name = userModel.user_name;
            }
            model.channel_id = artModel.channel_id;
            model.article_id = artModel.id;
            model.content = Vincent._DTcms.Utils.ToHtml(_content);
            model.user_id = user_id;
            model.user_name = user_name;
            model.user_ip = Vincent._DTcms.DTRequest.GetIP();
            model.add_time = DateTime.Now;
            model.is_reply = 0;
            if (bll.Add(model) > 0)
            {
                context.Response.Write("{\"status\": 1, \"msg\": \"恭喜您，评价提交成功啦！\"}");
                return;
            }
            context.Response.Write("{\"status\": 0, \"msg\": \"对不起，保存过程中发生错误！\"}");
            return;
        }
        #endregion

        #region 删除评论的处理方法OK=============================
        private void deleteComment(HttpContext context)
        {
            int userId = Vincent._DTcms.DTRequest.GetFormInt("id");
            string action = Vincent._DTcms.DTRequest.GetQueryString("action");
            BLL.article_comment comment = new BLL.article_comment();
            comment.DeleteListConsult(0,userId);
            //context.Response.Write("<script>window.location.href='/user/comment5.html';</script>");
            context.Response.Write("{\"status\":1,\"msg\":\"删除成功！\"}");
        }
        #endregion

        #region 修改密码=========================================
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="context"></param>
        public void ResetPwd(HttpContext context)
        {
            string olderpwd = Vincent._DTcms.DTRequest.GetFormString("oldpwd");
            string newpwd = Vincent._DTcms.DTRequest.GetFormString("newpwd");
            string code = Vincent._DTcms.DTRequest.GetFormString("strcode");

            BuysingooShop.Model.users userInfo = new BuysingooShop.Web.UI.BasePage().GetUserInfo();
            BuysingooShop.BLL.users bll = new BuysingooShop.BLL.users();
            if (userInfo == null)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"你还没有登录，请先登录！\"}");
                return;
            }
            if (code != context.Session[Vincent._DTcms.DTKeys.SESSION_CODE].ToString())
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"验证码不正确！\"}");
                return;
            }
            if (userInfo.password != _DESEncrypt.Encrypt(newpwd, userInfo.salt))
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"原密码不正确！\"}");
                return;
            }
            userInfo.password = _DESEncrypt.Encrypt(olderpwd, userInfo.salt);
            if (bll.Update(userInfo))
            {
                context.Response.Write("{\"status\":\"1\",\"msg\":\"修改成功！\"}");
            }
            else
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"修改失败！\"}");
            }
        } 
        #endregion

        #region 修改用户信息=====================================
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="context"></param>
        private void EditUserInfo(HttpContext context)
        {
            string txtNickName = Vincent._DTcms.DTRequest.GetFormString("nick_name");
            string txtRealName = Vincent._DTcms.DTRequest.GetFormString("real_name");
            string txtBirthday = Vincent._DTcms.DTRequest.GetFormString("birthday");
            string txtMobile = Vincent._DTcms.DTRequest.GetFormString("mobile");
            string txtTelphone = Vincent._DTcms.DTRequest.GetFormString("telphone");
            string txtEmail = Vincent._DTcms.DTRequest.GetFormString("email");
            string txtAddress = Vincent._DTcms.DTRequest.GetFormString("address");
            string txtPostcode = Vincent._DTcms.DTRequest.GetFormString("postcode");
            string txtMale = Vincent._DTcms.DTRequest.GetFormString("Male");

            BuysingooShop.Model.users userInfo = new BuysingooShop.Web.UI.BasePage().GetUserInfo();
            BuysingooShop.BLL.users bll = new BuysingooShop.BLL.users();
            Model.user_address addressModel = new Model.user_address();
            BLL.user_address addressBll = new BLL.user_address();
            if (!addressBll.Existsuserid(userInfo.id))
            {
                if (userInfo != null && txtAddress != "" && txtMobile != "" && txtPostcode!="")
                {
                    addressModel.user_id = userInfo.id;
                    addressModel.acceptName = userInfo.user_name;
                    addressModel.street = txtAddress;
                    addressModel.mobile = txtMobile;
                    addressModel.add_time = DateTime.Now;
                    addressModel.postcode = int.Parse(txtPostcode.ToString());
                    //执行新增操作
                    addressBll.Add(addressModel);
                }

                
            }
            userInfo.sex = txtMale;
            if (userInfo == null)
            {
                context.Response.Write("{\"status\":\"0\",\"info\":\"你还没有登录，请先登录！\"}");
                return;
            }
            if (txtNickName != "")
            {
                userInfo.nick_name = txtNickName;
            }
            if (txtRealName != "")
            {
                userInfo.real_name = txtRealName;
            }
            if (txtBirthday != "")
            {
                userInfo.birthday = DateTime.Parse(txtBirthday);
            }
            if (txtMobile != "")
            {
                userInfo.mobile = txtMobile;
            }
            if (txtTelphone != "")
            {
                userInfo.telphone = txtTelphone;
            }
            if (txtEmail != "")
            {
                userInfo.email = txtEmail;
            }
            if (txtAddress != "")
            {
                userInfo.address = txtAddress;
            }
            if (txtPostcode != "")
            {
                userInfo.postcode = txtPostcode;
            }

            if (bll.Update(userInfo))
            {
                context.Response.Write("{\"status\":\"1\",\"info\":\"修改成功！\"}");
            }
            else
            {
                context.Response.Write("{\"status\":\"0\",\"info\":\"修改失败！\"}");
            }
        } 
        #endregion

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}