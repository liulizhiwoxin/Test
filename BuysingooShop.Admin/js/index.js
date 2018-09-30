// JavaScript Document
$(function() {
    $(".search-text").focus(function() {
        $(".hot-words").css("display", "none");
    });

    $(".search-text").blur(function() {
        $(".hot-words").css("display", "block");
    });

    $(".site-header .nav .menu").hover(function() {
            $(this).children("ul").addClass("show").removeClass("hide");
        }, function() {
            $(this).children("ul").addClass("hide").removeClass("show");
        }
    );

    $(".site-header .nav ul.s_nav li").hover(function() {
            $(this).children("ul").slideDown("fast");
        }, function() {
            $(this).children("ul").slideUp("fast");
        }
    );

    $(".hot").hover(function() {
            $(this).children(".hot .prev1,.hot .next1").addClass("show").removeClass("hide");
        }, function() {
            $(this).children(".hot .prev1,.hot .next1").addClass("hide").removeClass("show");
        }
    );

    $(window).bind("scroll", function() {
        var d = $(document).scrollTop();
        if (d > 830) {
            $(".new").animate({ "margin-left": '0' }, "slow");
            $(".hot_d").animate({ "margin-right": '0' }, "slow");
        }


    });
    $(".cart_list table tbody tr td:nth-child(1n)").css("border-left", "1px solid #2f7536");
    $(".cart_list table tbody tr td:nth-child(6n)").css("border-right", "1px solid #2f7536");
    $(".pro_hot li:nth-child(3)").css("border", "0");
    $(".cart_list table tbody tr td:first").css("border-top", "none");

    $(".picList li").hover(function() {
        $(this).next().css("border-right")
        sl = $('.picList').position().left;
        var num = -(sl / 273);
        if ($(this).index() == num) {
            $(this).parents(".bd").css("border-left", "1px solid #ff9c00");
        } else {
            $(this).parents(".bd").css("border-left", "1px solid #b3b3b3");
        }
    });

    $(document).ready(function() {
        $('#settings').click(function() {
            $('#opciones').slideToggle();
            $(this).toggleClass("memcc");
            var v = $(this).attr("status");
            //alert(v);
            if (v = 1) {
                //$('#opciones').slideUp();
                $('#opciones1').slideDown();
                $(this).prop("status", 2);

            } else {
                $('#opciones1').slideUp();
                //$('#opciones').slideDown();
                $(this).prop("status", 1);

            }
        });
        $('#opciones1').hide();
        $('#settings1').click(function() {
            $('#opciones1').slideToggle();
            $(this).toggleClass("memcc");
        });
    });
    $(document).ready(function() {
        var $tab_li = $('.tal_ment .tab ul li');
        $tab_li.hover(function() {
            $(this).addClass('selected').siblings().removeClass('selected');
            var index = $tab_li.index(this);
            $('div.tab_box > div').eq(index).show().siblings().hide();
        });
        $(document).ready(function() {
            var $tab_li = $('.tal_coll .tab ul li');
            $tab_li.hover(function() {
                $(this).addClass('selected').siblings().removeClass('selected');
                var index = $tab_li.index(this);
                $('div.tab_box > div').eq(index).show().siblings().hide();
            });
        });
    });

    $(".site-header .nav ul.s_nav li").hover(
	function () {
	    $(this).children("ul").stop(true, true).slideDown("fast");
	},
	function () {
	    $(this).children("ul").stop(true, true).slideUp("fast");
	}
	);

    $(".xxi  .sect4 table td:nth-child(3n),.xxi  .sect4 table td:nth-child(4n)").css("color", "#2f7536");

    //选择大米类型和重量
    $(".sel_pro dl.item1 dd").click(function() {
        $(this).addClass("selected").siblings().removeClass("selected");
        //alert($(this).find("h4").text());
        $(".t_num span:first").text($(this).find("h4").text());
        $("#hideId").attr("datatype", $(this).find("h4").text()).attr("dataprice", $(this).find(".descp span").text());
    });
    $(".sel_pro dl.item2 dd").click(function() {
        $(this).addClass("selected").siblings().removeClass("selected");
        $(".t_num span:eq(1)").text($(this).find("input").val());
        $(".t_num span:eq(2)").text("X");
        $(".t_num span:last").text($(this).find("span").text());
        $("#hideId").attr("datanum", $(this).find("input").val()).attr("dataweight", $(this).find("span:first").text());

    });

    $(".r_login li input.user,.r_login li input.pass,.r_login li input.phone,.r_login li input.vail").focus(function() {
        var email_txt = $(this).val();
        if (email_txt == this.defaultValue) {
            $(this).val("");
        }
    });
    $(".r_login li input.user,.r_login li input.pass,.r_login li input.phone,.r_login li input.vail").blur(function() {
        var email_txt = $(this).val();
        if (email_txt == "") {
            $(this).val(this.defaultValue);
        }
    });
    $(".r_login li .down").hover(function() {
        $(".r_login li.erwei").css("display", "block")
    }, function() {
        $(".r_login li.erwei").css("display", "none")
    });

});

$(function () {
    $(".tal_set .item li .mail,.tal_set .item li .passbtn").click(function () {
        $(this).parent().find("table").show();
    })
})



