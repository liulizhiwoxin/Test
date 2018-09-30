using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;
using BuysingooShop.DBUtility;
using Vincent;

namespace BuysingooShop.DAL
{
    /// <summary>
    /// 数据访问类:投诉
    /// </summary>
    public partial class complian
    {
        private string databaseprefix; //数据库表名前缀
        public complian(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }

        //#region 基本方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "complian");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string order_no)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "complian");
            strSql.Append(" where order_no=@order_no ");
            SqlParameter[] parameters = {
                    new SqlParameter("@order_no", SqlDbType.NVarChar,100)};
            parameters[0].Value = order_no;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        /// <param name="id">退款ID</param>
        /// <param name="user_name">用户名</param>
        /// <returns></returns>
        public bool Exists(int id,string user_name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "complian");
            strSql.Append(" where id=@id and user_name=@user_name");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4),
                    new SqlParameter("@user_name", SqlDbType.NVarChar,100)};
            parameters[0].Value = id;
            parameters[1].Value = user_name;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 返回数据数
        /// </summary>
        public int GetCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H from " + databaseprefix + "complian ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }

        /// <summary>
        /// 增加一条数据,及其子表数据
        /// </summary>
        public int Add(Model.complian model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "complian(");
            strSql.Append(" user_id,user_name,complian_title,complian_time,complian_content,mobile_phone,is_status,is_anonymous,parent_id,com_type,good_id,target,order_no,reply_content)");
            strSql.Append(" values (");
            strSql.Append("@user_id,@user_name,@complian_title,@complian_time,@complian_content,@mobile_phone,@is_status,@is_anonymous,@parent_id,@com_type,@good_id,@target,@order_no,@reply_content)");
            strSql.Append(";set @ReturnValue= @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@user_id", SqlDbType.Int,10),
                    new SqlParameter("@user_name", SqlDbType.NVarChar,100),
                    new SqlParameter("@complian_title", SqlDbType.NVarChar,100),
                    new SqlParameter("@complian_time", SqlDbType.DateTime),
                    new SqlParameter("@complian_content", SqlDbType.NVarChar,500),
                    new SqlParameter("@mobile_phone", SqlDbType.VarChar,11),
                    new SqlParameter("@is_status", SqlDbType.TinyInt,1),
                    new SqlParameter("@is_anonymous", SqlDbType.TinyInt,1),
                    new SqlParameter("@parent_id", SqlDbType.TinyInt,1),
                    new SqlParameter("@com_type", SqlDbType.TinyInt,1),
                    new SqlParameter("@good_id", SqlDbType.Int,10),
                    new SqlParameter("@target", SqlDbType.NVarChar,500),
                    new SqlParameter("@order_no", SqlDbType.NVarChar,500),
                    new SqlParameter("@reply_content", SqlDbType.NVarChar,500),
                    new SqlParameter("@ReturnValue",SqlDbType.Int)};
            parameters[0].Value = model.user_id;
            parameters[1].Value = model.user_name;
            parameters[2].Value = model.complian_title;
            parameters[3].Value = model.complian_time;
            parameters[4].Value = model.complian_content;
            parameters[5].Value = model.mobile_phone;
            parameters[6].Value = model.is_status;
            parameters[7].Value = model.is_anonymous;
            parameters[8].Value = model.parent_id;
            parameters[9].Value = model.com_type;
            parameters[10].Value = model.good_id;
            parameters[11].Value = model.target;
            parameters[12].Value = model.order_no;
            parameters[13].Value = model.reply_content;
            parameters[14].Direction = ParameterDirection.Output;
           
            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            DbHelperSQL.ExecuteSqlTranWithIndentity(sqllist);
            int id = (int)parameters[14].Value;
            return id;
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "complian set " + strValue);
            strSql.Append(" where id=" + id);
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public bool UpdateField(string order_no, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "complian set " + strValue);
            strSql.Append(" where order_no='" + order_no + "'");
            int rowsAffected = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.complian model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "complian set ");
            strSql.Append("order_no=@order_no,");
            strSql.Append("user_id=@user_id,");
            strSql.Append("user_name=@user_name,");
            strSql.Append("complian_title=@complian_title,");
            strSql.Append("complian_time=@complian_time,");
            strSql.Append("complian_content=@complian_content,");
            strSql.Append("mobile_phone=@mobile_phone,");
            strSql.Append("is_status=@is_status,");
            strSql.Append("is_anonymous=@is_anonymous,");
            strSql.Append("parent_id=@parent_id,");
            strSql.Append("com_type=@com_type,");
            strSql.Append("good_id=@good_id");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@order_no", SqlDbType.NVarChar,100),
                    new SqlParameter("@user_id", SqlDbType.Int,10),
                    new SqlParameter("@user_name", SqlDbType.NVarChar,100),
                    new SqlParameter("@complian_title", SqlDbType.NVarChar,100),
                    new SqlParameter("@complian_time", SqlDbType.DateTime),
                    new SqlParameter("@complian_content", SqlDbType.NVarChar,500),
                    new SqlParameter("@mobile_phone", SqlDbType.VarChar,11),
                    new SqlParameter("@is_status", SqlDbType.TinyInt,1),
                    new SqlParameter("@is_anonymous", SqlDbType.TinyInt,1),
                    new SqlParameter("@parent_id", SqlDbType.TinyInt,1),
                    new SqlParameter("@com_type", SqlDbType.TinyInt,1),
                    new SqlParameter("@good_id", SqlDbType.Int,10),
                    new SqlParameter("@id", SqlDbType.Int,10)};
            parameters[0].Value = model.order_no;
            parameters[1].Value = model.user_id;
            parameters[2].Value = model.user_name;
            parameters[3].Value = model.complian_title;
            parameters[4].Value = model.complian_time;
            parameters[5].Value = model.complian_content;
            parameters[6].Value = model.mobile_phone;
            parameters[7].Value = model.is_status;
            parameters[8].Value = model.is_anonymous;
            parameters[9].Value = model.parent_id;
            parameters[10].Value = model.com_type;
            parameters[11].Value = model.good_id;
            parameters[12].Value = model.id;

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
        /// 删除一条数据，及子表所有相关数据
        /// </summary>
        public bool Delete(int id)
        {
            List<CommandInfo> sqllist = new List<CommandInfo>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "complian ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,10)};
            parameters[0].Value = id;

            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);
            int rowsAffected = DbHelperSQL.ExecuteSqlTran(sqllist);
            if (rowsAffected > 0)
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
        public Model.complian GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,user_id,user_name,complian_title,complian_time,complian_content,mobile_phone,is_status,is_anonymous,parent_id,com_type,good_id");
            strSql.Append(" from " + databaseprefix + "complian ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,10)};
            parameters[0].Value = id;

            Model.complian model = new Model.complian();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                #region 表信息
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["user_id"].ToString() != "")
                {
                    model.user_id = int.Parse(ds.Tables[0].Rows[0]["user_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["complian_title"].ToString() != "")
                {
                    model.user_name = ds.Tables[0].Rows[0]["user_name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["complian_title"].ToString() != "")
                {
                    model.complian_title = ds.Tables[0].Rows[0]["complian_title"].ToString();
                }
                if (ds.Tables[0].Rows[0]["complian_time"].ToString() != "")
                {
                    model.complian_time = DateTime.Parse(ds.Tables[0].Rows[0]["complian_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["complian_content"].ToString() != "")
                {
                    model.complian_content = ds.Tables[0].Rows[0]["complian_content"].ToString();
                }
                if (ds.Tables[0].Rows[0]["mobile_phone"].ToString() != "")
                {
                    model.mobile_phone = ds.Tables[0].Rows[0]["mobile_phone"].ToString();
                }
                if (ds.Tables[0].Rows[0]["is_status"].ToString() != "")
                {
                    model.is_status = int.Parse(ds.Tables[0].Rows[0]["is_status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["is_anonymous"].ToString() != "")
                {
                    model.is_anonymous = int.Parse(ds.Tables[0].Rows[0]["is_anonymous"].ToString());
                }
                if (ds.Tables[0].Rows[0]["parent_id"].ToString() != "")
                {
                    model.parent_id = int.Parse(ds.Tables[0].Rows[0]["parent_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["com_type"].ToString() != "")
                {
                    model.com_type = int.Parse(ds.Tables[0].Rows[0]["com_type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["good_id"].ToString() != "")
                {
                    model.good_id = int.Parse(ds.Tables[0].Rows[0]["good_id"].ToString());
                }
                #endregion
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
            strSql.Append(" id,user_id,user_name,complian_title,complian_time,complian_content,mobile_phone,is_status,is_anonymous,parent_id,com_type,good_id,target,order_no,reply_content ");
            strSql.Append(" FROM " + databaseprefix + "complian ");
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
            strSql.Append("select * FROM " + databaseprefix + "complian");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(Vincent._DTcms.PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(Vincent._DTcms.PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }

       
    }
}