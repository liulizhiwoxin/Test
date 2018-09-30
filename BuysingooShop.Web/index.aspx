<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="BuysingooShop.Web.index" %>
<%@ Import Namespace="System.Data" %>
<%@ Register Src="~/WebControl/top.ascx" TagPrefix="uc1" TagName="top" %>
<%@ Register Src="~/WebControl/bottom.ascx" TagPrefix="uc1" TagName="bottom" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>宇航龙pc端</title>
<script src="js/jquery-1.8.0.min.js"></script>
<link href="css/global.css" type="text/css" rel="stylesheet" media="screen" />
<link href="css/total.css?v=20170110" type="text/css" rel="stylesheet" media="screen" />
<link href="css/prozoom.css" type="text/css" rel="stylesheet" media="screen" />
<script   type="text/javascript" src="js/myutil.js"></script>
<link href="css/common.css" rel="stylesheet" type="text/css"/>
<link href="css/scrolls.css" rel="stylesheet" type="text/css"/>
<link href="css/sidePanel.css" media="screen" rel="stylesheet" type="text/css"/>
<link href="css/rfix.css?v=201701102323" media="screen" rel="stylesheet" type="text/css"/>
     <!--[if lt IE 9]>
         <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
         <script src="https://oss.maxcdn.com/libs/respond.js/1.3.0/respond.min.js"></script>
      <![endif]-->
