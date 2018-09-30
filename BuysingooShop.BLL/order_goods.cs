using System;
using System.Data;
using System.Collections.Generic;
using BuysingooShop.Model;
namespace BuysingooShop.BLL
{
	/// <summary>
	/// 订单商品表
	/// </summary>
	public partial class order_goods
	{
		private readonly BuysingooShop.DAL.order_goods dal=new BuysingooShop.DAL.order_goods("dt_");
		public order_goods()
		{}
		#region  BasicMethod
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
		public int  Add(BuysingooShop.Model.order_goods model)
		{
            model.total = model.goods_price * model.quantity * model.weight;
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(BuysingooShop.Model.order_goods model)
		{
		    model.total = model.goods_price*model.quantity*model.weight;
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

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public BuysingooShop.Model.order_goods GetModel(int id)
		{
			
			return dal.GetModel(id);
		}

        /// <summary>
        /// 得到一个对象实体(通过orderID)
        /// </summary>
        public BuysingooShop.Model.order_goods GetModelorderid(int orderid)
        {

            return dal.GetModelorderid(orderid);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public BuysingooShop.Model.order_goods GetModel(string strwhere)
        {

            return dal.GetModel(strwhere);
        }
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}

        /// <summary>
        /// 获得前几行数据(订单状态)
        /// </summary>
        public DataSet GetListStatus(int Top, string strWhere, string filedOrder)
        {
            return dal.GetListStatus(Top, strWhere, filedOrder);
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
		public List<BuysingooShop.Model.order_goods> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<BuysingooShop.Model.order_goods> DataTableToList(DataTable dt)
		{
			List<BuysingooShop.Model.order_goods> modelList = new List<BuysingooShop.Model.order_goods>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				BuysingooShop.Model.order_goods model;
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


		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

