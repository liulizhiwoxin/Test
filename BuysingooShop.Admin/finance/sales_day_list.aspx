<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sales_day_list.aspx.cs" Inherits="BuysingooShop.Admin.finance.sales_day_list" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>财务日报表</title>

    <script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script type="text/javascript" src="../js/layout.js"></script>
    <link href="../../css/pagination.css" rel="stylesheet" type="text/css" />
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
         
    <script src="../../scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>

</head>

<body class="mainbody">
    <form id="form1" runat="server">
        <!--导航栏-->
        <div class="location">
            <a href="javascript:history.back(-1);" class="back"><i></i><span>返回上一页</span></a>
            <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
            <i class="arrow"></i>
            <span>财务日报表</span>
        </div>
        <!--/导航栏-->

        <!--工具栏-->
        <div class="toolbar-wrap">
            <div id="floatHead" class="toolbar">
                <div class="l-list">
                    <ul class="icon-list">                        
                        <li>
                            <input id="ipt_Time" name="ipt_Time" type="text" runat="server" onclick="WdatePicker({ skin: 'whyGreen', minDate: '2015-01-20', maxDate: '2020-12-30' })" />
                        </li>
                        <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="btn-search" OnClick="btnSearch_Click">查询</asp:LinkButton>
                    </ul>
                </div>
                <div class="r-list">
                    
                </div>
            </div>
        </div>
        <!--/工具栏-->

        <!--列表-->
        <asp:Repeater ID="rptList" runat="server">
            <HeaderTemplate>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="ltable">
                    <tr>   
                        <th align="center" width="10%">姓名</th>                                            
                        <th align="center" width="10%">手机号</th>                       
                        <th align="center" width="15%">联系电话</th>
                        <th align="center" width="16%">下单时间</th>
                        <th align="center" width="15%">下单金额</th>                                                
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>   
                    <td align="center"><%#Eval("nick_name")%></td>                  
                    <td align="center"><%#Eval("user_name")%></td>                    
                    <td align="center"><%#Eval("mobile")%></td>
                    <td align="center"><%#string.Format("{0:g}", Eval("add_time"))%></td>
                    <td align="center"><%#Eval("real_amount")%></td>                                     
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"5\">暂无记录</td></tr>" : ""%>
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
