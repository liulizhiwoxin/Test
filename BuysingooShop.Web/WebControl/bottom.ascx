<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="bottom.ascx.cs" Inherits="BuysingooShop.Web.WebControl.bottom" %>
<div class="footer-index">
    <div class="content c">
        <div class="clear"></div>
        <div id="customerHelp" class="w-bp w-customerHelp">
            <div class="clearfix" id="service-2014">
            </div>
            <p class="time">售前售后服务时间:周一至周六，早上9:00-下午18:00</p>
            <div class="lazy-fn lazy-fn-done">

                <div class="w">
                    <div id="footer-2014" clstag="h|keycount|2015|33a">

                        <div class="pull-left">
                            版权所有：宇航龙科技（深圳）有限公司        粤ICP备15111072号        技术支持：宇航龙科技（深圳）有限公司
                
                        </div>
                        <div class="pull-right">
                            <a rel="nofollow" target="_blank" href="http://www.bj.cyberpolice.cn/index.do">
                                <img width="103" height="32" alt="朝阳网络警察" src="../images/54b8874bN694454a5.png"
                                    class="err-product">
                            </a>
                            <a rel="nofollow" target="_blank" href="http://www.hd315.gov.cn/beian/view.asp?bianhao=010202007080200026">
                                <img width="103" height="32" alt="经营性网站备案中心" src="../images/54b8871eNa9a7067e.png"
                                    class="err-product">
                            </a>
                            <a rel="nofollow" target="_blank" id="urlknet" tabindex="-1" href="https://ss.knet.cn/verifyseal.dll?sn=2008070300100000031&amp;ct=df&amp;pa=294005">
                                <img border="true" width="103" height="32" onclick="CNNIC_change('urlknet')"
                                    oncontextmenu="return false;" name="CNNIC_seal" alt="可信网站" src="../images/54b8872dNe37a9860.png"
                                    class="err-product">
                            </a>

                            <a rel="nofollow" target="_blank" href="https://search.szfw.org/cert/l/CX20120111001803001836">
                                <img width="103" height="32" src="../images/54b8875fNad1e0c4c.png"
                                    class="err-product">
                            </a>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

<script type="text/javascript">
    $(function () {


        shopping();
    });
    //获取shopping
    function shopping() {
        var html = "";
        var htmler = "    <dl><dt><img src=\"../images/ew.jpg\" class=\"pull-right\"/></dt></dl>";
        var htmls = '  <dl><dt>友情链接</dt><dd><a href="http://www.szyuhanglong.com/" rel="nofollow" target="_top">宇航龙官网</a></dd><dd><a href="http://www.81.cn/" rel="nofollow" target="_top">中国军网</a></dd><dd><a href="http://www.gov.cn/" rel="nofollow" target="_top">中国政府网</a></dd><dd><a href="http://www.mod.gov.cn/" rel="nofollow" target="_top">中国国防网</a></dd><dd><a href="http://www.szjunqu.com" rel="nofollow" target="_top">广东军渠集团务</a></dd><dd><a href="http://www.jqcharity.org/" rel="nofollow" target="_top">军渠公益</a></dd><dd><a href="http://www.kuaidi100.com/" rel="nofollow" target="_top">快递查询</a></dd></dl>';

        $.ajax({
            type: 'POST',
            url: 'Service/AdminServiceHandler.ashx',
            data: {
                "param": "producttyreserieslist",
                "id": 121
            },
            dataType: 'json',
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("状态：" + textStatus + ";出错提示：" + errorThrown);
            },
            success: function (data) {
                if (data.status != 0) {
                    $.each(data.T_blog, function (i, item) {
                        html += "<dl>";
                        html += "   <dt>" + item.title + "</dt>";
                        var htmls = "";
                        $.ajax({
                            type: 'POST',
                            url: 'Service/AdminServiceHandler.ashx',
                            data: {
                                "param": "productHonor",
                                "id": item.id
                            },
                            async: false,
                            dataType: 'json',
                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                alert("状态：" + textStatus + ";出错提示：" + errorThrown);
                            },
                            success: function (datas) {
                                if (datas.status != 0) {
                                    $.each(datas.T_blog, function (i, items) {
                                        htmls += "<dd>";
                                        htmls += "   <a href='helps.aspx?id=" + items.id + "' rel='nofollow' target='_top'>" + items.title + "</a>";
                                        htmls += "</dd>";
                                    });
                                }
                            }
                        });
                        html += htmls;
                        html += "</dl>";
                    });
                    $("#service-2014").append(html);
                    $("#service-2014").append(htmls);
                    $("#service-2014").append(htmler);

                }
            }
        });
    }
</script>
