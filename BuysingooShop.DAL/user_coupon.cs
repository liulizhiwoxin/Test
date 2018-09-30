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
    public partial class user_coupon
    {
        private string databaseprefix; //数据库表名前缀
        public user_coupon(string _databaseprefix)
        { 
            databaseprefix = _databaseprefix; 
        }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string code)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "user_coupon");
            strSql.Append(" where str_code=@code");
            SqlParameter[] parameters = {
					new SqlParameter("@code", SqlDbType.NVarChar,50)};
            parameters[0].Value = code;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.user_coupon model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "user_coupon(");
            strSql.Append("title,remark,type,amount,str_code,add_time,start_time,end_time,status,userid,img_url)");
            strSql.Append(" values (");
            strSql.Append("@title,@remark,@type,@amount,@str_code,@add_time,@start_time,@end_time,@status,@userid,@img_url)");

            SqlParameter[] parameters = {
					new SqlParameter("@title", SqlDbType.NVarChar,255),
					new SqlParameter("@remark", SqlDbType.NVarChar,500),
                    new SqlParameter("@type", SqlDbType.NVarChar,50),
                    new SqlParameter("@amount", SqlDbType.Decimal,5),
                    new SqlParameter("@str_code", SqlDbType.NVarChar,255),
					new SqlParameter("@add_time", SqlDbType.DateTime),
                    new SqlParameter("@start_time", SqlDbType.DateTime),
                    new SqlParameter("@end_time", SqlDbType.DateTime),
                    new SqlParameter("@status", SqlDbType.TinyInt,1),
                    new SqlParameter("@userid", SqlDbType.Int),
                    new SqlParameter("@img_url", SqlDbType.NVarChar,255)};
            parameters[0].Value = model.title;
            parameters[1].Value = model.remark;
            parameters[2].Value = model.type;
            parameters[3].Value = model.amount;
            parameters[4].Value = model.str_code;
            parameters[5].Value = DateTime.Now;
            parameters[6].Value = model.start_time;
            parameters[7].Value = model.end_time;
            parameters[8].Value = model.status;
            parameters[9].Value = model.userid;
            parameters[10].Value = model.img_url;

            object obj = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
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
            strSql.Append("update " + databaseprefix + "user_coupon set " + strValue);
            strSql.Append(" where id=" + id);
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.user_coupon model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "user_coupon set ");
            strSql.Append("title=@title,");
            strSql.Append("remark=@remark,");
            strSql.Append("type=@type,");
            strSql.Append("amount=@amount,");
            strSql.Append("str_code=@str_code,");
            strSql.Append("add_time=@add_time,");
            strSql.Append("start_time=@start_time,");
            strSql.Append("end_time=@end_time,");
            strSql.Append("status=@status,");
            strSql.Append("userid=@userid,");
            strSql.Append("img_url=@img_url");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@title", SqlDbType.NVarChar,255),
					new SqlParameter("@remark", SqlDbType.NVarChar,500),
                    new SqlParameter("@type", SqlDbType.NVarChar,50),
                    new SqlParameter("@amount", SqlDbType.Decimal,5),
                    new SqlParameter("@str_code", SqlDbType.NVarChar,255),
					new SqlParameter("@add_time", SqlDbType.DateTime),
                    new SqlParameter("@start_time", SqlDbType.DateTime),
                    new SqlParameter("@end_time", SqlDbType.DateTime),
                    new SqlParameter("@status", SqlDbType.TinyInt,1),
                    new SqlParameter("@userid", SqlDbType.Int),
                    new SqlParameter("@id", SqlDbType.Int),
                    new SqlParameter("@img_url", SqlDbType.NVarChar,255)};
            parameters[0].Value = model.title;
            parameters[1].Value = model.remark;
            parameters[2].Value = model.type;
            parameters[3].Value = model.amount;
            parameters[4].Value = model.str_code;
            parameters[5].Value = model.add_time;
            parameters[6].Value = model.start_time;
            parameters[7].Value = model.end_time;
            parameters[8].Value = model.status;
            parameters[9].Value = model.userid;
            parameters[10].Value = model.id;
            parameters[11].Value = model.img_url;

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
            strSql.Append("delete from " + databaseprefix + "user_coupon ");
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
        
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.user_coupon GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,title,remark,type,amount,str_code,add_time,start_time,end_time,status,userid,img_url from " + databaseprefix + "user_coupon ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            return DsSetToModel(ds);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.user_coupon GetModel(string strwhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,title,remark,type,amount,str_code,add_time,start_time,end_time,status,userid,img_url from " + databaseprefix + "user_coupon ");
            strSql.Append(" where " + strwhere);
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            return DsSetToModel(ds);
        }

        /// <summary>
        /// DataSet转化为对象实体
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public Model.user_coupon DsSetToModel(DataSet ds)
        {
            Model.user_coupon model = new Model.user_coupon();
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["title"] != null && ds.Tables[0].Rows[0]["title"].ToString() != "")
                {
                    model.title = ds.Tables[0].Rows[0]["title"].ToString();
                }
                if (ds.Tables[0].Rows[0]["remark"] != null && ds.Tables[0].Rows[0]["remark"].ToString() != "")
                {
                    model.remark = ds.Tables[0].Rows[0]["remark"].ToString();
                }
                if (ds.Tables[0].Rows[0]["type"] != null && ds.Tables[0].Rows[0]["type"].ToString() != "")
                {
                    model.type = ds.Tables[0].Rows[0]["type"].ToString();
                }
                if (ds.Tables[0].Rows[0]["amount"].ToString() != "")
                {
                    model.amount = decimal.Parse(ds.Tables[0].Rows[0]["amount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["str_code"] != null && ds.Tables[0].Rows[0]["str_code"].ToString() != "")
                {
                    model.str_code = ds.Tables[0].Rows[0]["str_code"].ToString();
                }
                if (ds.Tables[0].Rows[0]["add_time"] != null && ds.Tables[0].Rows[0]["add_time"].ToString() != "")
                {
                    model.add_time = DateTime.Parse(ds.Tables[0].Rows[0]["add_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["start_time"] != null && ds.Tables[0].Rows[0]["start_time"].ToString() != "")
                {
                    model.start_time = DateTime.Parse(ds.Tables[0].Rows[0]["start_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["end_time"] != null && ds.Tables[0].Rows[0]["end_time"].ToString() != "")
                {
                    model.end_time = DateTime.Parse(ds.Tables[0].Rows[0]["end_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["status"] != null && ds.Tables[0].Rows[0]["status"].ToString() != "")
                {
                    model.status = int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["userid"] != null && ds.Tables[0].Rows[0]["userid"].ToString() != "")
                {
                    model.userid = int.Parse(ds.Tables[0].Rows[0]["userid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["img_url"] != null && ds.Tables[0].Rows[0]["img_url"].ToString() != "")
                {
                    model.img_url = ds.Tables[0].Rows[0]["img_url"].ToString();
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
            strSql.Append(" id,title,remark,type,amount,str_code,add_time,start_time,end_time,status,userid,img_url ");
            strSql.Append(" FROM " + databaseprefix + "user_coupon ");
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
        public DataSet GetList(string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(" DISTINCT ");
            strSql.Append(" title id,title ");
            strSql.Append(" FROM " + databaseprefix + "user_coupon ");
            strSql.Append(" WHERE "+strWhere);
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM " + databaseprefix + "user_coupon");
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