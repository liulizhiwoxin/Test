<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="outlet_edit_.aspx.cs" Inherits="BuysingooShop.Admin.outlet.outlet_edit_" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>编辑信息</title>
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script type="text/javascript" src="../../scripts/datepicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../scripts/swfupload1/swfupload.js"></script>
    <script type="text/javascript" src="../../scripts/swfupload1/swfupload.queue.js"></script>
    <script type="text/javascript" src="../../scripts/swfupload1/swfupload.handlers.js"></script>
    <script type="text/javascript" charset="utf-8" src="../../editor/kindeditor-min.js"></script>
    <script type="text/javascript" charset="utf-8" src="../../editor/lang/zh_CN.js"></script>
    <script type="text/javascript" src="../js/layout.js"></script>
    <script type="text/javascript" src="../../scripts/PCASClass.js"></script>
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            new PCAS("provinces", "city", "area");

            $("#provinces,#city,#area").change(function () {
                $("#provinces1").val($("#provinces option:selected").text());
                $("#city1").val($("#city option:selected").text());
                $("#area1").val($("#area option:selected").text());
            });
            $("#provinces option:selected").text($("#provinces1").val());
            $("#city option:selected").text($("#city1").val());
            $("#area option:selected").text($("#area1").val());


            //初始化表单验证
            $("#form1").initValidform();
            //计算用户组价格
            $("#field_control_sell_price").change(function () {
                var sprice = $(this).val();
                if (sprice > 0) {
                    $(".groupprice").each(function () {
                        var num = $(this).attr("discount") * sprice / 100;
                        $(this).val(ForDight(num, 2));
                    });
                }
            });
            //初始化编辑器
            var editor = KindEditor.create('.editor', {
                width: '98%',
                height: '350px',
                resizeType: 1,
                uploadJson: '../../tools/upload_ajax.ashx?action=EditorFile&IsWater=1',
                fileManagerJson: '../../tools/upload_ajax.ashx?action=ManagerFile',
                allowFileManager: true
            });
            var editorMini = KindEditor.create('.editor-mini', {
                width: '98%',
                height: '250px',
                resizeType: 1,
                uploadJson: '../../tools/upload_ajax.ashx?action=EditorFile&IsWater=1',
                items: [
                    'fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold', 'italic', 'underline',
                    'removeformat', '|', 'justifyleft', 'justifycenter', 'justifyright', 'insertorderedlist',
                    'insertunorderedlist', '|', 'emoticons', 'image', 'link']
            });
            //初始化上传控件
            $(".upload-img").each(function () {
                $(this).InitSWFUpload({ sendurl: "../../tools/upload_ajax.ashx", flashurl: "../../scripts/swfupload/swfupload.swf" });
            });
            $(".upload-album").each(function () {
                $(this).InitSWFUpload({ btntext: "批量上传", btnwidth: 66, single: false, water: true, thumbnail: true, filesize: "2048", sendurl: "../../tools/upload_ajax.ashx", flashurl: "../../scripts/swfupload/swfupload.swf", filetypes: "*.jpg;*.jpge;*.png;*.gif;" });
            });
            $(".attach-btn").click(function () {
                showAttachDialog();
            });
            //设置封面图片的样式
            $(".photo-list ul li .img-box img").each(function () {
                if ($(this).attr("src") == $("#hidFocusPhoto").val()) {
                    $(this).parent().addClass("selected");
                }
            });
        });
        //创建附件窗口
        function showAttachDialog(obj) {
            var objNum = arguments.length;
            var attachDialog = $.dialog({
                id: 'attachDialogId',
                lock: true,
                max: false,
                min: false,
                title: "上传附件",
                content: 'url:dialog/dialog_attach.aspx',
                width: 500,
                height: 180
            });
            //如果是修改状态，将对象传进去
            if (objNum == 1) {
                attachDialog.data = obj;
            }
        }
        //删除附件节点
        function delAttachNode(obj) {
            $(obj).parent().remove();
        }

    </script>
