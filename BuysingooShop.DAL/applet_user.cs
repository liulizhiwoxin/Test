using BuysingooShop.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuysingooShop.DAL
{
    /// <summary>
    /// 小程序 用户表  访问类
    /// </summary>
    public partial class applet_user
    {
        private string databaseprefix; //数据库表名前缀
        public applet_user(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }

        /// <summary>
        /// 是否存在该记录  id
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from "+ databaseprefix + "applet_user");
            strSql.Append(" where id=@ID");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)
            };
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 根据openid查询是否存在此记录
        /// </summary>
        public bool Exists_openid(string ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "applet_user");
            strSql.Append(" where user_openid=@ID");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.NVarChar,50)
            };
            parameters[0].Value = ID;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="set"> 修改语句</param>
        /// <param name="where">修改的条件</param>
        /// <returns>true/false</returns>
        public bool Update(string set, string where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update "+databaseprefix+ "applet_user set ");
            strSql.Append(""+set+"");
            strSql.Append(" where "+where+"");

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
        /// 添加数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(Model.applet_user model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "applet_user(");
            strSql.Append("user_name,user_sex,user_country,user_province,user_city,user_avatarUrl,add_time,user_openid,user_kindling_num)");
            strSql.Append(" values (");
            strSql.Append("@user_name,@user_sex,@user_country,@user_province,@user_city,@user_avatarUrl,@add_time,@user_openid,@user_kindling_num)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                new SqlParameter("@user_name",SqlDbType.NVarChar,50),
                new SqlParameter("@user_sex",SqlDbType.NVarChar,50),
                new SqlParameter("@user_country",SqlDbType.NVarChar,50),
                new SqlParameter("@user_province",SqlDbType.NVarChar,50),
                new SqlParameter("@user_city",SqlDbType.NVarChar,50),
                new SqlParameter("@user_avatarUrl",SqlDbType.NVarChar,250),
                new SqlParameter("@add_time",SqlDbType.DateTime),
                new SqlParameter("@user_openid",SqlDbType.NVarChar,50),
                new SqlParameter("@user_kindling_num",SqlDbType.Float,4)
            };
            parameters[0].Value = model.user_name;
            parameters[1].Value = model.user_sex;
            parameters[2].Value = model.user_country;
            parameters[3].Value = model.user_province;
            parameters[4].Value = model.user_city;
            parameters[5].Value = model.user_avatarUrl;
            parameters[6].Value = DateTime.Now.ToString();
            parameters[7].Value = model.user_openid;
            parameters[8].Value = model.user_kindling_num;

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

        // <summary>
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
            strSql.Append(" * ");
            strSql.Append(" FROM "+databaseprefix+"applet_user ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }


        /// <summary>
        /// 根据openid获取火苗数
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="filedOrder"></param>
        /// <returns></returns>
        public double GetList_user_kindling_num(string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(" user_kindling_num ");
            strSql.Append(" FROM " + databaseprefix + "applet_user ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            DataTable dt=DbHelperSQL.Query(strSql.ToString()).Tables[0];
            return Convert.ToDouble(dt.Rows[0]["user_kindling_num"].ToString());
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "applet_user ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters1 = {
                    new SqlParameter("@id", SqlDbType.Int,4)};
            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters1);
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


    }
}
