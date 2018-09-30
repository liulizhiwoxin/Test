using BuysingooShop.Web.AdminService;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using Vincent;
using Vincent._DTcms;

namespace BuysingooShop.Web.Service
{
    /// <summary>
    /// AdminServiceHandler 的摘要说明
    /// </summary>
    public class AdminServiceHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        public AdminServiceSoapClient SoapClient = new AdminServiceSoapClient();

        public void ProcessRequest(HttpContext context)
        {
            string param = _Request.GetString("param", "");

            switch (param)
            {
                case "productlist":
                    productlist(context);        //获取产品系列列表
                    break;
                case "productlistclassify":
                    productlistclassify(context);        //获取产品分类系列列表
                    break;
                case "productlistclassifyarticle":
                    productlistclassifyarticle(context);        //获取产品
                    break;
                case "producttyreserieslist":
                    producttyreserieslist(context);        //获取轮胎系列列表
                    break;
                case "producttyreseries":
                    producttyreseries(context);        //获取轮胎系列产品
                    break;
                case "productengineoil":
                    productengineoil(context);        //获取机油系列列表
                    break;
                case "productSealant":
                    productSealant(context);        //获取镀晶系列列表
                    break;
                case "producthealth":
                    producthealth(context);        //获取养生系列列表
                    break;
                case "productHonor":
                    productHonor(context);        //获取荣誉资质系列列表
                    break;
                case "membership":
                    membership(context);        //获取钻石会员套餐
                    break;
                case "contact":
                    contact(context);        //获取钻石会员套餐
                    break;
                case "contactabout":
                    contactabout(context);        //获取商品详细描述
                    break;
                case "username":
                    username(context);        //获取username
                    break;
                case "Productcategory":
                    Productcategory(context);        //获取产品相关类别
                    break;
                case "Productsimg":
                    Productsimg(context);         //获取产品相册
                    break;
                case "user_login_out":
                    user_login_out(context);         //退出
                    break;
                case "productSealant2":
                    productSealant2(context);         //镀晶系列2
                    break;
                case "membership_hot":
                    membership_hot(context);         //获取热卖商品
                    break;
                case "loadnews":
                    loadnews(context);         //获取热卖商品
                    break;
                case "loadnotice":
                    loadnotice(context);         //获取公告
                    break;
                case "GetConfigUrl":
                    GetConfigUrl(context);         //获取图片主机地址
                    break;
                case "GetAddress":
                    GetAddress(context);            //获取收货地址
                    break;
                case "DeleteAddress":
                    DeleteAddress(context);         //删除地址
                    break;
                case "GetUpdateAddress":
                    GetUpdateAddress(context);      //获取修改地址
                    break;
                case "addAddress":
                    addAddress(context);            //添加收货地址
                    break;
                case "UpdateAddress":
                    UpdateAddress(context);            //修改收货地址
                    break;
                case "DefaultAddress":
                    DefaultAddress(context);        //默认地址
                    break;
                case "submitorders":
                    submitorders(context);          //提交提单(应付金额为零)
                    break;
                case "submitorder":
                    submitorder(context);           //提交提单
                    break;
                case "submit_withdraw":
                    submit_withdraw(context);       //提现
                    break;
                case "getOpenarea":
                    getOpenarea(context);               //获取开放城市
                    break;
                case "Getoutletlist1":
                    Getoutletlist1(context);        //获取店铺列表
                    break;
                case "GetoutletDetails":
                    GetoutletDetails(context);      //获取店铺详情
                    break;
                case "Getoutletlist":
                    Getoutletlist(context);         //获取店铺列表
                    break;
                case "Getoutlet":
                    Getoutlet(context);             //获取店铺
                    break;
                case "getorderamount":
                    getorderamount(context);        //获取订单金额（保存订单金额）
                    break;
                case "getgoodstype2":
                    getgoodstype2(context);         //获取分类
                    break;
                case "productengineoil2":
                    productengineoil2(context);         //获取列表
                    break;
                case "GetRecordCount":
                    GetRecordCount(context);         //获取文章总数
                    break;
                case "GetGoodsInfos":
                    GetGoodsInfos(context);         //获取
                    break;
                case "GetGoodsInfos2":
                    GetGoodsInfos2(context);         //获取带规格商品
                    break;
                case "getorders":
                    getorders(context);             //获取用户所有订单（除退换货）
                    break;
                case "getorder":
                    getorder(context);              //获取订单详细信息
                    break;
                case "getgoodsorder":
                    getgoodsorder(context);         //获取订单商品信息
                    break;
                case "UpdateUserinfo":
                    UpdateUserinfo(context);         //修改用户信息
                    break;
                case "GetUserListByTop":
                    GetUserListByTop(context);         //积分排行榜
                    break;
                case "GetUserListByShare":
                    GetUserListByShare(context);                //我分享的客户
                    break;
                case "order_complete":
                    order_complete(context);        //确认收货
                    break;
                case "submit_complian":
                    submit_complian(context);       //用户投诉
                    break;
                case "order_cancel":
                    order_cancel(context);          //用户取消订单
                    break;
                case "searchpro":
                    searchpro(context);                //获取搜索产品
                    break;
                case "get_usercredits":
                    get_usercredits(context);                //获取用户消耗积分
                    break;

                case "GetMsgcode":
                    GetMsgcode(context);                //修改密码验证码
                    break;
                case "updatePassword":
                    updatePassword(context);        //找回密码
                    break;

                case "GetUserPoint":
                    GetUserPoint(context);          //获取积分
                    break;
                case "is_point_shop":
                    is_point_shop(context);          //判断channer_id积分商城商品
                    break;
                case "GetUserByUid":
                    GetUserByUid(context);          //直接通过用id 返回用户信息
                    break;

                case "team_total":
                    team_total(context);        //团队销售总额
                    break;
            }
        }
        /// <summary>
        /// 团队销售总额
        /// </summary>
        public void team_total(HttpContext context)
        {
            string outmsg = "{\"status\":0,\"msg\":\"暂无信息！\"}";

            int uid = _Request.GetInt("user_id");
            BLL.users bll = new BLL.users();
            DataTable dt = bll.GetTeam_amount(uid).Tables[0];
            outmsg = CreateJsonParameters(dt);
            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }
        /// <summary>
        /// 直接通过用id 返回用户信息
        /// </summary>
        public void GetUserByUid(HttpContext context)
        {
            string outmsg = "";
            int uid = _Request.GetInt("user_id");
            BLL.users bll = new BLL.users();
            Model.users user = bll.GetModel(uid);
            outmsg = ObjectToJSON(user);

            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }


