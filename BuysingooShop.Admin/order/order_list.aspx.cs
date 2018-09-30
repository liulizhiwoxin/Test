using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vincent;
using System.IO;

namespace BuysingooShop.Admin.order
{
    public partial class order_list : Web.UI.ManagePage
    {
        protected int totalCount;
        protected int page;
        protected int pageSize;

        protected int status;
        protected int payment_status;
        protected int express_status;
        protected string keywords = string.Empty;
        protected string keyDate = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.status = Vincent._DTcms.DTRequest.GetQueryInt("status");
            this.payment_status = Vincent._DTcms.DTRequest.GetQueryInt("payment_status");
            this.express_status = Vincent._DTcms.DTRequest.GetQueryInt("express_status");
            this.keywords = Vincent._DTcms.DTRequest.GetQueryString("keywords");
            this.keyDate = Vincent._DTcms.DTRequest.GetQueryString("keyDate");

            this.pageSize = GetPageSize(10); //每页数量
            if (!Page.IsPostBack)
            {

                ChkAdminLevel("order_list", Vincent._DTcms.DTEnums.ActionEnum.View.ToString()); //检查权限
                RptBind(" 1=1 " + CombSqlTxt(this.status, this.payment_status, this.express_status, this.keywords,this.keyDate), "add_time desc");
            }
        }

        #region 数据绑定=================================
        private void RptBind(string _strWhere, string _orderby)
        {
            this.page = Vincent._DTcms.DTRequest.GetQueryInt("page", 1);
            if (this.status > 0)
            {
                this.ddlStatus.SelectedValue = this.status.ToString();
            }
            if (this.payment_status > 0)
            {
                this.ddlPaymentStatus.SelectedValue = this.payment_status.ToString();
            }
            if (this.express_status > 0)
            {
                this.ddlExpressStatus.SelectedValue = this.express_status.ToString();
            }
            txtKeywords.Text = this.keywords;
            txtDate.Value = this.keyDate;
            BLL.orders bll = new BLL.orders();
            BuysingooShop.Model.manager manModel = Session[Vincent._DTcms.DTKeys.SESSION_ADMIN_INFO] as Model.manager;
            if (manModel.brand_id != 0)
            {
                _strWhere += " and store_id=" + manModel.brand_id;
            }
            this.rptList.DataSource = bll.GetList1 (this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
            this.rptList.DataBind();

            //绑定页码
            txtPageNum.Text = this.pageSize.ToString();
            string pageUrl = Vincent._DTcms.Utils.CombUrlTxt("order_list.aspx", "status={0}&payment_status={1}&express_status={2}&keywords={3}&keyDate={4}&page={5}",
                this.status.ToString(), this.payment_status.ToString(), this.express_status.ToString(), this.keywords,this.keyDate, "__id__");
            PageContent.InnerHtml = Vincent._DTcms.Utils.OutPageList(this.pageSize, this.page, this.totalCount, pageUrl, 8);
        }
        #endregion





        #region 返回用户名=========================
        /// <summary>
        /// 获取用户昵称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected string Getusernick_name(int id)
        {
            string nick_name = "";
            Model.users model = new BLL.users().GetModel(id);
            if (model == null)
            {
                return nick_name = "--";
            }
            return nick_name = model.nick_name;

        }
        #endregion












        #region 组合SQL查询语句==========================
        protected string CombSqlTxt(int _status, int _payment_status, int _express_status, string _keywords,string _keyDate)
        {
            StringBuilder strTemp = new StringBuilder();
            #region 角色判断
            Model.manager model=GetAdminInfo();
            int roleType = model.role_type;
            int roleID = model.role_id;
            //if (roleType == 2)
            //{
                //if (roleID == 3)//酒厂
                //{
                //    //strTemp.Append(" and id in (select order_id from dt_order_goods where id in(select id from dt_article where brand_id="+model.brand_id+"))");
                //    //订单表里有品牌id  可直接从订单表查询
                //    strTemp.Append(" and brand_id=" + model.brand_id);
                //}
                //else if (roleID == 4)//经销商
                //{
                //    strTemp.Append(" and str_code='"+model.str_code+"'");
                //}   
            //}
            #endregion
            if (_status > 0)
            {
                if (_status == 5)
                {//取消订单
                    strTemp.Append(" and status=99");
                }
                else if (_status == 6)
                {//作废订单
                    strTemp.Append(" and status=100");
                }
                else if (_status == 4)
                {//完成订单
                    strTemp.Append(" and status=90");
                }
                else if (_status == 2)
                {//待处理
                    strTemp.Append(" and status=2 and payment_status=2 and express_status=1");
                }
                else if (_status == 3)
                {//已确认
                    strTemp.Append(" and status=2 and payment_status=2 and express_status=2");
                }//退货中
                else if (_status==7)
                {
                    //strTemp.Append(" and refund_status is not null");
                    strTemp.Append(" and status!=90 and status!=100 and refund_status is not null");
                }
                else
                {
                    strTemp.Append(" and status=" + _status);
                }
                
            }
            if (_payment_status > 0 && _status!=2)
            {
                strTemp.Append(" and payment_status=" + _payment_status);
            }
            if (_express_status > 0 && _status != 3)
            {
                strTemp.Append(" and express_status=" + _express_status);
            }
            _keywords = _keywords.Replace("'", "");
            _keywords = _keywords.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "");
            if (!string.IsNullOrEmpty(_keywords))
            {
                strTemp.Append(" and (t1.order_no like '%" + _keywords.ToString() + "%' or t1.user_name like '%" + _keywords.ToString() + "%' or accept_name like '%" + _keywords.ToString() + "%')");
            }

            _keyDate = _keyDate.Replace("'", "");
            _keyDate = _keyDate.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "");
            if (!string.IsNullOrEmpty(_keyDate))
            {
                strTemp.Append(" and datediff(dd,add_time,'" + _keyDate.ToString() + "')=0");
            }

