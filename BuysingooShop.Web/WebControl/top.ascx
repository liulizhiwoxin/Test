<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="top.ascx.cs" Inherits="BuysingooShop.Web.WebControl.top" %>

<div class="main_content">
    <!--左侧菜单-->
    <div class="Function_tabs_frame">
        <div class="Function_tabs_box">
            <div class="Function_tabs">
                <div class="BannerConten_left_nav">
                    <div class="Weixin_FunctionList_Course">

                        <div class="Weixin_FunctionList_ContentBox">
                            <div class="Weixin_FunctionList_Content">

                                <div class="Weixin_FunctionList_Name Weixin_FunctionList_NameChecked">
                                    网站<br />
                                    导航
                                </div>
                            </div>
                            <div class="Weixin_FunctionList_Content">

                                <div class="Weixin_FunctionList_Name">
                                    会员<br />
                                    礼包
                                </div>
                            </div>
                            <div class="Weixin_FunctionList_Content">

                                <div class="Weixin_FunctionList_Name">
                                    最新<br />
                                    活动
                                </div>
                            </div>
                            <div class="Weixin_FunctionList_Content">
                                <div class="Weixin_FunctionList_Name">
                                    轮胎<br />
                                    系列
                                </div>
                            </div>
                            <div class="Weixin_FunctionList_Content">
                                <div class="Weixin_FunctionList_Name">
                                    机油<br />
                                    系列
                                </div>
                            </div>
                            <%--                                            <div class="Weixin_FunctionList_Content">
                                                <div class="Weixin_FunctionList_Name">
                                                    镀晶<br />
系列
                                                </div>
                                            </div>--%>
                            <div class="Weixin_FunctionList_Content">
                                <div class="Weixin_FunctionList_Name">
                                    养生<br />
                                    系列
                                </div>
                            </div>
                            <div class="Weixin_FunctionList_Content">

                                <div class="Weixin_FunctionList_Name">
                                    在线<br />
                                    留言
                                </div>
                            </div>
                            <div class="Weixin_FunctionList_Content">

                                <div class="Weixin_FunctionList_Name">
                                    荣誉<br />
                                    资质
                                </div>
                            </div>

                            <div class="Weixin_FunctionList_Content ">

                                <div class="Weixin_FunctionList_Name htop">
                                    <span><a href="#topone"></a></span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<!--右侧贴边导航-->
<div class="mui-mbar-tabs">
    <div class="quick_link_mian">
        <div class="quick_links_panel">
            <div id="quick_links" class="quick_links">

                <li id="tatol">
                    <a href="totals_userinfo.aspx" target="_top" class="history_list"><i class="view"></i></a>
                    <div class="mp_tooltip">个人中心<i class="icon_arrow_right_black"></i></div>
                </li>
                <li>
                    <a href="cart.aspx" class="mpbtn_histroy"><i class="zuji"></i></a>
                    <div class="mp_tooltip">购物车<i class="icon_arrow_right_black"></i></div>
                </li>
                <li>
                    <a href="#" class="mpbtn_wdsc"><i class="wdsc"></i></a>
                    <div class="mp_tooltip">我的收藏<i class="icon_arrow_right_black"></i></div>
                </li>
                <li>
                    <a href="#" class="mpbtn_recharge"><i class="chongzhi"></i></a>
                    <div class="mp_tooltip">抢购物品<i class="icon_arrow_right_black"></i></div>
                </li>
                <li>
                    <a href="#" class="mpbtn_wdsc"><i class="cart"></i></a>
                    <div class="mp_tooltip">我要充值<i class="icon_arrow_right_black"></i></div>
                </li>
                <li>
                    <a href="#" class="mpbtn_recharge"><i class="qa"></i></a>
                    <div class="mp_tooltip">客服中心<i class="icon_arrow_right_black"></i></div>
                </li>
            </div>
            <div class="quick_toggle">

                <li>
                    <a href="#none"><i class="mpbtn_qrcode"></i></a>
                    <div class="mp_qrcode" style="display: none;">
                        <img src="../images/ew.jpg" width="148" height="148" /><i class="icon_arrow_white"></i></div>
                </li>

            </div>
        </div>
    </div>
