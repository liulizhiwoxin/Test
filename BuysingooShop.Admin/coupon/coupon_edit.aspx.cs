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

namespace BuysingooShop.Admin.coupon
{
    public partial class coupon_edit : Web.UI.ManagePage
    {
        private string action = Vincent._DTcms.DTEnums.ActionEnum.Add.ToString(); //操作类型
        protected string channel_name = string.Empty; //频道名称
        protected int channel_id;


        //页面初始化事件
        protected void Page_Init(object sernder, EventArgs e)
        {
            this.channel_id = Vincent._DTcms.DTRequest.GetQueryInt("channel_id"); 
        }

        //页面加载事件
        protected void Page_Load(object sender, EventArgs e)
        {
            string _action = Vincent._DTcms.DTRequest.GetQueryString("action");
            
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("channel_" + this.channel_name + "_list", Vincent._DTcms.DTEnums.ActionEnum.View.ToString()); //检查权限
                ShowSysField(this.channel_id); //显示相应的默认控件
                GroupBind(""); //绑定用户组
                TreeBind(this.channel_id); //绑定类别
                if (action == Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString()) //修改
                {
                    //ShowInfo(this.id);
                }
            }
        }

        #region 显示默认扩展字段=========================
        private void ShowSysField(int _channel_id)
        {
            //查找该频道所选的默认字段
            List<Model.article_attribute_field> ls = new BLL.article_attribute_field().GetModelList(this.channel_id, "is_sys=1");
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

        #region 绑定类别=================================
        private void TreeBind(int _channel_id)
        {
            BLL.article_category bll = new BLL.article_category();
            DataTable dt = bll.GetList(0, _channel_id);

            //this.ddlCategoryId.Items.Clear();
            //this.ddlCategoryId.Items.Add(new ListItem("请选择类别...", ""));
            foreach (DataRow dr in dt.Rows)
            {
                string Id = dr["id"].ToString();
                int ClassLayer = int.Parse(dr["class_layer"].ToString());
                string Title = dr["title"].ToString().Trim();

                if (ClassLayer == 1)
                {
                    //this.ddlCategoryId.Items.Add(new ListItem(Title, Id));
                }
                else
                {
                    Title = "├ " + Title;
                    Title = Vincent._DTcms.Utils.StringOfChar(ClassLayer - 1, "　") + Title;
                    //this.ddlCategoryId.Items.Add(new ListItem(Title, Id));
                }
            }
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
            bool result = true;
            Model.user_coupon model = new Model.user_coupon();
            BLL.user_coupon bll = new BLL.user_coupon();
              
            model.title = txtTitle.Text.Trim();
            model.remark = txtRemark.Text.Trim();
            model.type = RadioBut1.Checked == true ? "平台优惠券" : "品牌优惠券";
            model.amount = Convert.ToDecimal(txtDecimal.Text);
            model.start_time = Convert.ToDateTime(txtStartTime.Text);
            model.end_time = Convert.ToDateTime(txtEndTime.Text);
            int num = Convert.ToInt32(txtNum.Text);
            string st = "";
            for (int i = 1; i <= num; i++)
            {
                st = i.ToString().PadLeft(5, '0');//补齐优惠券的位数
                string str_code = txtStrCode.Text + DateTime.Now.ToString("yyyyMMddhhmmss") + st;
                model.str_code = Vincent._MD5Encrypt.GetMD5(str_code);
                if (bll.Add(model) > 0)
                {
                    result = true;
                }
            }
            AddAdminLog(Vincent._DTcms.DTEnums.ActionEnum.Add.ToString(), "发布了" + num + "张优惠券:");//记录日志
            return result;
        }
        #endregion

        //提交保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (action == Vincent._DTcms.DTEnums.ActionEnum.Add.ToString()) 
            {
                ChkAdminLevel("channel_" + this.channel_name + "_list", Vincent._DTcms.DTEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误啦！", "", "Error");
                    return;
                }
                JscriptMsg("添加信息成功！", "coupon_list.aspx", "Success");
            }
            
        }
    }
}