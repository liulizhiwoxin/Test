$(document).ready(function () {

    // 选项卡切换
    function Tab(option,active,tabObj,siblingObj) {
        var $bp = $(option);
        $bp.click(function () {
            $(this).addClass(active).siblings().removeClass(active);
            var index = $bp.index(this);
            $(tabObj).eq(index).show().siblings(siblingObj).hide();
        });
    }

    Tab('.sexOpt>.optItem', 'active');
    Tab('.careerOpt>.optItem', 'active');


});
