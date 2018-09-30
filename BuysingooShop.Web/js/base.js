/* 
*作者：一些事情
*时间：2012-6-28
*需要结合jquery和Validform和lhgdialog一起使用
----------------------------------------------------------*/
/*返回顶部*/
var lastScrollY = 0;
$(function () {
    $("body").prepend("<a id=\"gotop\" class=\"gotop\" href=\"#\" title=\"返回顶部\" onfocus=\"this.blur()\" onclick=\"window.scrollTo(0,0);\"></a>");
    window.setInterval("gotop()", 1);

    //密码明文显示
    $(".SeePassword").click(function () {
        var $pass = $(this).parent().find("#txtPassword");
        if ($pass.attr("type") == "password") {
            $pass.attr("type", "text");
        }
        else {
            $pass.attr("type", "password");  
        }
    });


});
function gotop(){
	var diffY;
	if (document.documentElement && document.documentElement.scrollTop)
		diffY = document.documentElement.scrollTop;
	else if (document.body)
		diffY = document.body.scrollTop
	else
		{/*Netscape stuff*/}
	percent=.1*(diffY-lastScrollY);
	if(percent>0)percent=Math.ceil(percent);
	else percent=Math.floor(percent);
	lastScrollY=lastScrollY+percent;
	if(lastScrollY<100){
	document.getElementById("gotop").style.display="none";
	} else {
	document.getElementById("gotop").style.display="block";
	}
}
/*搜索查询*/
function SiteSearch(send_url, divTgs, channel_name) {
    var strwhere = "";
    if (channel_name !== undefined) {
        strwhere = "&channel_name=" + channel_name
    }
	var str = $.trim($(divTgs).val());
	if (str.length > 0 && str != "输入关健字") {
	    window.location.href = send_url + "?keyword=" + encodeURI($(divTgs).val()) + strwhere;
	}
	return false;
}
/*切换验证码*/
function ToggleCode(obj, codeurl) {
    $(obj).children("img").eq(0).attr("src", codeurl + "?time=" + Math.random());
	return false;
}
//复制文本
function copyText(txt){
	window.clipboardData.setData("Text",txt); 
	$.dialog.tips("复制成功，可以通过粘贴来发送！",2,"32X32/succ.png");
} 
//全选取消按钮函数，调用样式如：
function checkAll(chkobj){
	if($(chkobj).text()=="全选"){
	    $(chkobj).text("取消");
		$(".checkall").prop("checked", true);
	}else{
    	$(chkobj).text("全选");
		$(".checkall").prop("checked", false);
	}
}
//货币换算
function MoneyExchange(price) {

    $.ajax({
        type: "post",
        url: "../tools/submit_ajax.ashx?action=currency_change1&price=" + price,
        dataType: "json",
        timeout: 20000,
        success: function (data, textStatus) {
            if (data.status == 1) {
                $("#currencyMoney").html(data.msg);
            }
            else if (data.status == 0) {
                alert(data.msg);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $.dialog.alert("状态：" + textStatus + "；出错提示：" + errorThrown);
        }
    });
}
function MoneyExchange1(obj,price) {
    
        $.ajax({
            type: "post",
        url: "../tools/submit_ajax.ashx?action=currency_change1&price=" + price,
        dataType: "json",
        timeout: 20000,
        success: function (data, textStatus) {
            if (data.status == 1) {
                $(obj).html(data.msg);
            }
            else if (data.status == 0) {
                alert(data.msg);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $.dialog.alert("状态：" + textStatus + "；出错提示：" + errorThrown);
        }
    });
    
    
}
//全选取消按钮函数，调用样式如：
function checkAll2(chkobj) {
    if ($(chkobj).html().indexOf("√") < 0) {
        $(chkobj).html("√");
        $(".checkall").html("√");
        //计算金额
    var prices = 0;
    var points = 0;

    for (var i = 0; i < $(".checkall").length; i++) {
        var quantity = $(".checkall").eq(i).parents("tr").find("input[name=goods_quantity]").val();
        var price = $(".checkall").eq(i).parents("tr").find("input[name=goods_price]").val();
        var point = $(".checkall").eq(i).parents("tr").find("input[name=goods_point]").val();

        prices = prices + (quantity * 1 * parseFloat(price));
        points = points + (quantity * 1 * parseFloat(point));
    }
    $(".total_point").html(points);
    $("#total_price").val(prices);
    $("#currencyMoney").html(prices)
    } else {
        $(chkobj).html("&nbsp;");
        $(".checkall").html("");
        $(".total_point").html("0");
        $("#total_price").val("0");
        $("#currencyMoney").html("0")
    }
    
}

//选择取消按钮函数，调用样式如：
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
    var points = 0;

    for (var i = 0; i < $(".checkall:contains(√)").length; i++) {
        var quantity = $(".checkall:contains(√)").eq(i).parents("tr").find("input[name=goods_quantity]").val();
        var price = $(".checkall:contains(√)").eq(i).parents("tr").find("input[name=goods_price]").val();
        var point = $(".checkall:contains(√)").eq(i).parents("tr").find("input[name=goods_point]").val();

            prices = prices + (quantity * 1 * parseFloat(price));
            points = points + (quantity * 1 * parseFloat(point));
    }
    $(".total_point").html(points);
    $("#total_price").val(prices);

    $("#currencyMoney").html(prices)
}
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
    $("input[name=address_id]").val(address_id);
}
function radioselect1(chkobj, payment_id) {
    $("input[name=payment]").val(payment_id);
    $(".radio2").removeClass("checked");
    $(chkobj).addClass("checked");
    $("input[name=payment_id]").val(payment_id);
}
function radioselect2(chkobj, express_id, express_fee, added_weight, volume) {
    $("input[name=express]").val(express_id);
    $(".radio3").removeClass("checked");
    $(chkobj).addClass("checked");
    $("input[name=express_id]").val(express_id);
    $("input[name=express_fee]").val(express_fee);

    //总金额

    var total_price = $("input[name=total_price]").val();
    var express = $("input[name=express_fee]").val();

    var _fee = 0;
    var b = false;

    $("input[name=goods_volume]").each(function () {

        

        var _v = parseFloat($(this).attr("volume"));
        var _w = parseFloat($(this).attr("weight"));
        var _quantity = parseInt($(this).attr("quantity"));

        if (_v > 0) {
            if (_v > volume) {
                _w = _v / volume;
            }
            if (_w > 0.5) {
                var _ii = Math.round(_w / 0.5);
                if (_ii * 0.5 < _w) {
                    _ii = _ii + 1;

                }
            }
            _fee = _fee * 1 + (_ii - 1) * 0.5 * 2 * added_weight * _quantity;
            b = true;
        }
    });

    if (b==true) {

        _fee = _fee + parseFloat(express);
    }
    else {
        _fee = express;
    }
        $("i", chkobj).html(_fee)

        var order_amount = parseFloat(total_price) + parseFloat(_fee);
        $("input[name=express_fee]").val(_fee);
        $("#realAmount").text(order_amount.toFixed(2));


    }


