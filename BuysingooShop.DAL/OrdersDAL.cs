using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BuysingooShop.DBUtility;

namespace BuysingooShop.DAL
{
    public class OrdersDAL
    {
        
        /// <summary>
        /// 确认支付
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static bool ConfirmPay(int ID, int PayType, int pay)
        {
            orders bll = new orders("dt_");
            Model.orders model = bll.GetModel(ID);

            user_coupon coubll = new user_coupon("dt_");
            Model.user_coupon coumodel = new Model.user_coupon();
            if (model.str_code != "")
            {
                coumodel = coubll.GetModel(" str_code='" + model.str_code + "'");
            }

            if (model.status > 1 || model.payment_status == 2)
            {
                return false;
            }
            model.payment_status = 2;
            model.payment_time = DateTime.Now;
            model.status = PayType;
            model.payment_id = pay;
            model.confirm_time = DateTime.Now;
            if (bll.Update(model))
            {
                if (model.str_code != "")
                {

                    coumodel.status = 2;
                    coubll.Update(coumodel);

                    users bll1 = new users("dt_");
                    Model.users userinfo = bll1.GetModel(model.user_id);
                    //优惠券使用记录
                    user_coupon_log cbll = new user_coupon_log("dt_");
                    Model.user_coupon_log cmodel = new Model.user_coupon_log();
                    cmodel.user_id = userinfo.id;
                    cmodel.user_name = userinfo.user_name;
                    cmodel.coupon_id = coumodel.id;
                    cmodel.str_code = model.str_code;
                    cmodel.order_id = model.id;
                    cmodel.order_no = model.order_no;
                    cmodel.add_time = coumodel.add_time;
                    cmodel.use_time = DateTime.Now;
                    cmodel.status = 2;
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// 确认充值
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static bool ConfirmRecharge(string ID, int PayType)
        {
            user_point_log bll = new user_point_log("dt_");
            Model.user_point_log model = bll.GetModelrechargeno(ID);
            users bll1 = new users("dt_");
            Model.users model1 = bll1.GetModel(model.user_id);

            if (model.status == 2 || model.pointtype == 2)
            {
                return false;
            }
            //积分表
            model.status = PayType;
            model.add_time = DateTime.Now;
            //用户表
            model1.amount += model.amount;
            if (bll.Update(model)&&bll1.Update(model1))
            {

                return true;
            }
            return false;
        }

        /// <summary>
        /// 财务确认结算
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="PayType"> //1确认快递代收  2客户已经付款 </param>
        /// <returns></returns>
        public static bool ConfirmIsPay(int ID, int PayType)
        {
            bool isOK = false;
            
            return isOK;
        }


        #region 存储过程

        /// <summary>
        /// 前端支付成功，更新用户状态，计算业绩，排网络
        /// </summary>
        /// <param name="billno">微信支付过来的订单号</param>
        /// <returns></returns>
        public static int p_update_users(int id)
        {
            int ret = 0;
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int, 4)
			};
            parameters[0].Value = id;

            SqlDataReader sr = DbHelperSQL.RunProcedure("p_update_users", parameters);
        
            sr.Close();

     
            return ret;
        }

        #endregion

        #region 存储过程

        /// <summary>
        /// 前端支付成功，更新用户状态，计算业绩，排网络
        /// </summary>
        /// <param name="billno">微信支付过来的订单号</param>
        /// <returns></returns>
        public static int p_update_users_point(int id)
        {
            int ret = 0;
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int, 4)
			};
            parameters[0].Value = id;

            SqlDataReader sr = DbHelperSQL.RunProcedure("p_update_users_point", parameters);

            sr.Close();


            return ret;
        }

        #endregion

    }
}
