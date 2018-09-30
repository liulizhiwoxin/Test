using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vincent;

namespace BuysingooShop.Admin.good
{
    public partial class template_list : Web.UI.ManagePage
    {
        protected int channel_id;
        protected int totalCount;
        protected int page;
        protected int pageSize;

        protected int category_id;
        protected string channel_name = string.Empty;
        protected string islock = string.Empty;
        protected string keywords = string.Empty;
        protected string prolistview = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.channel_id = Vincent._DTcms.DTRequest.GetQueryInt("channel_id");
            this.category_id = Vincent._DTcms.DTRequest.GetQueryInt("category_id");
            this.keywords = Vincent._DTcms.DTRequest.GetQueryString("keywords");
            this.islock = Vincent._DTcms.DTRequest.GetQueryString("islock");

            if (channel_id == 0)
            {
                JscriptMsg("频道参数不正确！", "back", "Error");
                return;
            }
            this.channel_name = new BLL.channel().GetChannelName(this.channel_id); //取得频道名称
            this.pageSize = GetPageSize(10); //每页数量
            this.prolistview = Vincent._DTcms.Utils.GetCookie("template_list_view"); //显示方式
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("channel_" + this.channel_name + "_list", Vincent._DTcms.DTEnums.ActionEnum.View.ToString()); //检查权限
                TreeBind(this.channel_id); //绑定类别
                RptBind(this.category_id, "id>0" + CombSqlTxt(this.keywords, this.islock), "sort_id asc,addTime desc,id desc");
            }
        }

        #region 绑定类别=================================
        private void TreeBind(int _channel_id)
        {
            BLL.article_category bll = new BLL.article_category();
            DataTable dt = bll.GetList(0, _channel_id);

            this.ddlCategoryId.Items.Clear();
            this.ddlCategoryId.Items.Add(new ListItem("所有类别", ""));
            foreach (DataRow dr in dt.Rows)
            {
                string Id = dr["id"].ToString();
                int ClassLayer = int.Parse(dr["class_layer"].ToString());
                string Title = dr["title"].ToString().Trim();

                if (ClassLayer == 1)
                {
                    this.ddlCategoryId.Items.Add(new ListItem(Title, Id));
                }
                else
                {
                    Title = "├ " + Title;
                    Title = Vincent._DTcms.Utils.StringOfChar(ClassLayer - 1, "　") + Title;
                    this.ddlCategoryId.Items.Add(new ListItem(Title, Id));
                }
            }
        }
        #endregion

        #region 数据绑定=================================
        private void RptBind(int _category_id, string _strWhere, string _orderby)
        {
            this.page = Vincent._DTcms.DTRequest.GetQueryInt("page", 1);

            if (this.category_id > 0)
            {
                this.ddlCategoryId.SelectedValue = _category_id.ToString();
            }
            this.txtKeywords.Text = this.keywords;
            this.ddlLock.SelectedValue = this.islock;
            //图表或列表显示
            BLL.good_template bll = new BLL.good_template();
            switch (this.prolistview)
            {
                case "Txt":
                    this.rptList2.Visible = false;
                    this.rptList1.DataSource = bll.GetList(_category_id, this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
                    this.rptList1.DataBind();
                    break;
                default:
                    this.rptList1.Visible = false;
                    this.rptList2.DataSource = bll.GetList(_category_id, this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
                    this.rptList2.DataBind();
                    break;
            }
            //绑定页码
            txtPageNum.Text = this.pageSize.ToString();
            string pageUrl = Vincent._DTcms.Utils.CombUrlTxt("template_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&islock={3}&page={4}",
                this.channel_id.ToString(), _category_id.ToString(), this.keywords, this.islock, "__id__");
            PageContent.InnerHtml = Vincent._DTcms.Utils.OutPageList(this.pageSize, this.page, this.totalCount, pageUrl, 8);
        }
        #endregion

        #region 组合SQL查询语句==========================
        protected string CombSqlTxt(string _keywords, string _islock)
        {
            StringBuilder strTemp = new StringBuilder();
            _keywords = _keywords.Replace("'", "");
            if (!string.IsNullOrEmpty(_keywords))
            {
                strTemp.Append(" and name like '%" + _keywords + "%'");
            }
            if (!string.IsNullOrEmpty(_islock))
            {
                strTemp.Append(" and islock=" + _islock);
            }
            return strTemp.ToString();
        }
        #endregion

        #region 返回图文每页数量=========================
        private int GetPageSize(int _default_size)
        {
            int _pagesize;
            if (int.TryParse(Vincent._DTcms.Utils.GetCookie("template_page_size"), out _pagesize))
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
            Response.Redirect(Vincent._DTcms.Utils.CombUrlTxt("template_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&islock={3}",
               this.channel_id.ToString(), this.category_id.ToString(), this.keywords, ddlLock.SelectedValue));
        }

        //设置操作
        protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            ChkAdminLevel("channel_" + this.channel_name + "_list", Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString()); //检查权限
            int id = Convert.ToInt32(((HiddenField)e.Item.FindControl("hidId")).Value);
            BLL.good_template bll = new BLL.good_template();
            Model.good_template model = bll.GetModel(id);
            switch (e.CommandName)
            {
                case "lbtnIsAd":
                    if (model.isAd == 1)
                        bll.UpdateField(id, "isAd=0");
                    else
                        bll.UpdateField(id, "isAd=1");
                    break;
            }
            this.RptBind(this.category_id, "id>0" + CombSqlTxt(this.keywords, this.islock), "sort_id asc,addTime desc,id desc");
        }

        //关健字查询
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(Vincent._DTcms.Utils.CombUrlTxt("template_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&islock={3}",
                this.channel_id.ToString(), this.category_id.ToString(), txtKeywords.Text, this.islock));
        }

        //筛选类别
        protected void ddlCategoryId_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(Vincent._DTcms.Utils.CombUrlTxt("template_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&islock={3}",
                this.channel_id.ToString(), ddlCategoryId.SelectedValue, this.keywords, this.islock));
        }

        //设置文字列表显示
        protected void lbtnViewTxt_Click(object sender, EventArgs e)
        {
            Vincent._DTcms.Utils.WriteCookie("template_list_view", "Txt", 14400);
            Response.Redirect(Vincent._DTcms.Utils.CombUrlTxt("template_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&islock={3}&page={4}",
                this.channel_id.ToString(), this.category_id.ToString(), this.keywords, this.islock, this.page.ToString()));
        }

        //设置图文列表显示
        protected void lbtnViewImg_Click(object sender, EventArgs e)
        {
            Vincent._DTcms.Utils.WriteCookie("template_list_view", "Img", 14400);
            Response.Redirect(Vincent._DTcms.Utils.CombUrlTxt("template_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&islock={3}&page={4}",
                this.channel_id.ToString(), this.category_id.ToString(), this.keywords, this.islock, this.page.ToString()));
        }

        //设置分页数量
        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            int _pagesize;
            if (int.TryParse(txtPageNum.Text.Trim(), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    Vincent._DTcms.Utils.WriteCookie("template_page_size", _pagesize.ToString(), 43200);
                }
            }
            Response.Redirect(Vincent._DTcms.Utils.CombUrlTxt("template_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&islock={3}",
                this.channel_id.ToString(), this.category_id.ToString(), this.keywords, this.islock));
        }

        //保存排序
        protected void btnSave_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("channel_" + this.channel_name + "_list", Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString()); //检查权限
            BLL.good_template bll = new BLL.good_template();
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
                //排序
                int sortId;
                if (!int.TryParse(((TextBox)rptList.Items[i].FindControl("txtSortId")).Text.Trim(), out sortId))
                {
                    sortId = 99;
                }
                bll.UpdateField(id, "sort_id=" + sortId.ToString());
            }
            AddAdminLog(Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString(), "保存" + this.channel_name + "模板排序"); //记录日志
            JscriptMsg("保存排序成功啦！", Vincent._DTcms.Utils.CombUrlTxt("template_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&islock={3}",
                this.channel_id.ToString(), this.category_id.ToString(), this.keywords, this.islock), "Success");
        }

        //保存广告位
        protected void btnSave1_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("channel_" + this.channel_name + "_list", Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString()); //检查权限
            BLL.good_template bll = new BLL.good_template();
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
                //广告位
                int IsAd = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidIsAd")).Value);
                if (IsAd == 1)
                {
                    int sortAd;
                    if (!int.TryParse(((TextBox)rptList.Items[i].FindControl("txtSortAd")).Text.Trim(), out sortAd))
                    {
                        sortAd = 0;
                    }
                    if (sortAd > 0)
                    {
                        bll.UpdateField(id, "sort_ad=" + sortAd.ToString());
                    }
                }
                else
                {
                    JscriptMsg("请先开启广告位设置！", Vincent._DTcms.Utils.CombUrlTxt("template_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&islock={3}",
                this.channel_id.ToString(), this.category_id.ToString(), this.keywords, this.islock), "Success");
                }
            }
            AddAdminLog(Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString(), "保存" + this.channel_name + "模板广告位"); //记录日志
            JscriptMsg("保存广告位成功啦！", Vincent._DTcms.Utils.CombUrlTxt("template_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&islock={3}",
                this.channel_id.ToString(), this.category_id.ToString(), this.keywords, this.islock), "Success");
        }

        //批量删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("channel_" + this.channel_name + "_list", Vincent._DTcms.DTEnums.ActionEnum.Delete.ToString()); //检查权限
            int sucCount = 0; //成功数量
            int errorCount = 0; //失败数量
            BLL.good_template bll = new BLL.good_template();
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
            AddAdminLog(Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString(), "删除" + this.channel_name + "模板成功" + sucCount + "条，失败" + errorCount + "条"); //记录日志
            JscriptMsg("删除成功" + sucCount + "条，失败" + errorCount + "条！", Vincent._DTcms.Utils.CombUrlTxt("template_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&islock={3}",
                this.channel_id.ToString(), this.category_id.ToString(), this.keywords, this.islock), "Success");
        }
    }
}