using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vincent;
using System.Data;
using System.IO;
using Vincent.Excel;

namespace BuysingooShop.Admin.salesreport
{
    public partial class areareport : Web.UI.ManagePage
    {
        protected int totalCount;
        protected int page;
        protected int pageSize;
        protected decimal total_amount = 0M;//订单总金额统计
        protected decimal coupon_amount = 0M;//优惠券使用统计
        protected decimal refund_amount = 0M;//退款统计
        protected string datetime = null;
        protected int outlet_id;//店铺id
        protected string keywords = string.Empty;
        protected int city_id;//城市id

        protected void Page_Load(object sender, EventArgs e)
        {
            this.pageSize = GetPageSize(15); //每页数量


            this.outlet_id = Vincent._DTcms.DTRequest.GetQueryInt("outlet_id");
            this.city_id = Vincent._DTcms.DTRequest.GetQueryInt("city_id");
            getTotalAmount();//获取订单中金额
            if (!Page.IsPostBack)
            {

                ChkAdminLevel("finance_recharge_day", Vincent._DTcms.DTEnums.ActionEnum.View.ToString()); //检查权限
                BrandBind(ddlGroupId, 3);//店铺
                CityBind(DropDownList1);//城市
                RptBind("status!=1 and status!=99" + CombSqlTxt(this.outlet_id, this.city_id, this.keywords), "add_time desc,id desc");

            }
        }

        #region 数据绑定=================================
        private void RptBind(string _strWhere, string _orderby)
        {
            this.page = Vincent._DTcms.DTRequest.GetQueryInt("page", 1);

            if (this.outlet_id > 0)
            {
                this.ddlGroupId.SelectedValue = this.outlet_id.ToString();
            }
            if (this.city_id > 0)
            {
                this.DropDownList1.SelectedValue = this.city_id.ToString();
            }

            BLL.orders bll = new BLL.orders();
            this.rptList.DataSource = bll.GetList1(this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
            this.rptList.DataBind();

            //绑定页码
            txtPageNum.Text = this.pageSize.ToString();
            string pageUrl = Vincent._DTcms.Utils.CombUrlTxt("areareport.aspx", "keywords={0}&page={1}", "", "__id__");
            PageContent.InnerHtml = Vincent._DTcms.Utils.OutPageList(this.pageSize, this.page, this.totalCount, pageUrl, 15);
        }
        #endregion

        #region 组合SQL查询语句==========================
        protected string CombSqlTxt(int _outlet_id, int _city_id, string _keywords)
        {
            StringBuilder strTemp = new StringBuilder();

            if (_city_id > 0)
            {
                BLL.openarea open = new BLL.openarea();
                Model.openarea openm = open.GetModel(_city_id);
                if (openm != null)
                {
                    BLL.outlet outlet = new BLL.outlet();
                    DataTable dt = outlet.GetList(0, " city='" + openm.city + "'", " id").Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        StringBuilder st = new StringBuilder();
                        string ss = string.Empty;
                        st.Append("(");
                        foreach (DataRow item in dt.Rows)
                        {
                            st.Append(item["id"] + ",");
                        }
                        if (st.ToString().Trim() != "(")
                        {
                            ss = DelLastComma(st.ToString().Trim());
                            st.Append(")");

                        }
                        strTemp.Append(" and store_id in" + ss + ")");
                    }
                    else
                    {
                        strTemp.Append(" and store_id in(null)");
                    }
                }
            }

            if (_outlet_id > 0)
            {
                strTemp.Append(" and store_id=" + _outlet_id);
            }

            _keywords = _keywords.Replace("'", "");
            if (!string.IsNullOrEmpty(_keywords))
            {
                strTemp.Append(" and (user_name='" + _keywords + "' or remark like '%" + _keywords + "%')");
            }

            return strTemp.ToString();
        }
        #endregion

        /// <summary>
        /// 删除最后结尾的一个逗号
        /// 
        /// </summary>
        public static string DelLastComma(string str)
        {
            if (str.Length < 1)
                return "";
            return str.Substring(0, str.Length - 1);
        }

        #region 店铺=================================
        private void BrandBind(DropDownList ddl, int role_id)
        {
            BLL.outlet bll = new BLL.outlet();
            DataTable dt = bll.GetList(0, "0=0", "addtime desc,id asc").Tables[0];

            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("请选择店铺...", ""));
            foreach (DataRow dr in dt.Rows)
            {
                ddl.Items.Add(new ListItem(dr["name"].ToString(), dr["id"].ToString()));
            }
        }
        #endregion

