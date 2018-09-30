using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using Vincent;

namespace BuysingooShop.Admin.outlet
{
    public partial class openarea_edit : Web.UI.ManagePage
    {
        private string action = Vincent._DTcms.DTEnums.ActionEnum.Add.ToString(); //操作类型
        protected string channel_name = string.Empty; //频道名称
        protected int channel_id;
        private int id = 0;

        // 页面初始化事件
        protected void Page_Init(object sernder, EventArgs e)
        {
            CreateOtherField(this.channel_id); //动态生成相应的扩展字段
        }

        //页面加载事件
        protected void Page_Load(object sender, EventArgs e)
        {
            string _action = Vincent._DTcms.DTRequest.GetQueryString("action");

            this.channel_name = new BLL.channel().GetChannelName(this.channel_id); //取得频道名称

            if (!string.IsNullOrEmpty(_action) && _action == Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString())
            {
                this.action = Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString();//修改类型
                this.id = Vincent._DTcms.DTRequest.GetQueryInt("id");
                if (this.id == 0)
                {
                    JscriptMsg("传输参数不正确！", "back", "Error");
                    return;
                }
                if (!new BLL.openarea().Exists(this.id))
                {
                    JscriptMsg("信息不存在或已被删除！", "back", "Error");
                    return;
                }
            }
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("openarea_edit", Vincent._DTcms.DTEnums.ActionEnum.View.ToString()); //检查权限
                ShowSysField(this.channel_id); //显示相应的默认控件
                GroupBind(""); //绑定用户组
                //TreeBind(this.channel_id); //绑定类别
                if (action == Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(this.id);
                }
            }
        }



