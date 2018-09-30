<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="withdraw_list.aspx.cs" Inherits="BuysingooShop.Admin.finance.withdraw_list" %>
<%@ Import Namespace="Vincent" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>客户日充值</title>
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script type="text/javascript" src="../js/layout.js"></script>
    <script type="text/javascript" src="../../scripts/swfupload1/swfupload.js"></script>
    <script type="text/javascript" src="../../scripts/swfupload1/swfupload.queue.js"></script>
    <script type="text/javascript" src="../../scripts/swfupload1/swfupload.handlers.js"></script>
    <link  href="../../css/pagination.css" rel="stylesheet" type="text/css" />
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />


    <script type="text/javascript">
        

        //发送AJAX请求
        function sendAjaxUrl(winObj, postData, sendUrl) {
            $.ajax({
                type: "post",
                url: sendUrl,
                data: postData,
                dataType: "json",
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    $.dialog.alert('尝试发送失败，错误信息：' + errorThrown, function () { }, winObj);
                },
                success: function (data, textStatus) {
                    if (data.status == 1) {
                        winObj.close();
                        $.dialog.tips(data.msg, 2, '32X32/succ.png', function () { location.reload(); }); //刷新页面
                    } else {
                        $.dialog.alert('错误提示：' + data.msg, function () { }, winObj);
                    }
                }
            });
        };


        //驳回提现
        function unAcceptRefund(obj) {
            var dialog = $.dialog({
                title: '驳回提现',
                content: '<textarea id="txtOrderRemark" name="txtOrderRemark" rows="2" cols="20" class="input"></textarea>',
                min: false,
                max: false,
                lock: true,
                ok: function () {
                    var remark = $("#txtOrderRemark", parent.document).val();
                    if (remark == "") {
                        $.dialog.alert('对不起，请输入内容！', function () { }, dialog);
                        return false;
                    }
                    var postData = { "withdraw_id": $(obj).parent(".withdraw_id").attr("data"), "editType": "unaccept", "remark": remark };
                    //发送AJAX请求
                    sendAjaxUrl(dialog, postData, "../../tools/admin_ajax.ashx?action=edit_withdraw_status");
                    return false;
                },
                cancel: true
            });
        }

        //同意提现
        function AcceptRefund(obj) {
            var dialog = $.dialog({
                title: '同意提现',
                content: 'url:dialog/dialog_withdraw.aspx?withdraw_id=' + $(obj).parent(".withdraw_id").attr("data"),
                min: false,
                max: false,
                lock: true,
                width: 450
            });
        }


//        //同意提现
//        function AcceptRefund() {
//            $.ajax({
//                type: "post",
//                url: "../../tools/admin_ajax.ashx?action=edit_withdraw_status",
//                data: { "withdraw_id": $("#hiddenId").attr("data"), "editType": "accept" },
//                success: function (data) {
//                    var v = $.parseJSON(data);
//                    $.dialog.alert('提示：' + v.msg, function () { location.reload(); });
//                }
//            });
//        }
</script>
</head>

<body class="mainbody">
    <form id="form1" runat="server">
    <!--导航栏-->
<div class="location">
  <a href="javascript:history.back(-1);" class="back"><i></i><span>返回上一页</span></a>
  <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
  <i class="arrow"></i>
  <span>提现</span>
</div>
<!--/导航栏-->

<!--工具栏-->
<div class="toolbar-wrap">
  <div id="floatHead" class="toolbar">
    <div class="l-list">
      <ul class="icon-list">
        <li><a class="all" href="javascript:;" onclick="checkAll(this);"><i></i><span>全选</span></a></li>
        <li><asp:LinkButton ID="btnDelete" runat="server" CssClass="del" OnClientClick="return ExePostBack('btnDelete');" onclick="btnDelete_Click"><i></i><span>删除</span></asp:LinkButton></li>
      </ul>
    </div>
    <div class="r-list">
      <asp:TextBox ID="txtKeywords" runat="server" CssClass="keyword" />
      <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="btn-search" onclick="btnSearch_Click">查询</asp:LinkButton>
    </div>
  </div>
</div>
<!--/工具栏-->

<!--列表-->
<asp:Repeater ID="rptList" runat="server">
<HeaderTemplate>
<table width="100%" border="0" cellspacing="0" cellpadding="0" class="ltable">
  <tr>
    <th width="8%">选择</th>
    <th align="left" width="10%">用户名</th>   
    <th align="left" width="10%">用户账号</th>   
    <th align="left" width="10%">提现金额</th>
    <th align="left" width="10%">状态</th>
    <th align="left" width="10%">备注</th>
    <th align="left" width="10%">时间</th>
    <th align="left" width="10%">手机号</th>
    <th align="left" width="10%">微信OpenId</th>
    <th align="left" width="10%">操作</th>
  </tr>
</HeaderTemplate>
<ItemTemplate>
  <tr>
    <td align="center">
      <asp:CheckBox ID="chkId" CssClass="checkall" runat="server" style="vertical-align:middle;" />
      <asp:HiddenField ID="hidId" Value='<%#Eval("id")%>' runat="server" />
    </td>
        <td><%#Getusernick_name(Convert.ToInt32(Eval("user_id")))%></td>
        <td><%#Getusername(Convert.ToInt32(Eval("user_id")))%></td>
    <%--<td><%#Eval("card_no")%></td>
    <td><%#Getbanktype(Convert.ToInt32(Eval("banktype")))%></td>--%>
    <td><%#Eval("amount")%></td>
    <td style="color:red;"><%#Getstatus(Convert.ToInt32(Eval("status")))%></td>
    <td><%#Eval("remark")%></td>
    <td><%#string.Format("{0:g}", Eval("addtime")) == null ? "" : string.Format("{0:g}", Eval("addtime"))%></td>
       <td><%#Eval("mobile")%></td>
       <td><%#Eval("openid")%></td>
    <td align="left">

        <span class="withdraw_id" data="<%#Eval("id")%>"><a href="javascript:void(0)" onclick="AcceptRefund(this)" style="color: #2a72c5;" id="acceptRefund">同意</a>&nbsp;&nbsp;
        <a href="javascript:void(0)" onclick="unAcceptRefund(this)" style="color: #2a72c5;" id="unAcceptRefund">不同意</a></span>
    </td>
    <%--Eval("user_name").ToString() == "" ? "匿名用户" : Eval("user_name").ToString()--%>
  </tr>
</ItemTemplate>
<FooterTemplate>
  <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"5\">暂无记录</td></tr>" : ""%>
</table>
</FooterTemplate>
</asp:Repeater>
<!--/列表-->

<!--内容底部-->
<div class="line20"></div>
<div class="pagelist">
  <div class="l-btns">
    <span>显示</span><asp:TextBox ID="txtPageNum" runat="server" CssClass="pagenum" onkeydown="return checkNumber(event);" ontextchanged="txtPageNum_TextChanged" AutoPostBack="True"></asp:TextBox><span>条/页</span>
  </div>
  <div id="PageContent" runat="server" class="default"></div>
</div>
<!--/内容底部-->
    </form>
</body>
</html>
