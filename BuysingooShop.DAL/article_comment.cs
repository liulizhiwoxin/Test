using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using BuysingooShop.DBUtility;
using Vincent;

namespace BuysingooShop.DAL
{
    /// <summary>
    /// 数据访问类:文章评论
    /// </summary>
    public partial class article_comment
    {
        private string databaseprefix; //数据库表名前缀
        public article_comment(string _databaseprefix)
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
            strSql.Append("select count(1) from " + databaseprefix + "article_comment");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 返回数据总数(AJAX分页用到)
        /// </summary>
        public int GetCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H ");
            strSql.Append(" from " + databaseprefix + "article_comment ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.article_comment model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "article_comment(");
            strSql.Append("channel_id,article_id,parent_id,user_id,user_name,user_ip,content,is_lock,add_time,is_reply,reply_content,reply_time,data_type,comment_type,order_id)");
            strSql.Append(" values (");
            strSql.Append("@channel_id,@article_id,@parent_id,@user_id,@user_name,@user_ip,@content,@is_lock,@add_time,@is_reply,@reply_content,@reply_time,@data_type,@comment_type,@order_id)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@channel_id", SqlDbType.Int,4),
					new SqlParameter("@article_id", SqlDbType.Int,4),
					new SqlParameter("@parent_id", SqlDbType.Int,4),
					new SqlParameter("@user_id", SqlDbType.Int,4),
					new SqlParameter("@user_name", SqlDbType.NVarChar,100),
					new SqlParameter("@user_ip", SqlDbType.NVarChar,255),
					new SqlParameter("@content", SqlDbType.NText),
					new SqlParameter("@is_lock", SqlDbType.TinyInt,1),
					new SqlParameter("@add_time", SqlDbType.DateTime),
					new SqlParameter("@is_reply", SqlDbType.TinyInt,1),
					new SqlParameter("@reply_content", SqlDbType.NText),
					new SqlParameter("@reply_time", SqlDbType.DateTime),
                    new SqlParameter("@data_type", SqlDbType.TinyInt,1),
                    new SqlParameter("@comment_type", SqlDbType.TinyInt,1),
                    new SqlParameter("@order_id", SqlDbType.NVarChar,100)};
            parameters[0].Value = model.channel_id;
            parameters[1].Value = model.article_id;
            parameters[2].Value = model.parent_id;
            parameters[3].Value = model.user_id;
            parameters[4].Value = model.user_name;
            parameters[5].Value = model.user_ip;
            parameters[6].Value = model.content;
            parameters[7].Value = model.is_lock;
            parameters[8].Value = model.add_time;
            parameters[9].Value = model.is_reply;
            parameters[10].Value = model.reply_content;
            parameters[11].Value = model.reply_time;
            parameters[12].Value = model.data_type;
            parameters[13].Value = model.comment_type;
            parameters[14].Value = model.order_id;

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
            strSql.Append("update " + databaseprefix + "article_comment set " + strValue);
            strSql.Append(" where id=" + id);
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.article_comment model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "article_comment set ");
            strSql.Append("channel_id=@channel_id,");
            strSql.Append("article_id=@article_id,");
            strSql.Append("parent_id=@parent_id,");
            strSql.Append("user_id=@user_id,");
            strSql.Append("user_name=@user_name,");
            strSql.Append("user_ip=@user_ip,");
            strSql.Append("content=@content,");
            strSql.Append("is_lock=@is_lock,");
            strSql.Append("add_time=@add_time,");
            strSql.Append("is_reply=@is_reply,");
            strSql.Append("reply_content=@reply_content,");
            strSql.Append("reply_time=@reply_time,");
            strSql.Append("data_type=@data_type,");
            strSql.Append("comment_type=@comment_type,");
            strSql.Append("order_id=@order_id");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@channel_id", SqlDbType.Int,4),
					new SqlParameter("@article_id", SqlDbType.Int,4),
					new SqlParameter("@parent_id", SqlDbType.Int,4),
					new SqlParameter("@user_id", SqlDbType.Int,4),
					new SqlParameter("@user_name", SqlDbType.NVarChar,100),
					new SqlParameter("@user_ip", SqlDbType.NVarChar,255),
					new SqlParameter("@content", SqlDbType.NText),
					new SqlParameter("@is_lock", SqlDbType.TinyInt,1),
					new SqlParameter("@add_time", SqlDbType.DateTime),
					new SqlParameter("@is_reply", SqlDbType.TinyInt,1),
					new SqlParameter("@reply_content", SqlDbType.NText),
					new SqlParameter("@reply_time", SqlDbType.DateTime),
                    new SqlParameter("@data_type", SqlDbType.TinyInt,1),
                    new SqlParameter("@comment_type", SqlDbType.TinyInt,1),
                    new SqlParameter("@order_id", SqlDbType.NVarChar,100)};
            parameters[0].Value = model.id;
            parameters[1].Value = model.channel_id;
            parameters[2].Value = model.article_id;
            parameters[3].Value = model.parent_id;
            parameters[4].Value = model.user_id;
            parameters[5].Value = model.user_name;
            parameters[6].Value = model.user_ip;
            parameters[7].Value = model.content;
            parameters[8].Value = model.is_lock;
            parameters[9].Value = model.add_time;
            parameters[10].Value = model.is_reply;
            parameters[11].Value = model.reply_content;
            parameters[12].Value = model.reply_time;
            parameters[13].Value = model.data_type;
            parameters[14].Value = model.comment_type;
            parameters[15].Value = model.order_id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return rows > 0 ? true : false;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "article_comment ");
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
            strSql.Append("delete from " + databaseprefix + "article_comment ");
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
        public Model.article_comment GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,channel_id,article_id,parent_id,user_id,user_name,user_ip,content,is_lock,add_time,is_reply,reply_content,reply_time,data_type,comment_type,order_id");
            strSql.Append(" from " + databaseprefix + "article_comment ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            Model.article_comment model = new Model.article_comment();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["channel_id"].ToString() != "")
                {
                    model.channel_id = int.Parse(ds.Tables[0].Rows[0]["channel_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["article_id"].ToString() != "")
                {
                    model.article_id = int.Parse(ds.Tables[0].Rows[0]["article_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["parent_id"].ToString() != "")
                {
                    model.parent_id = int.Parse(ds.Tables[0].Rows[0]["parent_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["user_id"].ToString() != "")
                {
                    model.user_id = int.Parse(ds.Tables[0].Rows[0]["user_id"].ToString());
                }
                model.user_name = ds.Tables[0].Rows[0]["user_name"].ToString();
                model.user_ip = ds.Tables[0].Rows[0]["user_ip"].ToString();
                model.content = ds.Tables[0].Rows[0]["content"].ToString();
                if (ds.Tables[0].Rows[0]["is_lock"].ToString() != "")
                {
                    model.is_lock = int.Parse(ds.Tables[0].Rows[0]["is_lock"].ToString());
                }
                if (ds.Tables[0].Rows[0]["add_time"].ToString() != "")
                {
                    model.add_time = DateTime.Parse(ds.Tables[0].Rows[0]["add_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["is_reply"].ToString() != "")
                {
                    model.is_reply = int.Parse(ds.Tables[0].Rows[0]["is_reply"].ToString());
                }
                model.reply_content = ds.Tables[0].Rows[0]["reply_content"].ToString();
                if (ds.Tables[0].Rows[0]["reply_time"].ToString() != "")
                {
                    model.reply_time = DateTime.Parse(ds.Tables[0].Rows[0]["reply_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["data_type"].ToString() != "")
                {
                    model.data_type = int.Parse(ds.Tables[0].Rows[0]["data_type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["comment_type"].ToString() != "")
                {
                    model.comment_type = int.Parse(ds.Tables[0].Rows[0]["comment_type"].ToString());
                }
                model.order_id = int.Parse(ds.Tables[0].Rows[0]["order_id"].ToString());
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
            strSql.Append(" id,channel_id,article_id,parent_id,user_id,user_name,user_ip,content,is_lock,add_time,is_reply,reply_content,reply_time,data_type,comment_type,order_id ");
            strSql.Append(" FROM " + databaseprefix + "article_comment ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 查询已评价的订单
        /// </summary>
        public DataSet GetMultiList(int Top, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            //SELECT  t1.goods_id ,t1.goods_title ,t1.real_price ,t1.quantity ,t1.goods_pic ,t1.point ,t1.goods_price ,t1.order_id  ,t1.type ,t1.weight,
            //       t2.content ,t2.add_time ,t2.comment_type ,t2.id ,t3.status ,t3.order_no,t3.add_time,t4.user_name,t4.avatar,t4.id AS userId
            //FROM    dbo.dt_order_goods t1
            //        JOIN dbo.dt_article_comment t2 ON t1.goods_id = t2.article_id
            //        JOIN dt_orders t3 ON t3.id = t1.order_id
            //        JOIN dt_users t4 ON t3.user_id=t4.id
            strSql.Append(" t1.goods_id, t1.goods_title, t1.real_price, t1.quantity, t1.goods_pic, t1.point, t1.goods_price ,t1.order_id  ,t1.type ,t1.weight, ");
            strSql.Append(" t2.content, t2.add_time, t2.comment_type,t2.id,  ");
            strSql.Append(" t3.status,t3.order_no,t3.add_time,t3.payment_status, ");
            strSql.Append(" t4.user_name,t4.avatar,t4.id AS userId ");
            strSql.Append(" FROM    dbo.dt_orders t3 ");
            strSql.Append(" JOIN dbo.dt_article_comment t2 ON t3.id = t2.order_id ");
            strSql.Append(" JOIN dt_order_goods t1 ON t3.id = t1.order_id ");
            strSql.Append(" JOIN dt_users t4 ON t3.user_id=t4.id ");

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" SELECT COUNT(goods_id) FROM dbo.dt_order_goods t1  JOIN dbo.dt_article_comment t2 ON t1.goods_id = t2.article_id  JOIN dt_orders t3 ON t3.id = t1.order_id  JOIN dt_users t4 ON t3.user_id=t4.id where "+ strWhere);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 查询未评价的订单
        /// </summary>
        public DataSet GetMultiLists(int Top, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            //SELECT  t1.goods_id ,t1.goods_title ,t1.real_price ,t1.quantity ,t1.goods_pic ,t1.point ,t1.goods_price ,t1.order_id  ,t1.type ,t1.weight,
            //       t2.content ,t2.add_time ,t2.comment_type ,t2.id ,t3.status ,t3.order_no,t3.add_time,t4.user_name,t4.avatar,t4.id AS userId
            //FROM    dbo.dt_order_goods t1
            //        JOIN dbo.dt_article_comment t2 ON t1.goods_id = t2.article_id
            //        JOIN dt_orders t3 ON t3.id = t1.order_id
            //        JOIN dt_users t4 ON t3.user_id=t4.id
            strSql.Append(" t1.goods_id,t1.order_id,t1.goods_pic,t1.type,t1.goods_title, ");
            strSql.Append(" t2.order_no,t2.payment_status, ");
            strSql.Append(" t3.add_time,t3.content,t3.comment_type ");
            strSql.Append(" FROM dbo.dt_order_goods t1 ");
            strSql.Append(" INNER JOIN dbo.dt_orders t2 ON t1.order_id=t2.id ");
            strSql.Append(" LEFT JOIN dbo.dt_article_comment t3 ON t3.order_id=t2.id ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" SELECT COUNT(goods_id) FROM dbo.dt_order_goods t1  JOIN dbo.dt_article_comment t3 ON t1.goods_id = t3.article_id  JOIN dt_orders t2 ON t2.id = t1.order_id  JOIN dt_users t4 ON t3.user_id=t4.id  where " + strWhere);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 删除订单评论
        /// </summary>
        public void DeleteListConsult(int Top, int strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from dt_article_comment");
            strSql.Append(" where id="+strWhere);
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 查询已咨询的订单
        /// </summary>
        public DataSet GetConsultList(int Top, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" t1.goods_id, t1.goods_title, t1.real_price, t1.quantity, t1.goods_pic, t1.point, t1.goods_price ,t1.order_id  ,t1.type ,t1.weight, ");
            strSql.Append(" t2.content, t2.add_time, t2.comment_type,t2.id,  ");
            strSql.Append(" t3.status,t3.order_no,t3.add_time, ");
            strSql.Append(" t4.user_name,t4.avatar,t4.id AS userId ");
            strSql.Append(" FROM    dbo.dt_order_goods t1 ");
            strSql.Append(" JOIN dbo.dt_article_comment t2 ON t1.goods_id = t2.article_id ");
            strSql.Append(" JOIN dt_orders t3 ON t3.id = t1.order_id ");
            strSql.Append(" JOIN dt_users t4 ON t3.user_id=t4.id ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" SELECT COUNT(goods_id) FROM dbo.dt_order_goods t1  JOIN dbo.dt_article_comment t2 ON t1.goods_id = t2.article_id  JOIN dt_orders t3 ON t3.id = t1.order_id  JOIN dt_users t4 ON t3.user_id=t4.id where " + strWhere);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM " + databaseprefix + "article_comment");
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