        #region 创建其它扩展字段=========================
        private void CreateOtherField(int _channel_id)
        {
            List<Model.article_attribute_field> ls = new BLL.article_attribute_field().GetModelList(this.channel_id, "is_sys=0");
            if (ls.Count > 0)
            {
                field_tab_item.Visible = true;
                field_tab_content.Visible = true;
            }
            foreach (Model.article_attribute_field modelt in ls)
            {
                //创建一个dl标签
                HtmlGenericControl htmlDL = new HtmlGenericControl("dl");
                HtmlGenericControl htmlDT = new HtmlGenericControl("dt");
                HtmlGenericControl htmlDD = new HtmlGenericControl("dd");
                htmlDT.InnerHtml = modelt.title;

                switch (modelt.control_type)
                {
                    case "single-text": //单行文本
                        //创建一个TextBox控件
                        TextBox txtControl = new TextBox();
                        txtControl.ID = "field_control_" + modelt.name;
                        //CSS样式及TextMode设置
                        if (modelt.control_type == "single-text") //单行
                        {
                            txtControl.CssClass = "input normal";
                            //是否密码框
                            if (modelt.is_password == 1)
                            {
                                txtControl.TextMode = TextBoxMode.Password;
                            }
                        }
                        else if (modelt.control_type == "multi-text") //多行
                        {
                            txtControl.CssClass = "input";
                            txtControl.TextMode = TextBoxMode.MultiLine;
                        }
                        else if (modelt.control_type == "number") //数字
                        {
                            txtControl.CssClass = "input small";
                        }
                        else if (modelt.control_type == "images") //图片
                        {
                            txtControl.CssClass = "input normal upload-path";
                        }
                        //设置默认值
                        txtControl.Text = modelt.default_value;
                        //验证提示信息
                        if (!string.IsNullOrEmpty(modelt.valid_tip_msg))
                        {
                            txtControl.Attributes.Add("tipmsg", modelt.valid_tip_msg);
                        }
                        //验证失败提示信息
                        if (!string.IsNullOrEmpty(modelt.valid_error_msg))
                        {
                            txtControl.Attributes.Add("errormsg", modelt.valid_error_msg);
                        }
                        //验证表达式
                        if (!string.IsNullOrEmpty(modelt.valid_pattern))
                        {
                            txtControl.Attributes.Add("datatype", modelt.valid_pattern);
                            txtControl.Attributes.Add("sucmsg", " ");
                        }
                        //创建一个Label控件
                        Label labelControl = new Label();
                        labelControl.CssClass = "Validform_checktip";
                        labelControl.Text = modelt.valid_tip_msg;

                        //将控件添加至DD中
                        htmlDD.Controls.Add(txtControl);
                        //如果是图片则添加上传按钮
                        if (modelt.control_type == "images")
                        {
                            HtmlGenericControl htmlBtn = new HtmlGenericControl("div");
                            htmlBtn.Attributes.Add("class", "upload-box upload-img");
                            htmlBtn.Attributes.Add("style", "margin-left:4px;");
                            htmlDD.Controls.Add(htmlBtn);
                        }
                        htmlDD.Controls.Add(labelControl);
                        break;
                    case "multi-text": //多行文本
                        goto case "single-text";
                    case "editor": //编辑器
                        HtmlTextArea txtTextArea = new HtmlTextArea();
                        txtTextArea.ID = "field_control_" + modelt.name;
                        txtTextArea.Attributes.Add("style", "visibility:hidden;");
                        //是否简洁型编辑器
                        if (modelt.editor_type == 1)
                        {
                            txtTextArea.Attributes.Add("class", "editor-mini");
                        }
                        else
                        {
                            txtTextArea.Attributes.Add("class", "editor");
                        }
                        txtTextArea.Value = modelt.default_value; //默认值
                        //验证提示信息
                        if (!string.IsNullOrEmpty(modelt.valid_tip_msg))
                        {
                            txtTextArea.Attributes.Add("tipmsg", modelt.valid_tip_msg);
                        }
                        //验证失败提示信息
                        if (!string.IsNullOrEmpty(modelt.valid_error_msg))
                        {
                            txtTextArea.Attributes.Add("errormsg", modelt.valid_error_msg);
                        }
                        //验证表达式
                        if (!string.IsNullOrEmpty(modelt.valid_pattern))
                        {
                            txtTextArea.Attributes.Add("datatype", modelt.valid_pattern);
                            txtTextArea.Attributes.Add("sucmsg", " ");
                        }
                        //创建一个Label控件
                        Label labelControl2 = new Label();
                        labelControl2.CssClass = "Validform_checktip";
                        labelControl2.Text = modelt.valid_tip_msg;
                        //将控件添加至DD中
                        htmlDD.Controls.Add(txtTextArea);
                        htmlDD.Controls.Add(labelControl2);
                        break;
                    case "images": //图片上传
                        goto case "single-text";
                    case "number": //数字
                        goto case "single-text";
                    case "checkbox": //复选框
                        CheckBox cbControl = new CheckBox();
                        cbControl.ID = "field_control_" + modelt.name;
                        //默认值
                        if (modelt.default_value == "1")
                        {
                            cbControl.Checked = true;
                        }
                        HtmlGenericControl htmlDiv1 = new HtmlGenericControl("div");
                        htmlDiv1.Attributes.Add("class", "rule-single-checkbox");
                        htmlDiv1.Controls.Add(cbControl);
                        //将控件添加至DD中
                        htmlDD.Controls.Add(htmlDiv1);
                        if (!string.IsNullOrEmpty(modelt.valid_tip_msg))
                        {
                            //创建一个Label控件
                            Label labelControl3 = new Label();
                            labelControl3.CssClass = "Validform_checktip";
                            labelControl3.Text = modelt.valid_tip_msg;
                            htmlDD.Controls.Add(labelControl3);
                        }
                        break;
                    case "multi-radio": //多项单选
                        RadioButtonList rblControl = new RadioButtonList();
                        rblControl.ID = "field_control_" + modelt.name;
                        rblControl.RepeatDirection = RepeatDirection.Horizontal;
                        rblControl.RepeatLayout = RepeatLayout.Flow;
                        HtmlGenericControl htmlDiv2 = new HtmlGenericControl("div");
                        htmlDiv2.Attributes.Add("class", "rule-multi-radio");
                        htmlDiv2.Controls.Add(rblControl);
                        //赋值选项
                        string[] valArr = modelt.item_option.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
                        for (int i = 0; i < valArr.Length; i++)
                        {
                            string[] valItemArr = valArr[i].Split('|');
                            if (valItemArr.Length == 2)
                            {
                                rblControl.Items.Add(new ListItem(valItemArr[0], valItemArr[1]));
                            }
                        }
                        rblControl.SelectedValue = modelt.default_value; //默认值
                        //创建一个Label控件
                        Label labelControl4 = new Label();
                        labelControl4.CssClass = "Validform_checktip";
                        labelControl4.Text = modelt.valid_tip_msg;
                        //将控件添加至DD中
                        htmlDD.Controls.Add(htmlDiv2);
                        htmlDD.Controls.Add(labelControl4);
                        break;
                    case "multi-checkbox": //多项多选
                        CheckBoxList cblControl = new CheckBoxList();
                        cblControl.ID = "field_control_" + modelt.name;
                        cblControl.RepeatDirection = RepeatDirection.Horizontal;
                        cblControl.RepeatLayout = RepeatLayout.Flow;
                        HtmlGenericControl htmlDiv3 = new HtmlGenericControl("div");
                        htmlDiv3.Attributes.Add("class", "rule-multi-checkbox");
                        htmlDiv3.Controls.Add(cblControl);
                        //赋值选项
                        string[] valArr2 = modelt.item_option.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
                        for (int i = 0; i < valArr2.Length; i++)
                        {
                            string[] valItemArr2 = valArr2[i].Split('|');
                            if (valItemArr2.Length == 2)
                            {
                                cblControl.Items.Add(new ListItem(valItemArr2[0], valItemArr2[1]));
                            }
                        }
                        cblControl.SelectedValue = modelt.default_value; //默认值
                        //创建一个Label控件
                        Label labelControl5 = new Label();
                        labelControl5.CssClass = "Validform_checktip";
                        labelControl5.Text = modelt.valid_tip_msg;
                        //将控件添加至DD中
                        htmlDD.Controls.Add(htmlDiv3);
                        htmlDD.Controls.Add(labelControl5);
                        break;
                }

                //将DT和DD添加到DL中
                htmlDL.Controls.Add(htmlDT);
                htmlDL.Controls.Add(htmlDD);
                //将DL添加至field_tab_content中
                field_tab_content.Controls.Add(htmlDL);
            }
        }
        #endregion

