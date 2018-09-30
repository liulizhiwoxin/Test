<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="outlet_fuwuzhan.aspx.cs" Inherits="BuysingooShop.Admin.outlet.outlet_fuwuzhan" %>

<%@ Import Namespace="Vincent" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>订单管理</title>
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script type="text/javascript" src="../js/layout.js"></script>
    <link href="../../css/pagination.css" rel="stylesheet" type="text/css" />
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <script src="../../scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function () {
            //确认发货
            $(".icon-list li a.sendout").click(function () {
                //var checkNum = $(".checkall input:checked").size();
                //if (checkNum > 1) {
                //    $.dialog.alert('请只选择一行操作的记录！');
                //    return false;
                //} else {
                //    if (checkNum <= 0) {
                //        $.dialog.alert('对不起，请选中您要操作的记录！');
                //        return false;
                //    }
                //    var orderNo;
                //    $("table tr td input:checked").each(function () {
                //        orderNo = $(this).parent().next("input:hidden").val();
                //    });
                //    var dialog = $.dialog({
                //        title: '确认发货',
                //        content: 'url:dialog/dialog_express.aspx?order_no=' + orderNo,
                //        min: false,
                //        max: false,
                //        lock: true,
                //        width: 450
                //    });
                //}
                order_express();
            });

            //批量打印
            $("#volumeprint").click(function () { volumeprint(); });

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
        }

        //批量发货
        function order_express() {
            var orders_no = "";

            $(".checkall input[type=checkbox]:checked").each(function (i, item) {
                orders_no += $(item).parent().next("input:hidden").val() + ",";
            })
            orders_no = orders_no.substr(0, orders_no.length - 1);

            if (orders_no == "") {
                $.dialog.tips("请选择数据...", 1, "error.gif");
                return;
            }

            var dialog = $.dialog.confirm('确认要发货吗？', function () {
                var postData = { "order_no": orders_no, "edit_type": "order_express" };
                //发送AJAX请求
                sendAjaxUrl(dialog, postData, "../../tools/admin_ajax.ashx?action=edit_order_status");
                return false;
            });
        }


        //批量打印
        function volumeprint() {
            var orders_no = "";

            $(".checkall input[type=checkbox]:checked").each(function (i, item) {
                orders_no += $(item).parent().next("input:hidden").val() + ",";
            })
            orders_no = orders_no.substr(0, orders_no.length - 1);

            if (orders_no == "") {
                $.dialog.tips("请选择数据...", 1, "error.gif");
                return;
            }

            var dialog = $.dialog({
                title: '打印订单',
                content: 'url:dialog/dialog_print1.aspx?order_no=' + orders_no,
                min: false,
                max: false,
                lock: true,
                width: 600,
                height: 400
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
            <span>订单列表</span>
        </div>
        <!--/导航栏-->

        <!--工具栏-->
        <div class="toolbar-wrap">
            <div id="floatHead" class="toolbar">
                <div class="l-list">
                    <ul class="icon-list">
                        <li><a class="all" href="javascript:;" onclick="checkAll(this);"><i></i><span>全选</span></a></li>
                        <li>
                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="list" OnClientClick="" OnClick="LinkButton1_Click"><i></i><span>导出excel</span></asp:LinkButton></li>
                        <li><a id="volumeprint" class="list"><i></i><span>批量打印</span></a></li>
                        <li>
                            <asp:LinkButton ID="btnDelete" runat="server" CssClass="del" OnClientClick="return ExePostBack('btnDelete','只允许删除已作废订单，是否继续？');" OnClick="btnDelete_Click"><i></i><span>删除</span></asp:LinkButton></li>
                        <li><a class="sendout" href="javascript:;"><i></i><span>发货</span></a></li>
                    </ul>
                   <%-- <div class="menu-list">
                        <div class="rule-single-select">
                            <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                <asp:ListItem Value="" Selected="True">订单状态</asp:ListItem>
                                <asp:ListItem Value="1">待付款</asp:ListItem>
                                <asp:ListItem Value="2">待发货</asp:ListItem>
                                <asp:ListItem Value="3">已发货</asp:ListItem>
                                <asp:ListItem Value="4">已完成</asp:ListItem>
                                <asp:ListItem Value="5">已取消</asp:ListItem>
                                <asp:ListItem Value="6">已作废</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div style="display: none" class="rule-single-select">
                            <asp:DropDownList ID="ddlPaymentStatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPaymentStatus_SelectedIndexChanged">
                                <asp:ListItem Value="0" Selected="True">支付状态</asp:ListItem>
                                <asp:ListItem Value="1">待支付</asp:ListItem>
                                <asp:ListItem Value="2">已支付</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div style="display: none" class="rule-single-select">
                            <asp:DropDownList ID="ddlExpressStatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlExpressStatus_SelectedIndexChanged">
                                <asp:ListItem Value="0" Selected="True">发货状态</asp:ListItem>
                                <asp:ListItem Value="1">待发货</asp:ListItem>
                                <asp:ListItem Value="2">已发货</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>--%>
                    <div class="r-list" style="float:left">
                        <input id="txtDate" name="txtDate" type="text" runat="server" onclick="WdatePicker({ skin: 'whyGreen', minDate: '2015-01-20', maxDate: '2020-12-30' })" class="keyword" />
                        <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn-search" OnClick="btnDate_Click">日期查询</asp:LinkButton>
                    </div>
                </div>
                <div class="r-list">
                    <asp:TextBox ID="txtKeywords" runat="server" CssClass="keyword" />
                    <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="btn-search" OnClick="btnSearch_Click">查询</asp:LinkButton>
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
                        <th align="center"width="10%">订单号</th>
                           <th align="center" width="10%">会员名</th>
                        <th align="center" width="10%">会员账号</th>
                        <th align="left" width="5%">支付方式</th>
                        <th align="left" width="5%">下单方式</th>
                        <th style="display: none" align="left" width="10%">配送方式</th>
                        <th width="8%">订单状态</th>
                        <th width="8%">服务项目</th>

                        <th style="display: none" width="8%">发货状态</th>
                        <th width="10%">总金额</th>
                        <th align="left" width="16%">下单时间</th>
                        <th align="left" width="10%">操作</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td align="center">
                        <asp:CheckBox ID="chkId" CssClass="checkall" runat="server" Style="vertical-align: middle;" />
                        <input type="hidden" value="<%#Eval("order_no")%>" />   <%--订单号--%>
                        <asp:HiddenField ID="hidId" Value='<%#Eval("id")%>' runat="server" /> <%--订单号ID--%>
                    </td>
                    <td><a href="order_edit.aspx?action=<%#Vincent._DTcms.DTEnums.ActionEnum.Edit %>&id=<%#Eval("id")%>"><%#Eval("order_no")%></a></td>
                     <td align="center"><%#Eval("nick_name")%></td>
                    <td align="center"><%#Eval("user_name").ToString() == "" ? "匿名用户" : Eval("user_name").ToString()%></td>
                    <td><%#new BuysingooShop.BLL.payment().GetTitle(Convert.ToInt32(Eval("payment_id")))%></td>
                    <td><%#Eval("down_order")%></td>
                    <td style="display: none"><%#new BuysingooShop.BLL.express().GetTitle(Convert.ToInt32(Eval("express_id")))%></td>
                    <td align="center"><%#GetOrderStatus(Convert.ToInt32(Eval("id")))%></td>
                    <td align="center"><%#GetOrderServer(Convert.ToInt32(Eval("id")))%></td>
                    <td style="display: none" align="center"><%#Convert.ToInt32(Eval("express_status")) == 2 ? "已发货" : "待发货"%></td>
                    <td align="center"><%#Eval("order_amount")%></td>
                    <td><%#string.Format("{0:g}",Eval("add_time"))%></td>
                    <td align="left">
                        <a href="outlet_detail.aspx?action=<%#Vincent._DTcms.DTEnums.ActionEnum.Edit %>&id=<%#Eval("id")%>">订单详细</a>&nbsp;&nbsp;

                        <a href="out_userlist.aspx?action=<%#Vincent._DTcms.DTEnums.ActionEnum.Edit %>&id=<%#Eval("user_id")%>">查看会员</a>&nbsp;&nbsp;

                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"11\">暂无记录</td></tr>" : ""%>
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
