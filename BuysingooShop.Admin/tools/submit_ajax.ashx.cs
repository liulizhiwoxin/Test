using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Data;
using System.Web;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using BuysingooShop.BLL;
using BuysingooShop.Model;
using BuysingooShop.Web.UI;
using Vincent;
using LitJson;
using System.Web.Script.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;


namespace BuysingooShop.Web.tools
{
    /// <summary>
    /// AJAX提交处理
    /// </summary>
    public class submit_ajax : IHttpHandler, IRequiresSessionState
    {
        Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig();
        Model.userconfig userConfig = new BLL.userconfig().loadConfig();
        public void ProcessRequest(HttpContext context)
        {
            //取得处事类型
            string action = Vincent._DTcms.DTRequest.GetQueryString("action");

            switch (action)
            {
                case "submit_comment": //提交评论
                    submit_comment(context);
                    break;
                case "GetGoodsInfos"://获取商品信息
                    GetGoodsInfos(context);
                    break;
                case "GetCategory"://获取品类名称
                    GetCategory(context);
                    break;
                case "Getproductimg"://获取产品图片
                    Getproductimg(context);
                    break;
                case "comment_list": //获取评论列表
                    comment_list(context);
                    break;
                case "comment_update": //修改评论
                    comment_update(context);
                    break;
                case "GetParameter": //获取加入我们数据
                    GetParameter(context);
                    break;
                case "complaint_add": //提交投诉
                    complaint_add(context);
                    break;
                case "submit_refund"://退换货
                    submit_refund(context);
                    break;
                case "refund_no"://提交退货信息
                    refund_no(context);
                    break;
                case "user_info_edit": //修改用户信息
                    user_info_edit(context);
                    break;
                case "user_avatar_crop": //确认裁剪用户头像
                    user_avatar_crop(context);
                    break;
                case "user_password_edit": //修改密码
                    user_password_edit(context);
                    break;
                case "user_email_edit": //修改邮箱
                    user_email_edit(context);
                    break;
                case "user_mobile_edit": //修改手机号
                    user_mobile_edit(context);
                    break;
                case "user_getpassword": //邮箱取回密码
                    user_getpassword(context);
                    break;
                case "user_getmailvalidate": //邮箱验证找回密码
                    user_getmailvalidate(context);
                    break;
                case "user_repassword": //邮箱重设密码
                    user_repassword(context);
                    break;
                case "user_validatepostbox": //邮箱验证
                    user_validatepostbox(context);
                    break;
                case "validatepost": //邮箱确认
                    validatepost(context);
                    break;
                case "user_mobilevalidate": //手机验证
                    user_mobilevalidate(context);
                    break;
                case "validateVerdict": //判断是否验证
                    validateVerdict(context);
                    break;
                case "reset": //邮箱验证重设密码
                    user_reMailPost(context);
                    break;
                case "message": //手机验证重设密码
                    user_reMobile(context);
                    break;
                case "user_message_delete": //删除短信息
                    user_message_delete(context);
                    break;
                case "user_message_add": //发布站内短消息
                    user_message_add(context);
                    break;
                case "user_point_convert": //用户兑换积分
                    user_point_convert(context);
                    break;
                case "user_point_delete": //删除用户积分明细
                    user_point_delete(context);
                    break;
                case "user_comment_delete": //删除用户评价
                    user_comment_delete(context);
                    break;
                case "user_amount_recharge": //用户在线充值
                    user_amount_recharge(context);
                    break;
                case "user_amount_delete": //删除用户收支明细
                    user_amount_delete(context);
                    break;
                case "user_coupon_delete": //删除用户优惠券
                    user_coupon_delete(context);
                    break;
                case "user_address_delete": //删除用户收货地址
                    user_address_delete(context);
                    break;
                case "user_address_default": //设置用户默认收货地址
                    user_address_default(context);
                    break;
                case "user_address_add": //新增收货地址
                    user_address_add(context);
                    break;
                case "user_quickregister"://快速注册
                    user_quickregister(context);
                    break;
                case "user_address_edit": //编辑收货地址
                    user_address_edit(context);
                    break;
                case "cart_goods_add": //购物车加入商品
                    cart_goods_add(context);
                    break;
                case "select_address_add"://获取选中地址
                    select_address_add(context);
                    break;
                case "cart_goods_update": //购物车修改商品
                    cart_goods_update(context);
                    break;
                case "cart_goods_updates": //购物车修改商品
                    cart_goods_updates(context);
                    break;
                case "cart_goods_delete": //购物车删除商品
                    cart_goods_delete(context);
                    break;
                case "favorite_cart_add": //加入收藏夹
                    favorite_cart_add(context);
                    break;
                case "favorite_goods_delete": //收藏夹删除商品
                    favorite_goods_delete(context);
                    break;
                case "order_save": //保存订单
                    order_save(context);
                    break;
                case "find_order": //查看订单
                    find_order(context);
                    break;
                case "is_coupon"://是否使用优惠券
                    is_coupon(context);
                    break;
                case "is_refund"://是否退货
                    is_refund(context);
                    break;
                case "order_cancel": //用户取消订单
                    order_cancel(context);
                    break;
                case "confirm_order": //确认订单
                    confirm_order(context);
                    break;
                case "refund_cancel"://用户取消退款
                    refund_cancel(context);
                    break;
                case "setpricesession"://设置订单金额session
                    setpricesession(context);
                    break;
                case "getorderamount"://获取订单金额（保存订单金额）
                    getorderamount(context);
                    break;
                case "view_article_click": //统计及输出阅读次数
                    view_article_click(context);
                    break;
                case "view_comment_count": //输出评论总数
                    view_comment_count(context);
                    break;
                case "view_attach_count": //输出附件下载总数
                    view_attach_count(context);
                    break;
                case "view_cart_count": //输出当前购物车总数
                    view_cart_count(context);
                    break;
                case "user_search": //搜索框查询
                    user_search(context);
                    break;
                case "goods_getId": //搜索查询商品
                    goods_getId(context);
                    break;
                case "user_isuserlogin": //检查用户是否登录
                    user_isuserlogin(context);
                    break;
            }
        }

        #region 提交评论的处理方法OK===========================
        private void submit_comment(HttpContext context)
        {
            BLL.article_comment bll = new BLL.article_comment();
            Model.article_comment model = new Model.article_comment();

            int article_id = Vincent._DTcms.DTRequest.GetFormInt("goods_id", 0);
            int _orders_id = Vincent._DTcms.DTRequest.GetFormInt("order_id");
            string _content = Vincent._DTcms.DTRequest.GetFormString("txtContent");

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
            Model.article artModel = new BLL.article().GetModel(article_id);
            if (artModel == null)
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"对不起，商品不存在或已删除！\"}");
                return;
            }
            //检查用户是否登录

