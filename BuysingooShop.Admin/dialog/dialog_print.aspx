<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dialog_print.aspx.cs" Inherits="BuysingooShop.Admin.dialog.dialog_print" %>

<%@ Import Namespace="System.Data" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>打印订单窗口</title>
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script type="text/javascript">

        //窗口API
        var api = frameElement.api, W = api.opener;
        api.button({
            name: '确认打印',
            focus: true,
            callback: function () {
                printWin();
            }
        }, {
            name: '取消'
        });
        //打印方法
        function printWin() {
            var oWin = window.open("", "_blank");
            oWin.document.write(document.getElementById("content").innerHTML);
            oWin.focus();
            oWin.document.close();
            oWin.print()
            oWin.close()
        }
    </script>

    <style>
	body{ background:#fff; font-family: "宋体",Verdana;margin: 0;
  padding: 0;
  font-size: 12px;
  color: #000;}
   .invoice{ width:300px; height:227px;  position:absolute; top:50%;margin-top:-113px; left:50%; margin-left:-108px;overflow-y: auto;
  overflow-x: hidden;
  scrollbar-base-color: #F2BACE;
  scrollbar-arrow-color: #666666;
  scrollbar-darkshadow-color: #FFFFFF;
  scrollbar-highlight-color: #FFFFFF;}
   .invoice .sect{ padding:2px; width:100%; height:100%;border:1px solid #000; box-sizing:border-box; -webkit-box-sizing:border-box; -moz-box-sizing:border-box;}
  
</style>
</head>

<body class="mainbody">
    <%--<form id="form1" runat="server">--%>
    <div id="content">
        <table class="sect">
                <tbody   style="font-size:13px;    height: 28px;line-height:28px;text-align:center;padding-bottom:10px">
                    <tr>
                        <td  width="80px" >订单编号:</td>
                        <td ><%:model.order_no %></td>
                         <td style="width:120px">  </td>
                          <td style="width:80px">下单时间:</td>
                        <td ><%:model.add_time %></td>
                    </tr>
               

               <%--     <tr>
                        <td>品名:</td>
                        <td colspan="3" style="width:180px">
                            <%BuysingooShop.BLL.order_goods goods = new BuysingooShop.BLL.order_goods();
                              DataTable dt = goods.GetList(" order_id=" + model.id).Tables[0];
                              if (dt.Rows.Count > 0)
                              {
                                  foreach (DataRow item in dt.Rows)
                                  {%>
                            <%:item["goods_title"] %>×<%:item["quantity"] %>&nbsp;
                                  <%}
                          }
                                  %>
                        </td>
                      
                    </tr>--%>
                <%--    <tr>
                        <td>地址:</td>
                        <td colspan="3"><%:model.store_address %></td>
                      
                    </tr>--%>
                   <%-- <tr>
                        <td colspan="4" style="text-align: center">400-6669-162</td>
                    </tr>
                    <tr><td colspan="4" style="text-align:right;"><%:DateTime.Now.ToString("yyyy-MM-dd") %></td></tr>--%>
                </tbody>
            </table>
    <dl style="margin-top:10px">
    <dd>
      <table border="0" cellspacing="0" cellpadding="0" class="border-table" width="98%">
        <thead>
            <tr style="width:558px">
                        <th colspan="4">宇航龙商城<sup>&reg;</sup></th>
           </tr>


          <tr>
            <th>商品名称</th>
            <th width="15%">销售价</th>
            <th width="10%">数量</th>
            <th width="15%">金额合计</th>
          </tr>
        </thead>
        <tbody>
        

               <%BuysingooShop.BLL.order_goods goods = new BuysingooShop.BLL.order_goods();
                              DataTable dt = goods.GetList(" order_id=" + model.id).Tables[0];
                              if (dt.Rows.Count > 0)
                              {
                                  foreach (DataRow item in dt.Rows)
                                  {%>
              <tr class="td_c">
                                <td  >  <%:item["goods_title"] %></td>
                                <td  >  <%:item["goods_price"] %></td>
                                <td  >  <%:item["quantity"] %></td>
                               <td><%:model.order_amount %></td>
                      </tr>
                                  <%}
                          }
                                  %>
      
        </tbody>
      </table>
    </dd>
  </dl>
          <table  style="font-size:13px;    height: 28px;line-height:28px;text-align:center;padding-bottom:10px;margin-top:20px">   <tr>
                         <td  width="80px" >会员:</td>
                        <td><%:model.user_name %></td><td style="width:120px">  </td>
                            <td  width="80px" >会员名称:</td>
                        <td><%:model.accept_name %></td><td style="width:120px">  </td>
                    </tr>
                    <tr>
                    <td  width="80px"  >会员级别:</td>
                        <td><%:GetGroupNameByUserId(model.user_id) %></td><td style="width:120px">  </td>
                    <td  width="80px"  >手机:</td>
                        <td><%:model.mobile %></td>
                    </tr>

                    <tr>
                    <td  width="80px"  >备注信息:</td>
                        <td  colspan="3" style="text-align:left"><%:model.message==""?"暂无":model.message %></td>
                    </tr>
                    <tr>
                        <td  width="80px"  >收货地址:</td>
                        <td colspan="3" style="text-align:left"><%:model.address %></td>
                    </tr>
                    <tr>
                    <td  width="80px" >订单合计:</td>
                         
                             <td style="text-align:left"><%:model.order_amount %></td>
                            
                              
                              
                        
                    </tr>
          </table>
     <div style="margin-left:150px;margin-top:15px">   更多精彩内容，尽在宇航龙商城。 yhlmall.com</div> 
    </div>
    <%--</form>--%>
</body>
</html>
