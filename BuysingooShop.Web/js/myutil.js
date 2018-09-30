

/**
 * 刷新图片验证码
 * 
 * @param imgId img标签Id
 * @param codeId 输入验证码的文本框Id
 */
function refreshImgCode(imgId,codeId) {
	
	document.getElementById(imgId).src="image.jsp?x=" + Math.random();
	
	document.getElementById(codeId).focus();
}

//倒计时
var totalSecond = 0, intervalSign = 0;

function countDown(second,btnId) {

	totalSecond = second;

	window.clearInterval(intervalSign);

	if (totalSecond > 0) {

		resetWord();

		intervalSign = window.setInterval(function(){resetWord(btnId);}, 1000);
	}
}

function resetWord(btnId) {

	if (totalSecond == 0) {

		$("#" + btnId).val("发送验证码");

		$("#" + btnId).attr("disabled", false);

		window.clearInterval(intervalSign);
		
	} else {

		$("#" + btnId).val("重发(" + totalSecond + ")");

		$("#" +btnId).attr("disabled", true);

		totalSecond--;
	}
}

/**
 * 发送短信验证码
 * @param phoneNumId 输入手机号的文本框Id
 * @param imgCodeId 输入图片验证码的文本框Id
 * @param btnId 触发事件的按钮Id
 * @param type 验证码类型
 */
function sendSmsCheckCode(phoneNumId,imgCodeId,btnId,type) {
	
	var phoneNum = $("#" + phoneNumId).val();
	var imgCode = $("#" + imgCodeId).val();
	
	// 验证手机号码
	if (phoneNum == null || $.trim(phoneNum) == "") {
		
		alert("请填写您的手机号码！");
		
		$("#" + phoneNumId).val('');
		
		$("#" + phoneNumId).select();
		
		return false;
	}
	
	var reg = /^\d{11}$/;
	
	if (!reg.test($.trim(phoneNum))) {
		
		alert("手机号码格式不正确，请重新输入！");
		
		$("#" + phoneNumId).val('');
		
		$("#" + phoneNumId).select();
		
		return false;
	}
	
	//验证图片验证码
	if (imgCode == null || $.trim(imgCode) == "") {
		
		alert("请填写图片校验码！");
		
		$("#" + imgCodeId).val('');
		
		$("#" + imgCodeId).select();
		
		return false;
	}
		
	if ($.trim(imgCode).length != 4) {
		
		alert("图片校验码长度为4位！");
		
		$("#" + imgCodeId).val('');
		
		$("#" + imgCodeId).select();
		
		return false;
	}
	
	var url = contextPath + "/sendSmsCheckCode";
	
	$.ajax({
		type: "POST",
		url: url,
		dataType:"json",
		data:{
			smsType:type,
			phoneNum:$.trim(phoneNum),
			imgCode:$.trim(imgCode),
			source:$("#source").val()
		},
		success: function(text) {
			
			var txt = text.result;
			
			if(txt == 'success') {
				
				alert("验证码已发出，请注意查收短信，如果没有收到，你可以在"+text.interval+"秒后要求系统重新发送，验证码有效时间"+text.validTime+"分钟！");
				
			}else if(txt=='imgCodeError'){
				
				alert("图片校验码错误，请重新输入！");
				
				$("#" + imgCodeId).val('');
				
				$("#" + imgCodeId).select();
				
				$("#" + btnId).removeAttr("disabled");
				
				if ($("img[title=刷新验证码]").length > 1) {
					
					document.getElementById($("#validaImg").attr("id")).src= contextPath + "/image.jsp?x=" + Math.random();
				
				}else {
					
					document.getElementById($("img[title=刷新验证码]").attr("id")).src= contextPath + "/image.jsp?x=" + Math.random();
				}
				
				if(type == 'dynamic_login') {
					
					$('#code_mask_div').show();
					$("#" + imgCodeId).select();
				}
				
			}else if(txt == 'wait'){
				
				alert("请等待" + text.waitTime + "秒后再发送验证码！");
				
				countDown(text.waitTime,btnId);
				
				return;
				
			}else if(txt == 'over_max_limit'){
				
				alert("对不起，您今天发送验证码的次数已超过限制次数。");
				
			}else if(txt == 'over_limit'){
				
				alert("您发送验证码过于频繁，请稍后重试。");
				
			}else if(txt == 'fail'){
				
				alert("短信发送失败，请直接与东目工作人员联系处理。");
				
			}else if(txt == 'forbid'){
				
				alert("请不要进行非法操作。");
				
			}else {
				
				alert("验证码发送失败，请稍后重试。");
			}
			
			countDown(text.interval,btnId);
			
		}, 
		error :function() {
			
			alert("验证码发送失败，系统异常，请与管理员联系！");
			
			$("#" + btnId).removeAttr("disabled");
			
			if(type == 'dynamic_login') {
				
				$("#" + imgCodeId).val('');
			}
			
			countDown(60,btnId);
		}
	});
}

