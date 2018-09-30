using BuysingooShop.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BuysingooShop.DAL
{
    /// <summary>
    /// 系统日志
    /// </summary>
    public class systemlog
    {
        private string databaseprefix; //数据库表名前缀
        public systemlog(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }

        #region 基本方法==============================
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "systemlog");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 返回数据数
        /// </summary>
        public int GetCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H from " + databaseprefix + "systemlog ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.systemlog model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "systemlog(");
            strSql.Append("user_id,user_name,action_type,remark,user_ip)");
            strSql.Append(" values (");
            strSql.Append("@user_id,@user_name,@action_type,@remark,@user_ip)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@user_id", SqlDbType.Int,4),
					new SqlParameter("@user_name", SqlDbType.NVarChar,100),
					new SqlParameter("@action_type", SqlDbType.NVarChar,100),
					new SqlParameter("@remark", SqlDbType.NVarChar,255),
					new SqlParameter("@user_ip", SqlDbType.NVarChar,30)
					};
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.UserName;
            parameters[2].Value = model.GroupId;
            parameters[3].Value = model.Description;
            parameters[4].Value = model.IPAddress;
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.systemlog GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from " + databaseprefix + "systemlog ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            Model.systemlog model = new Model.systemlog();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                
                model.UserID = ds.Tables[0].Rows[0]["UserID"].ToString();
                
                model.UserName = ds.Tables[0].Rows[0]["user_name"].ToString();
                if (ds.Tables[0].Rows[0]["TypeId"].ToString() != "")
                {
                    model.TypeId = int.Parse(ds.Tables[0].Rows[0]["TypeId"].ToString());
                }

                if (ds.Tables[0].Rows[0]["GroupId"].ToString() != "")
                {
                    model.GroupId = int.Parse(ds.Tables[0].Rows[0]["GroupId"].ToString());
                }

                model.Description = ds.Tables[0].Rows[0]["Description"].ToString();
                model.Value = ds.Tables[0].Rows[0]["Value"].ToString();
                model.IPAddress = ds.Tables[0].Rows[0]["IPAddress"].ToString(); 

                if (ds.Tables[0].Rows[0]["DateTime"].ToString() != "")
                {
                    model.DateTime = DateTime.Parse(ds.Tables[0].Rows[0]["DateTime"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }
     

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM " + databaseprefix + "systemlog ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM v_systemlog");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(Vincent._DTcms.PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(Vincent._DTcms.PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }

        /// <summary>
        /// 获得查询分页数据==》订单表+提现表
        /// </summary>
        public DataSet GetListByOrderandWithdraw(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  t1.user_name,t1.id,t1.order_no,t1.user_id,t1.payment_id,t1.payment_fee,t1.payment_status,t1.express_id,t1.express_status,t1.real_amount,t1.order_amount ,t1.add_time,t2.amount,t2.addtime,t2.openid,t2.status  from dt_orders t1 left join dt_withdraw t2 on t1.user_id=t2.user_id");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where   " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(Vincent._DTcms.PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(Vincent._DTcms.PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }

        #endregion
    }
}
