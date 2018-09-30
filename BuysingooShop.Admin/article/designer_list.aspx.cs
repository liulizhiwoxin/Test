using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vincent;

namespace BuysingooShop.Admin.article
{
    public partial class designer_list : Web.UI.ManagePage
    {
        protected int channel_id;
        protected int totalCount;
        protected int page;
        protected int pageSize;

        protected int category_id;
        protected string channel_name = string.Empty;
        protected string property = string.Empty;
        protected string keywords = string.Empty;
        protected string prolistview = string.Empty;

        protected Model.manager adminModel = new Model.manager();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.channel_id = Vincent._DTcms.DTRequest.GetQueryIntValue("channel_id",2);
            this.category_id = Vincent._DTcms.DTRequest.GetQueryIntValue("category_id",62);
            this.keywords = Vincent._DTcms.DTRequest.GetQueryString("keywords");
            this.property = Vincent._DTcms.DTRequest.GetQueryString("property");

            
                
            if (channel_id == 0)
            {
                JscriptMsg("频道参数不正确！", "back", "Error");
                return;
            }
            if (adminModel == null)
            {
                JscriptMsg("你还没有登录，请先登录！","back","Error");
                return;
            }
            adminModel = GetAdminInfo();
            string strwhere = "category_id=62";
            if (adminModel.role_id != 1)//超级管理员组
            {
                strwhere += " and brand_id=" + adminModel.id;
            }
            this.channel_name = new BLL.channel().GetChannelName(this.channel_id); //取得频道名称
            this.pageSize = GetPageSize(10); //每页数量
            this.prolistview = Vincent._DTcms.Utils.GetCookie("designer_list_view"); //显示方式
            if (!Page.IsPostBack)
            {
                //ChkAdminLevel("designer_pro", Vincent._DTcms.DTEnums.ActionEnum.View.ToString()); //检查权限
                //TreeBind(this.channel_id); //绑定类别
                RptBind(this.channel_id, this.category_id, strwhere, "sort_id asc,add_time desc,id desc");
            }
        }

        //#region 绑定类别=================================
        //private void TreeBind(int _channel_id)
        //{
        //    //BLL.article_category bll = new BLL.article_category();
        //    //DataTable dt = bll.GetList(0, _channel_id);

        //    //this.ddlCategoryId.Items.Clear();
        //    //this.ddlCategoryId.Items.Add(new ListItem("所有类别", ""));
        //    //foreach (DataRow dr in dt.Rows)
        //    //{
        //    //    string Id = dr["id"].ToString();
        //    //    int ClassLayer = int.Parse(dr["class_layer"].ToString());
        //    //    string Title = dr["title"].ToString().Trim();

        //    //    if (ClassLayer == 1)
        //    //    {
        //    //        this.ddlCategoryId.Items.Add(new ListItem(Title, Id));
        //    //    }
        //    //    else
        //    //    {
        //    //        Title = "├ " + Title;
        //    //        Title = Vincent._DTcms.Utils.StringOfChar(ClassLayer - 1, "　") + Title;
        //    //        this.ddlCategoryId.Items.Add(new ListItem(Title, Id));
        //    //    }
        //    //}
        // }
        //#endregion

