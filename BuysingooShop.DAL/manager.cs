using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using BuysingooShop.DBUtility;
using Vincent;

namespace BuysingooShop.DAL
{
    /// <summary>
    /// 数据访问类:管理员
    /// </summary>
    public partial class manager
    {
        private string databaseprefix; //数据库表名前缀
        public manager(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }
        #region 基本方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "manager");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 查询用户名是否存在
        /// </summary>
        public bool Exists(string user_name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "manager");
            strSql.Append(" where user_name=@user_name ");
            SqlParameter[] parameters = {
					new SqlParameter("@user_name", SqlDbType.NVarChar,100)};
            parameters[0].Value = user_name;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 查询折扣码是否存在
        /// </summary>
        public bool ExistsCode(string str_code)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "manager");
            strSql.Append(" where str_code=@str_code ");
            SqlParameter[] parameters = {
					new SqlParameter("@str_code", SqlDbType.NVarChar,100)};
            parameters[0].Value = str_code;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 根据用户名取得Salt
        /// </summary>
        public string GetSalt(string user_name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 salt from " + databaseprefix + "manager");
            strSql.Append(" where user_name=@user_name");
            SqlParameter[] parameters = {
                    new SqlParameter("@user_name", SqlDbType.NVarChar,100)};
            parameters[0].Value = user_name;
            string salt = Convert.ToString(DbHelperSQL.GetSingle(strSql.ToString(), parameters));
            if (string.IsNullOrEmpty(salt))
            {
                return "";
            }
            return salt;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.manager model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "manager(");
            strSql.Append("role_id,role_type,user_name,password,salt,real_name,telephone,email,is_lock,add_time,winery_id,str_code,brand_id,str_code_rage,age,workAge,style,img_url,remark,QQ)");
            strSql.Append(" values (");
            strSql.Append("@role_id,@role_type,@user_name,@password,@salt,@real_name,@telephone,@email,@is_lock,@add_time,@winery_id,@str_code,@brand_id,@str_code_rage,@age,@workAge,@style,@img_url,@remark,@QQ)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@role_id", SqlDbType.Int,4),
					new SqlParameter("@role_type", SqlDbType.Int,4),
					new SqlParameter("@user_name", SqlDbType.NVarChar,100),
					new SqlParameter("@password", SqlDbType.NVarChar,100),
					new SqlParameter("@salt", SqlDbType.NVarChar,20),
					new SqlParameter("@real_name", SqlDbType.NVarChar,50),
					new SqlParameter("@telephone", SqlDbType.NVarChar,30),
					new SqlParameter("@email", SqlDbType.NVarChar,30),
					new SqlParameter("@is_lock", SqlDbType.Int,4),
					new SqlParameter("@add_time", SqlDbType.DateTime),
                    new SqlParameter("@winery_id", SqlDbType.Int,4),
                    new SqlParameter("@str_code", SqlDbType.NVarChar,50),
                    new SqlParameter("@brand_id", SqlDbType.Int,4),
                    new SqlParameter("@str_code_rage", SqlDbType.NVarChar,50),
                    new SqlParameter("@age", SqlDbType.Int,4),
                    new SqlParameter("@workAge", SqlDbType.Int,4),
                    new SqlParameter("@style", SqlDbType.NVarChar,50),
                    new SqlParameter("@img_url", SqlDbType.NVarChar,50),
                    new SqlParameter("@remark", SqlDbType.NVarChar,100),
                    new SqlParameter("@QQ", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.role_id;
            parameters[1].Value = model.role_type;
            parameters[2].Value = model.user_name;
            parameters[3].Value = model.password;
            parameters[4].Value = model.salt;
            parameters[5].Value = model.real_name;
            parameters[6].Value = model.telephone;
            parameters[7].Value = model.email;
            parameters[8].Value = model.is_lock;
            parameters[9].Value = model.add_time;
            parameters[10].Value = model.winery_id;
            parameters[11].Value = model.str_code;
            parameters[12].Value = model.brand_id;
            parameters[13].Value = model.str_code_rage;
            parameters[14].Value = model.age;
            parameters[15].Value = model.workAge;
            parameters[16].Value = model.style;
            parameters[17].Value = model.img_url;
            parameters[18].Value = model.remark;
            parameters[19].Value = model.QQ;

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
        public bool Update(Model.manager model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "manager set ");
            strSql.Append("role_id=@role_id,");
            strSql.Append("role_type=@role_type,");
            strSql.Append("user_name=@user_name,");
            strSql.Append("password=@password,");
            strSql.Append("real_name=@real_name,");
            strSql.Append("telephone=@telephone,");
            strSql.Append("email=@email,");
            strSql.Append("is_lock=@is_lock,");
            strSql.Append("add_time=@add_time,");
            strSql.Append("winery_id=@winery_id,");
            strSql.Append("str_code=@str_code,");
            strSql.Append("brand_id=@brand_id,");
            strSql.Append("str_code_rage=@str_code_rage,");
            strSql.Append("age=@age,");
            strSql.Append("workAge=@workAge,");
            strSql.Append("style=@style,");
            strSql.Append("img_url=@img_url,");
            strSql.Append("remark=@remark,");
            strSql.Append("QQ=@QQ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@role_id", SqlDbType.Int,4),
					new SqlParameter("@role_type", SqlDbType.Int,4),
					new SqlParameter("@user_name", SqlDbType.NVarChar,100),
					new SqlParameter("@password", SqlDbType.NVarChar,100),
					new SqlParameter("@real_name", SqlDbType.NVarChar,50),
					new SqlParameter("@telephone", SqlDbType.NVarChar,30),
					new SqlParameter("@email", SqlDbType.NVarChar,30),
					new SqlParameter("@is_lock", SqlDbType.Int,4),
					new SqlParameter("@add_time", SqlDbType.DateTime),
                    new SqlParameter("@winery_id", SqlDbType.Int,4),
                    new SqlParameter("@str_code", SqlDbType.NVarChar,50),
                    new SqlParameter("@brand_id", SqlDbType.Int,4),
                    new SqlParameter("@str_code_rage", SqlDbType.NVarChar,50),
                    new SqlParameter("@age", SqlDbType.Int,4),
                    new SqlParameter("@workAge", SqlDbType.Int,4),
                    new SqlParameter("@style", SqlDbType.NVarChar,50),
                    new SqlParameter("@img_url", SqlDbType.NVarChar,50),
                    new SqlParameter("@remark", SqlDbType.NVarChar,100),
                    new SqlParameter("@QQ", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.id;
            parameters[1].Value = model.role_id;
            parameters[2].Value = model.role_type;
            parameters[3].Value = model.user_name;
            parameters[4].Value = model.password;
            parameters[5].Value = model.real_name;
            parameters[6].Value = model.telephone;
            parameters[7].Value = model.email;
            parameters[8].Value = model.is_lock;
            parameters[9].Value = model.add_time;
            parameters[10].Value = model.winery_id;
            parameters[11].Value = model.str_code;
            parameters[12].Value = model.brand_id;
            parameters[13].Value = model.str_code_rage;
            parameters[14].Value = model.age;
            parameters[15].Value = model.workAge;
            parameters[16].Value = model.style;
            parameters[17].Value = model.img_url;
            parameters[18].Value = model.remark;
            parameters[19].Value = model.QQ;

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
        public bool Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "manager ");
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
        public Model.manager GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,role_id,role_type,user_name,password,salt,real_name,telephone,email,is_lock,add_time,winery_id,str_code,brand_id,str_code_rage,age,workAge,style,img_url,remark,QQ from " + databaseprefix + "manager ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            return DataSetToModel(ds);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public Model.manager DataSetToModel(DataSet ds)
        {
            Model.manager model = new Model.manager();
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["role_id"].ToString() != "")
                {
                    model.role_id = int.Parse(ds.Tables[0].Rows[0]["role_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["role_type"].ToString() != "")
                {
                    model.role_type = int.Parse(ds.Tables[0].Rows[0]["role_type"].ToString());
                }
                model.user_name = ds.Tables[0].Rows[0]["user_name"].ToString();
                model.password = ds.Tables[0].Rows[0]["password"].ToString();
                model.salt = ds.Tables[0].Rows[0]["salt"].ToString();
                model.real_name = ds.Tables[0].Rows[0]["real_name"].ToString();
                model.telephone = ds.Tables[0].Rows[0]["telephone"].ToString();
                model.email = ds.Tables[0].Rows[0]["email"].ToString();
                if (ds.Tables[0].Rows[0]["is_lock"].ToString() != "")
                {
                    model.is_lock = int.Parse(ds.Tables[0].Rows[0]["is_lock"].ToString());
                }
                if (ds.Tables[0].Rows[0]["add_time"].ToString() != "")
                {
                    model.add_time = DateTime.Parse(ds.Tables[0].Rows[0]["add_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["winery_id"].ToString() != "")
                {
                    model.winery_id = int.Parse(ds.Tables[0].Rows[0]["winery_id"].ToString());
                }
                model.str_code = ds.Tables[0].Rows[0]["str_code"].ToString();
                if (ds.Tables[0].Rows[0]["brand_id"].ToString() != "")
                {
                    model.brand_id = int.Parse(ds.Tables[0].Rows[0]["brand_id"].ToString());
                }
                model.str_code_rage = ds.Tables[0].Rows[0]["str_code_rage"].ToString();
                if (ds.Tables[0].Rows[0]["age"].ToString() != "")
                {
                    model.age = int.Parse(ds.Tables[0].Rows[0]["age"].ToString());
                }
                if (ds.Tables[0].Rows[0]["workAge"].ToString() != "")
                {
                    model.workAge = int.Parse(ds.Tables[0].Rows[0]["workAge"].ToString());
                }
                if (ds.Tables[0].Rows[0]["style"].ToString() != "")
                {
                    model.style = ds.Tables[0].Rows[0]["style"].ToString();
                }
                if (ds.Tables[0].Rows[0]["img_url"].ToString() != "")
                {
                    model.img_url = ds.Tables[0].Rows[0]["img_url"].ToString();
                }
                if (ds.Tables[0].Rows[0]["remark"].ToString() != "")
                {
                    model.remark = ds.Tables[0].Rows[0]["remark"].ToString();
                }
                if (ds.Tables[0].Rows[0]["QQ"].ToString() != "")
                {
                    model.QQ = ds.Tables[0].Rows[0]["QQ"].ToString();
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据用户名密码返回一个实体
        /// </summary>
        public Model.manager GetModel(string user_name, string password)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from " + databaseprefix + "manager");
            strSql.Append(" where user_name=@user_name and password=@password and is_lock=0");
            SqlParameter[] parameters = {
					new SqlParameter("@user_name", SqlDbType.NVarChar,100),
                    new SqlParameter("@password", SqlDbType.NVarChar,100)};
            parameters[0].Value = user_name;
            parameters[1].Value = password;

            DataSet dtSet = DbHelperSQL.Query(strSql.ToString(), parameters);
            return DataSetToModel(dtSet);
        }

        /// <summary>
        /// 根据用户名返回一个实体
        /// </summary>
        public Model.manager GetModel(string user_name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from " + databaseprefix + "manager");
            strSql.Append(" where user_name=@user_name and is_lock=0");
            SqlParameter[] parameters = {
					new SqlParameter("@user_name", SqlDbType.NVarChar,100)};
            parameters[0].Value = user_name;

            DataSet dtSet = DbHelperSQL.Query(strSql.ToString(),parameters);
            return DataSetToModel(dtSet);
        }

        /// <summary>
        /// 根据折扣代码返回一个实体
        /// </summary>
        public Model.manager GetModelCode(string strwhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * from " + databaseprefix + "manager");
            strSql.Append(" where is_lock=0 and " + strwhere);
            DataSet dtSet = DbHelperSQL.Query(strSql.ToString());
            return DataSetToModel(dtSet);
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
            strSql.Append(" id,role_id,role_type,user_name,password,salt,real_name,telephone,email,is_lock,add_time,winery_id,str_code,brand_id,str_code_rage,age,workAge,style,img_url,remark,QQ ");
            strSql.Append(" FROM " + databaseprefix + "manager ");
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
            strSql.Append("select * FROM " + databaseprefix + "manager");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(Vincent._DTcms.PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(Vincent._DTcms.PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }

        #endregion  Method
    }
}