</div>
<!----顶部---->
<div class="header-top Function_bg" id="floor1">
    <div class="content">
        欢迎光临宇航龙科技（深圳）有限公司！
        <a href="#" id="logon" class="log">请登入</a>
        <a href="#" <%--onclick="user_logout()" --%> id="exitlogin" style="display: none">退出</a>
        <a href="#" id="register" class="reg">用户注册</a>
        <%--其他登入： <a href="#"><img src="../images/qq.jpg" /></a>
        <a href="#"><img src="../images/wx.jpg" /></a>--%>
        <div class="rside">
            <ul>

                <li class="sub"><a href="totals_userinfo.aspx" id="myInfos">我的资料</a><i></i>
                    <ul>
                        <li><a href="totals_address.aspx">收货地址</a></li>
                        <li><a href="totals_list.aspx">我的团队</a></li>
                        <li><a href="totals_userupdate.aspx">资料修改</a></li>
                    </ul>
                </li>

                <li class="sub"><a href="javascript:;" id="myOrders">我的订单</a><i></i>

                    <ul>
                        <li><a href="totals_order1.aspx">待支付</a></li>
                        <li><a href="totals_order2.aspx">待收货</a></li>
                        <li><a href="totals_order3.aspx">已完成</a></li>
                    </ul>

                </li>
                <li class="cart"><a href="cart.aspx"><span></span>购物车<em id="cartnum">0</em></a></li>

                <li><a href="javascript:void(0);" onclick="AddFavorite('我的网站',location.href)">收藏本站</a></li>
                <%-- <li class="sub"><a href="javascript:;">分享本站</a><i></i>
                	<ul><li><a href="#">我的姓名</a></li><li><a href="#">我的姓名</a></li><li><a href="#">我的姓名</a></li></ul>
                </li> --%>
                <li><a href="http://www.szyuhanglong.com/">访问官网</a></li>
            </ul>
        </div>
    </div>
</div>
<div class="header" id="topone">
    <div class="nav_top">
        <div class="logo fl">
            <a href="index.aspx">
                <img src="../images/logo.png" /></a>
        </div>
        <ul class="lside">
            <li class="m163">
                <input type="text" name="search" value="搜索商品 品牌 型号 种类..." id="search" /><input class="btn" type="submit" value="" id="searbtn" />
                <br />
                推荐词：<a href="#">防爆轮胎</a>、<a href="#">防爆轮胎</a>、<a href="#">防爆轮胎</a>

            </li>
            <li class="t1 fr">
                <img src="../images/er.jpg" class="pull-right" />
                <span><em></em>宇航科技</span><span><em></em>军工品质</span>
                <br />
                <span class="tel"><em></em>0755-86599886</span>

            </li>
        </ul>

    </div>
</div>




