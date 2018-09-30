<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dialog_complian.aspx.cs" Inherits="BuysingooShop.Admin.dialog.dialog_complian" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>修改收货信息窗口</title>
<script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
<script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
<script type="text/javascript" src="../../scripts/jquery/PCASClass.js"></script>
<script type="text/javascript" src="../js/layout.js"></script>
<link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
</head>

<body>
<div class="div-content">
    <dl>
      <dt>投诉人姓名</dt>
      <dd><input type="text" id="txtUserName" class="input txt" readonly="readonly" value="<%=model.user_name%>" /></dd>
    </dl>
    <dl>
      <dt>联系电话</dt>
      <dd><input type="text" id="txtTelphone" class="input txt" readonly="readonly" value="<%=model.mobile_phone%>" /></dd>
    </dl>
    <dl>
      <dt>投诉标题</dt>
      <dd><input type="text" id="TextTitle" class="input txt" readonly="readonly" value="<%=model.complian_title%>" /></dd>
    </dl>
    <dl>
      <dt>投诉时间</dt>
      <dd><input type="text" id="txtTime" class="input txt" readonly="readonly" value="<%=model.complian_time%>" /></dd>
    </dl>
    <dl>
      <dt>投诉内容</dt>
      <dd><textarea id="txtarContent" cols="20" rows="2" readonly="readonly" class="input normal" ><%=model.complian_content%></textarea></dd>
    </dl>
</div>
</body>
</html>