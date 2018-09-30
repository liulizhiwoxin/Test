using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using BuysingooShop.DBUtility;
using Vincent;

namespace BuysingooShop.DAL
{
    /// <summary>
    /// 预存款记录日志
    /// </summary>
    public partial class user_coupon_log
    {
        private string databaseprefix; //数据库表名前缀
        public user_coupon_log(string _databaseprefix)
        { 
            databaseprefix = _databaseprefix; 
        }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "user_coupon_log");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.user_coupon_log model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "user_coupon_log(");
            strSql.Append("user_id,user_name,coupon_id,str_code,order_id,order_no,add_time,use_time,status)");
            strSql.Append(" values (");
            strSql.Append("@user_id,@user_name,@coupon_id,@str_code,@order_id,@order_no,@add_time,@use_time,@status)");
            strSql.Append(";set @ReturnValue= @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@user_id", SqlDbType.Int,4),
					new SqlParameter("@user_name", SqlDbType.NVarChar,255),
                    new SqlParameter("@coupon_id", SqlDbType.Int,4),
                    new SqlParameter("@str_code", SqlDbType.NVarChar,255),
                    new SqlParameter("@order_id", SqlDbType.Int,4),
                    new SqlParameter("@order_no", SqlDbType.NVarChar,100),
					new SqlParameter("@add_time", SqlDbType.DateTime),
                    new SqlParameter("@use_time", SqlDbType.DateTime),
                    new SqlParameter("@status", SqlDbType.TinyInt,1),new SqlParameter("@ReturnValue",SqlDbType.Int)};
            parameters[0].Value = model.user_id;
            parameters[1].Value = model.user_name;
            parameters[2].Value = model.coupon_id;
            parameters[3].Value = model.str_code;
            parameters[4].Value = model.order_id;
            parameters[5].Value = model.order_no;
            parameters[6].Value = model.add_time;
            parameters[7].Value = model.use_time;
            parameters[8].Value = model.status;
            parameters[9].Direction = ParameterDirection.Output;

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
        /// 修改一列数据
        /// </summary>
        public int UpdateField(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "user_coupon_log set " + strValue);
            strSql.Append(" where id=" + id);
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.user_coupon_log model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "user_coupon_log set ");
            strSql.Append("user_id=@user_id,");
            strSql.Append("user_name=@user_name,");
            strSql.Append("coupon_id=@coupon_id,");
            strSql.Append("str_code=@str_code,");
            strSql.Append("order_id=@order_id,");
            strSql.Append("order_no=@order_no,");
            strSql.Append("add_time=@add_time,");
            strSql.Append("use_time=@use_time,");
            strSql.Append("status=@status");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@user_id", SqlDbType.Int,4),
					new SqlParameter("@user_name", SqlDbType.NVarChar,255),
                    new SqlParameter("@coupon_id", SqlDbType.Int,4),
                    new SqlParameter("@str_code", SqlDbType.NVarChar,255),
                    new SqlParameter("@order_id", SqlDbType.Int,4),
                    new SqlParameter("@order_no", SqlDbType.NVarChar,100),
					new SqlParameter("@add_time", SqlDbType.DateTime),
                    new SqlParameter("@use_time", SqlDbType.DateTime),
                    new SqlParameter("@status", SqlDbType.TinyInt,1)};
            parameters[0].Value = model.id;
            parameters[1].Value = model.user_id;
            parameters[2].Value = model.user_name;
            parameters[3].Value = model.coupon_id;
            parameters[4].Value = model.str_code;
            parameters[5].Value = model.order_id;
            parameters[6].Value = model.order_no;
            parameters[7].Value = model.add_time;
            parameters[8].Value = model.use_time;
            parameters[9].Value = model.status;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "user_coupon_log ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Delete(int id, string user_name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "user_coupon_log ");
            strSql.Append(" where id=@id and user_name=@user_name");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
                    new SqlParameter("@user_name", SqlDbType.NVarChar,100)};
            parameters[0].Value = id;
            parameters[1].Value = user_name;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.user_coupon_log GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,user_id,user_name,coupon_id,str_code,order_id,order_no,add_time,use_time,status from " + databaseprefix + "user_coupon_log ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            Model.user_coupon_log model = new Model.user_coupon_log();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["user_id"] != null && ds.Tables[0].Rows[0]["user_id"].ToString() != "")
                {
                    model.user_id = int.Parse(ds.Tables[0].Rows[0]["user_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["user_name"] != null && ds.Tables[0].Rows[0]["user_name"].ToString() != "")
                {
                    model.user_name = ds.Tables[0].Rows[0]["user_name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["coupon_id"] != null && ds.Tables[0].Rows[0]["coupon_id"].ToString() != "")
                {
                    model.coupon_id = int.Parse(ds.Tables[0].Rows[0]["coupon_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["str_code"] != null && ds.Tables[0].Rows[0]["str_code"].ToString() != "")
                {
                    model.str_code = ds.Tables[0].Rows[0]["str_code"].ToString();
                }
                if (ds.Tables[0].Rows[0]["order_id"] != null && ds.Tables[0].Rows[0]["order_id"].ToString() != "")
                {
                    model.order_id = int.Parse(ds.Tables[0].Rows[0]["order_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["order_no"] != null && ds.Tables[0].Rows[0]["order_no"].ToString() != "")
                {
                    model.order_no = ds.Tables[0].Rows[0]["order_no"].ToString();
                }
                if (ds.Tables[0].Rows[0]["add_time"] != null && ds.Tables[0].Rows[0]["add_time"].ToString() != "")
                {
                    model.add_time = DateTime.Parse(ds.Tables[0].Rows[0]["add_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["use_time"] != null && ds.Tables[0].Rows[0]["use_time"].ToString() != "")
                {
                    model.use_time = DateTime.Parse(ds.Tables[0].Rows[0]["use_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["status"] != null && ds.Tables[0].Rows[0]["status"].ToString() != "")
                {
                    model.status = int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
                } 
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.user_coupon_log GetModel(string str_code)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,user_id,user_name,coupon_id,str_code,order_id,order_no,add_time,use_time,status from " + databaseprefix + "user_coupon_log ");
            strSql.Append(" where str_code=@str_code");
            SqlParameter[] parameters = {
					new SqlParameter("@str_code", SqlDbType.NVarChar,50)
			};
            parameters[0].Value = str_code;

            Model.user_coupon_log model = new Model.user_coupon_log();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["user_id"] != null && ds.Tables[0].Rows[0]["user_id"].ToString() != "")
                {
                    model.user_id = int.Parse(ds.Tables[0].Rows[0]["user_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["user_name"] != null && ds.Tables[0].Rows[0]["user_name"].ToString() != "")
                {
                    model.user_name = ds.Tables[0].Rows[0]["user_name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["coupon_id"] != null && ds.Tables[0].Rows[0]["coupon_id"].ToString() != "")
                {
                    model.coupon_id = int.Parse(ds.Tables[0].Rows[0]["coupon_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["str_code"] != null && ds.Tables[0].Rows[0]["str_code"].ToString() != "")
                {
                    model.str_code = ds.Tables[0].Rows[0]["str_code"].ToString();
                }
                if (ds.Tables[0].Rows[0]["order_id"] != null && ds.Tables[0].Rows[0]["order_id"].ToString() != "")
                {
                    model.order_id = int.Parse(ds.Tables[0].Rows[0]["order_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["order_no"] != null && ds.Tables[0].Rows[0]["order_no"].ToString() != "")
                {
                    model.order_no = ds.Tables[0].Rows[0]["order_no"].ToString();
                }
                if (ds.Tables[0].Rows[0]["add_time"] != null && ds.Tables[0].Rows[0]["add_time"].ToString() != "")
                {
                    model.add_time = DateTime.Parse(ds.Tables[0].Rows[0]["add_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["use_time"] != null && ds.Tables[0].Rows[0]["use_time"].ToString() != "")
                {
                    model.use_time = DateTime.Parse(ds.Tables[0].Rows[0]["use_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["status"] != null && ds.Tables[0].Rows[0]["status"].ToString() != "")
                {
                    model.status = int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
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
            strSql.Append(" id,user_id,user_name,coupon_id,str_code,order_id,order_no,add_time,use_time,status ");
            strSql.Append(" FROM " + databaseprefix + "user_coupon_log ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList1(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" t1.id id,t1.user_id,t1.user_name,t1.coupon_id,t1.str_code,t1.order_id,t1.order_no,t1.add_time,t1.use_time,t1.status,t2.amount ");
            strSql.Append(" FROM " + databaseprefix + "user_coupon_log t1 ");
            strSql.Append(" left join " + databaseprefix + "user_coupon t2 on t1.coupon_id=t2.id");
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
            strSql.Append("select * FROM " + databaseprefix + "user_coupon_log");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(Vincent._DTcms.PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(Vincent._DTcms.PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }

        /// <summary>
        /// 获得多表分页数据
        /// </summary>
        public DataSet GetMultiList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT t2.id AS order_id,t4.id as coupon_id,t1.id coupon_log_id,t1.user_name,t1.str_code,t1.use_time,t1.status,t2.order_no,t2.order_amount,t2.real_amount, ");
            strSql.Append("t4.amount AS coupon_amount,t3.goods_title,t4.type AS coupon_type,t4.status AS coupon_status ");
            strSql.Append("FROM " + databaseprefix + "user_coupon_log t1 ");
            strSql.Append("INNER JOIN " + databaseprefix + "orders t2 ON t1.order_no = t2.order_no ");
            strSql.Append("INNER JOIN " + databaseprefix + "order_goods t3 ON t3.order_id=t2.id ");
            strSql.Append("INNER JOIN " + databaseprefix + "user_coupon t4 ON t4.str_code=t1.str_code ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(Vincent._DTcms.PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(Vincent._DTcms.PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }

        /// <summary>
        /// 获得多表分页数据
        /// </summary>
        public DataSet GetList1(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT  t1.id id,t1.user_id,t1.user_name,t1.coupon_id,t1.str_code,t1.order_id,t1.order_no,t1.add_time,t1.use_time,t1.status,t2.amount ");
            strSql.Append("FROM " + databaseprefix + "user_coupon_log t1 ");
            strSql.Append("LEFT JOIN " + databaseprefix + "user_coupon t2 ON t1.coupon_id=t2.id ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(Vincent._DTcms.PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(Vincent._DTcms.PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }

        #endregion  Method
    }
}