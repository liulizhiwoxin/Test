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
    /// 积分记录日志
    /// </summary>
    public partial class user_point_log
    {
        private string databaseprefix; //数据库表名前缀
        public user_point_log(string _databaseprefix)
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
            strSql.Append("select count(1) from " + databaseprefix + "user_point_log");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.user_point_log model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "user_point_log set ");
            strSql.Append("order_no=@order_no,");
            strSql.Append("user_id=@user_id,");
            strSql.Append("user_name=@user_name,");
            strSql.Append("remark=@remark,");
            strSql.Append("pointtype=@pointtype,");
            strSql.Append("status=@status,");
            strSql.Append("add_time=@add_time,");
            strSql.Append("value=@value,");
            strSql.Append("reason=@reason,");
            strSql.Append("order_status=@order_status,");
            strSql.Append("refund_no=@refund_no");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@order_no", SqlDbType.NVarChar,100),
					new SqlParameter("@user_id", SqlDbType.Int,4),
					new SqlParameter("@user_name", SqlDbType.NVarChar,100),
					new SqlParameter("@remark", SqlDbType.NVarChar,500),
					new SqlParameter("@pointtype", SqlDbType.Int,4),
					new SqlParameter("@status", SqlDbType.TinyInt,1),
					new SqlParameter("@add_time", SqlDbType.DateTime),
                    new SqlParameter("@value", SqlDbType.Int,4),
                    new SqlParameter("@reason", SqlDbType.NVarChar,500),
                    new SqlParameter("@order_status", SqlDbType.Int,4),
                    new SqlParameter("@refund_no", SqlDbType.NVarChar,100),
                    new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = model.order_no;
            parameters[1].Value = model.user_id;
            parameters[2].Value = model.user_name;
            parameters[3].Value = model.remark;
            parameters[4].Value = model.pointtype;
            parameters[5].Value = model.status;
            parameters[6].Value = model.add_time;
            parameters[7].Value = model.value;
            parameters[8].Value = model.reason;
            parameters[9].Value = model.order_status;
            parameters[10].Value = model.refund_no;
            parameters[11].Value = model.id;

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
        /// 增加一条数据
        /// </summary>
        public int Add(Model.user_point_log model, bool is_upgrade)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "user_point_log(");
            strSql.Append("user_id,user_name,value,remark,add_time,order_no,pointtype,status,amount,reason,order_status,refund_no)");
            strSql.Append(" values (");
            strSql.Append("@user_id,@user_name,@value,@remark,@add_time,@order_no,@pointtype,@status,@amount,@reason,@order_status,@refund_no)");
            strSql.Append(";set @ReturnValue= @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@user_id", SqlDbType.Int,4),
					new SqlParameter("@user_name", SqlDbType.NVarChar,100),
					new SqlParameter("@value", SqlDbType.Int,4),
					new SqlParameter("@remark", SqlDbType.NVarChar,500),
					new SqlParameter("@add_time", SqlDbType.DateTime),
                    new SqlParameter("@order_no", SqlDbType.NVarChar,100),
                    new SqlParameter("@pointtype",SqlDbType.Int,1),
                    new SqlParameter("@status",SqlDbType.Int,1),
                    new SqlParameter("@amount",SqlDbType.Decimal,5),
                    new SqlParameter("@reason", SqlDbType.NVarChar,500),
                    new SqlParameter("@order_status",SqlDbType.Int,1),
                    new SqlParameter("@refund_no", SqlDbType.NVarChar,100),
                    new SqlParameter("@ReturnValue",SqlDbType.Int)};
            parameters[0].Value = model.user_id;
            parameters[1].Value = model.user_name;
            parameters[2].Value = model.value;
            parameters[3].Value = model.remark;
            parameters[4].Value = model.add_time;
            parameters[5].Value = model.order_no;
            parameters[6].Value = model.pointtype;
            parameters[7].Value = model.status;
            parameters[8].Value = model.amount;
            parameters[9].Value = model.reason;
            parameters[10].Value = model.order_status;
            parameters[11].Value = model.refund_no;
            parameters[12].Direction = ParameterDirection.Output;

            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("update " + databaseprefix + "users set point=point+" + model.value);
            if (model.value > 0 && is_upgrade)
            {
                strSql2.Append(",exp=exp+" + model.value);
            }
            strSql2.Append(" where id=@id");
            SqlParameter[] parameters2 = {
                    new SqlParameter("@id", SqlDbType.Int,4)};
            parameters2[0].Value = model.user_id;
            cmd = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd);

            DbHelperSQL.ExecuteSqlTranWithIndentity(sqllist);
            return (int)parameters[12].Value;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "user_point_log ");
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
            strSql.Append("delete from " + databaseprefix + "user_point_log ");
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
        public Model.user_point_log GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,user_id,user_name,value,remark,add_time,order_no,pointtype,status,amount,reason,order_status,refund_no from " + databaseprefix + "user_point_log ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            Model.user_point_log model = new Model.user_point_log();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["user_id"] != null && ds.Tables[0].Rows[0]["user_id"].ToString() != "")
                {
                    model.user_id = int.Parse(ds.Tables[0].Rows[0]["user_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["user_name"] != null && ds.Tables[0].Rows[0]["user_name"].ToString() != "")
                {
                    model.user_name = ds.Tables[0].Rows[0]["user_name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["value"] != null && ds.Tables[0].Rows[0]["value"].ToString() != "")
                {
                    model.value = int.Parse(ds.Tables[0].Rows[0]["value"].ToString());
                }
                if (ds.Tables[0].Rows[0]["remark"] != null && ds.Tables[0].Rows[0]["remark"].ToString() != "")
                {
                    model.remark = ds.Tables[0].Rows[0]["remark"].ToString();
                }
                if (ds.Tables[0].Rows[0]["add_time"] != null && ds.Tables[0].Rows[0]["add_time"].ToString() != "")
                {
                    model.add_time = DateTime.Parse(ds.Tables[0].Rows[0]["add_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["order_no"] != null && ds.Tables[0].Rows[0]["order_no"].ToString() != "")
                {
                    model.order_no = ds.Tables[0].Rows[0]["order_no"].ToString();
                }
                if (ds.Tables[0].Rows[0]["pointtype"] != null && ds.Tables[0].Rows[0]["pointtype"].ToString() != "")
                {
                    model.pointtype = int.Parse(ds.Tables[0].Rows[0]["pointtype"].ToString());
                }
                if (ds.Tables[0].Rows[0]["status"] != null && ds.Tables[0].Rows[0]["status"].ToString() != "")
                {
                    model.status = int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["amount"] != null && ds.Tables[0].Rows[0]["amount"].ToString() != "")
                {
                    model.amount = decimal.Parse(ds.Tables[0].Rows[0]["amount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["reason"] != null && ds.Tables[0].Rows[0]["reason"].ToString() != "")
                {
                    model.reason = ds.Tables[0].Rows[0]["reason"].ToString();
                }
                if (ds.Tables[0].Rows[0]["order_status"] != null && ds.Tables[0].Rows[0]["order_status"].ToString() != "")
                {
                    model.order_status = int.Parse(ds.Tables[0].Rows[0]["order_status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["refund_no"] != null && ds.Tables[0].Rows[0]["refund_no"].ToString() != "")
                {
                    model.refund_no = ds.Tables[0].Rows[0]["refund_no"].ToString();
                }
                return model;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体(根据退货单号)
        /// </summary>
        public Model.user_point_log GetModel(string refund_no)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,user_id,user_name,value,remark,add_time,order_no,pointtype,status,amount,reason,order_status,refund_no from " + databaseprefix + "user_point_log ");
            strSql.Append(" where refund_no=@refund_no");
            SqlParameter[] parameters = {
					new SqlParameter("@refund_no", SqlDbType.NVarChar,100)};
            parameters[0].Value = refund_no;

            Model.user_point_log model = new Model.user_point_log();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["user_id"] != null && ds.Tables[0].Rows[0]["user_id"].ToString() != "")
                {
                    model.user_id = int.Parse(ds.Tables[0].Rows[0]["user_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["user_name"] != null && ds.Tables[0].Rows[0]["user_name"].ToString() != "")
                {
                    model.user_name = ds.Tables[0].Rows[0]["user_name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["value"] != null && ds.Tables[0].Rows[0]["value"].ToString() != "")
                {
                    model.value = int.Parse(ds.Tables[0].Rows[0]["value"].ToString());
                }
                if (ds.Tables[0].Rows[0]["remark"] != null && ds.Tables[0].Rows[0]["remark"].ToString() != "")
                {
                    model.remark = ds.Tables[0].Rows[0]["remark"].ToString();
                }
                if (ds.Tables[0].Rows[0]["add_time"] != null && ds.Tables[0].Rows[0]["add_time"].ToString() != "")
                {
                    model.add_time = DateTime.Parse(ds.Tables[0].Rows[0]["add_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["order_no"] != null && ds.Tables[0].Rows[0]["order_no"].ToString() != "")
                {
                    model.order_no = ds.Tables[0].Rows[0]["order_no"].ToString();
                }
                if (ds.Tables[0].Rows[0]["pointtype"] != null && ds.Tables[0].Rows[0]["pointtype"].ToString() != "")
                {
                    model.pointtype = int.Parse(ds.Tables[0].Rows[0]["pointtype"].ToString());
                }
                if (ds.Tables[0].Rows[0]["status"] != null && ds.Tables[0].Rows[0]["status"].ToString() != "")
                {
                    model.status = int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["amount"] != null && ds.Tables[0].Rows[0]["amount"].ToString() != "")
                {
                    model.amount = decimal.Parse(ds.Tables[0].Rows[0]["amount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["reason"] != null && ds.Tables[0].Rows[0]["reason"].ToString() != "")
                {
                    model.reason = ds.Tables[0].Rows[0]["reason"].ToString();
                }
                if (ds.Tables[0].Rows[0]["order_status"] != null && ds.Tables[0].Rows[0]["order_status"].ToString() != "")
                {
                    model.order_status = int.Parse(ds.Tables[0].Rows[0]["order_status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["refund_no"] != null && ds.Tables[0].Rows[0]["refund_no"].ToString() != "")
                {
                    model.refund_no = ds.Tables[0].Rows[0]["refund_no"].ToString();
                }
                return model;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体(通过充值单号)
        /// </summary>
        public Model.user_point_log GetModelrechargeno(string id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,user_id,user_name,value,remark,add_time,order_no,pointtype,status,amount from " + databaseprefix + "user_point_log ");
            strSql.Append(" where order_no=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.NVarChar,100)};
            parameters[0].Value = id;

            Model.user_point_log model = new Model.user_point_log();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["user_id"] != null && ds.Tables[0].Rows[0]["user_id"].ToString() != "")
                {
                    model.user_id = int.Parse(ds.Tables[0].Rows[0]["user_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["user_name"] != null && ds.Tables[0].Rows[0]["user_name"].ToString() != "")
                {
                    model.user_name = ds.Tables[0].Rows[0]["user_name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["value"] != null && ds.Tables[0].Rows[0]["value"].ToString() != "")
                {
                    model.value = int.Parse(ds.Tables[0].Rows[0]["value"].ToString());
                }
                if (ds.Tables[0].Rows[0]["remark"] != null && ds.Tables[0].Rows[0]["remark"].ToString() != "")
                {
                    model.remark = ds.Tables[0].Rows[0]["remark"].ToString();
                }
                if (ds.Tables[0].Rows[0]["add_time"] != null && ds.Tables[0].Rows[0]["add_time"].ToString() != "")
                {
                    model.add_time = DateTime.Parse(ds.Tables[0].Rows[0]["add_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["order_no"] != null && ds.Tables[0].Rows[0]["order_no"].ToString() != "")
                {
                    model.order_no = ds.Tables[0].Rows[0]["order_no"].ToString();
                }
                if (ds.Tables[0].Rows[0]["pointtype"] != null && ds.Tables[0].Rows[0]["pointtype"].ToString() != "")
                {
                    model.pointtype = int.Parse(ds.Tables[0].Rows[0]["pointtype"].ToString());
                }
                if (ds.Tables[0].Rows[0]["status"] != null && ds.Tables[0].Rows[0]["status"].ToString() != "")
                {
                    model.status = int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["amount"] != null && ds.Tables[0].Rows[0]["amount"].ToString() != "")
                {
                    model.amount = decimal.Parse(ds.Tables[0].Rows[0]["amount"].ToString());
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
            strSql.Append(" id,user_id,user_name,value,remark,add_time,order_no,status,amount,reason,order_status,refund_no,pointtype ");
            strSql.Append(" FROM " + databaseprefix + "user_point_log ");
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
            strSql.Append("select * FROM " + databaseprefix + "user_point_log");
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