        #region 城市=================================
        private void CityBind(DropDownList ddl)
        {
            BLL.openarea bll = new BLL.openarea();
            DataTable dt = bll.GetList(0, "1=1", "id asc").Tables[0];

            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("请选择城市...", ""));
            foreach (DataRow dr in dt.Rows)
            {
                ddl.Items.Add(new ListItem(dr["city"].ToString(), dr["id"].ToString()));
            }
        }
        #endregion


        //导出excel
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            StringBuilder OutFileContent = new StringBuilder();//容器
            //写头文件
            OutFileContent = Exportexcel.AddHeadFile(OutFileContent);
            //写内容
            StringBuilder sbMsg = new StringBuilder();//容器


            //查询需要导出的数据
            string _strWhere = "status!=1 and status!=99";
            _strWhere += CombSqlTxt(this.outlet_id, this.city_id, this.keywords);

            this.datetime = Vincent._DTcms.DTRequest.GetQueryString("datetime");
            //if (datetime != null)
            //{
            //    _strWhere += " and datediff(dd,add_time,'" + datetime + "')=0";
            //}

            BLL.orders bll = new BLL.orders();
            BuysingooShop.Model.manager manModel = Session[Vincent._DTcms.DTKeys.SESSION_ADMIN_INFO] as Model.manager;
            if (manModel.brand_id != 0)
            {
                _strWhere += " and store_id=" + manModel.brand_id;
            }
            DataSet ds = bll.GetOrderAmount(0, _strWhere, " id");



            OutFileContent.Append(Exportexcel.AddContentFile(sbMsg, ds));
            //写尾文件
            OutFileContent = Exportexcel.AddEndFile(OutFileContent);
            //保存到xls
            string strRandomFileName = "简单生活订单报表";
            //strRandomFileName = strRandomFileName + DateTime.Now.ToString("yyyy-MM-dd");
            //strRandomFileName=strRandomFileName+"-"+DateTime.Now.Minute.ToString();
            //strRandomFileName = strRandomFileName+"-" + DateTime.Now.Second.ToString();

            string strPath = Server.MapPath(Context.Request.ApplicationPath);
            string strExcelFile = strPath + strRandomFileName + ".xls";


            try
            {
                //生成excel
                FileStream OutFile = new FileStream(strExcelFile, FileMode.Create, FileAccess.Write);
                byte[] btArray = new byte[OutFileContent.Length];
                btArray = Encoding.UTF8.GetBytes(OutFileContent.ToString());
                OutFile.Write(btArray, 0, btArray.Length);
                OutFile.Flush();
                OutFile.Close();


                //下载文件
                FileStream fs = new FileStream(strExcelFile, FileMode.Open);
                byte[] bytes = new byte[(int)fs.Length];
                fs.Read(bytes, 0, bytes.Length);
                fs.Close();
                Response.ContentType = "application/ms-word";
                //通知浏览器下载文件而不是打开
                Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(strExcelFile, System.Text.Encoding.UTF8));
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                JscriptMsg("请稍后再试！" + ex.Message, Vincent._DTcms.Utils.CombUrlTxt("areareport.aspx", "keywords={0}", this.keywords), "Success");
            }



            JscriptMsg("导出成功！", Vincent._DTcms.Utils.CombUrlTxt("areareport.aspx", "keywords={0}", this.keywords), "Success");
        }


        #region 返回每页数量=============================
        private int GetPageSize(int _default_size)
        {
            int _pagesize;
            if (int.TryParse(Vincent._DTcms.Utils.GetCookie("dt_orders_page_size"), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    return _pagesize;
                }
            }
            return _default_size;
        }
        #endregion


        /// <summary>
        /// 获取订单总金额
        /// </summary>
        /// <param name="total_amount"></param>
        protected void getTotalAmount()
        {
            total_amount = 0M;
            coupon_amount = 0M;
            refund_amount = 0M;
            if (datetime == null)
            {
                BLL.orders bll = new BLL.orders();
                DataTable dt;
                if (this.outlet_id > 0)
                {
                    dt = bll.GetOrderAmount(0, " status!=1 and status!=99 and store_id=" + this.outlet_id, " add_time desc,id desc").Tables[0];
                }
                else if (this.city_id > 0)
                {
                    BLL.openarea open = new BLL.openarea();
                    Model.openarea openm = open.GetModel(this.city_id);
                    StringBuilder strTemp = new StringBuilder();
                    if (openm != null)
                    {
                        BLL.outlet outlet = new BLL.outlet();
                        DataTable dts = outlet.GetList(0, " city='" + openm.city + "'", " id").Tables[0];
                        if (dts.Rows.Count > 0)
                        {
                            StringBuilder st = new StringBuilder();
                            string ss = string.Empty;
                            st.Append("(");
                            foreach (DataRow item in dts.Rows)
                            {
                                st.Append(item["id"] + ",");
                            }
                            if (st.ToString().Trim() != "(")
                            {
                                ss = DelLastComma(st.ToString().Trim());
                                st.Append(")");

                            }
                            strTemp.Append(" and store_id in" + ss + ")");
                        }
                        else
                        {
                            strTemp.Append(" and store_id in(null)");
                        }
                    }
                    dt = bll.GetOrderAmount(0, " status!=1 and status!=99"+strTemp, " add_time desc,id desc").Tables[0];
                }
                else
                {
                    dt = bll.GetOrderAmount(0, " status!=1 and status!=99", " add_time desc,id desc").Tables[0];
                }
                foreach (DataRow row in dt.Rows)
                {
                    total_amount += decimal.Parse(row["order_amount"].ToString());
                    if (row["refund_status"].ToString() != "" && int.Parse(row["refund_status"].ToString()) == 3) //统计退款
                    {
                        refund_amount += decimal.Parse(row["order_amount"].ToString());
                    }
                    BLL.user_coupon_log couponbll = new BLL.user_coupon_log();//统计优惠券
                    if (row["str_code"].ToString() != "")
                    {
                        Model.user_coupon_log couponmodel = couponbll.GetModel(row["str_code"].ToString());
                        if (couponmodel != null && couponmodel.status == 2)
                        {
                            BLL.user_coupon copbl = new BLL.user_coupon();
                            Model.user_coupon copmo = copbl.GetModel(couponmodel.coupon_id);
                            if (copmo != null)
                            {
                                if (copmo.amount > decimal.Parse(row["order_amount"].ToString()))
                                {
                                    coupon_amount += decimal.Parse(row["order_amount"].ToString());
                                }
                                else
                                {
                                    coupon_amount += copmo.amount;
                                }
                            }
                        }
                    }

                }
            }
            else
            {
                BLL.orders bll = new BLL.orders();

                DataTable dt;
                if (this.outlet_id > 0)
                {
                    dt = bll.GetOrderAmount(0, " status!=1 and status!=99 and store_id=" + this.outlet_id, " add_time desc,id desc").Tables[0];
                }
                else
                {
                    dt = bll.GetOrderAmount(0, " status!=1 and status!=99", " add_time desc,id desc").Tables[0];
                }

                //DataTable dt = bll.GetOrderAmount(0, " status!=1 and status!=99 and datediff(dd,add_time,'" + datetime + "')=0", " add_time desc,id desc").Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    total_amount += decimal.Parse(row["order_amount"].ToString());
                    if (row["refund_status"].ToString() != "" && int.Parse(row["refund_status"].ToString()) == 3)
                    {
                        refund_amount += decimal.Parse(row["order_amount"].ToString());
                    }
                    BLL.user_coupon_log couponbll = new BLL.user_coupon_log();//统计优惠券
                    if (row["str_code"].ToString() != "")
                    {
                        Model.user_coupon_log couponmodel = couponbll.GetModel(row["str_code"].ToString());
                        if (couponmodel != null && couponmodel.status == 2)
                        {
                            BLL.user_coupon copbl = new BLL.user_coupon();
                            Model.user_coupon copmo = copbl.GetModel(couponmodel.coupon_id);
                            if (copmo != null)
                            {
                                if (copmo.amount > decimal.Parse(row["order_amount"].ToString()))
                                {
                                    coupon_amount += decimal.Parse(row["order_amount"].ToString());
                                }
                                else
                                {
                                    coupon_amount += copmo.amount;
                                }
                            }
                        }
                    }
                }
            }
        }

        //筛选店铺
        protected void ddlGroupId_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(Vincent._DTcms.Utils.CombUrlTxt("areareport.aspx", "outlet_id={0}",
                ddlGroupId.SelectedValue));
        }

        //筛选城市
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(Vincent._DTcms.Utils.CombUrlTxt("areareport.aspx", "city_id={0}",
                 DropDownList1.SelectedValue));
        }

        //关健字查询
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            var date = ipt_Time.Value;
            datetime = date;
            RptBind("status!=1 and status!=99 and datediff(dd,add_time,'" + date + "')=0", "add_time desc,id desc");

            getTotalAmount();
        }

        //设置分页数量
        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            int _pagesize;
            if (int.TryParse(txtPageNum.Text.Trim(), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    Vincent._DTcms.Utils.WriteCookie("user_point_log_page_size", _pagesize.ToString(), 14400);
                }
            }
            //Response.Redirect(Vincent._DTcms.Utils.CombUrlTxt("recharge_day_list.aspx", "keywords={0}", this.keywords));
        }
    }
}