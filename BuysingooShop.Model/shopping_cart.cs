using System;
using System.Collections.Generic;
using System.Text;

namespace BuysingooShop.Model
{
    /// <summary>
    /// 购物车实体类
    /// </summary>
    [Serializable]
    public partial class cart_items
    {
        public cart_items()
        { }
        #region Model
        private int _id;
        private int _user_id;
        private string _title;
        private string _img_url;
        private decimal _price = 0M;
        private decimal _user_price = 0M;
        private decimal _total_price = 0M;
        private int _point = 0;
        private string _quantity = "";
        private int _stock_quantity = 0;
        private string _type = "";
        private string _weight = "";
        private int _is_checked = 0;
        private string _strcode ;

        /// <summary>
        /// 商品ID
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
        /// 商品名称
        /// </summary>
        public string title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 图片路径
        /// </summary>
        public string img_url
        {
            set { _img_url = value; }
            get { return _img_url; }
        }
        /// <summary>
        /// 销售单价
        /// </summary>
        public decimal price
        {
            set { _price = value; }
            get { return _price; }
        }
        /// <summary>
        /// 会员单价
        /// </summary>
        public decimal user_price
        {
            set { _user_price = value; }
            get { return _user_price; }
        }
        /// <summary>
        /// 价格小计
        /// </summary>
        public decimal total_price
        {
            set { _total_price = value; }
            get { return _total_price; }
        }
        /// <summary>
        /// 所需/获得积分
        /// </summary>
        public int point
        {
            set { _point = value; }
            get { return _point; }
        }
        /// <summary>
        /// 购买数量
        /// </summary>
        public string quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }
        /// <summary>
        /// 库存数量
        /// </summary>
        public int stock_quantity
        {
            set { _stock_quantity = value; }
            get { return _stock_quantity; }
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
        /// 商品重量
        /// </summary>
        public string weight
        {
            set { _weight = value; }
            get { return _weight; }
        }
        /// <summary>
        /// 是否选中
        /// </summary>
        public int is_checked
        {
            set { _is_checked = value; }
            get { return _is_checked; }
        }
        /// <summary>
        /// 折扣号
        /// </summary>
        public string strcode
        {
            set { _strcode = value; }
            get { return _strcode; }
        }
        #endregion
    }

    /// <summary>
    /// 购物车属性类
    /// </summary>
    [Serializable]
    public partial class cart_total
    {
        public cart_total()
        { }
        #region Model
        private int _total_num = 0;
        private int _total_quantity = 0;
        private decimal _payable_amount = 0M;
        private decimal _real_amount = 0M;
        private int _total_point = 0;
        private int _total_brand = 0;

        /// <summary>
        /// 商品种数
        /// </summary>
        public int total_num
        {
            set { _total_num = value; }
            get { return _total_num; }
        }
        /// <summary>
        /// 商品总数量
        /// </summary>
        public int total_quantity
        {
            set { _total_quantity = value; }
            get { return _total_quantity; }
        }
        /// <summary>
        /// 应付商品总金额
        /// </summary>
        public decimal payable_amount
        {
            set { _payable_amount = value; }
            get { return _payable_amount; }
        }
        /// <summary>
        /// 实付商品总金额
        /// </summary>
        public decimal real_amount
        {
            set { _real_amount = value; }
            get { return _real_amount; }
        }
        /// <summary>
        /// 总积分
        /// </summary>
        public int total_point
        {
            set { _total_point = value; }
            get { return _total_point; }
        }
        /// <summary>
        /// 总计几个品牌
        /// </summary>
        public int total_brand
        {
            set { _total_brand = value; }
            get { return _total_brand; }
        }
        #endregion
    }

    /// <summary>
    /// 收藏夹实体类
    /// </summary>
    public partial class collect_items
    {
        public collect_items()
        {
        }

        #region Model

        private int _id = 0;
        private string _title = "";
        private string _type = "";
        private string _price = "";
        private string _url_img = "";

        /// <summary>
        /// 商品ID
        /// </summary>
        public int id {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string title
        {
            get { return _title; }
            set { _title = value; }
        }
        /// <summary>
        /// 商品类别
        /// </summary>
        public string type
        {
            get { return _type; }
            set { _type = value; }
        }
        /// <summary>
        /// 商品价格
        /// </summary>
        public string price
        {
            get { return _price; }
            set { _price = value; }
        }
        /// <summary>
        /// 商品图片
        /// </summary>
        public string url_img
        {
            get { return _url_img; }
            set { _url_img = value; }
        }
        #endregion

    }
}
