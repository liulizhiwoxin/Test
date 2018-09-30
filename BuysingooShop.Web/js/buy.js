// JavaScript Document
$(".hot_jp .pro ul.pict li:nth-child(6n)").css("margin-right",0);
$(".pro_main ul li:nth-child(5n)").css("margin-right",0);
$(".destab .tab_menu li:nth-child(3n)").css("margin-right",0);
$(".destab1 .tab_menu li:nth-child(2n)").css("margin-right",0);
$(".destab2 .tab_menu li:nth-child(2n)").css("margin-right",0);
$(document).ready(function(){
	var $tab_li = $('.prod-promote ul li');
	$tab_li.hover(function(){
		$(this).addClass('selected').siblings().removeClass('selected');
		var index = $tab_li.index(this);
		$('.prod-promote .tab_box > div').eq(index).show().siblings().hide();
	});	
});
$(document).ready(function(){
	var $tab_li = $('.destab ul li');
	$tab_li.hover(function(){
		$(this).addClass('selected').siblings().removeClass('selected');
		var index = $tab_li.index(this);
		$('.destab .tab_box > div').eq(index).show().siblings().hide();
	});	
});
$(document).ready(function(){
	var $tab_li = $('.destab1 ul li');
	$tab_li.hover(function(){
		$(this).addClass('selected').siblings().removeClass('selected');
		var index = $tab_li.index(this);
		$('.destab1 .tab_box > div').eq(index).show().siblings().hide();
	});	
});
$(document).ready(function(){
	var $tab_li = $('.destab2 ul li');
	$tab_li.hover(function(){
		$(this).addClass('selected').siblings().removeClass('selected');
		var index = $tab_li.index(this);
		$('.destab2 .tab_box > div').eq(index).show().siblings().hide();
	});	
});
$(".sect1 .btnn").click(function(){
	$(".sect1 .box").slideDown("fast");
	$(this).addClass("btnn2");

	});