/**
 * 发送邮箱验证码
 * @param emailNumId 输入邮箱的文本框Id
 * @param imgCodeId 输入图片验证码的文本框Id
 * @param btnId 触发事件的按钮Id
 * @param type 验证码类型
 */
function sendEmailCheckCode(emailNumId,imgCodeId,btnId,type) {
	
	var emailNum = $("#" + emailNumId).val();
	var imgCode = $("#" + imgCodeId).val();
	
	// 验证手机号码
	if (emailNum == null || $.trim(emailNum) == "") {
		
		alert("请填写您的邮箱！");
		
		$("#" + emailNumId).val('');
		
		$("#" + emailNumId).select();
		
		return false;
	}
	
	var reg = /\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/;
	
	if (!reg.test($.trim(emailNum))) {
		
		alert("邮箱格式不正确，请重新输入！");
		
		$("#" + emailNumId).val('');
		
		$("#" + emailNumId).select();
		
		return false;
	}
	
	//验证图片验证码
	if (imgCode == null || $.trim(imgCode) == "") {
		
		alert("请填写图片校验码！");
		
		$("#" + imgCodeId).val('');
		
		$("#" + imgCodeId).select();
		
		return false;
	}
	
	if ($.trim(imgCode).length != 4) {
		
		alert("图片校验码长度为4位！");
		
		$("#" + imgCodeId).val('');
		
		$("#" + imgCodeId).select();
		
		return false;
	}
	
	var url = contextPath + "/sendEmailCheckCode";
	
	$.ajax({
		type: "POST",
		url: url,
		dataType:"json",
		data:{
			
			emailType:type,
			email:$.trim(emailNum),
			imgCode:$.trim(imgCode),
			source:$("#source").val()
		},
		success: function(text) {
			
			var txt = text.result;
			
			if(txt == 'success') {
				
				alert("验证码已发出，请注意查收邮件，如果没有收到，你可以在"+text.interval+"秒后要求系统重新发送，验证码有效时间"+text.validTime+"分钟！");
				
			}else if(txt=='imgCodeError'){
				
				alert("图片校验码错误，请重新输入！");
				
				$("#" + imgCodeId).val('');
				
				$("#" + imgCodeId).select();
				
				$("#" + btnId).removeAttr("disabled");
				
				document.getElementById($("img[title=刷新验证码]").attr("id")).src= contextPath + "/image.jsp?x=" + Math.random();
				
				return;
				
			} else if(txt == 'wait'){
				
				alert("请等待" + text.waitTime + "秒后再发送验证码！");
				
				countDown(text.waitTime,btnId);
				
				return;
				
			} else if(txt == 'noexist'){
				
				alert("邮箱账号不存在！");
				
			} else if(txt == 'forbid'){
				
				alert("请不要进行非法操作。");
				
			}else if(txt == 'fail'){
				
				alert("邮件发送失败，请直接与东目工作人员联系处理。");
				
			}else {
				
				alert("邮件发送失败，请直接与东目工作人员联系处理。");
			}
			
			countDown(text.interval,btnId);
			// 禁用按钮
			$("#" + btnId).attr("disabled","disabled");
		}, 
		error :function() {
			
			alert("邮件发送失败，系统异常，请与管理员联系！");
			
			$("#" + btnId).removeAttr("disabled");
			
			countDown(60,btnId);
			
			// 禁用按钮
			$("#" + btnId).attr("disabled","disabled");
		}
	});
}


