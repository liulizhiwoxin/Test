/*
 * File:        JQSelect.js
 * Version:     1.0
 * Description: html select.
 * Author:      airita
*/

(function($) {
    var defaults = {
        showLength: 1, //初始显示的option数量
        option_click: function() {//绑定li点击事件
            $(this).parents(".js-scroll").find("input").val($(this).html());
            $(this).parents(".js-scroll").find("input").attr("val", $(this).attr("val"))
            $(this).parents(".js-scroll-content").hide();
        }
    };
    var Scrollfns = {
        changeBarPosition: function($obj, moveDis, scrollBarHeight, barBgHeight, selectHeight) {
            //change scrollbar
            if (moveDis < 0) { moveDis = 0 }
            if (moveDis > (barBgHeight - scrollBarHeight)) { moveDis = barBgHeight - scrollBarHeight }
            $obj.css("top", moveDis + "px");
            //change barBg
            var bgMoveDis = -(moveDis * (selectHeight - barBgHeight) / (barBgHeight - scrollBarHeight));
            $obj.siblings(".js-scroll-content-body").css("top", bgMoveDis + "px");
        }
    };
    $.fn.extend({
        mousewheel: function(fn) {
            return this.each(function() {
                this.direction = 0;
                if ($.browser.mozilla) {
                    $(this)[0].addEventListener("DOMMouseScroll", function(e) {
                        this.direction = e.detail > 0 ? -1 : 1; //-1代表向下滚动
                        e.preventDefault()
                        fn.call(this);
                    }, false);
                }
                else {
                    $(this)[0].onmousewheel = function() {
                        this.direction = event.wheelDelta;
                        event.returnValue = false;
                        fn.call(this);
                    }
                }
            })
        }
    });
    $.fn.extend({
        jsScroll: function(options) {
            var opts = $.extend({}, defaults, options);
            this.each(function() {
                if ($(this)[0].tagName.toString().toLowerCase() == 'select') {
                    var opLength = $(this).find("option").length;
                    var selectedValue = $(this).val();
                    if (opLength > 0) {
                        var divID = $(this).attr("id");
                        var divClass = $(this).attr("class");
                        var divIDHtml = divID == "" ? "" : "id='" + divID + "'";
                        var divClassHtml = divClass == "" ? "js-scroll" : "js-scroll " + divClass;
                        //create backgroundDiv
                        var jsScrollText = "<div class='js-scroll-text'><div class='l'></div><div class='c'><input type='text' val='" + selectedValue + "' disabled='disabled'><a class='img-on'></a></div><div class='r'></div></div><div style='clear:both'></div>";

                        var jsScrollContent = "<div class='js-scroll-content'><div class='contentbg-top'></div><div class='contentbg-middle'></div><div class='contentbg-bottom'></div></div>"
                        var jsScroll = "<div " + divIDHtml + " class='" + divClassHtml + "'>" + jsScrollText + jsScrollContent + "</div>";
                        $(this).after(jsScroll);

                        //create content
                        var options = "<div class='js-scroll-content-body'><ul>";
                        for (var i = 0; i < opLength; i++) {
                            var $op = $(this).find("option").eq(i);
                            options += "<li class='liop' val='" + $op.attr("value") + "'>" + $op.html() + "</li>\n";
                        }
                        options += "</ul></div>";

                        var $js_scroll_self = $(this).next();
                        $js_scroll_self.find(".contentbg-middle").append(options);

                        var $liop = $js_scroll_self.find(".liop")
                        var liHeight = $liop.height();
                        var liPaddingTop = parseInt($liop.css("padding-top"));
                        var liPaddingBottom = parseInt($liop.css("padding-bottom"));
                        var midHeight;

                        if (opLength >= opts.showLength) {
                            midHeight = opts.showLength * liHeight + opts.showLength * liPaddingBottom + opts.showLength * liPaddingBottom;
                            $js_scroll_self.find(".contentbg-middle").height(midHeight + "px");

                            //create barbg
                            var jsBarBg = "<div class='js-scroll-barbg' style='top:0px;'><div class='barbg-top'></div><div class='barbg-middle'></div><div class='barbg-bottom'></div></div>";
                            $(this).next().find(".contentbg-middle").append(jsBarBg);
                            var barBgHeight = midHeight - $js_scroll_self.find(".barbg-top").height() - $js_scroll_self.find(".barbg-bottom").height();
                            $js_scroll_self.find(".barbg-middle").height(barBgHeight);

                            //create scrollbar
                            var jsScrollBar = "<div class='js-scroll-scrollbar' style='top:0px;margin:0'><div class='scrollbar-top'></div><div class='scrollbar-middle'></div><div class='scrollbar-bottom'></div></div>";
                            $js_scroll_self.find(".contentbg-middle").append(jsScrollBar);
                            $(this).remove();

                            //scroll
                            var moveDis = 0;
                            var scrollBarHeight = $js_scroll_self.find(".js-scroll-scrollbar").height();
                            var barBgHeight = $js_scroll_self.find(".js-scroll-barbg").height();
                            var selectHeight = $js_scroll_self.find(".js-scroll-content-body").height();
                            $js_scroll_self.find(".js-scroll-scrollbar").mousedown(function(e) {
                                var mouseOldY = e.pageY;
                                var this_top_dis = parseInt($(this).css("top"));
                                $(document).bind("mousemove", function(e) {
                                    var mouseNewY = e.pageY;
                                    moveDis = mouseNewY - mouseOldY + parseInt(this_top_dis);
                                    Scrollfns.changeBarPosition($js_scroll_self.find(".js-scroll-scrollbar"), moveDis, scrollBarHeight, barBgHeight, selectHeight);
                                })
                                $(document).mouseup(function() {
                                    $(this).unbind("mousemove");
                                })
                            })

                            //mousewheel
                            $js_scroll_self.mousewheel(function() {
                                if (this.direction != 0) {
                                    moveDis += this.direction > 0 ? (-10) : 10;
                                }
                                Scrollfns.changeBarPosition($(this).find(".js-scroll-scrollbar"), moveDis, scrollBarHeight, barBgHeight, selectHeight);

                            })
                            $js_scroll_self.find(".js-scroll-content").hide();

                            $js_scroll_self.find(".js-scroll-text .c a").click(function() {
                                var $content = $(this).parents(".js-scroll").find(".js-scroll-content");
                                if ($content.is(":visible")) {
                                    $content.hide();
                                }
                                else {
                                    $content.show();
                                }
                            })
                        }

                        $js_scroll_self.hover(function() {

                        }, function() {
                            $(this).find(".js-scroll-content").hide();
                        })

                        //bind li click
                        $liop.click(function() {
                            if ($.isFunction(opts.option_click)) {
                                opts.option_click.call(this);
                            }
                            else {
                                throw "option_clic isn't fn";
                            }
                        })

                        //bind 箭头 mouseover
                        $js_scroll_self.find(".c a").hover(function() {
                            $(this).addClass("arrow-over");
                        }, function() {
                            $(this).removeClass("arrow-over");
                        })

                    }
                }
                return;
            })
        }
    })
})(jQuery)