using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vincent;
using Com.Alipay;

namespace BuysingooShop.Web.AppCode
{
    public class Utility
    {
         /// <summary>
        /// 获取session中保存的，当前登录用户信息
        /// </summary>
        /// <returns></returns>
        public static Model.users GetUserBySession()
        {            
            Model.users model = HttpContext.Current.Session[Vincent._DTcms.DTKeys.SESSION_USER_INFO] as Model.users;
            if (model != null)
            {
                //为了能查询到最新的用户信息，必须查询最新的用户资料
                model = new BLL.users().GetModel(model.id);
                return model;
            }

            return null;
        }
        
        #region 支付宝充值

        /// <summary>
        /// 获取支付宝充值的html代码
        /// </summary>
        /// <param name="out_trade_no">商户订单号</param>
        /// <param name="subject">订单名称</param>
        /// <param name="price">付款金额</param>
        /// <param name="emsprice">邮费</param>
        /// <param name="body">订单描述</param>
        //public static string GetAlipayHtml(string out_trade_no, string subject, string price, string emsprice, string body)
        //{
        //    ////////////////////////////////////////////请求参数////////////////////////////////////////////

        //    var User = GetUserBySession();  //当前充值的用户

        //    //支付类型
        //    string payment_type = "1";
        //    //必填，不能修改
        //    //服务器异步通知页面路径
        //    string notify_url = _Utility.GetConfigAppSetting("notify_url");
        //    //需http://格式的完整路径，不能加?id=123这类自定义参数

        //    //页面跳转同步通知页面路径
        //    string return_url = _Utility.GetConfigAppSetting("return_url");
        //    //需http://格式的完整路径，不能加?id=123这类自定义参数，不能写成http://localhost/

        //    //卖家支付宝帐户
        //    string seller_email = _Utility.GetConfigAppSetting("seller_email"); //WIDseller_email.Text.Trim();
        //    //必填

        //    //商户订单号
        //    //string out_trade_no = ""; //WIDout_trade_no.Text.Trim();
        //    //商户网站订单系统中唯一订单号，必填

        //    //订单名称
        //    //string subject = ""; //WIDsubject.Text.Trim();
        //    //必填

        //    //付款金额
        //    //string price = ""; //WIDprice.Text.Trim();
        //    //必填

        //    //商品数量
        //    string quantity = "1";
        //    //必填，建议默认为1，不改变值，把一次交易看成是一次下订单而非购买一件商品
        //    //物流费用
        //    string logistics_fee = emsprice;
        //    //必填，即运费
        //    //物流类型
        //    string logistics_type = "EXPRESS";
        //    //必填，三个值可选：EXPRESS（快递）、POST（平邮）、EMS（EMS）
        //    //物流支付方式
        //    string logistics_payment = "BUYER_PAY";
        //    //必填，两个值可选：SELLER_PAY（卖家承担运费）、BUYER_PAY（买家承担运费）
            
        //    //订单描述
        //    //string body = ""; //WIDbody.Text.Trim();

        //    //商品展示地址
        //    string show_url = ""; //WIDshow_url.Text.Trim();
        //    //需以http://开头的完整路径，如：http://www.xxx.com/myorder.html

        //    //收货人姓名
        //    string receive_name = User.real_name; //WIDreceive_name.Text.Trim();
        //    //如：张三

        //    //收货人地址
        //    string receive_address = User.address; //WIDreceive_address.Text.Trim();
        //    //如：XX省XXX市XXX区XXX路XXX小区XXX栋XXX单元XXX号

        //    //收货人邮编
        //    string receive_zip = "415000"; //WIDreceive_zip.Text.Trim();
        //    //如：123456

        //    //收货人电话号码
        //    string receive_phone = User.telephone; //WIDreceive_phone.Text.Trim();
        //    //如：0571-88158090

        //    //收货人手机号码
        //    string receive_mobile = User.mobile; //WIDreceive_mobile.Text.Trim();
        //    //如：13312341234

        //    ////////////////////////////////////////////////////////////////////////////////////////////////

