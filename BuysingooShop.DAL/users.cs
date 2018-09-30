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
    /// 数据访问类:用户
    /// </summary>
    public partial class users
    {
        private string databaseprefix; //数据库表名前缀
        public users(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }

        #region 基本方法===========================================
        /// <summary>
        /// 查询最大的ID
        /// </summary>
        public int GetMaxId()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 id from " + databaseprefix + "users ORDER BY id DESC");
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }
        /// <summary>
        /// 返回数据数
        /// </summary>
        public int GetCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H from " + databaseprefix + "users ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }
        /// <summary>
        /// 返回累积佣金
        /// </summary>
        public int Getuser_amount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("  select  sum(amount_total)  as total  from dt_users");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }

          /// <summary>
        /// 返回佣金
        /// </summary>
        public int GetAlluser_amount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("  select  sum(amount_total-frozen_amount_total)  as total  from dt_users");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "users");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 检查用户名是否存在
        /// </summary>
        public bool Exists(string user_name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "users");
            strSql.Append(" where user_name=@user_name ");
            SqlParameter[] parameters = {
					new SqlParameter("@user_name", SqlDbType.NVarChar,100)};
            parameters[0].Value = user_name;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 检查同一IP注册间隔(小时)内是否存在
        /// </summary>
        public bool Exists(string reg_ip, int regctrl)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "users");
            strSql.Append(" where reg_ip=@reg_ip and DATEDIFF(hh,reg_time,getdate())<@regctrl ");
            SqlParameter[] parameters = {
					new SqlParameter("@reg_ip", SqlDbType.NVarChar,30),
                    new SqlParameter("@regctrl", SqlDbType.Int,4)};
            parameters[0].Value = reg_ip;
            parameters[1].Value = regctrl;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 检查Email是否存在
        /// </summary>
        public bool ExistsEmail(string email)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "users");
            strSql.Append(" where email=@email ");
            SqlParameter[] parameters = {
					new SqlParameter("@email", SqlDbType.NVarChar,100)};
            parameters[0].Value = email;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 检查手机号码是否存在
        /// </summary>
        public bool ExistsMobile(string mobile)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "users");
            strSql.Append(" where mobile=@mobile ");
            SqlParameter[] parameters = {
					new SqlParameter("@mobile", SqlDbType.NVarChar,20)};
            parameters[0].Value = mobile;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 根据用户名取得id
        /// </summary>
        public int Getid(string user_name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id from " + databaseprefix + "users");
            strSql.Append(" where user_name=@user_name ");
            SqlParameter[] parameters = {
					new SqlParameter("@user_name", SqlDbType.NVarChar,20)};
            parameters[0].Value = user_name;
            int id = Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString(), parameters));
            if (id==0)
            {
                return 0;
            }
            return id;
        }

        /// <summary>
        /// 根据用户名取得Salt
        /// </summary>
        public string GetSalt(string user_name)
        {
            //尝试用户名取得Salt
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 salt from " + databaseprefix + "users");
            strSql.Append(" where isDelete = 0 and user_name=@user_name");
            SqlParameter[] parameters = {
                    new SqlParameter("@user_name", SqlDbType.NVarChar,100)};
            parameters[0].Value = user_name;
            string salt = Convert.ToString(DbHelperSQL.GetSingle(strSql.ToString(), parameters));
            if (!string.IsNullOrEmpty(salt))
            {
                return salt;
            }
            //尝试用手机号取得Salt
            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append("select top 1 salt from " + databaseprefix + "users");
            strSql1.Append(" where isDelete = 0 and mobile=@mobile");
            SqlParameter[] parameters1 = {
                    new SqlParameter("@mobile", SqlDbType.NVarChar,20)};
            parameters1[0].Value = user_name;
            salt = Convert.ToString(DbHelperSQL.GetSingle(strSql1.ToString(), parameters1));
            if (!string.IsNullOrEmpty(salt))
            {
                return salt;
            }
            //尝试用邮箱取得Salt
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("select top 1 salt from " + databaseprefix + "users");
            strSql2.Append(" where email=@email");
            SqlParameter[] parameters2 = {
                    new SqlParameter("@email", SqlDbType.NVarChar,50)};
            parameters2[0].Value = user_name;
            salt = Convert.ToString(DbHelperSQL.GetSingle(strSql2.ToString(), parameters2));
            if (!string.IsNullOrEmpty(salt))
            {
                return salt;
            }
            return string.Empty;
        }

        /// <summary>
        /// 增加一条数据pc
        /// </summary>
        public int AddPc(Model.users model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "users(");
            strSql.Append("group_id,user_name,password,salt,email,nick_name,avatar,sex,birthday,telphone,mobile,qq,address,safe_question,safe_answer,amount,point,exp,status,reg_time,reg_ip,strcode,postCode,parentSalt,real_name,isMobile,frozen_amount,parentId)");
            strSql.Append(" values (");
            strSql.Append("@group_id,@user_name,@password,@salt,@email,@nick_name,@avatar,@sex,@birthday,@telphone,@mobile,@qq,@address,@safe_question,@safe_answer,@amount,@point,@exp,@status,@reg_time,@reg_ip,@strcode,@postCode,@parentSalt,@real_name,@isMobile,@frozen_amount,@parentId)");
            SqlParameter[] parameters = {
					new SqlParameter("@group_id", SqlDbType.Int,4),
					new SqlParameter("@user_name", SqlDbType.NVarChar,100),
					new SqlParameter("@password", SqlDbType.NVarChar,100),
					new SqlParameter("@salt", SqlDbType.NVarChar,20),
					new SqlParameter("@email", SqlDbType.NVarChar,50),
					new SqlParameter("@nick_name", SqlDbType.NVarChar,100),
					new SqlParameter("@avatar", SqlDbType.NVarChar,255),
					new SqlParameter("@sex", SqlDbType.NVarChar,20),
					new SqlParameter("@birthday", SqlDbType.DateTime),
					new SqlParameter("@telphone", SqlDbType.NVarChar,50),
					new SqlParameter("@mobile", SqlDbType.NVarChar,20),
					new SqlParameter("@qq", SqlDbType.NVarChar,30),
					new SqlParameter("@address", SqlDbType.NVarChar,255),
					new SqlParameter("@safe_question", SqlDbType.NVarChar,255),
					new SqlParameter("@safe_answer", SqlDbType.NVarChar,255),
					new SqlParameter("@amount", SqlDbType.Decimal,5),
					new SqlParameter("@point", SqlDbType.Int,4),
					new SqlParameter("@exp", SqlDbType.Int,4),
					new SqlParameter("@status", SqlDbType.TinyInt,1),
					new SqlParameter("@reg_time", SqlDbType.DateTime),
					new SqlParameter("@reg_ip", SqlDbType.NVarChar,30),
                    new SqlParameter("@strcode", SqlDbType.NVarChar,50),
                    new SqlParameter("@postCode",SqlDbType.NVarChar,50),
                    new SqlParameter("@parentSalt",SqlDbType.NVarChar,50),
                    new SqlParameter("@real_name",SqlDbType.NVarChar,50),
                    new SqlParameter("@isMobile",SqlDbType.Int,4),
                    new SqlParameter("@frozen_amount", SqlDbType.Decimal,5),
                     new SqlParameter("@parentId", SqlDbType.Int,4)
                                        };
            parameters[0].Value = model.group_id;
            parameters[1].Value = model.user_name;
            parameters[2].Value = model.password;
            parameters[3].Value = model.salt;
            parameters[4].Value = model.email;
            parameters[5].Value = model.nick_name;
            parameters[6].Value = model.avatar;
            parameters[7].Value = model.sex;
            parameters[8].Value = model.birthday;
            parameters[9].Value = model.telphone;
            parameters[10].Value = model.mobile;
            parameters[11].Value = model.qq;
            parameters[12].Value = model.address;
            parameters[13].Value = model.safe_question;
            parameters[14].Value = model.safe_answer;
            parameters[15].Value = model.amount;
            parameters[16].Value = model.point;
            parameters[17].Value = model.exp;
            parameters[18].Value = model.status;
            parameters[19].Value = model.reg_time;
            parameters[20].Value = model.reg_ip;
            parameters[21].Value = model.strcode;
            parameters[22].Value = model.postcode;
            parameters[23].Value = model.parentSalt;
            parameters[24].Value = model.real_name;
            parameters[25].Value = model.isMobile;
            parameters[26].Value = model.frozen_amount;
            parameters[27].Value = model.Parentid;
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
        /// 增加一条数据
        /// </summary>
        public int Add(Model.users model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "users(");
            strSql.Append("group_id,user_name,password,salt,email,nick_name,avatar,sex,birthday,telphone,mobile,qq,address,safe_question,safe_answer,amount,point,exp,status,reg_time,reg_ip,strcode,postCode,parentSalt,real_name,isMobile,frozen_amount,preId)");
            strSql.Append(" values (");
            strSql.Append("@group_id,@user_name,@password,@salt,@email,@nick_name,@avatar,@sex,@birthday,@telphone,@mobile,@qq,@address,@safe_question,@safe_answer,@amount,@point,@exp,@status,@reg_time,@reg_ip,@strcode,@postCode,@parentSalt,@real_name,@isMobile,@frozen_amount,@preId)");
            SqlParameter[] parameters = {
					new SqlParameter("@group_id", SqlDbType.Int,4),
					new SqlParameter("@user_name", SqlDbType.NVarChar,100),
					new SqlParameter("@password", SqlDbType.NVarChar,100),
					new SqlParameter("@salt", SqlDbType.NVarChar,20),
					new SqlParameter("@email", SqlDbType.NVarChar,50),
					new SqlParameter("@nick_name", SqlDbType.NVarChar,100),
					new SqlParameter("@avatar", SqlDbType.NVarChar,255),
					new SqlParameter("@sex", SqlDbType.NVarChar,20),
					new SqlParameter("@birthday", SqlDbType.DateTime),
					new SqlParameter("@telphone", SqlDbType.NVarChar,50),
					new SqlParameter("@mobile", SqlDbType.NVarChar,20),
					new SqlParameter("@qq", SqlDbType.NVarChar,30),
					new SqlParameter("@address", SqlDbType.NVarChar,255),
					new SqlParameter("@safe_question", SqlDbType.NVarChar,255),
					new SqlParameter("@safe_answer", SqlDbType.NVarChar,255),
					new SqlParameter("@amount", SqlDbType.Decimal,5),
					new SqlParameter("@point", SqlDbType.Int,4),
					new SqlParameter("@exp", SqlDbType.Int,4),
					new SqlParameter("@status", SqlDbType.TinyInt,1),
					new SqlParameter("@reg_time", SqlDbType.DateTime),
					new SqlParameter("@reg_ip", SqlDbType.NVarChar,30),
                    new SqlParameter("@strcode", SqlDbType.NVarChar,50),
                    new SqlParameter("@postCode",SqlDbType.NVarChar,50),
                    new SqlParameter("@parentSalt",SqlDbType.NVarChar,50),
                    new SqlParameter("@real_name",SqlDbType.NVarChar,50),
                    new SqlParameter("@isMobile",SqlDbType.Int,4),
                    new SqlParameter("@frozen_amount", SqlDbType.Decimal,5),
                    new SqlParameter("@preId",SqlDbType.Int,4)
                                        
                                        };
            parameters[0].Value = model.group_id;
            parameters[1].Value = model.user_name;
            parameters[2].Value = model.password;
            parameters[3].Value = model.salt;
            parameters[4].Value = model.email;
            parameters[5].Value = model.nick_name;
            parameters[6].Value = model.avatar;
            parameters[7].Value = model.sex;
            parameters[8].Value = model.birthday;
            parameters[9].Value = model.telphone;
            parameters[10].Value = model.mobile;
            parameters[11].Value = model.qq;
            parameters[12].Value = model.address;
            parameters[13].Value = model.safe_question;
            parameters[14].Value = model.safe_answer;
            parameters[15].Value = model.amount;
            parameters[16].Value = model.point;
            parameters[17].Value = model.exp;
            parameters[18].Value = model.status;
            parameters[19].Value = model.reg_time;
            parameters[20].Value = model.reg_ip;
            parameters[21].Value = model.strcode;
            parameters[22].Value = model.postcode;
            parameters[23].Value = model.parentSalt;
            parameters[24].Value = model.real_name;
            parameters[25].Value = model.isMobile;
            parameters[26].Value = model.frozen_amount;
            parameters[27].Value = model.PreId;

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
        /// 增加一条数据
        /// </summary>
        public int Add(Model.users model, int typeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "users(");
            strSql.Append("group_id,user_name,password,salt,email,nick_name,avatar,sex,birthday,telphone,mobile,qq,address,safe_question,safe_answer,amount,point,exp,status,reg_time,reg_ip,strcode,postCode,parentSalt,real_name,isMobile,frozen_amount,parentId,leftOrRight,marketId,organizeId,preId,provinces,city,Area)");
            strSql.Append(" values (");
            strSql.Append("@group_id,@user_name,@password,@salt,@email,@nick_name,@avatar,@sex,@birthday,@telphone,@mobile,@qq,@address,@safe_question,@safe_answer,@amount,@point,@exp,@status,@reg_time,@reg_ip,@strcode,@postCode,@parentSalt,@real_name,@isMobile,@frozen_amount,@parentId,@leftOrRight,@marketId,@organizeId,@preId,@provinces,@city,@Area)");
            SqlParameter[] parameters = {
					new SqlParameter("@group_id", SqlDbType.Int,4),
					new SqlParameter("@user_name", SqlDbType.NVarChar,100),
					new SqlParameter("@password", SqlDbType.NVarChar,100),
					new SqlParameter("@salt", SqlDbType.NVarChar,20),
					new SqlParameter("@email", SqlDbType.NVarChar,50),
					new SqlParameter("@nick_name", SqlDbType.NVarChar,100),
					new SqlParameter("@avatar", SqlDbType.NVarChar,255),
					new SqlParameter("@sex", SqlDbType.NVarChar,20),
					new SqlParameter("@birthday", SqlDbType.DateTime),
					new SqlParameter("@telphone", SqlDbType.NVarChar,50),
					new SqlParameter("@mobile", SqlDbType.NVarChar,20),
					new SqlParameter("@qq", SqlDbType.NVarChar,30),
					new SqlParameter("@address", SqlDbType.NVarChar,255),
					new SqlParameter("@safe_question", SqlDbType.NVarChar,255),
					new SqlParameter("@safe_answer", SqlDbType.NVarChar,255),
					new SqlParameter("@amount", SqlDbType.Decimal,5),
					new SqlParameter("@point", SqlDbType.Int,4),
					new SqlParameter("@exp", SqlDbType.Int,4),
					new SqlParameter("@status", SqlDbType.TinyInt,1),
					new SqlParameter("@reg_time", SqlDbType.DateTime),
					new SqlParameter("@reg_ip", SqlDbType.NVarChar,30),
                    new SqlParameter("@strcode", SqlDbType.NVarChar,50),
                    new SqlParameter("@postCode",SqlDbType.NVarChar,50),
                    new SqlParameter("@parentSalt",SqlDbType.NVarChar,50),
                    new SqlParameter("@real_name",SqlDbType.NVarChar,50),
                    new SqlParameter("@isMobile",SqlDbType.Int,4),
                    new SqlParameter("@frozen_amount", SqlDbType.Decimal,5),
                    new SqlParameter("@parentId",SqlDbType.Int,4),
                    new SqlParameter("@leftOrRight",SqlDbType.Int,4),
                    new SqlParameter("@marketId",SqlDbType.Int,4),
                    new SqlParameter("@organizeId",SqlDbType.Int,4),
                    new SqlParameter("@preId",SqlDbType.Int,4),
                    new SqlParameter("@provinces",SqlDbType.VarChar,100),
                    new SqlParameter("@city",SqlDbType.VarChar,10),
                      new SqlParameter("@Area",SqlDbType.VarChar,20),
               

                                        };
            parameters[0].Value = model.group_id;
            parameters[1].Value = model.user_name;
            parameters[2].Value = model.password;
            parameters[3].Value = model.salt;
            parameters[4].Value = model.email;
            parameters[5].Value = model.nick_name;
            parameters[6].Value = model.avatar;
            parameters[7].Value = model.sex;
            parameters[8].Value = model.birthday;
            parameters[9].Value = model.telphone;
            parameters[10].Value = model.mobile;
            parameters[11].Value = model.qq;
            parameters[12].Value = model.address;
            parameters[13].Value = model.safe_question;
            parameters[14].Value = model.safe_answer;
            parameters[15].Value = model.amount;
            parameters[16].Value = model.point;
            parameters[17].Value = model.exp;
            parameters[18].Value = model.status;
            parameters[19].Value = model.reg_time;
            parameters[20].Value = model.reg_ip;
            parameters[21].Value = model.strcode;
            parameters[22].Value = model.postcode;
            parameters[23].Value = model.parentSalt;
            parameters[24].Value = model.real_name;
            parameters[25].Value = model.isMobile;
            parameters[26].Value = model.frozen_amount;
            parameters[27].Value = model.Parentid;
            parameters[28].Value = model.Leftor_right;
            parameters[29].Value = model.MarketId;
            parameters[30].Value = model.OrganizeId;
            parameters[31].Value = model.PreId;
            parameters[32].Value = model.Provinces;
            parameters[33].Value = model.City;
            parameters[34].Value = model.Area;
        
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
        /// 修改一列数据
        /// </summary>
        public int UpdateField(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "users set " + strValue);
            strSql.Append(" where id=" + id);
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.users model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "users set ");
            strSql.Append("group_id=@group_id,");
            strSql.Append("user_name=@user_name,");
            strSql.Append("password=@password,");
            strSql.Append("salt=@salt,");
            strSql.Append("email=@email,");
            strSql.Append("nick_name=@nick_name,");
            strSql.Append("avatar=@avatar,");
            strSql.Append("sex=@sex,");
            strSql.Append("birthday=@birthday,");
            strSql.Append("telphone=@telphone,");
            strSql.Append("mobile=@mobile,");
            strSql.Append("qq=@qq,");
            strSql.Append("address=@address,");
            strSql.Append("safe_question=@safe_question,");
            strSql.Append("safe_answer=@safe_answer,");
            strSql.Append("amount=@amount,");
            strSql.Append("point=@point,");
            strSql.Append("exp=@exp,");
            strSql.Append("status=@status,");
            strSql.Append("reg_time=@reg_time,");
            strSql.Append("reg_ip=@reg_ip,");
            strSql.Append("strcode=@strcode,");
            strSql.Append("postCode=@postCode,");
            strSql.Append("parentSalt=@parentSalt,");
            strSql.Append("isEmail=@isEmail,");
            strSql.Append("isMobile=@isMobile,");
            strSql.Append("real_name=@real_name,");
            strSql.Append("frozen_amount=@frozen_amount,");
            strSql.Append("frozen_amount_total=@frozen_amount_total,");
            strSql.Append("organizeId=@organizeId,");           
            strSql.Append("preId=@preId,");
            strSql.Append("provinces=@provinces,");
            strSql.Append("city=@city,");
            strSql.Append("Area=@Area");
        
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@group_id", SqlDbType.Int,4),
					new SqlParameter("@user_name", SqlDbType.NVarChar,100),
					new SqlParameter("@password", SqlDbType.NVarChar,100),
					new SqlParameter("@salt", SqlDbType.NVarChar,20),
					new SqlParameter("@email", SqlDbType.NVarChar,50),
					new SqlParameter("@nick_name", SqlDbType.NVarChar,100),
					new SqlParameter("@avatar", SqlDbType.NVarChar,255),
					new SqlParameter("@sex", SqlDbType.NVarChar,20),
					new SqlParameter("@birthday", SqlDbType.DateTime),
					new SqlParameter("@telphone", SqlDbType.NVarChar,50),
					new SqlParameter("@mobile", SqlDbType.NVarChar,20),
					new SqlParameter("@qq", SqlDbType.NVarChar,30),
					new SqlParameter("@address", SqlDbType.NVarChar,255),
					new SqlParameter("@safe_question", SqlDbType.NVarChar,255),
					new SqlParameter("@safe_answer", SqlDbType.NVarChar,255),
					new SqlParameter("@amount", SqlDbType.Decimal,5),
					new SqlParameter("@point", SqlDbType.Int,4),
					new SqlParameter("@exp", SqlDbType.Int,4),
					new SqlParameter("@status", SqlDbType.TinyInt,1),
					new SqlParameter("@reg_time", SqlDbType.DateTime),
                    new SqlParameter("@strcode", SqlDbType.NVarChar,50),
					new SqlParameter("@reg_ip", SqlDbType.NVarChar,30),
                    new SqlParameter("@postCode",SqlDbType.NVarChar,50),
                    new SqlParameter("@parentSalt",SqlDbType.NVarChar,50),
                    new SqlParameter("@isEmail",SqlDbType.Int,4),
                    new SqlParameter("@isMobile",SqlDbType.Int,4),
                    new SqlParameter("@real_name",SqlDbType.NVarChar,50),
                    new SqlParameter("@frozen_amount", SqlDbType.Decimal,5),
                    new SqlParameter("@frozen_amount_total", SqlDbType.Decimal,5),
                    new SqlParameter("@organizeId",SqlDbType.Int,4),
                    new SqlParameter("@preId",SqlDbType.Int,4),
                    new SqlParameter("@provinces",SqlDbType.VarChar,10),
                    new SqlParameter("@city",SqlDbType.VarChar,100),
                    new SqlParameter("@Area",SqlDbType.VarChar,20),
                
                                        };
            parameters[0].Value = model.id;
            parameters[1].Value = model.group_id;
            parameters[2].Value = model.user_name;
            parameters[3].Value = model.password;
            parameters[4].Value = model.salt;
            parameters[5].Value = model.email;
            parameters[6].Value = model.nick_name;
            parameters[7].Value = model.avatar;
            parameters[8].Value = model.sex;
            parameters[9].Value = model.birthday;
            parameters[10].Value = model.telphone;
            parameters[11].Value = model.mobile;
            parameters[12].Value = model.qq;
            parameters[13].Value = model.address;
            parameters[14].Value = model.safe_question;
            parameters[15].Value = model.safe_answer;
            parameters[16].Value = model.amount;
            parameters[17].Value = model.point;
            parameters[18].Value = model.exp;
            parameters[19].Value = model.status;
            parameters[20].Value = model.reg_time;
            parameters[21].Value = model.strcode;
            parameters[22].Value = model.reg_ip;
            parameters[23].Value = model.postcode;
            parameters[24].Value = model.password;
            parameters[25].Value = model.isEmail;
            parameters[26].Value = model.isMobile;
            parameters[27].Value = model.real_name;
            parameters[28].Value = model.frozen_amount;
            parameters[29].Value = model.frozen_amount_total;
            parameters[30].Value = model.OrganizeId;
            parameters[31].Value = model.PreId;
            parameters[32].Value = model.Provinces;
            parameters[33].Value = model.City;
            parameters[34].Value = model.Area; 
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
        /// 更新一条数据
        /// </summary>
        public bool UpdateCallBack(Model.users model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "users set ");
            strSql.Append("group_id=@group_id,");
            strSql.Append("user_name=@user_name,");
            strSql.Append("password=@password,");
            strSql.Append("salt=@salt,");            
            strSql.Append("email=@email,");
            strSql.Append("nick_name=@nick_name,");
            strSql.Append("avatar=@avatar,");
            strSql.Append("sex=@sex,");
            strSql.Append("birthday=@birthday,");
            strSql.Append("telphone=@telphone,");
            strSql.Append("mobile=@mobile,");
            strSql.Append("qq=@qq,");
            strSql.Append("address=@address,");
            strSql.Append("safe_question=@safe_question,");
            strSql.Append("safe_answer=@safe_answer,");
            strSql.Append("amount=@amount,");
            strSql.Append("point=@point,");
            strSql.Append("exp=@exp,");
            strSql.Append("status=@status,");
            strSql.Append("reg_time=@reg_time,");
            strSql.Append("reg_ip=@reg_ip,");
            strSql.Append("strcode=@strcode,");
            strSql.Append("postCode=@postCode,");
            strSql.Append("parentSalt=@parentSalt,");
            strSql.Append("isEmail=@isEmail,");
            strSql.Append("isMobile=@isMobile,");
            strSql.Append("real_name=@real_name,");
            strSql.Append("frozen_amount=@frozen_amount,");
            strSql.Append("frozen_amount_total=@frozen_amount_total,");
            strSql.Append("organizeId=@organizeId,");
            strSql.Append("preId=@preId,");
            strSql.Append("provinces=@provinces,");
            strSql.Append("city=@city,");
            strSql.Append("Area=@Area,");
            strSql.Append("pay_time=@pay_time,");
            strSql.Append("isBuwei=@isBuwei");

            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4),
                    new SqlParameter("@group_id", SqlDbType.Int,4),
                    new SqlParameter("@user_name", SqlDbType.NVarChar,100),
                    new SqlParameter("@password", SqlDbType.NVarChar,100),
                    new SqlParameter("@salt", SqlDbType.NVarChar,20),
                    new SqlParameter("@email", SqlDbType.NVarChar,50),
                    new SqlParameter("@nick_name", SqlDbType.NVarChar,100),
                    new SqlParameter("@avatar", SqlDbType.NVarChar,255),
                    new SqlParameter("@sex", SqlDbType.NVarChar,20),
                    new SqlParameter("@birthday", SqlDbType.DateTime),
                    new SqlParameter("@telphone", SqlDbType.NVarChar,50),
                    new SqlParameter("@mobile", SqlDbType.NVarChar,20),
                    new SqlParameter("@qq", SqlDbType.NVarChar,30),
                    new SqlParameter("@address", SqlDbType.NVarChar,255),
                    new SqlParameter("@safe_question", SqlDbType.NVarChar,255),
                    new SqlParameter("@safe_answer", SqlDbType.NVarChar,255),
                    new SqlParameter("@amount", SqlDbType.Decimal,5),
                    new SqlParameter("@point", SqlDbType.Int,4),
                    new SqlParameter("@exp", SqlDbType.Int,4),
                    new SqlParameter("@status", SqlDbType.TinyInt,1),
                    new SqlParameter("@reg_time", SqlDbType.DateTime),
                    new SqlParameter("@strcode", SqlDbType.NVarChar,50),
                    new SqlParameter("@reg_ip", SqlDbType.NVarChar,30),
                    new SqlParameter("@postCode",SqlDbType.NVarChar,50),
                    new SqlParameter("@parentSalt",SqlDbType.NVarChar,50),
                    new SqlParameter("@isEmail",SqlDbType.Int,4),
                    new SqlParameter("@isMobile",SqlDbType.Int,4),
                    new SqlParameter("@real_name",SqlDbType.NVarChar,50),
                    new SqlParameter("@frozen_amount", SqlDbType.Decimal,5),
                    new SqlParameter("@frozen_amount_total", SqlDbType.Decimal,5),
                    new SqlParameter("@organizeId",SqlDbType.Int,4),
                    new SqlParameter("@preId",SqlDbType.Int,4),
                    new SqlParameter("@provinces",SqlDbType.VarChar,10),
                    new SqlParameter("@city",SqlDbType.VarChar,100),
                    new SqlParameter("@Area",SqlDbType.VarChar,20),
                    new SqlParameter("@pay_time",SqlDbType.DateTime),
                    new SqlParameter("@isBuwei",SqlDbType.Int,4)
                                        };
            parameters[0].Value = model.id;
            parameters[1].Value = model.group_id;
            parameters[2].Value = model.user_name;
            parameters[3].Value = model.password;
            parameters[4].Value = model.salt;
            parameters[5].Value = model.email;
            parameters[6].Value = model.nick_name;
            parameters[7].Value = model.avatar;
            parameters[8].Value = model.sex;
            parameters[9].Value = model.birthday;
            parameters[10].Value = model.telphone;
            parameters[11].Value = model.mobile;
            parameters[12].Value = model.qq;
            parameters[13].Value = model.address;
            parameters[14].Value = model.safe_question;
            parameters[15].Value = model.safe_answer;
            parameters[16].Value = model.amount;
            parameters[17].Value = model.point;
            parameters[18].Value = model.exp;
            parameters[19].Value = model.status;
            parameters[20].Value = model.reg_time;
            parameters[21].Value = model.strcode;
            parameters[22].Value = model.reg_ip;
            parameters[23].Value = model.postcode;
            parameters[24].Value = model.password;
            parameters[25].Value = model.isEmail;
            parameters[26].Value = model.isMobile;
            parameters[27].Value = model.real_name;
            parameters[28].Value = model.frozen_amount;
            parameters[29].Value = model.frozen_amount_total;
            parameters[30].Value = model.OrganizeId;
            parameters[31].Value = model.PreId;
            parameters[32].Value = model.Provinces;
            parameters[33].Value = model.City;
            parameters[34].Value = model.Area;
            parameters[35].Value = model.pay_time;
            parameters[36].Value = model.IsBuwei;
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
            //获取用户旧数据
            Model.users model = GetModel(id);
            if (model == null)
            {
                return false;
            }

            List<CommandInfo> sqllist = new List<CommandInfo>();
            //删除积分记录
            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append("delete from " + databaseprefix + "user_point_log ");
            strSql1.Append(" where user_id=@id");
            SqlParameter[] parameters1 = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters1[0].Value = id;
            CommandInfo cmd = new CommandInfo(strSql1.ToString(), parameters1);
            sqllist.Add(cmd);

            //删除金额记录
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("delete from " + databaseprefix + "user_amount_log ");
            strSql2.Append(" where user_id=@id");
            SqlParameter[] parameters2 = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters2[0].Value = id;
            cmd = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd);

            //删除短消息
            StringBuilder strSql3 = new StringBuilder();
            strSql3.Append("delete from " + databaseprefix + "user_message ");
            strSql3.Append(" where post_user_name=@post_user_name or accept_user_name=@accept_user_name");
            SqlParameter[] parameters3 = {
					new SqlParameter("@post_user_name", SqlDbType.NVarChar,100),
                    new SqlParameter("@accept_user_name", SqlDbType.NVarChar,100)};
            parameters3[0].Value = model.user_name;
            parameters3[1].Value = model.user_name;
            cmd = new CommandInfo(strSql3.ToString(), parameters3);
            sqllist.Add(cmd);

            //删除申请码
            StringBuilder strSql4 = new StringBuilder();
            strSql4.Append("delete from " + databaseprefix + "user_code ");
            strSql4.Append(" where user_id=@id");
            SqlParameter[] parameters4 = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters4[0].Value = id;
            cmd = new CommandInfo(strSql4.ToString(), parameters4);
            sqllist.Add(cmd);

            //删除登录日志
            StringBuilder strSql5 = new StringBuilder();
            strSql5.Append("delete from " + databaseprefix + "user_login_log ");
            strSql5.Append(" where user_id=@id");
            SqlParameter[] parameters5 = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters5[0].Value = id;
            cmd = new CommandInfo(strSql5.ToString(), parameters5);
            sqllist.Add(cmd);

            //删除用户记录
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "users ");
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
        public Model.users GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,group_id,user_name,password,salt,email,nick_name,avatar,sex,birthday,telphone,mobile,qq,address,safe_question,safe_answer,amount,point,exp,status,reg_time,pay_time,reg_ip,strcode,postCode,parentSalt,isEmail,isMobile,real_name,frozen_amount,parentId,leftOrRight,marketId,amount_total,frozen_amount_total,organizeId,preId,provinces,city,Area,point_total,isBuwei,convert(varchar(10),reg_time,120)as reg_time_str, isnull(convert(varchar(10),pay_time,120), '')as pay_time_str from " + databaseprefix + "users ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            Model.users model = new Model.users();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["group_id"].ToString() != "")
                {
                    model.group_id = int.Parse(ds.Tables[0].Rows[0]["group_id"].ToString());
                }

                if (ds.Tables[0].Rows[0]["preId"].ToString() != "")
                {
                    model.PreId = int.Parse(ds.Tables[0].Rows[0]["preId"].ToString());
                }

                model.user_name = ds.Tables[0].Rows[0]["user_name"].ToString();
                model.password = ds.Tables[0].Rows[0]["password"].ToString();
                model.salt = ds.Tables[0].Rows[0]["salt"].ToString();
                model.email = ds.Tables[0].Rows[0]["email"].ToString();
                model.nick_name = ds.Tables[0].Rows[0]["nick_name"].ToString();
                model.avatar = ds.Tables[0].Rows[0]["avatar"].ToString();
                model.sex = ds.Tables[0].Rows[0]["sex"].ToString();
                if (ds.Tables[0].Rows[0]["birthday"].ToString() != "")
                {
                    model.birthday = DateTime.Parse(ds.Tables[0].Rows[0]["birthday"].ToString());
                }
                model.telphone = ds.Tables[0].Rows[0]["telphone"].ToString();
                model.mobile = ds.Tables[0].Rows[0]["mobile"].ToString();
                model.qq = ds.Tables[0].Rows[0]["qq"].ToString();
                model.address = ds.Tables[0].Rows[0]["address"].ToString();
                model.safe_question = ds.Tables[0].Rows[0]["safe_question"].ToString();
                model.safe_answer = ds.Tables[0].Rows[0]["safe_answer"].ToString();
                model.isEmail = int.Parse(ds.Tables[0].Rows[0]["isEmail"].ToString());
                model.isMobile = int.Parse(ds.Tables[0].Rows[0]["isMobile"].ToString());
                if (ds.Tables[0].Rows[0]["amount"].ToString() != "")
                {
                    model.amount = decimal.Parse(ds.Tables[0].Rows[0]["amount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["point"].ToString() != "")
                {
                    model.point = int.Parse(ds.Tables[0].Rows[0]["point"].ToString());
                }
                if (ds.Tables[0].Rows[0]["exp"].ToString() != "")
                {
                    model.exp = int.Parse(ds.Tables[0].Rows[0]["exp"].ToString());
                }
                if (ds.Tables[0].Rows[0]["status"].ToString() != "")
                {
                    model.status = int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["reg_time"].ToString() != "")
                {
                    model.reg_time = DateTime.Parse(ds.Tables[0].Rows[0]["reg_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["pay_time"].ToString() != "")
                {
                    model.pay_time = DateTime.Parse(ds.Tables[0].Rows[0]["pay_time"].ToString());
                }
                model.reg_time_str = ds.Tables[0].Rows[0]["reg_time_str"].ToString();
                model.pay_time_str = ds.Tables[0].Rows[0]["pay_time_str"].ToString();

                model.reg_ip = ds.Tables[0].Rows[0]["reg_ip"].ToString();
                model.strcode = ds.Tables[0].Rows[0]["strcode"].ToString();
                model.postcode = ds.Tables[0].Rows[0]["postcode"].ToString();
                model.real_name = ds.Tables[0].Rows[0]["real_name"].ToString();
                model.parentSalt = ds.Tables[0].Rows[0]["parentSalt"].ToString();
                if (ds.Tables[0].Rows[0]["frozen_amount"].ToString() != "")
                {
                    model.frozen_amount = decimal.Parse(ds.Tables[0].Rows[0]["frozen_amount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["parentId"].ToString() != "")
                {
                    model.Parentid = int.Parse(ds.Tables[0].Rows[0]["parentId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["leftOrRight"].ToString() != "")
                {
                    model.Leftor_right = int.Parse(ds.Tables[0].Rows[0]["leftOrRight"].ToString());
                }
                if (ds.Tables[0].Rows[0]["marketId"].ToString() != "")
                {
                    model.MarketId = int.Parse(ds.Tables[0].Rows[0]["marketId"].ToString());
                }

                if (ds.Tables[0].Rows[0]["organizeId"].ToString() != "")
                {
                    model.OrganizeId = int.Parse(ds.Tables[0].Rows[0]["organizeId"].ToString());
                }

                if (ds.Tables[0].Rows[0]["amount_total"].ToString() != "")
                {
                    model.amount_total = decimal.Parse(ds.Tables[0].Rows[0]["amount_total"].ToString());
                }

                if (ds.Tables[0].Rows[0]["frozen_amount_total"].ToString() != "")
                {
                    model.frozen_amount_total = decimal.Parse(ds.Tables[0].Rows[0]["frozen_amount_total"].ToString());
                }
                
                model.Provinces = ds.Tables[0].Rows[0]["provinces"].ToString();
                model.City = ds.Tables[0].Rows[0]["city"].ToString();
                model.Area = ds.Tables[0].Rows[0]["Area"].ToString();
                if (ds.Tables[0].Rows[0]["point_total"].ToString() != "")
                {
                    model.point_total = int.Parse(ds.Tables[0].Rows[0]["point_total"].ToString());
                }

                if (ds.Tables[0].Rows[0]["isBuwei"].ToString() != "")
                {
                    model.IsBuwei = int.Parse(ds.Tables[0].Rows[0]["isBuwei"].ToString());
                }

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
        public Model.users GetModelByOpenId(string safe_question)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,group_id,user_name,password,salt,email,nick_name,avatar,sex,birthday,telphone,mobile,qq,address,safe_question,safe_answer,amount,point,exp,status,reg_time,pay_time,reg_ip,strcode,postCode,parentSalt,isEmail,isMobile,real_name,frozen_amount,parentId,leftOrRight,marketId,amount_total,frozen_amount_total,organizeId,preId,provinces,city,Area,point_total,isBuwei,convert(varchar(10),reg_time,120)as reg_time_str, isnull(convert(varchar(10),pay_time,120), '')as pay_time_str from " + databaseprefix + "users ");
            strSql.Append(" where safe_question=@safe_question");
            SqlParameter[] parameters = {
                    new SqlParameter("@safe_question", SqlDbType.VarChar,255)};
            parameters[0].Value = safe_question;

            Model.users model = new Model.users();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["group_id"].ToString() != "")
                {
                    model.group_id = int.Parse(ds.Tables[0].Rows[0]["group_id"].ToString());
                }

                if (ds.Tables[0].Rows[0]["preId"].ToString() != "")
                {
                    model.PreId = int.Parse(ds.Tables[0].Rows[0]["preId"].ToString());
                }

                model.user_name = ds.Tables[0].Rows[0]["user_name"].ToString();
                model.password = ds.Tables[0].Rows[0]["password"].ToString();
                model.salt = ds.Tables[0].Rows[0]["salt"].ToString();
                model.email = ds.Tables[0].Rows[0]["email"].ToString();
                model.nick_name = ds.Tables[0].Rows[0]["nick_name"].ToString();
                model.avatar = ds.Tables[0].Rows[0]["avatar"].ToString();
                model.sex = ds.Tables[0].Rows[0]["sex"].ToString();
                if (ds.Tables[0].Rows[0]["birthday"].ToString() != "")
                {
                    model.birthday = DateTime.Parse(ds.Tables[0].Rows[0]["birthday"].ToString());
                }
                model.telphone = ds.Tables[0].Rows[0]["telphone"].ToString();
                model.mobile = ds.Tables[0].Rows[0]["mobile"].ToString();
                model.qq = ds.Tables[0].Rows[0]["qq"].ToString();
                model.address = ds.Tables[0].Rows[0]["address"].ToString();
                model.safe_question = ds.Tables[0].Rows[0]["safe_question"].ToString();
                model.safe_answer = ds.Tables[0].Rows[0]["safe_answer"].ToString();
                model.isEmail = int.Parse(ds.Tables[0].Rows[0]["isEmail"].ToString());
                model.isMobile = int.Parse(ds.Tables[0].Rows[0]["isMobile"].ToString());
                if (ds.Tables[0].Rows[0]["amount"].ToString() != "")
                {
                    model.amount = decimal.Parse(ds.Tables[0].Rows[0]["amount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["point"].ToString() != "")
                {
                    model.point = int.Parse(ds.Tables[0].Rows[0]["point"].ToString());
                }
                if (ds.Tables[0].Rows[0]["exp"].ToString() != "")
                {
                    model.exp = int.Parse(ds.Tables[0].Rows[0]["exp"].ToString());
                }
                if (ds.Tables[0].Rows[0]["status"].ToString() != "")
                {
                    model.status = int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["reg_time"].ToString() != "")
                {
                    model.reg_time = DateTime.Parse(ds.Tables[0].Rows[0]["reg_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["pay_time"].ToString() != "")
                {
                    model.pay_time = DateTime.Parse(ds.Tables[0].Rows[0]["pay_time"].ToString());
                }
                model.reg_time_str = ds.Tables[0].Rows[0]["reg_time_str"].ToString();
                model.pay_time_str = ds.Tables[0].Rows[0]["pay_time_str"].ToString();

                model.reg_ip = ds.Tables[0].Rows[0]["reg_ip"].ToString();
                model.strcode = ds.Tables[0].Rows[0]["strcode"].ToString();
                model.postcode = ds.Tables[0].Rows[0]["postcode"].ToString();
                model.real_name = ds.Tables[0].Rows[0]["real_name"].ToString();
                model.parentSalt = ds.Tables[0].Rows[0]["parentSalt"].ToString();
                if (ds.Tables[0].Rows[0]["frozen_amount"].ToString() != "")
                {
                    model.frozen_amount = decimal.Parse(ds.Tables[0].Rows[0]["frozen_amount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["parentId"].ToString() != "")
                {
                    model.Parentid = int.Parse(ds.Tables[0].Rows[0]["parentId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["leftOrRight"].ToString() != "")
                {
                    model.Leftor_right = int.Parse(ds.Tables[0].Rows[0]["leftOrRight"].ToString());
                }
                if (ds.Tables[0].Rows[0]["marketId"].ToString() != "")
                {
                    model.MarketId = int.Parse(ds.Tables[0].Rows[0]["marketId"].ToString());
                }

                if (ds.Tables[0].Rows[0]["organizeId"].ToString() != "")
                {
                    model.OrganizeId = int.Parse(ds.Tables[0].Rows[0]["organizeId"].ToString());
                }

                if (ds.Tables[0].Rows[0]["amount_total"].ToString() != "")
                {
                    model.amount_total = decimal.Parse(ds.Tables[0].Rows[0]["amount_total"].ToString());
                }

                if (ds.Tables[0].Rows[0]["frozen_amount_total"].ToString() != "")
                {
                    model.frozen_amount_total = decimal.Parse(ds.Tables[0].Rows[0]["frozen_amount_total"].ToString());
                }

                model.Provinces = ds.Tables[0].Rows[0]["provinces"].ToString();
                model.City = ds.Tables[0].Rows[0]["city"].ToString();
                model.Area = ds.Tables[0].Rows[0]["Area"].ToString();
                if (ds.Tables[0].Rows[0]["point_total"].ToString() != "")
                {
                    model.point_total = int.Parse(ds.Tables[0].Rows[0]["point_total"].ToString());
                }

                if (ds.Tables[0].Rows[0]["isBuwei"].ToString() != "")
                {
                    model.IsBuwei = int.Parse(ds.Tables[0].Rows[0]["isBuwei"].ToString());
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
        public Model.users GetModel(string user_name, string password, int emaillogin, int mobilelogin)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 id from " + databaseprefix + "users");
            strSql.Append(" where (user_name=@user_name");
            if (emaillogin == 1)
            {
                strSql.Append(" or email=@user_name");
            }
            if (mobilelogin == 1)
            {
                strSql.Append(" or mobile=@user_name");
            }
            strSql.Append(") and password=@password and status<3 and isDelete=0");

            SqlParameter[] parameters = {
					    new SqlParameter("@user_name", SqlDbType.NVarChar,100),
                        new SqlParameter("@password", SqlDbType.NVarChar,100)};
            parameters[0].Value = user_name;
            parameters[1].Value = password;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj != null)
            {
                return GetModel(Convert.ToInt32(obj));
            }

            return null;
        }
        /// <summary>
        /// 根据用户名密码返回一个实体
        /// </summary>
        public Model.users GetModel(string user_name, string password)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 id from " + databaseprefix + "users");
            strSql.Append(" where (user_name=@user_name");
            
            strSql.Append(") and password=@password and status<3 and isDelete=0");

            SqlParameter[] parameters = {
					    new SqlParameter("@user_name", SqlDbType.NVarChar,100),
                        new SqlParameter("@password", SqlDbType.NVarChar,100)};
            parameters[0].Value = user_name;
            parameters[1].Value = password;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj != null)
            {
                return GetModel(Convert.ToInt32(obj));
            }

            return null;
        }
        /// <summary>
        /// 根据用户名返回一个实体
        /// </summary>
        public Model.users GetModel(string user_name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from " + databaseprefix + "users");
            strSql.Append(" where user_name=@user_name and isDelete=0 ");
            SqlParameter[] parameters = {
					new SqlParameter("@user_name", SqlDbType.NVarChar,100)};
            parameters[0].Value = user_name;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj != null)
            {
                return GetModel(Convert.ToInt32(obj));
            }
            return null;
        }

        /// <summary>
        /// 根据用户名返回一个实体
        /// </summary>
        public Model.users GetModelByName(string user_name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id from " + databaseprefix + "users");
            strSql.Append(" where user_name=@user_name and isDelete=0");
            SqlParameter[] parameters = {
					new SqlParameter("@user_name", SqlDbType.NVarChar,100)};
            parameters[0].Value = user_name;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj != null)
            {
                return GetModel(Convert.ToInt32(obj));
            }
            return null;
        }

        /// <summary>
        /// 根据邮箱返回一个实体
        /// </summary>
        public Model.users GetModels(string email)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id from " + databaseprefix + "users");
            strSql.Append(" where email=@email and status<3");
            SqlParameter[] parameters = {
					new SqlParameter("@email", SqlDbType.NVarChar,100)};
            parameters[0].Value = email;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj != null)
            {
                return GetModel(Convert.ToInt32(obj));
            }
            return null;
        }

        /// <summary>
        /// 根据手机号返回一个实体
        /// </summary>
        public Model.users GetModelMobile(string mobile)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id from " + databaseprefix + "users");
            strSql.Append(" where mobile=@mobile and status<3");
            SqlParameter[] parameters = {
					new SqlParameter("@mobile", SqlDbType.NVarChar,100)};
            parameters[0].Value = mobile;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj != null)
            {
                return GetModel(Convert.ToInt32(obj));
            }
            return null;
        }

        /// <summary>
        /// 根据手机号返回一个实体
        /// </summary>
        public Model.users GetModelMobile(string mobile, string name, string address)
        {
            //更新字段
            var sql = "update dt_users set real_name='" + name + "',address='" + address + "' where user_name='" + mobile + "'";
            DbHelperSQL.ExecuteSql(sql);


            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id from " + databaseprefix + "users");
            strSql.Append(" where mobile=@mobile and status<3");
            SqlParameter[] parameters = {
                    new SqlParameter("@mobile", SqlDbType.NVarChar,100)};
            parameters[0].Value = mobile;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj != null)
            {
                return GetModel(Convert.ToInt32(obj));
            }

            
            return null;
        }


        /// <summary>
        /// 根据手机号返回一个实体
        /// </summary>
        public Model.users GetModelMobile2(string mobile)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id from " + databaseprefix + "users");
            strSql.Append(" where mobile=@mobile and isDelete=0");
            SqlParameter[] parameters = {
					new SqlParameter("@mobile", SqlDbType.NVarChar,100)};
            parameters[0].Value = mobile;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj != null)
            {
                return GetModel(Convert.ToInt32(obj));
            }
            return null;
        }
        /// <summary>
        /// 获得团队销售总额
        /// </summary>
        public DataSet GetTeam_amount(int userId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select sum(order_amount) as team_total  from dt_orders where status in (2,90)  and user_id in (select  id  from  F_GetUserNetByShare(" + userId + ") where level in(1,2,3)) ");
            return DbHelperSQL.Query(strSql.ToString());
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
            strSql.Append(" id,group_id,user_name,password,salt,email,nick_name,avatar,sex,birthday,telphone,mobile,qq,address,safe_question,safe_answer,amount,point,exp,status,reg_time,reg_ip,strcode,postCode,parentSalt,real_name,frozen_amount,share_total,amount_total,frozen_amount_total,marketId,organizeId,point_total ");
            strSql.Append(" FROM " + databaseprefix + "users ");
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
            strSql.Append("select * FROM " + databaseprefix + "users");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(Vincent._DTcms.PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(Vincent._DTcms.PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetListByShare(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {             
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,group_id,user_name,nick_name,amount,reg_time FROM " + databaseprefix + "users");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(Vincent._DTcms.PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(Vincent._DTcms.PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }


        /// <summary>
        /// 我的团队总人数，总下线人数
        /// </summary>
        public DataSet GetUserInfo_Tuandui(int userId, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from F_GetUserNetByShare(" + userId + ")");
            
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(Vincent._DTcms.PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion


        #region 扩展方法

        /// <summary>
        /// 更新用户的微信logo图标地址
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="headimgurl"></param>
        /// <returns></returns>
        public int UpdateHeadImageUrl(int user_id, string headimgurl)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "users set avatar='" + headimgurl + "' where id = " + user_id);
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }


        /// <summary>
        /// 根据用户名id,获取上级信息
        /// </summary>
        public DataSet GetPreUserInfo(int user_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select * from dt_users where id = (select preId from dt_users where id = " + user_id + ")");
            
            return DbHelperSQL.Query(strSql.ToString());
        }

     

        #endregion
    }
}