﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="outlet_user_level.aspx.cs" Inherits="BuysingooShop.Admin.outlet.outlet_user_level" %>

<%@ Import Namespace="Vincent" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>用户管理</title>
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script type="text/javascript" src="../js/layout.js"></script>
    <link href="../../css/pagination.css" rel="stylesheet" type="text/css" />
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        //发送短信
        function PostSMS(mobiles) {
            if (mobiles == "") {
                $.dialog.alert('对不起，手机号码不能为空！');
                return false;
            }
            var dialog = $.dialog({
                title: '发送手机短信',
                content: '<textarea id="txtSmsContent" name="txtSmsContent" rows="2" cols="20" class="input"></textarea>',
                min: false,
                max: false,
                lock: true,
                ok: function () {
                    var remark = $("#txtSmsContent", parent.document).val();
                    if (remark == "") {
                        $.dialog.alert('对不起，请输入手机短信内容！', function () { }, dialog);
                        return false;
                    }
                    var postData = { "mobiles": mobiles, "content": remark };
                    //发送AJAX请求
                    $.ajax({
                        type: "post",
                        url: "../../tools/admin_ajax.ashx?action=sms_message_post",
                        data: postData,
                        dataType: "json",
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            $.dialog.alert('尝试发送失败，错误信息：' + errorThrown, function () { }, dialog);
                        },
                        success: function (data, textStatus) {
                            if (data.status == 1) {
                                dialog.close();
                                $.dialog.tips(data.msg, 2, '32X32/succ.png', function () { location.reload(); }); //刷新页面
                            } else {
                                $.dialog.alert('错误提示：' + data.msg, function () { }, dialog);
                            }
                        }
                    });
                    return false;
                },
                cancel: true
            });
        }
    </script>
</head>

