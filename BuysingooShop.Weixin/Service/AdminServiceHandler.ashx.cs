using BuysingooShop.Weixin.AdminService;
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

namespace BuysingooShop.Weixin.Service
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
                case "GetUserInfoByLogin":
                    GetUserInfoByLogin(context);        //用户登录 返回用户信息
                    break;

                case "GetUserInfoByUserName":
                    GetUserInfoByUserName(context);     //直接通过用户名 返回用户信息
                    break;

                case "submit_comment":
                    submit_comment(context);        //用户评价
                    break;
                case "submit_complian":
                    submit_complian(context);       //用户投诉
                    break;
                case "GetParameter":
                    GetParameter(context);          //获取加入我们数据
                    break;
                case "GetUserRegedit":
                    GetUserRegedit(context);        //用户注册 返回用户信息
                    break;
                case "GetMsgcode":
                    GetMsgcode(context);            //获取手机验证码
                    break;
                case "updatePassword":
                    updatePassword(context);        //找回密码
                    break;
                case "UpdateUserinfo":
                    UpdateUserinfo(context);        //修改用户信息
                    break;
                case "addUsername":
                    addUsername(context);           //修改用户名
                    break;
                case "order_cancel":
                    order_cancel(context);          //用户取消订单
                    break;
                case "order_complete":
                    order_complete(context);        //确认收货
                    break;
                case "GetUserInfo":
                    GetUserInfo(context);           //获取用户信息
                    break;

                case "GetUserList":
                    GetUserList(context);               //获取用户的虚拟用户
                    break;

                case "GetUserListByPage":
                    GetUserListByPage(context);         //获取用户的虚拟用户 分页处理
                    break;

                case "GetUserListByTop":
                    GetUserListByTop(context);         //积分排行榜
                    break;


                case "setpricesession":
                    setpricesession(context);           //设置订单金额session
                    break;
                case "getOpenarea":
                    getOpenarea(context);               //获取开放城市
                    break;
                case "validate_str_code":
                    validate_str_code(context);         //验证优惠券
                    break;
                case "GetMobileMsgcode":
                    GetMobileMsgcode(context);          //用户注册 获取手机验证码
                    break;
                case "setMsgcode":
                    setMsgcode(context);                //发送验证码
                    break;

                case "setMsgcodeByWithdraw":
                    setMsgcodeByWithdraw(context);      //发送提现安全码
                    break;

                case "userLogin":
                    userLogin(context);                 //用户登录
                    break;
                case "GetAddress1":
                    GetAddress1(context);               //获取地址
                    break;
                case "UpdateAddress":
                    UpdateAddress(context);             //修改地址
                    break;
                case "getrefundorder":
                    getrefundorder(context);        //获取退换货订单
                    break;
                case "Getrefundinfo":
                    Getrefundinfo(context);         //获取退换单详情
                    break;
                case "GetUpdateAddress":
                    GetUpdateAddress(context);      //获取修改地址
                    break;
                case "DeleteAddress":
                    DeleteAddress(context);         //删除地址
                    break;
                case "DefaultAddress":
                    DefaultAddress(context);        //默认地址
                    break;
                case "submitorder":
                    submitorder(context);           //提交提单
                    break;
                case "submitorders":
                    submitorders(context);          //提交提单(应付金额为零)
                    break;
                case "submit_withdraw":
                    submit_withdraw(context);       //提现
                    break;
                case "GetContentus":
                    GetContentus(context);          //取联系我们的数据
                    break;
                case "Getcategory":
                    Getcategory(context);           //获取分类
                    break;
                case "GetCompany":
                    GetCompany(context);            //获取公司介绍的数据
                    break;
                case "GetGoodsInfos":
                    GetGoodsInfos(context);         //获取商品详细信息
                    break;
                case "GetorderInfo":
                    GetorderInfo(context);          //获取订单信息
                    break;
                case "submitpay":
                    submitpay(context);             //支付成功修改订单状态
                    break;
                case "GetAddress":
                    GetAddress(context);            //获取收货地址
                    break;
                case "Addcart":
                    Addcart(context);               //加入购物车
                    break;
                case "GetConfigUrl":
                    GetConfigUrl(context);          //获取URL地址
                    break;
                case "getGoodsImg":
                    getGoodsImg(context);           //商品展示图片
                    break;
                case "GetGoodsomment":
                    GetGoodsomment(context);        //获取商品评论
                    break;
                case "getGoodslist":
                    getGoodslist(context);          //商品列表
                    break;
                case "getGoodslistall":
                    getGoodslistall(context);       //获取所有列表
                    break;

                case "getvideolist":
                    getvideolist(context);          //所有视频列表
                    break;

                case "getgoodstype":
                    getgoodstype(context);          //获取商品分类
                    break;

                case "getbannergoods":
                    getbannergoods(context);        //获取广告商品
                    break;
                case "getbanner":
                    getbanner(context);             //获取广告
                    break;
                case "GetLongDistance":
                    GetLongDistance(context);       //获取两地之间距离
                    break;
                case "addAddress":
                    addAddress(context);            //添加收货地址
                    break;
                case "getorders":
                    getorders(context);             //获取用户所有订单（除退换货）
                    break;
                case "getorder":
                    getorder(context);              //获取订单详细信息
                    break;
                case "getorderamount":
                    getorderamount(context);        //获取订单金额（保存订单金额）
                    break;
                case "getgoodsorder":
                    getgoodsorder(context);         //获取订单商品信息
                    break;
                case "GetCollect":
                    GetCollect(context);            //获取收藏列表
                    break;
                case "GetSelectAddress":
                    GetSelectAddress(context);      //获取选中地址
                    break;
                case "GetExpress":
                    GetExpress(context);            //获取快递信息
                    break;
                case "Getoutlet":
                    Getoutlet(context);             //获取店铺
                    break;
                case "GetoutletDetails":
                    GetoutletDetails(context);      //获取店铺详情
                    break;
                case "Getoutletlist":
                    Getoutletlist(context);         //获取店铺列表
                    break;
                case "Getoutletlist1":
                    Getoutletlist1(context);        //获取店铺列表
                    break;
                case "test":
                    test(context);                  //测试
                    break;

                case "GetBalanceTotalDay":
                    GetBalanceTotalDay(context);                //奖池总金额
                    break;

                case "GetUserBalanceList":
                    GetUserBalanceList(context);                //奖金收入明细
                    break;

                case "GetUserBalanceListByShare":
                    GetUserBalanceListByShare(context);         //我分享的客户
                    break;

                case "GetUserBalanceListByShareByPage":
                    GetUserBalanceListByShareByPage(context);   //我分享的客户 分页查询
                    break;

                case "GetUserListByShare":
                    GetUserListByShare(context);                //我分享的客户
                    break;

                case "getMessage":
                    getMessage(context);                        //通告
                    break;

                case "GetTop3":
                    GetTop3(context);                           //排行榜
                    break;

                case "GetUserInfo_Tuandui":
                    GetUserInfo_Tuandui(context);                //我的团队总人数，总下线人数
                    break;
                case "getnewslist":
                    getnewslist(context);                //获取新闻列表
                    break;
                case "joininfo":
                    joininfo(context);                //获取加盟信息
                    break;
                case "getnewspage":
                    getnewspage(context);                //获取新闻详细
                    break;
                case "searchpro":
                    searchpro(context);                //获取搜索产品
                    break;
                case "GetGoodsInfos2":
                    GetGoodsInfos2(context);         //获取带规格商品
                    break;

                case "getgoodstype2":
                    getgoodstype2(context);         //获取分类 不通过AdminServiceSoapClient
                    break;
                case "getGoodslist2":
                    getGoodslist2(context);          //商品列表2
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
                case "UpdateUserinfo2":
                    UpdateUserinfo2(context);        //修改用户信息2头像等信息
                    break;
                case "team_total":
                    team_total(context);        //团队销售总额
                    break;
                case "get_TeamMonth":
                    get_TeamMonth(context);        //按月份团队销售总额
                    break;
                case "getWithdraw":
                    getWithdraw(context);        //提现记录
                    break;
                case "Getuserpoint_log":
                    Getuserpoint_log(context);        //提现记录
                    break;
                case "GetTeamTotalByMonthDetails":
                    GetTeamTotalByMonthDetails(context);        //详细消费记录按月份排列
                    break;


                case "updateUser":
                    updateUser(context);                //更新用户信息
                    break;
                case "updateUserCallBack":
                    updateUserCallBack(context);        //支付成功，回调信息
                    break;


                    

            }
        }
        /// <summary>
        /// 团队销售总额
        /// </summary>
        public void GetTeamTotalByMonthDetails(HttpContext context)
        {
            string outmsg = "{\"status\":0,\"msg\":\"暂无信息！\"}";
            int uid = _Request.GetInt("user_id");
            string month = _Request.GetString("month");
            string year = _Request.GetString("year");
            BLL.orders bll = new BLL.orders();
            DataTable dt = bll.GetTeamTotalByMonthDetails(month, year, uid).Tables[0];
            if (dt.Rows.Count > 0 && dt != null)
            {
                outmsg = CreateJsonParameters(dt);
            }
            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }
        /// <summary>
        /// 团队销售总额
        /// </summary>
        public void Getuserpoint_log(HttpContext context)
        {
            string outmsg = "{\"status\":0,\"msg\":\"暂无信息！\"}";
            int uid = _Request.GetInt("user_id");
            BLL.user_point_log bll = new BLL.user_point_log();
            DataTable dt = bll.GetList(0, "user_id=" + uid + "", "id desc").Tables[0];
            if (dt.Rows.Count > 0 && dt != null)
            {
                outmsg = CreateJsonParameters(dt);
            }
            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }


         /// <summary>
        /// 团队销售总额
        /// </summary>
        public void getWithdraw(HttpContext context)
        {
            string outmsg = "{\"status\":0,\"msg\":\"暂无信息！\"}";
            int uid = _Request.GetInt("user_id");
            BLL.withdraw bll = new BLL.withdraw();
            DataTable dt = bll.GetLists(0,"user_id="+uid+"","id desc").Tables[0];
            if (dt.Rows.Count>0&&dt!=null)
            {
                outmsg = CreateJsonParameters(dt);
            }
            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }
        /// <summary>
        /// 团队销售总额
        /// </summary>
        public void get_TeamMonth(HttpContext context)
        {
            string outmsg = "0";
            int uid = _Request.GetInt("user_id");
            BLL.orders bll = new BLL.orders();
            DataTable dt = bll.GetTeamTotalByMonth(uid).Tables[0];
            outmsg = CreateJsonParameters(dt);
            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
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
        /// 修改用户信息2 头像等信息
        /// </summary>
        /// <param name="context"></param>
        public void UpdateUserinfo2(HttpContext context)
        {
            string outmsg = "{\"status\":0,\"msg\":\"修改失败！\"}";
            int user_id = _Request.GetInt("user_id");
            string nick_name = _Request.GetString("nick_name");
            string real_name = _Request.GetString("real_name");
            string sex = _Request.GetString("sex");
            string telphone = _Request.GetString("telphone");
            string province = _Request.GetString("province");//省
            string city = _Request.GetString("city");//市
            string Area = _Request.GetString("Area", "");//市
            string detail_adress = _Request.GetString("detail_adress");//市
            string password = _Request.GetString("password", "");
            string oldPassword = _Request.GetString("oldPassword", "");


            HttpPostedFile file = context.Request.Files[0];
            string exrension = Path.GetExtension(file.FileName);//上传文件扩展名
            Random r = new Random();
            BLL.users bll = new BLL.users();
            Model.users model = bll.GetModel(user_id);
            model.nick_name = nick_name;
            model.real_name = real_name;
            model.sex = sex;
            model.telphone = telphone;
            model.Provinces = province;
            model.City = city;
            model.Area = Area;//用户所在区域
            model.address  = detail_adress;//详细地址
            if (oldPassword != password && password != "")
            {
                model.salt = Vincent._DTcms.Utils.GetCheckCode(6); //获得6位的salt加密字符串
                model.password = _DESEncrypt.Encrypt(password, model.salt);
            }

            if (exrension == "")//用户没有上传头像
            {
                if (bll.Update(model))
              outmsg = "{\"status\":1,\"msg\":\"修改成功！\"}";             
            }
            else//上传了头像
            {
                if (exrension.ToLower() == ".jpg" || exrension.ToLower() == ".jpeg" || exrension.ToLower() == ".gif" || exrension.ToLower() == ".png")
                {
                    if (file.ContentLength <= 1024 * 1024 *2)//图片大小
                    {
                        string newname = DateTime.Now.ToString("yyyyMMddhhmmss") + r.Next(1000, 10000) + exrension;
                        model.avatar = "../upload/" + newname;
                        if (bll.Update(model))
                        {
                            file.SaveAs(context.Server.MapPath("~/upload/" + newname));
                            outmsg = "{\"status\":1,\"msg\":\"修改成功！\"}";            
                        }
                    }
                }
            }
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

            string article_id = _Request.GetString("goodData");  
            BLL.article art = new BLL.article();
            string[] strArr = Vincent._DTcms.Utils.DelLastChar(article_id, "&").Split('&');
            decimal money = 0;
            foreach (var item in strArr)
            {
                string[] strArr2 = item.Split('&');
                var channel_id = art.GetModel(int.Parse(strArr2[0].ToString())).channel_id;


                if (channel_id ==7)                                      //积分商城 == 7
                {
                    money += Convert.ToDecimal(art.GetModel(int.Parse(strArr2[0].ToString())).sell_price);
                    outmsg = "{\"status\": 1, \"money\": \"" + money + "\"}";
                }
                else if (channel_id==2)
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
        /// 用户登录 返回用户信息
        /// </summary>
        public void GetUserInfoByLogin(HttpContext context)
        {
            string outmsg = "n";
            var mobile = _Request.GetString("phone", "");
            var pwd = _Request.GetString("pwd", "");

            mobile = mobile.Trim();
            if (mobile.Length > 11)
            {
                _Log.SaveMessage("登录失败：" + mobile + "/" + pwd + "/" + mobile);
                context.Response.Clear();
                context.Response.Write(outmsg);
                context.Response.End();
            }

            //string userPwd1 = Vincent._MD5Encrypt.GetMD5(pwd.Trim());

            string strJson = SoapClient.GetUserInfoByLogin(mobile, pwd);
            if (!string.IsNullOrEmpty(strJson))
            {
                outmsg = strJson;
                _Log.SaveMessage("登录成功：" + mobile + "/" + pwd + "/" + mobile);
            }
            else
            {
                _Log.SaveMessage("登录失败：" + mobile + "/" + pwd + "/" + mobile);
            }

            //注：如果以上都处理成功，返回json，处理失败，返回""
            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }

        /// <summary>
        /// 直接通过用户名 返回用户信息
        /// </summary>
        public void GetUserInfoByUserName(HttpContext context)
        {
            string outmsg = "";
            var user_name = _Request.GetString("user_name", "");

            BLL.users bll = new BLL.users();
            Model.users user = bll.GetModelByName(user_name);
            outmsg = ObjectToJSON(user);

            Vincent._Log.SaveMessage("根据用户名获取用户信息：" + outmsg);

            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }

        /// <summary>
        /// 用户评价
        /// </summary>
        /// <param name="context"></param>
        public void submit_comment(HttpContext context)
        {
            var order_id = _Request.GetInt("order_id");
            var comment = _Request.GetString("comment", "");
            var user_id = new BLL.orders().GetModel(order_id).user_id;
            Model.users userm = new BLL.users().GetModel(user_id);

            BLL.article_comment bll = new BLL.article_comment();
            Model.article_comment model = new Model.article_comment();

            DataTable dt = new BLL.order_goods().GetList(" order_id=" + order_id).Tables[0];

            int k = 0;

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    //检查该文章是否存在
                    Model.article artModel = new BLL.article().GetModel(int.Parse(item["goods_id"].ToString()));
                    if (artModel == null)
                    {
                        context.Response.Write("{\"status\": 0, \"msg\": \"对不起，商品不存在或已删除！\"}");
                        return;
                    }
                    model.user_id = userm.id;
                    model.user_name = userm.user_name;
                    model.channel_id = artModel.channel_id;
                    model.article_id = artModel.id;
                    model.order_id = order_id;
                    model.content = Vincent._DTcms.Utils.ToHtml(comment);
                    model.user_ip = Vincent._DTcms.DTRequest.GetIP();
                    model.is_lock = 0; //审核开关
                    model.add_time = DateTime.Now;
                    model.is_reply = 0;
                    model.comment_type = 0;

                    if (bll.Add(model) < 1)
                    {
                        k = 1;
                    }
                }
            }

            if (k == 1)
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"对不起，保存过程中发生错误！\"}");
                return;
            }
            context.Response.Write("{\"status\": 1, \"msg\": \"恭喜您，评论提交成功啦！\"}");
            return;
        }

        public void submit_complian(HttpContext context)
        {
            string outmsg = "{\"status\": 0, \"msg\": \"提交失败！\"}";
            int user_id = Vincent._DTcms.DTRequest.GetQueryInt("user_id", 0);
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
        /// 获取加入我们数据
        /// </summary>
        private void GetParameter(HttpContext context)
        {
            int id = Vincent._DTcms.DTRequest.GetQueryInt("id");
            Model.article dt = new BLL.article().GetModel(id);
            context.Response.Write(ObjectToJSON(dt));
            return;
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        public void GetUserRegedit(HttpContext context)
        {
            string outmsg = "n";
            string mobile = _Request.GetString("txtMobile", "");
            string realname = _Request.GetString("realname", "");

            string msgcode = _Request.GetString("txtMsgcode", "");
            string password = _Request.GetString("password", "");

            //int marketId = _Request.GetInt("marketId", 2);
            //int organizeId = _Request.GetInt("organizeId", 0);
            //int preId = _Request.GetInt("preId", 0); 

            //id_marketIId_organizeId   12_2_224
            string preIdStr = _Request.GetString("preId", "");
            string[] preIdList = preIdStr.Split('_');

            int preId = _Convert.ToInt(preIdList[0], 0);
            int marketId = _Convert.ToInt(preIdList[1], 2);
            int organizeId = _Convert.ToInt(preIdList[2], 0);


            // 判断是否 开放二维码注册 1开启二维码注册(必须是扫码进来的) 0关闭二维码注册 
            string IsOpenRegist = Vincent._WebConfig.GetAppSettingsString("IsOpenRegist");
            if (IsOpenRegist == "1")
            {
                if (preId <= 0)
                {
                    outmsg = "{\"status\":\"n\",\"info\":\"游客用户,注册失败\"}";

                    context.Response.Clear();
                    context.Response.Write(outmsg);
                    context.Response.End();

                    return;
                }
            }

            string jsondata = SoapClient.GetUserRegedit2(mobile, msgcode, password, marketId, organizeId, preId, realname);
            if (!string.IsNullOrEmpty(jsondata))
            {
                if (!jsondata.Contains("已被注册"))
                {
                    outmsg = "{\"status\":\"y\",\"info\":" + jsondata + "}";
                }

                _Log.SaveMessage("注册成功：" + mobile + "/" + password + "/" + mobile + "/" + marketId + "/" + organizeId + "/" + preId);
            }
            else
            {
                _Log.SaveMessage("注册失败：" + mobile + "/" + password + "/" + mobile + "/" + marketId + "/" + organizeId + "/" + preId);
            }

            //注：如果以上都处理成功，返回json，处理失败，返回""
            context.Response.Clear();
            context.Response.Write(outmsg);
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
                context.Response.Write("{\"status\":0, \"msg\":\"该手机号不存在！\"}");
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

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="context"></param>
        public void UpdateUserinfo(HttpContext context)
        {
            string outmsg = "{\"status\":0,\"msg\":\"修改失败！\"}";
            int user_id = _Request.GetInt("user_id");
            string nick_name = _Request.GetString("nick_name");
            string sex = _Request.GetString("sex");

            string oldPassword = _Request.GetString("oldPassword", "");
            string password = _Request.GetString("password", "");

            DateTime birthday = Vincent._Convert.ToDateTime(_Request.GetString("birthday"), DateTime.Now);

            BLL.users bll = new BLL.users();
            Model.users model = bll.GetModel(user_id);


            if (model != null)
            {
                model.nick_name = nick_name;
                model.sex = sex;
                model.birthday = birthday;

                if (oldPassword != password && password != "")
                {
                    model.salt = Vincent._DTcms.Utils.GetCheckCode(6); //获得6位的salt加密字符串
                    model.password = _DESEncrypt.Encrypt(password, model.salt);
                }

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
        /// 修改用户名
        /// </summary>
        /// <param name="context"></param>
        public void addUsername(HttpContext context)
        {
            string outmsg = "{\"status\":0,\"msg\":\"修改失败！\"}";
            string mobile = _Request.GetString("mobile", "");
            string nick_name = _Request.GetString("nick_name");
            string avatar = _Request.GetString("avatar");

            BLL.users bll = new BLL.users();
            Model.users model = bll.GetModelMobile(mobile);
            if (model != null)
            {
                model.user_name = nick_name;
                model.avatar = avatar;
                if (bll.Update(model))
                {
                    outmsg = "{\"status\":1,\"msg\":\"修改成功！\"}";
                }
            }

            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }

        #region 用户取消订单OK=================================
        private void order_cancel(HttpContext context)
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
        /// 获取用户信息
        /// </summary>
        public void GetUserInfo(HttpContext context)
        {
            string outmsg = "";
            int user_id = _Request.GetInt("user_id");

            BLL.users bll = new BLL.users();
            Model.users user = bll.GetModel(user_id);

            if (user.user_name.Length != 11)
            {
                outmsg = null;

                context.Response.Clear();
                context.Response.Write(outmsg);
                context.Response.End();
            }
            else
            {
                outmsg = ObjectToJSON(user);

                context.Response.Clear();
                context.Response.Write(outmsg);
                context.Response.End();
            }


        }

        /// <summary>
        /// 获取用户的所有虚拟用户
        /// </summary>
        /// <param name="context"></param>
        public void GetUserList(HttpContext context)
        {
            string outmsg = "";
            int user_id = _Request.GetInt("user_id");

            BLL.users bll = new BLL.users();
            var userList = bll.GetList(100, "isDelete = 0 and [user_name] like (select [user_name] from dt_users where id = " + user_id + " ) + '%'", "id");
            if (userList != null && userList.Tables.Count > 0)
                outmsg = CreateJsonParameters(userList.Tables[0]);

            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }

        /// <summary>
        /// 获取用户的所有虚拟用户 分页处理
        /// </summary>
        /// <param name="context"></param>
        public void GetUserListByPage(HttpContext context)
        {
            string outmsg = "";
            int user_id = _Request.GetInt("user_id");
            int pageIndex = _Request.GetInt("pageIndex");
            int pageSize = _Request.GetInt("pageSize");

            var recordCount = 0;        //总记录数
            int pageall = 0;            //总页数

            BLL.users bll = new BLL.users();
            //var userList = bll.GetList(100, "isDelete = 0 and [user_name] like (select [user_name] from dt_users where id = " + user_id + " ) + '%'", "id");

            var userList = bll.GetList(pageSize, pageIndex, "isDelete = 0 and [user_name] like (select [user_name] from dt_users where id = " + user_id + " ) + '%'", "id", out recordCount);

            DataTable dt = userList.Tables[0];

            pageall = _Utility.GetPageAll(pageSize, recordCount);
            _Json json = new _Json(dt);
            outmsg = "{page:{pageindex:'" + pageIndex + "',pagesize:'" + pageSize + "',recordtotal:'" + recordCount + "',pageall:'" + pageall + "'},record:" + json.ToJson() + "}";

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
        /// 设置订单金额session
        /// </summary>
        /// <param name="context"></param>
        private void setpricesession(HttpContext context)
        {
            var price = _Request.GetString("price");
            context.Session["price"] = price;
            context.Response.Write("{\"status\":1, \"msg\":\"保存成功！\"}");
            return;
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
        /// 验证优惠券
        /// </summary>
        public void validate_str_code(HttpContext context)
        {
            string outmsg = "";
            string coupon_no = _Request.GetString("coupon_no");

            Model.user_coupon model = new Model.user_coupon();
            model = new BLL.user_coupon().GetModel(" str_code='" + coupon_no + "'");

            if (model == null)
            {
                context.Response.Write("{\"status\":0,\"msg\":\"优惠券编码输入有误！\"}");
                return;
            }

            context.Response.Clear();
            context.Response.Write("{\"status\":1,\"msg\":\"成功！\",\"str_amount\":" + model.amount + "}");
            context.Response.End();
        }

        /// <summary>
        /// 获取手机短信验证码
        /// </summary>
        public void GetMobileMsgcode(HttpContext context)
        {
            string mobile = _Request.GetString("mobile", "");
            string outmsg = SoapClient.SendMsgCode(mobile);

            //注：如果以上都处理成功，返回json，处理失败，返回""
            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }

        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="context"></param>
        public void setMsgcode(HttpContext context)
        {
            string mobile = _Request.GetString("mobile", "");
            BLL.users bll = new BLL.users();
            string outresult = "";
            if (string.IsNullOrEmpty(mobile))
            {
                outresult = "{\"status\":\"n\",\"info\":\"手机号不能为空\"}";
                context.Response.Clear();
                context.Response.Write(outresult);
                context.Response.End();
            }
            if (bll.ExistsMobile(mobile))
            {
                outresult = "{\"status\":\"n\",\"info\":\"手机号已被注册\"}";
                context.Response.Clear();
                context.Response.Write(outresult);
                context.Response.End();
            }

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
        /// 发送提现安全码
        /// </summary>
        /// <param name="context"></param>
        public void setMsgcodeByWithdraw(HttpContext context)
        {
            string mobile = _Request.GetString("mobile", "");
            BLL.users bll = new BLL.users();
            string outresult = "";
            if (string.IsNullOrEmpty(mobile))
            {
                outresult = "{\"status\":\"n\",\"info\":\"手机号不能为空\"}";
                context.Response.Clear();
                context.Response.Write(outresult);
                context.Response.End();
            }
            if (!bll.ExistsMobile(mobile))
            {
                outresult = "{\"status\":\"n\",\"info\":\"不是系统内用户\"}";
                context.Response.Clear();
                context.Response.Write(outresult);
                context.Response.End();
            }

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

            //写短信数据，发SMS
            //var message_name = _Utility.GetConfigAppSetting("message_name");
            //var message_pwd = _Utility.GetConfigAppSetting("message_pwd");
            //var message_content = _Utility.GetConfigAppSetting("message_content");
            //message_content = message_content.Replace("num", msgcode.ToString());

            //_Message clientMessage = _Message.getInstance(message_name, message_pwd);
            //string resultMT = clientMessage.MT(message_content, mobile, "", "");
            //var messageNum = _Convert.ToInt64(resultMT, 0);
            ////Model.userconfig userConfig = new BLL.userconfig().loadConfig();

            context.Response.Clear();
            context.Response.Write(outresult);
            context.Response.End();
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="context"></param>
        public void userLogin(HttpContext context)
        {
            string outmsg = "{\"status\":0, \"msg\":\"登录失败！\"}";
            string mobile = _Request.GetString("phone", "");

            string name = _Request.GetString("name", "");
            string address = _Request.GetString("address", "");

            BLL.users bll = new BLL.users();
            Model.users model = bll.GetModelMobile(mobile, name, address);
            if (model != null)
            {
                outmsg = ObjectToJSON(model);
            }

            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        public string setregist(string mobile)
        {
            string outresult = "{\"status\":\"y\",\"info\":\"恭喜你，注册成功\"}";
            BLL.users bll = new BLL.users();
            Model.users model = new Model.users();

            if (mobile == "")
            {
                outresult = "{\"status\":\"n\",\"info\":\"用户名不能为空\"}";
                return outresult;
            }
            //if (bll.Exists(username.Trim()))
            //{
            //    outresult = "{\"status\":\"n\",\"info\":\"该用户名已被注册\"}";
            //    return outresult;
            //}
            //保存注册信息
            model.group_id = 1;//普通用户注册
            model.mobile = mobile;
            model.salt = Vincent._DTcms.Utils.GetCheckCode(6); //获得6位的salt加密字符串
            //model.password = _DESEncrypt.Encrypt(password, model.salt);
            model.reg_time = DateTime.Now;
            model.strcode = Vincent._DTcms.Utils.GetCheckCode(20);//生成随机码
            model.status = 0; //正常
            model.isMobile = 1;
            model.password = "";
            Random ro = new Random();
            var no = ro.Next(1000, 9999); //随机一个数

            model.user_name = "jd_" + no.ToString();

            int newId = bll.Add(model);
            if (newId < 1)
            {
                outresult = "{\"status\":\"n\",\"info\":\"系统故障，请联系网站管理员！\"}";
                return outresult;
            }
            model = bll.GetModel(newId);
            if (model != null)
            {

                //防止Session提前过期
                //Vincent._DTcms.Utils.WriteCookie(_DTKeys.COOKIE_USER_NAME_REMEMBER, "SimpleLife", model.user_name);
                //Vincent._DTcms.Utils.WriteCookie(_DTKeys.COOKIE_USER_PWD_REMEMBER, "SimpleLife", model.password);

                //写入登录日志
                new BLL.user_login_log().Add(model.id, model.user_name, "会员登录");
                return ObjectToJSON(model);
            }
            else
            {
                outresult = "{\"status\":0, \"msg\":\"注册失败！\"}";
                return outresult;
            }
        }

        /// <summary>
        /// 获取地址
        /// </summary>
        /// <param name="context"></param>
        public void GetAddress1(HttpContext context)
        {
            string outmsg = "{\"info\":\"你还没有地址，请先添加地址！\", \"status\":0}";
            string userid = _Request.GetString("userid", "");
            outmsg = SoapClient.GetAddress1(userid);
            if (outmsg == null || outmsg == "")
            {
                outmsg = "{\"info\":\"你还没有地址，请先添加地址！\", \"status\":0}";
            }
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
        /// 获取退换货订单
        /// </summary>
        /// <param name="context"></param>
        public void getrefundorder(HttpContext context)
        {
            string outmsg = "{\"info\":\"没有订单！\", \"status\":0}";

            int userid = _Request.GetInt("userid");

            BLL.refund bll = new BLL.refund();
            DataTable dt = bll.GetRefundList1(0, " t1.user_id=" + userid, " t3.id desc").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                outmsg = CreateJsonParameters(dt);
            }

            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();

        }

        /// <summary>
        /// 获取退货单详情
        /// </summary>
        /// <param name="context"></param>
        public void Getrefundinfo(HttpContext context)
        {
            string outmsg = "NO";

            int refund_id = _Request.GetInt("refund_id");

            BLL.refund bll = new BLL.refund();
            DataTable dt = bll.GetList(0, " id=" + refund_id, " id desc").Tables[0];
            if (dt != null)
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
            string point = _Request.GetString("point", "");
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

                model.store_name = store_name;
                model.store_address = store_address;
                model.store_id = Vincent._Convert.ToInt(store_id, 1);

                model.bill_type = int.Parse(bill_type);
                if (int.Parse(bill_type) != 0)
                {
                    model.is_bill = 1;
                }
                model.invoice_rise = bill_rise;
                model.down_order = down_order;
                model.remark = remark;
                model.point = int.Parse(point);
                if (schoolData!="undefined")
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
                model.store_id = Vincent._Convert.ToInt(store_id, 1);
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
        /// 取联系我们的数据
        /// </summary>
        public void GetContentus(HttpContext context)
        {
            string outmsg = SoapClient.GetContentus();


            //注：如果以上都处理成功，返回json，处理失败，返回""
            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }

        /// <summary>
        /// 获取分类
        /// </summary>
        /// <param name="context"></param>
        public void Getcategory(HttpContext context)
        {
            string outmsg = "{\"status\":0,\"msg\":\"没有分类！\"}";

            int parent_id = _Request.GetInt("parent_id");

            BLL.article_category bll = new BLL.article_category();
            DataTable dt = bll.GetList(parent_id, 2);
            if (dt != null && dt.Rows.Count > 0)
            {
                outmsg = CreateJsonParameters(dt);
            }

            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }

        /// <summary>
        /// 取公司介绍的数据
        /// </summary>
        public void GetCompany(HttpContext context)
        {
            string outmsg = SoapClient.GetCompany();
            //注：如果以上都处理成功，返回json，处理失败，返回""
            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }

        /// <summary>
        /// 获取商品详细信息
        /// </summary>
        public void GetGoodsInfos(HttpContext context)
        {
            int goodsId = _Request.GetInt("goodsId");

            string outmsg = SoapClient.GetGoodsInfos(goodsId);
            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }
        /// <summary>
        /// 获取带规格的商品
        /// </summary>
        public void GetGoodsInfos2(HttpContext context)
        {
            int goodsId = _Request.GetInt("goodsId");//类别ID 
            string outmsg = "{\"status\":0,\"msg\":\"暂无产品！\"}";//有规格标识 is_red=1
            var getsearchpro = new BuysingooShop.BLL.article().GetList1(100, "category_id=(select category_id from dt_article  where id=" + goodsId + "   ) ", "sort_id asc");
            outmsg = CreateJsonParameters(getsearchpro.Tables[0]);
            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }



        /// <summary>
        /// 获取订单信息
        /// </summary>
        public void GetorderInfo(HttpContext context)
        {
            int orderid = _Request.GetInt("orderid");

            string outmsg = SoapClient.getorder(orderid);
            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }

        public void submitpay(HttpContext context)
        {
            var out_trade_no = _Request.GetString("orderid");
            string outmsg = "";

            Vincent._Log.SaveMessage("付款成功,开始计算奖金,orderid=" + out_trade_no);

            if (out_trade_no.Contains("_"))
            {
                string[] tradeList = out_trade_no.Split('_');

                int id = Vincent._Convert.ToInt(tradeList[0], 0);

                // 计算奖金
                if (id > 0)
                {
                    var outId = BuysingooShop.BLL.OrdersBLL.p_update_users(id);

                    outmsg = "OK";
                }
            }


            //string outmsg = SoapClient.submitpay(orderid);
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

            //string outmsg = SoapClient.GetAddress(userid);

            DataTable dt = new BLL.user_address().GetList(" user_id=" + userid).Tables[0];
            outmsg = CreateJsonParameters(dt);
            if (outmsg == null || outmsg == "")
            {
                outmsg = "{\"status\":0,\"msg\":\"您还没有添加任何地址！\"}";
            }

            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }

        /// <summary>
        /// 获取商品展示图片
        /// </summary>
        public void getGoodsImg(HttpContext context)
        {
            int goodsId = _Request.GetInt("goodsId");
            string outmsg = SoapClient.getGoodsImg(goodsId);
            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }

        /// <summary>
        /// 获取商品评论
        /// </summary>
        /// <param name="context"></param>
        public void GetGoodsomment(HttpContext context)
        {
            string outmsg = "{\"status\":0,\"msg\":\"该商品暂时没有评论！\"}";

            int goodsId = _Request.GetInt("goodsId");
            BLL.article_comment comment = new BLL.article_comment();
            DataTable dt = comment.GetList(0, " article_id=" + goodsId, " add_time desc").Tables[0];
            if (dt.Rows.Count > 0)
            {
                outmsg = CreateJsonParameters(dt);
            }

            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }

        /// <summary>
        /// 获取商品列表
        /// </summary>
        public void getGoodslist(HttpContext context)
        {
            string outmsg = "{\"status\":0,\"msg\":\"该分类还没有任何商品！\"}";
            int category_id = _Request.GetInt("category_id", 0);

            outmsg = SoapClient.getGoodsListByCategoryId(category_id);
            //byte[] cc = Convert.FromBase64String(outmsg);
            //string dd = Encoding.UTF8.GetString(cc);

            if (outmsg == null || outmsg == "")
            {
                outmsg = "{\"status\":0,\"msg\":\"该分类还没有任何商品！\"}";
            }

            outmsg = outmsg.Replace("<p>", "");
            outmsg = outmsg.Replace("</p>", "");

            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }
        /// <summary>
        /// 获取商品列表2
        /// </summary>
        public void getGoodslist2(HttpContext context)
        {
            string outmsg = "{\"status\":0,\"msg\":\"该分类还没有任何商品！\"}";
            int cid = _Request.GetInt("category_id", 0);//CID
            BLL.article art = new BLL.article();
            var info = art.GetList1(0, "category_id in (select  id FROM dt_article_category  where parent_id =" + cid + ") or category_id=" + cid + "", "id asc");

            outmsg = CreateJsonParameters(info.Tables[0]);

            if (outmsg == null || outmsg == "")
            {
                outmsg = "{\"status\":0,\"msg\":\"该分类还没有任何商品！\"}";
            }
            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }
        public void getGoodslistall(HttpContext context)
        {
            string outmsg = "{\"status\":0,\"msg\":\"还没有任何商品！\"}";

            BLL.article art = new BLL.article();
            var info = art.GetList1(100, " channel_id=2 and status=0", " sort_id desc");

            // Vincent._Json json = new _Json();

            //return Convert.ToBase64String(Encoding.UTF8.GetBytes(CreateJsonParameters(info.Tables[0])));
            outmsg = CreateJsonParameters(info.Tables[0]);
            if (outmsg == null || outmsg == "")
            {
                outmsg = "{\"status\":0,\"msg\":\"还没有任何商品！\"}";
            }
            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }

        public void getvideolist(HttpContext context)
        {
            string outmsg = "{\"status\":0,\"msg\":\"还没有任何视频！\"}";

            string category_id = Vincent._Request.GetString("category_id");

            BLL.article art = new BLL.article();
            var info = art.GetList1(0, " category_id=" + category_id + " and status=0", " sort_id desc");

            // Vincent._Json json = new _Json();

            //return Convert.ToBase64String(Encoding.UTF8.GetBytes(CreateJsonParameters(info.Tables[0])));
            outmsg = CreateJsonParameters(info.Tables[0]);
            if (outmsg == null || outmsg == "")
            {
                outmsg = "{\"status\":0,\"msg\":\"还没有任何视频！\"}";
            }
            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }

        /// <summary>
        /// 获取商品分类
        /// </summary>
        public void getgoodstype(HttpContext context)
        {
            string outmsg = "{\"status\":0,\"msg\":\"暂无商品分类！\"}";
            int channel_id = _Request.GetInt("channel_id", 0);

            outmsg = SoapClient.getGoodsTypelist(channel_id);
            //byte[] cc = Convert.FromBase64String(outmsg);
            //string dd = Encoding.UTF8.GetString(cc);

            if (outmsg == null || outmsg == "")
            {
                outmsg = "{\"status\":0,\"msg\":\"暂无商品分类！\"}";
            }
            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }
        /// <summary>
        /// 获取商品分类   未通过==》AdminServiceSoapClient
        /// </summary>
        public void getgoodstype2(HttpContext context)
        {
            string outmsg = "{\"status\":0,\"msg\":\"暂无商品分类！\"}";
            int channel_id = _Request.GetInt("channel_id");//类别ID 

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
        /// 获取广告商品列表
        /// </summary>
        /// <param name="context"></param>
        public void getbannergoods(HttpContext context)
        {
            string outmsg = "{\"status\":0,\"msg\":\"还没有任何商品！\"}";

            BLL.article art = new BLL.article();
            var info = art.GetList2(0, " channel_id=7 and status=0", " id desc");

            // Vincent._Json json = new _Json();

            //return Convert.ToBase64String(Encoding.UTF8.GetBytes(CreateJsonParameters(info.Tables[0])));
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
        /// 获取广告
        /// </summary>
        /// <param name="context"></param>
        public void getbanner(HttpContext context)
        {
            string outmsg = "{\"status\":0,\"msg\":\"还没有广告图片！\"}";

            var category_id = _Request.GetInt("category_id");

            BLL.article art = new BLL.article();
            var info = art.GetList2(10, " category_id=" + category_id + " and status=0", " id desc");

            // Vincent._Json json = new _Json();

            //return Convert.ToBase64String(Encoding.UTF8.GetBytes(CreateJsonParameters(info.Tables[0])));
            outmsg = CreateJsonParameters(info.Tables[0]);
            if (outmsg == null || outmsg == "")
            {
                outmsg = "{\"status\":0,\"msg\":\"还没有广告图片！\"}";
            }
            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }

        /// <summary>
        /// 获取两地之间距离
        /// </summary>
        /// <param name="context"></param>
        public void GetLongDistance(HttpContext context)
        {
            string outmsg = "{\"status\":0,\"msg\":\"获取失败！\"}";
            double longitude = double.Parse(_Request.GetString("longitude"));
            double latitude = double.Parse(_Request.GetString("latitude"));
            double longitudeend = double.Parse(_Request.GetString("longitudeend"));
            double latitudeend = double.Parse(_Request.GetString("latitudeend"));

            var dis = Vincent._Baidu.Map.GetLongDistance(longitude, latitude, longitudeend, latitudeend);
            outmsg = "{\"status\":1,\"msg\":\"" + dis + "\"}";

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
            //Vincent._Log.SaveMessage("执行到这");
            //string outmsg = SoapClient.addAddress(userid,name,province,city,area,street,zipcode,tel);
            //Vincent._Log.SaveMessage("执行到这"+outmsg);

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
        /// 获取订单商品信息
        /// </summary>
        public void getgoodsorder(HttpContext context)
        {
            int orderid = int.Parse(_Request.GetString("orderid"));

            string outmsg = SoapClient.getgoodsorder(orderid);
            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }

        /// <summary>
        /// 获取收藏列表
        /// </summary>
        public void GetCollect(HttpContext context)
        {
            string outmsg = "NO";
            int userid = int.Parse(_Request.GetString("userid"));

            BLL.collect collect = new BLL.collect();
            DataTable dt = collect.GetList(0, " user_id=" + userid, " id desc").Tables[0];
            if (dt.Rows.Count > 0)
            {
                outmsg = CreateJsonParameters(dt);
            }


            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }

        /// <summary>
        /// 获取选中地址
        /// </summary>
        public void GetSelectAddress(HttpContext context)
        {
            string outmsg = "{\"status\":0, \"msg\":\"参数有误！\"}";
            int addressid = int.Parse(_Request.GetString("addressid"));

            outmsg = SoapClient.GetSelectAddress(addressid);

            if (outmsg == null)
            {
                outmsg = "{\"status\":0, \"msg\":\"参数有误！\"}";
            }

            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }

        /// <summary>
        /// 获取快递信息
        /// </summary>
        public void GetExpress(HttpContext context)
        {

            string outmsg = SoapClient.GetExpress();
            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
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
        /// 加入购物车
        /// </summary>
        public void Addcart(HttpContext context)
        {
            //int goodsId = _Request.GetInt("goodsId");
            //string goodstype = _Request.GetString("goodstype");
            //if (goodsId == "" || goodsId == 0)
            //{
            //    context.Response.Write("{\"status\":0,\"msg\":\"你提交的商品参数有误！\"}");
            //    return;
            //}
            //if (goodstype == "" || goodstype == null)
            //{
            //    context.Response.Write("{\"status\":0,\"msg\":\"你提交的商品参数有误！\"}");
            //    return;
            //}
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
        /// 测试
        /// </summary>
        public void test(HttpContext context)
        {
            string json = "{\"status\":0,\"msg\":\"你提交的商品参数有误！\"}";

            var startDateTime = Vincent._Request.GetString("startDateTime");    //开始时间
            var endDateTime = Vincent._Request.GetString("endDateTime");        //结束时间
            var timeday_url = Vincent._WebConfig.GetAppSettingsString("timeday_url");

            context.Response.Clear();
            context.Response.Write(json);
            context.Response.End();
        }

        /// <summary>
        /// 奖池总金额
        /// </summary>
        /// <param name="context"></param>
        public void GetBalanceTotalDay(HttpContext context)
        {
            string outmsg = "";

            BLL.orders bll = new BLL.orders();
            outmsg = bll.GetBalanceTotalDay();
            var outmsgAll = bll.GetBalanceTotalAll();

            //<add key="goods_unit_price" value="288"/>     <!-- 商品单价 -->
            //<add key="a_money_unit_price" value="100"/>   <!-- 奖池A每份多少钱 -->
            //<add key="b_money_unit_price" value="50"/>    <!-- 奖池B每份多少钱 -->
            //<add key="c_money_unit_price" value="30"/>    <!-- 奖池C每份多少钱 -->

            var goods_unit_price = Vincent._Convert.ToFloat(Vincent._WebConfig.GetAppSettingsString("goods_unit_price"), 0);
            var a_money_unit_price = Vincent._Convert.ToFloat(Vincent._WebConfig.GetAppSettingsString("a_money_unit_price"), 0);
            var b_money_unit_price = Vincent._Convert.ToFloat(Vincent._WebConfig.GetAppSettingsString("b_money_unit_price"), 0);
            var c_money_unit_price = Vincent._Convert.ToFloat(Vincent._WebConfig.GetAppSettingsString("c_money_unit_price"), 0);

            var count = (Vincent._Convert.ToFloat(outmsg, 0) / goods_unit_price);

            var a_total = a_money_unit_price * count;
            var b_total = b_money_unit_price * count;
            var c_total = c_money_unit_price * count;
            var d_total = (Vincent._Convert.ToFloat(outmsgAll, 0) / goods_unit_price);

            StringBuilder JsonString = new StringBuilder();
            JsonString.Append("{\"a_total\": " + a_total + ", \"b_total\": " + b_total + ", \"c_total\":" + c_total + ", \"d_total\":" + d_total + "}");

            outmsg = JsonString.ToString();


            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }

        /// <summary>
        /// 奖金收入明细
        /// </summary>
        /// <param name="context"></param>
        public void GetUserBalanceList(HttpContext context)
        {
            string outmsg = "";

            var user_id = Vincent._Request.GetString("user_id", "0");
            var typeid = Vincent._Request.GetInt("typeid", 0);

            BLL.orders bll = new BLL.orders();
            //outmsg = bll.get
            int recordCount = 0;
            var balanceList = new DataSet();

            if (typeid == 0)
                balanceList = bll.GetUserBalanceList(200, 1, "userid='" + user_id + "' ", "id desc", out recordCount);
            else
                balanceList = bll.GetUserBalanceList(100, 1, "userid='" + user_id + "' and typeid = " + typeid, "id desc", out recordCount);

            if (balanceList != null && balanceList.Tables.Count > 0)
                outmsg = CreateJsonParameters(balanceList.Tables[0]);
            else
                outmsg = "{\"T_blog\":[]}";


            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }

        /// <summary>
        /// 我分享的客户
        /// </summary>
        /// <param name="context"></param>
        public void GetUserBalanceListByShare(HttpContext context)
        {
            string outmsg = "";

            var user_id = Vincent._Request.GetString("user_id", "0");

            BLL.users bll = new BLL.users();
            //outmsg = bll.get
            int recordCount = 0;
            var balanceList = bll.GetList(1000, 1, "isDelete=0 and preId=" + user_id + "", "id asc", out recordCount);

            if (balanceList != null && balanceList.Tables.Count > 0)
                outmsg = CreateJsonParameters(balanceList.Tables[0]);

            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
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

        public void GetUserBalanceListByShareByPage(HttpContext context)
        {
            string outmsg = "";
            int user_id = _Request.GetInt("user_id");
            int pageIndex = _Request.GetInt("pageIndex");
            int pageSize = _Request.GetInt("pageSize");

            string userName = _Request.GetString("userName");

            var recordCount = 0;        //总记录数
            int pageall = 0;            //总页数

            BLL.users bll = new BLL.users();
            //var userList = bll.GetList(100, "isDelete = 0 and [user_name] like (select [user_name] from dt_users where id = " + user_id + " ) + '%'", "id");

            //var userList = bll.GetList(pageSize, pageIndex, "isDelete=0 and preId=" + user_id + "", "id asc", out recordCount);

            var userList = bll.GetListByShare(pageSize, pageIndex, "id <> " + user_id + " and isBuwei = 0 and id in (select id from F_GetUserNetByShare(" + user_id + "))", "id asc", out recordCount);


            DataTable dt = userList.Tables[0];

            pageall = _Utility.GetPageAll(pageSize, recordCount);
            _Json json = new _Json(dt);
            outmsg = "{page:{pageindex:'" + pageIndex + "',pagesize:'" + pageSize + "',recordtotal:'" + recordCount + "',pageall:'" + pageall + "'},record:" + json.ToJson() + "}";

            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }

        public void getMessage(HttpContext context)
        {
            string outmsg = "{\"status\":0,\"msg\":\"暂无数据！\"}";

            string category_id = Vincent._Request.GetString("category_id");

            BLL.article art = new BLL.article();
            var info = art.GetList(1, " category_id=" + category_id + " and is_red=1", " sort_id desc");

            // Vincent._Json json = new _Json();

            //return Convert.ToBase64String(Encoding.UTF8.GetBytes(CreateJsonParameters(info.Tables[0])));
            outmsg = CreateJsonParameters(info.Tables[0]);
            if (outmsg == null || outmsg == "")
            {
                outmsg = "{\"status\":0,\"msg\":\"暂无数据！\"}";
            }
            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }

        public void GetTop3(HttpContext context)
        {
            string outmsg = "{\"status\":0,\"msg\":\"暂无数据！\"}";

            int topid = Vincent._Request.GetInt("topid", 3);

            var UserNameList = Vincent._WebConfig.GetAppSettingsString("UserNameList");

            BLL.users art = new BLL.users();
            var info = art.GetList(3, "user_name not in(" + UserNameList + ")", " share_total desc");

            // Vincent._Json json = new _Json();

            //return Convert.ToBase64String(Encoding.UTF8.GetBytes(CreateJsonParameters(info.Tables[0])));
            outmsg = CreateJsonParameters(info.Tables[0]);
            if (outmsg == null || outmsg == "")
            {
                outmsg = "{\"status\":0,\"msg\":\"暂无数据！\"}";
            }
            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }

        /// <summary>
        /// 我的团队总人数，总下线人数
        /// </summary>
        /// <param name="context"></param>
        public void GetUserInfo_Tuandui(HttpContext context)
        {
            string outmsg = "";

            var user_id = _Request.GetInt("user_id", 0);

            int recordCount = 0;        //总记录数

            BLL.users bll = new BLL.users();

            var balanceList = bll.GetUserInfo_Tuandui(user_id, out recordCount);

            if (recordCount > 1)
            {
                recordCount = recordCount - 1;
            }

            context.Response.Clear();
            context.Response.Write(recordCount);
            context.Response.End();
        }
        /// <summary>
        /// 获取新闻列表
        /// </summary>
        /// <param name="context"></param>
        private void getnewslist(HttpContext context)
        {
            Random r = new Random();

            string num = ((r.Next(1, 1000)) * 10).ToString();

            string urltime = Convert.ToString(DateTime.Now) + num;

            int id = _Request.GetInt("id");

            BLL.article bll = new BLL.article();

            DataTable dt = bll.GetListalbums(0, "  dt_article.category_id =" + id + " ", "dt_article.id asc").Tables[0];
            string html = "";
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow news in dt.Rows)
                {
                    html += "<li   style=\"marmargin-bottom: 0.5rem;\">   <a onclick=\"goToBalanceList('newspage.html?id=" + news["id"] + "&v=" + num + "')\"";

                    html += "  href='#'    class='item-link item-content'><div class='item-media'><img src=" + news["img_url"] + " style='width: 4rem;'></div>";
                    html += "   <div class='item-inner'><div class='item-title-row'><div class='item-title'>" + news["title"] + "</div>";
                    html += " </div><div class='item-text'>" + substr(news["zhaiyao"].ToString(), 20) + "</div></div></a></li>";
                }
                context.Response.Clear();
                context.Response.Write(html);
                context.Response.End();
            }
            else
            {
                context.Response.Clear();
                context.Response.Write("暂无新闻~");
                context.Response.End();
            }
        }
        /// <summary>
        /// 获取新闻列表内容
        /// </summary>
        /// <param name="context"></param>
        private void getnewspage(HttpContext context)
        {

            int id = _Request.GetInt("id");
            BLL.article bll = new BLL.article();


            DataTable dt = bll.GetListalbums(0, "  dt_article.id =" + id + " ", "dt_article.id asc").Tables[0];
            string html = "";
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow aaa in dt.Rows)
                {
                    html += "  <h3>" + aaa["title"] + "</h3>";
                    html += "  <p>" + aaa["content"] + "</p>";
                }
                context.Response.Clear();
                context.Response.Write(html);
                context.Response.End();
            }
            else
            {
                context.Response.Clear();
                context.Response.Write("暂无新闻~");
                context.Response.End();
            }


        }

        private void joininfo(HttpContext context)
        {
            BLL.article_comment bll = new BLL.article_comment();

            string username = DTRequest.GetFormString("txtname");//加盟人
            string mobile = DTRequest.GetFormString("tel");//电话
            string idnum = DTRequest.GetFormString("txtid");//加盟人身份证ID验证
            string adress = DTRequest.GetFormString("adress");//加盟详细地址
            string mendian = DTRequest.GetFormString("mendian");//加盟面积
            string cate = DTRequest.GetFormString("txtcategory").ToString() == "1" ? "服务店" : "代理商";//加盟类别
            string bank = "";
            if (DTRequest.GetFormString("bank").Trim().ToString() == null || DTRequest.GetFormString("bank").Trim().ToString() == "")
            {
                bank = "用户暂未提供银行户头信息";
            }
            else
            {
                bank = DTRequest.GetFormString("bank");
            }
            Model.article_comment model = new Model.article_comment();
            model.channel_id = 1;
            model.user_name = "加盟人姓名:" + username + "　加盟人身份证信息：" + idnum;//联系人
            model.user_ip = mobile;//电话
            model.content = "加盟地址:" + adress + "　加盟面积：" + mendian + "m²" + "　加盟类型：" + cate + "　银行户头信息：" + bank;
            if (bll.Add(model) > 0)
            {
                context.Response.Write("提交成功~我们会在稍后联系您！");
            }
            else
            {
                context.Response.Write("网咯繁忙~请您稍后再试！");
            }
        }
        /// <summary>
        /// 获取搜索的产品并json
        /// </summary>
        /// <param name="context"></param>
        private void searchpro(HttpContext context)
        {

            string outmsg = "{\"status\":0,\"msg\":\"暂无产品！\"}";
            string search = DTRequest.GetFormString("searchword");
            string html = "";
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

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="context"></param>
        public void updateUser(HttpContext context)
        {
            string outmsg = "{\"status\":0,\"msg\":\"修改失败！\"}";

            //userid = ' + user_id + ' & real_name = ' + real_name + ' & mobile = ' + mobile + ' & sex = ' + sex + ' & avatar = ' + avatar + ' & address = ' + address

            int user_id = _Request.GetInt("userid");
            string user_name = _Request.GetString("user_name");
            string real_name = _Request.GetString("real_name");
            string sex = _Request.GetString("sex");
            string mobile = _Request.GetString("mobile");
            string address = _Request.GetString("address");
            string avatar = _Request.GetString("avatar", "");
            
            
            BLL.users bll = new BLL.users();
            Model.users model = bll.GetModel(user_id);            
            model.real_name = real_name;
            model.sex = sex;
            model.mobile = mobile;
            model.sex = sex;
            model.avatar = avatar;            
            model.address = address;//详细地址
     
            if (user_id > 0)
            {
                if(bll.Update(model))
                {                                        
                    Model.users model1 = bll.GetModelMobile(user_name);
                    if (model1 != null)
                    {
                        outmsg = ObjectToJSON(model1);
                    }
                }
            }

            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }


        /// <summary>
        /// 支付成功，回调信息
        /// </summary>
        /// <param name="context"></param>
        public void updateUserCallBack(HttpContext context)
        {
            string outmsg = "{\"status\":0,\"msg\":\"回调失败！\"}";

            //userid = ' + user_id + ' & real_name = ' + real_name + ' & mobile = ' + mobile + ' & sex = ' + sex + ' & avatar = ' + avatar + ' & address = ' + address

            int user_id = _Request.GetInt("userid");
            string user_name = _Request.GetString("user_name");

            BLL.users bll = new BLL.users();
            Model.users model = bll.GetModel(user_id);
            model.group_id = 2;
            model.IsBuwei = 1;
            model.reg_time = DateTime.Now;
            model.pay_time = DateTime.Now.AddYears(1);
           
            if (user_id > 0)
            {
                if (bll.UpdateCallBack(model))
                {
                    Model.users model1 = bll.GetModelMobile(user_name);
                    if (model1 != null)
                    {
                        outmsg = ObjectToJSON(model1);
                    }
                }
            }

            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }


        


        protected string substr(string str, int length)
        {
            if (str.Length > length)
            {
                str = str.Substring(0, length) + "...";
            }
            else
            {
                str = str + "...";
            }
            return str;
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