using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuysingooShop.BLL
{
    /// <summary>
    /// 小程序 
    /// </summary>
    public partial class applet_user
    {
        
        private readonly DAL.applet_user dal;
        public applet_user()
        {
            dal = new DAL.applet_user("dt_");
        }


        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }

        /// <summary>
        /// 根据openid是否存在该记录
        /// </summary>
        public bool Exists_openid(string openid)
        {
            return dal.Exists_openid(openid);
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(Model.applet_user model)
        {
            return dal.Add(model);
        }

        // <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top,strWhere,filedOrder);
        }

        /// <summary>
        /// 根据openid获取火苗数
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="filedOrder"></param>
        /// <returns></returns>
        public double GetList_user_kindling_num(string strWhere, string filedOrder)
        {
            return dal.GetList_user_kindling_num(strWhere,filedOrder);
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="set"> 修改语句</param>
        /// <param name="where">修改的条件</param>
        /// <returns>true/false</returns>
        public bool Update(string set, string where)
        {
            return dal.Update(set,where);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            return dal.Delete(id);
        }
    }
}
