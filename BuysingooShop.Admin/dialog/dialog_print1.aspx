<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dialog_print1.aspx.cs" Inherits="BuysingooShop.Admin.dialog.dialog_print1" %>

<%@ Import Namespace="BuysingooShop.BLL" %>
<%@ Import Namespace="System.Data" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>打印订单窗口</title>
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script type="text/javascript">
        //窗口API
        var api = frameElement.api, W = api.opener;
        api.button({
            name: '确认打印',
            focus: true,
            callback: function () {
                printWin();
            }
        }, {
            name: '取消'
        });
        //打印方法
        function printWin() {
            var oWin = window.open("", "_blank");
            oWin.document.write(document.getElementById("content").innerHTML);
            oWin.focus();
            oWin.document.close();
            oWin.print()
            oWin.close()
        }

        <%--$(function () {
        alert(<%=order_no%>);
    })--%>
    </script>

    <style>
	body{ background:#fff; font-family: "宋体",Verdana;margin: 0;
  padding: 0;
  font-size: 12px;
  color: #000;}
   .invoice{ width:216px; height:227px;  position:absolute; top:50%;margin-top:-113px; left:50%; margin-left:-108px;overflow-y: auto;
  overflow-x: hidden;
  scrollbar-base-color: #F2BACE;
  scrollbar-arrow-color: #666666;
  scrollbar-darkshadow-color: #FFFFFF;
  scrollbar-highlight-color: #FFFFFF;}
   .invoice .sect{ padding:2px; width:100%; height:100%;border:1px solid #000; box-sizing:border-box; -webkit-box-sizing:border-box; -moz-box-sizing:border-box; margin-bottom:10px}
  
</style>
</head>

<body>
    <form id="form1" runat="server">

        <div id="content" class="invoice">
            <%var ordern = order_no.Split(',');
              foreach (var item in ordern)
              {
                  BuysingooShop.BLL.orders bll = new BuysingooShop.BLL.orders();
                  BuysingooShop.Model.orders model = bll.GetModel(item);
            %>
            <table class="sect">
                <tbody>
                    <tr>
                        <th colspan="4">宇航龙商城<sup>&reg;</sup></th>
                    </tr>
                    <tr>
                        <td>订单:</td>
                        <td colspan="3"><%:model.order_no %></td>
                    </tr>
                    <tr>
                        <td>用户:</td>
                        <td><%:model.user_name %></td>
                        <td>手机:</td>
                        <td><%:model.mobile %></td>
                    </tr>
                    <tr>
                        <td>品名:</td>
                        <td colspan="3"><%BuysingooShop.BLL.order_goods goods = new BuysingooShop.BLL.order_goods();
                                          DataTable dt = goods.GetList(" order_id=" + model.id).Tables[0];
                                          if (dt.Rows.Count > 0)
                                          {
                                              foreach (DataRow items in dt.Rows)
                                              {%>
                            <%:items["goods_title"] %>×<%:items["quantity"] %>&nbsp;
                                  <%}
                                          }
                                  %></td>
                    </tr>
                    <tr>
                        <td>地址:</td>
                        <td colspan="3"><%:model.store_address %></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">400-0579-813</td>
                    </tr>
                    <tr><td colspan="4" style="text-align:right;"><%:DateTime.Now.ToString("yyyy-MM-dd") %></td></tr>
                </tbody>
            </table>
            <%} %>
        </div>

    </form>

    <script type="text/javascript">
        $("table tr").each(function () {    // 遍历每一行
            $(this).children('td:eq(0)').css("width", "30px");  // td:eq(0)选择器表示第一个单元格
        });
	</script>
</body>
</html>