/**
 * 发送短信验证码
 * @param phoneNumId 输入手机号的文本框Id
 * @param imgCodeId 输入图片验证码的文本框Id
 * @param btnId 触发事件的按钮Id
 * @param type 验证码类型
 */
function sendSpecialSmsCheckCode(phoneType,imgCodeId,btnId,type) {
	
	var selVar = "input[name="+phoneType+"]:checked";
	
	if ($(selVar).length != 1) {
		
		alert("请选择需要接收验证码的手机！");
		
		return false;
	}
	
	var imgCode = $("#" + imgCodeId).val();
	
	
	//验证图片验证码
	if (imgCode == null || $.trim(imgCode) == "") {
		
		alert("请填写图片校验码！");
		
		$("#" + imgCodeId).val('');
		
		$("#" + imgCodeId).select();
		
		return false;
	}
	
	if ($.trim(imgCode).length != 4) {
		
		alert("图片校验码长度为4位！");
		
		$("#" + imgCodeId).val('');
		
		$("#" + imgCodeId).select();
		
		return false;
	}
	
	var url = contextPath + "/sendSpecialSmsCheckCode";
	
	$.ajax({
		type: "POST",
		url: url,
		dataType:"json",
		data:{
			smsType:type,
			phoneType:$(selVar).val(),
			imgCode:$.trim(imgCode)
		},
		success: function(text) {
			
			var txt = text.result;
			
			if(txt == 'success') {
				
				alert("验证码已发出，请注意查收短信，如果没有收到，你可以在"+text.interval+"秒后要求系统重新发送，验证码有效时间"+text.validTime+"分钟！");
				
			}else if(txt=='imgCodeError'){
				
				alert("图片校验码错误，请重新输入！");
				
				$("#" + imgCodeId).val('');
				
				$("#" + imgCodeId).select();
				
				$("#" + btnId).removeAttr("disabled");
				
				document.getElementById($("img[title=刷新验证码]").attr("id")).src= contextPath + "/image.jsp?x=" + Math.random();
				
				if(type == 'dynamic_login') {
					
					$('#code_mask_div').show();
					$("#" + imgCodeId).select();
				}
				
			}else if(txt == 'wait'){
				
				alert("请等待" + text.waitTime + "秒后再发送验证码！");
				
				countDown(text.waitTime,btnId);
				
				return;
				
			}else if(txt == 'over_max_limit'){
				
				alert("对不起，您今天发送验证码的次数已超过限制次数。");
				
			}else if(txt == 'over_limit'){
				
				alert("您发送验证码过于频繁，请稍后重试。");
				
			}else if(txt == 'fail'){
				
				alert("短信发送失败，请直接与东目工作人员联系处理。");
				
			}else if(txt == 'forbid'){
				
				alert("请不要进行非法操作。");
				
			}else {
				
				alert("验证码发送失败，请稍后重试。");
			}
			
			if (text.interval == null) {
				
				countDown(60,btnId);
				
			}else {
				countDown(text.interval,btnId);
			}
			
			if(type == 'dynamic_login') {
				
				$("#" + imgCodeId).val('');
			}
		}, 
		error :function() {
			
			alert("验证码发送失败，系统异常，请与管理员联系！");
			
			$("#" + btnId).removeAttr("disabled");
			
			if(type == 'dynamic_login') {
				
				$("#" + imgCodeId).val('');
			}
			
			countDown(60,btnId);
		}
	});
}

/**
 * 发送邮箱验证码
 * @param imgCodeId 输入图片验证码的文本框Id
 * @param btnId 触发事件的按钮Id
 * @param type 验证码类型
 */
