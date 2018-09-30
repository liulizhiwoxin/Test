<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="refund_edit.aspx.cs" Inherits="BuysingooShop.Admin.order.refund_edit" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>查看退款信息</title>
<script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
<script type="text/javascript" src="../../scripts/jquery/Validform_v5.3.2_min.js"></script>
<script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
<script type="text/javascript" src="../js/layout.js"></script>
<link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    $(function () {
        $("#btnConfirm").click(function () { OrderConfirm(); });   //确认退款
        $("#btnComplete").click(function () { OrderComplete(); }); //完成退款
        $("#btnCancel").click(function () { OrderCancel(); });     //取消退款
        $("#btnPrint").click(function () { OrderPrint(); });       //打印退款

        $("#btnEditAcceptInfo").click(function () { EditAcceptInfo(); }); //修改收货信息
        $("#btnEditRemark").click(function () { EditOrderRemark(); });    //修改退款备注
        $("#btnEditRealAmount").click(function () { EditRealAmount(); }); //修改商品总金额
        $("#btnEditExpressFee").click(function () { EditExpressFee(); }); //修改配送费用
        $("#btnEditPaymentFee").click(function () { EditPaymentFee(); }); //修改支付手续费
    });

    //确认退款
    function OrderConfirm() {
        var dialog = $.dialog.confirm('确认退款后将无法修改金额，确认要继续吗？', function () {
            var postData = { "order_no": $("#spanOrderNo").text(), "edit_type": "order_confirm" };
            //发送AJAX请求
            sendAjaxUrl(dialog, postData, "../../tools/admin_ajax.ashx?action=edit_order_status");
            return false;
        });
    }
    //完成退款
    function OrderComplete() {
        var dialog = $.dialog.confirm('退款完成后，退款处理完毕，确认要继续吗？', function () {
            var postData = { "order_no": $("#spanOrderNo").text(), "edit_type": "order_complete" };
            //发送AJAX请求
            sendAjaxUrl(dialog, postData, "../../tools/admin_ajax.ashx?action=edit_order_status");
            return false;
        });
    }
    //取消退款
    function OrderCancel() {
        var dialog = $.dialog({
            title: '取消退款',
            content: '操作提示信息：<br />1、匿名用户，请线下与客户沟通；<br />2、会员用户，自动检测退还金额或积分到账户；<br />3、请单击相应按钮继续下一步操作！',
            min: false,
            max: false,
            lock: true,
            icon: 'confirm.gif',
            button: [{
                name: '检测退还',
                callback: function () {
                    var postData = { "order_no": $("#spanOrderNo").text(), "edit_type": "order_cancel", "check_revert": 1 };
                    //发送AJAX请求
                    sendAjaxUrl(dialog, postData, "../../tools/admin_ajax.ashx?action=edit_order_status");
                    return false;
                },
                focus: true
            }, {
                name: '直接取消',
                callback: function () {
                    var postData = { "order_no": $("#spanOrderNo").text(), "edit_type": "order_cancel", "check_revert": 0 };
                    //发送AJAX请求
                    sendAjaxUrl(dialog, postData, "../../tools/admin_ajax.ashx?action=edit_order_status");
                    return false;
                }
            }, {
                name: '关闭'
            }]
        });

    }
   
    //打印退款
    function OrderPrint() {
        var dialog = $.dialog({
            title: '打印退款',
            content: 'url:dialog/dialog_print.aspx?order_no=' + $("#spanOrderNo").text(),
            min: false,
            max: false,
            lock: true,
            width: 850//,
            //height: 500
        });
    }
    //修改收货信息
    function EditAcceptInfo() {
        var dialog = $.dialog({
            title: '修改收货信息',
            content: 'url:dialog/dialog_accept.aspx',
            min: false,
            max: false,
            lock: true,
            width: 550,
            height:320
        });
    }
    //修改退款备注
    function EditOrderRemark() {
        var dialog = $.dialog({
            title: '退款备注',
            content: '<textarea id="txtOrderRemark" name="txtOrderRemark" rows="2" cols="20" class="input">' + $("#divRemark").html() + '</textarea>',
            min: false,
            max: false,
            lock: true,
            ok: function () {
                var remark = $("#txtOrderRemark", parent.document).val();
                if (remark == "") {
                    $.dialog.alert('对不起，请输入退款备注内容！', function () { }, dialog);
                    return false;
                }
                var postData = { "order_no": $("#spanOrderNo").text(), "edit_type": "edit_order_remark", "remark": remark };
                //发送AJAX请求
                sendAjaxUrl(dialog, postData, "../../tools/admin_ajax.ashx?action=edit_order_status");
                return false;
            },
            cancel: true
        });
    }
    //修改商品总金额
    function EditRealAmount() {
        var pop = $.dialog.prompt('请修改商品总金额',
            function (val) {
                if (!checkIsMoney(val)) {
                    $.dialog.alert('对不起，请输入正确的配送金额！', function () { }, pop);
                    return false;
                }
                var postData = { "order_no": $("#spanOrderNo").text(), "edit_type": "edit_real_amount", "real_amount": val };
                //发送AJAX请求
                sendAjaxUrl(pop, postData, "../../tools/admin_ajax.ashx?action=edit_order_status");
                return false;
            },
            $("#spanRealAmountValue").text()
        );
    }
    //修改配送费用
    function EditExpressFee() {
        var pop = $.dialog.prompt('请修改配送费用',
            function (val) {
                if (!checkIsMoney(val)) {
                    $.dialog.alert('对不起，请输入正确的配送金额！', function () { }, pop);
                    return false;
                }
                var postData = { "order_no": $("#spanOrderNo").text(), "edit_type": "edit_express_fee", "express_fee": val };
                //发送AJAX请求
                sendAjaxUrl(pop, postData, "../../tools/admin_ajax.ashx?action=edit_order_status");
                return false;
            },
            $("#spanExpressFeeValue").text()
        );
    }
    //修改手续费用
    function EditPaymentFee() {
        var pop = $.dialog.prompt('请修改支付手续费用',
            function (val) {
                if (!checkIsMoney(val)) {
                    $.dialog.alert('对不起，请输入正确的手续费用！', function () { }, pop);
                    return false;
                }
                var postData = { "order_no": $("#spanOrderNo").text(), "edit_type": "edit_payment_fee", "payment_fee": val };
                //发送AJAX请求
                sendAjaxUrl(pop, postData, "../../tools/admin_ajax.ashx?action=edit_order_status");
                return false;
            },
            $("#spanPaymentFeeValue").text()
        );
    }

    //=================================工具类的JS函数====================================
    //检查是否货币格式
    function checkIsMoney(val) {
        var regtxt = /^(([1-9]{1}\d*)|([0]{1}))(\.(\d){1,2})?$/;
        if (!regtxt.test(val)) {
            return false;
        }
        return true;
    }
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
</script>
</head>

