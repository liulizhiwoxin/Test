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
        private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //���վ��������Ϣ
        private readonly DAL.article_comment dal;
		public article_comment()
		{
            dal = new DAL.article_comment(siteConfig.sysdatabaseprefix);
        }

		#region Method
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(int id)
		{
			return dal.Exists(id);
		}

        /// <summary>
        /// ������������(AJAX��ҳ�õ�)
        /// </summary>
        public int GetCount(string strWhere)
        {
            return dal.GetCount(strWhere);
        }

		/// <summary>
		/// ����һ������
		/// </summary>
		public int  Add(Model.article_comment model)
		{
			return dal.Add(model);
		}

        

        /// <summary>
        /// �޸�һ������
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            dal.UpdateField(id, strValue);
        }

		/// <summary>
		/// ����һ������
		/// </summary>
		public bool Update(Model.article_comment model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
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
		/// �õ�һ������ʵ��
		/// </summary>
		public Model.article_comment GetModel(int id)
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

        /// <summary>
        /// ��ѯ�����۵Ķ���
        /// </summary>
        /// <param name="Top"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
	    public DataSet GetMultiList(int Top, string strWhere)
	    {
            return dal.GetMultiList(Top, strWhere);
	    }

        /// <summary>
        /// ��ѯδ���۵Ķ���
        /// </summary>
        /// <param name="Top"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetMultiLists(int Top, string strWhere)
        {
            return dal.GetMultiLists(Top, strWhere);
        }

        /// <summary>
        /// ɾ����������
        /// </summary>
        /// <param name="Top"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public void DeleteListConsult(int Top, int strWhere)
        {
            dal.DeleteListConsult(Top, strWhere);
        }

        /// <summary>
        /// ��ѯ����ѯ�Ķ���
        /// </summary>
        /// <param name="Top"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetConsultList(int Top, string strWhere)
        {
            return dal.GetConsultList(Top, strWhere);
        }

	    /// <summary>
        /// ��ò�ѯ��ҳ����
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

		#endregion
	}
}

