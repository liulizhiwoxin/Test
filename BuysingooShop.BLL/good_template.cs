using System;
using System.Data;
using System.Collections.Generic;

namespace BuysingooShop.BLL
{
    /// <summary>
    /// 商品品牌业务类
    /// </summary>
    public partial class good_template
    {
        private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息
        private readonly DAL.good_template dal;
        public good_template()
        {
            dal = new DAL.good_template(siteConfig.sysdatabaseprefix);
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
        /// 返回品牌名称
        /// </summary>
        public string GetTitle(int id)
        {
            return dal.GetTitle(id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.good_template model)
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

        /// <summary>
        /// 修改数据
        /// </summary>
        public void UpdateField(string strValue, string strWhere)
        {
            dal.UpdateField(strValue, strWhere);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.good_template model)
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
        public Model.good_template GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
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
        public DataSet GetList(int category_id, int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetList(category_id, pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

        #endregion
    }
}
