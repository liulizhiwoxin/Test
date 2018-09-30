<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="BuysingooShop.Weixin.index" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <title>VIP会员</title>
    <link rel="stylesheet" href="css/common.css?v=20180410">
    <link rel="stylesheet" href="css/vip.css?v=20180410">

    <script src="js/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script src="js/SingGooJS.js"></script>

    <script type="text/javascript">

        var user_id = 0;    // 用户id
        var user_name = ""; // 用户名
        var isBuwei = 0;    // 0未付费 1已付费

        $(function () {
            
            var user = localStorage.getItem("userinfo");

            if (user == null) {

                alert("用户尚未登录，请先登录！");
                window.location.href = "http://" + window.location.host + "/login.aspx";
                return;
            } else {
                var jsonobj = JSON.parse(user);
                user_id = jsonobj._id;

                //操作用户数据
                var reg_time = jsonobj._reg_time_str;
                var pay_time = jsonobj._pay_time_str;
                var nick_name = jsonobj._nick_name;
                user_name = jsonobj._user_name;
                var real_name = jsonobj._real_name;
                isBuwei = jsonobj._isBuwei;

                //付款成功，跳回该页面，需要重新取一下数据
                $.ajax({
                    type: 'GET',
                    url: 'Service/AdminServiceHandler.ashx',
                    data: 'param=userLogin&phone=' + user_name,
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
                            var jsonobj2 = JSON.parse(json);
                            reg_time = jsonobj2._reg_time_str;
                            pay_time = jsonobj2._pay_time_str;                         

                        } else {
                            alert('登录失败！');
                        }
                    }
                });


                if (reg_time != "") {
                    //$("#spanRegTime").text(reg_time);
                }

                if (pay_time != "") {
                    //$("#spanPayTime").text(pay_time);
                }

                if (real_name != "") {
                    //$("#spanRealName").text(real_name);
                }

                if (nick_name != "") {
                    $("#spanNickName").text(nick_name);
                }
            }
           
        });

        //去百度资源库下载资源
        function gotoBaidu() {
            if (isBuwei == "0") {
                alert("您还未付费,暂不能查看该资源,立即付费！");
                window.location.href = "http://" + window.location.host + "/WeixinPay/JSAPI_PAY.aspx?userid=" + user_id + "&user_name=" + user_name;
            } else if (isBuwei == "1") {
                window.location.href = "download.aspx?v=" + randomNum(10,99);                
            }
        }

        //生成从minNum到maxNum的随机数
        function randomNum(minNum,maxNum){ 
            switch(arguments.length){ 
                case 1: 
                    return parseInt(Math.random()*minNum+1,10); 
                    break; 
                case 2: 
                    return parseInt(Math.random()*(maxNum-minNum+1)+minNum,10); 
                    break; 
                default: 
                    return 0; 
                    break; 
            } 
        } 
      
    </script>

</head>
<body>

    <div class="vipCard">
    <img src="images/img_vipCard.png">
    <div class="touxiang">
		<div class="pic" id="divLogo" runat="server"><img src="images/touxiang.png"/></div>
		<div class="text"><span id="spanRealName" runat="server"></span><br/>ID.<span id="spanNickName"></span></div>
	</div>
</div>
<div class="privilege">
    <h4><span>特权类别目录</span></h4>
    <ul>
		<li>大师作品集</li>
		<li>软装素材</li>
		<li>供应商资料</li>
		<li>概念方案大全</li>
		<li>色彩搭配</li>
		<li>概念氛围</li>
		<li>样板房软装</li>
		<li>别墅豪宅</li>
		<li>家装设计</li>
		<li>品牌酒店</li>
		<li>酒店风格分类</li>
		<li>精品酒店</li>
		<li>办公空间</li>
		<li>餐厅茶楼咖啡</li>
		<li>公共文体</li>
		<li>售楼中心</li>
		<li>会所软装</li>
		<li>商城商店</li>
		<li>展厅展示</li>
		<li>KTV酒吧</li>
		<li>美容美发</li>
		<li>空间分类</li>
		<li>彩屏彩立PSD</li>
		<li>软装培训</li>
		<li>软装风水</li>
		<li>灯光设计</li>
		<li>风格解析</li>
		<li>公司管理</li>
		<li>物料表格</li>
		<li>国外杂志</li>
		<li>软装书籍</li>
		<li>手绘学习</li>
		<li>方案排版</li>
		<li>合同预算</li>
		<li>软件技巧</li>
		<div style="clear:both;"></div>
	</ul>