<script type="text/javascript">
    var parent_id = 0;//父类id
    $(function () {
        var imgUrlServer = "<%=imgUrlServer %>";  //图片服务器地址
        banner(imgUrlServer);//获取banner
        membership(imgUrlServer);//获取钻石会员套餐
        hotsale(imgUrlServer);//获取热卖商品
        productlist(imgUrlServer);//获取菜单产品系列列表
        productluntai(imgUrlServer);//轮胎系列
        productengineoil(imgUrlServer);//获取机油系列列表
        productSealant(imgUrlServer);//获取镀晶系列列表
        producthealth(imgUrlServer);//获取养生系列列表
        productHonor(imgUrlServer);//获取荣誉资质系列列表
        loadnews();//加载活动
        loadnotice();//加载公告
        loginbefore();//登录前
    })
    //获取banner
    function banner(imgUrlServer) {
        var html = "";
        $.ajax({
            type: 'POST',
            url: 'Service/AdminServiceHandler.ashx',
            data: {
                "param": "productHonor",
                "id": 114
            },
            async: false,
            dataType: 'json',
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                //    alert("状态：" + textStatus + ";出错提示：" + errorThrown);
                alert("请等待网页加载完成之后进行操作！");
            },
            success: function (data) {
                if (data.status != 0) {
                    $("#banner div").remove();
                    $.each(data.T_blog, function (i, item) {
                        var imgUrls = imgUrlServer + item.img_url;
                        html += "<div>";
                        html += "   <ul>";
                        html += "       <li><a >";
                        html += "           <img src='" + item.img_url + "'/>";
                        html += "       </a></li>";
                        html += "    </ul>";
                        html += "</div>";
                    });
                    $("#banner").html(html);
                }
            }
        });
    }

    //获取会员套餐
    function membership(imgUrlServer) {
        var count = "";
        var html = "";
        $.ajax({
            type: 'POST',
            url: 'Service/AdminServiceHandler.ashx',
            data: {
                "param": "membership",
                "id": 138
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
                        var imgUrls = imgUrlServer + item.img_url;
                        html += "<li>";
                        html += "   <div class=\"box\">";
                        html += "   <div class='des'>";
                        html += "       <h3  style=\"cursor: pointer\"     onclick='goto_buy(\"" + item.id + "\")'>" + (item.title).substr(0, 5) + "</h3>";
                        html += "       <p>会员套餐系列</p>";
                        html += "       <span>￥" + item.sell_price + "</span>";
                        html += "       <a  onclick='addcart(\"" + item.id + "|" + item.title + "|" + item.sell_price + "|1|" + item.img_url + "\")'   href='javascript:;' >购买</a>";
                        html += "    </div>";
                        html += "       <div class='pic' title=" + item.title + "    style=\"cursor: pointer\"     onclick='goto_buy(\"" + item.id + "\")' ><img   src='http://admin.szyuhangl.com" + item.img_url + "' />";
                        html += "       </div>";
                        html += "  </div>";
                        html += "   </li>";
                    });
                    $("#membership").html(html);
                }
            }
        });
    }
    //获取热卖商品
    function hotsale(imgUrlServer) {
        var count = "";
        var html = "";
        $.ajax({
            type: 'POST',
            url: 'Service/AdminServiceHandler.ashx',
            data: {
                "param": "membership_hot",//热卖商品
            },
            async: false,
            dataType: 'json',
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                //    alert("状态：" + textStatus + ";出错提示：" + errorThrown);
                alert("请等待网页加载完成之后进行操作！");
            },
            success: function (data) {
                if (data.status != 0) {
                    //$("#hotsale li").remove();
                    $.each(data.T_blog, function (i, item) {
                        //热卖商品数据
                        if (item.is_top == 1) {
                            var imgUrls = imgUrlServer + item.img_url;
                            html += "<li>";
                            html += "   <div class='rm'></div>";
                            html += "       <div class='box'>";
                            html += "       <div class='pic'><img src='http://admin.szyuhangl.com" + item.img_url + "' /></div>";
                            html += "        <div class='des'><h3  style=\"cursor: pointer\"  onclick='goto_buy(\"" + item.id + "\")'   >" + item.title + "</h3>";
                            html += "        <span>￥" + item.sell_price + "</span>";
                            html += "            <div class='nprice'>￥" + item.market_price + "</div>";
                            html += "        <a  onclick='addcart(\"" + item.id + "|" + item.title + "|" + item.sell_price + "|1|" + item.img_url + "\")'  href='javascript:;' >购买</a>";
                            html += "       </div>";
                            html += "       </div>";
                            html += "</li>";

                        }
                        $("#hotsale").html(html);
                    });

                }
            }
        });
    }











    //获取产品系列
    function productlist(imgUrlServer) {
        //加载一级类别异步
        $.ajax({
            type: 'POST',
            url: 'Service/AdminServiceHandler.ashx',
            data: {
                "param": "productlist",
                "parent_id": parent_id
            },
            dataType: 'json',
            async: false,
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                //    alert("状态：" + textStatus + ";出错提示：" + errorThrown);
                alert("请等待网页加载完成之后进行操作！");
            },
            success: function (data) {
                var html = "";
                if (data.status != 0) {
                    $("#list li").remove();
                    $.each(data.T_blog, function (i, item) {

                        html += "  <li class=\"mod_cate\"><h2><a href=\"productlist.aspx?id= " + item.id + " \" class=\"m1\"><i></i>" + item.title + "</a></h2></li> ";
                    });
                }
                $("#list").html(html);
            }
        });
    }



    //获取产品系列列表
    //function productlist(imgUrlServer) {
    //    //加载一级类别异步
    //    $.ajax({

    //        type: 'POST',
    //        url: 'Service/AdminServiceHandler.ashx',
    //        data: {
    //            "param": "productlist",
    //            "parent_id": parent_id
    //        },
    //        dataType: 'json',
    //        error: function (XMLHttpRequest, textStatus, errorThrown) {
    //            alert("状态：" + textStatus + ";出错提示：" + errorThrown);
    //        },
    //        success: function (data) {
    //            var html = "";
    //            if (data.status != 0) {
    //                $("#list li").remove();
    //                $.each(data.T_blog, function (i, item) {
    //                    var i1 = i + 1;
    //                    html += "<li class='mod_cate'><h2><a href='#' class='m" + i1 + "'><i></i>" + item.title + "</a></h2><div class='mod_subcate'><div class='mod_subcate_main'>";
    //                    //加载二级类别同步
    //                    var htmls = "";
    //                    $.ajax({
    //                        type: 'POST',
    //                        url: 'Service/AdminServiceHandler.ashx',
    //                        data: {
    //                            "param": "productlistclassify",
    //                            "id": item.id
    //                        },
    //                        async: false,
    //                        dataType: 'json',
    //                        error: function (XMLHttpRequest, textStatus, errorThrown) {
    //                            alert("状态：" + textStatus + ";出错提示：" + errorThrown);
    //                        },
    //                        success: function (data) {
    //                            if (data.status != 0) {
    //                                $.each(data.T_blog, function (j, items) {
    //                                    htmls += "<dl>";
    //                                    htmls += "     <dt>";
    //                                    htmls += "       <h3>";
    //                                    htmls += "        <a href=\"productlist.aspx?id=" + items.id + "\">";
    //                                    htmls += "            " + items.title + "</a>";
    //                                    htmls += "       </h3>";
    //                                    htmls += "     </dt>";
    //                                    //加载产品同步
    //                                    var htmlst = "";
    //                                    $.ajax({
    //                                        type: 'POST',
    //                                        url: 'Service/AdminServiceHandler.ashx',
    //                                        data: {
    //                                            "param": "productlistclassifyarticle",
    //                                            "id": items.id
    //                                        },
    //                                        async: false,
    //                                        dataType: 'json',
    //                                        error: function (XMLHttpRequest, textStatus, errorThrown) {
    //                                            alert("状态：" + textStatus + ";出错提示：" + errorThrown);
    //                                        },
    //                                        success: function (data) {
    //                                            if (data.status != 0) {
    //                                                $.each(data.T_blog, function (h, itemh) {
    //                                                    htmlst += "     <dd>";
    //                                                    htmlst += "       <h3>";
    //                                                    htmlst += "        <a href=\"pro_buy.aspx?id=" + itemh.id + "\">";
    //                                                    htmlst += "            " + itemh.title + "</a>";
    //                                                    htmlst += "       </h3>";
    //                                                    htmlst += "     </dd>";
    //                                                });
    //                                            }
    //                                        }
    //                                    });
    //                                    htmls += htmlst;
    //                                    htmls += "</dl>";
    //                                    if (j == 2) {//当产品大于三个时候加一次换行
    //                                        htmls += " <br class='clear'>";
    //                                    }
    //                                });
    //                            }
    //                        }
    //                    });
    //                    html += htmls;
    //                    html += "</div></div> </li>";
    //                });
    //                $("#list").html(html);
    //                //执行ajax后鼠标移入移出，显示隐藏
    //                $('#list li').mouseover(function () {
    //                    $(this).addClass('on').siblings().removeClass('on');
    //                });
    //                $('#list li').mouseleave(function () {
    //                    $(this).removeClass('on');
    //                });
    //            }
    //            else {



    //            }
    //        }
    //    });
    //}
    //获取轮胎系列
    function productluntai(imgUrlServer) {
        var html = "";
        var count = 0;
        $.ajax({
            type: 'POST',
            url: 'Service/AdminServiceHandler.ashx',
            data: {
                "param": "producttyreseries",
                "id": 141
            },
            async: false,
            dataType: 'json',
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                //    alert("状态：" + textStatus + ";出错提示：" + errorThrown);
                alert("请等待网页加载完成之后进行操作！");
            },
            success: function (data) {
                if (data.status != 0) {
                    $("#productluntai li").remove();
                    $.each(data.T_blog, function (i, item) {
                        if (count < 4) {
                            var imgUrls = imgUrlServer + item.img_url;
                            html += "<li>";
                            html += "    <div class='pic'><img src='http://admin.szyuhangl.com" + item.img_url + "'></div>";
                            html += "       <div class=\"des\">";
                            html += "           <h3  style=\"cursor: pointer\" onclick='goto_buy(\"" + item.id + "\")'   >" + item.title.substr(0, 6) + "</h3>";
                            html += "        <p>" + item.title.substr(6, item.title.length) + "</p>";
                            html += "           <span>￥ " + item.sell_price + "</span>";
                            html += "     <div class=\"nprice\">￥  " + item.market_price + "<a  onclick='addcart(\"" + item.id + "|" + item.title + "|" + item.sell_price + "|1|" + item.img_url + "\")'   href='javascript:;' class=\"fr\">购买</a></div>";
                            html += "     </div>";
                            html += "      </li>";
                        }
                        count++;
                    });
                    $("#productluntai").html(html);
                }
            }
        });
    }

    //获取机油系列列表
    function productengineoil(imgUrlServer) {
        var count = 0;
        var html = "";
        $.ajax({
            type: 'POST',
            url: 'Service/AdminServiceHandler.ashx',
            data: {
                "param": "productengineoil",
                "id": 94
            },
            async: false,
            dataType: 'json',
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                //    alert("状态：" + textStatus + ";出错提示：" + errorThrown);
                alert("请等待网页加载完成之后进行操作！");
            },
            success: function (data) {
                if (data.status != 0) {
                    $("#productengineoil li").remove();
                    $.each(data.T_blog, function (i, item) {
                        if (count < 6) {
                            var imgUrls = imgUrlServer + item.img_url;

                            html += "<li>";
                            html += "    <div class='pic'><img src='http://admin.szyuhangl.com" + item.img_url + "'></div>";
                            html += "       <div class='des'>";
                            html += "           <h3  style=\"cursor: pointer\"  onclick='goto_buy(\"" + item.id + "\")' >" + item.title + "</h3>";

                            html += "           <span>￥ " + item.sell_price + "</span>";
                            html += "           <div class='nprice'>￥ " + item.market_price + "  </div>";
                            html += "            <a  onclick='addcart(\"" + item.id + "|" + item.title + "|" + item.sell_price + "|1|" + item.img_url + "\")'  href='javascript:;'>购买</a>";
                            html += "       </div>";
                            html += "</li>";
                        }
                        count++;
                    });
                    $("#productengineoil").html(html);
                }
            }
        });
    }

    //获取镀晶系列列表
    function productSealant(imgUrlServer) {
        var count = 0;
        var html = "";
        $.ajax({

            type: 'POST',
            url: 'Service/AdminServiceHandler.ashx',
            data: {
                "param": "productSealant2",
                "id": 93
            },
            async: false,
            dataType: 'json',
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                //    alert("状态：" + textStatus + ";出错提示：" + errorThrown);
                alert("请等待网页加载完成之后进行操作！");
            },
            success: function (data) {
                if (data.status != 0) {
                    $("#productSealant").empty();
                    $.each(data.T_blog, function (i, item) {
                        if (count < 3) {
                            var imgUrls = imgUrlServer + item.img_url;

                            html += "<li>";
                            html += " <div class='box'>";
                            html += "    <div class='pic'><img src='http://admin.szyuhangl.com" + item.img_url + "'></div>";
                            html += "       <div class='des'>";
                            html += "           <h3  style=\"cursor: pointer\" onclick='goto_buy(\"" + item.id + "\")' >" + item.title + "</h3>";
                            html += "           <span>￥ " + item.sell_price + "</span>";
                            html += "           <span class='nprice'>￥ " + item.market_price + "  </span>";
                            html += "            <a onclick='addcart(\"" + item.id + "|" + item.title + "|" + item.sell_price + "|1|" + item.img_url + "\")'  href='javascript:;'>购买</a>";
                            html += "       </div>";
                            html += "</div></li>";
                        }
                        count++;
                    });
                    $("#productSealant").html(html);
                }
            }
        });
    }
    //获取养生系列列表
    function producthealth(imgUrlServer) {
        var count = 0;
        var html = "";
        $.ajax({
            type: 'POST',
            url: 'Service/AdminServiceHandler.ashx',
            data: {
                "param": "producthealth",
                "id": 86
            },
            async: false,
            dataType: 'json',
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                //    alert("状态：" + textStatus + ";出错提示：" + errorThrown);
                alert("请等待网页加载完成之后进行操作！");
            },
            success: function (data) {
                if (data.status != 0) {
                    $("#producthealth li").remove();
                    $.each(data.T_blog, function (i, item) {
                        var imgUrls = imgUrlServer + item.img_url;
                        html += "<li>";
                        html += "<div class='box'>";
                        html += "    <div class='pic'><img style='width: 200px; margin-top: -20px;' src='http://admin.szyuhangl.com" + item.img_url + "'></div>";
                        html += "       <div class='des'>";
                        html += "           <h3   style=\"cursor: pointer\"  onclick='goto_buy(\"" + item.id + "\")' >" + item.title + "</h3>";

                        html += "           <span>￥ " + item.sell_price + "</span>";
                        html += "           <div class='nprice'>￥ " + item.market_price + "  ";
                        html += "            <a  onclick='addcart(\"" + item.id + "|" + item.title + "|" + item.sell_price + "|1|" + item.img_url + "\")'  href='javascript:;'  class='fr'>购买</a></div>";
                        html += "       </div>";
                        html += "       </div>";
                        html += "</li>";

                    });
                    $("#producthealth").html(html);
                }
            }
        });
    }

    //获取荣誉资质系列列表
    function productHonor(imgUrlServer) {
        var html = "";
        $.ajax({
            type: 'POST',
            url: 'Service/AdminServiceHandler.ashx',
            data: {
                "param": "productHonor",
                "id": 117
            },
            async: false,
            dataType: 'json',
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                //    alert("状态：" + textStatus + ";出错提示：" + errorThrown);
                alert("请等待网页加载完成之后进行操作！");
            },
            success: function (data) {

                if (data.status != 0) {
                    $("#productHonor li").remove();
                    $.each(data.T_blog, function (i, item) {
                        var imgUrls = imgUrlServer + item.img_url;

                        html += "<li>";
                        html += "<div class='pic'>";
                        html += "    <a   href='#' target='_blank'>";
                        html += "       <img src='" + item.img_url + "' alt='" + item.title + "'/>";

                        html += "       </a>";
                        html += "       </div>";
                        html += "</li>";

                    });
                    $("#productHonor").html(html);
                }
            }
        });
    }

    function addcart(goods) {
        debugger;
        var param = goods;//参数goods
        var rices = new Array();//rices 数组
        var ricekind = localStorage.getItem("cart");//获取ricekind  cart

        rices = JSON.parse(ricekind);   //rices里等于取到的cart值
        if (rices == null) {
            var rice = new Array();
            rice[0] = param;//参数
            var ricestr = JSON.stringify(rice);
            localStorage.setItem("cart", ricestr);
        } else {
            var j = 0;
            $.each(rices, function (i, item) {
                var ricetype = item.split("|");
                if (ricetype[0] == goodsId) {
                    j = 1;
                    rices[i] = param;
                    var ricestr = JSON.stringify(rices);
                    localStorage.setItem("cart", ricestr);
                    return false;
                }
            });
            if (j != 1) {
                var lengths = parseInt(rices.length);
                rices[lengths] = param;
                var ricestr = JSON.stringify(rices);
                localStorage.setItem("cart", ricestr);
            }
        }
        window.location.href = "cart.aspx";
        //var url = "cart.html";
        //$(".active_button").attr("href", encodeURI(url + "?goodsId=" + goodsId));
    }

    //登录前会员中心
    function loginbefore() {
        var html1 = "";
        var uname = localStorage.getItem("userinfo");

        if (uname == "" || uname == undefined || uname == null) {
            $("#goodsInfo").empty();
            html1 += "  <div class=\"login-bf\">";
            html1 += "  <div class=\"head\">";
            html1 += "      <img src=\"../images/head.png\"    style=\"height:70px;width:70px;border-radius:50px\" />";
            html1 += "       <br />";
            html1 += "        <span>Hi!你好</span>";
            html1 += "      </div>";
            html1 += "      <dl>";
            html1 += "         <dt><span>登录</span><span>注册</span></dt>";
            html1 += "      </dl>";
            html1 += "  </div>";
        }
        $("#goodsInfo").html(html1);
    }

    // 加载会员中心下的新闻列表
    function loadnews() {
        var html = "";
        $.ajax({
            type: 'POST',
            url: 'Service/AdminServiceHandler.ashx',
            data: {
                //获取最新活动
                "param": "loadnews",
                "id": 82
            },
            async: false,
            dataType: 'json',
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                //    alert("状态：" + textStatus + ";出错提示：" + errorThrown);
                alert("请等待网页加载完成之后进行操作！");
            },
            //  <ul   id="activity" >   <ul   id="notice">
            success: function (data) {
                if (data.status != 0) {
                    //最新活动
                    //$("#activity").remove();
                    $.each(data.T_blog, function (i, item) {
                        html += "<li><a href='news.html?id=" + item.id + "'>  " + item.title + " </a></li>";
                    });
                    $("#activity").html(html);
                }
            }
        });
    }

    // 加载会员中心下网站公告
    function loadnotice() {
        var html = "";
        $.ajax({
            type: 'POST',
            url: 'Service/AdminServiceHandler.ashx',
            data: {
                //获取最新活动
                "param": "loadnotice",
                "id": 75
            },
            async: false,
            dataType: 'json',
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                //    alert("状态：" + textStatus + ";出错提示：" + errorThrown);
                alert("请等待网页加载完成之后进行操作！");
            },
            //  <ul   id="activity" >   <ul   id="notice">
            success: function (data) {
                if (data.status != 0) {
                    //最新活动
                    //$("#activity").remove();
                    $.each(data.T_blog, function (i, item) {
                        html += "<li><a href='news.html?id=" + item.id + "'>  " + item.title + " </a></li>";
                    });
                    $("#notice").html(html);
                }
            }
        });
    }

    //前往详情页面
    function goto_buy(id) {
        window.location.href = "pro_buy.aspx?id=" + id;
    }
           </script>
