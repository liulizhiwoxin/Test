using System;
using System.Data;
using System.Collections.Generic;
using BuysingooShop.Model;
namespace BuysingooShop.BLL
{
	/// <summary>
	/// ������Ʒ��
	/// </summary>
	public partial class order_goods
	{
		private readonly BuysingooShop.DAL.order_goods dal=new BuysingooShop.DAL.order_goods("dt_");
		public order_goods()
		{}
		#region  BasicMethod
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(int id)
		{
			return dal.Exists(id);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public int  Add(BuysingooShop.Model.order_goods model)
		{
            model.total = model.goods_price * model.quantity * model.weight;
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public bool Update(BuysingooShop.Model.order_goods model)
		{
		    model.total = model.goods_price*model.quantity*model.weight;
			return dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public bool Delete(int id)
		{
			
			return dal.Delete(id);
		}
		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public bool DeleteList(string idlist )
		{
			return dal.DeleteList(idlist );
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public BuysingooShop.Model.order_goods GetModel(int id)
		{
			
			return dal.GetModel(id);
		}

        /// <summary>
        /// �õ�һ������ʵ��(ͨ��orderID)
        /// </summary>
        public BuysingooShop.Model.order_goods GetModelorderid(int orderid)
        {

            return dal.GetModelorderid(orderid);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public BuysingooShop.Model.order_goods GetModel(string strwhere)
        {

            return dal.GetModel(strwhere);
        }
		/// <summary>
		/// ��������б�
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}

        /// <summary>
        /// ���ǰ��������(����״̬)
        /// </summary>
        public DataSet GetListStatus(int Top, string strWhere, string filedOrder)
        {
            return dal.GetListStatus(Top, strWhere, filedOrder);
        }

		/// <summary>
		/// ���ǰ��������
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<BuysingooShop.Model.order_goods> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
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
		/// ��������б�
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// ��ҳ��ȡ�����б�
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			return dal.GetRecordCount(strWhere);
		}
		/// <summary>
		/// ��ҳ��ȡ�����б�
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

