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
    public partial class refund : Web.UI.ManagePage
    {
        protected int totalCount;
        protected int page;
        protected int pageSize;
        protected decimal refund_amount = 0M;//退款统计
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
                RptBind("pointtype=3" + CombSqlTxt(this.keywords, this.keyDate), "add_time desc,id desc");
            }
        }

        #region 数据绑定=================================
        private void RptBind(string _strWhere, string _orderby)
        {
            this.page = Vincent._DTcms.DTRequest.GetQueryInt("page", 1);
            ipt_Time.Value = this.keyDate;

            BLL.user_point_log bll = new BLL.user_point_log();
            this.rptList.DataSource = bll.GetList(this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
            this.rptList.DataBind();

            //绑定页码
            txtPageNum.Text = this.pageSize.ToString();
            string pageUrl = Vincent._DTcms.Utils.CombUrlTxt("refund.aspx", "keywords={0}&keyDate={1}&page={2}", this.keywords, this.keyDate, "__id__");
            PageContent.InnerHtml = Vincent._DTcms.Utils.OutPageList(this.pageSize, this.page, this.totalCount, pageUrl, 15);
        }
        #endregion

        #region 组合SQL查询语句==========================
        protected string CombSqlTxt(string _keywords,string _keyDate)
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
                strTemp.Append(" and datediff(dd,add_time,'" + _keyDate.ToString() + "')=0");
            }

            return strTemp.ToString();
        }
        #endregion

        /// <summary>
        /// 获取退款状态
        /// </summary>
        /// <returns></returns>
        protected string GetrefundStatus(int id) {
            string title = "";
            Model.user_point_log model = new BLL.user_point_log().GetModel(id);
            if (model != null)
            {
                switch (model.order_status)
                {
                    case 1:
                        title = "申请退款";
                        break;
                    case 2:
                        title = "同意退款";
                        break;
                    case 3:
                        title = "驳回退款";
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
            refund_amount = 0M;
            if (this.keyDate == "")
            {
                BLL.user_point_log bll = new BLL.user_point_log();
                DataTable dt = bll.GetList(0, " pointtype=3", " add_time desc,id desc").Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    if (int.Parse(row["order_status"].ToString()) == 2) //统计退款
                    {
                        refund_amount += decimal.Parse(row["amount"].ToString());
                    }

                }
            }
            else
            {
                BLL.user_point_log bll = new BLL.user_point_log();
                DataTable dt = bll.GetList(0, " pointtype=3 and datediff(dd,add_time,'" + this.keyDate + "')=0", " add_time desc,id desc").Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    if (int.Parse(row["order_status"].ToString()) == 2)
                    {
                        refund_amount += decimal.Parse(row["amount"].ToString());
                    }
                }
            }
        }

        //关健字查询
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            var date = ipt_Time.Value;
            //datetime = date;
            //RptBind("pointtype=3 and datediff(dd,add_time,'" + date + "')=0", "add_time desc,id desc");
            Response.Redirect(Vincent._DTcms.Utils.CombUrlTxt("refund.aspx", "keywords={0}&keyDate={1}", this.keywords, ipt_Time.Value));

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