        //    //把请求参数打包成数组
        //    SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
        //    sParaTemp.Add("partner", Config.Partner);
        //    sParaTemp.Add("_input_charset", Config.Input_charset.ToLower());
        //    sParaTemp.Add("service", "trade_create_by_buyer");
        //    sParaTemp.Add("payment_type", payment_type);
        //    sParaTemp.Add("notify_url", notify_url);
        //    sParaTemp.Add("return_url", return_url);
        //    sParaTemp.Add("seller_email", seller_email);
        //    sParaTemp.Add("out_trade_no", out_trade_no);
        //    sParaTemp.Add("subject", subject);
        //    sParaTemp.Add("price", price);
        //    sParaTemp.Add("quantity", quantity);
        //    sParaTemp.Add("logistics_fee", logistics_fee);
        //    sParaTemp.Add("logistics_type", logistics_type);
        //    sParaTemp.Add("logistics_payment", logistics_payment);
        //    sParaTemp.Add("body", body);
        //    sParaTemp.Add("show_url", show_url);
        //    sParaTemp.Add("receive_name", receive_name);
        //    sParaTemp.Add("receive_address", receive_address);
        //    sParaTemp.Add("receive_zip", receive_zip);
        //    sParaTemp.Add("receive_phone", receive_phone);
        //    sParaTemp.Add("receive_mobile", receive_mobile);

        //    //建立请求
        //    string sHtmlText = Submit.BuildRequest(sParaTemp, "get", "确认");

        //    return sHtmlText;
        //}

        /// <summary>
        /// 获取支付宝充值的html代码
        /// </summary>
        /// <param name="out_trade_no">商户订单号</param>
        /// <param name="subject">订单名称</param>
        /// <param name="price">付款金额</param>
        /// <param name="emsprice">邮费</param>
        /// <param name="body">订单描述</param>
        public static string GetAlipayHtml(string out_trade_no, string subject, string price, string emsprice, string body)
        {
            ////////////////////////////////////////////请求参数////////////////////////////////////////////

            var User = GetUserBySession();  //当前充值的用户

            ////支付类型
            //string payment_type = "1";
            ////必填，不能修改
            ////服务器异步通知页面路径
            //string notify_url = Happy.Common._Utility.GetConfigAppSetting("notify_url");
            ////需http://格式的完整路径，不能加?id=123这类自定义参数

            ////页面跳转同步通知页面路径
            //string return_url = Happy.Common._Utility.GetConfigAppSetting("return_url");
            ////需http://格式的完整路径，不能加?id=123这类自定义参数，不能写成http://localhost/

            ////卖家支付宝帐户
            //string seller_email = Happy.Common._Utility.GetConfigAppSetting("seller_email"); //WIDseller_email.Text.Trim();
            ////必填

            ////商户订单号
            ////string out_trade_no = ""; //WIDout_trade_no.Text.Trim();
            ////商户网站订单系统中唯一订单号，必填

            ////订单名称
            ////string subject = ""; //WIDsubject.Text.Trim();
            ////必填

            ////付款金额
            ////string price = ""; //WIDprice.Text.Trim();
            ////必填

            ////商品数量
            //string quantity = "1";
            ////必填，建议默认为1，不改变值，把一次交易看成是一次下订单而非购买一件商品
            ////物流费用
            //string logistics_fee = "0.00";
            ////必填，即运费
            ////物流类型
            //string logistics_type = "EXPRESS";
            ////必填，三个值可选：EXPRESS（快递）、POST（平邮）、EMS（EMS）
            ////物流支付方式
            //string logistics_payment = "SELLER_PAY";
            ////必填，两个值可选：SELLER_PAY（卖家承担运费）、BUYER_PAY（买家承担运费）

            ////订单描述
            ////string body = ""; //WIDbody.Text.Trim();

            ////商品展示地址
            //string show_url = ""; //WIDshow_url.Text.Trim();
            ////需以http://开头的完整路径，如：http://www.xxx.com/myorder.html

            ////收货人姓名
            //string receive_name = User.TrueName; //WIDreceive_name.Text.Trim();
            ////如：张三

            ////收货人地址
            //string receive_address = User.Address; //WIDreceive_address.Text.Trim();
            ////如：XX省XXX市XXX区XXX路XXX小区XXX栋XXX单元XXX号

            ////收货人邮编
            //string receive_zip = User.PostCode; //WIDreceive_zip.Text.Trim();
            ////如：123456

            ////收货人电话号码
            //string receive_phone = User.Tel; //WIDreceive_phone.Text.Trim();
            ////如：0571-88158090

            ////收货人手机号码
            //string receive_mobile = User.Mobile; //WIDreceive_mobile.Text.Trim();
            ////如：13312341234


            //支付类型
            string payment_type = "1";
            //必填，不能修改
            //服务器异步通知页面路径
            string notify_url = Vincent._Utility.GetConfigAppSetting("notify_url");
            //需http://格式的完整路径，不能加?id=123这类自定义参数

            //页面跳转同步通知页面路径
            string return_url = Vincent._Utility.GetConfigAppSetting("return_url");
            //需http://格式的完整路径，不能加?id=123这类自定义参数，不能写成http://localhost/

            //卖家支付宝帐户
            string seller_email = Vincent._Utility.GetConfigAppSetting("seller_email");
            //必填

            //商户订单号
            //string out_trade_no = WIDout_trade_no.Text.Trim();
            //商户网站订单系统中唯一订单号，必填

            //订单名称
            //string subject = WIDsubject.Text.Trim();
            //必填

            //付款金额
            string total_fee = (Vincent._Convert.ToFloat(emsprice, 0) + Vincent._Convert.ToFloat(price, 0)).ToString(); //WIDtotal_fee.Text.Trim();
            //必填

            //订单描述

            //string body = WIDbody.Text.Trim();
            //商品展示地址
            string show_url = "";// WIDshow_url.Text.Trim();
            //需以http://开头的完整路径，例如：http://www.xxx.com/myorder.html

            //防钓鱼时间戳
            string anti_phishing_key = "";
            //若要使用请调用类文件submit中的query_timestamp函数

            //客户端的IP地址
            string exter_invoke_ip = "";
            //非局域网的外网IP地址，如：221.0.0.1


            ////////////////////////////////////////////////////////////////////////////////////////////////


            //把请求参数打包成数组
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("partner", Config.Partner);
            sParaTemp.Add("_input_charset", Config.Input_charset.ToLower());
            sParaTemp.Add("service", "create_direct_pay_by_user");
            sParaTemp.Add("payment_type", payment_type);
            sParaTemp.Add("notify_url", notify_url);
            sParaTemp.Add("return_url", return_url);
            sParaTemp.Add("seller_email", seller_email);
            sParaTemp.Add("out_trade_no", out_trade_no);
            sParaTemp.Add("subject", subject);
            sParaTemp.Add("total_fee", total_fee);
            sParaTemp.Add("body", body);
            sParaTemp.Add("show_url", show_url);
            sParaTemp.Add("anti_phishing_key", anti_phishing_key);
            sParaTemp.Add("exter_invoke_ip", exter_invoke_ip);

            //建立请求
            string sHtmlText = Submit.BuildRequest(sParaTemp, "get", "确认");

            return sHtmlText;
        }

