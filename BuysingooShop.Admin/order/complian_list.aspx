<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="complian_list.aspx.cs" Inherits="BuysingooShop.Admin.order.complian_list" %>
<%@ Import Namespace="Vincent" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>投诉管理</title>
<script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
<script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
<script type="text/javascript" src="../js/layout.js"></script>
<link  href="../../css/pagination.css" rel="stylesheet" type="text/css" />
<link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    $(function () {
        $(".complianDetail").click(function () {
            var dialog = $.dialog({
                title: '投诉详情',
                content: 'url:dialog/dialog_complian.aspx?id=' + $(this).attr("data"),
                min: false,
                max: false,
                lock: true,
                width: 550,
                height: 320
            });
        });
    });
</script>

</head>

<body class="mainbody">
<form id="form1" runat="server">
<!--导航栏-->
<div class="location">
  <a href="javascript:history.back(-1);" class="back"><i></i><span>返回上一页</span></a>
  <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
  <i class="arrow"></i>
  <span>投诉列表</span>
</div>
<!--/导航栏-->

<!--工具栏-->
<div class="toolbar-wrap">
  <div id="floatHead" class="toolbar">
    <div class="l-list">
      <ul class="icon-list">
        <li><a class="all" href="javascript:;" onclick="checkAll(this);"><i></i><span>全选</span></a></li>
        <li><asp:LinkButton ID="btnDelete" runat="server" CssClass="del" OnClientClick="return ExePostBack('btnDelete','是否确定删除？');" 
            onclick="btnDelete_Click"><i></i><span>删除</span></asp:LinkButton></li>
      </ul>
      <div class="menu-list">
        <div class="rule-single-select">
          <asp:DropDownList ID="ddlExpressStatus" runat="server" AutoPostBack="True" onselectedindexchanged="ddlExpressStatus_SelectedIndexChanged">
            <asp:ListItem Value="0" Selected="True">投诉状态</asp:ListItem>
            <asp:ListItem Value="1">未受理</asp:ListItem>
            <asp:ListItem Value="2">受理中</asp:ListItem>
            <asp:ListItem Value="3">已完成</asp:ListItem>
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
<%--    <th align="left" width="12%">投诉编号</th>
    <th align="left" width="10%">投诉标题</th>
    <th width="20%">被投诉方</th>
    <th align="left" width="10%">投诉原因</th>
    <th width="20%">申请时间</th>
    <th align="center" width="10%">投诉状态</th>
    <th align="center" width="8%">操作</th>--%>
    <th align="left" width="12%">投诉人</th>
    <th align="left" width="10%">投诉时间</th>
    <th align="left" width="10%">投诉标题</th>
    <th width="20%">投诉内容</th>
    <th align="center" width="10%">投诉状态</th>
    <th align="center" width="8%">操作</th>
  </tr>
</HeaderTemplate>
<ItemTemplate>
  <tr>
    <td align="center">
      <asp:CheckBox ID="chkId" CssClass="checkall" runat="server" style="vertical-align:middle;" />
      <asp:HiddenField ID="hidId" Value='<%#Eval("id")%>' runat="server" />
    </td>
    <td><%#Eval("user_name").ToString() == "" ? "匿名用户" : Eval("user_name").ToString()%></td>
    <td><%#string.Format("{0:g}",Eval("complian_time"))%></td>
    <td><%#Eval("complian_title")%></td>
    <td><%#ContentSub(Eval("complian_content").ToString(), 100)%></td>
    <td align="center"><%#Convert.ToInt32(Eval("is_status"))==1?"未受理":(Convert.ToInt32(Eval("is_status"))==2?"受理中":"受理完成")%></td>
    <td align="center"><a class="complianDetail" data="<%#Eval("id")%>" style="cursor: pointer;">详情</a></td>
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
