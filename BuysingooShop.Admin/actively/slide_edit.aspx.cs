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
    public partial class slide_edit : Web.UI.ManagePage
    {

        private string action = Vincent._DTcms.DTEnums.ActionEnum.Add.ToString(); //操作类型
        protected string channel_name = string.Empty; //频道名称
        protected int channel_id = 6;
        private int id = 0;

        // 页面初始化事件
        protected void Page_Init(object sernder, EventArgs e)
        {
            //this.channel_id = Vincent._DTcms.DTRequest.GetQueryInt("channel_id");
            //CreateOtherField(this.channel_id); //动态生成相应的扩展字段
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
            //this.channel_name = new BLL.channel().GetChannelName(this.channel_id); //取得频道名称

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
                //ChkAdminLevel("channel_" + this.channel_name + "_list", Vincent._DTcms.DTEnums.ActionEnum.View.ToString()); //检查权限
                //ShowSysField(this.channel_id); //显示相应的默认控件
                //GroupBind(""); //绑定用户组
                //TreeBind(this.channel_id); //绑定类别
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

            ddlCategoryId.SelectedValue = model.category_id.ToString();
            txtCallIndex.Text = model.call_index;
            txtTitle.Text = model.title;
            txtLinkUrl.Text = model.link_url;
            //不是相册图片就绑定            
            string filename = model.img_url.Substring(model.img_url.LastIndexOf("/") + 1);
            if (!filename.StartsWith("thumb_"))
            {
                txtImgUrl.Text = model.img_url;
            }
            txtSeoTitle.Text = model.seo_title;
            txtSeoKeywords.Text = model.seo_keywords;
            txtSeoDescription.Text = model.seo_description;
            txtZhaiyao.Text = model.zhaiyao;
            txtContent.Value = model.content;
            txtSortId.Text = model.sort_id.ToString();
            txtClick.Text = model.click.ToString();
            rblStatus.SelectedValue = model.status.ToString();
            txtAddTime.Text = model.add_time.ToString("yyyy-MM-dd HH:mm:ss");
            if (model.is_msg == 1)
            {
                cblItem.Items[0].Selected = true;
            }
            if (model.is_top == 1)
            {
                cblItem.Items[1].Selected = true;
            }
            if (model.is_red == 1)
            {
                cblItem.Items[2].Selected = true;
            }
            if (model.is_hot == 1)
            {
                cblItem.Items[3].Selected = true;
            }
            if (model.is_slide == 1)
            {
                cblItem.Items[4].Selected = true;
            }
            //扩展字段赋值
            List<Model.article_attribute_field> ls1 = new BLL.article_attribute_field().GetModelList(this.channel_id, "");
            foreach (Model.article_attribute_field modelt1 in ls1)
            {
                switch (modelt1.control_type)
                {
                    case "single-text": //单行文本
                        TextBox txtControl = FindControl("field_control_" + modelt1.name) as TextBox;
                        if (txtControl != null && model.fields.ContainsKey(modelt1.name))
                        {
                            if (modelt1.is_password == 1)
                            {
                                txtControl.Attributes.Add("value", model.fields[modelt1.name]);
                            }
                            else
                            {
                                txtControl.Text = model.fields[modelt1.name];
                            }
                        }
                        break;
                    case "multi-text": //多行文本
                        goto case "single-text";
                    case "editor": //编辑器
                        HtmlTextArea txtAreaControl = FindControl("field_control_" + modelt1.name) as HtmlTextArea;
                        if (txtAreaControl != null && model.fields.ContainsKey(modelt1.name))
                        {
                            txtAreaControl.Value = model.fields[modelt1.name];
                        }
                        break;
                    case "images": //图片上传
                        goto case "single-text";
                    case "number": //数字
                        goto case "single-text";
                    case "checkbox": //复选框
                        CheckBox cbControl = FindControl("field_control_" + modelt1.name) as CheckBox;
                        if (cbControl != null && model.fields.ContainsKey(modelt1.name))
                        {
                            if (model.fields[modelt1.name] == "1")
                            {
                                cbControl.Checked = true;
                            }
                            else
                            {
                                cbControl.Checked = false;
                            }
                        }
                        break;
                    case "multi-radio": //多项单选
                        RadioButtonList rblControl = FindControl("field_control_" + modelt1.name) as RadioButtonList;
                        if (rblControl != null && model.fields.ContainsKey(modelt1.name))
                        {
                            rblControl.SelectedValue = model.fields[modelt1.name];
                        }
                        break;
                    case "multi-checkbox": //多项多选
                        CheckBoxList cblControl = FindControl("field_control_" + modelt1.name) as CheckBoxList;
                        if (cblControl != null && model.fields.ContainsKey(modelt1.name))
                        {
                            string[] valArr = model.fields[modelt1.name].Split(',');
                            for (int i = 0; i < cblControl.Items.Count; i++)
                            {
                                cblControl.Items[i].Selected = false; //先取消默认的选中
                                foreach (string str in valArr)
                                {
                                    if (cblControl.Items[i].Value == str)
                                    {
                                        cblControl.Items[i].Selected = true;
                                    }
                                }
                            }
                        }
                        break;
                }
            }
            //绑定图片相册
            if (filename.StartsWith("thumb_"))
            {
                hidFocusPhoto.Value = model.img_url; //封面图片
            }
            rptAlbumList.DataSource = model.albums;
            rptAlbumList.DataBind();
            //绑定内容附件
            rptAttachList.DataSource = model.attach;
            rptAttachList.DataBind();
            //赋值用户组价格
            if (model.group_price != null)
            {
                for (int i = 0; i < this.rptPrice.Items.Count; i++)
                {
                    int hideId = Convert.ToInt32(((HiddenField)this.rptPrice.Items[i].FindControl("hideGroupId")).Value);
                    foreach (Model.user_group_price modelt in model.group_price)
                    {
                        if (hideId == modelt.group_id)
                        {
                            ((HiddenField)this.rptPrice.Items[i].FindControl("hidePriceId")).Value = modelt.id.ToString();
                            ((TextBox)this.rptPrice.Items[i].FindControl("txtGroupPrice")).Text = modelt.price.ToString();
                        }
                    }
                }
            }
        }
        #endregion
        
        #region 增加操作=================================
        private bool DoAdd()
        {
            bool result = false;
            Model.article model = new Model.article();
            BLL.article bll = new BLL.article();

            model.channel_id = this.channel_id;
            model.category_id = Vincent._DTcms.Utils.StrToInt(ddlCategoryId.SelectedValue, 0);
            model.call_index = txtCallIndex.Text.Trim();
            model.title = txtTitle.Text.Trim();
            model.link_url = txtLinkUrl.Text.Trim();
            model.img_url = txtImgUrl.Text;
            model.seo_title = txtSeoTitle.Text.Trim();
            model.seo_keywords = txtSeoKeywords.Text.Trim();
            model.seo_description = txtSeoDescription.Text.Trim();
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
            model.sort_id = Vincent._DTcms.Utils.StrToInt(txtSortId.Text.Trim(), 99);
            model.click = int.Parse(txtClick.Text.Trim());
            model.status = Vincent._DTcms.Utils.StrToInt(rblStatus.SelectedValue, 0);
            model.is_msg = 0;   
            model.is_top = 0;
            model.is_red = 0;
            model.is_hot = 0;
            model.is_slide = 0;
            if (cblItem.Items[0].Selected == true)
            {
                model.is_msg = 1;
            }
            if (cblItem.Items[1].Selected == true)
            {
                model.is_top = 1;
            }
            if (cblItem.Items[2].Selected == true)
            {
                model.is_red = 1;
            }
            if (cblItem.Items[3].Selected == true)
            {
                model.is_hot = 1;
            }
            if (cblItem.Items[4].Selected == true)
            {
                model.is_slide = 1;
            }
            //model.is_sys = 1; //管理员发布
            //model.user_name = "admin"; //获得当前登录用户名
            model.add_time = Vincent._DTcms.Utils.StrToDateTime(txtAddTime.Text.Trim());
            model.fields = SetFieldValues(this.channel_id); //扩展字段赋值            

            #region 保存相册====================
            //检查是否有自定义图片
            if (txtImgUrl.Text.Trim() == "")
            {
                model.img_url = hidFocusPhoto.Value;
            }
            string[] albumArr = Request.Form.GetValues("hid_photo_name");
            string[] remarkArr = Request.Form.GetValues("hid_photo_remark");
            if (albumArr != null && albumArr.Length > 0)
            {
                List<Model.article_albums> ls = new List<Model.article_albums>();
                for (int i = 0; i < albumArr.Length; i++)
                {
                    string[] imgArr = albumArr[i].Split('|');
                    if (imgArr.Length == 3)
                    {
                        if (!string.IsNullOrEmpty(remarkArr[i]))
                        {
                            ls.Add(new Model.article_albums { original_path = imgArr[1], thumb_path = imgArr[2], remark = remarkArr[i] });
                        }
                        else
                        {
                            ls.Add(new Model.article_albums { original_path = imgArr[1], thumb_path = imgArr[2] });
                        }
                    }
                }
                model.albums = ls;
            }
            #endregion

            #region 保存附件====================
            //保存附件
            string[] attachFileNameArr = Request.Form.GetValues("hid_attach_filename");
            string[] attachFilePathArr = Request.Form.GetValues("hid_attach_filepath");
            string[] attachFileSizeArr = Request.Form.GetValues("hid_attach_filesize");
            string[] attachPointArr = Request.Form.GetValues("txt_attach_point");
            if (attachFileNameArr != null && attachFilePathArr != null && attachFileSizeArr != null && attachPointArr != null
                && attachFileNameArr.Length > 0 && attachFilePathArr.Length > 0 && attachFileSizeArr.Length > 0 && attachPointArr.Length > 0)
            {
                List<Model.article_attach> ls = new List<Model.article_attach>();
                for (int i = 0; i < attachFileNameArr.Length; i++)
                {
                    int fileSize = Vincent._DTcms.Utils.StrToInt(attachFileSizeArr[i], 0);
                    string fileExt = Vincent._DTcms.Utils.GetFileExt(attachFilePathArr[i]);
                    int _point = Vincent._DTcms.Utils.StrToInt(attachPointArr[i], 0);
                    ls.Add(new Model.article_attach { file_name = attachFileNameArr[i], file_path = attachFilePathArr[i], file_size = fileSize, file_ext = fileExt, point = _point });
                }
                model.attach = ls;
            }
            #endregion

            #region 保存会员组价格==============
            List<Model.user_group_price> priceList = new List<Model.user_group_price>();
            for (int i = 0; i < rptPrice.Items.Count; i++)
            {
                int _groupid = Convert.ToInt32(((HiddenField)rptPrice.Items[i].FindControl("hideGroupId")).Value);
                decimal _price = Convert.ToDecimal(((TextBox)rptPrice.Items[i].FindControl("txtGroupPrice")).Text.Trim());
                priceList.Add(new Model.user_group_price { group_id = _groupid, price = _price });
            }
            model.group_price = priceList;
            #endregion

            if (bll.Add(model) > 0)
            {
                //开始生成缩略图咯
                AddAdminLog(Vincent._DTcms.DTEnums.ActionEnum.Add.ToString(), "添加" + this.channel_name + "频道内容:" + model.title); //记录日志
                result = true;
            }
            return result;
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

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            bool result = false;
            BLL.article bll = new BLL.article();
            Model.article model = bll.GetModel(_id);

            model.channel_id = this.channel_id;
            model.category_id = Vincent._DTcms.Utils.StrToInt(ddlCategoryId.SelectedValue, 0);
            model.call_index = txtCallIndex.Text.Trim();
            model.title = txtTitle.Text.Trim();
            model.link_url = txtLinkUrl.Text.Trim();
            model.img_url = txtImgUrl.Text;
            model.seo_title = txtSeoTitle.Text.Trim();
            model.seo_keywords = txtSeoKeywords.Text.Trim();
            model.seo_description = txtSeoDescription.Text.Trim();
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
            model.sort_id = Vincent._DTcms.Utils.StrToInt(txtSortId.Text.Trim(), 99);
            model.click = int.Parse(txtClick.Text.Trim());
            model.status = Vincent._DTcms.Utils.StrToInt(rblStatus.SelectedValue, 0);
            model.is_msg = 0;
            model.is_top = 0;
            model.is_red = 0;
            model.is_hot = 0;
            model.is_slide = 0;
            if (cblItem.Items[0].Selected == true)
            {
                model.is_msg = 1;
            }
            if (cblItem.Items[1].Selected == true)
            {
                model.is_top = 1;
            }
            if (cblItem.Items[2].Selected == true)
            {
                model.is_red = 1;
            }
            if (cblItem.Items[3].Selected == true)
            {
                model.is_hot = 1;
            }
            if (cblItem.Items[4].Selected == true)
            {
                model.is_slide = 1;
            }
            model.add_time = Vincent._DTcms.Utils.StrToDateTime(txtAddTime.Text.Trim());
            model.update_time = DateTime.Now;
            model.fields = SetFieldValues(this.channel_id); //扩展字段赋值

            #region 保存相册====================
            //检查是否有自定义图片
            if (txtImgUrl.Text.Trim() == "")
            {
                model.img_url = hidFocusPhoto.Value;
            }
            if (model.albums != null)
            {
                model.albums.Clear();
            }
            string[] albumArr = Request.Form.GetValues("hid_photo_name");
            string[] remarkArr = Request.Form.GetValues("hid_photo_remark");
            if (albumArr != null)
            {
                List<Model.article_albums> ls = new List<Model.article_albums>();
                for (int i = 0; i < albumArr.Length; i++)
                {
                    string[] imgArr = albumArr[i].Split('|');
                    int img_id = Vincent._DTcms.Utils.StrToInt(imgArr[0], 0);
                    if (imgArr.Length == 3)
                    {
                        if (!string.IsNullOrEmpty(remarkArr[i]))
                        {
                            ls.Add(new Model.article_albums { id = img_id, article_id = _id, original_path = imgArr[1], thumb_path = imgArr[2], remark = remarkArr[i] });
                        }
                        else
                        {
                            ls.Add(new Model.article_albums { id = img_id, article_id = _id, original_path = imgArr[1], thumb_path = imgArr[2] });
                        }
                    }
                }
                model.albums = ls;
            }
            #endregion


            #region 保存附件====================
            if (model.attach != null)
            {
                model.attach.Clear();
            }
            string[] attachIdArr = Request.Form.GetValues("hid_attach_id");
            string[] attachFileNameArr = Request.Form.GetValues("hid_attach_filename");
            string[] attachFilePathArr = Request.Form.GetValues("hid_attach_filepath");
            string[] attachFileSizeArr = Request.Form.GetValues("hid_attach_filesize");
            string[] attachPointArr = Request.Form.GetValues("txt_attach_point");
            if (attachIdArr != null && attachFileNameArr != null && attachFilePathArr != null && attachFileSizeArr != null && attachPointArr != null
                && attachIdArr.Length > 0 && attachFileNameArr.Length > 0 && attachFilePathArr.Length > 0 && attachFileSizeArr.Length > 0 && attachPointArr.Length > 0)
            {
                List<Model.article_attach> ls = new List<Model.article_attach>();
                for (int i = 0; i < attachFileNameArr.Length; i++)
                {
                    int attachId = Vincent._DTcms.Utils.StrToInt(attachIdArr[i], 0);
                    int fileSize = Vincent._DTcms.Utils.StrToInt(attachFileSizeArr[i], 0);
                    string fileExt = Vincent._DTcms.Utils.GetFileExt(attachFilePathArr[i]);
                    int _point = Vincent._DTcms.Utils.StrToInt(attachPointArr[i], 0);
                    ls.Add(new Model.article_attach { id = attachId, article_id = _id, file_name = attachFileNameArr[i], file_path = attachFilePathArr[i], file_size = fileSize, file_ext = fileExt, point = _point, });
                }
                model.attach = ls;
            }
            #endregion

            #region 保存会员组价格==============
            List<Model.user_group_price> priceList = new List<Model.user_group_price>();
            for (int i = 0; i < rptPrice.Items.Count; i++)
            {
                int hidPriceId = 0;
                if (!string.IsNullOrEmpty(((HiddenField)rptPrice.Items[i].FindControl("hidePriceId")).Value))
                {
                    hidPriceId = Convert.ToInt32(((HiddenField)rptPrice.Items[i].FindControl("hidePriceId")).Value);
                }
                int hidGroupId = Convert.ToInt32(((HiddenField)rptPrice.Items[i].FindControl("hideGroupId")).Value);
                decimal _price = Convert.ToDecimal(((TextBox)rptPrice.Items[i].FindControl("txtGroupPrice")).Text.Trim());
                priceList.Add(new Model.user_group_price { id = hidPriceId, article_id = _id, group_id = hidGroupId, price = _price });
            }
            model.group_price = priceList;
            #endregion

            if (bll.Update(model))
            {

                AddAdminLog(Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString(), "修改" + this.channel_name + "频道内容:" + model.title); //记录日志
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
                //ChkAdminLevel("channel_" + this.channel_name + "_list", Vincent._DTcms.DTEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit(this.id))
                {
                    JscriptMsg("保存过程中发生错误啦！", "", "Error");
                    return;
                }
                JscriptMsg("修改信息成功！", "slide_list.aspx?channel_id=" + this.channel_id, "Success");
            }
            else //添加
            {
                //ChkAdminLevel("channel_" + this.channel_name + "_list", Vincent._DTcms.DTEnums.ActionEnum.Add.ToString()); //检查权限/actively/slide_list.aspx
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误啦！", "", "Error");
                    return;
                }
                JscriptMsg("添加信息成功！", "slide_list.aspx?channel_id=" + this.channel_id, "Success");
            }
        }




    }
}