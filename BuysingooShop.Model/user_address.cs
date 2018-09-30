using System;
namespace BuysingooShop.Model
{
    /// <summary>
    /// user_address:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class user_address
    {
        public user_address()
        { }
        #region Model
        private int _id;
        private int _user_id;
        private string _provinces;
        private string _citys;
        private string _area;
        private string _street;
        private string _address;
        private DateTime? _add_time;
        private DateTime? _modity_time;
        private int _is_default = 0;
        private string _mobile;
        private int _postcode;
        private string _acceptName;
        private string _post;
        private string _idstring;

        /// <summary>
        /// 主键
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// idstring
        /// </summary>
        public string idstring
        {
            set { _idstring = value; }
            get { return _idstring; }
        }
        /// <summary>
        /// 用户id
        /// </summary>
        public int user_id
        {
            set { _user_id = value; }
            get { return _user_id; }
        }
        /// <summary>
        /// 省份
        /// </summary>
        public string provinces
        {
            set { _provinces = value; }
            get { return _provinces; }
        }
        /// <summary>
        /// 城市
        /// </summary>
        public string citys
        {
            set { _citys = value; }
            get { return _citys; }
        }
        /// <summary>
        /// 区域
        /// </summary>
        public string area
        {
            set { _area = value; }
            get { return _area; }
        }
        /// <summary>
        /// 街道
        /// </summary>
        public string street
        {
            set { _street = value; }
            get { return _street; }
        }
        /// <summary>
        /// 详细地址
        /// </summary>
        public string address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? modity_time
        {
            set { _modity_time = value; }
            get { return _modity_time; }
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime? add_time
        {
            set { _add_time = value; }
            get { return _add_time; }
        }
        /// <summary>
        /// 是否默认（0否  1是）
        /// </summary>
        public int is_default
        {
            set { _is_default = value; }
            get { return _is_default; }
        }
        /// <summary>
        /// 手机号
        /// </summary>
        public string mobile
        {
            set { _mobile = value; }
            get { return _mobile; }
        }
        /// <summary>
        /// 邮编
        /// </summary>
        public int postcode
        {
            set { _postcode = value; }
            get { return _postcode; }
        }
        /// <summary>
        /// 收货人
        /// </summary>
        public string acceptName
        {
            set { _acceptName = value; }
            get { return _acceptName; }
        }
        /// <summary>
        /// 邮编(特殊)
        /// </summary>
        public string post
        {
            set { _post = value; }
            get { return _post; }
        }
        #endregion Model

    }
}

