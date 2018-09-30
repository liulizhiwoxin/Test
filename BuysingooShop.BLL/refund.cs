using System;
using System.Data;
using System.Collections.Generic;
using Vincent;

namespace BuysingooShop.BLL
{
    /// <summary>
    /// 退款表
    /// </summary>
    public partial class refund
    {
        private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息
        private DAL.refund dal;
        public refund()
        {
            dal = new DAL.refund(siteConfig.sysdatabaseprefix);
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
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string order_no)
        {
            return dal.Exists(order_no);
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        /// <param name="id">退款ID</param>
        /// <param name="user_name">用户名</param>
        /// <returns></returns>
        public bool Exists(int id, string user_name)
        {
            return dal.Exists(id, user_name);
        }

        /// <summary>
        /// 返回数据数
        /// </summary>
        public int GetCount(string strWhere)
        {
            return dal.GetCount(strWhere);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.refund model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public bool UpdateField(int id, string strValue)
        {
           return dal.UpdateField(id, strValue);
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
        public bool Update(Model.refund model)
        {
            //计算退款总金额:商品总金额+配送费用+支付手续费
            //model.order_amount = model.real_amount + model.express_fee + model.payment_fee;
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
        public Model.refund GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// 根据订单号得到一个对象实体
        /// </summary>
        public Model.refund GetorderModel(string order_no)
        {
            return dal.GetorderModel(order_no);
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
        public DataSet GetRefundList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetRefundList(Top, strWhere, filedOrder);
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetRefundList1(int Top, string strWhere, string filedOrder)
        {
            return dal.GetRefundList1(Top, strWhere, filedOrder);
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

        #endregion  Method
    }
}