function sendSpecialEmailCheckCode(imgCodeId,btnId,type) {
	
	var imgCode = $("#" + imgCodeId).val();
	
	//验证图片验证码
	if (imgCode == null || $.trim(imgCode) == "") {
		
		alert("请填写图片校验码！");
		
		$("#" + imgCodeId).val('');
		
		$("#" + imgCodeId).select();
		
		return false;
	}
	
	if ($.trim(imgCode).length != 4) {
		
		alert("图片校验码长度为4位！");
		
		$("#" + imgCodeId).val('');
		
		$("#" + imgCodeId).select();
		
		return false;
	}
	
	var url = contextPath + "/sendSpecialEmailCheckCode";
	
	$.ajax({
		type: "POST",
		url: url,
		dataType:"json",
		data:{
			
			emailType:type,
			imgCode:$.trim(imgCode),
			source:$("#source").val()
		},
		success: function(text) {
			
			var txt = text.result;
			
			if(txt == 'success') {
				
				alert("验证码已发出，请注意查收邮件，如果没有收到，你可以在"+text.interval+"秒后要求系统重新发送，验证码有效时间"+text.validTime+"分钟！");
				
			}else if(txt=='imgCodeError'){
				
				alert("图片校验码错误，请重新输入！");
				
				$("#" + imgCodeId).val('');
				
				$("#" + imgCodeId).select();
				
				$("#" + btnId).removeAttr("disabled");
				
				document.getElementById($("img[title=刷新验证码]").attr("id")).src= contextPath + "/image.jsp?x=" + Math.random();
				
				return;
				
			}else if(txt == 'forbid'){
				
				alert("请不要进行非法操作。");
				
			}else if(txt == 'wait'){
				
				alert("请等待" + text.waitTime + "秒后再发送验证码！");
				
				countDown(text.waitTime,btnId);
				
				return;
				
			}else if(txt == 'fail'){
				
				alert("邮件发送失败，请直接与东目工作人员联系处理。");
				
			}else {
				
				alert("邮件发送失败，请直接与东目工作人员联系处理。");
			}
			
			if (text.interval == null) {
				
				countDown(60,btnId);
				
			}else {
				countDown(text.interval,btnId);
			}
			// 禁用按钮
			$("#" + btnId).attr("disabled","disabled");
		}, 
		error :function() {
			
			alert("邮件发送失败，系统异常，请与管理员联系！");
			
			$("#" + btnId).removeAttr("disabled");
			
			countDown(60,btnId);
			
			// 禁用按钮
			$("#" + btnId).attr("disabled","disabled");
		}
	});
}


/**
 * 控制输入框的提示文字
 * @param inputId
 * @param promotText
 */
function inputTextPrompt(inputId , promotText){
	
	// 文本框提示效果
	$("#"+inputId).on('blur', function() {

		if ($.trim($(this).val()) == '') {

			$(this).val(promotText);
			$(this).css("color","#908E89");
		}

	});

	$("#"+inputId).on('focus', function() {

		if ($.trim($(this).val()) == promotText) {

			$(this).val('');
			$(this).css("color","#666");
		}

	});

	$("#"+inputId).blur();
	
}

/**
 * 控制密码输入框的提示文字：html的要求是对应的密码框要默认隐藏，额外这个密码输入框要一个对于的文本输入框。
 * @param passwordInputId:密码框的id
 * @param textInputId：对应文本框的id
 */
function inputPassWordTextPrompt(passwordInputId,textInputId){
		
	$("#"+textInputId).on('focus', function() {

		$("#"+textInputId).hide();
		$("#"+passwordInputId).show();
		$("#"+passwordInputId).focus();

	});
	// 文本框提示效果
	$("#"+passwordInputId).on('blur', function() {

		if($("#"+passwordInputId).val() == ''){
			
			$("#"+passwordInputId).hide();
			$("#"+textInputId).show();
		}	

	});
}

/******************************************************/
/***********************手机端APP************************/
/******************************************************/

/**
 * 发送短信验证码
 * @param phoneNumId 输入手机号的文本框Id
 * @param imgCodeId 输入图片验证码的文本框Id
 * @param btnId 触发事件的按钮Id
 * @param type 验证码类型
 */
