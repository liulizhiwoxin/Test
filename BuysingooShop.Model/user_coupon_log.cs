using System;
namespace BuysingooShop.Model
{
    /// <summary>
    /// 优惠券
    /// </summary>
    [Serializable]
    public partial class user_coupon_log
    {
        public user_coupon_log()
        { }
        #region Model
        private int _id;
        private int _user_id;
        private string _user_name;
        private int _coupon_id;
        private string _str_code;
        private int _order_id;
        private string _order_no;
        private DateTime _add_time = DateTime.Now;
        private DateTime _use_time;
        private int _status=0;

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
        /// 优惠券ID
        /// </summary>
        public int coupon_id
        {
            set { _coupon_id = value; }
            get { return _coupon_id; }
        }
        /// <summary>
        /// sn码
        /// </summary>
        public string str_code
        {
            set { _str_code = value; }
            get { return _str_code; }
        }
        /// <summary>
        /// 订单ID
        /// </summary>
        public int order_id
        {
            set { _order_id = value; }
            get { return _order_id; }
        }
        /// <summary>
        /// 订单单号
        /// </summary>
        public string order_no
        {
            set { _order_no = value; }
            get { return _order_no; }
        }
        /// <summary>
        /// 生成时间
        /// </summary>
        public DateTime add_time
        {
            set { _add_time = value; }
            get { return _add_time; }
        }
        /// <summary>
        /// 使用时间
        /// </summary>
        public DateTime use_time
        {
            set { _use_time = value; }
            get { return _use_time; }
        }
        /// <summary>
        /// 状态(1未使用 2已使用 3已过期)
        /// </summary>
        public int status
        {
            set { _status = value; }
            get { return _status; }
        }
        #endregion Model

    }
}