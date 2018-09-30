using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vincent;

namespace BuysingooShop.Admin.manager
{
    public partial class manager_edit : Web.UI.ManagePage
    {
        string defaultpassword = "0|0|0|0"; //默认显示密码
        private string action = Vincent._DTcms.DTEnums.ActionEnum.Add.ToString(); //操作类型
        private int id = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            string _action = Vincent._DTcms.DTRequest.GetQueryString("action");
            if (!string.IsNullOrEmpty(_action) && _action == Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString())
            {
                this.action = Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString();//修改类型
                if (!int.TryParse(Request.QueryString["id"] as string, out this.id))
                {
                    JscriptMsg("传输参数不正确！", "back", "Error");
                    return;
                }
                if (!new BLL.manager().Exists(this.id))
                {
                    JscriptMsg("记录不存在或已被删除！", "back", "Error");
                    return;
                }
            }
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("manager_list", Vincent._DTcms.DTEnums.ActionEnum.View.ToString()); //检查权限
                Model.manager model = GetAdminInfo(); //取得管理员信息
                RoleBind(ddlRoleId, model.role_type);//绑定角色
                //WineryBind(ddlWineryId, 3);//绑定酒厂
                BrandBind(ddlBrandId, 3);//店铺
                if (action == Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(this.id);
                }
            }
        }

        #region 角色类型=================================
        private void RoleBind(DropDownList ddl, int role_type)
        {
            BLL.manager_role bll = new BLL.manager_role();
            DataTable dt = bll.GetList("").Tables[0];

            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("请选择角色...", ""));
            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToInt32(dr["role_type"]) >= role_type)
                {
                    ddl.Items.Add(new ListItem(dr["role_name"].ToString(), dr["id"].ToString()));
                }
            }
        }
        #endregion
        
        #region 酒厂=================================
        private void WineryBind(DropDownList ddl, int role_id)
        {
            BLL.manager bll = new BLL.manager();
            DataTable dt = bll.GetList(0, "role_id=" + role_id, "add_time desc,id asc").Tables[0];

            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("请选择酒厂...", ""));
            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToInt32(dr["role_id"]) >= role_id)
                {
                    ddl.Items.Add(new ListItem(dr["user_name"].ToString(), dr["id"].ToString()));
                }
            }
        }
        #endregion
        
        #region 店铺=================================
        private void BrandBind(DropDownList ddl, int role_id)
        {
            BLL.outlet bll = new BLL.outlet();
            DataTable dt = bll.GetList(0, "0=0", "addtime desc,id asc").Tables[0];

            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("请选择店铺...", ""));
            foreach (DataRow dr in dt.Rows)
            {
                    ddl.Items.Add(new ListItem("(" + dr["provinces"].ToString() + dr["city"].ToString() + dr["area"].ToString() + ")" + dr["name"].ToString(), dr["id"].ToString()));
            }
        }
        #endregion

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            BLL.manager bll = new BLL.manager();
            Model.manager model = bll.GetModel(_id);
            ddlRoleId.SelectedValue = model.role_id.ToString();
            if (model.is_lock == 0)
            {
                cbIsLock.Checked = true;
            }
            else
            {
                cbIsLock.Checked = false;
            }
            txtUserName.Text = model.user_name;
            txtUserName.ReadOnly = true;
            txtUserName.Attributes.Remove("ajaxurl");
            if (!string.IsNullOrEmpty(model.password))
            {
                txtPassword.Attributes["value"] = txtPassword1.Attributes["value"] = defaultpassword;
            }
            txtRealName.Text = model.real_name;
            txtTelephone.Text = model.telephone;
            txtEmail.Text = model.email;

            provinces.Value = model.str_code;
            provinces1.Value = model.str_code;
            city.Value = model.str_code_rage;
            city1.Value = model.str_code_rage;

            if (model.role_id == 3)//酒厂
            {
                this.Brand.Style.Add("display", "block");
                ddlBrandId.Attributes.Add("datatype", "*");
                ddlBrandId.Attributes.Add("errormsg", "请选择店铺");
                ddlBrandId.Attributes.Add("sucmsg", " ");
                this.ddlBrandId.SelectedValue = model.brand_id.ToString();
                
            }
            else if (model.role_id == 4)//经销商
            {
                //this.Code.Style.Add("display", "block");
                txtStrCode.Text = model.str_code;
                //this.CodeRage.Style.Add("display", "block");
                txtCodeRage.Text = model.str_code_rage;
                //this.Winery.Style.Add("display", "block");
                //ddlWineryId.Attributes.Add("datatype", "*");
                //ddlWineryId.Attributes.Add("errormsg", "请选择酒厂");
                //ddlWineryId.Attributes.Add("sucmsg", " ");
                //ddlWineryId.SelectedValue = model.winery_id.ToString();
            }
            else if (model.role_id == 5)//设计师
            {
                this.dlAge.Style.Add("display", "block");
                txtAge.Text = model.age.ToString();
                this.dlWorkAge.Style.Add("display", "block");
                txtWorkAge.Text = model.workAge.ToString();
                this.dlstyle.Style.Add("display", "block");
                txtStyle.Text = model.style;
                this.dlImgUrl.Style.Add("display", "block");
                txtImgUrl.Text = model.img_url;
                this.dlRemark.Style.Add("display", "block");
                txtRemark.Text = model.remark;
                this.dlQQ.Style.Add("display","block");
                txtQQ.Text = model.QQ;
            }
        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            Model.manager model = new Model.manager();
            BLL.manager bll = new BLL.manager();
            model.role_id = int.Parse(ddlRoleId.SelectedValue);
            model.role_type = new BLL.manager_role().GetModel(model.role_id).role_type;
            if (cbIsLock.Checked == true)
            {
                model.is_lock = 0;
            }
            else
            {
                model.is_lock = 1;
            }
            //检测用户名是否重复
            if (bll.Exists(txtUserName.Text.Trim()))
            {
                return false;
            }
            model.user_name = txtUserName.Text.Trim();
            //获得6位的salt加密字符串
            model.salt = Vincent._DTcms.Utils.GetCheckCode(6);
            //以随机生成的6位字符串做为密钥加密
            model.password = _DESEncrypt.Encrypt(txtPassword.Text.Trim(), model.salt);
            model.real_name = txtRealName.Text.Trim();
            model.telephone = txtTelephone.Text.Trim();
            model.email = txtEmail.Text.Trim();
            model.add_time = DateTime.Now;
            if (model.role_id == 3)//酒厂
            {
                model.brand_id = int.Parse(ddlBrandId.SelectedValue);
            }

            model.str_code = this.provinces1.Value;
            model.str_code_rage = this.city1.Value;

            //else if (model.role_id == 4)//经销商
            //{
            //    model.str_code = txtStrCode.Text;
            //    model.str_code_rage = txtCodeRage.Text;
            //    model.winery_id=int.Parse(ddlWineryId.SelectedValue);
            //    BuysingooShop.Model.manager manModel = new BuysingooShop.BLL.manager().GetModel(model.winery_id);
            //    if (manModel != null)
            //    {
            //        model.brand_id = manModel.brand_id;
            //    }

            //}
            //else if(model.role_id==5)//设计师
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

            if (bll.Add(model) > 0)
            {
                AddAdminLog(Vincent._DTcms.DTEnums.ActionEnum.Add.ToString(), "添加管理员:" + model.user_name); //记录日志
                return true;
            }
            return false;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            bool result = false;
            BLL.manager bll = new BLL.manager();
            Model.manager model = bll.GetModel(_id);

            model.role_id = int.Parse(ddlRoleId.SelectedValue);
            model.role_type = new BLL.manager_role().GetModel(model.role_id).role_type;
            if (cbIsLock.Checked == true)
            {
                model.is_lock = 0;
            }
            else
            {
                model.is_lock = 1;
            }
            //判断密码是否更改
            if (txtPassword.Text.Trim() != defaultpassword)
            {
                //获取用户已生成的salt作为密钥加密
                model.password = _DESEncrypt.Encrypt(txtPassword.Text.Trim(), model.salt);
            }
            model.real_name = txtRealName.Text.Trim();
            model.telephone = txtTelephone.Text.Trim();
            model.email = txtEmail.Text.Trim();
            if (model.role_id == 3)//酒厂
            {
                model.brand_id = int.Parse(ddlBrandId.SelectedValue);
            }

            model.str_code = this.provinces1.Value;
            model.str_code_rage = this.city1.Value;

            //else if (model.role_id == 4)//经销商
            //{
            //    model.str_code = txtStrCode.Text;
            //    model.str_code_rage = txtCodeRage.Text;
            //    model.winery_id = int.Parse(ddlWineryId.SelectedValue);
            //    BuysingooShop.Model.manager manModel = new BuysingooShop.BLL.manager().GetModel(model.winery_id);
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

            if (bll.Update(model))
            {
                AddAdminLog(Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString(), "修改管理员:" + model.user_name); //记录日志
                result = true;
            }

            return result;
        }
        #endregion

        //保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (action == Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString()) //修改
            {
                ChkAdminLevel("manager_list", Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit(this.id))
                {
                    JscriptMsg("保存过程中发生错误！", "", "Error");
                    return;
                }
                JscriptMsg("修改管理员信息成功！", "manager_list.aspx", "Success");
            }
            else //添加
            {
                ChkAdminLevel("manager_list", Vincent._DTcms.DTEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误！", "", "Error");
                    return;
                }
                JscriptMsg("添加管理员信息成功！", "manager_list.aspx", "Success");
            }
        }
    }
}