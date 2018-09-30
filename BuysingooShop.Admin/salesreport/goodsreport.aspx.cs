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
using Vincent._DTcms;
using Vincent.Excel;

namespace BuysingooShop.Admin.salesreport
{
    public partial class goodsreport : Web.UI.ManagePage
    {
        protected int totalCount;
        protected int page;
        protected int pageSize;
        protected decimal total_amount = 0M;//订单总金额统计
        protected decimal coupon_amount = 0M;//优惠券使用统计
        protected decimal refund_amount = 0M;//退款统计
        protected string date = null;
        protected string keywords = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.date = Vincent._DTcms.DTRequest.GetQueryString("date");

            this.pageSize = GetPageSize(15); //每页数量

            getTotalAmount();//获取订单中金额
            if (!Page.IsPostBack)
            {

                ChkAdminLevel("finance_recharge_day", Vincent._DTcms.DTEnums.ActionEnum.View.ToString()); //检查权限
                RptBind("status!=1 and status!=99"+CombSqlTxt(this.date,this.keywords), "add_time desc,id desc");
            }
        }

        #region 数据绑定=================================
        private void RptBind(string _strWhere, string _orderby)
        {
            this.page = Vincent._DTcms.DTRequest.GetQueryInt("page", 1);

            if (date != null && date != "")
            {
                ipt_Time.Value = date;
            }
            
            BLL.orders bll = new BLL.orders();
            this.rptList.DataSource = bll.GetList1(this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
            this.rptList.DataBind();

            //绑定页码
            txtPageNum.Text = this.pageSize.ToString();
            string pageUrl = Vincent._DTcms.Utils.CombUrlTxt("goodsreport.aspx", "date={0}&keywords={1}&page={2}", this.date,this.keywords, "__id__");
            PageContent.InnerHtml = Vincent._DTcms.Utils.OutPageList(this.pageSize, this.page, this.totalCount, pageUrl, 15);
        }
        #endregion

        #region 组合SQL查询语句==========================
        protected string CombSqlTxt(string dates, string _keywords)
        {
            StringBuilder strTemp = new StringBuilder();

            if (date != null && date != "")
            {
                strTemp.Append(" and datediff(dd,add_time,'" + date + "')=0");
            }

            _keywords = _keywords.Replace("'", "");
            if (!string.IsNullOrEmpty(_keywords))
            {
                strTemp.Append(" and (user_name='" + _keywords + "' or remark like '%" + _keywords + "%')");
            }

            return strTemp.ToString();
        }
        #endregion

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
            if (date == null || date=="")
            {
                BLL.orders bll = new BLL.orders();
                DataTable dt = bll.GetOrderAmount(0, " status!=1 and status!=99", " add_time desc,id desc").Tables[0];
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
                DataTable dt = bll.GetOrderAmount(0, " status!=1 and status!=99 and datediff(dd,add_time,'" + date + "')=0", " add_time desc,id desc").Tables[0];
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
            //_strWhere += CombSqlTxt(this.status, this.payment_status, this.express_status, this.keywords);

            this.date = Vincent._DTcms.DTRequest.GetQueryString("date");
            if (date != null && date!="")
            {
                _strWhere += " and datediff(dd,add_time,'" + date + "')=0";
            }

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
                JscriptMsg("请稍后再试！" + ex.Message, Vincent._DTcms.Utils.CombUrlTxt("order_list.aspx", "keywords={0}", this.keywords), "Success");
            }



            JscriptMsg("导出成功！", Vincent._DTcms.Utils.CombUrlTxt("order_list.aspx", "keywords={0}",this.keywords), "Success");
        }

        //关健字查询
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            var dates = ipt_Time.Value;
            //RptBind("status!=1 and status!=99 and datediff(dd,add_time,'" + date + "')=0", "add_time desc,id desc");
            Response.Redirect(Vincent._DTcms.Utils.CombUrlTxt("goodsreport.aspx", "date={0}", ipt_Time.Value));

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