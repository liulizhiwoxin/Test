using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using BuysingooShop.DBUtility;
using Vincent;
using System.Collections.Generic;


namespace BuysingooShop.DAL
{
    /// <summary>
    /// 数据访问类:素材
    /// </summary>
    public partial class source_material
    {
        private string databaseprefix; //数据库表名前缀
        public source_material(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }

        #region 基本方法========================================
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "source_material");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 返回素材名称
        /// </summary>
        public string GetTitle(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 title from " + databaseprefix + "source_material");
            strSql.Append(" where id=" + id);
            string title = Convert.ToString(DbHelperSQL.GetSingle(strSql.ToString()));
            if (string.IsNullOrEmpty(title))
            {
                return "";
            }
            return title;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.source_material model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "source_material(");
            strSql.Append("user_id,title,subtitle,img_url,remark,type,sort_id,add_time)");
            strSql.Append(" values (");
            strSql.Append("@user_id,@title,@subtitle,@img_url,@remark,@type,@sort_id,@add_time)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					            new SqlParameter("@user_id", SqlDbType.Int,4),
					            new SqlParameter("@title", SqlDbType.NVarChar,100),
                                new SqlParameter("@subtitle", SqlDbType.NVarChar,100),
                                new SqlParameter("@img_url", SqlDbType.NVarChar,255),
                                new SqlParameter("@remark", SqlDbType.NVarChar,500),
					            new SqlParameter("@type", SqlDbType.Int,4),
					            new SqlParameter("@sort_id", SqlDbType.Int,4),
					            new SqlParameter("@add_time", SqlDbType.DateTime)};
            parameters[0].Value = model.user_id;
            parameters[1].Value = model.title;
            parameters[2].Value = model.subtitle;
            parameters[3].Value = model.img_url;
            parameters[4].Value = model.remark;
            parameters[5].Value = model.type;
            parameters[6].Value = model.sort_id;
            parameters[7].Value = model.add_time;

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
        public void UpdateField(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "source_material set " + strValue);
            strSql.Append(" where id=" + id);
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.source_material model)
        {
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("update " + databaseprefix + "source_material set ");
                        strSql.Append("user_id=@user_id,");
                        strSql.Append("title=@title,");
                        strSql.Append("subtitle=@subtitle,");
                        strSql.Append("img_url=@img_url,");
                        strSql.Append("remark=@remark,");
                        strSql.Append("type=@type,");
                        strSql.Append("sort_id=@sort_id,");
                        strSql.Append("add_time=@add_time");
                        strSql.Append(" where id=@id");
                        SqlParameter[] parameters = {
					            new SqlParameter("@user_id", SqlDbType.Int,4),
					            new SqlParameter("@title", SqlDbType.NVarChar,100),
                                new SqlParameter("@subtitle", SqlDbType.NVarChar,100),
                                new SqlParameter("@img_url", SqlDbType.NVarChar,255),
                                new SqlParameter("@remark", SqlDbType.NVarChar,500),
					            new SqlParameter("@type", SqlDbType.Int,4),
					            new SqlParameter("@sort_id", SqlDbType.Int,4),
					            new SqlParameter("@add_time", SqlDbType.DateTime),
                                new SqlParameter("@id", SqlDbType.Int,4)};
                        parameters[0].Value = model.user_id;
                        parameters[1].Value = model.title;
                        parameters[2].Value = model.subtitle;
                        parameters[3].Value = model.img_url;
                        parameters[4].Value = model.remark;
                        parameters[5].Value = model.type;
                        parameters[6].Value = model.sort_id;
                        parameters[7].Value = model.add_time;
                        parameters[8].Value = model.id;
                        DbHelperSQL.ExecuteSql(conn, trans, strSql.ToString(), parameters);

                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "source_material ");
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
        /// 删除多条数据
        /// </summary>
        public bool Delete(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "source_material ");
            strSql.Append(" where " + strWhere);
            return DbHelperSQL.ExecuteSql(strSql.ToString()) > 0 ? true : false;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.source_material GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,user_id,title,subtitle,img_url,remark,type,sort_id,add_time from " + databaseprefix + "source_material ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            Model.source_material model = new Model.source_material();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["user_id"] != null && ds.Tables[0].Rows[0]["user_id"].ToString() != "")
                {
                    model.user_id =int.Parse( ds.Tables[0].Rows[0]["user_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["title"] != null && ds.Tables[0].Rows[0]["title"].ToString() != "")
                {
                    model.title = ds.Tables[0].Rows[0]["title"].ToString();
                }
                if (ds.Tables[0].Rows[0]["subtitle"] != null && ds.Tables[0].Rows[0]["subtitle"].ToString() != "")
                {
                    model.subtitle = ds.Tables[0].Rows[0]["subtitle"].ToString();
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
                    model.type = int.Parse(ds.Tables[0].Rows[0]["type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["sort_id"] != null && ds.Tables[0].Rows[0]["sort_id"].ToString() != "")
                {
                    model.sort_id = int.Parse(ds.Tables[0].Rows[0]["sort_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["add_time"].ToString() != "")
                {
                    model.add_time = DateTime.Parse(ds.Tables[0].Rows[0]["add_time"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 得到一个对象实体(重载，带事务)
        /// </summary>
        public Model.source_material GetModel(SqlConnection conn, SqlTransaction trans, int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,user_id,title,subtitle,img_url,remark,type,sort_id,add_time from " + databaseprefix + "source_material ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            Model.source_material model = new Model.source_material();
            DataSet ds = DbHelperSQL.Query(conn, trans, strSql.ToString(), parameters);
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
                if (ds.Tables[0].Rows[0]["title"] != null && ds.Tables[0].Rows[0]["title"].ToString() != "")
                {
                    model.title = ds.Tables[0].Rows[0]["title"].ToString();
                }
                if (ds.Tables[0].Rows[0]["subtitle"] != null && ds.Tables[0].Rows[0]["subtitle"].ToString() != "")
                {
                    model.subtitle = ds.Tables[0].Rows[0]["subtitle"].ToString();
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
                    model.type = int.Parse(ds.Tables[0].Rows[0]["type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["sort_id"] != null && ds.Tables[0].Rows[0]["sort_id"].ToString() != "")
                {
                    model.sort_id = int.Parse(ds.Tables[0].Rows[0]["sort_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["add_time"].ToString() != "")
                {
                    model.add_time = DateTime.Parse(ds.Tables[0].Rows[0]["add_time"].ToString());
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
            strSql.Append(" id,user_id,title,subtitle,img_url,remark,type,sort_id,add_time ");
            strSql.Append(" FROM " + databaseprefix + "source_material ");
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
            strSql.Append("select * FROM " + databaseprefix + "source_material");


            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(Vincent._DTcms.PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(Vincent._DTcms.PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }


        #endregion
    }
}
