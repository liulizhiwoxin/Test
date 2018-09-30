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
    public partial class order_receive : Web.UI.ManagePage
    {
        protected int totalCount;
        protected int page;
        protected int pageSize;

        protected string keywords = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.keywords = Vincent._DTcms.DTRequest.GetQueryString("keywords");

            this.pageSize = GetPageSize(10); //每页数量
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("order_list", Vincent._DTcms.DTEnums.ActionEnum.View.ToString()); //检查权限
                RptBind("status>=4 and express_status=2 " + CombSqlTxt(this.keywords), "add_time desc,id desc");
            }
        }

        #region 数据绑定=================================
        private void RptBind(string _strWhere, string _orderby)
        {
            this.page = Vincent._DTcms.DTRequest.GetQueryInt("page", 1);
            txtKeywords.Text = this.keywords;
            BLL.orders bll = new BLL.orders();
            BuysingooShop.Model.manager manModel = Session[Vincent._DTcms.DTKeys.SESSION_ADMIN_INFO] as Model.manager;
            if (manModel.brand_id!=0 )
            {
                _strWhere += " and brand_id=" + manModel.brand_id;
            }
            this.rptList.DataSource = bll.GetList(this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
            this.rptList.DataBind();

            //绑定页码
            txtPageNum.Text = this.pageSize.ToString();
            string pageUrl = Vincent._DTcms.Utils.CombUrlTxt("order_confirm.aspx", "keywords={0}&page={1}", this.keywords, "__id__");
            PageContent.InnerHtml = Vincent._DTcms.Utils.OutPageList(this.pageSize, this.page, this.totalCount, pageUrl, 8);
        }
        #endregion

        #region 组合SQL查询语句==========================
        protected string CombSqlTxt(string _keywords)
        {
            StringBuilder strTemp = new StringBuilder();
            #region 角色判断
            Model.manager model = GetAdminInfo();
            int roleType = model.role_type;
            int roleID = model.role_id;
            //if (roleType == 2)
            //{
            if (roleID == 3)//酒厂
            {
                strTemp.Append(" and brand_id=" + model.brand_id);
            }
            else if (roleID == 4)//经销商
            {
                strTemp.Append(" and str_code='" + model.str_code + "'");
            }
            //}
            #endregion
            _keywords = _keywords.Replace("'", "");
            if (!string.IsNullOrEmpty(_keywords))
            {
                strTemp.Append(" and (order_no like '%" + _keywords + "%' or user_name like '%" + _keywords + "%' or accept_name like '%" + _keywords + "%')");
            }
            return strTemp.ToString();
        }
        #endregion

        #region 返回用户每页数量=========================
        private int GetPageSize(int _default_size)
        {
            int _pagesize;
            if (int.TryParse(Vincent._DTcms.Utils.GetCookie("order_confirm_page_size"), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    return _pagesize;
                }
            }
            return _default_size;
        }
        #endregion

        #region 返回订单状态=============================
        protected string GetOrderStatus(int _id)
        {
            string _title = string.Empty;
            Model.orders model = new BLL.orders().GetModel(_id);
            switch (model.status)
            {
                case 1: //如果是线下支付，支付状态为0，如果是线上支付，支付成功后会自动改变订单状态为已确认
                    if (model.payment_status == 1)
                    {
                        _title = "待付款";
                    }
                    else
                    {
                        _title = "已付款";
                    }
                    break;
                case 2://平台确认
                    _title = "待平台确认";
                    break;
                case 3://酒厂确认
                    _title = "待酒厂确认";
                    break;
                case 4://酒厂生产完成
                    if (model.express_status == 1)
                    {
                        _title = "待发货";
                    }
                    else
                    {
                        _title = "已发货";
                    }
                    break;
                case 90:
                    _title = "交易完成";
                    break;
                case 99:
                    _title = "已取消";
                    break;
                case 100:
                    _title = "已作废";
                    break;
            }

            return _title;
        }
        #endregion

        //关健字查询
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(Vincent._DTcms.Utils.CombUrlTxt("order_confirm.aspx", "keywords={0}", txtKeywords.Text));
        }

        //设置分页数量
        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            int _pagesize;
            if (int.TryParse(txtPageNum.Text.Trim(), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    Vincent._DTcms.Utils.WriteCookie("order_confirm_page_size", _pagesize.ToString(), 14400);
                }
            }
            Response.Redirect(Vincent._DTcms.Utils.CombUrlTxt("order_confirm.aspx", "keywords={0}", this.keywords));
        }

        //确认订单
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("order_list", Vincent._DTcms.DTEnums.ActionEnum.Confirm.ToString()); //检查权限
            int sucCount = 0;
            int errorCount = 0;
            BLL.orders bll = new BLL.orders();
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    Model.orders model = bll.GetModel(id);
                    if (model != null)
                    {
                        //检查订单是否使用在线支付方式
                        //if (model.payment_status > 0)
                        //{
                            //在线支付方式
                            //model.payment_status = 2; //标志已付款
                            //model.payment_time = DateTime.Now; //支付时间
                            //model.status = 3; // 订单为确认状态
                            //model.confirm_time = DateTime.Now; //确认时间
                        //}
                        //else
                        //{
                            //线下支付方式
                        //    model.status = 3; // 订单为确认状态
                        //    model.confirm_time = DateTime.Now; //确认时间
                        //}
                        model.status = 2; // 平台确认订单
                        if (bll.Update(model))
                        {
                            sucCount++;
                        }
                        else
                        {
                            errorCount++;
                        }
                    }
                    else
                    {
                        errorCount++;
                    }
                }
            }
            AddAdminLog(Vincent._DTcms.DTEnums.ActionEnum.Confirm.ToString(), "确认订单成功" + sucCount + "条，失败" + errorCount + "条"); //记录日志
            JscriptMsg("确认成功" + sucCount + "条，失败" + errorCount + "条！", Vincent._DTcms.Utils.CombUrlTxt("order_confirm.aspx", "keywords={0}", this.keywords), "Success");
        }
    }
}