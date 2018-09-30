<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="brand_edit.aspx.cs" Inherits="BuysingooShop.Admin.good.brand_edit" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>编辑信息</title>
<script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
<script type="text/javascript" src="../../scripts/jquery/Validform_v5.3.2_min.js"></script>
<script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
<script type="text/javascript" src="../../scripts/datepicker/WdatePicker.js"></script>

<script type="text/javascript" src="../../scripts/swfupload1/swfupload.js"></script>
<script type="text/javascript" src="../../scripts/swfupload1/swfupload.queue.js"></script>
<script type="text/javascript" src="../../scripts/swfupload1/swfupload.handlers.js"></script>

<script type="text/javascript" charset="utf-8" src="../../editor/kindeditor-min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../editor/lang/zh_CN.js"></script>
<script type="text/javascript" src="../js/layout.js"></script>
<link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    $(function () {
        //初始化表单验证
        $("#form1").initValidform();
        //计算用户组价格
        $("#field_control_sell_price").change(function () {
            var sprice = $(this).val();
            if (sprice > 0) {
                $(".groupprice").each(function () {
                    var num = $(this).attr("discount") * sprice / 100;
                    $(this).val(ForDight(num, 2));
                });
            }
        });
        //初始化编辑器
//        var editor = KindEditor.create('.editor', {
//            width: '98%',
//            height: '350px',
//            resizeType: 1,
//            uploadJson: '../../tools/upload_ajax.ashx?action=EditorFile&IsWater=1',
//            fileManagerJson: '../../tools/upload_ajax.ashx?action=ManagerFile',
//            allowFileManager: true
//        });
//        var editorMini = KindEditor.create('.editor-mini', {
//            width: '98%',
//            height: '250px',
//            resizeType: 1,
//            uploadJson: '../../tools/upload_ajax.ashx?action=EditorFile&IsWater=1',
//            items: [
//				'fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold', 'italic', 'underline',
//				'removeformat', '|', 'justifyleft', 'justifycenter', 'justifyright', 'insertorderedlist',
//				'insertunorderedlist', '|', 'emoticons', 'image', 'link']
//        });
        //初始化上传控件
        $(".upload-img").each(function () {
            $(this).InitSWFUpload({ sendurl: "../../tools/upload_ajax.ashx", flashurl: "../../scripts/swfupload/swfupload.swf" });
        });
        $(".upload-album").each(function () {
            $(this).InitSWFUpload({ btntext: "批量上传", btnwidth: 66, single: false, water: true, thumbnail: true, filesize: "2048", sendurl: "../../tools/upload_ajax.ashx", flashurl: "../../scripts/swfupload/swfupload.swf", filetypes: "*.jpg;*.jpge;*.png;*.gif;" });
        });
        $(".attach-btn").click(function () {
            showAttachDialog();
        });
        //设置封面图片的样式
        $(".photo-list ul li .img-box img").each(function () {
            if ($(this).attr("src") == $("#hidFocusPhoto").val()) {
                $(this).parent().addClass("selected");
            }
        });
    });
    //创建附件窗口
    function showAttachDialog(obj) {
        var objNum = arguments.length;
        var attachDialog = $.dialog({
            id: 'attachDialogId',
            lock: true,
            max: false,
            min: false,
            title: "上传附件",
            content: 'url:dialog/dialog_attach.aspx',
            width: 500,
            height: 180
        });
        //如果是修改状态，将对象传进去
        if (objNum == 1) {
            attachDialog.data = obj;
        }
    }
    //删除附件节点
    function delAttachNode(obj) {
        $(obj).parent().remove();
    }
</script>
</head>
<body class="mainbody">
<form id="form1" runat="server">
<!--导航栏-->
<div class="location">
  <a href="brand_list.aspx?channel_id=<%=this.channel_id %>" class="back"><i></i><span>返回列表页</span></a>
  <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
  <i class="arrow"></i>
  <a href="brand_list.aspx?channel_id=<%=this.channel_id %>"><span>内容管理</span></a>
  <i class="arrow"></i>
  <span>编辑信息</span>
</div>
<div class="line10"></div>
<!--/导航栏-->

<!--内容-->
<div class="content-tab-wrap">
  <div id="floatHead" class="content-tab">
    <div class="content-tab-ul-wrap">
      <ul>
        <li><a href="javascript:;" onclick="tabs(this);" class="selected">基本信息</a></li>
      </ul>
    </div>
  </div>
</div>