//购物车下单
function checkout() {
    var num = $(".checkall:contains(√)").length;
    if (num > 0) {
        var strwhere = "";
        $(".checkall:contains(√)").each(function () {
            var goods_id = $(this).parents("tr").find("input[name=goods_id]").val();
            var goods_color = $(this).parents("tr").find("input[name=goods_color]").val();
            var goods_size = $(this).parents("tr").find("input[name=goods_size]").val();
            strwhere += goods_id + "|" + goods_color + goods_size + ",";
        });
        //检查是否登录

        $.ajax({
            type: "POST",
            url: "/tools/submit_ajax.ashx?action=user_check_login",
            dataType: "json",
            timeout: 20000,
            success: function (data, textStatus) {
                if (data.status == 1) {
                    //location.href = '/goods/confirm.aspx?checkgoods=' + unescape(strwhere);
                    $.ajax({
                        type: "POST",
                        url: "/goods/confirm.aspx?checkgoods=" + encodeURI(strwhere, "UTF-8"),
                        dataType: "text",
                        success: function (data, textStatus) {
                            location.href = "/goods/confirm.aspx";
                        }
                    });
                } else {
                    alert("请先登录！");
                    $('.login').removeClass('hide');
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                $.dialog.alert("状态：" + textStatus + "；出错提示：" + errorThrown);
            }
        });

    }
    else {
        alert("请选择一个商品！");
    }
}
//购物车下单
function checkout2() {
    var num = $(".mark3 .cart>li").length;
    if (num > 0) {
        var strwhere = "";
        $(".mark3 .cart>li").each(function () {
            var goods_id = $(this).find("input[name=goods_id]").val();
            var goods_color = $(this).find("input[name=goods_color]").val();
            var goods_size = $(this).find("input[name=goods_size]").val();
            strwhere += goods_id + "|" + goods_color + goods_size + ",";
        });
        //检查是否登录

        $.ajax({
            type: "POST",
            url: "/tools/submit_ajax.ashx?action=user_check_login",
            dataType: "json",
            timeout: 20000,
            success: function (data, textStatus) {
                if (data.status == 1) {
                    //location.href = '/goods/confirm.aspx?checkgoods=' + unescape(strwhere);
                    $.ajax({
                        type: "POST",
                        url: "/goods/confirm.aspx?checkgoods=" + unescape(strwhere),
                        dataType: "text",
                        success: function (data, textStatus) {
                            location.href = "/goods/confirm.aspx";
                        }
                    });
                } else {
                    alert("请先登录！");
                    $('.login').removeClass('hide');
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                $.dialog.alert("状态：" + textStatus + "；出错提示：" + errorThrown);
            }
        });

    }
    else {
        alert("请选择一个商品！");
    }
}
//积分兑换
function NumConvert(obj){
	var maxAmount = parseFloat($("#hideAmount").val()); //总金额
	var pointCashrate = parseFloat($("#hideCashrate").val()); //兑换比例
	var currAmount = parseFloat($(obj).val()); //需要转换的金额
    
	if(currAmount > maxAmount){
		currAmount = maxAmount;
		$(obj).val(maxAmount);
	}
	var convertPoint = currAmount * pointCashrate;
	$("#convertPoint").text(convertPoint);
}
//用积分支付
function PointPay(obj) {
    var maxPoint = parseInt($("#hidePoint").val()); //总积分
    var pointCashrate = parseInt($("#hideCashrate").val()); //兑换比例
    var express = parseFloat($("input[name=express_fee]").val());
    

    if ($(obj).val() != "") {
        var currPoint = parseInt($(obj).val()); //需要转换的积分

        $(obj).val(currPoint);
        if (currPoint > maxPoint) {
            currPoint = maxPoint;
            $(obj).val(maxPoint);
        }
        var convertAmount = currPoint / pointCashrate;
        $("#convertAmount").text(convertAmount);
        $("#hidPointAmount").val(convertAmount);

        var _total = parseFloat(realAmount - convertAmount + express).toFixed(2);
        $("#realAmount").text(_total);

    }
    else {
        $("#convertAmount").text("0.00");
        $("#hidPointAmount").val("0");
        $("#realAmount").text(realAmount + express);
    }
    
    
}
function isshow(obj) {
   var c= $(".points").css("display");
   if (c == "none") {
       $(".points").show();
   }
   else {
       $(".points").hide();
       $(".points input[name=order_point]").val("");
   }
}
/*PROPS选择卡特效*/
function ToggleProps(obj, cssname){
	$(obj).parent().children("li").removeClass(cssname);
	$(obj).addClass(cssname);
}
//Tab控制选项卡
function tabs(tabId, event) {
    //绑定事件
	var tabItem = $(tabId + " #tab_head ul li a");
	tabItem.bind(event,function(){
		//设置点击后的切换样式
		tabItem.removeClass("current");
		$(this).addClass("current");
		//设置点击后的切换内容
		var tabNum = tabItem.parent().index($(this).parent());
		$(tabId + " .tab_inner").hide();
        $(tabId + " .tab_inner").eq(tabNum).show();
	});
}
//显示浮动窗口
function showWindow(objId){
	var box = '<div style="text-align:left;line-height:1.8em;">' + $('#' + objId).html() + '</div>';
	var tit = $('#' + objId).attr("title");
	var dialog = $.dialog({
		lock: true,
		min: false,
		max: false,
		resize: false,
		title: tit,
		content: box,
		width: 480,
		ok: function () {
		},
		cancel: false
	});
}

//执行删除操作
function ExecDelete(sendUrl, checkValue, urlId){
	var urlObj = $('#' + urlId);
	//检查传输的值
	if (!checkValue) {
	    $.dialog.alert("对不起，请选中您要操作的记录！");
        return false;
	}
    var m = $.dialog.confirm("删除记录后不可恢复，您确定吗？", function () {
		$.ajax({
			type: "POST",
			url: sendUrl,
			dataType: "json",
			data: {
				"checkId":  checkValue
			},
			timeout: 20000,
			success: function(data, textStatus) {
				if (data.status == 1){
					$.dialog.tips(data.msg, 2, "32X32/succ.png", function(){
						if(urlObj){
							location.href = urlObj.val();
						}else{
							location.reload();
						}
					});
				} else {
					$.dialog.alert(data.msg);
				}
			},
			error: function (XMLHttpRequest, textStatus, errorThrown) {
			    $.dialog.alert("状态：" + textStatus + "；出错提示：" + errorThrown);
			}
		});
	}, function(){ });
}
//执行设置默认操作
function ExecDdfault(sendUrl, checkValue, urlId) {
    var urlObj = $('#' + urlId);
    //检查传输的值
    if (!checkValue) {
        $.dialog.alert("对不起，请选中您要操作的记录！");
        return false;
    }
    var m = $.dialog.confirm("你确定设置为默认？", function () {
        $.ajax({
            type: "POST",
            url: sendUrl,
            dataType: "json",
            data: {
                "checkId": checkValue
            },
            timeout: 20000,
            success: function (data, textStatus) {
                if (data.status == 1) {
                    $.dialog.tips(data.msg, 2, "32X32/succ.png", function () {
                        if (urlObj) {
                            location.href = urlObj.val();
                        } else {
                            location.reload();
                        }
                    });
                } else {
                    $.dialog.alert(data.msg);
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                $.dialog.alert("状态：" + textStatus + "；出错提示：" + errorThrown);
            }
        });
    }, function () { });
}

//单击执行AJAX请求操作
function clickSubmit(sendUrl){
	$.ajax({
		type: "POST",
		url: sendUrl,
		dataType: "json",
		timeout: 20000,
		success: function(data, textStatus) {
			if (data.status == 1){
				$.dialog.tips(data.msg, 2, "32X32/succ.png", function(){
					location.reload();
			    });
			} else {
				$.dialog.alert(data.msg);
			}
		},
		error: function (XMLHttpRequest, textStatus, errorThrown) {
		    $.dialog.alert("状态：" + textStatus + "；出错提示：" + errorThrown);
		}
	});
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

//加入收藏
function addFavorites(article_id) {
    var sendUrl = "/tools/submit_ajax.ashx?action=favorites_add&article_id="+article_id;
    $.ajax({
        type: "POST",
        url: sendUrl,
        dataType: "json",
        timeout: 20000,
        success: function (data, textStatus) {
            if (data.status == 1) {
                $.dialog.tips(data.msg, 2, "32X32/succ.png", function () {
                    
                });
            } else  if(data.status == 2){
                var m = $.dialog({
                    lock: true,
                    max: false,
                    min: false,
                    padding: 0,
                    title: "温馨提示",
                    content: "<div style='padding:15px 0;'>" + data.msg + "</div>",
                    button: [{
                        name: '登录',
                        callback: function () {
                            $('.login').removeClass('hide');
                        }
                    }, {
                        name: '取消',
                        focus: true
                    }]
                });
            }
            else
            {
                $.dialog.alert(data.msg);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $.dialog.alert("状态：" + textStatus + "；出错提示：" + errorThrown);
        }
    });
}

//链接下载
function downLink(point, linkurl){
	if(point > 0){
		$.dialog.confirm("下载需扣除" + point + "个积分<br />有效时间内重复下载不扣积分，继续吗？", function () {
			window.location.href = linkurl;
		});
	}else{
		window.location.href = linkurl;
	}
	return false;
}

//智能浮动层函数
$.fn.smartFloat = function() {
	var position = function(element) {
		var top = element.position().top, pos = element.css("position");
		var w = element.innerWidth();
		$(window).scroll(function() {
			var scrolls = $(this).scrollTop();
			if (scrolls > top) {
				if (window.XMLHttpRequest) {
					element.css({
						width: w,
						position: "fixed",
						top: 0
					});	
				} else {
					element.css({
						top: scrolls
					});	
				}
			}else {
				element.css({
					position: pos,
					top: top
				});	
			}
		});
	};
	return $(this).each(function() {
		position($(this));						 
	}); 
};

//=====================发送验证邮件=====================
function SendEmail(username, sendurl) {
	if(username == ""){
	    $.dialog.alert('对不起，用户名是不允许为空！');
		return false;
	}
	//提交
	$.ajax({
		url: sendurl,
		type: "POST",
		timeout: 60000,
		data: {
			username: function () {
				return username;
			}
		},
		dataType: "json",
		success: function (data, type) {
			if (data.status == 1) {
				$.dialog.tips(data.msg, 2, "32X32/succ.png", function(){});
			} else {
				$.dialog.alert(data.msg);
			}
		},
		error: function(XMLHttpRequest, textStatus, errorThrown){
		    $.dialog.alert("状态：" + textStatus + "；出错提示：" + errorThrown);
		}
	});
}

/*表单AJAX提交封装(包含验证)*/
function AjaxInitForm(formId, btnId, isDialog, urlId){
    var formObj = $('#' + formId);
	var btnObj = $("#" + btnId);
	var urlObj = $("#" + urlId);
	formObj.Validform({
		tiptype:3,
		callback:function(form){
			//AJAX提交表单
            $(form).ajaxSubmit({
                beforeSubmit: formRequest,
                success: formResponse,
                error: formError,
                url: formObj.attr("url"),
                type: "post",
                dataType: "json",
                timeout: 60000
            });
            return false;
		}
	});
    
    //表单提交前
    function formRequest(formData, jqForm, options) {
        btnObj.prop("disabled", true);
        btnObj.val("提交中...");
    }

    //表单提交后
    function formResponse(data, textStatus) {
		if (data.status == 1) {
		    btnObj.val("提交成功");
			//是否提示，默认不提示
			if(isDialog == 1){
				$.dialog.tips(data.msg, 2, "32X32/succ.png", function(){
					if(data.url){
						location.href = data.url;
					}else if(urlObj.length > 0 && urlObj.val() != ""){
						location.href = urlObj.val();
					}else{
						location.reload();
					}
				});
			}else{
				if(data.url){
					location.href = data.url;
				}else if(urlObj){
					location.href = urlObj.val();
				}else{
					location.reload();
				}
			}
        } else {
            $.dialog.alert(data.msg);
            btnObj.prop("disabled", false);
            btnObj.val("再次提交");
        }
    }
    //表单提交出错
    function formError(XMLHttpRequest, textStatus, errorThrown) {
        $.dialog.alert("状态：" + textStatus + "；出错提示：" + errorThrown);
        btnObj.prop("disabled", false);
        btnObj.val("再次提交");
    }
}

/*显示AJAX分页列表*/
function AjaxPageList(listDiv, pageDiv, pageSize, pageCount, sendUrl, defaultAvatar) {
    //pageIndex -页面索引初始值
    //pageSize -每页显示条数初始化
    //pageCount -取得总页数
	InitComment(0);//初始化评论数据
	$(pageDiv).pagination(pageCount, {
		callback: pageselectCallback,
		prev_text: "« 上一页",
		next_text: "下一页 »",
		items_per_page:pageSize,
		num_display_entries:3,
		current_page:0,
		num_edge_entries:5,
		link_to:"javascript:;"
	});
	
    //分页点击事件
    function pageselectCallback(page_id, jq) {
        InitComment(page_id);
    }
    //请求评论数据
    function InitComment(page_id) {                                
        page_id++;
        $.ajax({
            type: "POST",
            dataType: "json",
            url: sendUrl + "&page_size=" + pageSize + "&page_index=" + page_id,
            beforeSend: function (XMLHttpRequest) {
                $(listDiv).html('<p style="line-height:35px;">正在很努力加载，请稍候...</p>');
            },
            success: function (data) {
                //$(listDiv).html(data);
                var strHtml = '';
                for (var i in data) {
                    strHtml += '<li>';
                    strHtml += '<div class="s1">';
                    strHtml += '<div class="vip"><img src="/images/five1.png"/></div>';
                    strHtml += '<div class="pic"><img src="/images/buy_pic.png"/></div>';
                    strHtml += '<div class="tit">VIP3 BUYER</div>';
                    strHtml += '<div class="time">' + data[i].add_time + '</div>';
                    strHtml += '</div>';
                    strHtml += '<div class="s2">';
                    strHtml += '<div class="con">' + unescape(data[i].content) + '</div> ';
                    if (data[i].albums) {
                        if (data[i].albums.length > 0) {
                            for (var j in data[i].albums) {

                                strHtml += '<img width="100" height="100" src="' + data[i].albums[j].img + '"/>';
                            }
                        }
                    }
                    strHtml += '</div>';
//                    strHtml += '<div class="s3">';
//                    strHtml += '<div class="ques"><a href="#">Was this review helpful?</a></div>';
//                    strHtml += '<div class="app"><a href="#">(0)</a></div>';
//                    strHtml += '<div class="opp"><a href="#">(0)</a></div>';
//                    strHtml += '<div class="share"><a href="#">Share to facebook</a></div>';
//                    strHtml += '</div>';

                    strHtml += '</li>';
                }
                $(listDiv).html(strHtml);
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                $(listDiv).html('<p style="line-height:35px;">暂无评论，快来抢沙发吧！</p>');
            }
        });
    }
}
/*显示AJAX分页列表*/
function AjaxPageList2(listDiv, pageDiv, pageSize, pageCount, sendUrl, defaultAvatar) {
    //pageIndex -页面索引初始值
    //pageSize -每页显示条数初始化
    //pageCount -取得总页数
    InitFeedback(0); //初始化问答数据
    $(pageDiv).pagination(pageCount, {
        callback: pageselectCallback2,
        prev_text: "« 上一页",
        next_text: "下一页 »",
        items_per_page: pageSize,
        num_display_entries: 3,
        current_page: 0,
        num_edge_entries: 5,
        link_to: "javascript:;"
    });

    //分页点击事件
    function pageselectCallback2(page_id, jq) {
        InitFeedback(page_id);
    }
    //请求问答数据
    function InitFeedback(page_id) {
        page_id++;
        $.ajax({
            type: "POST",
            dataType: "json",
            url: sendUrl + "&page_size=" + pageSize + "&page_index=" + page_id,
            beforeSend: function (XMLHttpRequest) {
                $(listDiv).html('<p style="line-height:35px;">正在很努力加载，请稍候...</p>');
            },
            success: function (data) {
                //$(listDiv).html(data);
                var strHtml = '';
                for (var i in data) {
                    strHtml += '<li>';

                    strHtml += '<div class="left"><img src="../images/prod_des.jpg" /></div>';
                    strHtml += '<div class="fr">';
                    strHtml += '<div class="tit">' + data[i].title + '</div>';
                    strHtml += '<div class="time">' + data[i].add_time + '</div>';
                    strHtml += '<div class="dess">' + unescape(data[i].content) + '</div>';
                    strHtml += '<div class="dess">回复 : ' + unescape(data[i].reply_content) + '</div>';
                    strHtml += '</div>';

                    strHtml += '</li>';
                }
                $(listDiv).html(strHtml);
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                $(listDiv).html('<p style="line-height:35px;">暂无评论，快来抢沙发吧！</p>');
            }
        });
    }
}
