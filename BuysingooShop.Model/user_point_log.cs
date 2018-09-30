using System;
namespace BuysingooShop.Model
{
    /// <summary>
    /// 积分记录日志
    /// </summary>
    [Serializable]
    public partial class user_point_log
    {
        public user_point_log()
        { }
        #region Model
        private int _id;
        private int _user_id;
        private string _user_name;
        private int _value = 0;
        private string _remark;
        private DateTime _add_time = DateTime.Now;
        private string _order_no = "";
        private int _pointtype = 0;
        private int _status = 0;
        private decimal _amount = 0M;
        private string _reason = "";
        private int _order_status = 0;
        private string _refund_no = "";

        /// <summary>
        /// 自增ID
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
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
        /// 增减积分
        /// </summary>
        public int value
        {
            set { _value = value; }
            get { return _value; }
        }
        /// <summary>
        /// 备注说明
        /// </summary>
        public string remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime add_time
        {
            set { _add_time = value; }
            get { return _add_time; }
        }
        /// <summary>
        /// 订单号|充值单号
        /// </summary>
        public string order_no
        {
            get { return _order_no; }
            set { _order_no = value; }
        }
        /// <summary>
        /// 1.充值
        /// 2.积分
        /// 3.退换货
        /// </summary>
        public int pointtype
        {
            get { return _pointtype; }
            set { _pointtype = value; }
        }
        /// <summary>
        /// 1.未充值
        /// 2.已充值
        /// </summary>
        public int status
        {
            get { return _status; }
            set { _status = value; }
        }
        /// <summary>
        /// 充值金额|退款金额
        /// </summary>
        public decimal amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        /// <summary>
        /// 驳回理由
        /// </summary>
        public string reason
        {
            get { return _reason; }
            set { _reason = value; }
        }
        /// <summary>
        /// 退货状态
        /// </summary>
        public int order_status
        {
            get { return _order_status; }
            set { _order_status = value; }
        }
        /// <summary>
        /// 退货单号
        /// </summary>
        public string refund_no
        {
            get { return _refund_no; }
            set { _refund_no = value; }
        }
        #endregion Model

    }
}