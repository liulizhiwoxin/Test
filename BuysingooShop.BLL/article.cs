using System;
using System.Data;
using System.Collections.Generic;
using Vincent;

namespace BuysingooShop.BLL
{
    /// <summary>
    /// ��������
    /// </summary>
    public partial class article
    {
        private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //���վ��������Ϣ
        private readonly DAL.article dal;

        public article()
        {
            dal = new DAL.article(siteConfig.sysdatabaseprefix);
        }

        #region ��������=============================================
        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }
        /// <summary>
        /// �Ƿ���ڱ���
        /// </summary>
        public bool ExistsTitle(string title)
        {
            return dal.ExistsTitle(title);
        }
          /// <summary>
        /// �Ƿ���ڱ���
        /// </summary>
        public bool ExistsTitle(string title, int category_id)
        {
            return dal.ExistsTitle(title, category_id);
        }
        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(string call_index)
        {
            return dal.Exists(call_index);
        }

        /// <summary>
        /// ������Ϣ����
        /// </summary>
        public string GetTitle(int id)
        {
            return dal.GetTitle(id);
        }

        /// <summary>
        /// ����Ʒ������
        /// </summary>
        public string GetBrandName(int id)
        {
            int brandid =dal.GetModel(id) != null ? GetModel(id).brand_id : 0;
            return new BLL.brand().GetTitle(brandid); 
        }

        /// <summary>
        /// ��ȡ�Ķ�����
        /// </summary>
        public int GetClick(int id)
        {
            return dal.GetClick(id);
        }

        /// <summary>
        /// ������������
        /// </summary>
        public int GetCount(string strWhere)
        {
            return dal.GetCount(strWhere);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(Model.article model)
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
        public bool Update(Model.article model)
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

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public Model.article GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public Model.article GetModel(string call_index)
        {
            return dal.GetModel(call_index);
        }

        /// <summary>
        /// ���ǰ��������
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// ���������Ʒ��Ϣ
        /// </summary>
        public DataSet GetSreachPro(int Top, string strWhere, string filedOrder)
        {
            return dal.GetSreachPro(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// ���ǰ��������dt_article_albums
        /// </summary>
        public DataSet GetListalbums(int Top, string strWhere, string filedOrder)
        {
            return dal.GetListalbums(Top, strWhere, filedOrder);
        }

        /// <summary>
        /// ���ǰ��������
        /// </summary>
        public DataSet GetListqian(int Top, string strWhere, string filedOrder)
        {
            return dal.GetListqian(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// ���ǰ��������
        /// </summary>
        public DataSet GetListlianhevalue(int Top, string strWhere, string filedOrder)
        {
            return dal.GetListlianhevalue(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// ���id,img_url,title
        /// </summary>
        public DataSet GetList1(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList1(Top, strWhere, filedOrder);
        }


        /// <summary>
        /// �����Ʒ����ҳ
        /// </summary>
        public DataSet GetList3(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList3(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// ���id,,title,img_url
        /// </summary>
        public DataSet GetList2(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList2(Top, strWhere, filedOrder);
        }

        /// <summary>
        /// ��ȡ����ƥ����Ʒ
        /// </summary>
        /// <param name="searchstr">ƥ���ַ�</param>
        /// <returns></returns>
        public string GetGoodsName(string searchstr)
        {
            return dal.GetGoodsName(searchstr);
        }

        /// <summary>
        /// ��ȡ����ƥ����ƷID
        /// </summary>
        /// <param name="searchstr">ƥ���ַ�</param>
        /// <returns></returns>
        public string GetGoodsId(string searchstr)
        {
            return dal.GetGoodsId(searchstr);
        }
        
        /// <summary>
        /// ��ò�ѯ��ҳ����
        /// </summary>
        public DataSet GetList(int channel_id, int category_id, int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetList(channel_id, category_id, pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

        /// <summary>
        /// ��ò�ѯ��ҳ����
        /// </summary>
        public DataSet GetList1(int channel_id, int brand_id, int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetList1(channel_id, brand_id, pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

        #endregion  Method

        #region ǰ̨ģ���õ��ķ���===================================
        /// <summary>
        /// ������ͼ��ʾǰ��������
        /// </summary>
        public DataSet GetList(string channel_name, int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(channel_name, Top, strWhere, filedOrder);
        }

        /// <summary>
        /// ������ͼ��ʾǰ��������
        /// </summary>
        public DataSet GetList(string channel_name, int category_id, int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(channel_name, category_id, Top, strWhere, filedOrder);
        }

        /// <summary>
        /// ������ͼ��ò�ѯ��ҳ����
        /// </summary>
        public DataSet GetList(string channel_name, int category_id, int pageIndex, string strWhere, string filedOrder, out int recordCount, out int pageSize)
        {
            pageSize = new channel().GetPageSize(channel_name); //�Զ����Ƶ����ҳ����
            return dal.GetList(channel_name, category_id, pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }
          /// <summary>
        /// ������ͼ��ȡ�ܼ�¼��
        /// </summary>
        public int GetCount(string channel_name, int category_id, string strWhere)
        {
            return dal.GetCount(channel_name, category_id,strWhere);
        }

        /// <summary>
        /// ��ò�ѯ��ҳ����(�����õ�)
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }
        #endregion

       
        #region ת��Ϊjson��ʾ��

        /// <summary>
        /// ���ǰ��������
        /// </summary>
        public string GetListByJson(int Top, string strWhere, string filedOrder)
        {
            var ds = GetList(Top, strWhere, filedOrder);

            DataTable dt = null;
            if (ds != null && ds.Tables.Count > 0)
                dt = ds.Tables[0];

            _Json json = new _Json(dt);
            return "{record:" + json.ToJson() + "}";
        }


        #endregion
    }
}