        #region 显示默认扩展字段=========================
        private void ShowSysField(int _channel_id)
        {
            //查找该频道所选的默认字段
            List<Model.article_attribute_field> ls = new BLL.article_attribute_field().GetModelList(this.channel_id, "is_sys=1");
            if (ls.Count > 0)
            {
                field_tab_item.Visible = true;
                field_tab_content.Visible = true;
            }
            foreach (Model.article_attribute_field modelt in ls)
            {
                //查找相应的控件，如找到则显示
                HtmlGenericControl htmlDiv = FindControl("div_" + modelt.name) as HtmlGenericControl;
                if (htmlDiv != null)
                {
                    htmlDiv.Visible = true;
                    ((Label)htmlDiv.FindControl("div_" + modelt.name + "_title")).Text = modelt.title;
                    ((TextBox)htmlDiv.FindControl("field_control_" + modelt.name)).Text = modelt.default_value;
                    ((Label)htmlDiv.FindControl("div_" + modelt.name + "_tip")).Text = modelt.valid_tip_msg;
                }
            }
        }
        #endregion

        /*#region 绑定类别=================================
        private void TreeBind(int _channel_id)
        {
            BLL.article_category bll = new BLL.article_category();
            DataTable dt = bll.GetList(0, _channel_id);

            this.ddlCategoryId.Items.Clear();
            this.ddlCategoryId.Items.Add(new ListItem("请选择类别...", ""));
            foreach (DataRow dr in dt.Rows)
            {
                string Id = dr["id"].ToString();
                int ClassLayer = int.Parse(dr["class_layer"].ToString());
                string Title = dr["title"].ToString().Trim();

                if (ClassLayer == 1)
                {
                    this.ddlCategoryId.Items.Add(new ListItem(Title, Id));
                }
                else
                {
                    Title = "├ " + Title;
                    Title = Vincent._DTcms.Utils.StringOfChar(ClassLayer - 1, "　") + Title;
                    this.ddlCategoryId.Items.Add(new ListItem(Title, Id));
                }
            }
        }
        #endregion*/

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            BLL.openarea bll = new BLL.openarea();
            Model.openarea model = bll.GetModel(_id);