        #region 数据绑定=================================
        private void RptBind(int _channel_id, int _category_id,string _strWhere, string _orderby)
        {
            this.page = Vincent._DTcms.DTRequest.GetQueryInt("page", 1);
            //if (this.category_id > 0)
            //{
            //    this.ddlCategoryId.SelectedValue = _category_id.ToString();
            //}
            //this.ddlProperty.SelectedValue = this.property;
            this.txtKeywords.Text = this.keywords;
            //图表或列表显示
            BLL.article bll = new BLL.article();
            switch (this.prolistview)
            {
                case "Txt":
                    this.rptList2.Visible = false;
                    this.rptList1.DataSource = bll.GetList(_channel_id, _category_id, this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
                    this.rptList1.DataBind();
                    break;
                default:
                    this.rptList1.Visible = false;
                    this.rptList2.DataSource = bll.GetList(_channel_id, _category_id, this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
                    this.rptList2.DataBind();
                    break;
            }
            //绑定页码
            txtPageNum.Text = this.pageSize.ToString();
            string pageUrl = Vincent._DTcms.Utils.CombUrlTxt("designer_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}&page={4}",
                _channel_id.ToString(), _category_id.ToString(), this.keywords, this.property, "__id__");
            PageContent.InnerHtml = Vincent._DTcms.Utils.OutPageList(this.pageSize, this.page, this.totalCount, pageUrl, 8);
        }
        #endregion

        //#region 组合SQL查询语句==========================
        //protected string CombSqlTxt(string _keywords, string _property)
        //{
        //    StringBuilder strTemp = new StringBuilder();
        //    _keywords = _keywords.Replace("'", "");
        //    if (!string.IsNullOrEmpty(_keywords))
        //    {
        //        strTemp.Append(" and title like '%" + _keywords + "%'");
        //    }
        //    if (!string.IsNullOrEmpty(_property))
        //    {
        //        switch (_property)
        //        {
        //            case "isLock":
        //                strTemp.Append(" and is_lock=1");
        //                break;
        //            case "unIsLock":
        //                strTemp.Append(" and is_lock=0");
        //                break;
        //            case "isMsg":
        //                strTemp.Append(" and is_msg=1");
        //                break;
        //            case "isTop":
        //                strTemp.Append(" and is_top=1");
        //                break;
        //            case "isRed":
        //                strTemp.Append(" and is_red=1");
        //                break;
        //            case "isHot":
        //                strTemp.Append(" and is_hot=1");
        //                break;
        //            case "isSlide":
        //                strTemp.Append(" and is_slide=1");
        //                break;
        //        }
        //    }
        //    return strTemp.ToString();
        //}
        //#endregion

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

        //设置操作
        protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //ChkAdminLevel("channel_" + this.channel_name + "_list", Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString()); //检查权限
            int id = Convert.ToInt32(((HiddenField)e.Item.FindControl("hidId")).Value);
            BLL.article bll = new BLL.article();
            Model.article model = bll.GetModel(id);
            switch (e.CommandName)
            {
                case "lbtnIsMsg":
                    if (model.is_msg == 1)
                        bll.UpdateField(id, "is_msg=0");
                    else
                        bll.UpdateField(id, "is_msg=1");
                    break;
                case "lbtnIsTop":
                    if (model.is_top == 1)
                        bll.UpdateField(id, "is_top=0");
                    else
                        bll.UpdateField(id, "is_top=1");
                    break;
                case "lbtnIsRed":
                    if (model.is_red == 1)
                        bll.UpdateField(id, "is_red=0");
                    else
                        bll.UpdateField(id, "is_red=1");
                    break;
                case "lbtnIsHot":
                    if (model.is_hot == 1)
                        bll.UpdateField(id, "is_hot=0");
                    else
                        bll.UpdateField(id, "is_hot=1");
                    break;
                case "lbtnIsSlide":
                    if (model.is_slide == 1)
                        bll.UpdateField(id, "is_slide=0");
                    else
                        bll.UpdateField(id, "is_slide=1");
                    break;
            }
            adminModel = GetAdminInfo();
            string strwhere = "category_id=62";//设计师类别
            if (adminModel.role_id != 1)//超级管理员组
            {
                strwhere += " and brand_id=" + adminModel.id;
            }
            this.RptBind(this.channel_id, this.category_id, strwhere, "sort_id asc,add_time desc,id desc");
        }

        //关健字查询
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(Vincent._DTcms.Utils.CombUrlTxt("designer_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}",
                this.channel_id.ToString(), this.category_id.ToString(), txtKeywords.Text, this.property));
        }

        ////筛选类别
        //protected void ddlCategoryId_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Response.Redirect(Vincent._DTcms.Utils.CombUrlTxt("designer_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}",
        //        this.channel_id.ToString(), ddlCategoryId.SelectedValue, this.keywords, this.property));
        //}

        ////筛选属性
        //protected void ddlProperty_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Response.Redirect(Vincent._DTcms.Utils.CombUrlTxt("designer_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}",
        //       this.channel_id.ToString(), this.category_id.ToString(), this.keywords, ddlProperty.SelectedValue));
        //}

        //设置文字列表显示
        protected void lbtnViewTxt_Click(object sender, EventArgs e)
        {
            Vincent._DTcms.Utils.WriteCookie("designer_list_view", "Txt", 14400);
            Response.Redirect(Vincent._DTcms.Utils.CombUrlTxt("designer_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}&page={4}",
                this.channel_id.ToString(), this.category_id.ToString(), this.keywords, this.property,this.page.ToString()));
        }

        //设置图文列表显示
        protected void lbtnViewImg_Click(object sender, EventArgs e)
        {
            Vincent._DTcms.Utils.WriteCookie("designer_list_view", "Img", 14400);
            Response.Redirect(Vincent._DTcms.Utils.CombUrlTxt("designer_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}&page={4}",
                this.channel_id.ToString(), this.category_id.ToString(), this.keywords, this.property, this.page.ToString()));
        }

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
            Response.Redirect(Vincent._DTcms.Utils.CombUrlTxt("designer_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}",
                this.channel_id.ToString(), this.category_id.ToString(), this.keywords, this.property));
        }

        //保存排序
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //ChkAdminLevel("channel_" + this.channel_name + "_list", Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString()); //检查权限
            BLL.article bll = new BLL.article();
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
            AddAdminLog(Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString(), "保存" + this.channel_name + "频道内容排序"); //记录日志
            JscriptMsg("保存排序成功啦！", Vincent._DTcms.Utils.CombUrlTxt("designer_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}",
                this.channel_id.ToString(), this.category_id.ToString(), this.keywords, this.property), "Success");
        }

        //批量删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            //ChkAdminLevel("channel_" + this.channel_name + "_list", Vincent._DTcms.DTEnums.ActionEnum.Delete.ToString()); //检查权限
            int sucCount = 0; //成功数量
            int errorCount = 0; //失败数量
            BLL.article bll = new BLL.article();
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
            AddAdminLog(Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString(), "删除" + this.channel_name + "频道内容成功" + sucCount + "条，失败" + errorCount + "条"); //记录日志
            JscriptMsg("删除成功" + sucCount + "条，失败" + errorCount + "条！", Vincent._DTcms.Utils.CombUrlTxt("designer_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}",
                this.channel_id.ToString(), this.category_id.ToString(), this.keywords, this.property), "Success");
        }
    }
}