<script type="text/javascript">
    var users = localStorage.getItem("userinfo");



    $(function () {

        if (users == null) {
            $(".sub>ul").empty();
        }
        username();
        user_logout();
        //判断用户是否登录
        islogin();

        searchpro();
        //获取购物车数量
        var cart = localStorage.getItem("cart");
        if (cart == null) {
            $("#cartnum").text("0");
        }
        else {
            var cartarray = JSON.parse(cart);//购物车数量
            var cartnum = cartarray.length;
            $("#cartnum").text(cartnum);
        }
        ewcode();
        getEW();
    });



    //搜索产品
    function searchpro() {
        var searchpro = $("#search").val();
        $("#searbtn").click(function () {
            var search = $("#search").val();
            var url = "product_search.aspx?search=";
            window.location.href = encodeURI(url + search);
        });
    };



    //加入收藏
    function AddFavorite(title, url) {
        try {
            window.external.addFavorite(url, title);
        }
        catch (e) {
            try {
                window.sidebar.addPanel(title, url, "");
            }
            catch (e) {
                alert("抱歉，此操作被浏览器拒绝! \n\n加入收藏失败，请使用Ctrl+D进行添加");
            }
        }
    }









    //获取用户信息
    function username() {
        //获取用户信息
        var html = "";
        $.ajax({
            type: 'POST',
            url: 'Service/AdminServiceHandler.ashx',
            data: {
                "param": "username",
                "users": users
            },
            async: false,
            dataType: 'json',
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                //    alert("状态：" + textStatus + ";出错提示：" + errorThrown);
                alert("请等待网页加载完成之后进行操作！");

            },
            success: function (data) {

                if (data.status != 0) {

                    $("#logon").attr("style", "display:none;");
                    //增加退出按钮
                    $("#exitlogin").attr("style", "display:inline");

                    $.each(data.T_blog, function (i, item) {

                        $("#register").text(item.user_name);
                        if (users != "" || users != undefined || users != null) {

                            $("#goodsInfo").empty();
                            //会员类型判断
                            var goodstype = "";
                            if (item.group_id == 1) {
                                goodstype = "金卡"
                            } else if (item.group_id == 2) {
                                goodstype = "钻石"
                            } else if (item.group_id == 3) {
                                goodstype = "服务商"
                            } else if (item.group_id == 4) {
                                goodstype = "总监"
                            } else if (item.group_id == 5) {
                                goodstype = "公司"
                            } else if (item.group_id == 0) {
                                goodstype = "推广员"
                            }
                            //用户积分
                            // var usable_amount = (parseFloat(item.amount_total) - parseFloat(item.frozen_amount_total)).toFixed(2);
                            var usable_amount = item.point;

                            var user_img = "";

                            if (item.avatar == "") {
                                user_img = "../images/head.png";
                            }
                            if (item.avatar.indexOf("qlogo") == -1) {
                                user_img = "http://weixin.szyuhangl.com/" + item.avatar;

                                // user_img = "  http://localhost:63590/" + item.avatar;
                            }
                            else { user_img = item.avatar; }

                            //市场ID
                            var marketId = 0;

                            // 用户组Id
                            var organizeId = 0;

                            // 用户ID
                            var id = 0;

                            if (item.group_id > 0) {
                                if (item.marketId <= 0) {
                                    marketId = item.id;
                                } else {
                                    marketId = item.marketId;
                                }
                                organizeId = item.organizeId;
                                id = item.id;
                                if (marketId > 0) {
                                    //var imagecode = "WeixinPay/MakeQRCode.aspx?data=http://" + window.location.host + "/regist.htm?id=" + id + "&marketId=" + marketId + "&organizeId=" + organizeId;
                                    var imagecode = "MakeQRCode.aspx?data=http://weixin.szyuhangl.com/regist.html?id=" + id + "_" + marketId + "_" + organizeId;


                                }
                            } else {
                                var imagecode = "新用户暂不能推广,<a href='product.html?id=218'>立即体验</a>";
                            }





                            //页面加载
                            html += '<div class="head fl">';
                            html += '<img src="' + user_img + '"   style="height:70px;width:70px;border-radius:50px" />';
                            html += '</div>';
                            html += '<dl class="fl"   id="goodInfo">';
                            html += '<dt>' + (item.user_name).substr(0, 3) + "..." + (item.user_name).substr(7, 11) + '<span>' + goodstype + '</span></dt>';//绑定手机号+会员类型
                            html += '<dd>昵称: ' + item.nick_name + '</dd>';//绑定昵称+ID编号
                            html += '<dd>积分:' + Math.round((usable_amount * 100)) / 100 + '</dd>';//用户积分
                            html += '</dl>';
                            html += '<div class="clearfix"></div>';
                            html += '<div class="menu">';
                            html += '<ul>';
                            html += '<li class="t1">';
                            html += '<a href="totals_order1.aspx"><div class="icon"></div>';
                            html += '我的订单';
                            html += '</a>';
                            html += '</li>';
                            html += '<li class="t2" id="amount_tips">';
                            html += '<a href="#"><div class="icon"></div>';
                            html += '我的钱包';
                            html += '</a>';
                            html += '</li>';
                            html += '<li class="t3" id="ew_code">';
                            html += '<a href="javascript:;"><div class="icon"></div>';
                            html += '我的二维码';
                            html += '</a>';
                            html += ' <div  id="ew_code2" style="display:none;z-index:1200;position:absolute;margin-top:15px;"></div>';
                            html += '</li>';
                            html += '<li class="t4">';
                            html += '<a href="totals_userinfo.aspx"><div class="icon"></div>';
                            html += '个人中心';
                            html += '</a>';
                            html += '</li>';
                            html += '</ul>';
                            html += '</div>';
                        }
                        $("#goodsInfo").append(html);



                    });
                }
            }
        });
    }
    //用户退出时 清除Session /cookis  和localStorage存储的用户信息
    function user_logout() {

        $("#exitlogin").click(function () {
            //var sendurl = "../Service/AdminServiceHandler.ashx?action=user_login_out";
            var sendurl = "Service/AdminServiceHandler.ashx";
            $.ajax({
                async: false,
                type: "POST",
                url: sendurl,
                data: { "param": "user_login_out", },
                dataType: "json",
                success: function (data) {
                    localStorage.removeItem("userinfo");
                    //window.location = window.location;
                    window.location.reload();//刷新当前页面.
                    loginbefore();
                }
            });
            return false;
        });
    }

    //判断用户是否登录
    function islogin() {
        $("#myInfos").click(function () {
            if (users == "" || users == undefined || users == null) {
                alert("用户尚未登录，请先登录！");
                return false;
            }
        });
        $("#myOrders").click(function () {
            if (users == "" || users == undefined || users == null) {
                alert("用户尚未登录，请先登录！");
                return false;
            }
        });
        $("#tatol").click(function () {
            if (users == "" || users == undefined || users == null) {
                alert("用户尚未登录，请先登录！");
                return false;
            }
        });
    }


    function ewcode() {
        $("#ew_code").mouseover(function () {
            $(this).children("#ew_code2").css("display", "block");
        });

        $("#ew_code").mouseleave(function () {
            $(this).children("#ew_code2").css("display", "none");
        });

   
    }



    function getEW() {
        $.ajax({
            type: 'POST',
            url: 'Service/AdminServiceHandler.ashx',
            data: {
                "param": "username",
                "users": users
            },
            async: false,
            dataType: 'json',
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                //    alert("状态：" + textStatus + ";出错提示：" + errorThrown);
                alert("请等待网页加载完成之后进行操作！");
            },
            success: function (data) {
                if (data.status != 0) {
                    $.each(data.T_blog, function (i, item) {

                        var marketId = 0;

                        var organizeId = 0;

                        var id = 0;

                        if (item.group_id > 0) {
                            if (item.marketId <= 0) {
                                marketId = item.id;
                            } else {
                                marketId = item.marketId;
                            }
                            organizeId = item.organizeId;
                            id = item.id;
                            if (marketId > 0) {

                                var imagecode = "MakeQRCode.aspx?data=http://weixin.szyuhangl.com/regist.html?id=" + id + "_" + marketId + "_" + organizeId;
                                $("#ew_code2").html("<img src=" + imagecode + " width=\"108\" height=\"108\" /><i class=\"icon_arrow_white\"></i>");
                            }
                        } else {
                            $("#ew_code2").html("<span style=\"color:red\">消费后获取推广资格！");

                        }
                    });
                }
            }
        });




    }



</script>
