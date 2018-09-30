//数量加减
function CartComputNum(obj, webpath, goods_id, num) {
    if (num > 0) {
        $(obj).prev().prev().removeClass("minus-off");
        var goods_quantity = $(obj).prev("input[name='goods_quantity']");
        $(goods_quantity).val(parseInt($(goods_quantity).val()) + 1);

        var quantity = $(goods_quantity).val(); //数量
        var price = $(obj).parents("tr").find("input[name=goods_price1]").val(); //单价
        var weight = $(obj).parents("tr").find("input[name=goods_weight]").val(); //重量
        var goodtype = $(obj).parents("tr").find("input[name=type]").val();//类型
        var prices = 0;
        if (goodtype == "智能家居") { 
            prices = quantity * price;
        } else {
            prices = quantity * price * weight;
        }
        $(obj).parents("tr").find("td:eq(4) h2").text("￥" + prices.toFixed(2));

        //计算购物车金额
        CartAmountTotal($(goods_quantity), webpath, goods_id);
    } else {
        var goods_quantity = $(obj).next("input[name='goods_quantity']");
        if (parseInt($(goods_quantity).val()) > 1) {
            $(goods_quantity).val(parseInt($(goods_quantity).val()) - 1);

            var quantity = $(goods_quantity).val(); //数量
            var price = $(obj).parents("tr").find("input[name=goods_price1]").val(); //单价
            var weight = $(obj).parents("tr").find("input[name=goods_weight]").val(); //重量
            var goodtype = $(obj).parents("tr").find("input[name=type]").val(); //类型
            var prices = 0;
            if (goodtype == "智能家居") {
                prices = quantity * price;
            } else {
                prices = quantity * price * weight;
            }
            $(obj).parents("tr").find("td:eq(4) h2").text("￥" + prices.toFixed(2));

            //计算购物车金额
            CartAmountTotal($(goods_quantity), webpath, goods_id);
        }
        if (parseInt($(goods_quantity).val()) <=1) {
            $(obj).addClass("minus-off");//禁用减号
        }
    }
}
//数量加减
function CartComputNum1(obj, num) {
    if (num > 0) {
        var goods_quantity = $(obj).prev("input[name='goods_quantity']");
        $(goods_quantity).val(parseInt($(goods_quantity).val()) + 1);
        if (isNaN($(goods_quantity).val()) || $(goods_quantity).val() == "") {
            alert('输入商品数量不能为空!');
            $(goods_quantity).val("1");
        }
        if (parseInt($(goods_quantity).val()) > 100) {
            alert("商品数量不能大于100");
            $(goods_quantity).val("100");
        }
        if ($(goods_quantity).val().substr(0, 1) == "0") {
            alert("商品数量输入有误");
            $(goods_quantity).val("1");
        }
    } else {
        var goods_quantity = $(obj).next("input[name='goods_quantity']");
        if (parseInt($(goods_quantity).val()) > 1) {
            $(goods_quantity).val(parseInt($(goods_quantity).val()) - 1);
            if (isNaN($(goods_quantity).val()) || $(goods_quantity).val() == "") {
                alert('输入商品数量不能为空!');
                $(goods_quantity).val("1");
            }
            if (parseInt($(goods_quantity).val()) > 100) {
                alert("商品数量不能大于100");
                $(goods_quantity).val("100");
            }
            if ($(goods_quantity).val().substr(0, 1) == "0") {
                alert("商品数量输入有误");
                $(goods_quantity).val("1");
            }
        }
    }
}
//全选
function checkAll2(chkobj) {
    if ($(chkobj).html().indexOf("√") < 0) {
        $(chkobj).html("√");
        $(".checkall").html("√");
        //计算金额
        var prices = 0;
        for (var i = 0; i < $(".checkall").length; i++) {
            prices += parseFloat($(".checkall").eq(i).parents("tr").find("td.price span").text());
        }
        $(".total_price,.total_pirce1").text(prices.toFixed(2));
    } else {
        $(chkobj).html("&nbsp;");
        $(".checkall").html("&nbsp;");
        $(".total_price").html("0.00");
        $(".total_pirce1,.total_pirce2").text("0.00");
    }

}
//单选
function checkOne(chkobj) {
    if ($(chkobj).html().indexOf("√") < 0) {
        $(chkobj).html("√");
        var num = $(".checkall").length;
        var num2 = $(".checkall:contains(√)").length;
        if (num == num2) {
            $(".chkall").html("√");
        }

    } else {
        $(chkobj).html("");
        $(".chkall").html("&nbsp;");
    }
    //计算金额
    var prices = 0;
    for (var i = 0; i < $(".checkall:contains(√)").length; i++) {
        prices += parseFloat($(".checkall:contains(√)").eq(i).parents("tr").find("td.price span").text());
    }
    $(".total_price,.total_pirce1").text(prices.toFixed(2));


}

