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
    public partial class brand_edit : Web.UI.ManagePage
    {
        private string action = Vincent._DTcms.DTEnums.ActionEnum.Add.ToString(); //操作类型
        protected string channel_name = string.Empty; //频道名称
        protected int channel_id;
        private int id = 0;

        /// <summary>
        /// 页面初始化事件
        /// </summary>
        /// <param name="sernder"></param>
        /// <param name="e"></param>
        protected void Page_Init(object sernder, EventArgs e)
        {
            this.channel_id = Vincent._DTcms.DTRequest.GetQueryInt("channel_id");
        }

        /// <summary>
        /// 页面加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                if (!new BLL.brand().Exists(this.id))
                {
                    JscriptMsg("信息不存在或已被删除！", "back", "Error");
                    return;
                }
            }
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("channel_" + this.channel_name + "_list", Vincent._DTcms.DTEnums.ActionEnum.View.ToString()); //检查权限
                TreeBind(this.channel_id);
                if (action == Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(this.id);
                }
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                JscriptMsg("修改信息成功！", "brand_list.aspx?channel_id=" + this.channel_id, "Success");
            }
            else //添加
            {
                ChkAdminLevel("channel_" + this.channel_name + "_list", Vincent._DTcms.DTEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误啦！", "", "Error");
                    return;
                }
                JscriptMsg("添加信息成功！", "brand_list.aspx?channel_id=" + this.channel_id, "Success");
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
            BLL.brand bll = new BLL.brand();
            Model.brand model = bll.GetModel(_id);

            txtAddTime.Text = model.add_time.ToString("yyyy-MM-dd HH:mm:ss");
            if (!string.IsNullOrEmpty(model.brandImgUrl))
            {
                txtBrandImgUrl.Text = model.brandImgUrl;
            }
            txtBrandName.Text = model.brandName;
            txtRemark.Text = model.remark;
            txtSortId.Text = model.sort_id.ToString();
            rblIsLock.SelectedValue = model.isLock.ToString();
            //图片列表
            rptAlbumList.DataSource = model.brand_attach;
            rptAlbumList.DataBind();
           
        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            bool result = false;
            Model.brand model = new Model.brand();
            BLL.brand bll = new BLL.brand();

            model.brandName = txtBrandName.Text.Trim();
            model.brandImgUrl = txtBrandImgUrl.Text;
            //内容摘要提取内容前500个字符
            if (!string.IsNullOrEmpty(txtRemark.Text.Trim()) && txtRemark.Text.Trim().Length > 500)
            {
                model.remark = Vincent._DTcms.Utils.DropHTML(txtRemark.Text, 500);
            }
            else
            {
                model.remark = txtRemark.Text;
            }
            model.isLock =Vincent._DTcms.Utils.StrToInt(this.rblIsLock.SelectedValue,1);
            model.sort_id = Vincent._DTcms.Utils.StrToInt(txtSortId.Text.Trim(), 99);
            int managerId = 0;
            if (IsAdminLogin())
            {
                managerId = GetAdminInfo().id;
            }
            model.managerId = managerId;
            model.add_time = Vincent._DTcms.Utils.StrToDateTime(txtAddTime.Text.Trim());
            model.update_time = model.add_time;

            #region 保存相册====================
            //检查是否有自定义图片
            if (txtBrandImgUrl.Text.Trim() == "")
            {
                model.brandImgUrl = hidFocusPhoto.Value;
            }
            if (model.brand_attach != null)
            {
                model.brand_attach.Clear();
            }
            string[] albumArr = Request.Form.GetValues("hid_photo_name");
            string[] remarkArr = Request.Form.GetValues("hid_photo_remark");
            string[] categoryArr = Request.Form.GetValues("hid_photo_category");
            string[] sizeArr = Request.Form.GetValues("hid_photo_size");
            if (albumArr != null)
            {
                List<Model.brand_attach> ls = new List<Model.brand_attach>();
                for (int i = 0; i < albumArr.Length; i++)
                {
                    string[] imgArr = albumArr[i].Split('|');
                    int img_id = Vincent._DTcms.Utils.StrToInt(imgArr[0], 0);
                    if (imgArr.Length == 3)
                    {
                        if (!string.IsNullOrEmpty(remarkArr[i]))
                        {
                            ls.Add(new Model.brand_attach { 
                                theme_id = Vincent._DTcms.Utils.StrToInt(categoryArr[i], 0),
                                img_url = imgArr[1].Trim(),
                                small_imgurl = imgArr[2].Trim(), 
                                remark = remarkArr[i], 
                                //brand_id = Vincent._DTcms.Utils.StrToInt(categoryArr[i], 0), 
                                size = sizeArr[i] });
                        }
                        else
                        {
                            ls.Add(new Model.brand_attach { 
                                theme_id = Vincent._DTcms.Utils.StrToInt(categoryArr[i], 0),
                                img_url = imgArr[1].Trim(),
                                small_imgurl = imgArr[2].Trim(), 
                                //brand_id = Vincent._DTcms.Utils.StrToInt(categoryArr[i], 0), //当前新增类别的id
                                size = sizeArr[i] });
                        }
                    }
                }
                model.brand_attach = ls;
            }
            #endregion

            if (bll.Add(model) > 0)
            {
                AddAdminLog(Vincent._DTcms.DTEnums.ActionEnum.Add.ToString(), "添加" + this.channel_name + "频道内容:" + model.brandName); //记录日志
                result = true;
            }
            return result;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            bool result = false;
            BLL.brand bll = new BLL.brand();
            Model.brand model = bll.GetModel(_id);

            model.brandName = txtBrandName.Text.Trim();
            model.brandImgUrl = txtBrandImgUrl.Text;
            //内容摘要提取内容前500个字符
            if (!string.IsNullOrEmpty(txtRemark.Text.Trim()) && txtRemark.Text.Trim().Length > 500)
            {
                model.remark = Vincent._DTcms.Utils.DropHTML(txtRemark.Text, 500);
            }
            else
            {
                model.remark = txtRemark.Text;
            }
            model.isLock = Vincent._DTcms.Utils.StrToInt(this.rblIsLock.SelectedValue, 1);
            model.sort_id = Vincent._DTcms.Utils.StrToInt(txtSortId.Text.Trim(), 99);
            int managerId = 0;
            if (IsAdminLogin())
            {
                managerId = GetAdminInfo().id;
            }
            model.managerId = managerId;
            model.add_time = Vincent._DTcms.Utils.StrToDateTime(txtAddTime.Text.Trim());
            model.update_time = DateTime.Now;


            #region 保存相册====================
            //检查是否有自定义图片
            if (txtBrandImgUrl.Text.Trim() == "")
            {
                model.brandImgUrl = hidFocusPhoto.Value;
            }
            if (model.brand_attach != null)
            {
                model.brand_attach.Clear();
            }
            string[] albumArr = Request.Form.GetValues("hid_photo_name");
            string[] remarkArr = Request.Form.GetValues("hid_photo_remark");
            string[] categoryArr = Request.Form.GetValues("hid_photo_category");
            string[] sizeArr = Request.Form.GetValues("hid_photo_size");
            if (albumArr != null)
            {
                List<Model.brand_attach> ls = new List<Model.brand_attach>();
                for (int i = 0; i < albumArr.Length; i++)
                {
                    string[] imgArr = albumArr[i].Split('|');
                    int img_id = Vincent._DTcms.Utils.StrToInt(imgArr[0], 0);
                    if (imgArr.Length == 3)
                    {
                        if (!string.IsNullOrEmpty(remarkArr[i]))
                        {
                            ls.Add(new Model.brand_attach {
                                brand_id = _id,
                                theme_id = Vincent._DTcms.Utils.StrToInt(categoryArr[i], 0),
                                img_url = imgArr[1].Trim(),
                                small_imgurl = imgArr[2].Trim(), 
                                remark = remarkArr[i],
                                size = sizeArr[i].Trim()});
                        }
                        else
                        {
                            ls.Add(new Model.brand_attach {
                                brand_id = _id,
                                theme_id = Vincent._DTcms.Utils.StrToInt(categoryArr[i], 0),
                                img_url = imgArr[1].Trim(),
                                small_imgurl = imgArr[2].Trim(),
                                size = sizeArr[i].Trim()});
                        }
                    }
                }
                model.brand_attach = ls;
            }
            #endregion

            if (bll.Update(model))
            {
                AddAdminLog(Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString(), "修改" + this.channel_name + "频道内容:" + model.brandName); //记录日志
                result = true;
            }

            return result;
        }
        #endregion


    }
}