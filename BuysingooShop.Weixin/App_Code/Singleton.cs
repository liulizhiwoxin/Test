using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuysingooShop.Weixin.App_Code
{
    public class Singleton
    {
        /// 单例模式，未考虑多线程
        //private static Singleton _instance = null;
        //private Singleton() { }

        ///
        //public static Singleton CreateInstance()
        //{
        //    if (_instance == null)
        //    {
        //        _instance = new Singleton();
        //    }
        //    return _instance;
        //}

      
        // 考虑了线程安全
        private volatile static Singleton _instance = null;
        private static readonly object lockHelper = new object();
        private Singleton() { }
        public static Singleton CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new Singleton();
                }
            }
            return _instance;
        }

        public void CallBack(string out_trade_no)
        {
            if (out_trade_no.Contains("_"))
            {
                string[] tradeList = out_trade_no.Split('_');

                int id = Vincent._Convert.ToInt(tradeList[0], 0);

                // 计算奖金
                if (id > 0)
                {
                    string _point = new BuysingooShop.BLL.orders().GetList(0, "id=" + id + "", "id asc").Tables[0].Rows[0]["point"].ToString();

                    Vincent._Log.SaveMessage("回调积分支付3" + _point);

                    if (Convert.ToInt32(_point) > 0)         //使用积分 
                    {
                        var outId = BuysingooShop.BLL.OrdersBLL.p_update_users_point(id);
                    }
                    else      //未使用积分 
                    {
                        var outId = BuysingooShop.BLL.OrdersBLL.p_update_users(id);
                    }



                  //  var outId = BuysingooShop.BLL.OrdersBLL.p_update_users(id);
                }
            }
        }

        public void CallBack2(string out_trade_no)
        {
            if (out_trade_no.Contains("_"))
            {
                string[] tradeList = out_trade_no.Split('_');

                int user_id = Vincent._Convert.ToInt(tradeList[0], 0);


                if (user_id > 0)
                {
                    BLL.users bll = new BLL.users();
                    Model.users model = bll.GetModel(user_id);
                    model.group_id = 2;
                    model.IsBuwei = 1;
                    model.reg_time = DateTime.Now;
                    model.pay_time = DateTime.Now.AddYears(1);


                    if (bll.UpdateCallBack(model))
                    {

                    }


                    //  var outId = BuysingooShop.BLL.OrdersBLL.p_update_users(id);
                }
            }
        }


       
    }
}