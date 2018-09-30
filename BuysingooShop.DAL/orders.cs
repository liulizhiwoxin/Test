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
    /// 数据访问类:订单
    /// </summary>
    public partial class orders
    {
        private string databaseprefix; //数据库表名前缀
        public orders(string _databaseprefix)
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
            strSql.Append("select count(1) from " + databaseprefix + "orders");
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
            strSql.Append("select count(1) from " + databaseprefix + "orders");
            strSql.Append(" where order_no=@order_no ");
            SqlParameter[] parameters = {
					new SqlParameter("@order_no", SqlDbType.NVarChar,100)};
            parameters[0].Value = order_no;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        /// <param name="id">订单ID</param>
        /// <param name="user_name">用户名</param>
        /// <returns></returns>
        public bool Exists(int id, string user_name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "orders");
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
            strSql.Append("select count(*) as H from " + databaseprefix + "orders ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }

        /// <summary>
        /// 獲取月成交數量
        /// </summary>
        /// <returns></returns>
        public int GetMonthSellCount()
        {
            //SELECT * FROM dbo.dt_orders WHERE convert(char(7),add_time,120)= '2014-10'
            string nowTime = string.Format("{0:yyyy-MM}", DateTime.Now);
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM " + databaseprefix + "orders ");
            strSql.Append("WHERE convert(char(7),add_time,120)= '" + nowTime + "' ");
            DataTable dt = DbHelperSQL.Query(strSql.ToString()).Tables[0];
            return dt.Rows.Count;
        }
        /// <summary>
        /// 获取团队交易金额按月-年统计  
        /// </summary>
        /// <returns></returns>
        public DataSet GetTeamTotalByMonth(int userid)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(" select  year(add_time) as year,month(add_time) as  month ,sum(order_amount) as team_total from dt_orders where status in (2,90) and user_id in (select  id  from  F_GetUserNetByShare(" + userid + ") where level in(1,2,3))  group by year(add_time),month(add_time)");
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 获取团队交易金额按月-年统计详细信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetTeamTotalByMonthDetails(string month,string year,int userid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select nick_name,add_time,order_no,dt_orders.user_name as user_name,order_amount  from dt_users   left join  dt_orders   on dt_users.id=dt_orders.user_id where month(add_time)=" + month + " and year(add_time)=" + year + "  and user_id in (select  id  from  F_GetUserNetByShare(" + userid + ") where level in(1,2,3))  and dt_orders.status in (2,90) order by add_time desc");
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取累积交易金额（已完成订单）
        /// </summary>
        /// <returns></returns>
        public DataSet GetSellTotal()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT sum(order_amount)  as  order_amount  FROM " + databaseprefix + "orders   where status in (2,90)");
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 增加一条数据,及其子表数据
        /// </summary>
        public int Add(Model.orders model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "orders(");
            strSql.Append("order_no,trade_no,user_id,user_name,payment_id,payment_fee,payment_status,payment_time,express_id,express_no,express_fee,express_status,express_time,accept_name,post_code,telphone,mobile,area,address,message,remark,payable_amount,real_amount,order_amount,point,status,add_time,confirm_time,complete_time,str_code,brand_id,winery_time,is_bill,bill_type,down_order,invoice_rise,store_name,store_address,store_id,school_info)");
            strSql.Append(" values (");
            strSql.Append("@order_no,@trade_no,@user_id,@user_name,@payment_id,@payment_fee,@payment_status,@payment_time,@express_id,@express_no,@express_fee,@express_status,@express_time,@accept_name,@post_code,@telphone,@mobile,@area,@address,@message,@remark,@payable_amount,@real_amount,@order_amount,@point,@status,@add_time,@confirm_time,@complete_time,@str_code,@brand_id,@winery_time,@is_bill,@bill_type,@down_order,@invoice_rise,@store_name,@store_address,@store_id,@school_info)");
            strSql.Append(";set @ReturnValue= @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@order_no", SqlDbType.NVarChar,100),
					new SqlParameter("@trade_no", SqlDbType.NVarChar,100),
					new SqlParameter("@user_id", SqlDbType.Int,4),
					new SqlParameter("@user_name", SqlDbType.NVarChar,100),
					new SqlParameter("@payment_id", SqlDbType.Int,4),
					new SqlParameter("@payment_fee", SqlDbType.Decimal,5),
					new SqlParameter("@payment_status", SqlDbType.TinyInt,1),
					new SqlParameter("@payment_time", SqlDbType.DateTime),
					new SqlParameter("@express_id", SqlDbType.Int,4),
					new SqlParameter("@express_no", SqlDbType.NVarChar,100),
					new SqlParameter("@express_fee", SqlDbType.Decimal,5),
					new SqlParameter("@express_status", SqlDbType.TinyInt,1),
					new SqlParameter("@express_time", SqlDbType.DateTime),
					new SqlParameter("@accept_name", SqlDbType.NVarChar,50),
					new SqlParameter("@post_code", SqlDbType.NVarChar,20),
					new SqlParameter("@telphone", SqlDbType.NVarChar,30),
					new SqlParameter("@mobile", SqlDbType.NVarChar,20),
					new SqlParameter("@area", SqlDbType.NVarChar,100),
					new SqlParameter("@address", SqlDbType.NVarChar,500),
					new SqlParameter("@message", SqlDbType.NVarChar,500),
					new SqlParameter("@remark", SqlDbType.NVarChar,500),
					new SqlParameter("@payable_amount", SqlDbType.Decimal,5),
					new SqlParameter("@real_amount", SqlDbType.Decimal,5),
					new SqlParameter("@order_amount", SqlDbType.Decimal,5),
					new SqlParameter("@point", SqlDbType.Int,4),
					new SqlParameter("@status", SqlDbType.TinyInt,1),
					new SqlParameter("@add_time", SqlDbType.DateTime),
					new SqlParameter("@confirm_time", SqlDbType.DateTime),
					new SqlParameter("@complete_time", SqlDbType.DateTime),
                    new SqlParameter("@str_code", SqlDbType.NVarChar,50),
                    new SqlParameter("@brand_id", SqlDbType.Int,10),
                    new SqlParameter("@winery_time",SqlDbType.DateTime), 
                    new SqlParameter("@is_bill", SqlDbType.Int,10),
                    new SqlParameter("@bill_type", SqlDbType.Int,10),
                    new SqlParameter("@down_order", SqlDbType.NVarChar,50),
                    new SqlParameter("@invoice_rise", SqlDbType.NVarChar,50),
                    new SqlParameter("@store_name", SqlDbType.NVarChar,100),
                    new SqlParameter("@store_address", SqlDbType.NVarChar,100),
                    new SqlParameter("@store_id", SqlDbType.Int),
                    new SqlParameter("@school_info", SqlDbType.NVarChar,100),
                    new SqlParameter("@ReturnValue",SqlDbType.Int)};
            parameters[0].Value = model.order_no;
            parameters[1].Value = model.trade_no;
            parameters[2].Value = model.user_id;
            parameters[3].Value = model.user_name;
            parameters[4].Value = model.payment_id;
            parameters[5].Value = model.payment_fee;
            parameters[6].Value = model.payment_status;
            parameters[7].Value = model.payment_time;
            parameters[8].Value = model.express_id;
            parameters[9].Value = model.express_no;
            parameters[10].Value = model.express_fee;
            parameters[11].Value = model.express_status;
            parameters[12].Value = model.express_time;
            parameters[13].Value = model.accept_name;
            parameters[14].Value = model.post_code;
            parameters[15].Value = model.telphone;
            parameters[16].Value = model.mobile;
            parameters[17].Value = model.area;
            parameters[18].Value = model.address;
            parameters[19].Value = model.message;
            parameters[20].Value = model.remark;
            parameters[21].Value = model.payable_amount;
            parameters[22].Value = model.real_amount;
            parameters[23].Value = model.order_amount;
            parameters[24].Value = model.point;
            parameters[25].Value = model.status;
            parameters[26].Value = model.add_time;
            parameters[27].Value = model.confirm_time;
            parameters[28].Value = model.complete_time;
            parameters[29].Value = model.str_code;
            parameters[30].Value = model.brand_id;
            parameters[31].Value = model.winery_time;
            parameters[32].Value = model.is_bill;
            parameters[33].Value = model.bill_type;
            parameters[34].Value = model.down_order;
            parameters[35].Value = model.invoice_rise;
            parameters[36].Value = model.store_name;
            parameters[37].Value = model.store_address;
            parameters[38].Value = model.store_id;
            parameters[39].Value = model.school_info ;
            parameters[40].Direction = ParameterDirection.Output;
            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            //订单商品列表
            if (model.order_goods != null)
            {
                StringBuilder strSql2;
                foreach (Model.order_goods models in model.order_goods)
                {
                    strSql2 = new StringBuilder();
                    strSql2.Append("insert into " + databaseprefix + "order_goods(");
                    strSql2.Append("order_id,goods_id,goods_title,goods_price,real_price,quantity,strcode,point,goods_pic,type,total,weight)");
                    strSql2.Append(" values (");
                    strSql2.Append("@order_id,@goods_id,@goods_title,@goods_price,@real_price,@quantity,@strcode,@point,@goods_pic,@type,@total,@weight)");
                    SqlParameter[] parameters2 = {
						    new SqlParameter("@order_id", SqlDbType.Int,4),
						    new SqlParameter("@goods_id", SqlDbType.Int,4),
						    new SqlParameter("@goods_title", SqlDbType.NVarChar,100),
						    new SqlParameter("@goods_price", SqlDbType.Decimal,5),
						    new SqlParameter("@real_price", SqlDbType.Decimal,5),
						    new SqlParameter("@quantity", SqlDbType.Int,4),
                            new SqlParameter("@strcode", SqlDbType.VarChar,50),
						    new SqlParameter("@point", SqlDbType.Int,4), 
                            new SqlParameter("@goods_pic", SqlDbType.NVarChar,100), 
                            new SqlParameter("@type", SqlDbType.VarChar,50),
                            new SqlParameter("@total", SqlDbType.Decimal,5),
                            new SqlParameter("@weight", SqlDbType.Decimal,5) };
                    parameters2[0].Direction = ParameterDirection.InputOutput;
                    parameters2[1].Value = models.goods_id;
                    parameters2[2].Value = models.goods_title;
                    parameters2[3].Value = models.goods_price;
                    parameters2[4].Value = models.real_price;
                    parameters2[5].Value = models.quantity;
                    parameters2[6].Value = models.strcode;
                    parameters2[7].Value = models.point;
                    parameters2[8].Value = models.goods_pic;
                    parameters2[9].Value = models.type;
                    parameters2[10].Value = models.goods_price * models.quantity * models.weight;
                    parameters2[11].Value = models.weight;
                    cmd = new CommandInfo(strSql2.ToString(), parameters2);
                    sqllist.Add(cmd);
                }
            }
            DbHelperSQL.ExecuteSqlTranWithIndentity(sqllist);
            int id = (int)parameters[40].Value;
            return id;
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "orders set " + strValue);
            strSql.Append(" where id=" + id);
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }
        /// <summary>
        /// 修改一列数据
        /// </summary>
        public bool UpdateField_byid(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("update " + databaseprefix + "orders set " + strValue);
            strSql.Append(" where id='" + id + "'");

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
            strSql.Append("update " + databaseprefix + "orders set " + strValue);
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
        public bool Update(Model.orders model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "orders set ");
            strSql.Append("order_no=@order_no,");
            strSql.Append("trade_no=@trade_no,");
            strSql.Append("user_id=@user_id,");
            strSql.Append("user_name=@user_name,");
            strSql.Append("payment_id=@payment_id,");
            strSql.Append("payment_fee=@payment_fee,");
            strSql.Append("payment_status=@payment_status,");
            strSql.Append("payment_time=@payment_time,");
            strSql.Append("express_id=@express_id,");
            strSql.Append("express_no=@express_no,");
            strSql.Append("express_fee=@express_fee,");
            strSql.Append("express_status=@express_status,");
            strSql.Append("express_time=@express_time,");
            strSql.Append("accept_name=@accept_name,");
            strSql.Append("post_code=@post_code,");
            strSql.Append("telphone=@telphone,");
            strSql.Append("mobile=@mobile,");
            strSql.Append("area=@area,");
            strSql.Append("address=@address,");
            strSql.Append("message=@message,");
            strSql.Append("remark=@remark,");
            strSql.Append("payable_amount=@payable_amount,");
            strSql.Append("real_amount=@real_amount,");
            strSql.Append("order_amount=@order_amount,");
            strSql.Append("point=@point,");
            strSql.Append("status=@status,");
            strSql.Append("add_time=@add_time,");
            strSql.Append("confirm_time=@confirm_time,");
            strSql.Append("complete_time=@complete_time,");
            strSql.Append("str_code=@str_code,");
            strSql.Append("winery_time=@winery_time,");
            strSql.Append("is_bill=@is_bill,");
            strSql.Append("bill_type=@bill_type,");
            strSql.Append("store_name=@store_name,");
            strSql.Append("store_address=@store_address,");
            strSql.Append("store_id=@store_id");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@order_no", SqlDbType.NVarChar,100),
					new SqlParameter("@trade_no", SqlDbType.NVarChar,100),
					new SqlParameter("@user_id", SqlDbType.Int,4),
					new SqlParameter("@user_name", SqlDbType.NVarChar,100),
					new SqlParameter("@payment_id", SqlDbType.Int,4),
					new SqlParameter("@payment_fee", SqlDbType.Decimal,5),
					new SqlParameter("@payment_status", SqlDbType.TinyInt,1),
					new SqlParameter("@payment_time", SqlDbType.DateTime),
					new SqlParameter("@express_id", SqlDbType.Int,4),
					new SqlParameter("@express_no", SqlDbType.NVarChar,100),
					new SqlParameter("@express_fee", SqlDbType.Decimal,5),
					new SqlParameter("@express_status", SqlDbType.TinyInt,1),
					new SqlParameter("@express_time", SqlDbType.DateTime),
					new SqlParameter("@accept_name", SqlDbType.NVarChar,50),
					new SqlParameter("@post_code", SqlDbType.NVarChar,20),
					new SqlParameter("@telphone", SqlDbType.NVarChar,30),
					new SqlParameter("@mobile", SqlDbType.NVarChar,20),
					new SqlParameter("@area", SqlDbType.NVarChar,100),
					new SqlParameter("@address", SqlDbType.NVarChar,500),
					new SqlParameter("@message", SqlDbType.NVarChar,500),
					new SqlParameter("@remark", SqlDbType.NVarChar,500),
					new SqlParameter("@payable_amount", SqlDbType.Decimal,5),
					new SqlParameter("@real_amount", SqlDbType.Decimal,5),
					new SqlParameter("@order_amount", SqlDbType.Decimal,5),
					new SqlParameter("@point", SqlDbType.Int,4),
					new SqlParameter("@status", SqlDbType.TinyInt,1),
					new SqlParameter("@add_time", SqlDbType.DateTime),
					new SqlParameter("@confirm_time", SqlDbType.DateTime),
					new SqlParameter("@complete_time", SqlDbType.DateTime),
                    new SqlParameter("@str_code", SqlDbType.NVarChar,50),
                    new SqlParameter("@winery_time",SqlDbType.DateTime),
                    new SqlParameter("@is_bill", SqlDbType.Int,4),
                    new SqlParameter("@bill_type", SqlDbType.Int,4),
                    new SqlParameter("@store_name", SqlDbType.NVarChar,100),
                    new SqlParameter("@store_address", SqlDbType.NVarChar,100),
                    new SqlParameter("@store_id", SqlDbType.Int),
                    new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = model.order_no;
            parameters[1].Value = model.trade_no;
            parameters[2].Value = model.user_id;
            parameters[3].Value = model.user_name;
            parameters[4].Value = model.payment_id;
            parameters[5].Value = model.payment_fee;
            parameters[6].Value = model.payment_status;
            parameters[7].Value = model.payment_time;
            parameters[8].Value = model.express_id;
            parameters[9].Value = model.express_no;
            parameters[10].Value = model.express_fee;
            parameters[11].Value = model.express_status;
            parameters[12].Value = model.express_time;
            parameters[13].Value = model.accept_name;
            parameters[14].Value = model.post_code;
            parameters[15].Value = model.telphone;
            parameters[16].Value = model.mobile;
            parameters[17].Value = model.area;
            parameters[18].Value = model.address;
            parameters[19].Value = model.message;
            parameters[20].Value = model.remark;
            parameters[21].Value = model.payable_amount;
            parameters[22].Value = model.real_amount;
            parameters[23].Value = model.order_amount;
            parameters[24].Value = model.point;
            parameters[25].Value = model.status;
            parameters[26].Value = model.add_time;
            parameters[27].Value = model.confirm_time;
            parameters[28].Value = model.complete_time;
            parameters[29].Value = model.str_code;
            parameters[30].Value = model.winery_time;
            parameters[31].Value = model.is_bill;
            parameters[32].Value = model.bill_type;
            parameters[33].Value = model.store_name;
            parameters[34].Value = model.store_address;
            parameters[35].Value = model.store_id;
            parameters[36].Value = model.id;

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
        /// 删除一条数据，及子表所有相关数据
        /// </summary>
        public bool Delete(int id)
        {
            List<CommandInfo> sqllist = new List<CommandInfo>();
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("delete from " + databaseprefix + "order_goods ");
            strSql2.Append(" where order_id=@order_id ");
            SqlParameter[] parameters2 = {
					new SqlParameter("@order_id", SqlDbType.Int,4)};
            parameters2[0].Value = id;

            CommandInfo cmd = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd);
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "orders ");
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
        public Model.orders GetModelUser(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,order_no,trade_no,user_id,user_name,payment_id,payment_fee,payment_status,payment_time,express_id,express_no,express_fee,express_status,express_time,accept_name,post_code,telphone,mobile,area,address,message,remark,payable_amount,real_amount,order_amount,point,status,add_time,confirm_time,complete_time,str_code,brand_id,winery_time,is_bill,bill_type,store_name,store_address,store_id");
            strSql.Append(" from " + databaseprefix + "orders ");
            strSql.Append(" where user_id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            Model.orders model = new Model.orders();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                #region 父表信息
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                model.order_no = ds.Tables[0].Rows[0]["order_no"].ToString();
                model.trade_no = ds.Tables[0].Rows[0]["trade_no"].ToString();
                if (ds.Tables[0].Rows[0]["user_id"].ToString() != "")
                {
                    model.user_id = int.Parse(ds.Tables[0].Rows[0]["user_id"].ToString());
                }
                model.user_name = ds.Tables[0].Rows[0]["user_name"].ToString();
                if (ds.Tables[0].Rows[0]["payment_id"].ToString() != "")
                {
                    model.payment_id = int.Parse(ds.Tables[0].Rows[0]["payment_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["payment_fee"].ToString() != "")
                {
                    model.payment_fee = decimal.Parse(ds.Tables[0].Rows[0]["payment_fee"].ToString());
                }
                if (ds.Tables[0].Rows[0]["payment_status"].ToString() != "")
                {
                    model.payment_status = int.Parse(ds.Tables[0].Rows[0]["payment_status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["payment_time"].ToString() != "")
                {
                    model.payment_time = DateTime.Parse(ds.Tables[0].Rows[0]["payment_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["express_id"].ToString() != "")
                {
                    model.express_id = int.Parse(ds.Tables[0].Rows[0]["express_id"].ToString());
                }
                model.express_no = ds.Tables[0].Rows[0]["express_no"].ToString();
                if (ds.Tables[0].Rows[0]["express_fee"].ToString() != "")
                {
                    model.express_fee = decimal.Parse(ds.Tables[0].Rows[0]["express_fee"].ToString());
                }
                if (ds.Tables[0].Rows[0]["express_status"].ToString() != "")
                {
                    model.express_status = int.Parse(ds.Tables[0].Rows[0]["express_status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["express_time"].ToString() != "")
                {
                    model.express_time = DateTime.Parse(ds.Tables[0].Rows[0]["express_time"].ToString());
                }
                model.accept_name = ds.Tables[0].Rows[0]["accept_name"].ToString();
                model.post_code = ds.Tables[0].Rows[0]["post_code"].ToString();
                model.telphone = ds.Tables[0].Rows[0]["telphone"].ToString();
                model.mobile = ds.Tables[0].Rows[0]["mobile"].ToString();
                model.area = ds.Tables[0].Rows[0]["area"].ToString();
                model.address = ds.Tables[0].Rows[0]["address"].ToString();
                model.message = ds.Tables[0].Rows[0]["message"].ToString();
                model.remark = ds.Tables[0].Rows[0]["remark"].ToString();
                if (ds.Tables[0].Rows[0]["payable_amount"].ToString() != "")
                {
                    model.payable_amount = decimal.Parse(ds.Tables[0].Rows[0]["payable_amount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["real_amount"].ToString() != "")
                {
                    model.real_amount = decimal.Parse(ds.Tables[0].Rows[0]["real_amount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["order_amount"].ToString() != "")
                {
                    model.order_amount = decimal.Parse(ds.Tables[0].Rows[0]["order_amount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["point"].ToString() != "")
                {
                    model.point = int.Parse(ds.Tables[0].Rows[0]["point"].ToString());
                }
                if (ds.Tables[0].Rows[0]["status"].ToString() != "")
                {
                    model.status = int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["add_time"].ToString() != "")
                {
                    model.add_time = DateTime.Parse(ds.Tables[0].Rows[0]["add_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["add_time"].ToString() != "")
                {
                    model.timestring = ds.Tables[0].Rows[0]["add_time"].ToString();
                }
                if (ds.Tables[0].Rows[0]["confirm_time"].ToString() != "")
                {
                    model.confirm_time = DateTime.Parse(ds.Tables[0].Rows[0]["confirm_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["complete_time"].ToString() != "")
                {
                    model.complete_time = DateTime.Parse(ds.Tables[0].Rows[0]["complete_time"].ToString());
                }
                model.str_code = ds.Tables[0].Rows[0]["str_code"].ToString();
                if (ds.Tables[0].Rows[0]["brand_id"].ToString() != "")
                {
                    model.brand_id = int.Parse(ds.Tables[0].Rows[0]["brand_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["winery_time"].ToString() != "")
                {
                    model.winery_time = DateTime.Parse(ds.Tables[0].Rows[0]["winery_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["is_bill"].ToString() != "")
                {
                    model.is_bill = int.Parse(ds.Tables[0].Rows[0]["is_bill"].ToString());
                }
                if (ds.Tables[0].Rows[0]["bill_type"].ToString() != "")
                {
                    model.bill_type = int.Parse(ds.Tables[0].Rows[0]["bill_type"].ToString());
                }
                #endregion

                #region 子表信息
                StringBuilder strSql2 = new StringBuilder();
                strSql2.Append("select id,order_id,goods_id,goods_title,goods_price,real_price,quantity,point,goods_pic,strcode,type,total,weight from " + databaseprefix + "order_goods ");
                strSql2.Append(" where order_id=@id ");
                SqlParameter[] parameters2 = {
					    new SqlParameter("@id", SqlDbType.Int,4)};
                parameters2[0].Value = id;

                DataSet ds2 = DbHelperSQL.Query(strSql2.ToString(), parameters2);
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    int i = ds2.Tables[0].Rows.Count;
                    List<Model.order_goods> models = new List<Model.order_goods>();
                    Model.order_goods modelt;
                    for (int n = 0; n < i; n++)
                    {
                        modelt = new Model.order_goods();
                        if (ds2.Tables[0].Rows[n]["id"] != null && ds2.Tables[0].Rows[n]["id"].ToString() != "")
                        {
                            modelt.id = int.Parse(ds2.Tables[0].Rows[n]["id"].ToString());
                        }
                        if (ds2.Tables[0].Rows[n]["order_id"] != null && ds2.Tables[0].Rows[n]["order_id"].ToString() != "")
                        {
                            modelt.order_id = int.Parse(ds2.Tables[0].Rows[n]["order_id"].ToString());
                        }
                        if (ds2.Tables[0].Rows[n]["goods_id"] != null && ds2.Tables[0].Rows[n]["goods_id"].ToString() != "")
                        {
                            modelt.goods_id = int.Parse(ds2.Tables[0].Rows[n]["goods_id"].ToString());
                        }
                        if (ds2.Tables[0].Rows[n]["goods_title"] != null && ds2.Tables[0].Rows[n]["goods_title"].ToString() != "")
                        {
                            modelt.goods_title = ds2.Tables[0].Rows[n]["goods_title"].ToString();
                        }
                        if (ds2.Tables[0].Rows[n]["goods_price"] != null && ds2.Tables[0].Rows[n]["goods_price"].ToString() != "")
                        {
                            modelt.goods_price = decimal.Parse(ds2.Tables[0].Rows[n]["goods_price"].ToString());
                        }
                        if (ds2.Tables[0].Rows[n]["real_price"] != null && ds2.Tables[0].Rows[n]["real_price"].ToString() != "")
                        {
                            modelt.real_price = decimal.Parse(ds2.Tables[0].Rows[n]["real_price"].ToString());
                        }
                        if (ds2.Tables[0].Rows[n]["quantity"] != null && ds2.Tables[0].Rows[n]["quantity"].ToString() != "")
                        {
                            modelt.quantity = int.Parse(ds2.Tables[0].Rows[n]["quantity"].ToString());
                        }
                        if (ds2.Tables[0].Rows[n]["point"] != null && ds2.Tables[0].Rows[n]["point"].ToString() != "")
                        {
                            modelt.point = int.Parse(ds2.Tables[0].Rows[n]["point"].ToString());
                        }
                        if (ds2.Tables[0].Rows[n]["goods_pic"] != null && ds2.Tables[0].Rows[n]["goods_pic"].ToString() != "")
                        {
                            modelt.goods_pic = ds2.Tables[0].Rows[n]["goods_pic"].ToString();
                        }
                        if (ds2.Tables[0].Rows[n]["strcode"] != null && ds2.Tables[0].Rows[n]["strcode"].ToString() != "")
                        {
                            modelt.strcode = ds2.Tables[0].Rows[n]["strcode"].ToString();
                        }
                        if (ds2.Tables[0].Rows[n]["type"] != null && ds2.Tables[0].Rows[n]["type"].ToString() != "")
                        {
                            modelt.type = ds2.Tables[0].Rows[n]["type"].ToString();
                        }
                        if (ds2.Tables[0].Rows[n]["total"] != null && ds2.Tables[0].Rows[n]["total"].ToString() != "")
                        {
                            modelt.total = decimal.Parse(ds2.Tables[0].Rows[n]["total"].ToString());
                        }
                        if (ds2.Tables[0].Rows[n]["weight"] != null && ds2.Tables[0].Rows[n]["weight"].ToString() != "")
                        {
                            modelt.weight = decimal.Parse(ds2.Tables[0].Rows[n]["weight"].ToString());
                        }
                        models.Add(modelt);
                    }
                    model.order_goods = models;
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
        public Model.orders GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,order_no,trade_no,user_id,user_name,payment_id,payment_fee,payment_status,payment_time,express_id,express_no,express_fee,express_status,express_time,accept_name,post_code,telphone,mobile,area,address,message,remark,payable_amount,real_amount,order_amount,point,status,add_time,confirm_time,complete_time,str_code,brand_id,winery_time,is_bill,bill_type,store_name,store_address,store_id");
            strSql.Append(" from " + databaseprefix + "orders ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            Model.orders model = new Model.orders();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                #region 父表信息
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                model.order_no = ds.Tables[0].Rows[0]["order_no"].ToString();
                model.trade_no = ds.Tables[0].Rows[0]["trade_no"].ToString();
                if (ds.Tables[0].Rows[0]["user_id"].ToString() != "")
                {
                    model.user_id = int.Parse(ds.Tables[0].Rows[0]["user_id"].ToString());
                }
                model.user_name = ds.Tables[0].Rows[0]["user_name"].ToString();
                if (ds.Tables[0].Rows[0]["payment_id"].ToString() != "")
                {
                    model.payment_id = int.Parse(ds.Tables[0].Rows[0]["payment_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["payment_fee"].ToString() != "")
                {
                    model.payment_fee = decimal.Parse(ds.Tables[0].Rows[0]["payment_fee"].ToString());
                }
                if (ds.Tables[0].Rows[0]["payment_status"].ToString() != "")
                {
                    model.payment_status = int.Parse(ds.Tables[0].Rows[0]["payment_status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["payment_time"].ToString() != "")
                {
                    model.payment_time = DateTime.Parse(ds.Tables[0].Rows[0]["payment_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["express_id"].ToString() != "")
                {
                    model.express_id = int.Parse(ds.Tables[0].Rows[0]["express_id"].ToString());
                }
                model.express_no = ds.Tables[0].Rows[0]["express_no"].ToString();
                if (ds.Tables[0].Rows[0]["express_fee"].ToString() != "")
                {
                    model.express_fee = decimal.Parse(ds.Tables[0].Rows[0]["express_fee"].ToString());
                }
                if (ds.Tables[0].Rows[0]["express_status"].ToString() != "")
                {
                    model.express_status = int.Parse(ds.Tables[0].Rows[0]["express_status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["express_time"].ToString() != "")
                {
                    model.express_time = DateTime.Parse(ds.Tables[0].Rows[0]["express_time"].ToString());
                }
                model.accept_name = ds.Tables[0].Rows[0]["accept_name"].ToString();
                model.post_code = ds.Tables[0].Rows[0]["post_code"].ToString();
                model.telphone = ds.Tables[0].Rows[0]["telphone"].ToString();
                model.mobile = ds.Tables[0].Rows[0]["mobile"].ToString();
                model.area = ds.Tables[0].Rows[0]["area"].ToString();
                model.address = ds.Tables[0].Rows[0]["address"].ToString();
                model.message = ds.Tables[0].Rows[0]["message"].ToString();
                model.remark = ds.Tables[0].Rows[0]["remark"].ToString();
                if (ds.Tables[0].Rows[0]["payable_amount"].ToString() != "")
                {
                    model.payable_amount = decimal.Parse(ds.Tables[0].Rows[0]["payable_amount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["real_amount"].ToString() != "")
                {
                    model.real_amount = decimal.Parse(ds.Tables[0].Rows[0]["real_amount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["order_amount"].ToString() != "")
                {
                    model.order_amount = decimal.Parse(ds.Tables[0].Rows[0]["order_amount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["point"].ToString() != "")
                {
                    model.point = int.Parse(ds.Tables[0].Rows[0]["point"].ToString());
                }
                if (ds.Tables[0].Rows[0]["status"].ToString() != "")
                {
                    model.status = int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["add_time"].ToString() != "")
                {
                    model.add_time = DateTime.Parse(ds.Tables[0].Rows[0]["add_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["add_time"].ToString() != "")
                {
                    model.timestring = ds.Tables[0].Rows[0]["add_time"].ToString();
                }
                if (ds.Tables[0].Rows[0]["confirm_time"].ToString() != "")
                {
                    model.confirm_time = DateTime.Parse(ds.Tables[0].Rows[0]["confirm_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["complete_time"].ToString() != "")
                {
                    model.complete_time = DateTime.Parse(ds.Tables[0].Rows[0]["complete_time"].ToString());
                }
                model.str_code = ds.Tables[0].Rows[0]["str_code"].ToString();
                if (ds.Tables[0].Rows[0]["brand_id"].ToString() != "")
                {
                    model.brand_id = int.Parse(ds.Tables[0].Rows[0]["brand_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["winery_time"].ToString() != "")
                {
                    model.winery_time = DateTime.Parse(ds.Tables[0].Rows[0]["winery_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["is_bill"].ToString() != "")
                {
                    model.is_bill = int.Parse(ds.Tables[0].Rows[0]["is_bill"].ToString());
                }
                if (ds.Tables[0].Rows[0]["bill_type"].ToString() != "")
                {
                    model.bill_type = int.Parse(ds.Tables[0].Rows[0]["bill_type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["store_name"].ToString() != "")
                {
                    model.store_name = ds.Tables[0].Rows[0]["store_name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["store_address"].ToString() != "")
                {
                    model.store_address = ds.Tables[0].Rows[0]["store_address"].ToString();
                }
                if (ds.Tables[0].Rows[0]["store_id"].ToString() != "")
                {
                    model.store_id = int.Parse(ds.Tables[0].Rows[0]["store_id"].ToString());
                }
                #endregion

                #region 子表信息
                StringBuilder strSql2 = new StringBuilder();
                strSql2.Append("select id,order_id,goods_id,goods_title,goods_price,real_price,quantity,point,goods_pic,strcode,type,total,weight from " + databaseprefix + "order_goods ");
                strSql2.Append(" where order_id=@id ");
                SqlParameter[] parameters2 = {
					    new SqlParameter("@id", SqlDbType.Int,4)};
                parameters2[0].Value = id;

                DataSet ds2 = DbHelperSQL.Query(strSql2.ToString(), parameters2);
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    int i = ds2.Tables[0].Rows.Count;
                    List<Model.order_goods> models = new List<Model.order_goods>();
                    Model.order_goods modelt;
                    for (int n = 0; n < i; n++)
                    {
                        modelt = new Model.order_goods();
                        if (ds2.Tables[0].Rows[n]["id"] != null && ds2.Tables[0].Rows[n]["id"].ToString() != "")
                        {
                            modelt.id = int.Parse(ds2.Tables[0].Rows[n]["id"].ToString());
                        }
                        if (ds2.Tables[0].Rows[n]["order_id"] != null && ds2.Tables[0].Rows[n]["order_id"].ToString() != "")
                        {
                            modelt.order_id = int.Parse(ds2.Tables[0].Rows[n]["order_id"].ToString());
                        }
                        if (ds2.Tables[0].Rows[n]["goods_id"] != null && ds2.Tables[0].Rows[n]["goods_id"].ToString() != "")
                        {
                            modelt.goods_id = int.Parse(ds2.Tables[0].Rows[n]["goods_id"].ToString());
                        }
                        if (ds2.Tables[0].Rows[n]["goods_title"] != null && ds2.Tables[0].Rows[n]["goods_title"].ToString() != "")
                        {
                            modelt.goods_title = ds2.Tables[0].Rows[n]["goods_title"].ToString();
                        }
                        if (ds2.Tables[0].Rows[n]["goods_price"] != null && ds2.Tables[0].Rows[n]["goods_price"].ToString() != "")
                        {
                            modelt.goods_price = decimal.Parse(ds2.Tables[0].Rows[n]["goods_price"].ToString());
                        }
                        if (ds2.Tables[0].Rows[n]["real_price"] != null && ds2.Tables[0].Rows[n]["real_price"].ToString() != "")
                        {
                            modelt.real_price = decimal.Parse(ds2.Tables[0].Rows[n]["real_price"].ToString());
                        }
                        if (ds2.Tables[0].Rows[n]["quantity"] != null && ds2.Tables[0].Rows[n]["quantity"].ToString() != "")
                        {
                            modelt.quantity = int.Parse(ds2.Tables[0].Rows[n]["quantity"].ToString());
                        }
                        if (ds2.Tables[0].Rows[n]["point"] != null && ds2.Tables[0].Rows[n]["point"].ToString() != "")
                        {
                            modelt.point = int.Parse(ds2.Tables[0].Rows[n]["point"].ToString());
                        }
                        if (ds2.Tables[0].Rows[n]["goods_pic"] != null && ds2.Tables[0].Rows[n]["goods_pic"].ToString() != "")
                        {
                            modelt.goods_pic = ds2.Tables[0].Rows[n]["goods_pic"].ToString();
                        }
                        if (ds2.Tables[0].Rows[n]["strcode"] != null && ds2.Tables[0].Rows[n]["strcode"].ToString() != "")
                        {
                            modelt.strcode = ds2.Tables[0].Rows[n]["strcode"].ToString();
                        }
                        if (ds2.Tables[0].Rows[n]["type"] != null && ds2.Tables[0].Rows[n]["type"].ToString() != "")
                        {
                            modelt.type = ds2.Tables[0].Rows[n]["type"].ToString();
                        }
                        if (ds2.Tables[0].Rows[n]["total"] != null && ds2.Tables[0].Rows[n]["total"].ToString() != "")
                        {
                            modelt.total = decimal.Parse(ds2.Tables[0].Rows[n]["total"].ToString());
                        }
                        if (ds2.Tables[0].Rows[n]["weight"] != null && ds2.Tables[0].Rows[n]["weight"].ToString() != "")
                        {
                            modelt.weight = decimal.Parse(ds2.Tables[0].Rows[n]["weight"].ToString());
                        }
                        models.Add(modelt);
                    }
                    model.order_goods = models;
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
        /// 得到一个对象实体(根据用户id)
        /// </summary>
        public Model.orders GetModelUserId(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,order_no,trade_no,user_id,user_name,payment_id,payment_fee,payment_status,payment_time,express_id,express_no,express_fee,express_status,express_time,accept_name,post_code,telphone,mobile,area,address,message,remark,payable_amount,real_amount,order_amount,point,status,add_time,confirm_time,complete_time,str_code,brand_id,winery_time,is_bill,bill_type,store_name,store_address,store_id");
            strSql.Append(" from " + databaseprefix + "orders ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            Model.orders model = new Model.orders();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                #region 父表信息
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                model.order_no = ds.Tables[0].Rows[0]["order_no"].ToString();
                model.trade_no = ds.Tables[0].Rows[0]["trade_no"].ToString();
                if (ds.Tables[0].Rows[0]["user_id"].ToString() != "")
                {
                    model.user_id = int.Parse(ds.Tables[0].Rows[0]["user_id"].ToString());
                }
                model.user_name = ds.Tables[0].Rows[0]["user_name"].ToString();
                if (ds.Tables[0].Rows[0]["payment_id"].ToString() != "")
                {
                    model.payment_id = int.Parse(ds.Tables[0].Rows[0]["payment_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["payment_fee"].ToString() != "")
                {
                    model.payment_fee = decimal.Parse(ds.Tables[0].Rows[0]["payment_fee"].ToString());
                }
                if (ds.Tables[0].Rows[0]["payment_status"].ToString() != "")
                {
                    model.payment_status = int.Parse(ds.Tables[0].Rows[0]["payment_status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["payment_time"].ToString() != "")
                {
                    model.payment_time = DateTime.Parse(ds.Tables[0].Rows[0]["payment_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["express_id"].ToString() != "")
                {
                    model.express_id = int.Parse(ds.Tables[0].Rows[0]["express_id"].ToString());
                }
                model.express_no = ds.Tables[0].Rows[0]["express_no"].ToString();
                if (ds.Tables[0].Rows[0]["express_fee"].ToString() != "")
                {
                    model.express_fee = decimal.Parse(ds.Tables[0].Rows[0]["express_fee"].ToString());
                }
                if (ds.Tables[0].Rows[0]["express_status"].ToString() != "")
                {
                    model.express_status = int.Parse(ds.Tables[0].Rows[0]["express_status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["express_time"].ToString() != "")
                {
                    model.express_time = DateTime.Parse(ds.Tables[0].Rows[0]["express_time"].ToString());
                }
                model.accept_name = ds.Tables[0].Rows[0]["accept_name"].ToString();
                model.post_code = ds.Tables[0].Rows[0]["post_code"].ToString();
                model.telphone = ds.Tables[0].Rows[0]["telphone"].ToString();
                model.mobile = ds.Tables[0].Rows[0]["mobile"].ToString();
                model.area = ds.Tables[0].Rows[0]["area"].ToString();
                model.address = ds.Tables[0].Rows[0]["address"].ToString();
                model.message = ds.Tables[0].Rows[0]["message"].ToString();
                model.remark = ds.Tables[0].Rows[0]["remark"].ToString();
                if (ds.Tables[0].Rows[0]["payable_amount"].ToString() != "")
                {
                    model.payable_amount = decimal.Parse(ds.Tables[0].Rows[0]["payable_amount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["real_amount"].ToString() != "")
                {
                    model.real_amount = decimal.Parse(ds.Tables[0].Rows[0]["real_amount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["order_amount"].ToString() != "")
                {
                    model.order_amount = decimal.Parse(ds.Tables[0].Rows[0]["order_amount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["point"].ToString() != "")
                {
                    model.point = int.Parse(ds.Tables[0].Rows[0]["point"].ToString());
                }
                if (ds.Tables[0].Rows[0]["status"].ToString() != "")
                {
                    model.status = int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["add_time"].ToString() != "")
                {
                    model.add_time = DateTime.Parse(ds.Tables[0].Rows[0]["add_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["add_time"].ToString() != "")
                {
                    model.timestring = ds.Tables[0].Rows[0]["add_time"].ToString();
                }
                if (ds.Tables[0].Rows[0]["confirm_time"].ToString() != "")
                {
                    model.confirm_time = DateTime.Parse(ds.Tables[0].Rows[0]["confirm_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["complete_time"].ToString() != "")
                {
                    model.complete_time = DateTime.Parse(ds.Tables[0].Rows[0]["complete_time"].ToString());
                }
                model.str_code = ds.Tables[0].Rows[0]["str_code"].ToString();
                if (ds.Tables[0].Rows[0]["brand_id"].ToString() != "")
                {
                    model.brand_id = int.Parse(ds.Tables[0].Rows[0]["brand_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["winery_time"].ToString() != "")
                {
                    model.winery_time = DateTime.Parse(ds.Tables[0].Rows[0]["winery_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["is_bill"].ToString() != "")
                {
                    model.is_bill = int.Parse(ds.Tables[0].Rows[0]["is_bill"].ToString());
                }
                if (ds.Tables[0].Rows[0]["bill_type"].ToString() != "")
                {
                    model.bill_type = int.Parse(ds.Tables[0].Rows[0]["bill_type"].ToString());
                }
                #endregion

                #region 子表信息
                StringBuilder strSql2 = new StringBuilder();
                strSql2.Append("select id,order_id,goods_id,goods_title,goods_price,real_price,quantity,point,goods_pic,strcode,type,total,weight from " + databaseprefix + "order_goods ");
                strSql2.Append(" where order_id=@id ");
                SqlParameter[] parameters2 = {
					    new SqlParameter("@id", SqlDbType.Int,4)};
                parameters2[0].Value = id;

                DataSet ds2 = DbHelperSQL.Query(strSql2.ToString(), parameters2);
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    int i = ds2.Tables[0].Rows.Count;
                    List<Model.order_goods> models = new List<Model.order_goods>();
                    Model.order_goods modelt;
                    for (int n = 0; n < i; n++)
                    {
                        modelt = new Model.order_goods();
                        if (ds2.Tables[0].Rows[n]["id"] != null && ds2.Tables[0].Rows[n]["id"].ToString() != "")
                        {
                            modelt.id = int.Parse(ds2.Tables[0].Rows[n]["id"].ToString());
                        }
                        if (ds2.Tables[0].Rows[n]["order_id"] != null && ds2.Tables[0].Rows[n]["order_id"].ToString() != "")
                        {
                            modelt.order_id = int.Parse(ds2.Tables[0].Rows[n]["order_id"].ToString());
                        }
                        if (ds2.Tables[0].Rows[n]["goods_id"] != null && ds2.Tables[0].Rows[n]["goods_id"].ToString() != "")
                        {
                            modelt.goods_id = int.Parse(ds2.Tables[0].Rows[n]["goods_id"].ToString());
                        }
                        if (ds2.Tables[0].Rows[n]["goods_title"] != null && ds2.Tables[0].Rows[n]["goods_title"].ToString() != "")
                        {
                            modelt.goods_title = ds2.Tables[0].Rows[n]["goods_title"].ToString();
                        }
                        if (ds2.Tables[0].Rows[n]["goods_price"] != null && ds2.Tables[0].Rows[n]["goods_price"].ToString() != "")
                        {
                            modelt.goods_price = decimal.Parse(ds2.Tables[0].Rows[n]["goods_price"].ToString());
                        }
                        if (ds2.Tables[0].Rows[n]["real_price"] != null && ds2.Tables[0].Rows[n]["real_price"].ToString() != "")
                        {
                            modelt.real_price = decimal.Parse(ds2.Tables[0].Rows[n]["real_price"].ToString());
                        }
                        if (ds2.Tables[0].Rows[n]["quantity"] != null && ds2.Tables[0].Rows[n]["quantity"].ToString() != "")
                        {
                            modelt.quantity = int.Parse(ds2.Tables[0].Rows[n]["quantity"].ToString());
                        }
                        if (ds2.Tables[0].Rows[n]["point"] != null && ds2.Tables[0].Rows[n]["point"].ToString() != "")
                        {
                            modelt.point = int.Parse(ds2.Tables[0].Rows[n]["point"].ToString());
                        }
                        if (ds2.Tables[0].Rows[n]["goods_pic"] != null && ds2.Tables[0].Rows[n]["goods_pic"].ToString() != "")
                        {
                            modelt.goods_pic = ds2.Tables[0].Rows[n]["goods_pic"].ToString();
                        }
                        if (ds2.Tables[0].Rows[n]["strcode"] != null && ds2.Tables[0].Rows[n]["strcode"].ToString() != "")
                        {
                            modelt.strcode = ds2.Tables[0].Rows[n]["strcode"].ToString();
                        }
                        if (ds2.Tables[0].Rows[n]["type"] != null && ds2.Tables[0].Rows[n]["type"].ToString() != "")
                        {
                            modelt.type = ds2.Tables[0].Rows[n]["type"].ToString();
                        }
                        if (ds2.Tables[0].Rows[n]["total"] != null && ds2.Tables[0].Rows[n]["total"].ToString() != "")
                        {
                            modelt.total = decimal.Parse(ds2.Tables[0].Rows[n]["total"].ToString());
                        }
                        if (ds2.Tables[0].Rows[n]["weight"] != null && ds2.Tables[0].Rows[n]["weight"].ToString() != "")
                        {
                            modelt.weight = decimal.Parse(ds2.Tables[0].Rows[n]["weight"].ToString());
                        }
                        models.Add(modelt);
                    }
                    model.order_goods = models;
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
        /// 根据订单号返回一个实体
        /// </summary>
        public Model.orders GetModel(string order_no)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id from " + databaseprefix + "orders");
            strSql.Append(" where order_no=@order_no");
            SqlParameter[] parameters = {
					new SqlParameter("@order_no", SqlDbType.NVarChar,100)};
            parameters[0].Value = order_no;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj != null)
            {
                return GetModel(Convert.ToInt32(obj));
            }
            return null;
        }

        /// <summary>
        /// 根据用户ID返回一个实体
        /// </summary>
        public Model.orders GetModelUserid(int userid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  id,order_no,trade_no,user_id,user_name,payment_id,payment_fee,payment_status,payment_time,express_id,express_no,express_fee,express_status,express_time,accept_name,post_code,telphone,mobile,area,address,message,remark,payable_amount,real_amount,order_amount,point,status,add_time,confirm_time,complete_time,str_code,brand_id,winery_time,is_bill,bill_type");
            strSql.Append(" from " + databaseprefix + "orders ");
            strSql.Append(" where user_id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = userid;

            Model.orders model = new Model.orders();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                //时间拼接
                string _timestring = "";
                //订单号拼接
                string _order_no = "";
                //总额拼接
                string _order_amountstring = "";
                //状态拼接
                string _statusstring = "";
                //发货状态
                string _express_statusstring = "";
                //订单ID
                string _idstring = "";
                #region 父表信息
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["id"].ToString() != "")
                    {
                        _idstring += ds.Tables[0].Rows[i]["id"].ToString() + ",";
                    }
                    _order_no += ds.Tables[0].Rows[i]["order_no"].ToString() + ",";
                    model.trade_no = ds.Tables[0].Rows[i]["trade_no"].ToString();
                    if (ds.Tables[0].Rows[i]["user_id"].ToString() != "")
                    {
                        model.user_id = int.Parse(ds.Tables[0].Rows[i]["user_id"].ToString());
                    }
                    model.user_name = ds.Tables[0].Rows[i]["user_name"].ToString();
                    if (ds.Tables[0].Rows[i]["payment_id"].ToString() != "")
                    {
                        model.payment_id = int.Parse(ds.Tables[0].Rows[i]["payment_id"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["payment_fee"].ToString() != "")
                    {
                        model.payment_fee = decimal.Parse(ds.Tables[0].Rows[i]["payment_fee"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["payment_status"].ToString() != "")
                    {
                        model.payment_status = int.Parse(ds.Tables[0].Rows[i]["payment_status"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["payment_time"].ToString() != "")
                    {
                        model.payment_time = DateTime.Parse(ds.Tables[0].Rows[i]["payment_time"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["express_id"].ToString() != "")
                    {
                        model.express_id = int.Parse(ds.Tables[0].Rows[i]["express_id"].ToString());
                    }
                    model.express_no = ds.Tables[0].Rows[i]["express_no"].ToString();
                    if (ds.Tables[0].Rows[i]["express_fee"].ToString() != "")
                    {
                        model.express_fee = decimal.Parse(ds.Tables[0].Rows[i]["express_fee"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["express_status"].ToString() != "")
                    {
                        _express_statusstring += ds.Tables[0].Rows[i]["express_status"].ToString() + ",";
                    }
                    if (ds.Tables[0].Rows[i]["express_time"].ToString() != "")
                    {
                        model.express_time = DateTime.Parse(ds.Tables[0].Rows[i]["express_time"].ToString());
                    }
                    model.accept_name = ds.Tables[0].Rows[i]["accept_name"].ToString();
                    model.post_code = ds.Tables[0].Rows[i]["post_code"].ToString();
                    model.telphone = ds.Tables[0].Rows[i]["telphone"].ToString();
                    model.mobile = ds.Tables[0].Rows[i]["mobile"].ToString();
                    model.area = ds.Tables[0].Rows[i]["area"].ToString();
                    model.address = ds.Tables[0].Rows[i]["address"].ToString();
                    model.message = ds.Tables[0].Rows[i]["message"].ToString();
                    model.remark = ds.Tables[0].Rows[i]["remark"].ToString();
                    if (ds.Tables[0].Rows[i]["payable_amount"].ToString() != "")
                    {
                        model.payable_amount = decimal.Parse(ds.Tables[0].Rows[i]["payable_amount"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["real_amount"].ToString() != "")
                    {
                        model.real_amount += decimal.Parse(ds.Tables[0].Rows[i]["real_amount"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["order_amount"].ToString() != "")
                    {
                        _order_amountstring += ds.Tables[0].Rows[i]["order_amount"].ToString() + ",";
                    }
                    if (ds.Tables[0].Rows[i]["point"].ToString() != "")
                    {
                        model.point = int.Parse(ds.Tables[0].Rows[i]["point"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["status"].ToString() != "")
                    {
                        _statusstring += ds.Tables[0].Rows[i]["status"].ToString() + ",";
                    }
                    if (ds.Tables[0].Rows[i]["add_time"].ToString() != "")
                    {
                        _timestring += ds.Tables[0].Rows[i]["add_time"].ToString() + ",";
                    }
                    if (ds.Tables[0].Rows[i]["confirm_time"].ToString() != "")
                    {
                        model.confirm_time = DateTime.Parse(ds.Tables[0].Rows[i]["confirm_time"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["complete_time"].ToString() != "")
                    {
                        model.complete_time = DateTime.Parse(ds.Tables[0].Rows[i]["complete_time"].ToString());
                    }
                    model.str_code = ds.Tables[0].Rows[i]["str_code"].ToString();
                    if (ds.Tables[0].Rows[i]["brand_id"].ToString() != "")
                    {
                        model.brand_id = int.Parse(ds.Tables[0].Rows[i]["brand_id"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["winery_time"].ToString() != "")
                    {
                        model.winery_time = DateTime.Parse(ds.Tables[0].Rows[i]["winery_time"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["is_bill"].ToString() != "")
                    {
                        model.is_bill = int.Parse(ds.Tables[0].Rows[i]["is_bill"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["bill_type"].ToString() != "")
                    {
                        model.bill_type = int.Parse(ds.Tables[0].Rows[i]["bill_type"].ToString());
                    }
                }
                #endregion
                model.idstring = _idstring;
                model.express_statusstring = _express_statusstring;
                model.timestring = _timestring;
                model.order_no = _order_no;
                model.order_amountstring = _order_amountstring;
                model.statusstring = _statusstring;
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
            strSql.Append(@"id,order_no,trade_no,user_id,user_name,payment_id,payment_fee,payment_status,payment_time,express_id,express_no,express_fee,express_status,express_time,accept_name,post_code,telphone,mobile,area,address,message,remark,payable_amount,real_amount,order_amount,point,status,add_time,confirm_time,complete_time,str_code,brand_id,winery_time,is_bill,bill_type,store_name,store_address,store_id ");
            strSql.Append(" FROM " + databaseprefix + "orders ");
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
        public DataSet GetOrderList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top distinct " + Top.ToString());
            }
            strSql.Append(@"t1.id,t1.order_no,t1.trade_no,t1.user_id,t1.user_name,t1.payment_id,t1.payment_fee,t1.payment_status payment_status,t1.payment_time,t1.express_id,t1.express_no,t1.express_fee,t1.express_status express_status,t1.express_time,t1.accept_name,t1.post_code,t1.telphone,t1.mobile,t1.area,t1.address,t1.message,t1.remark,t1.payable_amount,t1.real_amount,t1.order_amount,t1.point,t1.status status,t1.add_time,t1.confirm_time,t1.complete_time,t1.str_code,t1.brand_id,t1.winery_time,t1.is_bill,t1.bill_type,t1.invoice_rise,t1.store_name,t1.store_address,t1.store_id,t2.refund_status refund_status,t2.id refund_id,t2.un_refund_reason un_refund_reason ");
            strSql.Append(" FROM dt_orders t1 LEFT JOIN dt_refund t2 ON t1.order_no = t2.order_no ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得订单数据查询订单金额
        /// </summary>
        public DataSet GetOrderAmount(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top distinct " + Top.ToString());
            }
            strSql.Append(@"t1.id,t1.order_no,t1.trade_no,t1.user_id,t1.user_name,t1.payment_id,t1.payment_fee,t1.payment_status payment_status,t1.payment_time,t1.express_id,t1.express_no,t1.express_fee,t1.express_status express_status,t1.express_time,t1.accept_name,t1.post_code,t1.telphone,t1.mobile,t1.area,t1.address,t1.message,t1.remark,t1.payable_amount,t1.real_amount,t1.order_amount,t1.point,t1.status status,t1.add_time,t1.confirm_time,t1.complete_time,t1.str_code,t1.brand_id,t1.winery_time,t1.is_bill,t1.bill_type,t1.invoice_rise,t1.store_name,t1.store_address,t2.refund_status refund_status,t2.id refund_id,t2.un_refund_reason un_refund_reason ");
            strSql.Append(" FROM dt_orders t1 LEFT JOIN dt_refund t2 ON t1.order_no = t2.order_no ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得退货订单
        /// </summary>
        public DataSet GetrefundList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top distinct " + Top.ToString());
            }
            strSql.Append(@"t1.id,t1.order_no,t1.trade_no,t1.user_id,t1.user_name,t1.payment_id,t1.payment_fee,t1.payment_status,t1.payment_time,t1.express_id,t1.express_no,t1.express_fee,t1.express_status,t1.express_time,t1.accept_name,t1.post_code,t1.telphone,t1.mobile,t1.area,t1.address,t1.message,t1.remark,t1.payable_amount,t1.real_amount,t1.order_amount,t1.point,t1.status,t1.add_time,t1.confirm_time,t1.complete_time,t1.str_code,t1.brand_id,t1.winery_time,t1.is_bill,t1.bill_type,t2.refund_status refund_status,t2.id refund_id ");
            strSql.Append(" FROM dt_orders t1 inner JOIN dt_refund t2 ON t1.order_no = t2.order_no ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得订单所有数据
        /// </summary>
        public DataSet Getorderinfo(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top distinct " + Top.ToString());
            }
            strSql.Append(@"t1.id,t1.order_no,t1.trade_no,t1.user_id,t1.user_name,t1.payment_id,t1.payment_fee,t1.payment_status payment_status,t1.payment_time,t1.express_id,t1.express_no,t1.express_fee,t1.express_status express_status,t1.express_time,t1.accept_name,t1.post_code,t1.telphone,t1.mobile,t1.area,t1.address,t1.message,t1.remark,t1.payable_amount,t1.real_amount,t1.order_amount,t1.point,t1.status status,t1.add_time,t1.confirm_time,t1.complete_time,t1.str_code,t1.brand_id,t1.winery_time,t1.is_bill,t1.bill_type,t2.refund_status refund_status,t2.id refund_id,t2.un_refund_reason un_refund_reason,t3.goods_pic goods_pic ");
            strSql.Append(" FROM dt_orders t1 LEFT JOIN dt_refund t2 ON t1.order_no = t2.order_no INNER JOIN dt_order_goods t3 ON t1.id=t3.order_id ");
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
            strSql.Append(@"SELECT id, order_no, trade_no, user_id, user_name,nick_name, payment_id, payment_fee, payment_status, payment_time, express_id, express_no, 
                        express_fee, express_status, express_time, accept_name, post_code, telphone, mobile, area, address, message, remark, payable_amount, 
                        real_amount, order_amount, point, status, add_time, confirm_time, complete_time, str_code, brand_id,winery_time,is_bill,bill_type");
            strSql.Append(" FROM v_orders ");
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
        public DataSet GetList1(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            //if (pageSize > 0)
            //{
            //    strSql.Append(" top " + pageSize.ToString());
            //}
            strSql.Append(@" t1.id,t1.order_no,t1.trade_no,t1.user_id,t1.user_name,t1.payment_id,t1.payment_fee,t1.payment_status,t1.payment_time,t1.express_id,t1.express_no,t1.express_fee,t1.express_status,t1.express_time,t1.accept_name,t1.post_code,t1.telphone,t1.mobile,t1.area,t1.address,t1.message,t1.remark,t1.payable_amount,t1.real_amount,t1.order_amount,t1.point,t1.status,t1.add_time,t1.confirm_time,t1.complete_time,t1.str_code,t1.brand_id,t1.winery_time,t1.is_bill,t1.bill_type,t1.down_order,t1.store_id,t1.store_name,t2.refund_status refund_status,t2.id refund_id ");
            strSql.Append(" FROM dt_orders t1 LEFT JOIN dt_refund t2 ON t1.order_no = t2.order_no ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(Vincent._DTcms.PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(Vincent._DTcms.PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }
        /// <summary>
        /// 获得查询分页数据并查询出用户名
        /// </summary>
        public DataSet GetList1andaddnickname(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(@"
    t1.id,t1.order_no,t1.trade_no,t1.user_id,t1.user_name,t1.payment_id,
    t1.payment_fee,t1.payment_status,t1.payment_time,t1.express_id,t1.express_no,
    t1.express_fee,t1.express_status,t1.express_time,t1.accept_name,t1.post_code,
    t1.telphone,t1.mobile,t1.area,t1.address,t1.message,t1.remark,t1.payable_amount,
    t1.real_amount,t1.order_amount,t1.point,t1.status,t1.add_time,t1.confirm_time,
    t1.complete_time,t1.str_code,t1.brand_id,t1.winery_time,t1.is_bill,t1.bill_type,
    t1.down_order,t1.store_id,t1.store_name,t2.refund_status refund_status,
    t2.id refund_id,  t3.nick_name  ");
            strSql.Append(" FROM (dt_orders t1 LEFT JOIN dt_refund t2 ON t1.order_no = t2.order_no)  LEFT JOIN dt_users t3 on t1.user_id=t3.id ");
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
        public DataSet GetMultiList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append("  * FROM dt_orders t1 INNER JOIN dbo.dt_order_goods t2 ON t1.id=t2.order_id");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取下线订单
        /// </summary>
        public DataSet GetsShareUserOrder(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("   select     t1.id,t1.order_no,t1.trade_no,t1.user_id,t1.user_name,t1.payment_id,t1.payment_fee,t1.payment_status,t1.payment_time,t1.express_id,t1.express_no,t1.express_fee,t1.express_status,t1.express_time,t1.accept_name,t1.post_code,t1.telphone,t1.mobile,t1.area,t1.address,t1.message,t1.remark,t1.payable_amount,t1.real_amount,t1.order_amount,t1.point,t1.status,t1.add_time,t1.confirm_time,t1.complete_time,t1.str_code,t1.brand_id,t1.winery_time,t1.is_bill,t1.bill_type,t1.down_order,t1.store_id,t1.store_name, u.nick_name");
            strSql.Append(" from dt_orders t1 left join dt_users u on t1.user_id=u.id  left join dt_outlet d on d.userid=t1.user_id ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where  user_id in  (select id  from dbo.F_GetUserNetByShare (" + strWhere + "))");
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(Vincent._DTcms.PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(Vincent._DTcms.PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }

        /// <summary>
        /// 获取店铺id
        /// </summary>
        public DataSet Getoutlet_fuwuzhan(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("   select     t1.id,t1.order_no,t1.trade_no,t1.user_id,t1.user_name,t1.payment_id,t1.payment_fee,t1.payment_status,t1.payment_time,t1.express_id,t1.express_no,t1.express_fee,t1.express_status,t1.express_time,t1.accept_name,t1.post_code,t1.telphone,t1.mobile,t1.area,t1.address,t1.message,t1.remark,t1.payable_amount,t1.real_amount,t1.order_amount,t1.point,t1.status,t1.add_time,t1.confirm_time,t1.complete_time,t1.str_code,t1.brand_id,t1.winery_time,t1.is_bill,t1.bill_type,t1.down_order,t1.store_id,t1.store_name, u.nick_name");
            strSql.Append(" from dt_orders t1 left join dt_users u on t1.user_id=u.id  left join dt_outlet d on d.userid=t1.user_id ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where t1.brand_id=" + strWhere + "");
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(Vincent._DTcms.PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(Vincent._DTcms.PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }

        /// <summary>
        /// 获取店铺id
        /// </summary>
        public DataSet Getoutlet_fuwuzhanbystoreid(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("   select     t1.id,t1.order_no,t1.trade_no,t1.user_id,t1.user_name,t1.payment_id,t1.payment_fee,t1.payment_status,t1.payment_time,t1.express_id,t1.express_no,t1.express_fee,t1.express_status,t1.express_time,t1.accept_name,t1.post_code,t1.telphone,t1.mobile,t1.area,t1.address,t1.message,t1.remark,t1.payable_amount,t1.real_amount,t1.order_amount,t1.point,t1.status,t1.add_time,t1.confirm_time,t1.complete_time,t1.str_code,t1.brand_id,t1.winery_time,t1.is_bill,t1.bill_type,t1.down_order,t1.store_id,t1.store_name, u.nick_name");
            strSql.Append(" from dt_orders t1 left join dt_users u on t1.user_id=u.id  left join dt_outlet d on d.userid=t1.user_id ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where t1.store_id=" + strWhere + "");
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(Vincent._DTcms.PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(Vincent._DTcms.PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }


        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetOrderLists(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select t1.id,t1.order_no,t1.trade_no,t1.user_id,t1.user_name,t1.payment_id,t1.payment_fee,t1.payment_status,t1.payment_time,t1.express_id,t1.express_no,t1.express_fee,t1.express_status,t1.express_time,t1.accept_name,t1.post_code,t1.telphone,t1.mobile,t1.area,t1.address,t1.message,t1.remark,t1.payable_amount,t1.real_amount,t1.order_amount,t1.point,t1.status,t1.add_time,t1.confirm_time,t1.complete_time,t1.str_code,t1.brand_id,t1.winery_time,t1.is_bill,t1.bill_type,t2.goods_pic");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append("   FROM dt_orders t1 INNER JOIN dbo.dt_order_goods t2 ON t1.id=t2.order_id inner join dt_users t3 on t1.user_id=t3.id");
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
        public DataSet GetMultiList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT t1.*,t2.order_id, t2.goods_id, t2.goods_title, t2.goods_price, t2.real_price, t2.quantity, t2.point as t2point, t2.goods_pic FROM dbo.dt_orders t1 ");
            strSql.Append("INNER JOIN dbo.dt_order_goods t2 ON t1.id = t2.order_id ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(Vincent._DTcms.PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(Vincent._DTcms.PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }

        /// <summary>
        /// 今日奖池总金额
        /// </summary>
        /// <returns></returns>
        public string GetBalanceTotalDay()
        {
            //SELECT * FROM dbo.dt_orders WHERE convert(char(7),add_time,120)= '2014-10'
            string nowTime = string.Format("{0:yyyy-MM}", DateTime.Now);
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT isnull(SUM(real_amount),0) as totalAmount FROM " + databaseprefix + "orders ");

            strSql.Append("where payment_status = 2 and DATEDIFF(dd, add_time, GETDATE()) = 0 ");
            //select isnull(SUM(real_amount),0) as totalAmount from dt_orders where payment_status = 2 and DATEDIFF(dd, add_time, GETDATE()) = 0 
            //strSql.Append("WHERE convert(char(7),add_time,120)= '" + nowTime + "' ");

            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0].Rows[0]["totalAmount"].ToString();
            }

            return "0";
        }

        /// <summary>
        /// 奖池总金额
        /// </summary>
        /// <returns></returns>
        public string GetBalanceTotalAll()
        {
            //SELECT * FROM dbo.dt_orders WHERE convert(char(7),add_time,120)= '2014-10'
            string nowTime = string.Format("{0:yyyy-MM}", DateTime.Now);
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT isnull(SUM(real_amount),0) as totalAmount FROM " + databaseprefix + "orders ");

            strSql.Append("where payment_status = 2 and id >479");
            //select isnull(SUM(real_amount),0) as totalAmount from dt_orders where payment_status = 2 and DATEDIFF(dd, add_time, GETDATE()) = 0 
            //strSql.Append("WHERE convert(char(7),add_time,120)= '" + nowTime + "' ");

            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0].Rows[0]["totalAmount"].ToString();
            }

            return "0";
        }

        /// <summary>
        /// 奖金收入明细
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="strWhere"></param>
        /// <param name="filedOrder"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public DataSet GetUserBalanceList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM v_systemlog ");
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