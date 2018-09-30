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
    /// 数据访问类:商品品牌
    /// </summary>
    public partial class brand
    {
        private string databaseprefix; //数据库表名前缀
        public brand(string _databaseprefix)
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
            strSql.Append("select count(1) from " + databaseprefix + "brand");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 返回品牌名称
        /// </summary>
        public string GetTitle(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 brandName from " + databaseprefix + "brand");
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
        public int Add(Model.brand model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "brand(");
            strSql.Append("brandName,brandImgUrl,remark,isLock,managerId,sort_id,add_time,update_time)");
            strSql.Append(" values (");
            strSql.Append("@brandName,@brandImgUrl,@remark,@isLock,@managerId,@sort_id,@add_time,@update_time)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					            new SqlParameter("@brandName", SqlDbType.NVarChar,20),
					            new SqlParameter("@brandImgUrl", SqlDbType.NVarChar,50),
                                new SqlParameter("@remark", SqlDbType.NVarChar,500),
					            new SqlParameter("@isLock", SqlDbType.Int,4),
					            new SqlParameter("@managerId", SqlDbType.Int,4),
					            new SqlParameter("@sort_id", SqlDbType.Int,4),
					            new SqlParameter("@add_time", SqlDbType.DateTime),
					            new SqlParameter("@update_time", SqlDbType.DateTime)};
            parameters[0].Value = model.brandName;
            parameters[1].Value = model.brandImgUrl;
            parameters[2].Value = model.remark;
            parameters[3].Value = model.isLock;
            parameters[4].Value = model.managerId;
            parameters[5].Value = model.sort_id;
            parameters[6].Value = model.add_time;
            parameters[7].Value = model.update_time;

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
            strSql.Append("update " + databaseprefix + "brand set " + strValue);
            strSql.Append(" where id=" + id);
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.brand model)
        {
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("update " + databaseprefix + "brand set ");
                        strSql.Append("brandName=@brandName,");
                        strSql.Append("brandImgUrl=@brandImgUrl,");
                        strSql.Append("remark=@remark,");
                        strSql.Append("isLock=@isLock,");
                        strSql.Append("managerId=@managerId,");
                        strSql.Append("sort_id=@sort_id,");
                        strSql.Append("add_time=@add_time,");
                        strSql.Append("update_time=@update_time");
                        strSql.Append(" where id=@id");
                        SqlParameter[] parameters = {
					             new SqlParameter("@brandName", SqlDbType.NVarChar,20),
					            new SqlParameter("@brandImgUrl", SqlDbType.NVarChar,50),
                                new SqlParameter("@remark", SqlDbType.NVarChar,500),
					            new SqlParameter("@isLock", SqlDbType.Int,4),
					            new SqlParameter("@managerId", SqlDbType.Int,4),
					            new SqlParameter("@sort_id", SqlDbType.Int,4),
					            new SqlParameter("@add_time", SqlDbType.DateTime),
					            new SqlParameter("@update_time", SqlDbType.DateTime),
                                new SqlParameter("@id", SqlDbType.Int,4)};
                        parameters[0].Value = model.brandName;
                        parameters[1].Value = model.brandImgUrl;
                        parameters[2].Value = model.remark;
                        parameters[3].Value = model.isLock;
                        parameters[4].Value = model.managerId;
                        parameters[5].Value = model.sort_id;
                        parameters[6].Value = model.add_time;
                        parameters[7].Value = model.update_time;
                        parameters[8].Value = model.id;
                        DbHelperSQL.ExecuteSql(conn, trans, strSql.ToString(), parameters);

                        #region 添加/修改相册
                        //删除原有的数据
                        bool bl = new brand_attach().Delete(" brand_id=" + model.id);
                        //添加、修改相册
                        if (model.brand_attach != null)
                        {
                            StringBuilder strSql2;
                            foreach (Model.brand_attach modelt in model.brand_attach)
                            {
                                strSql2 = new StringBuilder();
                                if (modelt.id > 0)
                                {
                                    strSql2.Append("update " + databaseprefix + "brand_attach set ");
                                    strSql2.Append("brand_id=@brand_id,");
                                    strSql2.Append("theme_id=@theme_id,");
                                    strSql2.Append("size=@size,");
                                    strSql2.Append("img_url=@img_url,");
                                    strSql2.Append("remark=@remark");
                                    strSql2.Append("small_imgurl=@small_imgurl");
                                    strSql2.Append("add_time=@add_time");
                                    strSql2.Append(" where id=@id");
                                    SqlParameter[] parameters2 = {
					                            new SqlParameter("@brand_id", SqlDbType.Int,4),
					                            new SqlParameter("@theme_id", SqlDbType.Int,4),
					                            new SqlParameter("@size", SqlDbType.NChar,10),
					                            new SqlParameter("@img_url", SqlDbType.NChar,100),
					                            new SqlParameter("@remark", SqlDbType.NVarChar,100),
                                                new SqlParameter("@small_imgurl", SqlDbType.NChar,100),
                                                new SqlParameter("@add_time", SqlDbType.DateTime),
					                            new SqlParameter("@id", SqlDbType.Int,4)};
                                    parameters2[0].Value = modelt.brand_id;
                                    parameters2[1].Value = modelt.theme_id;
                                    parameters2[2].Value = modelt.size;
                                    parameters2[3].Value = modelt.img_url;
                                    parameters2[4].Value = modelt.remark;
                                    parameters2[5].Value = modelt.small_imgurl;
                                    parameters2[6].Value = modelt.add_time;
                                    parameters2[7].Value = modelt.id;
                                    DbHelperSQL.ExecuteSql(conn, trans, strSql2.ToString(), parameters2);
                                }
                                else
                                {
                                    strSql2.Append("insert into " + databaseprefix + "brand_attach(");
                                    strSql2.Append("brand_id,theme_id,size,img_url,remark,small_imgurl,add_time)");
                                    strSql2.Append(" values (");
                                    strSql2.Append("@brand_id,@theme_id,@size,@img_url,@remark,@small_imgurl,@add_time)");
                                    strSql2.Append(";select @@IDENTITY");
                                    SqlParameter[] parameters2 = {
					                            new SqlParameter("@brand_id", SqlDbType.Int,4),
					                            new SqlParameter("@theme_id", SqlDbType.Int,4),
					                            new SqlParameter("@size", SqlDbType.NChar,10),
					                            new SqlParameter("@img_url", SqlDbType.NChar,100),
					                            new SqlParameter("@remark", SqlDbType.NVarChar,100),
                                                new SqlParameter("@small_imgurl", SqlDbType.NChar,100),
                                                new SqlParameter("@add_time", SqlDbType.DateTime)};
                                    parameters2[0].Value = modelt.brand_id;
                                    parameters2[1].Value = modelt.theme_id;
                                    parameters2[2].Value = modelt.size;
                                    parameters2[3].Value = modelt.img_url;
                                    parameters2[4].Value = modelt.remark;
                                    parameters2[5].Value = modelt.small_imgurl;
                                    parameters2[6].Value = modelt.add_time;
                                    DbHelperSQL.ExecuteSql(conn, trans, strSql2.ToString(), parameters2);
                                }
                            }
                        }

                        #endregion

                        trans.Commit();
                    }
                    catch(Exception ex)
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
            strSql.Append("delete from " + databaseprefix + "brand ");
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
        public Model.brand GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,brandName,brandImgUrl,remark,isLock,managerId,sort_id,add_time,update_time from " + databaseprefix + "brand ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            Model.brand model = new Model.brand();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                #region 基表信息
                if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["brandName"] != null && ds.Tables[0].Rows[0]["brandName"].ToString() != "")
                {
                    model.brandName = ds.Tables[0].Rows[0]["brandName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["brandImgUrl"] != null && ds.Tables[0].Rows[0]["brandImgUrl"].ToString() != "")
                {
                    model.brandImgUrl = ds.Tables[0].Rows[0]["brandImgUrl"].ToString();
                }
                if (ds.Tables[0].Rows[0]["remark"] != null && ds.Tables[0].Rows[0]["remark"].ToString() != "")
                {
                    model.remark = ds.Tables[0].Rows[0]["remark"].ToString();
                }
                if (ds.Tables[0].Rows[0]["isLock"] != null && ds.Tables[0].Rows[0]["isLock"].ToString() != "")
                {
                    model.isLock = int.Parse(ds.Tables[0].Rows[0]["isLock"].ToString());
                }
                if (ds.Tables[0].Rows[0]["managerId"] != null && ds.Tables[0].Rows[0]["managerId"].ToString() != "")
                {
                    model.managerId = int.Parse(ds.Tables[0].Rows[0]["managerId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["sort_id"] != null && ds.Tables[0].Rows[0]["sort_id"].ToString() != "")
                {
                    model.sort_id = int.Parse(ds.Tables[0].Rows[0]["sort_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["add_time"].ToString() != "")
                {
                    model.add_time = DateTime.Parse(ds.Tables[0].Rows[0]["add_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["update_time"].ToString() != "")
                {
                    model.update_time = DateTime.Parse(ds.Tables[0].Rows[0]["update_time"].ToString());
                } 
                #endregion

                #region 图片附表
                //相册信息
                model.brand_attach = new brand_attach().GetAttachList(id);
                #endregion

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
        public Model.brand GetModel(SqlConnection conn, SqlTransaction trans, int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,brandName,brandImgUrl,remark,isLock,managerId,sort_id,add_time,update_time from " + databaseprefix + "brand ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            Model.brand model = new Model.brand();
            DataSet ds = DbHelperSQL.Query(conn, trans, strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["brandName"] != null && ds.Tables[0].Rows[0]["brandName"].ToString() != "")
                {
                    model.brandName = ds.Tables[0].Rows[0]["brandName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["brandImgUrl"] != null && ds.Tables[0].Rows[0]["brandImgUrl"].ToString() != "")
                {
                    model.brandImgUrl = ds.Tables[0].Rows[0]["brandImgUrl"].ToString();
                }
                if (ds.Tables[0].Rows[0]["remark"] != null && ds.Tables[0].Rows[0]["remark"].ToString() != "")
                {
                    model.remark = ds.Tables[0].Rows[0]["remark"].ToString();
                }
                if (ds.Tables[0].Rows[0]["isLock"] != null && ds.Tables[0].Rows[0]["isLock"].ToString() != "")
                {
                    model.isLock = int.Parse(ds.Tables[0].Rows[0]["isLock"].ToString());
                }
                if (ds.Tables[0].Rows[0]["managerId"] != null && ds.Tables[0].Rows[0]["managerId"].ToString() != "")
                {
                    model.managerId = int.Parse(ds.Tables[0].Rows[0]["managerId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["sort_id"] != null && ds.Tables[0].Rows[0]["sort_id"].ToString() != "")
                {
                    model.sort_id = int.Parse(ds.Tables[0].Rows[0]["sort_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["add_time"].ToString() != "")
                {
                    model.add_time = DateTime.Parse(ds.Tables[0].Rows[0]["add_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["update_time"].ToString() != "")
                {
                    model.update_time = DateTime.Parse(ds.Tables[0].Rows[0]["update_time"].ToString());
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
            strSql.Append(" id,brandName,brandImgUrl,remark,isLock,managerId,sort_id,add_time,update_time ");
            strSql.Append(" FROM " + databaseprefix + "brand ");
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
            strSql.Append("select * FROM " + databaseprefix + "brand");
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