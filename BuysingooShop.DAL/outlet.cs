using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;
using BuysingooShop.DBUtility;
using Vincent;
using System.Collections;


namespace BuysingooShop.DAL
{
    public class outlet
    {
        private string databaseprefix; //数据库表名前缀

        public outlet(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "outlet");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.outlet model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "outlet set ");
            strSql.Append("name=@name,");
            strSql.Append("provinces=@provinces,");
            strSql.Append("city=@city,");
            strSql.Append("area=@area,");
            strSql.Append("street=@street,");
            strSql.Append("address=@address,");
            strSql.Append("mobile=@mobile,");
            strSql.Append("addtime=@addtime,");
            strSql.Append("x_zb=@x_zb,");
            strSql.Append("y_zb=@y_zb,");
            strSql.Append("img=@img,");
            strSql.Append("Linkman=@Linkman,");
            strSql.Append("WeChat=@WeChat,");
            strSql.Append("Other=@Other,");
            strSql.Append("userId=@userId,");
            strSql.Append("busintime=@busintime");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@name", SqlDbType.VarChar,100),
                    new SqlParameter("@provinces", SqlDbType.VarChar,100),
					new SqlParameter("@city", SqlDbType.VarChar,100),
                    new SqlParameter("@area", SqlDbType.VarChar,100),
                    new SqlParameter("@street", SqlDbType.VarChar,100),
					new SqlParameter("@address", SqlDbType.NVarChar,500),
                    new SqlParameter("@mobile", SqlDbType.VarChar,20),
                    new SqlParameter("@addtime", SqlDbType.DateTime),
                    new SqlParameter("@x_zb", SqlDbType.VarChar,50),
                    new SqlParameter("@y_zb", SqlDbType.VarChar,50),
                    new SqlParameter("@img", SqlDbType.VarChar,100),
                    new SqlParameter("@Linkman",SqlDbType.NVarChar,100),
                    new SqlParameter("@WeChat",SqlDbType.NVarChar,100),
                    new SqlParameter("@Other",SqlDbType.NVarChar,100),
                    new SqlParameter("@userId", SqlDbType.Int,4),
                    new SqlParameter("@busintime",SqlDbType.NVarChar,50)};
            parameters[0].Value = model.id;
            parameters[1].Value = model.name;
            parameters[2].Value = model.provinces;
            parameters[3].Value = model.city;
            parameters[4].Value = model.area;
            parameters[5].Value = model.street;
            parameters[6].Value = model.address;
            parameters[7].Value = model.mobile;
            parameters[8].Value = model.addtime;
            parameters[9].Value = model.x_zb;
            parameters[10].Value = model.y_zb;
            parameters[11].Value = model.img;
            parameters[12].Value = model.Linkman;
            parameters[13].Value = model.WeChat;
            parameters[14].Value = model.Other;
            parameters[15].Value = model.userId;
            parameters[16].Value = model.busintime;

            try
            {
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
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "outlet ");
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
        /// 增加一条数据
        /// </summary>
        public int Add(Model.outlet model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "outlet(");
            strSql.Append("name,provinces,city,area,street,address,mobile,addtime,x_zb,y_zb,img,busintime,Linkman,WeChat,Other,userId)");
            strSql.Append(" values (");
            strSql.Append("@name,@provinces,@city,@area,@street,@address,@mobile,@addtime,@x_zb,@y_zb,@img,@busintime,@Linkman,@WeChat,@Other,@userId)");
            strSql.Append(";set @ReturnValue= @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.VarChar,100),
                    new SqlParameter("@provinces", SqlDbType.VarChar,100),
					new SqlParameter("@city", SqlDbType.VarChar,100),
                    new SqlParameter("@area", SqlDbType.VarChar,100),
                    new SqlParameter("@street", SqlDbType.VarChar,100),
					new SqlParameter("@address", SqlDbType.NVarChar,500),
                    new SqlParameter("@mobile", SqlDbType.VarChar,20),
                    new SqlParameter("@addtime", SqlDbType.DateTime),
                    new SqlParameter("@x_zb", SqlDbType.VarChar,50),
                    new SqlParameter("@y_zb", SqlDbType.VarChar,50),
                    new SqlParameter("@img", SqlDbType.VarChar,100),
                    new SqlParameter("@busintime",SqlDbType.NVarChar,50),
                    new SqlParameter("@Linkman",SqlDbType.NVarChar,100),
                    new SqlParameter("@WeChat",SqlDbType.NVarChar,100),
                    new SqlParameter("@Other",SqlDbType.NVarChar,100),
                    new SqlParameter("@userId",SqlDbType.Int),
                    new SqlParameter("@ReturnValue",SqlDbType.Int)};

            parameters[0].Value = model.name;
            parameters[1].Value = model.provinces;
            parameters[2].Value = model.city;
            parameters[3].Value = model.area;
            parameters[4].Value = model.street;
            parameters[5].Value = model.address;
            parameters[6].Value = model.mobile;
            parameters[7].Value = model.addtime;
            parameters[8].Value = model.x_zb;
            parameters[9].Value = model.y_zb;
            parameters[10].Value = model.img;
            parameters[11].Value = model.busintime;
            parameters[12].Value = model.Linkman;
            parameters[13].Value = model.WeChat;
            parameters[14].Value = model.Other;
            parameters[15].Value = model.userId;
            parameters[16].Direction = ParameterDirection.Output;

            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            DbHelperSQL.ExecuteSqlTranWithIndentity(sqllist);
            return (int)parameters[16].Value;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.outlet GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,name,provinces,city,area,street,address,mobile,addtime,x_zb,y_zb,img,busintime,Linkman,WeChat,Other,userId from " + databaseprefix + "outlet ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            Model.outlet model = new Model.outlet();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["name"] != null && ds.Tables[0].Rows[0]["name"].ToString() != "")
                {
                    model.name = ds.Tables[0].Rows[0]["name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["provinces"] != null && ds.Tables[0].Rows[0]["provinces"].ToString() != "")
                {
                    model.provinces = ds.Tables[0].Rows[0]["provinces"].ToString();
                }
                if (ds.Tables[0].Rows[0]["city"] != null && ds.Tables[0].Rows[0]["city"].ToString() != "")
                {
                    model.city = ds.Tables[0].Rows[0]["city"].ToString();
                }
                if (ds.Tables[0].Rows[0]["area"] != null && ds.Tables[0].Rows[0]["area"].ToString() != "")
                {
                    model.area = ds.Tables[0].Rows[0]["area"].ToString();
                }
                if (ds.Tables[0].Rows[0]["street"] != null && ds.Tables[0].Rows[0]["street"].ToString() != "")
                {
                    model.street = ds.Tables[0].Rows[0]["street"].ToString();
                }
                if (ds.Tables[0].Rows[0]["address"] != null && ds.Tables[0].Rows[0]["address"].ToString() != "")
                {
                    model.address = ds.Tables[0].Rows[0]["address"].ToString();
                }
                if (ds.Tables[0].Rows[0]["mobile"] != null && ds.Tables[0].Rows[0]["mobile"].ToString() != "")
                {
                    model.mobile = ds.Tables[0].Rows[0]["mobile"].ToString();
                }
                if (ds.Tables[0].Rows[0]["addtime"] != null && ds.Tables[0].Rows[0]["addtime"].ToString() != "")
                {
                    model.addtime = DateTime.Parse(ds.Tables[0].Rows[0]["addtime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["x_zb"] != null && ds.Tables[0].Rows[0]["x_zb"].ToString() != "")
                {
                    model.x_zb = ds.Tables[0].Rows[0]["x_zb"].ToString();
                }
                if (ds.Tables[0].Rows[0]["y_zb"] != null && ds.Tables[0].Rows[0]["y_zb"].ToString() != "")
                {
                    model.y_zb = ds.Tables[0].Rows[0]["y_zb"].ToString();
                }
                if (ds.Tables[0].Rows[0]["img"] != null && ds.Tables[0].Rows[0]["img"].ToString() != "")
                {
                    model.img = ds.Tables[0].Rows[0]["img"].ToString();
                }
                if (ds.Tables[0].Rows[0]["busintime"] != null && ds.Tables[0].Rows[0]["busintime"].ToString() != "")
                {
                    model.busintime = ds.Tables[0].Rows[0]["busintime"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Linkman"] != null && ds.Tables[0].Rows[0]["Linkman"].ToString() != "")
                {
                    model.Linkman = ds.Tables[0].Rows[0]["Linkman"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WeChat"] != null && ds.Tables[0].Rows[0]["WeChat"].ToString() != "")
                {
                    model.WeChat = ds.Tables[0].Rows[0]["WeChat"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Other"] != null && ds.Tables[0].Rows[0]["Other"].ToString() != "")
                {
                    model.Other = ds.Tables[0].Rows[0]["Other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["userId"] != null && ds.Tables[0].Rows[0]["userId"].ToString() != "")
                {
                    model.Other = ds.Tables[0].Rows[0]["userId"].ToString();
                }
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM " + databaseprefix + "outlet");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(Vincent._DTcms.PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(Vincent._DTcms.PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
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
            strSql.Append(" id,name,provinces,city,area,street,address,mobile,addtime,x_zb,y_zb,img,busintime ");
            strSql.Append(" FROM " + databaseprefix + "outlet ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }
    }
}
