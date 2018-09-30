<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="promotion_edit.aspx.cs" Inherits="BuysingooShop.Admin.actively.promotion_edit" ValidateRequest="false" %>
<%@ Import Namespace="Vincent" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>编辑活动</title>
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script type="text/javascript" src="../../scripts/swfupload1/swfupload.js"></script>
    <script type="text/javascript" src="../../scripts/swfupload1/swfupload.queue.js"></script>
    <script type="text/javascript" src="../../scripts/swfupload1/swfupload.handlers.js"></script>
    <script type="text/javascript" src="../js/layout.js"></script>
    <script src="../../scripts/datepicker/WdatePicker.js" type="text/javascript"></script>

<link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    $(function () {
        //初始化表单验证
        $("#form1").initValidform();
        //选择活动类型
        $("#ddlRoleId").change(function () {
            var vals = $(this).val();
            if (vals == "1") {
                //满额减价
                $("#dlgift,#dlNum").css("display", "block");
                //$("#ddlWineryId").removeAttr("datatype errormsg sucmsg");
                $("#dlrpter").css("display", "none");

//                $("#Brand").css("display", "block");
//                $("#ddlBrandId").attr({ "datatype": "*", "errormsg": "请选择品牌", "sucmsg": " " });

            } else {
                $("#dlgift,#dlNum").css("display", "none").removeAttr("datatype errormsg sucmsg");
                $("#dlrpter").css("display", "block");
            }

        });


        //初始化上传控件
        $(".upload-img").each(function () {
            $(this).InitSWFUpload({ sendurl: "../../tools/upload_ajax.ashx", flashurl: "../../scripts/swfupload/swfupload.swf" });
        });

    });
    //添加一行变量
    function addVarTr() {
        //创建HTML的TR
        var varHtml = '<tr>'
            + '<td style="display: none"><input id="txtid" type="text" name="itemid" value="0"/></td>'
            + '<td><span>充值</span>'
            + '<input id="txtGroupPrice" name="itemfields" class="td-input groupprice" type="text"/><span>送</span>'
            + '</td><td>'
            + '<input id="txtValues" name="itemvalues" class="td-input groupprice" type="text"/>'
            + '</td>'
            + '<td><a title="删除" class="img-btn del operator" onclick="delVarTr(this);">删除</a></td></tr>';
        $("#tr_box").append(varHtml);
    }
    //删除一行变量
    function delVarTr(obj) {
        $(obj).parent().parent().remove();
    }
</script>
</head>

<body class="mainbody">
<form id="form1" runat="server">
<!--导航栏-->
<div class="location">
  <a href="promotion_list.aspx" class="back"><i></i><span>返回列表页</span></a>
  <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
  <i class="arrow"></i>
  <a href="promotion_list.aspx"><span>活动</span></a>
  <i class="arrow"></i>
  <span>编辑活动</span>
</div>
<div class="line10"></div>
<!--/导航栏-->

<!--内容-->
<div class="content-tab-wrap">
  <div id="floatHead" class="content-tab">
    <div class="content-tab-ul-wrap">
      <ul>
        <li><a href="javascript:;" onclick="tabs(this);" class="selected">编辑活动</a></li>
      </ul>
    </div>
  </div>
</div>

<div class="tab-content">
  <dl>
    <dt>活动类型</dt>
    <dd>
      <div class="rule-single-select">
          <select id="ddlRoleId" runat="server" datatype="*" errormsg="请选择活动类型" sucmsg=" ">
              <option value="0">请选择活动...</option>
              <option value="1">满额减价</option>
              <option value="2">赠送礼品</option>
          </select>
      </div>
    </dd>
  </dl>
  <dl>
    <dt>商品类型</dt>
    <dd>
      <div class="rule-multi-checkbox">
        <asp:CheckBoxList ID="cblItem" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow"></asp:CheckBoxList>
      </div>
      <span class="Validform_checktip">*可多选</span>
    </dd>
  </dl>
  <dl>
    <dt>是否启用</dt>
    <dd>
      <div class="rule-single-checkbox">
          <asp:CheckBox ID="cbIsLock" runat="server" Checked="True" />
      </div>
      <span class="Validform_checktip">*不启用则活动不会生效</span>
    </dd>
  </dl>
  <dl>
    <dt>生效时间</dt>
    <dd>
        <div class="input-date">
            <asp:TextBox ID="txtStartTime" runat="server" CssClass="input date" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"
                datatype="/^\s*$|^\d{4}\-\d{1,2}\-\d{1,2}\s{1}(\d{1,2}:){2}\d{1,2}$/" errormsg="请选择正确的日期" sucmsg=" " /><i>日期</i>
        </div>
        <span class="Validform_checktip">不选择默认当前发布时间</span>
    </dd>
  </dl>
  <dl>
    <dt>结束时间</dt>
    <dd>
        <div class="input-date">
            <asp:TextBox ID="txtEndTime" runat="server" CssClass="input date" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"
                datatype="/^\s*$|^\d{4}\-\d{1,2}\-\d{1,2}\s{1}(\d{1,2}:){2}\d{1,2}$/" errormsg="请选择正确的日期" sucmsg=" " /><i>日期</i>
        </div>
        <span class="Validform_checktip">不选择默认当前发布时间</span>
    </dd>
  </dl>
  <dl runat="server" id="dlgift" Visible="True">
      <dt>赠送商品</dt>
      <dd>
          <asp:TextBox runat="server" ID="txtGoodId" CssClass="input small" datatype="*" errorMsg="*请填写商品"></asp:TextBox>
          <span class="Validform_checktip">*请填写商品编号</span>
      </dd>
  </dl>
  <dl runat="server" id="dlNum" Visible="True">
      <dt>数量</dt>
      <dd>
          <asp:TextBox runat="server" ID="txtGoodNum" CssClass="input small" datatype="*" errorMsg="*请填写数量"></asp:TextBox>
          <span class="Validform_checktip">*请填写数量</span>
      </dd>
  </dl>
  <dl ID="dlrpter" runat="server" style="display: none;">
    <dt>充值活动：</dt>
    <dd><a class="icon-btn add" onclick="addVarTr();"><i></i><span>添加活动</span></a>
        <asp:HiddenField ID="HideId" Value='<%#Eval("ID") %>' runat="server" />
    </dd>
    <dd>
        <table id="tr_box" border="0" cellspacing="0" cellpadding="0" class="border-table">
        <asp:Repeater ID="rptPrice" runat="server">
            <ItemTemplate>
            <tr>
                <td style="display: none"><input id="txtid" type="hidden" name="itemid" value="<%#Eval("ID") %>"/></td>
                <td><span>充值</span>
                    <input type="text" name="itemfields" class="td-input groupprice"  value='<%#Eval("fields") %>'/><span>送</span>
                </td>
                <td>
                    <input type="text" name="itemvalues" class="td-input groupprice" value='<%#Eval("value") %>'/>
                </td>
                <td><a title="删除" class="img-btn del operator" onclick="delVarTr(this);">删除</a></td>
            </tr>
            </ItemTemplate>
        </asp:Repeater>
        </table>
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
