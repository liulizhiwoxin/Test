using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vincent;

namespace BuysingooShop.Admin.finance
{
    public partial class withdraw_list : Web.UI.ManagePage
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
                ChkAdminLevel("finance_withdraw", Vincent._DTcms.DTEnums.ActionEnum.View.ToString()); //检查权限
                RptBind("id>0" + CombSqlTxt(keywords), "addtime desc,id desc");
            }
        }

        #region 数据绑定=================================
        private void RptBind(string _strWhere, string _orderby)
        {
            this.page = Vincent._DTcms.DTRequest.GetQueryInt("page", 1);
            txtKeywords.Text = this.keywords;
            BLL.withdraw bll = new BLL.withdraw();
            this.rptList.DataSource = bll.GetList(this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
            this.rptList.DataBind();

            //绑定页码
            txtPageNum.Text = this.pageSize.ToString();
            string pageUrl = Vincent._DTcms.Utils.CombUrlTxt("withdraw_list.aspx", "keywords={0}&page={1}", this.keywords, "__id__");
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
                var id = Getuserid(_keywords);
                strTemp.Append(" and (user_id='" + id + "' or remark like '%" + _keywords + "%' or mobile like '%" + _keywords + "%')");
            }

            return strTemp.ToString();
        }
        #endregion

        #region 返回每页数量=============================
        private int GetPageSize(int _default_size)
        {
            int _pagesize;
            if (int.TryParse(Vincent._DTcms.Utils.GetCookie("user_point_log_page_size"), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    return _pagesize;
                }
            }
            return _default_size;
        }
        #endregion

        //获取用户id
        protected int Getuserid(string username)
        {
            int user_id = 0;
            user_id = new BLL.users().Getid(username);
            return user_id;

        }

        //获取用户名
        protected string Getusername(int id)
        {
            string username = "";
            Model.users model = new BLL.users().GetModel(id);
            if (model == null)
            {
                return username = "--";
            }
            return username = model.user_name;
            
        }
        /// <summary>
        /// 获取用户昵称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected string Getusernick_name(int id)
        {
            string nick_name = "";
            Model.users model = new BLL.users().GetModel(id);
            if (model == null)
            {
                return nick_name = "--";
            }
            return nick_name = model.nick_name;

        }
        //银行类型
        protected string Getbanktype(int id)
        {
            string title="";
            switch (id)
            {
            case 1:
                title="支付宝";
            break;
            case 2:
            title = "中国银行";
            break;
            case 3:
            title = "工商银行";
            break;
            case 4:
            title = "招商银行";
            break;
            case 5:
            title = "农业银行";
            break;
            case 6:
            title = "交通银行";
            break;
            case 7:
            title = "建设银行";
            break;

            }
            return title;
        }

        //状态类型
        protected string Getstatus(int id)
        {
            string title = "";
            switch (id)
            {
                case 1:
                    title = "待审核";
                    break;
                case 2:
                    title = "同意提现";
                    break;
                case 3:
                    title = "驳回提现";
                    break;

            }
            return title;
        }


        //关健字查询
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(Vincent._DTcms.Utils.CombUrlTxt("withdraw_list.aspx", "keywords={0}", txtKeywords.Text));
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
            Response.Redirect(Vincent._DTcms.Utils.CombUrlTxt("withdraw_list.aspx", "keywords={0}", this.keywords));
        }

        //批量删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("finance_recharge_day", Vincent._DTcms.DTEnums.ActionEnum.Delete.ToString()); //检查权限
            int sucCount = 0;
            int errorCount = 0;
            BLL.user_point_log bll = new BLL.user_point_log();
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
            AddAdminLog(Vincent._DTcms.DTEnums.ActionEnum.Delete.ToString(), "删除积分日专成功" + sucCount + "条，失败" + errorCount + "条"); //记录日志
            JscriptMsg("删除成功" + sucCount + "条，失败" + errorCount + "条！", Vincent._DTcms.Utils.CombUrlTxt("withdraw_list.aspx", "keywords={0}", this.keywords), "Success");
        }
    }
}