            return strTemp.ToString();
        }
        #endregion

        #region 返回用户每页数量=========================
        private int GetPageSize(int _default_size)
        {
            int _pagesize;
            if (int.TryParse(Vincent._DTcms.Utils.GetCookie("order_list_page_size"), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    return _pagesize;
                }
            }
            return _default_size;
        }
        #endregion

        #region 返回订单状态=============================
        protected string GetOrderStatus(int _id)
        {
            string _title = string.Empty;
            //Model.orders model = new BLL.orders().GetModel(_id);
            DataTable dt = new BLL.orders().GetOrderList(0, " t1.id=" + _id, " id desc").Tables[0];

            var refund_status = 0;
            if (dt.Rows[0]["refund_status"].ToString() != "" && dt.Rows[0]["refund_status"].ToString() != null)
            {
                refund_status = int.Parse(dt.Rows[0]["refund_status"].ToString());
            }

            switch (int.Parse(dt.Rows[0]["status"].ToString()))
            {
                case 1: //如果是线下支付，支付状态为0，如果是线上支付，支付成功后会自动改变订单状态为已确认
                    if (int.Parse(dt.Rows[0]["payment_status"].ToString()) == 1)
                    {
                        _title = "待付款";
                    }
                    else
                    {
                        _title = "已付款";
                    }
                    break;
                case 2://平台确认
                    if (int.Parse(dt.Rows[0]["payment_status"].ToString()) == 2 && int.Parse(dt.Rows[0]["express_status"].ToString()) == 1 && refund_status == 0)
                    {
                        _title = "待发货";
                    }
                    if (int.Parse(dt.Rows[0]["payment_status"].ToString()) == 2 && int.Parse(dt.Rows[0]["express_status"].ToString()) == 2 && refund_status == 0)
                    {
                        _title = "已发货";
                    }
                    if (int.Parse(dt.Rows[0]["payment_status"].ToString()) == 2 && int.Parse(dt.Rows[0]["express_status"].ToString()) == 1 && refund_status == 1)
                    {
                        _title = "退款中";
                    }
                    if (int.Parse(dt.Rows[0]["payment_status"].ToString()) == 2 && int.Parse(dt.Rows[0]["express_status"].ToString()) == 1 && refund_status == 2)
                    {
                        _title = "确认退款";
                    }
                    if (int.Parse(dt.Rows[0]["payment_status"].ToString()) == 2 && int.Parse(dt.Rows[0]["express_status"].ToString()) == 1 && refund_status == 3)
                    {
                        _title = "完成退款";
                    }
                    if (int.Parse(dt.Rows[0]["payment_status"].ToString()) == 2 && int.Parse(dt.Rows[0]["express_status"].ToString()) == 1 && refund_status == 4)
                    {
                        _title = "取消退款";
                    }
                    if (int.Parse(dt.Rows[0]["payment_status"].ToString()) == 2 && int.Parse(dt.Rows[0]["express_status"].ToString()) == 1 && refund_status == 5)
                    {
                        _title = "驳回退款";
                    }
                    if (int.Parse(dt.Rows[0]["payment_status"].ToString()) == 2 && int.Parse(dt.Rows[0]["express_status"].ToString()) == 2 && refund_status == 1)
                    {
                        _title = "退货中";
                    }
                    if (int.Parse(dt.Rows[0]["payment_status"].ToString()) == 2 && int.Parse(dt.Rows[0]["express_status"].ToString()) == 2 && refund_status == 2)
                    {
                        _title = "确认退货";
                    }
                    if (int.Parse(dt.Rows[0]["payment_status"].ToString()) == 2 && int.Parse(dt.Rows[0]["express_status"].ToString()) == 2 && refund_status == 3)
                    {
                        _title = "完成退货";
                    }
                    if (int.Parse(dt.Rows[0]["payment_status"].ToString()) == 2 && int.Parse(dt.Rows[0]["express_status"].ToString()) == 2 && refund_status == 4)
                    {
                        _title = "取消退货";
                    }
                    if (int.Parse(dt.Rows[0]["payment_status"].ToString()) == 2 && int.Parse(dt.Rows[0]["express_status"].ToString()) == 2 && refund_status == 5)
                    {
                        _title = "驳回退货";
                    }
                    break;
                case 3://酒厂确认
                    _title = "待酒厂确认";
                    break;
                case 4://酒厂生产完成
                    if (int.Parse(dt.Rows[0]["express_status"].ToString()) == 1)
                    {
                        _title = "待发货";
                    }
                    else
                    {
                        _title = "已发货";
                    }
                    break;
                case 90:
                    _title = "交易完成";
                    break;
                case 99:
                    _title = "已取消";
                    break;
                case 100:
                    _title = "已作废";
                    break;
            }

            //switch (model.status)
            //{
            //    case 1: //如果是线下支付，支付状态为0，如果是线上支付，支付成功后会自动改变订单状态为已确认
            //        if (model.payment_status == 1)
            //        {
            //            _title = "待付款";
            //        }
            //        else
            //        {
            //            _title = "已付款";
            //        }
            //        break;
            //    case 2://平台确认
            //        _title = "待平台确认";
            //        break;
            //    case 3://酒厂确认
            //        _title = "待酒厂确认";
            //        break;
            //    case 4://酒厂生产完成
            //        if (model.express_status == 1)
            //        {
            //            _title = "待发货";
            //        }
            //        else
            //        {
            //            _title = "已发货";
            //        }
            //        break;
            //    case 90:
            //        _title = "交易完成";
            //        break;
            //    case 99:
            //        _title = "已取消";
            //        break;
            //    case 100:
            //        _title = "已作废";
            //        break;
            //}

            return _title;
        }
        #endregion

        //关健字查询
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(Vincent._DTcms.Utils.CombUrlTxt("order_list.aspx", "status={0}&payment_status={1}&express_status={2}&keywords={3}&keyDate={4}",
                this.status.ToString(), this.payment_status.ToString(), this.express_status.ToString(), txtKeywords.Text,this.keyDate));
        }

        //日期查询
        protected void btnDate_Click(object sender, EventArgs e)
        {
            Response.Redirect(Vincent._DTcms.Utils.CombUrlTxt("order_list.aspx", "status={0}&payment_status={1}&express_status={2}&keywords={3}&keyDate={4}",
                this.status.ToString(), this.payment_status.ToString(), this.express_status.ToString(), this.keywords,txtDate.Value));
        }

        //订单状态
        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(Vincent._DTcms.Utils.CombUrlTxt("order_list.aspx", "status={0}&payment_status={1}&express_status={2}&keywords={3}&keyDate={4}",
                ddlStatus.SelectedValue, this.payment_status.ToString(), this.express_status.ToString(), this.keywords,this.keyDate));
        }

        //支付状态
        protected void ddlPaymentStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(Vincent._DTcms.Utils.CombUrlTxt("order_list.aspx", "status={0}&payment_status={1}&express_status={2}&keywords={3}",
                this.status.ToString(), ddlPaymentStatus.SelectedValue, this.express_status.ToString(), this.keywords));
        }

        //发货状态
        protected void ddlExpressStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(Vincent._DTcms.Utils.CombUrlTxt("order_list.aspx", "status={0}&payment_status={1}&express_status={2}&keywords={3}",
                this.status.ToString(), this.payment_status.ToString(), ddlExpressStatus.SelectedValue, this.keywords));
        }

        //设置分页数量
        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            int _pagesize;
            if (int.TryParse(txtPageNum.Text.Trim(), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    Vincent._DTcms.Utils.WriteCookie("order_list_page_size", _pagesize.ToString(), 14400);
                }
            }
            Response.Redirect(Vincent._DTcms.Utils.CombUrlTxt("order_list.aspx", "status={0}&payment_status={1}&express_status={2}&keywords={3}",
                this.status.ToString(), this.payment_status.ToString(), this.express_status.ToString(), this.keywords));
        }

        //导出excel
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            StringBuilder OutFileContent = new StringBuilder();//容器
            //写头文件
            OutFileContent = AddHeadFile(OutFileContent);
            //写内容
            StringBuilder sbMsg = new StringBuilder();//容器


            //查询需要导出的数据
            string _strWhere = "1=1";
            _strWhere+=CombSqlTxt(this.status, this.payment_status, this.express_status, this.keywords,this.keyDate);
            BLL.orders bll = new BLL.orders();
            BuysingooShop.Model.manager manModel = Session[Vincent._DTcms.DTKeys.SESSION_ADMIN_INFO] as Model.manager;
            if (manModel.brand_id != 0)
            {
                _strWhere += " and store_id=" + manModel.brand_id;
            }
            DataSet ds = bll.GetOrderAmount(0, _strWhere," id");



            OutFileContent.Append(this.AddContentFile(sbMsg, ds));
            //写尾文件
            OutFileContent = AddEndFile(OutFileContent);
            //保存到xls
            string strRandomFileName = "简单生活订单报表";
            //strRandomFileName = strRandomFileName + DateTime.Now.ToString("yyyy-MM-dd");
            //strRandomFileName=strRandomFileName+"-"+DateTime.Now.Minute.ToString();
            //strRandomFileName = strRandomFileName+"-" + DateTime.Now.Second.ToString();

            string strPath = Server.MapPath(Context.Request.ApplicationPath);
            string strExcelFile = strPath + strRandomFileName + ".xls";
            

            try
            {
                //生成excel
                FileStream OutFile = new FileStream(strExcelFile, FileMode.Create, FileAccess.Write);
                byte[] btArray = new byte[OutFileContent.Length];
                btArray = Encoding.UTF8.GetBytes(OutFileContent.ToString());
                OutFile.Write(btArray, 0, btArray.Length);
                OutFile.Flush();
                OutFile.Close();


                //下载文件
                FileStream fs = new FileStream(strExcelFile, FileMode.Open);
                byte[] bytes = new byte[(int)fs.Length];
                fs.Read(bytes, 0, bytes.Length);
                fs.Close();
                Response.ContentType = "application/ms-word";
                //通知浏览器下载文件而不是打开
                Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(strExcelFile, System.Text.Encoding.UTF8));
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                JscriptMsg("请稍后再试！"+ex.Message, Vincent._DTcms.Utils.CombUrlTxt("order_list.aspx", "status={0}&payment_status={1}&express_status={2}&keywords={3}",
                this.status.ToString(), this.payment_status.ToString(), this.express_status.ToString(), this.keywords), "Success");
            }

            

            JscriptMsg("导出成功！", Vincent._DTcms.Utils.CombUrlTxt("order_list.aspx", "status={0}&payment_status={1}&express_status={2}&keywords={3}",
                this.status.ToString(), this.payment_status.ToString(), this.express_status.ToString(), this.keywords), "Success");

        }

        #region 写数据内容
        private StringBuilder AddContentFile(StringBuilder OutFileContent, DataSet ds)
        {

            //写列头
            OutFileContent.Append("<Row ss:AutoFitHeight=\"0\">");
            OutFileContent.Append("<Cell><Data ss:Type=\"String\">订单号</Data></Cell>");
            OutFileContent.Append("<Cell><Data ss:Type=\"String\">用户名</Data></Cell>");
            OutFileContent.Append("<Cell><Data ss:Type=\"String\">用户手机号</Data></Cell>");
            OutFileContent.Append("<Cell><Data ss:Type=\"String\">订单状态</Data></Cell>");
            OutFileContent.Append("<Cell><Data ss:Type=\"String\">订单金额</Data></Cell>");
            OutFileContent.Append("<Cell><Data ss:Type=\"String\">店铺名</Data></Cell>");
            OutFileContent.Append("<Cell><Data ss:Type=\"String\">自提点地址</Data></Cell>");
            OutFileContent.Append("<Cell><Data ss:Type=\"String\">下单日期</Data></Cell>");
            OutFileContent.Append("</Row>");
            //写内容
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                OutFileContent.Append("<Row ss:AutoFitHeight=\"0\">");
                OutFileContent.Append("<Cell><Data ss:Type=\"String\">" + row["order_no"].ToString() + "</Data></Cell>");
                OutFileContent.Append("<Cell><Data  ss:Type=\"String\">" + row["user_name"].ToString() + "</Data></Cell>");
                OutFileContent.Append("<Cell><Data  ss:Type=\"String\">" + row["mobile"].ToString() + "</Data></Cell>");
                OutFileContent.Append("<Cell><Data ss:Type=\"String\">" + GetOrderStatus(int.Parse(row["id"].ToString())) + "</Data></Cell>");
                OutFileContent.Append("<Cell><Data ss:Type=\"String\">" + row["order_amount"].ToString() + "</Data></Cell>");
                OutFileContent.Append("<Cell><Data ss:Type=\"String\">" + row["store_name"].ToString() + "</Data></Cell>");
                OutFileContent.Append("<Cell><Data ss:Type=\"String\">" + row["store_address"].ToString() + "</Data></Cell>");
                OutFileContent.Append("<Cell><Data ss:Type=\"String\">" + row["add_time"].ToString() + "</Data></Cell>");
                OutFileContent.Append("</Row>");
            }
            return OutFileContent;
        }
        #endregion

        #region 写Excel头
        /// <summary>
        ///
        /// </summary>
        /// <param name="OutFileContent"></param>
        /// <returns></returns>
        private StringBuilder AddHeadFile(StringBuilder OutFileContent)
        {
            OutFileContent.Append("<?xml version=\"1.0\"?>\r\n");
            OutFileContent.Append("<?mso-application progid=\"Excel.Sheet\"?>\r\n");
            OutFileContent.Append("<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"\r\n");
            OutFileContent.Append(" xmlns:o=\"urn:schemas-microsoft-com:office:office\"\r\n");
            OutFileContent.Append(" xmlns:x=\"urn:schemas-microsoft-com:office:excel\"\r\n");
            OutFileContent.Append(" xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\"\r\n");
            OutFileContent.Append(" xmlns:html=\"http://www.w3.org/TR/REC-html40\">\r\n");
            OutFileContent.Append(" <DocumentProperties xmlns=\"urn:schemas-microsoft-com:office:office\">\r\n");
            OutFileContent.Append("  <Author>panss</Author>\r\n");
            OutFileContent.Append("  <LastAuthor>Оґ¶ЁТе</LastAuthor>\r\n");
            OutFileContent.Append("  <Created>2004-12-31T03:40:31Z</Created>\r\n");
            OutFileContent.Append("  <Company>Prcedu</Company>\r\n");
            OutFileContent.Append("  <Version>12.00</Version>\r\n");
            OutFileContent.Append(" </DocumentProperties>\r\n");
            OutFileContent.Append(" <OfficeDocumentSettings xmlns=\"urn:schemas-microsoft-com:office:office\">\r\n");
            OutFileContent.Append("  <DownloadComponents/>\r\n");
            OutFileContent.Append("  <LocationOfComponents HRef=\"file:///F:\\Tools\\OfficeXP\\OfficeXP\\\"/>\r\n");
            OutFileContent.Append(" </OfficeDocumentSettings>\r\n");
            OutFileContent.Append(" <ExcelWorkbook xmlns=\"urn:schemas-microsoft-com:office:excel\">\r\n");
            OutFileContent.Append("  <WindowHeight>9000</WindowHeight>\r\n");
            OutFileContent.Append("  <WindowWidth>10620</WindowWidth>\r\n");
            OutFileContent.Append("  <WindowTopX>480</WindowTopX>\r\n");
            OutFileContent.Append("  <WindowTopY>45</WindowTopY>\r\n");
            OutFileContent.Append("  <ProtectStructure>False</ProtectStructure>\r\n");
            OutFileContent.Append("  <ProtectWindows>False</ProtectWindows>\r\n");
            OutFileContent.Append(" </ExcelWorkbook>\r\n");
            OutFileContent.Append(" <Styles>\r\n");
            OutFileContent.Append("  <Style ss:ID=\"Default\" ss:Name=\"Normal\">\r\n");
            OutFileContent.Append("   <Alignment ss:Vertical=\"Center\" />\r\n");
            OutFileContent.Append("   <Borders/>\r\n");
            OutFileContent.Append("   <Font ss:FontName=\"ЛОМе\" x:CharSet=\"134\" ss:Size=\"12\"/>\r\n");
            OutFileContent.Append("   <Interior/>\r\n");
            OutFileContent.Append("   <NumberFormat/>\r\n");
            OutFileContent.Append("   <Protection/>\r\n");
            OutFileContent.Append("  </Style>\r\n");
            OutFileContent.Append("  <Style ss:ID=\"s62\">\r\n");
            OutFileContent.Append("   <Alignment ss:Vertical=\"Center\" ss:Horizontal=\"Center\" ss:WrapText=\"1\"/>\r\n");
            OutFileContent.Append("   <Font ss:FontName=\"ЛОМе\" x:CharSet=\"134\" ss:Size=\"9\"/>\r\n");
            OutFileContent.Append("  </Style>\r\n");
            OutFileContent.Append("  <Style ss:ID=\"s74\">\r\n");
            OutFileContent.Append("   <Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Center\"/>\r\n");
            OutFileContent.Append("   <Borders>\r\n");
            OutFileContent.Append("  <Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\r\n");
            OutFileContent.Append("  <Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\r\n");
            OutFileContent.Append("  <Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\r\n");
            OutFileContent.Append("  <Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\r\n");
            OutFileContent.Append("  </Borders>\r\n");
            OutFileContent.Append("   <Font ss:FontName=\"ЛОМе\" x:CharSet=\"134\" ss:Size=\"12\" ss:Bold=\"1\"/>\r\n");
            OutFileContent.Append("   <Interior ss:Color=\"#BFBFBF\" ss:Pattern=\"Solid\"/>\r\n");
            OutFileContent.Append("  </Style>\r\n");
            OutFileContent.Append(" </Styles>\r\n");
            OutFileContent.Append(" <Worksheet ss:Name=\"Sheet1\">\r\n");
            OutFileContent.Append("  <Table ss:ExpandedColumnCount=\"255\" x:FullColumns=\"1\" \r\n");
            OutFileContent.Append("x:FullRows=\"1\" ss:StyleID=\"s62\" ss:DefaultColumnWidth=\"75\" ss:DefaultRowHeight=\"20.25\">\r\n");
            OutFileContent.Append("<Column ss:StyleID=\"s62\" ss:AutoFitWidth=\"0\" ss:Width=\"112.5\"/>\r\n");
            return OutFileContent;
        }
        #endregion

        #region 写Excel尾
        /// <summary>
        /// РґexcelОІ
        /// </summary>
        /// <param name="OutFileContent"></param>
        /// <returns></returns>
        private StringBuilder AddEndFile(StringBuilder OutFileContent)
        {
            OutFileContent.Append("</Table>\r\n");
            OutFileContent.Append("<WorksheetOptions xmlns=\"urn:schemas-microsoft-com:office:excel\">\r\n");
            OutFileContent.Append("<Unsynced/>\r\n");
            OutFileContent.Append("<Print>\r\n");
            OutFileContent.Append("    <ValidPrinterInfo/>\r\n");
            OutFileContent.Append("    <PaperSizeIndex>9</PaperSizeIndex>\r\n");
            OutFileContent.Append("    <HorizontalResolution>600</HorizontalResolution>\r\n");
            OutFileContent.Append("    <VerticalResolution>0</VerticalResolution>\r\n");
            OutFileContent.Append("</Print>\r\n");
            OutFileContent.Append("<Selected/>\r\n");
            OutFileContent.Append("<Panes>\r\n");
            OutFileContent.Append("    <Pane>\r\n");
            OutFileContent.Append("    <Number>3</Number>\r\n");
            OutFileContent.Append("    <RangeSelection>R1:R65536</RangeSelection>\r\n");
            OutFileContent.Append("    </Pane>\r\n");
            OutFileContent.Append("</Panes>\r\n");
            OutFileContent.Append("<ProtectObjects>False</ProtectObjects>\r\n");
            OutFileContent.Append("<ProtectScenarios>False</ProtectScenarios>\r\n");
            OutFileContent.Append("</WorksheetOptions>\r\n");
            OutFileContent.Append("</Worksheet>\r\n");
            OutFileContent.Append("<Worksheet ss:Name=\"Sheet2\">\r\n");
            OutFileContent.Append("<Table ss:ExpandedColumnCount=\"1\" ss:ExpandedRowCount=\"1\" x:FullColumns=\"1\"\r\n");
            OutFileContent.Append("x:FullRows=\"1\" ss:DefaultColumnWidth=\"54\" ss:DefaultRowHeight=\"14.25\">\r\n");
            OutFileContent.Append("<Row ss:AutoFitHeight=\"0\"/>\r\n");
            OutFileContent.Append("</Table>\r\n");
            OutFileContent.Append("<WorksheetOptions xmlns=\"urn:schemas-microsoft-com:office:excel\">\r\n");
            OutFileContent.Append("<Unsynced/>\r\n");
            OutFileContent.Append("<ProtectObjects>False</ProtectObjects>\r\n");
            OutFileContent.Append("<ProtectScenarios>False</ProtectScenarios>\r\n");
            OutFileContent.Append("</WorksheetOptions>\r\n");
            OutFileContent.Append("</Worksheet>\r\n");
            OutFileContent.Append("<Worksheet ss:Name=\"Sheet3\">\r\n");
            OutFileContent.Append("<Table ss:ExpandedColumnCount=\"1\" ss:ExpandedRowCount=\"1\" x:FullColumns=\"1\"\r\n");
            OutFileContent.Append("x:FullRows=\"1\" ss:DefaultColumnWidth=\"54\" ss:DefaultRowHeight=\"14.25\">\r\n");
            OutFileContent.Append("<Row ss:AutoFitHeight=\"0\"/>\r\n");
            OutFileContent.Append("</Table>\r\n");
            OutFileContent.Append("<WorksheetOptions xmlns=\"urn:schemas-microsoft-com:office:excel\">\r\n");
            OutFileContent.Append("<Unsynced/>\r\n");
            OutFileContent.Append("<ProtectObjects>False</ProtectObjects>\r\n");
            OutFileContent.Append("<ProtectScenarios>False</ProtectScenarios>\r\n");
            OutFileContent.Append("</WorksheetOptions>\r\n");
            OutFileContent.Append("</Worksheet>\r\n");
            OutFileContent.Append("</Workbook>\r\n");
            return OutFileContent;
        }
        #endregion

        //批量删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("order_list", Vincent._DTcms.DTEnums.ActionEnum.Delete.ToString()); //检查权限
            int sucCount = 0;
            int errorCount = 0;
            BLL.orders bll = new BLL.orders();
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    if (bll.Delete(id))
                    {
                        sucCount += 1;
                    }
                    else
                    {
                        errorCount += 1;
                    }
                }
            }
            AddAdminLog(Vincent._DTcms.DTEnums.ActionEnum.Delete.ToString(), "删除订单成功" + sucCount + "条，失败" + errorCount + "条"); //记录日志
            JscriptMsg("删除成功" + sucCount + "条，失败" + errorCount + "条！", Vincent._DTcms.Utils.CombUrlTxt("order_list.aspx", "status={0}&payment_status={1}&express_status={2}&keywords={3}",
                this.status.ToString(), this.payment_status.ToString(), this.express_status.ToString(), this.keywords), "Success");
        }

    }
}