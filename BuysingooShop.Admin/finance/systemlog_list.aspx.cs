using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BuysingooShop.Admin.finance
{
    public partial class systemlog_list : Web.UI.ManagePage
    {
        protected int totalCount;
        protected int page;
        protected int pageSize;

        protected string keywords = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.keywords = Vincent._DTcms.DTRequest.GetQueryString("keywords");
            var model = GetAdminInfo();

            this.pageSize = GetPageSize(10); //每页数量
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("finance_recharge_day", Vincent._DTcms.DTEnums.ActionEnum.View.ToString()); //检查权限
                RptBind("  t1.payment_status=2" + CombSqlTxt(keywords), "add_time desc");
            }
        }

        /// <summary>
        /// 根据类型ID获取名称
        /// </summary>
        /// <returns></returns>
        public string GetTypeNameById(string typeid)
        {
            var typename = "";
            //3升级记录(会员14消费,1升级为VIP用户) 4未升级记录(新增左区，没有会员升级) 10会员积分收入 11累积收入记录  12一级分享业绩 13二级分享业绩
            if (typeid == "12")
            {
                typename = "一级分享业绩";
            }
            else if (typeid == "13")
            {
                typename = "二级分享业绩";
            }
            else if (typeid == "10")
            {
                typename = "VIP会员收入";
            }

            return typename;
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



        /// <summary>
        /// 获取消费情况
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected string GetDescriptionById(int id)
        {
            string Description = "";

            Model.order_goods model = new BLL.order_goods().GetModelorderid(id);
            if (model == null)
            {
                return Description = "暂无详情";
            }

            string title = Convert.ToString(model.goods_title);
            return Description = title;
        }

        /// <summary>
        /// 获取数量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected string GetQuantityById(int id)
        {
            string quantity = "";

            Model.order_goods model = new BLL.order_goods().GetModelorderid(id);
            if (model == null)
            {
                return quantity = "暂无详情";
            }

            return quantity = Convert.ToString(model.quantity);
        }

        /// <summary>
        /// 获取价格
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected string GetPrcieById(int id)
        {
            string Prcie = "";

            Model.order_goods model = new BLL.order_goods().GetModelorderid(id);
            if (model == null)
            {
                return Prcie = "暂无详情";
            }

            return Prcie = Convert.ToString(model.goods_price);
        }


        #region 数据绑定=================================
        private void RptBind(string _strWhere, string _orderby)
        {
            this.page = Vincent._DTcms.DTRequest.GetQueryInt("page", 1);
            txtKeywords.Text = this.keywords;
            BLL.systemlog bll = new BLL.systemlog();
            this.rptList.DataSource = bll.GetListByOrderandWithdraw(this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
            this.rptList.DataBind();

            //绑定页码
            txtPageNum.Text = this.pageSize.ToString();
            string pageUrl = Vincent._DTcms.Utils.CombUrlTxt("systemlog_list.aspx", "keywords={0}&page={1}", this.keywords, "__id__");
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
                strTemp.Append(" and (user_name like '%" + _keywords + "%' or description like '%" + _keywords + "%')");
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

        //关健字查询
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(Vincent._DTcms.Utils.CombUrlTxt("systemlog_list.aspx", "keywords={0}", txtKeywords.Text));
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
            Response.Redirect(Vincent._DTcms.Utils.CombUrlTxt("systemlog_list.aspx", "keywords={0}", this.keywords));
        }

        //批量删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("finance_recharge_day", Vincent._DTcms.DTEnums.ActionEnum.Delete.ToString()); //检查权限
            int sucCount = 0;
            int errorCount = 0;
            BLL.systemlog bll = new BLL.systemlog();
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    //if (bll.Delete(id))
                    //{
                    //    sucCount += 1;
                    //}
                    //else
                    //{
                    //    errorCount += 1;
                    //}
                }
            }
            AddAdminLog(Vincent._DTcms.DTEnums.ActionEnum.Delete.ToString(), "删除积分日专成功" + sucCount + "条，失败" + errorCount + "条"); //记录日志
            JscriptMsg("删除成功" + sucCount + "条，失败" + errorCount + "条！", Vincent._DTcms.Utils.CombUrlTxt("recharge_day_list.aspx", "keywords={0}", this.keywords), "Success");
        }
    }
}