function onKeyPressBlockNumbers(e) {
    var key = window.event ? e.keyCode : e.which;
    var keychar = String.fromCharCode(key);
    reg = /[\d\b]/;
    return reg.test(keychar);
}


//计算购物车金额
function CartAmountTotal(obj, webpath, goods_id) {
    if (isNaN($(obj).val()) || $(obj).val()=="") {
        alert('输入商品数量不能为空!');
        $(obj).val("1");
    }
    if (parseInt($(obj).val()) > 100) {
        alert("商品数量不能大于100");
        $(obj).val("100");
    }
    if ($(obj).val().substr(0, 1) == "0") {
        alert("商品数量输入有误");
        $(obj).val("1");
    }

    var quantity = $(obj).val(); //数量
    var price = $(obj).parents("tr").find("input[name=goods_price1]").val(); //单价
    var weight = $(obj).parents("tr").find("input[name=goods_weight]").val(); //重量
    var goodtype = $(obj).parents("tr").find("input[name=type]").val(); //类型
    var prices1 = 0;
    if (goodtype == "智能家居") {
        prices1 = quantity * price;
    } else {
        prices1 = quantity * price * weight;
    }
//    var prices1 = quantity * price * weight;
    $(obj).parents("tr").find("td:eq(4) h2").text("￥" + prices1.toFixed(2));

    //计算金额
    var num = $(".check1").length;
    var prices = 0;
    for (var i = 0; i < num; i++) {

        if ($(".check1").eq(i).attr("class") == "check check1 iconfont checkyes") {
            prices += parseFloat($(".tc-amount").eq(i).next().find("h2").text().split("￥")[1]);
        }
    }
    $(".pr10:eq(1) span").text("￥" + prices.toFixed(2));
}

