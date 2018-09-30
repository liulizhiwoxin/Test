<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="group_edit.aspx.cs" Inherits="BuysingooShop.Admin.users.group_edit" %>
<%@ Import Namespace="Vincent" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑用户组</title>
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script type="text/javascript" src="../js/layout.js"></script>
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            //初始化表单验证
            $("#form1").initValidform();
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
        <a href="group_list.aspx" class="back"><i></i><span>返回列表页</span></a> <a href="../center.aspx"
            class="home"><i></i><span>首页</span></a> <i class="arrow"></i><a href="group_list.aspx">
                <span>会员组别</span></a> <i class="arrow"></i><span>编辑组别</span>
    </div>
    <div class="line10">
    </div>
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
            <dt>组别名称：</dt>
            <dd>
                <asp:TextBox ID="txtTitle" runat="server" CssClass="input normal" datatype="*1-100" sucmsg=" " minlength="2" MaxLength="100"></asp:TextBox><span
                    class="Validform_checktip">*</span></dd>
        </dl>
        <dl>
            <dt>是否隐藏：</dt>
            <dd>
                <div class="rule-single-checkbox">
                    <asp:CheckBox ID="rblIsLock" runat="server" />
                </div>
                <span class="Validform_checktip">*隐藏后，用户将无法升级或显示该组别。</span>
            </dd>
        </dl>
        <dl>
            <dt>注册默认会员组：</dt>
            <dd>
                <div class="rule-single-checkbox">
                    <asp:CheckBox ID="rblIsDefault" runat="server" />
                </div>
                <span class="Validform_checktip">*用户注册成功后自动默认为该会员组，如果存在多条，则以等级值最小的为准。</span>
            </dd>
        </dl>
        <dl>
            <dt>参与自动升级：</dt>
            <dd>
                <div class="rule-single-checkbox">
                    <asp:CheckBox ID="rblIsUpgrade" runat="server" />
                </div>
                <span class="Validform_checktip">*如果是否，在满足升级条件下系统则不会自动升级为该会员组。</span>
            </dd>
        </dl>
        <dl>
            <dt>等级值：</dt>
            <dd>
                <asp:TextBox ID="txtGrade" runat="server" CssClass="input small" datatype="n" sucmsg=" "></asp:TextBox><span
                    class="Validform_checktip">*升级顺序，取值范围1-100，等级值越大，会员等级越高。</span></dd>
        </dl>
        <dl>
            <dt>升级所需积分：</dt>
            <dd>
                <asp:TextBox ID="txtUpgradeExp" runat="server" CssClass="input small" datatype="n"
                    sucmsg=" "></asp:TextBox><span class="Validform_checktip">*自动升级所需要的积分。</span></dd>
        </dl>
        <dl>
            <dt>初始金额：</dt>
            <dd>
                <asp:TextBox ID="txtAmount" runat="server" CssClass="input small" datatype="/^(([1-9]{1}\d*)|([0]{1}))(\.(\d){1,2})?$/" sucmsg=" "></asp:TextBox><span
                    class="Validform_checktip">*自动到该会员组赠送的金额，负数则扣减。</span></dd>
        </dl>
        <dl>
            <dt>初始积分：</dt>
            <dd>
                <asp:TextBox ID="txtPoint" runat="server" CssClass="input small" datatype="n" sucmsg=" "></asp:TextBox><span
                    class="Validform_checktip">*自动到该会员组赠送的积分，负数则扣减。</span></dd>
        </dl>
        <dl>
            <dt>购物折扣：</dt>
            <dd>
                <asp:TextBox ID="txtDiscount" runat="server" CssClass="input small" datatype="n"
                    sucmsg=" "></asp:TextBox><span class="Validform_checktip">*购物享受的折扣，取值范围：1-100。</span></dd>
        </dl>
 
      <dl ID="dlrpter" runat="server" Visible="False">
        <dt>充值活动：</dt>
        <dd><a class="icon-btn add" onclick="addVarTr();"><i></i><span>添加活动</span></a>
            <asp:HiddenField ID="HideId" Value='<%#Eval("ID") %>' runat="server" />
            <asp:HiddenField ID="hideGroupId" Value='<%#Eval("Group_Id") %>' runat="server" />
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
    <!--内容-->
    <!--工具栏-->
    <div class="page-footer">
        <div class="btn-list">
            <asp:Button ID="btnSubmit" runat="server" Text="提交保存" CssClass="btn" OnClick="btnSubmit_Click" />
            <input name="btnReturn" type="button" value="返回上一页" class="btn yellow" onclick="javascript:history.back(-1);" />
        </div>
        <div class="clear">
        </div>
    </div>
    <!--/工具栏-->
    </form>
</body>
</html>
