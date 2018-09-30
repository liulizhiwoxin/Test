using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using BuysingooShop.DBUtility;
using Vincent;//Please add references
namespace BuysingooShop.DAL
{
	/// <summary>
	/// 数据访问类:user_address
	/// </summary>
	public partial class user_address
	{
        private string databaseprefix; //数据库表名前缀
        public user_address(string _databaseprefix)
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
            strSql.Append("select count(1) from " + databaseprefix + "user_address");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}

        /// <summary>
        /// 是否存在该记录(userid)
        /// </summary>
        public bool Existsuserid(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "user_address");
            strSql.Append(" where user_id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(BuysingooShop.Model.user_address model)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "user_address(");
            strSql.Append("user_id,provinces,citys,area,street,address,add_time,modity_time,is_default,mobile,postcode,acceptName)");
			strSql.Append(" values (");
            strSql.Append("@user_id,@provinces,@citys,@area,@street,@address,@add_time,@modity_time,@is_default,@mobile,@postcode,@acceptName)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@user_id", SqlDbType.Int,4),
					new SqlParameter("@provinces", SqlDbType.NVarChar,50),
					new SqlParameter("@citys", SqlDbType.NVarChar,50),
					new SqlParameter("@area", SqlDbType.NVarChar,50),
					new SqlParameter("@street", SqlDbType.NVarChar,50),
					new SqlParameter("@address", SqlDbType.NVarChar,100),
					new SqlParameter("@add_time", SqlDbType.DateTime),
					new SqlParameter("@modity_time", SqlDbType.DateTime),
                    new SqlParameter("@is_default", SqlDbType.Int,4),
                    new SqlParameter("@mobile", SqlDbType.VarChar,11),
                    new SqlParameter("@postcode", SqlDbType.Int,6),
                    new SqlParameter("@acceptName", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.user_id;
			parameters[1].Value = model.provinces;
			parameters[2].Value = model.citys;
			parameters[3].Value = model.area;
			parameters[4].Value = model.street;
			parameters[5].Value = model.address;
			parameters[6].Value = model.add_time;
			parameters[7].Value = model.modity_time;
            parameters[8].Value = model.is_default;
            parameters[9].Value = model.mobile;
            parameters[10].Value = model.postcode;
            parameters[11].Value = model.acceptName;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);

            //修改默认收货地址
            string strwhere = string.Format("user_id={0} AND id!={1}", model.user_id, Convert.ToInt32(obj));
            if (model.is_default == 1)
            {
                UpdateField("is_default=0", "id IN(SELECT id FROM dbo.dt_user_address WHERE " + strwhere + ")");
            }
		    return obj == null ? 0 : Convert.ToInt32(obj);
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(BuysingooShop.Model.user_address model)
		{
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update dt_user_address set ");
                strSql.Append("user_id=@user_id,");
                strSql.Append("provinces=@provinces,");
                strSql.Append("citys=@citys,");
                strSql.Append("area=@area,");
                strSql.Append("street=@street,");
                strSql.Append("address=@address,");
                strSql.Append("add_time=@add_time,");
                strSql.Append("modity_time=@modity_time,");
                strSql.Append("is_default=@is_default,");
                strSql.Append("mobile=@mobile,");
                strSql.Append("postcode=@postcode,");
                strSql.Append("acceptName=@acceptName");
                strSql.Append(" where id=@id");
                SqlParameter[] parameters = {
					        new SqlParameter("@user_id", SqlDbType.Int,4),
					        new SqlParameter("@provinces", SqlDbType.NVarChar,50),
					        new SqlParameter("@citys", SqlDbType.NVarChar,50),
					        new SqlParameter("@area", SqlDbType.NVarChar,50),
					        new SqlParameter("@street", SqlDbType.NVarChar,50),
					        new SqlParameter("@address", SqlDbType.NVarChar,100),
					        new SqlParameter("@add_time", SqlDbType.DateTime),
					        new SqlParameter("@modity_time", SqlDbType.DateTime),
                            new SqlParameter("@is_default", SqlDbType.Int,4),
                            new SqlParameter("@mobile", SqlDbType.VarChar,11),
                            new SqlParameter("@postcode", SqlDbType.Int,6),
                            new SqlParameter("@acceptName", SqlDbType.NVarChar,50),
					        new SqlParameter("@id", SqlDbType.Int,4)};
                parameters[0].Value = model.user_id;
                parameters[1].Value = model.provinces;
                parameters[2].Value = model.citys;
                parameters[3].Value = model.area;
                parameters[4].Value = model.street;
                parameters[5].Value = model.address;
                parameters[6].Value = model.add_time;
                parameters[7].Value = model.modity_time;
                parameters[8].Value = model.is_default;
                parameters[9].Value = model.mobile;
                parameters[10].Value = model.postcode;
                parameters[11].Value = model.acceptName;
                parameters[12].Value = model.id;

                int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
                //修改默认收货地址
                string strwhere = string.Format("user_id={0} AND id!={1}", model.user_id, model.id);
                if (model.is_default == 1)
                {
                    UpdateField("is_default=0","id IN(SELECT id FROM dbo.dt_user_address WHERE " + strwhere + ")");
                }
                return rows > 0 ? true : false;

            }
            catch (Exception)
            {
                throw;
            }
			
		}
        
        /// <summary>
        /// 修改一列数据
        /// </summary>
        public void UpdateField(string strValue, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "user_address set " + strValue);
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where "+strWhere);
            }
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "user_address ");
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
            strSql.Append("delete from " + databaseprefix + "user_address ");
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
        public bool Delete(int id, int user_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "user_address ");
            strSql.Append(" where id=@id and user_id=@user_id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
                    new SqlParameter("@user_id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            parameters[1].Value = user_id;

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
		public BuysingooShop.Model.user_address GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select  top 1 id,user_id,provinces,citys,area,street,address,add_time,modity_time,is_default,mobile,postcode,acceptName");
            strSql.Append(" from " + databaseprefix + "user_address");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			BuysingooShop.Model.user_address model=new BuysingooShop.Model.user_address();
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
        /// 得到一个对象实体(特殊)
        /// </summary>
        public BuysingooShop.Model.user_address GetModels(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,user_id,provinces,citys,area,street,address,add_time,modity_time,is_default,mobile,postcode,acceptName");
            strSql.Append(" from " + databaseprefix + "user_address");
            strSql.Append(" where user_id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            BuysingooShop.Model.user_address model = new BuysingooShop.Model.user_address();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string _provinces="";
                string _citys = "";
                string _area = "";
                string _street = "";
                string _mobile = "";
                string _post = "";
                string _acceptName = "";
                string _address="";
                string _idstring = "";
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if(ds.Tables[0].Rows[i]["id"]!=null && ds.Tables[0].Rows[i]["id"].ToString()!="")
				    {
				    	_idstring+=ds.Tables[0].Rows[i]["id"].ToString()+",";
				    }
				    if(ds.Tables[0].Rows[i]["user_id"]!=null && ds.Tables[0].Rows[i]["user_id"].ToString()!="")
				    {
				    	model.user_id=int.Parse(ds.Tables[0].Rows[i]["user_id"].ToString());
				    }
				    if(ds.Tables[0].Rows[i]["provinces"]!=null)
				    {
				    	_provinces+=ds.Tables[0].Rows[i]["provinces"].ToString()+",";
				    }
				    if(ds.Tables[0].Rows[i]["citys"]!=null)
				    {
				    	_citys+=ds.Tables[0].Rows[i]["citys"].ToString()+",";
				    }
				    if(ds.Tables[0].Rows[i]["area"]!=null)
				    {
				    	_area+=ds.Tables[0].Rows[i]["area"].ToString()+",";
				    }
				    if(ds.Tables[0].Rows[i]["street"]!=null)
				    {
				    	_street+=ds.Tables[0].Rows[i]["street"].ToString()+",";
				    }
				    if(ds.Tables[0].Rows[i]["address"]!=null)
				    {
                        _address+= ds.Tables[0].Rows[i]["address"].ToString()+",";
				    }
				    if(ds.Tables[0].Rows[i]["add_time"]!=null && ds.Tables[0].Rows[i]["add_time"].ToString()!="")
				    {
				    	model.add_time=DateTime.Parse(ds.Tables[0].Rows[i]["add_time"].ToString());
				    }
				    if(ds.Tables[0].Rows[i]["modity_time"]!=null && ds.Tables[0].Rows[i]["modity_time"].ToString()!="")
				    {
				    	model.modity_time=DateTime.Parse(ds.Tables[0].Rows[i]["modity_time"].ToString());
				    }
                    if (ds.Tables[0].Rows[i]["is_default"] != null && ds.Tables[0].Rows[i]["is_default"].ToString() != "")
                    {
                        model.is_default = int.Parse(ds.Tables[0].Rows[i]["is_default"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["mobile"] != null && ds.Tables[0].Rows[i]["mobile"].ToString() != "")
                    {
                        _mobile+= ds.Tables[0].Rows[i]["mobile"].ToString()+",";
                    }
                    if (ds.Tables[0].Rows[i]["postcode"] != null && ds.Tables[0].Rows[i]["postcode"].ToString() != "")
                    {
                        _post+= ds.Tables[0].Rows[i]["postcode"].ToString()+",";
                    }
                    if (ds.Tables[0].Rows[i]["acceptName"] != null && ds.Tables[0].Rows[i]["acceptName"].ToString() != "")
                    {
                        _acceptName+= ds.Tables[0].Rows[i]["acceptName"].ToString()+",";
                    }
                }
                model.address = _address.Substring(0,_address.Length-1);
                model.provinces = _provinces.Substring(0,_provinces.Length-1);
                model.citys = _citys.Substring(0,_citys.Length-1);
                model.area = _area.Substring(0,_area.Length-1);
                model.street = _street.Substring(0,_street.Length-1);
                model.post = _post.Substring(0,_post.Length-1);
                model.acceptName = _acceptName.Substring(0,_acceptName.Length-1);
                model.mobile = _mobile.Substring(0,_mobile.Length-1);
                model.idstring = _idstring.Substring(0,_idstring.Length-1);
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
		public BuysingooShop.Model.user_address DataRowToModel(DataRow row)
		{
			BuysingooShop.Model.user_address model=new BuysingooShop.Model.user_address();
			if (row != null)
			{
				if(row["id"]!=null && row["id"].ToString()!="")
				{
					model.id=int.Parse(row["id"].ToString());
				}
				if(row["user_id"]!=null && row["user_id"].ToString()!="")
				{
					model.user_id=int.Parse(row["user_id"].ToString());
				}
				if(row["provinces"]!=null)
				{
					model.provinces=row["provinces"].ToString();
				}
				if(row["citys"]!=null)
				{
					model.citys=row["citys"].ToString();
				}
				if(row["area"]!=null)
				{
					model.area=row["area"].ToString();
				}
				if(row["street"]!=null)
				{
					model.street=row["street"].ToString();
				}
				if(row["address"]!=null)
				{
					model.address=row["address"].ToString();
				}
				if(row["add_time"]!=null && row["add_time"].ToString()!="")
				{
					model.add_time=DateTime.Parse(row["add_time"].ToString());
				}
				if(row["modity_time"]!=null && row["modity_time"].ToString()!="")
				{
					model.modity_time=DateTime.Parse(row["modity_time"].ToString());
				}
                if (row["is_default"] != null && row["is_default"].ToString() != "")
                {
                    model.is_default = int.Parse(row["is_default"].ToString());
                }
                if (row["mobile"] != null && row["mobile"].ToString() != "")
                {
                    model.mobile = row["mobile"].ToString();
                }
                if (row["postcode"] != null && row["postcode"].ToString() != "")
                {
                    model.postcode = int.Parse(row["postcode"].ToString());
                }
                if (row["acceptName"] != null && row["acceptName"].ToString() != "")
                {
                    model.acceptName = row["acceptName"].ToString();
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
            strSql.Append("select id,user_id,provinces,citys,area,street,address,add_time,modity_time,is_default,mobile,postcode,acceptName ");
            strSql.Append(" FROM " + databaseprefix + "user_address ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
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
			    strSql.Append(" top " + Top.ToString());
			}
            strSql.Append(" id,user_id,provinces,citys,area,street,address,add_time,modity_time,is_default,mobile,postcode,acceptName ");
            strSql.Append(" FROM " + databaseprefix + "user_address ");
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
            strSql.Append("select count(1) FROM " + databaseprefix + "user_address ");
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
            strSql.Append(")AS Row, T.*  from " + databaseprefix + "user_address T ");
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
			parameters[0].Value = "dt_user_address";
			parameters[1].Value = "id";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM " + databaseprefix + "user_address");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(Vincent._DTcms.PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(Vincent._DTcms.PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