<body class="mainbody">
    <form id="form1" runat="server">
        <!--导航栏-->
        <div class="location">
            <a href="javascript:history.back(-1);" class="back"><i></i><span>返回上一页</span></a>
            <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
            <i class="arrow"></i>
            <span>会员管理</span>
            <i class="arrow"></i>
            <span>用户列表</span>
        </div>
        <!--/导航栏-->

        <!--工具栏-->
        <div class="toolbar-wrap">
            <div id="floatHead" class="toolbar">
                <div class="l-list">
                    <ul class="icon-list">
                        <li><a class="add" href="user_edit.aspx?action=<%=Vincent._DTcms.DTEnums.ActionEnum.Add %>"><i></i><span>新增</span></a></li>
                        <li>
                            <asp:LinkButton ID="btnSmsPost" runat="server" CssClass="list" OnClientClick="return CheckPostBack('btnSmsPost');" OnClick="btnSmsPost_Click"><i></i><span>短信</span></asp:LinkButton></li>
                        <li><a class="all" href="javascript:;" onclick="checkAll(this);"><i></i><span>全选</span></a></li>
                        <li>
                            <asp:LinkButton ID="btnDelete" runat="server" CssClass="del" OnClientClick="return ExePostBack('btnDelete');" OnClick="btnDelete_Click"><i></i><span>删除</span></asp:LinkButton></li>
                    </ul>
                    <div class="menu-list">
                        <div class="rule-single-select">
                            <asp:DropDownList ID="ddlGroupId" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlGroupId_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="r-list">
                    <asp:TextBox ID="txtKeywords" runat="server" CssClass="keyword" />
                    <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="btn-search" OnClick="btnSearch_Click">查询</asp:LinkButton>
                </div>
            </div>
        </div>
        <!--/工具栏-->

        <!--列表-->
        <asp:Repeater ID="rptList" runat="server">
            <HeaderTemplate>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="ltable">
                    <tr>
                        <th width="4%">选择</th>
                        <th width="4%">编号</th>
                        <th align="left" colspan="2">用户名</th>
                        <th align="left" width="12%">会员组</th>
                        <th align="left" width="12%">添加时间</th>
                        <th align="left" width="12%">邮箱</th>
                        <th width="4%" align="">余额</th>
                        <th width="4%" align="">积分</th>
                        <th width="4%" align="">状态</th>
                        <th width="6%"  align="left">他的上线</th>
                        <th width="10%" align="center" colspan="2">操作</th>
                    
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td align="center">
                        <asp:CheckBox ID="chkId" CssClass="checkall" runat="server" Style="vertical-align: middle;" />
                        <asp:HiddenField ID="hidId" Value='<%#Eval("id")%>' runat="server" />
                    </td>
                    <td align="center"><%#Eval("id")%></td>
                    <td width="64">
                        <a href="user_edit.aspx?action=<%#Vincent._DTcms.DTEnums.ActionEnum.Edit %>&id=<%#Eval("id")%>">
                            <%#Eval("avatar").ToString() != "" ? "<img width=\"64\" height=\"64\" src=\"" + Eval("avatar") + "\" />" : "<b class=\"user-avatar\"></b>"%>
                        </a>
                    </td>
                    <td>
                        <div class="user-box">
                            <h4><b><%#Eval("user_name")%></b> (昵称：<%#Eval("nick_name")%>)</h4>
                            <i>注册时间：<%#string.Format("{0:g}",Eval("reg_time"))%></i>
                            <span>
                                <a class="amount" href="outlet_amount_log.aspx?keywords=<%#Eval("user_name")%>" title="消费记录">余额</a>
                                <a class="point" href="outlet_point_log.aspx?keywords=<%#Eval("user_name")%>" title="积分记录">积分</a>
                                <a class="msg" href="outlet_message_list.aspx?keywords=<%#Eval("user_name")%>" title="消息记录">短消息</a>
                                <%#Eval("mobile").ToString() != "" ? "<a class=\"sms\" href=\"javascript:;\" onclick=\"PostSMS('" + Eval("mobile") + "');\" title=\"发送手机短信通知\">短信通知</a>" : ""%>
                            </span>
                        </div>
                    </td>
                    <td><%#new BuysingooShop.BLL.user_groups().GetTitle(Convert.ToInt32(Eval("group_id")))%></td>
                    <td><%#Eval("reg_time")%></td>
                    <td><%#Eval("email")%></td>
                    <td align="center"><%#Eval("amount")%></td>
                    <td align="center"><%#Eval("point")%></td>
                    <td align="center"><%#GetUserStatus(Convert.ToInt32(Eval("status")))%></td>

                  
                    <td><%#GetUserPreName(Convert.ToInt32(Eval("id")))   %></td>

                    <td align="center">
                        <a href="outlet_user_level.aspx?action=<%#Vincent._DTcms.DTEnums.ActionEnum.Edit %>&id=<%#Eval("id")%>">他的下线</a>&nbsp;&nbsp;
                    </td>
                      <td align="center">
                        <a href="outlet_user_details.aspx?action=<%#Vincent._DTcms.DTEnums.ActionEnum.Edit %>&id=<%#Eval("id")%>">会员详情</a>&nbsp;&nbsp;
                    </td>

                </tr>
            </ItemTemplate>
            <FooterTemplate>
                <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"9\">暂无记录</td></tr>" : ""%>
</table>
            </FooterTemplate>
        </asp:Repeater>
        <!--/列表-->

        <!--内容底部-->
        <div class="line20"></div>
        <div class="pagelist">
            <div class="l-btns">
                <span>显示</span><asp:TextBox ID="txtPageNum" runat="server" CssClass="pagenum" onkeydown="return checkNumber(event);" OnTextChanged="txtPageNum_TextChanged" AutoPostBack="True"></asp:TextBox><span>条/页</span>
            </div>
            <div id="PageContent" runat="server" class="default"></div>
        </div>
        <!--/内容底部-->
    </form>
</body>
</html>
