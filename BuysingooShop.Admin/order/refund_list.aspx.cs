using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vincent;

namespace BuysingooShop.Admin.order
{
    public partial class refund_list : Web.UI.ManagePage
    {
        protected int totalCount;
        protected int page;
        protected int pageSize;

        protected int refund_status;
        protected int refund_type;
        protected string keywords = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.refund_status = Vincent._DTcms.DTRequest.GetQueryInt("refund_status");
            this.refund_type = Vincent._DTcms.DTRequest.GetQueryInt("refund_type");
            this.keywords = Vincent._DTcms.DTRequest.GetQueryString("keywords");

            this.pageSize = GetPageSize(10); //每页数量
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("refund_list", Vincent._DTcms.DTEnums.ActionEnum.View.ToString()); //检查权限
                RptBind("id>0" + CombSqlTxt(this.refund_status, this.refund_type, this.keywords), "apply_time desc,id desc");
            }
        }

        #region 数据绑定=================================
        private void RptBind(string _strWhere, string _orderby)
        {
            this.page = Vincent._DTcms.DTRequest.GetQueryInt("page", 1);
            if (this.refund_status > 0)
            {
                this.ddlrefund_status.SelectedValue = this.refund_status.ToString();
            }
            txtKeywords.Text = this.keywords;
            BLL.refund bll = new BLL.refund();
            this.rptList.DataSource = bll.GetList(this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
            this.rptList.DataBind();

            //绑定页码
            txtPageNum.Text = this.pageSize.ToString();
            string pageUrl = Vincent._DTcms.Utils.CombUrlTxt("refund_list.aspx", "refund_status={0}&refund_type={1}&keywords={2}&page={3}",
                this.refund_status.ToString(), this.refund_type.ToString(), this.keywords, "__id__");
            PageContent.InnerHtml = Vincent._DTcms.Utils.OutPageList(this.pageSize, this.page, this.totalCount, pageUrl, 8);
        }
        #endregion

        #region 组合SQL查询语句==========================
        protected string CombSqlTxt(int _refund_status, int _refund_type, string _keywords)
        {
            StringBuilder strTemp = new StringBuilder();
            if (_refund_status > 0)
            {
                strTemp.Append(" and refund_status=" + _refund_status);
            }
            if (_refund_type > 0)
            {
                strTemp.Append(" and refund_type=" + _refund_type);
            }
            _keywords = _keywords.Replace("'", "");
            if (!string.IsNullOrEmpty(_keywords))
            {
                strTemp.Append(" and (order_no like '%" + _keywords + "%' or user_name like '%" + _keywords + "%' or refund_reason like '%" + _keywords + "%')");
            }
            return strTemp.ToString();
        }
        #endregion

        #region 返回用户每页数量=========================
        private int GetPageSize(int _default_size)
        {
            int _pagesize;
            if (int.TryParse(Vincent._DTcms.Utils.GetCookie("order_list_page_size"), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    return _pagesize;
                }
            }
            return _default_size;
        }
        #endregion

        #region 返回退款状态=============================
        protected string GetRefund_status(int _refund_status)
        {
            string _title = string.Empty;
            switch (_refund_status)
            {
                case 1: //1生成退款单,2确认退款单,3完成退款单,4取消退款单,5驳回退款单
                    _title = "已提交";
                    break;
                case 2: 
                    _title = "已确认";
                    break;
                case 3:
                    _title = "已完成";
                    break;
                case 4:
                    _title = "已取消";
                    break;
                case 5:
                    _title = "已驳回";
                    break;
            }

            return _title;
        }
        #endregion


        #region 返回订单状态=============================
        protected string GetOrderStatus(int _id)
        {
            string _title = string.Empty;
            //Model.orders model = new BLL.orders().GetModel(_id);
            DataTable dt = new BLL.orders().GetOrderList(0, " t2.id=" + _id, " id desc").Tables[0];

            var refund_status = 0;
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["refund_status"].ToString() != "" && dt.Rows[0]["refund_status"].ToString() != null)
                {
                    refund_status = int.Parse(dt.Rows[0]["refund_status"].ToString());
                }

                switch (int.Parse(dt.Rows[0]["status"].ToString()))
                {

                    case 2://平台确认
                        if (int.Parse(dt.Rows[0]["payment_status"].ToString()) == 2 && int.Parse(dt.Rows[0]["express_status"].ToString()) == 1 && refund_status == 1)
                        {
                            _title = "同意退款";
                        }
                        if (int.Parse(dt.Rows[0]["payment_status"].ToString()) == 2 && int.Parse(dt.Rows[0]["express_status"].ToString()) == 1 && refund_status == 2)
                        {
                            _title = "同意退款";
                        }
                        if (int.Parse(dt.Rows[0]["payment_status"].ToString()) == 2 && int.Parse(dt.Rows[0]["express_status"].ToString()) == 1 && refund_status == 3)
                        {
                            _title = "同意退款";
                        }
                        if (int.Parse(dt.Rows[0]["payment_status"].ToString()) == 2 && int.Parse(dt.Rows[0]["express_status"].ToString()) == 1 && refund_status == 4)
                        {
                            _title = "同意退款";
                        }
                        if (int.Parse(dt.Rows[0]["payment_status"].ToString()) == 2 && int.Parse(dt.Rows[0]["express_status"].ToString()) == 1 && refund_status == 5)
                        {
                            _title = "同意退款";
                        }
                        if (int.Parse(dt.Rows[0]["payment_status"].ToString()) == 2 && int.Parse(dt.Rows[0]["express_status"].ToString()) == 2 && refund_status == 1)
                        {
                            _title = "同意退货";
                        }
                        if (int.Parse(dt.Rows[0]["payment_status"].ToString()) == 2 && int.Parse(dt.Rows[0]["express_status"].ToString()) == 2 && refund_status == 2)
                        {
                            _title = "同意退款";
                        }
                        if (int.Parse(dt.Rows[0]["payment_status"].ToString()) == 2 && int.Parse(dt.Rows[0]["express_status"].ToString()) == 2 && refund_status == 3)
                        {
                            _title = "同意退款";
                        }
                        if (int.Parse(dt.Rows[0]["payment_status"].ToString()) == 2 && int.Parse(dt.Rows[0]["express_status"].ToString()) == 2 && refund_status == 4)
                        {
                            _title = "同意退货";
                        }
                        if (int.Parse(dt.Rows[0]["payment_status"].ToString()) == 2 && int.Parse(dt.Rows[0]["express_status"].ToString()) == 2 && refund_status == 5)
                        {
                            _title = "同意退货";
                        }
                        break;
                    case 3://酒厂确认
                        _title = "同意退款";
                        break;
                    case 4://酒厂生产完成
                        if (int.Parse(dt.Rows[0]["express_status"].ToString()) == 1)
                        {
                            _title = "同意退款";
                        }
                        else
                        {
                            _title = "同意退款";
                        }
                        break;
                    case 90:
                        _title = "同意退款";
                        break;
                    case 99:
                        _title = "同意退款";
                        break;
                    case 100:
                        _title = "同意退款";
                        break;
                }

                return _title;
            }
            return _title;
        }
        #endregion


        //获取联系方式
        protected string GetSingleValue(string order_no)
        {
            BLL.orders bll = new BLL.orders();
            Model.orders model = bll.GetModel(order_no);
            return model == null ? "" : model.mobile;
        }

        //退款金额
        protected decimal GetPayAmount(string order_no)
        {
            Model.orders modelOrders = new BuysingooShop.BLL.orders().GetModel(order_no);
            //return modelOrders == null ? 0 : modelOrders.payable_amount;
            return modelOrders == null ? 0 : modelOrders.real_amount;
        }

        //关健字查询
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(Vincent._DTcms.Utils.CombUrlTxt("refund_list.aspx", "refund_status={0}&refund_type={1}&keywords={2}",
                this.refund_status.ToString(), this.refund_type.ToString(), txtKeywords.Text));
        }
        
        //退款状态
        protected void ddlrefundstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(Vincent._DTcms.Utils.CombUrlTxt("refund_list.aspx", "refund_status={0}&refund_type={1}&keywords={2}",
                ddlrefund_status.SelectedValue, this.refund_type.ToString(), this.keywords));
        }

        //设置分页数量
        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            int _pagesize;
            if (int.TryParse(txtPageNum.Text.Trim(), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    Vincent._DTcms.Utils.WriteCookie("order_list_page_size", _pagesize.ToString(), 14400);
                }
            }
            Response.Redirect(Vincent._DTcms.Utils.CombUrlTxt("user_list.aspx", "refund_status={0}&refund_type={1}&refund_status={2}&keywords={3}",
                this.refund_status.ToString(), this.refund_type.ToString(), this.refund_status.ToString(), this.keywords));
        }

        //批量删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("refund_list", Vincent._DTcms.DTEnums.ActionEnum.Delete.ToString()); //检查权限
            int sucCount = 0;
            int errorCount = 0;
            BLL.orders bll = new BLL.orders();
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    if (bll.Delete(id))
                    {
                        sucCount += 1;
                    }
                    else
                    {
                        errorCount += 1;
                    }
                }
            }
            AddAdminLog(Vincent._DTcms.DTEnums.ActionEnum.Delete.ToString(), "删除订单成功" + sucCount + "条，失败" + errorCount + "条"); //记录日志
            JscriptMsg("删除成功" + sucCount + "条，失败" + errorCount + "条！", Vincent._DTcms.Utils.CombUrlTxt("refund_list.aspx", "refund_status={0}&refund_type={1}&refund_status={2}&keywords={3}",
                this.refund_status.ToString(), this.refund_type.ToString(), this.refund_status.ToString(), this.keywords), "Success");
        }


       
    }
}