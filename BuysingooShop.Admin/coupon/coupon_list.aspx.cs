﻿using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vincent;

namespace BuysingooShop.Admin.coupon
{
    public partial class coupon_list : Web.UI.ManagePage
    {
        protected int totalCount;
        protected int page;
        protected int pageSize;

        protected int status;
        protected int coupon_stype;
        protected string keywords = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.status = Vincent._DTcms.DTRequest.GetQueryInt("status");
            this.coupon_stype = Vincent._DTcms.DTRequest.GetQueryInt("coupon_stype");
            this.keywords = Vincent._DTcms.DTRequest.GetQueryString("keywords");
            
            this.pageSize = GetPageSize(10); //每页数量
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("order_yhq", Vincent._DTcms.DTEnums.ActionEnum.View.ToString()); //检查权限
                RptBind("id>0" + CombSqlTxt(this.status, this.coupon_stype, this.keywords), "status asc,add_time desc,id desc");
            }
        }

        #region 数据绑定=================================
        private void RptBind(string _strWhere, string _orderby)
        {
            this.page = Vincent._DTcms.DTRequest.GetQueryInt("page", 1);
            if (this.status > 0)
            {
                this.ddlStatus.SelectedValue = this.status.ToString();
            }
            if (this.coupon_stype > 0)
            {
                this.ddlPaymentStatus.SelectedValue = this.coupon_stype.ToString();
            }
            txtKeywords.Text = this.keywords;
            BLL.user_coupon bll = new BLL.user_coupon();
            this.rptList.DataSource = bll.GetList(this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
            this.rptList.DataBind();

            //绑定页码
            txtPageNum.Text = this.pageSize.ToString();
            string pageUrl = Vincent._DTcms.Utils.CombUrlTxt("coupon_list.aspx", "status={0}&coupon_stype={1}&page={2}&page={3}",
                this.status.ToString(), this.coupon_stype.ToString(), this.keywords, "__id__");
            PageContent.InnerHtml = Vincent._DTcms.Utils.OutPageList(this.pageSize, this.page, this.totalCount, pageUrl, 8);
        }
        #endregion

        #region 组合SQL查询语句==========================
        protected string CombSqlTxt(int _status, int _payment_status, string _keywords)
        {
            StringBuilder strTemp = new StringBuilder();
            if (_status > 0)
            {
                strTemp.Append(" and status=" + _status);
            }
            if (_payment_status > 0)
            {
                strTemp.Append(" and status=" + _payment_status);
            }
            _keywords = _keywords.Replace("'", "");
            if (!string.IsNullOrEmpty(_keywords))
            {
                strTemp.Append(" and (title like '%" + _keywords + "%' or str_code like '%" + _keywords + "%' or amount like '%" + _keywords + "%')");
            }
            return strTemp.ToString();
        }
        #endregion

        #region 返回用户每页数量=========================
        private int GetPageSize(int _default_size)
        {
            int _pagesize;
            if (int.TryParse(Vincent._DTcms.Utils.GetCookie("coupon_list_page_size"), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    return _pagesize;
                }
            }
            return _default_size;
        }
        #endregion

        #region 返回优惠券状态=============================
        protected string GetCouponStatus(int _id)
        {
            string _title = string.Empty;
            Model.user_coupon model = new BLL.user_coupon().GetModel(_id);
            switch (model.status)
            {
                case 1:
                    _title = "未使用";
                    break;
                case 2: _title = "已使用";
                    break;
                case 3: _title = "已过期";
                    break;
            }

            return _title;
        }
        #endregion

        //关健字查询
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(Vincent._DTcms.Utils.CombUrlTxt("coupon_list.aspx", "status={0}&coupon_stype={1}&keywords={2}",
                this.status.ToString(), this.coupon_stype.ToString(), txtKeywords.Text));
        }

        //优惠券状态
        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(Vincent._DTcms.Utils.CombUrlTxt("coupon_list.aspx", "status={0}&coupon_stype={1}&keywords={2}",
                ddlStatus.SelectedValue, ddlPaymentStatus.SelectedValue, this.keywords));
        }

        //优惠券类型
        protected void ddlPaymentStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(Vincent._DTcms.Utils.CombUrlTxt("coupon_list.aspx", "status={0}&coupon_stype={1}&keywords={2}",
                ddlStatus.SelectedValue, ddlPaymentStatus.SelectedValue, this.keywords));
        }


        //设置分页数量
        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            int _pagesize;
            if (int.TryParse(txtPageNum.Text.Trim(), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    Vincent._DTcms.Utils.WriteCookie("coupon_list_page_size", _pagesize.ToString(), 14400);
                }
            }
            Response.Redirect(Vincent._DTcms.Utils.CombUrlTxt("coupon_list.aspx", "status={0}&coupon_stype={1}&keywords={2}",
                 this.status.ToString(), this.coupon_stype.ToString(), this.keywords));
        }

        //批量删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("coupon_list", Vincent._DTcms.DTEnums.ActionEnum.Delete.ToString()); //检查权限
            int sucCount = 0;
            int errorCount = 0;
            BLL.user_coupon bll = new BLL.user_coupon();
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
            AddAdminLog(Vincent._DTcms.DTEnums.ActionEnum.Delete.ToString(), "删除优惠券成功" + sucCount + "条，失败" + errorCount + "条"); //记录日志
            JscriptMsg("删除成功" + sucCount + "条，失败" + errorCount + "条！", Vincent._DTcms.Utils.CombUrlTxt("coupon_list.aspx", "status={0}&coupon_stype={1}&keywords={2}",
                this.status.ToString(), this.coupon_stype.ToString(), this.keywords), "Success");
        }

    }
}