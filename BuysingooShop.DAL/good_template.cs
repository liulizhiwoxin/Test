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
    /// 数据访问类:模板
    /// </summary>
    public partial class good_template
    {
        private string databaseprefix; //数据库表名前缀
        public good_template(string _databaseprefix)
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
            strSql.Append("select count(1) from " + databaseprefix + "good_template");
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
            strSql.Append("select top 1 name from " + databaseprefix + "good_template");
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
        public int Add(Model.good_template model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "good_template(");
            strSql.Append("goodId,categoryId,name,remark,sort_id,addTime,isLock,isDefault,img_url,isAd,sort_ad,img_url1)");
            strSql.Append(" values (");
            strSql.Append("@goodId,@categoryId,@name,@remark,@sort_id,@addTime,@isLock,@isDefault,@img_url,@isAd,@sort_ad,@img_url1)");
            strSql.Append(";set @ReturnValue= @@IDENTITY");
            SqlParameter[] parameters = {
					            new SqlParameter("@goodId", SqlDbType.Int,4),
					            new SqlParameter("@categoryId", SqlDbType.Int,4),
                                new SqlParameter("@name", SqlDbType.NVarChar,50),
                                new SqlParameter("@remark", SqlDbType.NVarChar,500),
					            new SqlParameter("@sort_id", SqlDbType.Int,4),
					            new SqlParameter("@addTime", SqlDbType.DateTime),
					            new SqlParameter("@isLock", SqlDbType.Int,4),
                                new SqlParameter("@isDefault", SqlDbType.Int,4),
                                new SqlParameter("@img_url",SqlDbType.NVarChar,255),
                                new SqlParameter("@isAd", SqlDbType.Int,4),
                                new SqlParameter("@sort_ad", SqlDbType.Int,4),
                                new SqlParameter("@img_url1",SqlDbType.NVarChar,255),
                                new SqlParameter("@ReturnValue",SqlDbType.Int)};
            parameters[0].Value = model.goodId;
            parameters[1].Value = model.categoryId;
            parameters[2].Value = model.name;
            parameters[3].Value = model.remark;
            parameters[4].Value = model.sort_id;
            parameters[5].Value = model.addTime;
            parameters[6].Value = model.isLock;
            parameters[7].Value = model.isDefault;
            parameters[8].Value = model.img_url;
            parameters[9].Value = model.isAd;
            parameters[10].Value = model.sort_ad;
            parameters[11].Value = model.img_url1;
            parameters[12].Direction = ParameterDirection.Output;

            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            //模板图片
            if (model.pics != null)
            {
                StringBuilder strSql2;
                foreach (Model.good_template_pic modelt in model.pics)
                {
                    strSql2 = new StringBuilder();
                    strSql2.Append("insert into " + databaseprefix + "good_template_pic(");
                    strSql2.Append("templateId,typeId,picUrl,smallPicUrl,addTime,isLock)");
                    strSql2.Append(" values (");
                    strSql2.Append("@templateId,@typeId,@picUrl,@smallPicUrl,@addTime,@isLock)");
                    SqlParameter[] parameters2 = {
					    new SqlParameter("@templateId", SqlDbType.Int,4),
                        new SqlParameter("@typeId", SqlDbType.Int,4),
					    new SqlParameter("@picUrl", SqlDbType.NVarChar,100),
					    new SqlParameter("@smallPicUrl", SqlDbType.NVarChar,100),
					    new SqlParameter("@addTime", SqlDbType.DateTime),
                        new SqlParameter("@isLock",SqlDbType.Int,4)};
                    parameters2[0].Direction = ParameterDirection.InputOutput;
                    parameters2[1].Value = modelt.typeId;
                    parameters2[2].Value = modelt.picUrl;
                    parameters2[3].Value = modelt.smallPicUrl;
                    parameters2[4].Value = modelt.addTime;
                    parameters2[5].Value = modelt.isLock;
                    cmd = new CommandInfo(strSql2.ToString(), parameters2);
                    sqllist.Add(cmd);
                }
            }

            DbHelperSQL.ExecuteSqlTranWithIndentity(sqllist);
            return (int)parameters[12].Value;
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "good_template set " + strValue);
            strSql.Append(" where id=" + id);
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }
        
        /// <summary>
        /// 修改数据
        /// </summary>
        public void UpdateField(string strValue,string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "good_template set " + strValue);
            if(!string.IsNullOrEmpty(strWhere))
            {
            strSql.Append(" where "+strWhere);
            }
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.good_template model)
        {
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("update " + databaseprefix + "good_template set ");
                        strSql.Append("goodId=@goodId,");
                        strSql.Append("categoryId=@categoryId,");
                        strSql.Append("name=@name,");
                        strSql.Append("remark=@remark,");
                        strSql.Append("sort_id=@sort_id,");
                        strSql.Append("addTime=@addTime,");
                        strSql.Append("isLock=@isLock,");
                        strSql.Append("isDefault=@isDefault,");
                        strSql.Append("img_url=@img_url,");
                        strSql.Append("isAd=@isAd,");
                        strSql.Append("sort_ad=@sort_ad,");
                        strSql.Append("img_url1=@img_url1");
                        strSql.Append(" where id=@id");
                        SqlParameter[] parameters = {
					            new SqlParameter("@goodId", SqlDbType.Int,4),
					            new SqlParameter("@categoryId", SqlDbType.Int,4),
                                new SqlParameter("@name", SqlDbType.NVarChar,50),
                                new SqlParameter("@remark", SqlDbType.NVarChar,500),
					            new SqlParameter("@sort_id", SqlDbType.Int,4),
					            new SqlParameter("@addTime", SqlDbType.DateTime),
					            new SqlParameter("@isLock", SqlDbType.Int,4),
                                new SqlParameter("@isDefault", SqlDbType.Int,4),
                                new SqlParameter("@img_url",SqlDbType.NVarChar,255),
                                new SqlParameter("@isAd", SqlDbType.Int,4),
                                new SqlParameter("@sort_ad", SqlDbType.Int,4),
                                new SqlParameter("@img_url1",SqlDbType.NVarChar,255),
                                new SqlParameter("@id", SqlDbType.Int,4)};
                        parameters[0].Value = model.goodId;
                        parameters[1].Value = model.categoryId;
                        parameters[2].Value = model.name;
                        parameters[3].Value = model.remark;
                        parameters[4].Value = model.sort_id;
                        parameters[5].Value = model.addTime;
                        parameters[6].Value = model.isLock;
                        parameters[7].Value = model.isDefault;
                        parameters[8].Value = model.img_url;
                        parameters[9].Value = model.isAd;
                        parameters[10].Value = model.sort_ad;
                        parameters[11].Value = model.img_url1;
                        parameters[12].Value = model.id;
                        DbHelperSQL.ExecuteSql(conn, trans, strSql.ToString(), parameters);

                        //删除已删除的图片
                        new good_template_pic(databaseprefix).DeleteList(conn, trans, model.pics, model.id);
                        //添加/修改图片
                        if (model.pics != null)
                        {
                            StringBuilder strSql3;
                            foreach (Model.good_template_pic modelt in model.pics)
                            {
                                strSql3 = new StringBuilder();
                                if (modelt.id > 0)
                                {
                                    strSql3.Append("update " + databaseprefix + "good_template_pic set ");
                                    strSql3.Append("templateId=@templateId,");
                                    strSql3.Append("typeId=@typeId,");
                                    strSql3.Append("picUrl=@picUrl,");
                                    strSql3.Append("smallPicUrl=@smallPicUrl,");
                                    strSql3.Append("addTime=@addTime,");
                                    strSql3.Append("isLock=@isLock");
                                    strSql3.Append(" where id=@id");
                                    SqlParameter[] parameters3 = {
                                            new SqlParameter("@templateId", SqlDbType.Int,4),
					                        new SqlParameter("@typeId", SqlDbType.Int,4),
                                            new SqlParameter("@picUrl", SqlDbType.NVarChar,100),
					                        new SqlParameter("@smallPicUrl", SqlDbType.NVarChar,100),
					                        new SqlParameter("@addTime", SqlDbType.DateTime),
					                        new SqlParameter("@isLock", SqlDbType.Int,4),
                                            new SqlParameter("@id", SqlDbType.Int,4)};
                                    parameters3[0].Value = modelt.templateId;
                                    parameters3[1].Value = modelt.typeId;
                                    parameters3[2].Value = modelt.picUrl;
                                    parameters3[3].Value = modelt.smallPicUrl;
                                    parameters3[4].Value = modelt.addTime;
                                    parameters3[5].Value = modelt.isLock;
                                    parameters3[6].Value = modelt.id;
                                    DbHelperSQL.ExecuteSql(conn, trans, strSql3.ToString(), parameters3);
                                }
                                else
                                {
                                    strSql3.Append("insert into " + databaseprefix + "good_template_pic(");
                                    strSql3.Append("templateId,typeId,picUrl,smallPicUrl,addTime,isLock)");
                                    strSql3.Append(" values (");
                                    strSql3.Append("@templateId,@typeId,@picUrl,@smallPicUrl,@addTime,@isLock)");
                                    SqlParameter[] parameters3 = {
                                            new SqlParameter("@templateId", SqlDbType.Int,4),
					                        new SqlParameter("@typeId", SqlDbType.Int,4),
                                            new SqlParameter("@picUrl", SqlDbType.NVarChar,100),
					                        new SqlParameter("@smallPicUrl", SqlDbType.NVarChar,100),
					                        new SqlParameter("@addTime", SqlDbType.DateTime),
					                        new SqlParameter("@isLock", SqlDbType.Int,4)};
                                    parameters3[0].Value = modelt.templateId;
                                    parameters3[1].Value = modelt.typeId;
                                    parameters3[2].Value = modelt.picUrl;
                                    parameters3[3].Value = modelt.smallPicUrl;
                                    parameters3[4].Value = modelt.addTime;
                                    parameters3[5].Value = modelt.isLock;
                                    DbHelperSQL.ExecuteSql(conn, trans, strSql3.ToString(), parameters3);
                                }
                            }
                        }

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

            //取得模板图片MODEL
            List<Model.good_template_pic> piclist = new DAL.good_template_pic(databaseprefix).GetList(id);
            //删除图片
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("delete from " + databaseprefix + "good_template_pic ");
            strSql2.Append(" where templateId=@templateId ");
            SqlParameter[] parameters2 = {
					new SqlParameter("@templateId", SqlDbType.Int,4)};
            parameters2[0].Value = id;

            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo(strSql2.ToString(), parameters2);

            //删除主表
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "good_template ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            int rowsAffected = DbHelperSQL.ExecuteSqlTran(sqllist);
            if (rowsAffected > 0)
            {
                new DAL.good_template_pic(databaseprefix).DeleteFile(piclist); //删除图片
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
        public Model.good_template GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,goodId,categoryId,name,remark,sort_id,addTime,isLock,isDefault,img_url,isAd,sort_ad,img_url1 from " + databaseprefix + "good_template ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            Model.good_template model = new Model.good_template();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                #region 主表信息======================
                if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["goodId"] != null && ds.Tables[0].Rows[0]["goodId"].ToString() != "")
                {
                    model.goodId = int.Parse(ds.Tables[0].Rows[0]["goodId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["categoryId"] != null && ds.Tables[0].Rows[0]["categoryId"].ToString() != "")
                {
                    model.categoryId = int.Parse(ds.Tables[0].Rows[0]["categoryId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["name"] != null && ds.Tables[0].Rows[0]["name"].ToString() != "")
                {
                    model.name = ds.Tables[0].Rows[0]["name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["remark"] != null && ds.Tables[0].Rows[0]["remark"].ToString() != "")
                {
                    model.remark = ds.Tables[0].Rows[0]["remark"].ToString();
                }
                if (ds.Tables[0].Rows[0]["sort_id"] != null && ds.Tables[0].Rows[0]["sort_id"].ToString() != "")
                {
                    model.sort_id = int.Parse(ds.Tables[0].Rows[0]["sort_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["addTime"].ToString() != "")
                {
                    model.addTime = DateTime.Parse(ds.Tables[0].Rows[0]["addTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["isLock"] != null && ds.Tables[0].Rows[0]["isLock"].ToString() != "")
                {
                    model.isLock = int.Parse(ds.Tables[0].Rows[0]["isLock"].ToString());
                }
                if (ds.Tables[0].Rows[0]["isDefault"] != null && ds.Tables[0].Rows[0]["isDefault"].ToString() != "")
                {
                    model.isDefault = int.Parse(ds.Tables[0].Rows[0]["isDefault"].ToString());
                }
                if (ds.Tables[0].Rows[0]["img_url"] != null && ds.Tables[0].Rows[0]["img_url"].ToString() != "")
                {
                    model.img_url = ds.Tables[0].Rows[0]["img_url"].ToString();
                }
                if (ds.Tables[0].Rows[0]["isAd"] != null && ds.Tables[0].Rows[0]["isAd"].ToString() != "")
                {
                    model.isAd = int.Parse(ds.Tables[0].Rows[0]["isAd"].ToString());
                }
                if (ds.Tables[0].Rows[0]["sort_ad"] != null && ds.Tables[0].Rows[0]["sort_ad"].ToString() != "")
                {
                    model.sort_ad = int.Parse(ds.Tables[0].Rows[0]["sort_ad"].ToString());
                }
                if (ds.Tables[0].Rows[0]["img_url1"] != null && ds.Tables[0].Rows[0]["img_url1"].ToString() != "")
                {
                    model.img_url1 = ds.Tables[0].Rows[0]["img_url1"].ToString();
                }
                #endregion

                //相片信息
                model.pics = new good_template_pic(databaseprefix).GetList(id);

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
        public Model.good_template GetModel(SqlConnection conn, SqlTransaction trans, int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,goodId,categoryId,name,remark,sort_id,addTime,isLock,isDefault,img_url,isAd,sort_ad,img_url1 from " + databaseprefix + "good_template ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            Model.good_template model = new Model.good_template();
            DataSet ds = DbHelperSQL.Query(conn, trans, strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["goodId"] != null && ds.Tables[0].Rows[0]["goodId"].ToString() != "")
                {
                    model.goodId = int.Parse(ds.Tables[0].Rows[0]["goodId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["categoryId"] != null && ds.Tables[0].Rows[0]["categoryId"].ToString() != "")
                {
                    model.categoryId = int.Parse(ds.Tables[0].Rows[0]["categoryId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["name"] != null && ds.Tables[0].Rows[0]["name"].ToString() != "")
                {
                    model.name = ds.Tables[0].Rows[0]["name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["remark"] != null && ds.Tables[0].Rows[0]["remark"].ToString() != "")
                {
                    model.remark = ds.Tables[0].Rows[0]["remark"].ToString();
                }
                if (ds.Tables[0].Rows[0]["sort_id"] != null && ds.Tables[0].Rows[0]["sort_id"].ToString() != "")
                {
                    model.sort_id = int.Parse(ds.Tables[0].Rows[0]["sort_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["addTime"].ToString() != "")
                {
                    model.addTime = DateTime.Parse(ds.Tables[0].Rows[0]["addTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["isLock"] != null && ds.Tables[0].Rows[0]["isLock"].ToString() != "")
                {
                    model.isLock = int.Parse(ds.Tables[0].Rows[0]["isLock"].ToString());
                }
                if (ds.Tables[0].Rows[0]["isDefault"] != null && ds.Tables[0].Rows[0]["isDefault"].ToString() != "")
                {
                    model.isDefault = int.Parse(ds.Tables[0].Rows[0]["isDefault"].ToString());
                }
                if (ds.Tables[0].Rows[0]["img_url"] != null && ds.Tables[0].Rows[0]["img_url"].ToString() != "")
                {
                    model.img_url = ds.Tables[0].Rows[0]["img_url"].ToString();
                }
                if (ds.Tables[0].Rows[0]["isAd"] != null && ds.Tables[0].Rows[0]["isAd"].ToString() != "")
                {
                    model.isAd = int.Parse(ds.Tables[0].Rows[0]["isAd"].ToString());
                }
                if (ds.Tables[0].Rows[0]["sort_ad"] != null && ds.Tables[0].Rows[0]["sort_ad"].ToString() != "")
                {
                    model.sort_ad = int.Parse(ds.Tables[0].Rows[0]["sort_ad"].ToString());
                }
                if (ds.Tables[0].Rows[0]["img_url1"] != null && ds.Tables[0].Rows[0]["img_url1"].ToString() != "")
                {
                    model.img_url1 = ds.Tables[0].Rows[0]["img_url1"].ToString();
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
            strSql.Append(" id,goodId,categoryId,name,remark,sort_id,addTime,isLock,isDefault,img_url,isAd,sort_ad,img_url1 ");
            strSql.Append(" FROM " + databaseprefix + "good_template ");
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
            strSql.Append("select * FROM " + databaseprefix + "good_template");


            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(Vincent._DTcms.PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(Vincent._DTcms.PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int category_id, int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM " + databaseprefix + "good_template");
            if (category_id > 0)
            {
                strSql.Append(" where categoryId="+category_id);

            }
            if (strWhere.Trim() != "")
            {
                if (category_id > 0)
                {
                    strSql.Append(" and " + strWhere);
                }
                else
                {
                    strSql.Append(" where " + strWhere);
                }

            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(Vincent._DTcms.PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(Vincent._DTcms.PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }

        #endregion
    }
}