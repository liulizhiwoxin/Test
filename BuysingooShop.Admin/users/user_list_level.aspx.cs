using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vincent;

namespace BuysingooShop.Admin.users
{
    public partial class user_list_level : Web.UI.ManagePage
    {
        protected int totalCount;
        protected int page;
        protected int pageSize;

        protected int group_id;
        protected int id;
        protected string keywords = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            ChkAdminLevel("user_list_level", Vincent._DTcms.DTEnums.ActionEnum.View.ToString()); //检查权限
            this.group_id = Vincent._DTcms.DTRequest.GetQueryInt("group_id");
            this.id = Vincent._DTcms.DTRequest.GetQueryInt("id");
            this.keywords = Vincent._DTcms.DTRequest.GetQueryString("keywords");

            if (id > 0)
            {
                Session["id"] = id;
            }
            var preId = "";
            if (Session["id"] != null)
            {
                preId = Session["id"].ToString();
            }
            

            this.pageSize = GetPageSize(10); //每页数量
            if (!Page.IsPostBack)
            {
                TreeBind("is_lock=0"); //绑定类别
                if (preId != "")
                {
                    RptBind("id>0 and preid=" + preId + CombSqlTxt(this.group_id, this.id, this.keywords), "reg_time desc,id desc");
                }
                else
                {
                    RptBind("id>0 " + CombSqlTxt(this.group_id, this.id, this.keywords), "reg_time desc,id desc");
                }
            }
        }

        #region 绑定组别=================================
        private void TreeBind(string strWhere)
        {
            BLL.user_groups bll = new BLL.user_groups();
            DataTable dt = bll.GetList(0, strWhere, "id desc").Tables[0];

            this.ddlGroupId.Items.Clear();
            this.ddlGroupId.Items.Add(new ListItem("所有会员组", ""));
            //this.ddlGroupId.Items.Add(new ListItem("新用户", "0"));
            foreach (DataRow dr in dt.Rows)
            {
                this.ddlGroupId.Items.Add(new ListItem(dr["title"].ToString(), dr["id"].ToString()));
                
            }
        }
        #endregion

        #region 数据绑定=================================
        private void RptBind(string _strWhere, string _orderby)
        {
            this.page = Vincent._DTcms.DTRequest.GetQueryInt("page", 1);
            if (this.group_id > 0)
            {
                this.ddlGroupId.SelectedValue = this.group_id.ToString();
            }
            this.txtKeywords.Text = this.keywords;
            BLL.users bll = new BLL.users();
            this.rptList.DataSource = bll.GetList(this.pageSize, this.page, _strWhere + " and 1=1 and isDelete = 0", _orderby, out this.totalCount);
            this.rptList.DataBind();

            //绑定页码
            txtPageNum.Text = this.pageSize.ToString();
            string pageUrl = Vincent._DTcms.Utils.CombUrlTxt("user_list_level.aspx", "group_id={0}&keywords={1}&page={2}",
                this.group_id.ToString(), this.keywords, "__id__");
            PageContent.InnerHtml = Vincent._DTcms.Utils.OutPageList(this.pageSize, this.page, this.totalCount, pageUrl, 8);
        }


        #endregion

        #region 组合SQL查询语句==========================
        protected string CombSqlTxt(int _group_id, string _keywords)
        {
            StringBuilder strTemp = new StringBuilder();
            if (_group_id > 0)
            {
                strTemp.Append(" and group_id=" + _group_id);
            }
            _keywords = _keywords.Replace("'", "");
            if (!string.IsNullOrEmpty(_keywords))
            {
                strTemp.Append(" and (user_name like '%" + _keywords + "%' or mobile like '%" + _keywords + "%' or email like '%" + _keywords + "%' or nick_name like '%" + _keywords + "%')");
            }
            return strTemp.ToString();
        }

        protected string CombSqlTxt(int _group_id, int _id, string _keywords)
        {
            StringBuilder strTemp = new StringBuilder();
            if (_group_id > 0)
            {
                strTemp.Append(" and group_id=" + _group_id);
            }
            if (_id > 0)
            {
                strTemp.Append(" and preId=" + _id);
            }
            _keywords = _keywords.Replace("'", "");
            if (!string.IsNullOrEmpty(_keywords))
            {
                strTemp.Append(" and (user_name like '%" + _keywords + "%' or mobile like '%" + _keywords + "%' or email like '%" + _keywords + "%' or nick_name like '%" + _keywords + "%')");
            }
            return strTemp.ToString();
        }
        #endregion