</head>
<body>

  <uc1:top runat="server" ID="top" />
<!---顶部---->
<div class="nav">
	<div class="content">
    	<ul>
            <li><a href="index.aspx" >首页</a></li>
            <li><a href="#">最新活动</a></li>
            <li><a href="point_shop.aspx?id=180">积分商城</a></li>
            <li><a href="cityselect.html">服务门店</a></li>
            <li><a href="about.aspx?id=179">关于我们</a></li>
            <li><a href="contact.aspx">联系我们</a></li>
        </ul>
    </div>
</div>



<div class="main1">
	<div class="content">
    <!--<div class="ads"> 
        <form action="" method="post" name="theForm1" >
        <div class="shade"></div>
        <div class="box1">
          <span class="close">×</span>
          <img src="../images/c_con.jpg" />  
        </div>
       </form>
        </div>-->
    <!---登录注册---->
   
       
 <!---登录注册---->
        <div class="login hide"> 
        <form action="" method="post" name="theForm1" >
        <div class="shade"></div>
        <div class="box1">
        <div class="box" >
          <p><span class="close">×</span></p>
          <div class="fl"><img src="../images/logo.png" /><div class="reg"><a  id="regzhu" href="#">注册</a></div>
          <div class="clear"></div>
          <dl><dt>手机号</dt><dd><input type="text" id="username" name="use" class="t1"/></dd></dl>
          <dl><dt>密码</dt><dd><input type="password" id="userpass" name="pass" class="t2" /></dd></dl>
          <dl><dt>
             <%-- <img src="../images/vail.jpg" />--%>
               <img src="../Service/verify_codes.ashx" id="validateImg" onclick="ToggleCode(this, '../Service/verify_codes.ashx');return false;" style="cursor: pointer;width:78px;height:40px;margin-left: 10px;" title="刷新验证码">
              </dt><dd><input type="text" name="pass" id="imgCode" class="t3" /></dd></dl>
          <dl><dt class="forg"><a href="#">忘记密码？</a></dt><dd class="log"><input type="button" id="login" value="" /></dd></dl>
          </div>
          <div class="fr">
              <div class="hot">品质轮胎</div>
              <div class="detail"><img src="../images/logpro.jpg" />
              <ul>
              	<li><a href="#">-More details</a></li>
                <li><a href="#">-Enter the shop</a></li>
              </ul>
              </div>
          </div>
        </div>  
        </div>
       </form>
        </div>  
        <div class="regist hide">
        <form action="" method="post" name="theForm1" > 
        <div class="shade"></div>
        <div class="box1">
        <div class="box" >
          <p><span class="close">×</span></p>
          <div class="sign fr"><a id="logindeng" href="javascript:;">登录</a></div>
          <div class="clear"></div>
          <dl><dt>手机号：</dt><dd><input type="text" id="usersmoblie" name="use" class="t1"/></dd></dl>
          <dl ><dt>验证码：</dt>
              <dd>
                  <input type="text" name="use"  style=" height:40px;border:1px solid #bfbfbf;border-radius:3px;padding-left:10px;width: 158px;"  id="code" placeholder="验证码"/>
                  <a class="msgclass" href="javascript:;" style="float:right; width:110px; padding-right:10px; text-align:center; color:#f49601;background:url(../images/fas_yanzm12.png) no-repeat; font-family:"微软雅黑", Arial;" onclick="SendMsgCode(this)" isable="0">获取验证码</a></dd></dl>

          <dl><dt>昵称</dt><dd><input type="text" name="use" class="t1" id="user_nick" /></dd></dl>
          <dl><dt>密码：</dt><dd><input type="password" name="pass" id="userspass" class="t2" /></dd></dl>
          <dl><dt>确认密码：</dt><dd><input type="password" id="usersconpass" name="use" class="t1"/></dd></dl>
          <dl style="display:none;"><dt>Default Aera</dt><dd><input type="text" id="usersaera" name="use" class="t1"/></dd></dl>
          <dl><dt class="agree"><input type="checkbox" checked="checked" id="checkbox" />同意的条款和条件</dt><dd class="reg"><input type="button" id="submit" name="reg" value=""/></dd></dl>
          </div>
          
        </div> 
        </form>  
       </div>
      <!---登录注册---->


      <!---登录注册---->
      <!---banner的竖菜单--->
        <div id="nav">
        <h3><i></i>产品系列</h3>
            	<ul class="tit" id="list">



                 </ul>
    </div>
    <!---banner竖菜单--->
    </div>
    <!-----banner---->
    	<div class="dex_bant">
        <div class="dex_ban">
            <div class="hd">
            <div class="prev"></div>
            <div class="next"></div>
                <ul>
                    <li></li>
                    <li></li>
                    <li></li>
                    <li></li>
                    <li></li>
                </ul>
            </div>
            <div class="bd">
            	<div class="ulWrap" id="banner">
          
            </div>

