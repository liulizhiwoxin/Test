<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="areareport.aspx.cs" Inherits="BuysingooShop.Admin.salesreport.areareport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>财务日报表</title>

    <script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script type="text/javascript" src="../js/layout.js"></script>
    <link href="../../css/pagination.css" rel="stylesheet" type="text/css" />
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />

    <script src="../../scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>

</head>

<body class="mainbody">
    <form id="form1" runat="server">
        <!--导航栏-->
        <div class="location">
            <a href="javascript:history.back(-1);" class="back"><i></i><span>返回上一页</span></a>
            <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
            <i class="arrow"></i>
            <span>销售地区报表</span>
        </div>
        <!--/导航栏-->

        <!--工具栏-->
        <div class="toolbar-wrap">
            <div id="floatHead" class="toolbar">
                <div class="l-list">
                    <ul class="icon-list">  
                        <li><asp:LinkButton ID="LinkButton1" runat="server" CssClass="list" OnClientClick="" onclick="LinkButton1_Click"><i></i><span>导出excel</span></asp:LinkButton></li>
                    </ul>


                    <div class="menu-list">
                        <div class="rule-single-select">
                            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="menu-list">
                        <div class="rule-single-select">
                            <asp:DropDownList ID="ddlGroupId" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlGroupId_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <ul class="icon-list" style="display:none">
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
                        <th align="left" width="10%">用户名</th>
                        <th align="left" width="10%">订单号</th>
                        <th align="left" width="10%">提货店铺</th>
                        <th align="left" width="16%">下单时间</th>
                        <th align="left" width="15%">下单金额</th>

                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%#Eval("user_name")%></td>
                    <td><%#Eval("order_no")%></td>
                    <td><%#Eval("store_name")%></td>
                    <td><%#string.Format("{0:g}", Eval("add_time"))%></td>
                    <td><%#Eval("order_amount")%></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"5\">暂无记录</td></tr>" : ""%><tr>
                    <td>订单金额统计：<%:total_amount%></td>
                    <td style="display: none">优惠券统计：<%:coupon_amount%></td>
                    <td style="display: none">退款统计：<%:refund_amount%></td>
                </tr>
                </table>
            </FooterTemplate>
        </asp:Repeater>
        <!--/列表-->

        <!--内容底部-->
        <div class="line20"></div>
        <div class="pagelist">
            <div class="l-btns">
                <span>显示</span><asp:TextBox ID="txtPageNum" runat="server" CssClass="pagenum" onkeydown="return checkNumber(event);" OnTextChanged="txtPageNum_TextChanged" AutoPostBack="True"></asp:TextBox><span>条/页</span>
            </div>
            <div id="PageContent" runat="server" class="default"></div>
        </div>
        <!--/内容底部-->
    </form>
</body>
</html>
