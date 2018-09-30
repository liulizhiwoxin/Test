<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="order_edit.aspx.cs" Inherits="BuysingooShop.Admin.order.order_edit" ValidateRequest="false" %>

<%@ Import Namespace="BuysingooShop.Model" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>查看订单信息</title>
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script type="text/javascript" src="../js/layout.js"></script>
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <script src="../js/SingGooJS.js"></script>
    <script type="text/javascript">
        $(function () {
            SelectServer();//查询服务内容
            $("#btnConfirm").click(function () { OrderConfirm(); });   //DIY确认订单
            $("#btnPayment").click(function () { OrderPayment(); });   //确认付款
            $("#btnExpress").click(function () { OrderExpress(); });   //确认发货
            $("#btnComplete").click(function () { OrderComplete(); }); //完成订单
            $("#btnCancel").click(function () { OrderCancel(); });     //取消订单
            $("#btnInvalid").click(function () { OrderInvalid(); });   //作废订单
            $("#btnPrint").click(function () { OrderPrint(); });       //打印订单

            $("#btnEditAcceptInfo").click(function () { EditAcceptInfo(); }); //修改收货信息aa
            $("#btnEditRemark").click(function () { EditOrderRemark(); });    //修改订单备注
            $("#btnEditRealAmount").click(function () { EditRealAmount(); }); //修改商品总金额
            $("#btnEditExpressFee").click(function () { EditExpressFee(); }); //修改配送费用
            $("#btnEditPaymentFee").click(function () { EditPaymentFee(); }); //修改支付手续费
        });

        //DIY确认
        function OrderConfirm() {
            var dialog = $.dialog.confirm('是否标识为DIY确认？', function () {
                var postData = { "order_no": $("#spanOrderNo").text(), "edit_type": "order_confirm" };
                //发送AJAX请求
                sendAjaxUrl(dialog, postData, "../../tools/admin_ajax.ashx?action=edit_order_status");
                return false;
            });
        }
        //    //确认订单
        //    function OrderConfirm() {
        //        var dialog = $.dialog.confirm('确认订单后将无法修改金额，确认要继续吗？', function () {
        //            var postData = { "order_no": $("#spanOrderNo").text(), "edit_type": "order_confirm" };
        //            //发送AJAX请求
        //            sendAjaxUrl(dialog, postData, "../../tools/admin_ajax.ashx?action=edit_order_status");
        //            return false;
        //        });
        //    }
        //确认付款
        function OrderPayment() {
            var dialog = $.dialog.confirm('操作提示信息：<br />1、该订单使用在线支付方式，付款成功后自动确认；<br />2、如客户确实已打款而没有自动确认可使用该功能；<br />3、确认付款后无法修改金额，确认要继续吗？', function () {
                var postData = { "order_no": $("#spanOrderNo").text(), "edit_type": "order_payment" };
                //发送AJAX请求
                sendAjaxUrl(dialog, postData, "../../tools/admin_ajax.ashx?action=edit_order_status");
                return false;
            });
        }
        ////确认发货
        //function OrderExpress() {
        //    var dialog = $.dialog({
        //        title: '确认发货',
        //        content: 'url:dialog/dialog_express.aspx?order_no=' + $("#spanOrderNo").text(),
        //        min: false,
        //        max: false,
        //        lock: true,
        //        width: 450
        //    });
        //}

        //确认发货
        function OrderExpress() {

            var dialog = $.dialog.confirm('确认要发货吗？', function () {
                var postData = { "order_no": $("#spanOrderNo").text(), "edit_type": "order_express" };
                //发送AJAX请求
                sendAjaxUrl(dialog, postData, "../../tools/admin_ajax.ashx?action=edit_order_status");
                return false;
            });
        }

        //完成订单
        function OrderComplete() {
            var dialog = $.dialog.confirm('订单处理完毕，确认要继续吗？', function () {
                var postData = { "order_no": $("#spanOrderNo").text(), "edit_type": "order_complete" };
                //发送AJAX请求
                sendAjaxUrl(dialog, postData, "../../tools/admin_ajax.ashx?action=edit_order_status");
                return false;
            });
        }
        //取消订单
        function OrderCancel() {
            var dialog = $.dialog({
                title: '取消订单',
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
        //作废订单
        function OrderInvalid() {
            var dialog = $.dialog({
                title: '取消订单',
                content: '操作提示信息：<br />1、匿名用户，请线下与客户沟通；<br />2、会员用户，自动检测退还金额或积分到账户；<br />3、请单击相应按钮继续下一步操作！',
                min: false,
                max: false,
                lock: true,
                icon: 'confirm.gif',
                button: [{
                    name: '检测退还',
                    callback: function () {
                        var postData = { "order_no": $("#spanOrderNo").text(), "edit_type": "order_invalid", "check_revert": 1 };
                        //发送AJAX请求
                        sendAjaxUrl(dialog, postData, "../../tools/admin_ajax.ashx?action=edit_order_status");
                        return false;
                    },
                    focus: true
                }, {
                    name: '直接作废',
                    callback: function () {
                        var postData = { "order_no": $("#spanOrderNo").text(), "edit_type": "order_invalid", "check_revert": 0 };
                        //发送AJAX请求
                        sendAjaxUrl(dialog, postData, "../../tools/admin_ajax.ashx?action=edit_order_status");
                        return false;
                    }
                }, {
                    name: '关闭'
                }]
            });
        }
        //打印订单
        function OrderPrint() {
            var dialog = $.dialog({
                title: '打印订单',
                content: 'url:dialog/dialog_print.aspx?order_no=' + $("#spanOrderNo").text(),
                min: false,
                max: false,
                lock: true,
                width: 600,
                height: 400

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
                height: 320
            });
        }
        //修改订单备注
        function EditOrderRemark() {
            var dialog = $.dialog({
                title: '订单备注',
                content: '<textarea id="txtOrderRemark" name="txtOrderRemark" rows="2" cols="20" class="input">' + $("#divRemark").html() + '</textarea>',
                min: false,
                max: false,
                lock: true,
                ok: function () {
                    var remark = $("#txtOrderRemark", parent.document).val();
                    if (remark == "") {
                        $.dialog.alert('对不起，请输入订单备注内容！', function () { }, dialog);
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


        //查询服务内容
        function SelectServer() {
            var order_id = SingGooJS.GetQueryString("id");//订单id

            $.ajax({
                type: 'POST',
                url: '../../tools/admin_ajax.ashx?action=getOrderinfo',
                data: {
                    "order_id": order_id
                },

                dataType: 'json',
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert("状态：" + textStatus + ";出错提示：" + errorThrown);
                },
                success: function (json) {
                    var servertype = "";
                    if (json._bill_type == 0) {
                        servertype = "更换轮胎"
                    } else if (json._bill_type == 1) {
                        servertype = "更换机油"
                    } else if (json._bill_type == 2) {
                        servertype = "镀晶一档"
                    } else if (json._bill_type == 3) {
                        servertype = "镀晶二档"
                    } else if (json._bill_type == 4) {
                        servertype = "镀晶三档"
                    }
                    $("#server_content").html(servertype);
                }
            });
        }
        //修改服务内容
        function EditServer() {
            var order_id = SingGooJS.GetQueryString("id");
                var sid = $("#Select1 option:selected").val();
                if (sid!= "99") {
                    $.ajax({
                        type: 'POST',
                        url: '../../tools/admin_ajax.ashx?action=EditOrderinfo',
                        data: {
                            "type_id": sid,
                            "order_id": order_id
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert("状态：" + textStatus + ";出错提示：" + errorThrown);
                        },
                        async: false,
                        success: function (json) {
                            if (json.status != 0) {
                                document.getElementById("tip").innerHTML = "修改成功!";
                                setTimeout(function () {   
                                    window.location.reload(); 
                                }, 500);
                            }
                            else {
                                window.location.reload();
                            }
                        }
                    });
                }
        
            //<option value="0"> 更换轮胎</option>
            //<option value="1"> 更换机油</option>
            //<option value="2"> 镀晶一档</option>
            //<option value="3"> 镀晶二档</option>
            //<option value="4"> 镀晶三档</option>
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
            <a href="order_list.aspx"><span>订单管理</span></a>
            <i class="arrow"></i>
            <span>订单详细</span>
        </div>
        <div class="line10"></div>
        <!--/导航栏-->

        <!--内容-->
        <div class="content-tab-wrap">
            <div id="floatHead" class="content-tab">
                <div class="content-tab-ul-wrap">
                    <ul>
                        <li><a href="javascript:;" onclick="tabs(this);" class="selected">订单详细信息</a></li>
                    </ul>
                </div>
            </div>
        </div>

        <div class="tab-content">
            <dl>
                <dd style="margin-left: 50px; text-align: center;">
                    <asp:Literal ID="lt_status" runat="server"></asp:Literal>
                </dd>
            </dl>
            <dl>
                <dt>订单号</dt>
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
                                        <th width="12%">折扣号</th>
                                    </tr>
                                </thead>
                                <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="td_c">
                        <td style="text-align: left; white-space: normal;"><%#Eval("goods_title")%></td>
                        <td><%#Eval("goods_price")%></td>
                        <td><%#Eval("real_price")%></td>
                        <td><%#Eval("point")%></td>
                        <td><%#Eval("quantity")%></td>
                        <td><%=model.real_amount %></td>
                        <td><%#Convert.ToInt32(Eval("point")) * Convert.ToInt32(Eval("quantity"))%></td>
                        <td><%#Eval("strcode") %></td>
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
                <dt>提货信息</dt>
                <dd>
                    <table border="0" cellspacing="0" cellpadding="0" class="border-table" width="98%">
                        <tr>
                            <th width="20%">提货点</th>
                            <td>
                                <div class="position">
                                    <span id="spanAcceptName"><%=model.address %></span>
                                    <input id="btnEditAcceptInfo" runat="server" visible="false" type="button" style="display: none" class="ibtn" value="修改" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <th>提货地址</th>
                            <td>
                                <span id="spanArea" style="display: block;"><%--<%=addressModel.provinces + "," + addressModel.citys + "," + addressModel.area%>--%></span>
                                <span id="spanAddress"><%=model.store_address %></span>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <th>邮政编码</th>
                            <td><span id="spanPostCode"><%=model.post_code %></span></td>
                        </tr>
                        <tr style="display: none">
                            <th>联系方式</th>
                            <td><span id="spanMobile"><%=model.mobile %></span></td>
                        </tr>
                        <%--        <tr>
          <th>电话</th>
          <td><span id="spanTelphone"><%=model.telphone %></span></td>
        </tr>--%>
                    </table>
                </dd>
            </dl>
            <dl id="dlUserInfo" runat="server" visible="false">
                <dt>会员信息</dt>
                <dd>
                    <table border="0" cellspacing="0" cellpadding="0" class="border-table" width="98%">
                        <tr>
                            <th width="20%">会员名</th>
                            <td>
                                <asp:Label ID="lbnick_Name" runat="server" Text="-" /></td>
                        </tr>
                        <tr>
                            <th>会员账户</th>
                            <td>
                                <asp:Label ID="lbUserName" runat="server" Text="-" /></td>
                        </tr>
                        <tr>
                            <th>会员组别</th>
                            <td>
                                <asp:Label ID="lbUserGroup" runat="server" Text="-" /></td>
                        </tr>

                        <tr>
                            <th>购物折扣</th>
                            <td>
                                <asp:Label ID="lbUserDiscount" runat="server" Text="100" /></td>
                        </tr>
                        <tr>
                            <th>账户余额</th>
                            <td>
                                <asp:Label ID="lbUserAmount" runat="server" Text="0" />
                                元</td>
                        </tr>
                        <tr style="display: none">
                            <th>账户积分</th>
                            <td>
                                <asp:Label ID="lbUserPoint" runat="server" Text="0" />
                                分</td>
                        </tr>
                    </table>
                </dd>
            </dl>
            <dl>
                <dt>支付方式</dt>
                <dd>
                    <table border="0" cellspacing="0" cellpadding="0" class="border-table" width="98%">
                        <tr>
                            <th width="20%">支付方式</th>
                            <td><%=new BuysingooShop.BLL.payment().GetTitle(model.payment_id) %></td>
                        </tr>
                        <tr style="display: block;">
                            <th>配送方式</th>
                            <td><%=new BuysingooShop.BLL.express().GetTitle(model.express_id) %></td>
                        </tr>
                        <tr>
                            <th>用户留言</th>
                            <td><%=model.message %></td>
                        </tr>
                        <tr>
                            <th valign="top">订单备注</th>
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
            <dl>
                <dt>服务内容</dt>
                <dd>
                    <table border="0" cellspacing="0" cellpadding="0" class="border-table" width="98%">
                        <tr width="100%">
                            <th width="20%">当前订单服务：<span id="server_content"></span></th>
                            <td>
                                <select id="Select1" onchange="EditServer()">
                                    <option value="99">可更换服务内容</option>
                                    <option value="0">更换轮胎</option>
                                    <option value="1">更换机油</option>
                                    <option value="2">镀晶一档</option>
                                    <option value="3">镀晶二档</option>
                                    <option value="4">镀晶三档</option>
                                </select>   <span id="tip" style="color:red"></span>
                            </td>
                        </tr>
                    </table>
                </dd>
            </dl>
            <dl>
                <dt>订单统计</dt>
                <dd>
                    <table border="0" cellspacing="0" cellpadding="0" class="border-table" width="98%">
                        <tr>
                            <th width="20%">商品总金额</th>
                            <td>
                                <div class="position">
                                    <span id="spanRealAmountValue"><%=model.real_amount %></span> 元
              <input id="btnEditRealAmount" runat="server" visible="false" type="button" class="ibtn" value="调价" />
                                </div>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <th>配送费用</th>
                            <td>
                                <div class="position">
                                    <span id="spanExpressFeeValue"><%=model.express_fee %></span> 元
              <input id="btnEditExpressFee" runat="server" visible="false" type="button" class="ibtn" value="调价" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <th>支付手续费</th>
                            <td>
                                <div class="position">
                                    <span id="spanPaymentFeeValue"><%=model.payment_fee %></span> 元
              <input id="btnEditPaymentFee" runat="server" visible="false" type="button" class="ibtn" value="调价" />
                                </div>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <th>积分总计</th>
                            <td>
                                <div class="position">
                                    <%=model.point > 0 ? "+" + model.point.ToString() : model.point.ToString()%> 分
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <th>订单总金额</th>
                            <td><%=model.order_amount%> 元</td>
                        </tr>
                    </table>
                </dd>
            </dl>
        </div>
        <!--/内容-->


        <!--工具栏-->
        <div class="page-footer">
            <div class="btn-list">
                <input id="btnConfirm" runat="server" visible="False" type="button" value="DIY确认" class="btn" />
                <input id="btnPayment" runat="server" visible="false" type="button" value="确认付款" class="btn" />
                <input id="btnExpress" runat="server" visible="false" type="button" value="确认发货" class="btn" />
                <input id="btnComplete" runat="server" visible="false" type="button" value="完成订单" class="btn" />
                <input id="btnCancel" runat="server" visible="false" type="button" value="取消订单" class="btn green" />
                <input id="btnInvalid" runat="server" visible="false" type="button" value="作废订单" class="btn green" />
                <input id="btnPrint" type="button" value="打印订单" class="btn violet" />
                <input id="btnReturn" type="button" value="返回上一页" class="btn yellow" onclick="javascript: history.back(-1);" />
            </div>
            <div class="clear"></div>
        </div>
        <!--/工具栏-->
    </form>
</body>
</html>
