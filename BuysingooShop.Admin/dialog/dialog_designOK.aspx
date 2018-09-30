<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dialog_designOK.aspx.cs"
    Inherits="BuysingooShop.Admin.dialog.dialog_designOK" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>确认设计窗口</title>
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script type="text/javascript">
        $(function () {
            $("#btnCancel").bind("click", function() {
                window.close();
//            });
//            $("#btnOk").on("click", function () {
//                alert(456);
//            });
        });
    </script>
</head>
<body style="margin: 0;">
    <form id="form1" runat="server">
    <div id="content">
        <input id="hiddNO" runat="server" type="hidden" value=""/>
        <table style="font-size: 12px; font-family: '微软雅黑'; background: #fff;" border="0"
            cellpadding="3" cellspacing="0" align="center" width="500">
            <tbody>
                <tr>
                    <td colspan="3" style="font-size: 20px; text-align: center;" height="50" width="346">
                        确认设计
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center;">订单编号：</td>
                    <td colspan="2"><%:model.order_no %></td>
                </tr>
                <tr>
                    <td style="text-align: center;">会&nbsp;&nbsp;&nbsp;&nbsp;员：</td>
                    <td colspan="2"><%:model.user_name %></td>
                </tr>
                <tr>
                    <td style="text-align: center;">联系方式：</td>
                    <td colspan="2"><%:model.mobile %></td>
                </tr>
                <tr>
                    <td style="text-align: center;">收货地址：</td>
                    <td colspan="2"><%:model.address %></td>
                </tr>
                <tr>
                    <td style="text-align: center;vertical-align: initial;">订单详情：</td>
                    <td style="text-align: center;"><a style="color:black;text-decoration: none;" href="1.jpg"><img width="100px" src="1.jpg"/><div>DIY详情</div></a></td>
                    <td><a style="color:black;text-decoration: none;" href="1.jpg"><img width="100px" src="1.jpg"/><div style="width: 100px; text-align: center;">商品详情</div></a></td>
                </tr>
                <tr>
                    <td style="text-align: center;vertical-align: initial;"></td>
                    <td style="text-align: center;"><asp:Button ID="btnOK" runat="server" Text="确认设计" onclick="btnOK_Click" /></td>
                    <td><input id="butCancel" type="button" value="取消设计" /></td>
                </tr>
            </tbody>
        </table>
    </div>
    </form>
</body>
</html>
