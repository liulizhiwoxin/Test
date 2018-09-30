using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BuysingooShop.Model
{
    [Serializable]
    public class openarea
    {
        public openarea() { }

        private int _id;
        private string _provinces;
        private string _city = "";

        /// <summary>
        /// 开放区域ID
        /// </summary>
        public int id
        {
            get { return _id; }
            set { _id = value; }
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
    }
}
