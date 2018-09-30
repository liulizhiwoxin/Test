<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="download.aspx.cs" Inherits="BuysingooShop.Weixin.download" %>

<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="BuysingooShop.BLL" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">

    <title>资源列表</title>
    <link rel="stylesheet" href="css/common.css">
    <link rel="stylesheet" href="css/vip.css?v=20180515">
</head>
<body>

    <div class="download">
        <ul>
            <% DataTable allNews = new article_category().GetList(1, 1);
                if (allNews.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow item in allNews.Rows)
                    { %>

                        <li><a href="<%=item["link_url"]%>"><%=item["title"]%></a></li>            
            <%     
                    }
                }
            %>


            <%--<li><a href="javascript:">B01 大师作品</a></li>
        <li><a href="javascript:">B02 软装素材</a></li>
        <li><a href="javascript:">B03 供应商素材</a></li>
        <li><a href="javascript:">B04 2018概念方案大全</a></li>
        <li><a href="javascript:">B05 色彩搭配</a></li>
        <li><a href="javascript:">B06 概念氛围</a></li>
        <li><a href="javascript:">B07 样板房软装</a></li>
        <li><a href="javascript:">B07 B08 别墅豪宅</a></li>
        <li><a href="javascript:">B09 家装设计</a></li>
        <li><a href="javascript:">B10 品牌酒店</a></li>
        <li><a href="javascript:">B11 酒店风格分类</a></li>
        <li><a href="javascript:">B12 精品酒店</a></li>
        <li><a href="javascript:">B13 办公空间</a></li>
        <li><a href="javascript:">B14 餐厅茶楼咖啡</a></li>
        <li><a href="javascript:">B15 公共文体</a></li>
        <li><a href="javascript:">B16 售楼中心</a></li>
        <li><a href="javascript:">B17 会所软装</a></li>
        <li><a href="javascript:">B18 商场商店</a></li>
        <li><a href="javascript:">B19 展厅展示</a></li>
        <li><a href="javascript:">B20 KTV酒吧</a></li>
        <li><a href="javascript:">B21 美容美发</a></li>
        <li><a href="javascript:">B22 空间分类</a></li>
        <li><a href="javascript:">B23 彩平彩立PSD</a></li>
        <li><a href="javascript:">B24 软装培训</a></li>
        <li><a href="javascript:">B25 软装风水</a></li>
        <li><a href="javascript:">B26 灯光设计</a></li>
        <li><a href="javascript:">B27 风格解析</a></li>
        <li><a href="javascript:">B28 公司管理</a></li>
        <li><a href="javascript:">B29 物料表格</a></li>
        <li><a href="javascript:">B30 国外杂志</a></li>
        <li><a href="javascript:">B31 软装书籍</a></li>
        <li><a href="javascript:">B32 手绘学习</a></li>
        <li><a href="javascript:">B33 方案排版</a></li>
        <li><a href="javascript:">B34 合同预算</a></li>
        <li><a href="javascript:">B35 软件技巧</a></li>--%>
        </ul>
    </div>

</body>
</html>
