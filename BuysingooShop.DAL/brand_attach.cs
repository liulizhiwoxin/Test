using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using BuysingooShop.DBUtility;
using Vincent;
using System.Collections.Generic;

namespace BuysingooShop.DAL
{
	/// <summary>
	/// 数据访问类:brand_attach
	/// </summary>
	public partial class brand_attach
	{
		public brand_attach()
		{}

		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from dt_brand_attach");
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
		public int Add(BuysingooShop.Model.brand_attach model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into dt_brand_attach(");
            strSql.Append("brand_id,theme_id,size,img_url,remark,small_imgurl,add_time)");
			strSql.Append(" values (");
            strSql.Append("@brand_id,@theme_id,@size,@img_url,@remark,@small_imgurl,add_time)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@brand_id", SqlDbType.Int,4),
					new SqlParameter("@theme_id", SqlDbType.Int,4),
					new SqlParameter("@size", SqlDbType.NChar,10),
					new SqlParameter("@img_url", SqlDbType.NChar,100),
					new SqlParameter("@remark", SqlDbType.NVarChar,100),
                    new SqlParameter("@small_imgurl", SqlDbType.NChar,100),
                    new SqlParameter("@add_time", SqlDbType.DateTime)};
			parameters[0].Value = model.brand_id;
			parameters[1].Value = model.theme_id;
			parameters[2].Value = model.size;
			parameters[3].Value = model.img_url;
			parameters[4].Value = model.remark;
            parameters[5].Value = model.small_imgurl;
            parameters[6].Value = model.add_time;

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
		public bool Update(BuysingooShop.Model.brand_attach model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update dt_brand_attach set ");
			strSql.Append("brand_id=@brand_id,");
			strSql.Append("theme_id=@theme_id,");
			strSql.Append("size=@size,");
			strSql.Append("img_url=@img_url,");
			strSql.Append("remark=@remark");
            strSql.Append("small_imgurl=@small_imgurl");
            strSql.Append("add_time=@add_time");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@brand_id", SqlDbType.Int,4),
					new SqlParameter("@theme_id", SqlDbType.Int,4),
					new SqlParameter("@size", SqlDbType.NChar,10),
					new SqlParameter("@img_url", SqlDbType.NChar,100),
					new SqlParameter("@remark", SqlDbType.NVarChar,100),
                    new SqlParameter("@small_imgurl", SqlDbType.NChar,100),
                    new SqlParameter("@add_time", SqlDbType.DateTime),
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = model.brand_id;
			parameters[1].Value = model.theme_id;
			parameters[2].Value = model.size;
			parameters[3].Value = model.img_url;
			parameters[4].Value = model.remark;
            parameters[5].Value = model.small_imgurl;
            parameters[6].Value = model.add_time;
			parameters[7].Value = model.id;

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
			strSql.Append("delete from dt_brand_attach ");
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
        /// 删除一条数据
        /// </summary>
        public bool Delete(string whereStr)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from dt_brand_attach ");
            if (whereStr != "")
            {
                strSql.Append(" where " + whereStr);
            }
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            return rows > 0 ? true : false;
        }

		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from dt_brand_attach ");
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
		public BuysingooShop.Model.brand_attach GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select  top 1 id,brand_id,theme_id,size,img_url,remark,small_imgurl,add_time from dt_brand_attach ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			BuysingooShop.Model.brand_attach model=new BuysingooShop.Model.brand_attach();
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
        /// 得到一个对象实体
        /// </summary>
        public BuysingooShop.Model.brand_attach DataRowToModel(DataRow row)
        {
            BuysingooShop.Model.brand_attach model = new BuysingooShop.Model.brand_attach();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["brand_id"] != null && row["brand_id"].ToString() != "")
                {
                    model.brand_id = int.Parse(row["brand_id"].ToString());
                }
                if (row["theme_id"] != null && row["theme_id"].ToString() != "")
                {
                    model.theme_id = int.Parse(row["theme_id"].ToString());
                }
                if (row["size"] != null)
                {
                    model.size = row["size"].ToString();
                }
                if (row["img_url"] != null)
                {
                    model.img_url = row["img_url"].ToString();
                }
                if (row["remark"] != null)
                {
                    model.remark = row["remark"].ToString();
                }
                if (row["small_imgurl"] != null)
                {
                    model.small_imgurl = row["small_imgurl"].ToString();
                }
                if (row["add_time"] != null)
                {
                    model.add_time = DateTime.Parse(row["add_time"].ToString());
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
            strSql.Append("select id,brand_id,theme_id,size,img_url,remark,small_imgurl,add_time ");
			strSql.Append(" FROM dt_brand_attach ");
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
				strSql.Append(" top "+Top.ToString());
			}
            strSql.Append(" id,brand_id,theme_id,size,img_url,remark,small_imgurl,add_time ");
			strSql.Append(" FROM dt_brand_attach ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

        /// <summary>
        /// 获得图片数据列表
        /// </summary>
        public List<Model.brand_attach> GetAttachList(int brand_id)
        {
            List<Model.brand_attach> modelList = new List<Model.brand_attach>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,brand_id,theme_id,size,img_url,remark,small_imgurl,add_time ");
            strSql.Append(" FROM dt_brand_attach ");
            strSql.Append(" where brand_id=" + brand_id);
            DataSet ds = DbHelperSQL.Query(strSql.ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {
                Model.brand_attach model = new Model.brand_attach();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    model= DataRowToModel(ds.Tables[0].Rows[i]);
                    modelList.Add(model);
                }
            }
            return modelList;
        }

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM dt_brand_attach ");
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
			strSql.Append(")AS Row, T.*  from dt_brand_attach T ");
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
			parameters[0].Value = "dt_brand_attach";
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

