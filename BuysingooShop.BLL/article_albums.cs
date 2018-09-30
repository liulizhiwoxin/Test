using System;
using System.Data;
using System.Collections.Generic;
using BuysingooShop.Model;
namespace BuysingooShop.BLL
{
	/// <summary>
	/// 图片相册
	/// </summary>
	public partial class article_albums
	{
        private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息
        private readonly BuysingooShop.DAL.article_albums dal;

        public article_albums()
        {
            dal = new DAL.article_albums(siteConfig.sysdatabaseprefix);
        }

		#region  BasicMethod
        ///// <summary>
        ///// 是否存在该记录
        ///// </summary>
        //public bool Exists(int id)
        //{
        //    return dal.Exists(id);
        //}

        ///// <summary>
        ///// 增加一条数据
        ///// </summary>
        //public int  Add(BuysingooShop.Model.article_albums model)
        //{
        //    return dal.Add(model);
        //}

        ///// <summary>
        ///// 更新一条数据
        ///// </summary>
        //public bool Update(BuysingooShop.Model.article_albums model)
        //{
        //    return dal.Update(model);
        //}

        ///// <summary>
        ///// 删除一条数据
        ///// </summary>
        //public bool Delete(int id)
        //{
			
        //    return dal.Delete(id);
        //}
		/// <summary>
		/// 删除一条数据
		/// </summary>
        //public bool DeleteList(string idlist )
        //{
        //    return dal.DeleteList(idlist );
        //}

        ///// <summary>
        ///// 得到一个对象实体
        ///// </summary>
        //public BuysingooShop.Model.article_albums GetModel(int id)
        //{
			
        //    return dal.GetModel(id);
        //}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}

        /// <summary>
        /// 返回对象实体
        /// </summary>
        public Model.article_albums GetModel(int id)
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
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        //public List<BuysingooShop.Model.article_albums> GetModelList(string strWhere)
        //{
        //    DataSet ds = dal.GetList(strWhere);
        //    return DataTableToList(ds.Tables[0]);
        //}
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        //public List<BuysingooShop.Model.article_albums> DataTableToList(DataTable dt)
        //{
        //    List<BuysingooShop.Model.article_albums> modelList = new List<BuysingooShop.Model.article_albums>();
        //    int rowsCount = dt.Rows.Count;
        //    if (rowsCount > 0)
        //    {
        //        BuysingooShop.Model.article_albums model;
        //        for (int n = 0; n < rowsCount; n++)
        //        {
        //            model = dal.DataRowToModel(dt.Rows[n]);
        //            if (model != null)
        //            {
        //                modelList.Add(model);
        //            }
        //        }
        //    }
        //    return modelList;
        //}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

        ///// <summary>
        ///// 分页获取数据列表
        ///// </summary>
        //public int GetRecordCount(string strWhere)
        //{
        //    return dal.GetRecordCount(strWhere);
        //}
        ///// <summary>
        ///// 分页获取数据列表
        ///// </summary>
        //public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        //{
        //    return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
        //}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

