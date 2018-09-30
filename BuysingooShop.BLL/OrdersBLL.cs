using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BuysingooShop.BLL
{
    public class OrdersBLL
    {
        /// <summary>
        /// 确认支付
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static bool ConfirmPay(int ID,int PayType,int pay)
        {
            return BuysingooShop.DAL.OrdersDAL.ConfirmPay(ID, PayType,pay);
        }

        /// <summary>
        /// 确认充值
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static bool ConfirmRecharge(string ID, int PayType)
        {
            return BuysingooShop.DAL.OrdersDAL.ConfirmRecharge(ID, PayType);
        }


        /// <summary>
        /// 财务确认结算
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="PayType"> //1确认快递代收  2客户已经付款 </param>
        /// <returns></returns>
        public static bool ConfirmIsPay(int ID, int PayType)
        {
            return BuysingooShop.DAL.OrdersDAL.ConfirmIsPay(ID, PayType);
        }

        /// <summary>
        /// 前端支付成功，更新用户状态，计算业绩，排网络
        /// </summary>
        /// <param name="billno">微信支付过来的订单号</param>        
        /// <returns></returns>
        public static int p_update_users(int id)
        {
            return BuysingooShop.DAL.OrdersDAL.p_update_users(id);
        }
        /// <summary>
        /// 前端支付成功，更新用户状态，计算业绩，排网络(积分相关)
        /// </summary>
        /// <param name="billno">微信支付过来的订单号)</param>        
        /// <returns></returns>
        public static int p_update_users_point(int id)
        {
            return BuysingooShop.DAL.OrdersDAL.p_update_users_point(id);
        }
    }
}
