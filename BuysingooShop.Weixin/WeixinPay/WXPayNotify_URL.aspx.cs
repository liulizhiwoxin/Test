using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data;
using BuysingooShop.BLL;
namespace BuysingooShop.Weixin.WeixinPay
{
    public partial class WXPayNotify_URL : System.Web.UI.Page
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

            Vincent._Log.SaveMessage("回调第一步：进入WXPayNotify_URL:" + wxNotifyXml);

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
                    if (out_trade_no.Contains("_"))
                    {
                        Vincent._Log.SaveMessage("回调积分支付1" + out_trade_no);

                        string[] tradeList = out_trade_no.Split('_');

                        int id = Vincent._Convert.ToInt(tradeList[0], 0);

                        Vincent._Log.SaveMessage("回调积分支付2" + id);

                        // 计算奖金
                        if (id > 0)
                        {

                            string _point =  new BuysingooShop.BLL.orders().GetList(0, "id=" + id + "", "id asc").Tables[0].Rows[0]["point"].ToString();

                            Vincent._Log.SaveMessage("回调积分支付3" + _point);

                            if (Convert.ToInt32(_point)>0)         //使用积分 
                            {
                                var outId = BuysingooShop.BLL.OrdersBLL.p_update_users_point(id);
                            }
                            else      //未使用积分 
                            {
                                var outId = BuysingooShop.BLL.OrdersBLL.p_update_users(id);
                            }

                        
                           
                        }
                    }
                    
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