        #endregion

        #region 短信通知

        /// <summary>
        /// 短信通知Sales有新的订单下来，请及时跟进
        /// </summary>
        public static void SendMessageToSales(string toMobile, string content)
        {
            //写短信数据，发SMS
            var message_name = Vincent._Utility.GetConfigAppSetting("message_name");
            var message_pwd = Vincent._Utility.GetConfigAppSetting("message_pwd");

           
            /*
            >0	成功,系统生成的任务编号,long类型
             0	失败
            -1	用户名或者密码不正确
            -2	必填选项为空
            -3	短信内容0个字节
            -4	0个有效号码
            -5	余额不够
            -6	含有一级敏感词
            -7	含有二级敏感词，人工审核
            -8	提交频率太快，退避重发
            -9	数据格式错误
            -10	用户被禁用
            -11	短信内容过长
             * */
           
            var MessageNum = Vincent._MobileMessage.SendMessageCode(content, toMobile);
                  
            if (MessageNum <= 0)
            {
                //记录日志
                _Log.SaveMessage("手机：" + toMobile + "，原因:" + NumToMsg(MessageNum)); 
            }
        }

        /// <summary>
        /// 短信码转信息详情
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string NumToMsg(Int64 num)
        {    
            var outmsg = "";
            switch (num)
            {
                case 0:
                    outmsg = "失败";
                    break;
                case -1:
                    outmsg = "用户名或者密码不正确";
                    break;
                case -2:
                    outmsg = "必填选项为空";
                    break;
                case -3:
                    outmsg = "短信内容0个字节";
                    break;
                case -4:
                    outmsg = "0个有效号码";
                    break;
                case -5:
                    outmsg = "余额不够";
                    break;
                case -6:
                    outmsg = "含有一级敏感词";
                    break;
                case -7:
                    outmsg = "含有二级敏感词，人工审核";
                    break;
                case -8:
                    outmsg = "提交频率太快，退避重发";
                    break;
                case -9:
                    outmsg = "数据格式错误";
                    break;
                case -10:
                    outmsg = "用户被禁用";
                    break;
                case -11:
                    outmsg = "短信内容过长";
                    break;
            }

            return outmsg;
        }

        #endregion

    }
}