        /// <summary>
        /// 直接通过用户名 返回用户信息
        /// </summary>
        public void is_point_shop(HttpContext context)
        {
            string outmsg = "{\"status\": 0, \"msg\": \"no！\"}";

            string article_id = _Request.GetString("goodData"); //通过商品id获取 channel 
            BLL.article art = new BLL.article();
            string[] strArr = Vincent._DTcms.Utils.DelLastChar(article_id, "&").Split('&');
            decimal money = 0;
            foreach (var item in strArr)
            {
                string[] strArr2 = item.Split('&');
                var channel_id = art.GetModel(int.Parse(strArr2[0].ToString())).channel_id;
                if (channel_id ==7)       //如果为积分商城中商品            服务器：7   本地：4 
                {
                    money += Convert.ToDecimal(art.GetModel(int.Parse(strArr2[0].ToString())).sell_price);
                    outmsg = "{\"status\": 1, \"money\": \"" + money + "\"}";
                }
                else
                {
                    outmsg = "{\"status\": 0, \"msg\": \"no！\"}";
                    break;
                }
            }
            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }
        /// <summary>
        /// 直接通过用户名 返回用户信息
        /// </summary>
        public void GetUserPoint(HttpContext context)
        {
            string outmsg = "";
            int userid = _Request.GetInt("uid");
            BLL.users bll = new BLL.users();
            Model.users user = bll.GetModel(userid);
            outmsg = ObjectToJSON(user);
            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();

        }
        /// <summary>
        /// 找回密码
        /// </summary>
        /// <param name="context"></param>
        public void updatePassword(HttpContext context)
        {
            string mobile = _Request.GetString("mobile", "");
            string password = _Request.GetString("password", "");
            //验证用户是否存在
            BLL.users userBll = new BLL.users();
            if (!userBll.ExistsMobile(mobile))
            {
                context.Response.Write("{\"status\":0, \"msg\":\"该用户不存在！\"}");
                return;
            }
            Model.users userModel = userBll.GetModelMobile2(mobile);
            //执行修改操作
            var salt = Vincent._DTcms.Utils.GetCheckCode(6); //获得6位的salt加密字符串
            var passwordNew = _DESEncrypt.Encrypt(password, salt);

            userModel.password = passwordNew;
            userModel.salt = salt;
            userBll.Update(userModel);

            context.Response.Clear();
            context.Response.Write("{\"status\":1, \"msg\":\"修改成功！\"}");
            context.Response.End();
        }

        public void GetMsgcode(HttpContext context)
        {
            string outresult = "";
            string mobile = _Request.GetString("mobile", "");

            Random rd = new Random();
            int msgcode = rd.Next(100000, 999999);

            var messageNum = Vincent._MobileMessage.SendMessageCode(msgcode.ToString(), mobile);

            if (messageNum >= 0)
            {
                //写Session，设置验证码有效期，比如10分钟
                _Session.SetSession(DTKeys.SESSION_CODE, msgcode.ToString());
                _Cookie.SetCookie(DTKeys.SESSION_SMS_CODE, msgcode.ToString(), 600);

                outresult = "{\"status\":\"y\",\"info\":" + msgcode + "}";
            }
            else
            {
                outresult = "{\"status\":\"n\",\"info\":\"短信发送失败\"}";
            }

            context.Response.Clear();
            context.Response.Write(outresult);
            context.Response.End();
        }
        /// <summary>
        /// 获取用户消耗积分
        /// </summary>
        /// <param name="context"></param>
        private void get_usercredits(HttpContext context)
        {

            string outmsg = "{\"status\":0,\"msg\":\"数据错误！\"}";
            string search = DTRequest.GetQueryString("amount_val");//消费积分
            int uid =Convert.ToInt32(  DTRequest.GetQueryString("users"));//uid

            //updateamount_val

            Model.users model = new BLL.users().GetModel(uid);
            decimal credits = 0;

            if (search=="NaN")
            {
              credits =   model.amount_total;
            }
            else
            {
              credits = model.amount_total - model.frozen_amount - Convert.ToDecimal(search);
            }

            var updateamount_val = new BuysingooShop.BLL.users().UpdateField(uid, "amount_total=" + credits + "");

            if (updateamount_val>0)
            {
                   outmsg = "{\"status\":1\"}";
            }
            else
            {
                   outmsg = "{\"status\":0\"}";
            }
            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }
        /// <summary>
        /// 获取搜索的产品并json
        /// </summary>
        /// <param name="context"></param>
        private void searchpro(HttpContext context)
        {

            string outmsg = "{\"status\":0,\"msg\":\"暂无产品！\"}";
            string search = DTRequest.GetFormString("searchword");
            
            var getsearchpro = new BuysingooShop.BLL.article().GetSreachPro(0, "title like  '%" + search + "%' and  channel_id=2 ", "id desc");
            outmsg = CreateJsonParameters(getsearchpro.Tables[0]);
            if (outmsg == null || outmsg == "")
            {
                outmsg = "{\"status\":0,\"msg\":\"暂无产品！\"}";
            }
            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }


