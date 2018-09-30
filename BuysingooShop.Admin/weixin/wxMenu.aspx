<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wxMenu.aspx.cs" Inherits="BuysingooShop.Admin.weixin.wxMenu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>自定义菜单</title>
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script type="text/javascript" src="../../scripts/datepicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../scripts/swfupload/swfupload.js"></script>
    <script type="text/javascript" src="../../scripts/swfupload/swfupload.queue.js"></script>
    <script type="text/javascript" src="../../scripts/swfupload/swfupload.handlers.js"></script>
    <script type="text/javascript" charset="utf-8" src="../../editor/kindeditor-min.js"></script>
    <script type="text/javascript" charset="utf-8" src="../../editor/lang/zh_CN.js"></script>
    <script type="text/javascript" src="../js/layout.js"></script>
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <link href="../skin/mystyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        
        thead td {
            font-weight: bolder;
            text-align: center;
            font-size: 18px;
            height:30px;
        }

        tbody tr td label {
            width: 100px;
        }

        .txtNameValue, .txtNameKey, .txtNameUrl {
            padding: 1px 2px;
            line-height: 20px;
            border: 1px solid #d4d4d4;
            background: #fff;
            vertical-align: middle;
            color: #333;
            font-size: 100%;
            margin:2px 0px  2px;
        }

        .txtNameValue {
            width: 150px;
        }

        .txtNameKey {
            width: 150px;
        }

        .txtNameUrl {
            width: 200px;
            margin-right:10px;
        }

        .form_table td {
            border: 1px solid #e1e1e1;
        }

        .innertable td {
            border: 0px;
        }
        .td_shengru {
        text-align: center; width:120px;
        }
        .td_titleName {
        width:45px;
        text-align:center;
        }
        .chu {
        font-weight:bolder;
        
        }

    </style>
    <script type="text/javascript">
        $(function () {
            //初始化表单验证
            $("#form1").initValidform();
        });
    </script>


</head>

