<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="lanzuan.aspx.cs" Inherits="BuysingooShop.Weixin.lanzuan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <title>门川家居</title>
    <link rel="stylesheet" href="css/common.css?v=20180410">
    <link rel="stylesheet" href="css/login.css?v=20180410">

    <script type="text/javascript">

        function btnOK() {
            window.location.href = "http://" + window.location.host + "/login_reg.aspx";
        }

    </script>
</head>
<body>
    <div id="lanzuan">
	<img src="images/lanzuan.png"/>
	<a href="javascript:btnOK()">去注册打赏、赢得小恶魔</a>
</div>
</body>
</html>
