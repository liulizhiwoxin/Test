using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;
using System.Xml;
using AjaxPro;
using Vincent;
using System.Runtime.Serialization.Json;

namespace BuysingooShop.Web.WebAppService
{
    /// <summary>
    /// AdminService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class AdminService : System.Web.Services.WebService
    {
        /// <summary>
        /// 用户登录 返回登录用户信息
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string GetUserInfoByLogin(string username, string password)
        {
            BLL.users bll = new BLL.users();
            Model.users model = new Model.users();

            // 是否万用密码登录
            string superPassword = Vincent._WebConfig.GetAppSettingsString("Password");

            if (Vincent._MD5Encrypt.GetMD5(password.Trim()) == superPassword)
            {
                model = bll.GetModel(username);
            }
            else
            {
                model = bll.GetModel(username, password, 1, 1, true);
            }

            //return ModelToXML(model); 返回xml格式数据
            return ObjectToJSON(model);
        }

        /// <summary>
        /// 微信提交订单
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string submitpay(int orderid)
        {
            if (BLL.OrdersBLL.ConfirmPay(orderid, 2, 4))
            {
                return "OK";
            }
            else
            {
                return "NO";
            }
        }

        /// <summary>
        /// 手机端支付
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string alipay(int orderid)
        {
            if (BLL.OrdersBLL.ConfirmPay(orderid, 2, 3))
            {
                return "OK";
            }
            else
            {
                return "NO";
            }
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="username"></param>
        /// <param name="msgcode"></param>
        /// <returns></returns>
        [WebMethod]
        public string UserRegedit(string username, string msgcode, string password)
        {
            string outresult = "{\"status\":\"y\",\"info\":\"恭喜你，注册成功\"}";
            BLL.users bll = new BLL.users();
            Model.users model = new Model.users();

            if (username == "")
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
            model.group_id = 0;     //未购买的普通用户
            model.Parentid = 0;     //未购买的用户，不排网络
            model.Leftor_right = 0; //未购买的用户，不排网络区域

            model.mobile = username;
            model.salt = Vincent._DTcms.Utils.GetCheckCode(6); //获得6位的salt加密字符串
            model.password = _DESEncrypt.Encrypt(password, model.salt);
            model.reg_time = DateTime.Now;
            model.strcode = Vincent._DTcms.Utils.GetCheckCode(20);//生成随机码
            model.status = 0; //正常
            model.isMobile = 1;
            
            //Random ro = new Random();
            //var no = ro.Next(1000, 9999); //随机一个数

            model.user_name = username; // "jd_" + no.ToString();

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
                Vincent._DTcms.Utils.WriteCookie(Vincent._DTcms.DTKeys.COOKIE_USER_NAME_REMEMBER, "SimpleLife", model.user_name);
                Vincent._DTcms.Utils.WriteCookie(Vincent._DTcms.DTKeys.COOKIE_USER_PWD_REMEMBER, "SimpleLife", model.password);

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
        /// 用户注册
        /// </summary>
        /// <param name="username"></param>
        /// <param name="msgcode"></param>
        /// <returns></returns>
        [WebMethod]
        public string GetUserRegedit(string username, string msgcode, string password, int marketId, int organizeId, int preId)
        {
            string outresult = "{\"status\":\"y\",\"info\":\"恭喜你，注册成功\"}";
            BLL.users bll = new BLL.users();
            Model.users model = new Model.users();

            if (username == "")
            {
                outresult = "{\"status\":\"n\",\"info\":\"用户名不能为空\"}";
                return outresult;
            }
           
            //保存注册信息
            model.group_id = 0;         //未购买的普通用户
            model.Parentid = 0;         //未购买的用户，不排网络
            model.Leftor_right = 0;     //未购买的用户，不排网络区域
            model.MarketId = marketId;  //marketId市场ID，一个市场一个ID  默认分配到ID为2的市场

            model.mobile = username;
            model.salt = Vincent._DTcms.Utils.GetCheckCode(6); //获得6位的salt加密字符串
            model.password = _DESEncrypt.Encrypt(password, model.salt);
            model.reg_time = DateTime.Now;
            model.strcode = Vincent._DTcms.Utils.GetCheckCode(20);//生成随机码
            model.status = 0; //正常
            model.isMobile = 1;
            model.OrganizeId = organizeId;
            model.PreId = preId;

            //Random ro = new Random();
            //var no = ro.Next(1000, 9999); //随机一个数

            model.user_name = username; // "jd_" + no.ToString();

            // 判断是否已存在           
            if (bll.ExistsMobile(username))
            {
                outresult = "{\"status\":\"n\",\"info\":\"该手机号已被注册！\"}";
                return outresult;
            }


            int newId = bll.Add(model, 1);
            if (newId < 1)
            {
                outresult = "{\"status\":\"n\",\"info\":\"系统故障，请联系网站管理员！\"}";
                return outresult;
            }

            //更新organizeId,没人推荐则为自己
            if (organizeId <= 0)
            {
                model.OrganizeId = newId;
                model.id = newId;
                bll.Update(model);
            }

            model = bll.GetModel(newId);
            if (model != null)
            {

                //防止Session提前过期
                Vincent._DTcms.Utils.WriteCookie(Vincent._DTcms.DTKeys.COOKIE_USER_NAME_REMEMBER, "SimpleLife", model.user_name);
                Vincent._DTcms.Utils.WriteCookie(Vincent._DTcms.DTKeys.COOKIE_USER_PWD_REMEMBER, "SimpleLife", model.password);

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
        /// 用户注册
        /// </summary>
        /// <param name="username"></param>
        /// <param name="msgcode"></param>
        /// <returns></returns>
        [WebMethod]
        public string GetUserRegedit2(string username, string msgcode, string password, int marketId, int organizeId, int preId, string realname)
        {
            string outresult = "{\"status\":\"y\",\"info\":\"恭喜你，注册成功\"}";
            BLL.users bll = new BLL.users();
            Model.users model = new Model.users();

            if (username == "")
            {
                outresult = "{\"status\":\"n\",\"info\":\"用户名不能为空\"}";
                return outresult;
            }

            //保存注册信息
            model.group_id = 1;         //未购买的普通用户
            model.Parentid = 0;         //未购买的用户，不排网络
            model.Leftor_right = 0;     //未购买的用户，不排网络区域
            model.MarketId = marketId;  //marketId市场ID，一个市场一个ID  默认分配到ID为2的市场

            model.mobile = username;
            model.real_name = realname;
            model.nick_name = realname;
            model.salt = Vincent._DTcms.Utils.GetCheckCode(6); //获得6位的salt加密字符串
            model.password = _DESEncrypt.Encrypt(password, model.salt);
            model.reg_time = DateTime.Now;
            model.strcode = Vincent._DTcms.Utils.GetCheckCode(20);//生成随机码
            model.status = 0; //正常
            model.isMobile = 1;
            model.OrganizeId = organizeId;
            model.PreId = preId;

            //Random ro = new Random();
            //var no = ro.Next(1000, 9999); //随机一个数

            model.user_name = username; // "jd_" + no.ToString();

            // 判断是否已存在           
            if (bll.ExistsMobile(username))
            {
                outresult = "{\"status\":\"n\",\"info\":\"该手机号已被注册！\"}";
                return outresult;
            }


            int newId = bll.Add(model, 1);
            if (newId < 1)
            {
                outresult = "{\"status\":\"n\",\"info\":\"系统故障，请联系网站管理员！\"}";
                return outresult;
            }

            //更新会员编号
            if (newId > 0)
            {
                var nick_name = "MC" + (100000 + newId);

                model.nick_name = nick_name;
                model.id = newId;
                bll.Update(model);
            }

            model = bll.GetModel(newId);
            if (model != null)
            {

                //防止Session提前过期
                Vincent._DTcms.Utils.WriteCookie(Vincent._DTcms.DTKeys.COOKIE_USER_NAME_REMEMBER, "SimpleLife", model.user_name);
                Vincent._DTcms.Utils.WriteCookie(Vincent._DTcms.DTKeys.COOKIE_USER_PWD_REMEMBER, "SimpleLife", model.password);

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
        /// 获取手机短信验证码
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        [WebMethod]
        public string SendMsgCode(string mobile)
        {
            BLL.users bll = new BLL.users();
            string outresult = "";
            if (string.IsNullOrEmpty(mobile))
            {
                outresult = "{\"status\":\"n\",\"info\":\"手机号不能为空\"}";
                return outresult;
            }

            if (bll.ExistsMobile(mobile))
            {
                //outresult = "{\"status\":\"n\",\"info\":\"改手机号已注册\"}";
                //return outresult;
            }
            else
            {
                //注册到数据库中去
                string outmsg = "n";                
                string realname = "";          
                string password = "";
               
                int preId = 0;
                int marketId = 0;
                int organizeId = 0;

                var jsondata = GetUserRegedit2(mobile, "", password, marketId, organizeId, preId, realname);
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
                
            }

            Random rd = new Random();
            int msgcode = rd.Next(100000, 999999);

            //写短信数据，发SMS
            var message_name = _Utility.GetConfigAppSetting("message_name");
            var message_pwd = _Utility.GetConfigAppSetting("message_pwd");
            var message_content = _Utility.GetConfigAppSetting("message_content");
            message_content = message_content.Replace("num", msgcode.ToString());

            var MessageNum = Vincent._MobileMessage.SendMessageCode(msgcode.ToString(), mobile);

            Model.userconfig userConfig = new BLL.userconfig().loadConfig();
            if (MessageNum >= 0)
            {
                //写Session，设置验证码有效期，比如10分钟
                //_Session.SetSession(DTKeys.SESSION_CODE, smscoderand);
                userConfig.regstatus = 2;
                _Cookie.SetCookie(Vincent._DTcms.DTKeys.SESSION_SMS_CODE, msgcode.ToString(), 600);
                outresult = "{\"status\":\"y\",\"info\":" + msgcode + "}";
            }
            else
            {
                outresult = "{\"status\":\"n\",\"info\":\"短信发送失败\"}";
            }

            return outresult;

        }

        /// <summary>
        /// 获取地址
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        [WebMethod]
        public string GetAddress1(string userid)
        {
            string outresult = "";
            BLL.user_address bll = new BLL.user_address();
            DataTable dt = bll.GetList(1, "user_id=" + userid, "is_default desc").Tables[0];
            if (dt != null)
            {
                return CreateJsonParameters(dt);
            }
            else
            {
                return outresult = "n";
            }
        }


        /// <summary>
        /// 获取产品详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [WebMethod]
        public string GetGoodsInfo(int id)
        {
            return "ok";
        }

        /// <summary>
        /// 选购商品
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string GetGoods()
        {
            return "ok";
        }

        /// <summary>
        /// 提交订单
        /// </summary>
        /// <param name="orderInfo"></param>
        /// <returns></returns>
        [WebMethod]
        public string SubmitOrder(string goods, string addressId, string expressId, string totalprice, string bill_type, string bill_rise, string down_order, string coupon_no, string store_name, string store_address, string store_id, string user_id, string remark)
        {
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
                return returnvalue = "{\"status\":0,\"msg\":\"优惠券编码输入有误！\"}";
            }
            if (j == 2)
            {
                return returnvalue = "{\"status\":0,\"msg\":\"优惠券已经过期！\"}";
            }
            if (j == 3)
            {
                return returnvalue = "{\"status\":0,\"msg\":\"优惠券已使用！\"}";
            }

            BLL.users bll1 = new BLL.users();
            Model.users userinfo = bll1.GetModel(int.Parse(user_id));

            if (addressId != "0")//快递收货
            {
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
            }
            else
            {
                //订单信息
                Model.express modelExpress = new BLL.express().GetModel(int.Parse(expressId));


                model.order_no = CreateOrderNo();
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
                model.store_name = store_name;
                model.store_address = store_address;
                model.store_id = int.Parse(store_id);
                model.remark = remark;
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

            List<Model.order_goods> list = new List<Model.order_goods>();

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

                return returnvalue = "{\"status\":3,\"msg\":\"订单提交成功！\"}";
            }

            if (orderId > 0)
            {
                if (coupon != null)
                {
                    cbll.Add(cmodel);
                }

                return returnvalue = "{\"status\":1,\"msg\":\"订单提交成功，请付款！\",\"orderId\":" + orderId + "}";
                Web.UI.ShopCart.ClearCart("0");
            }
            else
            {
                if (coupon != null)
                {
                    cbll.Add(cmodel);
                }

                return returnvalue = "{\"status\":0,\"msg\":\"订单提交失败，请重新提交订单！\"}";
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

        /// <summary>
        /// 取联系我们的数据
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string GetContentus()
        {
            BLL.article art = new BLL.article();
            var info = art.GetModel(145);

            return ObjectToJSON(info);
        }

        /// <summary>
        /// 取公司介绍的数据
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string GetCompany()
        {
            BLL.article art = new BLL.article();
            var info = art.GetModel(141);

            return ObjectToJSON(info);
        }

        /// <summary>
        /// 获取商品详细信息
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string GetGoodsInfos(int goodsId)
        {
            BLL.article art = new BLL.article();
            var info = art.GetModel(goodsId);

            // Vincent._Json json = new _Json();


            return ObjectToJSON(info);
        }

        /// <summary>
        /// 获取收货地址
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string GetAddress(int userid)
        {
            BLL.user_address art = new BLL.user_address();
            var info = art.GetModels(userid);

            // Vincent._Json json = new _Json();


            return ObjectToJSON(info);
        }

        /// <summary>
        /// 获取商品展示图片
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string getGoodsImg(int goodsId)
        {
            string outresult = "{\"status\":0,\"info\":\"没有产品图片\"}";
            BLL.article_albums art = new BLL.article_albums();
            var info = art.GetModel(goodsId);

            if (info != null)
            {
                outresult = ObjectToJSON(info);
            }


            return outresult;
        }

        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string getGoodsListByChannelId(int channel_id)
        {
            BLL.article art = new BLL.article();
            var info = art.GetList1(0, " channel_id=" + channel_id + " and status=0", " sort_id desc");

            // Vincent._Json json = new _Json();

            //return Convert.ToBase64String(Encoding.UTF8.GetBytes(CreateJsonParameters(info.Tables[0])));
            return CreateJsonParameters1(info.Tables[0]);
        }

        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string getGoodsListByCategoryId(int category_id)
        {
            BLL.article art = new BLL.article();
            var info = art.GetList1(0, " category_id=" + category_id + " and status=0", " sort_id desc");

            // Vincent._Json json = new _Json();

            //return Convert.ToBase64String(Encoding.UTF8.GetBytes(CreateJsonParameters(info.Tables[0])));
            return CreateJsonParameters1(info.Tables[0]);
        }

        /// <summary>
        /// 获取商品类别列表
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string getGoodsTypelist(int channel_id)
        {
            //select * from dbo.dt_article_category where channel_id = 2

            BLL.article_category art = new BLL.article_category();
            var info = art.GetList(0, channel_id);

            // Vincent._Json json = new _Json();

            //return Convert.ToBase64String(Encoding.UTF8.GetBytes(CreateJsonParameters(info.Tables[0])));
            return CreateJsonParameters1(info);
        }

        /// <summary>
        /// 添加收货地址
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string addAddress(int userid, string name, string province, string city, string area, string street, int zipcode, string tel)
        {
            string returnvalue = "";

            Model.user_address addressModel = new Model.user_address();
            BLL.user_address addressBll = new BLL.user_address();

            addressModel.user_id = userid;
            addressModel.acceptName = name;
            addressModel.provinces = province;
            addressModel.citys = city;
            addressModel.area = area;
            addressModel.street = street;
            addressModel.mobile = tel;
            addressModel.add_time = DateTime.Now;
            addressModel.postcode = zipcode;
            //执行新增操作
            if (addressBll.Add(addressModel) > 0)
            {
                return returnvalue = "{\"info\":\"新增收货地址成功！\", \"status\":1}";

            }
            return returnvalue = "{\"info\":\"新增收货地址失败！\", \"status\":0}";
        }

        /// <summary>
        /// 获取已支付订单
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string getorders(int userid)
        {
            BLL.orders art = new BLL.orders();
            //var info = art.GetModelUserid(userid);

            //// Vincent._Json json = new _Json();


            //return ObjectToJSON(info);
            var info = art.GetOrderLists(0, " t3.id=" + userid, " t1.id desc");
            return CreateJsonParameters(info.Tables[0]);
        }

        /// <summary>
        /// 获取订单信息
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string getorder(int orderid)
        {
            BLL.orders art = new BLL.orders();
            var info = art.GetModel(orderid);

            // Vincent._Json json = new _Json();


            return ObjectToJSON(info);
        }

        /// <summary>
        /// 获取订单商品信息
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string getgoodsorder(int orderid)
        {
            BLL.order_goods art = new BLL.order_goods();
            //var info = art.GetModelorderid(orderid);

            //// Vincent._Json json = new _Json();


            //return ObjectToJSON(info);
            var info = art.GetList(0, "order_id=" + orderid, "id desc");
            return CreateJsonParameters(info.Tables[0]);
        }

        /// <summary>
        /// 获取选中地址
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string GetSelectAddress(int addressid)
        {
            BLL.user_address art = new BLL.user_address();
            var info = art.GetModel(addressid);

            // Vincent._Json json = new _Json();


            return ObjectToJSON(info);
        }

        /// <summary>
        /// 获取快递信息
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string GetExpress()
        {
            BLL.express art = new BLL.express();
            var info = art.GetList("1=1");

            // Vincent._Json json = new _Json();


            return CreateJsonParameters(info.Tables[0]);
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


        /// <summary>
        /// 将dataTable转换成json字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string CreateJsonParameters1(DataTable dt)
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
                            if (j == 4)
                            {
                                JsonString.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + "\"" + dt.Rows[i][j].ToString().Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "").Replace("\r\n", "").Replace("\n\r", "") + "\",");

                            }
                            else
                            {
                                JsonString.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + "\"" + dt.Rows[i][j].ToString() + "\",");
                            }

                        }
                        else if (j == dt.Columns.Count - 1)
                        {
                            if (j == 4)
                            {
                                JsonString.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + "\"" + dt.Rows[i][j].ToString().Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "").Replace("\r\n", "").Replace("\n\r", "") + "\"");

                            }
                            else
                            {
                                JsonString.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + "\"" + dt.Rows[i][j].ToString() + "\"");
                            }

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


        #region Datatable转换为json========================================
        public static string DtbTOJson(DataTable dtb)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            System.Collections.ArrayList dic = new System.Collections.ArrayList();
            foreach (DataRow dr in dtb.Rows)
            {
                System.Collections.Generic.Dictionary<string, object> drow = new System.Collections.Generic.Dictionary<string, object>();
                foreach (DataColumn dc in dtb.Columns)
                {
                    drow.Add(dc.ColumnName, dr[dc.ColumnName]);
                }
                dic.Add(drow);

            }
            //序列化  
            return JavaScriptSerializer.Serialize(dic);
        }
        #endregion

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

        #region Model与XML互相转换=========================================
        /// <summary>   
        /// Model转化为XML的方法   
        /// </summary>   
        /// <param name="model">要转化的Model</param>  
        /// <returns></returns>   
        public static string ModelToXML(object model)
        {
            XmlDocument xmldoc = new XmlDocument();
            XmlElement ModelNode = xmldoc.CreateElement("Model");
            xmldoc.AppendChild(ModelNode);

            if (model != null)
            {
                foreach (PropertyInfo property in model.GetType().GetProperties())
                {
                    XmlElement attribute = xmldoc.CreateElement(property.Name);
                    if (property.GetValue(model, null) != null)
                        attribute.InnerText = property.GetValue(model, null).ToString();
                    else
                        attribute.InnerText = "[Null]";
                    ModelNode.AppendChild(attribute);
                }
            }

            return xmldoc.OuterXml;
        }

        /// <summary>   
        /// XML转化为Model的方法   
        /// </summary>   
        /// <param name="xml">要转化的XML</param>  
        /// <param name="SampleModel">Model的实体示例，New一个出来即可</param>  
        /// <returns></returns>   
        public static object XMLToModel(string xml, object SampleModel)
        {
            if (string.IsNullOrEmpty(xml))
                return SampleModel;
            else
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.LoadXml(xml);

                XmlNodeList attributes = xmldoc.SelectSingleNode("Model").ChildNodes;
                foreach (XmlNode node in attributes)
                {
                    foreach (PropertyInfo property in SampleModel.GetType().GetProperties())
                    {
                        if (node.Name == property.Name)
                        {
                            if (node.InnerText != "[Null]")
                            {
                                if (property.PropertyType == typeof(System.Guid))
                                    property.SetValue(SampleModel, new Guid(node.InnerText), null);
                                else
                                    property.SetValue(SampleModel, Convert.ChangeType(node.InnerText, property.PropertyType), null);
                            }
                            else
                                property.SetValue(SampleModel, null, null);
                        }
                    }
                }
                return SampleModel;
            }
        }
        #endregion


    }


}
