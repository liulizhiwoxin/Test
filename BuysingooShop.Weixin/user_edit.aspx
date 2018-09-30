<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user_edit.aspx.cs" Inherits="BuysingooShop.Weixin.user_edit" %>

<!DOCTYPE html>

<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <title>门川家居</title>
    <link rel="stylesheet" href="css/common.css">
    <link rel="stylesheet" href="css/userData.css">

    <script src="js/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script src="js/SingGooJS.js"></script>

    <script type="text/javascript">

        var user_id = 0;//用户id
        var user_name = "";

        $(function () {
            
            var user = localStorage.getItem("userinfo");

            if (user == null) {
                alert("用户尚未登录，请先登录！");
                window.location.href = "http://" + window.location.host + "/login.aspx";
                return;
            } else {
                var jsonobj = JSON.parse(user);
                user_id = jsonobj._id;
                user_name = jsonobj._user_name;
            }
                
            var address = jsonobj._address;
            var mobile = jsonobj._mobile;
            var real_name = jsonobj._real_name;

            var sex = jsonobj._sex;
            var avatar = jsonobj._avatar;

            $("#Text_Realname").val(real_name);
            $("#Text_Mobile").val(mobile);
            
            if (sex == "男") {
                $("#spanSex1").attr("class", "optItem active");
            } else if (sex == "女") {
                $("#spanSex2").attr("class", "optItem active");
            }

            if (avatar == "经销商") {
                $("#spanAvatar1").attr("class", "optItem active");
            } else if (avatar == "设计师") {
                $("#spanAvatar2").attr("class", "optItem active");
            } else if (avatar == "其它") {
                $("#spanAvatar3").attr("class", "optItem active");
            }

            $("#Text_Address").val(address);
           

        });

        var isPost = 0; //1正在提交

        //提交修改
        function updateOK() {

            if (isPost == 0) {

                var real_name = $("#Text_Realname").val();                
                if (real_name == "") {
                    alert("姓名不能为空！");
                    $("#Text_Realname").focus();
                    return;
                }

                var mobile = $("#Text_Mobile").val();
                if (mobile == "") {
                    alert("电话不能为空！");
                    $("#Text_Mobile").focus();
                    return;
                }

                var sex = "";
                if ($("#spanSex1").attr("class") == "optItem active") {
                    sex = "男";
                }
                if ($("#spanSex2").attr("class") == "optItem active") {
                    sex = "女";
                }

                var avatar = "";
                if ($("#spanAvatar1").attr("class") == "optItem active") {
                    avatar = "经销商";
                }
                if ($("#spanAvatar2").attr("class") == "optItem active") {
                    avatar = "设计师";
                }
                if ($("#spanAvatar3").attr("class") == "optItem active") {
                    avatar = "其它";
                }

                var address = $("#Text_Address").val();
                if (address == "") {
                    alert("详细地址不能为空！");
                    $("#Text_Address").focus();
                    return;
                }

                isPost == 1;

                $.ajax({
                    type: 'GET',
                    url: 'Service/AdminServiceHandler.ashx',
                    data: 'param=updateUser&userid=' + user_id + '&user_name=' + user_name + '&real_name=' + real_name + '&mobile=' + mobile + '&sex=' + sex + '&avatar=' + avatar + '&address=' + address,
                    timeout: '120000',
                    dateType: 'json',
                    error: function (eee) {
                        //alert('系统繁忙,请稍候后再试！');
                    }, success: function (json) {
                        isPost = 0;

                        if (json != 0) {
                            alert('资料修改成功！');
                            localStorage.removeItem("userinfo");        //清空storage
                            localStorage.setItem("userinfo", json);     //设置一个键值
                            
                        } else {
                            alert('资料修改失败！');
                        }
                    }
                });


            } else {
                //alert("修改中...");
            }

        }

        
       
    </script>

</head>
<body>
<div class="dataBox">
    <div class="card">完善个人资料</div>
    <div class="line">
        <span class="fl">姓名：</span>
        <input type="text" id="Text_Realname" placeholder="">
    </div>
    <div class="line">
        <span class="fl">电话：</span>
        <input type="text" id="Text_Mobile" placeholder="">
    </div>
    <div class="line">
        <span class="fl">性别：</span>
        <div class="sexOpt">
            <span id="spanSex1" class="optItem">男</span>
            <span id="spanSex2" class="optItem">女</span>
        </div>
    </div>
    <div class="line">
        <span class="fl">职业：</span>
        <div class="careerOpt">
            <span id="spanAvatar1" class="optItem">经销商</span>
            <span id="spanAvatar2" class="optItem">设计师</span>
            <span id="spanAvatar3" class="optItem">其他</span>
        </div>
    </div>
    <div class="line">
        <span class="fl">详细地址：</span>
        <textarea id="Text_Address" placeholder="填写您的收货地址，赠送礼品及最新梁景华作品集"></textarea>
    </div>
    <button onclick="return updateOK()">确认</button>
</div>
<script src="js/jquery.min.js"></script>
<script src="js/option.js"></script>
</body>
</html>