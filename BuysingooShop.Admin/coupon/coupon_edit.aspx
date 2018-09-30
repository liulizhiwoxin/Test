<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="coupon_edit.aspx.cs" Inherits="BuysingooShop.Admin.coupon.coupon_edit"
    ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>编辑信息</title>
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
    <script type="text/javascript">
        $(function () {
            //初始化表单验证
            $("#form1").initValidform();
        });
    </script>



    
</head>
<body class="mainbody">
    <form id="form1" runat="server">
    <!--导航栏-->
    <div class="location">
        <a href="coupon_list.aspx?channel_id=<%=this.channel_id %>" class="back"><i></i><span>
            返回列表页</span></a> <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
        <i class="arrow"></i><a href="coupon_list.aspx?channel_id=<%=this.channel_id %>"><span>
            优惠券管理</span></a> <i class="arrow"></i><span>发布优惠券</span>
    </div>
    <div class="line10">
    </div>
    <!--/导航栏-->
    <!--内容-->
    <div class="content-tab-wrap">
        <div id="floatHead" class="content-tab">
            <div class="content-tab-ul-wrap">
                <ul>
                    <li><a href="javascript:;" onclick="tabs(this);" class="selected">基本信息</a></li>
                </ul>
            </div>
        </div>
    </div>
    <div class="tab-content">
        <dl>
            <dt>类&nbsp;&nbsp;&nbsp;&nbsp; 型</dt>
            <dd>
                <div>
                    <label>
                        <asp:RadioButton ID="RadioBut1" Checked="true" runat="server" GroupName="coupon" /><span>平台优惠券</span></label>
                    <label>
                        <asp:RadioButton ID="RadioBut2" runat="server" GroupName="coupon" /><span>品牌优惠券</span></label>
                </div>
            </dd>
        </dl>
        <dl>
            <dt>标&nbsp;&nbsp;&nbsp;&nbsp; 题</dt>
            <dd>
                <asp:TextBox ID="txtTitle" runat="server" CssClass="input normal" datatype="*2-100"
                    sucmsg=" " />
                <span class="Validform_checktip">*标题最多100个字符</span>
            </dd>
        </dl>
        <dl>
            <dt>备&nbsp;&nbsp;&nbsp;&nbsp; 注</dt>
            <dd>
                <asp:TextBox ID="txtRemark" runat="server" CssClass="input normal" datatype="*2-100"
                    sucmsg=" " />
                <span class="Validform_checktip">*标题最多100个字符</span>
            </dd>
        </dl>
        <dl>
            <dt>优惠券前缀</dt>
            <dd>
                <asp:TextBox ID="txtStrCode" runat="server" CssClass="input small" datatype="*2-100"
                    sucmsg=" ">pthy_</asp:TextBox>
                <span class="Validform_checktip">*请输入优惠券前缀</span>
            </dd>
        </dl>
        <dl>
            <dt>发布数量</dt>
            <dd>
                <asp:TextBox ID="txtNum" runat="server" CssClass="input small" datatype="n" sucmsg=" ">99</asp:TextBox>
                <span class="Validform_checktip">*请输入数字</span>
            </dd>
        </dl>
        <dl>
            <dt>优惠券面额</dt>
            <dd>
                <asp:TextBox ID="txtDecimal" runat="server" CssClass="input small" datatype="n" sucmsg=" ">100</asp:TextBox>&nbsp;元
                <span class="Validform_checktip">*请输入数字</span>
            </dd>
        </dl>
        <dl>
            <dt>生效日期</dt>
            <dd>
                <div class="input-date">
                    <asp:TextBox ID="txtStartTime" runat="server" CssClass="input date" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss',minDate:'%y-%M-%d',maxDate:'#F{$dp.$D(\'txtEndTime\',{d:0});}'})"
                        datatype="*" errormsg="请选择正确的日期" sucmsg=" " />
                    <i>日期</i>
                </div>
                <span class="Validform_checktip">不选择默认当前发布时间</span>
            </dd>
        </dl>
        <dl>
            <dt>结束日期</dt>
            <dd>
                <div class="input-date">
                    <asp:TextBox ID="txtEndTime" runat="server" CssClass="input date" onFocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss',minDate:'#F{$dp.$D(\'txtStartTime\',{d:1});}'})"
                        datatype="*" errormsg="请选择正确的日期" sucmsg=" " />
                    <i>日期</i>
                </div>
                <span class="Validform_checktip">结束日期应大于开始日期</span>
            </dd>
        </dl>
        <dl id="div_albums_container" runat="server" visible="false">
            <dd>
                <div class="upload-box upload-album">
                </div>
                <input type="hidden" name="hidFocusPhoto" id="hidFocusPhoto" runat="server" class="focus-photo" />
                <div class="photo-list">
                    <ul>
                        <asp:Repeater ID="rptAlbumList" runat="server">
                            <ItemTemplate>
                                <li>
                                    <input type="hidden" name="hid_photo_name" value="<%#Eval("id")%>|<%#Eval("original_path")%>|<%#Eval("thumb_path")%>" />
                                    <input type="hidden" name="hid_photo_remark" value="<%#Eval("remark")%>" />
                                    <div class="img-box" onclick="setFocusImg(this);">
                                        <img src="<%#Eval("thumb_path")%>" bigsrc="<%#Eval("original_path")%>" />
                                        <span class="remark"><i>
                                            <%#Eval("remark").ToString() == "" ? "暂无描述..." : Eval("remark").ToString()%></i></span>
                                    </div>
                                    <a href="javascript:;" onclick="setRemark(this);">描述</a> <a href="javascript:;" onclick="delImg(this);">
                                        删除</a> </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
            </dd>
        </dl>
    </div>
    <div id="field_tab_content" runat="server" visible="false" class="tab-content" style="display: none">
    </div>
    <div class="tab-content" style="display: none">
        <dl>
            <dt>调用别名</dt>
            <dd>
                <asp:TextBox ID="txtCallIndex" runat="server" CssClass="input normal" datatype="/^\s*$|^[a-zA-Z0-9\-\_]{2,50}$/"
                    sucmsg=" "></asp:TextBox>
                <span class="Validform_checktip">*别名访问，非必填，不可重复</span>
            </dd>
        </dl>
        <dl>
            <dt>URL链接</dt>
            <dd>
                <asp:TextBox ID="txtLinkUrl" runat="server" MaxLength="255" CssClass="input normal" />
                <span class="Validform_checktip">填写后直接跳转到该网址</span>
            </dd>
        </dl>
        <dl id="div_source" runat="server" visible="false">
            <dt>
                <asp:Label ID="div_source_title" runat="server" Text="信息来源" /></dt>
            <dd>
                <asp:TextBox ID="field_control_source" runat="server" CssClass="input normal"></asp:TextBox>
                <asp:Label ID="div_source_tip" runat="server" CssClass="Validform_checktip" />
            </dd>
        </dl>
        <dl id="div_author" runat="server" visible="false">
            <dt>
                <asp:Label ID="div_author_title" runat="server" Text="文章作者" /></dt>
            <dd>
                <asp:TextBox ID="field_control_author" runat="server" CssClass="input normal" datatype="s0-50"
                    sucmsg=" "></asp:TextBox>
                <asp:Label ID="div_author_tip" runat="server" CssClass="Validform_checktip" />
            </dd>
        </dl>
        <dl>
            <dt>内容摘要</dt>
            <dd>
                <asp:TextBox ID="txtZhaiyao" runat="server" CssClass="input" TextMode="MultiLine"
                    datatype="*0-255" sucmsg=" "></asp:TextBox>
                <span class="Validform_checktip">不填写则自动截取内容前255字符</span>
            </dd>
        </dl>
        <dl>
            <dt>内容描述</dt>
            <dd>
                <textarea id="txtContent" class="editor" style="visibility: hidden;" runat="server"></textarea>
            </dd>
        </dl>
    </div>
    <div class="tab-content" style="display: none">
        <dl>
            <dt>SEO标题</dt>
            <dd>
                <asp:TextBox ID="txtSeoTitle" runat="server" MaxLength="255" CssClass="input normal"
                    datatype="*0-100" sucmsg=" " />
                <span class="Validform_checktip">255个字符以内</span>
            </dd>
        </dl>
        <dl>
            <dt>SEO关健字</dt>
            <dd>
                <asp:TextBox ID="txtSeoKeywords" runat="server" CssClass="input" TextMode="MultiLine"
                    datatype="*0-255" sucmsg=" "></asp:TextBox>
                <span class="Validform_checktip">以“,”逗号区分开，255个字符以内</span>
            </dd>
        </dl>
        <dl>
            <dt>SEO描述</dt>
            <dd>
                <asp:TextBox ID="txtSeoDescription" runat="server" CssClass="input" TextMode="MultiLine"
                    datatype="*0-255" sucmsg=" "></asp:TextBox>
                <span class="Validform_checktip">255个字符以内</span>
            </dd>
        </dl>
    </div>
    <!--/内容-->
    <!--工具栏-->
    <div class="page-footer">
        <div class="btn-list">
            <asp:Button ID="btnSubmit" runat="server" Text="提交保存" CssClass="btn" OnClick="btnSubmit_Click" />
            <input name="btnReturn" type="button" value="返回上一页" class="btn yellow" onclick="javascript:history.back(-1);" />
        </div>
        <div class="clear">
        </div>
    </div>
    <!--/工具栏-->
    </form>
</body>
</html>
