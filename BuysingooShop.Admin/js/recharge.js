var seedRefundPath = "seedApply.action?callback=?";

function cleanMes(){
	$(".msg-error").html("");
}

function checkMoney(){
    var balance=$("#balance").val();
    var isWhiteVip =   $("#isWhiteVip").val();
    if(balance>=50000 &&  "true" != isWhiteVip){
        $(".msg-error").html("您的帐户余额已经超过了￥50000元,无法充值");
        return;
    }else if (balance>=1000000 && "true" == isWhiteVip){
        $(".msg-error").html("您的帐户余额已经超过了￥1000000元,无法充值");
        return;
    }
    var account = $("#txtAccount").val();
//	var patrn=/^(1000|\d{2,3})$/;
//	if(!patrn.exec(account)){
//		prompt.error("只能填写大于等于10，小于等于1000的整数金额");
//		return;
//	}

    var patrnVip=/^(\d{2,7})$/;
    var patrn=/^(\d{2,5})$/;
    if("true" == isWhiteVip){
        if(!patrnVip.exec(account)){
            $(".msg-error").html("只能填写大于等于10，小于等于1000000的整数金额");
            return;
        }
        else{
            if(parseInt(account)>1000000){
                $(".msg-error").html("只能填写大于等于10，小于等于1000000的整数金额");
                return;
            }
        }
    }else{
        if(!patrn.exec(account)){
            $(".msg-error").html("只能填写大于等于10，小于等于50000的整数金额");
            return;
        }
        else{
            if(parseInt(account)>50000){
                $(".msg-error").html("只能填写大于等于10，小于等于50000的整数金额");
                return;
            }
        }
    }
}

function DoChonge() {
	var mes=$(".msg-error").html();
	if(mes==""){
		submitreturn();
	}
    
}

function submitreturn() {
    jQuery.ajax({
        type: "GET",
        url: "http://safe.jd.com/user/paymentpassword/getUserSafeInfo.action?callback=?",
        data: "",
        dataType: "jsonp",
        timeout: 6000,
        success: function(result) {
            if (result.usedFlag == 1 || result.dynamicUsedFlag == 1) {
                chong();
                return true;
            }
            else {
                $.jdThickBox({
                    type: "text",
                    title: "完善账户安全设置提醒",
                    width: 500,
                    height: 150,
                    source: "<div class=\"m jdsafe\"><div class=\"mc\"><s class=\"icon-warn03\"></s><div class=\"fore\"><h3 class=\"ftx-04\">尊敬的用户您好，为了保障您的账户资产安全，余额充值前，请您开启支付密码。</h3><p class=\"ftx-03\">支付密码开启后，使用账户中余额、礼品卡、优惠券时，需要输入支付密码，为您的账户资金加把锁。</p></div><div class=\"btns\"><a href=\"http://safe.jd.com/user/paymentpassword/safetyCenter.action\" class=\"btn btn-7\"><s></s>立即开启支付密码</a></div></div></div>",
                    _autoReposi: true
                });
                return false;
            }
        },
        Error: function() {
            chong();
        }
    });
}

function chong()
{		
	var account = $("#txtAccount").val(); 	
    var balance=$("#balance").val();
    var isWhiteVip =   $("#isWhiteVip").val();
    if(balance>=50000 && isWhiteVip != "true"){
    	prompt.error("您的帐户余额已经超过了￥50000元,无法充值");
		return;
	}else if(balance>=1000000 && isWhiteVip == "true"){
        prompt.error("您的帐户余额已经超过了￥1000000元,无法充值");
        return;
    }
//	var patrn=/^(1000|\d{2,3})$/;
//	if(!patrn.exec(account)){
//		prompt.error("只能填写大于等于10，小于等于1000的整数金额");
//		return; 
//	} 
	
	var patrn=/^(\d{2,5})$/;
    var patrnVip=/^(\d{2,7})$/;
    if("true" == isWhiteVip){
        if(!patrnVip.exec(account)){
            $(".msg-error").html("只能填写大于等于10，小于等于1000000的整数金额");
            return;
        }
        else{
            if(parseInt(account)>1000000){
                $(".msg-error").html("只能填写大于等于10，小于等于1000000的整数金额");
                return;
            }
        }
    }else{
        if(!patrn.exec(account)){
            $(".msg-error").html("只能填写大于等于10，小于等于50000的整数金额");
            return;
        }
        else{
            if(parseInt(account)>50000){
                $(".msg-error").html("只能填写大于等于10，小于等于50000的整数金额");
                return;
            }
        }
    }


    var data = {
			balance : account
		};
    $.ajax( {
		type : "GET",
		url : seedRefundPath,
		data : data,
		contentType : "application/x-www-form-urlencoded; charset=UTF-8",
		dataType : "jsonp",
		cache : false,
		success : function(json) {
			if(json.flag == "true" || json.flag == true) {
				window.location.href=json.message;
			}else{
				prompt.error(json.message);
			}
		},
		error : function() {
			prompt.error("系统异常，请稍后重试.");
		}
	});
}


var prompt = (function() {
	return {
		error : function(info) {
			$.jdThickBox( {
						type : "text",
						title : "无法充值",
						width : 320,
						height : 110,
						source : "<div class=\"m over-box\"><div class=\"mc\"><s class=\"icon-warn02\"></s><div class=\"fore\"><h3 class=\"ftx-04\">"
								+ info
								+ "</h3></div></div><div class=\"btns btns01\"><a href=\"#none\" class=\"btn btn-11\" onclick=\"jdThickBoxclose()\"><s></s>关闭</a></div></div>",
						_autoReposi : true
					});
		}		
	};
})();