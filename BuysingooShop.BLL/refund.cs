using System;
using System.Data;
using System.Collections.Generic;
using Vincent;

namespace BuysingooShop.BLL
{
    /// <summary>
    /// �˿��
    /// </summary>
    public partial class refund
    {
        private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //���վ��������Ϣ
        private DAL.refund dal;
        public refund()
        {
            dal = new DAL.refund(siteConfig.sysdatabaseprefix);
        }

        #region ��������
        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(string order_no)
        {
            return dal.Exists(order_no);
        }

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        /// <param name="id">�˿�ID</param>
        /// <param name="user_name">�û���</param>
        /// <returns></returns>
        public bool Exists(int id, string user_name)
        {
            return dal.Exists(id, user_name);
        }

        /// <summary>
        /// ����������
        /// </summary>
        public int GetCount(string strWhere)
        {
            return dal.GetCount(strWhere);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(Model.refund model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// �޸�һ������
        /// </summary>
        public bool UpdateField(int id, string strValue)
        {
           return dal.UpdateField(id, strValue);
        }

        /// <summary>
        /// �޸�һ������
        /// </summary>
        public bool UpdateField(string order_no, string strValue)
        {
            return dal.UpdateField(order_no, strValue);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public bool Update(Model.refund model)
        {
            //�����˿��ܽ��:��Ʒ�ܽ��+���ͷ���+֧��������
            //model.order_amount = model.real_amount + model.express_fee + model.payment_fee;
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
        /// �õ�һ������ʵ��
        /// </summary>
        public Model.refund GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// ���ݶ����ŵõ�һ������ʵ��
        /// </summary>
        public Model.refund GetorderModel(string order_no)
        {
            return dal.GetorderModel(order_no);
        }

        /// <summary>
        /// ���ǰ��������
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }

        /// <summary>
        /// ���ǰ��������
        /// </summary>
        public DataSet GetRefundList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetRefundList(Top, strWhere, filedOrder);
        }

        /// <summary>
        /// ���ǰ��������
        /// </summary>
        public DataSet GetRefundList1(int Top, string strWhere, string filedOrder)
        {
            return dal.GetRefundList1(Top, strWhere, filedOrder);
        }

        /// <summary>
        /// ��ò�ѯ��ҳ����
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

        #endregion  Method
    }
}