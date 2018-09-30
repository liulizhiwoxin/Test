using System;
using System.Data;
using System.Collections.Generic;

namespace BuysingooShop.BLL
{
    /// <summary>
    /// 预存款记录日志
    /// </summary>
    public partial class user_coupon
    {
        private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息
        private readonly DAL.user_coupon dal;
        public user_coupon()
        {
            dal = new DAL.user_coupon(siteConfig.sysdatabaseprefix);
        }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string code)
        {
            return dal.Exists(code);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.user_coupon model)
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
        public bool Update(Model.user_coupon model)
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
        
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.user_coupon GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.user_coupon GetModel(string strwhere)
        {
            return dal.GetModel(strwhere);
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
        public DataSet GetList(string strWhere, string filedOrder)
        {
            return dal.GetList(strWhere,filedOrder);
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