</div>
<div class="menu">
    <ul>        
        <li>
            <a href="javascript:gotoBaidu();">
                <h5>资源库获取</h5>
                <p>立即获取在线资源库</p>
            </a>
        </li>

    </ul>
</div>

    <script src="js/wx-share.js?v=20180411"></script>
    <script type="text/javascript">

        var vtype = "";
        if (vtype == "preview") {
            WXJSSDK.load({
                ready: function () {
                    wx.hideOptionMenu();
                }
            });
        } else {
            debugger;

            var user_id_str;
            var user = localStorage.getItem("userinfo");

            if (user == null) {

            } else {
                var jsonobj = JSON.parse(user);
                user_id_str = jsonobj._id;
            }


            var title = "门川家居.品质生活";
            var content = "历经八年探索之路创始人及创始团队联合中国两岸三地顶级一线家居设计师推出高品质、高质量的全新品牌--'门川家居'";
            var link = "http://shop.mc-house.com/login.aspx?preid=" + user_id_str;

            var imgUrl = "http://shop.mc-house.com/images/share_logo.jpg";

            var wxShareDate = {
                "title": title,
                "content": content,
                "imgUrl": imgUrl,
                "shareFriend": link,
                "shareCircle": link,
                "link": link
            };

            //微信新版分享
            WX_wid = "1";
            WXJSSDK.load({
                ready: function () {
                    WXAPI.shares({
                        title: wxShareDate.title,               // 分享标题
                        desc: wxShareDate.content,              // 分享描述
                        link: "",                               // 其他分享链接
                        shareFriend: wxShareDate.shareFriend,   // 分享给朋友
                        shareCircle: wxShareDate.shareCircle,   // 分享到朋友圈
                        imgUrl: wxShareDate.imgUrl              // 分享图标
                    })
                }
            });
        }
    </script>

<%--<div class="vipCard">
    <img src="images/img_vipCard.png">
    <div class="time">
        <span id="spanRegTime"></span>~<span id="spanPayTime"></span>&nbsp;
        <span id="spanNickName"></span>
    </div>
</div>
<div class="privilege">
    <h4><span>我的特权</span></h4>
    <ul>
        <li class="clf">
            <i class="icon icon1 fl"></i>
            <h5>设计大师作品资源库</h5>
            <p>知名设计师作品集完整打包赠送，随时更新大师最新作品</p>
        </li>
        <li class="clf">
            <i class="icon icon2 fl"></i>
            <h5>国内外产业链结构资源库</h5>
            <p>打通欧洲地区及国内家具产业链上下游，为您打造完美产业链</p>
        </li>
        <li class="clf">
            <i class="icon icon3 fl"></i>
            <h5>代工厂资源库</h5>
            <p>提供中国百家标杆代工厂资源，多种业态复合资源一应掌握</p>
        </li>
    </ul>
</div>
<div class="menu">
    <ul>
        <li>
            <a href="user_edit.aspx" >
                <h5>我的联系地址</h5>
                <p>填写联系地址即可获赠最新更新设计大师作品集</p>
            </a>
        </li>
        <li>
            <a href="javascript:alert('建设中...');">
                <h5>资源库获取</h5>
                <p>立即获取在线资源库</p>
            </a>
        </li>

        
    </ul>
</div>--%>
</body>
</html>