            Model.users userModel = new Web.UI.BasePage().GetUserInfo();
            if (userModel == null)
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"对不起，你还未登陆！\"}");
                return;
            }
            model.user_id = userModel.id;
            model.user_name = userModel.user_name;
            model.channel_id = artModel.channel_id;
            model.article_id = artModel.id;
            model.order_id = _orders_id;
            model.content = Vincent._DTcms.Utils.ToHtml(_content);
            model.user_ip = Vincent._DTcms.DTRequest.GetIP();
            model.is_lock = siteConfig.commentstatus; //审核开关
            model.add_time = DateTime.Now;
            model.is_reply = 0;
            model.comment_type = 0;
            if (bll.Add(model) > 0)
            {
                context.Response.Write("{\"status\": 1, \"msg\": \"恭喜您，评论提交成功啦！\"}");
                return;
            }
            context.Response.Write("{\"status\": 0, \"msg\": \"对不起，保存过程中发生错误！\"}");
            return;
        }
        #endregion

        /// <summary>
        /// 获取商品信息
        /// </summary>
        /// <param name="context"></param>
        private void GetGoodsInfos(HttpContext context)
        {
            int goodsId = Vincent._DTcms.DTRequest.GetInt("goodsId", 0);

            BLL.article art = new BLL.article();
            var info = art.GetModel(goodsId);

            // Vincent._Json json = new _Json();


            context.Response.Write(ObjectToJSON(info));
        }

        /// <summary>
        /// 获取品类名称
        /// </summary>
        /// <param name="context"></param>
        private void GetCategory(HttpContext context)
        {
            string outmsg = "{\"status\":0,\"msg\":\"获取失败！\"}";
            int category_id = Vincent._DTcms.DTRequest.GetInt("category_id",0);
            BuysingooShop.BLL.article_category bll = new BuysingooShop.BLL.article_category();
            BuysingooShop.Model.article_category model = bll.GetModel(category_id);
            if (model != null)
            {
                outmsg = ObjectToJSON(model);
            }
            context.Response.Write(outmsg);
        }

        /// <summary>
        /// 获取产品图片
        /// </summary>
        /// <param name="context"></param>
        private void Getproductimg(HttpContext context)
        {
            string outmsg = "{\"status\":0,\"msg\":\"获取失败！\"}";
            int goodsId = Vincent._DTcms.DTRequest.GetInt("goodsId", 0);
            BuysingooShop.BLL.article_albums bll = new BuysingooShop.BLL.article_albums();
            DataTable dt = bll.GetList(" article_id=" + goodsId).Tables[0];
            if (dt != null && dt.Rows.Count>0)
            {
                outmsg = CreateJsonParameters(dt);
            }
            context.Response.Write(outmsg);
        }

        #region 取得评论列表方法OK=============================
        private void comment_list(HttpContext context)
        {
            int totalcount;
            StringBuilder strTxt = new StringBuilder();

            BLL.article_comment bll = new BLL.article_comment();
            DataSet ds = bll.GetList(20, 1, "", "add_time asc", out totalcount);
            //如果记录存在
            if (ds.Tables[0].Rows.Count > 0)
            {
                strTxt.Append("[");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];
                    strTxt.Append("{");
                    strTxt.Append("\"user_id\":" + dr["user_id"]);
                    strTxt.Append(",\"user_name\":\"" + dr["user_name"] + "\"");
                    Model.users userModel = new BLL.users().GetModel(Convert.ToInt32(dr["user_id"]));
                    strTxt.Append("");
                    strTxt.Append(",\"content\":\"" + Microsoft.JScript.GlobalObject.escape(dr["content"]) + "\"");
                    strTxt.Append(",\"add_time\":\"" + dr["add_time"] + "\"");
                    strTxt.Append(",\"is_reply\":" + dr["is_reply"]);
                    strTxt.Append(",\"reply_content\":\"" + Microsoft.JScript.GlobalObject.escape(dr["reply_content"]) + "\"");
                    strTxt.Append(",\"reply_time\":\"" + dr["reply_time"] + "\"");
                    strTxt.Append("}");
                    //是否加逗号
                    if (i < ds.Tables[0].Rows.Count - 1)
                    {
                        strTxt.Append(",");
                    }

                }
                strTxt.Append("]");
            }
            context.Response.Write(strTxt.ToString());
        }
        #endregion

        #region 修改评论方法OK=================================
        private void comment_update(HttpContext context)
        {
            int _id = Vincent._DTcms.Utils.StrToInt(Vincent._DTcms.DTRequest.GetFormString("id"), 0);
            int article_id = Vincent._DTcms.Utils.StrToInt(Vincent._DTcms.DTRequest.GetFormString("article_id"), 0);
            string _content = Vincent._DTcms.DTRequest.GetFormString("txtContent");
            BLL.article_comment bll = new BLL.article_comment();
            Model.article_comment model = bll.GetModel(_id);
            model.content = _content;
            model.reply_content = null;


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
            Model.article artModel = new BLL.article().GetModel(article_id);
            if (artModel == null)
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"对不起，主题不存在或已删除！\"}");
                return;
            }
            if (bll.Update(model))
            {
                context.Response.Write("{\"status\": 1, \"msg\": \"恭喜您，评价修改成功啦！\"}");
                return;
            }
            context.Response.Write("{\"status\": 0, \"msg\": \"对不起，保存过程中发生错误！\"}");
            return;
        }
        #endregion

        #region 提交投诉的处理方法OK===========================
        private void complaint_add(HttpContext context)
        {
            StringBuilder strTxt = new StringBuilder();
            BLL.complian bll = new BLL.complian();
            Model.complian model = new Model.complian();

            //string code = Vincent._DTcms.DTRequest.GetFormString("txtCode");
            string _title = Vincent._DTcms.DTRequest.GetFormString("txtTitle");
            string order_id = Vincent._DTcms.DTRequest.GetFormString("order_id");
            string _content = Vincent._DTcms.DTRequest.GetFormString("txtContent");
            string target = Vincent._DTcms.DTRequest.GetFormString("target");

            ////校检验证码
            //string result = verify_code(context, code);
            //if (result != "success")
            //{
            //    context.Response.Write(result);
            //    return;
            //}


            //检查内容
            if (string.IsNullOrEmpty(_content))
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"对不起，请输入投诉的内容！\"}");
                return;
            }
            //检查标题
            if (string.IsNullOrEmpty(_title))
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"对不起，请输入投诉的标题！\"}");
                return;
            }
            //检查objects
            if (string.IsNullOrEmpty(target))
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"对不起，请输入投诉的对象！\"}");
                return;
            }

            //检查用户是否登录
            int user_id = 0;
            string user_name = "匿名用户";
            Model.users userModel = new Web.UI.BasePage().GetUserInfo();
            if (userModel != null)
            {
                user_id = userModel.id;
                user_name = userModel.user_name;
            }
            model.com_type = 1;
            model.complian_content = _content;
            model.complian_time = DateTime.Now;
            model.complian_title = _title;
            model.is_status = 1;
            model.user_id = user_id;
            model.target = target;
            model.user_name = user_name;
            model.order_no = order_id;
            if (bll.Add(model) > 0)
            {
                context.Response.Write("{\"status\": 1, \"msg\": \"恭喜您，投诉提交成功啦！\"}");
                return;
            }
            context.Response.Write("{\"status\": 0, \"msg\": \"对不起，保存过程中发生错误！\"}");
            return;
        }
        #endregion


        private void GetParameter(HttpContext context)
        {
            int id = Vincent._DTcms.DTRequest.GetQueryInt("id");
            //DataTable dt = new BLL.article().GetList(0, " id=169 and id=170 and id=171 and id=144 ", "id").Tables[0];
            BuysingooShop.Model.article dt = new BLL.article().GetModel(id);
            context.Response.Write(ObjectToJSON(dt));
            return;
        }


        /// <summary>
        /// 将dataTable转换成json字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string CreateJsonParameters(DataTable dt)
        {
            /**/
            /**/
            /**/
            /* /****************************************************************************
      * Without goingin to the depth of the functioning of this Method, i will try to give an overview
      * As soon as this method gets a DataTable it starts to convert it into JSON String,
      * it takes each row and in each row it grabs the cell name and its data.
      * This kind of JSON is very usefull when developer have to have Column name of the .
      * Values Can be Access on clien in this way. OBJ.HEAD[0].<ColumnName>
      * NOTE: One negative point. by this method user will not be able to call any cell by its index.
     * *************************************************************************/
            StringBuilder JsonString = new StringBuilder();
            //Exception Handling        
            if (dt != null && dt.Rows.Count > 0)
            {
                JsonString.Append("{");
                JsonString.Append("\"T_blog\":[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    JsonString.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j < dt.Columns.Count - 1)
                        {
                            JsonString.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + "\"" + dt.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == dt.Columns.Count - 1)
                        {
                            JsonString.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + "\"" + dt.Rows[i][j].ToString() + "\"");
                        }
                    }
                    /**/
                    /**/
                    /**/
                    /*end Of String*/
                    if (i == dt.Rows.Count - 1)
                    {
                        JsonString.Append("}");
                    }
                    else
                    {
                        JsonString.Append("},");
                    }
                }
                JsonString.Append("]}");
                return JsonString.ToString();
            }
            else
            {
                return null;
            }
        }


        #region 退换货=========================================
        private void submit_refund(HttpContext context)
        {
            string goodId = Vincent._DTcms.DTRequest.GetFormString("goods_id");
            string txtContent = Vincent._DTcms.DTRequest.GetFormString("txtContent");
            string ordernumber = Vincent._DTcms.DTRequest.GetFormString("ordernumber");

            Model.orders orderModel = new BLL.orders().GetModel(ordernumber);
            if (ordernumber == "" || orderModel == null)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，订单号不存在或已删除！\"}");
                return;
            }

            if (goodId == "")
            {
                context.Response.Write("{status:0,msg:参数传输错误！}");
                return;
            }
            if (txtContent == "")
            {
                context.Response.Write("{status:0,msg:请输入退货货的理由！}");
                return;
            }
            //检查用户是否登录
            Model.users userModel = new BasePage().GetUserInfo();
            if (userModel == null)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，用户尚未登录或已超时！\"}");
                return;
            }

            //添加退货记录
            Model.user_point_log model = new Model.user_point_log();
            BLL.user_point_log bll = new BLL.user_point_log();

            //添加退货
            BLL.refund bll_refund = new BLL.refund();
            Model.refund model_refund = new Model.refund();

            ////是否存在记录
            //if (bll_refund.Exists(ordernumber))
            //{
            //    context.Response.Write("{\"status\": 0, \"msg\": \"退款单已经存在！\"}");
            //    return;
            //}


            model_refund.user_id = userModel.id;
            model_refund.user_name = userModel.user_name;
            model_refund.order_no = ordernumber;
            model_refund.refund_status = 1;
            model_refund.refund_fee = 0;
            model_refund.refund_reason = txtContent;
            model_refund.refund_no = "R" + GetOrderNumber(); //退款单号以R开头为商品订单
            model_refund.order_goods = orderModel.order_goods;
            model_refund.refund_money = orderModel.real_amount;
            model_refund.apply_time = DateTime.Now;
            model_refund.number = GetOrderNumber();

            //检测订单是否存在（存在的话只是修改，不存在则新增）
            //

            int j = 0;
            if (bll_refund.Exists(ordernumber))//存在
            {
                Model.refund model_refund1 = bll_refund.GetorderModel(ordernumber);

                model_refund1.user_id = userModel.id;
                model_refund1.user_name = userModel.user_name;
                model_refund1.order_no = ordernumber;
                model_refund1.refund_status = 1;
                model_refund1.refund_fee = 0;
                model_refund1.refund_reason = txtContent;
                model_refund1.order_goods = orderModel.order_goods;
                model_refund1.refund_money = orderModel.real_amount;
                model_refund1.apply_time = DateTime.Now;
                model_refund1.number = GetOrderNumber();

                

                model.user_id = userModel.id;
                model.user_name = userModel.user_name;
                model.remark = txtContent;
                model.add_time = DateTime.Now;
                model.pointtype = 3;
                model.order_status = 1;
                model.refund_no = model_refund1.number;
                model.order_no = model_refund1.order_no;
                model.amount = model_refund1.refund_money;



                if (bll.Addrecharge(model,false) > 0 && bll_refund.Update(model_refund1))
                {
                    j = 1;
                    context.Response.Write("{\"status\":1, \"msg\":\"申请提交成功，等待审核\"}");
                    return;
                }
            }
            else//不存在
            {
                model.user_id = userModel.id;
                model.user_name = userModel.user_name;
                model.remark = txtContent;
                model.add_time = DateTime.Now;
                model.pointtype = 3;
                model.order_status = 1;
                model.refund_no = model_refund.number;
                model.order_no = model_refund.order_no;
                model.amount = model_refund.refund_money;

                if (bll.Addrecharge(model,false) > 0 && bll_refund.Add(model_refund) > 0)
                {
                    j = 1;
                    context.Response.Write("{\"status\":1, \"msg\":\"申请提交成功，等待审核\"}");
                    return;
                }
            }

            if (j == 1)
            {
                context.Response.Write("{\"status\":1, \"msg\":\"申请提交成功，等待审核\"}");
                return;
            }
            //if (model == null)
            //{
            //    context.Response.Write("{\"status\": 0, \"msg\": \"退款单不存在或已被删除！\"}");
            //    return;
            //}

            
            context.Response.Write("{\"status\":0, \"msg\":\"数据保存出错\"}");
            return;
        }
        #endregion

        /// <summary>
        /// 提交退货信息
        /// </summary>
        /// <param name="context"></param>
        private void refund_no(HttpContext context) 
        {
            string order_no = Vincent._DTcms.DTRequest.GetQueryString("order_no");
            string express_no = Vincent._DTcms.DTRequest.GetQueryString("express_no");
            string express_code = Vincent._DTcms.DTRequest.GetQueryString("express_code");

            Model.refund model = new BLL.refund().GetorderModel(order_no);
            if (model == null)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"该退货单号不存在\"}");
                return;
            }

            if (model.express_no != "")
            {
                context.Response.Write("{\"status\":0, \"msg\":\"不能重复提交\"}");
                return;
            }

            model.express_no = express_no;
            model.express_code = express_code;
            if (new BLL.refund().Update(model))
            {
                context.Response.Write("{\"status\":1, \"msg\":\"提交成功\"}");
                return;
            }

            context.Response.Write("{\"status\":0, \"msg\":\"提交失败\"}");
            return;

        }



        /// <summary>
        /// 根据日期和随机码生成订单号
        /// </summary>
        /// <returns></returns>
        public static string GetOrderNumber()
        {
            string num = DateTime.Now.ToString("yyMMddHHmmss");//yyyyMMddHHmmssms
            return num + Number(2).ToString();
        }

        /// <summary>
        /// 生成随机数字
        /// </summary>
        /// <param name="length">生成长度</param>
        /// <returns></returns>
        public static string Number(int Length)
        {
            return Number(Length, false);
        }

        /// <summary>
        /// 生成随机数字
        /// </summary>
        /// <param name="Length">生成长度</param>
        /// <param name="Sleep">是否要在生成前将当前线程阻止以避免重复</param>
        /// <returns></returns>
        public static string Number(int Length, bool Sleep)
        {
            if (Sleep)
                System.Threading.Thread.Sleep(3);
            string result = "";
            System.Random random = new Random();
            for (int i = 0; i < Length; i++)
            {
                result += random.Next(10).ToString();
            }
            return result;
        }

        #region 修改用户信息OK=================================
        private void user_info_edit(HttpContext context)
        {
            //检查用户是否登录
            Model.users model = new BasePage().GetUserInfo();
            if (model == null)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，用户尚未登录或已超时！\"}");
                return;
            }
            string email = Vincent._DTcms.Utils.ToHtml(Vincent._DTcms.DTRequest.GetFormString("txtEmail"));
            string nick_name = Vincent._DTcms.Utils.ToHtml(Vincent._DTcms.DTRequest.GetFormString("txtNickName"));
            string sex = Vincent._DTcms.DTRequest.GetFormString("rblSex");
            string birthday = Vincent._DTcms.DTRequest.GetFormString("txtBirthday");
            string telphone = Vincent._DTcms.Utils.ToHtml(Vincent._DTcms.DTRequest.GetFormString("txtTelphone"));
            string mobile = Vincent._DTcms.Utils.ToHtml(Vincent._DTcms.DTRequest.GetFormString("txtMobile"));
            string qq = Vincent._DTcms.Utils.ToHtml(Vincent._DTcms.DTRequest.GetFormString("txtQQ"));
            string address = Vincent._DTcms.Utils.ToHtml(context.Request.Form["txtAddress"]);
            string safe_question = Vincent._DTcms.Utils.ToHtml(context.Request.Form["txtSafeQuestion"]);
            string safe_answer = Vincent._DTcms.Utils.ToHtml(context.Request.Form["txtSafeAnswer"]);
            //检查昵称
            if (nick_name == "")
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，请输入您的姓名昵称！\"}");
                return;
            }
            //检查邮箱
            if (userConfig.emaillogin == 1 && email == "")
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，请输入您邮箱帐号！\"}");
                return;
            }
            //检查手机
            if (userConfig.mobilelogin == 1 && mobile == "")
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，请输入手机号码！\"}");
                return;
            }

            //开始写入数据库
            model.email = email;
            model.nick_name = nick_name;
            model.sex = sex;
            DateTime _birthday;
            if (DateTime.TryParse(birthday, out _birthday))
            {
                model.birthday = _birthday;
            }
            model.telphone = telphone;
            model.mobile = mobile;
            model.qq = qq;
            model.address = address;
            model.safe_question = safe_question;
            model.safe_answer = safe_answer;


            new BLL.users().Update(model);
            context.Response.Write("{\"status\":1, \"msg\":\"账户资料已修改成功！\"}");
            return;
        }
        #endregion

        #region 确认裁剪用户头像OK=============================
        private void user_avatar_crop(HttpContext context)
        {
            //检查用户是否登录
            Model.users model = new BasePage().GetUserInfo();
            if (model == null)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，用户尚未登录或已超时！\"}");
                return;
            }
            string fileName = Vincent._DTcms.DTRequest.GetFormString("hideFileName");
            int x1 = Vincent._DTcms.DTRequest.GetFormInt("hideX1");
            int y1 = Vincent._DTcms.DTRequest.GetFormInt("hideY1");
            int w = Vincent._DTcms.DTRequest.GetFormInt("hideWidth");
            int h = Vincent._DTcms.DTRequest.GetFormInt("hideHeight");
            //检查是否图片

            //检查参数
            if (!Vincent._DTcms.Utils.FileExists(fileName) || w == 0 || h == 0)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，请先上传一张图片！\"}");
                return;
            }
            //取得保存的新文件名
            UpLoad upFiles = new UpLoad();
            bool result = upFiles.cropSaveAs(fileName, fileName, 180, 180, w, h, x1, y1);
            if (!result)
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"图片裁剪过程中发生意外错误！\"}");
                return;
            }
            //删除原用户头像
            Vincent._DTcms.Utils.DeleteFile(model.avatar);
            model.avatar = fileName;
            //修改用户头像
            new BLL.users().UpdateField(model.id, "avatar='" + model.avatar + "'");
            context.Response.Write("{\"status\": 1, \"msg\": \"" + model.avatar + "\"}");
            return;
        }
        #endregion

        #region 修改登录密码OK=================================
        private void user_password_edit(HttpContext context)
        {
            //检查用户是否登录
            Model.users model = new BasePage().GetUserInfo();
            if (model == null)
            {
                //context.Response.Write("{\"info\":\"对不起，用户尚未登录或已超时！！\", \"status\":\"n\"}");
                //context.Response.Write("<script type='text/javascript'>alert('对不起,用户尚未登录或已超时！！');window.location.href='/login.html'</script>");
                context.Response.Write("{\"status\":\"n\",\"msg\":\"对不起，用户尚未登录或已超时！\"}");
                return;
            }
            string oldpassword = Vincent._DTcms.DTRequest.GetFormString("txtPwd");
            string password = Vincent._DTcms.DTRequest.GetFormString("txtPwd2");
            //检查输入的旧密码
            if (string.IsNullOrEmpty(oldpassword))
            {
                //context.Response.Write("{\"info\":\"请输入您的旧登录密码\", \"status\":\"n\"}");
                //context.Response.Write("<script type='text/javascript'>alert('请输入您的旧登录密码！！');</script>");
                context.Response.Write("{\"status\":\"n\",\"msg\":\"请输入您的旧登录密码！\"}");
                return;
            }
            //检查输入的新密码
            if (string.IsNullOrEmpty(password))
            {
                //context.Response.Write("{\"info\":\"请输入您的新登录密码\", \"status\":\"n\"}");
                //context.Response.Write("<script type='text/javascript'>alert('请输入您的新登录密码！！');window.location.href='/user/total_set1.html'</script>");
                context.Response.Write("{\"status\":\"n\",\"msg\":\"请输入您的新登录密码！\"}");
                return;
            }
            //旧密码是否正确
            if (model.password != _DESEncrypt.Encrypt(oldpassword, model.salt))
            {
                //context.Response.Write("{\"info\":\"对不起，您输入的旧密码不正确！\", \"status\":\"n\"}");
                //context.Response.Write("<script type='text/javascript'>alert('对不起，您输入的旧密码不正确！！');window.location.href='/user/total_set1.html'</script>");
                context.Response.Write("{\"status\":\"n\",\"msg\":\"对不起，您输入的旧密码不正确！\"}");
                return;
            }
            //执行修改操作
            model.password = _DESEncrypt.Encrypt(password, model.salt);
            new BLL.users().Update(model);
            //context.Response.Write("{\"info\":\"您的密码已修改成功，请记住新密码！\", \"status\":\"y\"}");
            //context.Response.Write("<script type='text/javascript'>alert('您的密码已修改成功，请记住新密码！！');window.location.href='/user/total_set1.html'</script>");
            context.Response.Write("{\"status\":\"y\",\"msg\":\"您的密码已修改成功，请记住新密码！\"}");
            return;
        }
        #endregion

        #region 修改邮箱OK=====================================
        private void user_email_edit(HttpContext context)
        {
            //检查用户是否登录
            Model.users model = new BasePage().GetUserInfo();
            if (model == null)
            {
                context.Response.Write("{\"info\":\"对不起，用户尚未登录或已超时！！\", \"status\":\"n\"}");
                return;
            }
            //string txtemail = Vincent._DTcms.DTRequest.GetFormString("txtEmail");
            //string txtemailmsg = Vincent._DTcms.DTRequest.GetFormString("txtEmailMsg");
            ////邮箱验证码是否正确
            //if (txtemailmsg != Vincent._DTcms.Utils.GetCookie(Vincent._DTcms.DTKeys.SESSION_SMS_CODE))
            //{
            //    context.Response.Write("{\"info\":\"对不起，您输入的邮箱验证码不正确！\", \"status\":\"n\"}");
            //    return;
            //}
            ////执行修改操作
            //model.mobile = txtemail;
            //new BLL.users().Update(model);
            //context.Response.Write("{\"info\":\"您的邮箱已修改成功！\", \"status\":\"y\"}");
            return;
        }
        #endregion

        #region 修改手机号OK===================================
        private void user_mobile_edit(HttpContext context)
        {
            //检查用户是否登录
            Model.users model = new BasePage().GetUserInfo();
            if (model == null)
            {
                context.Response.Write("{\"info\":\"对不起，用户尚未登录或已超时！！\", \"status\":\"n\"}");
                return;
            }
            string txtMobile = Vincent._DTcms.DTRequest.GetFormString("txtMobile");
            string txtMobileMsg = Vincent._DTcms.DTRequest.GetFormString("txtMobileMsg");
            //短信验证码是否正确
            if (txtMobileMsg != Vincent._DTcms.Utils.GetCookie(Vincent._DTcms.DTKeys.SESSION_SMS_CODE))
            {
                context.Response.Write("{\"info\":\"对不起，您输入的验证码不正确！\", \"status\":\"n\"}");
                return;
            }
            //执行修改操作
            model.mobile = txtMobile;
            new BLL.users().Update(model);
            context.Response.Write("{\"info\":\"您的手机号已修改成功！\", \"status\":\"y\"}");
            return;
        }
        #endregion

        #region 邮箱取回密码===================================
        private void user_getpassword(HttpContext context)
        {
            string code = Vincent._DTcms.DTRequest.GetFormString("txtCode");
            string username = Vincent._DTcms.DTRequest.GetFormString("txtUserName").Trim();
            //检查用户名是否正确
            if (string.IsNullOrEmpty(username))
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，用户名不可为空！\"}");
                return;
            }
            //校检验证码
            string result = verify_code(context, code);
            if (result != "success")
            {
                context.Response.Write(result);
                return;
            }
            //检查用户信息
            BLL.users bll = new BLL.users();
            Model.users model = bll.GetModel(username);
            if (model == null)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，您输入的用户名不存在！\"}");
                return;
            }
            if (string.IsNullOrEmpty(model.email))
            {
                context.Response.Write("{\"status\":0, \"msg\":\"您尚未设置邮箱地址，无法使用取回密码功能！\"}");
                return;
            }
            //生成随机码
            string strcode = Vincent._DTcms.Utils.GetCheckCode(20);
            //获得邮件内容
            Model.mail_template mailModel = new BLL.mail_template().GetModel("getpassword");
            if (mailModel == null)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"邮件发送失败，邮件模板内容不存在！\"}");
                return;
            }
            //检查是否重复提交
            BLL.user_code codeBll = new BLL.user_code();
            Model.user_code codeModel;
            codeModel = codeBll.GetModel(username, Vincent._DTcms.DTEnums.CodeEnum.RegVerify.ToString(), "d");
            if (codeModel == null)
            {
                codeModel = new Model.user_code();
                //写入数据库
                codeModel.user_id = model.id;
                codeModel.user_name = model.user_name;
                codeModel.type = Vincent._DTcms.DTEnums.CodeEnum.Password.ToString();
                codeModel.str_code = strcode;
                codeModel.eff_time = DateTime.Now.AddDays(userConfig.regemailexpired);
                codeModel.add_time = DateTime.Now;
                codeBll.Add(codeModel);
            }
            //替换模板内容
            string titletxt = mailModel.maill_title;
            string bodytxt = mailModel.content;
            titletxt = titletxt.Replace("{webname}", siteConfig.webname);
            titletxt = titletxt.Replace("{username}", model.user_name);
            bodytxt = bodytxt.Replace("{webname}", siteConfig.webname);
            bodytxt = bodytxt.Replace("{weburl}", siteConfig.weburl);
            bodytxt = bodytxt.Replace("{webtel}", siteConfig.webtel);
            bodytxt = bodytxt.Replace("{valid}", userConfig.regemailexpired.ToString());
            bodytxt = bodytxt.Replace("{username}", model.user_name);
            bodytxt = bodytxt.Replace("{linkurl}", "http://" + HttpContext.Current.Request.Url.Authority.ToLower() + new BasePage().linkurl("repassword", "reset", strcode));
            //发送邮件
            try
            {
                _Email.SendMail(siteConfig.emailsmtp,
                    siteConfig.emailusername,
                    _DESEncrypt.Decrypt(siteConfig.emailpassword),
                    siteConfig.emailnickname,
                    siteConfig.emailfrom,
                    model.email,
                    titletxt, bodytxt);
            }
            catch
            {
                context.Response.Write("{\"status\":0, \"msg\":\"邮件发送失败，请联系本站管理员！\"}");
                return;
            }
            context.Response.Write("{\"status\":1, \"msg\":\"邮件发送成功，请登录您的邮箱找回登录密码！\"}");
            return;
        }
        #endregion

        /// <summary>
        /// 邮箱验证找回密码
        /// </summary>
        /// <param name="context"></param>
        private void user_getmailvalidate(HttpContext context)
        {
            string email = Vincent._DTcms.DTRequest.GetFormString("email");

            //检查用户名是否正确
            if (string.IsNullOrEmpty(email))
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，邮箱不可为空！\"}");
                return;
            }
            BLL.users bll = new BLL.users();
            Model.users model = bll.GetModels(email);
            if (model == null)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，您输入的邮箱不存在！\"}");
                return;
            }
            if (string.IsNullOrEmpty(model.email))
            {
                context.Response.Write("{\"status\":0, \"msg\":\"您尚未设置邮箱地址，无法使用取回密码功能！\"}");
                return;
            }
            //生成随机码
            string strcode = Vincent._DTcms.Utils.GetCheckCode(20);
            //获得邮件内容
            Model.mail_template mailModel = new BLL.mail_template().GetModel("getpassword");
            if (mailModel == null)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"邮件发送失败，邮件模板内容不存在！\"}");
                return;
            }
            //检查是否重复提交
            BLL.user_code codeBll = new BLL.user_code();
            Model.user_code codeModel;
            codeModel = codeBll.GetModel(model.user_name, Vincent._DTcms.DTEnums.CodeEnum.Password.ToString(), "d");
            if (codeModel == null)
            {
                codeModel = new Model.user_code();
                //写入数据库
                codeModel.user_id = model.id;
                codeModel.user_name = model.user_name;
                codeModel.type = Vincent._DTcms.DTEnums.CodeEnum.Password.ToString();
                codeModel.str_code = strcode;
                codeModel.eff_time = DateTime.Now.AddDays(userConfig.regemailexpired);
                codeModel.add_time = DateTime.Now;
                codeBll.Add(codeModel);
            }
            //替换模板内容
            string titletxt = mailModel.maill_title;
            string bodytxt = mailModel.content;
            titletxt = titletxt.Replace("{webname}", siteConfig.webname);
            titletxt = titletxt.Replace("{username}", model.user_name);
            bodytxt = bodytxt.Replace("{webname}", siteConfig.webname);
            bodytxt = bodytxt.Replace("{weburl}", siteConfig.weburl);
            bodytxt = bodytxt.Replace("{webtel}", siteConfig.webtel);
            bodytxt = bodytxt.Replace("{valid}", userConfig.regemailexpired.ToString());
            bodytxt = bodytxt.Replace("{username}", model.user_name);
            bodytxt = bodytxt.Replace("{linkurl}", "http://" + HttpContext.Current.Request.Url.Authority.ToLower() + new BasePage().linkurl("repassword", "reset", strcode));
            //发送邮件
            try
            {
                _Email.SendEmail(siteConfig.emailfrom, model.email, titletxt, bodytxt, siteConfig.emailsmtp,
                    siteConfig.emailusername,
                    _DESEncrypt.Decrypt(siteConfig.emailpassword)
                    );
            }
            catch
            {
                context.Response.Write("{\"status\":0, \"msg\":\"邮件发送失败，请联系本站管理员！\"}");
                return;
            }


            ////发送邮件
            //try
            //{
            //    _Email.SendEmail("smileykeke@126.com", model.email, titletxt, bodytxt, siteConfig.emailsmtp,
            //        "smileykeke",
            //        "15268973529wang."
            //        );
            //}
            //catch
            //{
            //    context.Response.Write("{\"status\":0, \"msg\":\"邮件发送失败，请联系本站管理员！\"}");
            //    return;
            //}

            //("LLorJJ999@163.com", "773872823@qq.com", "测试",
            //     "aadsdfds", "smtp.163.com", "LLorJJ999@163.com", "XXXXXX")
            context.Response.Write("{\"status\":1, \"msg\":\"邮件发送成功，请登录您的邮箱找回登录密码！\"}");
            return;
        }


        /// <summary>
        /// 邮箱验证
        /// </summary>
        /// <param name="context"></param>
        private void user_validatepostbox(HttpContext context)
        {
            //string code = Vincent._DTcms.DTRequest.GetFormString("txtCode");
            string username = Vincent._DTcms.DTRequest.GetFormString("username").Trim();
            string userEmail = Vincent._DTcms.DTRequest.GetFormString("txtEmail").Trim();
            //检查用户名是否正确
            if (string.IsNullOrEmpty(username))
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，用户名不可为空！\"}");
                return;
            }
            if (string.IsNullOrEmpty(userEmail))
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，邮箱不能为空！\"}");
                return;
            }
            //校检验证码
            //string result = verify_code(context, code);
            //if (result != "success")
            //{
            //    context.Response.Write(result);
            //    return;
            //}
            //检查用户信息
            BLL.users bll = new BLL.users();
            Model.users model = bll.GetModel(username);
            if (model.isEmail == 1)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"你的邮箱已经激活，请勿重复激活！\"}");
                return;
            }
            model.email=userEmail;
            if (!bll.Update(model))
            {
                context.Response.Write("{\"status\":0, \"msg\":\"邮箱验证失败！\"}");
                return;
            }
            Model.users model1 = bll.GetModel(username);
            //生成随机码
            string strcode = Vincent._DTcms.Utils.GetCheckCode(20);
            //获得邮件内容
            Model.mail_template mailModel = new BLL.mail_template().GetModel("regverify");
            if (mailModel == null)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"邮件发送失败，邮件模板内容不存在！\"}");
                return;
            }
            //检查是否重复提交
            BLL.user_code codeBll = new BLL.user_code();
            Model.user_code codeModel;
            codeModel = codeBll.GetModel(username, Vincent._DTcms.DTEnums.CodeEnum.RegVerify.ToString(), "d");
            if (codeModel == null)
            {
                codeModel = new Model.user_code();
                //写入数据库
                codeModel.user_id = model1.id;
                codeModel.user_name = model1.user_name;
                codeModel.type = Vincent._DTcms.DTEnums.CodeEnum.RegVerify.ToString();
                codeModel.str_code = strcode;
                codeModel.eff_time = DateTime.Now.AddDays(userConfig.regemailexpired);
                codeModel.add_time = DateTime.Now;
                codeBll.Add(codeModel);
            }
            //替换模板内容
            string titletxt = mailModel.maill_title;
            string bodytxt = mailModel.content;
            titletxt = titletxt.Replace("{webname}", siteConfig.webname);
            titletxt = titletxt.Replace("{username}", model1.user_name);
            bodytxt = bodytxt.Replace("{webname}", siteConfig.webname);
            bodytxt = bodytxt.Replace("{weburl}", siteConfig.weburl);
            bodytxt = bodytxt.Replace("{webtel}", siteConfig.webtel);
            bodytxt = bodytxt.Replace("{valid}", userConfig.regemailexpired.ToString());
            bodytxt = bodytxt.Replace("{username}", model1.user_name);
            bodytxt = bodytxt.Replace("{linkurl}", "http://" + HttpContext.Current.Request.Url.Authority.ToLower() + new BasePage().linkurl("repassword", "validatepost", strcode));
            //发送邮件
            try
            {
                _Email.SendEmail(siteConfig.emailfrom, model1.email, titletxt, bodytxt, siteConfig.emailsmtp,
                    siteConfig.emailusername,
                    _DESEncrypt.Decrypt(siteConfig.emailpassword)
                    );
            }
            catch(Exception e)
            {
                Vincent._Log.SaveMessage(e.Message + "地址：" + model1.email);
                context.Response.Write("{\"status\":0, \"msg\":\"邮件发送失败，请联系本站管理员！\"}");
                return;
            }


            ////发送邮件
            //try
            //{
            //    _Email.SendEmail("smileykeke@126.com", model.email, titletxt, bodytxt, siteConfig.emailsmtp,
            //        "smileykeke",
            //        "15268973529wang."
            //        );
            //}
            //catch
            //{
            //    context.Response.Write("{\"status\":0, \"msg\":\"邮件发送失败，请联系本站管理员！\"}");
            //    return;
            //}

            //("LLorJJ999@163.com", "773872823@qq.com", "测试",
            //     "aadsdfds", "smtp.163.com", "LLorJJ999@163.com", "XXXXXX")
            context.Response.Write("{\"status\":1, \"msg\":\"邮件发送成功，请登录您的邮箱激活邮箱验证！\"}");
            return;
        }


        /// <summary>
        /// 手机验证
        /// </summary>
        /// <param name="context"></param>
        private void user_mobilevalidate(HttpContext context)
        {
            string code = Vincent._DTcms.DTRequest.GetFormString("txtCode").Trim();
            string mobile = Vincent._DTcms.DTRequest.GetFormString("txtMobile").Trim();
            string username = Vincent._DTcms.DTRequest.GetFormString("username").Trim();
            if (mobile == null || mobile == "")
            {
                context.Response.Write("{\"status\":0, \"msg\":\"请输入手机号！\"}");
                return;
            }
            if (username == null || username == "")
            {
                context.Response.Write("{\"status\":0, \"msg\":\"请先登录！\"}");
                return;
            }
            if (code.ToLower() != (_Cookie.GetCookie(Vincent._DTcms.DTKeys.SESSION_SMS_CODE).ToString()).ToLower())
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，你的手机验证码不正确！\"}");
                return;
            }

            //验证用户是否存在
            BLL.users userBll = new BLL.users();
            Model.users userModel = userBll.GetModel(username);
            userModel.mobile = mobile;
            userModel.isMobile = 1;
            userBll.Update(userModel);
            context.Response.Write("{\"status\":1, \"msg\":\"手机验证成功！\"}");
            return;

        }

        #region 邮箱重设密码OK=================================
        private void user_repassword(HttpContext context)
        {
            string code = context.Request.Form["txtCode"];
            string strcode = context.Request.Form["hideCode"];
            string password = context.Request.Form["txtPassword"];

            //校检验证码
            string result = verify_code(context, code);
            if (result != "success")
            {
                context.Response.Write(result);
                return;
            }
            //检查验证字符串
            if (string.IsNullOrEmpty(strcode))
            {
                context.Response.Write("{\"status\":0, \"msg\":\"邮件验证码不存在或已删除！\"}");
                return;
            }
            //检查输入的新密码
            if (string.IsNullOrEmpty(password))
            {
                context.Response.Write("{\"status\":0, \"msg\":\"请输入您的新密码！\"}");
                return;
            }

            BLL.user_code codeBll = new BLL.user_code();
            Model.user_code codeModel = codeBll.GetModel(strcode);
            if (codeModel == null)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"邮件验证码不存在或已过期！\"}");
                return;
            }
            //验证用户是否存在
            BLL.users userBll = new BLL.users();
            if (!userBll.Exists(codeModel.user_id))
            {
                context.Response.Write("{\"status\":0, \"msg\":\"该用户不存在或已被删除！\"}");
                return;
            }
            Model.users userModel = userBll.GetModel(codeModel.user_id);
            //执行修改操作
            userModel.password = _DESEncrypt.Encrypt(password, userModel.salt);
            userBll.Update(userModel);
            //更改验证字符串状态
            codeModel.count = 1;
            codeModel.status = 1;
            codeBll.Update(codeModel);
            context.Response.Write("{\"status\":1, \"msg\":\"修改密码成功，请记住新密码！\"}");
            return;
        }
        #endregion


        #region 邮箱验证重设密码OK=================================
        private void user_reMailPost(HttpContext context)
        {
            //string code = context.Request.Form["txtCode"];
            string strcode = Vincent._DTcms.DTRequest.GetQueryString("code");
            string password = Vincent._DTcms.DTRequest.GetFormString("txtPassword");

            //检查验证字符串
            if (string.IsNullOrEmpty(strcode))
            {
                context.Response.Write("{\"status\":0, \"msg\":\"邮件验证码不存在或已删除！\"}");
                return;
            }
            //检查输入的新密码
            if (string.IsNullOrEmpty(password))
            {
                context.Response.Write("{\"status\":0, \"msg\":\"请输入您的新密码！\"}");
                return;
            }

            BLL.user_code codeBll = new BLL.user_code();
            Model.user_code codeModel = codeBll.GetModel(strcode);
            if (codeModel == null)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"邮件验证码不存在或已过期！\"}");
                return;
            }
            //验证用户是否存在
            BLL.users userBll = new BLL.users();
            if (!userBll.Exists(codeModel.user_id))
            {
                context.Response.Write("{\"status\":0, \"msg\":\"该用户不存在或已被删除！\"}");
                return;
            }
            Model.users userModel = userBll.GetModel(codeModel.user_id);
            //执行修改操作
            userModel.password = _DESEncrypt.Encrypt(password, userModel.salt);
            userBll.Update(userModel);
            //更改验证字符串状态
            codeModel.count = 1;
            codeModel.status = 1;
            codeBll.Update(codeModel);
            context.Response.Write("{\"status\":1, \"msg\":\"修改密码成功，请记住新密码！\"}");
            return;
        }
        #endregion

        /// <summary>
        /// 手机验证重设密码
        /// </summary>
        /// <param name="context"></param>
        private void user_reMobile(HttpContext context)
        {
            //string code = context.Request.Form["txtCode"];
            string mobile = HttpContext.Current.Session["mobile"].ToString();
            string password = Vincent._DTcms.DTRequest.GetFormString("txtPassword");

            //检查验证字符串
            if (string.IsNullOrEmpty(mobile))
            {
                context.Response.Write("{\"status\":0, \"msg\":\"密码修改失败！\"}");
                return;
            }
            //检查输入的新密码
            if (string.IsNullOrEmpty(password))
            {
                context.Response.Write("{\"status\":0, \"msg\":\"请输入您的新密码！\"}");
                return;
            }

            //验证用户是否存在
            BLL.users userBll = new BLL.users();
            if (!userBll.ExistsMobile(mobile))
            {
                context.Response.Write("{\"status\":0, \"msg\":\"该手机号不存在！\"}");
                return;
            }
            Model.users userModel = userBll.GetModelMobile(mobile);
            //执行修改操作
            userModel.password = _DESEncrypt.Encrypt(password, userModel.salt);
            userBll.Update(userModel);
            context.Response.Write("{\"status\":1, \"msg\":\"修改密码成功，请记住新密码！\"}");
            return;
        }

        /// <summary>
        /// 判断是否验证
        /// </summary>
        /// <param name="context"></param>
        private void validateVerdict(HttpContext context)
        {
            int userid = Vincent._DTcms.DTRequest.GetFormInt("userid",0);
            if (userid == 0)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"还没有验证！\"}");
                return;
            }
            BLL.users userBll = new BLL.users();
            Model.users userModel = userBll.GetModel(userid);
            if (userModel.isEmail != 1&&userModel.isMobile!=1)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"还没有验证！\"}");
                return;
            }
            if (userModel.isMobile != 1 && userModel.isEmail == 1)
            {
                context.Response.Write("{\"status\":1, \"msg\":\"邮箱验证，手机没有验证！\"}");
                return;
            }
            if (userModel.isMobile == 1 && userModel.isEmail != 1)
            {
                context.Response.Write("{\"status\":2, \"msg\":\"手机验证，邮箱没有验证！\"}");
                return;
            }
            context.Response.Write("{\"status\":3, \"msg\":\"已经验证！\"}");
            return;
        }

        /// <summary>
        /// 邮箱确认
        /// </summary>
        /// <param name="context"></param>
        private void validatepost(HttpContext context)
        {
            //string strcode = context.Request.Form["code"];
            string strcode = Vincent._DTcms.DTRequest.GetQueryString("code");
            //string password = context.Request.Form["txtPassword"];

            //检查验证字符串
            if (string.IsNullOrEmpty(strcode))
            {
                context.Response.Write("{\"status\":0, \"msg\":\"邮件验证码不存在或已删除！\"}");
                return;
            }

            BLL.user_code codeBll = new BLL.user_code();
            Model.user_code codeModel = codeBll.GetModel(strcode);
            if (codeModel == null)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"邮件验证码不存在或已过期！\"}");
                return;
            }
            //验证用户是否存在
            BLL.users userBll = new BLL.users();
            if (!userBll.Exists(codeModel.user_id))
            {
                context.Response.Write("{\"status\":0, \"msg\":\"该用户不存在或已被删除！\"}");
                return;
            }
            Model.users userModel = userBll.GetModel(codeModel.user_id);
            userModel.isEmail = 1;
            userBll.Update(userModel);
            context.Response.Write("{\"status\":1, \"msg\":\"邮箱验证成功！\"}");
            return;
        }


        #region 删除短消息OK===================================
        private void user_message_delete(HttpContext context)
        {
            //检查用户是否登录
            Model.users model = new BasePage().GetUserInfo();
            if (model == null)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，用户尚未登录或已超时！\"}");
                return;
            }
            string check_id = Vincent._DTcms.DTRequest.GetFormString("checkId");
            if (check_id == "")
            {
                context.Response.Write("{\"status\":0, \"msg\":\"删除失败，请检查传输参数！\"}");
                return;
            }
            string[] arrId = check_id.Split(',');
            for (int i = 0; i < arrId.Length; i++)
            {
                new BLL.user_message().Delete(int.Parse(arrId[i]), model.user_name);
            }
            context.Response.Write("{\"status\":1, \"msg\":\"删除短消息成功！\"}");
            return;
        }
        #endregion

        #region 发布站内短消息OK===============================
        private void user_message_add(HttpContext context)
        {
            //检查用户是否登录
            Model.users model = new BasePage().GetUserInfo();
            if (model == null)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，用户尚未登录或已超时！\"}");
                return;
            }
            string code = context.Request.Form["txtCode"];
            string send_save = Vincent._DTcms.DTRequest.GetFormString("sendSave");
            string user_name = Vincent._DTcms.Utils.ToHtml(Vincent._DTcms.DTRequest.GetFormString("txtUserName"));
            string title = Vincent._DTcms.Utils.ToHtml(Vincent._DTcms.DTRequest.GetFormString("txtTitle"));
            string content = Vincent._DTcms.Utils.ToHtml(Vincent._DTcms.DTRequest.GetFormString("txtContent"));
            //校检验证码
            string result = verify_code(context, code);
            if (result != "success")
            {
                context.Response.Write(result);
                return;
            }
            //检查用户名
            if (user_name == "" || !new BLL.users().Exists(user_name))
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，该用户不存在或已删除！\"}");
                return;
            }
            //检查标题
            if (title == "")
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，请输入短消息标题！\"}");
                return;
            }
            //检查内容
            if (content == "")
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，请输入短消息内容！\"}");
                return;
            }
            //保存数据
            Model.user_message modelMessage = new Model.user_message();
            modelMessage.type = 2;
            modelMessage.post_user_name = model.user_name;
            modelMessage.accept_user_name = user_name;
            modelMessage.title = title;
            modelMessage.content = Vincent._DTcms.Utils.ToHtml(content);
            new BLL.user_message().Add(modelMessage);
            if (send_save == "true") //保存到发件箱
            {
                modelMessage.type = 3;
                new BLL.user_message().Add(modelMessage);
            }
            context.Response.Write("{\"status\":1, \"msg\":\"发布短信息成功！\"}");
            return;
        }
        #endregion

        #region 用户兑换积分OK=================================
        private void user_point_convert(HttpContext context)
        {
            //检查系统是否启用兑换积分功能
            if (userConfig.pointcashrate == 0)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，网站已关闭兑换积分功能！\"}");
                return;
            }
            //检查用户是否登录
            Model.users model = new BasePage().GetUserInfo();
            if (model == null)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，用户尚未登录或已超时！\"}");
                return;
            }
            int amout = Vincent._DTcms.DTRequest.GetFormInt("txtAmount");
            string password = Vincent._DTcms.DTRequest.GetFormString("txtPassword");
            if (model.amount < 1)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，您账户上的余额不足！\"}");
                return;
            }
            if (amout < 1)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，最小兑换金额为1元！\"}");
                return;
            }
            if (amout > model.amount)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，您兑换的金额大于账户余额！\"}");
                return;
            }
            if (password == "")
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，请输入您账户的密码！\"}");
                return;
            }
            //验证密码
            if (_DESEncrypt.Encrypt(password, model.salt) != model.password)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，您输入的密码不正确！\"}");
                return;
            }
            //计算兑换后的积分值
            int convertPoint = (int)(Convert.ToDecimal(amout) * userConfig.pointcashrate);
            //扣除金额
            int amountNewId = new BLL.user_amount_log().Add(model.id, model.user_name, Vincent._DTcms.DTEnums.AmountTypeEnum.Convert.ToString(), amout * -1, "用户兑换积分", 1);
            //增加积分
            if (amountNewId < 1)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"转换过程中发生错误，请重新提交！\"}");
                return;
            }
            int pointNewId = new BLL.user_point_log().Add(model.id, model.user_name, convertPoint, "用户兑换积分", true);
            if (pointNewId < 1)
            {
                //返还金额
                new BLL.user_amount_log().Add(model.id, model.user_name, Vincent._DTcms.DTEnums.AmountTypeEnum.Convert.ToString(), amout, "用户兑换积分失败，返还金额", 1);
                context.Response.Write("{\"status\":0, \"msg\":\"转换过程中发生错误，请重新提交！\"}");
                return;
            }

            context.Response.Write("{\"status\":1, \"msg\":\"恭喜您，积分兑换成功！\"}");
            return;
        }
        #endregion

        #region 删除用户积分明细OK=============================
        private void user_point_delete(HttpContext context)
        {
            //检查用户是否登录
            Model.users model = new BasePage().GetUserInfo();
            if (model == null)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，用户尚未登录或已超时！\"}");
                return;
            }
            string check_id = Vincent._DTcms.DTRequest.GetFormString("checkId");
            if (check_id == "")
            {
                context.Response.Write("{\"status\":0, \"msg\":\"删除失败，请检查传输参数！\"}");
                return;
            }
            string[] arrId = check_id.Split(',');
            for (int i = 0; i < arrId.Length; i++)
            {
                new BLL.user_point_log().Delete(int.Parse(arrId[i]), model.user_name);
            }
            context.Response.Write("{\"status\":1, \"msg\":\"积分明细删除成功！\"}");
            return;
        }
        #endregion

        #region 删除用户评价OK=================================
        private void user_comment_delete(HttpContext context)
        {
            //检查用户是否登录
            Model.users model = new BasePage().GetUserInfo();
            if (model == null)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，用户尚未登录或已超时！\"}");
                return;
            }
            string check_id = Vincent._DTcms.DTRequest.GetFormString("checkId");
            if (check_id == "")
            {
                context.Response.Write("{\"status\":0, \"msg\":\"删除失败，请检查传输参数！\"}");
                return;
            }
            string[] arrId = check_id.Split(',');
            for (int i = 0; i < arrId.Length; i++)
            {
                new BLL.article_comment().Delete(int.Parse(arrId[i]), model.user_name);
            }
            context.Response.Write("{\"status\":1, \"msg\":\"评价删除成功！\"}");
            return;
        }
        #endregion

        #region 用户在线充值OK=================================
        private void user_amount_recharge(HttpContext context)
        {
            //检查用户是否登录
            Model.users model = new BasePage().GetUserInfo();
            if (model == null)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，用户尚未登录或已超时！\"}");
                return;
            }
            decimal amount = Vincent._DTcms.DTRequest.GetFormDecimal("order_amount", 0);
            int payment_id = Vincent._DTcms.DTRequest.GetFormInt("payment_id");
            if (amount == 0)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，请输入正确的充值金额！\"}");
                return;
            }
            if (payment_id == 0)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，请选择正确的支付方式！\"}");
                return;
            }
            if (!new BLL.payment().Exists(payment_id))
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，支付方式不存在或已删除！\"}");
                return;
            }
            //生成订单号
            string order_no = "R" + Vincent._DTcms.Utils.GetOrderNumber(); //订单号R开头为充值订单
            new BLL.user_amount_log().Add(model.id, model.user_name, Vincent._DTcms.DTEnums.AmountTypeEnum.Recharge.ToString(), order_no,
                payment_id, amount, "账户充值(" + new BLL.payment().GetModel(payment_id).title + ")", 0);
            //保存成功后返回订单号
            context.Response.Write("{\"status\":1, \"msg\":\"订单保存成功！\", \"url\":\"" + new Web.UI.BasePage().linkurl("payment", "confirm", order_no) + "\"}");
            return;

        }
        #endregion

        #region 删除用户收支明细OK=============================
        private void user_amount_delete(HttpContext context)
        {
            //检查用户是否登录
            Model.users model = new BasePage().GetUserInfo();
            if (model == null)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，用户尚未登录或已超时！\"}");
                return;
            }
            string check_id = Vincent._DTcms.DTRequest.GetFormString("checkId");
            if (check_id == "")
            {
                context.Response.Write("{\"status\":0, \"msg\":\"删除失败，请检查传输参数！\"}");
                return;
            }
            string[] arrId = check_id.Split(',');
            for (int i = 0; i < arrId.Length; i++)
            {
                new BLL.user_amount_log().Delete(int.Parse(arrId[i]), model.user_name);
            }
            context.Response.Write("{\"status\":1, \"msg\":\"收支明细删除成功！\"}");
            return;
        }
        #endregion

        #region 删除用户优惠券OK===============================
        private void user_coupon_delete(HttpContext context)
        {
            //检查用户是否登录
            Model.users model = new BasePage().GetUserInfo();
            if (model == null)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，用户尚未登录或已超时！\"}");
                return;
            }
            string check_id = Vincent._DTcms.DTRequest.GetFormString("checkId");
            if (check_id == "")
            {
                context.Response.Write("{\"status\":0, \"msg\":\"删除失败，请检查传输参数！\"}");
                return;
            }
            string[] arrId = check_id.Split(',');
            for (int i = 0; i < arrId.Length; i++)
            {
                new BLL.user_coupon_log().Delete(int.Parse(arrId[i]), model.user_name);
            }
            context.Response.Write("{\"status\":1, \"msg\":\"优惠券删除成功！\"}");
            return;
        }
        #endregion

        #region 删除用户收货地址OK=============================
        private void user_address_delete(HttpContext context)
        {
            //检查用户是否登录
            Model.users model = new BasePage().GetUserInfo();
            if (model == null)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，用户尚未登录或已超时！\"}");
                return;
            }
            string check_id = Vincent._DTcms.DTRequest.GetFormString("checkId");
            if (check_id == "")
            {
                context.Response.Write("{\"status\":0, \"msg\":\"删除失败，请检查传输参数！\"}");
                return;
            }
            string[] arrId = check_id.Split(',');
            for (int i = 0; i < arrId.Length; i++)
            {
                new BLL.user_address().Delete(int.Parse(arrId[i]), model.id);
            }
            context.Response.Write("{\"status\":1, \"msg\":\"用户收货地址删除成功！\"}");
            return;
        }
        #endregion

        #region 设置用户默认收货地址OK=============================
        private void user_address_default(HttpContext context)
        {
            //检查用户是否登录
            Model.users model = new BasePage().GetUserInfo();
            if (model == null)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，用户尚未登录或已超时！\"}");
                return;
            }
            string check_id = Vincent._DTcms.DTRequest.GetFormString("checkId");
            if (check_id == "")
            {
                context.Response.Write("{\"status\":0, \"msg\":\"设置失败，请检查传输参数！\"}");
                return;
            }

            //设置默认地址
            new BLL.user_address().UpdateField("is_default=0", "user_id=" + model.id);
            new BLL.user_address().UpdateField("is_default=1", "id=" + Vincent._DTcms.Utils.StrToInt(check_id, 0) + " and user_id=" + model.id);

            context.Response.Write("{\"status\":1, \"msg\":\"用户默认收货地址设置成功！\"}");
            return;
        }
        #endregion

        #region 新增用户收货地址OK=================================
        private void user_address_add(HttpContext context)
        {
            string AcceptName = Vincent._DTcms.DTRequest.GetFormString("txtAcceptName");
            string Province = Vincent._DTcms.DTRequest.GetFormString("txtProvince");
            string City = Vincent._DTcms.DTRequest.GetFormString("txtCity");
            string Area = Vincent._DTcms.DTRequest.GetFormString("txtArea");
            string Street = Vincent._DTcms.DTRequest.GetFormString("txtStreet");
            int PostCode = Vincent._DTcms.DTRequest.GetFormInt("txtPostCode");
            string Mobile = Vincent._DTcms.DTRequest.GetFormString("txtMobile");
            string type = Vincent._DTcms.DTRequest.GetFormString("type");

            //检查用户是否登录
            Model.users model = new BasePage().GetUserInfo();
            if (model == null)
            {
                context.Response.Write("{\"info\":\"用户未登录！\", \"status\":3}");
                return;
                ////用户注册
                //BLL.users bll = new BLL.users();
                //Model.users user_model = new Model.users();

                ////生成随机
                //string smscoderand = new Random().Next(100000, 999999).ToString();

                ////保存注册信息
                //user_model.group_id = 1;//普通用户注册
                //user_model.user_name = Mobile;
                //user_model.salt = Vincent._DTcms.Utils.GetCheckCode(6); //获得6位的salt加密字符串
                //user_model.password = _DESEncrypt.Encrypt(smscoderand, user_model.salt);
                //user_model.mobile = Mobile;
                //user_model.reg_ip = Vincent._DTcms.DTRequest.GetIP();
                //user_model.reg_time = DateTime.Now;
                //user_model.strcode = Vincent._DTcms.Utils.GetCheckCode(20);//生成随机码
                //int newId = bll.Add(user_model);
                //if (newId > 0)
                //{
                //    model = bll.GetModel(newId);
                //    //写入coocik
                //    context.Session[Vincent._DTcms.DTKeys.SESSION_USER_INFO] = model;
                //    context.Session.Timeout = 45;

                //    //写短信数据，发SMS
                //    var message_name = _Utility.GetConfigAppSetting("message_name");
                //    var message_pwd = _Utility.GetConfigAppSetting("message_pwd");
                //    var message_content = _Utility.GetConfigAppSetting("message_content");
                //    message_content = message_content.Replace("num", smscoderand);

                //    _Message client = _Message.getInstance(message_name, message_pwd);
                //    string resultMT = client.MT(message_content, Mobile, "", "");

                //    var MessageNum = _Convert.ToInt64(resultMT, 0);
                //    if (MessageNum > 0)
                //    {
                //        context.Response.Write("{\"status\":1, \"msg\":\"恭喜你，你已注册为农夫优品的会员，你的密码为" + smscoderand + ",请注意查收你的短信\"}");
                //        return;
                //    }

                //}
                //else
                //{
                //    context.Response.Write("{\"status\":0, \"msg\":\"新增收货地址失败！\"}");
                //    return;
                //}

            }
            Model.user_address addressModel = new Model.user_address();
            BLL.user_address addressBll = new BLL.user_address();

            addressModel.user_id = model.id;
            addressModel.acceptName = AcceptName;
            addressModel.provinces = Province;
            addressModel.citys = City;
            addressModel.area = Area;
            addressModel.street = Street;
            addressModel.mobile = Mobile;
            addressModel.add_time = DateTime.Now;
            addressModel.postcode = PostCode;

            int j = 0;
            if (type != "0")
            {
                BLL.user_address bll = new BLL.user_address();
                Model.user_address model1 = bll.GetModel(int.Parse(type));

                model1.id = int.Parse(type);
                model1.acceptName = AcceptName;
                model1.provinces = Province;
                model1.citys = City;
                model1.area = Area;
                model1.street = Street;
                model1.mobile = Mobile;
                model1.modity_time = DateTime.Now;
                model1.postcode = PostCode;
                model1.address = Street;
                //执行更新操作
                if (addressBll.Update(model1))
                {
                    j = 1;

                    //设置默认地址
                    new BLL.user_address().UpdateField("is_default=0", "user_id=" + model.id);
                    new BLL.user_address().UpdateField("is_default=1", "id=" + Vincent._DTcms.Utils.StrToInt(type, 0) + " and user_id=" + model.id);

                    context.Response.Write("{\"info\":\"修改收货地址成功！\", \"status\":1}");
                    return;
                }
            }
            else
            {
                addressModel.is_default = 1;//设置默认地址
                //执行新增操作
                if (addressBll.Add(addressModel) > 0)
                {
                    j = 2;
                    context.Response.Write("{\"info\":\"新增收货地址成功！\", \"status\":1}");
                    return;
                }
            }
            if (j == 1)
            {
                context.Response.Write("{\"info\":\"修改收货地址成功！\", \"status\":1}");
                return;
            }
            if (j == 2)
            {
                context.Response.Write("{\"info\":\"新增收货地址成功！\", \"status\":1}");
                return;
            }
            
            context.Response.Write("{\"info\":\"新增收货地址失败！\", \"status\":0}");
            context.Response.End();
        }
        #endregion

        /// <summary>
        /// 快速注册
        /// </summary>
        /// <param name="context"></param>
        private void user_quickregister(HttpContext context)
        {
            string AcceptName = Vincent._DTcms.DTRequest.GetFormString("txtAcceptName");
            string Province = Vincent._DTcms.DTRequest.GetFormString("txtProvince");
            string City = Vincent._DTcms.DTRequest.GetFormString("txtCity");
            string Area = Vincent._DTcms.DTRequest.GetFormString("txtArea");
            string Street = Vincent._DTcms.DTRequest.GetFormString("txtStreet");
            int PostCode = Vincent._DTcms.DTRequest.GetFormInt("txtPostCode");
            string Mobile = Vincent._DTcms.DTRequest.GetFormString("txtMobile");
            string type = Vincent._DTcms.DTRequest.GetFormString("type");

            if (!_Regex.Regex_IsContains(Mobile, @"^(((13[0-9]{1})|(14[0-9]{1})|(15[0-9]{1})|(16[0-9]{1})|(17[0-9]{1})|(18[0-9]{1}))+\d{8})$"))
            {
                context.Response.Write("{\"status\":0, \"msg\":\"手机号格式不正确\"}");
                return;
            }

            //检查用户是否登录
            Model.users model = new BasePage().GetUserInfo();
            if (model == null)
            {
                //用户注册
                BLL.users bll = new BLL.users();
                Model.users user_model = new Model.users();

                if(bll.ExistsMobile(Mobile))
                {
                    context.Response.Write("{\"status\":0, \"msg\":\"该手机号已经注册\"}");
                    return;
                }

                //生成随机
                string smscoderand = new Random().Next(100000, 999999).ToString();

                //保存注册信息
                user_model.group_id = 1;//普通用户注册
                user_model.user_name = Mobile;
                user_model.salt = Vincent._DTcms.Utils.GetCheckCode(6); //获得6位的salt加密字符串
                user_model.password = _DESEncrypt.Encrypt(smscoderand, user_model.salt);
                user_model.mobile = Mobile;
                user_model.reg_ip = Vincent._DTcms.DTRequest.GetIP();
                user_model.reg_time = DateTime.Now;
                user_model.strcode = Vincent._DTcms.Utils.GetCheckCode(20);//生成随机码
                int newId = bll.Add(user_model);
                if (newId > 0)
                {
                    model = bll.GetModel(newId);
                    //写入coocik
                    context.Session[Vincent._DTcms.DTKeys.SESSION_USER_INFO] = model;
                    context.Session.Timeout = 45;

                    //写短信数据，发SMS
                    
                    var message_name = _Utility.GetConfigAppSetting("message_name");
                    var message_pwd = _Utility.GetConfigAppSetting("message_pwd");
                    var message_content = _Utility.GetConfigAppSetting("message_regist");
                    message_content = message_content.Replace("username", user_model.user_name);
                    message_content = message_content.Replace("userpwd", smscoderand);

                    var MessageNum = Vincent._MobileMessage.SendMessageCode(message_content, Mobile);
                  
                    if (MessageNum > 0)
                    {
                        Model.user_address addressModel = new Model.user_address();
                        BLL.user_address addressBll = new BLL.user_address();

                        addressModel.user_id = model.id;
                        addressModel.acceptName = AcceptName;
                        addressModel.provinces = Province;
                        addressModel.citys = City;
                        addressModel.area = Area;
                        addressModel.street = Street;
                        addressModel.mobile = Mobile;
                        addressModel.add_time = DateTime.Now;
                        addressModel.postcode = PostCode;
                        //执行新增操作
                        if (addressBll.Add(addressModel) > 0)
                        {
                            context.Response.Write("{\"status\":1, \"msg\":\"恭喜你，你已注册为农夫优品的会员，你的密码为" + smscoderand + ",请注意查收你的短信\"}");
                            return;
                        }

                        context.Response.Write("{\"msg\":\"注册失败！\", \"status\":0}");
                        context.Response.End();
                        //context.Response.Write("{\"status\":1, \"msg\":\"恭喜你，你已注册为农夫优品的会员，你的密码为" + smscoderand + ",请注意查收你的短信\"}");
                        //return;
                    }

                }
                else
                {
                    context.Response.Write("{\"status\":0, \"msg\":\"注册失败！\"}");
                    return;
                }

            }
            
        }


        #region 编辑用户收货地址OK=================================
        private void user_address_edit(HttpContext context)
        {
            int AddressId = Vincent._DTcms.DTRequest.GetFormInt("hideId");
            string AcceptName = Vincent._DTcms.DTRequest.GetFormString("txtName");
            string Province = Vincent._DTcms.DTRequest.GetFormString("txtProvince");
            string City = Vincent._DTcms.DTRequest.GetFormString("txtCity");
            string Area = Vincent._DTcms.DTRequest.GetFormString("txtArea");
            string Street = Vincent._DTcms.DTRequest.GetFormString("txtStreet");
            int PostCode = Vincent._DTcms.DTRequest.GetFormInt("txtPostCode");
            string Mobile = Vincent._DTcms.DTRequest.GetFormString("txtMobile");

            if (AddressId == 0)
            {
                //检查用户是否登录
                Model.users model1 = new BasePage().GetUserInfo();
                //新增
                Model.user_address addressModel = new Model.user_address();
                BLL.user_address addressBll = new BLL.user_address();

                addressModel.user_id = model1.id;
                addressModel.acceptName = AcceptName;
                addressModel.provinces = Province;
                addressModel.citys = City;
                addressModel.area = Area;
                addressModel.street = Street;
                addressModel.mobile = Mobile;
                addressModel.add_time = DateTime.Now;
                addressModel.postcode = PostCode;
                //执行新增操作
                if (addressBll.Add(addressModel) > 0)
                {
                    context.Response.Write("{\"info\":\"新增收货地址成功！\", \"status\":\"y\"}");
                    return;
                }
            
                //context.Response.Write("{\"info\":\"修改收货地址失败！\", \"status\":\"n\"}");
                //return;
            }


            BLL.user_address bll = new BLL.user_address();
            Model.user_address model = bll.GetModel(AddressId);

            model.id = AddressId;
            model.acceptName = AcceptName;
            model.provinces = Province;
            model.citys = City;
            model.area = Area;
            model.street = Street;
            model.mobile = Mobile;
            model.modity_time = DateTime.Now;
            model.postcode = PostCode;


            

            //执行更新操作
            if (bll.Update(model))
            {
                context.Response.Write("{\"info\":\"修改收货地址成功！\", \"status\":\"y\"}");
                return;
            }
            
            context.Response.Write("{\"info\":\"修改收货地址失败！\", \"status\":\"n\"}");
            context.Response.End();
        }
        #endregion

        #region 购物车加入商品OK===============================
        private void cart_goods_add(HttpContext context)
        {
            string goods_id = Vincent._DTcms.DTRequest.GetFormString("goods_id");
            string goods_num = Vincent._DTcms.DTRequest.GetFormString("goods_num");
            string goods_type = Vincent._DTcms.DTRequest.GetFormString("goods_type");
            string goods_weight = Vincent._DTcms.DTRequest.GetFormString("goods_weight");
            string goods_price = Vincent._DTcms.DTRequest.GetFormString("goods_price");
            string str = goods_num + "|" + goods_type + "|" + goods_weight + "|" + goods_price;
            if (goods_id == "")
            {
                context.Response.Write("{\"status\":0, \"msg\":\"您提交的商品参数有误！\"}");
                return;
            }
            //查找会员组
            int group_id = 0;
            Model.users groupModel = new Web.UI.BasePage().GetUserInfo();
            Model.user_groups model = new Model.user_groups();
            BLL.user_groups bll = new BLL.user_groups();
            if (groupModel != null)
            {
                group_id = groupModel.group_id;
                model = bll.GetModel(group_id, int.Parse(goods_id));
            }
            //统计购物车
            Web.UI.ShopCart.AddCart(goods_id, str);
            Model.cart_total cartModel = Web.UI.ShopCart.GetTotal(group_id);

            if (groupModel == null)
            {
                context.Response.Write("{\"status\":1, \"msg\":\"商品已成功添加到购物车！\", \"quantity\":" + cartModel.total_quantity +
                                   ", \"amount\":\"" + cartModel.real_amount + "\",\"usergroup\":\" 普通游客，登陆后查看会员价格\",\"userprice\":\"0.00 \"}");
            }
            else
            {
                context.Response.Write("{\"status\":1, \"msg\":\"商品已成功添加到购物车！\", \"quantity\":" + cartModel.total_quantity +
                                   ", \"amount\":\"" + cartModel.real_amount + "\",\"usergroup\":\"" + model.title + "\",\"userprice\":\"" + model.price + "\"}");
            }

        }
        #endregion

        /// <summary>
        /// 获取选中地址
        /// </summary>
        /// <param name="context"></param>
        private void select_address_add(HttpContext context)
        {
            string addressid = Vincent._DTcms.DTRequest.GetFormString("addressid");
            if (addressid == "")
            {
                context.Response.Write("{\"status\":0, \"msg\":\"您提交的参数有误！\"}");
                return;
            }
            int adid = int.Parse(addressid);
            //查找会员组
            int group_id = 0;
            Model.users groupModel = new Web.UI.BasePage().GetUserInfo();
            Model.user_address model = new Model.user_address();
            BLL.user_address bll = new BLL.user_address();
            if (groupModel != null)
            {
                model = bll.GetModel(adid);
            }
            string outmsg = ObjectToJSON(model);
            context.Response.Write(outmsg);

        }

        #region Model与JSON相互转化========================================
        // using System.Runtime.Serialization.Json;
        public static T parse<T>(string jsonString)
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                return (T)new DataContractJsonSerializer(typeof(T)).ReadObject(ms);
            }
        }

        public static string ObjectToJSON(object jsonObject)
        {
            using (var ms = new MemoryStream())
            {
                if (jsonObject == null)
                {
                    return null;
                }
                new DataContractJsonSerializer(jsonObject.GetType()).WriteObject(ms, jsonObject);
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }
        #endregion

        #region 修改购物车商品OK===============================
        private void cart_goods_update(HttpContext context)
        {
            string goods_id = Vincent._DTcms.DTRequest.GetFormString("goods_id");
            int goods_quantity = Vincent._DTcms.DTRequest.GetFormInt("goods_quantity");
            if (goods_id == "")
            {
                context.Response.Write("{\"status\":0, \"msg\":\"您提交的商品参数有误！\"}");
                return;
            }

            if (Web.UI.ShopCart.UpdateCart(goods_id, goods_quantity.ToString()))
            {
                context.Response.Write("{\"status\":1, \"msg\":\"商品数量修改成功！\"}");
            }
            else
            {
                context.Response.Write("{\"status\":0, \"msg\":\"商品数量更改失败，请检查操作是否有误！\"}");
            }
            return;
        }
        #endregion

        #region 修改购物车多个商品OK===============================
        private void cart_goods_updates(HttpContext context)
        {
            string goods_id = Vincent._DTcms.DTRequest.GetFormString("goodId");
            string goods_quantity = Vincent._DTcms.DTRequest.GetFormString("goods_quantity");
            string[] arrayId = goods_id.Split('|');
            string[] arrayQuantity = goods_quantity.Split(',');
            // arrayId = goods_id.Split("|");
            // arrayQuantity = goods_quantity.Split(",");
            Model.users model = new BasePage().GetUserInfo();
            if (model == null)
            {
                context.Response.Write("用户尚未登录，请先登录！");
                return;
            }

            if (arrayId.Length <= 0)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"您提交的商品参数有误！\"}");
                return;
            }
            for (int i = 0; i < arrayId.Length; i++)
            {
                if (Web.UI.ShopCart.UpdateCart(arrayId[i].ToString(), arrayQuantity[i].ToString()))
                {

                    //context.Response.Write("{\"status\":1, \"msg\":\"商品数量修改成功！\"}");
                }
                else
                {
                    context.Response.Write("{\"status\":0, \"msg\":\"商品数量更改失败，请检查操作是否有误！\"}");
                }
            }
            context.Response.Write("1");
            return;
        }
        #endregion

        #region 删除购物车商品OK===============================
        private void cart_goods_delete(HttpContext context)
        {
            string goods_id = Vincent._DTcms.DTRequest.GetFormString("goods_id");
            //string goods_id = Vincent._DTcms.DTRequest.GetFormStringValue("goods_id");
            if (goods_id == "")
            {
                context.Response.Write("{\"status\":0, \"msg\":\"您提交的商品参数有误！\"}");
                return;
            }
            var goodsid = goods_id.Split(',');
            if (goodsid.Length > 1)
            {
                foreach (var item in goodsid)
                {
                    Web.UI.ShopCart.ClearCart(item);
                }
            }
            else
            {
                Web.UI.ShopCart.ClearCart(goodsid[0]);
            }
            

            
            context.Response.Write("{\"status\":1, \"msg\":\"商品移除成功！\"}");
            return;
        }
        #endregion
        
        #region 加入收藏夹OK===================================
        private void favorite_cart_add(HttpContext context)
        {
            string goods_id = Vincent._DTcms.DTRequest.GetFormString("goods_id");
            string goods_info = Vincent._DTcms.DTRequest.GetFormString("dataInfo");

            Model.users model = new BasePage().GetUserInfo();
            if (model == null)
            {
                context.Response.Write("{\"status\":2, \"msg\":\"用户还没有登录,请先登录！\"}");
                return;
            }

            if (goods_id == "" || goods_info == "")
            {
                context.Response.Write("{\"status\":0, \"msg\":\"您提交的商品参数有误！\"}");
                return;
            }
            BLL.collect bll=new BLL.collect();
            var goods=goods_info.Split('|');
            if (bll.Exists(int.Parse(goods_id))) {
                context.Response.Write("{\"status\":0, \"msg\":\"该商品已经收藏！\"}");
                return;
            }
            Model.collect model1 = new Model.collect();
            model1.user_id = model.id;
            model1.good_id = int.Parse(goods_id);
            model1.title = goods[1];
            model1.good_type = goods[0];
            model1.good_price = decimal.Parse(goods[2]);
            model1.img_url = goods[3];
            model1.add_time = DateTime.Now;
            if (bll.Add(model1) < 0) {
                context.Response.Write("{\"status\":0, \"msg\":\"商品收藏失败！\"}");
                return;
            }
            context.Response.Write("{\"status\":1, \"msg\":\"收藏成功！\"}");
            return;
            //统计收藏夹
            //bool bl = Web.UI.ShopCart.AddfavoriteCart(goods_id, goods_info);

            //if (bl)
            //{
            //    context.Response.Write("{\"status\":1, \"msg\":\"商品已成功添加到收藏夹！\"}");
            //}
            //else
            //{
            //    context.Response.Write("{\"status\":0, \"msg\":\"商品添加到收藏夹失败！\"}");
            //}

        }
        #endregion

        #region 删除收藏夹商品OK===============================
        private void favorite_goods_delete(HttpContext context)
        {
            string goods_id = Vincent._DTcms.DTRequest.GetFormString("goods_id");
            //string goods_id = Vincent._DTcms.DTRequest.GetFormStringValue("goods_id");
            if (goods_id == "")
            {
                context.Response.Write("{\"status\":0, \"msg\":\"您提交的商品参数有误！\"}");
                return;
            }
            Web.UI.ShopCart.ClearCollect(goods_id);
            context.Response.Write("{\"status\":1, \"msg\":\"商品移除成功！\"}");
            return;
        }
        #endregion

        #region 保存用户订单OK=================================
        private void order_save(HttpContext context)
        {
            //获得传参信息
            int payment_id = Vincent._DTcms.DTRequest.GetFormInt("payment_id");
            int express_id = Vincent._DTcms.DTRequest.GetFormInt("express_id");
            string accept_name = Vincent._DTcms.Utils.ToHtml(Vincent._DTcms.DTRequest.GetFormString("accept_name"));
            string post_code = Vincent._DTcms.Utils.ToHtml(Vincent._DTcms.DTRequest.GetFormString("post_code"));
            string telphone = Vincent._DTcms.Utils.ToHtml(Vincent._DTcms.DTRequest.GetFormString("telphone"));
            string mobile = Vincent._DTcms.Utils.ToHtml(Vincent._DTcms.DTRequest.GetFormString("mobile"));
            string address = Vincent._DTcms.Utils.ToHtml(Vincent._DTcms.DTRequest.GetFormString("address"));
            string message = Vincent._DTcms.Utils.ToHtml(Vincent._DTcms.DTRequest.GetFormString("message"));
            //获取订单配置信息
            Model.orderconfig orderConfig = new BLL.orderconfig().loadConfig();

            //检查物流方式
            if (express_id == 0)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，请选择配送方式！\"}");
                return;
            }
            Model.express expModel = new BLL.express().GetModel(express_id);
            if (expModel == null)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，配送方式不存在或已删除！\"}");
                return;
            }
            //检查支付方式
            if (payment_id == 0)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，请选择支付方式！\"}");
                return;
            }
            Model.payment payModel = new BLL.payment().GetModel(payment_id);
            if (payModel == null)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，支付方式不存在或已删除！\"}");
                return;
            }
            //检查收货人
            if (string.IsNullOrEmpty(accept_name))
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，请输入收货人姓名！\"}");
                return;
            }
            //检查手机和电话
            if (string.IsNullOrEmpty(telphone) && string.IsNullOrEmpty(mobile))
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，请输入收货人联系电话或手机！\"}");
                return;
            }
            //检查地址
            if (string.IsNullOrEmpty(address))
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，请输入详细的收货地址！\"}");
                return;
            }
            //如果开启匿名购物则不检查会员是否登录
            int user_id = 0;
            int user_group_id = 0;
            string user_name = string.Empty;
            //检查用户是否登录
            Model.users userModel = new BasePage().GetUserInfo();
            if (userModel != null)
            {
                user_id = userModel.id;
                user_group_id = userModel.group_id;
                user_name = userModel.user_name;
            }
            if (orderConfig.anonymous == 0 && userModel == null)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，用户尚未登录或已超时！\"}");
                return;
            }
            //检查购物车商品
            IList<Model.cart_items> iList = BuysingooShop.Web.UI.ShopCart.GetList(user_group_id);
            if (iList == null)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，购物车为空，无法结算！\"}");
                return;
            }
            //统计购物车
            Model.cart_total cartModel = BuysingooShop.Web.UI.ShopCart.GetTotal(user_group_id);
            //保存订单=======================================================================
            Model.orders model = new Model.orders();
            model.order_no = "B" + Vincent._DTcms.Utils.GetOrderNumber(); //订单号B开头为商品订单
            model.user_id = user_id;
            model.user_name = user_name;
            model.payment_id = payment_id;
            model.express_id = express_id;
            model.accept_name = accept_name;
            model.post_code = post_code;
            model.telphone = telphone;
            model.mobile = mobile;
            model.address = address;
            model.message = message;
            model.payable_amount = cartModel.payable_amount;
            model.real_amount = cartModel.real_amount;
            model.express_status = 1;
            model.express_fee = expModel.express_fee; //物流费用
            //如果是先款后货的话
            if (payModel.type == 1)
            {
                model.payment_status = 1; //标记未付款
                if (payModel.poundage_type == 1) //百分比
                {
                    model.payment_fee = model.real_amount * payModel.poundage_amount / 100;
                }
                else //固定金额
                {
                    model.payment_fee = payModel.poundage_amount;
                }
            }
            //订单总金额=实付商品金额+运费+支付手续费
            model.order_amount = model.real_amount + model.express_fee + model.payment_fee;
            //购物积分,可为负数
            model.point = cartModel.total_point;
            model.add_time = DateTime.Now;
            //商品详细列表
            List<Model.order_goods> gls = new List<Model.order_goods>();
            foreach (Model.cart_items item in iList)
            {
                gls.Add(new Model.order_goods
                {
                    goods_id = item.id,
                    goods_title = item.title,
                    goods_price = item.price,
                    real_price = item.user_price,
                    quantity = Convert.ToInt32(item.quantity.Split('|')[0]),
                    point = item.point,
                    goods_pic = item.img_url
                });
            }
            model.order_goods = gls;
            int result = new BLL.orders().Add(model);
            if (result < 1)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"订单保存过程中发生错误，请重新提交！\"}");
                return;
            }
            //扣除积分
            if (model.point < 0)
            {
                new BLL.user_point_log().Add(model.user_id, model.user_name, model.point, "积分换购，订单号：" + model.order_no, false);
            }
            //清空购物车
            BuysingooShop.Web.UI.ShopCart.ClearCart("0");
            //提交成功，返回URL
            //context.Response.Write("{\"status\":1, \"url\":\"" + new Web.UI.BasePage().linkurl("payment", "confirm", model.order_no) + "\", \"msg\":\"恭喜您，订单已成功提交！\"}");
            context.Response.Write("{\"status\":1, \"url\":\"/user/payment.aspx?action=confirm&order_no=" + model.order_no + "\", \"msg\":\"恭喜您，订单已成功提交！\"}");

            return;
        }
        #endregion

        #region 查看用户订单OK=================================
        private void find_order(HttpContext context)
        {
            //检查用户是否登录
            Model.users userModel = new BasePage().GetUserInfo();
            if (userModel == null)
            {
                context.Response.Write("n");
                return;
            }

            //获得传参信息
            string order_no = Vincent._DTcms.DTRequest.GetQueryString("order_no");
           //Model.orders orderModel = new BLL.orders().GetModel(order_no);


            DataTable dt = new BLL.orders().GetOrderList(0, "t1.order_no='" + order_no + "' and t1.user_id=" + userModel.id, "id desc").Tables[0];


            string msg = CreateJsonParameters(dt);
           context.Response.Write(msg);
        }
        #endregion

        /// <summary>
        /// 是否使用优惠券
        /// </summary>
        /// <param name="context"></param>
        private void is_coupon(HttpContext context)
        {
            string order_no = Vincent._DTcms.DTRequest.GetQueryString("order_no");
            string outmsg = "{\"status\":0, \"msg\":\"没有使用优惠券！\"}";

            DataTable coupon = new BLL.user_coupon_log().GetList(0," order_no="+order_no," id").Tables[0];
            if (coupon.Rows.Count>0)
            {
                outmsg=CreateJsonParameters(coupon);
            }
            context.Response.Write(outmsg);
            
        }

        /// <summary>
        /// 是否退货
        /// </summary>
        /// <param name="context"></param>
        private void is_refund(HttpContext context)
        {
            string order_no = Vincent._DTcms.DTRequest.GetQueryString("order_no");
            string outmsg = "{\"status\":0, \"msg\":\"没有退货记录！\"}";
            Model.refund refund = new BLL.refund().GetorderModel(order_no);
            if (refund != null)
            {
                outmsg = ObjectToJSON(refund);
            }
            context.Response.Write(outmsg);
        }


        #region 添加、修改、删除收货地址OK=====================
        private void add_address(HttpContext context)
        {
            string act = Vincent._DTcms.DTRequest.GetString("act");
            string postData = Vincent._DTcms.DTRequest.GetString("postData");
            Model.user_address model = new Model.user_address();
            BLL.user_address bll = new BLL.user_address();

            JavaScriptSerializer js = new JavaScriptSerializer();
            Model.users userinfo = new BasePage().GetUserInfo();

            if (act == "add")
            {
                model = js.Deserialize<Model.user_address>(postData);
                model.user_id = userinfo != null ? userinfo.id : 0;
                //model.address = model.provinces + model.citys + model.area + model.street;
                model.add_time = DateTime.Now.ToLocalTime();
                if (bll.Add(model) > 0)
                {
                    context.Response.Write("{\"status\":1, \"msg\":\"新增地址成功！\"}");
                }
                else
                {
                    context.Response.Write("{\"status\":0, \"msg\":\"新增地址失败！\"}");
                }
            }
            if (act == "update")
            {
                string updateid = Vincent._DTcms.DTRequest.GetString("updateid");
                model = bll.GetModel(int.Parse(updateid));
                Model.user_address model2 = js.Deserialize<Model.user_address>(postData);
                model.acceptName = string.IsNullOrEmpty(model2.acceptName) ? model.acceptName : model2.acceptName;
                model.provinces = string.IsNullOrEmpty(model2.provinces) ? model.provinces : model2.provinces;
                model.citys = string.IsNullOrEmpty(model2.citys) ? model.citys : model2.citys;
                model.area = string.IsNullOrEmpty(model2.area) ? model.area : model2.area;
                model.street = string.IsNullOrEmpty(model2.street) ? model.street : model2.street;
                model.address = model.provinces + model.citys + model.area + model.street;
                model.postcode = model2.postcode == 0 ? model.postcode : model2.postcode;
                model.mobile = string.IsNullOrEmpty(model2.mobile) ? model.mobile : model2.mobile;
                model.is_default = model2.is_default;
                model.modity_time = DateTime.Now;
                if (bll.Update(model))
                {
                    context.Response.Write("{\"status\":1, \"msg\":\"修改收货地址成功！\"}");
                }
                else
                {
                    context.Response.Write("{\"status\":0, \"msg\":\"修改收货地址失败！\"}");
                }
            }
            if (act == "delete")
            {
                string deleteid = Vincent._DTcms.DTRequest.GetString("deleteid");
                if (bll.Delete(int.Parse(deleteid)))
                {
                    context.Response.Write("{\"status\":1, \"msg\":\"删除地址成功！\"}");
                }
                else
                {
                    context.Response.Write("{\"status\":0, \"msg\":\"删除地址失败！\"}");
                }
            }

        }
        #endregion

        #region 用户取消订单OK=================================
        private void order_cancel(HttpContext context)
        {
            //检查用户是否登录
            Model.users userModel = new BasePage().GetUserInfo();
            if (userModel == null)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，用户尚未登录或已超时！\"}");
                return;
            }
            //检查订单是否存在
            string order_no = Vincent._DTcms.DTRequest.GetFormString("order_no");
            Model.orders orderModel = new BLL.orders().GetModel(order_no);
            if (order_no == "" || orderModel == null)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，订单号不存在或已删除！\"}");
                return;
            }
            //检查是否自己的订单
            if (userModel.id != orderModel.user_id)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，不能取消别人的订单状态！\"}");
                return;
            }
            //检查订单状态
            if (orderModel.status > 1)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，订单不是生成状态，不能取消！\"}");
                return;
            }
            bool result = new BLL.orders().UpdateField(order_no, "status=99");
            if (!result)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，操作过程中发生不可遇知的错误！\"}");
                return;
            }
            //如果是积分换购则返还积分
            if (orderModel.point < 0)
            {
                new BLL.user_point_log().Add(orderModel.user_id, orderModel.user_name, -1 * orderModel.point, "取消订单，返还换购积分，订单号：" + orderModel.order_no, false);
            }
            context.Response.Write("{\"status\":1, \"msg\":\"取消订单成功！\"}");
            return;
        }
        #endregion

        #region 用户确认订单OK=================================
        private void confirm_order(HttpContext context)
        {
            //检查用户是否登录
            Model.users userModel = new BasePage().GetUserInfo();
            if (userModel == null)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，用户尚未登录或已超时！\"}");
                return;
            }
            //检查订单是否存在
            string order_no = Vincent._DTcms.DTRequest.GetFormString("order_no");
            Model.orders orderModel = new BLL.orders().GetModel(order_no);
            if (order_no == "" || orderModel == null)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，订单号不存在或已删除！\"}");
                return;
            }
            //检查是否自己的订单
            if (userModel.id != orderModel.user_id)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，不能确认别人的订单状态！\"}");
                return;
            }
            ////检查订单状态
            //if (orderModel.status > 1)
            //{
            //    context.Response.Write("{\"status\":0, \"msg\":\"对不起，订单不是生成状态，不能取消！\"}");
            //    return;
            //}
            BLL.orders bll = new BLL.orders();
            orderModel.status = 90;
            orderModel.complete_time = DateTime.Now;
            if (!bll.Update(orderModel))
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"确认订单完成失败！\"}");
                return;
            }
            ////如果是积分换购则返还积分
            //if (orderModel.point < 0)
            //{
            //    new BLL.user_point_log().Add(orderModel.user_id, orderModel.user_name, -1 * orderModel.point, "取消订单，返还换购积分，订单号：" + orderModel.order_no, false);
            //}
            context.Response.Write("{\"status\":1, \"msg\":\"订单确认成功！\"}");
            return;
        }
        #endregion

        #region 用户取消退款OK=================================
        private void refund_cancel(HttpContext context)
        {
            //检查用户是否登录
            Model.users userModel = new BasePage().GetUserInfo();
            if (userModel == null)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，用户尚未登录或已超时！\"}");
                return;
            }
            //检查退款单是否存在
            int refund_id = Vincent._DTcms.DTRequest.GetFormInt("refund_id");
            Model.refund orderModel = new BLL.refund().GetModel(refund_id);
            if (refund_id == 0 || orderModel == null)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，退款单不存在或已删除！\"}");
                return;
            }
            //检查是否自己的订单
            if (userModel.id != orderModel.user_id)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，不能取消别人的退款单状态！\"}");
                return;
            }
            //检查订单状态
            if (orderModel.refund_status > 1)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，退款单不是生成状态，不能取消！\"}");
                return;
            }
            bool result = new BLL.refund().UpdateField(refund_id, "refund_status=4");
            if (!result)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，操作过程中发生错误！\"}");
                return;
            }
            context.Response.Write("{\"status\":1, \"msg\":\"取消退款单成功！\"}");
            return;
        }
        #endregion

        /// <summary>
        /// 设置订单金额session
        /// </summary>
        /// <param name="context"></param>
        private void setpricesession(HttpContext context)
        {
            var price = float.Parse(Vincent._DTcms.DTRequest.GetFormString("price"));
            context.Session["price"] = price;
            context.Response.Write("{\"status\":1, \"msg\":\"保存成功！\"}");
            return;
        }

        /// <summary>
        /// 获取订单信息(保存订单金额)
        /// </summary>
        /// <param name="context"></param>
        private void getorderamount(HttpContext context)
        {
            string msg = "{\"status\":0, \"msg\":\"保存失败！\"}";

            var order_id = int.Parse(Vincent._DTcms.DTRequest.GetFormString("order_id"));

            Model.orders mo = new BLL.orders().GetModel(order_id);

            if (mo != null)
            {
                context.Session["price_" + order_id] = mo.order_amount;//应付金额保存到session
                msg = "{\"status\":1, \"msg\":\"保存成功！\"}";
            }

            context.Response.Write(msg);

        }

        #region 统计及输出阅读次数OK===========================
        private void view_article_click(HttpContext context)
        {
            int article_id = Vincent._DTcms.DTRequest.GetInt("id", 0);
            int click = Vincent._DTcms.DTRequest.GetInt("click", 0);
            int count = 0;
            if (article_id > 0)
            {
                BLL.article bll = new BLL.article();
                count = bll.GetClick(article_id);
                if (click > 0)
                {
                    bll.UpdateField(article_id, "click=click+1");
                }
            }
            context.Response.Write("document.write('" + count + "');");
        }
        #endregion

        #region 输出评论总数OK=================================
        private void view_comment_count(HttpContext context)
        {
            int article_id = Vincent._DTcms.DTRequest.GetInt("id", 0);
            int count = 0;
            if (article_id > 0)
            {
                count = new BLL.article_comment().GetCount("is_lock=0 and article_id=" + article_id);
            }
            context.Response.Write("document.write('" + count + "');");
        }
        #endregion

        #region 输出附件下载总数OK=============================
        private void view_attach_count(HttpContext context)
        {
            int attach_id = Vincent._DTcms.DTRequest.GetInt("id", 0);
            int count = 0;
            if (attach_id > 0)
            {
                count = new BLL.article_attach().GetDownNum(attach_id);
            }
            context.Response.Write("document.write('" + count + "');");
        }
        #endregion

        #region 输出购物车总数OK===============================
        private void view_cart_count(HttpContext context)
        {
            int group_id = 0;
            //检查用户是否登录
            Model.users model = new BasePage().GetUserInfo();
            if (model != null)
            {
                group_id = model.group_id;
            }
            Model.cart_total cartModel = Web.UI.ShopCart.GetTotal(group_id);
            context.Response.Write("document.write('" + cartModel.total_quantity + "');");
        }
        #endregion

        #region 搜索框查询OK===============================
        private void user_search(HttpContext context)
        {
            string searchstr = Vincent._DTcms.DTRequest.GetQueryString("q");
            BuysingooShop.BLL.article article = new BLL.article();
            string search = article.GetGoodsName(searchstr);
            string[] searchs = search.Split('丨');
            foreach (var item in searchs)
            {
                context.Response.Write(string.Format("{0}\n", item));
            }
        }
        #endregion

        #region 搜索查询商品OK===============================
        private void goods_getId(HttpContext context)
        {
            string searchstr = Vincent._DTcms.DTRequest.GetQueryString("searchstr");
            BuysingooShop.BLL.article article = new BLL.article();
            string search = article.GetGoodsId(searchstr);
            if (search == "0")
            {
                context.Response.Write("0");
                return;
            }
            context.Response.Write(search);
            return;
        }

        #endregion

        #region 检查用户是否登录(进入购物车)OK===============================
        private void user_isuserlogin(HttpContext context)
        {
            Model.users model = new BasePage().GetUserInfo();
            if (model == null)
            {
                context.Response.Write("用户尚未登录，请先登录！");
                return;
            }

            context.Response.Write("1");
            return;
        }

        #endregion

        #region 通用外理方法OK=================================
        //校检网站图片验证码
        private string verify_code(HttpContext context, string strcode)
        {
            if (string.IsNullOrEmpty(strcode))
            {
                return "{\"status\":0, \"msg\":\"对不起，请输入验证码！\"}";
            }
            if (context.Session[Vincent._DTcms.DTKeys.SESSION_CODE] == null)
            {
                return "{\"status\":0, \"msg\":\"对不起，验证码超时或已过期！\"}";
            }
            if (strcode.ToLower() != (context.Session[Vincent._DTcms.DTKeys.SESSION_CODE].ToString()).ToLower())
            {
                return "{\"status\":0, \"msg\":\"您输入的验证码与系统的不一致！\"}";
            }
            context.Session[Vincent._DTcms.DTKeys.SESSION_CODE] = null;
            return "success";
        }
        //校检网站手机验证码
        private string verify_sms_code(string strcode)
        {
            if (string.IsNullOrEmpty(strcode))
            {
                return "{\"status\":0, \"msg\":\"对不起，请输入验证码！\"}";
            }
            if (_Cookie.GetCookie(Vincent._DTcms.DTKeys.SESSION_SMS_CODE) == null)
            {
                return "{\"status\":0, \"msg\":\"对不起，短信验证码超时或已过期！\"}";
            }
            if (strcode.ToLower() != (_Cookie.GetCookie(Vincent._DTcms.DTKeys.SESSION_SMS_CODE).ToString()).ToLower())
            {
                return "{\"status\":0, \"msg\":\"您输入的验证码与系统的不一致！\"}";
            }
            _Cookie.DelCookie(Vincent._DTcms.DTKeys.SESSION_SMS_CODE);
            return "success";
        }
        #endregion END通用方法=================================================

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}