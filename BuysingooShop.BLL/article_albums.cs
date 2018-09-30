using System;
using System.Data;
using System.Collections.Generic;
using BuysingooShop.Model;
namespace BuysingooShop.BLL
{
	/// <summary>
	/// ͼƬ���
	/// </summary>
	public partial class article_albums
	{
        private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //���վ��������Ϣ
        private readonly BuysingooShop.DAL.article_albums dal;

        public article_albums()
        {
            dal = new DAL.article_albums(siteConfig.sysdatabaseprefix);
        }

		#region  BasicMethod
        ///// <summary>
        ///// �Ƿ���ڸü�¼
        ///// </summary>
        //public bool Exists(int id)
        //{
        //    return dal.Exists(id);
        //}

        ///// <summary>
        ///// ����һ������
        ///// </summary>
        //public int  Add(BuysingooShop.Model.article_albums model)
        //{
        //    return dal.Add(model);
        //}

        ///// <summary>
        ///// ����һ������
        ///// </summary>
        //public bool Update(BuysingooShop.Model.article_albums model)
        //{
        //    return dal.Update(model);
        //}

        ///// <summary>
        ///// ɾ��һ������
        ///// </summary>
        //public bool Delete(int id)
        //{
			
        //    return dal.Delete(id);
        //}
		/// <summary>
		/// ɾ��һ������
		/// </summary>
        //public bool DeleteList(string idlist )
        //{
        //    return dal.DeleteList(idlist );
        //}

        ///// <summary>
        ///// �õ�һ������ʵ��
        ///// </summary>
        //public BuysingooShop.Model.article_albums GetModel(int id)
        //{
			
        //    return dal.GetModel(id);
        //}

		/// <summary>
		/// ��������б�
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}

        /// <summary>
        /// ���ض���ʵ��
        /// </summary>
        public Model.article_albums GetModel(int id)
        {
            return dal.GetModel(id);
        }

		/// <summary>
		/// ���ǰ��������
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
        ///// <summary>
        ///// ��������б�
        ///// </summary>
        //public List<BuysingooShop.Model.article_albums> GetModelList(string strWhere)
        //{
        //    DataSet ds = dal.GetList(strWhere);
        //    return DataTableToList(ds.Tables[0]);
        //}
        ///// <summary>
        ///// ��������б�
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
		/// ��������б�
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

        ///// <summary>
        ///// ��ҳ��ȡ�����б�
        ///// </summary>
        //public int GetRecordCount(string strWhere)
        //{
        //    return dal.GetRecordCount(strWhere);
        //}
        ///// <summary>
        ///// ��ҳ��ȡ�����б�
        ///// </summary>
        //public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        //{
        //    return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
        //}
		/// <summary>
		/// ��ҳ��ȡ�����б�
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

