using System;
using System.Data;
using System.Collections.Generic;

namespace BuysingooShop.BLL
{
    /// <summary>
    /// 预存款记录日志
    /// </summary>
    public partial class user_coupon_log
    {
        private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息
        private readonly DAL.user_coupon_log dal;
        public user_coupon_log()
        {
            dal = new DAL.user_coupon_log(siteConfig.sysdatabaseprefix);
        }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.user_coupon_log model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public int UpdateField(int id, string strValue)
        {
            return dal.UpdateField(id, strValue);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.user_coupon_log model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            return dal.Delete(id);
        }
        public bool Delete(int id, string user_name)
        {
            return dal.Delete(id,user_name);
        }
        
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.user_coupon_log GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.user_coupon_log GetModel(string str_code)
        {
            return dal.GetModel(str_code);
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
        public DataSet GetList1(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList1(Top, strWhere, filedOrder);
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

        /// <summary>
        /// 获得多表分页数据
        /// </summary>
        public DataSet GetMultiList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetMultiList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

        /// <summary>
        /// 获得多表分页数据
        /// </summary>
        public DataSet GetList1(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetList1(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

        #endregion  Method
    }
}