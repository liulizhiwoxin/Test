using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;
using BuysingooShop.DBUtility;
using Vincent;
using BuysingooShop.Model;

namespace BuysingooShop.DAL
{
    /// <summary>
    ///  数据访问类:user_groups
    /// </summary>
    public partial class user_groups
    {
        private string databaseprefix; //数据库表名前缀
        public user_groups(string _databaseprefix)
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
            strSql.Append("select count(1) from " + databaseprefix + "user_groups");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 返回标题名称
        /// </summary>
        public string GetTitle(int id)
        {
            if (id == 0)
            {
                return "推广员";
            }

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 title from " + databaseprefix + "user_groups");
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
        public int Add(Model.user_groups model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "user_groups(");
            strSql.Append("title,grade,upgrade_exp,amount,point,discount,is_default,is_upgrade,is_lock)");
            strSql.Append(" values (");
            strSql.Append("@title,@grade,@upgrade_exp,@amount,@point,@discount,@is_default,@is_upgrade,@is_lock)");
            strSql.Append(";select @ReturnValue=@@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@title", SqlDbType.NVarChar,100),
					new SqlParameter("@grade", SqlDbType.Int,4),
					new SqlParameter("@upgrade_exp", SqlDbType.Int,4),
					new SqlParameter("@amount", SqlDbType.Decimal,5),
					new SqlParameter("@point", SqlDbType.Int,4),
					new SqlParameter("@discount", SqlDbType.Int,4),
					new SqlParameter("@is_default", SqlDbType.TinyInt,1),
					new SqlParameter("@is_upgrade", SqlDbType.TinyInt,1),
					new SqlParameter("@is_lock", SqlDbType.TinyInt,1),
                    new SqlParameter("@ReturnValue",SqlDbType.Int) };
            parameters[0].Value = model.title;
            parameters[1].Value = model.grade;
            parameters[2].Value = model.upgrade_exp;
            parameters[3].Value = model.amount;
            parameters[4].Value = model.point;
            parameters[5].Value = model.discount;
            parameters[6].Value = model.is_default;
            parameters[7].Value = model.is_upgrade;
            parameters[8].Value = model.is_lock;
            parameters[9].Direction = ParameterDirection.Output;

            List<CommandInfo> sqllist=new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);


            #region ==========================充值活动================================
            StringBuilder strSql2;
            foreach (Model.activity modelt in model.Acticity)
            {
                strSql2 = new StringBuilder();
                strSql2.Append("insert into activity(");
                strSql2.Append("Group_Id,fields,value,remark)");
                strSql2.Append(" values (");
                strSql2.Append("@Group_Id,@fields,@value,@remark)");
                strSql2.Append(";select @@IDENTITY");
                SqlParameter[] parameters2 = {
					new SqlParameter("@Group_Id", SqlDbType.Int,4),
					new SqlParameter("@fields", SqlDbType.NVarChar,50),
					new SqlParameter("@value", SqlDbType.NVarChar,50),
					new SqlParameter("@remark", SqlDbType.NVarChar,100)};
                parameters2[0].Value = parameters[9].Value;
                parameters2[1].Value = modelt.fields;
                parameters2[2].Value = modelt.value;
                parameters2[3].Value = modelt.remark;
                cmd = new CommandInfo(strSql2.ToString(), parameters2);
                sqllist.Add(cmd);
            } 
            #endregion


