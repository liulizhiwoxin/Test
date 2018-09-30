<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="manager_edit.aspx.cs" Inherits="BuysingooShop.Admin.manager.manager_edit" ValidateRequest="false" %>
<%@ Import Namespace="Vincent" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>编辑管理员</title>
<script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
<script type="text/javascript" src="../../scripts/jquery/Validform_v5.3.2_min.js"></script>
<script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
<script type="text/javascript" src="../../scripts/swfupload1/swfupload.js"></script>
<script type="text/javascript" src="../../scripts/swfupload1/swfupload.queue.js"></script>
<script type="text/javascript" src="../../scripts/swfupload1/swfupload.handlers.js"></script>
<script type="text/javascript" src="../js/layout.js"></script>
<link href="../skin/default/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../scripts/PCASClass.js"></script>


<script type="text/javascript">
    $(function () {
        new PCAS("provinces", "city", "area");

        $("#provinces,#city,#area").change(function () {
            $("#provinces1").val($("#provinces option:selected").text());
            $("#city1").val($("#city option:selected").text());
            $("#area1").val($("#area option:selected").text());
        });
        $("#provinces option:selected").text($("#provinces1").val());
        $("#city option:selected").text($("#city1").val());
        $("#area option:selected").text($("#area1").val());


        //初始化表单验证
        $("#form1").initValidform();

        //选择酒厂或者经销商时
        $("#ddlRoleId").change(function () {
            var vals = $(this).val();
            if (vals == "3") {
                //酒厂
                $("#Code,#CodeRage,#dlAge,#dlWorkAge,#dlstyle,#dlImgUrl,#dlRemark,#dlQQ").css("display", "none");
                $("#ddlWineryId").removeAttr("datatype errormsg sucmsg");
                $("#Winery").css("display", "none");

                $("#Brand").css("display", "block");
                $("#ddlBrandId").attr({ "datatype": "*", "errormsg": "请选择店铺", "sucmsg": " " });
                
            }
            //else if (vals == "4") {
            //    //经销商
            //    $("#Winery").css("display", "block");
            //    $("#ddlWineryId").attr({ "datatype": "*", "errormsg": "请选择酒厂", "sucmsg": " " });
            //    $("#Code,#CodeRage").css("display", "block");
            //    $("#Brand,#dlAge,#dlWorkAge,#dlstyle,#dlImgUrl,#dlRemark,#dlQQ").css("display", "none");
            //}
            //else if (vals == "5") {
            //    //设计师
            //    $("#dlAge,#dlWorkAge,#dlstyle,#dlImgUrl,#dlRemark,#dlQQ").css("display", "block");
            //    $("#txtAge").attr({ "datatype": "*", "errormsg": "请填写年龄", "sucmsg": " " });
            //    $("#txtWorkAge").attr({ "datatype": "*", "errormsg": "请填写从业年龄", "sucmsg": " " });
            //    $("#txtImgUrl").attr({ "datatype": "*", "errormsg": "请上传照片", "sucmsg": " " });
            //}
            //else { 
            //    $("#Code,#CodeRage").css("display", "none");
            //    $("#ddlWineryId").removeAttr("datatype errormsg sucmsg");
            //    $("#Winery").css("display", "none");
            //    $("#Brand").css("display", "none");
            //    $("#ddlBrandId").removeAttr("datatype errormsg sucmsg");
            //    $("#dlAge,#dlWorkAge,#dlstyle,#dlImgUrl,#dlRemark,#dlQQ").css("display", "none").removeAttr("datatype errormsg sucmsg");
            //}
        });


        //初始化上传控件
        $(".upload-img").each(function () {
            $(this).InitSWFUpload({ sendurl: "../../tools/upload_ajax.ashx", flashurl: "../../scripts/swfupload/swfupload.swf" });
        });

    });
</script>
</head>

<body class="mainbody">
<form id="form1" runat="server">
<!--导航栏-->
<div class="location">
  <a href="manager_list.aspx" class="back"><i></i><span>返回列表页</span></a>
  <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
  <i class="arrow"></i>
  <a href="manager_list.aspx"><span>管理员</span></a>
  <i class="arrow"></i>
  <span>编辑管理员</span>
</div>
<div class="line10"></div>
<!--/导航栏-->

<!--内容-->
<div class="content-tab-wrap">
  <div id="floatHead" class="content-tab">
    <div class="content-tab-ul-wrap">
      <ul>
        <li><a href="javascript:;" onclick="tabs(this);" class="selected">编辑管理员</a></li>
      </ul>
    </div>
  </div>
</div>

