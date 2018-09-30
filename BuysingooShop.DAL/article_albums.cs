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
	/// 图片相册
	/// </summary>
	public partial class article_albums
	{
        private string databaseprefix; //数据库表名前缀
        public article_albums(string _databaseprefix)
		{
            databaseprefix = _databaseprefix;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.article_albums> GetList(int article_id)
        {
            List<Model.article_albums> modelList = new List<Model.article_albums>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,article_id,thumb_path,original_path,remark,add_time,category_id,img_size ");
            strSql.Append(" FROM " + databaseprefix + "article_albums ");
            strSql.Append(" where article_id=" + article_id);
            DataTable dt = DbHelperSQL.Query(strSql.ToString()).Tables[0];

            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.article_albums model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.article_albums();
                    if (dt.Rows[n]["id"] != null && dt.Rows[n]["id"].ToString() != "")
                    {
                        model.id = int.Parse(dt.Rows[n]["id"].ToString());
                    }
                    if (dt.Rows[n]["article_id"] != null && dt.Rows[n]["article_id"].ToString() != "")
                    {
                        model.article_id = int.Parse(dt.Rows[n]["article_id"].ToString());
                    }
                    if (dt.Rows[n]["thumb_path"] != null && dt.Rows[n]["thumb_path"].ToString() != "")
                    {
                        model.thumb_path = dt.Rows[n]["thumb_path"].ToString();
                    }
                    if (dt.Rows[n]["original_path"] != null && dt.Rows[n]["original_path"].ToString() != "")
                    {
                        model.original_path = dt.Rows[n]["original_path"].ToString();
                    }
                    if (dt.Rows[n]["remark"] != null && dt.Rows[n]["remark"].ToString() != "")
                    {
                        model.remark = dt.Rows[n]["remark"].ToString();
                    }
                    if (dt.Rows[0]["add_time"].ToString() != "")
                    {
                        model.add_time = DateTime.Parse(dt.Rows[0]["add_time"].ToString());
                    }
                    if (dt.Rows[n]["category_id"] != null && dt.Rows[n]["category_id"].ToString() != "")
                    {
                        model.category_id = int.Parse(dt.Rows[n]["category_id"].ToString());
                    }
                    if (dt.Rows[n]["img_size"] != null && dt.Rows[n]["img_size"].ToString() != "")
                    {
                        model.img_size = dt.Rows[n]["img_size"].ToString();
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 查找不存在的图片并删除已删除的图片及数据
        /// </summary>
        public void DeleteList(SqlConnection conn, SqlTransaction trans, List<Model.article_albums> models, int article_id)
        {
            StringBuilder idList = new StringBuilder();
            if (models != null)
            {
                foreach (Model.article_albums modelt in models)
                {
                    if (modelt.id > 0)
                    {
                        idList.Append(modelt.id + ",");
                    }
                }
            }
            string id_list = Vincent._DTcms.Utils.DelLastChar(idList.ToString(), ",");
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,thumb_path,original_path from " + databaseprefix + "article_albums where article_id=" + article_id);
            if (!string.IsNullOrEmpty(id_list))
            {
                strSql.Append(" and id not in(" + id_list + ")");
            }
            DataSet ds = DbHelperSQL.Query(conn, trans, strSql.ToString());
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                int rows = DbHelperSQL.ExecuteSql(conn, trans, "delete from " + databaseprefix + "article_albums where id=" + dr["id"].ToString()); //删除数据库
                if (rows > 0)
                {
                    Vincent._DTcms.Utils.DeleteFile(dr["thumb_path"].ToString()); //删除缩略图
                    Vincent._DTcms.Utils.DeleteFile(dr["original_path"].ToString()); //删除原图
                }
            }
        }

        /// <summary>
        /// 删除相册图片
        /// </summary>
        public void DeleteFile(List<Model.article_albums> models)
        {
            if (models != null)
            {
                foreach (Model.article_albums modelt in models)
                {
                    Vincent._DTcms.Utils.DeleteFile(modelt.thumb_path);
                    Vincent._DTcms.Utils.DeleteFile(modelt.original_path);
                }
            }
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,article_id,thumb_path,original_path,remark,add_time,category_id,img_size ");
            strSql.Append(" FROM dt_article_albums ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 返回对象实体
        /// </summary>
        public Model.article_albums GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,article_id,thumb_path,original_path,remark,add_time,category_id,img_size ");
            strSql.Append(" FROM dt_article_albums where article_id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            Model.article_albums model = new Model.article_albums();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string original_path="";
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    model.id = int.Parse(ds.Tables[0].Rows[i]["id"].ToString());
                    model.article_id = int.Parse(ds.Tables[0].Rows[i]["article_id"].ToString());
                    model.thumb_path = ds.Tables[0].Rows[i]["thumb_path"].ToString();
                    original_path += ds.Tables[0].Rows[i]["original_path"].ToString()+",";
                    if (ds.Tables[0].Rows[i]["remark"].ToString() != "")
                    {
                        model.remark = ds.Tables[0].Rows[i]["remark"].ToString();
                    }
                    if (ds.Tables[0].Rows[i]["add_time"].ToString() != "")
                    {
                        model.add_time = DateTime.Parse(ds.Tables[0].Rows[i]["add_time"].ToString());
                    }
                    model.category_id = int.Parse(ds.Tables[0].Rows[i]["category_id"].ToString());
                    model.img_size = ds.Tables[0].Rows[i]["img_size"].ToString();
                }
                model.original_path = original_path.Substring(0, original_path.Length - 1);
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
            strSql.Append(" id,article_id,thumb_path,original_path,remark,add_time,category_id,img_size ");
            strSql.Append(" FROM dt_article_albums ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

	}
}

