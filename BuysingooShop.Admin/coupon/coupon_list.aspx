<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="coupon_list.aspx.cs" Inherits="BuysingooShop.Admin.coupon.coupon_list" %>
<%@ Import Namespace="Vincent" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>全部优惠券</title>
<script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
<script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
<script type="text/javascript" src="../js/layout.js"></script>
<link  href="../../css/pagination.css" rel="stylesheet" type="text/css" />
<link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
</head>

<body class="mainbody">
<form id="form1" runat="server">
<!--导航栏-->
<div class="location">
  <a href="javascript:history.back(-1);" class="back"><i></i><span>返回上一页</span></a>
  <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
  <i class="arrow"></i>
  <span>优惠券列表</span>
</div>
<!--/导航栏-->

<!--工具栏-->
<div class="toolbar-wrap">
  <div id="floatHead" class="toolbar">
    <div class="l-list">
      <ul class="icon-list">
        <li><a class="all" href="javascript:;" onclick="checkAll(this);"><i></i><span>全选</span></a></li>
        <li><a class="add" href="coupon_edit.aspx?action=<%=Vincent._DTcms.DTEnums.ActionEnum.Add %>"><i></i><span>新增</span></a></li>
        <li><asp:LinkButton ID="btnDelete" runat="server" CssClass="del" OnClientClick="return ExePostBack('btnDelete','是否确定删除？');" onclick="btnDelete_Click"><i></i><span>删除</span></asp:LinkButton></li>
      </ul>
      <div class="menu-list">
        <div class="rule-single-select">
          <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True" onselectedindexchanged="ddlStatus_SelectedIndexChanged">
          <asp:ListItem Value="" Selected="True">优惠券状态</asp:ListItem>
            <asp:ListItem Value="1">未使用</asp:ListItem>
            <asp:ListItem Value="2">已使用</asp:ListItem>
            <asp:ListItem Value="3">已过期</asp:ListItem>
          </asp:DropDownList>
        </div>
        <div style="display:none" class="rule-single-select">
          <asp:DropDownList ID="ddlPaymentStatus" runat="server" AutoPostBack="True" onselectedindexchanged="ddlPaymentStatus_SelectedIndexChanged">
            <asp:ListItem Value="" Selected="True">优惠券类型</asp:ListItem>
            <asp:ListItem Value="1">平台优惠券</asp:ListItem>
            <asp:ListItem Value="2">品牌优惠券</asp:ListItem>
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
    <th align="left">优惠券编号</th>
    <th align="left" width="12%">名称</th>
    <th align="left" width="10%">备注</th>
    <th align="left" width="10%">类型</th>
    <th width="8%">金额</th>
    <th width="10%">开始时间</th>
    <th align="center" width="16%">结束时间</th>
    <th width="8%">优惠券状态</th>
  </tr>
</HeaderTemplate>
<ItemTemplate>
  <tr>
    <td align="center">
      <asp:CheckBox ID="chkId" CssClass="checkall" runat="server"/>
      <asp:HiddenField ID="hidId" Value='<%#Eval("id")%>' runat="server" />
    </td>
    <td align="left"><%#Eval("str_code")%></td>
    <td align="left"><%#Eval("title").ToString()%></td>
    <td align="left"><%#Eval("remark")%></td>
    <td align="left"><%#Eval("type")%></td>
    <td align="center"><%#Eval("amount")%></td>
    <td align="center"><%#string.Format("{0:g}", Eval("start_time"))%></td>
    <td align="center"><%#string.Format("{0:g}", Eval("end_time"))%></td>
    <td align="center"><%#GetCouponStatus(Convert.ToInt32(Eval("id")))%></td>
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