<div class="tab-content">
  <dl>
    <dt>管理角色</dt>
    <dd>
      <div class="rule-single-select">
        <asp:DropDownList id="ddlRoleId" runat="server" datatype="*" errormsg="请选择管理员角色" sucmsg=" "></asp:DropDownList>
      </div>
    </dd>
  </dl>
  
  <dl id="Brand" style="display:block;" runat="server">
    <dt>所属店铺</dt>
    <dd>
      <div class="rule-single-select">
        <asp:DropDownList id="ddlBrandId" runat="server" ></asp:DropDownList>
      </div>
    </dd>
  </dl>

    
    <dl>
                <dt>省份</dt>
                <dd>
                    <div>
                        <select runat="server" id="provinces" name="provinces"></select>
                        <input id="provinces1" name="provinces1" type="hidden" value="所在省份" runat="server" />
                    </div>
                </dd>
            </dl>
            <dl>
                <dt>城市</dt>
                <dd>
                    <div>
                        <select runat="server" id="city" name="city"></select>
                        <input id="city1" name="city1" type="hidden" value="所在城市" runat="server" />
                    </div>
                </dd>
            </dl>


  <dl id="Code" style="display:none;" runat="server">
    <dt>折扣代码</dt>
    <dd><asp:TextBox ID="txtStrCode" runat="server" CssClass="input normal"></asp:TextBox></dd>
  </dl>
  <dl id="CodeRage" style="display:none;" runat="server">
    <dt>折扣率</dt>
    <dd><asp:TextBox ID="txtCodeRage" runat="server" CssClass="input normal"></asp:TextBox>
    <span class="Validform_checktip">*0~1之间的小数</span>
    </dd>
  </dl>
  <dl>
    <dt>是否启用</dt>
    <dd>
      <div class="rule-single-checkbox">
          <asp:CheckBox ID="cbIsLock" runat="server" Checked="True" />
      </div>
      <span class="Validform_checktip">*不启用则无法使用该账户登录</span>
    </dd>
  </dl>
  <dl>
    <dt>用户名</dt>
    <dd><asp:TextBox ID="txtUserName" runat="server" CssClass="input normal" datatype="/^[a-zA-Z0-9\-\_]{2,50}$/" sucmsg=" " ajaxurl="../../tools/admin_ajax.ashx?action=manager_validate"></asp:TextBox> <span class="Validform_checktip">*字母、下划线，不可修改</span></dd>
  </dl> 
  <dl>
    <dt>登录密码</dt>
    <dd><asp:TextBox ID="txtPassword" runat="server" CssClass="input normal" TextMode="Password" datatype="*6-20" nullmsg="请设置密码" errormsg="密码范围在6-20位之间" sucmsg=" "></asp:TextBox> 
        <span class="Validform_checktip">*</span></dd>
  </dl>
  <dl>
    <dt>确认密码</dt>
    <dd><asp:TextBox ID="txtPassword1" runat="server" CssClass="input normal" TextMode="Password" datatype="*" recheck="txtPassword" nullmsg="请再输入一次密码" errormsg="两次输入的密码不一致" sucmsg=" "></asp:TextBox> <span class="Validform_checktip">*</span></dd>
  </dl>
  <dl>
    <dt>姓名</dt>
    <dd><asp:TextBox ID="txtRealName" runat="server" CssClass="input normal"></asp:TextBox></dd>
  </dl>
  <dl id="dlImgUrl" runat="server" style="display: none;">
    <dt>封面图片</dt>
    <dd>
      <asp:TextBox ID="txtImgUrl" runat="server" CssClass="input normal upload-path" /><div class="upload-box upload-img"></div>
      <span class="Validform_checktip">*</span></dd>
  </dl>
  <dl id="dlAge" runat="server" style="display:none;">
    <dt>年龄</dt>
    <dd><asp:TextBox ID="txtAge" runat="server" CssClass="input normal"></asp:TextBox>
    <span class="Validform_checktip">*</span></dd>
  </dl>
  <dl id="dlWorkAge" runat="server" style="display:none;">
    <dt>从业年龄</dt>
    <dd><asp:TextBox ID="txtWorkAge" runat="server" CssClass="input normal"></asp:TextBox>
    <span class="Validform_checktip">*</span></dd>
  </dl>
  <dl id="dlstyle" runat="server" style="display:none;">
    <dt>擅长风格</dt>
    <dd><asp:TextBox ID="txtStyle" runat="server" CssClass="input normal"></asp:TextBox></dd>
  </dl>
  <dl>
    <dt>电话</dt>
    <dd><asp:TextBox ID="txtTelephone" runat="server" CssClass="input normal"></asp:TextBox></dd>
  </dl>
  <dl id="dlQQ" runat="server" style="display: none;">
    <dt>QQ</dt>
    <dd><asp:TextBox ID="txtQQ" runat="server" CssClass="input normal"></asp:TextBox></dd>
  </dl>
  <dl>
    <dt>邮箱</dt>
    <dd><asp:TextBox ID="txtEmail" runat="server" CssClass="input normal"></asp:TextBox></dd>
  </dl>
  <dl id="dlRemark" runat="server" style="display: none">
    <dt>备注</dt>
    <dd><asp:TextBox ID="txtRemark" runat="server" CssClass="input normal"></asp:TextBox></dd>
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