</div>
        </div>
    <!-----banner---->
        <div class="clear"></div>
    <div class="content">
    <!---第一部分----->
        <div class="h-mem clearfix" >

        	<div class="lside fl Function_bg">
            	<div class="top clearfix">
                	<ul>
                    	<li>
                        	<div class="circle fl"></div>
                            <dl class="fl">
                            	<dt>钻石会员礼包   <span>￥6880元</span></dt>
                                <dd>套餐一：琉璃至尊镀晶套装</dd>
<dd>套餐二：5盒宇航龙沉香茶、2瓶沙棘油</dd>
                            </dl>
                        </li>
                        <li class="t1">
                        	<div class="circle fl"></div>
                            <dl class="fl">
                            	<dt>金卡会员礼包    <span>￥600元</span></dt>
                                <dd>宇航龙1号和2号资料包</dd>
                                <dd>宇航龙机油组合系列</dd>

                            </dl>
                        </li>
                    </ul>
                </div>
                <div class="bot">
                	<ul id="membership">


                     
                         




                    </ul>
                </div>
            </div>





         <%--   //个人中心--%>

            <div class="rside fr">
            	<div class="top clearfix"   id="goodsInfo">

                	<%--<div class="head fl">
                    <img src="../images/head.png" />
                    </div>
                    <dl class="fl"   id="goodInfo">
                    	<dt>Eason<span>金卡会员</span></dt>
                        <dd>昵称: Eason (id: 1234)</dd>
                        <dd>积分: 1486</dd>
                    </dl>
                    <div class="clearfix"></div>
                    <div class="menu">
                	<ul>
                    	<li class="t1">
                        	<a href="#"><div class="icon"></div>
                            我的订单
                            </a>
                        </li>
                        <li class="t2">
                        	<a href="#"><div class="icon"></div>
                            我的钱包
                            </a>
                        </li>
                        <li class="t3">
                        	<a href="#"><div class="icon"></div>
                            我的推荐
                            </a>
                        </li>
                        <li class="t4">
                        	<a href="#"><div class="icon"></div>
                            我的奖励
                            </a>
                        </li>
                    </ul>
                    
                </div>--%>
                </div>




                <div class="bot Function_bg" >
                	<div class="mob">
                    	<ul  id="newstitle">
                        	<li  class="on"     >最新活动</li>
                            <li >网站公告</li>
                        </ul>
                    </div>
                   

                    <div class="tbox">
                    	<div>
                        <ul   id="activity" > 
                        <%--	<li><a href="#">2016年我国轮胎职业转型晋级显着加速... </a></li>
                            <li><a href="#">2016年我国轮胎职业转型晋级显着加速... </a></li>
                            <li><a href="#">2016年我国轮胎职业转型晋级显着加速... </a></li>--%>
                        </ul>
                        </div>
                        <div class="hide">
                        <ul   id="notice">
                        <%--	<li><a href="#">2016年我国轮胎职业转型晋级显着加速... </a></li>
                            <li><a href="#">2016年我国轮胎职业转型晋级显着加速... </a></li>
                            <li><a href="#">2016年我国轮胎职业转型晋级显着加速... </a></li>--%>
                        </ul>
                        </div>
                    </div>




                </div>
              
            </div>
        </div>
        <div class="h-sale clearfix" id="floor3">
        	<div class="hsale-l fl">
            	<img src="../images/sale.png"  style="border-radius:30px" />
            </div>
            
            
            <div class="tbox">
            <ul id="hotsale">

         

            </ul>
            </div>
            
       
       </div>
        <div class="ads">
        	<img src="../images/ads1.png" />
        </div>
     <!---第一部分----->
     <!---第二部分轮胎系列----->


       <section class="sort1 clearfix Function_bg"  id="floor4">
       	<div class="tit">
        	<h3>轮胎系列<a href="productlist.aspx?id=141"    target="_blank" class="fr">查看更多></a></h3>
            <span>Tire series</span>
        </div>
        <div class="pic fl">
        	<img src="../images/sort1.jpg" />
        </div>

       <div class="rbox">
        	
            <div class="tbox">
            	<ul id="productluntai">
                	

                </ul>
            </div>
        </div>
        
       </section>
     <!---第二部分----->
     <!---第三部分机油系列----->
       <section class="sort2 clearfix Function_bg" id="floor5">
       	<div class="tit">
        	<h3>机油系列<a href="productlist.aspx?id=94" target="_blank"  class="fr">查看更多></a></h3>
            <span>The oil series</span>
        </div>
        <div class="pic fr" >
        	<img src="../images/sort2.jpg" />
        </div>
        <div class="rbox">
        	
            <div class="tbox">
            	<ul id="productengineoil">
                	

                </ul>
            </div>
        </div>
        
       </section>
        <!---镀晶系列----->
       <section class="sort3 clearfix Function_bg" style="display:none;">
       	<div class="tit">
        	<h3>镀晶系列<a href="productlist.aspx?id=93"  target="_blank"  class="fr">查看更多></a></h3>
            <span>Plating crystal series</span>
        </div>
        <div class="pic fl">
        	<img src="../images/sort3.jpg" />
        </div>
        <div class="rbox">
        	
            <div class="tbox fl">
            	<ul id="productSealant">
                
                    
                </ul>
            </div>
        </div>
        
       </section>
       <div class="ads2">
       	<img src="../images/ads2.png" />
       </div>
      <!---养生系列----->
       <section class="sort4 clearfix fl Function_bg" id="floor7">
       	<div class="tit">
        	<h3>养生系列<a href="productlist.aspx?id=86"  target="_blank"  class="fr">查看更多></a></h3>
            <span>Series of preserve one's health</span>
        </div>
        <div class="pic fl">
        	<img src="../images/sort4.jpg" />
        </div>
        <div class="rbox">
        	
            <div class="tbox fl">
            	<ul id="producthealth">

                    
                    
                </ul>
            </div>
        </div>
        
       </section>
       <!---留言----->
       <section class="sort5 clearfix fr Function_bg" id="floor8">
       	<div class="tit">
        	<h3>在线留言</h3>
            <span>Online message</span>
        </div>
        <div class="rbox">
        	<ul>
            	<li><span class="tits">姓名:<sub>*</sub></span><input type="text" id="messagename" name="name"/></li>
                <li><span class="tits">电话:<sub>*</sub></span><input type="text" id="messagmodile"  name="name"/></li>
                <li><span class="tits">邮箱:<sub>*</sub></span><input type="text" id="messageemail"  name="name"/></li>
               
                <li><span class="tits">留言:<sub>*</sub></span><textarea id="messageliu"  rows="3"></textarea></li>
                <li><a href="#" id="tijiao" class="fl">提交</a><a href="#" class="cz fl">重置</a></li>
            </ul>
            
        </div>
        
       </section>
       <div class="clearfix"></div>
       <!---荣誉资质----->
       <section class="sort6 Function_bg clearfix" id="floor9">
       <div class="tit">
        	<h3>荣誉资质<a href="#" class="fr">查看更多></a></h3>
            <span>Honorary certificate</span>
        </div>
        <div class="picScroll-left">
			<div class="hd">
				<a class="next"></a>
				
				<a class="prev"></a>
				
			</div>
			<div class="bd">
				<ul class="picList" id="productHonor">
				

				</ul>
			</div>
		</div>
        <div class="bottom">
        	<ul>
            	<li class="li1">
                	
                	<div class="head fl">
                    
                    </div>
                    <dl class="fl">
                    	<dt>专业安装</dt>
                        <dd>专业安装服务店</dd>
                        
                    </dl>
                    

                
                </li>
                <li class="li2">
                	
                	<div class="head fl">
                    
                    </div>
                    <dl class="fl">
                    	<dt>军工正品</dt>
                        <dd>专业安装服务店</dd>
                        
                    </dl>
                    
                    
                
                </li>
                <li class="li3">
                	
                	<div class="head fl">
                    
                    </div>
                    <dl class="fl">
                    	<dt>专业安装</dt>
                        <dd>专业安装服务店</dd>
                        
                    </dl>
                    
                    
                
                </li>
                <li class="li4">
                	
                	<div class="head fl">
                    
                    </div>
                    <dl class="fl">
                    	<dt>专业安装</dt>
                        <dd>专业安装服务店</dd>

                    </dl>
               
                </li>
            </ul>
        </div>
       </section>
        <div class="clear"></div>
    </div>
