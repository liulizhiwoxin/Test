using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vincent;
using System.Data;

namespace BuysingooShop.Admin.finance
{
    public partial class coupon : Web.UI.ManagePage
    {
        protected int totalCount;
        protected int page;
        protected int pageSize;
        protected decimal coupon_amount = 0M;//优惠券抵用统计
        protected string datetime = null;
        protected string keywords = string.Empty;
        protected string keyDate = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.keywords = Vincent._DTcms.DTRequest.GetQueryString("keywords");
            this.keyDate = Vincent._DTcms.DTRequest.GetQueryString("keyDate");

            this.pageSize = GetPageSize(15); //每页数量

            getTotalAmount();//获取订单中金额
            if (!Page.IsPostBack)
            {

                ChkAdminLevel("finance_recharge_day", Vincent._DTcms.DTEnums.ActionEnum.View.ToString()); //检查权限
                RptBind("1=1" + CombSqlTxt(this.keywords, this.keyDate), "use_time desc");
            }
        }

        #region 数据绑定=================================
        private void RptBind(string _strWhere, string _orderby)
        {
            this.page = Vincent._DTcms.DTRequest.GetQueryInt("page", 1);
            ipt_Time.Value = this.keyDate;

            BLL.user_coupon_log bll = new BLL.user_coupon_log();
            this.rptList.DataSource = bll.GetList1(this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
            this.rptList.DataBind();

            //绑定页码
            txtPageNum.Text = this.pageSize.ToString();
            string pageUrl = Vincent._DTcms.Utils.CombUrlTxt("coupon.aspx", "keywords={0}&keyDate={1}&page={2}", this.keywords, this.keyDate, "__id__");
            PageContent.InnerHtml = Vincent._DTcms.Utils.OutPageList(this.pageSize, this.page, this.totalCount, pageUrl, 15);
        }
        #endregion

        #region 组合SQL查询语句==========================
        protected string CombSqlTxt(string _keywords, string _keyDate)
        {
            StringBuilder strTemp = new StringBuilder();
            _keywords = _keywords.Replace("'", "");
            if (!string.IsNullOrEmpty(_keywords))
            {
                strTemp.Append(" and (user_name='" + _keywords + "' or remark like '%" + _keywords + "%')");
            }

            _keyDate = _keyDate.Replace("'", "");
            _keyDate = _keyDate.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "");
            if (!string.IsNullOrEmpty(_keyDate))
            {
                strTemp.Append(" and datediff(dd,use_time,'" + _keyDate.ToString() + "')=0");
            }

            return strTemp.ToString();
        }
        #endregion

        /// <summary>
        /// 获取优惠券使用状态
        /// </summary>
        /// <returns></returns>
        protected string GetcouponStatus(int id)
        {
            string title = "";
            Model.user_coupon_log model = new BLL.user_coupon_log().GetModel(id);
            if (model != null)
            {
                switch (model.status)
                {
                    case 1:
                        title = "未使用";
                        break;
                    case 2:
                        title = "已使用";
                        break;
                    case 3:
                        title = "已过期";
                        break;
                    default:
                        break;
                }
            }
            return title;

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
            coupon_amount = 0M;
            if (this.keyDate == "")
            {
                BLL.user_coupon_log bll = new BLL.user_coupon_log();
                DataTable dt = bll.GetList1(0, " 1=1", " use_time desc").Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    Model.orders model = new BLL.orders().GetModel(int.Parse(row["order_id"].ToString()));
                    if (model != null)
                    {
                        if (decimal.Parse(row["amount"].ToString()) > model.order_amount)
                        {
                            coupon_amount += model.order_amount;
                        }
                        else
                        {
                            coupon_amount += decimal.Parse(row["amount"].ToString());
                        }
                    }

                }
            }
            else
            {
                BLL.user_coupon_log bll = new BLL.user_coupon_log();
                DataTable dt = bll.GetList1(0, " 1=1 and datediff(dd,use_time,'" + this.keyDate + "')=0", " use_time desc").Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    if (int.Parse(row["status"].ToString()) == 2)
                    {
                        Model.orders model = new BLL.orders().GetModel(int.Parse(row["order_id"].ToString()));
                        if (model != null)
                        {
                            if (decimal.Parse(row["amount"].ToString())>model.order_amount)
                            {
                                coupon_amount += model.order_amount;
                            }
                            else
                            {
                                coupon_amount += decimal.Parse(row["amount"].ToString());
                            }
                        }

                    }
                }
            }
        }

        //关健字查询
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            var date = ipt_Time.Value;
            //datetime = date;
            //RptBind("1=1 and datediff(dd,use_time,'" + date + "')=0", "use_time desc");
            Response.Redirect(Vincent._DTcms.Utils.CombUrlTxt("coupon.aspx", "keywords={0}&keyDate={1}", this.keywords, ipt_Time.Value));

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