<body class="mainbody">
    <form id="form1" runat="server">
        <!--内容-->
        <!--内容-->
        <div class="content-tab-wrap">
            <div id="floatHead" class="content-tab">
                <div class="content-tab-ul-wrap">
                    <ul>
                        <li><a href="javascript:;" onclick="tabs(this);" class="selected">设置自定义菜单</a></li>
                        
                    </ul>
                </div>
            </div>
        </div>

        <div class="tab-content">
            <table class="form_table">
                <thead>
                    <tr>
                        <td class="td_shengru">深度</td>
                        <td style="text-align: center;">第一列</td>
                        <td style="text-align: center;">第二列</td>
                        <td style="text-align: center;">第三列</td>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td class="td_shengru">一级菜单按钮</td>
                        <td>

                            <table border="0" class="innertable">
                                <tr>
                                    <td class="td_titleName chu">名称：</td>
                                    <td>
                                        <asp:TextBox ID="txtTop1" runat="server" CssClass="txtNameValue"></asp:TextBox></td>
                                </tr>

                                <tr>
                                    <td class="td_titleName">关键词:</td>
                                    <td>
                                        <asp:TextBox ID="txtTop1Key" runat="server" CssClass="txtNameKey"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="td_titleName">链接:</td>
                                    <td>
                                        <asp:TextBox ID="txtTop1Url" runat="server" CssClass="txtNameUrl"></asp:TextBox></td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table border="0" class="innertable">
                                <tr>
                                    <td class="td_titleName chu">名称：</td>
                                    <td>
                                        <asp:TextBox ID="txtTop2" runat="server" CssClass="txtNameValue"></asp:TextBox></td>
                                </tr>

                                <tr>
                                    <td class="td_titleName">关键词:</td>
                                    <td>
                                        <asp:TextBox ID="txtTop2Key" runat="server" CssClass="txtNameKey"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="td_titleName">链接:</td>
                                    <td>
                                        <asp:TextBox ID="txtTop2Url" runat="server" CssClass="txtNameUrl"></asp:TextBox></td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table border="0" class="innertable">
                                <tr>
                                    <td class="td_titleName chu">名称：</td>
                                    <td>
                                        <asp:TextBox ID="txtTop3" runat="server" CssClass="txtNameValue"></asp:TextBox></td>
                                </tr>

                                <tr>
                                    <td class="td_titleName">关键词:</td>
                                    <td>
                                        <asp:TextBox ID="txtTop3Key" runat="server" CssClass="txtNameKey"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="td_titleName">链接:</td>
                                    <td>
                                        <asp:TextBox ID="txtTop3Url" runat="server" CssClass="txtNameUrl"></asp:TextBox></td>
                                </tr>
                            </table>
                        </td>

                    </tr>
                    <tr>
                        <td colspan="3">&nbsp;
                        </td>
                    </tr>

                    <tr>
                        <td class="td_shengru">二级菜单No.1</td>
                        <td>
                            <table border="0" class="innertable">
                                <tr>
                                    <td class="td_titleName chu">名称：</td>
                                    <td>
                                        <asp:TextBox ID="txtMenu11" runat="server" CssClass="txtNameValue"></asp:TextBox></td>
                                </tr>

                                <tr>
                                    <td class="td_titleName">关键词:</td>
                                    <td>
                                        <asp:TextBox ID="txtMenu11Key" runat="server" CssClass="txtNameKey"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="td_titleName">链接:</td>
                                    <td>
                                        <asp:TextBox ID="txtMenu11Url" runat="server" CssClass="txtNameUrl"></asp:TextBox></td>
                                </tr>

                            </table>
                        </td>
                        <td>
                            <table border="0" class="innertable">
                                <tr>
                                    <td class="td_titleName chu">名称：</td>
                                    <td>
                                        <asp:TextBox ID="txtMenu21" runat="server" CssClass="txtNameValue"></asp:TextBox></td>
                                </tr>

                                <tr>
                                    <td class="td_titleName">关键词:</td>
                                    <td>
                                        <asp:TextBox ID="txtMenu21Key" runat="server" CssClass="txtNameKey"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="td_titleName">链接:</td>
                                    <td>
                                        <asp:TextBox ID="txtMenu21Url" runat="server" CssClass="txtNameUrl"></asp:TextBox></td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table border="0" class="innertable">
                                <tr>
                                    <td class="td_titleName chu">名称：</td>
                                    <td>
                                        <asp:TextBox ID="txtMenu31" runat="server" CssClass="txtNameValue"></asp:TextBox></td>
                                </tr>

                                <tr>
                                    <td class="td_titleName">关键词:</td>
                                    <td>
                                        <asp:TextBox ID="txtMenu31Key" runat="server" CssClass="txtNameKey"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="td_titleName">链接:</td>
                                    <td>
                                        <asp:TextBox ID="txtMenu31Url" runat="server" CssClass="txtNameUrl"></asp:TextBox></td>
                                </tr>
                            </table>
                        </td>

                    </tr>

                    <tr>
                        <td class="td_shengru">二级菜单No.2</td>
                        <td>
                            <table border="0" class="innertable">
                                <tr>
                                    <td class="td_titleName chu">名称：</td>
                                    <td>
                                        <asp:TextBox ID="txtMenu12" runat="server" CssClass="txtNameValue"></asp:TextBox></td>
                                </tr>

                                <tr>
                                    <td class="td_titleName">关键词:</td>
                                    <td>
                                        <asp:TextBox ID="txtMenu12Key" runat="server" CssClass="txtNameKey"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="td_titleName">链接:</td>
                                    <td>
                                        <asp:TextBox ID="txtMenu12Url" runat="server" CssClass="txtNameUrl"></asp:TextBox></td>
                                </tr>

                            </table>
                        </td>
                        <td>
                            <table border="0" class="innertable">
                                <tr>
                                    <td class="td_titleName chu">名称：</td>
                                    <td>
                                        <asp:TextBox ID="txtMenu22" runat="server" CssClass="txtNameValue"></asp:TextBox></td>
                                </tr>

                                <tr>
                                    <td class="td_titleName">关键词:</td>
                                    <td>
                                        <asp:TextBox ID="txtMenu22Key" runat="server" CssClass="txtNameKey"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="td_titleName">链接:</td>
                                    <td>
                                        <asp:TextBox ID="txtMenu22Url" runat="server" CssClass="txtNameUrl"></asp:TextBox></td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table border="0" class="innertable">
                                <tr>
                                    <td class="td_titleName chu">名称：</td>
                                    <td>
                                        <asp:TextBox ID="txtMenu32" runat="server" CssClass="txtNameValue"></asp:TextBox></td>
                                </tr>

                                <tr>
                                    <td class="td_titleName">关键词:</td>
                                    <td>
                                        <asp:TextBox ID="txtMenu32Key" runat="server" CssClass="txtNameKey"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="td_titleName">链接:</td>
                                    <td>
                                        <asp:TextBox ID="txtMenu32Url" runat="server" CssClass="txtNameUrl"></asp:TextBox></td>
                                </tr>
                            </table>
                        </td>

                    </tr>


                    <tr>
                        <td class="td_shengru">二级菜单No.3</td>
                        <td>
                            <table border="0" class="innertable">
                                <tr>
                                    <td class="td_titleName chu">名称：</td>
                                    <td>
                                        <asp:TextBox ID="txtMenu13" runat="server" CssClass="txtNameValue"></asp:TextBox></td>
                                </tr>

                                <tr>
                                    <td class="td_titleName">关键词:</td>
                                    <td>
                                        <asp:TextBox ID="txtMenu13Key" runat="server" CssClass="txtNameKey"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="td_titleName">链接:</td>
                                    <td>
                                        <asp:TextBox ID="txtMenu13Url" runat="server" CssClass="txtNameUrl"></asp:TextBox></td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table border="0" class="innertable">
                                <tr>
                                    <td class="td_titleName chu">名称：</td>
                                    <td>
                                        <asp:TextBox ID="txtMenu23" runat="server" CssClass="txtNameValue"></asp:TextBox></td>
                                </tr>

                                <tr>
                                    <td class="td_titleName">关键词:</td>
                                    <td>
                                        <asp:TextBox ID="txtMenu23Key" runat="server" CssClass="txtNameKey"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="td_titleName">链接:</td>
                                    <td>
                                        <asp:TextBox ID="txtMenu23Url" runat="server" CssClass="txtNameUrl"></asp:TextBox></td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table border="0" class="innertable">
                                <tr>
                                    <td class="td_titleName chu">名称：</td>
                                    <td>
                                        <asp:TextBox ID="txtMenu33" runat="server" CssClass="txtNameValue"></asp:TextBox></td>
                                </tr>

                                <tr>
                                    <td class="td_titleName">关键词:</td>
                                    <td>
                                        <asp:TextBox ID="txtMenu33Key" runat="server" CssClass="txtNameKey"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="td_titleName">链接:</td>
                                    <td>
                                        <asp:TextBox ID="txtMenu33Url" runat="server" CssClass="txtNameUrl"></asp:TextBox></td>
                                </tr>
                            </table>
                        </td>

                    </tr>


                    <tr>
                        <td class="td_shengru">二级菜单No.4</td>
                        <td>
                            <table border="0" class="innertable">
                                <tr>
                                    <td class="td_titleName chu">名称：</td>
                                    <td>
                                        <asp:TextBox ID="txtMenu14" runat="server" CssClass="txtNameValue"></asp:TextBox></td>
                                </tr>

                                <tr>
                                    <td class="td_titleName">关键词:</td>
                                    <td>
                                        <asp:TextBox ID="txtMenu14Key" runat="server" CssClass="txtNameKey"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="td_titleName">链接:</td>
                                    <td>
                                        <asp:TextBox ID="txtMenu14Url" runat="server" CssClass="txtNameUrl"></asp:TextBox></td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table border="0" class="innertable">
                                <tr>
                                    <td class="td_titleName chu">名称：</td>
                                    <td>
                                        <asp:TextBox ID="txtMenu24" runat="server" CssClass="txtNameValue"></asp:TextBox></td>
                                </tr>

                                <tr>
                                    <td class="td_titleName">关键词:</td>
                                    <td>
                                        <asp:TextBox ID="txtMenu24Key" runat="server" CssClass="txtNameKey"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="td_titleName">链接:</td>
                                    <td>
                                        <asp:TextBox ID="txtMenu24Url" runat="server" CssClass="txtNameUrl"></asp:TextBox></td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table border="0" class="innertable">
                                <tr>
                                    <td class="td_titleName chu">名称：</td>
                                    <td>
                                        <asp:TextBox ID="txtMenu34" runat="server" CssClass="txtNameValue"></asp:TextBox></td>
                                </tr>

                                <tr>
                                    <td class="td_titleName">关键词:</td>
                                    <td>
                                        <asp:TextBox ID="txtMenu34Key" runat="server" CssClass="txtNameKey"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="td_titleName">链接:</td>
                                    <td>
                                        <asp:TextBox ID="txtMenu34Url" runat="server" CssClass="txtNameUrl"></asp:TextBox></td>
                                </tr>
                            </table>
                        </td>

                    </tr>
                    <tr>
                        <td class="td_shengru">二级菜单No.5</td>
                        <td>
                            <table border="0" class="innertable">
                                <tr>
                                    <td class="td_titleName chu">名称：</td>
                                    <td>
                                        <asp:TextBox ID="txtMenu15" runat="server" CssClass="txtNameValue"></asp:TextBox></td>
                                </tr>

                                <tr>
                                    <td class="td_titleName">关键词:</td>
                                    <td>
                                        <asp:TextBox ID="txtMenu15Key" runat="server" CssClass="txtNameKey"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="td_titleName">链接:</td>
                                    <td>
                                        <asp:TextBox ID="txtMenu15Url" runat="server" CssClass="txtNameUrl"></asp:TextBox></td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table border="0" class="innertable">
                                <tr>
                                    <td class="td_titleName chu">名称：</td>
                                    <td>
                                        <asp:TextBox ID="txtMenu25" runat="server" CssClass="txtNameValue"></asp:TextBox></td>
                                </tr>

                                <tr>
                                    <td class="td_titleName">关键词:</td>
                                    <td>
                                        <asp:TextBox ID="txtMenu25Key" runat="server" CssClass="txtNameKey"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="td_titleName">链接:</td>
                                    <td>
                                        <asp:TextBox ID="txtMenu25Url" runat="server" CssClass="txtNameUrl"></asp:TextBox></td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table border="0" class="innertable">
                                <tr>
                                    <td class="td_titleName chu">名称：</td>
                                    <td>
                                        <asp:TextBox ID="txtMenu35" runat="server" CssClass="txtNameValue"></asp:TextBox></td>
                                </tr>

                                <tr>
                                    <td class="td_titleName">关键词:</td>
                                    <td>
                                        <asp:TextBox ID="txtMenu35Key" runat="server" CssClass="txtNameKey"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="td_titleName">链接:</td>
                                    <td>
                                        <asp:TextBox ID="txtMenu35Url" runat="server" CssClass="txtNameUrl"></asp:TextBox></td>
                                </tr>
                            </table>
                        </td>

                    </tr>

                </tbody>
            </table>
        </div>
       


        <!--/内容-->

        <!--工具栏-->
        <div class="page-footer" runat="server" id="div_gongju">
            <div class="btn-list">
                 <asp:Button ID="btnSubmit" runat="server" Text="生成自定义菜单" CssClass="btn" OnClick="btnSubmit_Click" />
                 <%--<asp:Button ID="btnDelMenu" runat="server" Text="删除当前菜单" CssClass="btn yellow" OnClick="btnDelMenu_Click" />--%>
                
            </div>
            <div class="clear"></div>
        </div>
        <!--/工具栏-->
    </form>
</body>
</html>
