// JavaScript Document
$(document).ready(function(){
	var $tab_li = $('.tal_r .xxi .tab ul li');
	$tab_li.click(function(){
		$(this).addClass('selected').siblings().removeClass('selected');
		$(this).siblings().children("div").addClass("sys").removeClass("sys1");
		$(this).siblings().children("div").removeClass("sys1");
		$(this).children("div").addClass("sys1").removeClass("sys");
		var index = $tab_li.index(this);
		$('div.tab_box > div').eq(index).show().siblings().hide();
	});	
	$(".tal_r .xxi .sect1 ul li:nth-child(3n)").css("padding-right","0");
	$(".talcon .recent ul li:nth-child(6n)").css("margin-right","0")
	$(".total .submenu li.dot").hover(function(){
		//$(this).parents("dd").children("span").removeClass("cov").addClass("cov1");
		},function(){
			//$(this).parents("dd").children("span").removeClass("cov1").addClass("cov");
			});
	$('#settings').click(function(){
				$('#opciones').slideToggle();
				$(this).toggleClass("memcc");
				var v = $(this).attr("status");
				//alert(v);
				if(v=1)
				{   
					//$('#opciones').slideUp();
					$('#opciones1').slideDown();
					$(this).prop("status", 2);

				}
				else
				{   $('#opciones1').slideUp();
					//$('#opciones').slideDown();
					$(this).prop("status", 1);

				}
    });
  $('#opciones1').hide();
			$('#settings1').click(function(){
				$('#opciones1').slideToggle();
				$(this).toggleClass("memcc");
    });
	var $tab_lio = $('.tal_ord .tab ul li');
	$tab_lio.hover(function(){
		$(this).addClass('selected').siblings().removeClass('selected');
		var index = $tab_lio.index(this);
		$('div.tab_box > div').eq(index).show().siblings().hide();
	});	
});
var mSwitch = new MenuSwitch("menuDiv");
mSwitch.setDefault(0);
mSwitch.setPrevious(false);
mSwitch.init();
$(".menuDiv ul li").click(function(){
	$(".menuDiv ul li").siblings().children("a").removeClass("active");
	$(this).children("a").addClass("active");  
	})
$(".menuDiv").click(function(){
	if($(this).children("ul").css("display")=="block")
	{$(this).children("div").addClass("arrow1").removeClass("arrow");}
	else
	{$(this).children("div").addClass("arrow").removeClass("arrow1");}
	});

var $tab_lic = $('.tal_coll .tab ul li');
	$tab_lic.hover(function(){
		$(this).addClass('selected').siblings().removeClass('selected');
		var index = $tab_lic.index(this);
		$('div.tab_box > div').eq(index).show().siblings().hide();
	});
	//
var $tab_lim = $('.tal_ment .tab ul li');
	$tab_lim.hover(function(){
		$(this).addClass('selected').siblings().removeClass('selected');
		var index = $tab_lim.index(this);
		$('div.tab_box > div').eq(index).show().siblings().hide();
	});	
	$(".talcon .recent ul li:nth-child(6n)").css("margin-right","0");
