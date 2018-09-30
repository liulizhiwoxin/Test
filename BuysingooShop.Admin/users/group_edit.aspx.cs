using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BuysingooShop.BLL;
using Vincent;


namespace BuysingooShop.Admin.users
{
    /// <summary>
    /// 会员组别（增加或编辑）
    /// </summary>
    public partial class group_edit : Web.UI.ManagePage
    {
        private string action = Vincent._DTcms.DTEnums.ActionEnum.Add.ToString(); //操作类型
        private int id = 0;

        // 页面加载操作
        protected void Page_Load(object sender, EventArgs e)
        {
            //取到操作类型
            string _action = Vincent._DTcms.DTRequest.GetQueryString("action");
            if (!string.IsNullOrEmpty(_action) && _action == Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString())
            {
                this.action = Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString();//修改类型
                this.id = Vincent._DTcms.DTRequest.GetQueryInt("id");
                if (this.id == 0)
                {
                    JscriptMsg("传输参数不正确！", "back", "Error");
                    return;
                }
                if (!new BLL.user_groups().Exists(this.id))
                {
                    JscriptMsg("用户组不存在或已被删除！", "back", "Error");
                    return;
                }
            }
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("user_group", Vincent._DTcms.DTEnums.ActionEnum.View.ToString()); //检查权限
                if (action == Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(this.id);
                }
            }

        }

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            BLL.user_groups bll = new BLL.user_groups();
            Model.user_groups model = bll.GetModel(_id);
            txtTitle.Text = model.title;
            if (model.is_lock == 1)
            {
                rblIsLock.Checked = true;
            }
            if (model.is_default == 1)
            {
                rblIsDefault.Checked = true;
            }
            if (model.is_upgrade == 1)
            {
                rblIsUpgrade.Checked = true;
            }
            txtGrade.Text = model.grade.ToString();
            txtUpgradeExp.Text = model.upgrade_exp.ToString();
            txtAmount.Text = model.amount.ToString();
            txtPoint.Text = model.point.ToString();
            txtDiscount.Text = model.discount.ToString();

            //充值活动
            if (siteConfig.rechangestatus == 1)
            {
                dlrpter.Visible = true;
            }

            BuysingooShop.BLL.activity bllAct = new BuysingooShop.BLL.activity();
            DataTable table = bllAct.GetList(0, "id=" + model.id, "id asc").Tables[0];
            rptPrice.DataSource = table;
            rptPrice.DataBind();

        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            bool result = false;
            Model.user_groups model = new Model.user_groups();
            BLL.user_groups bll = new BLL.user_groups();

            model.title = txtTitle.Text.Trim();
            model.is_lock = 0;
            if (rblIsLock.Checked == true)
            {
                model.is_lock = 1;
            }
            model.is_default = 0;
            if (rblIsDefault.Checked == true)
            {
                model.is_default = 1;
            }
            model.is_upgrade = 0;
            if (rblIsUpgrade.Checked == true)
            {
                model.is_upgrade = 1;
            }
            model.grade = int.Parse(txtGrade.Text.Trim());
            model.upgrade_exp = int.Parse(txtUpgradeExp.Text.Trim());
            model.amount = decimal.Parse(txtAmount.Text.Trim());
            model.point = int.Parse(txtPoint.Text.Trim());
            model.discount = int.Parse(txtDiscount.Text.Trim());

            //充值活动
            if (siteConfig.rechangestatus == 1)
            {
                dlrpter.Visible = true;
                List<Model.activity> list = new List<Model.activity>();
                string[] itemfieldsArr = Request.Form.GetValues("itemfields");
                string[] itemvaluesArr = Request.Form.GetValues("itemvalues");

                if (itemfieldsArr != null && itemvaluesArr != null)
                {
                    for (int i = 0; i < itemfieldsArr.Length; i++)
                    {
                        list.Add(new Model.activity { fields = itemfieldsArr[i], ID = model.id, value = itemvaluesArr[i] });
                    }
                }
                model.Acticity = list;
            }

            if (bll.Add(model) > 0)
            {
                AddAdminLog(Vincent._DTcms.DTEnums.ActionEnum.Add.ToString(), "添加用户组:" + model.title); //记录日志
                result = true;
            }
            return result;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            bool result = false;
            BLL.user_groups bll = new BLL.user_groups();
            Model.user_groups model = bll.GetModel(_id);

            model.title = txtTitle.Text.Trim();
            model.is_lock = 0;
            if (rblIsLock.Checked == true)
            {
                model.is_lock = 1;
            }
            model.is_default = 0;
            if (rblIsDefault.Checked == true)
            {
                model.is_default = 1;
            }
            model.is_upgrade = 0;
            if (rblIsUpgrade.Checked == true)
            {
                model.is_upgrade = 1;
            }
            model.grade = int.Parse(txtGrade.Text.Trim());
            model.upgrade_exp = int.Parse(txtUpgradeExp.Text.Trim());
            model.amount = decimal.Parse(txtAmount.Text.Trim());
            model.point = int.Parse(txtPoint.Text.Trim());
            model.discount = int.Parse(txtDiscount.Text.Trim());

            //充值活动
            if (siteConfig.rechangestatus == 1)
            {
                dlrpter.Visible = true;
                List<Model.activity> list = new List<Model.activity>();
                string[] itemidArr = Request.Form.GetValues("itemid");
                string[] itemfieldsArr = Request.Form.GetValues("itemfields");
                string[] itemvaluesArr = Request.Form.GetValues("itemvalues");

                if (itemfieldsArr != null && itemvaluesArr != null)
                {
                    for (int i = 0; i < itemfieldsArr.Length; i++)
                    {
                        list.Add(new Model.activity 
                        {
                            ID = int.Parse(itemidArr[i].ToString()),
                            fields = itemfieldsArr[i],
                            value = itemvaluesArr[i] 
                        });
                    }
                }
                model.Acticity = list;
            }

            if (bll.Update(model))
            {
                AddAdminLog(Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString(), "修改用户组:" + model.title); //记录日志
                result = true;
            }

            return result;
        }
        #endregion


        // 提交保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (action == Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString()) //修改
            {
                ChkAdminLevel("user_group", Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit(this.id))
                {
                    JscriptMsg("保存过程中发生错误！", "", "Error");
                    return;
                }
                JscriptMsg("修改用户组成功！", "group_list.aspx", "Success");
            }
            else //添加
            {
                ChkAdminLevel("user_group", Vincent._DTcms.DTEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误！", "", "Error");
                    return;
                }
                JscriptMsg("添加用户组成功！", "group_list.aspx", "Success");
            }
        }
    }
}