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
    public partial class withdraw
    {
        private string databaseprefix; //数据库表名前缀
        public withdraw(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "withdraw");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.withdraw model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "withdraw set ");
            strSql.Append("user_id=@user_id,");
            strSql.Append("card_no=@card_no,");
            strSql.Append("amount=@amount,");
            strSql.Append("banktype=@banktype,");
            strSql.Append("remark=@remark,");
            strSql.Append("status=@status,");
            strSql.Append("addtime=@addtime,");
            strSql.Append("reason=@reason,");
            strSql.Append("img_url=@img_url");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    
					new SqlParameter("@user_id", SqlDbType.Int,4),
					new SqlParameter("@card_no", SqlDbType.VarChar,100),
                    new SqlParameter("@amount", SqlDbType.Decimal,5),
                    new SqlParameter("@banktype", SqlDbType.Int,4),
					new SqlParameter("@remark", SqlDbType.NVarChar,4000),
                    new SqlParameter("@status", SqlDbType.TinyInt,1),
                    new SqlParameter("@addtime", SqlDbType.DateTime),
                    new SqlParameter("@reason", SqlDbType.NVarChar,500),
                    new SqlParameter("@img_url", SqlDbType.NVarChar,255),
                    new SqlParameter("@id", SqlDbType.Int,4)
                                        };
           
            parameters[0].Value = model.user_id;
            parameters[1].Value = model.card_no;
            parameters[2].Value = model.amount;
            parameters[3].Value = model.banktype;
            parameters[4].Value = model.remark;
            parameters[5].Value = model.status;
            parameters[6].Value = model.addtime;
            parameters[7].Value = model.reason;
            parameters[8].Value = model.img_url;
            parameters[9].Value = model.id;

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
        public int Add(Model.withdraw model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "withdraw(");
            strSql.Append("user_id,card_no,amount,banktype,remark,status,addtime,reason,img_url,openId,mobile)");
            strSql.Append(" values (");
            strSql.Append("@user_id,@card_no,@amount,@banktype,@remark,@status,@addtime,@reason,@img_url,@openId,@mobile)");
            strSql.Append(";set @ReturnValue= @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@user_id", SqlDbType.Int,4),
					new SqlParameter("@card_no", SqlDbType.VarChar,100),
                    new SqlParameter("@amount", SqlDbType.Decimal,5),
                    new SqlParameter("@banktype", SqlDbType.Int,4),
					new SqlParameter("@remark", SqlDbType.NVarChar,500),
                    new SqlParameter("@status", SqlDbType.TinyInt,1),
                    new SqlParameter("@addtime", SqlDbType.DateTime),
                    new SqlParameter("@reason", SqlDbType.NVarChar,500),
                    new SqlParameter("@img_url", SqlDbType.NVarChar,255),
                    new SqlParameter("@openId", SqlDbType.VarChar,100), 
                    new SqlParameter("@mobile", SqlDbType.VarChar,100),
                    new SqlParameter("@ReturnValue",SqlDbType.Int)
                    
                                        };
            parameters[0].Value = model.user_id;
            parameters[1].Value = model.card_no;
            parameters[2].Value = model.amount;
            parameters[3].Value = model.banktype;
            parameters[4].Value = model.remark;
            parameters[5].Value = model.status;
            parameters[6].Value = model.addtime;
            parameters[7].Value = model.reason;
            parameters[8].Value = model.img_url;
            parameters[9].Value = model.OpenId;
            parameters[10].Value = model.Mobile;
            parameters[11].Direction = ParameterDirection.Output;

            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            DbHelperSQL.ExecuteSqlTranWithIndentity(sqllist);
            return (int)parameters[11].Value;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.withdraw GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,user_id,card_no,amount,banktype,remark,status,addtime,reason,img_url,openid,mobile from " + databaseprefix + "withdraw ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            Model.withdraw model = new Model.withdraw();
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
                if (ds.Tables[0].Rows[0]["card_no"] != null && ds.Tables[0].Rows[0]["card_no"].ToString() != "")
                {
                    model.card_no = ds.Tables[0].Rows[0]["card_no"].ToString();
                }
                if (ds.Tables[0].Rows[0]["amount"] != null && ds.Tables[0].Rows[0]["amount"].ToString() != "")
                {
                    model.amount = decimal.Parse(ds.Tables[0].Rows[0]["amount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["banktype"] != null && ds.Tables[0].Rows[0]["banktype"].ToString() != "")
                {
                    model.banktype = int.Parse(ds.Tables[0].Rows[0]["banktype"].ToString());
                }
                if (ds.Tables[0].Rows[0]["remark"] != null && ds.Tables[0].Rows[0]["remark"].ToString() != "")
                {
                    model.remark = ds.Tables[0].Rows[0]["remark"].ToString();
                }
                if (ds.Tables[0].Rows[0]["status"] != null && ds.Tables[0].Rows[0]["status"].ToString() != "")
                {
                    model.status = int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["addtime"] != null && ds.Tables[0].Rows[0]["addtime"].ToString() != "")
                {
                    model.addtime = DateTime.Parse(ds.Tables[0].Rows[0]["addtime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["reason"] != null && ds.Tables[0].Rows[0]["reason"].ToString() != "")
                {
                    model.reason = ds.Tables[0].Rows[0]["reason"].ToString();
                }
                if (ds.Tables[0].Rows[0]["img_url"] != null && ds.Tables[0].Rows[0]["img_url"].ToString() != "")
                {
                    model.img_url = ds.Tables[0].Rows[0]["img_url"].ToString();
                }
                if (ds.Tables[0].Rows[0]["openid"] != null && ds.Tables[0].Rows[0]["openid"].ToString() != "")
                {
                    model.OpenId = ds.Tables[0].Rows[0]["openid"].ToString();
                }
                if (ds.Tables[0].Rows[0]["mobile"] != null && ds.Tables[0].Rows[0]["mobile"].ToString() != "")
                {
                    model.Mobile = ds.Tables[0].Rows[0]["mobile"].ToString();
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
            strSql.Append("select * FROM " + databaseprefix + "withdraw");
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
            strSql.Append(" id,user_id,card_no,amount,banktype,remark,status,addtime,reason,img_url ");
            strSql.Append(" FROM " + databaseprefix + "withdraw ");
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
        public DataSet GetLists(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" id,user_id,card_no,amount,banktype,status,addtime,reason ");
            strSql.Append(" FROM " + databaseprefix + "withdraw ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }


    }
}
