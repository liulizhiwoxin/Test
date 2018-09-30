using System;
namespace BuysingooShop.Model
{
    /// <summary>
    /// 优惠券
    /// </summary>
    [Serializable]
    public partial class user_coupon
    {
        public user_coupon()
        { }
        #region Model
        private int _id;
        private string _title;
        private string _remark;
        private string _type;
        private decimal _amount = 0M;
        private string _str_code;
        private DateTime _add_time = DateTime.Now;
        private DateTime _start_time;
        private DateTime _end_time;
        private int _status=1;
        private int _userid = 1;
        private string _img_url = "";

        /// <summary>
        /// 自增ID
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 类型( 1 平台优惠券、2 品牌优惠券)
        /// </summary>
        public string type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal amount
        {
            set { _amount = value; }
            get { return _amount; }
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
        /// 生成时间
        /// </summary>
        public DateTime add_time
        {
            set { _add_time = value; }
            get { return _add_time; }
        }
        /// <summary>
        /// 优惠券开始时间
        /// </summary>
        public DateTime start_time
        {
            set { _start_time = value; }
            get { return _start_time; }
        }
        /// <summary>
        /// 优惠券截止时间
        /// </summary>
        public DateTime end_time
        {
            set { _end_time = value; }
            get { return _end_time; }
        }
        /// <summary>
        /// 状态(1未使用 2已使用 3已过期)
        /// </summary>
        public int status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int userid
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 优惠券图片
        /// </summary>
        public string img_url
        {
            set { _img_url = value; }
            get { return _img_url; }
        }
        #endregion Model

    }
}