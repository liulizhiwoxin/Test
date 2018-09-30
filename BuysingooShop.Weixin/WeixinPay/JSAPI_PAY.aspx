<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JSAPI_PAY.aspx.cs" Inherits="BuysingooShop.Weixin.WeixinPay.JSAPI_PAY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=GBK" />
    <link href="../css/other.css" rel="stylesheet" type="text/css" />
    <script language="javascript" src="http://res.mail.qq.com/mmr/static/lib/js/jquery.js" type="text/javascript"></script>
    <script language="javascript" src="http://res.mail.qq.com/mmr/static/lib/js/lazyloadv3.js" type="text/javascript"></script>
    <meta id="viewport" name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1; user-scalable=no;" />
    <script src="../js/jquery-1.8.0.min.js" type="text/javascript"></script>
    
</head>
<body style="background: rgb(235, 236, 237);">
    <input type="hidden" name="name" value="" id="hidBill" runat="server" />
    <script type="text/javascript">

        try {
            // 当微信内置浏览器完成内部初始化后会触发WeixinJSBridgeReady事件。
            document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
              
                WeixinJSBridge.invoke('getBrandWCPayRequest', <%=wx_packageValue %> 
            , function (res) {
               
                if (res.err_msg == "get_brand_wcpay_request:ok") {
                    alert("支付成功！");

                    //回调更新数据
                    var user = localStorage.getItem("userinfo");

                    if (user == null) {
                        alert("用户尚未登录，请先登录！");
                        window.location.href = "http://" + window.location.host + "/login.aspx";
                        return;
                    } else {
                        var jsonobj = JSON.parse(user);
                        var user_id = jsonobj._id;   
                        var user_name = jsonobj._user_name;

                        $.ajax({
                            type: 'GET',
                            url: '../Service/AdminServiceHandler.ashx',
                            data: 'param=updateUserCallBack&userid=' + user_id + '&user_name=' + user_name,
                            timeout: '120000',
                            dateType: 'json',
                            error: function (eee) {
                                //alert('系统繁忙,请稍候后再试！');
                            }, success: function (json) {
                            
                                if (json != 0) {                                
                                    localStorage.removeItem("userinfo");        //清空storage
                                    localStorage.setItem("userinfo", json);     //设置一个键值
                            
                                } 
                            }
                        });
                    }
                                       
                    window.location.href = "http://" + window.location.host + "/index.aspx?userid=" + user_id;
                    return;
                }else {
                    
                    //window.location.replace("/orderlist1.html");
                }
                // 使用以上方式判断前端返回,微信团队郑重提示：res.err_msg将在用户支付成功后返回ok，但并不保证它绝对可靠。
                //因此微信团队建议，当收到ok返回时，向商户后台询问是否收到交易成功的通知，若收到通知，前端展示交易成功的界面；若此时未收到通知，商户后台主动调用查询订单接口，查询订单的当前状态，并反馈给前端展示相应的界面。
            });
         //});

     }, false)
 } catch (e) {
     alert(e);
 }
     					  
    </script>
    <!--<a id="getBrandWCPayRequest" href="javascript:void(0);" class="needMoney_Purple">立即支付</a>-->



   
</body>
</html>
