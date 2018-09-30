using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;
using BuysingooShop.DBUtility;
using Vincent;

namespace BuysingooShop.DAL
{
    /// <summary>
    /// 数据访问类:退款
    /// </summary>
    public partial class refund
    {
        private string databaseprefix; //数据库表名前缀
        public refund(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }

        //#region 基本方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "refund");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string order_no)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "refund");
            strSql.Append(" where order_no=@order_no ");
            SqlParameter[] parameters = {
					new SqlParameter("@order_no", SqlDbType.NVarChar,100)};
            parameters[0].Value = order_no;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        /// <param name="id">退款ID</param>
        /// <param name="user_name">用户名</param>
        /// <returns></returns>
        public bool Exists(int id,string user_name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "refund");
            strSql.Append(" where id=@id and user_name=@user_name");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
                    new SqlParameter("@user_name", SqlDbType.NVarChar,100)};
            parameters[0].Value = id;
            parameters[1].Value = user_name;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 返回数据数
        /// </summary>
        public int GetCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H from " + databaseprefix + "refund ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }

        /// <summary>
        /// 增加一条数据,及其子表数据
        /// </summary>
        public int Add(Model.refund model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "refund(");
            strSql.Append("order_no,user_id,user_name,refund_fee,refund_money,apply_time,affirm_time,complete_time,express_no,refund_type,refund_reason,refund_status,un_refund_reason,express_money,refund_no,express_code,number)");
            strSql.Append(" values (");
            strSql.Append("@order_no,@user_id,@user_name,@refund_fee,@refund_money,@apply_time,@affirm_time,@complete_time,@express_no,@refund_type,@refund_reason,@refund_status,@un_refund_reason,@express_money,@refund_no,@express_code,@number)");
            strSql.Append(";set @ReturnValue= @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@order_no", SqlDbType.NVarChar,100),
					new SqlParameter("@user_id", SqlDbType.Int,10),
					new SqlParameter("@user_name", SqlDbType.NVarChar,100),
					new SqlParameter("@refund_fee", SqlDbType.Decimal,5),
					new SqlParameter("@refund_money", SqlDbType.Decimal,5),
					new SqlParameter("@apply_time", SqlDbType.DateTime),
					new SqlParameter("@affirm_time", SqlDbType.DateTime),
					new SqlParameter("@complete_time", SqlDbType.DateTime),
					new SqlParameter("@express_no", SqlDbType.NVarChar,20),
					new SqlParameter("@refund_type", SqlDbType.Int,1),
					new SqlParameter("@refund_reason", SqlDbType.NVarChar,500),
                    new SqlParameter("@refund_status", SqlDbType.Int,1),
                    new SqlParameter("@un_refund_reason", SqlDbType.NVarChar,500),
                    new SqlParameter("@express_money", SqlDbType.Decimal,5),
                    new SqlParameter("@refund_no", SqlDbType.NVarChar,100),
                    new SqlParameter("@express_code", SqlDbType.NVarChar,100),
                    new SqlParameter("@number", SqlDbType.NVarChar,100),
                    new SqlParameter("@ReturnValue",SqlDbType.Int)};
            parameters[0].Value = model.order_no;
            parameters[1].Value = model.user_id;
            parameters[2].Value = model.user_name;
            parameters[3].Value = model.refund_fee;
            parameters[4].Value = model.refund_money;
            parameters[5].Value = model.apply_time;
            parameters[6].Value = model.affirm_time;
            parameters[7].Value = model.complete_time;
            parameters[8].Value = model.express_no;
            parameters[9].Value = model.refund_type;
            parameters[10].Value = model.refund_reason;
            parameters[11].Value = model.refund_status;
            parameters[12].Value = model.un_refund_reason;
            parameters[13].Value = model.express_money;
            parameters[14].Value = model.refund_no;
            parameters[15].Value = model.express_code;
            parameters[16].Value = model.number;
            parameters[17].Direction = ParameterDirection.Output;
            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            DbHelperSQL.ExecuteSqlTranWithIndentity(sqllist);
            return (int)parameters[17].Value;
        }


        /// <summary>
        /// 修改一列数据
        /// </summary>
        public bool UpdateField(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "refund set " + strValue);
            strSql.Append(" where id=" + id);
            int rowsAffected = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public bool UpdateField(string order_no, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "refund set " + strValue);
            strSql.Append(" where order_no='" + order_no + "'");
            int rowsAffected = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.refund model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "refund set ");
            strSql.Append("order_no=@order_no,");
            strSql.Append("user_id=@user_id,");
            strSql.Append("user_name=@user_name,");
            strSql.Append("refund_fee=@refund_fee,");
            strSql.Append("refund_money=@refund_money,");
            strSql.Append("apply_time=@apply_time,");
            strSql.Append("affirm_time=@affirm_time,");
            strSql.Append("complete_time=@complete_time,");
            strSql.Append("express_no=@express_no,");
            strSql.Append("refund_type=@refund_type,");
            strSql.Append("refund_reason=@refund_reason,");
            strSql.Append("refund_status=@refund_status,");
            strSql.Append("un_refund_reason=@un_refund_reason,");
            strSql.Append("express_money=@express_money,");
            strSql.Append("refund_no=@refund_no,");
            strSql.Append("express_code=@express_code,");
            strSql.Append("number=@number");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@order_no", SqlDbType.NVarChar,100),
					new SqlParameter("@user_id", SqlDbType.Int,10),
					new SqlParameter("@user_name", SqlDbType.NVarChar,100),
					new SqlParameter("@refund_fee", SqlDbType.Decimal,5),
					new SqlParameter("@refund_money", SqlDbType.Decimal,5),
					new SqlParameter("@apply_time", SqlDbType.DateTime),
					new SqlParameter("@affirm_time", SqlDbType.DateTime),
					new SqlParameter("@complete_time", SqlDbType.DateTime),
					new SqlParameter("@express_no", SqlDbType.NVarChar,20),
					new SqlParameter("@refund_type", SqlDbType.Int,1),
					new SqlParameter("@refund_reason", SqlDbType.NVarChar,500),
                    new SqlParameter("@refund_status", SqlDbType.Int,1),
                    new SqlParameter("@un_refund_reason", SqlDbType.NVarChar,500),
                    new SqlParameter("@express_money", SqlDbType.Decimal,5),
                    new SqlParameter("@refund_no", SqlDbType.NVarChar,100),
                    new SqlParameter("@express_code", SqlDbType.NVarChar,100),
                    new SqlParameter("@number", SqlDbType.NVarChar,100),
                    new SqlParameter("@id", SqlDbType.Int,10)};
            parameters[0].Value = model.order_no;
            parameters[1].Value = model.user_id;
            parameters[2].Value = model.user_name;
            parameters[3].Value = model.refund_fee;
            parameters[4].Value = model.refund_money;
            parameters[5].Value = model.apply_time;
            parameters[6].Value = model.affirm_time;
            parameters[7].Value = model.complete_time;
            parameters[8].Value = model.express_no;
            parameters[9].Value = model.refund_type;
            parameters[10].Value = model.refund_reason;
            parameters[11].Value = model.refund_status;
            parameters[12].Value = model.un_refund_reason;
            parameters[13].Value = model.express_money;
            parameters[14].Value = model.refund_no;
            parameters[15].Value = model.express_code;
            parameters[16].Value = model.number;
            parameters[17].Value = model.id;

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
        /// 删除一条数据，及子表所有相关数据
        /// </summary>
        public bool Delete(int id)
        {
            List<CommandInfo> sqllist = new List<CommandInfo>();
            //StringBuilder strSql2 = new StringBuilder();
            //strSql2.Append("delete from " + databaseprefix + "order_goods ");
            //strSql2.Append(" where id=@id ");
            //SqlParameter[] parameters2 = {
            //        new SqlParameter("@id", SqlDbType.Int,10)};
            //parameters2[0].Value = id;

            //CommandInfo cmd = new CommandInfo(strSql2.ToString(), parameters2);
            //sqllist.Add(cmd);
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "refund ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,10)};
            parameters[0].Value = id;

            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
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

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.refund GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,order_no,user_id,user_name,refund_fee,refund_money,apply_time,affirm_time,complete_time,express_no,refund_type,refund_reason,refund_status,un_refund_reason,express_money,refund_no,express_code,number");
            strSql.Append(" from " + databaseprefix + "refund ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,10)};
            parameters[0].Value = id;

            Model.refund model = new Model.refund();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                #region 表信息
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                model.order_no = ds.Tables[0].Rows[0]["order_no"].ToString();
                if (ds.Tables[0].Rows[0]["user_id"].ToString() != "")
                {
                    model.user_id = int.Parse(ds.Tables[0].Rows[0]["user_id"].ToString());
                }
                model.user_name = ds.Tables[0].Rows[0]["user_name"].ToString();
                if (ds.Tables[0].Rows[0]["refund_fee"].ToString() != "")
                {
                    model.refund_fee = decimal.Parse(ds.Tables[0].Rows[0]["refund_fee"].ToString());
                }
                if (ds.Tables[0].Rows[0]["refund_money"].ToString() != "")
                {
                    model.refund_money = decimal.Parse(ds.Tables[0].Rows[0]["refund_money"].ToString());
                }
                if (ds.Tables[0].Rows[0]["apply_time"].ToString() != "")
                {
                    model.apply_time = DateTime.Parse(ds.Tables[0].Rows[0]["apply_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["affirm_time"].ToString() != "")
                {
                    model.affirm_time = DateTime.Parse(ds.Tables[0].Rows[0]["affirm_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["complete_time"].ToString() != "")
                {
                    model.complete_time = DateTime.Parse(ds.Tables[0].Rows[0]["complete_time"].ToString());
                }
                model.express_no = ds.Tables[0].Rows[0]["express_no"].ToString();
                if (ds.Tables[0].Rows[0]["refund_type"].ToString() != "")
                {
                    model.refund_type = int.Parse(ds.Tables[0].Rows[0]["refund_type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["refund_reason"].ToString() != "")
                {
                    model.refund_reason = ds.Tables[0].Rows[0]["refund_reason"].ToString();
                }
                if (ds.Tables[0].Rows[0]["refund_status"].ToString() != "")
                {
                    model.refund_status = int.Parse(ds.Tables[0].Rows[0]["refund_status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["un_refund_reason"].ToString() != "")
                {
                    model.un_refund_reason = ds.Tables[0].Rows[0]["un_refund_reason"].ToString();
                }
                if (ds.Tables[0].Rows[0]["express_money"].ToString() != "")
                {
                    model.express_money = decimal.Parse(ds.Tables[0].Rows[0]["express_money"].ToString());
                }
                if (ds.Tables[0].Rows[0]["refund_no"].ToString() != "")
                {
                    model.refund_no = ds.Tables[0].Rows[0]["refund_no"].ToString();
                }
                if (ds.Tables[0].Rows[0]["express_code"].ToString() != "")
                {
                    model.express_code = ds.Tables[0].Rows[0]["express_code"].ToString();
                }
                if (ds.Tables[0].Rows[0]["number"].ToString() != "")
                {
                    model.number = ds.Tables[0].Rows[0]["number"].ToString();
                }
                
                #endregion
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.refund GetorderModel(string order_no)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,order_no,user_id,user_name,refund_fee,refund_money,apply_time,affirm_time,complete_time,express_no,refund_type,refund_reason,refund_status,un_refund_reason,express_money,refund_no,express_code,number");
            strSql.Append(" from " + databaseprefix + "refund ");
            strSql.Append(" where order_no=@order_no");
            SqlParameter[] parameters = {
                    new SqlParameter("@order_no", SqlDbType.NVarChar,100)};
            parameters[0].Value = order_no;

            Model.refund model = new Model.refund();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                #region 表信息
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                model.order_no = ds.Tables[0].Rows[0]["order_no"].ToString();
                if (ds.Tables[0].Rows[0]["user_id"].ToString() != "")
                {
                    model.user_id = int.Parse(ds.Tables[0].Rows[0]["user_id"].ToString());
                }
                model.user_name = ds.Tables[0].Rows[0]["user_name"].ToString();
                if (ds.Tables[0].Rows[0]["refund_fee"].ToString() != "")
                {
                    model.refund_fee = decimal.Parse(ds.Tables[0].Rows[0]["refund_fee"].ToString());
                }
                if (ds.Tables[0].Rows[0]["refund_money"].ToString() != "")
                {
                    model.refund_money = decimal.Parse(ds.Tables[0].Rows[0]["refund_money"].ToString());
                }
                if (ds.Tables[0].Rows[0]["apply_time"].ToString() != "")
                {
                    model.apply_time = DateTime.Parse(ds.Tables[0].Rows[0]["apply_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["affirm_time"].ToString() != "")
                {
                    model.affirm_time = DateTime.Parse(ds.Tables[0].Rows[0]["affirm_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["complete_time"].ToString() != "")
                {
                    model.complete_time = DateTime.Parse(ds.Tables[0].Rows[0]["complete_time"].ToString());
                }
                model.express_no = ds.Tables[0].Rows[0]["express_no"].ToString();
                if (ds.Tables[0].Rows[0]["refund_type"].ToString() != "")
                {
                    model.refund_type = int.Parse(ds.Tables[0].Rows[0]["refund_type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["refund_reason"].ToString() != "")
                {
                    model.refund_reason = ds.Tables[0].Rows[0]["refund_reason"].ToString();
                }
                if (ds.Tables[0].Rows[0]["refund_status"].ToString() != "")
                {
                    model.refund_status = int.Parse(ds.Tables[0].Rows[0]["refund_status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["un_refund_reason"].ToString() != "")
                {
                    model.un_refund_reason = ds.Tables[0].Rows[0]["un_refund_reason"].ToString();
                }
                if (ds.Tables[0].Rows[0]["express_money"].ToString() != "")
                {
                    model.express_money = decimal.Parse(ds.Tables[0].Rows[0]["express_money"].ToString());
                }
                if (ds.Tables[0].Rows[0]["refund_no"].ToString() != "")
                {
                    model.refund_no = ds.Tables[0].Rows[0]["refund_no"].ToString();
                }
                if (ds.Tables[0].Rows[0]["express_code"].ToString() != "")
                {
                    model.express_code = ds.Tables[0].Rows[0]["express_code"].ToString();
                }
                if (ds.Tables[0].Rows[0]["number"].ToString() != "")
                {
                    model.number = ds.Tables[0].Rows[0]["number"].ToString();
                }
                #endregion
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
            strSql.Append(" id,order_no,user_id,user_name,refund_fee,refund_money,apply_time,affirm_time,complete_time,express_no,refund_type,refund_reason,refund_status,un_refund_reason,express_money,refund_no,number ");
            strSql.Append(" FROM " + databaseprefix + "refund ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetRefundList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" t1.id,t1.apply_time,t1.order_no order_no,t1.refund_status refund_status,t4.img_url img_url ");
            strSql.Append(" FROM dt_refund t1 INNER JOIN dbo.dt_orders t2 ON t2.order_no=t1.order_no INNER JOIN dbo.dt_order_goods t3 ON t2.id=t3.order_id INNER JOIN dbo.dt_article t4 ON t3.goods_id=t4.id");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }


        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetRefundList1(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" t1.id id,t1.apply_time,t1.order_no order_no,t1.refund_status refund_status,t3.quantity,t3.goods_pic ");
            strSql.Append(" FROM dt_refund t1 INNER JOIN dbo.dt_orders t2 ON t2.order_no=t1.order_no INNER JOIN dbo.dt_order_goods t3 ON t2.id=t3.order_id");
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
            strSql.Append("select * FROM " + databaseprefix + "refund");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(Vincent._DTcms.PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(Vincent._DTcms.PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }

       
    }
}