        #region 返回用户每页数量=========================
        private int GetPageSize(int _default_size)
        {
            int _pagesize;
            if (int.TryParse(Vincent._DTcms.Utils.GetCookie("user_list_page_size"), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    return _pagesize;
                }
            }
            return _default_size;
        }
        #endregion

        #region 返回用户状态=============================
        protected string GetUserStatus(int status)
        {
            string result = string.Empty;
            switch (status)
            {
                case 0:
                    result = "正常";
                    break;
                case 1:
                    result = "待验证";
                    break;
                case 2:
                    result = "待审核";
                    break;
                case 3:
                    result = "已禁用";
                    break;
            }
            return result;
        }
        #endregion

        #region 返回用户团队信息=============================
        protected string GetTeam_amount(int id)
        {
            BLL.users bll = new BLL.users();
            string team_amount = string.Empty;
            DataTable dt = bll.GetTeam_amount(id).Tables[0];//获取该用户上线信息
            if (dt.Rows[0]["team_total"].ToString() == "")
            {
                return team_amount = "0.00";
            }
            team_amount = dt.Rows[0]["team_total"].ToString();
            return team_amount;
        }
        #endregion


        #region 返回用户上线名称=============================
        protected string GetUserPreName(int id)
        {
            BLL.users bll = new BLL.users();
            string preName = string.Empty;

            DataTable dt = bll.GetPreUserInfo(id).Tables[0];//获取该用户上线信息

            if (dt.Rows.Count>0)
         	{

                preName = dt.Rows[0]["nick_name"].ToString();
	        }
            else
            {
                preName = "暂无上线";
            }   

            return preName;
        }
        #endregion

        //关健字查询


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(Vincent._DTcms.Utils.CombUrlTxt("user_list_level.aspx", "group_id={0}&keywords={1}",
                this.group_id.ToString(), txtKeywords.Text));
        }

        //筛选类别
        protected void ddlGroupId_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(Vincent._DTcms.Utils.CombUrlTxt("user_list_level.aspx", "group_id={0}&keywords={1}",
                ddlGroupId.SelectedValue, this.keywords));
        }

        //设置分页数量
        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            int _pagesize;
            if (int.TryParse(txtPageNum.Text.Trim(), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    Vincent._DTcms.Utils.WriteCookie("user_list_page_size", _pagesize.ToString(), 14400);
                }
            }
            Response.Redirect(Vincent._DTcms.Utils.CombUrlTxt("user_list_level.aspx", "group_id={0}&keywords={1}",
                this.group_id.ToString(), this.keywords));
        }

        //发送手机短信
        protected void btnSmsPost_Click(object sender, EventArgs e)
        {
            BLL.users bll = new BLL.users();
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    Model.users model = bll.GetModel(id);
                    if (model != null && !string.IsNullOrEmpty(model.mobile))
                    {
                        str.Append(model.mobile + ",");
                    }
                }
            }
            if (!string.IsNullOrEmpty(str.ToString()))
            {
                Response.Redirect("user_sms.aspx?mobiles=" + Vincent._DTcms.Utils.UrlEncode(Vincent._DTcms.Utils.DelLastComma(str.ToString())));
            }
        }

        //批量删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("user_list", Vincent._DTcms.DTEnums.ActionEnum.Delete.ToString()); //检查权限
            int sucCount = 0;
            int errorCount = 0;
            BLL.users bll = new BLL.users();
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
            AddAdminLog(Vincent._DTcms.DTEnums.ActionEnum.Delete.ToString(), "删除用户" + sucCount + "条，失败" + errorCount + "条"); //记录日志
            JscriptMsg("删除成功" + sucCount + "条，失败" + errorCount + "条！",
                Vincent._DTcms.Utils.CombUrlTxt("user_list.aspx", "group_id={0}&keywords={1}", this.group_id.ToString(), this.keywords), "Success");
        }

    }
}