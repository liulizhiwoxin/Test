using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;
using BuysingooShop.DBUtility;
using Vincent;

namespace BuysingooShop.DAL
{
    public partial class collect
    {
        private string databaseprefix; //数据库表名前缀
        public collect(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }


        /// <summary>
        /// 查询最大的ID
        /// </summary>
        public int GetMaxId()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 id from " + databaseprefix + "collect ORDER BY id DESC");
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }


        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "collect");
            strSql.Append(" where good_id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.collect model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "collect(");
            strSql.Append("user_id,good_id,title,good_type,good_price,img_url,add_time,is_usable)");
            strSql.Append(" values (");
            strSql.Append("@user_id,@good_id,@title,@good_type,@good_price,@img_url,@add_time,@is_usable)");
            SqlParameter[] parameters = {
					new SqlParameter("@user_id", SqlDbType.Int,4),
					new SqlParameter("@good_id", SqlDbType.Int,4),
                    new SqlParameter("@title", SqlDbType.NVarChar,100),
					new SqlParameter("@good_type", SqlDbType.NVarChar,100),
					new SqlParameter("@good_price", SqlDbType.Decimal,5),
                    new SqlParameter("@img_url", SqlDbType.NVarChar,100),
					new SqlParameter("@add_time", SqlDbType.DateTime),
					new SqlParameter("@is_usable", SqlDbType.NChar,10)};
            parameters[0].Value = model.user_id;
            parameters[1].Value = model.good_id;
            parameters[2].Value = model.title;
            parameters[3].Value = model.good_type;
            parameters[4].Value = model.good_price;
            parameters[5].Value = model.img_url;
            parameters[6].Value = model.add_time;
            parameters[7].Value = model.is_usable;

            int obj = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (obj <= 0)
            {
                return 0;
            }
            else
            {
                return GetMaxId();
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "collect ");
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
            strSql.Append(" id,user_id,good_id,good_type,good_price,add_time,is_usable,title,img_url ");
            strSql.Append(" FROM " + databaseprefix + "collect ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }
    }
}
