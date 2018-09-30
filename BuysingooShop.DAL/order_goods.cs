using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using BuysingooShop.DBUtility;
using Vincent;

namespace BuysingooShop.DAL
{
	/// <summary>
	/// 数据访问类:order_goods
	/// </summary>
	public partial class order_goods
	{
        private string databaseprefix; //数据库表名前缀
        public order_goods(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }
		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "order_goods");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(BuysingooShop.Model.order_goods model)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "order_goods(");
            strSql.Append("order_id,goods_id,goods_title,goods_price,real_price,quantity,point,goods_pic,strcode,type,total,weight)");
			strSql.Append(" values (");
            strSql.Append("@order_id,@goods_id,@goods_title,@goods_price,@real_price,@quantity,@point,@goods_pic,@strcode,@type,@total,@weight)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@order_id", SqlDbType.Int,4),
					new SqlParameter("@goods_id", SqlDbType.Int,4),
					new SqlParameter("@goods_title", SqlDbType.NVarChar,100),
					new SqlParameter("@goods_price", SqlDbType.Decimal,5),
					new SqlParameter("@real_price", SqlDbType.Decimal,5),
					new SqlParameter("@quantity", SqlDbType.Int,4),
					new SqlParameter("@point", SqlDbType.Int,4),
					new SqlParameter("@goods_pic", SqlDbType.NVarChar,255),
                    new SqlParameter("@strcode", SqlDbType.VarChar,50),
                    new SqlParameter("@type", SqlDbType.VarChar,50),
                    new SqlParameter("@total", SqlDbType.Decimal,5),
                    new SqlParameter("@weight", SqlDbType.Decimal,5) };
			parameters[0].Value = model.order_id;
			parameters[1].Value = model.goods_id;
			parameters[2].Value = model.goods_title;
			parameters[3].Value = model.goods_price;
			parameters[4].Value = model.real_price;
			parameters[5].Value = model.quantity;
			parameters[6].Value = model.point;
			parameters[7].Value = model.goods_pic;
            parameters[8].Value = model.strcode;
            parameters[9].Value = model.type;
            parameters[10].Value = model.total;
            parameters[11].Value = model.weight;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
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
		public bool Update(BuysingooShop.Model.order_goods model)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("update " + databaseprefix + "order_goods set ");
			strSql.Append("order_id=@order_id,");
			strSql.Append("goods_id=@goods_id,");
			strSql.Append("goods_title=@goods_title,");
			strSql.Append("goods_price=@goods_price,");
			strSql.Append("real_price=@real_price,");
			strSql.Append("quantity=@quantity,");
			strSql.Append("point=@point,");
			strSql.Append("goods_pic=@goods_pic,");
            strSql.Append("strcode=@strcode,");
            strSql.Append("type=@type,");
            strSql.Append("total=@total,");
            strSql.Append("weight=@weight");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@order_id", SqlDbType.Int,4),
					new SqlParameter("@goods_id", SqlDbType.Int,4),
					new SqlParameter("@goods_title", SqlDbType.NVarChar,100),
					new SqlParameter("@goods_price", SqlDbType.Decimal,5),
					new SqlParameter("@real_price", SqlDbType.Decimal,5),
					new SqlParameter("@quantity", SqlDbType.Int,4),
					new SqlParameter("@point", SqlDbType.Int,4),
					new SqlParameter("@goods_pic", SqlDbType.NVarChar,255),
                    new SqlParameter("@strcode", SqlDbType.VarChar,50),              
                    new SqlParameter("@type", SqlDbType.VarChar,50),
                    new SqlParameter("@total", SqlDbType.Decimal,5),
                    new SqlParameter("@weight", SqlDbType.Decimal,5),
                    new SqlParameter("@id", SqlDbType.Int,4) };
			parameters[0].Value = model.order_id;
			parameters[1].Value = model.goods_id;
			parameters[2].Value = model.goods_title;
			parameters[3].Value = model.goods_price;
			parameters[4].Value = model.real_price;
			parameters[5].Value = model.quantity;
			parameters[6].Value = model.point;
			parameters[7].Value = model.goods_pic;
            parameters[8].Value = model.strcode;
            parameters[9].Value = model.type;
            parameters[10].Value = model.total;
            parameters[11].Value = model.weight;
            parameters[12].Value = model.id;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "order_goods ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string idlist )
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "order_goods ");
			strSql.Append(" where id in ("+idlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
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
		public BuysingooShop.Model.order_goods GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select  top 1 id,order_id,goods_id,goods_title,goods_price,real_price,quantity,point,goods_pic,strcode,type,total,weight from " + databaseprefix + "order_goods ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			BuysingooShop.Model.order_goods model=new BuysingooShop.Model.order_goods();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}

        /// <summary>
        /// 得到一个对象实体(通过orderID)
        /// </summary>
        public BuysingooShop.Model.order_goods GetModelorderid(int orderid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,order_id,goods_id,goods_title,goods_price,real_price,quantity,point,goods_pic,strcode,type,total,weight from " + databaseprefix + "order_goods ");
            strSql.Append(" where order_id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = orderid;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                //商品名称
                string _goods_title = "";
                //商品类型
                string _type = "";
                //商品总额
                string _totalstring = "";
                //商品数量
                string _quantitystring = "";
                //商品单价
                string _goods_pricestring = "";
                //商品重量
                string _weightstring = "";
                BuysingooShop.Model.order_goods model = new BuysingooShop.Model.order_goods();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["id"] != null && ds.Tables[0].Rows[i]["id"].ToString() != "")
                    {
                        model.id = int.Parse(ds.Tables[0].Rows[i]["id"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["order_id"] != null && ds.Tables[0].Rows[i]["order_id"].ToString() != "")
                    {
                        model.order_id = int.Parse(ds.Tables[0].Rows[i]["order_id"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["goods_id"] != null && ds.Tables[0].Rows[i]["goods_id"].ToString() != "")
                    {
                        model.goods_id = int.Parse(ds.Tables[0].Rows[i]["goods_id"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["goods_title"] != null)
                    {
                        _goods_title+= ds.Tables[0].Rows[i]["goods_title"].ToString()+",";
                    }
                    if (ds.Tables[0].Rows[i]["goods_price"] != null && ds.Tables[0].Rows[i]["goods_price"].ToString() != "")
                    {
                        _goods_pricestring+= ds.Tables[0].Rows[i]["goods_price"].ToString()+",";
                    }
                    if (ds.Tables[0].Rows[i]["real_price"] != null && ds.Tables[0].Rows[i]["real_price"].ToString() != "")
                    {
                        model.real_price = decimal.Parse(ds.Tables[0].Rows[i]["real_price"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["quantity"] != null && ds.Tables[0].Rows[i]["quantity"].ToString() != "")
                    {
                        _quantitystring+= ds.Tables[0].Rows[i]["quantity"].ToString()+",";
                    }
                    if (ds.Tables[0].Rows[i]["point"] != null && ds.Tables[0].Rows[i]["point"].ToString() != "")
                    {
                        model.point = int.Parse(ds.Tables[0].Rows[i]["point"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["goods_pic"] != null)
                    {
                        model.goods_pic = ds.Tables[0].Rows[i]["goods_pic"].ToString();
                    }
                    if (ds.Tables[0].Rows[i]["strcode"] != null)
                    {
                        model.strcode = ds.Tables[0].Rows[i]["strcode"].ToString();
                    }
                    if (ds.Tables[0].Rows[i]["type"] != null && ds.Tables[0].Rows[i]["type"].ToString() != "")
                    {
                        _type+= ds.Tables[0].Rows[i]["type"].ToString()+",";
                    }
                    if (ds.Tables[0].Rows[i]["total"] != null && ds.Tables[0].Rows[i]["total"].ToString() != "")
                    {
                        _totalstring+= ds.Tables[0].Rows[i]["total"].ToString()+",";
                    }
                    if (ds.Tables[0].Rows[i]["weight"] != null && ds.Tables[0].Rows[i]["weight"].ToString() != "")
                    {
                        _weightstring+= ds.Tables[0].Rows[i]["weight"].ToString()+",";
                    }
                }
                model.totalstring = _totalstring.Substring(0,_totalstring.Length-1);
                model.goods_title = _goods_title;
                model.type = _type;
                model.quantitystring = _quantitystring.Substring(0,_quantitystring.Length-1);
                model.goods_pricestring = _goods_pricestring;
                model.weightstring = _weightstring;
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
        public BuysingooShop.Model.order_goods GetModel(string strwhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,order_id,goods_id,goods_title,goods_price,real_price,quantity,point,goods_pic,strcode,type,total,weight from " + databaseprefix + "order_goods ");
            strSql.Append(" where " + strwhere);
            BuysingooShop.Model.order_goods model = new BuysingooShop.Model.order_goods();
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            return ds.Tables[0].Rows.Count > 0 ? DataRowToModel(ds.Tables[0].Rows[0]) : null;
        }

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public BuysingooShop.Model.order_goods DataRowToModel(DataRow row)
		{
			BuysingooShop.Model.order_goods model=new BuysingooShop.Model.order_goods();
			if (row != null)
			{
				if(row["id"]!=null && row["id"].ToString()!="")
				{
					model.id=int.Parse(row["id"].ToString());
				}
				if(row["order_id"]!=null && row["order_id"].ToString()!="")
				{
					model.order_id=int.Parse(row["order_id"].ToString());
				}
				if(row["goods_id"]!=null && row["goods_id"].ToString()!="")
				{
					model.goods_id=int.Parse(row["goods_id"].ToString());
				}
				if(row["goods_title"]!=null)
				{
					model.goods_title=row["goods_title"].ToString();
				}
				if(row["goods_price"]!=null && row["goods_price"].ToString()!="")
				{
					model.goods_price=decimal.Parse(row["goods_price"].ToString());
				}
				if(row["real_price"]!=null && row["real_price"].ToString()!="")
				{
					model.real_price=decimal.Parse(row["real_price"].ToString());
				}
				if(row["quantity"]!=null && row["quantity"].ToString()!="")
				{
					model.quantity=int.Parse(row["quantity"].ToString());
				}
				if(row["point"]!=null && row["point"].ToString()!="")
				{
					model.point=int.Parse(row["point"].ToString());
				}
				if(row["goods_pic"]!=null)
				{
					model.goods_pic=row["goods_pic"].ToString();
				}
                if (row["strcode"] != null)
                {
                    model.strcode = row["strcode"].ToString();
                }
                if (row["type"] != null && row["type"].ToString() != "")
                {
                    model.type = row["type"].ToString();
                }
                if (row["total"] != null && row["total"].ToString() != "")
                {
                    model.total = decimal.Parse(row["total"].ToString());
                }
                if (row["weight"] != null && row["weight"].ToString() != "")
                {
                    model.weight = decimal.Parse(row["weight"].ToString());
                }
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select id,order_id,goods_id,goods_title,goods_price,real_price,quantity,point,goods_pic,strcode,type,total,weight ");
            strSql.Append(" FROM " + databaseprefix + "order_goods ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

        /// <summary>
        /// 获得数据列表（订单状态）
        /// </summary>
        public DataSet GetListStatus(int Top,string strWhere,string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" b.id,b.order_id,b.goods_id,b.goods_title,b.goods_price,b.real_price,b.quantity,b.point,b.goods_pic,b.strcode,b.type,b.total,b.weight,a.status,a.payment_status,a.express_status ");
            strSql.Append(" FROM " + databaseprefix + "order_goods b left join dt_orders a on b.order_id=a.id");
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
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
            strSql.Append(" id,order_id,goods_id,goods_title,goods_price,real_price,quantity,point,goods_pic,strcode,type,total,weight ");
            strSql.Append(" FROM " + databaseprefix + "order_goods ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select count(1) FROM " + databaseprefix + "order_goods ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
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
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.id desc");
			}
            strSql.Append(")AS Row, T.*  from " + databaseprefix + "order_goods T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "dt_order_goods";
			parameters[1].Value = "id";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

