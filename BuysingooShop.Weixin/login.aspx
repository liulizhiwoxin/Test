<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="BuysingooShop.Weixin.login" %>

<!DOCTYPE html>

<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <title>门川家居</title>
    <link rel="stylesheet" href="css/common.css?v=20180410">
    <link rel="stylesheet" href="css/login.css?v=20180410">

    <script src="js/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script src="js/SingGooJS.js"></script>

    <script type="text/javascript">

        $(function () {
            var hmobile = $("#hMobile").val();
            var hvip = $("#hVip").val();
            
            //已保存openid 且 已是VIP会员
            if (hmobile != "" && hvip == "1") {
                //直接跳到个人中心去
                $.ajax({
                    type: 'GET',
                    url: 'Service/AdminServiceHandler.ashx',
                    data: 'param=userLogin&phone=' + hmobile,
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
                                //window.location.href = "http://" + window.location.host + "/WeixinPay/JSAPI_PAY.aspx?userid=" + userid + "&user_name=" + user_name;
                                window.location.href = "http://" + window.location.host + "/lanzuan.aspx";
                            }

                        } else {
                            alert('登录失败！');
                        }
                    }
                });                
            } else {
                window.location.href = "http://" + window.location.host + "/lanzuan.aspx";
            }

        });


        var preId = SingGooJS.GetQueryString("id");

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

                var mobile = $("#mobile").val();
                if (mobile == "") {
                    alert("手机号不能为空！");
                    $("#mobile").focus();
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
                        data: 'param=userLogin&phone=' + mobile,
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
                                    //window.location.href = "http://" + window.location.host + "/WeixinPay/JSAPI_PAY.aspx?userid=" + userid + "&user_name=" + user_name;
                                    window.location.href = "http://" + window.location.host + "/lanzuan.aspx";
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


    <div class="loginBox login">
        <div class="userCard"></div>
        <div class="line">
            <input type="text" placeholder="输入手机号" id="mobile"></div>
        <div class="line">
            <input type="text" id="code" class="w60" placeholder="输入6位数验证码">
            <a href="javascript:" id="aToCode" class="getAuthCode fr" onclick="SendMsgCode(this)" isable="0">获取验证码</a>
        </div>
        <button onclick="setlogin()">登录</button>
    </div>

    <input id="hMobile" runat="server" type="hidden" value="" />
    <input id="hVip" runat="server" type="hidden" value="0" />
</body>
</html>
