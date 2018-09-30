using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vincent;

namespace BuysingooShop.Admin.actively
{
    public partial class promotion_edit : Web.UI.ManagePage
    {
        //string defaultpassword = "0|0|0|0"; //默认显示密码
        //private string action = Vincent._DTcms.DTEnums.ActionEnum.Add.ToString(); //操作类型
        //private int id = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            //string _action = Vincent._DTcms.DTRequest.GetQueryString("action");
            //if (!string.IsNullOrEmpty(_action) && _action == Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString())
            //{
            //    this.action = Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString();//修改类型
            //    if (!int.TryParse(Request.QueryString["id"] as string, out this.id))
            //    {
            //        JscriptMsg("传输参数不正确！", "back", "Error");
            //        return;
            //    }
            //    if (!new BLL.activity().Exists(this.id))
            //    {
            //        JscriptMsg("记录不存在或已被删除！", "back", "Error");
            //        return;
            //    }
            //}
            //if (!Page.IsPostBack)
            //{
            //    ChkAdminLevel("promotion_list", Vincent._DTcms.DTEnums.ActionEnum.View.ToString()); //检查权限
            //    Model.activity model = GetAdminInfo(); //取得管理员信息
            //    //RoleBind(ddlRoleId, model.role_type);//绑定角色
            //    if (action == Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString()) //修改
            //    {
            //        //ShowInfo(this.id);
            //    }
            //}
            WineryBind(cblItem);//绑定商品类别
        }

        //#region 角色类型=================================
        //private void RoleBind(DropDownList ddl, int role_type)
        //{
        //    BLL.activity bll = new BLL.activity();
        //    DataTable dt = bll.GetList("").Tables[0];

        //    ddl.Items.Clear();
        //    ddl.Items.Add(new ListItem("请选择活动...", ""));
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        if (ddl.Items.Contains(dr["type"] as ListItem))
        //        {
        //            ddl.Items.Add(new ListItem(dr["remark"].ToString(), dr["type"].ToString()));
        //        }
        //    }
        //    ddl.Items.Add(new ListItem("自定义活动", "0"));
        //}
        //#endregion
        
