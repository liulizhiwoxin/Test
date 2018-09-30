using System;

namespace BuysingooShop.Model
{
    /// <summary>
    /// 订单商品表
    /// </summary>
    [Serializable]
    public partial class order_goods
    {
        public order_goods()
        { }
        #region Model
        private int _id;
        private int _order_id = 0;
        private int _goods_id = 0;
        private string _goods_title = "";
        private decimal _goods_price = 0M;
        private decimal _real_price = 0M;
        private int _quantity = 0;
        private int _point = 0;
        private string _goods_pic = "";
        private string _strcode = "";
        private string _type = "";
        private decimal _total = 0M;
        private decimal _weight = 0M;
        private string _totalstring = "";
        private string _quantitystring = "";
        private string _goods_pricestring = "";
        private string _weightstring = "";

        /// <summary>
        /// 自增ID
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
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
        /// 商品ID
        /// </summary>
        public int goods_id
        {
            set { _goods_id = value; }
            get { return _goods_id; }
        }
        /// <summary>
        /// 商品标题
        /// </summary>
        public string goods_title
        {
            set { _goods_title = value; }
            get { return _goods_title; }
        }
        /// <summary>
        /// 商品价格
        /// </summary>
        public decimal goods_price
        {
            set { _goods_price = value; }
            get { return _goods_price; }
        }

        /// <summary>
        /// 商品价格
        /// </summary>
        public string goods_pricestring
        {
            set { _goods_pricestring = value; }
            get { return _goods_pricestring; }
        }
        /// <summary>
        /// 实际价格
        /// </summary>
        public decimal real_price
        {
            set { _real_price = value; }
            get { return _real_price; }
        }
        /// <summary>
        /// 订购数量
        /// </summary>
        public int quantity
        {
            set { _quantity = value; }
            get { return _quantity; }
        }

        /// <summary>
        /// 订购数量
        /// </summary>
        public string quantitystring
        {
            set { _quantitystring = value; }
            get { return _quantitystring; }
        }
        /// <summary>
        /// 积分,正数赠送|负数消费
        /// </summary>
        public int point
        {
            set { _point = value; }
            get { return _point; }
        }
        /// <summary>
        /// 商品图片
        /// </summary>
        public string goods_pic
        {
            set { _goods_pic = value; }
            get { return _goods_pic; }
        }
        /// <summary>
        /// 折扣号
        /// </summary>
        public string strcode
        {
            set { _strcode = value; }
            get { return _strcode; }
        }
        /// <summary>
        /// 商品类型
        /// </summary>
        public string type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 商品价格小计
        /// </summary>
        public decimal total
        {
            set { _total = value; }
            get { return _total; }
        }
        /// <summary>
        /// 商品重量
        /// </summary>
        public decimal weight
        {
            set { _weight = value; }
            get { return _weight; }
        }

        /// <summary>
        /// 商品重量
        /// </summary>
        public string weightstring
        {
            set { _weightstring = value; }
            get { return _weightstring; }
        }
        /// <summary>
        /// 商品总额
        /// </summary>
        public string totalstring
        {
            set { _totalstring = value; }
            get { return _totalstring; }
        }
        #endregion Model

    }
}