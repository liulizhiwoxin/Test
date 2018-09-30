<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="total_shopinfo.aspx.cs" Inherits="BuysingooShop.Admin.finance.total_shopinfo" %>

<%@ Import Namespace="Vincent" %>
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
    <script>

        $(function () {

            //发送AJAX请求
            $.ajax({
                type: "post",
                url: "../../tools/admin_ajax.ashx?action=get_total",
                dataType: "text",
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    $.dialog.alert('尝试发送失败，错误信息：' + errorThrown, function () { }, dialog);
                },
                success: function (data) {
                    if (data==99) {
                        $("#total").html(0);
                    }
                    else {
                        $("#total").html(data);
                    }
                }
            });

            $.ajax({
                type: "post",
                url: "../../tools/admin_ajax.ashx?action=get_userCount",
                dataType: "text",
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    $.dialog.alert('尝试发送失败，错误信息：' + errorThrown, function () { }, dialog);
                },
                success: function (data) {
                    $("#user_total").html(data);
                }
            });


            $.ajax({
                type: "post",
                url: "../../tools/admin_ajax.ashx?action=Getuser_amount",
                dataType: "text",
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    $.dialog.alert('尝试发送失败，错误信息：' + errorThrown, function () { }, dialog);
                },
                success: function (data) {
                    $("#Getuser_amount").html(data);
                }
            });




            $.ajax({
                type: "post",
                url: "../../tools/admin_ajax.ashx?action=GetAlluser_amount",
                dataType: "text",
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    $.dialog.alert('尝试发送失败，错误信息：' + errorThrown, function () { }, dialog);
                },
                success: function (data) {
                    $("#GetAlluser_amount").html(data);

                }
            });
        } );
    </script>
</head>

<body class="mainbody">
    <form id="form1" runat="server">
        <!--导航栏-->
        <div class="location">
            <a href="javascript:history.back(-1);" class="back"><i></i><span>返回上一页</span></a>
            <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
            <i class="arrow"></i>
            <span>累积数据</span>
        </div>
        <!--/导航栏-->

        <!--工具栏-->
         
        <!--/工具栏-->

        <!--列表-->
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="ltable" style="margin-top:40px">
            <tr>
                <th align="center" width="100%">累积交易</th>
            </tr>
            <tr>
                <td align="center" width="100%" id="total">正在加载.. </td>
            </tr>

             <tr>
                <th align="center" width="100%">累积用户</th>
            </tr>
            <tr>
                <td align="center" width="100%"  id="user_total" >正在加载.. </td>
            </tr> 

            <tr>
                <th align="center" width="100%">累积佣金</th>
            </tr>
            <tr>
                <td align="center" width="100%" id="Getuser_amount"> 正在加载..</td>
            </tr>
           
            <tr>
                <th align="center" width="100%">累积可提佣金</th>
            </tr>
            <tr>
                <td align="center" width="100%" id="GetAlluser_amount"> 正在加载..</td>
            </tr>
        </table>
        <!--/列表-->

        <!--内容底部-->
        <div class="line20"></div>
    
        <!--/内容底部-->
    </form>
</body>
</html>
