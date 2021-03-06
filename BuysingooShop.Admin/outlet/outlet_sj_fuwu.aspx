﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="outlet_sj_fuwu.aspx.cs" Inherits="BuysingooShop.Admin.outlet.outlet_sj_fuwu" %>
<%@ Import Namespace="Vincent" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>订单管理</title>
<script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
<script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
<script type="text/javascript" src="../js/layout.js"></script>
<link  href="../../css/pagination.css" rel="stylesheet" type="text/css" />
<link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">

</script>

</head>

<body class="mainbody">
<form id="form1" runat="server">
<!--导航栏-->
<div class="location">
  <a href="javascript:history.back(-1);" class="back"><i></i><span>返回上一页</span></a>
  <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
  <i class="arrow"></i>
  <span>订单列表</span>
</div>
<!--/导航栏-->

<!--工具栏-->
<div class="toolbar-wrap">
  <div id="floatHead" class="toolbar">
    <div class="l-list">
      <ul class="icon-list">
        <li><a class="add" href="outlet_edit.aspx?action=<%=Vincent._DTcms.DTEnums.ActionEnum.Add %>"><i></i><span>新增</span></a></li>
        <li><a class="all" href="javascript:;" onclick="checkAll(this);"><i></i><span>全选</span></a></li>
        <li><asp:LinkButton ID="btnDelete" runat="server" CssClass="del" OnClientClick="return ExePostBack('btnDelete','是否确认删除？');" onclick="btnDelete_Click"><i></i><span>删除</span></asp:LinkButton></li>
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
    <th >选择</th>
    <th align="left">店铺名称</th>
    <th align="center">省份</th>
    <th align="center">城市</th>
 <%--   <th align="left">区域</th>--%>
    <th align="left">地址</th>
  
    <th align="left">联系电话</th>
    <th  align="left">操作</th>
  </tr>
</HeaderTemplate>
<ItemTemplate>
  <tr>
    <td align="center">
      <asp:CheckBox ID="chkId" CssClass="checkall" runat="server" style="vertical-align:middle;" />
      <input type="hidden" value="<%#Eval("id")%>"/>
      <asp:HiddenField ID="hidId" Value='<%#Eval("id")%>' runat="server" />
    </td>
    <td><%#Eval("name")%></td>
    <td align="center"><%#Eval("provinces")%></td>
    <td align="center"><%#Eval("city")%></td>
 <%--   <td><%#Eval("area")%></td>--%>
    <td><%#Eval("address")%></td>
 
    <td><%#Eval("mobile")%></td>
    <td align="left"><a href="outlet_edit.aspx?action=<%#Vincent._DTcms.DTEnums.ActionEnum.Edit %>&id=<%#Eval("id")%>">店铺管理</a>&nbsp;&nbsp;
   <a href="outlet_fuwuzhan.aspx?action=<%#Vincent._DTcms.DTEnums.ActionEnum.Edit %>&id=<%#Eval("id")%>&uid=<%#Eval("userId") %> ">查看详情</a>&nbsp;&nbsp;</td>

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

