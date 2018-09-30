using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace BuysingooShop.Weixin.WeixinPay
{
    public partial class return_url : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Vincent._Log.SaveMessage("进入回调页面");
          
            string wxNotifyXml = "";

            byte[] bytes = Request.BinaryRead(Request.ContentLength);
            wxNotifyXml = System.Text.Encoding.UTF8.GetString(bytes);

            if (wxNotifyXml.Length == 0)
            {
                return;
            }

            Vincent._Log.SaveMessage("回调第一步：进入return_url:" + wxNotifyXml);

            XmlDocument xmldoc = new XmlDocument();

            xmldoc.LoadXml(wxNotifyXml);

            string ResultCode = xmldoc.SelectSingleNode("/xml/result_code").InnerText;
            string ReturnCode = xmldoc.SelectSingleNode("/xml/return_code").InnerText;

            if (ReturnCode == "SUCCESS" && ResultCode == "SUCCESS")
            {
                //验证成功
                //取结果参数做业务处理
                string out_trade_no = xmldoc.SelectSingleNode("/xml/out_trade_no").InnerText;
                //财付通订单号
                string trade_no = xmldoc.SelectSingleNode("/xml/transaction_id").InnerText;
                //金额,以分为单位
                string total_fee = xmldoc.SelectSingleNode("/xml/total_fee").InnerText;

                /********************************
                 * 
                 * 自己业务处理 计算奖金
                 *
                *********************************/
                try
                {
                    // 休眠N微秒钟
                    //Random ran = new Random();
                    //int RandKey = ran.Next(2000, 5000);
                    //System.Threading.Thread.Sleep(RandKey);


                    var sing = BuysingooShop.Weixin.App_Code.Singleton.CreateInstance();
                    sing.CallBack2(out_trade_no);


                }
                catch (Exception ex)
                {
                    Vincent._Log.SaveMessage(ex.Message);
                }
                Vincent._Log.SaveMessage("回调第二步：自己业务处理:取结果参数做业务处理:" + out_trade_no + ";财付通订单号" + trade_no + ";金额,以分为单位" + total_fee);
            }
        }

    }
}