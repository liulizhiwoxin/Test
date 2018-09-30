using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vincent;

namespace BuysingooShop.Admin.actively
{
    public partial class blogroll_list : Web.UI.ManagePage
    {
        protected int channel_id;
        protected int totalCount;
        protected int page;
        protected int pageSize;

        protected int category_id;
        protected string property = string.Empty;
        protected string keywords = string.Empty;
        protected string prolistview = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.channel_id = Vincent._DTcms.DTRequest.GetQueryInt("channel_id",99);
            this.category_id = Vincent._DTcms.DTRequest.GetQueryInt("category_id",0);
            this.keywords = Vincent._DTcms.DTRequest.GetQueryString("keywords");
            this.property = Vincent._DTcms.DTRequest.GetQueryString("property");

            this.pageSize = GetPageSize(10); //每页数量
            this.prolistview = Vincent._DTcms.Utils.GetCookie("blogroll_list_view"); //显示方式
            if (!Page.IsPostBack)
            {
                RptBind(this.channel_id, this.category_id, "", "sort_id asc,add_time desc,id desc");
            }
        }

        #region 数据绑定=================================
        private void RptBind(int _channel_id, int _category_id,string _strWhere, string _orderby)
        {
            this.page = Vincent._DTcms.DTRequest.GetQueryInt("page", 1);
            //图表或列表显示
            BLL.article bll = new BLL.article();
            this.rptList1.DataSource = bll.GetList(_channel_id, _category_id, this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
            this.rptList1.DataBind();
            //绑定页码
            txtPageNum.Text = this.pageSize.ToString();
            string pageUrl = Vincent._DTcms.Utils.CombUrlTxt("blogroll_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}&page={4}",
                _channel_id.ToString(), _category_id.ToString(), this.keywords, this.property, "__id__");
            PageContent.InnerHtml = Vincent._DTcms.Utils.OutPageList(this.pageSize, this.page, this.totalCount, pageUrl, 8);
        }
        #endregion

        #region 返回图文每页数量=========================
        private int GetPageSize(int _default_size)
        {
            int _pagesize;
            if (int.TryParse(Vincent._DTcms.Utils.GetCookie("article_page_size"), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    return _pagesize;
                }
            }
            return _default_size;
        }
        #endregion

        //设置分页数量
        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            int _pagesize;
            if (int.TryParse(txtPageNum.Text.Trim(), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    Vincent._DTcms.Utils.WriteCookie("article_page_size", _pagesize.ToString(), 43200);
                }
            }
            Response.Redirect(Vincent._DTcms.Utils.CombUrlTxt("blogroll_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}",
                this.channel_id.ToString(), this.category_id.ToString(), this.keywords, this.property));
        }

        //保存排序
        protected void btnSave_Click(object sender, EventArgs e)
        {
            BLL.article bll = new BLL.article();
            Repeater rptList = new Repeater();
            rptList = this.rptList1;

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
            AddAdminLog(Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString(), "保存友情链接频道内容排序"); //记录日志
            JscriptMsg("保存排序成功啦！", Vincent._DTcms.Utils.CombUrlTxt("blogroll_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}",
                this.channel_id.ToString(), this.category_id.ToString(), this.keywords, this.property), "Success");
        }

        //批量删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int sucCount = 0; //成功数量
            int errorCount = 0; //失败数量
            BLL.article bll = new BLL.article();
            Repeater rptList = new Repeater();
            rptList = this.rptList1;

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
            AddAdminLog(Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString(), "删除友情链接频道内容成功" + sucCount + "条，失败" + errorCount + "条"); //记录日志
            JscriptMsg("删除成功" + sucCount + "条，失败" + errorCount + "条！", Vincent._DTcms.Utils.CombUrlTxt("blogroll_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}",
                this.channel_id.ToString(), this.category_id.ToString(), this.keywords, this.property), "Success");
        }
    }
}