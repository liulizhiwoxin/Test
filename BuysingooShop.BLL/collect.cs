using System;
using System.Data;
using System.Collections.Generic;
using Vincent;

namespace BuysingooShop.BLL
{
    public partial class collect
    {
        private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息
        private readonly DAL.collect dal;
        public collect()
        {
            dal = new DAL.collect(siteConfig.sysdatabaseprefix);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.collect model)
        {
            return dal.Add(model);
        }


        /// <summary>
        /// 检查记录是否存在
        /// </summary>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            return dal.Delete(id);
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
    }
}
