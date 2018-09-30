<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="BuysingooShop.Weixin.test" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Button ID="btn_pay" runat="server" OnClick="btn_pay_Click" Text="支付" class="btn_submit" />
    </div>

        <textarea  id="tblank" runat="server"></textarea>

    </form>
</body>
</html>
