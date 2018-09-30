using System;
using System.Data;
using System.Collections.Generic;
using BuysingooShop.Model;
namespace BuysingooShop.BLL
{
	/// <summary>
	/// user_address
	/// </summary>
	public partial class user_address
	{
		private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息
        private readonly DAL.user_address dal;
        public user_address()
        {
            dal = new DAL.user_address(siteConfig.sysdatabaseprefix);
        }
		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			return dal.Exists(id);
		}

        /// <summary>
        /// 是否存在该记录(userid)
        /// </summary>
        public bool Existsuserid(int id)
        {
            return dal.Existsuserid(id);
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(BuysingooShop.Model.user_address model)
		{
            model.address = model.provinces + model.citys + model.area + model.street;
			return dal.Add(model);
		}
        /// <summary>
        /// 修改一列数据
        /// </summary>
        public void UpdateField(string strValue, string strWhere)
        {
            dal.UpdateField(strValue, strWhere);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(BuysingooShop.Model.user_address model)
        {
            model.address = model.provinces + model.citys + model.area + model.street;
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
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string idlist )
		{
			return dal.DeleteList(idlist );
		}

        public bool Delete(int id, int user_id)
        {
            return dal.Delete(id, user_id);
        }

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public BuysingooShop.Model.user_address GetModel(int id)
		{
			
			return dal.GetModel(id);
		}

        /// <summary>
        /// 得到一个对象实体(特殊)
        /// </summary>
        public BuysingooShop.Model.user_address GetModels(int id)
        {

            return dal.GetModels(id);
        }

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<BuysingooShop.Model.user_address> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<BuysingooShop.Model.user_address> DataTableToList(DataTable dt)
		{
			List<BuysingooShop.Model.user_address> modelList = new List<BuysingooShop.Model.user_address>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				BuysingooShop.Model.user_address model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = dal.DataRowToModel(dt.Rows[n]);
					if (model != null)
					{
						modelList.Add(model);
					}
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			return dal.GetRecordCount(strWhere);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

