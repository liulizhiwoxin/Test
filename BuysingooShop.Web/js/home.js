// JavaScript Document
$(function(){
	//banner动效
	jQuery(".dex_ban").slide({
                titCell: ".hd ul",
                mainCell: ".bd .ulWrap",
                autoPage: true,
                effect: "leftLoop",
                autoPlay: true,
                vis: 1,
				interTime:5000
            });
	var num = $(".dex_ban .hd ul li").length;
	var w2 = $(".dex_ban .hd ul").outerWidth();
	var w3 = $(".dex_jp .pro .hd ul").outerWidth();
	$(".dex_jp .pro .hd").css("width",w3+"px")
	$(".dex_ban .hd ul").css("margin-right",-w2/2+"px");
	
	var $tab_lih = $('.h-mem .mob li');
	$tab_lih.hover(function(){
		$(this).addClass('on').siblings().removeClass('on');
		var index = $tab_lih.index(this);
		$('.h-mem div.tbox > div').eq(index).show().siblings().hide();
	});	
	//首页几个横向滑动产品动效
	jQuery(".h-mem .lside").slide({mainCell:".bot ul",autoPage:true,effect:"leftLoop",autoPlay:true,vis:3,trigger:"click"});
	jQuery(".h-sale").slide({mainCell:".tbox ul",autoPage:true,effect:"leftLoop",autoPlay:true,vis:3,trigger:"click"});
	jQuery(".sort3").slide({mainCell:".tbox ul",autoPage:true,effect:"leftLoop",autoPlay:false,vis:3,trigger:"click"});
	jQuery(".sort4").slide({mainCell:".tbox ul",autoPage:true,effect:"leftLoop",autoPlay:false,vis:2,trigger:"click"});
	//资质证书
	
	jQuery(".picScroll-left").slide({titCell:".hd ul",mainCell:".bd ul",autoPage:true,effect:"leftLoop",autoPlay:true,vis:5,trigger:"click"});
	//首页左侧和右侧浮动菜单
	var is_set_top = false;

            //左侧导航相对高度
            var top_list = [];

            function again_compute_top() {
                $('.Function_bg').each(function(i) {
                    top_list[i] = $(this).offset().top;
                });
            }
            again_compute_top();

            //解决左侧导航小屏问题
            //var client_height = $(window).height();
            // $('.BannerConten_left_nav').height(client_height - 82);
            var count = $(".BannerConten_left_nav .Weixin_FunctionList_Content").length;
            $('.BannerConten_left_nav').height(51 * count);

            // 窗体滚动处理
            $(window).scroll(function() {
                var offset = $(".Function_tabs_frame").offset();
                if (offset.top <= $(window).scrollTop()) {
                    $(".Function_tabs_box").addClass("Function_tabs_fixed");
                    is_set_top = true;
                } else {
                    $(".Function_tabs_box").removeClass("Function_tabs_fixed");
                    is_set_top = false;
                }

                for (var i = 0; i < top_list.length; i++) {
                    if (top_list[i] >= $(window).scrollTop()) {
						
                        
                        $('.Weixin_FunctionList_Name').removeClass("Weixin_FunctionList_NameChecked");
                        $('.Weixin_FunctionList_ContentBox .Weixin_FunctionList_Content').eq(i).find('.Weixin_FunctionList_Name').addClass('Weixin_FunctionList_NameChecked');

                        var nav_height = $('.BannerConten_left_nav').height();
                        var left_height = $('.Weixin_FunctionList_Course').height();
                        var check_top = $('.Weixin_FunctionList_Course').find('.Weixin_FunctionList_CircleChecked').position();
                        if (nav_height < left_height) {
                            if (check_top.top > (left_height / 2)) {
                                $('.Weixin_FunctionList_Course').css("top", (nav_height - left_height) - 25);
                            } else {
                                $('.Weixin_FunctionList_Course').css("top", 0);
                            }
                        }
                        return;
                    }
                }
          });

            //左侧导航
            $('.Weixin_FunctionList_Content').click(function() {
                var index = $(this).index();
				$(this).children().addClass('Weixin_FunctionList_NameChecked').parent().siblings().children().removeClass('Weixin_FunctionList_NameChecked')
                $('html,body').animate({
                    scrollTop: top_list[index]-70
                },
                160);
            });
            // 功能选择显示
            $(".quick_links_panel li").mouseenter(function(){
		$(this).children(".mp_tooltip").animate({left:-117,queue:true});
		$(this).children(".mp_tooltip").css("visibility","visible");
		$(this).children(".ibar_login_box").css("display","block");
	});
	$(".quick_links_panel li").mouseleave(function(){
		$(this).children(".mp_tooltip").css("visibility","hidden");
		$(this).children(".mp_tooltip").animate({left:-146,queue:true});
		$(this).children(".ibar_login_box").css("display","none");
	});
	$(".quick_toggle li").mouseover(function(){
		$(this).children(".mp_qrcode").show();
	});
	$(".quick_toggle li").mouseleave(function(){
		$(this).children(".mp_qrcode").hide();
	}); 
	//首页菜单
	$("#nav .tit").slide({
	type:"menu",
	titCell:".mod_cate",
	targetCell:".mod_subcate",
	delayTime:0,
	triggerTime:10,
	defaultPlay:false,
	returnDefault:true
});
	})