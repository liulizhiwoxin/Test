
$(function () {
    //默认选中项
    $("#o_box1,#e_free1").find("a:first").addClass("checked");
    var feer = $("#e_free1 a:first em:last").text();
    $("#express_fee").text(parseFloat(feer).toFixed(2));
    $("#realAmount").text((parseFloat($("#realAmount").text()) + parseFloat(feer)).toFixed(2));

    $(".setdef").css("border-width", "0");
    $(".newaddress").css("display", "none");

    //    var _id = '<%=_new_id %>';
    //    if (_id != "") {
    //        $(".radio1[id=" + _id + "]").addClass("checked");
    //        //                $("input[name=isdefault]").val(_id);
    //        //                $("input[name=address_id]").val(_id);
    //    }
    //    else {
    //        $(".radio1").each(function () {

    //            if ($(this).attr("isdefault") == "1") {
    //                $(this).addClass("checked");
    //                //                        $("input[name=isdefault]").val(this.id);
    //                //                        $("input[name=address_id]").val(this.id);
    //            }
    //        });
    //    }
});

function radioselect(chkobj, address_id) {
    $("input[name=isdefault]").val(address_id);
    $(".radio1").removeClass("checked");
    $(chkobj).addClass("checked");
    if ($("input[name=isdefault]").val() == "0") {
        $(".setdef").css("border-width", "1px");
        $(".newaddress").css("display", "block");
    }
    else {
        $(".setdef").css("border-width", "0");
        $(".newaddress").css("display", "none");
    }
    //$("input[name=address_id]").val(address_id);
}
//选择支付方式
function radioselect1(chkobj, payment_id) {
    $("input[name=payment]").val(payment_id);
    $(".radio2").removeClass("checked");
    $(chkobj).addClass("checked");
    //$("input[name=payment_id]").val(payment_id);
}
//选择快递方式
function radioselect2(chkobj, express_id, express_fee) {
    $("input[name=express]").val(express_id);
    $(".radio3").removeClass("checked");
    $(chkobj).addClass("checked");
    //$("input[name=express_id]").val(express_id);

    $("#express_fee").text(express_fee);
    //总金额
    var total_price = $(".stext2").text();
    var order_amount = parseFloat(total_price) + parseFloat(express_fee);
    $("#realAmount").text(order_amount.toFixed(2));
}

function ShowCoupons() {
    if ($(".coupons").css("display") == "none") {
        $(".coupons").show();
    }
    else {
        $(".coupons").hide();
    }
}



//提交订单
function submitOrder() {
    if (!$(".order_tip2 a").hasClass("checked")) {
        alert("请选择收货地址！");
        return;
    }
    if (!$("#e_free1 a").hasClass("checked")) {
        alert("请选择配送方式！");
        return;
    }
    if ($("#o_box1 p a:eq(1)").hasClass("checked")) {
        var balance = parseFloat($("#o_box1 p span:eq(4)").text()); //账户可用余额
        var realAmount = parseFloat($("#realAmount").text()); //订单金额
        var frozen_amount = parseFloat($("input[name=frozen_amount]").val());//冻结金额
        if (balance >= realAmount) {
            var goodData = "";
            for (var i = 0; i < $(".cart_list tbody tr").length; i++) {
                goodData += $(".cart_list tbody tr:eq(" + i + ")").find("td:last input").val() + "&";
            }

            $.ajax({
                url: "/tools/carabout_ajax.ashx?action=submit_order_account",
                type: "post",
                data: { goods: goodData,
                    AddressId: $(".order_tip2 a.checked").find("input[type=radio]").val(),
                    expressId: $("#e_free1 a.checked").find("input[type=radio]").val(),
                    totalprice: $("#realAmount").text()
                },
                dataType: "json",
                success: function (data) {
                    if (data.status == 1) {
                        alert(data.msg);
                        window.location = "http://" + window.location.host + "/user/orders.html";
                        return;
                    }
                },
                timeout: 20000
            });
        } else {
            alert("账户可用余额不足");
            return;
        }
    } else {
        var goodData = "";
        for (var i = 0; i < $(".cart_list tbody tr").length; i++) {
            goodData += $(".cart_list tbody tr:eq(" + i + ")").find("td:last input").val() + "&";
        }
        var total = $("#realAmount").text();
        $.ajax({
            url: "/tools/carabout_ajax.ashx?action=submit_order",
            type: "post",
            data: { goods: goodData,
                AddressId: $(".order_tip2 a.checked").find("input[type=radio]").val(),
                expressId: $("#e_free1 a.checked").find("input[type=radio]").val(),
                totalprice: $("#realAmount").text()
            },
            dataType: "json",
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("状态：" + textStatus + ";出错提示：" + errorThrown);
            },
            success: function (data) {
//                var Num = "";
//                for (var i = 0; i < 6; i++) {
//                    Num += Math.floor(Math.random() * 10);
//                }

                if (data.status == 1) {
//                    var idd = data.orderId + "_" + Num;
                    //                    window.location = "http://" + window.location.host + "/OnlinePay/charge.aspx?type=pay&id=" + data.orderId + "&money=" + $("#realAmount").text();
                    window.open("http://" + window.location.host + "/OnlinePay/charge.aspx?type=pay&id=" + data.orderId + "&money=" + $("#realAmount").text(), "_blank");
                } else {
                    alert(data.msg);
                }
            },
            timeout: 20000
        });
    }


} 