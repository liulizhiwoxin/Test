using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Vincent;
using BuysingooShop.DBUtility;//Please add references

namespace BuysingooShop.DAL
{
    /// <summary>
    /// 数据访问类:activity
    /// </summary>
    public partial class activity
    {
        public activity()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from activity");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(BuysingooShop.Model.activity model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into activity(");
            strSql.Append("fields,value,remark,is_close,start_time,end_time,category_id)");
            strSql.Append(" values (");
            strSql.Append("@fields,@value,@remark,@is_close,@start_time,@end_time,@category_id)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@fields", SqlDbType.NVarChar,50),
					new SqlParameter("@value", SqlDbType.NVarChar,50),
					new SqlParameter("@remark", SqlDbType.NVarChar,100),
					new SqlParameter("@is_close", SqlDbType.Int,4),
					new SqlParameter("@start_time", SqlDbType.DateTime),
					new SqlParameter("@end_time", SqlDbType.DateTime),
                    new SqlParameter("@category_id",SqlDbType.NVarChar,100) };
            parameters[0].Value = model.fields;
            parameters[1].Value = model.value;
            parameters[2].Value = model.remark;
            parameters[3].Value = model.is_close;
            parameters[4].Value = model.start_time;
            parameters[5].Value = model.end_time;
            parameters[6].Value = model.category_id;

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
        /// 更新一条数据
        /// </summary>
        public bool Update(BuysingooShop.Model.activity model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update activity set ");
            strSql.Append("fields=@fields,");
            strSql.Append("value=@value,");
            strSql.Append("remark=@remark,");
            strSql.Append("is_close=@is_close,");
            strSql.Append("start_time=@start_time,");
            strSql.Append("end_time=@end_time,");
            strSql.Append("category_id=@category_id");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@fields", SqlDbType.NVarChar,50),
					new SqlParameter("@value", SqlDbType.NVarChar,50),
					new SqlParameter("@remark", SqlDbType.NVarChar,100),
					new SqlParameter("@is_close", SqlDbType.Int,4),
					new SqlParameter("@start_time", SqlDbType.DateTime),
					new SqlParameter("@end_time", SqlDbType.DateTime),
                    new SqlParameter("@category_id",SqlDbType.NVarChar,100), 
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.fields;
            parameters[1].Value = model.value;
            parameters[2].Value = model.remark;
            parameters[3].Value = model.is_close;
            parameters[4].Value = model.start_time;
            parameters[5].Value = model.end_time;
            parameters[6].Value = model.category_id;
            parameters[7].Value = model.ID;

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
        public bool Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from activity ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

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
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from activity ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
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
        public BuysingooShop.Model.activity GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,fields,value,remark,is_close,start_time,end_time,category_id from dt_activity ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            BuysingooShop.Model.activity model = new BuysingooShop.Model.activity();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public BuysingooShop.Model.activity DataRowToModel(DataRow row)
        {
            BuysingooShop.Model.activity model = new BuysingooShop.Model.activity();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["fields"] != null)
                {
                    model.fields = row["fields"].ToString();
                }
                if (row["value"] != null)
                {
                    model.value = row["value"].ToString();
                }
                if (row["remark"] != null)
                {
                    model.remark = row["remark"].ToString();
                }
                if (row["is_close"] != null && row["is_close"].ToString() != "")
                {
                    model.is_close = int.Parse(row["is_close"].ToString());
                }
                if (row["start_time"] != null && row["start_time"].ToString() != "")
                {
                    model.start_time = DateTime.Parse(row["start_time"].ToString());
                }
                if (row["end_time"] != null && row["end_time"].ToString() != "")
                {
                    model.end_time = DateTime.Parse(row["end_time"].ToString());
                }
                if (row["category_id"] != null && row["category_id"].ToString() != "")
                {
                    model.category_id = row["category_id"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,fields,value,remark,is_close,start_time,end_time,category_id ");
            strSql.Append(" FROM dt_activity ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
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
            strSql.Append(" ID,fields,value,remark,is_close,start_time,end_time,category_id ");
            strSql.Append(" FROM dt_activity ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM dt_activity ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
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
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.ID desc");
            }
            strSql.Append(")AS Row, T.*  from activity T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM dt_activity");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(Vincent._DTcms.PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(Vincent._DTcms.PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }


        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}



