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

namespace BuysingooShop.Admin.source
{
    public partial class source_bottle_edit : Web.UI.ManagePage
    {
        private string action = Vincent._DTcms.DTEnums.ActionEnum.Add.ToString(); //操作类型
        protected string channel_name = string.Empty; //频道名称
        protected int channel_id;
        protected int type = 1;
        private int id = 0;
        

        //页面初始化事件
        protected void Page_Init(object sernder, EventArgs e)
        {
            this.channel_id = Vincent._DTcms.DTRequest.GetQueryInt("channel_id");
        }

        //页面加载事件
        protected void Page_Load(object sender, EventArgs e)
        {
            string _action = Vincent._DTcms.DTRequest.GetQueryString("action");

            if (this.channel_id == 0)
            {
                JscriptMsg("频道参数不正确！", "back", "Error");
                return;
            }
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
                if (!new BLL.source_material().Exists(this.id))
                {
                    JscriptMsg("信息不存在或已被删除！", "back", "Error");
                    return;
                }
            }
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("channel_" + this.channel_name + "_list", Vincent._DTcms.DTEnums.ActionEnum.View.ToString()); //检查权限
                if (action == Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(this.id);
                }
            }
        }

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            BLL.source_material bll = new BLL.source_material();
            Model.source_material model = bll.GetModel(_id);

            txtAddTime.Text = model.add_time.ToString("yyyy-MM-dd HH:mm:ss");
            if (!string.IsNullOrEmpty(model.img_url))
            {
                txtImgUrl.Text = model.img_url;
            }
            txtTitle.Text = model.title;
            txtRemark.Text = model.remark;
            txtSortId.Text = model.sort_id.ToString();
           
        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            bool result = false;
            Model.source_material model = new Model.source_material();
            BLL.source_material bll = new BLL.source_material();

            model.title = txtTitle.Text.Trim();
            model.img_url = txtImgUrl.Text;
            //内容摘要提取内容前500个字符
            if (!string.IsNullOrEmpty(txtRemark.Text.Trim()) && txtRemark.Text.Trim().Length > 500)
            {
                model.remark = Vincent._DTcms.Utils.DropHTML(txtRemark.Text, 500);
            }
            else
            {
                model.remark = txtRemark.Text;
            }
            int user_id = 0;
            if (IsAdminLogin())
            {
                user_id = GetAdminInfo().id;
            }
            model.type = type;
            model.user_id = user_id;
            model.add_time = Vincent._DTcms.Utils.StrToDateTime(txtAddTime.Text.Trim());


            if (bll.Add(model) > 0)
            {
                AddAdminLog(Vincent._DTcms.DTEnums.ActionEnum.Add.ToString(), "添加" + this.channel_name + "频道内容:" + model.title); //记录日志
                result = true;
            }
            return result;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            bool result = false;
            BLL.source_material bll = new BLL.source_material();
            Model.source_material model = bll.GetModel(_id);

            model.title = txtTitle.Text.Trim();
            model.img_url = txtImgUrl.Text;
            //内容摘要提取内容前500个字符
            if (!string.IsNullOrEmpty(txtRemark.Text.Trim()) && txtRemark.Text.Trim().Length > 500)
            {
                model.remark = Vincent._DTcms.Utils.DropHTML(txtRemark.Text, 500);
            }
            else
            {
                model.remark = txtRemark.Text;
            }
            model.add_time = Vincent._DTcms.Utils.StrToDateTime(txtAddTime.Text.Trim());

            if (bll.Update(model))
            {
                AddAdminLog(Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString(), "修改" + this.channel_name + "频道内容:" + model.title); //记录日志
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
                ChkAdminLevel("channel_" + this.channel_name + "_list", Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit(this.id))
                {
                    JscriptMsg("保存过程中发生错误啦！", "", "Error");
                    return;
                }
                JscriptMsg("修改信息成功！", "source_bottle_list.aspx?channel_id=" + this.channel_id, "Success");
            }
            else //添加
            {
                ChkAdminLevel("channel_" + this.channel_name + "_list", Vincent._DTcms.DTEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误啦！", "", "Error");
                    return;
                }
                JscriptMsg("添加信息成功！", "source_bottle_list.aspx?channel_id=" + this.channel_id, "Success");
            }
        }
    }
}