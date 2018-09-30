<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="refund_list.aspx.cs" Inherits="BuysingooShop.Admin.order.refund_list" %>
<%@ Import Namespace="Vincent" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>退款管理</title>
<script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
<script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
<script type="text/javascript" src="../js/layout.js"></script>
<link  href="../../css/pagination.css" rel="stylesheet" type="text/css" />
<link href="../skin/default/style.css" rel="stylesheet" type="text/css" />

<script type="text/javascript">
    $(function () {



    });

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


    //驳回退款
    function unAcceptRefund(obj) {
        var dialog = $.dialog({
            title: '驳回退款',
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
                var postData = { "refund_id": $(obj).parent(".refund_id").attr("data"), "editType": "unaccept", "refund_remark": remark };
                //发送AJAX请求
                sendAjaxUrl(dialog, postData, "../../tools/admin_ajax.ashx?action=edit_refund_status");
                return false;
            },
            cancel: true
        });
    }

    //同意退款
    function AcceptRefund(obj) {
        $.ajax({
            type: "post",
            url: "../../tools/admin_ajax.ashx?action=edit_refund_status",
            data: { "refund_id": $(obj).parent(".refund_id").attr("data"), "editType": "accept" },
            success: function (data) {
                var v = $.parseJSON(data);
                $.dialog.alert('提示：' + v.msg, function () { location.reload(); });
            }
        });
    }
</script>

</head>

<body class="mainbody">
<form id="form1" runat="server">
<!--导航栏-->
<div class="location">
  <a href="javascript:history.back(-1);" class="back"><i></i><span>返回上一页</span></a>
  <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
  <i class="arrow"></i>
  <span>退款列表</span>
</div>
<!--/导航栏-->

<!--工具栏-->
<div class="toolbar-wrap">
  <div id="floatHead" class="toolbar">
    <div class="l-list">
      <ul class="icon-list">
        <li><a class="all" href="javascript:;" onclick="checkAll(this);"><i></i><span>全选</span></a></li>
        <li><asp:LinkButton ID="btnDelete" runat="server" CssClass="del" OnClientClick="return ExePostBack('btnDelete','是否确认删除？');" onclick="btnDelete_Click"><i></i><span>删除</span></asp:LinkButton></li>
      </ul>
      <div class="menu-list">
        <div class="rule-single-select">
          <asp:DropDownList ID="ddlrefund_status" runat="server" AutoPostBack="True" onselectedindexchanged="ddlrefundstatus_SelectedIndexChanged">
            <asp:ListItem Value="" Selected="True">退款状态</asp:ListItem>
            <asp:ListItem Value="1">已生成退款</asp:ListItem>
            <asp:ListItem Value="2">已确认退款</asp:ListItem>
            <asp:ListItem Value="3">已完成退款</asp:ListItem>
            <asp:ListItem Value="4">已取消退款</asp:ListItem>
            <asp:ListItem Value="5">已驳回退款</asp:ListItem>
          </asp:DropDownList>
        </div>
      </div>
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
    <th align="center" width="7%">用户名</th>
    <th align="center">退款单号</th>
    <th align="center">订单号</th>
    <th align="center" width="10%">供应商</th>
    <th align="center" width="8%">交易金额</th>
    <th align="center" width="8%">退款金额</th>
    <th align="center" width="10%">联系方式</th>
    <th align="center" width="8%">快递单号</th>
    <th align="center" width="8%">快递代码</th>
    <th align="center" width="8%">退款状态</th>
    <th align="center" width="8%">申请时间</th>
    <th align="center" width="10%">操作</th>
  </tr>
</HeaderTemplate>
<ItemTemplate>
  <tr>
    <td align="center">
      <asp:CheckBox ID="chkId" CssClass="checkall" runat="server" style="vertical-align:middle;" />
      <asp:HiddenField ID="hidId" Value='<%#Eval("id")%>' runat="server" />
    </td>
    <td align="center"><%#Eval("user_name").ToString() == "" ? "匿名用户" : Eval("user_name").ToString()%></td>
    <td align="center"><%#Eval("refund_no")%></td>
    <td align="center"><a href="order_edit.aspx?action=<%#Vincent._DTcms.DTEnums.ActionEnum.Edit %>&refund_id=<%#Eval("id")%>"><%#Eval("order_no")%></a></td>
    <td align="center"><%#new BuysingooShop.BLL.orders().GetBrandName(Eval("order_no").ToString())%></td>
    <td align="center"><%#GetPayAmount(Eval("order_no").ToString())%></td>
    <td align="center"><%#Eval("refund_money")%></td>
    <td align="center"><%#GetSingleValue(Eval("order_no").ToString())%></td>
    <td align="center"><%#Eval("express_no")%></td>
    <td align="center"><%#Eval("express_code")%></td>
    <td align="center"><%#GetRefund_status(Convert.ToInt32(Eval("refund_status")))%></td>
    <td align="center"><%#string.Format("{0:g}", Eval("apply_time"))%></td>
    <td align="center"><span class="refund_id" data="<%#Eval("id") %>"><a href="javascript:void(0)" onclick="AcceptRefund(this)" style="color: #2a72c5;" id="acceptRefund"><%#GetOrderStatus(Convert.ToInt32(Eval("id")))%>
        <a href="javascript:void(0)" onclick="unAcceptRefund(this)" style="color: #2a72c5;" id="unAcceptRefund">不同意</a></span>
    </td>
  </tr>
</ItemTemplate>
<FooterTemplate>
  <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"9\">暂无记录</td></tr>" : ""%>
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