            DbHelperSQL.ExecuteSqlTranWithIndentity(sqllist);
            return (int) parameters[9].Value;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.user_groups model)
        {
            using (SqlConnection conn=new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                using (SqlTransaction trans=conn.BeginTransaction())
                {
                    try
                    {
                        #region 修改主表信息
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("update " + databaseprefix + "user_groups set ");
                        strSql.Append("title=@title,");
                        strSql.Append("grade=@grade,");
                        strSql.Append("upgrade_exp=@upgrade_exp,");
                        strSql.Append("amount=@amount,");
                        strSql.Append("point=@point,");
                        strSql.Append("discount=@discount,");
                        strSql.Append("is_default=@is_default,");
                        strSql.Append("is_upgrade=@is_upgrade,");
                        strSql.Append("is_lock=@is_lock");
                        strSql.Append(" where id=@id");
                        SqlParameter[] parameters = {
					            new SqlParameter("@title", SqlDbType.NVarChar,100),
					            new SqlParameter("@grade", SqlDbType.Int,4),
					            new SqlParameter("@upgrade_exp", SqlDbType.Int,4),
					            new SqlParameter("@amount", SqlDbType.Decimal,5),
					            new SqlParameter("@point", SqlDbType.Int,4),
					            new SqlParameter("@discount", SqlDbType.Int,4),
					            new SqlParameter("@is_default", SqlDbType.TinyInt,1),
					            new SqlParameter("@is_upgrade", SqlDbType.TinyInt,1),
					            new SqlParameter("@is_lock", SqlDbType.TinyInt,1),
					            new SqlParameter("@id", SqlDbType.Int,4)};
                        parameters[0].Value = model.title;
                        parameters[1].Value = model.grade;
                        parameters[2].Value = model.upgrade_exp;
                        parameters[3].Value = model.amount;
                        parameters[4].Value = model.point;
                        parameters[5].Value = model.discount;
                        parameters[6].Value = model.is_default;
                        parameters[7].Value = model.is_upgrade;
                        parameters[8].Value = model.is_lock;
                        parameters[9].Value = model.id;
                        DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);

                        #endregion

                        #region 添加/修改修改优惠活动
                        if (model.Acticity != null && model.Acticity.Count > 0)
                        {
                            StringBuilder strSql2;
                            foreach (Model.activity modelt in model.Acticity)
                            {
                                strSql2 = new StringBuilder();
                                if (modelt.ID > 0)
                                {
                                    strSql2.Append("update activity set ");
                                    strSql2.Append("Group_Id=@Group_Id,");
                                    strSql2.Append("fields=@fields,");
                                    strSql2.Append("value=@value,");
                                    strSql2.Append("remark=@remark");
                                    strSql2.Append(" where ID=@ID");
                                    SqlParameter[] parameters2 = {
					                        new SqlParameter("@Group_Id", SqlDbType.Int,4),
					                        new SqlParameter("@fields", SqlDbType.NVarChar,50),
					                        new SqlParameter("@value", SqlDbType.NVarChar,50),
					                        new SqlParameter("@remark", SqlDbType.NVarChar,100),
					                        new SqlParameter("@ID", SqlDbType.Int,4)};
                                    parameters2[0].Value = modelt.ID;
                                    parameters2[1].Value = modelt.fields;
                                    parameters2[2].Value = modelt.value;
                                    parameters2[3].Value = modelt.remark;
                                    parameters2[4].Value = modelt.ID;
                                    DbHelperSQL.ExecuteSql(conn, trans, strSql2.ToString(), parameters2);
                                }
                                else
                                {
                                    strSql2 = new StringBuilder();
                                    strSql2.Append("insert into activity(");
                                    strSql2.Append("fields,value,remark)");
                                    strSql2.Append(" values (");
                                    strSql2.Append("@fields,@value,@remark)");
                                    SqlParameter[] parameters2 = {
					                        new SqlParameter("@fields", SqlDbType.NVarChar,50),
					                        new SqlParameter("@value", SqlDbType.NVarChar,50),
					                        new SqlParameter("@remark", SqlDbType.NVarChar,100)};

                                    parameters2[0].Value = modelt.fields;
                                    parameters2[1].Value = modelt.value;
                                    parameters2[2].Value = modelt.remark;
                                    DbHelperSQL.ExecuteSql(conn, trans, strSql2.ToString(), parameters2);
                                }
                            }
                        }
                        #endregion


                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        return false;
                    }

                    return true;
                }
            }

        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            //删除会员组价格
            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append("delete from " + databaseprefix + "user_group_price ");
            strSql1.Append(" where group_id=@group_id ");
            SqlParameter[] parameters1 = {
                    new SqlParameter("@group_id", SqlDbType.Int,4)};
            parameters1[0].Value = id;
            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo(strSql1.ToString(), parameters1);
            sqllist.Add(cmd);

            //删除主表
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "user_groups ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            cmd = new CommandInfo(strSql.ToString(), parameters);
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
        public Model.user_groups GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,title,grade,upgrade_exp,amount,point,discount,is_default,is_upgrade,is_lock from " + databaseprefix + "user_groups ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            
            BuysingooShop.Model.user_groups model = new BuysingooShop.Model.user_groups();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                #region 主表信息
                if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["title"] != null && ds.Tables[0].Rows[0]["title"].ToString() != "")
                {
                    model.title = ds.Tables[0].Rows[0]["title"].ToString();
                }
                if (ds.Tables[0].Rows[0]["grade"] != null && ds.Tables[0].Rows[0]["grade"].ToString() != "")
                {
                    model.grade = int.Parse(ds.Tables[0].Rows[0]["grade"].ToString());
                }
                if (ds.Tables[0].Rows[0]["upgrade_exp"] != null && ds.Tables[0].Rows[0]["upgrade_exp"].ToString() != "")
                {
                    model.upgrade_exp = int.Parse(ds.Tables[0].Rows[0]["upgrade_exp"].ToString());
                }
                if (ds.Tables[0].Rows[0]["amount"] != null && ds.Tables[0].Rows[0]["amount"].ToString() != "")
                {
                    model.amount = decimal.Parse(ds.Tables[0].Rows[0]["amount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["point"] != null && ds.Tables[0].Rows[0]["point"].ToString() != "")
                {
                    model.point = int.Parse(ds.Tables[0].Rows[0]["point"].ToString());
                }
                if (ds.Tables[0].Rows[0]["discount"] != null && ds.Tables[0].Rows[0]["discount"].ToString() != "")
                {
                    model.discount = int.Parse(ds.Tables[0].Rows[0]["discount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["is_default"] != null && ds.Tables[0].Rows[0]["is_default"].ToString() != "")
                {
                    model.is_default = int.Parse(ds.Tables[0].Rows[0]["is_default"].ToString());
                }
                if (ds.Tables[0].Rows[0]["is_upgrade"] != null && ds.Tables[0].Rows[0]["is_upgrade"].ToString() != "")
                {
                    model.is_upgrade = int.Parse(ds.Tables[0].Rows[0]["is_upgrade"].ToString());
                }
                if (ds.Tables[0].Rows[0]["is_lock"] != null && ds.Tables[0].Rows[0]["is_lock"].ToString() != "")
                {
                    model.is_lock = int.Parse(ds.Tables[0].Rows[0]["is_lock"].ToString());
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
        public Model.user_groups GetModel(int id,int articleid)
        {
            string strWhere = string.Format("article_id={0} AND t1.id={1}", articleid, id);
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT t1.id, title, grade, upgrade_exp, amount, point, discount, is_default, is_upgrade, is_lock,article_id,price FROM dbo.dt_user_groups t1  ");
            strSql.Append(" LEFT JOIN dbo.dt_user_group_price t2 ON t1.id = t2.group_id");
            strSql.Append(" where " + strWhere);
            BuysingooShop.Model.user_groups model = new BuysingooShop.Model.user_groups();
            DataSet ds = DbHelperSQL.Query(strSql.ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["title"] != null && ds.Tables[0].Rows[0]["title"].ToString() != "")
                {
                    model.title = ds.Tables[0].Rows[0]["title"].ToString();
                }
                if (ds.Tables[0].Rows[0]["grade"] != null && ds.Tables[0].Rows[0]["grade"].ToString() != "")
                {
                    model.grade = int.Parse(ds.Tables[0].Rows[0]["grade"].ToString());
                }
                if (ds.Tables[0].Rows[0]["upgrade_exp"] != null && ds.Tables[0].Rows[0]["upgrade_exp"].ToString() != "")
                {
                    model.upgrade_exp = int.Parse(ds.Tables[0].Rows[0]["upgrade_exp"].ToString());
                }
                if (ds.Tables[0].Rows[0]["amount"] != null && ds.Tables[0].Rows[0]["amount"].ToString() != "")
                {
                    model.amount = decimal.Parse(ds.Tables[0].Rows[0]["amount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["point"] != null && ds.Tables[0].Rows[0]["point"].ToString() != "")
                {
                    model.point = int.Parse(ds.Tables[0].Rows[0]["point"].ToString());
                }
                if (ds.Tables[0].Rows[0]["discount"] != null && ds.Tables[0].Rows[0]["discount"].ToString() != "")
                {
                    model.discount = int.Parse(ds.Tables[0].Rows[0]["discount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["is_default"] != null && ds.Tables[0].Rows[0]["is_default"].ToString() != "")
                {
                    model.is_default = int.Parse(ds.Tables[0].Rows[0]["is_default"].ToString());
                }
                if (ds.Tables[0].Rows[0]["is_upgrade"] != null && ds.Tables[0].Rows[0]["is_upgrade"].ToString() != "")
                {
                    model.is_upgrade = int.Parse(ds.Tables[0].Rows[0]["is_upgrade"].ToString());
                }
                if (ds.Tables[0].Rows[0]["is_lock"] != null && ds.Tables[0].Rows[0]["is_lock"].ToString() != "")
                {
                    model.is_lock = int.Parse(ds.Tables[0].Rows[0]["is_lock"].ToString());
                }
                if (ds.Tables[0].Rows[0]["article_id"] != null && ds.Tables[0].Rows[0]["article_id"].ToString() != "")
                {
                    model.article_id = int.Parse(ds.Tables[0].Rows[0]["article_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["price"] != null && ds.Tables[0].Rows[0]["price"].ToString() != "")
                {
                    model.price = decimal.Parse(ds.Tables[0].Rows[0]["price"].ToString());
                } 
            }

            return model;

        }

        /// <summary>
        /// 取得默认组别实体
        /// </summary>
        public Model.user_groups GetDefault()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 id from " + databaseprefix + "user_groups where is_lock=0 order by is_default desc,id asc");
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj != null)
            {
                return GetModel(Convert.ToInt32(obj));
            }
            return null;
        }

        /// <summary>
        /// 根据经验值返回升级的组别实体
        /// </summary>
        public Model.user_groups GetUpgrade(int group_id, int exp)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 id from " + databaseprefix + "user_groups");
            strSql.Append(" where is_lock=0 and is_upgrade=1 and grade>(select grade from " + databaseprefix + "user_groups where id=" + group_id + ") and upgrade_exp<=" + exp);
            strSql.Append(" order by grade asc");
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj != null)
            {
                return GetModel(Convert.ToInt32(obj));
            }
            return null;
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
            strSql.Append(" id,title,grade,upgrade_exp,amount,point,discount,is_default,is_upgrade,is_lock ");
            strSql.Append(" FROM " + databaseprefix + "user_groups ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }
        
        #endregion  Method
    }
}