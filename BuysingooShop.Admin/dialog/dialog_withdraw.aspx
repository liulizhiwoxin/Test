<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dialog_withdraw.aspx.cs"
    Inherits="BuysingooShop.Admin.dialog.dialog_withdraw" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>上传转账凭据窗口</title>
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script type="text/javascript" src="../js/layout.js"></script>
    <script type="text/javascript" src="../../scripts/swfupload1/swfupload.js"></script>
    <script type="text/javascript" src="../../scripts/swfupload1/swfupload.queue.js"></script>
    <script type="text/javascript" src="../../scripts/swfupload1/swfupload.handlers.js"></script>
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        //窗口API
        var api = frameElement.api, W = api.opener;
        api.button({
            name: '确定',
            focus: true,
            callback: function () {
                submitForm();
                return false;
            }
        }, {
            name: '取消'
        });

        $(function () {
            $(".upload-img").each(function () {
                $(this).InitSWFUpload({sendurl: "../../tools/upload_ajax.ashx", flashurl: "../../scripts/swfupload/swfupload.swf", filetypes: "*.jpg;*.jpge;*.png;*.gif;" });
            });
        })


        //提交表单处理
        function submitForm() {
            //组合参数
            var postData = {
                "withdraw_id": $("#hiddOrderNo").val(), "editType": "accept", "img_url": $("#txtImgUrl").val()
            };
            //判断是否需要输入物流单号
            if ($("#txtImgUrl").val() == "") {
                W.$.dialog.alert('请上传转账凭据！',
            function () {
                $("#txtImgUrl").focus();
            },
            api);
                return false;
            } else {
                //发送AJAX请求
                W.sendAjaxUrl(api, postData, "../../tools/admin_ajax.ashx?action=edit_withdraw_status");
            }
            return false;
        }
    </script>

    <style type="text/css">
    .input.normal {
    width: 200px;
    }
    .tab-content dl dt {
    display: block;
    float: left;
    width: 150px;
    text-align: right;
    color: #333;
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="tab-content">
        <dl>
            <dt>转账凭据:</dt>
            <dd>
                <input id="hiddOrderNo" type="hidden" runat="server" value=""/>
                <asp:TextBox ID="txtImgUrl" runat="server" CssClass="input normal upload-path" />
                <div class="upload-box upload-img">
                </div>
            </dd>
        </dl>
    </div>
    </form>
</body>
</html>
