using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vincent;

namespace BuysingooShop.Admin.users
{
    public partial class user_edit : Web.UI.ManagePage
    {
        string defaultpassword = "0|0|0|0"; //默认显示密码
        protected string action = Vincent._DTcms.DTEnums.ActionEnum.Add.ToString(); //操作类型
        private int id = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
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
                if (!new BLL.users().Exists(this.id))
                {
                    JscriptMsg("信息不存在或已被删除！", "back", "Error");
                    return;
                }
            }
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("user_list", Vincent._DTcms.DTEnums.ActionEnum.View.ToString()); //检查权限
                TreeBind("is_lock=0"); //绑定类别

                TreeBindLevel("isDelete = 0 ");
                if (action == Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(this.id);
                }
            }
        }

        #region 绑定上线=================================
        private void TreeBindLevel(string strWhere)
        {
            //BLL.users bll = new BLL.users();
            //DataTable dt = bll.GetList(0, strWhere, "id asc").Tables[0];

            //this.DropDownList1.Items.Clear();
            //this.DropDownList1.Items.Add(new ListItem("请选择上级...", ""));
            //this.DropDownList1.Items.Add(new ListItem("公司", "0"));
            //foreach (DataRow dr in dt.Rows)
            //{
            //    if (dr["nick_name"].ToString() != "")
            //    {
            //        this.DropDownList1.Items.Add(new ListItem(dr["nick_name"].ToString(), dr["id"].ToString()));
            //    }
            //}
        }
        #endregion

        #region 绑定类别=================================
        private void TreeBind(string strWhere)
        {
            BLL.user_groups bll = new BLL.user_groups();
            DataTable dt = bll.GetList(0, strWhere, "grade asc,id asc").Tables[0];

            this.ddlGroupId.Items.Clear();
            this.ddlGroupId.Items.Add(new ListItem("请选择组别...", ""));
            foreach (DataRow dr in dt.Rows)
            {
                this.ddlGroupId.Items.Add(new ListItem(dr["title"].ToString(), dr["id"].ToString()));
            }
        }
        #endregion

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            BLL.users bll = new BLL.users();
            Model.users model = bll.GetModel(_id);

            provinces.Value = model.Provinces;
            provinces1.Value = model.Provinces;
            city.Value = model.City;
            city1.Value = model.City;

            //DropDownList1.SelectedValue = model.PreId.ToString();
            ddlGroupId.SelectedValue = model.group_id.ToString();
            rblStatus.SelectedValue = model.status.ToString();
            txtUserName.Text = model.user_name;
            txtUserName.ReadOnly = true;
            txtUserName.Attributes.Remove("ajaxurl");
            if (!string.IsNullOrEmpty(model.password))
            {
                txtPassword.Attributes["value"] = txtPassword1.Attributes["value"] = defaultpassword;
            }
            txtEmail.Text = model.email;
            txtNickName.Text = model.nick_name;
            txtAvatar.Text = model.avatar;
            rblSex.SelectedValue = model.sex;
            if (model.birthday != null)
            {
                txtBirthday.Text = model.birthday.GetValueOrDefault().ToString("yyyy-M-d");
            }
            txtTelphone.Text = model.telphone;
            txtMobile.Text = model.mobile;
            txtQQ.Text = model.qq;
            txtAddress.Text = model.address;
            txtAmount.Text = model.amount.ToString();
            txtPoint.Text = model.point.ToString();
            //txtExp.Text = model.exp.ToString();
            lblRegTime.Text = model.reg_time.ToString();
            lblRegIP.Text = model.reg_ip.ToString();
            //查找最近登录信息
            Model.user_login_log logModel = new BLL.user_login_log().GetLastModel(model.user_name);
            if (logModel != null)
            {
                lblLastTime.Text = logModel.login_time.ToString();
                lblLastIP.Text = logModel.login_ip;
            }
        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            bool result = false;
            Model.users model = new Model.users();
            BLL.users bll = new BLL.users();

            model.group_id = int.Parse(ddlGroupId.SelectedValue);

            //只允许加团长
            //if(model.group_id != 4){
            //    JscriptMsg("只允许添加团长级别的用户！", "", "Error");
            //    return false;
            //}
            model.PreId = 0;    // int.Parse(DropDownList1.SelectedValue); ;         //团长只允许放在 公司下面
            model.Leftor_right = 0;     //团长不区分左右区

            model.Provinces = this.provinces1.Value;
            model.City = this.city1.Value;

            model.status = int.Parse(rblStatus.SelectedValue);
            //检测用户名是否重复
            if (bll.Exists(txtUserName.Text.Trim()))
            {
                return false;
            }
            model.user_name = Vincent._DTcms.Utils.DropHTML(txtUserName.Text.Trim());
            //获得6位的salt加密字符串
            model.salt = Vincent._DTcms.Utils.GetCheckCode(6);
            //以随机生成的6位字符串做为密钥加密
            model.password = _DESEncrypt.Encrypt(txtPassword.Text.Trim(), model.salt);
            model.email = Vincent._DTcms.Utils.DropHTML(txtEmail.Text);
            model.nick_name = Vincent._DTcms.Utils.DropHTML(txtNickName.Text);
            model.avatar = Vincent._DTcms.Utils.DropHTML(txtAvatar.Text);
            model.sex = rblSex.SelectedValue;
            DateTime _birthday;
            if (DateTime.TryParse(txtBirthday.Text.Trim(), out _birthday))
            {
                model.birthday = _birthday;
            }
            model.telphone = Vincent._DTcms.Utils.DropHTML(txtTelphone.Text.Trim());
            model.mobile = Vincent._DTcms.Utils.DropHTML(txtMobile.Text.Trim());
            model.qq = Vincent._DTcms.Utils.DropHTML(txtQQ.Text);
            model.address = Vincent._DTcms.Utils.DropHTML(txtAddress.Text.Trim());
            model.amount = decimal.Parse(txtAmount.Text.Trim());
            model.point = int.Parse(txtPoint.Text.Trim());
            //model.exp = int.Parse(txtExp.Text.Trim());
            model.reg_time = DateTime.Now;
            model.reg_ip = Vincent._DTcms.DTRequest.GetIP();

            if (bll.Add(model, 1) > 0)
            {
                AddAdminLog(Vincent._DTcms.DTEnums.ActionEnum.Add.ToString(), "添加用户:" + model.user_name); //记录日志
                result = true;
            }
            return result;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            bool result = false;
            BLL.users bll = new BLL.users();
            Model.users model = bll.GetModel(_id);

            model.Provinces = this.provinces1.Value;
            model.City = this.city1.Value;

            model.PreId = 0;    // int.Parse(DropDownList1.SelectedValue);
            model.group_id = int.Parse(ddlGroupId.SelectedValue);
            model.status = int.Parse(rblStatus.SelectedValue);
            //判断密码是否更改
            if (txtPassword.Text.Trim() != defaultpassword)
            {
                //获取用户已生成的salt作为密钥加密
                model.password = _DESEncrypt.Encrypt(txtPassword.Text.Trim(), model.salt);
            }
            model.email = Vincent._DTcms.Utils.DropHTML(txtEmail.Text);
            model.nick_name = Vincent._DTcms.Utils.DropHTML(txtNickName.Text);
            model.avatar = Vincent._DTcms.Utils.DropHTML(txtAvatar.Text);
            model.sex = rblSex.SelectedValue;
            DateTime _birthday;
            if (DateTime.TryParse(txtBirthday.Text.Trim(), out _birthday))
            {
                model.birthday = _birthday;
            }
            model.telphone = Vincent._DTcms.Utils.DropHTML(txtTelphone.Text.Trim());
            model.mobile = Vincent._DTcms.Utils.DropHTML(txtMobile.Text.Trim());
            model.qq = Vincent._DTcms.Utils.DropHTML(txtQQ.Text);
            model.address = Vincent._DTcms.Utils.DropHTML(txtAddress.Text.Trim());
            model.amount = Vincent._DTcms.Utils.StrToDecimal(txtAmount.Text.Trim(), 0);
            model.point = Vincent._DTcms.Utils.StrToInt(txtPoint.Text.Trim(), 0);
            //model.exp = Vincent._DTcms.Utils.StrToInt(txtExp.Text.Trim(), 0);

            if (bll.Update(model))
            {
                AddAdminLog(Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString(), "修改用户信息:" + model.user_name); //记录日志
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
                ChkAdminLevel("user_list", Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit(this.id))
                {
                    JscriptMsg("保存过程中发生错误！", "", "Error");
                    return;
                }
                JscriptMsg("修改用户成功！", "user_list.aspx", "Success");
            }
            else //添加
            {
                ChkAdminLevel("user_list", Vincent._DTcms.DTEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误！", "", "Error");
                    return;
                }
                JscriptMsg("添加用户成功！", "user_list.aspx", "Success");
            }
        }
    }
}