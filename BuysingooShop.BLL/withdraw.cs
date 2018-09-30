using System;
using System.Data;
using System.Collections.Generic;
using Vincent;

namespace BuysingooShop.BLL
{
    public partial class withdraw
    {
        private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息
        private readonly DAL.withdraw dal;

        public withdraw()
        {
            dal = new DAL.withdraw(siteConfig.sysdatabaseprefix);
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
        /// 增加一条数据
        /// </summary>
        public int Add(Model.withdraw model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.withdraw model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.withdraw GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetLists(int Top, string strWhere, string filedOrder)
        {
            return dal.GetLists(Top, strWhere, filedOrder);
        }
        #endregion

    }
}