//选择购物车的商品
function confirm2() {
    var num2 = 0;

    for (var i = 0; i < $(".check1").length; i++) {
        if ($(".check1").eq(i).attr("class") == "check check1 iconfont checkyes") {
            num2++;
        }
    }
    if (num2 == 0) {
        alert("请先选择商品！");
        return;
    }
    var goodId = "";
    var goods_quantity = "";
    var goodsWeight = "";
    var goodsPrice = "";
    var arrayQuantity = "";
    var goodsType = "";

    for (var i = 0; i < $(".check1").length; i++) {
        if ($(".check1").eq(i).attr("class") == "check check1 iconfont checkyes") {
            goodId += $(".check1").eq(i).parents("tr").find("input[name=goods_id]").val() + "|";
            //            goods_quantity = $(".tc-amount").parents("tr").eq(i).find("input[name=goods_quantity]").val();
            goods_quantity = $("input[name=goods_quantity]").eq(i).val();
            if (isNaN(goods_quantity) || $(goods_quantity) == "") {
                alert('输入商品数量不能为空!');
                $(".tc-amount").eq(i).find("input").val("1");
                return;
            }
            if (parseInt(goods_quantity) > 100) {
                alert("商品数量不能大于100");
                $(".tc-amount").eq(i).find("input").val("100");
                return;
            }
            if (goods_quantity.substr(0, 1) == "0") {
                alert("商品数量输入有误");
                $(".tc-amount").eq(i).find("input").val("1");
                return;
            }
            goodsWeight = $(".trr").eq(i).find("input[name=goods_weight]").val();
            goodsPrice = $(".trr").eq(i).find("input[name=goods_price1]").val();
            goodsType = $(".trr").eq(i).find("input[name=type]").val();
            arrayQuantity += goods_quantity + "|" + goodsType + "|" + goodsWeight + "|" + goodsPrice + ",";
        }
    }
    //window.location = "http://" + window.location.host + "/goods/confirm2/" + goodId.substring(0, goodId.length - 1) + ".html";
    //window.location = "http://" + window.location.host + "/goods/confirm2.aspx?id=" + goodId.substring(0, goodId.length - 1);

    $.ajax({
        type: "post",
        url: "/tools/submit_ajax.ashx?action=cart_goods_updates",
        data: {
            "goodId": goodId.substring(0, goodId.length - 1),
            "goods_quantity": arrayQuantity.substring(0, arrayQuantity.length - 1)
        },
        dataType: "text",
        beforeSend: function (XMLHttpRequest) {
            //发送前动作
        },
        success: function (data, textStatus) {
            //alert(data);
            if (data == "1") {
                //alert(data.msg);
                window.location = "http://" + window.location.host + "/goods/f-order.aspx?id=" + goodId.substring(0, goodId.length - 1);
            }
            else {
                alert(data);
                window.location = "http://" + window.location.host + "/login"+1+".html";
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(XMLHttpRequest.responseText + "状态：" + textStatus + "；出错提示：" + errorThrown);
        },
        timeout: 20000
    });
        return false;
}

//添加进购物车
function CartAdd() {
    var dataId = $("#hideId").val();
    var datatype = $("#hideId").attr("datatype");
    var datanum = $("#hideId").attr("datanum");
    var dataweight = $("#hideId").attr("dataweight");
    var dataprice = $("#hideId").attr("dataprice");

    if (datatype == "") {
        alert("请选择大米种类！");
        return false;
    }
    if (datanum == "" || dataweight == "") {
        alert("请选择大米重量和数量！");
        return false;
    }
    $.ajax({
        type: "post",
        url: "/tools/submit_ajax.ashx?action=cart_goods_add",
        data: {
            "goods_id": dataId,
            "goods_num": datanum,
            "goods_type":datatype,
            "goods_weight":dataweight,
            "goods_price": dataprice
        },
        dataType: "json",
        beforeSend: function (XMLHttpRequest) {
            //发送前动作
        },
        success: function (data, textStatus) {
            if (data.status == 1) {
//                alert(data.msg);
                window.location = "http://" + window.location.host + "/goods/cart.html";
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("状态：" + textStatus + "；出错提示：" + errorThrown);
        },
        timeout: 20000
    });
    return false;
}

//删除购物车商品
function DeleteCart(goods_id) {
    if (!confirm("您确认要从购物车中移除吗？") || goods_id == "") {
        return false;
    }
    $.ajax({
        type: "post",
        url: "/tools/submit_ajax.ashx?action=cart_goods_delete",
        data: { "goods_id": goods_id },
        dataType: "json",
        beforeSend: function (XMLHttpRequest) {
            //发送前动作
        },
        success: function (data, textStatus) {
            if (data.status == 1) {
                location.reload();
            } else {
                alert(data.msg);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("状态：" + textStatus + "；出错提示：" + errorThrown);
        },
        timeout: 20000
    });
    return false;
}

//添加到收藏夹
function favoriteCart(goods_id, obj) {
    if ($(obj).attr("isfavorite") == 1) {
        return;
    }
    $.ajax({
        type: "post",
        url: "/tools/submit_ajax.ashx?action=favorite_cart_add",
        data: {
            "goods_id": goods_id,
            "dataInfo": $(obj).attr("dataInfo")
        },
        dataType: "json",
        beforeSend: function (XMLHttpRequest) {
            //发送前动作
        },
        success: function (data) {
            if (data.status == 1) {
                $(obj).attr("isfavorite", 1).text("已收藏");
            } else if (data.status == 2) {
                $(obj).attr("isfavorite", 0).text("收藏");
                alert(data.msg);
                window.location.href = "http://" + window.location.host + "/login.html";
            } else {
                $(obj).attr("isfavorite", 0).text("收藏");
                alert(data.msg);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("状态：" + textStatus + "；出错提示：" + errorThrown);
        },
        timeout: 20000
    });
    return false;
}


