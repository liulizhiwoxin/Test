using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vincent;

namespace BuysingooShop.Admin.outlet
{
    public partial class openarea_list : Web.UI.ManagePage
    {
        protected int totalCount;
        protected int page;
        protected int pageSize;

        protected int status;
        protected int payment_status;
        protected int express_status;
        protected string keywords = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.status = Vincent._DTcms.DTRequest.GetQueryInt("status");
            this.payment_status = Vincent._DTcms.DTRequest.GetQueryInt("payment_status");
            this.express_status = Vincent._DTcms.DTRequest.GetQueryInt("express_status");
            this.keywords = Vincent._DTcms.DTRequest.GetQueryString("keywords");

            this.pageSize = GetPageSize(10); //每页数量
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("openarea_list", Vincent._DTcms.DTEnums.ActionEnum.View.ToString()); //检查权限
                RptBind(" 1=1 " + CombSqlTxt(this.status, this.payment_status, this.express_status, this.keywords), "id desc");
            }
        }

        #region 数据绑定=================================
        private void RptBind(string _strWhere, string _orderby)
        {
            this.page = Vincent._DTcms.DTRequest.GetQueryInt("page", 1);
            txtKeywords.Text = this.keywords;
            BLL.openarea bll = new BLL.openarea();
            BuysingooShop.Model.manager manModel = Session[Vincent._DTcms.DTKeys.SESSION_ADMIN_INFO] as Model.manager;
            if (manModel.brand_id != 0)
            {
                _strWhere += " and brand_id=" + manModel.brand_id;
            }
            this.rptList.DataSource = bll.GetList(this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
            this.rptList.DataBind();

            //绑定页码
            txtPageNum.Text = this.pageSize.ToString();
            string pageUrl = Vincent._DTcms.Utils.CombUrlTxt("openarea_list.aspx", "status={0}&payment_status={1}&express_status={2}&keywords={3}&page={4}",
                this.status.ToString(), this.payment_status.ToString(), this.express_status.ToString(), this.keywords, "__id__");
            PageContent.InnerHtml = Vincent._DTcms.Utils.OutPageList(this.pageSize, this.page, this.totalCount, pageUrl, 8);
        }
        #endregion

        #region 组合SQL查询语句==========================
        protected string CombSqlTxt(int _status, int _payment_status, int _express_status, string _keywords)
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
                //strTemp.Append(" and id in (select order_id from dt_order_goods where id in(select id from dt_article where brand_id="+model.brand_id+"))");
                //订单表里有品牌id  可直接从订单表查询
                strTemp.Append(" and brand_id=" + model.brand_id);
            }
            else if (roleID == 4)//经销商
            {
                strTemp.Append(" and str_code='" + model.str_code + "'");
            }
            //}
            #endregion
            if (_status > 0)
            {
                if (_status == 5)
                {//取消订单
                    strTemp.Append(" and status=99");
                }
                else if (_status == 6)
                {//作废订单
                    strTemp.Append(" and status=100");
                }
                else if (_status == 4)
                {//完成订单
                    strTemp.Append(" and status=90");
                }
                else if (_status == 2)
                {//待处理
                    strTemp.Append(" and status=2 and payment_status=2 and express_status=1");
                }
                else if (_status == 3)
                {//已确认
                    strTemp.Append(" and status=2 and payment_status=2 and express_status=2");
                }//退货中
                else if (_status == 7)
                {
                    //strTemp.Append(" and refund_status is not null");
                    strTemp.Append(" and status!=90 and status!=100 and refund_status is not null");
                }
                else
                {
                    strTemp.Append(" and status=" + _status);
                }

            }
            if (_payment_status > 0 && _status != 2)
            {
                strTemp.Append(" and payment_status=" + _payment_status);
            }
            if (_express_status > 0 && _status != 3)
            {
                strTemp.Append(" and express_status=" + _express_status);
            }
            _keywords = _keywords.Replace("'", "");
            _keywords = _keywords.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "");
            if (!string.IsNullOrEmpty(_keywords))
            {
                strTemp.Append(" and (t1.order_no like '%" + _keywords.ToString() + "%' or t1.user_name like '%" + _keywords.ToString() + "%' or accept_name like '%" + _keywords.ToString() + "%')");
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

        //关健字查询
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(Vincent._DTcms.Utils.CombUrlTxt("openarea_list.aspx", "status={0}&payment_status={1}&express_status={2}&keywords={3}",
                this.status.ToString(), this.payment_status.ToString(), this.express_status.ToString(), txtKeywords.Text));
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
            Response.Redirect(Vincent._DTcms.Utils.CombUrlTxt("user_list.aspx", "status={0}&payment_status={1}&express_status={2}&keywords={3}",
                this.status.ToString(), this.payment_status.ToString(), this.express_status.ToString(), this.keywords));
        }

        //批量删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("openarea_list", Vincent._DTcms.DTEnums.ActionEnum.Delete.ToString()); //检查权限
            int sucCount = 0;
            int errorCount = 0;
            BLL.openarea bll = new BLL.openarea();
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
            AddAdminLog(Vincent._DTcms.DTEnums.ActionEnum.Delete.ToString(), "删除店铺成功" + sucCount + "条，失败" + errorCount + "条"); //记录日志
            JscriptMsg("删除成功" + sucCount + "条，失败" + errorCount + "条！", Vincent._DTcms.Utils.CombUrlTxt("openarea_list.aspx", "status={0}&payment_status={1}&express_status={2}&keywords={3}",
                this.status.ToString(), this.payment_status.ToString(), this.express_status.ToString(), this.keywords), "Success");
        }

    }
}