<div class="tab-content"> 
  <dl>
    <dt>是否锁定</dt>
    <dd>
      <div class="rule-multi-radio">
        <asp:RadioButtonList ID="rblIsLock" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
        <asp:ListItem Value="1" Selected="True">正常</asp:ListItem>
        <asp:ListItem Value="2">锁定</asp:ListItem>
        </asp:RadioButtonList>
      </div>
    </dd>
  </dl>
  <dl>
    <dt>品牌名称</dt>
    <dd>
      <asp:TextBox ID="txtBrandName" runat="server" CssClass="input normal" datatype="*2-100" sucmsg=" " />
      <span class="Validform_checktip">*标题最多100个字符</span>
    </dd>
  </dl>
  <dl>
    <dt>封面图片</dt>
    <dd>
      <asp:TextBox ID="txtBrandImgUrl" runat="server" CssClass="input normal upload-path" />
      <div class="upload-box upload-img"></div>
    </dd>
  </dl>
  <dl>
    <dt>备&nbsp;&nbsp;注</dt>
    <dd>
      <asp:TextBox ID="txtRemark" runat="server" CssClass="input" TextMode="MultiLine" datatype="*0-255" sucmsg=" "></asp:TextBox>
      <span class="Validform_checktip">内容不超过500字符</span>
    </dd>
  </dl>
  <dl>
    <dt>排序数字</dt>
    <dd>
      <asp:TextBox ID="txtSortId" runat="server" CssClass="input small" datatype="n" sucmsg=" ">99</asp:TextBox>
      <span class="Validform_checktip">*数字，越小越向前</span>
    </dd>
  </dl>
  <dl>
    <dt>发布时间</dt>
    <dd>
      <div class="input-date">
        <asp:TextBox ID="txtAddTime" runat="server" CssClass="input date" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})" datatype="/^\s*$|^\d{4}\-\d{1,2}\-\d{1,2}\s{1}(\d{1,2}:){2}\d{1,2}$/" errormsg="请选择正确的日期" sucmsg=" " />
        <i>日期</i>
      </div>
      <span class="Validform_checktip">不选择默认当前发布时间</span>
    </dd>
  </dl>
  <dl ID="div_albums_container" runat="server" visible="true">
    <dt>图片相册</dt>
    <dd>
      <div class="upload-box upload-album"></div>
      <input type="hidden" name="hidFocusPhoto" id="hidFocusPhoto" runat="server" class="focus-photo" />
      <div class="photo-list">
        <ul>
          <asp:Repeater ID="rptAlbumList" runat="server">
            <ItemTemplate>
            <li>
              <input type="hidden" name="hid_photo_name" value="<%#Eval("id")%>|<%#Eval("img_url")%>|<%#Eval("small_imgurl")%>" />
              <input type="hidden" name="hid_photo_remark" value="<%#Eval("remark")%>" />
              <input type="hidden" name="hid_photo_category" value="<%#Eval("theme_id")%>" />
              <input type="hidden" name="hid_photo_size" value="<%#Eval("size")%>" />
              
              <div class="img-box" onclick="setFocusImg(this);">
                <img src="<%#Eval("small_imgurl")%>" bigsrc="<%#Eval("img_url")%>" />
                <span class="remark">
                    <i><%#Eval("remark").ToString() == "" ? "暂无描述..." : Eval("remark").ToString()%></i>
                    <i><%#Eval("theme_id").ToString() == "0" ? "暂无主题..." : new BuysingooShop.BLL.article_category().GetTitle(Convert.ToInt32(Eval("theme_id")))%></i>
                    <i><%#Eval("size").ToString()=="" ? "暂无尺寸..." : Eval("size").ToString()%></i></span>
              </div>
              <a href="javascript:;" onclick="setRemark(this);">描述</a>
              <a href="javascript:;" onclick="delImg(this);">删除</a>
            </li>
            </ItemTemplate>
          </asp:Repeater>
        </ul>
      </div>
      <div id="categoryID" style="display:none;">
        <asp:DropDownList id="ddlCategoryId" runat="server"></asp:DropDownList>
      </div>
    </dd>
  </dl>
</div>
<!--/内容-->

<!--工具栏-->
<div class="page-footer">
  <div class="btn-list">
    <asp:Button ID="btnSubmit" runat="server" Text="提交保存" CssClass="btn" onclick="btnSubmit_Click" />
    <input name="btnReturn" type="button" value="返回上一页" class="btn yellow" onclick="javascript:history.back(-1);" />
  </div>
  <div class="clear"></div>
</div>
<!--/工具栏-->
</form>
</body>
</html>
