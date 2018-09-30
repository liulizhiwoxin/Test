using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BuysingooShop.BLL
{
    public class systemlog
    {
        private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息
        private readonly DAL.systemlog dal;
        public systemlog()
        {
            dal = new DAL.systemlog(siteConfig.sysdatabaseprefix);
        }

        #region 基本方法==============================
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }

        /// <summary>
        /// 增加管理日志
        /// </summary>
        /// <param name="用户id"></param>
        /// <param name="用户名"></param>
        /// <param name="操作类型"></param>
        /// <param name="备注"></param>
        /// <returns></returns>
        public int Add(string user_id, string user_name, int groupId, string remark)
        {
            Model.systemlog systemlog_model = new Model.systemlog();
            systemlog_model.UserName = user_id;
            systemlog_model.UserName = user_name;
            systemlog_model.GroupId = groupId;
            systemlog_model.Description = remark;
            systemlog_model.IPAddress = Vincent._DTcms.DTRequest.GetIP();
            return dal.Add(systemlog_model);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.systemlog model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.systemlog GetModel(int id)
        {
            return dal.GetModel(id);
        }

                    
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

        /// <summary>
        /// 获得查询分页数据==》订单表+提现表
        /// </summary>
        public DataSet GetListByOrderandWithdraw(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetListByOrderandWithdraw(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }


        #endregion
    }
}
