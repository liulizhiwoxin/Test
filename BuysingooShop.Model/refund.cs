using System;
using System.Collections.Generic;

namespace BuysingooShop.Model
{
    /// <summary>
    /// 退款表
    /// </summary>
    [Serializable]
    public partial class refund
    {
        public refund()
        { }

        #region Model
        private int _id;
        private string _order_no = "";
        private int _user_id = 0;
        private string _user_name = "";
        private decimal _refund_fee = 0M;
        private decimal _express_money = 0M;
        private decimal _refund_money = 0M;
        private DateTime? _apply_time = null;
        private DateTime? _affirm_time = null;
        private DateTime? _complete_time = null;
        private string _express_no = "";
        private int _refund_type = 0;
        private int _refund_status = 1;
        private string _refund_reason = "";
        private string _un_refund_reason = "";
        private string _refund_no = "";
        private string _express_code = "";
        private string _number = "";

        /// <summary>
        /// 自增ID
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 订单号
        /// </summary>
        public string order_no
        {
            set { _order_no = value; }
            get { return _order_no; }
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int user_id
        {
            set { _user_id = value; }
            get { return _user_id; }
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string user_name
        {
            set { _user_name = value; }
            get { return _user_name; }
        }
        /// <summary>
        /// 交易金额
        /// </summary>
        public decimal express_money
        {
            set { _express_money = value; }
            get { return _express_money; }
        }
        /// <summary>
        /// 退款金额
        /// </summary>
        public decimal refund_money
        {
            set { _refund_money = value; }
            get { return _refund_money; }
        }
        /// <summary>
        /// 退款手续费
        /// </summary>
        public decimal refund_fee
        {
            set { _refund_fee = value; }
            get { return _refund_fee; }
        }
        /// <summary>
        /// 申请退款时间
        /// </summary>
        public DateTime? apply_time
        {
            set { _apply_time = value; }
            get { return _apply_time; }
        }
        /// <summary>
        /// 确认退款时间
        /// </summary>
        public DateTime? affirm_time
        {
            set { _affirm_time = value; }
            get { return _affirm_time; }
        }
        /// <summary>
        /// 退款完成时间
        /// </summary>
        public DateTime? complete_time
        {
            set { _complete_time = value; }
            get { return _complete_time; }
        }
        /// <summary>
        /// 退款快递单号
        /// </summary>
        public string express_no
        {
            set { _express_no = value; }
            get { return _express_no; }
        }
        /// <summary>
        /// 退货方式(1、快递  2、上门取件)
        /// </summary>
        public int refund_type
        {
            set { _refund_type = value; }
            get { return _refund_type; }
        }
        /// <summary>
        /// 退款原因
        /// </summary>
        public string refund_reason
        {
            set { _refund_reason = value; }
            get { return _refund_reason; }
        }
        /// <summary>
        /// 退款驳回原因
        /// </summary>
        public string un_refund_reason
        {
            set { _un_refund_reason = value; }
            get { return _un_refund_reason; }
        }
        /// <summary>
        /// 退款状态 1生成退款单,2确认退款单,3完成退款单,4取消退款单,5驳回退款单
        /// </summary>
        public int refund_status
        {
            set { _refund_status = value; }
            get { return _refund_status; }
        }
        /// <summary>
        /// 退款单号
        /// </summary>
        public string refund_no
        {
            set { _refund_no = value; }
            get { return _refund_no; }
        }
        /// <summary>
        /// 快递代码
        /// </summary>
        public string express_code
        {
            set { _express_code = value; }
            get { return _express_code; }
        }
        /// <summary>
        /// 退货随机编号
        /// </summary>
        public string number
        {
            set { _number = value; }
            get { return _number; }
        }
        #endregion Model

        private List<order_goods> _order_goods;
        /// <summary>
        /// 商品列表
        /// </summary>
        public List<order_goods> order_goods
        {
            set { _order_goods = value; }
            get { return _order_goods; }
        }
    }
}