</div>
    </div>
<!---底部----->
<uc1:bottom runat="server" ID="bottom" />
<script src="js/jquery.SuperSlide.2.1.1.js" type="text/javascript"></script>
<script type="text/javascript" src="js/index.js"></script>
<script type="text/javascript" src="js/home.js"></script>
    <script type="text/javascript">
        //登录ID
        var goodsId = "";

        //获取登录id
        var url = location.search;
        var Request = new Array();
        if (url.indexOf("?") != -1) {
            var str = url.substr(1);
            Request = str.split("=");
            goodsId = Request[1];
        }
        //随机验证码
        function ToggleCode(obj, codeurl) {
            $(obj).attr("src", codeurl + "?time=" + Math.random());
        }


        // //校验密码：只能输入6-20个字母、数字、下划线  
        var passwordReg = /^(\w){6,20}$/;



        var myReg = /^(((13[0-9]{1})|(14[0-9]{1})|(15[0-9]{1})|(16[0-9]{1})|(17[0-9]{1})|(18[0-9]{1}))+\d{8})$/;
        //注册
        $("#submit").click(function () {
            var check = $("input[type='checkbox']").is(':checked');
            if (check == true) {
                var mobile = $("#usersmoblie").val();
                if (mobile == "") {
                    alert("手机号不能为空");
                    $("#usersmoblie").focus();
                    return;
                }
                if (myReg.test(mobile) == false) {
                    alert("手机号格式不正确");
                    $("#usersmoblie").focus();
                    return;
                }
                var usersname = "0";
                //if (usersname == "") {
                //    alert("姓名不能为空");
                //    $("#usersname").focus();
                //    return;
                //}

                var nickname = $("#user_nick").val();


                var password = $("#userspass").val();
                var password1 = $("#usersconpass").val();




                if (passwordReg.test(password) == false) {
                    alert("只能输入6-20个字母、数字、下划线  ");
                    return;
                }

                if (password == "") {
                    alert("密码不能为空");
                    $("#userspass").focus();
                    return;
                }
                if (password1 == "") {
                    alert("请确认密码");
                    $("#usersconpass").focus();
                    return;
                }
                if (password1 != password) {
                    alert("密码输入不一致");
                    $("#usersconpass").focus();
                    return;
                }
                var usersaera = "0";

                if (nickname == "") {
                    alert("请输入昵称");
                    $("#user_nick").focus();
                    return;
                }


                $.ajax({
                    type: 'POST',
                    url: 'Service/SubmitAjax.ashx',
                    data: {
                        "param": "regist",
                        "mobile": mobile,
                        "usersname": usersname,
                        "password": password,
                        "usersaera": usersaera,
                        "usernick": nickname//用户昵称

                    },
                    async: false,
                    dataType: 'json',
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert("状态：" + textStatus + ";出错提示：" + errorThrown);
                    },
                    success: function (data) {

                        if (data.status != 0) {

                            alert("温馨提示：注册成功！");
                            window.location.href = window.location.href;
                            window.location.reload();//刷新当前页面.
                        } else {
                            alert("错误提示：注册失败！");
                            window.location.href = window.location.href;
                            window.location.reload();//刷新当前页面.
                        }
                    }
                });
            } else {
                alert("是否同意的条款和条件");
            }
        });
        //发送验证码
        function SendMsgCode(obj) {
            var mobile = $("#usersmoblie").val();
            if (mobile == "") {
                alert("手机号不能为空");
                $("#usersmoblie").focus();
                return;
            }
            if (myReg.test(mobile) == false) {
                alert("手机号格式不正确");
                $("#usersmoblie").focus();
                return;
            }

            if ($(obj).attr("isable") == "0") {
                $.ajax({
                    type: 'POST',
                    url: 'Service/SubmitAjax.ashx',
                    data: {
                        "param": "GetMobileMsgcode",
                        "mobile": mobile

                    },
                    async: false,
                    dataType: 'json',
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert("状态：" + textStatus + ";出错提示：" + errorThrown);
                    },
                    success: function (result) {

                        if (result.status == "y") {
                            //防止重复提交
                            $(".msgclass").attr("isable", "1");
                            localStorage.setItem("msg_code", result.info);
                            time(obj);
                            //alert('验证码已发送至您输入的手机号！有效期5分钟！');
                        } else {
                            alert('验证码获取失败,或该手机号已注册！');
                        }
                    }
                });

            }
        }


        //登录
        $("#login").click(function () {
            var imgCode = $("#imgCode").val();  //验证码
            var mobile = $("#username").val();  //用户账号
            if (mobile == "") {
                alert("手机号不能为空");
                $("#username").focus();
                return;
            }
            if (myReg.test(mobile) == false) {
                alert("手机号格式不正确");
                $("#username").focus();
                return;
            }
            var password = $("#userpass").val();
            if (password == "") {
                alert("密码不能为空");
                $("#userpass").focus();
                return;
            }
            $.ajax({
                type: 'POST',
                url: 'Service/SubmitAjax.ashx',
                data: {
                    "param": "login",
                    "mobile": mobile,
                    "password": password,
                    "imgCode": imgCode
                },
                async: false,
                dataType: 'json',
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert("状态：" + textStatus + ";出错提示：" + errorThrown);
                },
                success: function (data) {
                    if (data.status == 2) {
                        alert("温馨提示：会员登录成功！");
                        localStorage.removeItem("userinfo");                   //清空storage
                        localStorage.setItem("userinfo", data.msg); //设置一个键值
                        window.location.reload();//刷新当前页面.

                    } else {

                        alert(data.msg);

                    }
                }
            });




        });

        //验证码倒计时
        var wait = 300;
        function time(o) {
            if (wait == 0) {
                //                $(o).removeAttribute("disabled");
                $(o).html("发送验证码");
                $(".msgclass").attr("isable", "0");
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

        $("#tijiao").click(function () {
            var messagename = $("#messagename").val();
            var messagmodile = $("#messagmodile").val();
            var messageemail = $("#messageemail").val();
            var messageliu = $("#messageliu").val();
            if (messagename == "") {
                alert("姓名不能为空");
                $("#messagename").focus();
                return;
            }
            if (messagmodile == "") {
                alert("手机号不能为空");
                $("#messagmodile").focus();
                return;
            }
            if (messageemail == "") {
                alert("邮箱不能为空");
                $("#messageemail").focus();
                return;
            }
            if (messageliu == "") {
                alert("内容不能为空");
                $("#messageliu").focus();
                return;
            }

            $.ajax({
                type: 'POST',
                url: 'Service/SubmitAjax.ashx',
                data: {
                    "param": "message",
                    "messagename": messagename,
                    "messagmodile": messagmodile,
                    "messageemail": messageemail,
                    "messageliu": messageliu

                },
                async: false,
                dataType: 'json',
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert("状态：" + textStatus + ";出错提示：" + errorThrown);
                },
                success: function (data) {

                    if (data.status != 0) {
                        alert("温馨提示：留言成功！");
                        window.location.reload();//刷新当前页面.
                    } else {
                        alert("错误提示：留言失败请稍候再试！");
                        window.location.reload();//刷新当前页面.
                    }
                }
            });
        });
</script>
</body>
</html>

