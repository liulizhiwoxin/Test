using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BuysingooShop.Model
{
    [Serializable]
    public class outlet
    {
        public outlet() { }

        private int _id;
        private string _name;
        private string _provinces;
        private string _city = "";
        private string _area = "";
        private string _street = "";
        private string _address = "";
        private string _mobile;
        private DateTime _addtime = DateTime.Now;
        private string _x_zb = "";
        private string _y_zb = "";
        private string _img;
        private string _busintime;
        private string _Linkman;
        private string _WeChat;
        private string _Other;
        private int _userId;
        /// <summary>
        /// 联系人
        /// </summary>
        public string Linkman
        {
            get { return _Linkman; }
            set { _Linkman = value; }
        }
        /// <summary>
        /// userid
        /// </summary>
        public int userId
        {
            get { return _userId; }
            set { _userId = value; }
        }
        /// <summary>
        /// 微信
        /// </summary>
        public string WeChat
        {
            get { return _WeChat; }
            set { _WeChat = value; }
        }
        /// <summary>
        /// 其它
        /// </summary>
        public string Other
        {
            get { return _Other; }
            set { _Other = value; }
        }

        /// <summary>
        /// 店铺ID
        /// </summary>
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 店铺名称
        /// </summary>
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }
        /// <summary>
        /// 城市
        /// </summary>
        public string city
        {
            get { return _city; }
            set { _city = value; }
        }
        /// <summary>
        /// 省份
        /// </summary>
        public string provinces
        {
            get { return _provinces; }
            set { _provinces = value; }
        }
        /// <summary>
        /// 区域
        /// </summary>
        public string area
        {
            get { return _area; }
            set { _area = value; }
        }
        /// <summary>
        /// 街道
        /// </summary>
        public string street
        {
            get { return _street; }
            set { _street = value; }
        }
        /// <summary>
        /// 地址
        /// </summary>
        public string address
        {
            get { return _address; }
            set { _address = value; }
        }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string mobile
        {
            get { return _mobile; }
            set { _mobile = value; }
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime addtime
        {
            get { return _addtime; }
            set { _addtime = value; }
        }
        /// <summary>
        /// X坐标
        /// </summary>
        public string x_zb
        {
            get { return _x_zb; }
            set { _x_zb = value; }
        }
        /// <summary>
        /// Y坐标
        /// </summary>
        public string y_zb
        {
            get { return _y_zb; }
            set { _y_zb = value; }
        }
        /// <summary>
        /// 图片
        /// </summary>
        public string img
        {
            get { return _img; }
            set { _img = value; }
        }
        /// <summary>
        /// 营业时间
        /// </summary>
        public string busintime
        {
            get { return _busintime; }
            set { _busintime = value; }
        }
    }
}