        //#region 绑定商品类别=================================
        private void WineryBind(CheckBoxList ckBox)
        {
            BLL.article_category bll = new BLL.article_category();
            DataTable dt = bll.GetList(0,2);
            
            ckBox.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                ckBox.Items.Add(new ListItem(dr["title"].ToString(), dr["id"].ToString()));
            }
        }
        //#endregion
        
        #region 增加操作=================================
        private bool DoAdd()
        {
            //Model.activity model = new Model.activity();
            //BLL.activity bll = new BLL.activity();
            //model.fields=
            //model.role_type = new BLL.activity().GetModel(model.role_id).role_type;
            //if (cbIsLock.Checked == true)
            //{
            //    model.is_lock = 0;
            //}
            //else
            //{
            //    model.is_lock = 1;
            //}
            ////检测用户名是否重复
            //if (bll.Exists(txtUserName.Text.Trim()))
            //{
            //    return false;
            //}
            //model.user_name = txtUserName.Text.Trim();
            ////获得6位的salt加密字符串
            //model.salt = Vincent._DTcms.Utils.GetCheckCode(6);
            ////以随机生成的6位字符串做为密钥加密
            //model.password = _DESEncrypt.Encrypt(txtPassword.Text.Trim(), model.salt);
            //model.real_name = txtRealName.Text.Trim();
            //model.telephone = txtTelephone.Text.Trim();
            //model.email = txtEmail.Text.Trim();
            //model.add_time = DateTime.Now;
            //if (model.role_id == 3)//自定义
            //{
            //    model.brand_id = int.Parse(ddlBrandId.SelectedValue);
            //}
            //else if (model.role_id == 4)//经销商
            //{
            //    model.str_code = txtStrCode.Text;
            //    model.str_code_rage = txtCodeRage.Text;
            //    model.winery_id = int.Parse(ddlWineryId.SelectedValue);
            //    BuysingooShop.Model.activity manModel = new BuysingooShop.BLL.activity().GetModel(model.winery_id);
            //    if (manModel != null)
            //    {
            //        model.brand_id = manModel.brand_id;
            //    }

            //}
            //else if (model.role_id == 5)//设计师
            //{
            //    model.age = int.Parse(txtAge.Text);
            //    model.workAge = int.Parse(txtWorkAge.Text);
            //    model.img_url = txtImgUrl.Text;
            //    model.remark = txtRemark.Text;
            //    model.QQ = txtQQ.Text;
            //    if (txtStyle.Text != "")
            //    {
            //        model.style = txtStyle.Text;
            //    }
            //}

            //if (bll.Add(model) > 0)
            //{
            //    AddAdminLog(Vincent._DTcms.DTEnums.ActionEnum.Add.ToString(), "添加管理员:" + model.user_name); //记录日志
            //    return true;
            //}
            return false;
        }
        #endregion

        //#region 修改操作=================================
        //private bool DoEdit(int _id)
        //{
        //    //BLL.activity bll = new BLL.activity();
        //    //Model.activity model = bll.GetModel(_id);

        //    //model.role_id = int.Parse(ddlRoleId.SelectedValue);
        //    //model.role_type = new BLL.activity().GetModel(model.role_id).role_type;
        //    //if (cbIsLock.Checked == true)
        //    //{
        //    //    model.is_lock = 0;
        //    //}
        //    //else
        //    //{
        //    //    model.is_lock = 1;
        //    //}
        //    ////判断密码是否更改
        //    //if (txtPassword.Text.Trim() != defaultpassword)
        //    //{
        //    //    //获取用户已生成的salt作为密钥加密
        //    //    model.password = _DESEncrypt.Encrypt(txtPassword.Text.Trim(), model.salt);
        //    //}
        //    //model.real_name = txtRealName.Text.Trim();
        //    //model.telephone = txtTelephone.Text.Trim();
        //    //model.email = txtEmail.Text.Trim();
        //    //if (model.role_id == 3)//自定义
        //    //{
        //    //    model.brand_id = int.Parse(ddlBrandId.SelectedValue);
        //    //}
        //    //else if (model.role_id == 4)//经销商
        //    //{
        //    //    model.str_code = txtStrCode.Text;
        //    //    model.str_code_rage = txtCodeRage.Text;
        //    //    model.winery_id = int.Parse(ddlWineryId.SelectedValue);
        //    //    BuysingooShop.Model.activity manModel = new BuysingooShop.BLL.activity().GetModel(model.winery_id);
        //    //    if (manModel != null)
        //    //    {
        //    //        model.brand_id = manModel.brand_id;
        //    //    }
        //    //}
        //    //else if (model.role_id == 5)//设计师
        //    //{
        //    //    model.age = int.Parse(txtAge.Text);
        //    //    model.workAge = int.Parse(txtWorkAge.Text);
        //    //    model.img_url = txtImgUrl.Text;
        //    //    model.remark = txtRemark.Text;
        //    //    model.QQ = txtQQ.Text;
        //    //    if (txtStyle.Text != "")
        //    //    {
        //    //        model.style = txtStyle.Text;
        //    //    }
        //    //}

        //    //if (bll.Update(model))
        //    //{
        //    //    AddAdminLog(Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString(), "修改管理员:" + model.user_name); //记录日志
        //    //    return true;
        //    //}

        //    return false;
        //}
        //#endregion

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //if (action == Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString()) //修改
            //{
            //    ChkAdminLevel("promotion_list", Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString()); //检查权限
            //    if (!DoEdit(this.id))
            //    {
            //        JscriptMsg("保存过程中发生错误！", "", "Error");
            //        return;
            //    }
            //    JscriptMsg("修改管理员信息成功！", "promotion_list.aspx", "Success");
            //}
            //else //添加
            //{
            //    ChkAdminLevel("promotion_list", Vincent._DTcms.DTEnums.ActionEnum.Add.ToString()); //检查权限
            //    if (!DoAdd())
            //    {
            //        JscriptMsg("保存过程中发生错误！", "", "Error");
            //        return;
            //    }
            //    JscriptMsg("添加管理员信息成功！", "promotion_list.aspx", "Success");
            //}
        }
    }
}