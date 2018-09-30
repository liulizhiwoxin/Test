<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="refund.aspx.cs" Inherits="BuysingooShop.Admin.finance.refund" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>财务日报表</title>
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script type="text/javascript" src="../js/layout.js"></script>
    <link href="../../css/pagination.css" rel="stylesheet" type="text/css" />
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <script src="../../scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <style type="text/css">
        .abc
        {
            width: 120px;
            overflow: hidden;
            text-overflow: ellipsis; /*文字溢出的部分隐藏并用省略号代替*/
            white-space: nowrap;} /*文本不自动换行*/</style>
</head>
<body class="mainbody">
    <form id="form1" runat="server">
    <!--导航栏-->
    <div class="location">
        <a href="javascript:history.back(-1);" class="back"><i></i><span>返回上一页</span></a>
        <a href="../center.aspx" class="home"><i></i><span>首页</span></a> <i class="arrow">
        </i><span>退款报表</span>
    </div>
    <!--/导航栏-->
    <!--工具栏-->
    <div class="toolbar-wrap">
        <div id="floatHead" class="toolbar">
            <div class="l-list">
                <ul class="icon-list">
                    <li>
                        <input id="ipt_Time" name="ipt_Time" type="text" runat="server" onclick="WdatePicker({ skin: 'whyGreen', minDate: '2015-01-20', maxDate: '2020-12-30' })" />
                    </li>
                    <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="btn-search" OnClick="btnSearch_Click">查询</asp:LinkButton>
                </ul>
            </div>
            <div class="r-list">
            </div>
        </div>
    </div>
    <!--/工具栏-->
    <!--列表-->
    <asp:Repeater ID="rptList" runat="server">
        <HeaderTemplate>
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="ltable">
                <tr>
                    <th align="left" width="10%">
                        用户名
                    </th>
                    <th align="left" width="10%">
                        订单号
                    </th>
                    <th align="left" width="10%">
                        退款状态
                    </th>
                    <th align="left" width="15%">
                        退款原因
                    </th>
                    <th align="left" width="16%">
                        退款时间
                    </th>
                    <th align="left" width="15%">
                        退款金额
                    </th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <%#Eval("user_name")%>
                </td>
                <td>
                    <%#Eval("order_no")%>
                </td>
                <td>
                    <%#GetrefundStatus(Convert.ToInt32(Eval("id")))%>
                </td>
                <td>
                    <div class="abc"><a title='<%#Eval("remark")%>'><%#Eval("remark")%></a></div>
                </td>
                <td>
                    <%#string.Format("{0:g}", Eval("add_time"))%>
                </td>
                <td>
                    <%#Eval("amount")%>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"5\">暂无记录</td></tr>" : ""%><tr>
                <td>
                    退款统计：<%:refund_amount%>
                </td>
            </tr>
            </table>
        </FooterTemplate>
    </asp:Repeater>
    <!--/列表-->
    <!--内容底部-->
    <div class="line20">
    </div>
    <div class="pagelist">
        <div class="l-btns">
            <span>显示</span><asp:TextBox ID="txtPageNum" runat="server" CssClass="pagenum" onkeydown="return checkNumber(event);"
                OnTextChanged="txtPageNum_TextChanged" AutoPostBack="True"></asp:TextBox><span>条/页</span>
        </div>
        <div id="PageContent" runat="server" class="default">
        </div>
    </div>
    <!--/内容底部-->
    </form>
</body>
</html>
