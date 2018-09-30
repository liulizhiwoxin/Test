/*
 * 微信分享6.0.2新版 API
 * vincent 2018-05-10
 */

//***********LOAD SDK****************

var LOADWXJS = document.createElement("script");
LOADWXJS.setAttribute("src","http://res.wx.qq.com/open/js/jweixin-1.0.0.js");
document.head.appendChild(LOADWXJS);

//***********SDK配置*******************

var WXJSSDK = WXJSSDK || {};
var WXAPI = WXAPI || {};

WXJSSDK.config = {
    debug: false, // 调试
    appId: 'wx7ec3b58c1c728301', // 必填，公众号的唯一标识,如果提示签名无效需检查此id是否和当前绑定的js安全域名同一个公众号
    timestamp: '', // 必填，生成签名的时间戳
    nonceStr: '', // 必填，生成签名的随机串
    signature: '',// 签名
    jsApiList: ["onMenuShareTimeline", "onMenuShareAppMessage", "onMenuShareQQ", "onMenuShareWeibo"] // 必填，需要使用的JS接口列表
};

WXJSSDK.signature = function (callback){
	$.ajax({
	    url: "Ajax/Index.ashx?param=WXJSSDK_config",
		type: "POST",
		data: { timestamp: WXJSSDK.config.timestamp, nonceStr: WXJSSDK.config.nonceStr, url: window.location.href.split('#')[0] },
		dataType: "JSON",
		success: function (data) {
		    
		    if (data.success == "1") {
		        debugger;
		        WXJSSDK.config.appId = data.appId;
		        WXJSSDK.config.timestamp = data.timestamp;
		        WXJSSDK.config.nonceStr = data.nonceStr;
		        WXJSSDK.config.signature = data.signature;
		    } else {
		        alert(data.msg);
		    }
			callback && callback();
		}
	});
};

// 加载微信开发工具包 
// callbacks： 是一个包含了各种回调的对象
WXJSSDK.load = function (callbacks){
	
	//SDK加载完成后
	LOADWXJS.onload = function(){
		//注入配置信息
		WXJSSDK.signature(function() {
			wx.config(WXJSSDK.config);
		});
		
		//config信息验证后会执行ready方法
		wx.ready(function(){
			callbacks.ready && callbacks.ready();
		});
		// 接口调用成功时执行的回调函数
		wx.success(function(res){
			callbacks.success && callbacks.success();
		});
		// 接口调用失败时执行的回调函数
		wx.fail(function(res){
			$.vAlert ? $.vAlert(res.errMsg) : alert(res.errMsg);
			callbacks.fail && callbacks.fail();
		});
		// 接口调用完成时执行的回调函数，无论成功或失败都会执行
		wx.complete(function(res){
			callbacks.complete && callbacks.complete();
		});
		// 用户点击取消时的回调函数，仅部分有用户取消操作的api才会用到
		wx.cancel(function(res){
			callbacks.cancel && callbacks.cancel();
		});
		// 监听Menu中的按钮点击时触发的方法，该方法仅支持Menu中的相关接口
		wx.trigger(function(res){
			callbacks.trigger && callbacks.trigger();
		});
		// config信息验证失败会执行error函数
		wx.error(function(res){
			$.flytip ? $.flytip(res.errMsg) : alert(res.errMsg);
			callbacks.error && callbacks.error();
		});
	};
	
};


//*************API定义*****************

//==============================
// 微信分享接口
//==============================
WXAPI.shares = function (params){
	wx.showOptionMenu();
	
	// 分享到朋友圈
	wx.onMenuShareTimeline({        
	    title: params.title, // 分享标题
	    link: params.shareCircle ? params.shareCircle : params.link, // 分享链接
	    imgUrl: params.imgUrl, // 分享图标
	    success: function () {
	    },
	    cancel: function () {
	    }
	});
	
	// 分享给朋友
	wx.onMenuShareAppMessage({
	    title: params.title, // 分享标题
	    desc: params.desc, // 分享描述
	    link: params.shareFriend ? params.shareFriend : params.link, // 分享链接
	    imgUrl: params.imgUrl, // 分享图标
	    type: params.type ? params.type : "link", // 分享类型,music、video或link，不填默认为link
	    dataUrl: params.dataUrl ? params.dataUrl : "", // 如果type是music或video，则要提供数据链接，默认为空
	    success: function () { 
	    },
	    cancel: function () { 
	    }
	});
	
	// 分享到QQ
	wx.onMenuShareQQ({
	    title: params.title, // 分享标题
	    desc: params.desc, // 分享描述
	    link: params.shareQQ ? params.shareQQ : params.link, // 分享链接
	    imgUrl: params.imgUrl, // 分享图标
	    success: function () { 
	    },
	    cancel: function () { 
	    }
	});
	
	// 分享到腾讯微博
	wx.onMenuShareWeibo({
	    title: params.title, // 分享标题
	    desc: params.desc, // 分享描述
	    link: params.shareWeibo ? params.shareWeibo : params.link, // 分享链接
	    imgUrl: params.imgUrl, // 分享图标
	    success: function () { 
	    },
	    cancel: function () { 
	    }
	});
};