function sendSmsCheckCodeApp(phoneNumId,imgCodeId,btnId,type) {
	
	var phoneNum = $("#" + phoneNumId).val();
	var imgCode = $("#" + imgCodeId).val();
	
	// 验证手机号码
	if (phoneNum == null || $.trim(phoneNum) == "") {
		
		showToast("请填写您的手机号码！");
		
		$("#" + phoneNumId).val('');
		
		return false;
	}
	
	var reg = /^\d{11}$/;
	
	if (!reg.test($.trim(phoneNum))) {
		
		showToast("手机号码格式不正确，请重新输入！");
		
		$("#" + phoneNumId).val('');
		
		return false;
	}
	
	//验证图片验证码
	if (imgCode == null || $.trim(imgCode) == "") {
		
		showToast("请填写图片校验码！");
		
		$("#" + imgCodeId).val('');
		
		return false;
	}
		
	if ($.trim(imgCode).length != 4) {
		
		showToast("图片校验码长度为4位！");
		
		$("#" + imgCodeId).val('');
		
		return false;
	}
	
	var url = contextPath + "/sendSmsCheckCode";
	
	$.ajax({
		type: "POST",
		url: url,
		dataType:"json",
		data:{
			smsType:type,
			phoneNum:$.trim(phoneNum),
			imgCode:$.trim(imgCode),
			source:$("#source").val()
		},
		success: function(text) {
			
			var txt = text.result;
			
			if(txt == 'success') {
				
				showToast("验证码发送成功！");
				
			}else if(txt=='imgCodeError'){
				
				showToast("图片校验码错误，请重新输入！");
				
				$("#" + imgCodeId).val('');
				
				$("#" + btnId).removeAttr("disabled");
				
				document.getElementById($("img[title=刷新验证码]").attr("id")).src= contextPath + "/image.jsp?x=" + Math.random();
				
				return;
				
			}else if(txt == 'wait'){
				
				showToast("请等待" + text.waitTime + "秒后再发送验证码！");
				
				countDown(text.waitTime,btnId);
				
				return;
				
			}else if(txt == 'over_max_limit'){
				
				showToast("对不起，您今天发送验证码的次数已超过限制次数。");
				
			}else if(txt == 'over_limit'){
				
				showToast("您发送验证码过于频繁，请稍后重试。");
				
			}else if(txt == 'fail'){
				
				showToast("短信发送失败，请直接与东目工作人员联系处理。");
				
			}else if(txt == 'forbid'){
				
				showToast("请不要进行非法操作。");
				
			}else {
				
				showToast("验证码发送失败，请稍后重试。");
			}
			
			countDown(text.interval,btnId);
			
		}, 
		error :function() {
			
			showToast("验证码发送失败，系统异常，请与管理员联系！");
			
			$("#" + btnId).removeAttr("disabled");
			
			if(type == 'dynamic_login') {
				
				$("#" + imgCodeId).val('');
			}
			
			countDown(60,btnId);
		}
	});
}

/**
 * 发送短信验证码
 * @param phoneNumId 输入手机号的文本框Id
 * @param imgCodeId 输入图片验证码的文本框Id
 * @param btnId 触发事件的按钮Id
 * @param type 验证码类型
 */
function sendSpecialSmsCheckCodeApp(phoneType,imgCodeId,btnId,type) {
	
	var selVar = "input[name="+phoneType+"]:checked";
	
	if ($(selVar).length != 1) {
		
		showToast("请选择需要接收验证码的手机！");
		
		return false;
	}
	
	var imgCode = $("#" + imgCodeId).val();
	
	
	//验证图片验证码
	if (imgCode == null || $.trim(imgCode) == "") {
		
		showToast("请填写图片校验码！");
		
		$("#" + imgCodeId).val('');
		
		return false;
	}
	
	if ($.trim(imgCode).length != 4) {
		
		showToast("图片校验码长度为4位！");
		
		$("#" + imgCodeId).val('');
		
		return false;
	}
	
	var url = contextPath + "/sendSpecialSmsCheckCode";
	
	$.ajax({
		type: "POST",
		url: url,
		dataType:"json",
		data:{
			smsType:type,
			phoneType:$(selVar).val(),
			imgCode:$.trim(imgCode)
		},
		success: function(text) {
			
			var txt = text.result;
			
			if(txt == 'success') {
				
				showToast("验证码发送成功！");
				
			}else if(txt=='imgCodeError'){
				
				showToast("图片校验码错误，请重新输入！");
				
				$("#" + imgCodeId).val('');
				
				$("#" + btnId).removeAttr("disabled");
				
				document.getElementById($("img[title=刷新验证码]").attr("id")).src= contextPath + "/image.jsp?x=" + Math.random();
				
				return;
				
			}else if(txt == 'wait'){
				
				showToast("请等待" + text.waitTime + "秒后再发送验证码！");
				
				countDown(text.waitTime,btnId);
				
				return;
				
			}else if(txt == 'over_max_limit'){
				
				showToast("对不起，您今天发送验证码的次数已超过限制次数。");
				
			}else if(txt == 'over_limit'){
				
				showToast("您发送验证码过于频繁，请稍后重试。");
				
			}else if(txt == 'fail'){
				
				showToast("短信发送失败，请直接与东目工作人员联系处理。");
				
			}else if(txt == 'forbid'){
				
				showToast("请不要进行非法操作。");
				
			}else {
				
				showToast("验证码发送失败，请稍后重试。");
			}
			
			if (text.interval == null) {
				
				countDown(60,btnId);
				
			}else {
				countDown(text.interval,btnId);
			}
			
		}, 
		error :function() {
			
			showToast("验证码发送失败，系统异常，请与管理员联系！");
			
			$("#" + btnId).removeAttr("disabled");
			
			if(type == 'dynamic_login') {
				
				$("#" + imgCodeId).val('');
			}
			
			countDown(60,btnId);
		}
	});
}

