using System;
using System.Data;
using System.Collections.Generic;
using Vincent;

namespace BuysingooShop.BLL
{
    /// <summary>
    /// 订单表
    /// </summary>
    public partial class orders
    {
        private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息
        private DAL.orders dal;
        public orders()
        {
            dal = new DAL.orders(siteConfig.sysdatabaseprefix);
        }

        #region 基本方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }
         /// <summary>
        /// 获取团队交易金额按月-年统计  
        /// </summary>
        /// <returns></returns>
        public DataSet GetTeamTotalByMonth(int userid)
        { return dal.GetTeamTotalByMonth(userid); }
        /// <summary>
        /// 获取团队交易金额按月-年统计详细信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetTeamTotalByMonthDetails(string month, string year, int userid)
        { return dal.GetTeamTotalByMonthDetails(month, year, userid); }

        /// <summary>
        /// 获取累积交易金额（已完成订单）
        /// </summary>
        /// <returns></returns>
        public DataSet GetSellTotal()
        {
            return dal.GetSellTotal();
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string order_no)
        {
            return dal.Exists(order_no);
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        /// <param name="id">订单ID</param>
        /// <param name="user_name">用户名</param>
        /// <returns></returns>
        public bool Exists(int id, string user_name)
        {
            return dal.Exists(id, user_name);
        }

        /// <summary>
        /// 根据订单号获取品牌
        /// </summary>
        /// <param name="order_no"></param>
        /// <returns></returns>
        public string GetBrandName(string order_no)
        {
            BuysingooShop.Model.orders or_model = GetModel(order_no);
            return new BuysingooShop.BLL.brand().GetTitle(or_model==null?0:or_model.brand_id);
        }

        /// <summary>
        /// 返回数据数
        /// </summary>
        public int GetCount(string strWhere)
        {
            return dal.GetCount(strWhere);
        }
        /// <summary>
        /// 獲取月成交數量
        /// </summary>
        /// <returns></returns>
        public int GetMonthSellCount()
        {
            return dal.GetMonthSellCount();
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.orders model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            dal.UpdateField(id, strValue);
        }
        public bool UpdateField_byid(int id, string strValue)
        {
            return dal.UpdateField_byid(id, strValue);
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public bool UpdateField(string order_no, string strValue)
        {
            return dal.UpdateField(order_no, strValue);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.orders model)
        {
            //计算订单总金额:商品总金额+配送费用+支付手续费
            model.order_amount = model.real_amount + model.express_fee + model.payment_fee;
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            return dal.Delete(id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.orders GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// 得到一个对象实体(通过用户id)
        /// </summary>
        public Model.orders GetModelUser(int id)
        {
            return dal.GetModelUser(id);
        }

        /// <summary>
        /// 得到一个对象实体（根据用户id）
        /// </summary>
        public Model.orders GetModelUserId(int id)
        {
            return dal.GetModelUserId(id);
        }

        /// <summary>
        /// 根据订单号返回一个实体
        /// </summary>
        public Model.orders GetModel(string order_no)
        {
            return dal.GetModel(order_no);
        }

        /// <summary>
        /// 根据用户ID返回一个实体
        /// </summary>
        public Model.orders GetModelUserid(int userid)
        {
            return dal.GetModelUserid(userid);
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetOrderLists(int Top, string strWhere, string filedOrder)
        {
            return dal.GetOrderLists(Top, strWhere, filedOrder);
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList1(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetList1(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }   
            /// <summary>
        ///  由GetList1查询分页数据——延伸新增加查询一列用户名 
            /// </summary>
            /// <param name="pageSize"></param>
            /// <param name="pageIndex"></param>
            /// <param name="strWhere"></param>
            /// <param name="filedOrder"></param>
            /// <param name="recordCount"></param>
            /// <returns></returns>
        public DataSet GetList1andaddnickname(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetList1andaddnickname(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetMultiList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetMultiList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 返回服务站站长下线订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet GetsShareUserOrder(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetsShareUserOrder(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

        /// <summary>
        /// 返回服务站订单通过服务占id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet Getoutlet_fuwuzhanbystoreid(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.Getoutlet_fuwuzhanbystoreid(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }
        /// <summary>
        /// 返回服务站订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet Getoutlet_fuwuzhan(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.Getoutlet_fuwuzhan(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetOrderList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetOrderList(Top, strWhere, filedOrder);
        }

        /// <summary>
        /// 获得订单数据查询订单金额
        /// </summary>
        public DataSet GetOrderAmount(int Top, string strWhere, string filedOrder)
        {
            return dal.GetOrderAmount(Top, strWhere, filedOrder);
        }

        /// <summary>
        /// 获得退货订单
        /// </summary>
        public DataSet GetrefundList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetrefundList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得订单所有数据
        /// </summary>
        public DataSet Getorderinfo(int Top, string strWhere, string filedOrder)
        {
            return dal.Getorderinfo(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetMultiList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetMultiList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

        /// <summary>
        /// 今日奖池总金额
        /// </summary>
        /// <returns></returns>
        public string GetBalanceTotalDay()
        {
            return dal.GetBalanceTotalDay();
        }

        /// <summary>
        /// 奖池总金额
        /// </summary>
        /// <returns></returns>
        public string GetBalanceTotalAll()
        {
            return dal.GetBalanceTotalAll();
        }

        /// <summary>
        /// 奖金收入明细
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="strWhere"></param>
        /// <param name="filedOrder"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public DataSet GetUserBalanceList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetUserBalanceList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }


        #endregion  Method
    }
}