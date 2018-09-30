<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login_reg.aspx.cs" Inherits="BuysingooShop.Weixin.login_reg" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <title>门川家居</title>
    <link rel="stylesheet" href="css/common.css?v=20180410">
    <link rel="stylesheet" href="css/login.css?v=20180410">

    <script src="js/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script src="js/SingGooJS.js"></script>


    <script type="text/javascript">

        $(function () {

            var user = localStorage.getItem("userinfo");

            if (user == null) {
            } else {
                var jsonobj = JSON.parse(user);
                user_id = jsonobj._id;

                //操作用户数据
                $("#name").val(jsonobj._real_name);
                $("#mobile").val(jsonobj._user_name);
                $("#address").val(jsonobj._address);
            }

        });


        var myReg = /^(((13[0-9]{1})|(14[0-9]{1})|(15[0-9]{1})|(16[0-9]{1})|(17[0-9]{1})|(18[0-9]{1}))+\d{8})$/;
        //发送验证码
        function SendMsgCode(obj) {

            if ($("#aToCode").attr("isable") == "0") {
                var mobile = $("#mobile").val();
                if (mobile == "") {
                    alert("手机号不能为空！");
                    $("#mobile").focus();
                    return;
                }
                if (myReg.test(mobile) == false) {
                    alert("手机号格式不正确！");
                    $("#mobile").focus();
                    return;
                }

                $("#aToCode").attr("isable", "1");  //防止重复提交
                $.ajax({
                    type: "get",
                    url: "Service/AdminServiceHandler.ashx?param=GetMobileMsgcode",
                    data: "mobile=" + mobile,
                    timeout: 30000,
                    dataType: "json",
                    success: function (result) {
                        $("#aToCode").attr("isable", "0");  //防止重复提交

                        if (result.status == "y") {
                            localStorage.setItem("msg_code", result.info);
                            time(obj);
                            //alert('验证码已发送至您输入的手机号！有效期5分钟！');
                        } else {
                            alert('验证码获取失败,或该手机号已注册！');
                        }
                    }
                });
            } else {
                //alert("请稍后！");
            }
        }

        //验证码倒计时
        var wait = 300;
        function time(o) {
            if (wait == 0) {
                //                $(o).removeAttribute("disabled");
                $(o).html("获取验证码");
                $("#aToCode").attr("isable", "0");
                wait = 300;
            } else {
                //                $(o).setAttribute("disabled", true);
                $(o).html(wait + "秒");
                wait--;
                setTimeout(function () {
                    time(o);
                },
            1000);
            }
        }

        var isPost = 0; //1正在提交

        //登录
        function setlogin() {

            if (isPost == 0) {

                var msgcode = $("#code").val();                

                var name = $("#name").val();
                if (name == "") {
                    alert("姓名不能为空！");
                    $("#name").focus();
                    return;
                }

                var mobile = $("#mobile").val();
                if (mobile == "") {
                    alert("手机号不能为空！");
                    $("#mobile").focus();
                    return;
                }

                var address = $("#address").val();
                if (address == "") {
                    alert("址址不能为空！");
                    $("#address").focus();
                    return;
                }

                if (msgcode != localStorage.getItem("msg_code")) {
                    alert("验证码输入错误！");
                    $("#code").focus();
                    return;
                } else {
                    isPost == 1;

                    $.ajax({
                        type: 'GET',
                        url: 'Service/AdminServiceHandler.ashx',
                        data: 'param=userLogin&phone=' + mobile + "&name=" + escape(name) + "&address=" + escape(address),
                        timeout: '120000',
                        dateType: 'json',
                        error: function (eee) {
                            //alert('系统繁忙,请稍候后再试！');
                        }, success: function (json) {
                            isPost = 0;

                            if (json != 0) {
                                localStorage.removeItem("userinfo");         //清空storage
                                localStorage.setItem("userinfo", json);

                                //判断是否为VIP会员，是则进入个人中心，否 则跳去支付页面
                                var jsonobj = JSON.parse(json);
                                var isBuwei = jsonobj._isBuwei; //0未付费 1已付费
                                var user_name = jsonobj._user_name;
                                var userid = jsonobj._id;

                                if (isBuwei == "1") {
                                    window.location.href = "http://" + window.location.host + "/index.aspx?userid=" + userid;
                                } else {
                                    window.location.href = "http://" + window.location.host + "/WeixinPay/JSAPI_PAY.aspx?userid=" + userid + "&user_name=" + user_name;                                    
                                }

                            } else {
                                alert('登录失败！');
                            }
                        }
                    });

                }
            } else {
                //alert("登录中...");
            }
        }

    </script>
</head>
<body>
    <div id="login_reg">
	<div class="lipin"><img src="images/lipin.png"/></div>
	<div class="minzi"><span><%--赠天猫138元掌心小恶魔充电宝</span><br/>（5200毫安）--%></div>
	<div class="loginBox2">
        <div class="line"><input type="text" id="name" placeholder="输入姓名"></div>
		<div class="line"><input type="text" id="mobile" placeholder="输入手机号"></div>
		<div class="line"><input type="text" id="address" placeholder="邮寄小恶魔地址"></div>
		<div class="line">
			<input type="text" class="w60" id="code" placeholder="输入6位数验证码">
			<a href="javascript:" class="getAuthCode fr" id="aToCode" onclick="SendMsgCode(this)" isable="0">获取验证码</a>
		</div>
		<button onclick="setlogin()">88元获取资源领取小恶魔</button>
	</div>
	<div class="lipin2"><img src="images/login-logo.png"/></div>
</div>



    <%--<div class="loginBox login">
        <div class="userCard"></div>
        <div class="line">
            <input type="text" placeholder="输入手机号" id="mobile"></div>
        <div class="line">
            <input type="text" id="code" class="w60" placeholder="输入6位数验证码">
            <a href="javascript:" id="aToCode" class="getAuthCode fr" onclick="SendMsgCode(this)" isable="0">获取验证码</a>
        </div>
        <button onclick="setlogin()">登录</button>
    </div>--%>

</body>
</html>
