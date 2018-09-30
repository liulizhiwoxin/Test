using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vincent;

namespace BuysingooShop.Admin.users
{
    public partial class issue_coupon : Web.UI.ManagePage
    {
        string mobiles = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            mobiles = Vincent._DTcms.DTRequest.GetString("mobiles");
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("issue_coupon", Vincent._DTcms.DTEnums.ActionEnum.View.ToString()); //检查权限
                ShowInfo(mobiles);
                TreeBind("is_lock=0"); //绑定类别
                couponBind();//绑定优惠券类型
            }
        }

        #region 绑定类别=================================
        private void TreeBind(string strWhere)
        {
            BLL.user_groups bll = new BLL.user_groups();
            DataTable dt = bll.GetList(0, strWhere, "grade asc,id asc").Tables[0];

            this.cblGroupId.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                this.cblGroupId.Items.Add(new ListItem(dr["title"].ToString(), dr["id"].ToString()));
            }
        }
        #endregion

        #region 绑定优惠券类型=================================
        private void couponBind()
        {
            BLL.user_coupon bll = new BLL.user_coupon();
            DataTable dt = bll.GetList(" datediff(dd,end_time,'" + DateTime.Now + "')<0", " id").Tables[0];

            this.ddlGroupId.Items.Clear();
            this.ddlGroupId.Items.Add(new ListItem("请选择...", ""));
            foreach (DataRow dr in dt.Rows)
            {
                string Id = dr["id"].ToString();
                string Title = dr["title"].ToString().Trim();
                this.ddlGroupId.Items.Add(new ListItem(Title, Id));
            }
        }
        #endregion

        #region 赋值操作=================================
        private void ShowInfo(string _mobiles)
        {
            if (!string.IsNullOrEmpty(_mobiles))
            {
                div_mobiles.Visible = true;
                div_group.Visible = false;
                rblSmsType.SelectedValue = "1";
                txtMobileNumbers.Text = _mobiles;

            }
            else
            {
                rblSmsType.SelectedValue = "2";
                div_mobiles.Visible = false;
                div_group.Visible = true;
            }
        }
        #endregion

        #region 返回会员组所有手机号码===================
        private string GetGroupMobile(ArrayList al)
        {
            StringBuilder str = new StringBuilder();
            foreach (Object obj in al)
            {
                DataTable dt = new BLL.users().GetList(0, "group_id=" + Convert.ToInt32(obj), "reg_time desc,id desc").Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(dr["mobile"].ToString()))
                    {
                        str.Append(dr["mobile"].ToString() + ",");
                    }
                }
            }
            return Vincent._DTcms.Utils.DelLastComma(str.ToString());
        }
        #endregion

        //选择发送类型
        protected void rblSmsType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblSmsType.SelectedValue == "1")
            {
                div_group.Visible = false;
                div_mobiles.Visible = true;
            }
            else
            {
                div_group.Visible = true;
                div_mobiles.Visible = false;
            }
        }

        //提交发送
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("issue_coupon", Vincent._DTcms.DTEnums.ActionEnum.Add.ToString()); //检查权限
            //优惠券类型
            if (ddlGroupId.SelectedValue == "")
            {
                JscriptMsg("请选择优惠券类型！", "", "Error");
                return;
            }

            //检查短信内容
            if (txtSmsContent.Text.Trim() == "")
            {
                JscriptMsg("请输入短信内容！", "", "Error");
                return;
            }
            //检查发送类型
            if (rblSmsType.SelectedValue == "1")
            {
                if (txtMobileNumbers.Text.Trim() == "")
                {
                    JscriptMsg("请输入手机号码！", "", "Error");
                    return;
                }

                string[] oldMobileArr = txtMobileNumbers.Text.Trim().Split(',');//将手机号存入数组
                DataTable dt = new BLL.user_coupon().GetList(0," title='"+ddlGroupId.SelectedValue+"' and status!=2 and status!=4"," id").Tables[0];
                if (dt.Rows.Count < 0) {
                    JscriptMsg("没有可发放的优惠券！", "", "Error");
                    return;
                }
                if (oldMobileArr.Length > dt.Rows.Count)
                {
                    JscriptMsg("该类型优惠券数量不够！", "", "Error");
                    return;
                }
                for (int i = 0; i < oldMobileArr.Length; i++)
                {
                    Model.users model = new BLL.users().GetModelMobile(oldMobileArr[i]);
                    if (model != null)
                    {
                        Model.user_coupon couponm = new BLL.user_coupon().GetModel(int.Parse(dt.Rows[i]["id"].ToString()));
                        couponm.userid = model.id;
                        couponm.status = 4;
                        new BLL.user_coupon().Update(couponm);
                    }
                }

                //开始发送短信
                string msg = string.Empty;
                bool result = new BLL.sms_message().Send(txtMobileNumbers.Text.Trim(), txtSmsContent.Text.Trim(), 2, out msg);
                if (result)
                {
                    AddAdminLog(Vincent._DTcms.DTEnums.ActionEnum.Add.ToString(), "发送手机短信"); //记录日志
                    JscriptMsg(msg, "../coupon/coupon_list.aspx", "Success");
                    return;
                }
                JscriptMsg(msg, "", "Error");
                return;
            }
            else
            {
                ArrayList al = new ArrayList();
                for (int i = 0; i < cblGroupId.Items.Count; i++)
                {
                    if (cblGroupId.Items[i].Selected)
                    {
                        al.Add(cblGroupId.Items[i].Value);
                    }
                }
                if (al.Count < 1)
                {
                    JscriptMsg("请选择会员组别！", "", "Error");
                    return;
                }
                string _mobiles = GetGroupMobile(al);

                string[] oldMobileArr = _mobiles.Split(',');//将手机号存入数组
                DataTable dt = new BLL.user_coupon().GetList(0, " title='" + ddlGroupId.SelectedValue + "' and status!=2 and status!=4", " id").Tables[0];
                if (dt.Rows.Count < 0)
                {
                    JscriptMsg("没有可发放的优惠券！", "", "Error");
                    return;
                }
                if (oldMobileArr.Length > dt.Rows.Count)
                {
                    JscriptMsg("该类型优惠券数量不够！", "", "Error");
                    return;
                }
                for (int i = 0; i < oldMobileArr.Length; i++)
                {
                    Model.users model = new BLL.users().GetModelMobile(oldMobileArr[i]);
                    if (model != null)
                    {
                        Model.user_coupon couponm = new BLL.user_coupon().GetModel(int.Parse(dt.Rows[i]["id"].ToString()));
                        couponm.userid = model.id;
                        couponm.status = 4;
                        new BLL.user_coupon().Update(couponm);
                    }
                }

                //开始发送短信
                string msg = string.Empty;
                bool result = new BLL.sms_message().Send(_mobiles, txtSmsContent.Text.Trim(), 2, out msg);
                if (result)
                {
                    AddAdminLog(Vincent._DTcms.DTEnums.ActionEnum.Add.ToString(), "发送手机短信"); //记录日志
                    JscriptMsg(msg, "issue_coupon.aspx", "Success");
                    return;
                }
                JscriptMsg(msg, "", "Error");
                return;
            }
        }

    }
}