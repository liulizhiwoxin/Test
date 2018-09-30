using System;
using System.Data;
using System.Collections.Generic;
using Vincent;

namespace BuysingooShop.BLL
{
    /// <summary>
    /// 文章内容
    /// </summary>
    public partial class article
    {
        private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息
        private readonly DAL.article dal;

        public article()
        {
            dal = new DAL.article(siteConfig.sysdatabaseprefix);
        }

        #region 基本方法=============================================
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }
        /// <summary>
        /// 是否存在标题
        /// </summary>
        public bool ExistsTitle(string title)
        {
            return dal.ExistsTitle(title);
        }
          /// <summary>
        /// 是否存在标题
        /// </summary>
        public bool ExistsTitle(string title, int category_id)
        {
            return dal.ExistsTitle(title, category_id);
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string call_index)
        {
            return dal.Exists(call_index);
        }

        /// <summary>
        /// 返回信息标题
        /// </summary>
        public string GetTitle(int id)
        {
            return dal.GetTitle(id);
        }

        /// <summary>
        /// 返回品牌名称
        /// </summary>
        public string GetBrandName(int id)
        {
            int brandid =dal.GetModel(id) != null ? GetModel(id).brand_id : 0;
            return new BLL.brand().GetTitle(brandid); 
        }

        /// <summary>
        /// 获取阅读次数
        /// </summary>
        public int GetClick(int id)
        {
            return dal.GetClick(id);
        }

        /// <summary>
        /// 返回数据总数
        /// </summary>
        public int GetCount(string strWhere)
        {
            return dal.GetCount(strWhere);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.article model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            dal.UpdateField(id, strValue);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.article model)
        {
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
        /// 得到一个对象实体
        /// </summary>
        public Model.article GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.article GetModel(string call_index)
        {
            return dal.GetModel(call_index);
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得搜索产品信息
        /// </summary>
        public DataSet GetSreachPro(int Top, string strWhere, string filedOrder)
        {
            return dal.GetSreachPro(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得前几行数据dt_article_albums
        /// </summary>
        public DataSet GetListalbums(int Top, string strWhere, string filedOrder)
        {
            return dal.GetListalbums(Top, strWhere, filedOrder);
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetListqian(int Top, string strWhere, string filedOrder)
        {
            return dal.GetListqian(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetListlianhevalue(int Top, string strWhere, string filedOrder)
        {
            return dal.GetListlianhevalue(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得id,img_url,title
        /// </summary>
        public DataSet GetList1(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList1(Top, strWhere, filedOrder);
        }


        /// <summary>
        /// 获得商品详情页
        /// </summary>
        public DataSet GetList3(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList3(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得id,,title,img_url
        /// </summary>
        public DataSet GetList2(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList2(Top, strWhere, filedOrder);
        }

        /// <summary>
        /// 获取搜索匹配商品
        /// </summary>
        /// <param name="searchstr">匹配字符</param>
        /// <returns></returns>
        public string GetGoodsName(string searchstr)
        {
            return dal.GetGoodsName(searchstr);
        }

        /// <summary>
        /// 获取搜索匹配商品ID
        /// </summary>
        /// <param name="searchstr">匹配字符</param>
        /// <returns></returns>
        public string GetGoodsId(string searchstr)
        {
            return dal.GetGoodsId(searchstr);
        }
        
        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int channel_id, int category_id, int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetList(channel_id, category_id, pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList1(int channel_id, int brand_id, int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetList1(channel_id, brand_id, pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

        #endregion  Method

        #region 前台模板用到的方法===================================
        /// <summary>
        /// 根据视图显示前几条数据
        /// </summary>
        public DataSet GetList(string channel_name, int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(channel_name, Top, strWhere, filedOrder);
        }

        /// <summary>
        /// 根据视图显示前几条数据
        /// </summary>
        public DataSet GetList(string channel_name, int category_id, int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(channel_name, category_id, Top, strWhere, filedOrder);
        }

        /// <summary>
        /// 根据视图获得查询分页数据
        /// </summary>
        public DataSet GetList(string channel_name, int category_id, int pageIndex, string strWhere, string filedOrder, out int recordCount, out int pageSize)
        {
            pageSize = new channel().GetPageSize(channel_name); //自动获得频道分页数量
            return dal.GetList(channel_name, category_id, pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }
          /// <summary>
        /// 根据视图获取总记录数
        /// </summary>
        public int GetCount(string channel_name, int category_id, string strWhere)
        {
            return dal.GetCount(channel_name, category_id,strWhere);
        }

        /// <summary>
        /// 获得查询分页数据(搜索用到)
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }
        #endregion

       
        #region 转换为json的示例

        /// <summary>
        /// 获得前几行数据
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