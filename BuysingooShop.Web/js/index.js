// JavaScript Document
$(document).ready(function() {
    $("a[href*=#top1]").click(function() {
        if (location.pathname.replace(/^\//, ') == this.pathname.replace(/^\//, ') && location.hostname == this.hostname) {
            var $target = $(this.hash);
            $target = $target.length && $target || $("[name=' + this.hash.slice(1) + ']");
            if ($target.length) {
                var targetOffset = $target.offset().top;
                $("html,body").animate({
                    scrollTop: targetOffset
                },
                1000);
                return false;
            }
        }
    });
});

//$(document).ready(function() {
//    $("a[href*=#topone],a[href*=#floor1],a[href*=#floor2],a[href*=#floor3],a[href*=#floor4],a[href*=#floor5],a[href*=#floor6],a[href*=#floor7],a[href*=#floor8],a[href*=#vail]").click(function() {
//		$(this).parent().addClass("s1").siblings().removeClass("s1");
//        if (location.pathname.replace(/^\//, ') == this.pathname.replace(/^\//, ') && location.hostname == this.hostname) {
//            var $target = $(this.hash);
//            $target = $target.length && $target || $("[name=' + this.hash.slice(1) + ']");
//            if ($target.length) {
//                var targetOffset = $target.offset().top;
//                $("html,body").animate({
//                    scrollTop: targetOffset
//                },
//                1000);
//                return false;
//            }
//        }
//    });
//});


$(function(){
	
	//页面顶部菜单显示
		$(".header-top ul li.sub").hover(function(){
			$(this).children("ul").show().parent().siblings().children("ul").hide();
			},function(){
			$(this).children("ul").hide();
			})
	//搜索框	
	$("#search,.tal_ord .tab .tsearch input,.login .box dd .t1,.login .box dd .t2").focus(function(){ 
var email_txt = $(this).val(); 
if (email_txt == this.defaultValue) { 
$(this).val(""); 
} 
}) 
$("#search,.tal_ord .tab .tsearch input,.login .box dd .t1,.login .box dd .t2").blur(function(){ 
var email_txt = $(this).val(); 
if (email_txt == "") { 
$(this).val(this.defaultValue); 
} 
}) 
});

	var $tab_li1 = $('.sort1 .menu li');
	$tab_li1.hover(function(){
		$(this).addClass('on').siblings().removeClass('on');
		var index = $tab_li1.index(this);
		$('div.tbox > div').eq(index).show().siblings().hide();
	});	
	var num = $(".dex_ban .hd ul li").length;
	var w2 = $(".dex_ban .hd ul").outerWidth();
	var w3 = $(".dex_jp .pro .hd ul").outerWidth();
	$(".dex_jp .pro .hd").css("width",w3+"px")
	$(".dex_ban .hd ul").css("margin-right",-w2/2+"px")
	$(".dex_ban").hover(
	  function () {
		$(".dex_ban .hd .prev,.dex_ban .hd .next").css("display","block"); 
	  },
	  function () {
		$(".dex_ban .hd .prev,.dex_ban .hd .next").css("display","none");
	  }
   );
   $(".dex_jp .pro").hover(
   function () {
	$(".dex_jp .pro .arrowL,.dex_jp .pro .arrowR,.dex_jp .pro .hd").css("display","block"); 
   },
   function () {
    $(".dex_jp .pro .arrowL,.dex_jp .pro .arrowR,.dex_jp .pro .hd").css("display","none");
   }
   );

	
$(function (){
  //添加注册弹窗
    var scrollHeight = $(document).scrollTop(); 
    var windowHeight = $(window).height();
    var boxHeight = $('.login .box1').height();
    var topHeight = scrollHeight+(windowHeight-boxHeight)/2;
  $('.login .box1').css({'top':topHeight});
  $('.header-top a.log,.h-mem .rside .top .login-bf dt span:eq(0)').click(function(){
    var scrollHeight = $(document).scrollTop(); 
    var windowHeight = $(window).height();
    var boxHeight = $('.login .box1').height();
    var topHeight = scrollHeight+(windowHeight-boxHeight)/2;
    $('.login .box1').css({'top':topHeight});
    $('.login').removeClass('hide');  
  });

  $(window).scroll(function(){
    //var scrollHeight = $(document).scrollTop(); 
//    var windowHeight = $(window).height();
//    var boxHeight = $('.login .box1').height();
//    var topHeight = scrollHeight+(windowHeight-boxHeight)/2;
    $('.login .box1').css({'top':topHeight});
  });
  $('.close').click(function(){
    $('.login').addClass('hide'); 
  });
  $('.header-top a.reg,.h-mem .rside .top .login-bf dt span:eq(1)').click(function(){
    var scrollHeight = $(document).scrollTop(); 
    var windowHeight = $(window).height();
    var boxHeight = $('.regist .box1').height();
    var topHeight = scrollHeight+(windowHeight-boxHeight)/2;
    $('.regist .box1').css({'top':topHeight});
    $('.regist').removeClass('hide');  
  });
   
  
  $('.close').click(function(){
    $('.regist').addClass('hide'); 
  });

    var boxHeight = $('.ads .box1').height();
	var topHeight = scrollHeight+(windowHeight-boxHeight)/2;
  $('.ads  .box1').css({'top':topHeight});
  $(".login .box .reg").click(function(){
	  var scrollHeight = $(document).scrollTop(); 
    var windowHeight = $(window).height();
    var boxHeight = $('.regist .box1').height();
    var topHeight = scrollHeight+(windowHeight-boxHeight)/2;
    $('.regist .box1').css({'top':topHeight});
	  $('.regist').removeClass('hide').siblings('.login').addClass('hide'); 
	  })
  $(".regist .box .sign").click(function(){
	  var scrollHeight = $(document).scrollTop(); 
    var windowHeight = $(window).height();
    var boxHeight = $('.login .box1').height();
    var topHeight = scrollHeight+(windowHeight-boxHeight)/2;
    $('.login .box1').css({'top':topHeight});
	  $('.login').removeClass('hide').siblings('.regist').addClass('hide'); 
	  })
  $(window).scroll(function(){
	  
   // var scrollHeight = $(document).scrollTop(); 
//    var windowHeight = $(window).height();
//    var boxHeight = $('.ads .box1').height();
//    var topHeight = scrollHeight+(windowHeight-boxHeight)/2;
    $('.ads .box1').css({'top':topHeight});
  });
  //$('.close').click(function(){
//    $('.ads').addClass('hide'); 
//  });
//产品列表页
$(".pro_ban").hover(
	  function () {
		$(".pro_ban .hd .prev,.pro_ban .hd .next").css("display","block"); 
	  },
	  function () {
		$(".pro_ban .hd .prev,.pro_ban .hd .next").css("display","none");
	  }
   );
 //产品页面1
 $(".pro_main ul:eq(0) li:nth-child(5n)").css("margin-right", 0);
    $(".pro_main .pro_list ul li:nth-child(3n)").css("margin-right", 0);
    $(".pro_main .pro_m2 li:nth-child(4n)").css("margin-right", 0);
	$(".pro_main1 ul li:nth-child(4n)").css("margin-right", 0);
$(".pro_main1 ul.nu li:nth-child(6n)").css("margin-right",0);
 
})
//添加注册弹窗
            var scrollHeight = $(document).scrollTop();
            var windowHeight = $(window).height();
            var boxHeight = $('.login .box1').height();
            var topHeight = scrollHeight + (windowHeight - boxHeight) / 2;
            $('.login .box1').css({ 'top': topHeight });
            $('ul.lside li.t1 a.log').click(function () {
                var scrollHeight = $(document).scrollTop();
                var windowHeight = $(window).height();
                var boxHeight = $('.login .box1').height();
                var topHeight = scrollHeight + (windowHeight - boxHeight) / 2;
                $('.login .box1').css({ 'top': topHeight });
                $('.login').removeClass('hide');
            });

            $(window).scroll(function () {
                var scrollHeight = $(document).scrollTop();
                var windowHeight = $(window).height();
                var boxHeight = $('.login .box1').height();
                var topHeight = scrollHeight + (windowHeight - boxHeight) / 2;
                $('.login .box1').css({ 'top': topHeight });
            });
            $('.close').click(function () {
                $('.login').addClass('hide');
            });
            $('ul.lside li.t1 a.reg').click(function () {
                var scrollHeight = $(document).scrollTop();
                var windowHeight = $(window).height();
                var boxHeight = $('.regist .box1').height();
                var topHeight = scrollHeight + (windowHeight - boxHeight) / 2 + 70;
                $('.regist .box1').css({ 'top': topHeight });
                $('.regist').removeClass('hide');
            });

            $(window).scroll(function () {
                var scrollHeight = $(document).scrollTop();
                var windowHeight = $(window).height();
                var boxHeight = $('.regist .box1').height();
                var topHeight = scrollHeight + (windowHeight - boxHeight) / 2 + 70;
                $('.regist .box1').css({ 'top': topHeight });


            });
            $('.close').click(function () {
                $('.regist').addClass('hide');
            });


$(function() {
                $(".deal .con").hover(function() {
                    $(this).addClass("dealon");

                },
                function() {
                    $(this).removeClass("dealon");
                });
				$(".side-panel .side-tab").hover(function(){
					$(this).find(".tab-tip").not($(".sign_panel")).animate({opacity:"1",right:"43px"});
					$(this).find(".tab-tip").not($(".sign_panel")).css("display","block");
					},
					function(){
					$(this).find(".tab-tip").not($(".sign_panel")).animate({opacity:"1",right:"62px"});
					$(this).find(".tab-tip").not($(".sign_panel")).css("display","none");
				});

            })
















