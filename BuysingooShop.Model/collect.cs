using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BuysingooShop.Model
{
    [Serializable]
    public partial class collect
    {
        public collect() { }

        private int _id;
        private int _user_id;
        private int _good_id;
        private string _title = "";
        private string _good_type = "";
        private decimal _good_price = 0M;
        private string _img_url = "";
        private DateTime _add_time = DateTime.Now;
        private char _is_usable = '0';

        /// <summary>
        /// 商品ID
        /// </summary>
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 用户id
        /// </summary>
        public int user_id
        {
            get { return _user_id; }
            set { _user_id = value; }
        }
        /// <summary>
        /// 商品id
        /// </summary>
        public int good_id
        {
            get { return _good_id; }
            set { _good_id = value; }
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
        /// 商品类型
        /// </summary>
        public string good_type
        {
            get { return _good_type; }
            set { _good_type = value; }
        }
        /// <summary>
        /// 商品价格
        /// </summary>
        public decimal good_price
        {
            get { return _good_price; }
            set { _good_price = value; }
        }
        /// <summary>
        /// 商品图片
        /// </summary>
        public string img_url
        {
            get { return _img_url; }
            set { _img_url = value; }
        }
        /// <summary>
        /// 收藏时间
        /// </summary>
        public DateTime add_time
        {
            get { return _add_time; }
            set { _add_time = value; }
        }
        /// <summary>
        /// 商品价格
        /// </summary>
        public char is_usable
        {
            get { return _is_usable; }
            set { _is_usable = value; }
        }
        

    }
}