        #region 用户取消订单OK=================================
        private void order_cancel(HttpContext context)
        {
            //检查订单是否存在
            string order_no = _Request.GetString("order_id");

            Model.orders orderModel = new BLL.orders().GetModel(order_no);

            if (order_no == "" || orderModel == null)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，订单号不存在或已删除！\"}");
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
        public void submit_complian(HttpContext context)
        {
            string outmsg = "{\"status\": 0, \"msg\": \"提交失败！\"}";
            int user_id = Vincent._DTcms.DTRequest.GetQueryInt("user_id", 0);
        //    string tel = _Request.GetString("tel");

            string title = _Request.GetString("title");
            string content = _Request.GetString("content");
            if (user_id == 0)
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"请先登录！\"}");
                return;
            }
            BLL.users user = new BLL.users();
            Model.users userm = user.GetModel(user_id);
            BLL.complian complian = new BLL.complian();
            Model.complian complianm = new Model.complian();
            complianm.user_id = userm.id;
            complianm.user_name = userm.user_name;
            complianm.complian_title = title;
            complianm.complian_time = DateTime.Now;
            complianm.complian_content = content;
            complianm.mobile_phone = userm.mobile;
            complianm.is_status = 1;
            complianm.com_type = 1;
            if (complian.Add(complianm) > 0)
            {
                outmsg = "{\"status\": 1, \"msg\": \"提交成功！\"}";
            }

            context.Response.Write(outmsg);
            return;

        }
        /// <summary>
        /// 确认收货
        /// </summary>
        /// <param name="context"></param>
        private void order_complete(HttpContext context)
        {
            //检查订单是否存在
            string order_no = _Request.GetString("order_no");
            Model.orders orderModel = new BLL.orders().GetModel(order_no);
            if (order_no == "" || orderModel == null)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，订单号不存在或已删除！\"}");
                return;
            }
            //检查订单状态
            if (orderModel.status >= 90)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，该订单已完成！\"}");
                return;
            }
            bool result = new BLL.orders().UpdateField(order_no, "status=90");
            if (!result)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，操作过程中发生不可遇知的错误！\"}");
                return;
            }
            context.Response.Write("{\"status\":1, \"msg\":\"取消订单成功！\"}");
            return;
        }
        /// <summary>
        /// 我分享的客户
        /// </summary>
        /// <param name="context"></param>
        public void GetUserListByShare(HttpContext context)
        {
            string outmsg = "";

            var user_id = _Request.GetString("user_id", "0");
            string userName = _Request.GetString("userName");
            var Level = _Request.GetInt("level", 0);

            int pageIndex = _Request.GetInt("pageIndex");
            int pageSize = _Request.GetInt("pageSize");

            var recordCount = 0;        //总记录数
            int pageall = 0;            //总页数

            BLL.users bll = new BLL.users();

            var balanceList = new DataSet();
            if (Level > 0)
                balanceList = bll.GetList(pageSize, pageIndex, "id <> " + user_id + " and len(user_name) = 11 and id in (select id from F_GetUserNetByShare(" + user_id + ") where [Level]=" + Level + ") ", "id asc", out recordCount);
            else
                balanceList = bll.GetList(pageSize, pageIndex, "id <> " + user_id + " and len(user_name) = 11 and id in (select id from F_GetUserNetByShare(" + user_id + ")) ", "id asc", out recordCount);

            DataTable dt = balanceList.Tables[0];


            var parnick_name = "公司";
            var preUserInfo = bll.GetPreUserInfo(Vincent._Convert.ToInt(user_id));
            if (preUserInfo != null && preUserInfo.Tables.Count > 0 && preUserInfo.Tables[0].Rows.Count > 0)
            {
                parnick_name = preUserInfo.Tables[0].Rows[0]["nick_name"].ToString();
            }

            pageall = _Utility.GetPageAll(pageSize, recordCount);
            _Json json = new _Json(dt);
            outmsg = "{page:{pageindex:'" + pageIndex + "',pagesize:'" + pageSize + "',recordtotal:'" + recordCount + "',pageall:'" + pageall + "',parnick_name:'" + parnick_name + "'},record:" + json.ToJson() + "}";

            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }
        /// <summary>
        /// 积分排行榜
        /// </summary>
        /// <param name="context"></param>
        public void GetUserListByTop(HttpContext context)
        {
            string outmsg = "";
            int user_id = _Request.GetInt("user_id");
            int pageIndex = _Request.GetInt("pageIndex");
            int pageSize = _Request.GetInt("pageSize");

            var recordCount = 0;        //总记录数
            int pageall = 0;            //总页数

            BLL.users bll = new BLL.users();
            //var userList = bll.GetList(100, "isDelete = 0 and [user_name] like (select [user_name] from dt_users where id = " + user_id + " ) + '%'", "id");

            var userList = bll.GetList(pageSize, pageIndex, "isDelete = 0 and group_id<5", "amount_total desc", out recordCount);

            DataTable dt = userList.Tables[0];

            pageall = _Utility.GetPageAll(pageSize, recordCount);
            _Json json = new _Json(dt);
            outmsg = "{page:{pageindex:'" + pageIndex + "',pagesize:'" + pageSize + "',recordtotal:'" + recordCount + "',pageall:'" + pageall + "'},record:" + json.ToJson() + "}";

            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }
        
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="context"></param>
        public void UpdateUserinfo(HttpContext context)
        {
            string outmsg = "{\"status\":0,\"msg\":\"修改失败！\"}";
            int users = _Request.GetInt("users");
            string nick_name = _Request.GetString("nick_name");
            string real_name = _Request.GetString("real_name");
            string sex = _Request.GetString("sex");//性别
            string telphone = _Request.GetString("telphone");
            string province = _Request.GetString("province");//省
            string city = _Request.GetString("city");//市
            DateTime birthday = Vincent._Convert.ToDateTime(_Request.GetString("birthday"));//生日


            BLL.users bll = new BLL.users();
            Model.users model = bll.GetModel(users);



            if (model != null)
            {
                model.nick_name = nick_name;
                model.real_name = real_name;
                model.sex = sex;
                model.birthday = birthday;
                model.telphone = telphone;
                model.Provinces = province;
                model.City = city;    

                if (bll.Update(model))
                {
                    outmsg = "{\"status\":1,\"msg\":\"修改成功！\"}";
                }
            }

            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }

        /// <summary>
        /// 获取订单商品信息
        /// </summary>
        public void getgoodsorder(HttpContext context)
        {
            int orderid = int.Parse(_Request.GetString("orderid")); 
            string outmsg = "{\"info\":\"订单信息！\", \"status\":0}";
            BLL.order_goods art = new BLL.order_goods();
            var info = art.GetList(0, "order_id=" + orderid, "id desc");
            outmsg=  CreateJsonParameters(info.Tables[0]);
            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }

     /// <summary>
        /// 获取用户所有订单（除退换货）
        /// </summary>
        public void getorders(HttpContext context)
        {
            int userid = _Request.GetInt("userid");
            string outmsg = "{\"info\":\"没有订单！\", \"status\":0}";
            DataTable dt = new BLL.orders().GetOrderList(0, " refund_status!=1 and refund_status!=2 and refund_status!=3 and t1.user_id=" + userid + " or refund_status IS NULL and t1.user_id=" + userid, " t1.id desc").Tables[0];

            dt.Columns.Add("goods_share", typeof(int));
        

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    DataTable d = new BLL.order_goods().GetList(" order_id=" + item["id"]).Tables[0];
                    item["goods_share"] = d.Rows.Count;
                }
               
            }
            
            outmsg = CreateJsonParameters(dt);
           
            if (outmsg == null || outmsg == "")
            {
                outmsg = "{\"info\":\"没有订单！\", \"status\":0}";
            }

            //string outmsg = SoapClient.getorders(userid);
            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }

        /// <summary>
        /// 获取订单详细信息
        /// </summary>
        public void getorder(HttpContext context)
        {
            int orderid = _Request.GetInt("orderid");
            string outmsg = "NO";

            DataTable dt = new BLL.orders().GetOrderList(0, " refund_status!=1 and t1.id=" + orderid + " or refund_status!=2 and t1.id=" + orderid + " or refund_status!=3 and t1.id=" + orderid + " or refund_status IS NULL and t1.id=" + orderid, " t1.id desc").Tables[0];
            if (dt.Rows.Count > 0)
            {
                outmsg = CreateJsonParameters(dt);
            }

            //string outmsg = SoapClient.getorders(userid);
            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }
        /// <summary>
        /// 获取带规格的商品
        /// </summary>
        public void GetGoodsInfos2(HttpContext context)
        {
            int goodsId = _Request.GetInt("id");//类别ID 
            string outmsg = "{\"status\":0,\"msg\":\"暂无产品！\"}";//有规格标识 is_red=1

            var gettypegoods = new BuysingooShop.BLL.article().GetList1(100, "category_id=(select category_id from dt_article  where id=" + goodsId + "   ) ", "sort_id asc");
            outmsg = CreateJsonParameters(gettypegoods.Tables[0]);
            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }
   
        /// <summary>
        /// 获取商品详细信息
        /// </summary>
        public void GetGoodsInfos(HttpContext context)
        {
            string outmsg = "{\"status\":0,\"msg\":\"暂无商品！\"}";
            int id = _Request.GetInt("id");

            BLL.article art = new BLL.article();

            var getGoodsinfo = art.GetList1(0, "id=" + id + "", "sort_id asc");

            outmsg = CreateJsonParameters(getGoodsinfo.Tables[0]);
            if (outmsg == null || outmsg == "")
            {
                outmsg = "{\"status\":0,\"msg\":\"还没有任何分类！\"}";
            }
            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }


        /// <summary>
        /// 获取记录总数
        /// </summary>
        public void GetRecordCount(HttpContext context)
        {
          
            int id = _Request.GetInt("id");//类别ID
            BLL.article bll = new BLL.article();
            int total = bll.GetCount("category_id in  (select  id FROM dt_article_category  where parent_id  in (select id FROM dt_article_category  where parent_id =" + id + "))  or  category_id= 	" + id + "  or  category_id in (select  id FROM dt_article_category  where parent_id =" + id + ")");
           
            context.Response.Clear();
            context.Response.Write(total);
            context.Response.End();
        }
        /// <summary>
        /// 获取商品分类   
        /// </summary>
        public void getgoodstype2(HttpContext context)
        {
            string outmsg = "{\"status\":0,\"msg\":\"暂无商品分类！\"}";
            int channel_id = _Request.GetInt("channel_id");//类别ID //2

            int parent_id = 0;//parent_id为0的

            BLL.article_category art_cate = new BLL.article_category();

            var getgoodTypes = art_cate.GetList1(0, "channel_id=" + channel_id + "  and  parent_id= " + parent_id + "", "id asc");


            outmsg = CreateJsonParameters(getgoodTypes.Tables[0]);
            if (outmsg == null || outmsg == "")
            {
                outmsg = "{\"status\":0,\"msg\":\"还没有任何分类！\"}";
            }
            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }
        /// <summary>
        /// 获取订单信息(保存订单金额)
        /// </summary>
        /// <param name="context"></param>
        private void getorderamount(HttpContext context)
        {
            string msg = "{\"status\":0, \"msg\":\"保存失败！\"}";

            var order_id = Vincent._Request.GetInt("order_id");     // int.Parse(Vincent._DTcms.DTRequest.GetFormString("order_id"));

            Model.orders mo = new BLL.orders().GetModel(order_id);

            if (mo != null)
            {
                context.Session["price_" + order_id] = mo.order_amount;//应付金额保存到session
                msg = "{\"status\":1, \"msg\":\"保存成功！\"}";
            }

            context.Response.Write(msg);

        }
        /// <summary>
        /// 获取店铺
        /// </summary>
        /// <param name="context"></param>
        public void Getoutlet(HttpContext context)
        {
            string outmsg = "{\"status\":0,\"msg\":\"该区域还没有任何店铺！\"}";
            string area = _Request.GetString("area");
            double longitude = double.Parse(_Request.GetString("longitude"));
            double latitude = double.Parse(_Request.GetString("latitude"));
            BLL.outlet bll = new BLL.outlet();
            DataTable dt = bll.GetList(0, " area='" + area + "'", " id").Tables[0];
            //dt.Columns.Add("space", typeof(string));

            //if (dt.Rows.Count > 0)
            //{
            //    foreach (DataRow item in dt.Rows)
            //    {
            //        var dis = BaiDuMap.GetLongDistance(longitude, latitude, double.Parse(item["x_zb"].ToString()), double.Parse(item["y_zb"].ToString()));
            //        item["space"] = dis;
            //    }
            //    dt.DefaultView.Sort = "space asc";
            //    dt = dt.DefaultView.ToTable();
            //}

            outmsg = CreateJsonParameters(dt);
            if (outmsg == null || outmsg == "")
            {
                outmsg = "{\"status\":0,\"msg\":\"该区域还没有任何店铺！\"}";
            }

            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();

        }

        /// <summary>
        /// 获取店铺列表
        /// </summary>
        /// <param name="context"></param>
        public void Getoutletlist(HttpContext context)
        {
            //double lon = 0.01185;
            //double lat = 0.00328;
            string outmsg = "{\"status\":0,\"msg\":\"该区域还没有任何店铺！\"}";
            double longitude = double.Parse(_Request.GetString("longitude"));
            double latitude = double.Parse(_Request.GetString("latitude"));
            BLL.outlet bll = new BLL.outlet();
            DataTable dt = bll.GetList(0, "0=0", " id").Tables[0];
            dt.Columns.Add("space", typeof(double));

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    var dis = Vincent._Baidu.Map.GetLongDistance(longitude, latitude, double.Parse(item["x_zb"].ToString()), double.Parse(item["y_zb"].ToString()));
                    item["space"] = dis;
                }
                dt.DefaultView.Sort = "space asc";
                dt = dt.DefaultView.ToTable();

                outmsg = CreateJsonParameters(dt);
            }
            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }

        /// <summary>
        /// 获取店铺列表
        /// </summary>
        /// <param name="context"></param>
        public void Getoutletlist1(HttpContext context)
        {
            //double lon = 0.01185;
            //double lat = 0.00328;
            string outmsg = "{\"status\":0,\"msg\":\"该区域还没有任何店铺！\"}";
            string area = _Request.GetString("area");
            double longitude = double.Parse(_Request.GetString("longitude"));
            double latitude = double.Parse(_Request.GetString("latitude"));
            BLL.outlet bll = new BLL.outlet();
            DataTable dt = bll.GetList(0, " area='" + area + "'", " id").Tables[0];
            dt.Columns.Add("space", typeof(double));

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    var dis = Vincent._Baidu.Map.GetLongDistance(longitude, latitude, double.Parse(item["x_zb"].ToString()), double.Parse(item["y_zb"].ToString()));
                    item["space"] = dis;
                }
                dt.DefaultView.Sort = "space asc";
                dt = dt.DefaultView.ToTable();

                outmsg = CreateJsonParameters(dt);
            }
            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }

        /// <summary>
        /// 获取店铺详情
        /// </summary>
        /// <param name="context"></param>
        public void GetoutletDetails(HttpContext context)
        {
            string outmsg = "{\"status\":0,\"msg\":\"获取失败！\"}";
            int outlet_id = _Request.GetInt("id");
            BLL.outlet bll = new BLL.outlet();
            Model.outlet model = bll.GetModel(outlet_id);
            if (model != null)
            {
                outmsg = ObjectToJSON(model);
            }
            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }
        /// <summary>
        /// 获取开放城市
        /// </summary>
        /// <param name="context"></param>
        public void getOpenarea(HttpContext context)
        {
            string outmsg = "{\"status\":0,\"msg\":\"暂时还没有开放任何城市！\"}";
            BLL.openarea bll = new BLL.openarea();
            DataTable dt = bll.GetList(0, " 0=0", " id desc").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                outmsg = CreateJsonParameters(dt);
            }
            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }
        /// <summary>
        /// 提交订单
        /// </summary>
        /// <param name="context"></param>
        public void submitorder(HttpContext context)
        {
            string goods = _Request.GetString("goods", "");
            string AddressId = _Request.GetString("AddressId", "");
            string expressId = _Request.GetString("expressId", "");
            string totalprice = _Request.GetString("totalprice", "");
            string bill_type = _Request.GetString("bill_type", "");
            string bill_rise = _Request.GetString("bill_rise", "");
            string down_order = _Request.GetString("down_order", "");
            string coupon_no = _Request.GetString("coupon_no", "");
            string store_name = _Request.GetString("store_name", "");
            string store_address = _Request.GetString("store_address", "");
            string store_id = _Request.GetString("store_id", "");
            string user_id = _Request.GetInt("user_id", 0).ToString();
            string remark = _Request.GetString("remark", "");

            string outmsg = SoapClient.SubmitOrder(goods, AddressId, expressId, totalprice, bill_type, bill_rise, down_order, coupon_no, store_name, store_address, store_id, user_id, remark);
            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }
        /// <summary>
        /// 提现
        /// </summary>
        /// <param name="context"></param>
        public void submit_withdraw(HttpContext context)
        {
            var user_id = _Request.GetInt("user_id");
            var amount = _Request.GetFloat("withdraw_amount");
            var banktype = _Request.GetString("banktype");
            var remark = _Request.GetString("reason");
            var card_no = _Request.GetString("card");
            var openId = _Request.GetString("openid");
            var mobile = _Request.GetString("mobile");

            Vincent._Log.SaveMessage("user_id=" + user_id);
            Vincent._Log.SaveMessage("amount=" + amount);
            Vincent._Log.SaveMessage("banktype=" + banktype);
            Vincent._Log.SaveMessage("remark=" + remark);
            Vincent._Log.SaveMessage("card_no=" + card_no);
            Vincent._Log.SaveMessage("openId=" + openId);
            Vincent._Log.SaveMessage("mobile=" + mobile);

            Model.withdraw model = new Model.withdraw();
            BLL.withdraw bll = new BLL.withdraw();

            //获取用户信息
            Model.users users = new BLL.users().GetModel(user_id);

            //提现信息
            model.user_id = user_id;
            model.card_no = card_no;
            model.amount = Vincent._Convert.ToDecimal(amount.ToString(), 0);
            model.banktype = Vincent._Convert.ToInt(banktype.ToString(), 1);
            model.remark = remark;
            model.status = 1;
            model.addtime = DateTime.Now;
            model.OpenId = openId;
            model.Mobile = mobile;


            //更新冻结金额
            users.frozen_amount_total += decimal.Parse(amount.ToString());
            bool user_frozen_amount = new BLL.users().Update(users);

            int orderId = bll.Add(model);
            string outmsg = "{\"status\":1,\"msg\":\"申请已提交，我们将尽快安排付款！\"}";

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
            string nowtime = DateTime.Now.ToString("HHmmss");
            //生成随机数
            string randonNum = new Random().Next(0, 9999).ToString();
            //补齐位数
            randonNum = string.Format("{0:d4}", randonNum);
            return nowtime + randonNum;
        }


        /// <summary>
        /// 提交订单（应付金额为零）
        /// </summary>
        /// <param name="context"></param>
        public void submitorders(HttpContext context)
        {
            string goods = _Request.GetString("goods", "");
            string AddressId = _Request.GetString("AddressId", "");
            string expressId = _Request.GetString("expressId", "");
            string totalprice = _Request.GetString("totalprice", "");
            string bill_type = _Request.GetString("bill_type", "");
            string bill_rise = _Request.GetString("bill_rise", "");
            string down_order = _Request.GetString("down_order", "");
            string coupon_no = _Request.GetString("coupon_no", "");
            string store_name = _Request.GetString("store_name", "");
            string store_address = _Request.GetString("store_address", "");
            string store_id = _Request.GetString("store_id", "");
            string user_id = _Request.GetInt("user_id", 0).ToString();
            string remark = _Request.GetString("remark", "");
            string point = _Request.GetString("point", "");//用户使用的积分
            string schoolData = _Request.GetString("schoolData", "");

            string returnvalue = "";
            Model.orders model = new Model.orders();
            BLL.orders bll = new BLL.orders();


            //验证优惠券
            var j = 0;

            BLL.user_coupon couponbll = new BLL.user_coupon();
            Model.user_coupon coupon = null;
            if (coupon_no != "")
            {

                coupon = couponbll.GetModel(" str_code='" + coupon_no + "'");
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
                returnvalue = "{\"status\":0,\"msg\":\"优惠券编码输入有误！\"}";
                context.Response.Write(returnvalue);
                return;
            }
            if (j == 2)
            {
                returnvalue = "{\"status\":0,\"msg\":\"优惠券已经过期！\"}";
                context.Response.Write(returnvalue);
                return;
            }
            if (j == 3)
            {
                returnvalue = "{\"status\":0,\"msg\":\"优惠券已使用！\"}";
                context.Response.Write(returnvalue);
                return;
            }

            BLL.users bll1 = new BLL.users();
            Model.users userinfo = bll1.GetModel(int.Parse(user_id));

            if (AddressId != "0")//快递收货
            {
                //订单信息
                Model.user_address modelAddress = new BLL.user_address().GetModel(int.Parse(AddressId));
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
                decimal real_amount = Decimal.Parse(totalprice) - modelExpress.express_fee;
                model.real_amount = real_amount;
                model.order_amount = Decimal.Parse(totalprice);
                model.bill_type = int.Parse(bill_type);
                if (int.Parse(bill_type) != 0)
                {
                    model.is_bill = 1;
                }
                model.invoice_rise = bill_rise;
                model.down_order = down_order;
                model.remark = remark;
                model.point = int.Parse( point);
                if (schoolData != "undefined")
                {
                    model.school_info = schoolData;
                }
            }
            else
            {
                //订单信息
                Model.express modelExpress = new BLL.express().GetModel(int.Parse(expressId));


                model.order_no = CreateOrderNo();
                model.add_time = DateTime.Now;
                model.user_id = userinfo.id;
                model.user_name = userinfo.user_name;
                model.mobile = userinfo.mobile;
                model.express_id = int.Parse(expressId);
                model.express_fee = modelExpress.express_fee;
                model.express_status = 1;
                model.status = 1;
                //decimal real_amount = Decimal.Parse(totalprice) - modelExpress.express_fee;
                model.real_amount = Decimal.Parse(totalprice);
                model.order_amount = Decimal.Parse(totalprice);
                model.bill_type = int.Parse(bill_type);
                if (int.Parse(bill_type) != 0)
                {
                    model.is_bill = 1;
                }
                model.invoice_rise = bill_rise;
                model.down_order = down_order;
                model.store_name = store_name;
                model.store_address = store_address;
                model.store_id = int.Parse(store_id);
                model.remark = remark;
                model.point = int.Parse(point);
                if (schoolData != "undefined")
                {
                    model.school_info = schoolData;
                }

            }

            var k = 0;
            var p = 0;
            if (coupon != null)
            {
                decimal payamount = Decimal.Parse(totalprice) - coupon.amount;
                if (payamount > 0)
                {
                    model.payable_amount = payamount;//实付款
                    model.str_code = coupon_no;
                }
                else
                {
                    model.payable_amount = 0M;//实付款
                    model.str_code = coupon_no;
                    model.status = 2;
                    model.payment_status = 2;
                    p = bll.Add(model);
                    k = 1;
                }
            }



            ////商品信息value="<%#Eval("id") %>|<%#Eval("type") %>|<%#Eval("price") %>|<%#Eval("quantity") %>|<%#Eval("weight") %>|<%#Eval("img_url") %>"

            System.Collections.Generic.List<Model.order_goods> list = new System.Collections.Generic.List<Model.order_goods>();

            string[] strArr = Vincent._DTcms.Utils.DelLastChar(goods, "&").Split('&');
            foreach (var item in strArr)
            {

                string[] strArr2 = item.Split('|');
                Model.order_goods modelGoods = new Model.order_goods();
                modelGoods.goods_id = int.Parse(strArr2[0].ToString());
                modelGoods.goods_price = decimal.Parse(strArr2[1].ToString());
                modelGoods.quantity = int.Parse(strArr2[2].ToString());
                modelGoods.goods_pic = strArr2[3].ToString();
                modelGoods.goods_title = strArr2[4].ToString();
                list.Add(modelGoods);
            }

            model.order_goods = list;
            int orderId = bll.Add(model);


            //优惠券使用记录
            BLL.user_coupon_log cbll = new BLL.user_coupon_log();
            Model.user_coupon_log cmodel = new Model.user_coupon_log();
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

            if (k == 1 && p > 0)
            {
                cmodel.status = 2;
                cbll.Add(cmodel);
                coupon.status = 2;
                couponbll.Update(coupon);

                returnvalue = "{\"status\":3,\"msg\":\"订单提交成功,已付款！\"}";
                context.Response.Write(returnvalue);
                return;
            }

            if (orderId > 0)
            {
                if (coupon != null)
                {
                    cbll.Add(cmodel);
                }

                returnvalue = "{\"status\":1,\"msg\":\"订单提交成功，请付款！\",\"orderId\":" + orderId + "}";


                Web.UI.ShopCart.ClearCart("0");
             
              
            }
            else
            {
                if (coupon != null)
                {
                    cbll.Add(cmodel);
                }

                returnvalue = "{\"status\":0,\"msg\":\"订单提交失败，请重新提交订单！\"}";
            }



            context.Response.Clear();
            context.Response.Write(returnvalue);
            context.Response.End();
        }
        /// <summary>
        /// 默认地址
        /// </summary>
        /// <param name="context"></param>
        public void DefaultAddress(HttpContext context)
        {
            string outmsg = "{\"status\":1, \"msg\":\"设置成功！\"}";
            int addressid = _Request.GetInt("addressid");
            int userid = _Request.GetInt("userid");

            BLL.user_address bll = new BLL.user_address();
            //设置默认地址
            bll.UpdateField("is_default=0", "user_id=" + userid);
            bll.UpdateField("is_default=1", "id=" + addressid + " and user_id=" + userid);


            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();

        }
        /// <summary>
        /// 修改地址
        /// </summary>
        /// <param name="context"></param>
        public void UpdateAddress(HttpContext context)
        {
            string outmsg = "{\"info\":\"修改收货地址失败！\", \"status\":0}";

            Model.user_address addressModel = new Model.user_address();
            BLL.user_address addressBll = new BLL.user_address();


            int userid = _Request.GetInt("userid");
            string name = _Request.GetString("name");
            string province = _Request.GetString("province");
            string city = _Request.GetString("city");
            string area = _Request.GetString("area");
            string street = _Request.GetString("street");
            int zipcode = int.Parse(_Request.GetString("zipcode"));
            string tel = _Request.GetString("tel");
            int addressid = int.Parse(_Request.GetString("addressid"));

            addressModel = addressBll.GetModel(addressid);

            if (addressModel == null)
            {
                context.Response.Write("{\"info\":\"修改收货地址失败！\", \"status\":0}");
                return;
            }

            addressModel.user_id = userid;
            addressModel.acceptName = name;
            addressModel.provinces = province;
            addressModel.citys = city;
            addressModel.area = area;
            addressModel.street = street;
            addressModel.mobile = tel;
            addressModel.add_time = DateTime.Now;
            addressModel.postcode = zipcode;


            //执行修改操作
            if (addressBll.Update(addressModel))
            {
                outmsg = "{\"info\":\"修改收货地址成功！\", \"status\":1}";

            }

            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();

        }

        /// <summary>
        /// 添加收货地址
        /// </summary>
        public void addAddress(HttpContext context)
        {
            string outmsg = "{\"info\":\"新增收货地址失败！\", \"status\":0}";

            Model.user_address addressModel = new Model.user_address();

            int userid = _Request.GetInt("userid");
            string name = _Request.GetString("name");
            string province = _Request.GetString("province");
            string city = _Request.GetString("city");
            string area = _Request.GetString("area");
            string street = _Request.GetString("street");
            int zipcode = Vincent._Convert.ToInt(_Request.GetString("zipcode"), 518000);
            string tel = _Request.GetString("tel");


            addressModel.user_id = userid;
            addressModel.acceptName = name;
            addressModel.provinces = province;
            addressModel.citys = city;
            addressModel.area = area;
            addressModel.street = street;
            addressModel.mobile = tel;
            addressModel.add_time = DateTime.Now;
            addressModel.postcode = zipcode;
          

            BLL.user_address addressBll = new BLL.user_address();
            //执行新增操作
            if (addressBll.Add(addressModel) > 0)
            {
                outmsg = "{\"info\":\"新增收货地址成功！\", \"status\":1}";

            }

            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }
        /// <summary>
        /// 获取修改地址
        /// </summary>
        /// <param name="context"></param>
        public void GetUpdateAddress(HttpContext context)
        {
            string outmsg = "ON";
            int addressid = _Request.GetInt("addressid");

            BLL.user_address bll = new BLL.user_address();
            if (bll.GetModel(addressid) != null)
            {
                outmsg = ObjectToJSON(bll.GetModel(addressid));
            }

            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();

        }

        /// <summary>
        /// 删除地址
        /// </summary>
        /// <param name="context"></param>
        public void DeleteAddress(HttpContext context)
        {
            string outmsg = "{\"status\":0, \"msg\":\"删除失败！\"}";
            int addressid = _Request.GetInt("addressid");

            BLL.user_address bll = new BLL.user_address();
            if (bll.Delete(addressid))
            {
                outmsg = "{\"status\":1, \"msg\":\"删除成功！\"}";
            }

            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();

        }
        /// <summary>
        /// 获取收货地址
        /// </summary>
        public void GetAddress(HttpContext context)
        {
            string outmsg = "{\"status\":0,\"msg\":\"您还没有添加任何地址！\"}";
            int userid = _Request.GetInt("userid");
         
            DataTable dt = new BLL.user_address().GetList(" user_id=" + userid).Tables[0];

            if (dt != null && dt.Rows.Count > 0)
            {
                outmsg = CreateJsonParameters(dt);
            }
            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }
        /// <summary>
        /// 获取产品相册
        /// </summary>
        public void Productsimg(HttpContext context)
        {
            string outmsg = "{\"status\":0,\"msg\":\"没有分类！\"}";
            int id = _Request.GetInt("id");

            BLL.article_albums bll = new BLL.article_albums();
            DataTable dt = bll.GetList(" article_id =" + id + " ").Tables[0];

             var model=  bll.GetModel(id);


            if (dt != null && dt.Rows.Count > 0)
            {
                outmsg = ObjectToJSON(model);
            }

            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }

        /// <summary>
        /// 获取产品相关类别
        /// </summary>
        public void Productcategory(HttpContext context)
        {
            string outmsg = "{\"status\":0,\"msg\":\"没有分类！\"}";
            int id = _Request.GetInt("id");
            BLL.article_category bll = new BLL.article_category();
            DataTable dt = bll.GetList1(0, " id =" + id + " ", "id asc").Tables[0];
            string json = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                json += dt.Rows[i]["parent_id"];
            }
            int parent_id = Convert.ToInt32(json);
            DataTable dts = bll.GetLists(parent_id, 2);
            if (dts != null && dts.Rows.Count > 0)
            {
                outmsg = CreateJsonParameters(dts);
            }
            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }








        /// <summary>
        /// 获取产品系列列表
        /// </summary>
        public void productlist(HttpContext context)
        {
            string outmsg = "{\"status\":0,\"msg\":\"没有分类！\"}";

            int parent_id = _Request.GetInt("parent_id");

            BLL.article_category bll = new BLL.article_category();
            DataTable dt = bll.GetLists(parent_id, 2);
            if (dt != null && dt.Rows.Count > 0)
            {
                outmsg = CreateJsonParameters(dt);
            }

            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }
        /// <summary>
        /// 获取产品分类系列列表
        /// </summary>
        public void productlistclassify(HttpContext context)
        {
            string outmsg = "{\"status\":0,\"msg\":\"没有分类！\"}";

            int parent_id = _Request.GetInt("id");

            BLL.article_category bll = new BLL.article_category();
            DataTable dt = bll.GetLists(parent_id, 2);
            if (dt != null && dt.Rows.Count > 0)
            {
                outmsg = CreateJsonParameters(dt);
            }

            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }

        /// <summary>
        /// 分类下获取产品
        /// </summary>
        public void productlistclassifyarticle(HttpContext context)
        {
            string outmsg = "{\"status\":0,\"msg\":\"没有分类！\"}";

            int id = _Request.GetInt("id");
            BLL.article bll = new BLL.article();
            DataTable dt = bll.GetList1(6, "category_id=" + id + " ", "sort_id asc").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                outmsg = CreateJsonParameters(dt);
            }

            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }

        /// <summary>
        /// 获取轮胎系列列表
        /// </summary>
        public void producttyreserieslist(HttpContext context)
        {
            string outmsg = "{\"status\":0,\"msg\":\"没有分类！\"}";

            int id = _Request.GetInt("id");
            BLL.article_category bll = new BLL.article_category();
            DataTable dt = bll.GetList1(0, "parent_id=" + id + " ", "sort_id asc,id desc").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                outmsg = CreateJsonParameters(dt);
            }

            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }
        /// <summary>
        /// 获取轮胎系列产品
        /// </summary>
        public void producttyreseries(HttpContext context)
        {
            string outmsg = "{\"status\":0,\"msg\":\"没有分类！\"}";

            int id = _Request.GetInt("id");
            BLL.article bll = new BLL.article();
            DataTable dt = bll.GetList1(4, "category_id in (select id from dt_article_category where parent_id =" + id + ")  or category_id=" + id + "  ", "sort_id asc").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                outmsg = CreateJsonParameters(dt);
            }
            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }

        /// <summary>
        /// 获取机油系列列表
        /// </summary>
        public void productengineoil(HttpContext context)
        {
            string outmsg = "{\"status\":0,\"msg\":\"没有分类！\"}";

            int id = _Request.GetInt("id");
            BLL.article bll = new BLL.article();
            DataTable dt = bll.GetList1(6, "category_id in  (select  id FROM dt_article_category  where parent_id  in (select id FROM dt_article_category  where parent_id =" + id + "))  or  category_id= 	" + id + "  or  category_id in (select  id FROM dt_article_category  where parent_id =" + id + ")", "sort_id asc").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                outmsg = CreateJsonParameters(dt);
            }
            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }
        /// <summary>
        /// 获取机油系列列表
        /// </summary>
        public void productengineoil2(HttpContext context)
        {
            string outmsg = "{\"status\":0,\"msg\":\"没有分类！\"}";

            int id = _Request.GetInt("id");
            int count = _Request.GetInt("count");


            BLL.article bll = new BLL.article();
            DataTable dt = bll.GetList1(count, "category_id in  (select  id FROM dt_article_category  where parent_id  in (select id FROM dt_article_category  where parent_id =" + id + "))  or  category_id= 	" + id + "  or  category_id in (select  id FROM dt_article_category  where parent_id =" + id + ")", "sort_id asc").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                outmsg = CreateJsonParameters(dt);
            }
            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }
        /// <summary>
        /// 获取镀晶系列列表
        /// </summary>
        public void productSealant(HttpContext context)
        {
            string outmsg = "{\"status\":0,\"msg\":\"没有分类！\"}";

            int id = _Request.GetInt("id");
            BLL.article bll = new BLL.article();
            DataTable dt = bll.GetList1(3, "category_id in (select id from dt_article_category where parent_id =" + id + ") ", "sort_id asc,id asc").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                outmsg = CreateJsonParameters(dt);
            }

            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }
        /// <summary>
        /// 获取镀晶系列列表
        /// </summary>
        public void productSealant2(HttpContext context)
        {
            string outmsg = "{\"status\":0,\"msg\":\"没有分类！\"}";

            int id = _Request.GetInt("id");
            BLL.article bll = new BLL.article();
            //DataTable dt = bll.GetList1(3, "category_id in (select id from dt_article_category where parent_id =" + id + ") ", "sort_id asc,id asc").Tables[0];
            DataTable dt = bll.GetList1(3, "category_id  =" + id + " ", "sort_id asc,id asc").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                outmsg = CreateJsonParameters(dt);
            }

            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }

        /// <summary>
        /// 获取养生系列列表
        /// </summary>
        public void producthealth(HttpContext context)
        {
            string outmsg = "{\"status\":0,\"msg\":\"没有分类！\"}";

            int id = _Request.GetInt("id");
            BLL.article bll = new BLL.article();
            DataTable dt = bll.GetList1(3, "category_id  =" + id + " ", "sort_id asc,id asc").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                outmsg = CreateJsonParameters(dt);
            }

            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }


        /// <summary>
        /// 获取荣誉资质系列列表
        /// </summary>
        public void productHonor(HttpContext context)
        {
            string outmsg = "{\"status\":0,\"msg\":\"没有分类！\"}";

            int id = _Request.GetInt("id");
            BLL.article bll = new BLL.article();
            DataTable dt = bll.GetList1(0, "category_id  =" + id + " ", "sort_id asc,id asc").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                outmsg = CreateJsonParameters(dt);
            }

            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }

        /// <summary>
        /// 获取钻石会员套餐
        /// </summary>
        public void membership(HttpContext context)
        {
            string outmsg = "{\"status\":0,\"msg\":\"没有分类！\"}";
            int id = _Request.GetInt("id");
            BLL.article bll = new BLL.article();
            DataTable dt = bll.GetList1(0, " category_id in (select id from dt_article_category where parent_id="+id+")", "sort_id asc").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                outmsg = CreateJsonParameters(dt);
            }
            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }


        /// <summary>
        /// 获取热卖商品
        /// </summary>
        /// <param name="context"></param>
        public void membership_hot(HttpContext context)
        {
            string outmsg = "{\"status\":0,\"msg\":\"还没有任何商品！\"}";

            BLL.article art = new BLL.article();
            var info = art.GetList1(0, " channel_id=2 and status=0 ", " sort_id desc");
            outmsg = CreateJsonParameters(info.Tables[0]);
            if (outmsg == null || outmsg == "")
            {
                outmsg = "{\"status\":0,\"msg\":\"还没有任何商品！\"}";
            }
            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }

        /// <summary>
        /// 获取主题内容
        /// </summary>
        public void contact(HttpContext context)
        {
            string outmsg = "{\"status\":0,\"msg\":\"没有分类！\"}";

            int id = _Request.GetInt("id");
            BLL.article bll = new BLL.article();
            DataTable dt = bll.GetListqian(0, "category_id  =" + id + " ", "sort_id asc,id asc").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                outmsg = CreateJsonParameters(dt);
            }

            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }
        /// <summary>
        /// 获取返回商品图片
        /// </summary>
        public void contactabout(HttpContext context)
        {
            string outmsg = "{\"status\":0,\"msg\":\"没有分类！\"}";

            int id = _Request.GetInt("id");
            BLL.article bll = new BLL.article();

            if (bll.GetModel(id) != null)
            {
                outmsg = ObjectToJSON(bll.GetModel(id));
            }

            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }
        /// <summary>
        /// 获取username
        /// </summary>
        public void username(HttpContext context)
        {
            string outmsg = "{\"status\":0,\"msg\":\"没有分类！\"}";

            int users = _Request.GetInt("users");
            BLL.users bll = new BLL.users();

            DataTable dt = bll.GetList(0, " id =" + users + " ", "id asc").Tables[0];

            if (dt != null && dt.Rows.Count > 0)
            {
                outmsg = CreateJsonParameters(dt);
            }

            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();


        }
        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="context"></param>
        private void user_login_out(HttpContext context)
        {
            //安全退出
            context.Session[DTKeys.SESSION_USER_INFO] = null;
            context.Session.Clear();
            context.Session.Remove(DTKeys.SESSION_USER_INFO);
            string rememberCookie = Utils.GetCookie(DTKeys.COOKIE_USER_NAME_REMEMBER, "Yhluser");
            if (rememberCookie != null)
            {
                Utils.WriteCookie(DTKeys.COOKIE_USER_NAME_REMEMBER, "Yhluser", null);
            }
            context.Response.Write("{\"status\":0, \"username\":\"　\"}");
        }

        /// <summary>
        /// 获取首页最新活动
        /// </summary>
        public void loadnews(HttpContext context)
        {
            string outmsg = "{\"status\":0,\"msg\":\"没有分类！\"}";
            int id = _Request.GetInt("id");
            BLL.article bll = new BLL.article();
            DataTable dt = bll.GetList(3, "category_id  =" + id + " ", "sort_id asc,id asc").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                outmsg = CreateJsonParameters(dt);
            }
            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();

        }
        /// <summary>
        /// 获取url地址
        /// </summary>
        public void GetConfigUrl(HttpContext context)
        {
            string imgUrl = System.Configuration.ConfigurationManager.AppSettings["imgUrlServer"].ToString();

            //注：如果以上都处理成功，返回json，处理失败，返回""
            context.Response.Clear();
            context.Response.Write(imgUrl);
            context.Response.End();
        }
        /// <summary>
        /// 获取首页公告
        /// </summary>
        public void loadnotice(HttpContext context)
        {
            string outmsg = "{\"status\":0,\"msg\":\"没有分类！\"}";
            int id = _Request.GetInt("id");
            BLL.article bll = new BLL.article();
            DataTable dt = bll.GetList(3, "category_id  =" + id + " ", "sort_id asc,id asc").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                outmsg = CreateJsonParameters(dt);
            }
            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();

        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        #region Model与JSON相互转化========================================

        /// <summary>
        /// 将dataTable转换成json字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string CreateJsonParameters(DataTable dt)
        {
            StringBuilder JsonString = new StringBuilder();

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

        // using System.Runtime.Serialization.Json;
        public static T parse<T>(string jsonString)
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                return (T)new DataContractJsonSerializer(typeof(T)).ReadObject(ms);
            }
        }

        /// <summary>
        /// Model与JSON相互转化
        /// </summary>
        /// <param name="jsonObject"></param>
        /// <returns></returns>
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
    }
}