/**
 * 发送邮箱验证码
 * @param emailNumId 输入邮箱的文本框Id
 * @param imgCodeId 输入图片验证码的文本框Id
 * @param btnId 触发事件的按钮Id
 * @param type 验证码类型
 */
function sendEmailCheckCodeApp(emailNumId,imgCodeId,btnId,type) {
	
	var emailNum = $("#" + emailNumId).val();
	var imgCode = $("#" + imgCodeId).val();
	
	// 验证手机号码
	if (emailNum == null || $.trim(emailNum) == "") {
		
		showToast("请填写您的邮箱！");
		
		$("#" + emailNumId).val('');
		
		return false;
	}
	
	var reg = /\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/;
	
	if (!reg.test($.trim(emailNum))) {
		
		showToast("邮箱格式不正确，请重新输入！");
		
		$("#" + emailNumId).val('');
		
		return false;
	}
	
	//验证图片验证码
	if (imgCode == null || $.trim(imgCode) == "") {
		
		showToast("请填写图片校验码！");
		
		$("#" + imgCodeId).val('');
		
		return false;
	}
	
	if ($.trim(imgCode).length != 4) {
		
		showToast("图片校验码长度为4位！");
		
		$("#" + imgCodeId).val('');
		
		return false;
	}
	
	var url = contextPath + "/sendEmailCheckCode";
	
	$.ajax({
		type: "POST",
		url: url,
		dataType:"json",
		data:{
			
			emailType:type,
			email:$.trim(emailNum),
			imgCode:$.trim(imgCode),
			source:$("#source").val()
		},
		success: function(text) {
			
			var txt = text.result;
			
			if(txt == 'success') {
				
				showToast("验证码发送成功！");
				
			}else if(txt=='imgCodeError'){
				
				showToast("图片校验码错误，请重新输入！");
				
				$("#" + imgCodeId).val('');
				
				$("#" + btnId).removeAttr("disabled");
				
				document.getElementById($("img[title=刷新验证码]").attr("id")).src= contextPath + "/image.jsp?x=" + Math.random();
				
				return;
				
			} else if(txt == 'wait'){
				
				showToast("请等待" + text.waitTime + "秒后再发送验证码！");
				
				countDown(text.waitTime,btnId);
				
				return;
				
			} else if(txt == 'noexist'){
				
				showToast("邮箱账号不存在！");
				
			} else if(txt == 'forbid'){
				
				showToast("请不要进行非法操作。");
				
			}else if(txt == 'fail'){
				
			    showToast("邮件发送失败，请直接与东目工作人员联系处理。");
				
			}else {
				
			    showToast("邮件发送失败，请直接与东目工作人员联系处理。");
			}
			
			countDown(text.interval,btnId);
			// 禁用按钮
			$("#" + btnId).attr("disabled","disabled");
		}, 
		error :function() {
			
			showToast("邮件发送失败，系统异常，请与管理员联系！");
			
			$("#" + btnId).removeAttr("disabled");
			
			countDown(60,btnId);
			
			// 禁用按钮
			$("#" + btnId).attr("disabled","disabled");
		}
	});
}