<body class="mainbody">
<form id="form1" runat="server">
<!--导航栏-->
<div class="location">
  <a href="express_list.aspx" class="back"><i></i><span>返回列表页</span></a>
  <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
  <i class="arrow"></i>
  <a href="order_list.aspx"><span>退款管理</span></a>
  <i class="arrow"></i>
  <span>退款详细</span>
</div>
<div class="line10"></div>
<!--/导航栏-->

<!--内容-->
<div class="content-tab-wrap">
  <div id="floatHead" class="content-tab">
    <div class="content-tab-ul-wrap">
      <ul>
        <li><a href="javascript:;" onclick="tabs(this);" class="selected">退款详细信息</a></li>
      </ul>
    </div>
  </div>
</div>

<div class="tab-content">
  <dl>
    <dd style="margin-left:50px;text-align:center;">
      <div class="order-flow" style="width:560px">
        <%if (model.status < 4)
          { %>
            <div class="order-flow-left">
              <a class="order-flow-input">已提交</a>
              <span><p class="name">客户已提交退款</p><p><%=model.add_time%></p></span>
            </div>
            <%if (model.payment_status == 1)
              { %>
                <div class="order-flow-wait">
                  <a class="order-flow-input">付款</a>
                  <span><p class="name">等待付款</p></span>
                </div>
            <%}
              else if (model.payment_status == 2)
              { %>
                <div class="order-flow-arrive">
                  <a class="order-flow-input">付款</a>
                  <span><p class="name">已付款</p><p><%=model.payment_time%></p></span>
                </div>
            <%} %>
            <%if (model.payment_status == 0 && model.status == 1)
              { %>
                <div class="order-flow-wait">
                   <a class="order-flow-input">确认</a>
                   <span><p class="name">等待确认</p></span>
                </div>
            <%}
              else if (model.payment_status == 0 && model.status > 1)
              { %>
                <div class="order-flow-arrive">
                  <a class="order-flow-input">确认</a>
                  <span><p class="name">已确认</p><p><%=model.confirm_time%></p></span>
                </div>
            <%} %>
            <%if (model.express_status == 1)
              { %>
                <div class="order-flow-wait">
                  <a class="order-flow-input">发货</a>
                  <span><p class="name">等待发货</p></span>
                </div>
            <%}
              else if (model.express_status == 2)
              { %>
                <div class="order-flow-arrive">
                  <a class="order-flow-input">发货</a>
                  <span><p class="name">已发货</p><p><%=model.express_time%></p></span>
                 </div>
             <%} %>
             <%if (model.status == 3)
               { %>
                 <div class="order-flow-right-arrive">
                   <a class="order-flow-input">完成</a>
                   <span><p class="name">退款完成</p><p><%=model.complete_time%></p></span>
                 </div>
             <%}
               else
               { %>
                 <div class="order-flow-right-wait">
                   <a class="order-flow-input">完成</a>
                   <span><p class="name">等待完成</p></span>
                 </div>
             <%} %>
             <%}
              else if (model.status == 4)
              {%>
          <div style="text-align:center;line-height:30px; font-size:20px; color:Red;">该退款已取消</div>
         <%}
          else if (model.status == 5)
          { %>
            <div style="text-align:center;line-height:30px; font-size:20px; color:Red;">该退款已作废</div>
         <%} %>
      </div>
    </dd>
  </dl>
  <dl>
    <dt>订单编号</dt>
    <dd><span id="spanOrderNo"><%=model.order_no %></span></dd>
  </dl>
  <asp:Repeater ID="rptList" runat="server">
  <HeaderTemplate>
  <dl>
    <dt>商品列表</dt>
    <dd>
      <table border="0" cellspacing="0" cellpadding="0" class="border-table" width="98%">
        <thead>
          <tr>
            <th>商品名称</th>
            <th width="12%">销售价</th>
            <th width="12%">优惠价</th>
            <th width="10%">积分</th>
            <th width="10%">数量</th>
            <th width="12%">金额合计</th>
            <th width="12%">积分合计</th>
          </tr>
        </thead>
        <tbody>
        </HeaderTemplate>
        <ItemTemplate>
          <tr class="td_c">
            <td style="text-align:left;white-space:normal;"><%#Eval("goods_title")%></td>
            <td><%#Eval("goods_price")%></td>
            <td><%#Eval("real_price")%></td>
            <td><%#Eval("point")%></td>
            <td><%#Eval("quantity")%></td>
            <td><%#Convert.ToDecimal(Eval("real_price"))*Convert.ToInt32(Eval("quantity"))%></td>
            <td><%#Convert.ToInt32(Eval("point")) * Convert.ToInt32(Eval("quantity"))%></td>
          </tr>
          </ItemTemplate>
          <FooterTemplate>
        </tbody>
      </table>
    </dd>
  </dl>
  </FooterTemplate>
  </asp:Repeater>
  <dl>
    <dt>退款信息</dt>
    <dd>
      <table border="0" cellspacing="0" cellpadding="0" class="border-table" width="98%">
        <tr>
          <th width="20%">支付方式</th>
          <td><%=new BuysingooShop.BLL.payment().GetTitle(model.payment_id) %></td>
        </tr>
        <tr>
          <th>配送方式</th>
          <td><%=new BuysingooShop.BLL.express().GetTitle(model.express_id) %></td>
        </tr>
        <tr>
          <th>用户留言</th>
          <td><%=model.message %></td>
        </tr>
        <tr>
          <th>配送费用</th>
          <td>
            <div class="position">
              <span id="span1"><%=model.express_fee %></span> 元
              <input id="Button1" runat="server" visible="false" type="button" class="ibtn" value="调价" />
            </div>
          </td>
        </tr>
        <tr>
          <th valign="top">退款备注</th>
          <td>
            <div class="position">
              <div id="divRemark"><%=model.remark %></div>
              <input id="btnEditRemark" runat="server" visible="false" type="button" class="ibtn" value="修改" />
            </div>
          </td>
        </tr>
      </table>
    </dd>
  </dl>
</div>
<!--/内容-->


<!--工具栏-->
<div class="page-footer">
  <div class="btn-list">
    <input id="btnConfirm" runat="server" visible="false" type="button" value="确认退款" class="btn" />
    <input id="btnComplete" runat="server" visible="false" type="button" value="完成退款" class="btn" />
    <input id="btnCancel" runat="server" visible="false" type="button" value="取消退款" class="btn green" />
    <input id="btnPrint" type="button" value="打印退款" class="btn violet" />
    <input id="btnReturn" type="button" value="返回上一页" class="btn yellow" onclick="javascript:history.back(-1);" />
  </div>
  <div class="clear"></div>
</div>
<!--/工具栏-->
</form>
</body>
</html>
