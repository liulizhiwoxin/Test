using System;
using System.Data;
using System.Collections.Generic;

namespace BuysingooShop.BLL
{
	/// <summary>
	/// article_comment
	/// </summary>
	public partial class article_comment
	{
        private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息
        private readonly DAL.article_comment dal;
		public article_comment()
		{
            dal = new DAL.article_comment(siteConfig.sysdatabaseprefix);
        }

		#region Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			return dal.Exists(id);
		}

        /// <summary>
        /// 返回数据总数(AJAX分页用到)
        /// </summary>
        public int GetCount(string strWhere)
        {
            return dal.GetCount(strWhere);
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Model.article_comment model)
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
		/// 更新一条数据
		/// </summary>
		public bool Update(Model.article_comment model)
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
            return dal.Delete(id, user_name);
        }

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Model.article_comment GetModel(int id)
		{
			return dal.GetModel(id);
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}

        /// <summary>
        /// 查询已评价的订单
        /// </summary>
        /// <param name="Top"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
	    public DataSet GetMultiList(int Top, string strWhere)
	    {
            return dal.GetMultiList(Top, strWhere);
	    }

        /// <summary>
        /// 查询未评价的订单
        /// </summary>
        /// <param name="Top"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetMultiLists(int Top, string strWhere)
        {
            return dal.GetMultiLists(Top, strWhere);
        }

        /// <summary>
        /// 删除订单评论
        /// </summary>
        /// <param name="Top"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public void DeleteListConsult(int Top, int strWhere)
        {
            dal.DeleteListConsult(Top, strWhere);
        }

        /// <summary>
        /// 查询已咨询的订单
        /// </summary>
        /// <param name="Top"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetConsultList(int Top, string strWhere)
        {
            return dal.GetConsultList(Top, strWhere);
        }

	    /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

		#endregion
	}
}

