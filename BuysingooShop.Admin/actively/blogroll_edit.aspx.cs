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

namespace BuysingooShop.Admin.actively
{
    public partial class blogroll_edit : Web.UI.ManagePage
    {
        
        private string action = Vincent._DTcms.DTEnums.ActionEnum.Add.ToString(); //操作类型
        protected int channel_id = 99;
        protected int category_id = 99;
        private int id = 0; 
               

        //页面加载事件
        protected void Page_Load(object sender, EventArgs e)
        {
            string _action = Vincent._DTcms.DTRequest.GetQueryString("action");

            if (this.channel_id == 0)
            {
                JscriptMsg("频道参数不正确！", "back", "Error");
                return;
            }

            if (!string.IsNullOrEmpty(_action) && _action == Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString())
            {
                this.action = Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString();//修改类型
                this.id = Vincent._DTcms.DTRequest.GetQueryInt("id");
                if (this.id == 0)
                {
                    JscriptMsg("传输参数不正确！", "back", "Error");
                    return;
                }
                if (!new BLL.article().Exists(this.id))
                {
                    JscriptMsg("信息不存在或已被删除！", "back", "Error");
                    return;
                }
            }
            if (!Page.IsPostBack)
            {
                if (action == Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(this.id);
                }
            }
        }

    




        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            BLL.article bll = new BLL.article();
            Model.article model = bll.GetModel(_id);

            txtCallIndex.Text = model.call_index;
            txtTitle.Text = model.title;
            txtLinkUrl.Text = model.link_url;
            txtZhaiyao.Text = model.zhaiyao;
            txtContent.Value = model.content;
           
          
        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            bool result = false;
            Model.article model = new Model.article();
            BLL.article bll = new BLL.article();

            model.channel_id = this.channel_id;
            model.category_id = this.category_id;
            model.call_index = txtCallIndex.Text.Trim();
            model.title = txtTitle.Text.Trim();
            model.link_url = txtLinkUrl.Text.Trim();
            //内容摘要提取内容前255个字符
            if (string.IsNullOrEmpty(txtZhaiyao.Text.Trim()))
            {
                model.zhaiyao = Vincent._DTcms.Utils.DropHTML(txtContent.Value, 255);
            }
            else
            {
                model.zhaiyao = Vincent._DTcms.Utils.DropHTML(txtZhaiyao.Text, 255);
            }
            model.content = txtContent.Value;

            model.fields=new Dictionary<string, string>();
            model.is_sys = 1; //管理员发布
            model.user_name = "admin"; //获得当前登录用户名

            if (bll.Add(model) > 0)
            {
                //开始生成缩略图咯

                AddAdminLog(Vincent._DTcms.DTEnums.ActionEnum.Add.ToString(), "添加友情链接频道内容:" + model.title); //记录日志
                result = true;
            }
            return result;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            bool result = false;
            BLL.article bll = new BLL.article();
            Model.article model = bll.GetModel(_id);

            model.call_index = txtCallIndex.Text.Trim();
            model.title = txtTitle.Text.Trim();
            model.link_url = txtLinkUrl.Text.Trim();
            model.fields = new Dictionary<string, string>();

            //内容摘要提取内容前255个字符
            if (string.IsNullOrEmpty(txtZhaiyao.Text.Trim()))
            {
                model.zhaiyao = Vincent._DTcms.Utils.DropHTML(txtContent.Value, 255);
            }
            else
            {
                model.zhaiyao = Vincent._DTcms.Utils.DropHTML(txtZhaiyao.Text, 255);
            }
            model.content = txtContent.Value;
            model.update_time = DateTime.Now;

            if (bll.Update(model))
            {

                AddAdminLog(Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString(), "修改友情链接频道内容:" + model.title); //记录日志
                result = true;
            }
            return result;
        }
        #endregion
        
        // 保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (action == Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString()) //修改
            {
                if (!DoEdit(this.id))
                {
                    JscriptMsg("保存过程中发生错误啦！", "", "Error");
                    return;
                }
                JscriptMsg("修改信息成功！", "blogroll_list.aspx?channel_id=" + this.channel_id, "Success");
            }
            else //添加
            {
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误啦！", "", "Error");
                    return;
                }
                JscriptMsg("添加信息成功！", "blogroll_list.aspx?channel_id=" + this.channel_id, "Success");
            }
        } 
    



    }
}