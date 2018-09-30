using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vincent;

namespace BuysingooShop.Admin.source
{
    public partial class source_bottle_list : Web.UI.ManagePage
    {
        protected int channel_id;
        protected int totalCount;
        protected int page;
        protected int pageSize;

        protected int type = 1;
        protected string channel_name = string.Empty;
        protected string keywords = string.Empty;
        protected string prolistview = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.channel_id = Vincent._DTcms.DTRequest.GetQueryInt("channel_id");
            this.keywords = Vincent._DTcms.DTRequest.GetQueryString("keywords");

            if (channel_id == 0)
            {
                JscriptMsg("频道参数不正确！", "back", "Error");
                return;
            }
            this.channel_name = new BLL.channel().GetChannelName(this.channel_id); //取得频道名称
            this.pageSize = GetPageSize(10); //每页数量
            this.prolistview = Vincent._DTcms.Utils.GetCookie("source_bottle_list_view"); //显示方式
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("channel_" + this.channel_name + "_list", Vincent._DTcms.DTEnums.ActionEnum.View.ToString()); //检查权限
                RptBind("id>0 and type="+type + CombSqlTxt(this.keywords), "sort_id asc,add_time desc,id desc");
            }
        }

        #region 数据绑定=================================
        private void RptBind(string _strWhere, string _orderby)
        {
            this.page = Vincent._DTcms.DTRequest.GetQueryInt("page", 1);
            this.txtKeywords.Text = this.keywords;
            //图表或列表显示
            BLL.source_material bll = new BLL.source_material();
            switch (this.prolistview)
            {
                case "Txt":
                    this.rptList2.Visible = false;
                    this.rptList1.DataSource = bll.GetList(this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
                    this.rptList1.DataBind();
                    break;
                default:
                    this.rptList1.Visible = false;
                    this.rptList2.DataSource = bll.GetList(this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
                    this.rptList2.DataBind();
                    break;
            }
            //绑定页码
            txtPageNum.Text = this.pageSize.ToString();
            string pageUrl = Vincent._DTcms.Utils.CombUrlTxt("source_bottle_list.aspx", "channel_id={0}&keywords={1}&page={2}",
                this.channel_id.ToString(), this.keywords, "__id__");
            PageContent.InnerHtml = Vincent._DTcms.Utils.OutPageList(this.pageSize, this.page, this.totalCount, pageUrl, 8);
        }
        #endregion

        #region 组合SQL查询语句==========================
        protected string CombSqlTxt(string _keywords)
        {
            StringBuilder strTemp = new StringBuilder();
            _keywords = _keywords.Replace("'", "");
            if (!string.IsNullOrEmpty(_keywords))
            {
                strTemp.Append(" and title like '%" + _keywords + "%'");
            }
            return strTemp.ToString();
        }
        #endregion

        #region 返回图文每页数量=========================
        private int GetPageSize(int _default_size)
        {
            int _pagesize;
            if (int.TryParse(Vincent._DTcms.Utils.GetCookie("source_bottle_page_size"), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    return _pagesize;
                }
            }
            return _default_size;
        }
        #endregion

        //筛选状态
        protected void ddlLock_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(Vincent._DTcms.Utils.CombUrlTxt("source_bottle_list.aspx", "channel_id={0}&keywords={1}",
               this.channel_id.ToString(), this.keywords));
        }

        //关健字查询
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(Vincent._DTcms.Utils.CombUrlTxt("source_bottle_list.aspx", "channel_id={0}&keywords={1}",
                this.channel_id.ToString(), txtKeywords.Text));
        }

        //设置文字列表显示
        protected void lbtnViewTxt_Click(object sender, EventArgs e)
        {
            Vincent._DTcms.Utils.WriteCookie("source_bottle_list_view", "Txt", 14400);
            Response.Redirect(Vincent._DTcms.Utils.CombUrlTxt("source_bottle_list.aspx", "channel_id={0}&keywords={1}&page={2}",
                this.channel_id.ToString(), this.keywords, this.page.ToString()));
        }

        //设置图文列表显示
        protected void lbtnViewImg_Click(object sender, EventArgs e)
        {
            Vincent._DTcms.Utils.WriteCookie("source_bottle_list_view", "Img", 14400);
            Response.Redirect(Vincent._DTcms.Utils.CombUrlTxt("source_bottle_list.aspx", "channel_id={0}&keywords={1}&page={2}",
                this.channel_id.ToString(), this.keywords, this.page.ToString()));
        }

        //设置分页数量
        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            int _pagesize;
            if (int.TryParse(txtPageNum.Text.Trim(), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    Vincent._DTcms.Utils.WriteCookie("source_bottle_page_size", _pagesize.ToString(), 43200);
                }
            }
            Response.Redirect(Vincent._DTcms.Utils.CombUrlTxt("source_bottle_list.aspx", "channel_id={0}&keywords={1}",
                this.channel_id.ToString(), this.keywords));
        }

        //保存排序
        protected void btnSave_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("channel_" + this.channel_name + "_list", Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString()); //检查权限
            BLL.source_material bll = new BLL.source_material();
            Repeater rptList = new Repeater();
            switch (this.prolistview)
            {
                case "Txt":
                    rptList = this.rptList1;
                    break;
                default:
                    rptList = this.rptList2;
                    break;
            }
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                int sortId;
                if (!int.TryParse(((TextBox)rptList.Items[i].FindControl("txtSortId")).Text.Trim(), out sortId))
                {
                    sortId = 99;
                }
                bll.UpdateField(id, "sort_id=" + sortId.ToString());
            }
            AddAdminLog(Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString(), "保存" + this.channel_name + "瓶子素材排序"); //记录日志
            JscriptMsg("保存排序成功啦！", Vincent._DTcms.Utils.CombUrlTxt("source_bottle_list.aspx", "channel_id={0}&keywords={1}",
                this.channel_id.ToString(), this.keywords), "Success");
        }

        //批量删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("channel_" + this.channel_name + "_list", Vincent._DTcms.DTEnums.ActionEnum.Delete.ToString()); //检查权限
            int sucCount = 0; //成功数量
            int errorCount = 0; //失败数量
            BLL.source_material bll = new BLL.source_material();
            Repeater rptList = new Repeater();
            switch (this.prolistview)
            {
                case "Txt":
                    rptList = this.rptList1;
                    break;
                default:
                    rptList = this.rptList2;
                    break;
            }
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    if (bll.Delete(id))
                    {
                        sucCount++;
                    }
                    else
                    {
                        errorCount++;
                    }
                }
            }
            AddAdminLog(Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString(), "删除" + this.channel_name + "瓶子素材成功" + sucCount + "条，失败" + errorCount + "条"); //记录日志
            JscriptMsg("删除成功" + sucCount + "条，失败" + errorCount + "条！", Vincent._DTcms.Utils.CombUrlTxt("source_bottle_list.aspx", "channel_id={0}&keywords={1}",
                this.channel_id.ToString(), this.keywords), "Success");
        }
    }
}