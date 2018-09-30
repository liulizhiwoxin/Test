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
		private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //���վ��������Ϣ
        private readonly DAL.user_address dal;
        public user_address()
        {
            dal = new DAL.user_address(siteConfig.sysdatabaseprefix);
        }
		#region  BasicMethod
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(int id)
		{
			return dal.Exists(id);
		}

        /// <summary>
        /// �Ƿ���ڸü�¼(userid)
        /// </summary>
        public bool Existsuserid(int id)
        {
            return dal.Existsuserid(id);
        }

		/// <summary>
		/// ����һ������
		/// </summary>
		public int  Add(BuysingooShop.Model.user_address model)
		{
            model.address = model.provinces + model.citys + model.area + model.street;
			return dal.Add(model);
		}
        /// <summary>
        /// �޸�һ������
        /// </summary>
        public void UpdateField(string strValue, string strWhere)
        {
            dal.UpdateField(strValue, strWhere);
        }
        /// <summary>
        /// ����һ������
        /// </summary>
        public bool Update(BuysingooShop.Model.user_address model)
        {
            model.address = model.provinces + model.citys + model.area + model.street;
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

        public bool Delete(int id, int user_id)
        {
            return dal.Delete(id, user_id);
        }

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public BuysingooShop.Model.user_address GetModel(int id)
		{
			
			return dal.GetModel(id);
		}

        /// <summary>
        /// �õ�һ������ʵ��(����)
        /// </summary>
        public BuysingooShop.Model.user_address GetModels(int id)
        {

            return dal.GetModels(id);
        }

		/// <summary>
		/// ��������б�
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
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
		public List<BuysingooShop.Model.user_address> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
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
		/// <summary>
		/// ��ҳ��ȡ�����б�
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

        /// <summary>
        /// ��ò�ѯ��ҳ����
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