            provinces.Value = model.provinces;
            provinces1.Value = model.provinces;
            city.Value = model.city;
            city1.Value = model.city;
        }
        #endregion

        #region 绑定会员组===============================
        private void GroupBind(string strWhere)
        {
            //检查网站是否开启会员功能
            if (siteConfig.memberstatus == 0)
            {
                return;
            }
            //检查该频道是否开启会员组价格
            Model.channel model = new BLL.channel().GetModel(this.channel_id);
            if (model == null || model.is_group_price == 0)
            {
                return;
            }
            BLL.user_groups bll = new BLL.user_groups();
            DataSet ds = bll.GetList(0, strWhere, "grade asc,id desc");
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.rptPrice.DataSource = ds;
                this.rptPrice.DataBind();
            }
        }
        #endregion

        #region 扩展字段赋值=============================
        private Dictionary<string, string> SetFieldValues(int _channel_id)
        {
            DataTable dt = new BLL.article_attribute_field().GetList(_channel_id, "").Tables[0];
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (DataRow dr in dt.Rows)
            {
                //查找相应的控件
                switch (dr["control_type"].ToString())
                {
                    case "single-text": //单行文本
                        TextBox txtControl = FindControl("field_control_" + dr["name"].ToString()) as TextBox;
                        if (txtControl != null)
                        {
                            dic.Add(dr["name"].ToString(), txtControl.Text.Trim());

                        }
                        break;
                    case "multi-text": //多行文本
                        goto case "single-text";
                    case "editor": //编辑器
                        HtmlTextArea htmlTextAreaControl = FindControl("field_control_" + dr["name"].ToString()) as HtmlTextArea;
                        if (htmlTextAreaControl != null)
                        {
                            dic.Add(dr["name"].ToString(), htmlTextAreaControl.Value);
                        }
                        break;
                    case "images": //图片上传
                        goto case "single-text";
                    case "number": //数字
                        goto case "single-text";
                    case "checkbox": //复选框
                        CheckBox cbControl = FindControl("field_control_" + dr["name"].ToString()) as CheckBox;
                        if (cbControl != null)
                        {
                            if (cbControl.Checked == true)
                            {
                                dic.Add(dr["name"].ToString(), "1");
                            }
                            else
                            {
                                dic.Add(dr["name"].ToString(), "0");
                            }
                        }
                        break;
                    case "multi-radio": //多项单选
                        RadioButtonList rblControl = FindControl("field_control_" + dr["name"].ToString()) as RadioButtonList;
                        if (rblControl != null)
                        {
                            dic.Add(dr["name"].ToString(), rblControl.SelectedValue);
                        }
                        break;
                    case "multi-checkbox": //多项多选
                        CheckBoxList cblControl = FindControl("field_control_" + dr["name"].ToString()) as CheckBoxList;
                        if (cblControl != null)
                        {
                            StringBuilder tempStr = new StringBuilder();
                            for (int i = 0; i < cblControl.Items.Count; i++)
                            {
                                if (cblControl.Items[i].Selected)
                                {
                                    tempStr.Append(cblControl.Items[i].Value.Replace(',', '，') + ",");
                                }
                            }
                            dic.Add(dr["name"].ToString(), Vincent._DTcms.Utils.DelLastComma(tempStr.ToString()));
                        }
                        break;
                }
            }
            return dic;
        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            bool result = false;
            Model.openarea model = new Model.openarea();
            BLL.openarea bll = new BLL.openarea();
            if (this.provinces1.Value == "所在省份")
            {
                JscriptMsg("请选择省份！", "", "Error");
                return result;
            }
            else if (this.provinces1.Value == "北京市")
            {
                model.provinces = this.provinces1.Value;
                model.city = this.provinces1.Value;
            }
            else if (this.provinces1.Value == "天津市")
            {
                model.provinces = this.provinces1.Value;
                model.city = this.provinces1.Value;
            }
            else if (this.provinces1.Value == "上海市")
            {
                model.provinces = this.provinces1.Value;
                model.city = this.provinces1.Value;
            }
            else if (this.provinces1.Value == "重庆市")
            {
                model.provinces = this.provinces1.Value;
                model.city = this.provinces1.Value;
            }
            else
            {
                model.provinces = this.provinces1.Value;
                model.city = this.city1.Value;
            }

            



            if (bll.Add(model) > 0)
            {
                //开始生成缩略图咯

                //AddAdminLog(Vincent._DTcms.DTEnums.ActionEnum.Add.ToString(), "添加" + this.channel_name + "频道内容:" + model.title); //记录日志
                result = true;
            }
            return result;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            bool result = false;
            Model.openarea model = new Model.openarea();
            BLL.openarea bll = new BLL.openarea();
            if (this.provinces1.Value == "所在省份")
            {
                JscriptMsg("请选择省份！", "", "Error");
                return result;
            }
            else if (this.provinces1.Value == "北京市")
            {
                model.provinces = this.provinces1.Value;
                model.city = this.provinces1.Value;
            }
            else if (this.provinces1.Value == "天津市")
            {
                model.provinces = this.provinces1.Value;
                model.city = this.provinces1.Value;
            }
            else if (this.provinces1.Value == "上海市")
            {
                model.provinces = this.provinces1.Value;
                model.city = this.provinces1.Value;
            }
            else if (this.provinces1.Value == "重庆市")
            {
                model.provinces = this.provinces1.Value;
                model.city = this.provinces1.Value;
            }
            else
            {
                model.provinces = this.provinces1.Value;
                model.city = this.city1.Value;
            }

            model.id = _id;



            if (bll.Update(model))
            {
                //开始生成缩略图咯

                //AddAdminLog(Vincent._DTcms.DTEnums.ActionEnum.Add.ToString(), "添加" + this.channel_name + "频道内容:" + model.title); //记录日志
                result = true;
            }
            return result;
        }
        #endregion

        #region 保存操作=================================
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (action == Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString()) //修改
            {
                ChkAdminLevel("openarea_edit", Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit(this.id))
                {
                    JscriptMsg("保存过程中发生错误啦！", "", "Error");
                    return;
                }
                JscriptMsg("修改信息成功！", "openarea_list.aspx", "Success");
            }
            else //添加
            {
                ChkAdminLevel("openarea_edit", Vincent._DTcms.DTEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误啦！", "", "Error");
                    return;
                }
                JscriptMsg("添加信息成功！", "openarea_list.aspx", "Success");
            }
        }
        #endregion
    }
}