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

namespace BuysingooShop.Admin.good
{
    public partial class template_edit : Web.UI.ManagePage
    {
        private string action = Vincent._DTcms.DTEnums.ActionEnum.Add.ToString(); //操作类型
        protected string channel_name = string.Empty; //频道名称
        protected int channel_id;
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
                if (!new BLL.good_template().Exists(this.id))
                {
                    JscriptMsg("信息不存在或已被删除！", "back", "Error");
                    return;
                }
            }
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("channel_" + this.channel_name + "_list", Vincent._DTcms.DTEnums.ActionEnum.View.ToString()); //检查权限
                TreeBind(this.channel_id); //绑定类别
                if (action == Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(this.id);
                }
            }
        }

        #region 绑定类别=================================
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
        #endregion

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            BLL.good_template bll = new BLL.good_template();
            Model.good_template model = bll.GetModel(_id);

            if (model.pics.Count > 0)
            {
                foreach (Model.good_template_pic list in model.pics)
                {
                    if (list.typeId == 1)
                    {
                        txtPicUrl1.Text = list.picUrl;
                    }
                    else if (list.typeId == 2)
                    {
                        txtPicUrl2.Text = list.picUrl;
                    }
                    else if (list.typeId == 3)
                    {
                        txtPicUrl3.Text = list.picUrl;
                    }
                    else if (list.typeId == 4)
                    {
                        txtPicUrl4.Text = list.picUrl;
                    }
                    else if (list.typeId == 5)
                    {
                        txtPicUrl5.Text = list.picUrl;
                    }
                    else if (list.typeId == 6)
                    {
                        txtPicUrl6.Text = list.picUrl;
                    }
                }

            }
            if (!string.IsNullOrEmpty(model.img_url))
            {
                txtImgUrl.Text = model.img_url;
            }
            ddlCategoryId.SelectedValue = model.categoryId.ToString();
            txtAddTime.Text = model.addTime.ToString("yyyy-MM-dd HH:mm:ss");
            txtName.Text = model.name;
            txtRemark.Text = model.remark;
            txtSortId.Text = model.sort_id.ToString();
            rblIsLock.SelectedValue = model.isLock.ToString();
            rblIsDefault.SelectedValue = model.isDefault.ToString();
            rblIsAd.SelectedValue = model.isAd.ToString();
            //是广告位就显示广告位信息
            if (model.isAd == 1)
            {
                this.txtSortAd.Text = model.sort_ad.ToString();
                if (!string.IsNullOrEmpty(model.img_url1))
                {
                    txtImgUrl1.Text = model.img_url1;
                }
            }
            txtGoodId.Text = model.goodId.ToString();

        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            bool result = false;
            Model.good_template model = new Model.good_template();
            BLL.good_template bll = new BLL.good_template();

            model.categoryId = Vincent._DTcms.Utils.StrToInt(ddlCategoryId.SelectedValue, 0);
            model.name = txtName.Text.Trim();
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
            model.sort_id = Vincent._DTcms.Utils.StrToInt(txtSortId.Text.Trim(), 99);
            model.isLock = Vincent._DTcms.Utils.StrToInt(this.rblIsLock.SelectedValue, 1);
            model.isDefault = Vincent._DTcms.Utils.StrToInt(this.rblIsDefault.SelectedValue, 0);
            #region 同一主题只能有一个默认模板
            if (model.isDefault==1)
            {
                bll.UpdateField("isDefault=0", "categoryId="+model.categoryId);
            }
            #endregion
            model.addTime = Vincent._DTcms.Utils.StrToDateTime(txtAddTime.Text.Trim());
            model.isAd = Vincent._DTcms.Utils.StrToInt(this.rblIsAd.SelectedValue, 0);
            if (model.isAd == 1)
            {
                model.sort_ad = Vincent._DTcms.Utils.StrToInt(txtSortAd.Text.Trim(), 1);
                model.img_url1 = this.txtImgUrl1.Text;
            }
            else
            {
                model.sort_ad = 0;
            }
            model.goodId=Vincent._DTcms.Utils.StrToInt(txtGoodId.Text.Trim(), 0);

            #region 保存图片====================
            
            List<Model.good_template_pic> ls = new List<Model.good_template_pic>();
            if (!string.IsNullOrEmpty(txtPicUrl1.Text))
            {
                ls.Add(new Model.good_template_pic { addTime = DateTime.Now, isLock = 0, picUrl = this.txtPicUrl1.Text, typeId = 1 });
            }
            else if (!string.IsNullOrEmpty(txtPicUrl2.Text))
            {
                ls.Add(new Model.good_template_pic { addTime = DateTime.Now, isLock = 0, picUrl = this.txtPicUrl1.Text, typeId = 2 });
            }
            else if (!string.IsNullOrEmpty(txtPicUrl3.Text))
            {
                ls.Add(new Model.good_template_pic { addTime = DateTime.Now, isLock = 0, picUrl = this.txtPicUrl1.Text, typeId = 3 });
            }
            else if (!string.IsNullOrEmpty(txtPicUrl4.Text))
            {
                ls.Add(new Model.good_template_pic { addTime = DateTime.Now, isLock = 0, picUrl = this.txtPicUrl1.Text, typeId = 4 });
            }
            else if (!string.IsNullOrEmpty(txtPicUrl5.Text))
            {
                ls.Add(new Model.good_template_pic { addTime = DateTime.Now, isLock = 0, picUrl = this.txtPicUrl1.Text, typeId = 5 });
            }
            else if (!string.IsNullOrEmpty(txtPicUrl6.Text))
            {
                ls.Add(new Model.good_template_pic { addTime = DateTime.Now, isLock = 0, picUrl = this.txtPicUrl1.Text, typeId = 6 });
            }

            model.pics = ls;

            #endregion

            if (bll.Add(model) > 0)
            {
                AddAdminLog(Vincent._DTcms.DTEnums.ActionEnum.Add.ToString(), "添加" + this.channel_name + "频道内容:" + model.name); //记录日志
                result = true;
            }
            return result;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            bool result = false;
            BLL.good_template bll = new BLL.good_template();
            Model.good_template model = bll.GetModel(_id);

            model.name = txtName.Text.Trim();
            model.img_url = txtImgUrl.Text;
            model.categoryId = Vincent._DTcms.Utils.StrToInt(ddlCategoryId.SelectedValue, 0);
            //内容摘要提取内容前500个字符
            if (!string.IsNullOrEmpty(txtRemark.Text.Trim()) && txtRemark.Text.Trim().Length > 500)
            {
                model.remark = Vincent._DTcms.Utils.DropHTML(txtRemark.Text, 500);
            }
            else
            {
                model.remark = txtRemark.Text;
            }
            model.sort_id = Vincent._DTcms.Utils.StrToInt(txtSortId.Text.Trim(), 99);
            model.isLock = Vincent._DTcms.Utils.StrToInt(this.rblIsLock.SelectedValue, 1);
            model.isDefault = Vincent._DTcms.Utils.StrToInt(this.rblIsDefault.SelectedValue, 0);
            #region 同一主题只能有一个默认模板
            if (model.isDefault == 1)
            {
                bll.UpdateField("isDefault=0", "categoryId=" + model.categoryId);
            }
            #endregion
            model.addTime = Vincent._DTcms.Utils.StrToDateTime(txtAddTime.Text.Trim());
            model.isAd = Vincent._DTcms.Utils.StrToInt(this.rblIsAd.SelectedValue, 0);
            if (model.isAd == 1)
            {
                model.sort_ad = Vincent._DTcms.Utils.StrToInt(txtSortAd.Text.Trim(), 1);
                model.img_url1 = this.txtImgUrl1.Text;
            }
            else
            {
                model.sort_ad = 0;
            }
            model.goodId=Vincent._DTcms.Utils.StrToInt(txtGoodId.Text.Trim(), 0);

            #region 保存图片====================
            List<Model.good_template_pic> ls = new List<Model.good_template_pic>();

            int[] arry = { 0, 0, 0, 0, 0, 0 };
            foreach (Model.good_template_pic m in model.pics)
            {
                int i = 1;
                if ((i + 1) == m.typeId)
                {
                    arry[i] = m.typeId;
                }
                i++;
            }
            if (!string.IsNullOrEmpty(txtPicUrl1.Text))
            {
                if (arry[0] > 0)
                {
                    ls.Add(new Model.good_template_pic { id = ls[0].id, templateId = model.id, addTime = DateTime.Now, isLock = 0, picUrl = this.txtPicUrl1.Text, typeId = 1 });
                }
                else
                {
                    ls.Add(new Model.good_template_pic { templateId = model.id, addTime = DateTime.Now, isLock = 0, picUrl = this.txtPicUrl1.Text, typeId = 1 });
                }
            }

            if (!string.IsNullOrEmpty(txtPicUrl2.Text))
            {
                if (arry[1] > 0)
                {
                    ls.Add(new Model.good_template_pic { id = ls[1].id, templateId = model.id, addTime = DateTime.Now, isLock = 0, picUrl = this.txtPicUrl2.Text, typeId = 2 });
                }
                else
                {
                    ls.Add(new Model.good_template_pic { templateId = model.id, addTime = DateTime.Now, isLock = 0, picUrl = this.txtPicUrl2.Text, typeId = 2 });
                }
            }

            if (!string.IsNullOrEmpty(txtPicUrl3.Text))
            {
                if (arry[2] > 0)
                {
                    ls.Add(new Model.good_template_pic { id = ls[2].id, templateId = model.id, addTime = DateTime.Now, isLock = 0, picUrl = this.txtPicUrl3.Text, typeId = 3 });
                }
                else
                {
                    ls.Add(new Model.good_template_pic { templateId = model.id, addTime = DateTime.Now, isLock = 0, picUrl = this.txtPicUrl3.Text, typeId = 3 });
                }
            }

            if (!string.IsNullOrEmpty(txtPicUrl4.Text))
            {
                if (arry[3] > 0)
                {
                    ls.Add(new Model.good_template_pic { id = ls[3].id, templateId = model.id, addTime = DateTime.Now, isLock = 0, picUrl = this.txtPicUrl4.Text, typeId = 4 });
                }
                else
                {
                    ls.Add(new Model.good_template_pic { templateId = model.id, addTime = DateTime.Now, isLock = 0, picUrl = this.txtPicUrl4.Text, typeId = 4 });
                }
            }

            if (!string.IsNullOrEmpty(txtPicUrl5.Text))
            {
                if (arry[4] > 0)
                {
                    ls.Add(new Model.good_template_pic { id = ls[4].id, templateId = model.id, addTime = DateTime.Now, isLock = 0, picUrl = this.txtPicUrl5.Text, typeId = 5 });
                }
                else
                {
                    ls.Add(new Model.good_template_pic { templateId = model.id, addTime = DateTime.Now, isLock = 0, picUrl = this.txtPicUrl5.Text, typeId = 5 });
                }
            }

            if (!string.IsNullOrEmpty(txtPicUrl6.Text))
            {
                if (arry[5] > 0)
                {
                    ls.Add(new Model.good_template_pic { id = ls[5].id, templateId = model.id, addTime = DateTime.Now, isLock = 0, picUrl = this.txtPicUrl6.Text, typeId = 6 });
                }
                else
                {
                    ls.Add(new Model.good_template_pic { templateId = model.id, addTime = DateTime.Now, isLock = 0, picUrl = this.txtPicUrl6.Text, typeId = 6 });
                }
            }


            model.pics = ls;

            #endregion

            if (bll.Update(model))
            {

                AddAdminLog(Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString(), "修改" + this.channel_name + "频道内容:" + model.name); //记录日志
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
                JscriptMsg("修改信息成功！", "template_list.aspx?channel_id=" + this.channel_id, "Success");
            }
            else //添加
            {
                ChkAdminLevel("channel_" + this.channel_name + "_list", Vincent._DTcms.DTEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误啦！", "", "Error");
                    return;
                }
                JscriptMsg("添加信息成功！", "template_list.aspx?channel_id=" + this.channel_id, "Success");
            }
        }
    }
}