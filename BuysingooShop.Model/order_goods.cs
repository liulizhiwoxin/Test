using System;

namespace BuysingooShop.Model
{
    /// <summary>
    /// ������Ʒ��
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
        /// ����ID
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// ����ID
        /// </summary>
        public int order_id
        {
            set { _order_id = value; }
            get { return _order_id; }
        }
        /// <summary>
        /// ��ƷID
        /// </summary>
        public int goods_id
        {
            set { _goods_id = value; }
            get { return _goods_id; }
        }
        /// <summary>
        /// ��Ʒ����
        /// </summary>
        public string goods_title
        {
            set { _goods_title = value; }
            get { return _goods_title; }
        }
        /// <summary>
        /// ��Ʒ�۸�
        /// </summary>
        public decimal goods_price
        {
            set { _goods_price = value; }
            get { return _goods_price; }
        }

        /// <summary>
        /// ��Ʒ�۸�
        /// </summary>
        public string goods_pricestring
        {
            set { _goods_pricestring = value; }
            get { return _goods_pricestring; }
        }
        /// <summary>
        /// ʵ�ʼ۸�
        /// </summary>
        public decimal real_price
        {
            set { _real_price = value; }
            get { return _real_price; }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public int quantity
        {
            set { _quantity = value; }
            get { return _quantity; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public string quantitystring
        {
            set { _quantitystring = value; }
            get { return _quantitystring; }
        }
        /// <summary>
        /// ����,��������|��������
        /// </summary>
        public int point
        {
            set { _point = value; }
            get { return _point; }
        }
        /// <summary>
        /// ��ƷͼƬ
        /// </summary>
        public string goods_pic
        {
            set { _goods_pic = value; }
            get { return _goods_pic; }
        }
        /// <summary>
        /// �ۿۺ�
        /// </summary>
        public string strcode
        {
            set { _strcode = value; }
            get { return _strcode; }
        }
        /// <summary>
        /// ��Ʒ����
        /// </summary>
        public string type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// ��Ʒ�۸�С��
        /// </summary>
        public decimal total
        {
            set { _total = value; }
            get { return _total; }
        }
        /// <summary>
        /// ��Ʒ����
        /// </summary>
        public decimal weight
        {
            set { _weight = value; }
            get { return _weight; }
        }

        /// <summary>
        /// ��Ʒ����
        /// </summary>
        public string weightstring
        {
            set { _weightstring = value; }
            get { return _weightstring; }
        }
        /// <summary>
        /// ��Ʒ�ܶ�
        /// </summary>
        public string totalstring
        {
            set { _totalstring = value; }
            get { return _totalstring; }
        }
        #endregion Model

    }
}