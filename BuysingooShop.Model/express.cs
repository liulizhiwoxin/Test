using System;
namespace BuysingooShop.Model
{
    /// <summary>
    /// 物流快递
    /// </summary>
    [Serializable]
    public partial class express
    {
        public express()
        { }
        #region Model
        private int _id;
        private string _title;
        private string _express_code = "";
        private decimal _express_fee = 0M;
        private string _website = "";
        private string _remark = "";
        private int _sort_id = 99;
        private int _is_lock = 0;
        private string _img_url = "";
        private int _dealer_id = 0;

        /// <summary>
        /// 自增ID
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 快递名称
        /// </summary>
        public string title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 快递代号
        /// </summary>
        public string express_code
        {
            set { _express_code = value; }
            get { return _express_code; }
        }
        /// <summary>
        /// 配送费用
        /// </summary>
        public decimal express_fee
        {
            set { _express_fee = value; }
            get { return _express_fee; }
        }
        /// <summary>
        /// 快递网址
        /// </summary>
        public string website
        {
            set { _website = value; }
            get { return _website; }
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
        /// 排序
        /// </summary>
        public int sort_id
        {
            set { _sort_id = value; }
            get { return _sort_id; }
        }
        /// <summary>
        /// 是否启用0启用1禁用
        /// </summary>
        public int is_lock
        {
            set { _is_lock = value; }
            get { return _is_lock; }
        }
        /// <summary>
        /// 图片地址
        /// </summary>
        public string img_url
        {
            set { _img_url = value; }
            get { return _img_url; }
        }
        /// <summary>
        /// 经销商ID
        /// </summary>
        public int dealer_id
        {
            set { _dealer_id = value; }
            get { return _dealer_id; }
        }
        #endregion Model

    }
}