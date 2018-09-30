using System;
using System.Data;
using System.Collections.Generic;
using Vincent;

namespace BuysingooShop.BLL
{
    /// <summary>
    /// ������
    /// </summary>
    public partial class orders
    {
        private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //���վ��������Ϣ
        private DAL.orders dal;
        public orders()
        {
            dal = new DAL.orders(siteConfig.sysdatabaseprefix);
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
        /// ��ȡ�Ŷӽ��׽���-��ͳ��  
        /// </summary>
        /// <returns></returns>
        public DataSet GetTeamTotalByMonth(int userid)
        { return dal.GetTeamTotalByMonth(userid); }
        /// <summary>
        /// ��ȡ�Ŷӽ��׽���-��ͳ����ϸ��Ϣ
        /// </summary>
        /// <returns></returns>
        public DataSet GetTeamTotalByMonthDetails(string month, string year, int userid)
        { return dal.GetTeamTotalByMonthDetails(month, year, userid); }

        /// <summary>
        /// ��ȡ�ۻ����׽�����ɶ�����
        /// </summary>
        /// <returns></returns>
        public DataSet GetSellTotal()
        {
            return dal.GetSellTotal();
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
        /// <param name="id">����ID</param>
        /// <param name="user_name">�û���</param>
        /// <returns></returns>
        public bool Exists(int id, string user_name)
        {
            return dal.Exists(id, user_name);
        }

        /// <summary>
        /// ���ݶ����Ż�ȡƷ��
        /// </summary>
        /// <param name="order_no"></param>
        /// <returns></returns>
        public string GetBrandName(string order_no)
        {
            BuysingooShop.Model.orders or_model = GetModel(order_no);
            return new BuysingooShop.BLL.brand().GetTitle(or_model==null?0:or_model.brand_id);
        }

        /// <summary>
        /// ����������
        /// </summary>
        public int GetCount(string strWhere)
        {
            return dal.GetCount(strWhere);
        }
        /// <summary>
        /// �@ȡ�³ɽ�����
        /// </summary>
        /// <returns></returns>
        public int GetMonthSellCount()
        {
            return dal.GetMonthSellCount();
        }
        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(Model.orders model)
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
        public bool UpdateField_byid(int id, string strValue)
        {
            return dal.UpdateField_byid(id, strValue);
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
        public bool Update(Model.orders model)
        {
            //���㶩���ܽ��:��Ʒ�ܽ��+���ͷ���+֧��������
            model.order_amount = model.real_amount + model.express_fee + model.payment_fee;
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
        public Model.orders GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// �õ�һ������ʵ��(ͨ���û�id)
        /// </summary>
        public Model.orders GetModelUser(int id)
        {
            return dal.GetModelUser(id);
        }

        /// <summary>
        /// �õ�һ������ʵ�壨�����û�id��
        /// </summary>
        public Model.orders GetModelUserId(int id)
        {
            return dal.GetModelUserId(id);
        }

        /// <summary>
        /// ���ݶ����ŷ���һ��ʵ��
        /// </summary>
        public Model.orders GetModel(string order_no)
        {
            return dal.GetModel(order_no);
        }

        /// <summary>
        /// �����û�ID����һ��ʵ��
        /// </summary>
        public Model.orders GetModelUserid(int userid)
        {
            return dal.GetModelUserid(userid);
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
        public DataSet GetOrderLists(int Top, string strWhere, string filedOrder)
        {
            return dal.GetOrderLists(Top, strWhere, filedOrder);
        }

        /// <summary>
        /// ��ò�ѯ��ҳ����
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

        /// <summary>
        /// ��ò�ѯ��ҳ����
        /// </summary>
        public DataSet GetList1(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetList1(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }   
            /// <summary>
        ///  ��GetList1��ѯ��ҳ���ݡ������������Ӳ�ѯһ���û��� 
            /// </summary>
            /// <param name="pageSize"></param>
            /// <param name="pageIndex"></param>
            /// <param name="strWhere"></param>
            /// <param name="filedOrder"></param>
            /// <param name="recordCount"></param>
            /// <returns></returns>
        public DataSet GetList1andaddnickname(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetList1andaddnickname(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }
        /// <summary>
        /// ���ǰ��������
        /// </summary>
        public DataSet GetMultiList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetMultiList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// ���ط���վվ�����߶���
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet GetsShareUserOrder(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetsShareUserOrder(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

        /// <summary>
        /// ���ط���վ����ͨ������ռid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet Getoutlet_fuwuzhanbystoreid(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.Getoutlet_fuwuzhanbystoreid(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }
        /// <summary>
        /// ���ط���վ����
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet Getoutlet_fuwuzhan(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.Getoutlet_fuwuzhan(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

        /// <summary>
        /// ���ǰ��������
        /// </summary>
        public DataSet GetOrderList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetOrderList(Top, strWhere, filedOrder);
        }

        /// <summary>
        /// ��ö������ݲ�ѯ�������
        /// </summary>
        public DataSet GetOrderAmount(int Top, string strWhere, string filedOrder)
        {
            return dal.GetOrderAmount(Top, strWhere, filedOrder);
        }

        /// <summary>
        /// ����˻�����
        /// </summary>
        public DataSet GetrefundList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetrefundList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// ��ö�����������
        /// </summary>
        public DataSet Getorderinfo(int Top, string strWhere, string filedOrder)
        {
            return dal.Getorderinfo(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// ��ò�ѯ��ҳ����
        /// </summary>
        public DataSet GetMultiList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetMultiList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

        /// <summary>
        /// ���ս����ܽ��
        /// </summary>
        /// <returns></returns>
        public string GetBalanceTotalDay()
        {
            return dal.GetBalanceTotalDay();
        }

        /// <summary>
        /// �����ܽ��
        /// </summary>
        /// <returns></returns>
        public string GetBalanceTotalAll()
        {
            return dal.GetBalanceTotalAll();
        }

        /// <summary>
        /// ����������ϸ
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="strWhere"></param>
        /// <param name="filedOrder"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public DataSet GetUserBalanceList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetUserBalanceList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }


        #endregion  Method
    }
}