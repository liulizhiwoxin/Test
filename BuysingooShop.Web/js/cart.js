
//删除元素
function HintRemove(obj){
	$("#"+obj).remove();
}

//添加进购物车
function CartAdd(obj, webpath, linktype, linkurl){
    if ($("#goods_id").val() == "" || $("#goods_quantity").val() == "") {
        return false;
    }
    else { 
        if($("#goods_color"))
        {
            if ($("#goods_color").val() == "") {
                alert("请选择颜色！");
                return false;
            }
        }
        if($("#goods_size"))
        {
            if ($("#goods_size").val() == "") {
                alert("请选择尺码！");
                return false;
            } 
        }

    }
    var goods_volume;
    var goods_weight;
    if ($("input[name=goods_volume]"))
    {
        goods_volume = $("input[name=goods_volume]").attr("volume");
        goods_weight = $("input[name=goods_volume]").attr("weight");
    }

	$.ajax({
		type: "post",
		url: webpath + "tools/submit_ajax.ashx?action=cart_goods_add",
		data: {
			"goods_id" : $("#goods_id").val(),
			"goods_quantity" : $("#goods_quantity").val(),
			"goods_color": $("#goods_color").val(),
			"goods_size": $("#goods_size").val(),
			"goods_img_url": $("#goods_img_url").val(),
			"goods_volume": goods_volume,
            "goods_weight": goods_weight
		},
		dataType: "json",
		beforeSend: function(XMLHttpRequest) {
			//发送前动作
		},
		success: function(data, textStatus) {
			if (data.status == 1) {
				if(linktype==1){
					location.href=linkurl;
				}else{
					$("#cart_info_hint").remove();
					var HintHtml = '<div id="cart_info_hint" class="msg_tips cart_info">'
						+ '<div class="ico"></div>'
						+ '<div class="msg">'
						+ '<strong>商品已成功添加到购物车！</strong>'
						+ '<p>购物车共有 <b>' + data.quantity + '</b> 件商品，合计：<b class="red">' + data.amount + '</b>元</p>'
						+ '<a class="btn-success" title="去购物车结算" href="' + linkurl + '" target="_blank">去结算</a>&nbsp;&nbsp;'
						+ '<a title="再逛逛" href="javascript:;" onclick="HintRemove(\'cart_info_hint\');">再逛逛</a>'
						+ '<a class="close" title="关闭" href="javascript:;" onclick="HintRemove(\'cart_info_hint\');"><span>关闭</span></a>'
						+ '</div>'
						+ '</div>'
					$(obj).after(HintHtml); //添加节点
				}
			} else {
				$("#cart_info_hint").remove();
				var HintHtml = '<div id="cart_info_hint" class="msg_tips cart_info">'
					+ '<div class="ico error"></div>'
					+ '<div class="msg">'
					+ '<strong>商品添加到购物车失败！</strong>'
					+ '<p>' + data.msg + '</p>'
					+ '<a class="close" title="关闭" href="javascript:void(0);" onclick="HintRemove(\'cart_info_hint\');"><span>关闭</span></a>'
					+ '</div>'
					+ '</div>'
				$(obj).after(HintHtml); //添加节点
				//alert(data.msg);
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
function DeleteCart(obj, webpath, goods_id){
	if(!confirm("您确认要从购物车中移除吗？") || goods_id==""){
		return false;
	}
	$.ajax({
		type: "post",
		url: webpath + "tools/submit_ajax.ashx?action=cart_goods_delete",
		data: {"goods_id" : goods_id},
		dataType: "json",
		beforeSend: function(XMLHttpRequest) {
			//发送前动作
		},
		success: function(data, textStatus) {
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
//批量删除购物车商品
function BatchDel() {
    var valueArr = '';
    var $checkA = $(".checkall:contains(√)");
    if ($checkA.length == 0) {
        alert("请选择一个商品！");
        return;
    }

    for (var i = 0; i < $checkA.length; i++) {
        var goods_id = $checkA.eq(i).parents("tr").find("input[name=goods_id]").val();
        var goods_color = $checkA.eq(i).parents("tr").find("input[name=goods_color]").val();
        var goods_size = $checkA.eq(i).parents("tr").find("input[name=goods_size]").val();

        valueArr += goods_id + "|" + goods_color + goods_size;
        if (i < $checkA.length - 1) {
            valueArr += ",";
        }
    }
    DeleteCart(this, "/", valueArr);
}
//批量加入收藏
function BatchFavorite() {
    var valueArr = '';
    var $checkA = $(".checkall:contains(√)");
    if ($checkA.length == 0) {
        alert("请选择一个商品！");
        return;
    }

    for (var i = 0; i < $checkA.length; i++) {
        var goods_id = $checkA.eq(i).parents("tr").find("input[name=goods_id]").val();

        valueArr += goods_id;
        if (i < $checkA.length - 1) {
            valueArr += ",";
        }
    }
    addFavorites(valueArr);
}
//计算购物车金额
function CartAmountTotal(obj, webpath, goods_id){
	if(isNaN($(obj).val())){
	    alert('商品数量只能输入数字!');
		$(obj).val("1");
	}
	$.ajax({
	    type: "post",
	    url: webpath + "tools/submit_ajax.ashx?action=cart_goods_update",
	    data: {
	        "goods_id": goods_id,
	        "goods_quantity": $(obj).val(),
	        "goods_color": $(obj).siblings("input[name=goods_color]").val(),
	        "goods_size": $(obj).siblings("input[name=goods_size]").val()
	    },
	    dataType: "json",
	    beforeSend: function (XMLHttpRequest) {
	        //发送前动作
	    },
	    success: function (data, textStatus) {
	        if (data.status == 1) {
	            var pp = $(obj).siblings("input[name=goods_price]");
	            var subtotal = parseInt($(obj).val()) * parseFloat($(pp).val());
	            $(obj).parents("tr").find(".subtotal").html(subtotal.toFixed(2));
                $(obj).parents("tr").find(".subtotalMoney").html(subtotal.toFixed(2));
	            var prices = 0;
	            var points = 0;
	            $(".checkall:contains(√)").each(function () {
	                var quantity = $(this).parents("tr").find("input[name=goods_quantity]").val();
	                var price = $(this).parents("tr").find("input[name=goods_price]").val();
	                var point = $(this).parents("tr").find("input[name=goods_point]").val();

	                prices = prices + (quantity * 1 * parseFloat(price));
	                points = points + (quantity * 1 * parseFloat(point));
	            });
	            $(".total_point").html(points);
	            $("#total_price").val(prices.toFixed(2));
	            $("#currencyMoney").html(prices.toFixed(2))
	            //location.reload();
	        } else {
	            alert(data.msg);
	            location.reload();
	        }
	    },
	    error: function (XMLHttpRequest, textStatus, errorThrown) {
	        alert("状态：" + textStatus + "；出错提示：" + errorThrown);
	    },
	    timeout: 20000
	});






	return false;
}
//购物车数量加减
function CartComputNum(obj, webpath, goods_id, num){
	if(num > 0){
		var goods_quantity = $(obj).prev("input[name='goods_quantity']");
		$(goods_quantity).val(parseInt($(goods_quantity).val()) + 1);
		//计算购物车金额
		CartAmountTotal($(goods_quantity), webpath, goods_id);
	}else{
    var goods_quantity = $(obj).next("input[name='goods_quantity']");
		if(parseInt($(goods_quantity).val()) > 1){
			$(goods_quantity).val(parseInt($(goods_quantity).val()) - 1);
			//计算购物车金额
			CartAmountTotal($(goods_quantity), webpath, goods_id);
		}
	}
}
//购物车数量加减
function CartNum(num) {
    if (num == 1) {
        var goods_quantity = $("input[name='goods_quantity']");
        $(goods_quantity).val(parseInt($(goods_quantity).val()) + 1);
    } else if (num == -1) {
        var goods_quantity = $("input[name='goods_quantity']");
        if (parseInt($(goods_quantity).val()) > 1) {
            $(goods_quantity).val(parseInt($(goods_quantity).val()) - 1);
        }
    }
    else {
        var goods_quantity = $("input[name='goods_quantity']");
        if (parseInt($(goods_quantity).val()) > 0) {
            $(goods_quantity).val(parseInt($(goods_quantity).val()));
        }
        else {
            alert("请输入正确的数字！");
        }
    }
}
//计算支付手续费总金额
function PaymentAmountTotal(obj){
	var payment_price = $(obj).next("input[name='payment_price']").val();
	$("#payment_fee").text(payment_price); //运费
	OrderAmountTotal();
}
//计算配送费用总金额
function FreightAmountTotal(obj){
	var express_price = $(obj).next("input[name='express_price']").val();
	$("#express_fee").text(express_price); //运费
	OrderAmountTotal();
}
//计算订单总金额
function OrderAmountTotal(){
	var goods_amount = $("#goods_amount").text(); //商品总金额
	var payment_fee = $("#payment_fee").text(); //手续费
	var express_fee = $("#express_fee").text(); //运费
	var order_amount = parseFloat(goods_amount) + parseFloat(payment_fee) + parseFloat(express_fee); //订单总金额 = 商品金额 + 手续费 + 运费
	$("#order_amount").text(order_amount.toFixed(2));
}