</head>
<body class="mainbody">
    <form id="form1" runat="server">
        <!--导航栏-->
        <div class="location">
            <a href="outlet_list.aspx %>" class="back"><i></i><span>返回列表页</span></a>
            <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
            <i class="arrow"></i>
            <a href="outlet_list.aspx %>"><span>内容管理</span></a>
            <i class="arrow"></i>
            <span>编辑信息</span>
        </div>
        <div class="line10"></div>
        <!--/导航栏-->

        <!--内容-->
        <div class="content-tab-wrap">
            <div id="floatHead" class="content-tab">
                <div class="content-tab-ul-wrap">
                    <ul>
                        <li><a href="javascript:;" onclick="tabs(this);" class="selected">基本信息</a></li>
                        <li id="field_tab_item" runat="server" visible="False"><a href="javascript:;" onclick="tabs(this);">扩展选项</a></li>
                        <li runat="server" visible="False"><a href="javascript:;" onclick="tabs(this);">详细描述</a></li>
                        <li style="display: none;"><a href="javascript:;" onclick="tabs(this);">SEO选项</a></li>
                    </ul>
                </div>
            </div>
        </div>
        <div class="tab-content">
            <dl>
                <dt>省份</dt>
                <dd>
                    <div>
                        <select runat="server" id="provinces" name="provinces"></select>
                        <input id="provinces1" name="provinces1" type="hidden" value="所在省份" runat="server" />
                    </div>
                </dd>
            </dl>
            <dl>
                <dt>城市</dt>
                <dd>
                    <div>
                        <select runat="server" id="city" name="city"></select>
                        <input id="city1" name="city1" type="hidden" value="所在城市" runat="server" />
                    </div>
                </dd>
            </dl>
            <dl>
                <dt>区域</dt>
                <dd>
                    <div>
                        <select runat="server" id="area" name="area"></select>
                        <input id="area1" name="area1" type="hidden" value="所在区域" runat="server" />
                    </div>
                </dd>
            </dl>
            <dl>
                <dt>详细地址</dt>
                <dd>
                    <asp:TextBox ID="txtaddress" runat="server" CssClass="input normal" datatype="*2-100" sucmsg=" " />
                    <span class="Validform_checktip">*</span>
                </dd>
            </dl>
            <dl>
    <dt>店长</dt>
    <dd>
      <div class="rule-single-select">
        <asp:DropDownList id="DropDownList1" runat="server" datatype="*" errormsg="请选择店长" sucmsg=" ">
           
        </asp:DropDownList>
      </div>
    </dd>
  </dl>
            <dl>
                <dt>店铺名称</dt>
                <dd>
                    <asp:TextBox ID="txtTitle" runat="server" CssClass="input normal" datatype="*2-100" sucmsg=" " />
                    <span class="Validform_checktip"></span>
                </dd>
            </dl>
            <dl id="div_sub_title" runat="server" visible="false">
                <dt>
                    <asp:Label ID="div_sub_title_title" runat="server" Text="副标题" /></dt>
                <dd>
                    <asp:TextBox ID="field_control_sub_title" runat="server" CssClass="input normal" datatype="*0-255" sucmsg=" " />
                    <asp:Label ID="div_sub_title_tip" runat="server" CssClass="Validform_checktip" />
                </dd>
            </dl>
            <dl>
                <dt>店铺图片 </dt>
                <dd>
                    <asp:TextBox ID="txtImgUrl" runat="server" CssClass="input normal upload-path" />
                    <div class="upload-box upload-img"></div>
                </dd>
            </dl>
            <dl>
                <dt>营业时间</dt>
                <dd>
                    <asp:TextBox ID="busintime" runat="server" CssClass="input normal" sucmsg=" " />
                    <span class="Validform_checktip">例如：周一至周日 8:00-21:30</span>
                </dd>
            </dl>
            <dl id="div_goods_no" runat="server" visible="false">
                <dt>
                    <asp:Label ID="div_goods_no_title" runat="server" Text="商品货号" /></dt>
                <dd>
                    <asp:TextBox ID="field_control_goods_no" runat="server" CssClass="input normal" datatype="*0-100" sucmsg=" " />
                    <asp:Label ID="div_goods_no_tip" runat="server" CssClass="Validform_checktip" />
                </dd>
            </dl>
            <dl id="div_point" runat="server" visible="false">
                <dt>
                    <asp:Label ID="div_point_title" runat="server" Text="积分" /></dt>
                <dd>
                    <asp:TextBox ID="field_control_point" runat="server" CssClass="input small" datatype="/^-?\d+$/" sucmsg=" ">0</asp:TextBox>
                    <asp:Label ID="div_point_tip" runat="server" CssClass="Validform_checktip" />
                </dd>
            </dl>
            <dl>
                <dt>联系电话</dt>
                <dd>
                    <asp:TextBox ID="txtmobile" runat="server" CssClass="input small" style="width:200px" datatype="n" sucmsg=" "></asp:TextBox>
                    <span class="Validform_checktip">*</span>
                </dd>
            </dl>
            <dl>
                <dt>联系人</dt>
                <dd>
                    <asp:TextBox ID="TextLinkman" runat="server" CssClass="input small" style="width:200px"  sucmsg=" "></asp:TextBox>
                    <span class="Validform_checktip">*</span>
                </dd>
            </dl>
            <dl>
                <dt>微信/Emali</dt>
                <dd>
                    <asp:TextBox ID="TextWeChat" runat="server" CssClass="input small" style="width:200px"  sucmsg=" "></asp:TextBox>
                    <span class="Validform_checktip">*</span>
                </dd>
            </dl>
            <dl>
                <dt>店铺X坐标</dt>
                <dd>
                    <asp:TextBox ID="txtX" runat="server" CssClass="input small" datatype="*2-100" sucmsg=" ">0</asp:TextBox>
                    <span class="Validform_checktip">*X坐标</span>
                </dd>
            </dl>
            
            <dl>
                <dt>店铺Y坐标</dt>
                <dd>
                    <asp:TextBox ID="txtY" runat="server" CssClass="input small" datatype="*2-100" sucmsg=" ">0</asp:TextBox>
                    <span class="Validform_checktip">*Y坐标</span>
                </dd>
            </dl>
            <dl>
                <dt>其它</dt>
                <dd>
                    <asp:TextBox ID="TextOther" runat="server" CssClass="input small" style="width:200px"  sucmsg=" "></asp:TextBox>
                    <span class="Validform_checktip">*</span>
                </dd>
            </dl>
            <dl runat="server" visible="false">
                <dt>发布时间</dt>
                <dd>
                    <div class="input-date">
                        <asp:TextBox ID="txtAddTime" runat="server" CssClass="input date" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})" datatype="/^\s*$|^\d{4}\-\d{1,2}\-\d{1,2}\s{1}(\d{1,2}:){2}\d{1,2}$/" errormsg="请选择正确的日期" sucmsg=" " />
                        <i>日期</i>
                    </div>
                    <span class="Validform_checktip">不选择默认当前发布时间</span>
                </dd>
            </dl>
            <dl id="div_albums_container" runat="server" visible="false">
                <dt>图片相册</dt>
                <dd>
                    <div class="upload-box upload-album"></div>
                    <input type="hidden" name="hidFocusPhoto" id="hidFocusPhoto" runat="server" class="focus-photo" />
                    <div class="photo-list">
                        <ul>
                            <asp:Repeater ID="rptAlbumList" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <input type="hidden" name="hid_photo_name" value="<%#Eval("id")%>|<%#Eval("original_path")%>|<%#Eval("thumb_path")%>" />
                                        <input type="hidden" name="hid_photo_remark" value="<%#Eval("remark")%>" />

                                        <div class="img-box" onclick="setFocusImg(this);">
                                            <img src="<%#Eval("original_path")%>" bigsrc="<%#Eval("thumb_path")%>" />


                                            <span class="remark"><i><%#Eval("remark").ToString() == "" ? "暂无描述..." : Eval("remark").ToString()%></i></span>
                                        </div>
                                        <a href="javascript:;" onclick="setRemark(this);">描述</a>
                                        <a href="javascript:;" onclick="delImg(this);">删除</a>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </dd>
            </dl>
            <dl id="div_attach_container" runat="server" visible="false">
                <dt>上传附件</dt>
                <dd>
                    <a class="icon-btn add attach-btn"><span>添加附件</span></a>
                    <div id="showAttachList" class="attach-list">
                        <ul>
                            <asp:Repeater ID="rptAttachList" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <input name="hid_attach_id" type="hidden" value="<%#Eval("id")%>" />
                                        <input name="hid_attach_filename" type="hidden" value="<%#Eval("file_name")%>" />
                                        <input name="hid_attach_filepath" type="hidden" value="<%#Eval("file_path")%>" />
                                        <input name="hid_attach_filesize" type="hidden" value="<%#Eval("file_size")%>" />
                                        <i class="icon"></i>
                                        <a href="javascript:;" onclick="delAttachNode(this);" class="del" title="删除附件"></a>
                                        <a href="javascript:;" onclick="showAttachDialog(this);" class="edit" title="更新附件"></a>
                                        <div class="title"><%#Eval("file_name")%></div>
                                        <div class="info">类型：<span class="ext"><%#Eval("file_ext")%></span> 大小：<span class="size"><%#(Convert.ToInt32(Eval("file_size")) / 1024) > 1024 ? Convert.ToDouble((Convert.ToDouble(Eval("file_size")) / 1048576f)).ToString("0.0") + "MB" : (Convert.ToInt32(Eval("file_size")) / 1024) + "KB"%></span> 下载：<span class="down"><%#Eval("down_num")%></span>次</div>
                                        <div class="btns">下载积分：<input type="text" name="txt_attach_point" onkeydown="return checkNumber(event);" value="<%#Eval("point")%>" /></div>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </dd>
            </dl>
        </div>

        <div id="field_tab_content" runat="server" class="tab-content" visible="False" style="display: none;">
            <dl id="div_damizhonglei" runat="server" class="multi-radio-tr multi-checkbox-tr" visible="false">
                <dt>
                    <asp:Label ID="div_damizhonglei_title" runat="server" Text="大米种类" /></dt>
                <dd>
                    <asp:TextBox ID="field_control_damizhonglei" runat="server" CssClass="input" TextMode="MultiLine" datatype="*" sucmsg=" "></asp:TextBox>
                    <span class="Validform_checktip">*请填写数据</span><br />
                    <asp:Label ID="div_damizhonglei_tip" runat="server" />
                </dd>
            </dl>
            <dl id="div_zhongliang" runat="server" class="multi-radio-tr multi-checkbox-tr" visible="false">
                <dt>
                    <asp:Label ID="div_zhongliang_title" runat="server" Text="重量" /></dt>
                <dd>
                    <asp:TextBox ID="field_control_zhongliang" runat="server" CssClass="input" TextMode="MultiLine" datatype="*" sucmsg=" "></asp:TextBox>
                    <span class="Validform_checktip">*请填写数据</span><br />
                    <asp:Label ID="div_zhongliang_tip" runat="server" />
                </dd>
            </dl>
            <dl id="div_stock_quantity" runat="server" visible="false">
                <dt>
                    <asp:Label ID="div_stock_quantity_title" runat="server" Text="库存数量" /></dt>
                <dd>
                    <asp:TextBox ID="field_control_stock_quantity" runat="server" CssClass="input small" datatype="n" sucmsg=" ">0</asp:TextBox>
                    <asp:Label ID="div_stock_quantity_tip" runat="server" CssClass="Validform_checktip" />
                </dd>
            </dl>
            <dl id="div_market_price" runat="server" visible="false">
                <dt>
                    <asp:Label ID="div_market_price_title" runat="server" Text="市场价格" /></dt>
                <dd>
                    <asp:TextBox ID="field_control_market_price" runat="server" CssClass="input small" datatype="/^(([1-9]{1}\d*)|([0]{1}))(\.(\d){1,2})?$/" sucmsg=" ">0</asp:TextBox>
                    元
          <asp:Label ID="div_market_price_tip" runat="server" CssClass="Validform_checktip" />
                </dd>
            </dl>
            <dl id="div_sell_price" runat="server" visible="false">
                <dt>
                    <asp:Label ID="div_sell_price_title" runat="server" Text="销售价格" /></dt>
                <dd>
                    <asp:TextBox ID="field_control_sell_price" runat="server" CssClass="input small" datatype="/^(([1-9]{1}\d*)|([0]{1}))(\.(\d){1,2})?$/" sucmsg=" ">0</asp:TextBox>
                    元
          <asp:Label ID="div_sell_price_tip" runat="server" CssClass="Validform_checktip" />
                </dd>
            </dl>
            <asp:Repeater ID="rptPrice" runat="server">
                <HeaderTemplate>
                    <dl>
                        <dt>会员价格</dt>
                        <dd>
                            <table border="0" cellspacing="0" cellpadding="0" class="border-table">
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <th width="80"><%#Eval("title")%></th>
                        <td>
                            <asp:HiddenField ID="hidePriceId" runat="server" />
                            <asp:HiddenField ID="hideGroupId" Value='<%#Eval("id") %>' runat="server" />
                            <asp:TextBox ID="txtGroupPrice" runat="server" size="10" discount='<%#Eval("discount") %>' CssClass="td-input groupprice"
                                MaxLength="10" datatype="/^(([1-9]{1}\d*)|([0]{1}))(\.(\d){1,2})?$/" sucmsg=" ">0</asp:TextBox>
                            <span class="Validform_checktip">*享受<%#Eval("discount") %>折优惠</span>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
            </dd>
          </dl>
                </FooterTemplate>
            </asp:Repeater>
        </div>

        <div class="tab-content" style="display: none">
            <dl>
                <dt>调用别名</dt>
                <dd>
                    <asp:TextBox ID="txtCallIndex" runat="server" CssClass="input normal" datatype="/^\s*$|^[a-zA-Z0-9\-\_]{2,50}$/" sucmsg=" "></asp:TextBox>
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
                    <asp:TextBox ID="field_control_author" runat="server" CssClass="input normal" datatype="s0-50" sucmsg=" "></asp:TextBox>
                    <asp:Label ID="div_author_tip" runat="server" CssClass="Validform_checktip" />
                </dd>
            </dl>
            <dl>
                <dt>内容摘要</dt>
                <dd>
                    <asp:TextBox ID="txtZhaiyao" runat="server" CssClass="input" TextMode="MultiLine" datatype="*0-255" sucmsg=" "></asp:TextBox>
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
                    <asp:TextBox ID="txtSeoTitle" runat="server" MaxLength="255" CssClass="input normal" datatype="*0-100" sucmsg=" " />
                    <span class="Validform_checktip">255个字符以内</span>
                </dd>
            </dl>
            <dl>
                <dt>SEO关健字</dt>
                <dd>
                    <asp:TextBox ID="txtSeoKeywords" runat="server" CssClass="input" TextMode="MultiLine" datatype="*0-255" sucmsg=" "></asp:TextBox>
                    <span class="Validform_checktip">以“,”逗号区分开，255个字符以内</span>
                </dd>
            </dl>
            <dl>
                <dt>SEO描述</dt>
                <dd>
                    <asp:TextBox ID="txtSeoDescription" runat="server" CssClass="input" TextMode="MultiLine" datatype="*0-255" sucmsg=" "></asp:TextBox>
                    <span class="Validform_checktip">255个字符以内</span>
                </dd>
            </dl>
        </div>
        <!--/内容-->

        <!--工具栏-->
        <div class="page-footer">
            <div class="btn-list">
                <asp:Button ID="btnSubmit" runat="server" Text="提交保存" CssClass="btn" OnClick="btnSubmit_Click" />
                <input name="btnReturn" type="button" value="返回上一页" class="btn yellow" onclick="javascript: history.back(-1);" />
            </div>
            <div class="clear"></div>
        </div>
        <!--/工具栏-->
    </form>
</body>
</html>
