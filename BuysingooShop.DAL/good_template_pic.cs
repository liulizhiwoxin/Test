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
    /// 数据访问类:模板图片
    /// </summary>
    public partial class good_template_pic
    {
        private string databaseprefix; //数据库表名前缀
        public good_template_pic(string _databaseprefix)
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
            strSql.Append("select count(1) from " + databaseprefix + "good_template_pic");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.good_template_pic model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "good_template_pic(");
            strSql.Append("templateId,typeId,picUrl,smallPicUrl,addTime,isLock)");
            strSql.Append(" values (");
            strSql.Append("@templateId,@typeId,@picUrl,@smallPicUrl,@addTime,@isLock)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					            new SqlParameter("@templateId", SqlDbType.Int,4),
					            new SqlParameter("@typeId", SqlDbType.Int,4),
                                new SqlParameter("@picUrl", SqlDbType.NVarChar,100),
					            new SqlParameter("@smallPicUrl", SqlDbType.NVarChar,100),
					            new SqlParameter("@addTime", SqlDbType.DateTime),
					            new SqlParameter("@isLock", SqlDbType.Int,4)};
            parameters[0].Value = model.templateId;
            parameters[1].Value = model.typeId;
            parameters[2].Value = model.picUrl;
            parameters[3].Value = model.smallPicUrl;
            parameters[4].Value = model.addTime;
            parameters[5].Value = model.isLock;

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
            strSql.Append("update " + databaseprefix + "good_template_pic set " + strValue);
            strSql.Append(" where id=" + id);
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.good_template_pic model)
        {
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("update " + databaseprefix + "good_template_pic set ");
                        strSql.Append("templateId=@templateId,");
                        strSql.Append("typeId=@typeId,");
                        strSql.Append("picUrl=@picUrl,");
                        strSql.Append("smallPicUrl=@smallPicUrl,");
                        strSql.Append("addTime=@addTime,");
                        strSql.Append("isLock=@isLock");
                        strSql.Append(" where id=@id");
                        SqlParameter[] parameters = {
					            new SqlParameter("@templateId", SqlDbType.Int,4),
					            new SqlParameter("@typeId", SqlDbType.Int,4),
                                new SqlParameter("@picUrl", SqlDbType.NVarChar,100),
					            new SqlParameter("@smallPicUrl", SqlDbType.NVarChar,100),
					            new SqlParameter("@addTime", SqlDbType.DateTime),
					            new SqlParameter("@isLock", SqlDbType.Int,4),
                                new SqlParameter("@id", SqlDbType.Int,4)};
                        parameters[0].Value = model.templateId;
                        parameters[1].Value = model.typeId;
                        parameters[2].Value = model.picUrl;
                        parameters[3].Value = model.smallPicUrl;
                        parameters[4].Value = model.addTime;
                        parameters[5].Value = model.isLock;
                        parameters[6].Value = model.id;
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
            strSql.Append("delete from " + databaseprefix + "good_template_pic ");
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
        public Model.good_template_pic GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,templateId,typeId,picUrl,smallPicUrl,addTime,isLock from " + databaseprefix + "good_template_pic ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            Model.good_template_pic model = new Model.good_template_pic();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["templateId"] != null && ds.Tables[0].Rows[0]["templateId"].ToString() != "")
                {
                    model.templateId = int.Parse(ds.Tables[0].Rows[0]["templateId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["typeId"] != null && ds.Tables[0].Rows[0]["typeId"].ToString() != "")
                {
                    model.typeId = int.Parse(ds.Tables[0].Rows[0]["typeId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["picUrl"] != null && ds.Tables[0].Rows[0]["picUrl"].ToString() != "")
                {
                    model.picUrl = ds.Tables[0].Rows[0]["picUrl"].ToString();
                }
                if (ds.Tables[0].Rows[0]["smallPicUrl"] != null && ds.Tables[0].Rows[0]["smallPicUrl"].ToString() != "")
                {
                    model.smallPicUrl = ds.Tables[0].Rows[0]["smallPicUrl"].ToString();
                }
                if (ds.Tables[0].Rows[0]["addTime"].ToString() != "")
                {
                    model.addTime = DateTime.Parse(ds.Tables[0].Rows[0]["addTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["isLock"] != null && ds.Tables[0].Rows[0]["isLock"].ToString() != "")
                {
                    model.isLock = int.Parse(ds.Tables[0].Rows[0]["isLock"].ToString());
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
        public Model.good_template_pic GetModel(SqlConnection conn, SqlTransaction trans, int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,templateId,typeId,picUrl,smallPicUrl,addTime,isLock from " + databaseprefix + "good_template_pic ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            Model.good_template_pic model = new Model.good_template_pic();
            DataSet ds = DbHelperSQL.Query(conn, trans, strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["templateId"] != null && ds.Tables[0].Rows[0]["templateId"].ToString() != "")
                {
                    model.templateId = int.Parse(ds.Tables[0].Rows[0]["templateId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["typeId"] != null && ds.Tables[0].Rows[0]["typeId"].ToString() != "")
                {
                    model.typeId = int.Parse(ds.Tables[0].Rows[0]["typeId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["picUrl"] != null && ds.Tables[0].Rows[0]["picUrl"].ToString() != "")
                {
                    model.picUrl = ds.Tables[0].Rows[0]["picUrl"].ToString();
                }
                if (ds.Tables[0].Rows[0]["smallPicUrl"] != null && ds.Tables[0].Rows[0]["smallPicUrl"].ToString() != "")
                {
                    model.smallPicUrl = ds.Tables[0].Rows[0]["smallPicUrl"].ToString();
                }
                if (ds.Tables[0].Rows[0]["addTime"].ToString() != "")
                {
                    model.addTime = DateTime.Parse(ds.Tables[0].Rows[0]["addTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["isLock"] != null && ds.Tables[0].Rows[0]["isLock"].ToString() != "")
                {
                    model.isLock = int.Parse(ds.Tables[0].Rows[0]["isLock"].ToString());
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
            strSql.Append(" id,templateId,typeId,picUrl,smallPicUrl,addTime,isLock ");
            strSql.Append(" FROM " + databaseprefix + "good_template_pic ");
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
            strSql.Append("select * FROM " + databaseprefix + "good_template_pic");


            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(Vincent._DTcms.PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(Vincent._DTcms.PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.good_template_pic> GetList(int template_id)
        {
            List<Model.good_template_pic> modelList = new List<Model.good_template_pic>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,templateId,typeId,picUrl,smallPicUrl,addTime,isLock ");
            strSql.Append(" FROM " + databaseprefix + "good_template_pic ");
            strSql.Append(" where templateId=" + template_id);
            DataTable dt = DbHelperSQL.Query(strSql.ToString()).Tables[0];

            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.good_template_pic model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.good_template_pic();
                    if (dt.Rows[n]["id"] != null && dt.Rows[n]["id"].ToString() != "")
                    {
                        model.id = int.Parse(dt.Rows[n]["id"].ToString());
                    }
                    if (dt.Rows[n]["templateId"] != null && dt.Rows[n]["templateId"].ToString() != "")
                    {
                        model.templateId = int.Parse(dt.Rows[n]["templateId"].ToString());
                    }
                    if (dt.Rows[n]["typeId"] != null && dt.Rows[n]["typeId"].ToString() != "")
                    {
                        model.typeId = int.Parse(dt.Rows[n]["typeId"].ToString());
                    }
                    if (dt.Rows[n]["picUrl"] != null && dt.Rows[n]["picUrl"].ToString() != "")
                    {
                        model.picUrl = dt.Rows[n]["picUrl"].ToString();
                    }
                    if (dt.Rows[n]["smallPicUrl"] != null && dt.Rows[n]["smallPicUrl"].ToString() != "")
                    {
                        model.smallPicUrl = dt.Rows[n]["smallPicUrl"].ToString();
                    }
                    if (dt.Rows[n]["addTime"].ToString() != "")
                    {
                        model.addTime = DateTime.Parse(dt.Rows[n]["addTime"].ToString());
                    }
                    if (dt.Rows[n]["isLock"] != null && dt.Rows[n]["isLock"].ToString() != "")
                    {
                        model.isLock = int.Parse(dt.Rows[n]["isLock"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 查找不存在的图片并删除已删除的图片及数据
        /// </summary>
        public void DeleteList(SqlConnection conn, SqlTransaction trans, List<Model.good_template_pic> models, int template_id)
        {
            StringBuilder idList = new StringBuilder();
            if (models != null)
            {
                foreach (Model.good_template_pic modelt in models)
                {
                    if (modelt.id > 0)
                    {
                        idList.Append(modelt.id + ",");
                    }
                }
            }
            string id_list = Vincent._DTcms.Utils.DelLastChar(idList.ToString(), ",");
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,templateId,typeId,picUrl,smallPicUrl,addTime,isLock from " + databaseprefix + "good_template_pic where templateId=" + template_id);
            if (!string.IsNullOrEmpty(id_list))
            {
                strSql.Append(" and id not in(" + id_list + ")");
            }
            DataSet ds = DbHelperSQL.Query(conn, trans, strSql.ToString());
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                int rows = DbHelperSQL.ExecuteSql(conn, trans, "delete from " + databaseprefix + "good_template_pic where id=" + dr["id"].ToString()); //删除数据库
                if (rows > 0)
                {
                    Vincent._DTcms.Utils.DeleteFile(dr["smallPicUrl"].ToString()); //删除缩略图
                    Vincent._DTcms.Utils.DeleteFile(dr["picUrl"].ToString()); //删除原图
                }
            }
        }

        /// <summary>
        /// 删除图片
        /// </summary>
        public void DeleteFile(List<Model.good_template_pic> models)
        {
            if (models != null)
            {
                foreach (Model.good_template_pic modelt in models)
                {
                    Vincent._DTcms.Utils.DeleteFile(modelt.picUrl);
                    Vincent._DTcms.Utils.DeleteFile(modelt.smallPicUrl);
                }
            }
        }

        #endregion
    }
}